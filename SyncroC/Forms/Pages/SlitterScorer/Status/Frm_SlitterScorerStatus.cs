using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.DataBase;
using SyncroC.Network;
//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
using SyncroC.DataBase.LineConfig;

namespace SyncroC.Forms.Pages.SlitterScorer.Status
{
	public class Frm_SlitterScorerStatus : SyncroC.Forms.Pages.SlitterScorer.Status.Frm_SlitterScorerStatusSubgroup
	{
		private SyncroC.Controls.StateButton stb_AutoMan;
		private SyncroC.Controls.TitledNumericUpDown nud_ScoreGapAux;
		private SyncroC.Controls.TitledNumericUpDown nud_ScoreGap1;
		private SyncroC.Controls.TitledNumericUpDown nud_ScoreGap2;
		private PictureBox pct_Lubrification;
		private PictureBox pct_maintenance;
		private PictureBox pct_Grinding;
		private SyncroC.Controls.Title ttl_CurrentSetup;
		private SyncroC.Controls.Title ttl_S1;
		private SyncroC.Controls.CheckBox chk_S1S1;
		private SyncroC.Controls.CheckBox chk_S1S2;
		private SyncroC.Controls.CheckBox chk_S1S3;
		private SyncroC.Controls.CheckBox chk_S1S4;
		private SyncroC.Controls.CheckBox chk_S1S5;
		private SyncroC.Controls.CheckBox chk_S1S6;
		private SyncroC.Controls.CheckBox chk_S1S7;
		private SyncroC.Controls.CheckBox chk_S1S8;
		private Panel pnl_S1;
		private Panel pnl_S2;
		private SyncroC.Controls.Title ttl_S2;
		private SyncroC.Controls.CheckBox chk_S2S1;
		private SyncroC.Controls.CheckBox chk_S2S2;
		private SyncroC.Controls.CheckBox chk_S2S3;
		private SyncroC.Controls.CheckBox chk_S2S4;
		private SyncroC.Controls.CheckBox chk_S2S5;
		private SyncroC.Controls.CheckBox chk_S2S6;
		private SyncroC.Controls.CheckBox chk_S2S7;
		private SyncroC.Controls.CheckBox chk_S2S8;
		private SyncroC.Controls.TitledTextBox ttb_CurrentSetupCode;
		private SyncroC.Controls.CheckBox chk_CurrentAux;
		private SyncroC.Controls.TitledTextBox ttb_CurrentSlit;
		private SyncroC.Controls.TitledTextBox ttb_CurrentScore;
		private SyncroC.Controls.CheckBox chk_CurrentTandem;
		private Panel pnl_CurrentSetup;
		private SyncroC.Controls.CheckBox chk_CurrentNoGap;
		private Panel pnl_NextSetup;
		private SyncroC.Controls.CheckBox chk_NextNoGap;
		private SyncroC.Controls.Title ttl_NextSetup;
		private SyncroC.Controls.CheckBox chk_NextTandem;
		private SyncroC.Controls.TitledTextBox ttb_NextSetupCode;
		private SyncroC.Controls.TitledTextBox ttb_NextScore;
		private SyncroC.Controls.CheckBox chk_NextAux;
		private SyncroC.Controls.TitledTextBox ttb_NextSlit;
		private SyncroC.Controls.CheckBox chk_S1S9;
		private SyncroC.Controls.CheckBox chk_S2S9;
		private PictureBox pct_Slit1;
		private ImageList imageList;
		private PictureBox pct_Slit2;
		private PictureBox pct_Score2;
		private PictureBox pct_Score1;
		private PictureBox pct_ScoreAux;
		private SyncroC.Controls.StateButton SendCurrentOrder;
		private SyncroC.Controls.StateButton SendNextOrder;
		private System.ComponentModel.IContainer components = null;

		//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
		private DBManager dbm;
		private PictureBox pct_SlitterScorer;
    private PictureBox pct_SlitterScorerCompact;
		private SyncroC.DataBase.LineConfig.SlitterScorer ss;
    private Panel Twin2;
    private Label SlitterAx1;
    private Label SlitterAx2;
    private Label ModifyScoreGap1;
    private Label ModifyScoreGap3;
    private Label ModifyScoreGap2;
    private Panel Shaft4;
    private Panel Shaft3;
    private Panel Shaft5;
    private Panel Shaft2;
    private Panel Shaft1;
    private Label ModifyScoreGap4;
    private Panel Shaft6;
    private Label lbl_ScoreGap2;
    private Label lbl_ScoreGap4;
    private Label lbl_ScoreGap1;
    private Label lbl_ScoreGap3;
    private PictureBox pct_Direction;
    private Label lbl_ord3;
    private Label lbl_ord5;
    private Label lbl_ord2;
    private Label lbl_ord1;
    private Label lbl_ord4;
    private Label lbl_NoTrid;
    private Label lbl_Num3;
    private Label lbl_Num5;
    private Label lbl_Num2;
    private Label lbl_Num1;
    private Label lbl_Num4;
    private Label lbl_AOC;
    //MSTAGETTI_FI-11-05-2010
    private bool noTrid = false; 

