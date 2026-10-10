using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase.SyncroConfig;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Knife.Settings
{
	public class Frm_KnifeSettingsTask : SyncroC.Forms.Pages.Knife.Settings.Frm_KnifeSettingsSubgroup
	{
		private TabControl tab_Settings;
		private TabPage tab_Knife_Fosber_21_11;
		private PropertyGrid CfgFosber21_11Grid;
		private PropertyGrid CfgFosber21_11CommsGrid;
		private System.ComponentModel.IContainer components = null;

		private SyncroConfig cfg;
		private bool modify = false;
		private TabPage tab_Knife_Fosber_21_20;
		private PropertyGrid CfgFosber21_20CommsGrid;
		private PropertyGrid CfgFosber21_20Grid;
		private bool modDigitec21_11 = false;
    //ABorelli_FI01/06/2008_1
    private bool modBMB21_11 = false;
		private TabPage tab_Knife_Fosber_21_30;
		private PropertyGrid CfgFosber21_30CommsGrid;
		private PropertyGrid CfgFosber21_30Grid;
		private bool modDigitec21_20 = false;
		private bool modDigitec21_30 = false;
    private TabPage tab_Knife_Marquip_DECC;
    private PropertyGrid CfgMarquipDeccCommsGrid;
    private PropertyGrid CfgMarquipDeccGrid;
    //ABorelli_FI01/06/2008_1
    private TabPage tab_Knife_Fosber_21_11_BMB;
    private PropertyGrid CfgFosber21_11CommsBMBGrid;
    private PropertyGrid CfgFosber21_11BMBGrid;
    private TabPage tab_Knife_LOGIMAC;
    private PropertyGrid CfgLOGIMACCommsGrid;
    private PropertyGrid CfgLOGIMACGrid;
    //PMASSARO_FI21/03/08 - ADDED FOR MARQUIP DECC
    private bool modMarquipDECC = false;
    private TabPage tab_Knife_BSCOM_Gapo;
    private PropertyGrid CfgBSCOMGapoCommsGrid;
    private PropertyGrid CfgBHSGrid;

    //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Electric
    //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Comms 
    private bool modLOGIMAC = false;   
    
    //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task MHI 58E Ctrl
    private bool modKnifeMhi58ECtrl = false;
    private TabPage tabKnife_MHI_58E;
    private PropertyGrid CfgKNIFE_MHI_58E_CtrlGrid;


    //agrassi_FI18-03-2010_1
    private bool modBSCOM = false;

		public Frm_KnifeSettingsTask()
		{
			InitializeComponent();
			cfg = DBManager.getDBManager().syncroConfig;

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		public override void refreshData()
		{
			SyncroConfig cfg = DBManager.getDBManager().syncroConfig;

			//RFazzi_FI17/01/2008_1.Added task configurations on graphics
			this.CfgFosber21_11CommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KNIFE21_11DigitecCommsSettings )cfg .KNIFEDigitec21_11CommsSetting.CreateCopy();
			this.CfgFosber21_11Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KNIFE21_11DigitecSettings)cfg.KNIFEDigitec21_11Setting.CreateCopy();
			this.CfgFosber21_20CommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KNIFE21_20DigitecCommsSettings)cfg.KNIFEDigitec21_20CommsSetting.CreateCopy();
			this.CfgFosber21_20Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KNIFE21_20DigitecSettings)cfg.KNIFEDigitec21_20Setting.CreateCopy();
			this.CfgFosber21_30CommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KNIFE21_30CommsSettings)cfg.KNIFE21_30CommsSetting.CreateCopy();
			this.CfgFosber21_30Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KNIFE21_30Settings)cfg.KNIFE21_30Setting.CreateCopy();
      //PMASSARO_FI21/03/08 - ADDED FOR MARQUIP DECC
      this.CfgMarquipDeccCommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KNIFE_MarquipDeccCommsSettings)cfg.KNIFEMarquipDeccCommsSetting.CreateCopy();
      this.CfgMarquipDeccGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KNIFE_MarquipSettings)cfg.KNIFEMarquipSetting.CreateCopy();
      //ABorelli_FI01/06/2008_1
      this.CfgFosber21_11CommsBMBGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KNIFE21_11BmbCommsSettings)cfg.KNIFEBmb21_11CommsSetting.CreateCopy();
      this.CfgFosber21_11BMBGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KNIFE21_11BmbSettings)cfg.KNIFEBmb21_11Setting.CreateCopy();

      this.CfgLOGIMACCommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KnifeLogimacComms_Settings)cfg.KNIFELogimacComms_Settings.CreateCopy();  //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Comms 
      this.CfgLOGIMACGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KnifeLogimac_Settings)cfg.KNIFELogimac_Setting.CreateCopy();  //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Electric

      this.CfgKNIFE_MHI_58E_CtrlGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KNIFE_MHI_58E_CtrlSettings)cfg.KNIFE_Mhi_58E_CtrlSettings.CreateCopy();  //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task MHI 58E Ctrl

      //agrassi_FI18-03-2010_1
      this.CfgBSCOMGapoCommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KnifeBSCOMGapoCommsSettings)cfg.KnifeBSCOMGapoCommsSetting.CreateCopy();
      //mlamioni_FI30-04-2010
      this.CfgBHSGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.KnifeBHSSettings)cfg.KnifeBHSSetting.CreateCopy();
    }
		
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

		protected override void F11()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_KnifeSettingsTask));
      this.tab_Settings = new System.Windows.Forms.TabControl();
      this.tab_Knife_Fosber_21_11 = new System.Windows.Forms.TabPage();
      this.CfgFosber21_11CommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgFosber21_11Grid = new System.Windows.Forms.PropertyGrid();
      this.tab_Knife_Fosber_21_20 = new System.Windows.Forms.TabPage();
      this.CfgFosber21_20CommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgFosber21_20Grid = new System.Windows.Forms.PropertyGrid();
      this.tab_Knife_Fosber_21_30 = new System.Windows.Forms.TabPage();
      this.CfgFosber21_30CommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgFosber21_30Grid = new System.Windows.Forms.PropertyGrid();
      this.tab_Knife_Fosber_21_11_BMB = new System.Windows.Forms.TabPage();
      this.CfgFosber21_11CommsBMBGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgFosber21_11BMBGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Knife_Marquip_DECC = new System.Windows.Forms.TabPage();
      this.CfgMarquipDeccCommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgMarquipDeccGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Knife_LOGIMAC = new System.Windows.Forms.TabPage();
      this.CfgLOGIMACCommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgLOGIMACGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Knife_BSCOM_Gapo = new System.Windows.Forms.TabPage();
      this.CfgBHSGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgBSCOMGapoCommsGrid = new System.Windows.Forms.PropertyGrid();
      this.tabKnife_MHI_58E = new System.Windows.Forms.TabPage();
      this.CfgKNIFE_MHI_58E_CtrlGrid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_Settings.SuspendLayout();
      this.tab_Knife_Fosber_21_11.SuspendLayout();
      this.tab_Knife_Fosber_21_20.SuspendLayout();
      this.tab_Knife_Fosber_21_30.SuspendLayout();
      this.tab_Knife_Fosber_21_11_BMB.SuspendLayout();
      this.tab_Knife_Marquip_DECC.SuspendLayout();
      this.tab_Knife_LOGIMAC.SuspendLayout();
      this.tab_Knife_BSCOM_Gapo.SuspendLayout();
      this.tabKnife_MHI_58E.SuspendLayout();
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
      this.tab_Settings.Controls.Add(this.tab_Knife_Fosber_21_11);
      this.tab_Settings.Controls.Add(this.tab_Knife_Fosber_21_20);
      this.tab_Settings.Controls.Add(this.tab_Knife_Fosber_21_30);
      this.tab_Settings.Controls.Add(this.tab_Knife_Fosber_21_11_BMB);
      this.tab_Settings.Controls.Add(this.tab_Knife_Marquip_DECC);
      this.tab_Settings.Controls.Add(this.tab_Knife_LOGIMAC);
      this.tab_Settings.Controls.Add(this.tab_Knife_BSCOM_Gapo);
      this.tab_Settings.Controls.Add(this.tabKnife_MHI_58E);
      this.tab_Settings.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Settings.Location = new System.Drawing.Point(31, 196);
      this.tab_Settings.Name = "tab_Settings";
      this.tab_Settings.SelectedIndex = 0;
      this.tab_Settings.Size = new System.Drawing.Size(1170, 519);
      this.tab_Settings.TabIndex = 119;
      // 
      // tab_Knife_Fosber_21_11
      // 
      this.tab_Knife_Fosber_21_11.Controls.Add(this.CfgFosber21_11CommsGrid);
      this.tab_Knife_Fosber_21_11.Controls.Add(this.CfgFosber21_11Grid);
      this.tab_Knife_Fosber_21_11.Location = new System.Drawing.Point(4, 27);
      this.tab_Knife_Fosber_21_11.Name = "tab_Knife_Fosber_21_11";
      this.tab_Knife_Fosber_21_11.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Knife_Fosber_21_11.Size = new System.Drawing.Size(1162, 488);
      this.tab_Knife_Fosber_21_11.TabIndex = 0;
      this.tab_Knife_Fosber_21_11.Text = "Fosber 21.11 Digitec";
      this.tab_Knife_Fosber_21_11.UseVisualStyleBackColor = true;
      // 
      // CfgFosber21_11CommsGrid
      // 
      this.CfgFosber21_11CommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_11CommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_11CommsGrid.Location = new System.Drawing.Point(573, 46);
      this.CfgFosber21_11CommsGrid.Name = "CfgFosber21_11CommsGrid";
      this.CfgFosber21_11CommsGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgFosber21_11CommsGrid.Size = new System.Drawing.Size(503, 399);
      this.CfgFosber21_11CommsGrid.TabIndex = 118;
      this.CfgFosber21_11CommsGrid.ToolbarVisible = false;
      this.CfgFosber21_11CommsGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgFosber21_11CommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgFosber21_11CommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgFosber21_11Grid
      // 
      this.CfgFosber21_11Grid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_11Grid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_11Grid.Location = new System.Drawing.Point(87, 46);
      this.CfgFosber21_11Grid.Name = "CfgFosber21_11Grid";
      this.CfgFosber21_11Grid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgFosber21_11Grid.Size = new System.Drawing.Size(480, 399);
      this.CfgFosber21_11Grid.TabIndex = 117;
      this.CfgFosber21_11Grid.ToolbarVisible = false;
      this.CfgFosber21_11Grid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgFosber21_11Grid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgFosber21_11Grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Knife_Fosber_21_20
      // 
      this.tab_Knife_Fosber_21_20.Controls.Add(this.CfgFosber21_20CommsGrid);
      this.tab_Knife_Fosber_21_20.Controls.Add(this.CfgFosber21_20Grid);
      this.tab_Knife_Fosber_21_20.Location = new System.Drawing.Point(4, 27);
      this.tab_Knife_Fosber_21_20.Name = "tab_Knife_Fosber_21_20";
      this.tab_Knife_Fosber_21_20.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Knife_Fosber_21_20.Size = new System.Drawing.Size(1162, 488);
      this.tab_Knife_Fosber_21_20.TabIndex = 1;
      this.tab_Knife_Fosber_21_20.Text = "Fosber 21.20 ";
      this.tab_Knife_Fosber_21_20.UseVisualStyleBackColor = true;
      // 
      // CfgFosber21_20CommsGrid
      // 
      this.CfgFosber21_20CommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_20CommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_20CommsGrid.Location = new System.Drawing.Point(573, 46);
      this.CfgFosber21_20CommsGrid.Name = "CfgFosber21_20CommsGrid";
      this.CfgFosber21_20CommsGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgFosber21_20CommsGrid.Size = new System.Drawing.Size(503, 399);
      this.CfgFosber21_20CommsGrid.TabIndex = 120;
      this.CfgFosber21_20CommsGrid.ToolbarVisible = false;
      this.CfgFosber21_20CommsGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgFosber21_20CommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgFosber21_20CommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgFosber21_20Grid
      // 
      this.CfgFosber21_20Grid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_20Grid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_20Grid.Location = new System.Drawing.Point(87, 46);
      this.CfgFosber21_20Grid.Name = "CfgFosber21_20Grid";
      this.CfgFosber21_20Grid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgFosber21_20Grid.Size = new System.Drawing.Size(480, 399);
      this.CfgFosber21_20Grid.TabIndex = 119;
      this.CfgFosber21_20Grid.ToolbarVisible = false;
      this.CfgFosber21_20Grid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgFosber21_20Grid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgFosber21_20Grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Knife_Fosber_21_30
      // 
      this.tab_Knife_Fosber_21_30.Controls.Add(this.CfgFosber21_30CommsGrid);
      this.tab_Knife_Fosber_21_30.Controls.Add(this.CfgFosber21_30Grid);
      this.tab_Knife_Fosber_21_30.Location = new System.Drawing.Point(4, 27);
      this.tab_Knife_Fosber_21_30.Name = "tab_Knife_Fosber_21_30";
      this.tab_Knife_Fosber_21_30.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Knife_Fosber_21_30.Size = new System.Drawing.Size(1162, 488);
      this.tab_Knife_Fosber_21_30.TabIndex = 2;
      this.tab_Knife_Fosber_21_30.Text = "Fosber 21.30";
      this.tab_Knife_Fosber_21_30.UseVisualStyleBackColor = true;
      // 
      // CfgFosber21_30CommsGrid
      // 
      this.CfgFosber21_30CommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_30CommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_30CommsGrid.Location = new System.Drawing.Point(573, 46);
      this.CfgFosber21_30CommsGrid.Name = "CfgFosber21_30CommsGrid";
      this.CfgFosber21_30CommsGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgFosber21_30CommsGrid.Size = new System.Drawing.Size(503, 399);
      this.CfgFosber21_30CommsGrid.TabIndex = 120;
      this.CfgFosber21_30CommsGrid.ToolbarVisible = false;
      this.CfgFosber21_30CommsGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgFosber21_30CommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgFosber21_30CommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgFosber21_30Grid
      // 
      this.CfgFosber21_30Grid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_30Grid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_30Grid.Location = new System.Drawing.Point(87, 46);
      this.CfgFosber21_30Grid.Name = "CfgFosber21_30Grid";
      this.CfgFosber21_30Grid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgFosber21_30Grid.Size = new System.Drawing.Size(480, 399);
      this.CfgFosber21_30Grid.TabIndex = 119;
      this.CfgFosber21_30Grid.ToolbarVisible = false;
      this.CfgFosber21_30Grid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgFosber21_30Grid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgFosber21_30Grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Knife_Fosber_21_11_BMB
      // 
      this.tab_Knife_Fosber_21_11_BMB.Controls.Add(this.CfgFosber21_11CommsBMBGrid);
      this.tab_Knife_Fosber_21_11_BMB.Controls.Add(this.CfgFosber21_11BMBGrid);
      this.tab_Knife_Fosber_21_11_BMB.Location = new System.Drawing.Point(4, 27);
      this.tab_Knife_Fosber_21_11_BMB.Name = "tab_Knife_Fosber_21_11_BMB";
      this.tab_Knife_Fosber_21_11_BMB.Size = new System.Drawing.Size(1162, 488);
      this.tab_Knife_Fosber_21_11_BMB.TabIndex = 4;
      this.tab_Knife_Fosber_21_11_BMB.Text = "Fosber 21.11 BMB";
      this.tab_Knife_Fosber_21_11_BMB.UseVisualStyleBackColor = true;
      // 
      // CfgFosber21_11CommsBMBGrid
      // 
      this.CfgFosber21_11CommsBMBGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_11CommsBMBGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_11CommsBMBGrid.Location = new System.Drawing.Point(573, 46);
      this.CfgFosber21_11CommsBMBGrid.Name = "CfgFosber21_11CommsBMBGrid";
      this.CfgFosber21_11CommsBMBGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgFosber21_11CommsBMBGrid.Size = new System.Drawing.Size(503, 399);
      this.CfgFosber21_11CommsBMBGrid.TabIndex = 120;
      this.CfgFosber21_11CommsBMBGrid.ToolbarVisible = false;
      this.CfgFosber21_11CommsBMBGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgFosber21_11CommsBMBGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgFosber21_11CommsBMBGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgFosber21_11BMBGrid
      // 
      this.CfgFosber21_11BMBGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_11BMBGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFosber21_11BMBGrid.Location = new System.Drawing.Point(87, 46);
      this.CfgFosber21_11BMBGrid.Name = "CfgFosber21_11BMBGrid";
      this.CfgFosber21_11BMBGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgFosber21_11BMBGrid.Size = new System.Drawing.Size(480, 399);
      this.CfgFosber21_11BMBGrid.TabIndex = 119;
      this.CfgFosber21_11BMBGrid.ToolbarVisible = false;
      this.CfgFosber21_11BMBGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgFosber21_11BMBGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgFosber21_11BMBGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Knife_Marquip_DECC
      // 
      this.tab_Knife_Marquip_DECC.Controls.Add(this.CfgMarquipDeccCommsGrid);
      this.tab_Knife_Marquip_DECC.Controls.Add(this.CfgMarquipDeccGrid);
      this.tab_Knife_Marquip_DECC.Location = new System.Drawing.Point(4, 27);
      this.tab_Knife_Marquip_DECC.Name = "tab_Knife_Marquip_DECC";
      this.tab_Knife_Marquip_DECC.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Knife_Marquip_DECC.Size = new System.Drawing.Size(1162, 488);
      this.tab_Knife_Marquip_DECC.TabIndex = 3;
      this.tab_Knife_Marquip_DECC.Text = "Knife Marquip DECC";
      this.tab_Knife_Marquip_DECC.UseVisualStyleBackColor = true;
      // 
      // CfgMarquipDeccCommsGrid
      // 
      this.CfgMarquipDeccCommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMarquipDeccCommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMarquipDeccCommsGrid.Location = new System.Drawing.Point(573, 46);
      this.CfgMarquipDeccCommsGrid.Name = "CfgMarquipDeccCommsGrid";
      this.CfgMarquipDeccCommsGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgMarquipDeccCommsGrid.Size = new System.Drawing.Size(503, 399);
      this.CfgMarquipDeccCommsGrid.TabIndex = 122;
      this.CfgMarquipDeccCommsGrid.ToolbarVisible = false;
      this.CfgMarquipDeccCommsGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgMarquipDeccCommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgMarquipDeccCommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgMarquipDeccGrid
      // 
      this.CfgMarquipDeccGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMarquipDeccGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgMarquipDeccGrid.Location = new System.Drawing.Point(87, 46);
      this.CfgMarquipDeccGrid.Name = "CfgMarquipDeccGrid";
      this.CfgMarquipDeccGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgMarquipDeccGrid.Size = new System.Drawing.Size(480, 399);
      this.CfgMarquipDeccGrid.TabIndex = 121;
      this.CfgMarquipDeccGrid.ToolbarVisible = false;
      this.CfgMarquipDeccGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgMarquipDeccGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgMarquipDeccGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Knife_LOGIMAC
      // 
      this.tab_Knife_LOGIMAC.Controls.Add(this.CfgLOGIMACCommsGrid);
      this.tab_Knife_LOGIMAC.Controls.Add(this.CfgLOGIMACGrid);
      this.tab_Knife_LOGIMAC.Location = new System.Drawing.Point(4, 27);
      this.tab_Knife_LOGIMAC.Name = "tab_Knife_LOGIMAC";
      this.tab_Knife_LOGIMAC.Size = new System.Drawing.Size(1162, 488);
      this.tab_Knife_LOGIMAC.TabIndex = 5;
      this.tab_Knife_LOGIMAC.Text = "Knife LOGIMAC";
      this.tab_Knife_LOGIMAC.UseVisualStyleBackColor = true;
      // 
      // CfgLOGIMACCommsGrid
      // 
      this.CfgLOGIMACCommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgLOGIMACCommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgLOGIMACCommsGrid.Location = new System.Drawing.Point(573, 46);
      this.CfgLOGIMACCommsGrid.Name = "CfgLOGIMACCommsGrid";
      this.CfgLOGIMACCommsGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgLOGIMACCommsGrid.Size = new System.Drawing.Size(503, 399);
      this.CfgLOGIMACCommsGrid.TabIndex = 124;
      this.CfgLOGIMACCommsGrid.ToolbarVisible = false;
      this.CfgLOGIMACCommsGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgLOGIMACCommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgLOGIMACCommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgLOGIMACGrid
      // 
      this.CfgLOGIMACGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgLOGIMACGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgLOGIMACGrid.Location = new System.Drawing.Point(87, 46);
      this.CfgLOGIMACGrid.Name = "CfgLOGIMACGrid";
      this.CfgLOGIMACGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgLOGIMACGrid.Size = new System.Drawing.Size(480, 399);
      this.CfgLOGIMACGrid.TabIndex = 123;
      this.CfgLOGIMACGrid.ToolbarVisible = false;
      this.CfgLOGIMACGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgLOGIMACGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgLOGIMACGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Knife_BSCOM_Gapo
      // 
      this.tab_Knife_BSCOM_Gapo.Controls.Add(this.CfgBHSGrid);
      this.tab_Knife_BSCOM_Gapo.Controls.Add(this.CfgBSCOMGapoCommsGrid);
      this.tab_Knife_BSCOM_Gapo.Location = new System.Drawing.Point(4, 27);
      this.tab_Knife_BSCOM_Gapo.Name = "tab_Knife_BSCOM_Gapo";
      this.tab_Knife_BSCOM_Gapo.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Knife_BSCOM_Gapo.Size = new System.Drawing.Size(1162, 488);
      this.tab_Knife_BSCOM_Gapo.TabIndex = 6;
      this.tab_Knife_BSCOM_Gapo.Text = "Knife BSCOM Gapo";
      this.tab_Knife_BSCOM_Gapo.UseVisualStyleBackColor = true;
      // 
      // CfgBHSGrid
      // 
      this.CfgBHSGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgBHSGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgBHSGrid.Location = new System.Drawing.Point(87, 46);
      this.CfgBHSGrid.Name = "CfgBHSGrid";
      this.CfgBHSGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgBHSGrid.Size = new System.Drawing.Size(480, 399);
      this.CfgBHSGrid.TabIndex = 124;
      this.CfgBHSGrid.ToolbarVisible = false;
      this.CfgBHSGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgBHSGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgBHSGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgBSCOMGapoCommsGrid
      // 
      this.CfgBSCOMGapoCommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgBSCOMGapoCommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgBSCOMGapoCommsGrid.Location = new System.Drawing.Point(573, 46);
      this.CfgBSCOMGapoCommsGrid.Name = "CfgBSCOMGapoCommsGrid";
      this.CfgBSCOMGapoCommsGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgBSCOMGapoCommsGrid.Size = new System.Drawing.Size(503, 399);
      this.CfgBSCOMGapoCommsGrid.TabIndex = 123;
      this.CfgBSCOMGapoCommsGrid.ToolbarVisible = false;
      this.CfgBSCOMGapoCommsGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgBSCOMGapoCommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgBSCOMGapoCommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tabKnife_MHI_58E
      // 
      this.tabKnife_MHI_58E.Controls.Add(this.CfgKNIFE_MHI_58E_CtrlGrid);
      this.tabKnife_MHI_58E.Location = new System.Drawing.Point(4, 27);
      this.tabKnife_MHI_58E.Name = "tabKnife_MHI_58E";
      this.tabKnife_MHI_58E.Size = new System.Drawing.Size(1162, 488);
      this.tabKnife_MHI_58E.TabIndex = 7;
      this.tabKnife_MHI_58E.Text = "Knife MHI 58E Ctrl";
      this.tabKnife_MHI_58E.UseVisualStyleBackColor = true;
      // 
      // CfgKNIFE_MHI_58E_CtrlGrid
      // 
      this.CfgKNIFE_MHI_58E_CtrlGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgKNIFE_MHI_58E_CtrlGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgKNIFE_MHI_58E_CtrlGrid.Location = new System.Drawing.Point(341, 45);
      this.CfgKNIFE_MHI_58E_CtrlGrid.Name = "CfgKNIFE_MHI_58E_CtrlGrid";
      this.CfgKNIFE_MHI_58E_CtrlGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgKNIFE_MHI_58E_CtrlGrid.Size = new System.Drawing.Size(480, 399);
      this.CfgKNIFE_MHI_58E_CtrlGrid.TabIndex = 118;
      this.CfgKNIFE_MHI_58E_CtrlGrid.ToolbarVisible = false;
      this.CfgKNIFE_MHI_58E_CtrlGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgKNIFE_MHI_58E_CtrlGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgKNIFE_MHI_58E_CtrlGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // Frm_KnifeSettingsTask
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tab_Settings);
      this.Name = "Frm_KnifeSettingsTask";
      this.Text = "Frm_KnifeSettingsTask";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.tab_Settings, 0);
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
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.tab_Settings.ResumeLayout(false);
      this.tab_Knife_Fosber_21_11.ResumeLayout(false);
      this.tab_Knife_Fosber_21_20.ResumeLayout(false);
      this.tab_Knife_Fosber_21_30.ResumeLayout(false);
      this.tab_Knife_Fosber_21_11_BMB.ResumeLayout(false);
      this.tab_Knife_Marquip_DECC.ResumeLayout(false);
      this.tab_Knife_LOGIMAC.ResumeLayout(false);
      this.tab_Knife_BSCOM_Gapo.ResumeLayout(false);
      this.tabKnife_MHI_58E.ResumeLayout(false);
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
					if (this.modDigitec21_11 )
					{
						cfg.KNIFEDigitec21_11CommsSetting  = (KNIFE21_11DigitecCommsSettings )this.CfgFosber21_11CommsGrid.SelectedObject;
						cfg.KNIFEDigitec21_11CommsSetting.Save();
						cfg.KNIFEDigitec21_11Setting = (KNIFE21_11DigitecSettings)this.CfgFosber21_11Grid.SelectedObject;
						cfg.KNIFEDigitec21_11Setting.Save();
					}

					if (this.modDigitec21_20)
					{
						cfg.KNIFEDigitec21_20CommsSetting = (KNIFE21_20DigitecCommsSettings)this.CfgFosber21_20CommsGrid.SelectedObject;
						cfg.KNIFEDigitec21_20CommsSetting.Save();
						cfg.KNIFEDigitec21_20Setting = (KNIFE21_20DigitecSettings)this.CfgFosber21_20Grid.SelectedObject;
						cfg.KNIFEDigitec21_20Setting.Save();
					}

					if (this.modDigitec21_30)
					{
						cfg.KNIFE21_30CommsSetting = (KNIFE21_30CommsSettings)this.CfgFosber21_30CommsGrid.SelectedObject;
						cfg.KNIFE21_30CommsSetting.Save();
						cfg.KNIFE21_30Setting = (KNIFE21_30Settings)this.CfgFosber21_30Grid.SelectedObject;
						cfg.KNIFE21_30Setting.Save();
					}

          //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Electric
          //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Comms
          if (this.modLOGIMAC)
          {
            cfg.KNIFELogimacComms_Settings = (KnifeLogimacComms_Settings)this.CfgLOGIMACCommsGrid.SelectedObject;
            cfg.KNIFELogimacComms_Settings.Save();
            cfg.KNIFELogimac_Setting = (KnifeLogimac_Settings)this.CfgLOGIMACGrid.SelectedObject;
            cfg.KNIFELogimac_Setting.Save();
          }

          //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task MHI 58E Ctrl
          if (this.modKnifeMhi58ECtrl)
          {
            cfg.KNIFE_Mhi_58E_CtrlSettings = (KNIFE_MHI_58E_CtrlSettings)this.CfgKNIFE_MHI_58E_CtrlGrid.SelectedObject;
            cfg.KNIFE_Mhi_58E_CtrlSettings.Save();
          }
      
          //PMASSARO_FI21/03/08 - ADDED FOR MARQUIP DECC
          if (this.modMarquipDECC)
          {
            cfg.KNIFEMarquipDeccCommsSetting = (KNIFE_MarquipDeccCommsSettings)this.CfgMarquipDeccCommsGrid.SelectedObject;
            cfg.KNIFEMarquipDeccCommsSetting.Save();
            cfg.KNIFEMarquipSetting = (KNIFE_MarquipSettings)this.CfgMarquipDeccGrid.SelectedObject;
            cfg.KNIFEMarquipSetting.Save();
          }

          //ABorelli_FI01/06/2008_1
          if (this.modBMB21_11)
          {
            cfg.KNIFEBmb21_11CommsSetting = (KNIFE21_11BmbCommsSettings)this.CfgFosber21_11CommsBMBGrid.SelectedObject;
            cfg.KNIFEBmb21_11CommsSetting.Save();
            cfg.KNIFEBmb21_11Setting = (KNIFE21_11BmbSettings)this.CfgFosber21_11BMBGrid.SelectedObject;
            cfg.KNIFEBmb21_11Setting.Save();
          }
          
          //agrassi_FI18-03-2010_1
          if (this.modBSCOM)
          {
            cfg.KnifeBSCOMGapoCommsSetting = (KnifeBSCOMGapoCommsSettings)this.CfgBSCOMGapoCommsGrid.SelectedObject;
            cfg.KnifeBSCOMGapoCommsSetting.Save();
            //mlamioni_FI30-04-2010
            cfg.KnifeBHSSetting = (KnifeBHSSettings)this.CfgBHSGrid.SelectedObject;
            cfg.KnifeBHSSetting.Save();
          }


					cfg.SendRelodSettings();
					this.refreshData();

					this.CfgFosber21_11CommsGrid.ViewForeColor = Color.DimGray;
					this.CfgFosber21_11Grid.ViewForeColor = Color.DimGray;
					this.CfgFosber21_20CommsGrid.ViewForeColor = Color.DimGray;
					this.CfgFosber21_20Grid.ViewForeColor = Color.DimGray;
					this.CfgFosber21_30CommsGrid.ViewForeColor = Color.DimGray;
					this.CfgFosber21_30Grid.ViewForeColor = Color.DimGray;

          //PMASSARO_FI21/03/08 - ADDED FOR MARQUIP DECC
          this.CfgMarquipDeccCommsGrid.ViewForeColor = Color.DimGray;
          this.CfgMarquipDeccGrid.ViewForeColor = Color.DimGray;

          //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Electric
          //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Comms
          this.CfgLOGIMACCommsGrid.ViewForeColor = Color.DimGray;
          this.CfgLOGIMACGrid.ViewForeColor = Color.DimGray;

          //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task MHI 58E Ctrl
          this.CfgKNIFE_MHI_58E_CtrlGrid.ViewForeColor = Color.DimGray;          

          //ABorelli_FI01/06/2008_1
          this.CfgFosber21_11CommsBMBGrid.ViewForeColor = Color.DimGray;
          this.CfgFosber21_11BMBGrid.ViewForeColor = Color.DimGray;
          //agrassi_FI18-03-2010_1
          this.CfgBSCOMGapoCommsGrid.ViewForeColor = Color.DimGray;
          this.CfgBHSGrid.ViewForeColor = Color.DimGray;

          lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(209, 127, 129); //Color.FromArgb(224, 224, 224);

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
					this.CfgFosber21_11CommsGrid.ViewForeColor = Color.Black;
					this.CfgFosber21_11Grid.ViewForeColor = Color.Black;
					this.CfgFosber21_20CommsGrid.ViewForeColor = Color.Black;
					this.CfgFosber21_20Grid.ViewForeColor = Color.Black; 
					this.CfgFosber21_30CommsGrid.ViewForeColor = Color.Black;
					this.CfgFosber21_30Grid.ViewForeColor = Color.Black;
          //PMASSARO_FI21/03/08 - ADDED FOR MARQUIP DECC
          this.CfgMarquipDeccCommsGrid.ViewForeColor = Color.Black;
          this.CfgMarquipDeccGrid.ViewForeColor = Color.Black;

          //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Electric
          //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Comms
          this.CfgLOGIMACCommsGrid.ViewForeColor = Color.Black;
          this.CfgLOGIMACGrid.ViewForeColor = Color.Black;

          //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task MHI 58E Ctrl
          this.CfgKNIFE_MHI_58E_CtrlGrid.ViewForeColor = Color.Black;

          //ABorelli_FI01/06/2008_1
          this.CfgFosber21_11CommsBMBGrid.ViewForeColor = Color.Black;
          this.CfgFosber21_11BMBGrid.ViewForeColor = Color.Black;
          //agrassi_FI18-03-2010_1
          this.CfgBSCOMGapoCommsGrid.ViewForeColor = Color.Black;
          this.CfgBHSGrid.ViewForeColor = Color.Black;

					this.modify = true;
					this.modDigitec21_11 = false;
					this.modDigitec21_20 = false;
					this.modDigitec21_30 = false;
          //PMASSARO_FI21/03/08 - ADDED FOR MARQUIP DECC
          this.modMarquipDECC = false;
          //ABorelli_FI01/06/2008_1
          this.modBMB21_11 = false;
          
          //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Electric
          //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Comms
          this.modLOGIMAC = false;
          //agrassi_FI18-03-2010_1
          this.modBSCOM = false;

          //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task MHI 58E Ctrl
          this.modKnifeMhi58ECtrl = false;
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
				this.CfgFosber21_11CommsGrid.ViewForeColor = Color.DimGray;
				this.CfgFosber21_11Grid.ViewForeColor = Color.DimGray;
				this.CfgFosber21_20CommsGrid.ViewForeColor = Color.DimGray;
				this.CfgFosber21_20Grid.ViewForeColor = Color.DimGray;
				this.CfgFosber21_30CommsGrid.ViewForeColor = Color.DimGray;
				this.CfgFosber21_30Grid.ViewForeColor = Color.DimGray;
        //PMASSARO_FI21/03/08 - ADDED FOR MARQUIP DECC
        this.CfgMarquipDeccCommsGrid.ViewForeColor = Color.DimGray;
        this.CfgMarquipDeccGrid.ViewForeColor = Color.DimGray;

        //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Electric
        //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Comms
        this.CfgLOGIMACCommsGrid.ViewForeColor = Color.DimGray;
        this.CfgLOGIMACGrid.ViewForeColor = Color.DimGray;

        //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task MHI 58E Ctrl
        this.CfgKNIFE_MHI_58E_CtrlGrid.ViewForeColor = Color.DimGray;

        //ABorelli_FI01/06/2008_1
        this.CfgFosber21_11CommsBMBGrid.ViewForeColor = Color.DimGray;
        this.CfgFosber21_11BMBGrid.ViewForeColor = Color.DimGray;
        //agrassi_FI18-03-2010_1
        this.CfgBSCOMGapoCommsGrid.ViewForeColor = Color.DimGray;
        this.CfgBHSGrid.ViewForeColor = Color.DimGray;

        lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(209, 127, 129); //Color.FromArgb(224, 224, 224);
				this.refreshData();
			}
		}

		private void CfgGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				if (((PropertyGrid)s).Equals(this.CfgFosber21_11CommsGrid) || ((PropertyGrid)s).Equals(this.CfgFosber21_11Grid))
				{
					this.modDigitec21_11 = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgFosber21_20CommsGrid) || ((PropertyGrid)s).Equals(this.CfgFosber21_20Grid))
				{
					this.modDigitec21_20 = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgFosber21_30CommsGrid) || ((PropertyGrid)s).Equals(this.CfgFosber21_30Grid))
				{
					this.modDigitec21_30 = true;
				}
        //PMASSARO_FI21/03/08 - ADDED FOR MARQUIP DECC
        if (((PropertyGrid)s).Equals(this.CfgMarquipDeccCommsGrid) || ((PropertyGrid)s).Equals(this.CfgMarquipDeccGrid))
        {
            this.modMarquipDECC = true;
        }

        //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Electric
        //PMASSARO_FI04/01/10 - Aggiunto task LOGIMAC Comms
        if (((PropertyGrid)s).Equals(this.CfgLOGIMACCommsGrid) || ((PropertyGrid)s).Equals(this.CfgLOGIMACGrid))
        {
          this.modLOGIMAC = true;
        }

        //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task MHI 58E Ctrl
        if (((PropertyGrid)s).Equals(this.CfgKNIFE_MHI_58E_CtrlGrid))
        {
          this.modKnifeMhi58ECtrl = true;
        }

        //ABorelli_FI01/06/2008_1
        if (((PropertyGrid)s).Equals(this.CfgFosber21_11CommsBMBGrid) || ((PropertyGrid)s).Equals(this.CfgFosber21_11BMBGrid))
        {
            this.modBMB21_11 = true;
			  }
        //agrassi_FI18-03-2010_1
        if (((PropertyGrid)s).Equals(this.CfgBSCOMGapoCommsGrid))
        {
          this.modBSCOM = true;
        }
      }
		}
	}
}

