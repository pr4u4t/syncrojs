using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase.SyncroConfig;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SlitterScorer.Settings
{
	public class Frm_SlitterScorerSettingsTask : SyncroC.Forms.Pages.SlitterScorer.Settings.Frm_SlitterScorerSettingsSubgroup
	{
		private TabControl tab_Settings;
		private TabPage tab_SS_TwinE;
		private TabPage tab_Twin22_60;
		private TabPage tab_Compact2;

		private PropertyGrid CfgSSTwinECommsGrid;
		private PropertyGrid CfgSSTwinEGrid;
		private PropertyGrid CfgSSTwin22_60CommsGrid;
		private PropertyGrid CfgSSTwin22_60Grid;
		private PropertyGrid CfgSSCompact22_2XCommsGrid;
		private PropertyGrid CfgSSCompact22_2XGrid;

		private System.ComponentModel.IContainer components = null;

		private SyncroConfig cfg;
		private bool modify = false;
		private bool modTwinE = false;
		private bool modTwin22_60 = false;
    //PMASSARO_FI30/11/08 - Added Compact 22.2X Mac Comms Task Settings
    private bool modCompact22_2XMacComms = false;
    private bool modCompact2 = false;
    //agrassi_FI25-05-2011_1
    private bool modTwin2 = false, modPaddles = false;

    private TabPage tab_Compact2MacComms;
    private PropertyGrid CfgSSCompact22_2XMacCommsGrid;
    private PropertyGrid CfgSSCompact22_2XMacGrid;
    private TabPage tab_Twin2;
    private TabPage tab_Paddles;
    private PropertyGrid CfgSSTwin22_40CommsGrid;
    private PropertyGrid CfgSSTwin22_40Grid;
    private PropertyGrid CfgPaddlesGrid;
		

		public Frm_SlitterScorerSettingsTask()
		{
			InitializeComponent();
			cfg = DBManager.getDBManager().syncroConfig;
		}

		public override void refreshData()
		{
			SyncroConfig cfg = DBManager.getDBManager().syncroConfig;

			this.CfgSSTwinECommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SSTwinE22_50CommsSettings)cfg.TwinECommsSetting.CreateCopy();
			this.CfgSSTwinEGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SSTwinESettings)cfg.TwinESetting.CreateCopy();
			this.CfgSSTwin22_60CommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SSTwin22_60CommsSettings)cfg.Twin22_60CommsSetting.CreateCopy();
			this.CfgSSTwin22_60Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SSTwin22_60Settings)cfg.Twin22_60Setting.CreateCopy();
			this.CfgSSCompact22_2XCommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SSCompact22_2XCommsSettings)cfg.Compact2CommsSetting.CreateCopy();
			this.CfgSSCompact22_2XGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SSCompact22_2XSettings)cfg.Compact2Setting.CreateCopy();
            
      //PMASSARO_FI30/11/08 - Added Compact 22.2X Mac Comms Task Settings
      this.CfgSSCompact22_2XMacGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SSCompact22_2XSettings)cfg.Compact2Setting.CreateCopy();
      this.CfgSSCompact22_2XMacCommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SSCompact22_2XMacCommsSettings)cfg.Compact2MacCommsSetting.CreateCopy();
      //agrassi_FI25-05-2011_1
      this.CfgSSTwin22_40Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SSTwin22_40Settings)cfg.Twin22_40Setting.CreateCopy();
      this.CfgSSTwin22_40CommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SSTwin22_40CommsSettings)cfg.Twin22_40CommsSetting.CreateCopy();
      this.CfgPaddlesGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.SSPaddlesSettings)cfg.PaddlesSettings.CreateCopy();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SlitterScorerSettingsTask));
      this.tab_Settings = new System.Windows.Forms.TabControl();
      this.tab_SS_TwinE = new System.Windows.Forms.TabPage();
      this.CfgSSTwinECommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgSSTwinEGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Twin22_60 = new System.Windows.Forms.TabPage();
      this.CfgSSTwin22_60CommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgSSTwin22_60Grid = new System.Windows.Forms.PropertyGrid();
      this.tab_Compact2 = new System.Windows.Forms.TabPage();
      this.CfgSSCompact22_2XCommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgSSCompact22_2XGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Compact2MacComms = new System.Windows.Forms.TabPage();
      this.CfgSSCompact22_2XMacCommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgSSCompact22_2XMacGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_Twin2 = new System.Windows.Forms.TabPage();
      this.tab_Paddles = new System.Windows.Forms.TabPage();
      this.CfgSSTwin22_40CommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgSSTwin22_40Grid = new System.Windows.Forms.PropertyGrid();
      this.CfgPaddlesGrid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_Settings.SuspendLayout();
      this.tab_SS_TwinE.SuspendLayout();
      this.tab_Twin22_60.SuspendLayout();
      this.tab_Compact2.SuspendLayout();
      this.tab_Compact2MacComms.SuspendLayout();
      this.tab_Twin2.SuspendLayout();
      this.tab_Paddles.SuspendLayout();
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
      this.tab_Settings.Controls.Add(this.tab_SS_TwinE);
      this.tab_Settings.Controls.Add(this.tab_Twin22_60);
      this.tab_Settings.Controls.Add(this.tab_Compact2);
      this.tab_Settings.Controls.Add(this.tab_Compact2MacComms);
      this.tab_Settings.Controls.Add(this.tab_Twin2);
      this.tab_Settings.Controls.Add(this.tab_Paddles);
      this.tab_Settings.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Settings.Location = new System.Drawing.Point(32, 193);
      this.tab_Settings.Name = "tab_Settings";
      this.tab_Settings.SelectedIndex = 0;
      this.tab_Settings.Size = new System.Drawing.Size(1171, 519);
      this.tab_Settings.TabIndex = 120;
      // 
      // tab_SS_TwinE
      // 
      this.tab_SS_TwinE.Controls.Add(this.CfgSSTwinECommsGrid);
      this.tab_SS_TwinE.Controls.Add(this.CfgSSTwinEGrid);
      this.tab_SS_TwinE.Location = new System.Drawing.Point(4, 27);
      this.tab_SS_TwinE.Name = "tab_SS_TwinE";
      this.tab_SS_TwinE.Padding = new System.Windows.Forms.Padding(3);
      this.tab_SS_TwinE.Size = new System.Drawing.Size(1163, 488);
      this.tab_SS_TwinE.TabIndex = 2;
      this.tab_SS_TwinE.Text = "TwinE 22.50";
      this.tab_SS_TwinE.UseVisualStyleBackColor = true;
      // 
      // CfgSSTwinECommsGrid
      // 
      this.CfgSSTwinECommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSTwinECommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSTwinECommsGrid.Location = new System.Drawing.Point(590, 46);
      this.CfgSSTwinECommsGrid.Name = "CfgSSTwinECommsGrid";
      this.CfgSSTwinECommsGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSSTwinECommsGrid.Size = new System.Drawing.Size(540, 399);
      this.CfgSSTwinECommsGrid.TabIndex = 120;
      this.CfgSSTwinECommsGrid.ToolbarVisible = false;
      this.CfgSSTwinECommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSSTwinECommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgSSTwinEGrid
      // 
      this.CfgSSTwinEGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSTwinEGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSTwinEGrid.Location = new System.Drawing.Point(30, 46);
      this.CfgSSTwinEGrid.Name = "CfgSSTwinEGrid";
      this.CfgSSTwinEGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSSTwinEGrid.Size = new System.Drawing.Size(540, 399);
      this.CfgSSTwinEGrid.TabIndex = 119;
      this.CfgSSTwinEGrid.ToolbarVisible = false;
      this.CfgSSTwinEGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgSSTwinEGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSSTwinEGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Twin22_60
      // 
      this.tab_Twin22_60.Controls.Add(this.CfgSSTwin22_60CommsGrid);
      this.tab_Twin22_60.Controls.Add(this.CfgSSTwin22_60Grid);
      this.tab_Twin22_60.Location = new System.Drawing.Point(4, 27);
      this.tab_Twin22_60.Name = "tab_Twin22_60";
      this.tab_Twin22_60.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Twin22_60.Size = new System.Drawing.Size(1163, 488);
      this.tab_Twin22_60.TabIndex = 3;
      this.tab_Twin22_60.Text = "Twin 22.60";
      this.tab_Twin22_60.UseVisualStyleBackColor = true;
      // 
      // CfgSSTwin22_60CommsGrid
      // 
      this.CfgSSTwin22_60CommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSTwin22_60CommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSTwin22_60CommsGrid.Location = new System.Drawing.Point(590, 46);
      this.CfgSSTwin22_60CommsGrid.Name = "CfgSSTwin22_60CommsGrid";
      this.CfgSSTwin22_60CommsGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSSTwin22_60CommsGrid.Size = new System.Drawing.Size(540, 399);
      this.CfgSSTwin22_60CommsGrid.TabIndex = 122;
      this.CfgSSTwin22_60CommsGrid.ToolbarVisible = false;
      this.CfgSSTwin22_60CommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSSTwin22_60CommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgSSTwin22_60Grid
      // 
      this.CfgSSTwin22_60Grid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSTwin22_60Grid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSTwin22_60Grid.Location = new System.Drawing.Point(30, 46);
      this.CfgSSTwin22_60Grid.Name = "CfgSSTwin22_60Grid";
      this.CfgSSTwin22_60Grid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSSTwin22_60Grid.Size = new System.Drawing.Size(540, 399);
      this.CfgSSTwin22_60Grid.TabIndex = 121;
      this.CfgSSTwin22_60Grid.ToolbarVisible = false;
      this.CfgSSTwin22_60Grid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgSSTwin22_60Grid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSSTwin22_60Grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Compact2
      // 
      this.tab_Compact2.Controls.Add(this.CfgSSCompact22_2XCommsGrid);
      this.tab_Compact2.Controls.Add(this.CfgSSCompact22_2XGrid);
      this.tab_Compact2.Location = new System.Drawing.Point(4, 27);
      this.tab_Compact2.Name = "tab_Compact2";
      this.tab_Compact2.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Compact2.Size = new System.Drawing.Size(1163, 488);
      this.tab_Compact2.TabIndex = 4;
      this.tab_Compact2.Text = "Compact 22.2X";
      this.tab_Compact2.UseVisualStyleBackColor = true;
      // 
      // CfgSSCompact22_2XCommsGrid
      // 
      this.CfgSSCompact22_2XCommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSCompact22_2XCommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSCompact22_2XCommsGrid.Location = new System.Drawing.Point(590, 46);
      this.CfgSSCompact22_2XCommsGrid.Name = "CfgSSCompact22_2XCommsGrid";
      this.CfgSSCompact22_2XCommsGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSSCompact22_2XCommsGrid.Size = new System.Drawing.Size(540, 399);
      this.CfgSSCompact22_2XCommsGrid.TabIndex = 122;
      this.CfgSSCompact22_2XCommsGrid.ToolbarVisible = false;
      this.CfgSSCompact22_2XCommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSSCompact22_2XCommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgSSCompact22_2XGrid
      // 
      this.CfgSSCompact22_2XGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSCompact22_2XGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSCompact22_2XGrid.Location = new System.Drawing.Point(30, 46);
      this.CfgSSCompact22_2XGrid.Name = "CfgSSCompact22_2XGrid";
      this.CfgSSCompact22_2XGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSSCompact22_2XGrid.Size = new System.Drawing.Size(540, 399);
      this.CfgSSCompact22_2XGrid.TabIndex = 121;
      this.CfgSSCompact22_2XGrid.ToolbarVisible = false;
      this.CfgSSCompact22_2XGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgSSCompact22_2XGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSSCompact22_2XGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Compact2MacComms
      // 
      this.tab_Compact2MacComms.Controls.Add(this.CfgSSCompact22_2XMacCommsGrid);
      this.tab_Compact2MacComms.Controls.Add(this.CfgSSCompact22_2XMacGrid);
      this.tab_Compact2MacComms.Location = new System.Drawing.Point(4, 27);
      this.tab_Compact2MacComms.Name = "tab_Compact2MacComms";
      this.tab_Compact2MacComms.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Compact2MacComms.Size = new System.Drawing.Size(1163, 488);
      this.tab_Compact2MacComms.TabIndex = 5;
      this.tab_Compact2MacComms.Text = "Compact2MacComms";
      this.tab_Compact2MacComms.UseVisualStyleBackColor = true;
      // 
      // CfgSSCompact22_2XMacCommsGrid
      // 
      this.CfgSSCompact22_2XMacCommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSCompact22_2XMacCommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSCompact22_2XMacCommsGrid.Location = new System.Drawing.Point(590, 46);
      this.CfgSSCompact22_2XMacCommsGrid.Name = "CfgSSCompact22_2XMacCommsGrid";
      this.CfgSSCompact22_2XMacCommsGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSSCompact22_2XMacCommsGrid.Size = new System.Drawing.Size(540, 399);
      this.CfgSSCompact22_2XMacCommsGrid.TabIndex = 124;
      this.CfgSSCompact22_2XMacCommsGrid.ToolbarVisible = false;
      this.CfgSSCompact22_2XMacCommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSSCompact22_2XMacCommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgSSCompact22_2XMacGrid
      // 
      this.CfgSSCompact22_2XMacGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSCompact22_2XMacGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSCompact22_2XMacGrid.Location = new System.Drawing.Point(30, 46);
      this.CfgSSCompact22_2XMacGrid.Name = "CfgSSCompact22_2XMacGrid";
      this.CfgSSCompact22_2XMacGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSSCompact22_2XMacGrid.Size = new System.Drawing.Size(540, 399);
      this.CfgSSCompact22_2XMacGrid.TabIndex = 123;
      this.CfgSSCompact22_2XMacGrid.ToolbarVisible = false;
      this.CfgSSCompact22_2XMacGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgSSCompact22_2XMacGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSSCompact22_2XMacGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Twin2
      // 
      this.tab_Twin2.Controls.Add(this.CfgSSTwin22_40CommsGrid);
      this.tab_Twin2.Controls.Add(this.CfgSSTwin22_40Grid);
      this.tab_Twin2.Location = new System.Drawing.Point(4, 27);
      this.tab_Twin2.Name = "tab_Twin2";
      this.tab_Twin2.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Twin2.Size = new System.Drawing.Size(1163, 488);
      this.tab_Twin2.TabIndex = 6;
      this.tab_Twin2.Text = "Twin2 22.40";
      this.tab_Twin2.UseVisualStyleBackColor = true;
      // 
      // tab_Paddles
      // 
      this.tab_Paddles.Controls.Add(this.CfgPaddlesGrid);
      this.tab_Paddles.Location = new System.Drawing.Point(4, 27);
      this.tab_Paddles.Name = "tab_Paddles";
      this.tab_Paddles.Size = new System.Drawing.Size(1163, 488);
      this.tab_Paddles.TabIndex = 8;
      this.tab_Paddles.Text = "Paddles";
      this.tab_Paddles.UseVisualStyleBackColor = true;
      // 
      // CfgSSTwin22_40CommsGrid
      // 
      this.CfgSSTwin22_40CommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSTwin22_40CommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSTwin22_40CommsGrid.Location = new System.Drawing.Point(591, 45);
      this.CfgSSTwin22_40CommsGrid.Name = "CfgSSTwin22_40CommsGrid";
      this.CfgSSTwin22_40CommsGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSSTwin22_40CommsGrid.Size = new System.Drawing.Size(540, 399);
      this.CfgSSTwin22_40CommsGrid.TabIndex = 124;
      this.CfgSSTwin22_40CommsGrid.ToolbarVisible = false;
      this.CfgSSTwin22_40CommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSSTwin22_40CommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgSSTwin22_40Grid
      // 
      this.CfgSSTwin22_40Grid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSTwin22_40Grid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgSSTwin22_40Grid.Location = new System.Drawing.Point(31, 45);
      this.CfgSSTwin22_40Grid.Name = "CfgSSTwin22_40Grid";
      this.CfgSSTwin22_40Grid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgSSTwin22_40Grid.Size = new System.Drawing.Size(540, 399);
      this.CfgSSTwin22_40Grid.TabIndex = 123;
      this.CfgSSTwin22_40Grid.ToolbarVisible = false;
      this.CfgSSTwin22_40Grid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgSSTwin22_40Grid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgSSTwin22_40Grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgPaddlesGrid
      // 
      this.CfgPaddlesGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgPaddlesGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgPaddlesGrid.Location = new System.Drawing.Point(311, 45);
      this.CfgPaddlesGrid.Name = "CfgPaddlesGrid";
      this.CfgPaddlesGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.CfgPaddlesGrid.Size = new System.Drawing.Size(540, 399);
      this.CfgPaddlesGrid.TabIndex = 124;
      this.CfgPaddlesGrid.ToolbarVisible = false;
      this.CfgPaddlesGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgPaddlesGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgPaddlesGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // Frm_SlitterScorerSettingsTask
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tab_Settings);
      this.Name = "Frm_SlitterScorerSettingsTask";
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
      this.tab_SS_TwinE.ResumeLayout(false);
      this.tab_Twin22_60.ResumeLayout(false);
      this.tab_Compact2.ResumeLayout(false);
      this.tab_Compact2MacComms.ResumeLayout(false);
      this.tab_Twin2.ResumeLayout(false);
      this.tab_Paddles.ResumeLayout(false);
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
					if (this.modTwinE )
					{
						cfg.TwinECommsSetting = (SSTwinE22_50CommsSettings)this.CfgSSTwinECommsGrid.SelectedObject;
						cfg.TwinECommsSetting.Save();
						cfg.TwinESetting = (SSTwinESettings)this.CfgSSTwinEGrid.SelectedObject;
						cfg.TwinESetting.Save();
					}

					if (this.modTwin22_60)
					{
						cfg.Twin22_60CommsSetting = (SSTwin22_60CommsSettings)this.CfgSSTwin22_60CommsGrid.SelectedObject;
						cfg.Twin22_60CommsSetting.Save();
						cfg.Twin22_60Setting  = (SSTwin22_60Settings)this.CfgSSTwin22_60Grid.SelectedObject;
						cfg.Twin22_60Setting.Save();
					}

					if (this.modCompact2)
					{
						cfg.Compact2CommsSetting = (SSCompact22_2XCommsSettings)this.CfgSSCompact22_2XCommsGrid.SelectedObject;
						cfg.Compact2CommsSetting.Save();
						cfg.Compact2Setting = (SSCompact22_2XSettings)this.CfgSSCompact22_2XGrid.SelectedObject;
						cfg.Compact2Setting.Save();
					}

          //PMASSARO_FI30/11/08 - Added Compact 22.2X Mac Comms Task Settings
          if (this.modCompact22_2XMacComms)
					{
            cfg.Compact2MacCommsSetting = (SSCompact22_2XMacCommsSettings)this.CfgSSCompact22_2XMacCommsGrid.SelectedObject;
            cfg.Compact2MacCommsSetting.Save();
						cfg.Compact2Setting = (SSCompact22_2XSettings)this.CfgSSCompact22_2XMacGrid.SelectedObject;
						cfg.Compact2Setting.Save();
					}

          //agrassi_FI25-05-2011_1
          if (this.modTwin2)
          {
            cfg.Twin22_40Setting = (SSTwin22_40Settings)this.CfgSSTwin22_40Grid.SelectedObject;
            cfg.Twin22_40Setting.Save();
            cfg.Twin22_40CommsSetting = (SSTwin22_40CommsSettings)this.CfgSSTwin22_40CommsGrid.SelectedObject;
            cfg.Twin22_40CommsSetting.Save();
          }

          if (this.modPaddles)
          {
            cfg.PaddlesSettings = (SSPaddlesSettings)this.CfgPaddlesGrid.SelectedObject;
            cfg.PaddlesSettings.Save();
          }

					cfg.SendRelodSettings();
					this.refreshData();

					this.CfgSSTwinECommsGrid.ViewForeColor = Color.DimGray;
					this.CfgSSTwinEGrid.ViewForeColor = Color.DimGray;
					this.CfgSSTwin22_60CommsGrid.ViewForeColor = Color.DimGray;
					this.CfgSSTwin22_60Grid.ViewForeColor = Color.DimGray;
					this.CfgSSCompact22_2XCommsGrid.ViewForeColor = Color.DimGray;
					this.CfgSSCompact22_2XGrid.ViewForeColor = Color.DimGray;

          //PMASSARO_FI30/11/08 - Added Compact 22.2X Mac Comms Task Settings
          this.CfgSSCompact22_2XMacCommsGrid.ViewForeColor = Color.DimGray;
          this.CfgSSCompact22_2XMacGrid.ViewForeColor = Color.DimGray;
          //agrassi_FI25-05-2011_1
          this.CfgSSTwin22_40Grid.ViewForeColor = Color.DimGray;
          this.CfgSSTwin22_40CommsGrid.ViewForeColor = Color.DimGray;
          this.CfgPaddlesGrid.ViewForeColor = Color.DimGray;

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
					
					this.CfgSSTwinECommsGrid.ViewForeColor = Color.Black ;
					this.CfgSSTwinEGrid.ViewForeColor = Color.Black ;
					this.CfgSSTwin22_60CommsGrid.ViewForeColor = Color.Black ;
					this.CfgSSTwin22_60Grid.ViewForeColor = Color.Black ;
					this.CfgSSCompact22_2XCommsGrid.ViewForeColor = Color.Black;
					this.CfgSSCompact22_2XGrid.ViewForeColor = Color.Black;
          //PMASSARO_FI30/11/08 - Added Compact 22.2X Mac Comms Task Settings
          this.CfgSSCompact22_2XMacCommsGrid.ViewForeColor = Color.Black;
          this.CfgSSCompact22_2XMacGrid.ViewForeColor = Color.Black;

          //agrassi_FI25-05-2011_1
          this.CfgSSTwin22_40Grid.ViewForeColor = Color.Black;
          this.CfgSSTwin22_40CommsGrid.ViewForeColor = Color.Black;
          this.CfgPaddlesGrid.ViewForeColor = Color.Black;
          
          this.modify = true;
					this.modTwinE = false;
					this.modTwin22_60 = false;
					this.modCompact2 = false;
          //PMASSARO_FI30/11/08 - Added Compact 22.2X Mac Comms Task Settings
          this.modCompact22_2XMacComms = false;
          //agrassi_FI25-05-2011_1
          this.modTwin2 = false;
          this.modPaddles = false;
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

				this.CfgSSTwinECommsGrid.ViewForeColor = Color.DimGray;
				this.CfgSSTwinEGrid.ViewForeColor = Color.DimGray;
				this.CfgSSTwin22_60CommsGrid.ViewForeColor = Color.DimGray;
				this.CfgSSTwin22_60Grid.ViewForeColor = Color.DimGray;
				this.CfgSSCompact22_2XCommsGrid.ViewForeColor = Color.DimGray;
				this.CfgSSCompact22_2XGrid.ViewForeColor = Color.DimGray;

        //PMASSARO_FI30/11/08 - Added Compact 22.2X Mac Comms Task Settings
        this.CfgSSCompact22_2XMacCommsGrid.ViewForeColor = Color.DimGray;
        this.CfgSSCompact22_2XMacGrid.ViewForeColor = Color.DimGray;

        //agrassi_FI25-05-2011_1
        this.CfgSSTwin22_40Grid.ViewForeColor = Color.DimGray;
        this.CfgSSTwin22_40CommsGrid.ViewForeColor = Color.DimGray;
        this.CfgPaddlesGrid.ViewForeColor = Color.DimGray;
        
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
				if (((PropertyGrid)s).Equals(this.CfgSSTwinECommsGrid ) || ((PropertyGrid)s).Equals(this.CfgSSTwinEGrid ))
				{
					this.modTwinE = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgSSTwin22_60CommsGrid ) || ((PropertyGrid)s).Equals(this.CfgSSTwin22_60Grid ))
				{
					this.modTwin22_60  = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgSSCompact22_2XCommsGrid) || ((PropertyGrid)s).Equals(this.CfgSSCompact22_2XGrid))
				{
					this.modCompact2 = true;
				}

        //PMASSARO_FI30/11/08 - Added Compact 22.2X Mac Comms Task Settings
        if (((PropertyGrid)s).Equals(this.CfgSSCompact22_2XMacCommsGrid) || ((PropertyGrid)s).Equals(this.CfgSSCompact22_2XMacGrid))
        {
            this.modCompact22_2XMacComms = true;
        }
        //agrassi_FI25-05-2011_1
        if (((PropertyGrid)s).Equals(this.CfgSSTwin22_40CommsGrid) || ((PropertyGrid)s).Equals(this.CfgSSTwin22_40CommsGrid))
        {
          this.modTwin2 = true;
        }
        if (((PropertyGrid)s).Equals(this.CfgPaddlesGrid))
        {
          this.modPaddles = true;
        }
      }
		}
	}
}

