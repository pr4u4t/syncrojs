using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

using SyncroC.Network;
using SyncroC.DataBase.LineConfig;
using SyncroC.Controls.Tools;

namespace SyncroC.Forms.Pages.Twin400.Maintenance
{
	public class Frm_twin400_maintenance : SyncroC.Forms.Pages.Twin400.Maintenance.Frm_twin400_maintenanceSubgroup
	{
    //AGrassi_FI06/12/2007_1
    //ABELLO_FI10-07-08_1 Rivisti i posizionamenti di Manutenzione
		private SyncroC.Controls.StateButton Maintenance;
		private SyncroC.Controls.Tools.MachineTools toolDetail;
		private MachineTools.ChangeShaftFunction changeFunction;
		private MachineTools.UpdateValuesFunction updateFunction;
		private int shaftIndex, slitIndex;
		private int[] selectedMask;
		private Label lbl_SelSlits1_YN;
		private Label lbl_SelectedEnum1;
		private Label lbl_SelectedEnum2;
		private Label lbl_SelSlits2_YN;
		private SyncroC.Controls.StateButton MaintenancePositioning1;
		private SyncroC.Controls.StateButton MaintenancePositioning2;
		private SyncroC.Controls.StateButton MaintenancePositioning3;
		private SyncroC.Controls.StateButton MaintenancePositioning4;
		private SyncroC.Controls.StateButton MaintenancePositioning5;
		private SyncroC.Controls.CheckBox chk_CleanToLeft;
		private SyncroC.Controls.StateButton MaintenancePositioning6;
		private SyncroC.Controls.StateButton MaintenancePositioning7;
		private SyncroC.Controls.StateButton MaintenancePositioning8;
		private Label lbl_MachineNotRunning_YN;
		private Label lbl_BoardNotPresent_YN;
		private SyncroC.Controls.StateButton SpinningForGrinding;
    private Label lbl_MachineNotPositioning_YN;
    private CheckBox chk_PLCComm;
    private Panel pnl_Debug;
    private CheckBox chk_PLCRun;
    private CheckBox chk_MachineInMaintenance;
    private CheckBox chk_MachineStatusStarted;
    private CheckBox chk_MachineStatusNotPositioning;
		private System.ComponentModel.IContainer components = null;