		public Frm_SlitterScorerStatus()
		{
			InitializeComponent();

			//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
			dbm = DBManager.getDBManager();
			this.ss = dbm.lineConfig.SlitterScorerConfig;

			if (this.ss.IsTwinE())
			{
				this.chk_CurrentTandem.Visible = true;
				this.chk_NextTandem.Visible = true;

				this.chk_CurrentNoGap.Visible = true;
				this.chk_NextNoGap.Visible = true;

				this.pct_SlitterScorer.Visible = true;
				this.pct_SlitterScorerCompact.Visible = false;
				this.nud_ScoreGap1.Visible = true;
				this.nud_ScoreGap2.Visible = true;
				if (this.ss.SlitterScorerHeader.auxPresent)
				{
					this.chk_CurrentAux.Visible = true;
					this.chk_NextAux.Visible = true;
					this.nud_ScoreGapAux.Visible = true;
				}
        //agrassi_FI25-05-2011_1
        this.Twin2.Visible = false;
        this.pct_Direction.Visible = false;
      }
			else if (this.ss.IsCompact2())
			{
				this.pct_SlitterScorer.Visible = false;
				this.pct_SlitterScorerCompact.Visible = true;
				
				//PMASSARO_FI04/11/08 - Disable the visualization in case of OldSS without control from Syncro
				/*
					nud_ScoreGap1.Location = new Point(500, 391);
					nud_ScoreGap1.Visible = true;
					if (this.ss.SlitterScorerHeader.auxPresent)
					{
						this.chk_CurrentAux.Visible = true;
						this.chk_NextAux.Visible = true;

						nud_ScoreGapAux.Location = new Point(450, 391);
						this.nud_ScoreGapAux.Visible = true;
				*/
				if (this.ss.SlitterScorerHeader.machineControlIntegratedInSyncro)
				{
					nud_ScoreGap1.Location = new Point(500, 391);
					nud_ScoreGap1.Visible = true;
					if (this.ss.SlitterScorerHeader.auxPresent)
					{
						this.chk_CurrentAux.Visible = true;
						this.chk_NextAux.Visible = true;

						nud_ScoreGapAux.Location = new Point(450, 391);
						this.nud_ScoreGapAux.Visible = true;
					}
				}
				else
				{
					this.ttb_CurrentSlit.Visible = false;
					this.ttb_NextSlit.Visible = false;
					this.ttb_CurrentScore.Visible = false;
					this.ttb_NextScore.Visible = false;
				}
        //agrassi_FI25-05-2011_1
        this.Twin2.Visible = false;
        this.pct_Direction.Visible = false;
      }
      //agrassi_FI25-05-2011_1
      else if (this.ss.IsTwin2())
      {
        this.Twin2.Visible = true;
        this.pct_Direction.Visible = true;
        this.pct_SlitterScorer.Visible = false;
        this.pct_SlitterScorerCompact.Visible = false;
        this.ttb_CurrentSlit.Visible = false;
        this.ttb_NextSlit.Visible = false;
        this.ttb_CurrentScore.Visible = false;
        this.ttb_NextScore.Visible = false;
        this.pnl_CurrentSetup.Visible = true;
        this.pnl_NextSetup.Visible = true;
        this.pct_Slit1.Visible = false;
        this.pct_Slit2.Visible = false;
        this.pct_Score1.Visible = false;
        this.pct_Score2.Visible = false;
        this.pct_ScoreAux.Visible = false;
        this.Shaft5.Visible = this.ModifyScoreGap3.Visible = (this.ss.Twin2.NumAuxShaft > 0);
        this.Shaft6.Visible = this.ModifyScoreGap4.Visible = (this.ss.Twin2.NumAuxShaft > 1);
      }

      if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
      {
        this.SendCurrentOrder.IsButton = false;
        this.SendNextOrder.IsButton = false;
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
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SlitterScorerStatus));
      this.stb_AutoMan = new SyncroC.Controls.StateButton();
      this.nud_ScoreGapAux = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_ScoreGap1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_ScoreGap2 = new SyncroC.Controls.TitledNumericUpDown();
      this.pct_Lubrification = new System.Windows.Forms.PictureBox();
      this.pct_maintenance = new System.Windows.Forms.PictureBox();
      this.pct_Grinding = new System.Windows.Forms.PictureBox();
      this.ttl_CurrentSetup = new SyncroC.Controls.Title();
      this.ttl_S1 = new SyncroC.Controls.Title();
      this.chk_S1S1 = new SyncroC.Controls.CheckBox();
      this.chk_S1S2 = new SyncroC.Controls.CheckBox();
      this.chk_S1S3 = new SyncroC.Controls.CheckBox();
      this.chk_S1S4 = new SyncroC.Controls.CheckBox();
      this.chk_S1S5 = new SyncroC.Controls.CheckBox();
      this.chk_S1S6 = new SyncroC.Controls.CheckBox();
      this.chk_S1S7 = new SyncroC.Controls.CheckBox();
      this.chk_S1S8 = new SyncroC.Controls.CheckBox();
      this.pnl_S1 = new System.Windows.Forms.Panel();
      this.chk_S1S9 = new SyncroC.Controls.CheckBox();
      this.pnl_S2 = new System.Windows.Forms.Panel();
      this.chk_S2S1 = new SyncroC.Controls.CheckBox();
      this.ttl_S2 = new SyncroC.Controls.Title();
      this.chk_S2S2 = new SyncroC.Controls.CheckBox();
      this.chk_S2S3 = new SyncroC.Controls.CheckBox();
      this.chk_S2S4 = new SyncroC.Controls.CheckBox();
      this.chk_S2S5 = new SyncroC.Controls.CheckBox();
      this.chk_S2S6 = new SyncroC.Controls.CheckBox();
      this.chk_S2S7 = new SyncroC.Controls.CheckBox();
      this.chk_S2S8 = new SyncroC.Controls.CheckBox();
      this.chk_S2S9 = new SyncroC.Controls.CheckBox();
      this.ttb_CurrentSetupCode = new SyncroC.Controls.TitledTextBox();
      this.chk_CurrentAux = new SyncroC.Controls.CheckBox();
      this.ttb_CurrentSlit = new SyncroC.Controls.TitledTextBox();
      this.ttb_CurrentScore = new SyncroC.Controls.TitledTextBox();
      this.chk_CurrentTandem = new SyncroC.Controls.CheckBox();
      this.pnl_CurrentSetup = new System.Windows.Forms.Panel();
      this.SendCurrentOrder = new SyncroC.Controls.StateButton();
      this.chk_CurrentNoGap = new SyncroC.Controls.CheckBox();
      this.pnl_NextSetup = new System.Windows.Forms.Panel();
      this.lbl_NoTrid = new System.Windows.Forms.Label();
      this.SendNextOrder = new SyncroC.Controls.StateButton();
      this.chk_NextNoGap = new SyncroC.Controls.CheckBox();
      this.ttl_NextSetup = new SyncroC.Controls.Title();
      this.chk_NextTandem = new SyncroC.Controls.CheckBox();
      this.ttb_NextSetupCode = new SyncroC.Controls.TitledTextBox();
      this.ttb_NextScore = new SyncroC.Controls.TitledTextBox();
      this.chk_NextAux = new SyncroC.Controls.CheckBox();
      this.ttb_NextSlit = new SyncroC.Controls.TitledTextBox();
      this.pct_Slit1 = new System.Windows.Forms.PictureBox();
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.pct_Slit2 = new System.Windows.Forms.PictureBox();
      this.pct_Score2 = new System.Windows.Forms.PictureBox();
      this.pct_Score1 = new System.Windows.Forms.PictureBox();
      this.pct_ScoreAux = new System.Windows.Forms.PictureBox();
      this.pct_SlitterScorer = new System.Windows.Forms.PictureBox();
      this.pct_SlitterScorerCompact = new System.Windows.Forms.PictureBox();
      this.Twin2 = new System.Windows.Forms.Panel();
      this.ModifyScoreGap4 = new System.Windows.Forms.Label();
      this.Shaft6 = new System.Windows.Forms.Panel();
      this.lbl_ScoreGap4 = new System.Windows.Forms.Label();
      this.SlitterAx1 = new System.Windows.Forms.Label();
      this.SlitterAx2 = new System.Windows.Forms.Label();
      this.ModifyScoreGap1 = new System.Windows.Forms.Label();
      this.ModifyScoreGap3 = new System.Windows.Forms.Label();
      this.ModifyScoreGap2 = new System.Windows.Forms.Label();
      this.Shaft3 = new System.Windows.Forms.Panel();
      this.lbl_Num3 = new System.Windows.Forms.Label();
      this.lbl_ord3 = new System.Windows.Forms.Label();
      this.lbl_ScoreGap1 = new System.Windows.Forms.Label();
      this.Shaft5 = new System.Windows.Forms.Panel();
      this.lbl_Num5 = new System.Windows.Forms.Label();
      this.lbl_ord5 = new System.Windows.Forms.Label();
      this.lbl_ScoreGap3 = new System.Windows.Forms.Label();
      this.Shaft2 = new System.Windows.Forms.Panel();
      this.lbl_Num2 = new System.Windows.Forms.Label();
      this.lbl_ord2 = new System.Windows.Forms.Label();
      this.Shaft1 = new System.Windows.Forms.Panel();
      this.lbl_Num1 = new System.Windows.Forms.Label();
      this.lbl_ord1 = new System.Windows.Forms.Label();
      this.Shaft4 = new System.Windows.Forms.Panel();
      this.lbl_Num4 = new System.Windows.Forms.Label();
      this.lbl_ord4 = new System.Windows.Forms.Label();
      this.lbl_ScoreGap2 = new System.Windows.Forms.Label();
      this.pct_Direction = new System.Windows.Forms.PictureBox();
      this.lbl_AOC = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Lubrification)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_maintenance)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Grinding)).BeginInit();
      this.pnl_S1.SuspendLayout();
      this.pnl_S2.SuspendLayout();
      this.pnl_CurrentSetup.SuspendLayout();
      this.pnl_NextSetup.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Slit1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Slit2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Score2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Score1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_ScoreAux)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SlitterScorer)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SlitterScorerCompact)).BeginInit();
      this.Twin2.SuspendLayout();
      this.Shaft6.SuspendLayout();
      this.Shaft3.SuspendLayout();
      this.Shaft5.SuspendLayout();
      this.Shaft2.SuspendLayout();
      this.Shaft1.SuspendLayout();
      this.Shaft4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Direction)).BeginInit();
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
      // stb_AutoMan
      // 
      this.stb_AutoMan.Active = false;
      this.stb_AutoMan.Alarmed = false;
      this.stb_AutoMan.Auto = false;
      this.stb_AutoMan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.stb_AutoMan.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.stb_AutoMan.Enabled = false;
      this.stb_AutoMan.FillBackColorWithImage = false;
      this.stb_AutoMan.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_AutoMan.FramePresent = false;
      this.stb_AutoMan.HighLightEnable = false;
      this.stb_AutoMan.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.stb_AutoMan.ImagePassword = null;
      this.stb_AutoMan.Inemergency = false;
      this.stb_AutoMan.InLocal = false;
      this.stb_AutoMan.IsButton = false;
      this.stb_AutoMan.IsForceToInvisible = false;
      this.stb_AutoMan.Location = new System.Drawing.Point(852, 207);
      this.stb_AutoMan.Name = "stb_AutoMan";
      this.stb_AutoMan.OnLine = false;
      this.stb_AutoMan.ShowImage = true;
      this.stb_AutoMan.ShowText = false;
      this.stb_AutoMan.Size = new System.Drawing.Size(100, 100);
      this.stb_AutoMan.Switching = false;
      this.stb_AutoMan.TabIndex = 218;
      this.stb_AutoMan.Text = "Auto/Man";
      this.stb_AutoMan.TranslateText = false;
      this.stb_AutoMan.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_AutoMan.UserDefinedActiveImage")));
      this.stb_AutoMan.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_AutoMan.UserDefinedImage")));
      this.stb_AutoMan.Visible = false;
      this.stb_AutoMan.Warned = false;
      // 
      // nud_ScoreGapAux
      // 
      this.nud_ScoreGapAux.DecimalPlaces = 0;
      this.nud_ScoreGapAux.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScoreGapAux.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGapAux.ImagePassword = null;
      this.nud_ScoreGapAux.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScoreGapAux.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGapAux.Location = new System.Drawing.Point(384, 391);
      this.nud_ScoreGapAux.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_ScoreGapAux.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScoreGapAux.Name = "nud_ScoreGapAux";
      this.nud_ScoreGapAux.ReadOnly = true;
      this.nud_ScoreGapAux.Size = new System.Drawing.Size(71, 40);
      this.nud_ScoreGapAux.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_ScoreGapAux.TabIndex = 219;
      this.nud_ScoreGapAux.TabStop = false;
      this.nud_ScoreGapAux.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGapAux.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScoreGapAux.TranslateText = true;
      this.nud_ScoreGapAux.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGapAux.UnitMeasureVisible = false;
      this.nud_ScoreGapAux.Value = "0";
      this.nud_ScoreGapAux.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScoreGapAux.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_ScoreGapAux.Visible = false;
      // 
      // nud_ScoreGap1
      // 
      this.nud_ScoreGap1.DecimalPlaces = 0;
      this.nud_ScoreGap1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScoreGap1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap1.ImagePassword = null;
      this.nud_ScoreGap1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScoreGap1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap1.Location = new System.Drawing.Point(474, 391);
      this.nud_ScoreGap1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_ScoreGap1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScoreGap1.Name = "nud_ScoreGap1";
      this.nud_ScoreGap1.ReadOnly = true;
      this.nud_ScoreGap1.Size = new System.Drawing.Size(71, 40);
      this.nud_ScoreGap1.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_ScoreGap1.TabIndex = 220;
      this.nud_ScoreGap1.TabStop = false;
      this.nud_ScoreGap1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScoreGap1.TranslateText = true;
      this.nud_ScoreGap1.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap1.UnitMeasureVisible = false;
      this.nud_ScoreGap1.Value = "0";
      this.nud_ScoreGap1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScoreGap1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_ScoreGap1.Visible = false;
      // 
      // nud_ScoreGap2
      // 
      this.nud_ScoreGap2.DecimalPlaces = 0;
      this.nud_ScoreGap2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScoreGap2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap2.ImagePassword = null;
      this.nud_ScoreGap2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScoreGap2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap2.Location = new System.Drawing.Point(551, 391);
      this.nud_ScoreGap2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_ScoreGap2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScoreGap2.Name = "nud_ScoreGap2";
      this.nud_ScoreGap2.ReadOnly = true;
      this.nud_ScoreGap2.Size = new System.Drawing.Size(71, 40);
      this.nud_ScoreGap2.Style = SyncroC.Controls.TitledNumericUpDownStyles.NoTitle;
      this.nud_ScoreGap2.TabIndex = 221;
      this.nud_ScoreGap2.TabStop = false;
      this.nud_ScoreGap2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScoreGap2.TranslateText = true;
      this.nud_ScoreGap2.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap2.UnitMeasureVisible = false;
      this.nud_ScoreGap2.Value = "0";
      this.nud_ScoreGap2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScoreGap2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_ScoreGap2.Visible = false;
      // 
      // pct_Lubrification
      // 
      this.pct_Lubrification.Image = global::SyncroC.Properties.Resources.LUBRIFICAZIONE_OLIO_ISO_0391;
      this.pct_Lubrification.Location = new System.Drawing.Point(720, 391);
      this.pct_Lubrification.Name = "pct_Lubrification";
      this.pct_Lubrification.Size = new System.Drawing.Size(40, 40);
      this.pct_Lubrification.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Lubrification.TabIndex = 222;
      this.pct_Lubrification.TabStop = false;
      this.pct_Lubrification.Visible = false;
      // 
      // pct_maintenance
      // 
      this.pct_maintenance.Image = global::SyncroC.Properties.Resources.MANUTENZIONE_ISO_0717;
      this.pct_maintenance.Location = new System.Drawing.Point(857, 502);
      this.pct_maintenance.Name = "pct_maintenance";
      this.pct_maintenance.Size = new System.Drawing.Size(100, 47);
      this.pct_maintenance.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_maintenance.TabIndex = 223;
      this.pct_maintenance.TabStop = false;
      this.pct_maintenance.Visible = false;
      // 
      // pct_Grinding
      // 
      this.pct_Grinding.Image = ((System.Drawing.Image)(resources.GetObject("pct_Grinding.Image")));
      this.pct_Grinding.Location = new System.Drawing.Point(718, 235);
      this.pct_Grinding.Name = "pct_Grinding";
      this.pct_Grinding.Size = new System.Drawing.Size(40, 40);
      this.pct_Grinding.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Grinding.TabIndex = 224;
      this.pct_Grinding.TabStop = false;
      this.pct_Grinding.Visible = false;
      // 
      // ttl_CurrentSetup
      // 
      this.ttl_CurrentSetup.BackColor = System.Drawing.Color.Black;
      this.ttl_CurrentSetup.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentSetup.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentSetup.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_CurrentSetup.Location = new System.Drawing.Point(4, 61);
      this.ttl_CurrentSetup.Name = "ttl_CurrentSetup";
      this.ttl_CurrentSetup.Size = new System.Drawing.Size(234, 260);
      this.ttl_CurrentSetup.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_CurrentSetup.TabIndex = 226;
      this.ttl_CurrentSetup.Text = "CURRENT SETUP";
      this.ttl_CurrentSetup.TranslateText = true;
      // 
      // ttl_S1
      // 
      this.ttl_S1.BackColor = System.Drawing.Color.Black;
      this.ttl_S1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_S1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_S1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_S1.Location = new System.Drawing.Point(3, 3);
      this.ttl_S1.Name = "ttl_S1";
      this.ttl_S1.Size = new System.Drawing.Size(549, 110);
      this.ttl_S1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_S1.TabIndex = 227;
      this.ttl_S1.Text = "SHAFT 1 Slits Status";
      this.ttl_S1.TranslateText = false;
      // 
      // chk_S1S1
      // 
      this.chk_S1S1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S1.Checked = true;
      this.chk_S1S1.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S1S1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S1S1.ImagePassword = null;
      this.chk_S1S1.Location = new System.Drawing.Point(15, 35);
      this.chk_S1S1.Name = "chk_S1S1";
      this.chk_S1S1.ReadOnly = true;
      this.chk_S1S1.Size = new System.Drawing.Size(60, 70);
      this.chk_S1S1.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S1S1.TabIndex = 275;
      this.chk_S1S1.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S1S1.TranslateText = false;
      this.chk_S1S1.Visible = false;
      // 
      // chk_S1S2
      // 
      this.chk_S1S2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S2.Checked = true;
      this.chk_S1S2.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S1S2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S1S2.ImagePassword = null;
      this.chk_S1S2.Location = new System.Drawing.Point(74, 35);
      this.chk_S1S2.Name = "chk_S1S2";
      this.chk_S1S2.ReadOnly = true;
      this.chk_S1S2.Size = new System.Drawing.Size(60, 70);
      this.chk_S1S2.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S1S2.TabIndex = 276;
      this.chk_S1S2.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S1S2.TranslateText = false;
      this.chk_S1S2.Visible = false;
      // 
      // chk_S1S3
      // 
      this.chk_S1S3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S3.Checked = true;
      this.chk_S1S3.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S1S3.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S1S3.ImagePassword = null;
      this.chk_S1S3.Location = new System.Drawing.Point(133, 35);
      this.chk_S1S3.Name = "chk_S1S3";
      this.chk_S1S3.ReadOnly = true;
      this.chk_S1S3.Size = new System.Drawing.Size(60, 70);
      this.chk_S1S3.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S1S3.TabIndex = 277;
      this.chk_S1S3.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S1S3.TranslateText = false;
      this.chk_S1S3.Visible = false;
      // 
      // chk_S1S4
      // 
      this.chk_S1S4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S4.Checked = true;
      this.chk_S1S4.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S1S4.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S1S4.ImagePassword = null;
      this.chk_S1S4.Location = new System.Drawing.Point(192, 35);
      this.chk_S1S4.Name = "chk_S1S4";
      this.chk_S1S4.ReadOnly = true;
      this.chk_S1S4.Size = new System.Drawing.Size(60, 70);
      this.chk_S1S4.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S1S4.TabIndex = 278;
      this.chk_S1S4.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S1S4.TranslateText = false;
      this.chk_S1S4.Visible = false;
      // 
      // chk_S1S5
      // 
      this.chk_S1S5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S5.Checked = false;
      this.chk_S1S5.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S1S5.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S1S5.ImagePassword = null;
      this.chk_S1S5.Location = new System.Drawing.Point(251, 35);
      this.chk_S1S5.Name = "chk_S1S5";
      this.chk_S1S5.ReadOnly = true;
      this.chk_S1S5.Size = new System.Drawing.Size(60, 70);
      this.chk_S1S5.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S1S5.TabIndex = 279;
      this.chk_S1S5.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S5.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S1S5.TranslateText = false;
      this.chk_S1S5.Visible = false;
      // 
      // chk_S1S6
      // 
      this.chk_S1S6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S6.Checked = true;
      this.chk_S1S6.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S1S6.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S1S6.ImagePassword = null;
      this.chk_S1S6.Location = new System.Drawing.Point(310, 35);
      this.chk_S1S6.Name = "chk_S1S6";
      this.chk_S1S6.ReadOnly = true;
      this.chk_S1S6.Size = new System.Drawing.Size(60, 70);
      this.chk_S1S6.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S1S6.TabIndex = 280;
      this.chk_S1S6.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S6.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S1S6.TranslateText = false;
      this.chk_S1S6.Visible = false;
      // 
      // chk_S1S7
      // 
      this.chk_S1S7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S7.Checked = true;
      this.chk_S1S7.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S1S7.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S1S7.ImagePassword = null;
      this.chk_S1S7.Location = new System.Drawing.Point(369, 35);
      this.chk_S1S7.Name = "chk_S1S7";
      this.chk_S1S7.ReadOnly = true;
      this.chk_S1S7.Size = new System.Drawing.Size(60, 70);
      this.chk_S1S7.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S1S7.TabIndex = 281;
      this.chk_S1S7.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S7.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S1S7.TranslateText = false;
      this.chk_S1S7.Visible = false;
      // 
      // chk_S1S8
      // 
      this.chk_S1S8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S8.Checked = true;
      this.chk_S1S8.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S1S8.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S1S8.ImagePassword = null;
      this.chk_S1S8.Location = new System.Drawing.Point(428, 35);
      this.chk_S1S8.Name = "chk_S1S8";
      this.chk_S1S8.ReadOnly = true;
      this.chk_S1S8.Size = new System.Drawing.Size(60, 70);
      this.chk_S1S8.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S1S8.TabIndex = 282;
      this.chk_S1S8.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S8.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S1S8.TranslateText = false;
      this.chk_S1S8.Visible = false;
      // 
      // pnl_S1
      // 
      this.pnl_S1.Controls.Add(this.ttl_S1);
      this.pnl_S1.Controls.Add(this.chk_S1S1);
      this.pnl_S1.Controls.Add(this.chk_S1S2);
      this.pnl_S1.Controls.Add(this.chk_S1S3);
      this.pnl_S1.Controls.Add(this.chk_S1S4);
      this.pnl_S1.Controls.Add(this.chk_S1S5);
      this.pnl_S1.Controls.Add(this.chk_S1S6);
      this.pnl_S1.Controls.Add(this.chk_S1S7);
      this.pnl_S1.Controls.Add(this.chk_S1S8);
      this.pnl_S1.Controls.Add(this.chk_S1S9);
      this.pnl_S1.Location = new System.Drawing.Point(30, 590);
      this.pnl_S1.Name = "pnl_S1";
      this.pnl_S1.Size = new System.Drawing.Size(551, 125);
      this.pnl_S1.TabIndex = 293;
      this.pnl_S1.Visible = false;
      // 
      // chk_S1S9
      // 
      this.chk_S1S9.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S9.Checked = true;
      this.chk_S1S9.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S1S9.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S1S9.ImagePassword = null;
      this.chk_S1S9.Location = new System.Drawing.Point(487, 35);
      this.chk_S1S9.Name = "chk_S1S9";
      this.chk_S1S9.ReadOnly = true;
      this.chk_S1S9.Size = new System.Drawing.Size(60, 70);
      this.chk_S1S9.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S1S9.TabIndex = 283;
      this.chk_S1S9.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S1S9.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S1S9.TranslateText = false;
      this.chk_S1S9.Visible = false;
      // 
      // pnl_S2
      // 
      this.pnl_S2.Controls.Add(this.chk_S2S1);
      this.pnl_S2.Controls.Add(this.ttl_S2);
      this.pnl_S2.Controls.Add(this.chk_S2S2);
      this.pnl_S2.Controls.Add(this.chk_S2S3);
      this.pnl_S2.Controls.Add(this.chk_S2S4);
      this.pnl_S2.Controls.Add(this.chk_S2S5);
      this.pnl_S2.Controls.Add(this.chk_S2S6);
      this.pnl_S2.Controls.Add(this.chk_S2S7);
      this.pnl_S2.Controls.Add(this.chk_S2S8);
      this.pnl_S2.Controls.Add(this.chk_S2S9);
      this.pnl_S2.Location = new System.Drawing.Point(667, 590);
      this.pnl_S2.Name = "pnl_S2";
      this.pnl_S2.Size = new System.Drawing.Size(557, 125);
      this.pnl_S2.TabIndex = 294;
      this.pnl_S2.Visible = false;
      // 
      // chk_S2S1
      // 
      this.chk_S2S1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S1.Checked = true;
      this.chk_S2S1.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S2S1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S2S1.ImagePassword = null;
      this.chk_S2S1.Location = new System.Drawing.Point(13, 35);
      this.chk_S2S1.Name = "chk_S2S1";
      this.chk_S2S1.ReadOnly = true;
      this.chk_S2S1.Size = new System.Drawing.Size(60, 70);
      this.chk_S2S1.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S2S1.TabIndex = 275;
      this.chk_S2S1.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S2S1.TranslateText = false;
      this.chk_S2S1.Visible = false;
      // 
      // ttl_S2
      // 
      this.ttl_S2.BackColor = System.Drawing.Color.Black;
      this.ttl_S2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_S2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_S2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_S2.Location = new System.Drawing.Point(3, 3);
      this.ttl_S2.Name = "ttl_S2";
      this.ttl_S2.Size = new System.Drawing.Size(549, 110);
      this.ttl_S2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_S2.TabIndex = 227;
      this.ttl_S2.Text = "SHAFT 2 Slits Status";
      this.ttl_S2.TranslateText = false;
      // 
      // chk_S2S2
      // 
      this.chk_S2S2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S2.Checked = true;
      this.chk_S2S2.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S2S2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S2S2.ImagePassword = null;
      this.chk_S2S2.Location = new System.Drawing.Point(72, 35);
      this.chk_S2S2.Name = "chk_S2S2";
      this.chk_S2S2.ReadOnly = true;
      this.chk_S2S2.Size = new System.Drawing.Size(60, 70);
      this.chk_S2S2.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S2S2.TabIndex = 276;
      this.chk_S2S2.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S2S2.TranslateText = false;
      this.chk_S2S2.Visible = false;
      // 
      // chk_S2S3
      // 
      this.chk_S2S3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S3.Checked = true;
      this.chk_S2S3.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S2S3.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S2S3.ImagePassword = null;
      this.chk_S2S3.Location = new System.Drawing.Point(131, 35);
      this.chk_S2S3.Name = "chk_S2S3";
      this.chk_S2S3.ReadOnly = true;
      this.chk_S2S3.Size = new System.Drawing.Size(60, 70);
      this.chk_S2S3.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S2S3.TabIndex = 277;
      this.chk_S2S3.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S2S3.TranslateText = false;
      this.chk_S2S3.Visible = false;
      // 
      // chk_S2S4
      // 
      this.chk_S2S4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S4.Checked = true;
      this.chk_S2S4.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S2S4.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S2S4.ImagePassword = null;
      this.chk_S2S4.Location = new System.Drawing.Point(190, 35);
      this.chk_S2S4.Name = "chk_S2S4";
      this.chk_S2S4.ReadOnly = true;
      this.chk_S2S4.Size = new System.Drawing.Size(60, 70);
      this.chk_S2S4.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S2S4.TabIndex = 278;
      this.chk_S2S4.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S2S4.TranslateText = false;
      this.chk_S2S4.Visible = false;
      // 
      // chk_S2S5
      // 
      this.chk_S2S5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S5.Checked = true;
      this.chk_S2S5.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S2S5.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S2S5.ImagePassword = null;
      this.chk_S2S5.Location = new System.Drawing.Point(249, 35);
      this.chk_S2S5.Name = "chk_S2S5";
      this.chk_S2S5.ReadOnly = true;
      this.chk_S2S5.Size = new System.Drawing.Size(60, 70);
      this.chk_S2S5.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S2S5.TabIndex = 279;
      this.chk_S2S5.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S5.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S2S5.TranslateText = false;
      this.chk_S2S5.Visible = false;
      // 
      // chk_S2S6
      // 
      this.chk_S2S6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S6.Checked = true;
      this.chk_S2S6.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S2S6.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S2S6.ImagePassword = null;
      this.chk_S2S6.Location = new System.Drawing.Point(308, 35);
      this.chk_S2S6.Name = "chk_S2S6";
      this.chk_S2S6.ReadOnly = true;
      this.chk_S2S6.Size = new System.Drawing.Size(60, 70);
      this.chk_S2S6.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S2S6.TabIndex = 280;
      this.chk_S2S6.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S6.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S2S6.TranslateText = false;
      this.chk_S2S6.Visible = false;
      // 
      // chk_S2S7
      // 
      this.chk_S2S7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S7.Checked = true;
      this.chk_S2S7.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S2S7.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S2S7.ImagePassword = null;
      this.chk_S2S7.Location = new System.Drawing.Point(367, 35);
      this.chk_S2S7.Name = "chk_S2S7";
      this.chk_S2S7.ReadOnly = true;
      this.chk_S2S7.Size = new System.Drawing.Size(60, 70);
      this.chk_S2S7.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S2S7.TabIndex = 281;
      this.chk_S2S7.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S7.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S2S7.TranslateText = false;
      this.chk_S2S7.Visible = false;
      // 
      // chk_S2S8
      // 
      this.chk_S2S8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S8.Checked = true;
      this.chk_S2S8.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S2S8.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S2S8.ImagePassword = null;
      this.chk_S2S8.Location = new System.Drawing.Point(426, 35);
      this.chk_S2S8.Name = "chk_S2S8";
      this.chk_S2S8.ReadOnly = true;
      this.chk_S2S8.Size = new System.Drawing.Size(60, 70);
      this.chk_S2S8.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S2S8.TabIndex = 282;
      this.chk_S2S8.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S8.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S2S8.TranslateText = false;
      this.chk_S2S8.Visible = false;
      // 
      // chk_S2S9
      // 
      this.chk_S2S9.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S9.Checked = true;
      this.chk_S2S9.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_S2S9.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_S2S9.ImagePassword = null;
      this.chk_S2S9.Location = new System.Drawing.Point(485, 35);
      this.chk_S2S9.Name = "chk_S2S9";
      this.chk_S2S9.ReadOnly = true;
      this.chk_S2S9.Size = new System.Drawing.Size(60, 70);
      this.chk_S2S9.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_S2S9.TabIndex = 283;
      this.chk_S2S9.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_S2S9.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_S2S9.TranslateText = false;
      this.chk_S2S9.Visible = false;
      // 
      // ttb_CurrentSetupCode
      // 
      this.ttb_CurrentSetupCode.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentSetupCode.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentSetupCode.ForeColor = System.Drawing.Color.Black;
      this.ttb_CurrentSetupCode.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentSetupCode.ImagePassword = null;
      this.ttb_CurrentSetupCode.Location = new System.Drawing.Point(13, 101);
      this.ttb_CurrentSetupCode.MaxChar = 18;
      this.ttb_CurrentSetupCode.Multiline = false;
      this.ttb_CurrentSetupCode.Name = "ttb_CurrentSetupCode";
      this.ttb_CurrentSetupCode.PasswordChar = '\0';
      this.ttb_CurrentSetupCode.ReadOnly = true;
      this.ttb_CurrentSetupCode.Size = new System.Drawing.Size(225, 60);
      this.ttb_CurrentSetupCode.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentSetupCode.TabIndex = 295;
      this.ttb_CurrentSetupCode.Text = "Setup Code";
      this.ttb_CurrentSetupCode.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentSetupCode.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentSetupCode.TranslateText = true;
      this.ttb_CurrentSetupCode.Value = " ";
      this.ttb_CurrentSetupCode.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // chk_CurrentAux
      // 
      this.chk_CurrentAux.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_CurrentAux.Checked = true;
      this.chk_CurrentAux.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_CurrentAux.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_CurrentAux.ImagePassword = null;
      this.chk_CurrentAux.Location = new System.Drawing.Point(173, 167);
      this.chk_CurrentAux.Name = "chk_CurrentAux";
      this.chk_CurrentAux.ReadOnly = true;
      this.chk_CurrentAux.Size = new System.Drawing.Size(64, 66);
      this.chk_CurrentAux.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_CurrentAux.TabIndex = 296;
      this.chk_CurrentAux.Text = "Aux";
      this.chk_CurrentAux.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_CurrentAux.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_CurrentAux.TranslateText = false;
      this.chk_CurrentAux.Visible = false;
      // 
      // ttb_CurrentSlit
      // 
      this.ttb_CurrentSlit.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentSlit.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentSlit.ForeColor = System.Drawing.Color.Black;
      this.ttb_CurrentSlit.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentSlit.ImagePassword = null;
      this.ttb_CurrentSlit.Location = new System.Drawing.Point(13, 167);
      this.ttb_CurrentSlit.MaxChar = 18;
      this.ttb_CurrentSlit.Multiline = false;
      this.ttb_CurrentSlit.Name = "ttb_CurrentSlit";
      this.ttb_CurrentSlit.PasswordChar = '\0';
      this.ttb_CurrentSlit.ReadOnly = true;
      this.ttb_CurrentSlit.Size = new System.Drawing.Size(78, 66);
      this.ttb_CurrentSlit.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentSlit.TabIndex = 297;
      this.ttb_CurrentSlit.Text = "Slit";
      this.ttb_CurrentSlit.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentSlit.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentSlit.TranslateText = true;
      this.ttb_CurrentSlit.Value = "1";
      this.ttb_CurrentSlit.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_CurrentScore
      // 
      this.ttb_CurrentScore.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentScore.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentScore.ForeColor = System.Drawing.Color.Black;
      this.ttb_CurrentScore.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentScore.ImagePassword = null;
      this.ttb_CurrentScore.Location = new System.Drawing.Point(96, 167);
      this.ttb_CurrentScore.MaxChar = 18;
      this.ttb_CurrentScore.Multiline = false;
      this.ttb_CurrentScore.Name = "ttb_CurrentScore";
      this.ttb_CurrentScore.PasswordChar = '\0';
      this.ttb_CurrentScore.ReadOnly = true;
      this.ttb_CurrentScore.Size = new System.Drawing.Size(71, 66);
      this.ttb_CurrentScore.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentScore.TabIndex = 298;
      this.ttb_CurrentScore.Text = "Score";
      this.ttb_CurrentScore.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentScore.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentScore.TranslateText = true;
      this.ttb_CurrentScore.Value = "1";
      this.ttb_CurrentScore.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // chk_CurrentTandem
      // 
      this.chk_CurrentTandem.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_CurrentTandem.Checked = true;
      this.chk_CurrentTandem.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_CurrentTandem.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_CurrentTandem.ImagePassword = null;
      this.chk_CurrentTandem.Location = new System.Drawing.Point(13, 239);
      this.chk_CurrentTandem.Name = "chk_CurrentTandem";
      this.chk_CurrentTandem.ReadOnly = true;
      this.chk_CurrentTandem.Size = new System.Drawing.Size(78, 66);
      this.chk_CurrentTandem.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_CurrentTandem.TabIndex = 299;
      this.chk_CurrentTandem.Text = "Tandem";
      this.chk_CurrentTandem.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_CurrentTandem.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_CurrentTandem.TranslateText = false;
      this.chk_CurrentTandem.Visible = false;
      // 
      // pnl_CurrentSetup
      // 
      this.pnl_CurrentSetup.Controls.Add(this.SendCurrentOrder);
      this.pnl_CurrentSetup.Controls.Add(this.chk_CurrentNoGap);
      this.pnl_CurrentSetup.Controls.Add(this.ttl_CurrentSetup);
      this.pnl_CurrentSetup.Controls.Add(this.chk_CurrentTandem);
      this.pnl_CurrentSetup.Controls.Add(this.ttb_CurrentSetupCode);
      this.pnl_CurrentSetup.Controls.Add(this.ttb_CurrentScore);
      this.pnl_CurrentSetup.Controls.Add(this.chk_CurrentAux);
      this.pnl_CurrentSetup.Controls.Add(this.ttb_CurrentSlit);
      this.pnl_CurrentSetup.Location = new System.Drawing.Point(966, 194);
      this.pnl_CurrentSetup.Name = "pnl_CurrentSetup";
      this.pnl_CurrentSetup.Size = new System.Drawing.Size(240, 381);
      this.pnl_CurrentSetup.TabIndex = 300;
      this.pnl_CurrentSetup.Visible = false;
      // 
      // SendCurrentOrder
      // 
      this.SendCurrentOrder.Active = false;
      this.SendCurrentOrder.Alarmed = false;
      this.SendCurrentOrder.Auto = false;
      this.SendCurrentOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendCurrentOrder.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendCurrentOrder.FillBackColorWithImage = true;
      this.SendCurrentOrder.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendCurrentOrder.ForeColor = System.Drawing.Color.Black;
      this.SendCurrentOrder.FramePresent = true;
      this.SendCurrentOrder.HighLightEnable = false;
      this.SendCurrentOrder.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendCurrentOrder.ImagePassword = null;
      this.SendCurrentOrder.Inemergency = false;
      this.SendCurrentOrder.InLocal = false;
      this.SendCurrentOrder.IsButton = true;
      this.SendCurrentOrder.IsForceToInvisible = false;
      this.SendCurrentOrder.Location = new System.Drawing.Point(137, 3);
      this.SendCurrentOrder.Name = "SendCurrentOrder";
      this.SendCurrentOrder.OnLine = false;
      this.SendCurrentOrder.ShowImage = true;
      this.SendCurrentOrder.ShowText = false;
      this.SendCurrentOrder.Size = new System.Drawing.Size(100, 58);
      this.SendCurrentOrder.Switching = false;
      this.SendCurrentOrder.TabIndex = 444;
      this.SendCurrentOrder.Text = "1";
      this.SendCurrentOrder.TranslateText = false;
      this.SendCurrentOrder.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendCurrentOrder.UserDefinedActiveImage")));
      this.SendCurrentOrder.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendCurrentOrder.UserDefinedImage")));
      this.SendCurrentOrder.Warned = false;
      this.SendCurrentOrder.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // chk_CurrentNoGap
      // 
      this.chk_CurrentNoGap.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_CurrentNoGap.Checked = true;
      this.chk_CurrentNoGap.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_CurrentNoGap.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_CurrentNoGap.ImagePassword = null;
      this.chk_CurrentNoGap.Location = new System.Drawing.Point(96, 239);
      this.chk_CurrentNoGap.Name = "chk_CurrentNoGap";
      this.chk_CurrentNoGap.ReadOnly = true;
      this.chk_CurrentNoGap.Size = new System.Drawing.Size(64, 66);
      this.chk_CurrentNoGap.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_CurrentNoGap.TabIndex = 300;
      this.chk_CurrentNoGap.Text = "No Gap";
      this.chk_CurrentNoGap.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_CurrentNoGap.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_CurrentNoGap.TranslateText = false;
      this.chk_CurrentNoGap.Visible = false;
      // 
      // pnl_NextSetup
      // 
      this.pnl_NextSetup.Controls.Add(this.lbl_NoTrid);
      this.pnl_NextSetup.Controls.Add(this.SendNextOrder);
      this.pnl_NextSetup.Controls.Add(this.chk_NextNoGap);
      this.pnl_NextSetup.Controls.Add(this.ttl_NextSetup);
      this.pnl_NextSetup.Controls.Add(this.chk_NextTandem);
      this.pnl_NextSetup.Controls.Add(this.ttb_NextSetupCode);
      this.pnl_NextSetup.Controls.Add(this.ttb_NextScore);
      this.pnl_NextSetup.Controls.Add(this.chk_NextAux);
      this.pnl_NextSetup.Controls.Add(this.ttb_NextSlit);
      this.pnl_NextSetup.Location = new System.Drawing.Point(30, 194);
      this.pnl_NextSetup.Name = "pnl_NextSetup";
      this.pnl_NextSetup.Size = new System.Drawing.Size(240, 381);
      this.pnl_NextSetup.TabIndex = 301;
      this.pnl_NextSetup.Visible = false;
      this.pnl_NextSetup.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_NextSetup_Paint);
      // 
      // lbl_NoTrid
      // 
      this.lbl_NoTrid.AutoSize = true;
      this.lbl_NoTrid.BackColor = System.Drawing.Color.Red;
      this.lbl_NoTrid.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_NoTrid.Location = new System.Drawing.Point(9, 14);
      this.lbl_NoTrid.Name = "lbl_NoTrid";
      this.lbl_NoTrid.Size = new System.Drawing.Size(87, 20);
      this.lbl_NoTrid.TabIndex = 446;
      this.lbl_NoTrid.Text = "No Trident";
      this.lbl_NoTrid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_NoTrid.Visible = false;
      // 
      // SendNextOrder
      // 
      this.SendNextOrder.Active = false;
      this.SendNextOrder.Alarmed = false;
      this.SendNextOrder.Auto = false;
      this.SendNextOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendNextOrder.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendNextOrder.FillBackColorWithImage = true;
      this.SendNextOrder.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendNextOrder.ForeColor = System.Drawing.Color.Black;
      this.SendNextOrder.FramePresent = true;
      this.SendNextOrder.HighLightEnable = false;
      this.SendNextOrder.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendNextOrder.ImagePassword = null;
      this.SendNextOrder.Inemergency = false;
      this.SendNextOrder.InLocal = false;
      this.SendNextOrder.IsButton = true;
      this.SendNextOrder.IsForceToInvisible = false;
      this.SendNextOrder.Location = new System.Drawing.Point(137, 3);
      this.SendNextOrder.Name = "SendNextOrder";
      this.SendNextOrder.OnLine = false;
      this.SendNextOrder.ShowImage = true;
      this.SendNextOrder.ShowText = false;
      this.SendNextOrder.Size = new System.Drawing.Size(100, 58);
      this.SendNextOrder.Switching = false;
      this.SendNextOrder.TabIndex = 445;
      this.SendNextOrder.Text = "2";
      this.SendNextOrder.TranslateText = false;
      this.SendNextOrder.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendNextOrder.UserDefinedActiveImage")));
      this.SendNextOrder.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendNextOrder.UserDefinedImage")));
      this.SendNextOrder.Warned = false;
      this.SendNextOrder.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // chk_NextNoGap
      // 
      this.chk_NextNoGap.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_NextNoGap.Checked = true;
      this.chk_NextNoGap.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_NextNoGap.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_NextNoGap.ImagePassword = null;
      this.chk_NextNoGap.Location = new System.Drawing.Point(96, 239);
      this.chk_NextNoGap.Name = "chk_NextNoGap";
      this.chk_NextNoGap.ReadOnly = true;
      this.chk_NextNoGap.Size = new System.Drawing.Size(71, 66);
      this.chk_NextNoGap.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_NextNoGap.TabIndex = 300;
      this.chk_NextNoGap.Text = "No Gap";
      this.chk_NextNoGap.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_NextNoGap.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_NextNoGap.TranslateText = false;
      this.chk_NextNoGap.Visible = false;
      // 
      // ttl_NextSetup
      // 
      this.ttl_NextSetup.BackColor = System.Drawing.Color.Black;
      this.ttl_NextSetup.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextSetup.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextSetup.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextSetup.Location = new System.Drawing.Point(4, 61);
      this.ttl_NextSetup.Name = "ttl_NextSetup";
      this.ttl_NextSetup.Size = new System.Drawing.Size(233, 260);
      this.ttl_NextSetup.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_NextSetup.TabIndex = 226;
      this.ttl_NextSetup.Text = "NEXT SETUP";
      this.ttl_NextSetup.TranslateText = true;
      // 
      // chk_NextTandem
      // 
      this.chk_NextTandem.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_NextTandem.Checked = true;
      this.chk_NextTandem.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_NextTandem.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_NextTandem.ImagePassword = null;
      this.chk_NextTandem.Location = new System.Drawing.Point(13, 239);
      this.chk_NextTandem.Name = "chk_NextTandem";
      this.chk_NextTandem.ReadOnly = true;
      this.chk_NextTandem.Size = new System.Drawing.Size(77, 66);
      this.chk_NextTandem.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_NextTandem.TabIndex = 299;
      this.chk_NextTandem.Text = "Tandem";
      this.chk_NextTandem.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_NextTandem.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_NextTandem.TranslateText = false;
      this.chk_NextTandem.Visible = false;
      // 
      // ttb_NextSetupCode
      // 
      this.ttb_NextSetupCode.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_NextSetupCode.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_NextSetupCode.ForeColor = System.Drawing.Color.Black;
      this.ttb_NextSetupCode.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_NextSetupCode.ImagePassword = null;
      this.ttb_NextSetupCode.Location = new System.Drawing.Point(13, 101);
      this.ttb_NextSetupCode.MaxChar = 18;
      this.ttb_NextSetupCode.Multiline = false;
      this.ttb_NextSetupCode.Name = "ttb_NextSetupCode";
      this.ttb_NextSetupCode.PasswordChar = '\0';
      this.ttb_NextSetupCode.ReadOnly = true;
      this.ttb_NextSetupCode.Size = new System.Drawing.Size(224, 60);
      this.ttb_NextSetupCode.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_NextSetupCode.TabIndex = 295;
      this.ttb_NextSetupCode.Text = "Setup Code";
      this.ttb_NextSetupCode.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_NextSetupCode.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_NextSetupCode.TranslateText = true;
      this.ttb_NextSetupCode.Value = " ";
      this.ttb_NextSetupCode.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_NextScore
      // 
      this.ttb_NextScore.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_NextScore.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_NextScore.ForeColor = System.Drawing.Color.Black;
      this.ttb_NextScore.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_NextScore.ImagePassword = null;
      this.ttb_NextScore.Location = new System.Drawing.Point(96, 167);
      this.ttb_NextScore.MaxChar = 18;
      this.ttb_NextScore.Multiline = false;
      this.ttb_NextScore.Name = "ttb_NextScore";
      this.ttb_NextScore.PasswordChar = '\0';
      this.ttb_NextScore.ReadOnly = true;
      this.ttb_NextScore.Size = new System.Drawing.Size(71, 66);
      this.ttb_NextScore.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_NextScore.TabIndex = 298;
      this.ttb_NextScore.Text = "Score";
      this.ttb_NextScore.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_NextScore.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_NextScore.TranslateText = true;
      this.ttb_NextScore.Value = "1";
      this.ttb_NextScore.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // chk_NextAux
      // 
      this.chk_NextAux.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_NextAux.Checked = true;
      this.chk_NextAux.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_NextAux.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_NextAux.ImagePassword = null;
      this.chk_NextAux.Location = new System.Drawing.Point(173, 167);
      this.chk_NextAux.Name = "chk_NextAux";
      this.chk_NextAux.ReadOnly = true;
      this.chk_NextAux.Size = new System.Drawing.Size(64, 66);
      this.chk_NextAux.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_NextAux.TabIndex = 296;
      this.chk_NextAux.Text = "Aux";
      this.chk_NextAux.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_NextAux.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_NextAux.TranslateText = false;
      this.chk_NextAux.Visible = false;
      // 
      // ttb_NextSlit
      // 
      this.ttb_NextSlit.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_NextSlit.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_NextSlit.ForeColor = System.Drawing.Color.Black;
      this.ttb_NextSlit.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_NextSlit.ImagePassword = null;
      this.ttb_NextSlit.Location = new System.Drawing.Point(12, 167);
      this.ttb_NextSlit.MaxChar = 18;
      this.ttb_NextSlit.Multiline = false;
      this.ttb_NextSlit.Name = "ttb_NextSlit";
      this.ttb_NextSlit.PasswordChar = '\0';
      this.ttb_NextSlit.ReadOnly = true;
      this.ttb_NextSlit.Size = new System.Drawing.Size(78, 66);
      this.ttb_NextSlit.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_NextSlit.TabIndex = 297;
      this.ttb_NextSlit.Text = "Slit";
      this.ttb_NextSlit.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_NextSlit.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_NextSlit.TranslateText = true;
      this.ttb_NextSlit.Value = "1";
      this.ttb_NextSlit.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // pct_Slit1
      // 
      this.pct_Slit1.Image = global::SyncroC.Properties.Resources.Slower;
      this.pct_Slit1.Location = new System.Drawing.Point(651, 335);
      this.pct_Slit1.Name = "pct_Slit1";
      this.pct_Slit1.Size = new System.Drawing.Size(34, 41);
      this.pct_Slit1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Slit1.TabIndex = 439;
      this.pct_Slit1.TabStop = false;
      this.pct_Slit1.Visible = false;
      // 
      // imageList
      // 
      this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
      this.imageList.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList.Images.SetKeyName(0, "Slower.bmp");
      this.imageList.Images.SetKeyName(1, "Faster.bmp");
      // 
      // pct_Slit2
      // 
      this.pct_Slit2.Image = global::SyncroC.Properties.Resources.Slower;
      this.pct_Slit2.Location = new System.Drawing.Point(789, 335);
      this.pct_Slit2.Name = "pct_Slit2";
      this.pct_Slit2.Size = new System.Drawing.Size(34, 41);
      this.pct_Slit2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Slit2.TabIndex = 440;
      this.pct_Slit2.TabStop = false;
      this.pct_Slit2.Visible = false;
      // 
      // pct_Score2
      // 
      this.pct_Score2.Image = global::SyncroC.Properties.Resources.Slower;
      this.pct_Score2.Location = new System.Drawing.Point(614, 335);
      this.pct_Score2.Name = "pct_Score2";
      this.pct_Score2.Size = new System.Drawing.Size(34, 41);
      this.pct_Score2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Score2.TabIndex = 441;
      this.pct_Score2.TabStop = false;
      this.pct_Score2.Visible = false;
      // 
      // pct_Score1
      // 
      this.pct_Score1.Image = global::SyncroC.Properties.Resources.Slower;
      this.pct_Score1.Location = new System.Drawing.Point(446, 335);
      this.pct_Score1.Name = "pct_Score1";
      this.pct_Score1.Size = new System.Drawing.Size(34, 41);
      this.pct_Score1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Score1.TabIndex = 442;
      this.pct_Score1.TabStop = false;
      this.pct_Score1.Visible = false;
      // 
      // pct_ScoreAux
      // 
      this.pct_ScoreAux.Image = global::SyncroC.Properties.Resources.Slower;
      this.pct_ScoreAux.Location = new System.Drawing.Point(345, 335);
      this.pct_ScoreAux.Name = "pct_ScoreAux";
      this.pct_ScoreAux.Size = new System.Drawing.Size(34, 41);
      this.pct_ScoreAux.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_ScoreAux.TabIndex = 443;
      this.pct_ScoreAux.TabStop = false;
      this.pct_ScoreAux.Visible = false;
      // 
      // pct_SlitterScorer
      // 
      this.pct_SlitterScorer.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_SlitterScorer.Image = global::SyncroC.Properties.Resources.TwinStatus;
      this.pct_SlitterScorer.Location = new System.Drawing.Point(273, 197);
      this.pct_SlitterScorer.Name = "pct_SlitterScorer";
      this.pct_SlitterScorer.Size = new System.Drawing.Size(684, 390);
      this.pct_SlitterScorer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pct_SlitterScorer.TabIndex = 110;
      this.pct_SlitterScorer.TabStop = false;
      // 
      // pct_SlitterScorerCompact
      // 
      this.pct_SlitterScorerCompact.BackColor = System.Drawing.Color.WhiteSmoke;
      this.pct_SlitterScorerCompact.Image = global::SyncroC.Properties.Resources.Compact_2_Asse_NO_Aux;
      this.pct_SlitterScorerCompact.Location = new System.Drawing.Point(276, 194);
      this.pct_SlitterScorerCompact.Name = "pct_SlitterScorerCompact";
      this.pct_SlitterScorerCompact.Size = new System.Drawing.Size(684, 390);
      this.pct_SlitterScorerCompact.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pct_SlitterScorerCompact.TabIndex = 444;
      this.pct_SlitterScorerCompact.TabStop = false;
      // 
      // Twin2
      // 
      this.Twin2.BackColor = System.Drawing.Color.BurlyWood;
      this.Twin2.Controls.Add(this.lbl_AOC);
      this.Twin2.Controls.Add(this.ModifyScoreGap4);
      this.Twin2.Controls.Add(this.Shaft6);
      this.Twin2.Controls.Add(this.SlitterAx1);
      this.Twin2.Controls.Add(this.SlitterAx2);
      this.Twin2.Controls.Add(this.ModifyScoreGap1);
      this.Twin2.Controls.Add(this.ModifyScoreGap3);
      this.Twin2.Controls.Add(this.ModifyScoreGap2);
      this.Twin2.Controls.Add(this.Shaft3);
      this.Twin2.Controls.Add(this.Shaft5);
      this.Twin2.Controls.Add(this.Shaft2);
      this.Twin2.Controls.Add(this.Shaft1);
      this.Twin2.Controls.Add(this.Shaft4);
      this.Twin2.Location = new System.Drawing.Point(310, 194);
      this.Twin2.Name = "Twin2";
      this.Twin2.Size = new System.Drawing.Size(536, 383);
      this.Twin2.TabIndex = 445;
      // 
      // ModifyScoreGap4
      // 
      this.ModifyScoreGap4.Image = global::SyncroC.Properties.Resources.OptionGap;
      this.ModifyScoreGap4.Location = new System.Drawing.Point(56, 388);
      this.ModifyScoreGap4.Name = "ModifyScoreGap4";
      this.ModifyScoreGap4.Size = new System.Drawing.Size(42, 42);
      this.ModifyScoreGap4.TabIndex = 270;
      // 
      // Shaft6
      // 
      this.Shaft6.BackColor = System.Drawing.Color.Silver;
      this.Shaft6.Controls.Add(this.lbl_ScoreGap4);
      this.Shaft6.Location = new System.Drawing.Point(35, 393);
      this.Shaft6.Name = "Shaft6";
      this.Shaft6.Size = new System.Drawing.Size(471, 30);
      this.Shaft6.TabIndex = 271;
      // 
      // lbl_ScoreGap4
      // 
      this.lbl_ScoreGap4.Location = new System.Drawing.Point(414, 2);
      this.lbl_ScoreGap4.Name = "lbl_ScoreGap4";
      this.lbl_ScoreGap4.Size = new System.Drawing.Size(56, 27);
      this.lbl_ScoreGap4.TabIndex = 3;
      // 
      // SlitterAx1
      // 
      this.SlitterAx1.Image = global::SyncroC.Properties.Resources.Slitter;
      this.SlitterAx1.Location = new System.Drawing.Point(58, 104);
      this.SlitterAx1.Name = "SlitterAx1";
      this.SlitterAx1.Size = new System.Drawing.Size(42, 51);
      this.SlitterAx1.TabIndex = 269;
      // 
      // SlitterAx2
      // 
      this.SlitterAx2.Image = global::SyncroC.Properties.Resources.Slitter;
      this.SlitterAx2.Location = new System.Drawing.Point(58, 33);
      this.SlitterAx2.Name = "SlitterAx2";
      this.SlitterAx2.Size = new System.Drawing.Size(42, 51);
      this.SlitterAx2.TabIndex = 268;
      // 
      // ModifyScoreGap1
      // 
      this.ModifyScoreGap1.Image = global::SyncroC.Properties.Resources.OptionGap;
      this.ModifyScoreGap1.Location = new System.Drawing.Point(56, 249);
      this.ModifyScoreGap1.Name = "ModifyScoreGap1";
      this.ModifyScoreGap1.Size = new System.Drawing.Size(42, 42);
      this.ModifyScoreGap1.TabIndex = 262;
      // 
      // ModifyScoreGap3
      // 
      this.ModifyScoreGap3.Image = global::SyncroC.Properties.Resources.OptionGap;
      this.ModifyScoreGap3.Location = new System.Drawing.Point(56, 319);
      this.ModifyScoreGap3.Name = "ModifyScoreGap3";
      this.ModifyScoreGap3.Size = new System.Drawing.Size(42, 42);
      this.ModifyScoreGap3.TabIndex = 261;
      // 
      // ModifyScoreGap2
      // 
      this.ModifyScoreGap2.Image = global::SyncroC.Properties.Resources.OptionGap;
      this.ModifyScoreGap2.Location = new System.Drawing.Point(57, 173);
      this.ModifyScoreGap2.Name = "ModifyScoreGap2";
      this.ModifyScoreGap2.Size = new System.Drawing.Size(42, 51);
      this.ModifyScoreGap2.TabIndex = 260;
      // 
      // Shaft3
      // 
      this.Shaft3.BackColor = System.Drawing.Color.Silver;
      this.Shaft3.Controls.Add(this.lbl_Num3);
      this.Shaft3.Controls.Add(this.lbl_ord3);
      this.Shaft3.Controls.Add(this.lbl_ScoreGap1);
      this.Shaft3.Location = new System.Drawing.Point(34, 254);
      this.Shaft3.Name = "Shaft3";
      this.Shaft3.Size = new System.Drawing.Size(472, 30);
      this.Shaft3.TabIndex = 264;
      // 
      // lbl_Num3
      // 
      this.lbl_Num3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Num3.Location = new System.Drawing.Point(4, 2);
      this.lbl_Num3.Name = "lbl_Num3";
      this.lbl_Num3.Size = new System.Drawing.Size(16, 24);
      this.lbl_Num3.TabIndex = 5;
      this.lbl_Num3.Text = "1";
      this.lbl_Num3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_ord3
      // 
      this.lbl_ord3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ord3.Location = new System.Drawing.Point(125, 2);
      this.lbl_ord3.Name = "lbl_ord3";
      this.lbl_ord3.Size = new System.Drawing.Size(223, 27);
      this.lbl_ord3.TabIndex = 4;
      this.lbl_ord3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_ScoreGap1
      // 
      this.lbl_ScoreGap1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ScoreGap1.Location = new System.Drawing.Point(415, 2);
      this.lbl_ScoreGap1.Name = "lbl_ScoreGap1";
      this.lbl_ScoreGap1.Size = new System.Drawing.Size(56, 27);
      this.lbl_ScoreGap1.TabIndex = 1;
      this.lbl_ScoreGap1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // Shaft5
      // 
      this.Shaft5.BackColor = System.Drawing.Color.Silver;
      this.Shaft5.Controls.Add(this.lbl_Num5);
      this.Shaft5.Controls.Add(this.lbl_ord5);
      this.Shaft5.Controls.Add(this.lbl_ScoreGap3);
      this.Shaft5.Location = new System.Drawing.Point(35, 324);
      this.Shaft5.Name = "Shaft5";
      this.Shaft5.Size = new System.Drawing.Size(471, 30);
      this.Shaft5.TabIndex = 265;
      // 
      // lbl_Num5
      // 
      this.lbl_Num5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Num5.Location = new System.Drawing.Point(3, 4);
      this.lbl_Num5.Name = "lbl_Num5";
      this.lbl_Num5.Size = new System.Drawing.Size(16, 24);
      this.lbl_Num5.TabIndex = 5;
      this.lbl_Num5.Text = "A";
      this.lbl_Num5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_ord5
      // 
      this.lbl_ord5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ord5.Location = new System.Drawing.Point(124, 2);
      this.lbl_ord5.Name = "lbl_ord5";
      this.lbl_ord5.Size = new System.Drawing.Size(223, 27);
      this.lbl_ord5.TabIndex = 4;
      this.lbl_ord5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_ScoreGap3
      // 
      this.lbl_ScoreGap3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ScoreGap3.Location = new System.Drawing.Point(414, 2);
      this.lbl_ScoreGap3.Name = "lbl_ScoreGap3";
      this.lbl_ScoreGap3.Size = new System.Drawing.Size(56, 27);
      this.lbl_ScoreGap3.TabIndex = 2;
      this.lbl_ScoreGap3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // Shaft2
      // 
      this.Shaft2.BackColor = System.Drawing.Color.Silver;
      this.Shaft2.Controls.Add(this.lbl_Num2);
      this.Shaft2.Controls.Add(this.lbl_ord2);
      this.Shaft2.Location = new System.Drawing.Point(34, 43);
      this.Shaft2.Name = "Shaft2";
      this.Shaft2.Size = new System.Drawing.Size(472, 30);
      this.Shaft2.TabIndex = 267;
      // 
      // lbl_Num2
      // 
      this.lbl_Num2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Num2.Location = new System.Drawing.Point(4, 3);
      this.lbl_Num2.Name = "lbl_Num2";
      this.lbl_Num2.Size = new System.Drawing.Size(16, 24);
      this.lbl_Num2.TabIndex = 2;
      this.lbl_Num2.Text = "2";
      this.lbl_Num2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_ord2
      // 
      this.lbl_ord2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ord2.Location = new System.Drawing.Point(130, 1);
      this.lbl_ord2.Name = "lbl_ord2";
      this.lbl_ord2.Size = new System.Drawing.Size(223, 27);
      this.lbl_ord2.TabIndex = 1;
      this.lbl_ord2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // Shaft1
      // 
      this.Shaft1.BackColor = System.Drawing.Color.Silver;
      this.Shaft1.Controls.Add(this.lbl_Num1);
      this.Shaft1.Controls.Add(this.lbl_ord1);
      this.Shaft1.Location = new System.Drawing.Point(34, 115);
      this.Shaft1.Name = "Shaft1";
      this.Shaft1.Size = new System.Drawing.Size(472, 30);
      this.Shaft1.TabIndex = 266;
      // 
      // lbl_Num1
      // 
      this.lbl_Num1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Num1.Location = new System.Drawing.Point(4, 2);
      this.lbl_Num1.Name = "lbl_Num1";
      this.lbl_Num1.Size = new System.Drawing.Size(16, 24);
      this.lbl_Num1.TabIndex = 3;
      this.lbl_Num1.Text = "1";
      this.lbl_Num1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_ord1
      // 
      this.lbl_ord1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ord1.Location = new System.Drawing.Point(125, 2);
      this.lbl_ord1.Name = "lbl_ord1";
      this.lbl_ord1.Size = new System.Drawing.Size(223, 27);
      this.lbl_ord1.TabIndex = 2;
      this.lbl_ord1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // Shaft4
      // 
      this.Shaft4.BackColor = System.Drawing.Color.Silver;
      this.Shaft4.Controls.Add(this.lbl_Num4);
      this.Shaft4.Controls.Add(this.lbl_ord4);
      this.Shaft4.Controls.Add(this.lbl_ScoreGap2);
      this.Shaft4.Location = new System.Drawing.Point(34, 185);
      this.Shaft4.Name = "Shaft4";
      this.Shaft4.Size = new System.Drawing.Size(472, 30);
      this.Shaft4.TabIndex = 263;
      // 
      // lbl_Num4
      // 
      this.lbl_Num4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Num4.Location = new System.Drawing.Point(4, 2);
      this.lbl_Num4.Name = "lbl_Num4";
      this.lbl_Num4.Size = new System.Drawing.Size(16, 24);
      this.lbl_Num4.TabIndex = 4;
      this.lbl_Num4.Text = "2";
      this.lbl_Num4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_ord4
      // 
      this.lbl_ord4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ord4.Location = new System.Drawing.Point(125, 2);
      this.lbl_ord4.Name = "lbl_ord4";
      this.lbl_ord4.Size = new System.Drawing.Size(223, 27);
      this.lbl_ord4.TabIndex = 3;
      this.lbl_ord4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_ScoreGap2
      // 
      this.lbl_ScoreGap2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ScoreGap2.Location = new System.Drawing.Point(415, 2);
      this.lbl_ScoreGap2.Name = "lbl_ScoreGap2";
      this.lbl_ScoreGap2.Size = new System.Drawing.Size(56, 27);
      this.lbl_ScoreGap2.TabIndex = 0;
      this.lbl_ScoreGap2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pct_Direction
      // 
      this.pct_Direction.Image = ((System.Drawing.Image)(resources.GetObject("pct_Direction.Image")));
      this.pct_Direction.Location = new System.Drawing.Point(852, 267);
      this.pct_Direction.Name = "pct_Direction";
      this.pct_Direction.Size = new System.Drawing.Size(100, 198);
      this.pct_Direction.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Direction.TabIndex = 448;
      this.pct_Direction.TabStop = false;
      // 
      // lbl_AOC
      // 
      this.lbl_AOC.BackColor = System.Drawing.Color.Red;
      this.lbl_AOC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_AOC.Location = new System.Drawing.Point(497, 2);
      this.lbl_AOC.Name = "lbl_AOC";
      this.lbl_AOC.Size = new System.Drawing.Size(38, 25);
      this.lbl_AOC.TabIndex = 272;
      this.lbl_AOC.Text = "AOC";
      this.lbl_AOC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_AOC.Visible = false;
      // 
      // Frm_SlitterScorerStatus
      // 
      this.ClientSize = new System.Drawing.Size(1281, 738);
      this.Controls.Add(this.pct_Direction);
      this.Controls.Add(this.pct_ScoreAux);
      this.Controls.Add(this.pct_Score1);
      this.Controls.Add(this.pct_Score2);
      this.Controls.Add(this.pct_Slit2);
      this.Controls.Add(this.pct_Slit1);
      this.Controls.Add(this.pnl_NextSetup);
      this.Controls.Add(this.pnl_CurrentSetup);
      this.Controls.Add(this.pnl_S1);
      this.Controls.Add(this.pct_Grinding);
      this.Controls.Add(this.pct_Lubrification);
      this.Controls.Add(this.pnl_S2);
      this.Controls.Add(this.pct_maintenance);
      this.Controls.Add(this.stb_AutoMan);
      this.Controls.Add(this.Twin2);
      this.Controls.Add(this.pct_SlitterScorer);
      this.Controls.Add(this.pct_SlitterScorerCompact);
      this.Controls.Add(this.nud_ScoreGap2);
      this.Controls.Add(this.nud_ScoreGap1);
      this.Controls.Add(this.nud_ScoreGapAux);
      this.Name = "Frm_SlitterScorerStatus";
      this.Tag = "";
      this.Load += new System.EventHandler(this.Frm_SlitterScorerStatus_Load);
      this.Controls.SetChildIndex(this.nud_ScoreGapAux, 0);
      this.Controls.SetChildIndex(this.nud_ScoreGap1, 0);
      this.Controls.SetChildIndex(this.nud_ScoreGap2, 0);
      this.Controls.SetChildIndex(this.pct_SlitterScorerCompact, 0);
      this.Controls.SetChildIndex(this.pct_SlitterScorer, 0);
      this.Controls.SetChildIndex(this.Twin2, 0);
      this.Controls.SetChildIndex(this.stb_AutoMan, 0);
      this.Controls.SetChildIndex(this.pct_maintenance, 0);
      this.Controls.SetChildIndex(this.pnl_S2, 0);
      this.Controls.SetChildIndex(this.pct_Lubrification, 0);
      this.Controls.SetChildIndex(this.pct_Grinding, 0);
      this.Controls.SetChildIndex(this.pnl_S1, 0);
      this.Controls.SetChildIndex(this.pnl_CurrentSetup, 0);
      this.Controls.SetChildIndex(this.pnl_NextSetup, 0);
      this.Controls.SetChildIndex(this.pct_Slit1, 0);
      this.Controls.SetChildIndex(this.pct_Slit2, 0);
      this.Controls.SetChildIndex(this.pct_Score2, 0);
      this.Controls.SetChildIndex(this.pct_Score1, 0);
      this.Controls.SetChildIndex(this.pct_ScoreAux, 0);
      this.Controls.SetChildIndex(this.tab_Tab4, 0);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
      this.Controls.SetChildIndex(this.lbl_Time, 0);
      this.Controls.SetChildIndex(this.lbl_Status, 0);
      this.Controls.SetChildIndex(this.tab_Tab1, 0);
      this.Controls.SetChildIndex(this.tab_Tab2, 0);
      this.Controls.SetChildIndex(this.tab_Tab3, 0);
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
      this.Controls.SetChildIndex(this.pct_Direction, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Lubrification)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_maintenance)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Grinding)).EndInit();
      this.pnl_S1.ResumeLayout(false);
      this.pnl_S2.ResumeLayout(false);
      this.pnl_CurrentSetup.ResumeLayout(false);
      this.pnl_NextSetup.ResumeLayout(false);
      this.pnl_NextSetup.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Slit1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Slit2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Score2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Score1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_ScoreAux)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SlitterScorer)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_SlitterScorerCompact)).EndInit();
      this.Twin2.ResumeLayout(false);
      this.Shaft6.ResumeLayout(false);
      this.Shaft3.ResumeLayout(false);
      this.Shaft5.ResumeLayout(false);
      this.Shaft2.ResumeLayout(false);
      this.Shaft1.ResumeLayout(false);
      this.Shaft4.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Direction)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// </summary>
		protected override void F3()
		{
		}

		public override void CorrugatorDirectionLeft()
		{
			this.pct_SlitterScorer.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
			this.pct_SlitterScorerCompact.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

			int leftLimit = this.pct_SlitterScorer.Location.X;
			int rightLimit = this.pct_SlitterScorer.Location.X + this.pct_SlitterScorer.Width;

			HorizontalLocationMirror(this.stb_AutoMan, leftLimit, rightLimit);
			
			HorizontalLocationMirror(this.nud_ScoreGap2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.nud_ScoreGap1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.nud_ScoreGapAux, leftLimit, rightLimit);

			HorizontalLocationMirror(this.pct_Lubrification, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pct_maintenance, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pct_Grinding, leftLimit, rightLimit);

			HorizontalLocationMirror(this.pct_Score1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pct_Score2, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pct_ScoreAux, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pct_Slit1, leftLimit, rightLimit);
			HorizontalLocationMirror(this.pct_Slit2, leftLimit, rightLimit);

			leftLimit = ttl_CurrentSetup.Location.X;
			rightLimit = ttl_CurrentSetup.Location.X + ttl_CurrentSetup.Width;

			HorizontalLocationMirror(this.ttl_CurrentSetup, leftLimit, rightLimit);
			HorizontalLocationMirror(this.ttl_CurrentSetup, leftLimit, rightLimit);
		}

		public void _SetCurrentOrderStatus(int setupId, int modId, int slitShaft, int scoreShaft, bool auxPresent, bool tandem, bool noGapPossible, bool valid, bool positioning, bool positioned, bool working)
		{
			this.pnl_CurrentSetup.Visible = true;

			DataRow row = DBManager.getDBManager().order.getRow(setupId.ToString(), modId.ToString());
			if (row != null)
			{//valorizzazione con ordine
				this.ttb_CurrentSetupCode.Value = row[Order.DEL_SETUP_CODE].ToString();
			}
			else
			{//ordine non presente nella lista ordini controllo presenza in past order
				row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString(), modId.ToString());
				if (row != null)
				{//valorizzazione con ordine di past order
					this.ttb_CurrentSetupCode.Value = row[PastOrder.POL_SETUP_CODE].ToString();
				}
				else
				{
					//PMASSARO_FI04/11/08 - Disable the visualization in case of OldSS without control from Syncro
					/*
					 this._ClearCurrentOrderStatus();
					*/
					if (this.ss.SlitterScorerHeader.machineControlIntegratedInSyncro)
					{					
						this._ClearCurrentOrderStatus();
					}
				}
			}
			
			//PMASSARO_FI04/11/08 - Disable the visualization in case of OldSS without control from Syncro
			/*
				this.ttb_CurrentSlit.Value = slitShaft.ToString();
				this.ttb_CurrentScore.Value = scoreShaft.ToString();
				this.chk_CurrentAux.Checked = auxPresent;
				this.chk_CurrentTandem.Checked = tandem;
				this.chk_CurrentNoGap.Checked = noGapPossible;
			*/
			if (this.ss.SlitterScorerHeader.machineControlIntegratedInSyncro)
			{
				this.ttb_CurrentSlit.Value = slitShaft.ToString();
				this.ttb_CurrentScore.Value = scoreShaft.ToString();
				this.chk_CurrentAux.Checked = auxPresent;
				this.chk_CurrentTandem.Checked = tandem;
				this.chk_CurrentNoGap.Checked = noGapPossible;
			}
			else
			{
				this.ttb_CurrentSlit.Visible = false;
				this.ttb_CurrentScore.Visible = false;
				this.chk_CurrentAux.Visible = false;
				this.chk_CurrentTandem.Visible = false;
				this.chk_CurrentNoGap.Visible = false;			
			}
			
			if (working)
				this.ttl_CurrentSetup.color = SyncroC.Controls.Colors.SyncroColor.Black;
			else
			{
				if (!valid)
					this.ttl_CurrentSetup.color = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
				else if (positioning)
					this.ttl_CurrentSetup.color = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
				else if (positioned)
					this.ttl_CurrentSetup.color = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
			}
		}
		public void _ClearCurrentOrderStatus()
		{
			this.ttb_CurrentSetupCode.Value = "";
			this.ttb_CurrentSlit.Value = "";
			this.ttb_CurrentScore.Value = "";
			this.chk_CurrentAux.Checked = true;
			this.chk_CurrentTandem.Checked = true;
			this.chk_CurrentNoGap.Checked = true;

			this.ttl_CurrentSetup.color = SyncroC.Controls.Colors.SyncroColor.Black;

		}

		public void _SetNextOrderStatus(int setupId, int modId, int slitShaft, int scoreShaft, bool auxPresent, bool tandem, bool noGapPossible, bool valid, bool positioning, bool positioned, bool working)
		{
			this.pnl_NextSetup.Visible = true;

			DataRow row = DBManager.getDBManager().order.getRow(setupId.ToString(), modId.ToString());
			if (row != null)
			{//valorizzazione con ordine
				this.ttb_NextSetupCode.Value = row[Order.DEL_SETUP_CODE].ToString();
			}
			else
			{//ordine non presente nella lista ordini controllo presenza in past order
				row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString(), modId.ToString());
				if (row != null)
				{//valorizzazione con ordine di past order
					this.ttb_NextSetupCode.Value = row[PastOrder.POL_SETUP_CODE].ToString();
				}
				else
				{
					//PMASSARO_FI04/11/08 - Disable the visualization in case of OldSS without control from Syncro
					/*				
					this._ClearNextOrderStatus();
					*/
					 if (this.ss.SlitterScorerHeader.machineControlIntegratedInSyncro)
					 {
						 this._ClearNextOrderStatus();
					 }
				}
			}
			//PMASSARO_FI04/11/08 - Disable the visualization in case of OldSS without control from Syncro
			/*			
				this.ttb_NextSlit.Value = slitShaft.ToString();
				this.ttb_NextScore.Value = scoreShaft.ToString();
				this.chk_NextAux.Checked = auxPresent;
				this.chk_NextTandem.Checked = tandem;
				this.chk_NextNoGap.Checked = noGapPossible;
			*/
			 if (this.ss.SlitterScorerHeader.machineControlIntegratedInSyncro)
			 {
				this.ttb_NextSlit.Value = slitShaft.ToString();
				this.ttb_NextScore.Value = scoreShaft.ToString();
				this.chk_NextAux.Checked = auxPresent;
				this.chk_NextTandem.Checked = tandem;
				this.chk_NextNoGap.Checked = noGapPossible;
			 }
			 else
			 {
				this.ttb_NextSlit.Visible = false;
				this.ttb_NextScore.Visible = false;
				this.chk_NextAux.Visible = false;
				this.chk_NextTandem.Visible = false;
				this.chk_NextNoGap.Visible = false;
			}
			
			if (working)
				this.ttl_NextSetup.color = SyncroC.Controls.Colors.SyncroColor.Black;
			else
			{
				if (!valid)
					this.ttl_NextSetup.color = SyncroC.Controls.Colors.SyncroColor.AlarmRed;
				else if (positioning)
					this.ttl_NextSetup.color = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
				else if (positioned)
					this.ttl_NextSetup.color = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
			}
		}

		public void _ClearNextOrderStatus()
		{
			this.ttb_NextSetupCode.Value = "";
			this.ttb_NextSlit.Value = "";
			this.ttb_NextScore.Value = "";
			this.chk_NextAux.Checked = true;
			this.chk_NextTandem.Checked = true;
			this.chk_NextNoGap.Checked = true;

			this.ttl_CurrentSetup.color = SyncroC.Controls.Colors.SyncroColor.Black;

		}

		public void _SetSlitShaft1Status(string slitShaftStatus1)
		{
			//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
			if (ss.IsTwinE())
			{

				if ((!slitShaftStatus1.Contains("1")) && (!slitShaftStatus1.Contains("0")))
				{
					this.pnl_S1.Visible = false;
					return;
				}

				this.pnl_S1.Visible = true;

				char[] appo = slitShaftStatus1.ToCharArray();

				this.chk_S1S1.Visible = (appo.Length > 0) && (appo[0] != 'N');
				this.chk_S1S1.Checked = (appo.Length > 0) && (appo[0] == '1');

				this.chk_S1S2.Visible = (appo.Length > 1) && (appo[1] != 'N');
				this.chk_S1S2.Checked = (appo.Length > 1) && (appo[1] == '1');

				this.chk_S1S3.Visible = (appo.Length > 2) && (appo[2] != 'N');
				this.chk_S1S3.Checked = (appo.Length > 2) && (appo[2] == '1');

				this.chk_S1S4.Visible = (appo.Length > 3) && (appo[3] != 'N');
				this.chk_S1S4.Checked = (appo.Length > 3) && (appo[3] == '1');

				this.chk_S1S5.Visible = (appo.Length > 4) && (appo[4] != 'N');
				this.chk_S1S5.Checked = (appo.Length > 4) && (appo[4] == '1');

				this.chk_S1S6.Visible = (appo.Length > 5) && (appo[5] != 'N');
				this.chk_S1S6.Checked = (appo.Length > 5) && (appo[5] == '1');

				this.chk_S1S7.Visible = (appo.Length > 6) && (appo[6] != 'N');
				this.chk_S1S7.Checked = (appo.Length > 6) && (appo[6] == '1');

				this.chk_S1S8.Visible = (appo.Length > 7) && (appo[7] != 'N');
				this.chk_S1S8.Checked = (appo.Length > 7) && (appo[7] == '1');

				this.chk_S1S9.Visible = (appo.Length > 8) && (appo[8] != 'N');
				this.chk_S1S9.Checked = (appo.Length > 8) && (appo[8] == '1');
			}
 
		}

		public void _SetSlitShaft2Status(string slitShaftStatus2)
		{
			//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
			if (ss.IsTwinE())
			{
				if ((!slitShaftStatus2.Contains("1")) && (!slitShaftStatus2.Contains("0")))
				{
					this.pnl_S2.Visible = false;
					return;
				}

				this.pnl_S2.Visible = true;

				char[] appo = slitShaftStatus2.ToCharArray();

				this.chk_S2S1.Visible = (appo.Length > 0) && (appo[0] != 'N');
				this.chk_S2S1.Checked = (appo.Length > 0) && (appo[0] == '1');

				this.chk_S2S2.Visible = (appo.Length > 1) && (appo[1] != 'N');
				this.chk_S2S2.Checked = (appo.Length > 1) && (appo[1] == '1');

				this.chk_S2S3.Visible = (appo.Length > 2) && (appo[2] != 'N');
				this.chk_S2S3.Checked = (appo.Length > 2) && (appo[2] == '1');

				this.chk_S2S4.Visible = (appo.Length > 3) && (appo[3] != 'N');
				this.chk_S2S4.Checked = (appo.Length > 3) && (appo[3] == '1');

				this.chk_S2S5.Visible = (appo.Length > 4) && (appo[4] != 'N');
				this.chk_S2S5.Checked = (appo.Length > 4) && (appo[4] == '1');

				this.chk_S2S6.Visible = (appo.Length > 5) && (appo[5] != 'N');
				this.chk_S2S6.Checked = (appo.Length > 5) && (appo[5] == '1');

				this.chk_S2S7.Visible = (appo.Length > 6) && (appo[6] != 'N');
				this.chk_S2S7.Checked = (appo.Length > 6) && (appo[6] == '1');

				this.chk_S2S8.Visible = (appo.Length > 7) && (appo[7] != 'N');
				this.chk_S2S8.Checked = (appo.Length > 7) && (appo[7] == '1');

				this.chk_S2S9.Visible = (appo.Length > 8) && (appo[8] != 'N');
				this.chk_S2S9.Checked = (appo.Length > 8) && (appo[8] == '1');
			}
		}

		public void _SetShaftStatus(bool slitShaft1Plunged, bool slitShaft2Plunged, bool scoreShaft1Plunged, bool scoreShaft2Plunged, bool scoreShaftAuxPlunged)
		{
			//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
      if (ss.IsTwinE())
      {
				this.pct_Score1.Visible = true;
				this.pct_Score2.Visible = true;
				this.pct_ScoreAux.Visible = true;
				this.pct_Slit1.Visible = true;
				this.pct_Slit2.Visible = true;

				this.pct_Score1.Image = (scoreShaft1Plunged) ? this.imageList.Images[0] : this.imageList.Images[1];
				this.pct_Score2.Image = (scoreShaft2Plunged) ? this.imageList.Images[0] : this.imageList.Images[1];
				this.pct_ScoreAux.Image = (scoreShaftAuxPlunged) ? this.imageList.Images[0] : this.imageList.Images[1];
				this.pct_Slit1.Image = (slitShaft1Plunged) ? this.imageList.Images[0] : this.imageList.Images[1];
				this.pct_Slit2.Image = (slitShaft2Plunged) ? this.imageList.Images[0] : this.imageList.Images[1];
			}
			else if (ss.IsCompact2())
			{

			}
    }

		private void Frm_SlitterScorerStatus_Load(object sender, EventArgs e)
		{
			this._ClearCurrentOrderStatus();
			this._ClearNextOrderStatus();
		}

		private void OnButtonPush(object sender, System.EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        //MSTAGETTI_FI-11-05-2010
        //Command.SLITTER_SCORER + "." + ((Control)sender).Name + "()");
    		Command.SLITTER_SCORER + "." + ((Control)sender).Name + "(" + this.noTrid.ToString() + ")");
}

		private void Frm_SlitterScorerStatuse_Load(object sender, EventArgs e)
		{
			this._CommunicationError(false);
		}

		private void _CommunicationError(bool isError)
		{
			this.SendCurrentOrder.Alarmed = isError;
			this.SendNextOrder.Alarmed = isError;
		}

		private void pnl_NextSetup_Paint(object sender, PaintEventArgs e)
		{

		}
    //MSTAGETTI_FI-11-05-2010
    protected override void ControlShiftAltT()
    {
      this.noTrid = !this.noTrid;
      this.lbl_NoTrid.Visible = this.noTrid;
    }
	}
}

