using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.DataBase.EventArgs;
using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.Twin400.Flutes
{
	public class Frm_twin400_flutes : SyncroC.Forms.Pages.Twin400.Flutes.Frm_twin400_flutesSubgroup
	{
		private SyncroC.Controls.Title title2;
		private SyncroC.Controls.TitledNumericUpDown tnud_BoardThickness;
		private SyncroC.Controls.TitledNumericUpDown tnud_GrindingDistance;
		private SyncroC.Controls.TitledNumericUpDown tnud_TrimGrindingMultiplier;
		private SyncroC.Controls.TitledNumericUpDown tnud_GrindingParameter;
		private SyncroC.Controls.TitledNumericUpDown tnud_ScoreGap;
		private SyncroC.Controls.TitledTextBox ttb_Text;
		private SyncroC.Controls.TitledTextBox ttb_FluteID;
		private SyncroC.Controls.Title ttl_FlutesList;
		private SyncroC.Controls.Grid lst_Flutes_NN;
		private SyncroC.Controls.TitledNumericUpDown tnud_LubricationTime;
		private SyncroC.Controls.TitledNumericUpDown tnud_LubricationParameter;
		private SyncroC.Controls.Title title1;
		private System.Windows.Forms.ColumnHeader FluteID;
		private System.ComponentModel.IContainer components = null;

		private enum State {Insert, Modify, Read};
		private State state=State.Read;

		private string SF1_000="-";
		private string SF2_000="-";
		private SyncroC.Controls.Bracket brk_Details;
		private string SF3_000="-";

    //per onde
    private bool modScoreGap = false;
    private bool modTrimGrindMultiplier = false;
    private bool modGrindDistance = false;
    private bool modGrindFrequency = false;
    private bool modLubeTime = false;
    private bool modLubeFrequency = false;
    private bool modThickness = false;
		//RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
		private bool modInternalFlapDisabled = false;
		private SyncroC.Controls.CheckBox chk_InternalFlapDisabled;
		private SyncroC.Controls.Title title3;
		private bool deleting;

		private void updateControlsEnable()
		{

      //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
      if (chk_InternalFlapDisabled.Visible == false)
				chk_InternalFlapDisabled.Visible = true;

			if (chk_InternalFlapDisabled.Enabled == false)
				chk_InternalFlapDisabled.Enabled = true;
		}

    public Frm_twin400_flutes()
		{
			InitializeComponent();
			DBManager.getDBManager().flute.OnDBFluteChange += new SyncroC.DataBase.Flute.DBChangedHandler(refresh);
      this.deleting = false;
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
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
			DBManager.getDBManager().flute.OnDBFluteChange -= new SyncroC.DataBase.Flute.DBChangedHandler(refresh);
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_flutes));
      this.title2 = new SyncroC.Controls.Title();
      this.tnud_BoardThickness = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_LubricationTime = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_GrindingDistance = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_TrimGrindingMultiplier = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_LubricationParameter = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_GrindingParameter = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_ScoreGap = new SyncroC.Controls.TitledNumericUpDown();
      this.ttb_Text = new SyncroC.Controls.TitledTextBox();
      this.ttb_FluteID = new SyncroC.Controls.TitledTextBox();
      this.ttl_FlutesList = new SyncroC.Controls.Title();
      this.lst_Flutes_NN = new SyncroC.Controls.Grid();
      this.FluteID = new System.Windows.Forms.ColumnHeader();
      this.title1 = new SyncroC.Controls.Title();
      this.brk_Details = new SyncroC.Controls.Bracket();
      this.chk_InternalFlapDisabled = new SyncroC.Controls.CheckBox();
      this.title3 = new SyncroC.Controls.Title();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabB
      // 
      this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Text = "New";
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Text = "Erase";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Text = "Modify";
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
      // title2
      // 
      this.title2.BackColor = System.Drawing.Color.Black;
      this.title2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title2.Location = new System.Drawing.Point(404, 347);
      this.title2.Name = "title2";
      this.title2.Size = new System.Drawing.Size(407, 264);
      this.title2.Style = SyncroC.Controls.TitleStyles.Small;
      this.title2.TabIndex = 160;
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
      this.tnud_BoardThickness.Location = new System.Drawing.Point(834, 459);
      this.tnud_BoardThickness.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_BoardThickness.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_BoardThickness.Name = "tnud_BoardThickness";
      this.tnud_BoardThickness.ReadOnly = true;
      this.tnud_BoardThickness.Size = new System.Drawing.Size(167, 64);
      this.tnud_BoardThickness.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_BoardThickness.TabIndex = 9;
      this.tnud_BoardThickness.TabStop = false;
      this.tnud_BoardThickness.Text = "Board Thickness";
      this.tnud_BoardThickness.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_BoardThickness.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_BoardThickness.TranslateText = true;
      this.tnud_BoardThickness.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tnud_BoardThickness.Value = "0";
      this.tnud_BoardThickness.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_BoardThickness.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_BoardThickness.ValueChanged += new System.EventHandler(this.tnud_Thickness_ValueChanged);
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
      this.tnud_LubricationTime.Location = new System.Drawing.Point(620, 379);
      this.tnud_LubricationTime.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
      this.tnud_LubricationTime.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_LubricationTime.Name = "tnud_LubricationTime";
      this.tnud_LubricationTime.ReadOnly = true;
      this.tnud_LubricationTime.Size = new System.Drawing.Size(191, 65);
      this.tnud_LubricationTime.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_LubricationTime.TabIndex = 6;
      this.tnud_LubricationTime.Text = "Lubrication Time";
      this.tnud_LubricationTime.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationTime.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LubricationTime.TranslateText = true;
      this.tnud_LubricationTime.UnitMeasure = SyncroC.Utility.UnitMeasure.TenthOfSecond;
      this.tnud_LubricationTime.Value = "0";
      this.tnud_LubricationTime.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationTime.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_LubricationTime.ValueChanged += new System.EventHandler(this.tnud_LubricationTime_ValueChanged);
      // 
      // tnud_GrindingDistance
      // 
      this.tnud_GrindingDistance.DecimalPlaces = 0;
      this.tnud_GrindingDistance.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_GrindingDistance.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingDistance.ImagePassword = null;
      this.tnud_GrindingDistance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GrindingDistance.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_GrindingDistance.Location = new System.Drawing.Point(412, 379);
      this.tnud_GrindingDistance.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
      this.tnud_GrindingDistance.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_GrindingDistance.Name = "tnud_GrindingDistance";
      this.tnud_GrindingDistance.ReadOnly = true;
      this.tnud_GrindingDistance.Size = new System.Drawing.Size(191, 65);
      this.tnud_GrindingDistance.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_GrindingDistance.TabIndex = 3;
      this.tnud_GrindingDistance.TabStop = false;
      this.tnud_GrindingDistance.Text = "Grinding Distance";
      this.tnud_GrindingDistance.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingDistance.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingDistance.TranslateText = true;
      this.tnud_GrindingDistance.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_GrindingDistance.Value = "0";
      this.tnud_GrindingDistance.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingDistance.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_GrindingDistance.ValueChanged += new System.EventHandler(this.tnud_GrindingDistance_ValueChanged);
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
      this.tnud_TrimGrindingMultiplier.Location = new System.Drawing.Point(412, 539);
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
      this.tnud_TrimGrindingMultiplier.Size = new System.Drawing.Size(191, 65);
      this.tnud_TrimGrindingMultiplier.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_TrimGrindingMultiplier.TabIndex = 5;
      this.tnud_TrimGrindingMultiplier.TabStop = false;
      this.tnud_TrimGrindingMultiplier.Text = "Trim Grindings Multiplier";
      this.tnud_TrimGrindingMultiplier.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_TrimGrindingMultiplier.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_TrimGrindingMultiplier.TranslateText = true;
      this.tnud_TrimGrindingMultiplier.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_TrimGrindingMultiplier.Value = "1";
      this.tnud_TrimGrindingMultiplier.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_TrimGrindingMultiplier.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_TrimGrindingMultiplier.ValueChanged += new System.EventHandler(this.tnud_TrimGrindingMultiplier_ValueChanged);
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
      this.tnud_LubricationParameter.Location = new System.Drawing.Point(620, 459);
      this.tnud_LubricationParameter.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
      this.tnud_LubricationParameter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_LubricationParameter.Name = "tnud_LubricationParameter";
      this.tnud_LubricationParameter.ReadOnly = true;
      this.tnud_LubricationParameter.Size = new System.Drawing.Size(191, 65);
      this.tnud_LubricationParameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_LubricationParameter.TabIndex = 7;
      this.tnud_LubricationParameter.Text = "Lubrication Frequency";
      this.tnud_LubricationParameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationParameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LubricationParameter.TranslateText = true;
      this.tnud_LubricationParameter.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_LubricationParameter.Value = "0";
      this.tnud_LubricationParameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationParameter.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_LubricationParameter.ValueChanged += new System.EventHandler(this.tnud_LubricationParameter_ValueChanged);
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
      this.tnud_GrindingParameter.Location = new System.Drawing.Point(412, 459);
      this.tnud_GrindingParameter.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
      this.tnud_GrindingParameter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_GrindingParameter.Name = "tnud_GrindingParameter";
      this.tnud_GrindingParameter.ReadOnly = true;
      this.tnud_GrindingParameter.Size = new System.Drawing.Size(191, 65);
      this.tnud_GrindingParameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_GrindingParameter.TabIndex = 4;
      this.tnud_GrindingParameter.TabStop = false;
      this.tnud_GrindingParameter.Text = "Grinding Frequency";
      this.tnud_GrindingParameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingParameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingParameter.TranslateText = true;
      this.tnud_GrindingParameter.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_GrindingParameter.Value = "0";
      this.tnud_GrindingParameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingParameter.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_GrindingParameter.ValueChanged += new System.EventHandler(this.tnud_GrindingParameter_ValueChanged);
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
      this.tnud_ScoreGap.Location = new System.Drawing.Point(834, 379);
      this.tnud_ScoreGap.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_ScoreGap.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_ScoreGap.Name = "tnud_ScoreGap";
      this.tnud_ScoreGap.ReadOnly = true;
      this.tnud_ScoreGap.Size = new System.Drawing.Size(167, 65);
      this.tnud_ScoreGap.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_ScoreGap.TabIndex = 8;
      this.tnud_ScoreGap.TabStop = false;
      this.tnud_ScoreGap.Text = "Score Gap";
      this.tnud_ScoreGap.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ScoreGap.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ScoreGap.TranslateText = true;
      this.tnud_ScoreGap.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tnud_ScoreGap.Value = "0";
      this.tnud_ScoreGap.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ScoreGap.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_ScoreGap.ValueChanged += new System.EventHandler(this.tnud_ScoreGap_ValueChanged);
      // 
      // ttb_Text
      // 
      this.ttb_Text.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ttb_Text.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Text.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Text.ImagePassword = null;
      this.ttb_Text.Location = new System.Drawing.Point(612, 225);
      this.ttb_Text.MaxChar = 100;
      this.ttb_Text.Multiline = false;
      this.ttb_Text.Name = "ttb_Text";
      this.ttb_Text.PasswordChar = '\0';
      this.ttb_Text.ReadOnly = false;
      this.ttb_Text.Size = new System.Drawing.Size(468, 64);
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
      // ttb_FluteID
      // 
      this.ttb_FluteID.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_FluteID.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_FluteID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_FluteID.ImagePassword = null;
      this.ttb_FluteID.Location = new System.Drawing.Point(404, 225);
      this.ttb_FluteID.MaxChar = 30;
      this.ttb_FluteID.Multiline = false;
      this.ttb_FluteID.Name = "ttb_FluteID";
      this.ttb_FluteID.PasswordChar = '\0';
      this.ttb_FluteID.ReadOnly = true;
      this.ttb_FluteID.Size = new System.Drawing.Size(104, 64);
      this.ttb_FluteID.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_FluteID.TabIndex = 1;
      this.ttb_FluteID.TabStop = false;
      this.ttb_FluteID.Text = "Flute ID";
      this.ttb_FluteID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_FluteID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_FluteID.TranslateText = true;
      this.ttb_FluteID.Value = " ";
      this.ttb_FluteID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_FluteID.Validating += new System.ComponentModel.CancelEventHandler(this.ttb_FluteID_Validating);
      // 
      // ttl_FlutesList
      // 
      this.ttl_FlutesList.BackColor = System.Drawing.Color.Black;
      this.ttl_FlutesList.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_FlutesList.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_FlutesList.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_FlutesList.Location = new System.Drawing.Point(30, 193);
      this.ttl_FlutesList.Name = "ttl_FlutesList";
      this.ttl_FlutesList.Size = new System.Drawing.Size(315, 518);
      this.ttl_FlutesList.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_FlutesList.TabIndex = 158;
      this.ttl_FlutesList.TabStop = false;
      this.ttl_FlutesList.Text = "FLUTES";
      this.ttl_FlutesList.TranslateText = true;
      // 
      // lst_Flutes_NN
      // 
      this.lst_Flutes_NN.BackColor = System.Drawing.Color.LightGray;
      this.lst_Flutes_NN.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FluteID});
      this.lst_Flutes_NN.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Narrow;
      this.lst_Flutes_NN.EvenItemsColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.lst_Flutes_NN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lst_Flutes_NN.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.lst_Flutes_NN.HideSelection = false;
      this.lst_Flutes_NN.ImagePassword = null;
      this.lst_Flutes_NN.Location = new System.Drawing.Point(42, 225);
      this.lst_Flutes_NN.MultiSelect = true;
      this.lst_Flutes_NN.Name = "lst_Flutes_NN";
      this.lst_Flutes_NN.OddItemsColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.lst_Flutes_NN.ShowForecolorItem = false;
      this.lst_Flutes_NN.ShowIndexesItem = false;
      this.lst_Flutes_NN.Size = new System.Drawing.Size(308, 483);
      this.lst_Flutes_NN.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lst_Flutes_NN.Style = SyncroC.Controls.GridStyles.Light;
      this.lst_Flutes_NN.TabIndex = 157;
      this.lst_Flutes_NN.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.lst_Flutes_NN.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.lst_Flutes_NN.TranslateText = false;
      this.lst_Flutes_NN.Leave += new System.EventHandler(this.lst_Flutes_NN_Leave);
      this.lst_Flutes_NN.SelectedIndexChanged += new System.EventHandler(this.lst_Flutes_NN_SelectedIndexChanged);
      // 
      // FluteID
      // 
      this.FluteID.Text = "Flute ID";
      this.FluteID.Width = 491;
      // 
      // title1
      // 
      this.title1.BackColor = System.Drawing.Color.Black;
      this.title1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title1.Location = new System.Drawing.Point(826, 347);
      this.title1.Name = "title1";
      this.title1.Size = new System.Drawing.Size(175, 264);
      this.title1.Style = SyncroC.Controls.TitleStyles.Small;
      this.title1.TabIndex = 161;
      this.title1.TabStop = false;
      this.title1.Text = "SCORES";
      this.title1.TranslateText = true;
      // 
      // brk_Details
      // 
      this.brk_Details.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.brk_Details.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.brk_Details.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_Details.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_Details.Location = new System.Drawing.Point(357, 193);
      this.brk_Details.Name = "brk_Details";
      this.brk_Details.Position = SyncroC.Controls.BracketPosition.Right;
      this.brk_Details.ShowArrow = true;
      this.brk_Details.Size = new System.Drawing.Size(844, 519);
      this.brk_Details.TabIndex = 162;
      this.brk_Details.Text = "DETAILS";
      this.brk_Details.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_Details.TranslateText = true;
      // 
      // chk_InternalFlapDisabled
      // 
      this.chk_InternalFlapDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_InternalFlapDisabled.Checked = false;
      this.chk_InternalFlapDisabled.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_InternalFlapDisabled.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_InternalFlapDisabled.ImagePassword = null;
      this.chk_InternalFlapDisabled.Location = new System.Drawing.Point(1041, 379);
      this.chk_InternalFlapDisabled.Name = "chk_InternalFlapDisabled";
      this.chk_InternalFlapDisabled.ReadOnly = true;
      this.chk_InternalFlapDisabled.Size = new System.Drawing.Size(160, 74);
      this.chk_InternalFlapDisabled.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_InternalFlapDisabled.TabIndex = 10;
      this.chk_InternalFlapDisabled.Text = "Internal Flap Disabled";
      this.chk_InternalFlapDisabled.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_InternalFlapDisabled.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_InternalFlapDisabled.TranslateText = true;
      this.chk_InternalFlapDisabled.OnCheckedChanged += new System.EventHandler(this.chk_InternalFlapDisabled_ValueChanged);
      // 
      // title3
      // 
      this.title3.BackColor = System.Drawing.Color.Black;
      this.title3.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title3.Location = new System.Drawing.Point(1026, 347);
      this.title3.Name = "title3";
      this.title3.Size = new System.Drawing.Size(175, 264);
      this.title3.Style = SyncroC.Controls.TitleStyles.Small;
      this.title3.TabIndex = 164;
      this.title3.TabStop = false;
      this.title3.Text = "OTHERS";
      this.title3.TranslateText = true;
      // 
      // Frm_twin400_flutes
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.title3);
      this.Controls.Add(this.chk_InternalFlapDisabled);
      this.Controls.Add(this.brk_Details);
      this.Controls.Add(this.title1);
      this.Controls.Add(this.title2);
      this.Controls.Add(this.tnud_BoardThickness);
      this.Controls.Add(this.tnud_LubricationTime);
      this.Controls.Add(this.tnud_GrindingDistance);
      this.Controls.Add(this.tnud_TrimGrindingMultiplier);
      this.Controls.Add(this.tnud_LubricationParameter);
      this.Controls.Add(this.tnud_GrindingParameter);
      this.Controls.Add(this.tnud_ScoreGap);
      this.Controls.Add(this.ttb_Text);
      this.Controls.Add(this.ttb_FluteID);
      this.Controls.Add(this.ttl_FlutesList);
      this.Controls.Add(this.lst_Flutes_NN);
      this.Name = "Frm_twin400_flutes";
      this.Tag = "";
      this.Text = "Frm_twin400_flutes";
      this.Activated += new System.EventHandler(this.Frm_twin400_flutes_Activated);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
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
      this.Controls.SetChildIndex(this.lst_Flutes_NN, 0);
      this.Controls.SetChildIndex(this.ttl_FlutesList, 0);
      this.Controls.SetChildIndex(this.ttb_FluteID, 0);
      this.Controls.SetChildIndex(this.ttb_Text, 0);
      this.Controls.SetChildIndex(this.tnud_ScoreGap, 0);
      this.Controls.SetChildIndex(this.tnud_GrindingParameter, 0);
      this.Controls.SetChildIndex(this.tnud_LubricationParameter, 0);
      this.Controls.SetChildIndex(this.tnud_TrimGrindingMultiplier, 0);
      this.Controls.SetChildIndex(this.tnud_GrindingDistance, 0);
      this.Controls.SetChildIndex(this.tnud_LubricationTime, 0);
      this.Controls.SetChildIndex(this.tnud_BoardThickness, 0);
      this.Controls.SetChildIndex(this.title2, 0);
      this.Controls.SetChildIndex(this.title1, 0);
      this.Controls.SetChildIndex(this.brk_Details, 0);
      this.Controls.SetChildIndex(this.chk_InternalFlapDisabled, 0);
      this.Controls.SetChildIndex(this.title3, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

		protected override void F11()
		{
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
		
    protected override void CtrlF1()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
        return;
      if (!this.IsOperationPossible())
        return;
      lst_Flutes_NN.UnSelectAll();

			//clean the graphics
			ttb_FluteID.Value="";
			ttb_Text.Value="";
			tnud_ScoreGap.Value="0";
			tnud_TrimGrindingMultiplier.Value="0";
			tnud_GrindingDistance.Value="0";
			tnud_GrindingParameter.Value="0";
			tnud_LubricationTime.Value="0";
			tnud_LubricationParameter.Value="0";
			tnud_BoardThickness.Value="0";

			state=State.Insert;
			readOnly(false);
			ttb_FluteID.Focus();
			lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor = lbl_Status.BackColor;
		}

		/// <summary>
		/// CtrlF2 key behaviour: Delete an existing record
		/// </summary>
		protected override void CtrlF2()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
        return;
      if (!this.IsOperationPossible())
        return;
      this.deleting = true;
      if (lst_Flutes_NN.Focused)
			{
				//DataRowCollection rows = DBManager.getDBManager().flute.flute.Tables[SyncroC.DataBase.Flute.FLUTE_LIST].Rows;
        DataRowCollection rows = DBManager.getDBManager().flute.getTable().Rows;
				if (rows.Count > 0)
				{
					Frm_Confirm conf = new Frm_Confirm();
					string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_39");
					if (lst_Flutes_NN.getSelectedIndices().Count != 0
						&& conf.ShowDialog(this, text).Equals(DialogResult.OK))
					{
						ArrayList key = new ArrayList();
						foreach (ListViewItem a in lst_Flutes_NN.getSelectedItems())
							key.Add(a.SubItems[a.SubItems.Count - 1].Text);

						//reverse the key ArrayList for a strange bug in the listview
						key.Reverse();

						CommunicationManager cm = CommunicationManager.GetCommunicationManager();
						foreach (string b in key)
							cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_FLUTE + "." + Command.DB_CMD_DEL + "(" + b + ")");
					}
				}
				state=State.Read;
				lst_Flutes_NN.Focus();
				readOnly(true);
				lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
					lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
			}
      this.deleting = false;
    }

		/// <summary>
		/// CtrlF3 key behaviour: Delete all DB flute
		/// </summary>
		protected override void CtrlF3()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
        return;
      if (!this.IsOperationPossible())
        return;
      this.deleting = true;
      Frm_Confirm conf = new Frm_Confirm();
			string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_40");
			if (lst_Flutes_NN.getItems().Count > 0
				&& conf.ShowDialog(this, text).Equals(DialogResult.OK))
			{
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_FLUTE + "." + Command.DB_CMD_CLR + "()");
				state=State.Read;
				lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
					lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
				lst_Flutes_NN.Focus();
				readOnly(true);
			}
      this.deleting = false;
    }

		/// <summary>
		/// CtrlF4 key behaviour: Save a flute
		/// </summary>
		protected override void CtrlF4()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
        return;
      if (!this.IsOperationPossible())
        return;
      if (ttb_FluteID.Value.Trim() == "")
			{
				PageManager.getPageManager()._SetMessage("SYCC_10", true);
				return;
			}
			
			string cb1;
				cb1=SF1_000;
			string cb2;
				cb2=SF2_000;
			string cb3;
				cb3=SF3_000;
			string toSend = Command.DB_FLUTE + ".";
			if(state == State.Modify)
				toSend += Command.DB_CMD_MOD;
			else
				toSend += Command.DB_CMD_ADD;
			toSend += "(";

			if((state==State.Modify)||(state==State.Insert))
			{
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_DB_MANAGER, 
					toSend +
					ttb_FluteID.Value + Command.PARAMETER_SEPARATOR +
					cb1+cb2+cb3 + Command.PARAMETER_SEPARATOR +
					ttb_Text.Value + Command.PARAMETER_SEPARATOR +
					tnud_ScoreGap.Value + Command.PARAMETER_SEPARATOR +
					tnud_GrindingParameter.Value + Command.PARAMETER_SEPARATOR +
					tnud_LubricationParameter.Value + Command.PARAMETER_SEPARATOR +
					tnud_TrimGrindingMultiplier.Value + Command.PARAMETER_SEPARATOR +
					tnud_GrindingDistance.Value+ Command.PARAMETER_SEPARATOR +
					tnud_LubricationTime.Value + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					"0" + Command.PARAMETER_SEPARATOR +
					tnud_BoardThickness.Value + Command.PARAMETER_SEPARATOR +
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
					"0" + 
					")");
        //per onde
        if (this.state == State.Modify)
          this.ManageQualityChangePerFlutes();
			}
			this.Escape();
		}

		/// <summary>
		/// CtrlF5 key behaviour: Modify a flute
		/// </summary>
		protected override void CtrlF5()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditOperatorSettings)
        return;
      if (!this.IsOperationPossible())
        return;
      if (lst_Flutes_NN.getItems().Count <= 0)
				return;

			if(state==State.Insert)
				return;
                
			state=State.Modify;
			readOnly(false);
			ttb_Text.Focus();
			lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
		}

		private void readOnly(bool isReadOnly)
		{
			if(state==State.Insert)
			{
				//tab
				ttb_FluteID.TabStop=true;

				//readonly
				ttb_FluteID.ReadOnly=false;
			}
			else
			{
				//tab
				ttb_FluteID.TabStop=false;

				//readonly
				ttb_FluteID.ReadOnly=true;
			}

      //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
      chk_InternalFlapDisabled.TabStop = !isReadOnly;
			chk_InternalFlapDisabled.ReadOnly = isReadOnly;

			if(isReadOnly)
			{
				//tab
				lst_Flutes_NN.TabStop=true;

				ttb_Text.TabStop=false;
				tnud_ScoreGap.TabStop=false;
				tnud_TrimGrindingMultiplier.TabStop=false;
				tnud_GrindingDistance.TabStop=false;
				tnud_GrindingParameter.TabStop=false;
				tnud_LubricationTime.TabStop=false;
				tnud_LubricationParameter.TabStop=false;
				tnud_BoardThickness.TabStop=false;

				//readonly
				ttb_Text.ReadOnly=true;
				tnud_ScoreGap.ReadOnly=true;
				tnud_TrimGrindingMultiplier.ReadOnly=true;
				tnud_GrindingDistance.ReadOnly=true;
				tnud_GrindingParameter.ReadOnly=true;
				tnud_LubricationTime.ReadOnly=true;
				tnud_LubricationParameter.ReadOnly=true;
				tnud_BoardThickness.ReadOnly=true;

        //per onde
        if (isReadOnly)
        {
          tnud_ScoreGap.BackColor = tnud_TrimGrindingMultiplier.BackColor = tnud_GrindingDistance.BackColor =
          tnud_GrindingParameter.BackColor = tnud_LubricationTime.BackColor =
          tnud_LubricationParameter.BackColor = tnud_BoardThickness.BackColor = Color.WhiteSmoke;

          this.modGrindDistance = this.modGrindFrequency = 
          this.modLubeFrequency = this.modLubeTime =
          this.modScoreGap = this.modThickness = this.modTrimGrindMultiplier = false;

          //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
          chk_InternalFlapDisabled.BackColor = Color.WhiteSmoke;
					this.modInternalFlapDisabled = false;
				}
      }
			else
			{
				//tab
				lst_Flutes_NN.TabStop=false;

				ttb_Text.TabStop=true;
				tnud_ScoreGap.TabStop=true;
				tnud_TrimGrindingMultiplier.TabStop=true;
				tnud_GrindingDistance.TabStop=true;
				tnud_GrindingParameter.TabStop=true;
				tnud_LubricationTime.TabStop=true;
				tnud_LubricationParameter.TabStop=true;
				tnud_BoardThickness.TabStop=true;

				//readonly
				ttb_Text.ReadOnly=false;
				tnud_ScoreGap.ReadOnly=false;
				tnud_TrimGrindingMultiplier.ReadOnly=false;
				tnud_GrindingDistance.ReadOnly=false;
				tnud_GrindingParameter.ReadOnly=false;
				tnud_LubricationTime.ReadOnly=false;
				tnud_LubricationParameter.ReadOnly=false;
				tnud_BoardThickness.ReadOnly=false;
			}
		}

		/// <summary>
		/// Escape key behaviour: Abort the operation
		/// </summary>
		protected override void Escape()
		{
      if (lst_Flutes_NN.Focused)
        return;
      else
      {
        lst_Flutes_NN.TabStop = true;
        lst_Flutes_NN.Focus();
      }
      this.state = State.Read;
			lst_Flutes_NN_SelectedIndexChanged(null,null);
		}


		private void lst_Flutes_NN_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.state!=State.Read) return;
			lst_Flutes_NN.Focus();
			readOnly(true);
			lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
				lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
      lst_Flutes_NN.Focus();
			try
			{
				//if(DBManager.getDBManager().flute.flute.Tables[SyncroC.DataBase.Flute.FLUTE_LIST].Rows.Count==0)
        if (DBManager.getDBManager().flute.getCount() == 0)
				{
					//clean the graphics
					ttb_FluteID.Value="";
					ttb_Text.Value="";
					tnud_ScoreGap.Value="0";
					tnud_TrimGrindingMultiplier.Value="0";
					tnud_GrindingDistance.Value="0";
					tnud_GrindingParameter.Value="0";
					tnud_LubricationTime.Value="0";
					tnud_LubricationParameter.Value="0";
					tnud_BoardThickness.Value="0";
          //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
          chk_InternalFlapDisabled.Checked = false;
				}
				else
				{
					//search for the DryEndList selected row into the database
					//DataRow row = DBManager.getDBManager().flute.flute.Tables[SyncroC.DataBase.Flute.FLUTE_LIST].Rows.Find(lst_Flutes_NN.getSelectedItems()[0].Text);
          DataRow row = DBManager.getDBManager().flute.getRow(lst_Flutes_NN.getSelectedItems()[0].Text);
					
					ttb_FluteID.Value=row[SyncroC.DataBase.Flute.FLUTE_ID].ToString();
					ttb_Text.Value=row[SyncroC.DataBase.Flute.TEXT].ToString();
					tnud_ScoreGap.Value=row[SyncroC.DataBase.Flute.SCOREGAP_UDdmm].ToString();
					tnud_TrimGrindingMultiplier.Value=row[SyncroC.DataBase.Flute.TRIM_GRINDINGS_MULTIPLIER].ToString();
          tnud_GrindingDistance.Value = row[SyncroC.DataBase.Flute.GRINDING_DISTANCE_UDm].ToString();
					tnud_GrindingParameter.Value=row[SyncroC.DataBase.Flute.GRINDING_PARAMETER_UDm].ToString();
					tnud_LubricationTime.Value=row[SyncroC.DataBase.Flute.LUBRICATION_DURATION_UTms].ToString();
					tnud_LubricationParameter.Value=row[SyncroC.DataBase.Flute.LUBRICATION_PARAMETER_UDm].ToString();
					tnud_BoardThickness.Value=row[SyncroC.DataBase.Flute.THICKNESS_UDdmm].ToString();
          //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
          chk_InternalFlapDisabled.Checked = row[SyncroC.DataBase.Flute.TWIN400_INTERNAL_FLAP_DISABLED].ToString() == "0" ? false : true;
				}
			}
			catch(Exception ex)
			{
				Logger.LogError(this, "Flute Details", ex);
			}
		}

		/// <summary>
		/// refresh all the data stored in a local databases showed on this page.
		/// to be called at the beginning and at every database change.
		/// </summary>
		public override void refreshData()
		{
      if ((this.state != State.Read) || this.deleting) return;
			lst_Flutes_NN.ClearItems();
			
      //DataTable fluteTable = DBManager.getDBManager().flute.flute.Tables[SyncroC.DataBase.Flute.FLUTE_LIST];
      DataTable fluteTable = DBManager.getDBManager().flute.getTable();

			for (int i = 0; i < fluteTable.Rows.Count; i++)
			{
				lst_Flutes_NN.AddItem(
					new ListViewItem(
					new String[]{
            fluteTable.Rows[i][SyncroC.DataBase.Flute.FLUTE_ID].ToString()}));
			}

			if(lst_Flutes_NN.getItems().Count!=0)
			{
				//lst_Flutes_NN.getItems()[0].Selected=true;
				lst_Flutes_NN.Focus();
			}
			else
			{
				//clean the graphics
				ttb_FluteID.Value="";
				ttb_Text.Value="";
				tnud_ScoreGap.Value="0";
				tnud_TrimGrindingMultiplier.Value="0";
				tnud_GrindingDistance.Value="0";
				tnud_GrindingParameter.Value="0";
				tnud_LubricationTime.Value="0";
				tnud_LubricationParameter.Value="0";
				tnud_BoardThickness.Value="0";
        //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
        chk_InternalFlapDisabled.Checked = false;
			}

			readOnly(true);
      base.refreshData();
		}

		public void refresh(object sender, DBChangedEventArgs args)
		{
			switch(args.Changement)       
			{         
				case DBChangement.Delete:
					//remove the item with the specified key that is into the last SubItem.
					if (lst_Flutes_NN.getItems().Count != 0)
						lst_Flutes_NN.RemoveItemsWithVal(args.Key, lst_Flutes_NN.getItems()[0].SubItems.Count - 1);
					break;
         
				case DBChangement.Insert:
					//DataTable fluteList = DBManager.getDBManager().flute.flute.Tables[SyncroC.DataBase.Flute.FLUTE_LIST];
          DataTable fluteList = DBManager.getDBManager().flute.getTable();
					lst_Flutes_NN.AddItem(new ListViewItem(new String[]{ fluteList.Rows[fluteList.Rows.Count - 1][SyncroC.DataBase.Flute.FLUTE_ID].ToString()}));
					break;
					
				case DBChangement.Modify:
					lst_Flutes_NN_SelectedIndexChanged(null,null);
					break;
			}
		}

    //private void lst_Flutes_NN_Enter(object sender, System.EventArgs e)
    //{
    //  state=State.Read;
    //  lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
    //    lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
    //}

    //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
    private void chk_InternalFlapDisabled_ValueChanged(object sender, EventArgs e)
		{
			if (this.state == State.Modify)
			{
				this.modInternalFlapDisabled = true;
				((Control)sender).BackColor = Color.Yellow;
			}
		}

		private void ttb_FluteID_Validating(object sender, CancelEventArgs e)
		{
			if (ttb_FluteID.Value.Trim() == "")
			{
				string text = PageManager.getPageManager()._SetMessage("§SYCC_02", true);
				ttb_FluteID.SetError(text);
			}
			else
				ttb_FluteID.SetError("");
		}

		private void Frm_twin400_flutes_Activated(object sender, EventArgs e)
		{
      //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
      updateControlsEnable();

			if (state.Equals(State.Read))
				lst_Flutes_NN.Focus();
		}

		private void lst_Flutes_NN_Leave(object sender, EventArgs e)
		{
			if (state.Equals(State.Read))
				lst_Flutes_NN.Focus();
		}
  
    private void tnud_ScoreGap_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modScoreGap = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_TrimGrindingMultiplier_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modTrimGrindMultiplier = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_GrindingDistance_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modGrindDistance = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_GrindingParameter_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modGrindFrequency = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_LubricationTime_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modLubeTime = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_LubricationParameter_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modLubeFrequency = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_Thickness_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modThickness = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    //per onde
    private void ManageQualityChangePerFlutes()
    {
      PopUp.Frm_Confirm conf = new Frm_Confirm();
      string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_140");
      DialogResult res = conf.ShowDialog(this, text);
      if ((res == DialogResult.Yes) || (res == DialogResult.OK))  //first alert
      {
        conf = new Frm_Confirm();
        text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_141");
        res = conf.ShowDialog(this, text);
        bool updateSetups = false;
        if ((res == DialogResult.Yes) || (res == DialogResult.OK)) //second alert
        {
          conf = new Frm_Confirm();
          text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_65");
          res = conf.ShowDialog(this, text);
          if ((res == DialogResult.Yes) || (res == DialogResult.OK))
            updateSetups = true;
          
          //DataTable quality = DBManager.getDBManager().quality.quality.Tables[Quality.QUALITY_LIST];
          DataTable quality = DBManager.getDBManager().quality.getTable();

          for (int i = 0; i < quality.Rows.Count; i++)
          {
            if (quality.Rows[i][Quality.FLUTE_ID].Equals(this.ttb_FluteID.Value))
            {
              string toSend = Command.DB_QUALITY + "." + Command.DB_CMD_MOD;
              if (updateSetups)
                toSend += "WithSetup";
              toSend += "(";
              toSend += this.NotEmptyString(quality.Rows[i][Quality.QUALITY_ID].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.QUALITY_TEXT].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.FLUTE_ID].ToString()) + Command.PARAMETER_SEPARATOR;
              //DRY DATA
              if (this.modScoreGap)
                toSend += this.NotEmptyString(this.tnud_ScoreGap.Value) + Command.PARAMETER_SEPARATOR;
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.SCOREGAP_UDdmm].ToString()) + Command.PARAMETER_SEPARATOR;
              if (this.modGrindFrequency)
                toSend += this.NotEmptyString(this.tnud_GrindingParameter.Value) + Command.PARAMETER_SEPARATOR;
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.GRINDING_PARAMETER_UDm].ToString()) + Command.PARAMETER_SEPARATOR;
              if (this.modLubeFrequency)
                toSend += this.NotEmptyString(this.tnud_LubricationParameter.Value) + Command.PARAMETER_SEPARATOR;
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.LUBRICATION_PARAMETER_UDm].ToString()) + Command.PARAMETER_SEPARATOR;
              if (this.modTrimGrindMultiplier)
                toSend += this.NotEmptyString(this.tnud_TrimGrindingMultiplier.Value) + Command.PARAMETER_SEPARATOR;
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.TRIM_GRINDINGS_MULTIPLIER].ToString()) + Command.PARAMETER_SEPARATOR;
              if (this.modGrindDistance)
                toSend += this.NotEmptyString(this.tnud_GrindingDistance.Value) + Command.PARAMETER_SEPARATOR;
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.GRINDING_DISTANCE_UDm].ToString()) + Command.PARAMETER_SEPARATOR;
              if (this.modLubeTime)
                toSend += this.NotEmptyString(this.tnud_LubricationTime.Value) + Command.PARAMETER_SEPARATOR;
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.LUBRICATION_DURATION_UTms].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PRODUCTION_SPEED_UVm_m].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.AOC_SPEED_UVm_m].ToString()) + Command.PARAMETER_SEPARATOR;
              if (this.modThickness)
                toSend += this.NotEmptyString(this.tnud_BoardThickness.Value) + Command.PARAMETER_SEPARATOR;
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.THICKNESS_UDdmm].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += "0" + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.BUNDLE_LAYERS_PER_STACK].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.BUNDLE_REVERSE_MODE].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_0].ToString()) + Command.PARAMETER_SEPARATOR;
							toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_0].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_1].ToString()) + Command.PARAMETER_SEPARATOR;
							toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_1].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_2].ToString()) + Command.PARAMETER_SEPARATOR;
							toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_2].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_3].ToString()) + Command.PARAMETER_SEPARATOR;
							toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_3].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_4].ToString()) + Command.PARAMETER_SEPARATOR;
							toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_4].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_5].ToString()) + Command.PARAMETER_SEPARATOR;
							toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_5].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_6].ToString()) + Command.PARAMETER_SEPARATOR;
							toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_6].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PREPRINT].ToString()) + Command.PARAMETER_SEPARATOR;
						  //PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
              //toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
							toSend += this.NotEmptyString(quality.Rows[i][Quality.INCLINATELASTBELT].ToString()) + Command.PARAMETER_SEPARATOR;
              //RFAZZI_FI12/02/2009_2.Added Parameter for disable the Flap in the Twin400 also in the PMac400 pages
              //toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
							if (this.modInternalFlapDisabled)
								toSend += (chk_InternalFlapDisabled.Checked ? "1" : "0") + Command.PARAMETER_SEPARATOR;
							else
								toSend += this.NotEmptyString(quality.Rows[i][Quality.TWIN400_INTERNAL_FLAP_DISABLED].ToString()) + Command.PARAMETER_SEPARATOR;
							toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
							toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
							toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
							toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
							toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
							toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
							toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
							toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
							toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
							toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
							toSend += "SPARE";
              toSend += ")";
              CommunicationManager cm = CommunicationManager.GetCommunicationManager();
              cm.SendTcp(cm.SYNCROS_DB_MANAGER, toSend);
              System.Threading.Thread.Sleep(400);
            }
          }//end for loop
          PopUp.Frm_Alert alert = new Frm_Alert();
          text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_142");
          res = alert.ShowDialog(text);
        }//end second alert
      }//end first alert
    }
  }
}

