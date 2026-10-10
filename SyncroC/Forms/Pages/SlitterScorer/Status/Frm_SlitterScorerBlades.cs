using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using	System.Reflection;
using System.Data;
using System.Threading;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Controls.Colors;
using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Controls.Tools;
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.SlitterScorer.Status
{
	public class Frm_SlitterScorerBlades : SyncroC.Forms.Pages.SlitterScorer.Status.Frm_SlitterScorerStatusSubgroup 
  {
    private SyncroC.Controls.Title title5;
		private int slitIndex, shaftIndex;
		private SyncroC.Controls.TitledLabel ttl_GrindInterval;
		private SyncroC.Controls.TitledLabel ttl_LubeInterval;
    private SyncroC.Controls.TitledLabel ttl_TrimGrindMultiplier;
		private SyncroC.Controls.TitledTextBox ttb_GrindDuration;
		private SyncroC.Controls.TitledTextBox ttb_LubeDuration;
		private int[] brokenSlits;
		private int[] newBrokenSlits;
    private int[][] slitPositions;
    private int[] plungedSlitMask;
    private bool[] goodSlitPositions;
    private bool mod=false;
		private string newLubeDuration, newGrindDuration;
    private SyncroC.Controls.Tools.MachineTools toolDetail;
		private CleanCutData cleanCutData;
    private MachineTools.ChangeShaftFunction changeFunction;
		private MachineTools.UpdateValuesFunction updateFunction;
		private Label ActiveCtrl;
    private System.Windows.Forms.Timer timGrinding;
    private Label lab_Grinding;
    private Label lab_Lubricating;
    private SyncroC.Controls.StateButton GrindSlit;
		private SyncroC.Controls.StateButton LubeSlit;
    private System.ComponentModel.IContainer components = null;
    private bool grindingRunning = false;
    private int[] selectedMaskForGrinding;

		private DBManager dbm;
		private SyncroC.DataBase.LineConfig.SlitterScorer ss;
    //agrassi_FI22-08-2011_1
    bool extToolDetail = false;

		public Frm_SlitterScorerBlades()
		{
			InitializeComponent();
			dbm = DBManager.getDBManager(); 
			this.ss = dbm.lineConfig.SlitterScorerConfig;

      this.timGrinding.Interval = 400;
      this.changeFunction = new MachineTools.ChangeShaftFunction(this.ChangeShaft);
			this.updateFunction = new MachineTools.UpdateValuesFunction(this.UpdateTempBrokenSlits);
			this.toolDetail.Config(DBManager.getDBManager().lineConfig, SyncroC.Controls.Tools.MachineToolsType.OnlySlits,
																true, false, this.changeFunction, this.updateFunction, "", ""); 
			this.toolDetail.PositionOnlySlits();
      this.selectedMaskForGrinding = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
      this.cleanCutData = new CleanCutData();
			this.brokenSlits = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
			this.plungedSlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
			this.goodSlitPositions = new bool[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
			slitPositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS][];
			for (int i = 0; i < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; i++)
				slitPositions[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
      this.slitIndex = 0;
      this.shaftIndex=(int)Shaft.Slit1;
			this.UpdateValues();
      //agrassi_FI09-06-2010_2
      //if (!DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication)
      if ((!DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication) ||
          (DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsCompact2() && 
           DBManager.getDBManager().lineConfig.SlitterScorerConfig.Compact2.PLCInterfaceVersion < Compact2.MachineVersion.Ver1))
          this.LubeSlit.Visible = false;

      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
      {
        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
        this.lbl_CtrlF5_YY.Text = "";
        this.LubeSlit.IsButton = false;
        this.GrindSlit.IsButton = false;
      }
    }
    //agrassi_FI22-08-2011_1
    public Frm_SlitterScorerBlades(MachineTools toolDetail, SyncroC.Controls.StateButton GrindSlit, Label lab_Grinding)
    {
      this.extToolDetail = true;
      dbm = DBManager.getDBManager();
      this.ss = dbm.lineConfig.SlitterScorerConfig;
      this.components = new System.ComponentModel.Container();
      this.timGrinding = new System.Windows.Forms.Timer(this.components);
      this.timGrinding.Interval = 400;
      this.timGrinding.Tick += new System.EventHandler(this.timGrinding_Tick);
      this.selectedMaskForGrinding = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
      this.brokenSlits = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
      this.plungedSlitMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
      this.goodSlitPositions = new bool[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
      slitPositions = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS][];
      for (int i = 0; i < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; i++)
        slitPositions[i] = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
      this.slitIndex = 0;
      this.shaftIndex = (int)Shaft.Slit1;
      this.toolDetail = toolDetail;
      this.GrindSlit = GrindSlit;
      this.lab_Grinding = lab_Grinding;
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

		protected override void F12()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SlitterScorerBlades));
      this.title5 = new SyncroC.Controls.Title();
      this.ttl_GrindInterval = new SyncroC.Controls.TitledLabel();
      this.ttl_LubeInterval = new SyncroC.Controls.TitledLabel();
      this.ttl_TrimGrindMultiplier = new SyncroC.Controls.TitledLabel();
      this.ttb_GrindDuration = new SyncroC.Controls.TitledTextBox();
      this.ttb_LubeDuration = new SyncroC.Controls.TitledTextBox();
      this.toolDetail = new SyncroC.Controls.Tools.MachineTools();
      this.ActiveCtrl = new System.Windows.Forms.Label();
      this.timGrinding = new System.Windows.Forms.Timer(this.components);
      this.lab_Grinding = new System.Windows.Forms.Label();
      this.lab_Lubricating = new System.Windows.Forms.Label();
      this.GrindSlit = new SyncroC.Controls.StateButton();
      this.LubeSlit = new SyncroC.Controls.StateButton();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
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
      this.tab_TabA.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      // 
      // tab_TabC
      // 
      this.tab_TabC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF5_YY.Text = "Modify KK";
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
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
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // title5
      // 
      this.title5.BackColor = System.Drawing.Color.Black;
      this.title5.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title5.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title5.ForeColor = System.Drawing.Color.White;
      this.title5.Location = new System.Drawing.Point(30, 192);
      this.title5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.title5.Name = "title5";
      this.title5.Size = new System.Drawing.Size(442, 269);
      this.title5.Style = SyncroC.Controls.TitleStyles.Big;
      this.title5.TabIndex = 116;
      this.title5.TabStop = false;
      this.title5.Text = "Clean Cut data (KK)";
      this.title5.TranslateText = true;
      // 
      // ttl_GrindInterval
      // 
      this.ttl_GrindInterval.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_GrindInterval.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_GrindInterval.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_GrindInterval.ImagePassword = null;
      this.ttl_GrindInterval.Location = new System.Drawing.Point(42, 308);
      this.ttl_GrindInterval.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.ttl_GrindInterval.Name = "ttl_GrindInterval";
      this.ttl_GrindInterval.Size = new System.Drawing.Size(192, 40);
      this.ttl_GrindInterval.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_GrindInterval.TabIndex = 130;
      this.ttl_GrindInterval.TabStop = false;
      this.ttl_GrindInterval.Text = "Grind interval (m)";
      this.ttl_GrindInterval.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_GrindInterval.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_GrindInterval.TranslateText = true;
      this.ttl_GrindInterval.Value = "5";
      this.ttl_GrindInterval.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_GrindInterval.ValueMultiline = true;
      // 
      // ttl_LubeInterval
      // 
      this.ttl_LubeInterval.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_LubeInterval.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_LubeInterval.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_LubeInterval.ImagePassword = null;
      this.ttl_LubeInterval.Location = new System.Drawing.Point(280, 218);
      this.ttl_LubeInterval.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.ttl_LubeInterval.Name = "ttl_LubeInterval";
      this.ttl_LubeInterval.Size = new System.Drawing.Size(192, 40);
      this.ttl_LubeInterval.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_LubeInterval.TabIndex = 132;
      this.ttl_LubeInterval.TabStop = false;
      this.ttl_LubeInterval.Text = "Lubrication interval (m)";
      this.ttl_LubeInterval.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_LubeInterval.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_LubeInterval.TranslateText = true;
      this.ttl_LubeInterval.Value = "5";
      this.ttl_LubeInterval.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_LubeInterval.ValueMultiline = true;
      // 
      // ttl_TrimGrindMultiplier
      // 
      this.ttl_TrimGrindMultiplier.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_TrimGrindMultiplier.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_TrimGrindMultiplier.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_TrimGrindMultiplier.ImagePassword = null;
      this.ttl_TrimGrindMultiplier.Location = new System.Drawing.Point(42, 367);
      this.ttl_TrimGrindMultiplier.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
      this.ttl_TrimGrindMultiplier.Name = "ttl_TrimGrindMultiplier";
      this.ttl_TrimGrindMultiplier.Size = new System.Drawing.Size(192, 40);
      this.ttl_TrimGrindMultiplier.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_TrimGrindMultiplier.TabIndex = 133;
      this.ttl_TrimGrindMultiplier.TabStop = false;
      this.ttl_TrimGrindMultiplier.Text = "Trim grinding multiplier";
      this.ttl_TrimGrindMultiplier.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_TrimGrindMultiplier.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_TrimGrindMultiplier.TranslateText = true;
      this.ttl_TrimGrindMultiplier.Value = "5";
      this.ttl_TrimGrindMultiplier.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_TrimGrindMultiplier.ValueMultiline = true;
      // 
      // ttb_GrindDuration
      // 
      this.ttb_GrindDuration.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_GrindDuration.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_GrindDuration.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_GrindDuration.ImagePassword = null;
      this.ttb_GrindDuration.Location = new System.Drawing.Point(42, 218);
      this.ttb_GrindDuration.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
      this.ttb_GrindDuration.MaxChar = 32767;
      this.ttb_GrindDuration.Multiline = false;
      this.ttb_GrindDuration.Name = "ttb_GrindDuration";
      this.ttb_GrindDuration.PasswordChar = '\0';
      this.ttb_GrindDuration.ReadOnly = true;
      this.ttb_GrindDuration.Size = new System.Drawing.Size(192, 40);
      this.ttb_GrindDuration.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_GrindDuration.TabIndex = 0;
      this.ttb_GrindDuration.TabStop = false;
      this.ttb_GrindDuration.Text = "Grinding duration(s/10)";
      this.ttb_GrindDuration.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_GrindDuration.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_GrindDuration.TranslateText = true;
      this.ttb_GrindDuration.Value = "5";
      this.ttb_GrindDuration.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_LubeDuration
      // 
      this.ttb_LubeDuration.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_LubeDuration.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_LubeDuration.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_LubeDuration.ImagePassword = null;
      this.ttb_LubeDuration.Location = new System.Drawing.Point(42, 263);
      this.ttb_LubeDuration.MaxChar = 32767;
      this.ttb_LubeDuration.Multiline = false;
      this.ttb_LubeDuration.Name = "ttb_LubeDuration";
      this.ttb_LubeDuration.PasswordChar = '\0';
      this.ttb_LubeDuration.ReadOnly = true;
      this.ttb_LubeDuration.Size = new System.Drawing.Size(192, 40);
      this.ttb_LubeDuration.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_LubeDuration.TabIndex = 1;
      this.ttb_LubeDuration.TabStop = false;
      this.ttb_LubeDuration.Text = "Lube duration (s/10)";
      this.ttb_LubeDuration.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_LubeDuration.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_LubeDuration.TranslateText = true;
      this.ttb_LubeDuration.Value = "5";
      this.ttb_LubeDuration.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // toolDetail
      // 
      this.toolDetail.BackColor = System.Drawing.Color.WhiteSmoke;
      this.toolDetail.Debug = false;
      this.toolDetail.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolDetail.Location = new System.Drawing.Point(131, 537);
      this.toolDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.toolDetail.Name = "toolDetail";
      this.toolDetail.Size = new System.Drawing.Size(1071, 197);
      this.toolDetail.TabIndex = 204;
      this.toolDetail.TabStop = false;
      this.toolDetail.TranslateText = false;
      this.toolDetail.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // ActiveCtrl
      // 
      this.ActiveCtrl.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ActiveCtrl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ActiveCtrl.Location = new System.Drawing.Point(309, 57);
      this.ActiveCtrl.Name = "ActiveCtrl";
      this.ActiveCtrl.Size = new System.Drawing.Size(344, 26);
      this.ActiveCtrl.TabIndex = 556;
      this.ActiveCtrl.Text = "Active control: ";
      this.ActiveCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ActiveCtrl.Visible = false;
      // 
      // timGrinding
      // 
      this.timGrinding.Tick += new System.EventHandler(this.timGrinding_Tick);
      // 
      // lab_Grinding
      // 
      this.lab_Grinding.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lab_Grinding.ForeColor = System.Drawing.Color.LimeGreen;
      this.lab_Grinding.Location = new System.Drawing.Point(904, 498);
      this.lab_Grinding.Name = "lab_Grinding";
      this.lab_Grinding.Size = new System.Drawing.Size(300, 37);
      this.lab_Grinding.TabIndex = 559;
      this.lab_Grinding.Text = "Grinding slit 5, shaft 2";
      this.lab_Grinding.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lab_Grinding.Visible = false;
      // 
      // lab_Lubricating
      // 
      this.lab_Lubricating.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lab_Lubricating.ForeColor = System.Drawing.Color.LimeGreen;
      this.lab_Lubricating.Location = new System.Drawing.Point(906, 454);
      this.lab_Lubricating.Name = "lab_Lubricating";
      this.lab_Lubricating.Size = new System.Drawing.Size(300, 37);
      this.lab_Lubricating.TabIndex = 560;
      this.lab_Lubricating.Text = "Lubricating slit 2, shaft 1";
      this.lab_Lubricating.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lab_Lubricating.Visible = false;
      // 
      // GrindSlit
      // 
      this.GrindSlit.Active = false;
      this.GrindSlit.Alarmed = false;
      this.GrindSlit.Auto = false;
      this.GrindSlit.BackColor = System.Drawing.Color.WhiteSmoke;
      this.GrindSlit.Cursor = System.Windows.Forms.Cursors.Hand;
      this.GrindSlit.FillBackColorWithImage = false;
      this.GrindSlit.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.GrindSlit.ForeColor = System.Drawing.SystemColors.ControlText;
      this.GrindSlit.FramePresent = true;
      this.GrindSlit.HighLightEnable = false;
      this.GrindSlit.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.GrindSlit.ImagePassword = null;
      this.GrindSlit.Inemergency = false;
      this.GrindSlit.InLocal = false;
      this.GrindSlit.IsButton = true;
      this.GrindSlit.IsForceToInvisible = false;
      this.GrindSlit.Location = new System.Drawing.Point(570, 465);
      this.GrindSlit.Name = "GrindSlit";
      this.GrindSlit.OnLine = false;
      this.GrindSlit.ShowImage = false;
      this.GrindSlit.ShowText = true;
      this.GrindSlit.Size = new System.Drawing.Size(132, 70);
      this.GrindSlit.Switching = false;
      this.GrindSlit.TabIndex = 561;
      this.GrindSlit.Text = "Grind";
      this.GrindSlit.TranslateText = true;
      this.GrindSlit.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("GrindSlit.UserDefinedActiveImage")));
      this.GrindSlit.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("GrindSlit.UserDefinedImage")));
      this.GrindSlit.Warned = false;
      this.GrindSlit.Click += new System.EventHandler(this.GrindSlit_Click);
      // 
      // LubeSlit
      // 
      this.LubeSlit.Active = false;
      this.LubeSlit.Alarmed = false;
      this.LubeSlit.Auto = false;
      this.LubeSlit.BackColor = System.Drawing.Color.WhiteSmoke;
      this.LubeSlit.Cursor = System.Windows.Forms.Cursors.Hand;
      this.LubeSlit.FillBackColorWithImage = false;
      this.LubeSlit.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.LubeSlit.FramePresent = true;
      this.LubeSlit.HighLightEnable = false;
      this.LubeSlit.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.LubeSlit.ImagePassword = null;
      this.LubeSlit.Inemergency = false;
      this.LubeSlit.InLocal = false;
      this.LubeSlit.IsButton = true;
      this.LubeSlit.IsForceToInvisible = false;
      this.LubeSlit.Location = new System.Drawing.Point(747, 465);
      this.LubeSlit.Name = "LubeSlit";
      this.LubeSlit.OnLine = false;
      this.LubeSlit.ShowImage = false;
      this.LubeSlit.ShowText = true;
      this.LubeSlit.Size = new System.Drawing.Size(132, 70);
      this.LubeSlit.Switching = false;
      this.LubeSlit.TabIndex = 562;
      this.LubeSlit.Text = "Lube";
      this.LubeSlit.TranslateText = true;
      this.LubeSlit.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("LubeSlit.UserDefinedActiveImage")));
      this.LubeSlit.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("LubeSlit.UserDefinedImage")));
      this.LubeSlit.Warned = false;
      this.LubeSlit.Click += new System.EventHandler(this.LubeSlit_Click);
      // 
      // Frm_SlitterScorerBlades
      // 
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.GrindSlit);
      this.Controls.Add(this.LubeSlit);
      this.Controls.Add(this.lab_Lubricating);
      this.Controls.Add(this.lab_Grinding);
      this.Controls.Add(this.ActiveCtrl);
      this.Controls.Add(this.ttb_LubeDuration);
      this.Controls.Add(this.ttb_GrindDuration);
      this.Controls.Add(this.ttl_TrimGrindMultiplier);
      this.Controls.Add(this.ttl_LubeInterval);
      this.Controls.Add(this.ttl_GrindInterval);
      this.Controls.Add(this.title5);
      this.Controls.Add(this.toolDetail);
      this.Location = new System.Drawing.Point(231, 13);
      this.Name = "Frm_SlitterScorerBlades";
      this.Tag = "";
      this.Text = "Frm_SlitterScorerBlades";
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Frm_SlitterScorerBlades_KeyUp);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.toolDetail, 0);
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
      this.Controls.SetChildIndex(this.title5, 0);
      this.Controls.SetChildIndex(this.ttl_GrindInterval, 0);
      this.Controls.SetChildIndex(this.ttl_LubeInterval, 0);
      this.Controls.SetChildIndex(this.ttl_TrimGrindMultiplier, 0);
      this.Controls.SetChildIndex(this.ttb_GrindDuration, 0);
      this.Controls.SetChildIndex(this.ttb_LubeDuration, 0);
      this.Controls.SetChildIndex(this.ActiveCtrl, 0);
      this.Controls.SetChildIndex(this.lab_Grinding, 0);
      this.Controls.SetChildIndex(this.lab_Lubricating, 0);
      this.Controls.SetChildIndex(this.LubeSlit, 0);
      this.Controls.SetChildIndex(this.GrindSlit, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

		public override void refreshData()
		{
			base.refreshData();
			this.UpdateValues();
      this.toolDetail.Invalidate();
    }

		private void UpdateValues()
		{
      //agrassi_FI22-08-2011_1
      if (this.extToolDetail)
        return;
      if (this.mod)
        return;
			SyncroC.DataBase.LineConfig.SlitterScorer ss = DBManager.getDBManager().lineConfig.SlitterScorerConfig;
      SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
      this.ttl_GrindInterval.Value = this.cleanCutData.grindInterval.ToString();
      this.ttl_LubeInterval.Value = this.cleanCutData.lubeInterval.ToString();
      this.ttl_TrimGrindMultiplier.Value = this.cleanCutData.trimGrindingMultiplier.ToString();
      this.ttb_GrindDuration.Value = this.cleanCutData.grindDuration.ToString();
      this.ttb_LubeDuration.Value = this.cleanCutData.lubeDuration.ToString();
      int typ;
      char slitType=ss.GetSlitType(this.shaftIndex, this.slitIndex);
			for (typ = 0; typ < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLIT_TYPES_MACHINE; typ++)
				if (ss.SlitterScorerHeader.SlitTypes[typ].Letter == slitType)
          break;
      this.toolDetail.Focus();
    }

    private void _UpdateCleanCutCounters(string slitsData)
    {
			string[] sData=slitsData.Split(Command.PARAMETER_SEPARATOR);

			if (sData.Length != 5) return;
			this.cleanCutData.grindDuration = intParse(sData[0]);
			this.cleanCutData.lubeDuration = intParse(sData[1]);
			this.cleanCutData.grindInterval = intParse(sData[2]);
			this.cleanCutData.lubeInterval = intParse(sData[3]);
			this.cleanCutData.trimGrindingMultiplier = intParse(sData[4]);

      this.UpdateValues();
    }

    private int RetriveTheoreticalPosition(int sh, int slt, int actualPos)
    {
      DataRowCollection setupRows = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows;
      if (setupRows.Count>0)
      {
        int shaftMask=this.intParse(setupRows[0][Order.DEL_SHAFT_MASK].ToString());
        if ((shaftMask & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(sh)) != 0)
          //agrassi_FI04-02-09
          //return this.intParse(setupRows[0][Order.DEL_SLIT_POSITION + sh.ToString() + ":" + slt.ToString()].ToString());
          return this.intParse(DBManager.getDBManager().order.GetDryOrderField(0, Order.DEL_SLIT_POSITION, sh, slt));
      }
      if (setupRows.Count > 1)
      {
        int shaftMask = this.intParse(setupRows[1][Order.DEL_SHAFT_MASK].ToString());
        if ((shaftMask & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(sh)) != 0)
          //agrassi_FI04-02-09
          //return this.intParse(setupRows[1][Order.DEL_SLIT_POSITION + sh.ToString() + ":" + slt.ToString()].ToString());
          return this.intParse(DBManager.getDBManager().order.GetDryOrderField(1, Order.DEL_SLIT_POSITION, sh, slt));
      }
      return actualPos;
    }

    //agrassi_FI22-08-2011_1
    //private void _UpdateSlitPositions(string slitPositions)
    public void _UpdateSlitPositions(string slitPositions)
    {
      bool update = false;
      int valInt = 0;
      bool valBool = false;
      string[] sPositions = slitPositions.Split(Command.PARAMETER_SEPARATOR);
			if (sPositions.Length != SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS * (SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT + 2)) 
        return;
			for (int sh = 0; sh < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
      {
        valBool = bool.Parse(sPositions[(sh + 1) * SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT + 2 * sh + 1]);
        if (this.goodSlitPositions[sh] != valBool)
        {
          this.goodSlitPositions[sh] = valBool;
          update = true;
        }
        for (int slt = 0; slt < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
        {
          valInt = intParse(sPositions[slt + sh * (SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT + 2)]);
          if (!this.goodSlitPositions[sh])
            valInt = this.RetriveTheoreticalPosition(sh, slt, valInt);
          if (this.slitPositions[sh][slt] != valInt)
          {
            this.slitPositions[sh][slt] = valInt;
            update = true;
          }
        }
				valInt = intParse(sPositions[(sh + 1) * SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT + 2 * sh]);
        if (this.plungedSlitMask[sh] != valInt)
        {
          this.plungedSlitMask[sh] = valInt;
          update = true;
        }
      }
      if (update)
        this.toolDetail.PositionOnlySlits(this.slitPositions, this.plungedSlitMask, this.goodSlitPositions);
    }

		protected override void CtrlF4()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      if (!this.mod ) return;
      this.ChangeTextBoxesEnableMod(false);
      CommunicationManager cm = null;
      if (this.mod)
      {
        if ((!this.ttb_GrindDuration.Value.Equals(this.newGrindDuration)) ||
          (!this.ttb_LubeDuration.Value.Equals(this.newLubeDuration)))
        {
          if (cm == null) cm = CommunicationManager.GetCommunicationManager();
          cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
            PageManager.getRequestCommand(typeof(Frm_SlitterScorerBlades)) + "." + Command.CLEANCUT_DATA +
            "(" + this.ttb_GrindDuration.Value + "," + this.ttb_LubeDuration.Value + ")");
        }
      }
			this.mod=false;
      this.UpdateValues();
    }

    protected override void CtrlF5()
		{
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      this.ChangeTextBoxesEnableMod(true);
			this.newBrokenSlits=(int[])this.brokenSlits.Clone();
			this.newGrindDuration=this.ttb_GrindDuration.Value;
			this.newLubeDuration=this.ttb_LubeDuration.Value;
			this.mod=true;
    }

		private void ChangeTextBoxesEnableMod(bool en)
		{
			this.ttb_GrindDuration.ReadOnly=!en;
      this.ttb_GrindDuration.TabStop = en;
      this.ttb_LubeDuration.ReadOnly = !en;
      this.ttb_LubeDuration.TabStop = en;
      if (en)
        this.ttb_GrindDuration.Focus();
      else
        this.tab_Tab1.Focus();
    }

    protected override void Escape()
    {
      this.toolDetail.ResetMultiSelection();
      if (!this.mod ) return;
      this.ChangeTextBoxesEnableMod(false);
      this.mod = false;
    }

    protected override void LeftArrow()
    {
      this.slitIndex = this.toolDetail.ScrollLeft(ref this.shaftIndex);
      this.UpdateValues();
    }

    protected override void RightArrow()
    {
      this.slitIndex = this.toolDetail.ScrollRight(ref this.shaftIndex);
      this.UpdateValues();
    }

    protected override void UpArrow()
    {
      this.shaftIndex = this.toolDetail.ChangeShaft(true);
      this.UpdateValues();
    }

    protected override void DownArrow()
    {
      this.shaftIndex = this.toolDetail.ChangeShaft(true);
      this.UpdateValues();
    }

    //agrassi_FI22-08-2011_1
    //private void ChangeShaft(int slitIndex, int shaftIndex)
    public void ChangeShaft(int slitIndex, int shaftIndex)
    {
      if (this.shaftIndex != shaftIndex)
        this.shaftIndex = this.toolDetail.ChangeShaft(false);
      this.slitIndex = slitIndex;
      this.UpdateValues();
    }

		private void UpdateTempBrokenSlits()
		{
			this.UpdateValues();
		}

		private void Frm_SlitterScorerBlades_KeyUp(object sender, KeyEventArgs e)
		{
		//  this.ActiveCtrl.Text = "Active control: " + this.ActiveControl.Name + " (" + this.ActiveControl.TabIndex.ToString() + ")";
		//  if (e.KeyCode == Keys.Enter)
		//    this.toolDetail.ToggleSelection(this.shaftIndex, this.slitIndex);
		}

    protected override void ControlD()
    {
      base.ControlD();
      ActiveCtrl.Visible = !ActiveCtrl.Visible;
      this.toolDetail.Debug = !this.toolDetail.Debug;
    }

    private void OnButtonPush(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_SlitterScorerBlades)) + "." + ((Control)sender).Name + "()");
    }

    //agrassi_FI22-08-2011_1
    //private void _SetGrinding(bool val, int shaft, int slit, bool multiple, string grindCounters, bool maintenance)
    public void _SetGrinding(bool val, int shaft, int slit, bool multiple, string grindCounters, bool maintenance)
    {
      this.grindingRunning = val;
      this.lab_Grinding.Visible = val;
      if (val)
      {
        SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
        if ((slit >= 0) && (slit < DBManager.getDBManager().lineConfig.SlitterScorerConfig.GetSlitCount()) &&
            (shaft >= 0) && (shaft < DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS) && (!maintenance))
          this.lab_Grinding.Text = msg.getTranslatedMessage("§SYCC_66# §SYCC_60# " + (slit + 1).ToString() + ", §SYCC_61# " + (shaft + 1).ToString());
        else
          this.lab_Grinding.Text = msg.getTranslatedMessage("§SYCC_66");
      }
      this.timGrinding.Enabled = val;
    }

    private void _SetLubricating(bool val, int shaft, int slit)
    {
      this.lab_Lubricating.Visible = val;
      if (val)
      {
        SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
        if ((slit >= 0) && (slit < DBManager.getDBManager().lineConfig.SlitterScorerConfig.GetSlitCount()) &&
            (shaft >= 0) && (shaft < DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS))
          this.lab_Lubricating.Text = msg.getTranslatedMessage("§SYCC_67# §SYCC_60# " + (slit + 1).ToString() + ", §SYCC_61# " + (shaft + 1).ToString());
        else
          this.lab_Lubricating.Text = msg.getTranslatedMessage("§SYCC_67");
      }
    }

    private void timGrinding_Tick(object sender, EventArgs e)
    {
      this.lab_Grinding.Visible = !this.lab_Grinding.Visible;
    }

    //agrassi_FI22-08-2011_1
    //private void GrindSlit_Click(object sender, EventArgs e)
    public void GrindSlit_Click(object sender, EventArgs e)
    {
			if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
				return;
			bool multiple = false;
			string[] selEnum = new string[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
			int[] slitCount = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
			for (int sh = 0; sh < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
			{
				this.selectedMaskForGrinding[sh] = this.toolDetail.GetSelectedSlitMask(sh);
				multiple = multiple || (this.selectedMaskForGrinding[sh] > 0);
				selEnum[sh] = "";
				for (int slt = 0; slt < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
					if (((this.selectedMaskForGrinding[sh] & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(slt)) != 0) &&
							((this.plungedSlitMask[sh] & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(slt)) != 0))
					{
						selEnum[sh] += ((selEnum[sh] == "") ? "" : ", ") + (slt + 1).ToString();
						slitCount[sh]++;
					}
			}
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_SlitterScorerBlades)) + "." + Command.GRIND_SLIT +
				"(" + this.slitIndex.ToString() + "," + this.shaftIndex.ToString() + ")");
		}

    private void LubeSlit_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

			if (!DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication)
      {
        SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
        string text = msg.getTranslatedMessage("§SYCC_171");
        Frm_Alert confirm = new Frm_Alert();
        confirm.ShowDialog(text);
        return;
      }
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_SlitterScorerBlades)) + "." + Command.LUBE_SLIT +
				"("+this.slitIndex.ToString()+","+this.shaftIndex.ToString()+")");
    }

  }
}