		public Frm_twin400_maintenance()
		{
			InitializeComponent();
			this.changeFunction = new MachineTools.ChangeShaftFunction(this.ChangeShaft);
			this.updateFunction = new MachineTools.UpdateValuesFunction(this.UpdateValues);
			this.toolDetail.Config(DBManager.getDBManager().lineConfig, SyncroC.Controls.Tools.MachineToolsType.OnlySlits,
															true, true, this.changeFunction, this.updateFunction, "", "");
			this.toolDetail.PositionOnlySlits();
			this.slitIndex = 0;
			this.shaftIndex = (int)Shaft.Slit1;
			this.selectedMask = new int[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
			this.UpdateValues();

      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
      {
        this.Maintenance.IsButton = false;
        this.MaintenancePositioning1.IsButton = false;
        this.MaintenancePositioning2.IsButton = false;
        this.MaintenancePositioning3.IsButton = false;
        this.MaintenancePositioning4.IsButton = false;
        this.MaintenancePositioning5.IsButton = false;
        this.MaintenancePositioning6.IsButton = false;
        this.MaintenancePositioning7.IsButton = false;
        this.MaintenancePositioning8.IsButton = false;
        this.SpinningForGrinding.IsButton = false;
      }
    }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		protected override void F10()
		{
		}


		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_maintenance));
      this.Maintenance = new SyncroC.Controls.StateButton();
      this.toolDetail = new SyncroC.Controls.Tools.MachineTools();
      this.lbl_SelSlits1_YN = new System.Windows.Forms.Label();
      this.lbl_SelectedEnum1 = new System.Windows.Forms.Label();
      this.lbl_SelectedEnum2 = new System.Windows.Forms.Label();
      this.lbl_SelSlits2_YN = new System.Windows.Forms.Label();
      this.MaintenancePositioning1 = new SyncroC.Controls.StateButton();
      this.MaintenancePositioning2 = new SyncroC.Controls.StateButton();
      this.MaintenancePositioning3 = new SyncroC.Controls.StateButton();
      this.MaintenancePositioning4 = new SyncroC.Controls.StateButton();
      this.MaintenancePositioning5 = new SyncroC.Controls.StateButton();
      this.chk_CleanToLeft = new SyncroC.Controls.CheckBox();
      this.MaintenancePositioning6 = new SyncroC.Controls.StateButton();
      this.MaintenancePositioning7 = new SyncroC.Controls.StateButton();
      this.MaintenancePositioning8 = new SyncroC.Controls.StateButton();
      this.lbl_MachineNotRunning_YN = new System.Windows.Forms.Label();
      this.lbl_BoardNotPresent_YN = new System.Windows.Forms.Label();
      this.SpinningForGrinding = new SyncroC.Controls.StateButton();
      this.lbl_MachineNotPositioning_YN = new System.Windows.Forms.Label();
      this.chk_PLCComm = new System.Windows.Forms.CheckBox();
      this.pnl_Debug = new System.Windows.Forms.Panel();
      this.chk_MachineStatusNotPositioning = new System.Windows.Forms.CheckBox();
      this.chk_MachineStatusStarted = new System.Windows.Forms.CheckBox();
      this.chk_MachineInMaintenance = new System.Windows.Forms.CheckBox();
      this.chk_PLCRun = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.pnl_Debug.SuspendLayout();
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
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // Maintenance
      // 
      this.Maintenance.Active = false;
      this.Maintenance.Alarmed = false;
      this.Maintenance.Auto = false;
      this.Maintenance.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Maintenance.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Maintenance.FillBackColorWithImage = false;
      this.Maintenance.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Maintenance.FramePresent = true;
      this.Maintenance.HighLightEnable = false;
      this.Maintenance.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Maintenance.ImagePassword = null;
      this.Maintenance.Inemergency = false;
      this.Maintenance.InLocal = false;
      this.Maintenance.IsButton = true;
      this.Maintenance.IsForceToInvisible = false;
      this.Maintenance.Location = new System.Drawing.Point(114, 210);
      this.Maintenance.Name = "Maintenance";
      this.Maintenance.OnLine = false;
      this.Maintenance.ShowImage = false;
      this.Maintenance.ShowText = true;
      this.Maintenance.Size = new System.Drawing.Size(165, 82);
      this.Maintenance.Switching = false;
      this.Maintenance.TabIndex = 176;
      this.Maintenance.Text = "Set Maintenance";
      this.Maintenance.TranslateText = true;
      this.Maintenance.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Maintenance.UserDefinedActiveImage")));
      this.Maintenance.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Maintenance.UserDefinedImage")));
      this.Maintenance.Warned = false;
      this.Maintenance.Click += new System.EventHandler(this.OnStateButtonPush);
      // 
      // toolDetail
      // 
      this.toolDetail.BackColor = System.Drawing.Color.WhiteSmoke;
      this.toolDetail.Debug = false;
      this.toolDetail.Location = new System.Drawing.Point(30, 509);
      this.toolDetail.Name = "toolDetail";
      this.toolDetail.Size = new System.Drawing.Size(1171, 208);
      this.toolDetail.TabIndex = 180;
      this.toolDetail.TranslateText = false;
      // 
      // lbl_SelSlits1_YN
      // 
      this.lbl_SelSlits1_YN.AutoSize = true;
      this.lbl_SelSlits1_YN.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_SelSlits1_YN.Location = new System.Drawing.Point(111, 465);
      this.lbl_SelSlits1_YN.Name = "lbl_SelSlits1_YN";
      this.lbl_SelSlits1_YN.Size = new System.Drawing.Size(181, 20);
      this.lbl_SelSlits1_YN.TabIndex = 181;
      this.lbl_SelSlits1_YN.Text = "Shaft 1, selected slits: ";
      // 
      // lbl_SelectedEnum1
      // 
      this.lbl_SelectedEnum1.AutoSize = true;
      this.lbl_SelectedEnum1.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_SelectedEnum1.Location = new System.Drawing.Point(281, 465);
      this.lbl_SelectedEnum1.Name = "lbl_SelectedEnum1";
      this.lbl_SelectedEnum1.Size = new System.Drawing.Size(74, 18);
      this.lbl_SelectedEnum1.TabIndex = 182;
      this.lbl_SelectedEnum1.Text = "1, 5, 6, 8";
      // 
      // lbl_SelectedEnum2
      // 
      this.lbl_SelectedEnum2.AutoSize = true;
      this.lbl_SelectedEnum2.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_SelectedEnum2.Location = new System.Drawing.Point(695, 465);
      this.lbl_SelectedEnum2.Name = "lbl_SelectedEnum2";
      this.lbl_SelectedEnum2.Size = new System.Drawing.Size(74, 18);
      this.lbl_SelectedEnum2.TabIndex = 184;
      this.lbl_SelectedEnum2.Text = "1, 5, 6, 8";
      // 
      // lbl_SelSlits2_YN
      // 
      this.lbl_SelSlits2_YN.AutoSize = true;
      this.lbl_SelSlits2_YN.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_SelSlits2_YN.Location = new System.Drawing.Point(525, 465);
      this.lbl_SelSlits2_YN.Name = "lbl_SelSlits2_YN";
      this.lbl_SelSlits2_YN.Size = new System.Drawing.Size(181, 20);
      this.lbl_SelSlits2_YN.TabIndex = 183;
      this.lbl_SelSlits2_YN.Text = "Shaft 2, selected slits: ";
      // 
      // MaintenancePositioning1
      // 
      this.MaintenancePositioning1.Active = false;
      this.MaintenancePositioning1.Alarmed = false;
      this.MaintenancePositioning1.Auto = false;
      this.MaintenancePositioning1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.MaintenancePositioning1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MaintenancePositioning1.FillBackColorWithImage = false;
      this.MaintenancePositioning1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MaintenancePositioning1.FramePresent = true;
      this.MaintenancePositioning1.HighLightEnable = false;
      this.MaintenancePositioning1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.MaintenancePositioning1.ImagePassword = null;
      this.MaintenancePositioning1.Inemergency = false;
      this.MaintenancePositioning1.InLocal = false;
      this.MaintenancePositioning1.IsButton = true;
      this.MaintenancePositioning1.IsForceToInvisible = false;
      this.MaintenancePositioning1.Location = new System.Drawing.Point(325, 209);
      this.MaintenancePositioning1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.MaintenancePositioning1.Name = "MaintenancePositioning1";
      this.MaintenancePositioning1.OnLine = false;
      this.MaintenancePositioning1.ShowImage = false;
      this.MaintenancePositioning1.ShowText = true;
      this.MaintenancePositioning1.Size = new System.Drawing.Size(165, 82);
      this.MaintenancePositioning1.Switching = false;
      this.MaintenancePositioning1.TabIndex = 195;
      this.MaintenancePositioning1.Text = "1 - Floor cleaning Left";
      this.MaintenancePositioning1.TranslateText = true;
      this.MaintenancePositioning1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning1.UserDefinedActiveImage")));
      this.MaintenancePositioning1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning1.UserDefinedImage")));
      this.MaintenancePositioning1.Warned = false;
      this.MaintenancePositioning1.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // MaintenancePositioning2
      // 
      this.MaintenancePositioning2.Active = false;
      this.MaintenancePositioning2.Alarmed = false;
      this.MaintenancePositioning2.Auto = false;
      this.MaintenancePositioning2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.MaintenancePositioning2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MaintenancePositioning2.FillBackColorWithImage = false;
      this.MaintenancePositioning2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MaintenancePositioning2.FramePresent = true;
      this.MaintenancePositioning2.HighLightEnable = false;
      this.MaintenancePositioning2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.MaintenancePositioning2.ImagePassword = null;
      this.MaintenancePositioning2.Inemergency = false;
      this.MaintenancePositioning2.InLocal = false;
      this.MaintenancePositioning2.IsButton = true;
      this.MaintenancePositioning2.IsForceToInvisible = false;
      this.MaintenancePositioning2.Location = new System.Drawing.Point(540, 210);
      this.MaintenancePositioning2.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.MaintenancePositioning2.Name = "MaintenancePositioning2";
      this.MaintenancePositioning2.OnLine = false;
      this.MaintenancePositioning2.ShowImage = false;
      this.MaintenancePositioning2.ShowText = true;
      this.MaintenancePositioning2.Size = new System.Drawing.Size(165, 82);
      this.MaintenancePositioning2.Switching = false;
      this.MaintenancePositioning2.TabIndex = 196;
      this.MaintenancePositioning2.Text = "2 - Floor cleaning Right";
      this.MaintenancePositioning2.TranslateText = true;
      this.MaintenancePositioning2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning2.UserDefinedActiveImage")));
      this.MaintenancePositioning2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning2.UserDefinedImage")));
      this.MaintenancePositioning2.Warned = false;
      this.MaintenancePositioning2.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // MaintenancePositioning3
      // 
      this.MaintenancePositioning3.Active = false;
      this.MaintenancePositioning3.Alarmed = false;
      this.MaintenancePositioning3.Auto = false;
      this.MaintenancePositioning3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.MaintenancePositioning3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MaintenancePositioning3.FillBackColorWithImage = false;
      this.MaintenancePositioning3.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MaintenancePositioning3.FramePresent = true;
      this.MaintenancePositioning3.HighLightEnable = false;
      this.MaintenancePositioning3.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.MaintenancePositioning3.ImagePassword = null;
      this.MaintenancePositioning3.Inemergency = false;
      this.MaintenancePositioning3.InLocal = false;
      this.MaintenancePositioning3.IsButton = true;
      this.MaintenancePositioning3.IsForceToInvisible = false;
      this.MaintenancePositioning3.Location = new System.Drawing.Point(748, 210);
      this.MaintenancePositioning3.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.MaintenancePositioning3.Name = "MaintenancePositioning3";
      this.MaintenancePositioning3.OnLine = false;
      this.MaintenancePositioning3.ShowImage = false;
      this.MaintenancePositioning3.ShowText = true;
      this.MaintenancePositioning3.Size = new System.Drawing.Size(165, 82);
      this.MaintenancePositioning3.Switching = false;
      this.MaintenancePositioning3.TabIndex = 197;
      this.MaintenancePositioning3.Text = "3 - Scores Disassembly";
      this.MaintenancePositioning3.TranslateText = true;
      this.MaintenancePositioning3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning3.UserDefinedActiveImage")));
      this.MaintenancePositioning3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning3.UserDefinedImage")));
      this.MaintenancePositioning3.Warned = false;
      this.MaintenancePositioning3.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // MaintenancePositioning4
      // 
      this.MaintenancePositioning4.Active = false;
      this.MaintenancePositioning4.Alarmed = false;
      this.MaintenancePositioning4.Auto = false;
      this.MaintenancePositioning4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.MaintenancePositioning4.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MaintenancePositioning4.FillBackColorWithImage = false;
      this.MaintenancePositioning4.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MaintenancePositioning4.FramePresent = true;
      this.MaintenancePositioning4.HighLightEnable = false;
      this.MaintenancePositioning4.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.MaintenancePositioning4.ImagePassword = null;
      this.MaintenancePositioning4.Inemergency = false;
      this.MaintenancePositioning4.InLocal = false;
      this.MaintenancePositioning4.IsButton = true;
      this.MaintenancePositioning4.IsForceToInvisible = false;
      this.MaintenancePositioning4.Location = new System.Drawing.Point(946, 210);
      this.MaintenancePositioning4.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.MaintenancePositioning4.Name = "MaintenancePositioning4";
      this.MaintenancePositioning4.OnLine = false;
      this.MaintenancePositioning4.ShowImage = false;
      this.MaintenancePositioning4.ShowText = true;
      this.MaintenancePositioning4.Size = new System.Drawing.Size(165, 82);
      this.MaintenancePositioning4.Switching = false;
      this.MaintenancePositioning4.TabIndex = 198;
      this.MaintenancePositioning4.Text = "4 - Grinder Disassembly";
      this.MaintenancePositioning4.TranslateText = true;
      this.MaintenancePositioning4.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning4.UserDefinedActiveImage")));
      this.MaintenancePositioning4.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning4.UserDefinedImage")));
      this.MaintenancePositioning4.Warned = false;
      this.MaintenancePositioning4.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // MaintenancePositioning5
      // 
      this.MaintenancePositioning5.Active = false;
      this.MaintenancePositioning5.Alarmed = false;
      this.MaintenancePositioning5.Auto = false;
      this.MaintenancePositioning5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.MaintenancePositioning5.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MaintenancePositioning5.FillBackColorWithImage = false;
      this.MaintenancePositioning5.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MaintenancePositioning5.FramePresent = true;
      this.MaintenancePositioning5.HighLightEnable = false;
      this.MaintenancePositioning5.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.MaintenancePositioning5.ImagePassword = null;
      this.MaintenancePositioning5.Inemergency = false;
      this.MaintenancePositioning5.InLocal = false;
      this.MaintenancePositioning5.IsButton = true;
      this.MaintenancePositioning5.IsForceToInvisible = false;
      this.MaintenancePositioning5.Location = new System.Drawing.Point(960, 456);
      this.MaintenancePositioning5.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.MaintenancePositioning5.Name = "MaintenancePositioning5";
      this.MaintenancePositioning5.OnLine = false;
      this.MaintenancePositioning5.ShowImage = false;
      this.MaintenancePositioning5.ShowText = true;
      this.MaintenancePositioning5.Size = new System.Drawing.Size(165, 82);
      this.MaintenancePositioning5.Switching = false;
      this.MaintenancePositioning5.TabIndex = 199;
      this.MaintenancePositioning5.Text = "5 - Grind New Blades";
      this.MaintenancePositioning5.TranslateText = true;
      this.MaintenancePositioning5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning5.UserDefinedActiveImage")));
      this.MaintenancePositioning5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning5.UserDefinedImage")));
      this.MaintenancePositioning5.Visible = false;
      this.MaintenancePositioning5.Warned = false;
      this.MaintenancePositioning5.Click += new System.EventHandler(this.OnButtonPushCheckSelectedSlits);
      // 
      // chk_CleanToLeft
      // 
      this.chk_CleanToLeft.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_CleanToLeft.Checked = true;
      this.chk_CleanToLeft.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_CleanToLeft.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_CleanToLeft.ImagePassword = null;
      this.chk_CleanToLeft.Location = new System.Drawing.Point(132, 408);
      this.chk_CleanToLeft.Name = "chk_CleanToLeft";
      this.chk_CleanToLeft.ReadOnly = false;
      this.chk_CleanToLeft.Size = new System.Drawing.Size(113, 42);
      this.chk_CleanToLeft.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_CleanToLeft.TabIndex = 200;
      this.chk_CleanToLeft.Text = "Clean to Left";
      this.chk_CleanToLeft.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_CleanToLeft.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_CleanToLeft.TranslateText = true;
      this.chk_CleanToLeft.Visible = false;
      // 
      // MaintenancePositioning6
      // 
      this.MaintenancePositioning6.Active = false;
      this.MaintenancePositioning6.Alarmed = false;
      this.MaintenancePositioning6.Auto = false;
      this.MaintenancePositioning6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.MaintenancePositioning6.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MaintenancePositioning6.FillBackColorWithImage = false;
      this.MaintenancePositioning6.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MaintenancePositioning6.FramePresent = true;
      this.MaintenancePositioning6.HighLightEnable = false;
      this.MaintenancePositioning6.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.MaintenancePositioning6.ImagePassword = null;
      this.MaintenancePositioning6.Inemergency = false;
      this.MaintenancePositioning6.InLocal = false;
      this.MaintenancePositioning6.IsButton = true;
      this.MaintenancePositioning6.IsForceToInvisible = false;
      this.MaintenancePositioning6.Location = new System.Drawing.Point(325, 325);
      this.MaintenancePositioning6.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.MaintenancePositioning6.Name = "MaintenancePositioning6";
      this.MaintenancePositioning6.OnLine = false;
      this.MaintenancePositioning6.ShowImage = false;
      this.MaintenancePositioning6.ShowText = true;
      this.MaintenancePositioning6.Size = new System.Drawing.Size(165, 82);
      this.MaintenancePositioning6.Switching = false;
      this.MaintenancePositioning6.TabIndex = 201;
      this.MaintenancePositioning6.Text = "6 - Grind Blades";
      this.MaintenancePositioning6.TranslateText = true;
      this.MaintenancePositioning6.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning6.UserDefinedActiveImage")));
      this.MaintenancePositioning6.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning6.UserDefinedImage")));
      this.MaintenancePositioning6.Warned = false;
      this.MaintenancePositioning6.Click += new System.EventHandler(this.OnButtonPushCheckSelectedSlits);
      // 
      // MaintenancePositioning7
      // 
      this.MaintenancePositioning7.Active = false;
      this.MaintenancePositioning7.Alarmed = false;
      this.MaintenancePositioning7.Auto = false;
      this.MaintenancePositioning7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.MaintenancePositioning7.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MaintenancePositioning7.FillBackColorWithImage = false;
      this.MaintenancePositioning7.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MaintenancePositioning7.FramePresent = true;
      this.MaintenancePositioning7.HighLightEnable = false;
      this.MaintenancePositioning7.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.MaintenancePositioning7.ImagePassword = null;
      this.MaintenancePositioning7.Inemergency = false;
      this.MaintenancePositioning7.InLocal = false;
      this.MaintenancePositioning7.IsButton = true;
      this.MaintenancePositioning7.IsForceToInvisible = false;
      this.MaintenancePositioning7.Location = new System.Drawing.Point(540, 325);
      this.MaintenancePositioning7.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.MaintenancePositioning7.Name = "MaintenancePositioning7";
      this.MaintenancePositioning7.OnLine = false;
      this.MaintenancePositioning7.ShowImage = false;
      this.MaintenancePositioning7.ShowText = true;
      this.MaintenancePositioning7.Size = new System.Drawing.Size(165, 82);
      this.MaintenancePositioning7.Switching = false;
      this.MaintenancePositioning7.TabIndex = 202;
      this.MaintenancePositioning7.Text = "7 - Blades Maintenance Shaft 1";
      this.MaintenancePositioning7.TranslateText = true;
      this.MaintenancePositioning7.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning7.UserDefinedActiveImage")));
      this.MaintenancePositioning7.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning7.UserDefinedImage")));
      this.MaintenancePositioning7.Warned = false;
      this.MaintenancePositioning7.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // MaintenancePositioning8
      // 
      this.MaintenancePositioning8.Active = false;
      this.MaintenancePositioning8.Alarmed = false;
      this.MaintenancePositioning8.Auto = false;
      this.MaintenancePositioning8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.MaintenancePositioning8.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MaintenancePositioning8.FillBackColorWithImage = false;
      this.MaintenancePositioning8.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MaintenancePositioning8.FramePresent = true;
      this.MaintenancePositioning8.HighLightEnable = false;
      this.MaintenancePositioning8.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.MaintenancePositioning8.ImagePassword = null;
      this.MaintenancePositioning8.Inemergency = false;
      this.MaintenancePositioning8.InLocal = false;
      this.MaintenancePositioning8.IsButton = true;
      this.MaintenancePositioning8.IsForceToInvisible = false;
      this.MaintenancePositioning8.Location = new System.Drawing.Point(748, 325);
      this.MaintenancePositioning8.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.MaintenancePositioning8.Name = "MaintenancePositioning8";
      this.MaintenancePositioning8.OnLine = false;
      this.MaintenancePositioning8.ShowImage = false;
      this.MaintenancePositioning8.ShowText = true;
      this.MaintenancePositioning8.Size = new System.Drawing.Size(165, 82);
      this.MaintenancePositioning8.Switching = false;
      this.MaintenancePositioning8.TabIndex = 203;
      this.MaintenancePositioning8.Text = "8 - Blades Maintenance Shaft 2";
      this.MaintenancePositioning8.TranslateText = true;
      this.MaintenancePositioning8.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning8.UserDefinedActiveImage")));
      this.MaintenancePositioning8.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("MaintenancePositioning8.UserDefinedImage")));
      this.MaintenancePositioning8.Warned = false;
      this.MaintenancePositioning8.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // lbl_MachineNotRunning_YN
      // 
      this.lbl_MachineNotRunning_YN.BackColor = System.Drawing.Color.LightGreen;
      this.lbl_MachineNotRunning_YN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_MachineNotRunning_YN.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_MachineNotRunning_YN.Location = new System.Drawing.Point(116, 324);
      this.lbl_MachineNotRunning_YN.Name = "lbl_MachineNotRunning_YN";
      this.lbl_MachineNotRunning_YN.Size = new System.Drawing.Size(163, 26);
      this.lbl_MachineNotRunning_YN.TabIndex = 204;
      this.lbl_MachineNotRunning_YN.Text = "Machine not running";
      this.lbl_MachineNotRunning_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_BoardNotPresent_YN
      // 
      this.lbl_BoardNotPresent_YN.BackColor = System.Drawing.Color.LightGreen;
      this.lbl_BoardNotPresent_YN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_BoardNotPresent_YN.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_BoardNotPresent_YN.Location = new System.Drawing.Point(116, 353);
      this.lbl_BoardNotPresent_YN.Name = "lbl_BoardNotPresent_YN";
      this.lbl_BoardNotPresent_YN.Size = new System.Drawing.Size(163, 26);
      this.lbl_BoardNotPresent_YN.TabIndex = 205;
      this.lbl_BoardNotPresent_YN.Text = "Board not present";
      this.lbl_BoardNotPresent_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // SpinningForGrinding
      // 
      this.SpinningForGrinding.Active = false;
      this.SpinningForGrinding.Alarmed = false;
      this.SpinningForGrinding.Auto = false;
      this.SpinningForGrinding.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SpinningForGrinding.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SpinningForGrinding.FillBackColorWithImage = false;
      this.SpinningForGrinding.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SpinningForGrinding.FramePresent = true;
      this.SpinningForGrinding.HighLightEnable = false;
      this.SpinningForGrinding.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SpinningForGrinding.ImagePassword = null;
      this.SpinningForGrinding.Inemergency = false;
      this.SpinningForGrinding.InLocal = false;
      this.SpinningForGrinding.IsButton = true;
      this.SpinningForGrinding.IsForceToInvisible = false;
      this.SpinningForGrinding.Location = new System.Drawing.Point(947, 325);
      this.SpinningForGrinding.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.SpinningForGrinding.Name = "SpinningForGrinding";
      this.SpinningForGrinding.OnLine = false;
      this.SpinningForGrinding.ShowImage = false;
      this.SpinningForGrinding.ShowText = true;
      this.SpinningForGrinding.Size = new System.Drawing.Size(166, 82);
      this.SpinningForGrinding.Switching = false;
      this.SpinningForGrinding.TabIndex = 206;
      this.SpinningForGrinding.Text = "Spinning for Grinding";
      this.SpinningForGrinding.TranslateText = true;
      this.SpinningForGrinding.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SpinningForGrinding.UserDefinedActiveImage")));
      this.SpinningForGrinding.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SpinningForGrinding.UserDefinedImage")));
      this.SpinningForGrinding.Visible = false;
      this.SpinningForGrinding.Warned = false;
      this.SpinningForGrinding.Click += new System.EventHandler(this.OnStateButtonPush);
      // 
      // lbl_MachineNotPositioning_YN
      // 
      this.lbl_MachineNotPositioning_YN.BackColor = System.Drawing.Color.LightGreen;
      this.lbl_MachineNotPositioning_YN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_MachineNotPositioning_YN.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_MachineNotPositioning_YN.Location = new System.Drawing.Point(116, 382);
      this.lbl_MachineNotPositioning_YN.Name = "lbl_MachineNotPositioning_YN";
      this.lbl_MachineNotPositioning_YN.Size = new System.Drawing.Size(163, 26);
      this.lbl_MachineNotPositioning_YN.TabIndex = 207;
      this.lbl_MachineNotPositioning_YN.Text = "Machine not positioning";
      this.lbl_MachineNotPositioning_YN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // chk_PLCComm
      // 
      this.chk_PLCComm.Location = new System.Drawing.Point(3, 6);
      this.chk_PLCComm.Name = "chk_PLCComm";
      this.chk_PLCComm.Size = new System.Drawing.Size(78, 24);
      this.chk_PLCComm.TabIndex = 208;
      this.chk_PLCComm.Text = "PLCComm";
      this.chk_PLCComm.UseVisualStyleBackColor = true;
      // 
      // pnl_Debug
      // 
      this.pnl_Debug.Controls.Add(this.chk_MachineStatusNotPositioning);
      this.pnl_Debug.Controls.Add(this.chk_MachineStatusStarted);
      this.pnl_Debug.Controls.Add(this.chk_MachineInMaintenance);
      this.pnl_Debug.Controls.Add(this.chk_PLCRun);
      this.pnl_Debug.Controls.Add(this.chk_PLCComm);
      this.pnl_Debug.Location = new System.Drawing.Point(131, 170);
      this.pnl_Debug.Name = "pnl_Debug";
      this.pnl_Debug.Size = new System.Drawing.Size(731, 36);
      this.pnl_Debug.TabIndex = 209;
      this.pnl_Debug.Visible = false;
      // 
      // chk_MachineStatusNotPositioning
      // 
      this.chk_MachineStatusNotPositioning.Location = new System.Drawing.Point(454, 6);
      this.chk_MachineStatusNotPositioning.Name = "chk_MachineStatusNotPositioning";
      this.chk_MachineStatusNotPositioning.Size = new System.Drawing.Size(172, 24);
      this.chk_MachineStatusNotPositioning.TabIndex = 212;
      this.chk_MachineStatusNotPositioning.Text = "MachineStatusNotPositioning";
      this.chk_MachineStatusNotPositioning.UseVisualStyleBackColor = true;
      // 
      // chk_MachineStatusStarted
      // 
      this.chk_MachineStatusStarted.Location = new System.Drawing.Point(315, 6);
      this.chk_MachineStatusStarted.Name = "chk_MachineStatusStarted";
      this.chk_MachineStatusStarted.Size = new System.Drawing.Size(133, 24);
      this.chk_MachineStatusStarted.TabIndex = 211;
      this.chk_MachineStatusStarted.Text = "MachineStatusStarted";
      this.chk_MachineStatusStarted.UseVisualStyleBackColor = true;
      // 
      // chk_MachineInMaintenance
      // 
      this.chk_MachineInMaintenance.Location = new System.Drawing.Point(171, 6);
      this.chk_MachineInMaintenance.Name = "chk_MachineInMaintenance";
      this.chk_MachineInMaintenance.Size = new System.Drawing.Size(138, 24);
      this.chk_MachineInMaintenance.TabIndex = 210;
      this.chk_MachineInMaintenance.Text = "MachineInMaintenance";
      this.chk_MachineInMaintenance.UseVisualStyleBackColor = true;
      // 
      // chk_PLCRun
      // 
      this.chk_PLCRun.Location = new System.Drawing.Point(87, 6);
      this.chk_PLCRun.Name = "chk_PLCRun";
      this.chk_PLCRun.Size = new System.Drawing.Size(78, 24);
      this.chk_PLCRun.TabIndex = 209;
      this.chk_PLCRun.Text = "PLCRun";
      this.chk_PLCRun.UseVisualStyleBackColor = true;
      // 
      // Frm_twin400_maintenance
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.pnl_Debug);
      this.Controls.Add(this.lbl_MachineNotPositioning_YN);
      this.Controls.Add(this.SpinningForGrinding);
      this.Controls.Add(this.lbl_BoardNotPresent_YN);
      this.Controls.Add(this.lbl_MachineNotRunning_YN);
      this.Controls.Add(this.MaintenancePositioning8);
      this.Controls.Add(this.MaintenancePositioning7);
      this.Controls.Add(this.MaintenancePositioning6);
      this.Controls.Add(this.chk_CleanToLeft);
      this.Controls.Add(this.MaintenancePositioning5);
      this.Controls.Add(this.MaintenancePositioning4);
      this.Controls.Add(this.MaintenancePositioning3);
      this.Controls.Add(this.MaintenancePositioning2);
      this.Controls.Add(this.MaintenancePositioning1);
      this.Controls.Add(this.lbl_SelectedEnum2);
      this.Controls.Add(this.lbl_SelSlits2_YN);
      this.Controls.Add(this.lbl_SelectedEnum1);
      this.Controls.Add(this.lbl_SelSlits1_YN);
      this.Controls.Add(this.toolDetail);
      this.Controls.Add(this.Maintenance);
      this.Name = "Frm_twin400_maintenance";
      this.Tag = "";
      this.Text = "Frm_twin400_maintenance";
      this.Controls.SetChildIndex(this.Maintenance, 0);
      this.Controls.SetChildIndex(this.toolDetail, 0);
      this.Controls.SetChildIndex(this.lbl_SelSlits1_YN, 0);
      this.Controls.SetChildIndex(this.lbl_SelectedEnum1, 0);
      this.Controls.SetChildIndex(this.lbl_SelSlits2_YN, 0);
      this.Controls.SetChildIndex(this.lbl_SelectedEnum2, 0);
      this.Controls.SetChildIndex(this.MaintenancePositioning1, 0);
      this.Controls.SetChildIndex(this.MaintenancePositioning2, 0);
      this.Controls.SetChildIndex(this.MaintenancePositioning3, 0);
      this.Controls.SetChildIndex(this.MaintenancePositioning4, 0);
      this.Controls.SetChildIndex(this.MaintenancePositioning5, 0);
      this.Controls.SetChildIndex(this.chk_CleanToLeft, 0);
      this.Controls.SetChildIndex(this.MaintenancePositioning6, 0);
      this.Controls.SetChildIndex(this.MaintenancePositioning7, 0);
      this.Controls.SetChildIndex(this.MaintenancePositioning8, 0);
      this.Controls.SetChildIndex(this.lbl_MachineNotRunning_YN, 0);
      this.Controls.SetChildIndex(this.lbl_BoardNotPresent_YN, 0);
      this.Controls.SetChildIndex(this.SpinningForGrinding, 0);
      this.Controls.SetChildIndex(this.lbl_MachineNotPositioning_YN, 0);
      this.Controls.SetChildIndex(this.pnl_Debug, 0);
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
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.pnl_Debug.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// Handles event raised by StateButton associated with commands with duration that are in the page, 
		/// build the proper command and send it to the RTC_MANAGER.
		/// </summary>
		private void OnStateButtonPush(object sender, System.EventArgs e)
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_twin400_maintenance)) + "." + ((Control)sender).Name + "()");
		}

		private void OnButtonPush(object sender, System.EventArgs e)
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_twin400_maintenance)) + "." + ((Control)sender).Name + "(" +
				this.selectedMask[(int)Shaft.Slit1].ToString() + "," + this.selectedMask[(int)Shaft.Slit2].ToString() + "," +
				(this.chk_CleanToLeft.Checked ? "1" : "0") + ")");
		}

		private void OnButtonPushCheckSelectedSlits(object sender, System.EventArgs e)
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      if ((this.selectedMask[(int)Shaft.Slit1] == 0) && (this.selectedMask[(int)Shaft.Slit2] == 0))
			{
				PageManager.getPageManager()._SetMessage("§SYCC_120", true);
				return;
			}
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				PageManager.getRequestCommand(typeof(Frm_twin400_maintenance)) + "." + ((Control)sender).Name + "(" +
				this.selectedMask[(int)Shaft.Slit1].ToString() + "," + this.selectedMask[(int)Shaft.Slit2].ToString() + "," +
				(this.chk_CleanToLeft.Checked ? "1" : "0") + ")");
		}
		protected override void LeftArrow()
		{
			this.slitIndex = this.toolDetail.ScrollLeft(ref this.shaftIndex);
		}

		protected override void RightArrow()
		{
			this.slitIndex = this.toolDetail.ScrollRight(ref this.shaftIndex);
		}

		protected override void UpArrow()
		{
			this.shaftIndex = this.toolDetail.ChangeShaft(true);
		}

		protected override void DownArrow()
		{
			this.shaftIndex = this.toolDetail.ChangeShaft(true);
		}

		protected override void Return()
		{
			this.toolDetail.ToggleSelection(this.shaftIndex, this.slitIndex);
		}

		private void ChangeShaft(int slitIndex, int shaftIndex)
		{
			if (this.shaftIndex != shaftIndex)
				this.shaftIndex = this.toolDetail.ChangeShaft(false);
			this.slitIndex = slitIndex;
		}

		private void UpdateValues()
		{
			string[] selEnum = new string[SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS];
			this.lbl_SelectedEnum1.Left = this.lbl_SelSlits1_YN.Left + this.lbl_SelSlits1_YN.Width + 5;
			this.lbl_SelectedEnum2.Left = this.lbl_SelSlits2_YN.Left + this.lbl_SelSlits2_YN.Width + 5;
			for (int sh = 0; sh < SyncroC.DataBase.LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
			{
				this.selectedMask[sh] = this.toolDetail.GetSelectedSlitMask(sh);
				selEnum[sh] = "";
				for (int slt = 0; slt < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
					if ((this.selectedMask[sh] & SyncroC.DataBase.LineConfig.SlitterScorer.BIT(slt)) != 0)
						selEnum[sh] += ((selEnum[sh] == "") ? "" : ", ") + (slt + 1).ToString();
			}
			this.lbl_SelectedEnum1.Text = selEnum[(int)Shaft.Slit1];
			this.lbl_SelectedEnum2.Text = selEnum[(int)Shaft.Slit2];
		}

		protected override void ControlD()
		{
			this.chk_CleanToLeft.Visible = !this.chk_CleanToLeft.Visible;
      this.pnl_Debug.Visible = !this.pnl_Debug.Visible;
		}
	}
}

