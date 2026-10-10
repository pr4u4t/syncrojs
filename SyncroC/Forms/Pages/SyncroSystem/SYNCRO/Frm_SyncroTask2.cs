using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase.SyncroConfig;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SyncroSystem.SYNCRO
{
	public class Frm_SyncroTask2 : SyncroC.Forms.Pages.SyncroSystem.SYNCRO.Frm_SyncroSubGroup
	{
		private TabControl tab_Settings;

		private TabPage tab_OfficeSPDE;
		private TabPage tab_Printer;
		private TabPage tab_OHD;
		private TabPage tab_WECSyncro;
		private TabPage tab_WEC_BMU;
		private TabPage tab_SyncroC;
		
		private PropertyGrid CfgOfficeSPDEGrid;
		private PropertyGrid CfgPrinterGrid;
		private PropertyGrid CfgOHDGrid;
		private PropertyGrid CfgWECSyncroGrid;
		private PropertyGrid CfgWECBmuGrid;
		private PropertyGrid CfgSyncroCGrid;
    //agrassi_FI27-02-09_1

		private System.ComponentModel.IContainer components = null;

		private SyncroConfig cfg;

		private bool modify = false;
		private bool modOfficeSPDE = false;
		private bool modPrinter = false;
		private bool modOHD = false;
		private bool modWECSyncro = false;
		private bool modWECBMU = false;
    //agrassi_FI22-10-08
    private bool modMTHMinda = false;
    //agrassi_FI27-02-09_1
    private bool modRSM = false;
		//RFAZZI_FI02/12/2009_2.Aggiunta su grafica di Syncro la configurazione del task Roll_Scan.  
		private bool modRollScan = false;
    //agrassi_FI24-02-2010_1
    private bool modMonitor = false;

    private TabPage tabMTHFosberMinda;
    private PropertyGrid CfgMTHMindaGrid;
    private TabPage tab_MTHStandardFOSBER;
    private PropertyGrid CfgMTHFosberStandardGrid;
		private bool modSyncroC = false;
    private TabPage tab_RSM;
    private PropertyGrid CfgRSMGrid;
		private TabPage tab_RollScan;
		private PropertyGrid CfgRollScanGrid;
    private TabPage tab_Monitor;
    private PropertyGrid CfgMonitorGrid;
    //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
    private bool modMTHFosberStandard = false;

		public Frm_SyncroTask2()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			cfg = DBManager.getDBManager().syncroConfig;
		}

		public override void refreshData()
		{
			SyncroConfig cfg = DBManager.getDBManager().syncroConfig;

			this.CfgOfficeSPDEGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.OFFICESPDESettings)cfg.OfficeSPDESetting.CreateCopy();
			this.CfgPrinterGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.PRINTERSettings)cfg.PrinterSetting.CreateCopy();
			this.CfgOHDGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.OHDSettings)cfg.OhdSetting.CreateCopy();
			this.CfgWECBmuGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.WECBMUSettings)cfg.WecBMUSetting.CreateCopy();
			this.CfgWECSyncroGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.WECSyncroSettings)cfg.WecSyncroSetting.CreateCopy();
			this.CfgSyncroCGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SyncroCSettings)cfg.SyncroCSetting.CreateCopy();
      //agrassi_FI22-10-08
      this.CfgMTHMindaGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.MTHFosberMindaCommsSettings)cfg.MthFosberMindaCommsSetting.CreateCopy();
      //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
      this.CfgMTHFosberStandardGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.MTHFosberStandardCommsSettings)cfg.MthFosberStandardCommsSettings.CreateCopy();
      //agrassi_FI27-02-09_1
      this.CfgRSMGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.RSMSettings)cfg.RSMSettings.CreateCopy();
			//RFAZZI_FI02/12/2009_2.Aggiunta su grafica di Syncro la configurazione del task Roll_Scan.  
			this.CfgRollScanGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.RollScanSettings)cfg.RollScanSettings.CreateCopy();
      //agrassi_FI24-02-2010_1
      this.CfgMonitorGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.MonitorSettings)cfg.MonitorSetting.CreateCopy();
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

		protected override void F13()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SyncroTask2));
      this.tab_Settings = new System.Windows.Forms.TabControl();
      this.tab_OfficeSPDE = new System.Windows.Forms.TabPage();
      this.CfgOfficeSPDEGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Printer = new System.Windows.Forms.TabPage();
      this.CfgPrinterGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_OHD = new System.Windows.Forms.TabPage();
      this.CfgOHDGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_WECSyncro = new System.Windows.Forms.TabPage();
      this.CfgWECSyncroGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_WEC_BMU = new System.Windows.Forms.TabPage();
      this.CfgWECBmuGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_SyncroC = new System.Windows.Forms.TabPage();
      this.CfgSyncroCGrid = new System.Windows.Forms.PropertyGrid();
      this.tabMTHFosberMinda = new System.Windows.Forms.TabPage();
      this.CfgMTHMindaGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_MTHStandardFOSBER = new System.Windows.Forms.TabPage();
      this.CfgMTHFosberStandardGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_RSM = new System.Windows.Forms.TabPage();
      this.CfgRSMGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_RollScan = new System.Windows.Forms.TabPage();
      this.CfgRollScanGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Monitor = new System.Windows.Forms.TabPage();
      this.CfgMonitorGrid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_Settings.SuspendLayout();
      this.tab_OfficeSPDE.SuspendLayout();
      this.tab_Printer.SuspendLayout();
      this.tab_OHD.SuspendLayout();
      this.tab_WECSyncro.SuspendLayout();
      this.tab_WEC_BMU.SuspendLayout();
      this.tab_SyncroC.SuspendLayout();
      this.tabMTHFosberMinda.SuspendLayout();
      this.tab_MTHStandardFOSBER.SuspendLayout();
      this.tab_RSM.SuspendLayout();
      this.tab_RollScan.SuspendLayout();
      this.tab_Monitor.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_TabD
      // 
      this.tab_TabD.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // tab_Settings
      // 
      this.tab_Settings.Controls.Add(this.tab_OfficeSPDE);
      this.tab_Settings.Controls.Add(this.tab_Printer);
      this.tab_Settings.Controls.Add(this.tab_OHD);
      this.tab_Settings.Controls.Add(this.tab_WECSyncro);
      this.tab_Settings.Controls.Add(this.tab_WEC_BMU);
      this.tab_Settings.Controls.Add(this.tab_SyncroC);
      this.tab_Settings.Controls.Add(this.tabMTHFosberMinda);
      this.tab_Settings.Controls.Add(this.tab_MTHStandardFOSBER);
      this.tab_Settings.Controls.Add(this.tab_RSM);
      this.tab_Settings.Controls.Add(this.tab_RollScan);
      this.tab_Settings.Controls.Add(this.tab_Monitor);
      this.tab_Settings.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Settings.Location = new System.Drawing.Point(30, 193);
      this.tab_Settings.Name = "tab_Settings";
      this.tab_Settings.SelectedIndex = 0;
      this.tab_Settings.Size = new System.Drawing.Size(1171, 519);
      this.tab_Settings.TabIndex = 119;
      // 
      // tab_OfficeSPDE
      // 
      this.tab_OfficeSPDE.Controls.Add(this.CfgOfficeSPDEGrid);
      this.tab_OfficeSPDE.Location = new System.Drawing.Point(4, 25);
      this.tab_OfficeSPDE.Name = "tab_OfficeSPDE";
      this.tab_OfficeSPDE.Padding = new System.Windows.Forms.Padding(3);
      this.tab_OfficeSPDE.Size = new System.Drawing.Size(1163, 490);
      this.tab_OfficeSPDE.TabIndex = 14;
      this.tab_OfficeSPDE.Text = "OFFICE SPDE";
      this.tab_OfficeSPDE.UseVisualStyleBackColor = true;
      // 
      // CfgOfficeSPDEGrid
      // 
      this.CfgOfficeSPDEGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgOfficeSPDEGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgOfficeSPDEGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgOfficeSPDEGrid.Name = "CfgOfficeSPDEGrid";
      this.CfgOfficeSPDEGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgOfficeSPDEGrid.TabIndex = 123;
      this.CfgOfficeSPDEGrid.ToolbarVisible = false;
      this.CfgOfficeSPDEGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgOfficeSPDEGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgOfficeSPDEGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Printer
      // 
      this.tab_Printer.Controls.Add(this.CfgPrinterGrid);
      this.tab_Printer.Location = new System.Drawing.Point(4, 25);
      this.tab_Printer.Name = "tab_Printer";
      this.tab_Printer.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Printer.Size = new System.Drawing.Size(1098, 490);
      this.tab_Printer.TabIndex = 15;
      this.tab_Printer.Text = "PRINTER";
      this.tab_Printer.UseVisualStyleBackColor = true;
      // 
      // CfgPrinterGrid
      // 
      this.CfgPrinterGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgPrinterGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgPrinterGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgPrinterGrid.Name = "CfgPrinterGrid";
      this.CfgPrinterGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgPrinterGrid.TabIndex = 124;
      this.CfgPrinterGrid.ToolbarVisible = false;
      this.CfgPrinterGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgPrinterGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgPrinterGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_OHD
      // 
      this.tab_OHD.Controls.Add(this.CfgOHDGrid);
      this.tab_OHD.Location = new System.Drawing.Point(4, 25);
      this.tab_OHD.Name = "tab_OHD";
      this.tab_OHD.Padding = new System.Windows.Forms.Padding(3);
      this.tab_OHD.Size = new System.Drawing.Size(1098, 490);
      this.tab_OHD.TabIndex = 16;
      this.tab_OHD.Text = "OHD";
      this.tab_OHD.UseVisualStyleBackColor = true;
      // 
      // CfgOHDGrid
      // 
      this.CfgOHDGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgOHDGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgOHDGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgOHDGrid.Name = "CfgOHDGrid";
      this.CfgOHDGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgOHDGrid.TabIndex = 125;
      this.CfgOHDGrid.ToolbarVisible = false;
      this.CfgOHDGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgOHDGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgOHDGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_WECSyncro
      // 
      this.tab_WECSyncro.Controls.Add(this.CfgWECSyncroGrid);
      this.tab_WECSyncro.Location = new System.Drawing.Point(4, 25);
      this.tab_WECSyncro.Name = "tab_WECSyncro";
      this.tab_WECSyncro.Padding = new System.Windows.Forms.Padding(3);
      this.tab_WECSyncro.Size = new System.Drawing.Size(1098, 490);
      this.tab_WECSyncro.TabIndex = 17;
      this.tab_WECSyncro.Text = "WEC SYNCRO";
      this.tab_WECSyncro.UseVisualStyleBackColor = true;
      // 
      // CfgWECSyncroGrid
      // 
      this.CfgWECSyncroGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgWECSyncroGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgWECSyncroGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgWECSyncroGrid.Name = "CfgWECSyncroGrid";
      this.CfgWECSyncroGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgWECSyncroGrid.TabIndex = 126;
      this.CfgWECSyncroGrid.ToolbarVisible = false;
      this.CfgWECSyncroGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgWECSyncroGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgWECSyncroGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_WEC_BMU
      // 
      this.tab_WEC_BMU.Controls.Add(this.CfgWECBmuGrid);
      this.tab_WEC_BMU.Location = new System.Drawing.Point(4, 25);
      this.tab_WEC_BMU.Name = "tab_WEC_BMU";
      this.tab_WEC_BMU.Padding = new System.Windows.Forms.Padding(3);
      this.tab_WEC_BMU.Size = new System.Drawing.Size(1098, 490);
      this.tab_WEC_BMU.TabIndex = 18;
      this.tab_WEC_BMU.Text = "WEC BMU";
      this.tab_WEC_BMU.UseVisualStyleBackColor = true;
      // 
      // CfgWECBmuGrid
      // 
      this.CfgWECBmuGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgWECBmuGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgWECBmuGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgWECBmuGrid.Name = "CfgWECBmuGrid";
      this.CfgWECBmuGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgWECBmuGrid.TabIndex = 125;
      this.CfgWECBmuGrid.ToolbarVisible = false;
      this.CfgWECBmuGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgWECBmuGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgWECBmuGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_SyncroC
      // 
      this.tab_SyncroC.Controls.Add(this.CfgSyncroCGrid);
      this.tab_SyncroC.Location = new System.Drawing.Point(4, 25);
      this.tab_SyncroC.Name = "tab_SyncroC";
      this.tab_SyncroC.Padding = new System.Windows.Forms.Padding(3);
      this.tab_SyncroC.Size = new System.Drawing.Size(1098, 490);
      this.tab_SyncroC.TabIndex = 19;
      this.tab_SyncroC.Text = "SyncroC";
      this.tab_SyncroC.UseVisualStyleBackColor = true;
      // 
      // CfgSyncroCGrid
      // 
      this.CfgSyncroCGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSyncroCGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSyncroCGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgSyncroCGrid.Name = "CfgSyncroCGrid";
      this.CfgSyncroCGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgSyncroCGrid.TabIndex = 126;
      this.CfgSyncroCGrid.ToolbarVisible = false;
      this.CfgSyncroCGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgSyncroCGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSyncroCGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tabMTHFosberMinda
      // 
      this.tabMTHFosberMinda.Controls.Add(this.CfgMTHMindaGrid);
      this.tabMTHFosberMinda.Location = new System.Drawing.Point(4, 25);
      this.tabMTHFosberMinda.Name = "tabMTHFosberMinda";
      this.tabMTHFosberMinda.Padding = new System.Windows.Forms.Padding(3);
      this.tabMTHFosberMinda.Size = new System.Drawing.Size(1098, 490);
      this.tabMTHFosberMinda.TabIndex = 20;
      this.tabMTHFosberMinda.Text = "MTH Minda";
      this.tabMTHFosberMinda.UseVisualStyleBackColor = true;
      // 
      // CfgMTHMindaGrid
      // 
      this.CfgMTHMindaGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMTHMindaGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMTHMindaGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgMTHMindaGrid.Name = "CfgMTHMindaGrid";
      this.CfgMTHMindaGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgMTHMindaGrid.TabIndex = 120;
      this.CfgMTHMindaGrid.ToolbarVisible = false;
      this.CfgMTHMindaGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgMTHMindaGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgMTHMindaGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_MTHStandardFOSBER
      // 
      this.tab_MTHStandardFOSBER.Controls.Add(this.CfgMTHFosberStandardGrid);
      this.tab_MTHStandardFOSBER.Location = new System.Drawing.Point(4, 25);
      this.tab_MTHStandardFOSBER.Name = "tab_MTHStandardFOSBER";
      this.tab_MTHStandardFOSBER.Size = new System.Drawing.Size(1098, 490);
      this.tab_MTHStandardFOSBER.TabIndex = 21;
      this.tab_MTHStandardFOSBER.Text = "MTH Standard FOSBER";
      this.tab_MTHStandardFOSBER.UseVisualStyleBackColor = true;
      // 
      // CfgMTHFosberStandardGrid
      // 
      this.CfgMTHFosberStandardGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMTHFosberStandardGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMTHFosberStandardGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgMTHFosberStandardGrid.Name = "CfgMTHFosberStandardGrid";
      this.CfgMTHFosberStandardGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgMTHFosberStandardGrid.TabIndex = 124;
      this.CfgMTHFosberStandardGrid.ToolbarVisible = false;
      this.CfgMTHFosberStandardGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgMTHFosberStandardGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgMTHFosberStandardGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_RSM
      // 
      this.tab_RSM.Controls.Add(this.CfgRSMGrid);
      this.tab_RSM.Location = new System.Drawing.Point(4, 25);
      this.tab_RSM.Name = "tab_RSM";
      this.tab_RSM.Size = new System.Drawing.Size(1098, 490);
      this.tab_RSM.TabIndex = 22;
      this.tab_RSM.Text = "RSM";
      this.tab_RSM.UseVisualStyleBackColor = true;
      // 
      // CfgRSMGrid
      // 
      this.CfgRSMGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgRSMGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgRSMGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgRSMGrid.Name = "CfgRSMGrid";
      this.CfgRSMGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgRSMGrid.TabIndex = 125;
      this.CfgRSMGrid.ToolbarVisible = false;
      this.CfgRSMGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgRSMGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgRSMGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_RollScan
      // 
      this.tab_RollScan.Controls.Add(this.CfgRollScanGrid);
      this.tab_RollScan.Location = new System.Drawing.Point(4, 25);
      this.tab_RollScan.Name = "tab_RollScan";
      this.tab_RollScan.Padding = new System.Windows.Forms.Padding(3);
      this.tab_RollScan.Size = new System.Drawing.Size(1098, 490);
      this.tab_RollScan.TabIndex = 23;
      this.tab_RollScan.Text = "RollScan";
      this.tab_RollScan.UseVisualStyleBackColor = true;
      // 
      // CfgRollScanGrid
      // 
      this.CfgRollScanGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgRollScanGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgRollScanGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgRollScanGrid.Name = "CfgRollScanGrid";
      this.CfgRollScanGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgRollScanGrid.TabIndex = 126;
      this.CfgRollScanGrid.ToolbarVisible = false;
      this.CfgRollScanGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgRollScanGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgRollScanGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Monitor
      // 
      this.tab_Monitor.Controls.Add(this.CfgMonitorGrid);
      this.tab_Monitor.Location = new System.Drawing.Point(4, 25);
      this.tab_Monitor.Name = "tab_Monitor";
      this.tab_Monitor.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Monitor.Size = new System.Drawing.Size(1098, 490);
      this.tab_Monitor.TabIndex = 24;
      this.tab_Monitor.Text = "Monitor";
      this.tab_Monitor.UseVisualStyleBackColor = true;
      // 
      // CfgMonitorGrid
      // 
      this.CfgMonitorGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMonitorGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMonitorGrid.Location = new System.Drawing.Point(50, 52);
      this.CfgMonitorGrid.Name = "CfgMonitorGrid";
      this.CfgMonitorGrid.Size = new System.Drawing.Size(959, 386);
      this.CfgMonitorGrid.TabIndex = 125;
      this.CfgMonitorGrid.ToolbarVisible = false;
      this.CfgMonitorGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgMonitorGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgMonitorGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // Frm_SyncroTask2
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tab_Settings);
      this.Name = "Frm_SyncroTask2";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
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
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.tab_Settings, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.tab_Settings.ResumeLayout(false);
      this.tab_OfficeSPDE.ResumeLayout(false);
      this.tab_Printer.ResumeLayout(false);
      this.tab_OHD.ResumeLayout(false);
      this.tab_WECSyncro.ResumeLayout(false);
      this.tab_WEC_BMU.ResumeLayout(false);
      this.tab_SyncroC.ResumeLayout(false);
      this.tabMTHFosberMinda.ResumeLayout(false);
      this.tab_MTHStandardFOSBER.ResumeLayout(false);
      this.tab_RSM.ResumeLayout(false);
      this.tab_RollScan.ResumeLayout(false);
      this.tab_Monitor.ResumeLayout(false);
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
					if (this.modOfficeSPDE )
					{
						cfg.OfficeSPDESetting  = (OFFICESPDESettings )this.CfgOfficeSPDEGrid.SelectedObject;
						cfg.OfficeSPDESetting.Save();
					}
					if (this.modPrinter )
					{
						cfg.PrinterSetting = (PRINTERSettings )this.CfgPrinterGrid.SelectedObject;
						cfg.PrinterSetting .Save();
					}
					if (this.modOHD )
					{
						cfg.OhdSetting = (OHDSettings)this.CfgOHDGrid.SelectedObject;
						cfg.OhdSetting.Save();
					}
					if (this.modWECBMU )
					{
						cfg.WecBMUSetting = (WECBMUSettings )this.CfgWECBmuGrid.SelectedObject;
						cfg.WecBMUSetting.Save();
					}
					if (this.modWECSyncro)
					{
						cfg.WecSyncroSetting = (WECSyncroSettings )this.CfgWECSyncroGrid.SelectedObject;
						cfg.WecSyncroSetting.Save();
					}
					if (this.modSyncroC)
					{
						cfg.SyncroCSetting = (SyncroCSettings)this.CfgSyncroCGrid.SelectedObject;
						cfg.SyncroCSetting.Save();
					}
          //agrassi_FI22-10-08
          if (this.modMTHMinda)
          {
            cfg.MthFosberMindaCommsSetting = (MTHFosberMindaCommsSettings)this.CfgMTHMindaGrid.SelectedObject;
            cfg.MthFosberMindaCommsSetting.Save();
          }

          //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
          if (this.modMTHFosberStandard)
          {
            cfg.MthFosberStandardCommsSettings = (MTHFosberStandardCommsSettings)this.CfgMTHFosberStandardGrid.SelectedObject;
            cfg.MthFosberStandardCommsSettings.Save();
          }

          //agrassi_FI27-02-09_1
          if (this.modRSM)
          {
            cfg.RSMSettings = (RSMSettings)this.CfgRSMGrid.SelectedObject;
            cfg.RSMSettings.Save();
          }

					//RFAZZI_FI02/12/2009_2.Aggiunta su grafica di Syncro la configurazione del task Roll_Scan.  
          if (this.modRollScan)
          {
            cfg.RollScanSettings = (RollScanSettings)this.CfgRollScanGrid.SelectedObject;
						cfg.RollScanSettings.Save();
          }

          //agrassi_FI24-02-2010_1
          if (this.modMonitor)
          {
            cfg.MonitorSetting = (MonitorSettings)this.CfgMonitorGrid.SelectedObject;
            cfg.MonitorSetting.Save();
          }

					cfg.SendRelodSettings();
					this.refreshData();

					this.CfgOfficeSPDEGrid.ViewForeColor = Color.DimGray;
					this.CfgPrinterGrid.ViewForeColor = Color.DimGray;
					this.CfgOHDGrid.ViewForeColor = Color.DimGray;
					this.CfgWECBmuGrid.ViewForeColor = Color.DimGray;
					this.CfgWECSyncroGrid.ViewForeColor = Color.DimGray;
					this.CfgSyncroCGrid.ViewForeColor = Color.DimGray;
          //agrassi_FI22-10-08
          this.CfgMTHMindaGrid.ViewForeColor = Color.DimGray;
          //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
          this.CfgMTHFosberStandardGrid.ViewForeColor = Color.DimGray;
          //agrassi_FI27-02-09_1
          this.CfgRSMGrid.ViewForeColor = Color.DimGray;
					//RFAZZI_FI02/12/2009_2.Aggiunta su grafica di Syncro la configurazione del task Roll_Scan.  
					this.CfgRollScanGrid.ViewForeColor = Color.DimGray;
          //agrassi_FI24-02-2010_1
          this.CfgMonitorGrid.ViewForeColor = Color.DimGray;
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
					this.CfgOfficeSPDEGrid.ViewForeColor = Color.Black ;
					this.CfgPrinterGrid.ViewForeColor = Color.Black;
					this.CfgOHDGrid.ViewForeColor = Color.Black;
					this.CfgWECBmuGrid.ViewForeColor = Color.Black;
					this.CfgWECSyncroGrid.ViewForeColor = Color.Black;
					this.CfgSyncroCGrid.ViewForeColor = Color.Black;
          //agrassi_FI22-10-08
          this.CfgMTHMindaGrid.ViewForeColor = Color.Black;
          //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
          this.CfgMTHFosberStandardGrid.ViewForeColor = Color.Black;
          //agrassi_FI27-02-09_1
          this.CfgRSMGrid.ViewForeColor = Color.Black;
					//RFAZZI_FI02/12/2009_2.Aggiunta su grafica di Syncro la configurazione del task Roll_Scan.  
					this.CfgRollScanGrid.ViewForeColor = Color.Black;
          //agrassi_FI24-02-2010_1
          this.CfgMonitorGrid.ViewForeColor = Color.Black;
					this.modify = true;
					this.modOfficeSPDE = false;
					this.modPrinter = false;
					this.modOHD = false;
					this.modWECBMU = false;
					this.modWECSyncro = false;
					this.modSyncroC = false;
          //agrassi_FI22-10-08
          this.modMTHMinda = false;
          //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
          this.modMTHFosberStandard = false;
          //agrassi_FI27-02-09_1
          this.modRSM = false;
					//RFAZZI_FI02/12/2009_2.Aggiunta su grafica di Syncro la configurazione del task Roll_Scan.  
					this.modRollScan = false;
          //agrassi_FI24-02-2010_1
          this.modMonitor = false;
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
				this.CfgOfficeSPDEGrid.ViewForeColor = Color.DimGray;
				this.CfgPrinterGrid.ViewForeColor = Color.DimGray;
				this.CfgOHDGrid.ViewForeColor = Color.DimGray;
				this.CfgWECBmuGrid.ViewForeColor = Color.DimGray;
				this.CfgWECSyncroGrid.ViewForeColor = Color.DimGray;
				this.CfgSyncroCGrid.ViewForeColor = Color.DimGray;
        //agrassi_FI22-10-08
        this.CfgMTHMindaGrid.ViewForeColor = Color.DimGray;
        //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
        this.CfgMTHFosberStandardGrid.ViewForeColor = Color.DimGray;
        //agrassi_FI27-02-09_1
        this.CfgRSMGrid.ViewForeColor = Color.DimGray;
				//RFAZZI_FI02/12/2009_2.Aggiunta su grafica di Syncro la configurazione del task Roll_Scan.  
				this.CfgRollScanGrid.ViewForeColor = Color.DimGray;
        //agrassi_FI24-02-2010_1
        this.CfgMonitorGrid.ViewForeColor = Color.DimGray;
        lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
				this.refreshData();
			}
		}

		private void CfgGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				if (((PropertyGrid)s).Equals(this.CfgOfficeSPDEGrid ))
				{
					this.modOfficeSPDE  = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgPrinterGrid ))
				{
					this.modPrinter = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgOHDGrid) )
				{
					this.modOHD = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgWECBmuGrid ))
				{
					this.modWECBMU = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgWECSyncroGrid))
				{
					this.modWECSyncro = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgSyncroCGrid))
				{
					this.modSyncroC = true;
				}
        //agrassi_FI22-10-08
        if (((PropertyGrid)s).Equals(this.CfgMTHMindaGrid))
        {
          this.modMTHMinda = true;
        }
        //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
        if (((PropertyGrid)s).Equals(this.CfgMTHFosberStandardGrid))
        {
          this.modMTHFosberStandard = true;
        }
        //agrassi_FI27-02-09_1
        if (((PropertyGrid)s).Equals(this.CfgRSMGrid))
        {
          this.modRSM = true;
        }
				//RFAZZI_FI02/12/2009_2.Aggiunta su grafica di Syncro la configurazione del task Roll_Scan.  
				if (((PropertyGrid)s).Equals(this.CfgRollScanGrid))
				{
					this.modRollScan = true;
				}
        //agrassi_FI24-02-2010_1
        if (((PropertyGrid)s).Equals(this.CfgMonitorGrid))
        {
          this.modMonitor = true;
        }
      }
		}
	}
}

