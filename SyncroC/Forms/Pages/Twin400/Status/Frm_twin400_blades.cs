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

//agrassi_FI07-04-08_10
//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
//namespace SyncroC.Forms.Pages.Twin400.Production
namespace SyncroC.Forms.Pages.Twin400.Status
{
	//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
	//public class Frm_twin400_blades : SyncroC.Forms.Pages.Twin400.Production.Frm_twin400_productionSubgroup
	public class Frm_twin400_blades : SyncroC.Forms.Pages.Twin400.Status.Frm_twin400_statusSubgroup 
  {
    private SyncroC.Controls.Title title5;
    private int slitIndex, shaftIndex;
    private SyncroC.Controls.TitledLabel ttl_RunMeters;
		private SyncroC.Controls.TitledLabel ttl_Grindings;
		private SyncroC.Controls.TitledLabel ttl_MetersSinceGrind;
		private SyncroC.Controls.TitledLabel ttl_MetersSinceLube;
		private SyncroC.Controls.TitledLabel ttl_GrindToolRunMeters;
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
    private bool mod=false, modBroken=false;
    private bool maintenance = false;
		private string newLubeDuration, newGrindDuration;
    private SyncroC.Controls.CheckBox chk_SlitState;
    private SyncroC.Controls.Tools.MachineTools toolDetail;
    private CleanCutData cleanCutData;
    private SyncroC.Controls.TitledLabel ttl_SlitType;
    private SyncroC.Controls.TitledLabel ttl_BladeFromLeft;
    private SyncroC.Controls.TitledLabel ttl_ChamferFromLeft;
    private SyncroC.Controls.TitledLabel ttl_MinDiameter;
    private SyncroC.Controls.TitledLabel ttl_MaxDiameter;
    private MachineTools.ChangeShaftFunction changeFunction;
    private MachineTools.UpdateValuesFunction updateFunction;
    private Label ActiveCtrl;
    private SyncroC.Controls.Title tit_Slit;
    private SyncroC.Controls.StateButton ResetGrinder;
    private System.Windows.Forms.Timer timGrinding;
    private Label lab_Grinding;
    private Label lab_Lubricating;
    private SyncroC.Controls.StateButton GrindSlit;
    private SyncroC.Controls.StateButton LubeSlit;
    private SyncroC.Controls.StateButton EnableGrinding;
    private SyncroC.Controls.StateButton ResetSlitCounters;
    private System.ComponentModel.IContainer components = null;
    private bool grindingRunning = false;
    private int[] selectedMaskForGrinding;
    private bool multipleGrindingRunning = false;
    private int multipleGrindingShaft;
		private SyncroC.Controls.TitledLabel ttl_GrindMetersSinceRegulation;
    private string[] numberOfGrindings;
    //Thread multipleGrindingThread=null;

		private DBManager dbm;
		private SyncroC.Controls.TitledLabel ttl_TotalGrindMeters;
		private SyncroC.DataBase.LineConfig.SlitterScorer ss;
    private SyncroC.Controls.StateButton StopMultiple;
    //agrassi_FI08-09-2010_1
    bool extToolDetail = false;

		public Frm_twin400_blades()
		{
			InitializeComponent();
			dbm = DBManager.getDBManager(); 
			this.ss = dbm.lineConfig.SlitterScorerConfig;

      this.timGrinding.Interval = 400;
      this.changeFunction = new MachineTools.ChangeShaftFunction(this.ChangeShaft);
      this.updateFunction = new MachineTools.UpdateValuesFunction(this.UpdateTempBrokenSlits);
      this.toolDetail.Config(DBManager.getDBManager().lineConfig, SyncroC.Controls.Tools.MachineToolsType.OnlySlits,
                                true, true, this.changeFunction, this.updateFunction, "", "");
      //this.toolDetail.EnableMultiSelect(true);
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
			this.UpdateBrokenSlits();

			//RFAZZI PROVA
			//this.LubeSlit.Visible = DBManager.getDBManager().lineConfig.SlitterScorerConfig.PositioningOptions.Slits.EnableLubrication;
			//this.GrindSlit.Visible = 
			//  this.EnableGrinding.Visible=DBManager.getDBManager().lineConfig.SlitterScorerConfig.PositioningOptions.Slits.EnableGrinding;
			this.LubeSlit.Visible = DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication;
			this.GrindSlit.Visible =
				this.EnableGrinding.Visible = DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.PositioningOptions.Slits.EnableGrinding;

      this.numberOfGrindings = new string[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
      {
        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
        this.lbl_CtrlF5_YY.Text = "";
        this.LubeSlit.IsButton = false;
        this.GrindSlit.IsButton = false;
        this.ResetGrinder.IsButton = false;
        this.ResetSlitCounters.IsButton = false;
      }

      //PMASSARO_FI08/08/11 - Riabilitazione Lubrificazione automatica su PMAC400 per tutte le versioni di PLC
      /*
			//RFAZZI_FI27/08/08_1. Added value MetersSinceLastSlitRegolation on graphics
			//RFAZZI PROVA
			//if (ss.PLCInterfaceVersion > SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver5)
			if (ss.Twin400.PLCInterfaceVersion > SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver5)
			{
				this.ttl_MetersSinceLube.Visible = false;
				this.ttl_Grindings.Visible = false;
			}
			else
			{
				this.ttl_GrindMetersSinceRegulation.Visible = false;
				this.ttl_TotalGrindMeters.Visible = false;
			}
      */
      if (ss.Twin400.PLCInterfaceVersion > SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver5)
      {
        if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication)
        {
          this.ttl_GrindMetersSinceRegulation.Visible = false;
          this.ttl_MetersSinceLube.Visible = true;
        }
        else
        {
          this.ttl_GrindMetersSinceRegulation.Visible = true;
          this.ttl_MetersSinceLube.Visible = false;
        }
        this.ttl_Grindings.Visible = false;
      }
      else
      {
        this.ttl_GrindMetersSinceRegulation.Visible = false;
        this.ttl_TotalGrindMeters.Visible = false;
      }

      //RFAZZI_FI07/06/2010_1. Aggiunta gestione per togliere a run time i parametri sulla lubrificazione in caso di Twin400V1. 
      this.ttb_LubeDuration.Visible = ss.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication;
      this.ttl_LubeInterval.Visible = ss.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication;
    }

    //agrassi_FI08-09-2010_1
    public Frm_twin400_blades(MachineTools toolDetail, SyncroC.Controls.StateButton GrindSlit, Label lab_Grinding, SyncroC.Controls.StateButton EnableGrinding)
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
      this.UpdateBrokenSlits();
      this.numberOfGrindings = new string[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT];

      this.toolDetail = toolDetail;
      this.GrindSlit = GrindSlit;
      this.lab_Grinding = lab_Grinding;
      this.EnableGrinding = EnableGrinding;
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
      //if (this.multipleGrindingThread!=null)
      //  if (this.multipleGrindingThread.IsAlive)
      //    this.multipleGrindingThread.Abort();
		}

