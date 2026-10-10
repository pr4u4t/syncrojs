using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.DataBase;
using SyncroC.Utility;

//ABELLO_FI10/12/2010_1 Le velocità di giunta sul dettaglio ordine wet end erano sbagliate. Quindi le ho tolte.
namespace SyncroC.Forms.Pages.Production.OrdersIns
{
	public class Frm_InsWetOrder : SyncroC.Forms.Pages.Production.OrdersIns.Frm_OrdersInsSubGroup
	{
		private SyncroC.Controls.TitledNumericUpDown nud_ProgramLength;
		private SyncroC.Controls.TitledTextBox ttb_index;
		private SyncroC.Controls.TitledNumericUpDown nud_WebWidth;
		private SyncroC.Controls.TitledTextBox ttb_ProgramNumber;
		private PictureBox pct1;
		private PictureBox pct3;
		private PictureBox pct2;
		private PictureBox pct0;
		private PictureBox pct6;
		private PictureBox pct4;
		private PictureBox pct5;
		private SyncroC.Controls.Title ttl_Quality;
		private SyncroC.Controls.TitledTextBox ttb_qualityText;
    private SyncroC.Controls.TitledNumericUpDown tnud_WeightFactor6;
		private SyncroC.Controls.TitledNumericUpDown tnud_WeightFactor5;
		private SyncroC.Controls.TitledNumericUpDown tnud_WeightFactor4;
		private SyncroC.Controls.TitledNumericUpDown tnud_WeightFactor3;
		private SyncroC.Controls.TitledNumericUpDown tnud_WeightFactor2;
		private SyncroC.Controls.TitledNumericUpDown tnud_WeightFactor1;
    private SyncroC.Controls.TitledNumericUpDown tnud_WeightFactor0;
		private System.ComponentModel.IContainer components = null;
		private SyncroC.Controls.TitledNumericUpDown tnud_WeightFactor;
		private SyncroC.Controls.TitledNumericUpDown tnud_Thickness;
		private SyncroC.Controls.Bracket brk_papers;
		private SyncroC.Controls.TitledTextBox ttb_quality;
		private SyncroC.Controls.TitledTextBox ttb_paper6;
		private SyncroC.Controls.TitledTextBox ttb_paper4;
		private SyncroC.Controls.TitledTextBox ttb_paper3;
		private SyncroC.Controls.TitledTextBox ttb_paper2;
		private SyncroC.Controls.TitledTextBox ttb_paper1;
		private SyncroC.Controls.TitledTextBox ttb_paper0;
		private SyncroC.Controls.TitledTextBox ttb_paper5;
		private SyncroC.Controls.TitledTextBox ttb_Note;
		private DataTable program;

