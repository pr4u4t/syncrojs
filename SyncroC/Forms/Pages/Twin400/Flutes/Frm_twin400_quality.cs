using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.DataBase;
using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.Twin400.Flutes
{
	public class Frm_twin400_quality : SyncroC.Forms.Pages.Twin400.Flutes.Frm_twin400_flutesSubgroup
	{
		private SyncroC.Controls.Title ttl_QualityList;
		private SyncroC.Controls.Grid lst_Quality_NN;
		private System.Windows.Forms.ColumnHeader QualityCode;
		private SyncroC.Controls.TitledTextBox ttb_Text;
		private SyncroC.Controls.TitledTextBox ttb_QualityID;
		private SyncroC.Controls.TitledList tlst_FluteID;
		private System.Windows.Forms.Panel pnl_Other;
		private System.ComponentModel.IContainer components = null;
		private SyncroC.Controls.Title title1;
		private SyncroC.Controls.Title title2;
		private SyncroC.Controls.TitledNumericUpDown tnud_BoardThickness;
		private SyncroC.Controls.TitledNumericUpDown tnud_LubricationTime;
		private SyncroC.Controls.TitledNumericUpDown tnud_GrindingDuration;
		private SyncroC.Controls.TitledNumericUpDown tnud_TrimGrindingMultiplier;
		private SyncroC.Controls.TitledNumericUpDown tnud_LubricationParameter;
		private SyncroC.Controls.TitledNumericUpDown tnud_GrindingParameter;
		private SyncroC.Controls.TitledNumericUpDown tnud_ScoreGap;
		private enum State {Insert, Modify, Read};
		private State state=State.Read;
    //private Quality quality;
		private SyncroC.Controls.Bracket brk_Details;
    private Flute flute;
		private SyncroC.Controls.Title title3;
    //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
    private SyncroC.Controls.CheckBox chk_InternalFlapDisabled;
		private SyncroC.Controls.TitledNumericUpDown tnud_WeightFactor;
    private bool deleting;

    //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
    private void updateControlsEnable() 
		{
			if (chk_InternalFlapDisabled.Visible == false)
				chk_InternalFlapDisabled.Visible = true;

			if (SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastLow())
			{
        //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
        chk_InternalFlapDisabled.ImagePassword = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
        //if (chk_InternalFlapDisabled.BackColor != System.Drawing.Color.WhiteSmoke)
        //  chk_InternalFlapDisabled.BackColor = System.Drawing.Color.WhiteSmoke;

				if (chk_InternalFlapDisabled.Enabled == false)
					chk_InternalFlapDisabled.Enabled = true;
			}
			else
			{
        //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
        chk_InternalFlapDisabled.ImagePassword = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
        //if (chk_InternalFlapDisabled.BackColor != System.Drawing.Color.Gainsboro)
        //  chk_InternalFlapDisabled.BackColor = System.Drawing.Color.Gainsboro;

				if (chk_InternalFlapDisabled.Enabled == true)
					chk_InternalFlapDisabled.Enabled = false;

			}

			//RFAZZI_FI12/02/2009_1.Added Quality Weight into Quality Pages. 
			if (tnud_WeightFactor.Visible == false)
				tnud_WeightFactor.Visible = true;
			if (tnud_WeightFactor.Enabled == false)
				tnud_WeightFactor.Enabled = true;
		}


    public Frm_twin400_quality()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

      //this.quality = DBManager.getDBManager().quality;
      //quality.OnDBQualityChange += new SyncroC.DataBase.Quality.DBChangedHandler(refreshQuality);

      DBManager.getDBManager().quality.OnDBQualityChange += new Quality.DBChangedHandler(refreshQuality);

			this.flute = DBManager.getDBManager().flute;
			flute.OnDBFluteChange += new SyncroC.DataBase.Flute.DBChangedHandler(refreshFlute);
      this.deleting = false;
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
      {
        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
        this.lbl_CtrlF5_YY.Text = "";
      }
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

			DBManager.getDBManager().quality.OnDBQualityChange -= new SyncroC.DataBase.Quality.DBChangedHandler(refreshQuality);
			flute.OnDBFluteChange -= new SyncroC.DataBase.Flute.DBChangedHandler(refreshFlute);
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_quality));
      this.ttl_QualityList = new SyncroC.Controls.Title();
      this.lst_Quality_NN = new SyncroC.Controls.Grid();
      this.QualityCode = new System.Windows.Forms.ColumnHeader();
      this.ttb_Text = new SyncroC.Controls.TitledTextBox();
      this.ttb_QualityID = new SyncroC.Controls.TitledTextBox();
      this.tlst_FluteID = new SyncroC.Controls.TitledList();
      this.pnl_Other = new System.Windows.Forms.Panel();
      this.tnud_WeightFactor = new SyncroC.Controls.TitledNumericUpDown();
      this.chk_InternalFlapDisabled = new SyncroC.Controls.CheckBox();
      this.title3 = new SyncroC.Controls.Title();
      this.title1 = new SyncroC.Controls.Title();
      this.title2 = new SyncroC.Controls.Title();
      this.tnud_BoardThickness = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_LubricationTime = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_GrindingDuration = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_TrimGrindingMultiplier = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_LubricationParameter = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_GrindingParameter = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_ScoreGap = new SyncroC.Controls.TitledNumericUpDown();
      this.brk_Details = new SyncroC.Controls.Bracket();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.pnl_Other.SuspendLayout();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // lbl_Time
      // 
      this.lbl_Time.Margin = new System.Windows.Forms.Padding(1, 3, 3, 1);
      // 
      // lbl_Date
      // 
      this.lbl_Date.Margin = new System.Windows.Forms.Padding(1, 1, 3, 3);
      // 
      // lbl_Status
      // 
      this.lbl_Status.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // pct_Home_NY
      // 
      this.pct_Home_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // pct_Back_NY
      // 
      this.pct_Back_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // pct_Forward_NY
      // 
      this.pct_Forward_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF3
      // 
      this.lbl_CtrlF3.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF5
      // 
      this.lbl_CtrlF5.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      // 
      // lbl_CtrlF2
      // 
      this.lbl_CtrlF2.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF1_YY.Text = "New";
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF2_YY.Text = "Erase";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF5_YY.Text = "Modify";
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
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
      // ttl_QualityList
      // 
      this.ttl_QualityList.BackColor = System.Drawing.Color.Black;
      this.ttl_QualityList.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_QualityList.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_QualityList.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_QualityList.Location = new System.Drawing.Point(30, 193);
      this.ttl_QualityList.Name = "ttl_QualityList";
      this.ttl_QualityList.Size = new System.Drawing.Size(432, 517);
      this.ttl_QualityList.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_QualityList.TabIndex = 107;
      this.ttl_QualityList.TabStop = false;
      this.ttl_QualityList.Text = "QUALITIES";
      this.ttl_QualityList.TranslateText = true;
      // 
      // lst_Quality_NN
      // 
      this.lst_Quality_NN.BackColor = System.Drawing.Color.LightGray;
      this.lst_Quality_NN.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.QualityCode});
      this.lst_Quality_NN.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Narrow;
      this.lst_Quality_NN.EvenItemsColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.lst_Quality_NN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lst_Quality_NN.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.lst_Quality_NN.HideSelection = false;
      this.lst_Quality_NN.ImagePassword = null;
      this.lst_Quality_NN.Location = new System.Drawing.Point(42, 217);
      this.lst_Quality_NN.MultiSelect = true;
      this.lst_Quality_NN.Name = "lst_Quality_NN";
      this.lst_Quality_NN.OddItemsColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.lst_Quality_NN.ShowForecolorItem = false;
      this.lst_Quality_NN.ShowIndexesItem = false;
      this.lst_Quality_NN.Size = new System.Drawing.Size(419, 493);
      this.lst_Quality_NN.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lst_Quality_NN.Style = SyncroC.Controls.GridStyles.Light;
      this.lst_Quality_NN.TabIndex = 106;
      this.lst_Quality_NN.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.lst_Quality_NN.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.lst_Quality_NN.TranslateText = false;
      this.lst_Quality_NN.Leave += new System.EventHandler(this.lst_Quality_NN_Leave);
      this.lst_Quality_NN.SelectedIndexChanged += new System.EventHandler(this.lst_Quality_NN_SelectedIndexChanged);
      // 
      // QualityCode
      // 
      this.QualityCode.Text = "Quality Code";
      this.QualityCode.Width = 493;
      // 
      // ttb_Text
      // 
      this.ttb_Text.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ttb_Text.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Text.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Text.ImagePassword = null;
      this.ttb_Text.Location = new System.Drawing.Point(761, 217);
      this.ttb_Text.MaxChar = 100;
      this.ttb_Text.Multiline = true;
      this.ttb_Text.Name = "ttb_Text";
      this.ttb_Text.PasswordChar = '\0';
      this.ttb_Text.ReadOnly = false;
      this.ttb_Text.Size = new System.Drawing.Size(320, 73);
      this.ttb_Text.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_Text.TabIndex = 2;
      this.ttb_Text.TabStop = false;
      this.ttb_Text.Text = "Text";
      this.ttb_Text.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_Text.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Text.TranslateText = true;
      this.ttb_Text.Value = " ";
      this.ttb_Text.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_QualityID
      // 
      this.ttb_QualityID.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_QualityID.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_QualityID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_QualityID.ImagePassword = null;
      this.ttb_QualityID.Location = new System.Drawing.Point(500, 217);
      this.ttb_QualityID.MaxChar = 30;
      this.ttb_QualityID.Multiline = false;
      this.ttb_QualityID.Name = "ttb_QualityID";
      this.ttb_QualityID.PasswordChar = '\0';
      this.ttb_QualityID.ReadOnly = true;
      this.ttb_QualityID.Size = new System.Drawing.Size(254, 73);
      this.ttb_QualityID.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_QualityID.TabIndex = 1;
      this.ttb_QualityID.TabStop = false;
      this.ttb_QualityID.Text = "Quality Code";
      this.ttb_QualityID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_QualityID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_QualityID.TranslateText = false;
      this.ttb_QualityID.Value = " ";
      this.ttb_QualityID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_QualityID.Validating += new System.ComponentModel.CancelEventHandler(this.ttb_QualityID_Validating);
      // 
      // tlst_FluteID
      // 
      this.tlst_FluteID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlst_FluteID.CanCancel = false;
      this.tlst_FluteID.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlst_FluteID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlst_FluteID.ImagePassword = null;
      this.tlst_FluteID.Location = new System.Drawing.Point(1088, 217);
      this.tlst_FluteID.Name = "tlst_FluteID";
      this.tlst_FluteID.ReadOnly = true;
      this.tlst_FluteID.SelectedIndex = -1;
      this.tlst_FluteID.SelectedItem = "";
      this.tlst_FluteID.SelectedItemData = "";
      this.tlst_FluteID.Size = new System.Drawing.Size(112, 72);
      this.tlst_FluteID.Sorted = true;
      this.tlst_FluteID.Style = SyncroC.Controls.TitledListStyles.Light;
      this.tlst_FluteID.TabIndex = 3;
      this.tlst_FluteID.TabStop = false;
      this.tlst_FluteID.Text = "Flute ID";
      this.tlst_FluteID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlst_FluteID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlst_FluteID.TranslateText = true;
      this.tlst_FluteID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlst_FluteID.SelectedItemChanged += new System.EventHandler(this.tlst_FluteID_YN_SelectedItemChanged);
      this.tlst_FluteID.Leave += new System.EventHandler(this.tlst_FluteID_YN_SelectedItemChanged);
      // 
      // pnl_Other
      // 
      this.pnl_Other.Controls.Add(this.tnud_WeightFactor);
      this.pnl_Other.Controls.Add(this.chk_InternalFlapDisabled);
      this.pnl_Other.Controls.Add(this.title3);
      this.pnl_Other.Controls.Add(this.title1);
      this.pnl_Other.Controls.Add(this.title2);
      this.pnl_Other.Controls.Add(this.tnud_BoardThickness);
      this.pnl_Other.Controls.Add(this.tnud_LubricationTime);
      this.pnl_Other.Controls.Add(this.tnud_GrindingDuration);
      this.pnl_Other.Controls.Add(this.tnud_TrimGrindingMultiplier);
      this.pnl_Other.Controls.Add(this.tnud_LubricationParameter);
      this.pnl_Other.Controls.Add(this.tnud_GrindingParameter);
      this.pnl_Other.Controls.Add(this.tnud_ScoreGap);
      this.pnl_Other.Location = new System.Drawing.Point(500, 310);
      this.pnl_Other.Name = "pnl_Other";
      this.pnl_Other.Size = new System.Drawing.Size(698, 324);
      this.pnl_Other.TabIndex = 159;
      // 
      // tnud_WeightFactor
      // 
      this.tnud_WeightFactor.DecimalPlaces = 0;
      this.tnud_WeightFactor.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_WeightFactor.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor.ImagePassword = null;
      this.tnud_WeightFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WeightFactor.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_WeightFactor.Location = new System.Drawing.Point(546, 149);
      this.tnud_WeightFactor.Maximum = new decimal(new int[] {
            10000,
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
      this.tnud_WeightFactor.Size = new System.Drawing.Size(154, 64);
      this.tnud_WeightFactor.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_WeightFactor.TabIndex = 12;
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
      // chk_InternalFlapDisabled
      // 
      this.chk_InternalFlapDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_InternalFlapDisabled.Checked = false;
      this.chk_InternalFlapDisabled.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_InternalFlapDisabled.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_InternalFlapDisabled.ImagePassword = null;
      this.chk_InternalFlapDisabled.Location = new System.Drawing.Point(546, 69);
      this.chk_InternalFlapDisabled.Name = "chk_InternalFlapDisabled";
      this.chk_InternalFlapDisabled.ReadOnly = true;
      this.chk_InternalFlapDisabled.Size = new System.Drawing.Size(152, 56);
      this.chk_InternalFlapDisabled.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_InternalFlapDisabled.TabIndex = 11;
      this.chk_InternalFlapDisabled.Text = "Internal Flap Disabled";
      this.chk_InternalFlapDisabled.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_InternalFlapDisabled.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_InternalFlapDisabled.TranslateText = true;
      // 
      // title3
      // 
      this.title3.BackColor = System.Drawing.Color.Black;
      this.title3.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title3.Location = new System.Drawing.Point(536, 37);
      this.title3.Name = "title3";
      this.title3.Size = new System.Drawing.Size(165, 264);
      this.title3.Style = SyncroC.Controls.TitleStyles.Small;
      this.title3.TabIndex = 171;
      this.title3.TabStop = false;
      this.title3.Text = "OTHER";
      this.title3.TranslateText = true;
      // 
      // title1
      // 
      this.title1.BackColor = System.Drawing.Color.Black;
      this.title1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title1.Location = new System.Drawing.Point(385, 37);
      this.title1.Name = "title1";
      this.title1.Size = new System.Drawing.Size(144, 264);
      this.title1.Style = SyncroC.Controls.TitleStyles.Small;
      this.title1.TabIndex = 170;
      this.title1.TabStop = false;
      this.title1.Text = "SCORES";
      this.title1.TranslateText = true;
      // 
      // title2
      // 
      this.title2.BackColor = System.Drawing.Color.Black;
      this.title2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title2.Location = new System.Drawing.Point(7, 37);
      this.title2.Name = "title2";
      this.title2.Size = new System.Drawing.Size(365, 264);
      this.title2.Style = SyncroC.Controls.TitleStyles.Small;
      this.title2.TabIndex = 169;
      this.title2.TabStop = false;
      this.title2.Text = "SLITTERS";
      this.title2.TranslateText = true;
      // 
      // tnud_BoardThickness
      // 
      this.tnud_BoardThickness.DecimalPlaces = 0;
      this.tnud_BoardThickness.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_BoardThickness.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_BoardThickness.ImagePassword = null;
      this.tnud_BoardThickness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_BoardThickness.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_BoardThickness.Location = new System.Drawing.Point(393, 149);
      this.tnud_BoardThickness.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.tnud_BoardThickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_BoardThickness.Name = "tnud_BoardThickness";
      this.tnud_BoardThickness.ReadOnly = true;
      this.tnud_BoardThickness.Size = new System.Drawing.Size(136, 64);
      this.tnud_BoardThickness.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_BoardThickness.TabIndex = 10;
      this.tnud_BoardThickness.TabStop = false;
      this.tnud_BoardThickness.Text = "Board Thickness";
      this.tnud_BoardThickness.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_BoardThickness.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_BoardThickness.TranslateText = true;
      this.tnud_BoardThickness.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tnud_BoardThickness.Value = "10";
      this.tnud_BoardThickness.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_BoardThickness.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_LubricationTime
      // 
      this.tnud_LubricationTime.DecimalPlaces = 0;
      this.tnud_LubricationTime.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_LubricationTime.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LubricationTime.ImagePassword = null;
      this.tnud_LubricationTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_LubricationTime.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_LubricationTime.Location = new System.Drawing.Point(201, 69);
      this.tnud_LubricationTime.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
      this.tnud_LubricationTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_LubricationTime.Name = "tnud_LubricationTime";
      this.tnud_LubricationTime.ReadOnly = true;
      this.tnud_LubricationTime.Size = new System.Drawing.Size(171, 65);
      this.tnud_LubricationTime.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_LubricationTime.TabIndex = 7;
      this.tnud_LubricationTime.Text = "Lubrication Time";
      this.tnud_LubricationTime.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationTime.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LubricationTime.TranslateText = true;
      this.tnud_LubricationTime.UnitMeasure = SyncroC.Utility.UnitMeasure.TenthOfSecond;
      this.tnud_LubricationTime.Value = "1";
      this.tnud_LubricationTime.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationTime.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_GrindingDuration
      // 
      this.tnud_GrindingDuration.DecimalPlaces = 0;
      this.tnud_GrindingDuration.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_GrindingDuration.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingDuration.ImagePassword = null;
      this.tnud_GrindingDuration.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GrindingDuration.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_GrindingDuration.Location = new System.Drawing.Point(15, 69);
      this.tnud_GrindingDuration.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
      this.tnud_GrindingDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GrindingDuration.Name = "tnud_GrindingDuration";
      this.tnud_GrindingDuration.ReadOnly = true;
      this.tnud_GrindingDuration.Size = new System.Drawing.Size(171, 65);
      this.tnud_GrindingDuration.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_GrindingDuration.TabIndex = 4;
      this.tnud_GrindingDuration.TabStop = false;
      this.tnud_GrindingDuration.Text = "Grinding Duration";
      this.tnud_GrindingDuration.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingDuration.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingDuration.TranslateText = true;
      this.tnud_GrindingDuration.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_GrindingDuration.Value = "1";
      this.tnud_GrindingDuration.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingDuration.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_TrimGrindingMultiplier
      // 
      this.tnud_TrimGrindingMultiplier.DecimalPlaces = 0;
      this.tnud_TrimGrindingMultiplier.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_TrimGrindingMultiplier.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_TrimGrindingMultiplier.ImagePassword = null;
      this.tnud_TrimGrindingMultiplier.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_TrimGrindingMultiplier.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_TrimGrindingMultiplier.Location = new System.Drawing.Point(15, 229);
      this.tnud_TrimGrindingMultiplier.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.tnud_TrimGrindingMultiplier.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_TrimGrindingMultiplier.Name = "tnud_TrimGrindingMultiplier";
      this.tnud_TrimGrindingMultiplier.ReadOnly = true;
      this.tnud_TrimGrindingMultiplier.Size = new System.Drawing.Size(171, 65);
      this.tnud_TrimGrindingMultiplier.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_TrimGrindingMultiplier.TabIndex = 6;
      this.tnud_TrimGrindingMultiplier.TabStop = false;
      this.tnud_TrimGrindingMultiplier.Text = "Trim Grindings Multiplier";
      this.tnud_TrimGrindingMultiplier.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_TrimGrindingMultiplier.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_TrimGrindingMultiplier.TranslateText = true;
      this.tnud_TrimGrindingMultiplier.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_TrimGrindingMultiplier.Value = "1";
      this.tnud_TrimGrindingMultiplier.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_TrimGrindingMultiplier.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_LubricationParameter
      // 
      this.tnud_LubricationParameter.DecimalPlaces = 0;
      this.tnud_LubricationParameter.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_LubricationParameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LubricationParameter.ImagePassword = null;
      this.tnud_LubricationParameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_LubricationParameter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_LubricationParameter.Location = new System.Drawing.Point(201, 149);
      this.tnud_LubricationParameter.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
      this.tnud_LubricationParameter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_LubricationParameter.Name = "tnud_LubricationParameter";
      this.tnud_LubricationParameter.ReadOnly = true;
      this.tnud_LubricationParameter.Size = new System.Drawing.Size(171, 65);
      this.tnud_LubricationParameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_LubricationParameter.TabIndex = 8;
      this.tnud_LubricationParameter.Text = "Lubrication Frequency";
      this.tnud_LubricationParameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationParameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LubricationParameter.TranslateText = true;
      this.tnud_LubricationParameter.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_LubricationParameter.Value = "1";
      this.tnud_LubricationParameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationParameter.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_GrindingParameter
      // 
      this.tnud_GrindingParameter.DecimalPlaces = 0;
      this.tnud_GrindingParameter.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_GrindingParameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingParameter.ImagePassword = null;
      this.tnud_GrindingParameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GrindingParameter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_GrindingParameter.Location = new System.Drawing.Point(15, 149);
      this.tnud_GrindingParameter.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
      this.tnud_GrindingParameter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GrindingParameter.Name = "tnud_GrindingParameter";
      this.tnud_GrindingParameter.ReadOnly = true;
      this.tnud_GrindingParameter.Size = new System.Drawing.Size(171, 65);
      this.tnud_GrindingParameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_GrindingParameter.TabIndex = 5;
      this.tnud_GrindingParameter.TabStop = false;
      this.tnud_GrindingParameter.Text = "Grinding Frequency";
      this.tnud_GrindingParameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingParameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingParameter.TranslateText = true;
      this.tnud_GrindingParameter.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_GrindingParameter.Value = "1";
      this.tnud_GrindingParameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingParameter.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_ScoreGap
      // 
      this.tnud_ScoreGap.DecimalPlaces = 0;
      this.tnud_ScoreGap.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_ScoreGap.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ScoreGap.ImagePassword = null;
      this.tnud_ScoreGap.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_ScoreGap.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_ScoreGap.Location = new System.Drawing.Point(393, 69);
      this.tnud_ScoreGap.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.tnud_ScoreGap.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_ScoreGap.Name = "tnud_ScoreGap";
      this.tnud_ScoreGap.ReadOnly = true;
      this.tnud_ScoreGap.Size = new System.Drawing.Size(136, 65);
      this.tnud_ScoreGap.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_ScoreGap.TabIndex = 9;
      this.tnud_ScoreGap.TabStop = false;
      this.tnud_ScoreGap.Text = "Score Gap";
      this.tnud_ScoreGap.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ScoreGap.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ScoreGap.TranslateText = true;
      this.tnud_ScoreGap.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tnud_ScoreGap.Value = "10";
      this.tnud_ScoreGap.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ScoreGap.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // brk_Details
      // 
      this.brk_Details.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.brk_Details.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.brk_Details.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_Details.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_Details.Location = new System.Drawing.Point(467, 193);
      this.brk_Details.Name = "brk_Details";
      this.brk_Details.Position = SyncroC.Controls.BracketPosition.Right;
      this.brk_Details.ShowArrow = true;
      this.brk_Details.Size = new System.Drawing.Size(734, 519);
      this.brk_Details.TabIndex = 160;
      this.brk_Details.Text = "DETAILS";
      this.brk_Details.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_Details.TranslateText = true;
      // 
      // Frm_twin400_quality
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.brk_Details);
      this.Controls.Add(this.ttb_Text);
      this.Controls.Add(this.ttb_QualityID);
      this.Controls.Add(this.ttl_QualityList);
      this.Controls.Add(this.lst_Quality_NN);
      this.Controls.Add(this.tlst_FluteID);
      this.Controls.Add(this.pnl_Other);
      this.Name = "Frm_twin400_quality";
      this.Tag = "";
      this.Text = "Frm_twin400_quality";
      this.Activated += new System.EventHandler(this.Frm_twin400_quality_Activated);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.pnl_Other, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
      this.Controls.SetChildIndex(this.pct_Home_NY, 0);
      this.Controls.SetChildIndex(this.pct_Back_NY, 0);
      this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.tlst_FluteID, 0);
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
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.lst_Quality_NN, 0);
      this.Controls.SetChildIndex(this.ttl_QualityList, 0);
      this.Controls.SetChildIndex(this.ttb_QualityID, 0);
      this.Controls.SetChildIndex(this.ttb_Text, 0);
      this.Controls.SetChildIndex(this.brk_Details, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.pnl_Other.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

		/// <summary>
		/// refresh all the data stored in a local databases showed on this page.
		/// to be called at the beginning and at every database change.
		/// </summary>
		public override void refreshData()
		{
      if ((this.state != State.Read) || this.deleting) return;
			//Quality
			lst_Quality_NN.ClearItems();
			DataTable qualityTable = DBManager.getDBManager().quality.getTable();

			for (int i = 0; i < qualityTable.Rows.Count; i++)
			{
				lst_Quality_NN.AddItem(
					new ListViewItem(
					new String[]{
												qualityTable.Rows[i][SyncroC.DataBase.Quality.QUALITY_ID].ToString()}));
			}

      //Flute
      tlst_FluteID.ClearItems();
      //DataTable fluteTable = this.flute.flute.Tables[Flute.FLUTE_LIST];
      DataTable fluteTable = DBManager.getDBManager().flute.getTable();

      for (int i = 0; i < fluteTable.Rows.Count; i++)
        tlst_FluteID.AddItem(fluteTable.Rows[i][Flute.FLUTE_ID].ToString());
      
      if (lst_Quality_NN.getItems().Count != 0)
      {
				lst_Quality_NN.Focus();
				//lst_Quality_NN.getItems()[0].Selected=true;
			}
			else
				this.CleanTheGraphics();
      base.refreshData();
		}

		public void refreshQuality(object sender, DBChangedEventArgs args)
		{
      //agrassi_FI07-09-09_2
      try
      {
        switch (args.Changement)       
			  {         
				  case DBChangement.Delete:
					  // Remove the item with the specified key.
					  if (lst_Quality_NN.getItems().Count != 0)
						  lst_Quality_NN.RemoveItemsWithVal(args.Key, lst_Quality_NN.getItems()[0].SubItems.Count - 1);
					  break;
           
				  case DBChangement.Insert:
					  // Add the new item
					  DataTable qualityList = DBManager.getDBManager().quality.getTable();
					  lst_Quality_NN.AddItem(new ListViewItem(new String[]{qualityList.Rows[qualityList.Rows.Count - 1][Quality.QUALITY_ID].ToString()}));
					  break;
  					
				  case DBChangement.Modify:
					  lst_Quality_NN_SelectedIndexChanged(null,null);
					  break;
			  }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "Error in quality_RowFill:" + ex.Message);
      }
		}

		public void refreshFlute(object sender, DBChangedEventArgs args)
		{
			switch(args.Changement)       
			{         
				case DBChangement.Delete:
					tlst_FluteID.RemoveItem(args.Key);
					break;
         
				case DBChangement.Insert:
					tlst_FluteID.AddItem(args.Key);
					break;
					
				case DBChangement.Modify:
					break;
			}
		}

		private void readOnly(bool isReadOnly)
		{
			if(state==State.Insert)
			{
				ttb_QualityID.TabStop=true;
				ttb_QualityID.ReadOnly=false;
			}
			else
			{
				ttb_QualityID.TabStop=false;
				ttb_QualityID.ReadOnly=true;
			}

			if(isReadOnly)
			{
				//tab
				tlst_FluteID.TabStop=false;
				ttb_Text.TabStop=false;
				tnud_ScoreGap.TabStop=false;
				tnud_TrimGrindingMultiplier.TabStop=false;
				tnud_GrindingDuration.TabStop=false;
				tnud_LubricationParameter.TabStop=false;
				tnud_LubricationTime.TabStop=false;
				tnud_GrindingParameter.TabStop=false;
				tnud_BoardThickness.TabStop=false;
        //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
        chk_InternalFlapDisabled.TabStop = false;
				//RFAZZI_FI12/02/2009_1.Added Quality Weight into Quality Pages. 
				tnud_WeightFactor.TabStop = false;
				
				//readonly
				tlst_FluteID.ReadOnly=true;
				ttb_Text.ReadOnly=true;
				tnud_ScoreGap.ReadOnly=true;
				tnud_TrimGrindingMultiplier.ReadOnly=true;
				tnud_GrindingDuration.ReadOnly=true;
				tnud_LubricationParameter.ReadOnly=true;
				tnud_LubricationTime.ReadOnly=true;
				tnud_GrindingParameter.ReadOnly=true;
				tnud_BoardThickness.ReadOnly=true;
        //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
        chk_InternalFlapDisabled.ReadOnly = true;
				//RFAZZI_FI12/02/2009_1.Added Quality Weight into Quality Pages. 
				tnud_WeightFactor.ReadOnly = true;
			}
			else
			{
				//tab
				tlst_FluteID.TabStop=true;
				ttb_Text.TabStop=true;
				tnud_ScoreGap.TabStop=true;
				tnud_TrimGrindingMultiplier.TabStop=true;
				tnud_GrindingDuration.TabStop=true;
				tnud_LubricationParameter.TabStop=true;
				tnud_LubricationTime.TabStop=true;
				tnud_GrindingParameter.TabStop=true;
				tnud_BoardThickness.TabStop=true;
        //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
        //chk_InternalFlapDisabled.TabStop = true;
				//RFAZZI_FI12/02/2009_1.Added Quality Weight into Quality Pages. 
				tnud_WeightFactor.TabStop = true;
        //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
        //RFAZZI_FI12/05/2010_1.Bug Fix - L'abilitazione alla modifica dell'Internal Flap TW4 viene data solo in caso di password inserita
        if (SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastLow())
          chk_InternalFlapDisabled.TabStop = true;

				//readonly
				tlst_FluteID.ReadOnly=false;
				ttb_Text.ReadOnly=false;
				tnud_ScoreGap.ReadOnly=false;
				tnud_TrimGrindingMultiplier.ReadOnly=false;
				tnud_GrindingDuration.ReadOnly=false;
				tnud_LubricationParameter.ReadOnly=false;
				tnud_LubricationTime.ReadOnly=false;
				tnud_GrindingParameter.ReadOnly=false;
				tnud_BoardThickness.ReadOnly=false;
        //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
        //chk_InternalFlapDisabled.ReadOnly = false;
				//RFAZZI_FI12/02/2009_1.Added Quality Weight into Quality Pages. 
				tnud_WeightFactor.ReadOnly = false;
        //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
        //RFAZZI_FI12/05/2010_1.Bug Fix - L'abilitazione alla modifica dell'Internal Flap TW4 viene data solo in caso di password inserita
        if (SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastLow())
          chk_InternalFlapDisabled.ReadOnly = false;
      }
		}

    private bool IsOperationPossible()
    {
			if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.CommunicatesWithSyncro)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_78");
        return false;
      }
      return true;
    }
		
		/// <summary>
		/// CtrlF1 key behaviour: New Quality
		/// </summary>
		protected override void CtrlF1()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;
      if (!this.IsOperationPossible())
        return;
			CleanTheGraphics();
			state=State.Insert;
			lst_Quality_NN.TabStop=false;
			readOnly(false);
			ttb_QualityID.Focus();
			lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor = lbl_Status.BackColor;
		}

		/// <summary>
		/// CtrlF2 key behaviour: Delete an existing record
		/// </summary>
		protected override void CtrlF2()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;
      if (!this.IsOperationPossible())
        return;
      this.deleting = true;
      if (lst_Quality_NN.Focused)
			{
				DataRowCollection rows = DBManager.getDBManager().quality.getTable().Rows;
				if (rows.Count > 0)
				{
					Frm_Confirm conf = new Frm_Confirm();
					string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_36");
					if (lst_Quality_NN.getSelectedIndices().Count != 0
						&& conf.ShowDialog(this, text).Equals(DialogResult.OK))
					{
						ArrayList key = new ArrayList();
						foreach (ListViewItem a in lst_Quality_NN.getSelectedItems())
							key.Add(a.SubItems[a.SubItems.Count - 1].Text);

						//reverse the key ArrayList for a strange bug in the listview
						key.Reverse();

						CommunicationManager cm = CommunicationManager.GetCommunicationManager();
						foreach (string b in key)
							cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_QUALITY + "." + Command.DB_CMD_DEL + "(" + b + ")");
					}
				}
				state=State.Read;
				lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
					lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
				lst_Quality_NN.Focus();
				readOnly(true);
			}
      this.deleting = false;
    }

		/// <summary>
		/// CtrlF3 key behaviour: Delete all DB papers
		/// </summary>
		protected override void CtrlF3()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;
      this.deleting = true;
      Frm_Confirm conf = new Frm_Confirm();
			string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_37");
			if (lst_Quality_NN.getItems().Count > 0
				&& conf.ShowDialog(this, text).Equals(DialogResult.OK))
			{
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_QUALITY + "." + Command.DB_CMD_CLR + "()");
				state=State.Read;
				lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
					lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
				lst_Quality_NN.Focus();
				readOnly(true);
			}
      this.deleting = false;
    }

		/// <summary>
		/// CtrlF4 key behaviour: Modify Quality
		/// </summary>
		protected override void CtrlF4()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;
      if (!this.IsOperationPossible())
        return;
      if (ttb_QualityID.Value.Trim() == "")
			{
				PageManager.getPageManager()._SetMessage("§SYCC_10", true);
				return;
			}

			string toSend = Command.DB_QUALITY + ".";
      if (state == State.Modify)
      {
        toSend += Command.DB_CMD_MOD;
        PopUp.Frm_Confirm conf = new Frm_Confirm();
        string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_65");
        DialogResult res = conf.ShowDialog(this, text);
        if ((res == DialogResult.Yes) || (res == DialogResult.OK))
          toSend += "WithSetup";
      }
      else
				toSend += Command.DB_CMD_ADD;
			toSend += "(";
			if((state==State.Modify)||(state==State.Insert))
			{
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_DB_MANAGER, 
					toSend +
					ttb_QualityID.Value + Command.PARAMETER_SEPARATOR +
					ttb_Text.Value + Command.PARAMETER_SEPARATOR +
					tlst_FluteID.Value + Command.PARAMETER_SEPARATOR +

					tnud_ScoreGap.Value + Command.PARAMETER_SEPARATOR +
					tnud_GrindingParameter.Value + Command.PARAMETER_SEPARATOR +
					tnud_LubricationParameter.Value + Command.PARAMETER_SEPARATOR +
					tnud_TrimGrindingMultiplier.Value + Command.PARAMETER_SEPARATOR +
					tnud_GrindingDuration.Value + Command.PARAMETER_SEPARATOR +
					tnud_LubricationTime.Value + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					tnud_BoardThickness.Value + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"false" + Command.PARAMETER_SEPARATOR +
			
					//WetEnd
					"---" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"---" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"---" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"---" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"---" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"---" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"---" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +

					//"0" + Command.PARAMETER_SEPARATOR +		//Quality Weight factor
					tnud_WeightFactor.Value + Command.PARAMETER_SEPARATOR +		//Quality Weight factor
					"false" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
          //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
          //"0" + Command.PARAMETER_SEPARATOR +
					(chk_InternalFlapDisabled.Checked ? "1" : "0") + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					")");
			}
			this.Escape();
		}

		/// <summary>
		/// CtrlF5 key behaviour: Save Quality
		/// </summary>
		protected override void CtrlF5()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;
      if (!this.IsOperationPossible())
        return;
      if (lst_Quality_NN.getItems().Count <= 0)
				return;

			if(state==State.Insert)
				return;

			state=State.Modify;
			lst_Quality_NN.TabStop=false;
			readOnly(false);
			ttb_Text.Focus();
			lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
		}

		/// <summary>
		/// Escape key behaviour: Abort the operation
		/// </summary>
		protected override void Escape()
		{
			if(lst_Quality_NN.Focused)
				return;
			else
			{
				tlst_FluteID.SetError("");
				lst_Quality_NN.TabStop=true;
				lst_Quality_NN.Focus();
			}
      this.state = State.Read;
			lst_Quality_NN_SelectedIndexChanged(null,null);
		}

		private void lst_Quality_NN_SelectedIndexChanged(object sender, System.EventArgs e)
		{
      if (this.state != State.Read) return;
      lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
				lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
			lst_Quality_NN.Focus();
			readOnly(true);

			try
			{
				if(DBManager.getDBManager().quality.getCount()==0)
					//clean the graphics
					CleanTheGraphics();
				else
				{
					string a = lst_Quality_NN.getSelectedItems()[0].Text;

					//search for the DryEndList selected row into the database
          //DataRow row = this.quality.quality.Tables[SyncroC.DataBase.Quality.QUALITY_LIST].
          //  Rows.Find(lst_Quality_NN.getSelectedItems()[0].Text);

          DataRow row = DBManager.getDBManager().quality.getRow(lst_Quality_NN.getSelectedItems()[0].Text);

					ttb_QualityID.Value=row[Quality.QUALITY_ID].ToString();
					ttb_Text.Value=row[Quality.QUALITY_TEXT].ToString();
					tlst_FluteID.SelectedItem=row[Quality.FLUTE_ID].ToString();

					tnud_ScoreGap.Value=row[Quality.SCOREGAP_UDdmm].ToString();
					tnud_TrimGrindingMultiplier.Value=row[Quality.TRIM_GRINDINGS_MULTIPLIER].ToString();
          tnud_GrindingDuration.Value = row[Quality.GRINDING_DISTANCE_UDm].ToString();
					tnud_LubricationParameter.Value=row[Quality.LUBRICATION_PARAMETER_UDm].ToString();
					tnud_LubricationTime.Value=row[Quality.LUBRICATION_DURATION_UTms].ToString();
					tnud_GrindingParameter.Value=row[Quality.GRINDING_PARAMETER_UDm].ToString();
					tnud_BoardThickness.Value=row[Quality.THICKNESS_UDdmm].ToString();
          //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
          chk_InternalFlapDisabled.Checked = row[Quality.TWIN400_INTERNAL_FLAP_DISABLED].ToString() == "0" ? false : true;
					//RFAZZI_FI12/02/2009_1.Added Quality Weight into Quality Pages. 
					tnud_WeightFactor.Value = row[Quality.WEIGHT_FACTOR].ToString();
				}
			}
			catch(Exception ex)
			{
				Logger.LogError(this, "Quality Details", ex);
			}
		}

		private void CleanTheGraphics()
		{
			ttb_QualityID.Value="";
			ttb_Text.Value="";
      tnud_ScoreGap.Value="0";
			tnud_TrimGrindingMultiplier.Value="0";
			tnud_GrindingDuration.Value="0";
			tnud_LubricationParameter.Value="0";
			tnud_LubricationTime.Value="0";
			tnud_GrindingParameter.Value="0";
			tnud_BoardThickness.Value="0";
      tlst_FluteID.SelectedIndex = 0;
      //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
      chk_InternalFlapDisabled.Checked = false;
			//RFAZZI_FI12/02/2009_1.Added Quality Weight into Quality Pages. 
			tnud_WeightFactor.Value = "0";
		}

    private void tlst_FluteID_YN_SelectedItemChanged(object sender, System.EventArgs e)
		{
      if (this.state == State.Read) return;
      if(tlst_FluteID.SelectedItem!=null)
			{
        if (tlst_FluteID.SelectedItem != "")
        {
          //Flute
          //DataRow row = this.flute.flute.Tables[Flute.FLUTE_LIST].Rows.Find(tlst_FluteID.SelectedItem);

          DataRow row = DBManager.getDBManager().flute.getRow(tlst_FluteID.SelectedItem);

          this.tnud_BoardThickness.Value = row[Flute.THICKNESS_UDdmm].ToString();
          this.tnud_GrindingParameter.Value = row[Flute.GRINDING_PARAMETER_UDm].ToString();
          this.tnud_GrindingDuration.Value = row[Flute.GRINDING_DISTANCE_UDm].ToString();
          this.tnud_LubricationParameter.Value = row[Flute.LUBRICATION_PARAMETER_UDm].ToString();
          this.tnud_LubricationTime.Value = row[Flute.LUBRICATION_DURATION_UTms].ToString();
          this.tnud_ScoreGap.Value = row[Flute.SCOREGAP_UDdmm].ToString();
          this.tnud_TrimGrindingMultiplier.Value = row[Flute.TRIM_GRINDINGS_MULTIPLIER].ToString();
          //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
          //agrassi_FI15-01-09_2
					this.chk_InternalFlapDisabled.Checked = row[Flute.TWIN400_INTERNAL_FLAP_DISABLED].ToString() == "0" ? false : true;
				}
      }
		}

		protected override void F10()
		{
		}

		private void ttb_QualityID_Validating(object sender, CancelEventArgs e)
		{
			if (ttb_QualityID.Value.Trim() == "")
			{
				string text = PageManager.getPageManager()._SetMessage("§SYCC_02", true);
				ttb_QualityID.SetError(text);
			}
			else
				ttb_QualityID.SetError("");
		}

		private void Frm_twin400_quality_Activated(object sender, EventArgs e)
		{
      //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
      updateControlsEnable(); 
			if (state.Equals(State.Read))
				lst_Quality_NN.Focus();
		}

		private void lst_Quality_NN_Leave(object sender, EventArgs e)
		{
			if (state.Equals(State.Read))
				lst_Quality_NN.Focus();
		}
	}
}

