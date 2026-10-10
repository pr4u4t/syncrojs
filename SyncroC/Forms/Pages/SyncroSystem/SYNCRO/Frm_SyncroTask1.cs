using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase.SyncroConfig;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SyncroSystem.SYNCRO
{
	public class Frm_SyncroTask1 : SyncroC.Forms.Pages.SyncroSystem.SYNCRO.Frm_SyncroSubGroup
	{
		private PropertyGrid CfgDBMSGrid;
		private PropertyGrid CfgGeneralGrid;
		private PropertyGrid CfgStarterGrid;
		private PropertyGrid CfgRTCGrid;
		private PropertyGrid CfgDIOGrid;
		private PropertyGrid CfgAIOGrid;
		private PropertyGrid CfgCTCGrid;
		private PropertyGrid CfgFCTGrid;
		private PropertyGrid CfgWECGrid;
		private PropertyGrid CfgMTHDuckerGrid;
		//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
		private PropertyGrid CfgMTHSeemiGrid;

		private PropertyGrid CfgALARMSGrid;
		private PropertyGrid CfgEVENTLOGGrid;
		private PropertyGrid CfgDATABRIDGEGrid;
		private PropertyGrid CfgSpeedGrid;
		private PropertyGrid CfgSpeedASRGrid;


		private TabControl tab_Settings;
		private TabPage tab_General;
		private TabPage tab_DBMS;
		private TabPage tab_Starter;
		private TabPage tab_RTC;
		private TabPage tabDIO;
		private TabPage tab_AIO;
		private TabPage tab_CTC;
		private TabPage tab_FCT;
		private TabPage tab_WEC;
		private TabPage tabMTHDucker;
		//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
		private TabPage tabMTHSeemi;
		private TabPage tab_ALARMS;
		private TabPage tabEVENTLOG;
		private TabPage tab_DATABRIDGE;
		private TabPage tab_Speed;
		private TabPage tab_SpeedASR;

		private System.ComponentModel.IContainer components = null;

		private SyncroConfig cfg;

		private bool modify = false;
		private bool modDBMS = false;
		private bool modGENERAL = false;
		private bool modStarter = false;
		private bool modRTC = false;
		private bool modDIO = false;
		private bool modAIO = false;
		private bool modCTC = false;
		private bool modFCT = false;
		private bool modWEC = false;
		//RFazzi_FI17/01/2008_1.Added task configurations on graphics
		private bool modMTHDucker = false;
		//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
		private bool modMTHSeemi = false;
		private bool modALARMS = false;
		private bool modEVENTLOG = false;
		private bool modDATABRIDGE = false;
		private bool modSPEED = false;
		private bool modSPEED_ASR = false;

		public Frm_SyncroTask1()
		{
			InitializeComponent();

			cfg = DBManager.getDBManager().syncroConfig;
		}

		public override void refreshData()
		{
			SyncroConfig cfg = DBManager.getDBManager().syncroConfig;
			
			this.CfgDBMSGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.DBMSSettings)cfg.DbmsSetting.CreateCopy();
			this.CfgGeneralGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.GeneralSettings)cfg.GeneralSetting.CreateCopy();
			this.CfgStarterGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.StarterSettings)cfg.StarterSetting.CreateCopy();
			this.CfgRTCGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.RTCSettings)cfg.RtcSetting.CreateCopy();
			this.CfgDIOGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.DIOSettings)cfg.DioSetting.CreateCopy();
			this.CfgAIOGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.AIOSettings)cfg.AioSetting.CreateCopy();
			this.CfgCTCGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.CTCSettings)cfg.CtcSetting.CreateCopy();
			this.CfgFCTGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.FCTSettings)cfg.FctSetting.CreateCopy();
			this.CfgWECGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.WECSettings)cfg.WecSetting.CreateCopy();
			//RFazzi_FI17/01/2008_1.Added task configurations on graphics
			this.CfgMTHDuckerGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.MTHDuckerSettings)cfg.MTHDuckerSetting.CreateCopy();
			//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
			this.CfgMTHSeemiGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.MTHSeemiSettings)cfg.MTHSeemiSetting.CreateCopy();
			this.CfgALARMSGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.ALARMSSettings)cfg.AlarmsSetting.CreateCopy();
			this.CfgEVENTLOGGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.EVENTLOGSettings)cfg.EventLogSetting.CreateCopy();
			this.CfgDATABRIDGEGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.DATABRIDGESettings)cfg.DataBridgeSetting.CreateCopy();
			this.CfgSpeedGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SPEEDSettings)cfg.SpeedSetting.CreateCopy();
			this.CfgSpeedASRGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SPEED_ASRSettings)cfg.SpeedASRSetting.CreateCopy();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SyncroTask1));
      this.CfgDBMSGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgGeneralGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Settings = new System.Windows.Forms.TabControl();
      this.tab_General = new System.Windows.Forms.TabPage();
      this.tab_DBMS = new System.Windows.Forms.TabPage();
      this.tab_Starter = new System.Windows.Forms.TabPage();
      this.CfgStarterGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_RTC = new System.Windows.Forms.TabPage();
      this.CfgRTCGrid = new System.Windows.Forms.PropertyGrid();
      this.tabDIO = new System.Windows.Forms.TabPage();
      this.CfgDIOGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_AIO = new System.Windows.Forms.TabPage();
      this.CfgAIOGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_CTC = new System.Windows.Forms.TabPage();
      this.CfgCTCGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_FCT = new System.Windows.Forms.TabPage();
      this.CfgFCTGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_WEC = new System.Windows.Forms.TabPage();
      this.CfgWECGrid = new System.Windows.Forms.PropertyGrid();
      this.tabMTHDucker = new System.Windows.Forms.TabPage();
      this.CfgMTHDuckerGrid = new System.Windows.Forms.PropertyGrid();
      this.tabMTHSeemi = new System.Windows.Forms.TabPage();
      this.CfgMTHSeemiGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_ALARMS = new System.Windows.Forms.TabPage();
      this.CfgALARMSGrid = new System.Windows.Forms.PropertyGrid();
      this.tabEVENTLOG = new System.Windows.Forms.TabPage();
      this.CfgEVENTLOGGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_DATABRIDGE = new System.Windows.Forms.TabPage();
      this.CfgDATABRIDGEGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Speed = new System.Windows.Forms.TabPage();
      this.CfgSpeedGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_SpeedASR = new System.Windows.Forms.TabPage();
      this.CfgSpeedASRGrid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_Settings.SuspendLayout();
      this.tab_General.SuspendLayout();
      this.tab_DBMS.SuspendLayout();
      this.tab_Starter.SuspendLayout();
      this.tab_RTC.SuspendLayout();
      this.tabDIO.SuspendLayout();
      this.tab_AIO.SuspendLayout();
      this.tab_CTC.SuspendLayout();
      this.tab_FCT.SuspendLayout();
      this.tab_WEC.SuspendLayout();
      this.tabMTHDucker.SuspendLayout();
      this.tabMTHSeemi.SuspendLayout();
      this.tab_ALARMS.SuspendLayout();
      this.tabEVENTLOG.SuspendLayout();
      this.tab_DATABRIDGE.SuspendLayout();
      this.tab_Speed.SuspendLayout();
      this.tab_SpeedASR.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_TabC
      // 
      this.tab_TabC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Text = "Modify";
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
      // CfgDBMSGrid
      // 
      this.CfgDBMSGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgDBMSGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgDBMSGrid.Location = new System.Drawing.Point(50, 50);
      this.CfgDBMSGrid.Name = "CfgDBMSGrid";
      this.CfgDBMSGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgDBMSGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgDBMSGrid.TabIndex = 116;
      this.CfgDBMSGrid.ToolbarVisible = false;
      this.CfgDBMSGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgDBMSGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgDBMSGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgGeneralGrid
      // 
      this.CfgGeneralGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgGeneralGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgGeneralGrid.Location = new System.Drawing.Point(50, 50);
      this.CfgGeneralGrid.Name = "CfgGeneralGrid";
      this.CfgGeneralGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgGeneralGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgGeneralGrid.TabIndex = 117;
      this.CfgGeneralGrid.ToolbarVisible = false;
      this.CfgGeneralGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgGeneralGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgGeneralGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Settings
      // 
      this.tab_Settings.Controls.Add(this.tab_General);
      this.tab_Settings.Controls.Add(this.tab_DBMS);
      this.tab_Settings.Controls.Add(this.tab_Starter);
      this.tab_Settings.Controls.Add(this.tab_RTC);
      this.tab_Settings.Controls.Add(this.tabDIO);
      this.tab_Settings.Controls.Add(this.tab_AIO);
      this.tab_Settings.Controls.Add(this.tab_CTC);
      this.tab_Settings.Controls.Add(this.tab_FCT);
      this.tab_Settings.Controls.Add(this.tab_WEC);
      this.tab_Settings.Controls.Add(this.tabMTHDucker);
      this.tab_Settings.Controls.Add(this.tabMTHSeemi);
      this.tab_Settings.Controls.Add(this.tab_ALARMS);
      this.tab_Settings.Controls.Add(this.tabEVENTLOG);
      this.tab_Settings.Controls.Add(this.tab_DATABRIDGE);
      this.tab_Settings.Controls.Add(this.tab_Speed);
      this.tab_Settings.Controls.Add(this.tab_SpeedASR);
      this.tab_Settings.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Settings.Location = new System.Drawing.Point(30, 193);
      this.tab_Settings.Name = "tab_Settings";
      this.tab_Settings.SelectedIndex = 0;
      this.tab_Settings.Size = new System.Drawing.Size(1171, 519);
      this.tab_Settings.TabIndex = 118;
      // 
      // tab_General
      // 
      this.tab_General.Controls.Add(this.CfgGeneralGrid);
      this.tab_General.Location = new System.Drawing.Point(4, 25);
      this.tab_General.Name = "tab_General";
      this.tab_General.Padding = new System.Windows.Forms.Padding(3);
      this.tab_General.Size = new System.Drawing.Size(1163, 490);
      this.tab_General.TabIndex = 0;
      this.tab_General.Text = "General";
      this.tab_General.UseVisualStyleBackColor = true;
      // 
      // tab_DBMS
      // 
      this.tab_DBMS.Controls.Add(this.CfgDBMSGrid);
      this.tab_DBMS.Location = new System.Drawing.Point(4, 25);
      this.tab_DBMS.Name = "tab_DBMS";
      this.tab_DBMS.Padding = new System.Windows.Forms.Padding(3);
      this.tab_DBMS.Size = new System.Drawing.Size(1163, 490);
      this.tab_DBMS.TabIndex = 1;
      this.tab_DBMS.Text = "DBMS";
      this.tab_DBMS.UseVisualStyleBackColor = true;
      // 
      // tab_Starter
      // 
      this.tab_Starter.Controls.Add(this.CfgStarterGrid);
      this.tab_Starter.Location = new System.Drawing.Point(4, 25);
      this.tab_Starter.Name = "tab_Starter";
      this.tab_Starter.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Starter.Size = new System.Drawing.Size(1102, 490);
      this.tab_Starter.TabIndex = 2;
      this.tab_Starter.Text = "Starter";
      this.tab_Starter.UseVisualStyleBackColor = true;
      // 
      // CfgStarterGrid
      // 
      this.CfgStarterGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStarterGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStarterGrid.Location = new System.Drawing.Point(50, 50);
      this.CfgStarterGrid.Name = "CfgStarterGrid";
      this.CfgStarterGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgStarterGrid.TabIndex = 117;
      this.CfgStarterGrid.ToolbarVisible = false;
      this.CfgStarterGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStarterGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStarterGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_RTC
      // 
      this.tab_RTC.Controls.Add(this.CfgRTCGrid);
      this.tab_RTC.Location = new System.Drawing.Point(4, 25);
      this.tab_RTC.Name = "tab_RTC";
      this.tab_RTC.Padding = new System.Windows.Forms.Padding(3);
      this.tab_RTC.Size = new System.Drawing.Size(1102, 490);
      this.tab_RTC.TabIndex = 3;
      this.tab_RTC.Text = "RTC";
      this.tab_RTC.UseVisualStyleBackColor = true;
      // 
      // CfgRTCGrid
      // 
      this.CfgRTCGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgRTCGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgRTCGrid.Location = new System.Drawing.Point(50, 50);
      this.CfgRTCGrid.Name = "CfgRTCGrid";
      this.CfgRTCGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgRTCGrid.TabIndex = 118;
      this.CfgRTCGrid.ToolbarVisible = false;
      this.CfgRTCGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgRTCGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgRTCGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tabDIO
      // 
      this.tabDIO.Controls.Add(this.CfgDIOGrid);
      this.tabDIO.Location = new System.Drawing.Point(4, 25);
      this.tabDIO.Name = "tabDIO";
      this.tabDIO.Padding = new System.Windows.Forms.Padding(3);
      this.tabDIO.Size = new System.Drawing.Size(1102, 490);
      this.tabDIO.TabIndex = 4;
      this.tabDIO.Text = "DIO";
      this.tabDIO.UseVisualStyleBackColor = true;
      // 
      // CfgDIOGrid
      // 
      this.CfgDIOGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgDIOGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgDIOGrid.Location = new System.Drawing.Point(50, 50);
      this.CfgDIOGrid.Name = "CfgDIOGrid";
      this.CfgDIOGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgDIOGrid.TabIndex = 118;
      this.CfgDIOGrid.ToolbarVisible = false;
      this.CfgDIOGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgDIOGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgDIOGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_AIO
      // 
      this.tab_AIO.Controls.Add(this.CfgAIOGrid);
      this.tab_AIO.Location = new System.Drawing.Point(4, 25);
      this.tab_AIO.Name = "tab_AIO";
      this.tab_AIO.Padding = new System.Windows.Forms.Padding(3);
      this.tab_AIO.Size = new System.Drawing.Size(1102, 490);
      this.tab_AIO.TabIndex = 5;
      this.tab_AIO.Text = "AIO";
      this.tab_AIO.UseVisualStyleBackColor = true;
      // 
      // CfgAIOGrid
      // 
      this.CfgAIOGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgAIOGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgAIOGrid.Location = new System.Drawing.Point(50, 50);
      this.CfgAIOGrid.Name = "CfgAIOGrid";
      this.CfgAIOGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgAIOGrid.TabIndex = 118;
      this.CfgAIOGrid.ToolbarVisible = false;
      this.CfgAIOGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgAIOGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgAIOGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_CTC
      // 
      this.tab_CTC.Controls.Add(this.CfgCTCGrid);
      this.tab_CTC.Location = new System.Drawing.Point(4, 25);
      this.tab_CTC.Name = "tab_CTC";
      this.tab_CTC.Padding = new System.Windows.Forms.Padding(3);
      this.tab_CTC.Size = new System.Drawing.Size(1102, 490);
      this.tab_CTC.TabIndex = 6;
      this.tab_CTC.Text = "CTC";
      this.tab_CTC.UseVisualStyleBackColor = true;
      // 
      // CfgCTCGrid
      // 
      this.CfgCTCGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgCTCGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgCTCGrid.Location = new System.Drawing.Point(50, 50);
      this.CfgCTCGrid.Name = "CfgCTCGrid";
      this.CfgCTCGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgCTCGrid.TabIndex = 118;
      this.CfgCTCGrid.ToolbarVisible = false;
      this.CfgCTCGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgCTCGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgCTCGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_FCT
      // 
      this.tab_FCT.Controls.Add(this.CfgFCTGrid);
      this.tab_FCT.Location = new System.Drawing.Point(4, 25);
      this.tab_FCT.Name = "tab_FCT";
      this.tab_FCT.Padding = new System.Windows.Forms.Padding(3);
      this.tab_FCT.Size = new System.Drawing.Size(1102, 490);
      this.tab_FCT.TabIndex = 7;
      this.tab_FCT.Text = "FCT";
      this.tab_FCT.UseVisualStyleBackColor = true;
      // 
      // CfgFCTGrid
      // 
      this.CfgFCTGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFCTGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFCTGrid.Location = new System.Drawing.Point(50, 50);
      this.CfgFCTGrid.Name = "CfgFCTGrid";
      this.CfgFCTGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgFCTGrid.TabIndex = 118;
      this.CfgFCTGrid.ToolbarVisible = false;
      this.CfgFCTGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgFCTGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgFCTGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_WEC
      // 
      this.tab_WEC.Controls.Add(this.CfgWECGrid);
      this.tab_WEC.Location = new System.Drawing.Point(4, 25);
      this.tab_WEC.Name = "tab_WEC";
      this.tab_WEC.Padding = new System.Windows.Forms.Padding(3);
      this.tab_WEC.Size = new System.Drawing.Size(1102, 490);
      this.tab_WEC.TabIndex = 8;
      this.tab_WEC.Text = "WEC";
      this.tab_WEC.UseVisualStyleBackColor = true;
      // 
      // CfgWECGrid
      // 
      this.CfgWECGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgWECGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgWECGrid.Location = new System.Drawing.Point(50, 50);
      this.CfgWECGrid.Name = "CfgWECGrid";
      this.CfgWECGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgWECGrid.TabIndex = 118;
      this.CfgWECGrid.ToolbarVisible = false;
      this.CfgWECGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgWECGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgWECGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tabMTHDucker
      // 
      this.tabMTHDucker.Controls.Add(this.CfgMTHDuckerGrid);
      this.tabMTHDucker.Location = new System.Drawing.Point(4, 25);
      this.tabMTHDucker.Name = "tabMTHDucker";
      this.tabMTHDucker.Padding = new System.Windows.Forms.Padding(3);
      this.tabMTHDucker.Size = new System.Drawing.Size(1102, 490);
      this.tabMTHDucker.TabIndex = 9;
      this.tabMTHDucker.Text = "MTHDucker";
      this.tabMTHDucker.UseVisualStyleBackColor = true;
      // 
      // CfgMTHDuckerGrid
      // 
      this.CfgMTHDuckerGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMTHDuckerGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMTHDuckerGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgMTHDuckerGrid.Name = "CfgMTHDuckerGrid";
      this.CfgMTHDuckerGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgMTHDuckerGrid.TabIndex = 119;
      this.CfgMTHDuckerGrid.ToolbarVisible = false;
      this.CfgMTHDuckerGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgMTHDuckerGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgMTHDuckerGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tabMTHSeemi
      // 
      this.tabMTHSeemi.Controls.Add(this.CfgMTHSeemiGrid);
      this.tabMTHSeemi.Location = new System.Drawing.Point(4, 25);
      this.tabMTHSeemi.Name = "tabMTHSeemi";
      this.tabMTHSeemi.Padding = new System.Windows.Forms.Padding(3);
      this.tabMTHSeemi.Size = new System.Drawing.Size(1102, 490);
      this.tabMTHSeemi.TabIndex = 9;
      this.tabMTHSeemi.Text = "MTHSeemi";
      this.tabMTHSeemi.UseVisualStyleBackColor = true;
      // 
      // CfgMTHSeemiGrid
      // 
      this.CfgMTHSeemiGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMTHSeemiGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMTHSeemiGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgMTHSeemiGrid.Name = "CfgMTHSeemiGrid";
      this.CfgMTHSeemiGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgMTHSeemiGrid.TabIndex = 119;
      this.CfgMTHSeemiGrid.ToolbarVisible = false;
      this.CfgMTHSeemiGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgMTHSeemiGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgMTHSeemiGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_ALARMS
      // 
      this.tab_ALARMS.Controls.Add(this.CfgALARMSGrid);
      this.tab_ALARMS.Location = new System.Drawing.Point(4, 25);
      this.tab_ALARMS.Name = "tab_ALARMS";
      this.tab_ALARMS.Padding = new System.Windows.Forms.Padding(3);
      this.tab_ALARMS.Size = new System.Drawing.Size(1102, 490);
      this.tab_ALARMS.TabIndex = 10;
      this.tab_ALARMS.Text = "ALARMS";
      this.tab_ALARMS.UseVisualStyleBackColor = true;
      // 
      // CfgALARMSGrid
      // 
      this.CfgALARMSGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgALARMSGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgALARMSGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgALARMSGrid.Name = "CfgALARMSGrid";
      this.CfgALARMSGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgALARMSGrid.TabIndex = 119;
      this.CfgALARMSGrid.ToolbarVisible = false;
      this.CfgALARMSGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgALARMSGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgALARMSGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tabEVENTLOG
      // 
      this.tabEVENTLOG.Controls.Add(this.CfgEVENTLOGGrid);
      this.tabEVENTLOG.Location = new System.Drawing.Point(4, 25);
      this.tabEVENTLOG.Name = "tabEVENTLOG";
      this.tabEVENTLOG.Padding = new System.Windows.Forms.Padding(3);
      this.tabEVENTLOG.Size = new System.Drawing.Size(1102, 490);
      this.tabEVENTLOG.TabIndex = 11;
      this.tabEVENTLOG.Text = "EVENTLOG";
      this.tabEVENTLOG.UseVisualStyleBackColor = true;
      // 
      // CfgEVENTLOGGrid
      // 
      this.CfgEVENTLOGGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgEVENTLOGGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgEVENTLOGGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgEVENTLOGGrid.Name = "CfgEVENTLOGGrid";
      this.CfgEVENTLOGGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgEVENTLOGGrid.TabIndex = 120;
      this.CfgEVENTLOGGrid.ToolbarVisible = false;
      this.CfgEVENTLOGGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgEVENTLOGGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgEVENTLOGGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_DATABRIDGE
      // 
      this.tab_DATABRIDGE.Controls.Add(this.CfgDATABRIDGEGrid);
      this.tab_DATABRIDGE.Location = new System.Drawing.Point(4, 25);
      this.tab_DATABRIDGE.Name = "tab_DATABRIDGE";
      this.tab_DATABRIDGE.Padding = new System.Windows.Forms.Padding(3);
      this.tab_DATABRIDGE.Size = new System.Drawing.Size(1102, 490);
      this.tab_DATABRIDGE.TabIndex = 12;
      this.tab_DATABRIDGE.Text = "DATABRIDGE";
      this.tab_DATABRIDGE.UseVisualStyleBackColor = true;
      // 
      // CfgDATABRIDGEGrid
      // 
      this.CfgDATABRIDGEGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgDATABRIDGEGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgDATABRIDGEGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgDATABRIDGEGrid.Name = "CfgDATABRIDGEGrid";
      this.CfgDATABRIDGEGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgDATABRIDGEGrid.TabIndex = 121;
      this.CfgDATABRIDGEGrid.ToolbarVisible = false;
      this.CfgDATABRIDGEGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgDATABRIDGEGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgDATABRIDGEGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Speed
      // 
      this.tab_Speed.Controls.Add(this.CfgSpeedGrid);
      this.tab_Speed.Location = new System.Drawing.Point(4, 25);
      this.tab_Speed.Name = "tab_Speed";
      this.tab_Speed.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Speed.Size = new System.Drawing.Size(1102, 490);
      this.tab_Speed.TabIndex = 13;
      this.tab_Speed.Text = "SPEED";
      this.tab_Speed.UseVisualStyleBackColor = true;
      // 
      // CfgSpeedGrid
      // 
      this.CfgSpeedGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSpeedGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSpeedGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgSpeedGrid.Name = "CfgSpeedGrid";
      this.CfgSpeedGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgSpeedGrid.TabIndex = 122;
      this.CfgSpeedGrid.ToolbarVisible = false;
      this.CfgSpeedGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgSpeedGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSpeedGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_SpeedASR
      // 
      this.tab_SpeedASR.Controls.Add(this.CfgSpeedASRGrid);
      this.tab_SpeedASR.Location = new System.Drawing.Point(4, 25);
      this.tab_SpeedASR.Name = "tab_SpeedASR";
      this.tab_SpeedASR.Padding = new System.Windows.Forms.Padding(3);
      this.tab_SpeedASR.Size = new System.Drawing.Size(1102, 490);
      this.tab_SpeedASR.TabIndex = 14;
      this.tab_SpeedASR.Text = "SPEED ASR";
      this.tab_SpeedASR.UseVisualStyleBackColor = true;
      // 
      // CfgSpeedASRGrid
      // 
      this.CfgSpeedASRGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSpeedASRGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSpeedASRGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgSpeedASRGrid.Name = "CfgSpeedASRGrid";
      this.CfgSpeedASRGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgSpeedASRGrid.TabIndex = 123;
      this.CfgSpeedASRGrid.ToolbarVisible = false;
      this.CfgSpeedASRGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgSpeedASRGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSpeedASRGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // Frm_SyncroTask1
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tab_Settings);
      this.DesignGridHorizontalLines = 5;
      this.DesignGridVerticalLines = 1;
      this.Name = "Frm_SyncroTask1";
      this.Load += new System.EventHandler(this.Frm_SyncroTask1_Load);
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
      this.Controls.SetChildIndex(this.tab_Settings, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.tab_Settings.ResumeLayout(false);
      this.tab_General.ResumeLayout(false);
      this.tab_DBMS.ResumeLayout(false);
      this.tab_Starter.ResumeLayout(false);
      this.tab_RTC.ResumeLayout(false);
      this.tabDIO.ResumeLayout(false);
      this.tab_AIO.ResumeLayout(false);
      this.tab_CTC.ResumeLayout(false);
      this.tab_FCT.ResumeLayout(false);
      this.tab_WEC.ResumeLayout(false);
      this.tabMTHDucker.ResumeLayout(false);
      this.tabMTHSeemi.ResumeLayout(false);
      this.tab_ALARMS.ResumeLayout(false);
      this.tabEVENTLOG.ResumeLayout(false);
      this.tab_DATABRIDGE.ResumeLayout(false);
      this.tab_Speed.ResumeLayout(false);
      this.tab_SpeedASR.ResumeLayout(false);
      this.ResumeLayout(false);

		}
		#endregion

		public override void ConfigPage(SyncroC.Utility.Settings.Configuration config)
		{
			if (!config.getOnServer())
			{
				lbl_CtrlF4_YY.Text = lbl_CtrlF5_YY.Text = string.Empty;
			}
		}

		protected override void CtrlF4()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				if (this.modify)
				{
					if (this.modGENERAL)
					{
						cfg.GeneralSetting = (GeneralSettings)this.CfgGeneralGrid.SelectedObject;
						cfg.GeneralSetting.Save();
					}
					if (this.modDBMS)
					{
						cfg.DbmsSetting = (DBMSSettings)this.CfgDBMSGrid.SelectedObject;
						cfg.DbmsSetting.Save();
					}
					if (this.modStarter)
					{
						cfg.StarterSetting = (StarterSettings)this.CfgStarterGrid.SelectedObject;
						cfg.StarterSetting.Save();
					}
					if (this.modRTC)
					{
						cfg.RtcSetting = (RTCSettings)this.CfgRTCGrid.SelectedObject;
						cfg.RtcSetting.Save();
					}
					if (this.modDIO)
					{
						cfg.DioSetting = (DIOSettings)this.CfgDIOGrid.SelectedObject;
						cfg.DioSetting.Save();
					}
					if (this.modAIO)
					{
						cfg.AioSetting = (AIOSettings)this.CfgAIOGrid.SelectedObject;
						cfg.AioSetting.Save();
					}
					if (this.modCTC)
					{
						cfg.CtcSetting = (CTCSettings)this.CfgCTCGrid.SelectedObject;
						cfg.CtcSetting.Save();
					}
					if (this.modFCT)
					{
						cfg.FctSetting = (FCTSettings)this.CfgFCTGrid.SelectedObject;
						cfg.FctSetting.Save();
					}
					if (this.modWEC)
					{
						cfg.WecSetting = (WECSettings)this.CfgWECGrid.SelectedObject;
						cfg.WecSetting.Save();
					}
					//RFazzi_FI17/01/2008_1.Added task configurations on graphics
					if (this.modMTHDucker )
					{
						cfg.MTHDuckerSetting  = (MTHDuckerSettings )this.CfgMTHDuckerGrid.SelectedObject;
						cfg.MTHDuckerSetting.Save();
					}

					//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
					if (this.modMTHSeemi)
					{
						cfg.MTHSeemiSetting = (MTHSeemiSettings)this.CfgMTHSeemiGrid.SelectedObject;
						cfg.MTHSeemiSetting.Save();
					}

					if (this.modALARMS )
					{
						cfg.AlarmsSetting = (ALARMSSettings)this.CfgALARMSGrid.SelectedObject ;
						cfg.AlarmsSetting.Save();
					}
					if (this.modEVENTLOG)
					{
						cfg.EventLogSetting = (EVENTLOGSettings)this.CfgEVENTLOGGrid.SelectedObject;
						cfg.EventLogSetting.Save();
					}
					if (this.modDATABRIDGE)
					{
						cfg.DataBridgeSetting = (DATABRIDGESettings)this.CfgDATABRIDGEGrid.SelectedObject;
						cfg.DataBridgeSetting.Save();
					}
					if (this.modSPEED )
					{
						cfg.SpeedSetting = (SPEEDSettings)this.CfgSpeedGrid.SelectedObject;
						cfg.SpeedSetting.Save();
					}
					if (this.modSPEED_ASR )
					{
						cfg.SpeedASRSetting = (SPEED_ASRSettings)this.CfgSpeedASRGrid.SelectedObject;
						cfg.SpeedASRSetting.Save();
					}

					cfg.SendRelodSettings();
					this.refreshData();

					this.CfgGeneralGrid.ViewForeColor = Color.DimGray;
					this.CfgDBMSGrid.ViewForeColor = Color.DimGray;
					this.CfgStarterGrid.ViewForeColor = Color.DimGray;
					this.CfgRTCGrid.ViewForeColor = Color.DimGray;
					this.CfgDIOGrid.ViewForeColor = Color.DimGray;
					this.CfgAIOGrid.ViewForeColor = Color.DimGray;
					this.CfgCTCGrid.ViewForeColor = Color.DimGray;
					this.CfgFCTGrid.ViewForeColor = Color.DimGray;
					this.CfgWECGrid.ViewForeColor = Color.DimGray;
					//RFazzi_FI17/01/2008_1.Added task configurations on graphics
					this.CfgMTHDuckerGrid.ViewForeColor = Color.DimGray;
					//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
					this.CfgMTHSeemiGrid.ViewForeColor = Color.DimGray;
					this.CfgALARMSGrid.ViewForeColor = Color.DimGray;
					this.CfgEVENTLOGGrid.ViewForeColor = Color.DimGray;
					this.CfgDATABRIDGEGrid.ViewForeColor = Color.DimGray;
					this.CfgSpeedGrid.ViewForeColor = Color.DimGray;
					this.CfgSpeedASRGrid.ViewForeColor = Color.DimGray;

					lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);

					this.modify = false;
				}
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

		protected override void CtrlF5()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
			{
				Utility.Settings s = Utility.Settings.getSettings();
				if (s.configuration.getOnServer())
				{
					lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.Black;
					this.CfgGeneralGrid.ViewForeColor = Color.Black;
					this.CfgDBMSGrid.ViewForeColor = Color.Black;
					this.CfgStarterGrid.ViewForeColor = Color.Black;
					this.CfgRTCGrid.ViewForeColor = Color.Black;
					this.CfgDIOGrid.ViewForeColor = Color.Black;
					this.CfgAIOGrid.ViewForeColor = Color.Black;
					this.CfgCTCGrid.ViewForeColor = Color.Black;
					this.CfgFCTGrid.ViewForeColor = Color.Black;
					this.CfgWECGrid.ViewForeColor = Color.Black;
					//RFazzi_FI17/01/2008_1.Added task configurations on graphics
					this.CfgMTHDuckerGrid.ViewForeColor = Color.Black;
					//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
					this.CfgMTHSeemiGrid.ViewForeColor = Color.Black;
					this.CfgALARMSGrid.ViewForeColor = Color.Black;
					this.CfgEVENTLOGGrid.ViewForeColor = Color.Black;
					this.CfgDATABRIDGEGrid.ViewForeColor = Color.Black;
					this.CfgSpeedGrid.ViewForeColor = Color.Black;
					this.CfgSpeedASRGrid.ViewForeColor = Color.Black;

					this.modify = true;
					this.modGENERAL = false;
					this.modDBMS = false;
					this.modStarter = false;
					this.modRTC = false;
					this.modDIO = false;
					this.modAIO = false;
					this.modCTC = false;
					this.modFCT = false;
					this.modWEC = false;
					//RFazzi_FI17/01/2008_1.Added task configurations on graphics
					this.modMTHDucker = false;
					//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
					this.modMTHSeemi = false;
					this.modALARMS = false;
					this.modEVENTLOG = false;
					this.modDATABRIDGE = false;
					this.modSPEED = false;
					this.modSPEED_ASR = false;
				}
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

		/// <summary>
		/// Escape key behaviour: Abort the operation
		/// </summary>
		protected override void Escape()
		{
			if (this.modify)
			{
				this.modify = false;
				this.CfgGeneralGrid.ViewForeColor = Color.DimGray;
				this.CfgDBMSGrid.ViewForeColor = Color.DimGray;
				this.CfgStarterGrid.ViewForeColor = Color.DimGray;
				this.CfgRTCGrid.ViewForeColor = Color.DimGray;
				this.CfgDIOGrid.ViewForeColor = Color.DimGray;
				this.CfgAIOGrid.ViewForeColor = Color.DimGray;
				this.CfgCTCGrid.ViewForeColor = Color.DimGray;
				this.CfgFCTGrid.ViewForeColor = Color.DimGray;
				this.CfgWECGrid.ViewForeColor = Color.DimGray;
				//RFazzi_FI17/01/2008_1.Added task configurations on graphics
				this.CfgMTHDuckerGrid.ViewForeColor = Color.DimGray;
				//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
				this.CfgMTHSeemiGrid.ViewForeColor = Color.DimGray;
				this.CfgALARMSGrid.ViewForeColor = Color.DimGray;
				this.CfgEVENTLOGGrid.ViewForeColor = Color.DimGray;
				this.CfgDATABRIDGEGrid.ViewForeColor = Color.DimGray;
				this.CfgSpeedGrid.ViewForeColor = Color.DimGray;
				this.CfgSpeedASRGrid.ViewForeColor = Color.DimGray; 
				lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
				this.refreshData();
			}
		}

		private void Frm_SyncroTask1_Load(object sender, EventArgs e)
		{
			//this.CfgDBMSGrid.ExpandAllGridItems();
			//this.CfgGeneralGrid.ExpandAllGridItems();
		}

		private void CfgGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				if (((PropertyGrid)s).Equals(this.CfgGeneralGrid))
				{
					this.modGENERAL = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgDBMSGrid))
				{
					this.modDBMS = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgStarterGrid))
				{
					this.modStarter = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgRTCGrid))
				{
					this.modRTC = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgDIOGrid))
				{
					this.modDIO = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgAIOGrid))
				{
					this.modAIO = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgCTCGrid))
				{
					this.modCTC = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgFCTGrid))
				{
					this.modFCT = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgWECGrid))
				{
					this.modWEC = true;
				}
				//RFazzi_FI17/01/2008_1.Added task configurations on graphics
				else if (((PropertyGrid)s).Equals(this.CfgMTHDuckerGrid ))
				{
					this.modMTHDucker  = true;
				}
				//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
				else if (((PropertyGrid)s).Equals(this.CfgMTHSeemiGrid))
				{
					this.modMTHSeemi = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgALARMSGrid ))
				{
					this.modALARMS  = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgEVENTLOGGrid ))
				{
					this.modEVENTLOG  = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgDATABRIDGEGrid))
				{
					this.modDATABRIDGE = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgSpeedGrid))
				{
					this.modSPEED = true;
				}
				else if (((PropertyGrid)s).Equals(this.CfgSpeedASRGrid))
				{
					this.modSPEED_ASR  = true;
				}
			}
		}


	}
}