		public Frm_InsWetOrder()
		{
			InitializeComponent();

      program = DBManager.getDBManager().order.order.Tables[Order.WETEND_LIST];

			showProgram(indexOrderToShowWET);
			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Excludes components of this form or of all the inheritors from the language database.
		/// To include them into the database let this method return true, or remove it.
		/// </summary>
		/// <returns></returns>
		protected override bool IncludeComponentsIntoLanguageDB()
		{
			return true;
		}

		protected override void F12()
		{
		}

		/// <summary>
		/// Show previous order.
		/// </summary>
		protected override void PageUp()
		{
			if (indexOrderToShowWET == 0)
				return;
			showProgram(--indexOrderToShowWET);
		}

		/// <summary>
		/// Show next order.
		/// </summary>
		protected override void PageDown()
		{
			if (indexOrderToShowWET == program.Rows.Count - 1)
				return;
			showProgram(++indexOrderToShowWET);
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_InsWetOrder));
      this.nud_ProgramLength = new SyncroC.Controls.TitledNumericUpDown();
      this.ttb_index = new SyncroC.Controls.TitledTextBox();
      this.nud_WebWidth = new SyncroC.Controls.TitledNumericUpDown();
      this.ttb_ProgramNumber = new SyncroC.Controls.TitledTextBox();
      this.pct1 = new System.Windows.Forms.PictureBox();
      this.pct3 = new System.Windows.Forms.PictureBox();
      this.pct2 = new System.Windows.Forms.PictureBox();
      this.pct0 = new System.Windows.Forms.PictureBox();
      this.pct6 = new System.Windows.Forms.PictureBox();
      this.pct4 = new System.Windows.Forms.PictureBox();
      this.pct5 = new System.Windows.Forms.PictureBox();
      this.ttl_Quality = new SyncroC.Controls.Title();
      this.ttb_qualityText = new SyncroC.Controls.TitledTextBox();
      this.tnud_WeightFactor6 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_WeightFactor5 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_WeightFactor4 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_WeightFactor3 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_WeightFactor2 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_WeightFactor1 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_WeightFactor0 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_WeightFactor = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_Thickness = new SyncroC.Controls.TitledNumericUpDown();
      this.brk_papers = new SyncroC.Controls.Bracket();
      this.ttb_quality = new SyncroC.Controls.TitledTextBox();
      this.ttb_paper6 = new SyncroC.Controls.TitledTextBox();
      this.ttb_paper4 = new SyncroC.Controls.TitledTextBox();
      this.ttb_paper3 = new SyncroC.Controls.TitledTextBox();
      this.ttb_paper2 = new SyncroC.Controls.TitledTextBox();
      this.ttb_paper1 = new SyncroC.Controls.TitledTextBox();
      this.ttb_paper0 = new SyncroC.Controls.TitledTextBox();
      this.ttb_paper5 = new SyncroC.Controls.TitledTextBox();
      this.ttb_Note = new SyncroC.Controls.TitledTextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct0)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct6)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct4)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct5)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabC
      // 
      this.tab_TabC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // nud_ProgramLength
      // 
      this.nud_ProgramLength.DecimalPlaces = 0;
      this.nud_ProgramLength.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ProgramLength.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ProgramLength.ImagePassword = null;
      this.nud_ProgramLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ProgramLength.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_ProgramLength.Location = new System.Drawing.Point(798, 193);
      this.nud_ProgramLength.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_ProgramLength.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ProgramLength.Name = "nud_ProgramLength";
      this.nud_ProgramLength.ReadOnly = true;
      this.nud_ProgramLength.Size = new System.Drawing.Size(184, 63);
      this.nud_ProgramLength.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_ProgramLength.TabIndex = 219;
      this.nud_ProgramLength.TabStop = false;
      this.nud_ProgramLength.Text = "PROGRAM LENGTH";
      this.nud_ProgramLength.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ProgramLength.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ProgramLength.TranslateText = true;
      this.nud_ProgramLength.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_ProgramLength.Value = "";
      this.nud_ProgramLength.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ProgramLength.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // ttb_index
      // 
      this.ttb_index.CausesValidation = false;
      this.ttb_index.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_index.Font = new System.Drawing.Font("Arial Unicode MS", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_index.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_index.ImagePassword = null;
      this.ttb_index.Location = new System.Drawing.Point(30, 193);
      this.ttb_index.MaxChar = 2;
      this.ttb_index.Multiline = false;
      this.ttb_index.Name = "ttb_index";
      this.ttb_index.PasswordChar = '\0';
      this.ttb_index.ReadOnly = true;
      this.ttb_index.Size = new System.Drawing.Size(171, 113);
      this.ttb_index.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_index.TabIndex = 218;
      this.ttb_index.TabStop = false;
      this.ttb_index.Text = "QUEUE";
      this.ttb_index.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_index.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_index.TranslateText = true;
      this.ttb_index.Value = " ";
      this.ttb_index.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // nud_WebWidth
      // 
      this.nud_WebWidth.DecimalPlaces = 0;
      this.nud_WebWidth.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_WebWidth.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_WebWidth.ImagePassword = null;
      this.nud_WebWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_WebWidth.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_WebWidth.Location = new System.Drawing.Point(990, 193);
      this.nud_WebWidth.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_WebWidth.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_WebWidth.Name = "nud_WebWidth";
      this.nud_WebWidth.ReadOnly = true;
      this.nud_WebWidth.Size = new System.Drawing.Size(211, 63);
      this.nud_WebWidth.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_WebWidth.TabIndex = 217;
      this.nud_WebWidth.Text = "WEB WIDTH";
      this.nud_WebWidth.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_WebWidth.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_WebWidth.TranslateText = true;
      this.nud_WebWidth.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_WebWidth.Value = "";
      this.nud_WebWidth.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_WebWidth.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // ttb_ProgramNumber
      // 
      this.ttb_ProgramNumber.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_ProgramNumber.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_ProgramNumber.ForeColor = System.Drawing.Color.Black;
      this.ttb_ProgramNumber.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ProgramNumber.ImagePassword = null;
      this.ttb_ProgramNumber.Location = new System.Drawing.Point(207, 193);
      this.ttb_ProgramNumber.MaxChar = 30;
      this.ttb_ProgramNumber.Multiline = false;
      this.ttb_ProgramNumber.Name = "ttb_ProgramNumber";
      this.ttb_ProgramNumber.PasswordChar = '\0';
      this.ttb_ProgramNumber.ReadOnly = true;
      this.ttb_ProgramNumber.Size = new System.Drawing.Size(585, 63);
      this.ttb_ProgramNumber.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_ProgramNumber.TabIndex = 214;
      this.ttb_ProgramNumber.Text = "PROGRAM NUMBER";
      this.ttb_ProgramNumber.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ProgramNumber.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ProgramNumber.TranslateText = true;
      this.ttb_ProgramNumber.Value = " ";
      this.ttb_ProgramNumber.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // pct1
      // 
      this.pct1.Image = global::SyncroC.Properties.Resources.medium;
      this.pct1.Location = new System.Drawing.Point(1151, 608);
      this.pct1.Name = "pct1";
      this.pct1.Size = new System.Drawing.Size(50, 40);
      this.pct1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct1.TabIndex = 235;
      this.pct1.TabStop = false;
      // 
      // pct3
      // 
      this.pct3.Image = global::SyncroC.Properties.Resources.medium;
      this.pct3.Location = new System.Drawing.Point(1151, 508);
      this.pct3.Name = "pct3";
      this.pct3.Size = new System.Drawing.Size(50, 40);
      this.pct3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct3.TabIndex = 234;
      this.pct3.TabStop = false;
      // 
      // pct2
      // 
      this.pct2.Image = global::SyncroC.Properties.Resources.liner;
      this.pct2.Location = new System.Drawing.Point(1151, 558);
      this.pct2.Name = "pct2";
      this.pct2.Size = new System.Drawing.Size(50, 40);
      this.pct2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct2.TabIndex = 233;
      this.pct2.TabStop = false;
      // 
      // pct0
      // 
      this.pct0.Image = global::SyncroC.Properties.Resources.liner;
      this.pct0.Location = new System.Drawing.Point(1151, 658);
      this.pct0.Name = "pct0";
      this.pct0.Size = new System.Drawing.Size(50, 40);
      this.pct0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct0.TabIndex = 232;
      this.pct0.TabStop = false;
      // 
      // pct6
      // 
      this.pct6.Image = global::SyncroC.Properties.Resources.liner;
      this.pct6.Location = new System.Drawing.Point(1151, 358);
      this.pct6.Name = "pct6";
      this.pct6.Size = new System.Drawing.Size(50, 40);
      this.pct6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct6.TabIndex = 231;
      this.pct6.TabStop = false;
      // 
      // pct4
      // 
      this.pct4.Image = global::SyncroC.Properties.Resources.liner;
      this.pct4.Location = new System.Drawing.Point(1151, 458);
      this.pct4.Name = "pct4";
      this.pct4.Size = new System.Drawing.Size(50, 40);
      this.pct4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct4.TabIndex = 230;
      this.pct4.TabStop = false;
      // 
      // pct5
      // 
      this.pct5.Image = global::SyncroC.Properties.Resources.medium;
      this.pct5.Location = new System.Drawing.Point(1151, 408);
      this.pct5.Name = "pct5";
      this.pct5.Size = new System.Drawing.Size(50, 40);
      this.pct5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct5.TabIndex = 229;
      this.pct5.TabStop = false;
      // 
      // ttl_Quality
      // 
      this.ttl_Quality.BackColor = System.Drawing.Color.Black;
      this.ttl_Quality.CausesValidation = false;
      this.ttl_Quality.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Quality.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Quality.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Quality.Location = new System.Drawing.Point(30, 329);
      this.ttl_Quality.Name = "ttl_Quality";
      this.ttl_Quality.Size = new System.Drawing.Size(500, 400);
      this.ttl_Quality.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Quality.TabIndex = 236;
      this.ttl_Quality.Text = "QUALITY";
      this.ttl_Quality.TranslateText = true;
      // 
      // ttb_qualityText
      // 
      this.ttb_qualityText.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_qualityText.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_qualityText.ForeColor = System.Drawing.Color.Black;
      this.ttb_qualityText.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_qualityText.ImagePassword = null;
      this.ttb_qualityText.Location = new System.Drawing.Point(42, 458);
      this.ttb_qualityText.MaxChar = 60;
      this.ttb_qualityText.Multiline = true;
      this.ttb_qualityText.Name = "ttb_qualityText";
      this.ttb_qualityText.PasswordChar = '\0';
      this.ttb_qualityText.ReadOnly = true;
      this.ttb_qualityText.Size = new System.Drawing.Size(488, 60);
      this.ttb_qualityText.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_qualityText.TabIndex = 237;
      this.ttb_qualityText.Text = "Text";
      this.ttb_qualityText.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_qualityText.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_qualityText.TranslateText = true;
      this.ttb_qualityText.Value = " ";
      this.ttb_qualityText.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tnud_WeightFactor6
      // 
      this.tnud_WeightFactor6.DecimalPlaces = 0;
      this.tnud_WeightFactor6.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_WeightFactor6.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor6.ImagePassword = null;
      this.tnud_WeightFactor6.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WeightFactor6.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor6.Location = new System.Drawing.Point(1015, 359);
      this.tnud_WeightFactor6.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_WeightFactor6.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WeightFactor6.Name = "tnud_WeightFactor6";
      this.tnud_WeightFactor6.ReadOnly = true;
      this.tnud_WeightFactor6.Size = new System.Drawing.Size(125, 42);
      this.tnud_WeightFactor6.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_WeightFactor6.TabIndex = 238;
      this.tnud_WeightFactor6.TabStop = false;
      this.tnud_WeightFactor6.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor6.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor6.TranslateText = true;
      this.tnud_WeightFactor6.UnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor6.Value = "0";
      this.tnud_WeightFactor6.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor6.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_WeightFactor5
      // 
      this.tnud_WeightFactor5.DecimalPlaces = 0;
      this.tnud_WeightFactor5.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_WeightFactor5.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor5.ImagePassword = null;
      this.tnud_WeightFactor5.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WeightFactor5.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor5.Location = new System.Drawing.Point(1015, 406);
      this.tnud_WeightFactor5.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_WeightFactor5.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WeightFactor5.Name = "tnud_WeightFactor5";
      this.tnud_WeightFactor5.ReadOnly = true;
      this.tnud_WeightFactor5.Size = new System.Drawing.Size(125, 42);
      this.tnud_WeightFactor5.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_WeightFactor5.TabIndex = 240;
      this.tnud_WeightFactor5.TabStop = false;
      this.tnud_WeightFactor5.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor5.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor5.TranslateText = true;
      this.tnud_WeightFactor5.UnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor5.Value = "0";
      this.tnud_WeightFactor5.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor5.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_WeightFactor4
      // 
      this.tnud_WeightFactor4.DecimalPlaces = 0;
      this.tnud_WeightFactor4.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_WeightFactor4.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor4.ImagePassword = null;
      this.tnud_WeightFactor4.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WeightFactor4.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor4.Location = new System.Drawing.Point(1015, 459);
      this.tnud_WeightFactor4.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_WeightFactor4.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WeightFactor4.Name = "tnud_WeightFactor4";
      this.tnud_WeightFactor4.ReadOnly = true;
      this.tnud_WeightFactor4.Size = new System.Drawing.Size(125, 42);
      this.tnud_WeightFactor4.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_WeightFactor4.TabIndex = 241;
      this.tnud_WeightFactor4.TabStop = false;
      this.tnud_WeightFactor4.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor4.TranslateText = true;
      this.tnud_WeightFactor4.UnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor4.Value = "0";
      this.tnud_WeightFactor4.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor4.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_WeightFactor3
      // 
      this.tnud_WeightFactor3.DecimalPlaces = 0;
      this.tnud_WeightFactor3.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_WeightFactor3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor3.ImagePassword = null;
      this.tnud_WeightFactor3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WeightFactor3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor3.Location = new System.Drawing.Point(1015, 509);
      this.tnud_WeightFactor3.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_WeightFactor3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WeightFactor3.Name = "tnud_WeightFactor3";
      this.tnud_WeightFactor3.ReadOnly = true;
      this.tnud_WeightFactor3.Size = new System.Drawing.Size(125, 42);
      this.tnud_WeightFactor3.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_WeightFactor3.TabIndex = 242;
      this.tnud_WeightFactor3.TabStop = false;
      this.tnud_WeightFactor3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor3.TranslateText = true;
      this.tnud_WeightFactor3.UnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor3.Value = "0";
      this.tnud_WeightFactor3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_WeightFactor2
      // 
      this.tnud_WeightFactor2.DecimalPlaces = 0;
      this.tnud_WeightFactor2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_WeightFactor2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor2.ImagePassword = null;
      this.tnud_WeightFactor2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WeightFactor2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor2.Location = new System.Drawing.Point(1015, 559);
      this.tnud_WeightFactor2.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_WeightFactor2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WeightFactor2.Name = "tnud_WeightFactor2";
      this.tnud_WeightFactor2.ReadOnly = true;
      this.tnud_WeightFactor2.Size = new System.Drawing.Size(125, 42);
      this.tnud_WeightFactor2.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_WeightFactor2.TabIndex = 243;
      this.tnud_WeightFactor2.TabStop = false;
      this.tnud_WeightFactor2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor2.TranslateText = true;
      this.tnud_WeightFactor2.UnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor2.Value = "0";
      this.tnud_WeightFactor2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_WeightFactor1
      // 
      this.tnud_WeightFactor1.DecimalPlaces = 0;
      this.tnud_WeightFactor1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_WeightFactor1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor1.ImagePassword = null;
      this.tnud_WeightFactor1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WeightFactor1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor1.Location = new System.Drawing.Point(1015, 609);
      this.tnud_WeightFactor1.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_WeightFactor1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WeightFactor1.Name = "tnud_WeightFactor1";
      this.tnud_WeightFactor1.ReadOnly = true;
      this.tnud_WeightFactor1.Size = new System.Drawing.Size(125, 42);
      this.tnud_WeightFactor1.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_WeightFactor1.TabIndex = 244;
      this.tnud_WeightFactor1.TabStop = false;
      this.tnud_WeightFactor1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor1.TranslateText = true;
      this.tnud_WeightFactor1.UnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor1.Value = "0";
      this.tnud_WeightFactor1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_WeightFactor0
      // 
      this.tnud_WeightFactor0.DecimalPlaces = 0;
      this.tnud_WeightFactor0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_WeightFactor0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor0.ImagePassword = null;
      this.tnud_WeightFactor0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WeightFactor0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor0.Location = new System.Drawing.Point(1015, 658);
      this.tnud_WeightFactor0.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_WeightFactor0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WeightFactor0.Name = "tnud_WeightFactor0";
      this.tnud_WeightFactor0.ReadOnly = true;
      this.tnud_WeightFactor0.Size = new System.Drawing.Size(125, 42);
      this.tnud_WeightFactor0.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.tnud_WeightFactor0.TabIndex = 245;
      this.tnud_WeightFactor0.TabStop = false;
      this.tnud_WeightFactor0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor0.TranslateText = true;
      this.tnud_WeightFactor0.UnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor0.Value = "0";
      this.tnud_WeightFactor0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_WeightFactor
      // 
      this.tnud_WeightFactor.DecimalPlaces = 0;
      this.tnud_WeightFactor.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_WeightFactor.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor.ImagePassword = null;
      this.tnud_WeightFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WeightFactor.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_WeightFactor.Location = new System.Drawing.Point(42, 557);
      this.tnud_WeightFactor.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.tnud_WeightFactor.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WeightFactor.Name = "tnud_WeightFactor";
      this.tnud_WeightFactor.ReadOnly = true;
      this.tnud_WeightFactor.Size = new System.Drawing.Size(185, 70);
      this.tnud_WeightFactor.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_WeightFactor.TabIndex = 252;
      this.tnud_WeightFactor.TabStop = false;
      this.tnud_WeightFactor.Text = "Weight Factor";
      this.tnud_WeightFactor.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor.TranslateText = true;
      this.tnud_WeightFactor.UnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor.Value = "0";
      this.tnud_WeightFactor.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_Thickness
      // 
      this.tnud_Thickness.DecimalPlaces = 0;
      this.tnud_Thickness.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Thickness.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Thickness.ImagePassword = null;
      this.tnud_Thickness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Thickness.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_Thickness.Location = new System.Drawing.Point(345, 557);
      this.tnud_Thickness.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.tnud_Thickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Thickness.Name = "tnud_Thickness";
      this.tnud_Thickness.ReadOnly = true;
      this.tnud_Thickness.Size = new System.Drawing.Size(185, 70);
      this.tnud_Thickness.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Thickness.TabIndex = 253;
      this.tnud_Thickness.TabStop = false;
      this.tnud_Thickness.Text = "Thickness";
      this.tnud_Thickness.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Thickness.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Thickness.TranslateText = true;
      this.tnud_Thickness.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tnud_Thickness.Value = "10";
      this.tnud_Thickness.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Thickness.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // brk_papers
      // 
      this.brk_papers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.brk_papers.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.brk_papers.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_papers.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_papers.Location = new System.Drawing.Point(562, 329);
      this.brk_papers.Name = "brk_papers";
      this.brk_papers.Position = SyncroC.Controls.BracketPosition.Right;
      this.brk_papers.ShowArrow = true;
      this.brk_papers.Size = new System.Drawing.Size(639, 400);
      this.brk_papers.TabIndex = 254;
      this.brk_papers.Text = "PAPERS";
      this.brk_papers.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_papers.TranslateText = true;
      // 
      // ttb_quality
      // 
      this.ttb_quality.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_quality.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_quality.ForeColor = System.Drawing.Color.Black;
      this.ttb_quality.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_quality.ImagePassword = null;
      this.ttb_quality.Location = new System.Drawing.Point(42, 359);
      this.ttb_quality.MaxChar = 18;
      this.ttb_quality.Multiline = false;
      this.ttb_quality.Name = "ttb_quality";
      this.ttb_quality.PasswordChar = '\0';
      this.ttb_quality.ReadOnly = true;
      this.ttb_quality.Size = new System.Drawing.Size(488, 60);
      this.ttb_quality.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_quality.TabIndex = 255;
      this.ttb_quality.Text = "Quality";
      this.ttb_quality.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_quality.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_quality.TranslateText = true;
      this.ttb_quality.Value = " ";
      this.ttb_quality.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_paper6
      // 
      this.ttb_paper6.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_paper6.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_paper6.ForeColor = System.Drawing.Color.Black;
      this.ttb_paper6.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper6.ImagePassword = null;
      this.ttb_paper6.Location = new System.Drawing.Point(605, 359);
      this.ttb_paper6.MaxChar = 18;
      this.ttb_paper6.Multiline = false;
      this.ttb_paper6.Name = "ttb_paper6";
      this.ttb_paper6.PasswordChar = '\0';
      this.ttb_paper6.ReadOnly = true;
      this.ttb_paper6.Size = new System.Drawing.Size(397, 42);
      this.ttb_paper6.Style = SyncroC.Controls.TitledTextBoxStyles.NoTitle;
      this.ttb_paper6.TabIndex = 256;
      this.ttb_paper6.Text = "Quality";
      this.ttb_paper6.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_paper6.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper6.TranslateText = true;
      this.ttb_paper6.Value = " ";
      this.ttb_paper6.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_paper4
      // 
      this.ttb_paper4.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_paper4.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_paper4.ForeColor = System.Drawing.Color.Black;
      this.ttb_paper4.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper4.ImagePassword = null;
      this.ttb_paper4.Location = new System.Drawing.Point(605, 459);
      this.ttb_paper4.MaxChar = 18;
      this.ttb_paper4.Multiline = false;
      this.ttb_paper4.Name = "ttb_paper4";
      this.ttb_paper4.PasswordChar = '\0';
      this.ttb_paper4.ReadOnly = true;
      this.ttb_paper4.Size = new System.Drawing.Size(397, 42);
      this.ttb_paper4.Style = SyncroC.Controls.TitledTextBoxStyles.NoTitle;
      this.ttb_paper4.TabIndex = 257;
      this.ttb_paper4.Text = "Quality";
      this.ttb_paper4.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_paper4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper4.TranslateText = true;
      this.ttb_paper4.Value = " ";
      this.ttb_paper4.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_paper3
      // 
      this.ttb_paper3.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_paper3.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_paper3.ForeColor = System.Drawing.Color.Black;
      this.ttb_paper3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper3.ImagePassword = null;
      this.ttb_paper3.Location = new System.Drawing.Point(605, 509);
      this.ttb_paper3.MaxChar = 18;
      this.ttb_paper3.Multiline = false;
      this.ttb_paper3.Name = "ttb_paper3";
      this.ttb_paper3.PasswordChar = '\0';
      this.ttb_paper3.ReadOnly = true;
      this.ttb_paper3.Size = new System.Drawing.Size(397, 42);
      this.ttb_paper3.Style = SyncroC.Controls.TitledTextBoxStyles.NoTitle;
      this.ttb_paper3.TabIndex = 258;
      this.ttb_paper3.Text = "Quality";
      this.ttb_paper3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_paper3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper3.TranslateText = true;
      this.ttb_paper3.Value = " ";
      this.ttb_paper3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_paper2
      // 
      this.ttb_paper2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_paper2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_paper2.ForeColor = System.Drawing.Color.Black;
      this.ttb_paper2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper2.ImagePassword = null;
      this.ttb_paper2.Location = new System.Drawing.Point(605, 559);
      this.ttb_paper2.MaxChar = 18;
      this.ttb_paper2.Multiline = false;
      this.ttb_paper2.Name = "ttb_paper2";
      this.ttb_paper2.PasswordChar = '\0';
      this.ttb_paper2.ReadOnly = true;
      this.ttb_paper2.Size = new System.Drawing.Size(397, 42);
      this.ttb_paper2.Style = SyncroC.Controls.TitledTextBoxStyles.NoTitle;
      this.ttb_paper2.TabIndex = 259;
      this.ttb_paper2.Text = "Quality";
      this.ttb_paper2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_paper2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper2.TranslateText = true;
      this.ttb_paper2.Value = " ";
      this.ttb_paper2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_paper1
      // 
      this.ttb_paper1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_paper1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_paper1.ForeColor = System.Drawing.Color.Black;
      this.ttb_paper1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper1.ImagePassword = null;
      this.ttb_paper1.Location = new System.Drawing.Point(605, 609);
      this.ttb_paper1.MaxChar = 18;
      this.ttb_paper1.Multiline = false;
      this.ttb_paper1.Name = "ttb_paper1";
      this.ttb_paper1.PasswordChar = '\0';
      this.ttb_paper1.ReadOnly = true;
      this.ttb_paper1.Size = new System.Drawing.Size(397, 42);
      this.ttb_paper1.Style = SyncroC.Controls.TitledTextBoxStyles.NoTitle;
      this.ttb_paper1.TabIndex = 260;
      this.ttb_paper1.Text = "Quality";
      this.ttb_paper1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_paper1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper1.TranslateText = true;
      this.ttb_paper1.Value = " ";
      this.ttb_paper1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_paper0
      // 
      this.ttb_paper0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_paper0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_paper0.ForeColor = System.Drawing.Color.Black;
      this.ttb_paper0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper0.ImagePassword = null;
      this.ttb_paper0.Location = new System.Drawing.Point(605, 658);
      this.ttb_paper0.MaxChar = 18;
      this.ttb_paper0.Multiline = false;
      this.ttb_paper0.Name = "ttb_paper0";
      this.ttb_paper0.PasswordChar = '\0';
      this.ttb_paper0.ReadOnly = true;
      this.ttb_paper0.Size = new System.Drawing.Size(397, 42);
      this.ttb_paper0.Style = SyncroC.Controls.TitledTextBoxStyles.NoTitle;
      this.ttb_paper0.TabIndex = 261;
      this.ttb_paper0.Text = "Quality";
      this.ttb_paper0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_paper0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper0.TranslateText = true;
      this.ttb_paper0.Value = " ";
      this.ttb_paper0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_paper5
      // 
      this.ttb_paper5.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_paper5.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_paper5.ForeColor = System.Drawing.Color.Black;
      this.ttb_paper5.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper5.ImagePassword = null;
      this.ttb_paper5.Location = new System.Drawing.Point(605, 406);
      this.ttb_paper5.MaxChar = 18;
      this.ttb_paper5.Multiline = false;
      this.ttb_paper5.Name = "ttb_paper5";
      this.ttb_paper5.PasswordChar = '\0';
      this.ttb_paper5.ReadOnly = true;
      this.ttb_paper5.Size = new System.Drawing.Size(397, 42);
      this.ttb_paper5.Style = SyncroC.Controls.TitledTextBoxStyles.NoTitle;
      this.ttb_paper5.TabIndex = 262;
      this.ttb_paper5.Text = "Quality";
      this.ttb_paper5.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_paper5.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_paper5.TranslateText = true;
      this.ttb_paper5.Value = " ";
      this.ttb_paper5.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_Note
      // 
      this.ttb_Note.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_Note.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Note.ForeColor = System.Drawing.Color.Black;
      this.ttb_Note.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Note.ImagePassword = null;
      this.ttb_Note.Location = new System.Drawing.Point(207, 263);
      this.ttb_Note.MaxChar = 30;
      this.ttb_Note.Multiline = false;
      this.ttb_Note.Name = "ttb_Note";
      this.ttb_Note.PasswordChar = '\0';
      this.ttb_Note.ReadOnly = true;
      this.ttb_Note.Size = new System.Drawing.Size(994, 63);
      this.ttb_Note.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_Note.TabIndex = 263;
      this.ttb_Note.Text = "NOTE";
      this.ttb_Note.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Note.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_Note.TranslateText = true;
      this.ttb_Note.Value = "Ciao a tutti queste sono note per produrre una buona qualità";
      this.ttb_Note.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // Frm_InsWetOrder
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.ttb_Note);
      this.Controls.Add(this.ttb_paper5);
      this.Controls.Add(this.ttb_paper0);
      this.Controls.Add(this.ttb_paper1);
      this.Controls.Add(this.ttb_paper2);
      this.Controls.Add(this.ttb_paper3);
      this.Controls.Add(this.ttb_paper4);
      this.Controls.Add(this.ttb_paper6);
      this.Controls.Add(this.ttb_quality);
      this.Controls.Add(this.brk_papers);
      this.Controls.Add(this.tnud_WeightFactor);
      this.Controls.Add(this.tnud_Thickness);
      this.Controls.Add(this.tnud_WeightFactor0);
      this.Controls.Add(this.tnud_WeightFactor1);
      this.Controls.Add(this.tnud_WeightFactor2);
      this.Controls.Add(this.tnud_WeightFactor3);
      this.Controls.Add(this.tnud_WeightFactor4);
      this.Controls.Add(this.tnud_WeightFactor5);
      this.Controls.Add(this.tnud_WeightFactor6);
      this.Controls.Add(this.ttb_qualityText);
      this.Controls.Add(this.ttl_Quality);
      this.Controls.Add(this.pct1);
      this.Controls.Add(this.pct3);
      this.Controls.Add(this.pct2);
      this.Controls.Add(this.pct0);
      this.Controls.Add(this.pct6);
      this.Controls.Add(this.pct4);
      this.Controls.Add(this.pct5);
      this.Controls.Add(this.nud_ProgramLength);
      this.Controls.Add(this.ttb_index);
      this.Controls.Add(this.nud_WebWidth);
      this.Controls.Add(this.ttb_ProgramNumber);
      this.Name = "Frm_InsWetOrder";
      this.Text = "Frm_InsWetOrder";
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
      this.Controls.SetChildIndex(this.ttb_ProgramNumber, 0);
      this.Controls.SetChildIndex(this.nud_WebWidth, 0);
      this.Controls.SetChildIndex(this.ttb_index, 0);
      this.Controls.SetChildIndex(this.nud_ProgramLength, 0);
      this.Controls.SetChildIndex(this.pct5, 0);
      this.Controls.SetChildIndex(this.pct4, 0);
      this.Controls.SetChildIndex(this.pct6, 0);
      this.Controls.SetChildIndex(this.pct0, 0);
      this.Controls.SetChildIndex(this.pct2, 0);
      this.Controls.SetChildIndex(this.pct3, 0);
      this.Controls.SetChildIndex(this.pct1, 0);
      this.Controls.SetChildIndex(this.ttl_Quality, 0);
      this.Controls.SetChildIndex(this.ttb_qualityText, 0);
      this.Controls.SetChildIndex(this.tnud_WeightFactor6, 0);
      this.Controls.SetChildIndex(this.tnud_WeightFactor5, 0);
      this.Controls.SetChildIndex(this.tnud_WeightFactor4, 0);
      this.Controls.SetChildIndex(this.tnud_WeightFactor3, 0);
      this.Controls.SetChildIndex(this.tnud_WeightFactor2, 0);
      this.Controls.SetChildIndex(this.tnud_WeightFactor1, 0);
      this.Controls.SetChildIndex(this.tnud_WeightFactor0, 0);
      this.Controls.SetChildIndex(this.tnud_Thickness, 0);
      this.Controls.SetChildIndex(this.tnud_WeightFactor, 0);
      this.Controls.SetChildIndex(this.brk_papers, 0);
      this.Controls.SetChildIndex(this.ttb_quality, 0);
      this.Controls.SetChildIndex(this.ttb_paper6, 0);
      this.Controls.SetChildIndex(this.ttb_paper4, 0);
      this.Controls.SetChildIndex(this.ttb_paper3, 0);
      this.Controls.SetChildIndex(this.ttb_paper2, 0);
      this.Controls.SetChildIndex(this.ttb_paper1, 0);
      this.Controls.SetChildIndex(this.ttb_paper0, 0);
      this.Controls.SetChildIndex(this.ttb_paper5, 0);
      this.Controls.SetChildIndex(this.ttb_Note, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct0)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct6)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct4)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct5)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		private void cleanAllFields()
		{
			ttb_Note.Value = ttb_ProgramNumber.Value = nud_ProgramLength.Value = nud_WebWidth.Value = string.Empty;
			ttb_quality.Value = ttb_qualityText.Value = tnud_WeightFactor.Value = tnud_Thickness.Value = string.Empty;
			ttb_paper6.Value = ttb_paper5.Value = ttb_paper4.Value = ttb_paper3.Value = ttb_paper2.Value = ttb_paper1.Value = ttb_paper0.Value = string.Empty;
			tnud_WeightFactor6.Value = tnud_WeightFactor5.Value = tnud_WeightFactor4.Value = tnud_WeightFactor3.Value = tnud_WeightFactor2.Value = tnud_WeightFactor1.Value = tnud_WeightFactor1.Value = string.Empty;
		}

		/// <summary>
    /// Show the specified program into the screen.
    /// </summary>
    /// <param name="index">Index of the program to be showed.</param>
		private void showProgram(int index)
		{
			cleanAllFields();
			if (program.Rows.Count == 0)
			{
				Logger.LogError(this, "showProgram - Program queue is empty.");
				return;
			}
			if (index < 0)
			{
				index = 0;
			}
			if (index >= program.Rows.Count)
			{
				index = program.Rows.Count - 1;
			}
			DataRow programRow = program.Rows[index];

			this.ttb_index.Value = programRow[Order.WEL_POSITION].ToString();

			ttb_ProgramNumber.Value = programRow[Order.WEL_PROGRAM_CODE].ToString();
			nud_ProgramLength.Value = programRow[Order.WEL_PROGRAM_LENGTH].ToString();
			nud_WebWidth.Value = programRow[Order.WEL_PAPER_WIDTH].ToString();
			ttb_Note.Value = programRow[Order.WEL_PROGRAM_NOTE].ToString();

      DataRow qualityRow = DBManager.getDBManager().quality.getRow(programRow[Order.WEL_PROGRAM_QUALITY].ToString());
			if (qualityRow != null)
			{
				ttb_quality.Value = qualityRow[Quality.QUALITY_ID].ToString();
				ttb_qualityText.Value = qualityRow[Quality.QUALITY_TEXT].ToString();
				tnud_WeightFactor.Value = qualityRow[Quality.WEIGHT_FACTOR].ToString();
				tnud_Thickness.Value = qualityRow[Quality.THICKNESS_UDdmm].ToString();

				DataRow paperRow = null;

				try
				{
          paperRow = DBManager.getDBManager().paper.getRow(qualityRow[Quality.PAPER_ID_0].ToString(), Convert.ToInt32(qualityRow[Quality.WEIGHT_FACTOR_0].ToString()));
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "showProgram - error getting paper row 0", ex);
				}
				if (paperRow != null)
				{
					ttb_paper0.Value = paperRow[Paper.PAPER_ID].ToString();
					tnud_WeightFactor0.Value = paperRow[Paper.WEIGHT_FACTOR].ToString();
				}

				try
				{
          paperRow = DBManager.getDBManager().paper.getRow(qualityRow[Quality.PAPER_ID_1].ToString(), Convert.ToInt32(qualityRow[Quality.WEIGHT_FACTOR_1].ToString()));
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "showProgram - error getting paper row 1", ex);
				}
				if (paperRow != null)
				{
					ttb_paper1.Value = paperRow[Paper.PAPER_ID].ToString();
					tnud_WeightFactor1.Value = paperRow[Paper.WEIGHT_FACTOR].ToString();
				}

				try
				{
          paperRow = DBManager.getDBManager().paper.getRow(qualityRow[Quality.PAPER_ID_2].ToString(), Convert.ToInt32(qualityRow[Quality.WEIGHT_FACTOR_2].ToString()));
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "showProgram - error getting paper row 2", ex);
				}
				if (paperRow != null)
				{
					ttb_paper2.Value = paperRow[Paper.PAPER_ID].ToString();
					tnud_WeightFactor2.Value = paperRow[Paper.WEIGHT_FACTOR].ToString();
				}

				try 
				{
          paperRow = DBManager.getDBManager().paper.getRow(qualityRow[Quality.PAPER_ID_3].ToString(), Convert.ToInt32(qualityRow[Quality.WEIGHT_FACTOR_3].ToString()));
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "showProgram - error getting paper row 3", ex);
				}
				if (paperRow != null)
				{
					ttb_paper3.Value = paperRow[Paper.PAPER_ID].ToString();
					tnud_WeightFactor3.Value = paperRow[Paper.WEIGHT_FACTOR].ToString();
				}
				ttb_paper3.Visible = tnud_WeightFactor3.Visible = pct3.Visible = (paperRow != null);

				try
				{
          paperRow = DBManager.getDBManager().paper.getRow(qualityRow[Quality.PAPER_ID_4].ToString(), Convert.ToInt32(qualityRow[Quality.WEIGHT_FACTOR_4].ToString()));
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "showProgram - error getting paper row 4", ex);
				}
				if (paperRow != null)
				{
					ttb_paper4.Value = paperRow[Paper.PAPER_ID].ToString();
					tnud_WeightFactor4.Value = paperRow[Paper.WEIGHT_FACTOR].ToString();
				}
				ttb_paper4.Visible = tnud_WeightFactor4.Visible = pct4.Visible = (paperRow != null);

				try
				{
          paperRow = DBManager.getDBManager().paper.getRow(qualityRow[Quality.PAPER_ID_5].ToString(), Convert.ToInt32(qualityRow[Quality.WEIGHT_FACTOR_5].ToString()));
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "showProgram - error getting paper row 5", ex);
				}
				if (paperRow != null)
				{
					ttb_paper5.Value = paperRow[Paper.PAPER_ID].ToString();
					tnud_WeightFactor5.Value = paperRow[Paper.WEIGHT_FACTOR].ToString();
				}
				ttb_paper5.Visible = tnud_WeightFactor5.Visible = pct5.Visible = (paperRow != null);

				try
				{
          paperRow = DBManager.getDBManager().paper.getRow(qualityRow[Quality.PAPER_ID_6].ToString(), Convert.ToInt32(qualityRow[Quality.WEIGHT_FACTOR_6].ToString()));
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "showProgram - error getting paper row 6", ex);
				}
				if (paperRow != null)
				{
					ttb_paper6.Value = paperRow[Paper.PAPER_ID].ToString();
					tnud_WeightFactor6.Value = paperRow[Paper.WEIGHT_FACTOR].ToString();
				}
				ttb_paper6.Visible = tnud_WeightFactor6.Visible = pct6.Visible = (paperRow != null);
			}
		}

    public override void _Show()
    {
      base._Show();
      showProgram(indexOrderToShowWET);
    }
	}
}