		//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
		//protected override void F11()
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_blades));
      this.title5 = new SyncroC.Controls.Title();
      this.ttl_RunMeters = new SyncroC.Controls.TitledLabel();
      this.ttl_Grindings = new SyncroC.Controls.TitledLabel();
      this.ttl_MetersSinceGrind = new SyncroC.Controls.TitledLabel();
      this.ttl_MetersSinceLube = new SyncroC.Controls.TitledLabel();
      this.ttl_GrindInterval = new SyncroC.Controls.TitledLabel();
      this.ttl_LubeInterval = new SyncroC.Controls.TitledLabel();
      this.ttl_TrimGrindMultiplier = new SyncroC.Controls.TitledLabel();
      this.ttl_GrindToolRunMeters = new SyncroC.Controls.TitledLabel();
      this.chk_SlitState = new SyncroC.Controls.CheckBox();
      this.ttb_GrindDuration = new SyncroC.Controls.TitledTextBox();
      this.ttb_LubeDuration = new SyncroC.Controls.TitledTextBox();
      this.toolDetail = new SyncroC.Controls.Tools.MachineTools();
      this.ttl_SlitType = new SyncroC.Controls.TitledLabel();
      this.ttl_BladeFromLeft = new SyncroC.Controls.TitledLabel();
      this.ttl_ChamferFromLeft = new SyncroC.Controls.TitledLabel();
      this.ttl_MinDiameter = new SyncroC.Controls.TitledLabel();
      this.ttl_MaxDiameter = new SyncroC.Controls.TitledLabel();
      this.ActiveCtrl = new System.Windows.Forms.Label();
      this.tit_Slit = new SyncroC.Controls.Title();
      this.ResetGrinder = new SyncroC.Controls.StateButton();
      this.timGrinding = new System.Windows.Forms.Timer(this.components);
      this.lab_Grinding = new System.Windows.Forms.Label();
      this.lab_Lubricating = new System.Windows.Forms.Label();
      this.GrindSlit = new SyncroC.Controls.StateButton();
      this.LubeSlit = new SyncroC.Controls.StateButton();
      this.EnableGrinding = new SyncroC.Controls.StateButton();
      this.ResetSlitCounters = new SyncroC.Controls.StateButton();
      this.ttl_GrindMetersSinceRegulation = new SyncroC.Controls.TitledLabel();
      this.ttl_TotalGrindMeters = new SyncroC.Controls.TitledLabel();
      this.StopMultiple = new SyncroC.Controls.StateButton();
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
      this.lbl_CtrlF3_YY.Text = "Broken slits";
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
      this.title5.Location = new System.Drawing.Point(759, 184);
      this.title5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.title5.Name = "title5";
      this.title5.Size = new System.Drawing.Size(442, 269);
      this.title5.Style = SyncroC.Controls.TitleStyles.Big;
      this.title5.TabIndex = 116;
      this.title5.TabStop = false;
      this.title5.Text = "Clean Cut data (KK)";
      this.title5.TranslateText = true;
      // 
      // ttl_RunMeters
      // 
      this.ttl_RunMeters.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_RunMeters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_RunMeters.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_RunMeters.ImagePassword = null;
      this.ttl_RunMeters.Location = new System.Drawing.Point(305, 210);
      this.ttl_RunMeters.Name = "ttl_RunMeters";
      this.ttl_RunMeters.Size = new System.Drawing.Size(229, 40);
      this.ttl_RunMeters.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_RunMeters.TabIndex = 121;
      this.ttl_RunMeters.TabStop = false;
      this.ttl_RunMeters.Text = "Total run metres";
      this.ttl_RunMeters.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_RunMeters.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_RunMeters.TranslateText = true;
      this.ttl_RunMeters.Value = "5";
      this.ttl_RunMeters.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_RunMeters.ValueMultiline = true;
      // 
      // ttl_Grindings
      // 
      this.ttl_Grindings.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Grindings.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Grindings.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Grindings.ImagePassword = null;
      this.ttl_Grindings.Location = new System.Drawing.Point(303, 300);
      this.ttl_Grindings.Name = "ttl_Grindings";
      this.ttl_Grindings.Size = new System.Drawing.Size(229, 40);
      this.ttl_Grindings.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Grindings.TabIndex = 122;
      this.ttl_Grindings.TabStop = false;
      this.ttl_Grindings.Text = "Total grindings";
      this.ttl_Grindings.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Grindings.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Grindings.TranslateText = true;
      this.ttl_Grindings.Value = "5";
      this.ttl_Grindings.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Grindings.ValueMultiline = true;
      // 
      // ttl_MetersSinceGrind
      // 
      this.ttl_MetersSinceGrind.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_MetersSinceGrind.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_MetersSinceGrind.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_MetersSinceGrind.ImagePassword = null;
      this.ttl_MetersSinceGrind.Location = new System.Drawing.Point(303, 256);
      this.ttl_MetersSinceGrind.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.ttl_MetersSinceGrind.Name = "ttl_MetersSinceGrind";
      this.ttl_MetersSinceGrind.Size = new System.Drawing.Size(278, 39);
      this.ttl_MetersSinceGrind.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_MetersSinceGrind.TabIndex = 123;
      this.ttl_MetersSinceGrind.TabStop = false;
      this.ttl_MetersSinceGrind.Text = "Run metres since last grind";
      this.ttl_MetersSinceGrind.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_MetersSinceGrind.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_MetersSinceGrind.TranslateText = true;
      this.ttl_MetersSinceGrind.Value = "5";
      this.ttl_MetersSinceGrind.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_MetersSinceGrind.ValueMultiline = true;
      // 
      // ttl_MetersSinceLube
      // 
      this.ttl_MetersSinceLube.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_MetersSinceLube.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_MetersSinceLube.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_MetersSinceLube.ImagePassword = null;
      this.ttl_MetersSinceLube.Location = new System.Drawing.Point(303, 343);
      this.ttl_MetersSinceLube.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
      this.ttl_MetersSinceLube.Name = "ttl_MetersSinceLube";
      this.ttl_MetersSinceLube.Size = new System.Drawing.Size(278, 40);
      this.ttl_MetersSinceLube.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_MetersSinceLube.TabIndex = 124;
      this.ttl_MetersSinceLube.TabStop = false;
      this.ttl_MetersSinceLube.Text = "Run metres since last lube";
      this.ttl_MetersSinceLube.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_MetersSinceLube.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_MetersSinceLube.TranslateText = true;
      this.ttl_MetersSinceLube.Value = "5";
      this.ttl_MetersSinceLube.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_MetersSinceLube.ValueMultiline = true;
      // 
      // ttl_GrindInterval
      // 
      this.ttl_GrindInterval.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_GrindInterval.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_GrindInterval.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_GrindInterval.ImagePassword = null;
      this.ttl_GrindInterval.Location = new System.Drawing.Point(771, 210);
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
      this.ttl_LubeInterval.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_LubeInterval.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_LubeInterval.ImagePassword = null;
      this.ttl_LubeInterval.Location = new System.Drawing.Point(771, 300);
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
      this.ttl_TrimGrindMultiplier.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_TrimGrindMultiplier.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_TrimGrindMultiplier.ImagePassword = null;
      this.ttl_TrimGrindMultiplier.Location = new System.Drawing.Point(771, 256);
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
      // ttl_GrindToolRunMeters
      // 
      this.ttl_GrindToolRunMeters.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_GrindToolRunMeters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_GrindToolRunMeters.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_GrindToolRunMeters.ImagePassword = null;
      this.ttl_GrindToolRunMeters.Location = new System.Drawing.Point(999, 256);
      this.ttl_GrindToolRunMeters.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
      this.ttl_GrindToolRunMeters.Name = "ttl_GrindToolRunMeters";
      this.ttl_GrindToolRunMeters.Size = new System.Drawing.Size(192, 40);
      this.ttl_GrindToolRunMeters.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_GrindToolRunMeters.TabIndex = 134;
      this.ttl_GrindToolRunMeters.TabStop = false;
      this.ttl_GrindToolRunMeters.Text = "Grinding tool run metres";
      this.ttl_GrindToolRunMeters.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_GrindToolRunMeters.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_GrindToolRunMeters.TranslateText = true;
      this.ttl_GrindToolRunMeters.Value = "5";
      this.ttl_GrindToolRunMeters.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_GrindToolRunMeters.ValueMultiline = true;
      // 
      // chk_SlitState
      // 
      this.chk_SlitState.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SlitState.Checked = false;
      this.chk_SlitState.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_SlitState.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_SlitState.ImagePassword = null;
      this.chk_SlitState.Location = new System.Drawing.Point(164, 391);
      this.chk_SlitState.Name = "chk_SlitState";
      this.chk_SlitState.ReadOnly = true;
      this.chk_SlitState.Size = new System.Drawing.Size(102, 40);
      this.chk_SlitState.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_SlitState.TabIndex = 2;
      this.chk_SlitState.TabStop = false;
      this.chk_SlitState.Text = "Blade state";
      this.chk_SlitState.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_SlitState.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_SlitState.TranslateText = true;
      this.chk_SlitState.Click += new System.EventHandler(this.chk_SlitState_DoubleClick);
      // 
      // ttb_GrindDuration
      // 
      this.ttb_GrindDuration.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_GrindDuration.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_GrindDuration.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_GrindDuration.ImagePassword = null;
      this.ttb_GrindDuration.Location = new System.Drawing.Point(999, 210);
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
      this.ttb_GrindDuration.Text = "Grinding duration (m)";
      this.ttb_GrindDuration.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_GrindDuration.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_GrindDuration.TranslateText = true;
      this.ttb_GrindDuration.Value = "5";
      this.ttb_GrindDuration.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_LubeDuration
      // 
      this.ttb_LubeDuration.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_LubeDuration.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_LubeDuration.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_LubeDuration.ImagePassword = null;
      this.ttb_LubeDuration.Location = new System.Drawing.Point(999, 300);
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
      this.toolDetail.Location = new System.Drawing.Point(30, 537);
      this.toolDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.toolDetail.Name = "toolDetail";
      this.toolDetail.Size = new System.Drawing.Size(1171, 197);
      this.toolDetail.TabIndex = 204;
      this.toolDetail.TabStop = false;
      this.toolDetail.TranslateText = false;
      this.toolDetail.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // ttl_SlitType
      // 
      this.ttl_SlitType.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SlitType.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SlitType.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SlitType.ImagePassword = null;
      this.ttl_SlitType.Location = new System.Drawing.Point(46, 390);
      this.ttl_SlitType.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.ttl_SlitType.Name = "ttl_SlitType";
      this.ttl_SlitType.Size = new System.Drawing.Size(102, 40);
      this.ttl_SlitType.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_SlitType.TabIndex = 205;
      this.ttl_SlitType.TabStop = false;
      this.ttl_SlitType.Text = "Type of slit";
      this.ttl_SlitType.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_SlitType.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SlitType.TranslateText = true;
      this.ttl_SlitType.Value = "K";
      this.ttl_SlitType.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_SlitType.ValueMultiline = true;
      // 
      // ttl_BladeFromLeft
      // 
      this.ttl_BladeFromLeft.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_BladeFromLeft.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_BladeFromLeft.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_BladeFromLeft.ImagePassword = null;
      this.ttl_BladeFromLeft.Location = new System.Drawing.Point(46, 210);
      this.ttl_BladeFromLeft.Name = "ttl_BladeFromLeft";
      this.ttl_BladeFromLeft.Size = new System.Drawing.Size(202, 40);
      this.ttl_BladeFromLeft.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_BladeFromLeft.TabIndex = 206;
      this.ttl_BladeFromLeft.TabStop = false;
      this.ttl_BladeFromLeft.Text = "Blade from left edge";
      this.ttl_BladeFromLeft.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_BladeFromLeft.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_BladeFromLeft.TranslateText = true;
      this.ttl_BladeFromLeft.Value = "1255";
      this.ttl_BladeFromLeft.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_BladeFromLeft.ValueMultiline = true;
      // 
      // ttl_ChamferFromLeft
      // 
      this.ttl_ChamferFromLeft.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ChamferFromLeft.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ChamferFromLeft.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ChamferFromLeft.ImagePassword = null;
      this.ttl_ChamferFromLeft.Location = new System.Drawing.Point(46, 255);
      this.ttl_ChamferFromLeft.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.ttl_ChamferFromLeft.Name = "ttl_ChamferFromLeft";
      this.ttl_ChamferFromLeft.Size = new System.Drawing.Size(202, 40);
      this.ttl_ChamferFromLeft.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_ChamferFromLeft.TabIndex = 207;
      this.ttl_ChamferFromLeft.TabStop = false;
      this.ttl_ChamferFromLeft.Text = "Chamfer from left edge";
      this.ttl_ChamferFromLeft.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_ChamferFromLeft.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ChamferFromLeft.TranslateText = true;
      this.ttl_ChamferFromLeft.Value = "1255";
      this.ttl_ChamferFromLeft.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_ChamferFromLeft.ValueMultiline = true;
      // 
      // ttl_MinDiameter
      // 
      this.ttl_MinDiameter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_MinDiameter.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_MinDiameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_MinDiameter.ImagePassword = null;
      this.ttl_MinDiameter.Location = new System.Drawing.Point(46, 300);
      this.ttl_MinDiameter.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.ttl_MinDiameter.Name = "ttl_MinDiameter";
      this.ttl_MinDiameter.Size = new System.Drawing.Size(202, 40);
      this.ttl_MinDiameter.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_MinDiameter.TabIndex = 208;
      this.ttl_MinDiameter.TabStop = false;
      this.ttl_MinDiameter.Text = "Minimum blade diameter";
      this.ttl_MinDiameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_MinDiameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_MinDiameter.TranslateText = true;
      this.ttl_MinDiameter.Value = "1255";
      this.ttl_MinDiameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_MinDiameter.ValueMultiline = true;
      // 
      // ttl_MaxDiameter
      // 
      this.ttl_MaxDiameter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_MaxDiameter.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_MaxDiameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_MaxDiameter.ImagePassword = null;
      this.ttl_MaxDiameter.Location = new System.Drawing.Point(46, 345);
      this.ttl_MaxDiameter.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.ttl_MaxDiameter.Name = "ttl_MaxDiameter";
      this.ttl_MaxDiameter.Size = new System.Drawing.Size(202, 40);
      this.ttl_MaxDiameter.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_MaxDiameter.TabIndex = 209;
      this.ttl_MaxDiameter.TabStop = false;
      this.ttl_MaxDiameter.Text = "Maximum blade diameter";
      this.ttl_MaxDiameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_MaxDiameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_MaxDiameter.TranslateText = true;
      this.ttl_MaxDiameter.Value = "1255";
      this.ttl_MaxDiameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_MaxDiameter.ValueMultiline = true;
      // 
      // ActiveCtrl
      // 
      this.ActiveCtrl.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ActiveCtrl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ActiveCtrl.Location = new System.Drawing.Point(108, 61);
      this.ActiveCtrl.Name = "ActiveCtrl";
      this.ActiveCtrl.Size = new System.Drawing.Size(344, 32);
      this.ActiveCtrl.TabIndex = 556;
      this.ActiveCtrl.Text = "Active control: ";
      this.ActiveCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ActiveCtrl.Visible = false;
      // 
      // tit_Slit
      // 
      this.tit_Slit.BackColor = System.Drawing.Color.Black;
      this.tit_Slit.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tit_Slit.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tit_Slit.ForeColor = System.Drawing.Color.White;
      this.tit_Slit.Location = new System.Drawing.Point(30, 184);
      this.tit_Slit.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.tit_Slit.Name = "tit_Slit";
      this.tit_Slit.Size = new System.Drawing.Size(715, 269);
      this.tit_Slit.Style = SyncroC.Controls.TitleStyles.Big;
      this.tit_Slit.TabIndex = 557;
      this.tit_Slit.TabStop = false;
      this.tit_Slit.Text = "SLIT 1  - SHAFT 2";
      this.tit_Slit.TranslateText = false;
      // 
      // ResetGrinder
      // 
      this.ResetGrinder.Active = false;
      this.ResetGrinder.Alarmed = false;
      this.ResetGrinder.Auto = false;
      this.ResetGrinder.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ResetGrinder.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ResetGrinder.FillBackColorWithImage = false;
      this.ResetGrinder.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ResetGrinder.FramePresent = true;
      this.ResetGrinder.HighLightEnable = false;
      this.ResetGrinder.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.ResetGrinder.ImagePassword = null;
      this.ResetGrinder.Inemergency = false;
      this.ResetGrinder.InLocal = false;
      this.ResetGrinder.IsButton = true;
      this.ResetGrinder.IsForceToInvisible = false;
      this.ResetGrinder.Location = new System.Drawing.Point(999, 357);
      this.ResetGrinder.Name = "ResetGrinder";
      this.ResetGrinder.OnLine = false;
      this.ResetGrinder.ShowImage = false;
      this.ResetGrinder.ShowText = true;
      this.ResetGrinder.Size = new System.Drawing.Size(192, 60);
      this.ResetGrinder.Switching = false;
      this.ResetGrinder.TabIndex = 558;
      this.ResetGrinder.Text = "Reset grinder";
      this.ResetGrinder.TranslateText = true;
      this.ResetGrinder.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ResetGrinder.UserDefinedActiveImage")));
      this.ResetGrinder.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ResetGrinder.UserDefinedImage")));
      this.ResetGrinder.Warned = false;
      this.ResetGrinder.Click += new System.EventHandler(this.ResetGrinder_Click);
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
      this.lab_Lubricating.Location = new System.Drawing.Point(904, 456);
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
      this.LubeSlit.Location = new System.Drawing.Point(292, 465);
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
      // EnableGrinding
      // 
      this.EnableGrinding.Active = false;
      this.EnableGrinding.Alarmed = false;
      this.EnableGrinding.Auto = false;
      this.EnableGrinding.BackColor = System.Drawing.Color.WhiteSmoke;
      this.EnableGrinding.Cursor = System.Windows.Forms.Cursors.Hand;
      this.EnableGrinding.FillBackColorWithImage = false;
      this.EnableGrinding.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.EnableGrinding.FramePresent = true;
      this.EnableGrinding.HighLightEnable = false;
      this.EnableGrinding.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.EnableGrinding.ImagePassword = null;
      this.EnableGrinding.Inemergency = false;
      this.EnableGrinding.InLocal = false;
      this.EnableGrinding.IsButton = true;
      this.EnableGrinding.IsForceToInvisible = false;
      this.EnableGrinding.Location = new System.Drawing.Point(708, 465);
      this.EnableGrinding.Name = "EnableGrinding";
      this.EnableGrinding.OnLine = false;
      this.EnableGrinding.ShowImage = false;
      this.EnableGrinding.ShowText = true;
      this.EnableGrinding.Size = new System.Drawing.Size(196, 70);
      this.EnableGrinding.Switching = false;
      this.EnableGrinding.TabIndex = 563;
      this.EnableGrinding.Text = "Enable Grinding";
      this.EnableGrinding.TranslateText = true;
      this.EnableGrinding.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("EnableGrinding.UserDefinedActiveImage")));
      this.EnableGrinding.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("EnableGrinding.UserDefinedImage")));
      this.EnableGrinding.Warned = false;
      this.EnableGrinding.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // ResetSlitCounters
      // 
      this.ResetSlitCounters.Active = false;
      this.ResetSlitCounters.Alarmed = false;
      this.ResetSlitCounters.Auto = false;
      this.ResetSlitCounters.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ResetSlitCounters.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ResetSlitCounters.FillBackColorWithImage = false;
      this.ResetSlitCounters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ResetSlitCounters.FramePresent = true;
      this.ResetSlitCounters.HighLightEnable = false;
      this.ResetSlitCounters.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.ResetSlitCounters.ImagePassword = null;
      this.ResetSlitCounters.Inemergency = false;
      this.ResetSlitCounters.InLocal = false;
      this.ResetSlitCounters.IsButton = true;
      this.ResetSlitCounters.IsForceToInvisible = false;
      this.ResetSlitCounters.Location = new System.Drawing.Point(627, 210);
      this.ResetSlitCounters.Name = "ResetSlitCounters";
      this.ResetSlitCounters.OnLine = false;
      this.ResetSlitCounters.ShowImage = false;
      this.ResetSlitCounters.ShowText = true;
      this.ResetSlitCounters.Size = new System.Drawing.Size(119, 96);
      this.ResetSlitCounters.Switching = false;
      this.ResetSlitCounters.TabIndex = 564;
      this.ResetSlitCounters.Text = "Reset Counters";
      this.ResetSlitCounters.TranslateText = true;
      this.ResetSlitCounters.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ResetSlitCounters.UserDefinedActiveImage")));
      this.ResetSlitCounters.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ResetSlitCounters.UserDefinedImage")));
      this.ResetSlitCounters.Warned = false;
      this.ResetSlitCounters.Click += new System.EventHandler(this.ResetSlitCounters_Click);
      // 
      // ttl_GrindMetersSinceRegulation
      // 
      this.ttl_GrindMetersSinceRegulation.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_GrindMetersSinceRegulation.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_GrindMetersSinceRegulation.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_GrindMetersSinceRegulation.ImagePassword = null;
      this.ttl_GrindMetersSinceRegulation.Location = new System.Drawing.Point(303, 343);
      this.ttl_GrindMetersSinceRegulation.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
      this.ttl_GrindMetersSinceRegulation.Name = "ttl_GrindMetersSinceRegulation";
      this.ttl_GrindMetersSinceRegulation.Size = new System.Drawing.Size(276, 40);
      this.ttl_GrindMetersSinceRegulation.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_GrindMetersSinceRegulation.TabIndex = 565;
      this.ttl_GrindMetersSinceRegulation.TabStop = false;
      this.ttl_GrindMetersSinceRegulation.Text = "Grind metres since last regulation";
      this.ttl_GrindMetersSinceRegulation.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_GrindMetersSinceRegulation.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_GrindMetersSinceRegulation.TranslateText = true;
      this.ttl_GrindMetersSinceRegulation.Value = "5";
      this.ttl_GrindMetersSinceRegulation.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_GrindMetersSinceRegulation.ValueMultiline = true;
      // 
      // ttl_TotalGrindMeters
      // 
      this.ttl_TotalGrindMeters.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_TotalGrindMeters.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_TotalGrindMeters.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_TotalGrindMeters.ImagePassword = null;
      this.ttl_TotalGrindMeters.Location = new System.Drawing.Point(303, 300);
      this.ttl_TotalGrindMeters.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
      this.ttl_TotalGrindMeters.Name = "ttl_TotalGrindMeters";
      this.ttl_TotalGrindMeters.Size = new System.Drawing.Size(278, 40);
      this.ttl_TotalGrindMeters.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_TotalGrindMeters.TabIndex = 566;
      this.ttl_TotalGrindMeters.TabStop = false;
      this.ttl_TotalGrindMeters.Text = "Total Grind metres ";
      this.ttl_TotalGrindMeters.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_TotalGrindMeters.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_TotalGrindMeters.TranslateText = true;
      this.ttl_TotalGrindMeters.Value = "5";
      this.ttl_TotalGrindMeters.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_TotalGrindMeters.ValueMultiline = true;
      // 
      // StopMultiple
      // 
      this.StopMultiple.Active = false;
      this.StopMultiple.Alarmed = false;
      this.StopMultiple.Auto = false;
      this.StopMultiple.BackColor = System.Drawing.Color.WhiteSmoke;
      this.StopMultiple.Cursor = System.Windows.Forms.Cursors.Hand;
      this.StopMultiple.FillBackColorWithImage = false;
      this.StopMultiple.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.StopMultiple.ForeColor = System.Drawing.SystemColors.ControlText;
      this.StopMultiple.FramePresent = true;
      this.StopMultiple.HighLightEnable = false;
      this.StopMultiple.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.StopMultiple.ImagePassword = null;
      this.StopMultiple.Inemergency = false;
      this.StopMultiple.InLocal = false;
      this.StopMultiple.IsButton = true;
      this.StopMultiple.IsForceToInvisible = false;
      this.StopMultiple.Location = new System.Drawing.Point(432, 465);
      this.StopMultiple.Name = "StopMultiple";
      this.StopMultiple.OnLine = false;
      this.StopMultiple.ShowImage = false;
      this.StopMultiple.ShowText = true;
      this.StopMultiple.Size = new System.Drawing.Size(132, 70);
      this.StopMultiple.Switching = false;
      this.StopMultiple.TabIndex = 567;
      this.StopMultiple.Text = "Stop";
      this.StopMultiple.TranslateText = true;
      this.StopMultiple.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("StopMultiple.UserDefinedActiveImage")));
      this.StopMultiple.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("StopMultiple.UserDefinedImage")));
      this.StopMultiple.Visible = false;
      this.StopMultiple.Warned = false;
      this.StopMultiple.Click += new System.EventHandler(this.StopMultiple_Click);
      // 
      // Frm_twin400_blades
      // 
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.ttl_GrindMetersSinceRegulation);
      this.Controls.Add(this.StopMultiple);
      this.Controls.Add(this.ttl_TotalGrindMeters);
      this.Controls.Add(this.EnableGrinding);
      this.Controls.Add(this.GrindSlit);
      this.Controls.Add(this.ResetSlitCounters);
      this.Controls.Add(this.LubeSlit);
      this.Controls.Add(this.lab_Lubricating);
      this.Controls.Add(this.lab_Grinding);
      this.Controls.Add(this.tit_Slit);
      this.Controls.Add(this.ActiveCtrl);
      this.Controls.Add(this.ttl_MaxDiameter);
      this.Controls.Add(this.ttl_MinDiameter);
      this.Controls.Add(this.ttl_ChamferFromLeft);
      this.Controls.Add(this.ttl_BladeFromLeft);
      this.Controls.Add(this.ttl_SlitType);
      this.Controls.Add(this.chk_SlitState);
      this.Controls.Add(this.ttl_MetersSinceGrind);
      this.Controls.Add(this.ttl_Grindings);
      this.Controls.Add(this.ttl_RunMeters);
      this.Controls.Add(this.title5);
      this.Controls.Add(this.toolDetail);
      this.Controls.Add(this.ttl_GrindToolRunMeters);
      this.Controls.Add(this.ResetGrinder);
      this.Controls.Add(this.ttl_GrindInterval);
      this.Controls.Add(this.ttl_TrimGrindMultiplier);
      this.Controls.Add(this.ttb_GrindDuration);
      this.Controls.Add(this.ttb_LubeDuration);
      this.Controls.Add(this.ttl_LubeInterval);
      this.Controls.Add(this.ttl_MetersSinceLube);
      this.Location = new System.Drawing.Point(231, 13);
      this.Name = "Frm_twin400_blades";
      this.Tag = "";
      this.Text = "Frm_twin400_blades";
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Frm_twin400_blades_KeyUp);
      this.Controls.SetChildIndex(this.ttl_MetersSinceLube, 0);
      this.Controls.SetChildIndex(this.ttl_LubeInterval, 0);
      this.Controls.SetChildIndex(this.ttb_LubeDuration, 0);
      this.Controls.SetChildIndex(this.ttb_GrindDuration, 0);
      this.Controls.SetChildIndex(this.ttl_TrimGrindMultiplier, 0);
      this.Controls.SetChildIndex(this.ttl_GrindInterval, 0);
      this.Controls.SetChildIndex(this.ResetGrinder, 0);
      this.Controls.SetChildIndex(this.ttl_GrindToolRunMeters, 0);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.toolDetail, 0);
      this.Controls.SetChildIndex(this.title5, 0);
      this.Controls.SetChildIndex(this.ttl_RunMeters, 0);
      this.Controls.SetChildIndex(this.ttl_Grindings, 0);
      this.Controls.SetChildIndex(this.ttl_MetersSinceGrind, 0);
      this.Controls.SetChildIndex(this.chk_SlitState, 0);
      this.Controls.SetChildIndex(this.ttl_SlitType, 0);
      this.Controls.SetChildIndex(this.ttl_BladeFromLeft, 0);
      this.Controls.SetChildIndex(this.ttl_ChamferFromLeft, 0);
      this.Controls.SetChildIndex(this.ttl_MinDiameter, 0);
      this.Controls.SetChildIndex(this.ttl_MaxDiameter, 0);
      this.Controls.SetChildIndex(this.ActiveCtrl, 0);
      this.Controls.SetChildIndex(this.tit_Slit, 0);
      this.Controls.SetChildIndex(this.lab_Grinding, 0);
      this.Controls.SetChildIndex(this.lab_Lubricating, 0);
      this.Controls.SetChildIndex(this.LubeSlit, 0);
      this.Controls.SetChildIndex(this.ResetSlitCounters, 0);
      this.Controls.SetChildIndex(this.GrindSlit, 0);
      this.Controls.SetChildIndex(this.EnableGrinding, 0);
      this.Controls.SetChildIndex(this.ttl_TotalGrindMeters, 0);
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
      this.Controls.SetChildIndex(this.StopMultiple, 0);
      this.Controls.SetChildIndex(this.ttl_GrindMetersSinceRegulation, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

		public override void refreshData()
		{
			base.refreshData();
			this.UpdateBrokenSlits();
			this.UpdateValues();
      this.toolDetail.Invalidate();
    }

		private void UpdateValues()
		{
      //agrassi_FI08-09-2010_1
      if (this.extToolDetail)
        return;
      if (this.mod || this.modBroken)
        return;
      //agrassi_FI07-04-08_10
      //double consumption, actualDiameter, protrusion;
			SyncroC.DataBase.LineConfig.SlitterScorer ss = DBManager.getDBManager().lineConfig.SlitterScorerConfig;
      SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
      this.tit_Slit.Text = msg.getTranslatedMessage("§SYCC_60") + " " + (this.slitIndex + 1).ToString() +
        " - " + msg.getTranslatedMessage("§SYCC_61") + " " + (this.shaftIndex + 1).ToString();
			int[] tmpBrokenSlits = (int[])(this.modBroken ? this.newBrokenSlits : this.brokenSlits).Clone();
      this.chk_SlitState.Checked = (!((tmpBrokenSlits[this.shaftIndex] & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(this.slitIndex)) > 0));
      this.ttl_RunMeters.Value = this.cleanCutData.slitCounters[this.shaftIndex][this.slitIndex].totalRunMeters.ToString();
			//RFAZZI_FI04/11/08_1. APM TW4084. Added new counters "TotalGrindMeters" and "grindMetersSinceLastRegulation" in the machine protocol.
			//this.ttl_Grindings.Value = this.cleanCutData.slitCounters[this.shaftIndex][this.slitIndex].totalGrindings.ToString();
      this.ttl_MetersSinceGrind.Value = this.cleanCutData.slitCounters[this.shaftIndex][this.slitIndex].metersSinceLastGrind.ToString();

      //PMASSARO_FI08/08/11 - Riabilitazione Lubrificazione automatica su PMAC400 per tutte le versioni di PLC
      /*
      //RFAZZI_FI27/08/08_1. Added value MetersSinceLastSlitRegolation on graphics
			//if (ss.PLCInterfaceVersion > SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver5)
			if (ss.Twin400.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver5)
			{
				//RFAZZI_FI04/11/08_1. APM TW4084. Added new counters "TotalGrindMeters" and "grindMetersSinceLastRegulation" in the machine protocol.
				this.ttl_GrindMetersSinceRegulation.Value = this.cleanCutData.slitCounters[this.shaftIndex][this.slitIndex].grindMetersSinceLastRegulation.ToString();
				this.ttl_TotalGrindMeters.Value = this.cleanCutData.slitCounters[this.shaftIndex][this.slitIndex].totalGrindMetres.ToString();
			}
			else
			{
				this.ttl_MetersSinceLube.Value = this.cleanCutData.slitCounters[this.shaftIndex][this.slitIndex].metersSinceLastLube.ToString();  
				this.ttl_Grindings.Value = this.cleanCutData.slitCounters[this.shaftIndex][this.slitIndex].totalGrindings.ToString();
			}
      */
      if (ss.Twin400.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver5)
      {
        this.ttl_GrindMetersSinceRegulation.Value = this.cleanCutData.slitCounters[this.shaftIndex][this.slitIndex].grindMetersSinceLastRegulation.ToString();
        this.ttl_TotalGrindMeters.Value = this.cleanCutData.slitCounters[this.shaftIndex][this.slitIndex].totalGrindMetres.ToString();
        this.ttl_MetersSinceLube.Value = this.cleanCutData.slitCounters[this.shaftIndex][this.slitIndex].metersSinceLastLube.ToString();
        if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication)
        {
          this.ttl_GrindMetersSinceRegulation.Visible = false;
          this.ttl_MetersSinceLube.Visible = true;
        }
        else
        {
          this.ttl_GrindMetersSinceRegulation.Visible = true;
          this.ttl_MetersSinceLube.Visible = false;
        }
      }
      else
      {
        this.ttl_Grindings.Value = this.cleanCutData.slitCounters[this.shaftIndex][this.slitIndex].totalGrindings.ToString();
      }

      this.ttl_GrindToolRunMeters.Value = this.cleanCutData.grindToolMeters.ToString();
      this.ttl_GrindInterval.Value = this.cleanCutData.grindInterval.ToString();
      this.ttl_TrimGrindMultiplier.Value = this.cleanCutData.trimGrindingMultiplier.ToString();
      this.ttb_GrindDuration.Value = this.cleanCutData.grindDuration.ToString();
      
      //RFAZZI_FI07/06/2010_1. Aggiunta gestione per togliere a run time i parametri sulla lubrificazione in caso di Twin400V1. 
      if (ss.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication)
      {
        this.ttl_LubeInterval.Value = this.cleanCutData.lubeInterval.ToString();
        this.ttb_LubeDuration.Value = this.cleanCutData.lubeDuration.ToString();
      }
      //DataRowCollection setupRows = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows;
      //DataRowCollection qualityRows = DBManager.getDBManager().quality.quality.Tables[0].Rows;
      //if ((setupRows.Count > 0) && (qualityRows.Count>0))
      //{
      //  string quality = setupRows[0][Order.DEL_QUALITY_ID].ToString().Trim();
      //  if (!quality.Equals(string.Empty))
      //  {
      //    DataRow qualityFound=qualityRows.Find(quality);
      //    if (qualityFound!=null)
      //    {
      //      this.ttl_GrindInterval.Value = qualityFound[Quality.GRINDING_PARAMETER_UDm].ToString();
      //      this.ttl_LubeInterval.Value = qualityFound[Quality.LUBRICATION_PARAMETER_UDm].ToString();
      //      this.ttl_TrimGrindMultiplier.Value = qualityFound[Quality.TRIM_GRINDINGS_MULTIPLIER].ToString();
      //    }
      //    else
      //    {
      //      this.ttl_GrindInterval.Value = "0";
      //      this.ttl_LubeInterval.Value = "0";
      //      this.ttl_TrimGrindMultiplier.Value = "0";
      //    }
      //  }
      //  else 
      //  {
      //    this.ttl_GrindInterval.Value = "0";
      //    this.ttl_LubeInterval.Value = "0";
      //    this.ttl_TrimGrindMultiplier.Value = "0";
      //  }
      //}
      int typ;
      char slitType=ss.GetSlitType(this.shaftIndex, this.slitIndex);
      this.ttl_SlitType.Value = slitType.ToString();
			for (typ = 0; typ < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLIT_TYPES_MACHINE; typ++)
				if (ss.SlitterScorerHeader.SlitTypes[typ].Letter == slitType)
          break;
			if (typ < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLIT_TYPES_MACHINE)
      {
				this.ttl_BladeFromLeft.Value = ss.SlitterScorerHeader.SlitTypes[typ].BladeFromLeft.ToString();
				this.ttl_ChamferFromLeft.Value = ss.SlitterScorerHeader.SlitTypes[typ].HandleFromLeft.ToString();
				this.ttl_MinDiameter.Value = ss.SlitterScorerHeader.SlitTypes[typ].MinBladeDiameter.ToString();
				this.ttl_MaxDiameter.Value = ss.SlitterScorerHeader.SlitTypes[typ].MaxBladeDiameter.ToString();
      }
      //agrassi_FI07-04-08_10
      //consumption = (double)ss.KBladeConsumption * (double)this.cleanCutData.slitCounters[this.shaftIndex][this.slitIndex].totalGrindings / 100.0;
      //actualDiameter = ((double)ss.SlitTypes[typ].MaxBladeDiameter - 2*consumption);
      //protrusion = (actualDiameter - (double)ss.SlitTypes[typ].SupportDiameter) / 2.0;
      //this.ttl_Kconsumption.Value = ss.KBladeConsumption.ToString();
      //this.ttl_ActualDiameter.Value = actualDiameter.ToString();
      //if (actualDiameter <= ss.SlitTypes[typ].MinBladeDiameter)
      //  this.ttl_ActualDiameter.ForeColor = Color.Red;
      //else
      //  this.ttl_ActualDiameter.ForeColor = Color.Black;
      //this.ttl_Consumption.Value = consumption.ToString();
      //this.ttb_Protrusion.Value = protrusion.ToString();
      this.toolDetail.Focus();
    }

    private void _UpdateCleanCutCounters(string slitsData)
    {
			string[] sData=slitsData.Split(Command.PARAMETER_SEPARATOR);

			//RFAZZI_FI27/08/08_1. Added value MetersSinceLastSlitRegolation on graphics
			//if (sData.Length != 86) return;
			//for (int sh = 0; sh < SyncroC.DataBase.LineConfig.SlitterScorer.NUM_SLIT_SHAFTS; sh++)
			//{
			//  for (int slt = 0; slt < SyncroC.DataBase.LineConfig.SlitterScorer.MAX_SLITS_PER_SHAFT; slt++)
			//  {
			//    this.cleanCutData.slitCounters[sh][slt].totalRunMeters = intParse(sData[(sh * 10 + slt) * 4 + 0]);
			//    this.cleanCutData.slitCounters[sh][slt].totalGrindings = intParse(sData[(sh * 10 + slt) * 4 + 1]);
			//    this.cleanCutData.slitCounters[sh][slt].metersSinceLastGrind = intParse(sData[(sh * 10 + slt) * 4 + 2]);
			//    this.cleanCutData.slitCounters[sh][slt].metersSinceLastLube = intParse(sData[(sh * 10 + slt) * 4 + 3]);
			//  }
			//}
			//this.cleanCutData.grindToolMeters = intParse(sData[SyncroC.DataBase.LineConfig.SlitterScorer.NUM_SLIT_SHAFTS * SyncroC.DataBase.LineConfig.SlitterScorer.MAX_SLITS_PER_SHAFT * 4 + 0]);
			//this.cleanCutData.grindDuration = intParse(sData[SyncroC.DataBase.LineConfig.SlitterScorer.NUM_SLIT_SHAFTS * SyncroC.DataBase.LineConfig.SlitterScorer.MAX_SLITS_PER_SHAFT * 4 + 1]);
			//this.cleanCutData.lubeDuration = intParse(sData[SyncroC.DataBase.LineConfig.SlitterScorer.NUM_SLIT_SHAFTS * SyncroC.DataBase.LineConfig.SlitterScorer.MAX_SLITS_PER_SHAFT * 4 + 2]);
			//this.cleanCutData.grindInterval = intParse(sData[SyncroC.DataBase.LineConfig.SlitterScorer.NUM_SLIT_SHAFTS * SyncroC.DataBase.LineConfig.SlitterScorer.MAX_SLITS_PER_SHAFT * 4 + 3]);
			//this.cleanCutData.lubeInterval = intParse(sData[SyncroC.DataBase.LineConfig.SlitterScorer.NUM_SLIT_SHAFTS * SyncroC.DataBase.LineConfig.SlitterScorer.MAX_SLITS_PER_SHAFT * 4 + 4]);
			//this.cleanCutData.trimGrindingMultiplier = intParse(sData[SyncroC.DataBase.LineConfig.SlitterScorer.NUM_SLIT_SHAFTS * SyncroC.DataBase.LineConfig.SlitterScorer.MAX_SLITS_PER_SHAFT * 4 + 5]);

			if (sData.Length != 126) return;
			for (int sh = 0; sh < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
			{
				for (int slt = 0; slt < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
				{
					this.cleanCutData.slitCounters[sh][slt].totalRunMeters = intParse(sData[(sh * 10 + slt) * 6 + 0]);
					this.cleanCutData.slitCounters[sh][slt].totalGrindings = intParse(sData[(sh * 10 + slt) * 6 + 1]);
					this.cleanCutData.slitCounters[sh][slt].metersSinceLastGrind = intParse(sData[(sh * 10 + slt) * 6 + 2]);
					this.cleanCutData.slitCounters[sh][slt].metersSinceLastLube = intParse(sData[(sh * 10 + slt) * 6 + 3]);
					//RFAZZI_FI04/11/08_1. APM TW4084. Added new counters "TotalGrindMeters" and "grindMetersSinceLastRegulation" in the machine protocol.
					this.cleanCutData.slitCounters[sh][slt].grindMetersSinceLastRegulation = intParse(sData[(sh * 10 + slt) * 6 + 4]);
					this.cleanCutData.slitCounters[sh][slt].totalGrindMetres = intParse(sData[(sh * 10 + slt) * 6 + 5]);
				}
			}
			this.cleanCutData.grindToolMeters = intParse(sData[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS * SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT * 6 + 0]);
			this.cleanCutData.grindDuration = intParse(sData[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS * SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT * 6 + 1]);
			this.cleanCutData.lubeDuration = intParse(sData[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS * SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT * 6 + 2]);
			this.cleanCutData.grindInterval = intParse(sData[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS * SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT * 6 + 3]);
			this.cleanCutData.lubeInterval = intParse(sData[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS * SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT * 6 + 4]);
			this.cleanCutData.trimGrindingMultiplier = intParse(sData[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS * SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT * 6 + 5]);

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

    //agrassi_FI08-09-2010_1
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
      if (!(this.mod || this.modBroken)) return;
      this.ChangeTextBoxesEnableMod(false);
      this.ChangeTextBoxesEnableModBroken(false);
      CommunicationManager cm = null;
      if (this.mod)
      {
        if ((!this.ttb_GrindDuration.Value.Equals(this.newGrindDuration)) ||
          (!this.ttb_LubeDuration.Value.Equals(this.newLubeDuration)))
        {
          if (cm == null) cm = CommunicationManager.GetCommunicationManager();
          cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
            PageManager.getRequestCommand(typeof(Frm_twin400_blades)) + "." + Command.CLEANCUT_DATA +
            "(" + this.ttb_GrindDuration.Value + "," + this.ttb_LubeDuration.Value + ")");
        }
      }
      if (this.modBroken)
      {
        if ((this.brokenSlits[(int)Shaft.Slit1]!=this.newBrokenSlits[(int)Shaft.Slit1]) ||
				  (this.brokenSlits[(int)Shaft.Slit2]!=this.newBrokenSlits[(int)Shaft.Slit2]))
			  {
				  if (cm==null) cm=CommunicationManager.GetCommunicationManager();
				  cm.SendTcp(cm.SYNCROS_RTC_MANAGER, 
					  PageManager.getRequestCommand(typeof(Frm_twin400_blades))+ "." + Command.BROKEN_SLITS + 
					  "("+newBrokenSlits[0].ToString()+","+newBrokenSlits[1].ToString()+")");
			  }
        //this.toolDetail.EnableMultiSelect(false);
        this.toolDetail.ResetMultiSelection();
      }
			this.mod=false;
      this.modBroken = false;
      this.UpdateValues();
    }

    protected override void CtrlF3()
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      this.ChangeTextBoxesEnableModBroken(true);
      this.newBrokenSlits = (int[])this.brokenSlits.Clone();
      this.mod = false;
      this.modBroken = true;
      //this.toolDetail.EnableMultiSelect(true);
      this.toolDetail.ResetMultiSelection();
    }

    protected override void CtrlF5()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      this.ChangeTextBoxesEnableMod(true);
			this.newBrokenSlits=(int[])this.brokenSlits.Clone();
			this.newGrindDuration=this.ttb_GrindDuration.Value;
			this.newLubeDuration=this.ttb_LubeDuration.Value;
      //agrassi_FI07-04-08_10
      //this.newProtrusion=this.ttb_Protrusion.Value;
			this.mod=true;
      this.modBroken = false;
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

    private void ChangeTextBoxesEnableModBroken(bool en)
    {
      this.chk_SlitState.ReadOnly = !en;
      this.chk_SlitState.TabStop = en;
      if (en)
        this.chk_SlitState.Focus();
      else
        this.tab_Tab1.Focus();
    }

    private void UpdateBrokenSlits()
    {
			for (Shaft sh=Shaft.Slit1; sh<=Shaft.Slit2; sh++)
				this.brokenSlits[(int)sh] = DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.SlitShafts[(int)sh].BrokenBlades;
		}

		private void chk_SlitState_DoubleClick(object sender, System.EventArgs e)
		{
      if (!this.modBroken) return;
      this.toolDetail.ToggleSelection(this.shaftIndex, this.slitIndex);
    }

    //agrassi_FI08-09-2010_1
    //protected override void Escape()
    public void StopMultiple_Click(object sender, EventArgs e)
    {
      if (this.multipleGrindingRunning)
      {
        Frm_Confirm confirm = new Frm_Confirm();
        SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
        string text = msg.getTranslatedMessage("§SYCC_176");
        DialogResult res = confirm.ShowDialog(this, text);
        if (res == DialogResult.OK)
        {
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
            PageManager.getRequestCommand(typeof(Frm_twin400_blades)) + ".ResetMultipleGrinding()");
          return;
        }
      }
      this.toolDetail.ResetMultiSelection();
      if (!(this.mod || this.modBroken)) return;
      this.ChangeTextBoxesEnableMod(false);
      this.ChangeTextBoxesEnableModBroken(false);
      this.mod = false;
      this.modBroken = false;
      //this.toolDetail.EnableMultiSelect(false);
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

    //agrassi_FI08-09-2010_1
    //private void ChangeShaft(int slitIndex, int shaftIndex)
    public void ChangeShaft(int slitIndex, int shaftIndex)
    {
      if (this.shaftIndex != shaftIndex)
        this.shaftIndex = this.toolDetail.ChangeShaft(false);
      this.slitIndex = slitIndex;
      this.UpdateValues();
    }

    //agrassi_FI08-09-2010_1
    //private void UpdateTempBrokenSlits()
    public void UpdateTempBrokenSlits()
    {
      if (this.modBroken) this.newBrokenSlits[this.shaftIndex] ^= SyncroC.DataBase.LineConfig.SlitterScorer.BIT(this.slitIndex);
      this.UpdateValues();
    }

    private void Frm_twin400_blades_KeyUp(object sender, KeyEventArgs e)
    {
      this.ActiveCtrl.Text = "Active control: " + this.ActiveControl.Name + " (" + this.ActiveControl.TabIndex.ToString() + ")";
      //if (this.modBroken)
        if (e.KeyCode == Keys.Enter)
          if (!this.multipleGrindingRunning)
            this.toolDetail.ToggleSelection(this.shaftIndex, this.slitIndex);
    }

    protected override void ControlD()
    {
      base.ControlD();
      ActiveCtrl.Visible = !ActiveCtrl.Visible;
      this.toolDetail.Debug = !this.toolDetail.Debug;
    }

    //agrassi_FI08-09-2010_1
    //private void OnButtonPush(object sender, EventArgs e)
    public void OnButtonPush(object sender, EventArgs e)
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(Frm_twin400_blades)) + "." + ((Control)sender).Name + "()");
    }

    //agrassi_FI18-04-08_1
    //private void _SetGrinding(bool val, int shaft, int slit, bool multiple, string grindCounters, bool maintenance)
    //agrassi_FI08-09-2010_1
    //private void _SetGrinding(bool val, int shaft, int slit, bool multiple, string grindCounters, bool maintenance)
    public void _SetGrinding(bool val, int shaft, int slit, bool multiple, string grindCounters, bool maintenance)
    {
      this.grindingRunning = val;
      this.lab_Grinding.Visible = val;
      if (val)
      {
        SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
        //agrassi_FI18-04-08_1
        //if ((slit >= 0) && (slit < DBManager.getDBManager().lineConfig.SlitterScorerConfig.GetSlitCount()) &&
        //    (shaft >= 0) && (shaft < DataBase.LineConfig.SlitterScorer.NUM_SLIT_SHAFTS))
        if ((slit >= 0) && (slit < DBManager.getDBManager().lineConfig.SlitterScorerConfig.GetSlitCount()) &&
            (shaft >= 0) && (shaft < DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS) && (!maintenance))
          this.lab_Grinding.Text = msg.getTranslatedMessage("§SYCC_66# §SYCC_60# " + (slit + 1).ToString() + ", §SYCC_61# " + (shaft + 1).ToString());
        else
          this.lab_Grinding.Text = msg.getTranslatedMessage("§SYCC_66");
      }
      if (multiple)
        this.multipleGrindingShaft = shaft;
      else
        this.multipleGrindingShaft = -1;
      this.timGrinding.Enabled = val;
      this.multipleGrindingRunning = multiple;
      this.SetMultipleGrinding();
      this.numberOfGrindings=grindCounters.Split(Command.PARAMETER_SEPARATOR);
      //for (int i = 0; i < this.numberOfGrindings.Length; i++)
      //    if (this.numberOfGrindings[i] == "0")
      //        this.numberOfGrindings[i] = " ";

			//RFAZZI_FI01/07/2008_1.Added control for refresh the slitter only when needed
			if ( multiple || val )
				this.toolDetail.PositionOnlySlitsGrinding(this.numberOfGrindings, this.multipleGrindingShaft);
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

    public void GrindSlit_Click(object sender, EventArgs e)
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
			//RFAZZI PROVA
			//if (!DBManager.getDBManager().lineConfig.SlitterScorerConfig.PositioningOptions.Slits.EnableGrinding)
			if (!DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.PositioningOptions.Slits.EnableGrinding)
      {
        SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
        string text = msg.getTranslatedMessage("§SYCC_170");
        Frm_Alert confirm = new Frm_Alert();
        confirm.ShowDialog(text);
        return;
      }
      bool multiple=false;
      string[] selEnum = new string[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
      int[] slitCount=new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
      for (int sh = 0; sh < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
      {
        this.selectedMaskForGrinding[sh] = this.toolDetail.GetSelectedSlitMask(sh);
        multiple=multiple || (this.selectedMaskForGrinding[sh]>0);
        selEnum[sh] = "";
        for (int slt = 0; slt < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
          if (((this.selectedMaskForGrinding[sh] & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(slt)) != 0) &&
              ((this.plungedSlitMask[sh] & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(slt)) != 0))
          {
            selEnum[sh] += ((selEnum[sh] == "") ? "" : ", ") + (slt + 1).ToString();
            slitCount[sh]++;
          }
      }
      if (multiple)
      {
        SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
        if ((selectedMaskForGrinding[(int)Shaft.Slit1] > 0) && (selectedMaskForGrinding[(int)Shaft.Slit2] > 0))
        {
          Frm_Alert alert = new Frm_Alert();
          alert.ShowDialog(this, msg.getTranslatedMessage("§SYCC_177"));
          return;
        }
        string text = "";
        if (selEnum[0].Length > 0)
        {
          text += msg.getTranslatedMessage("§SYCC_173# " + selEnum[0]+" §SYCC_174# 1");
          if (selEnum[1].Length > 0)
            text += ".\n";
        }
        else if (selEnum[1].Length > 0)
          text += msg.getTranslatedMessage("§SYCC_173# " + selEnum[1] + " §SYCC_174# 2");
        if ((selEnum[0].Length == 0) && (selEnum[1].Length == 0))
          return;
        text += ".\n" + msg.getTranslatedMessage("§SYCC_175#\n(MAX 5)");
        Frm_AskValueAndConfirm confirm = new Frm_AskValueAndConfirm();
        FosberDialogResult res= confirm.ShowDialog(this, text, 1, 1, 5);
        if (res.Dialog == DialogResult.OK)
        {
          string grindString = "";
          int shaftToGrind = (int)Shaft.Slit1;
          if (selectedMaskForGrinding[(int)Shaft.Slit2]>0)
            shaftToGrind=(int)Shaft.Slit2;
          bool first=true;
          for (int slt = 0; slt < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
            if (((this.selectedMaskForGrinding[shaftToGrind] & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(slt)) != 0) &&
                ((this.plungedSlitMask[shaftToGrind] & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(slt)) != 0))
            {
              grindString += slt.ToString() + ",";
              if (first)
              {
                first = false;
                grindString += shaftToGrind.ToString() + "," + res.Data[0].ToString() + "," + (slitCount[shaftToGrind] - 1).ToString() + ",";
              }
            }
          grindString = grindString.Substring(0, grindString.Length - 1);
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
            PageManager.getRequestCommand(typeof(Frm_twin400_blades)) + "." + Command.GRIND_SLIT +
            "(" + grindString + ")");
        }
        else
        {
          this.multipleGrindingRunning = false;
        }
      }
      else
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_twin400_blades)) + "." + Command.GRIND_SLIT +
          "(" + this.slitIndex.ToString() + "," + this.shaftIndex.ToString() + ")");
      }
    }

    private void LubeSlit_Click(object sender, EventArgs e)
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
			//RFAZZI PROVA
			//if (!DBManager.getDBManager().lineConfig.SlitterScorerConfig.PositioningOptions.Slits.EnableLubrication)
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
				PageManager.getRequestCommand(typeof(Frm_twin400_blades))+ "." + Command.LUBE_SLIT +
				"("+this.slitIndex.ToString()+","+this.shaftIndex.ToString()+")");
    }

    private void _SetMaintenance(bool val)
    {
      this.maintenance = val;
    }

    private void ResetSlitCounters_Click(object sender, EventArgs e)
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      if (this.maintenance)
      {
        SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
        string text = msg.getTranslatedMessage("§SYCC_131#\n§SYCC_60#" + (this.slitIndex + 1).ToString() + ", §SYCC_61# " + (this.shaftIndex + 1).ToString() + "?");
        Frm_Confirm confirm = new Frm_Confirm();
        if (confirm.ShowDialog(this, text) == DialogResult.OK)
        {
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
            PageManager.getRequestCommand(typeof(Frm_twin400_blades)) + "." + Command.RESET_SLIT_COUNTERS +
            "(" + this.slitIndex.ToString() + "," + this.shaftIndex.ToString() + ")");
        }
      }
      else 
        PageManager.getPageManager()._SetMessage("§SYCC_130");
    }

    private void ResetGrinder_Click(object sender, EventArgs e)
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      if (this.maintenance)
      {
        SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
        string text = msg.getTranslatedMessage("§SYCC_132#?");
        Frm_Confirm confirm = new Frm_Confirm();
        if (confirm.ShowDialog(this, text) == DialogResult.OK)
        {
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
            PageManager.getRequestCommand(typeof(Frm_twin400_blades)) + "." + ((Control)sender).Name +
            "()");
        }
      }
      else
        PageManager.getPageManager()._SetMessage("§SYCC_130");
    }

    private void SetMultipleGrinding()
    {
      this.GrindSlit.BackColor = (this.multipleGrindingRunning ? Color.Yellow : Color.WhiteSmoke);
      this.lab_Grinding.ForeColor = (this.multipleGrindingRunning ? Color.Blue : Color.LimeGreen);
      if (this.multipleGrindingRunning)
      {
        this.toolDetail.BlockMultiSelect(true);
        //agrassi_FI08-09-2010_1
        //PageManager.getPageManager()._SetMessage(DBManager.getDBManager().message.getTranslatedMessage("§SYCC_178"));
      }
      else
      {
				//RFAZZI_FI01/07/2008_1.Added control for refresh the slitter only when needed
				if ( this.toolDetail.getIsBlockMultiSelectActive() )
					this.toolDetail.ResetMultiSelection();
				this.toolDetail.BlockMultiSelect(false);
      }
      Application.DoEvents();
    }
  }
}

