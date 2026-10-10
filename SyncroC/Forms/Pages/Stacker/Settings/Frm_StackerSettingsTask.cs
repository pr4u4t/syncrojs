using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.DataBase.SyncroConfig;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Stacker.Settings
{
	public class Frm_StackerSettingsTask : SyncroC.Forms.Pages.Stacker.Settings.Frm_StackerSettingsSubgroup
	{
		private TabControl tab_Settings;

		private TabPage tab_Terminal20_11;
		private TabPage tab_Terminal20_20;
		private TabPage tab_Terminal400;
		private TabPage tab_NewEngineering;
    //ABorelli_FI01/06/2008_1
    private TabPage tab_FavalessaLRE;

		private PropertyGrid CfgTerminal20_11Grid;
		private PropertyGrid CfgTerminal20_11CommGrid;
		private PropertyGrid CfgTerminal20_20CommGrid;
		private PropertyGrid CfgTerminal20_20Grid;
		private PropertyGrid CfgTerminal400CommGrid;
		private PropertyGrid CfgTerminal400Grid;
		private PropertyGrid CfgStackerNECommsGrid;
		private PropertyGrid CfgStackerNEGrid;
    //ABorelli_FI01/06/2008_1
    private PropertyGrid CfgStackerLRECommsGrid;
    private PropertyGrid CfgStackerLREGrid;

		private System.ComponentModel.IContainer components = null;

		private bool modify = false;
		private bool modSTK20_11 = false;
		private bool modSTK20_20 = false;
		private bool modSTK400 = false;
		private bool modSTK_NE = false;
    //ABorelli_FI01/06/2008_1
    private bool modSTK_LRE = false;
    //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
    private bool modSTK_GenericCTRL = false;

    //PMASSARO_FI18/08/08 - 1 - Added To Manage Universal Stacker Of Job 3742_Drezdenko
    //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)
    private bool modSTK_Universal = false;
    //agrassi_FI26-04-2010_1
    private bool modSTK_BSCOM_GAPO = false;

    //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
    private bool modFanFoldHOETTEN = false;

    private TabPage tab_GenericCTRL;
    private PropertyGrid CfgStackerGenericCTRL_CommsGrid;
    private PropertyGrid CfgStackerGenericCTRLGrid;
    private TabPage tab_StackerUniversal;
    private PropertyGrid CfgStackerUniversalCTRLGrid;
    private PropertyGrid CfgStackerUniversalGrid;
    private TabPage tab_BSCOMGAPO;
    private PropertyGrid CfgStackerBSCOMGAPOCTRLGrid;
    private PropertyGrid CfgStackerBSCOMGAPOGrid;
    private TabPage tabFanfoldHoetten;
    private PropertyGrid CfgFanfoldHoettenCTRLGrid;

    //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
    private bool modSTK_Marquip = false;
    private TabPage tab_StackerMarquip;
    private PropertyGrid CfgStackerMarquip_CommsGrid;
    private PropertyGrid CfgStackerMarquipGrid;

		private SyncroConfig cfg;

		public Frm_StackerSettingsTask()
		{
			InitializeComponent();
			cfg = DBManager.getDBManager().syncroConfig;
		}

		public Frm_StackerSettingsTask(int index)
			: this()
		{
			if (!(index == 1 || index == 2 || index == 3))
				Logger.LogError(this, "Frm_StackerSettingsTask - Invalid parameter: \"" + index.ToString() + "\". Only \"1\", \"2\", \"3\" values are allowed.");
			this.index = index;

			DBManager dbm = DBManager.getDBManager();
			level = (LevelName)index;
			if ((dbm.lineConfig.KnifeConfig.LevelCount == 2) && (index == 2))
				level = LevelName.Upper;

			lbl_Title_YN.Text += " " + level.ToString();
		}

		public override void refreshData()
		{
			SyncroConfig cfg = DBManager.getDBManager().syncroConfig;

      //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
      this.CfgStackerGenericCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_20Settings)cfg.StkTerminal20_20Settings.CreateCopy();
			this.CfgTerminal20_11Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_20Settings)cfg.StkTerminal20_20Settings.CreateCopy();
			this.CfgTerminal20_20Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_20Settings)cfg.StkTerminal20_20Settings.CreateCopy();
			this.CfgTerminal400Grid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_20Settings)cfg.StkTerminal20_20Settings.CreateCopy();
			this.CfgStackerNEGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_20Settings)cfg.StkTerminal20_20Settings.CreateCopy();
            
      //PMASSARO_FI18/08/08 - 1 - Added To Manage Universal Stacker Of Job 3742_Drezdenko
      this.CfgStackerUniversalGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKStackerUniversalSettings)cfg.StkStackerUniversalSettings.CreateCopy();

      //ABorelli_FI01/06/2008_1
      this.CfgStackerLREGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_20Settings)cfg.StkTerminal20_20Settings.CreateCopy();
      //agrassi_FI26-04-2010_1
      this.CfgStackerBSCOMGAPOCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKBSCOMGapoCommsSettings)cfg.StkBSCOMGapoCommsSetting.CreateCopy();
      this.CfgStackerBSCOMGAPOGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_20Settings)cfg.StkTerminal20_20Settings.CreateCopy();

      //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
      this.CfgStackerMarquip_CommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STK_MarquipDeccCommsSettings)cfg.StkMarquipCommsSetting.CreateCopy();
      this.CfgStackerMarquipGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_20Settings)cfg.StkTerminal20_20Settings.CreateCopy();

      if (index == 1)
			{
        //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
        this.CfgStackerGenericCTRL_CommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STK_GenericCTRLSettings)cfg.STKGenericCTRL1Settings.CreateCopy();

				this.CfgTerminal20_11CommGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_11CommsSettings)cfg.StkTerminal20_11Level1Settings.CreateCopy();
				this.CfgTerminal20_20CommGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_20CommsSettings)cfg.StkTerminal20_20Level1Settings.CreateCopy();
				this.CfgTerminal400CommGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal400CommsSettings)cfg.StkTerminal400Level1Settings.CreateCopy();
				this.CfgStackerNECommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STK_NE_CommsSettings)cfg.Stk_NE_Level1Settings.CreateCopy();
        //ABorelli_FI01/06/2008_1
        this.CfgStackerLRECommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STK_LRE_CommsSettings)cfg.Stk_LRE_Level1Settings.CreateCopy();

        //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)	       
        this.CfgStackerUniversalCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKStackerUniversalCtrlSettings)cfg.StkStackerUniversalLevel1CtrlSettings.CreateCopy();

        //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
        this.CfgFanfoldHoettenCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.FanFoldHOETTENCommsSettings)cfg.FanFoldHoettenCommsSettings1.CreateCopy();
			}
			else if (index == 2)
			{
        //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
        this.CfgStackerGenericCTRL_CommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STK_GenericCTRLSettings)cfg.STKGenericCTRL2Settings.CreateCopy();

				this.CfgTerminal20_11CommGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_11CommsSettings)cfg.StkTerminal20_11Level2Settings.CreateCopy();
				this.CfgTerminal20_20CommGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_20CommsSettings)cfg.StkTerminal20_20Level2Settings.CreateCopy();
				this.CfgTerminal400CommGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal400CommsSettings)cfg.StkTerminal400Level2Settings.CreateCopy();
				this.CfgStackerNECommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STK_NE_CommsSettings)cfg.Stk_NE_Level2Settings.CreateCopy();
        //ABorelli_FI01/06/2008_1
        this.CfgStackerLRECommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STK_LRE_CommsSettings)cfg.Stk_LRE_Level2Settings.CreateCopy();

        //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)	       
        this.CfgStackerUniversalCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKStackerUniversalCtrlSettings)cfg.StkStackerUniversalLevel2CtrlSettings.CreateCopy();

        //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
        this.CfgFanfoldHoettenCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.FanFoldHOETTENCommsSettings)cfg.FanFoldHoettenCommsSettings2.CreateCopy();
			}
			else
			{
        //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
        this.CfgStackerGenericCTRL_CommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STK_GenericCTRLSettings)cfg.STKGenericCTRL3Settings.CreateCopy();

				this.CfgTerminal20_11CommGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_11CommsSettings)cfg.StkTerminal20_11Level3Settings.CreateCopy();
				this.CfgTerminal20_20CommGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal20_20CommsSettings)cfg.StkTerminal20_20Level3Settings.CreateCopy();
				this.CfgTerminal400CommGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKTerminal400CommsSettings)cfg.StkTerminal400Level3Settings.CreateCopy();
				this.CfgStackerNECommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STK_NE_CommsSettings)cfg.Stk_NE_Level3Settings.CreateCopy();
        //ABorelli_FI01/06/2008_1
        this.CfgStackerLRECommsGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STK_LRE_CommsSettings)cfg.Stk_LRE_Level3Settings.CreateCopy();

        //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)	       
        this.CfgStackerUniversalCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.STKStackerUniversalCtrlSettings)cfg.StkStackerUniversalLevel3CtrlSettings.CreateCopy();

        //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
        this.CfgFanfoldHoettenCTRLGrid.SelectedObject = (SyncroC.DataBase.SyncroConfig.FanFoldHOETTENCommsSettings)cfg.FanFoldHoettenCommsSettings3.CreateCopy();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StackerSettingsTask));
      this.tab_Settings = new System.Windows.Forms.TabControl();
      this.tab_Terminal20_11 = new System.Windows.Forms.TabPage();
      this.CfgTerminal20_11CommGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgTerminal20_11Grid = new System.Windows.Forms.PropertyGrid();
      this.tab_Terminal20_20 = new System.Windows.Forms.TabPage();
      this.CfgTerminal20_20CommGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgTerminal20_20Grid = new System.Windows.Forms.PropertyGrid();
      this.tab_Terminal400 = new System.Windows.Forms.TabPage();
      this.CfgTerminal400CommGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgTerminal400Grid = new System.Windows.Forms.PropertyGrid();
      this.tab_NewEngineering = new System.Windows.Forms.TabPage();
      this.CfgStackerNECommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgStackerNEGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_FavalessaLRE = new System.Windows.Forms.TabPage();
      this.CfgStackerLRECommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgStackerLREGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_GenericCTRL = new System.Windows.Forms.TabPage();
      this.CfgStackerGenericCTRL_CommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgStackerGenericCTRLGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_StackerUniversal = new System.Windows.Forms.TabPage();
      this.CfgStackerUniversalCTRLGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgStackerUniversalGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_BSCOMGAPO = new System.Windows.Forms.TabPage();
      this.CfgStackerBSCOMGAPOCTRLGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgStackerBSCOMGAPOGrid = new System.Windows.Forms.PropertyGrid();
      this.tabFanfoldHoetten = new System.Windows.Forms.TabPage();
      this.CfgFanfoldHoettenCTRLGrid = new System.Windows.Forms.PropertyGrid();
      //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
      this.tab_StackerMarquip = new System.Windows.Forms.TabPage();
      this.CfgStackerMarquip_CommsGrid = new System.Windows.Forms.PropertyGrid();
      this.CfgStackerMarquipGrid = new System.Windows.Forms.PropertyGrid();
      
    
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_Settings.SuspendLayout();
      this.tab_Terminal20_11.SuspendLayout();
      this.tab_Terminal20_20.SuspendLayout();
      this.tab_Terminal400.SuspendLayout();
      this.tab_NewEngineering.SuspendLayout();
      this.tab_FavalessaLRE.SuspendLayout();
      this.tab_GenericCTRL.SuspendLayout();
      this.tab_StackerUniversal.SuspendLayout();
      this.tab_BSCOMGAPO.SuspendLayout();
      this.tabFanfoldHoetten.SuspendLayout();
      //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
      this.tab_StackerMarquip.SuspendLayout();
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
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // tab_Settings
      // 
      this.tab_Settings.Controls.Add(this.tab_Terminal20_11);
      this.tab_Settings.Controls.Add(this.tab_Terminal20_20);
      this.tab_Settings.Controls.Add(this.tab_Terminal400);
      this.tab_Settings.Controls.Add(this.tab_NewEngineering);
      this.tab_Settings.Controls.Add(this.tab_FavalessaLRE);
      this.tab_Settings.Controls.Add(this.tab_GenericCTRL);
      this.tab_Settings.Controls.Add(this.tab_StackerUniversal);
      this.tab_Settings.Controls.Add(this.tab_BSCOMGAPO);
      this.tab_Settings.Controls.Add(this.tabFanfoldHoetten);
      //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
      this.tab_Settings.Controls.Add(this.tab_StackerMarquip);

      this.tab_Settings.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tab_Settings.Location = new System.Drawing.Point(30, 194);
      this.tab_Settings.Name = "tab_Settings";
      this.tab_Settings.SelectedIndex = 0;
      this.tab_Settings.Size = new System.Drawing.Size(1171, 519);
      this.tab_Settings.TabIndex = 120;
      // 
      // tab_Terminal20_11
      // 
      this.tab_Terminal20_11.Controls.Add(this.CfgTerminal20_11CommGrid);
      this.tab_Terminal20_11.Controls.Add(this.CfgTerminal20_11Grid);
      this.tab_Terminal20_11.Location = new System.Drawing.Point(4, 27);
      this.tab_Terminal20_11.Name = "tab_Terminal20_11";
      this.tab_Terminal20_11.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Terminal20_11.Size = new System.Drawing.Size(1163, 488);
      this.tab_Terminal20_11.TabIndex = 14;
      this.tab_Terminal20_11.Text = "TERMINAL 20.11";
      this.tab_Terminal20_11.UseVisualStyleBackColor = true;
      // 
      // CfgTerminal20_11CommGrid
      // 
      this.CfgTerminal20_11CommGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgTerminal20_11CommGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgTerminal20_11CommGrid.Location = new System.Drawing.Point(593, 52);
      this.CfgTerminal20_11CommGrid.Name = "CfgTerminal20_11CommGrid";
      this.CfgTerminal20_11CommGrid.Size = new System.Drawing.Size(503, 386);
      this.CfgTerminal20_11CommGrid.TabIndex = 124;
      this.CfgTerminal20_11CommGrid.ToolbarVisible = false;
      this.CfgTerminal20_11CommGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgTerminal20_11CommGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgTerminal20_11CommGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgTerminal20_11Grid
      // 
      this.CfgTerminal20_11Grid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgTerminal20_11Grid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgTerminal20_11Grid.Location = new System.Drawing.Point(66, 52);
      this.CfgTerminal20_11Grid.Name = "CfgTerminal20_11Grid";
      this.CfgTerminal20_11Grid.Size = new System.Drawing.Size(510, 386);
      this.CfgTerminal20_11Grid.TabIndex = 123;
      this.CfgTerminal20_11Grid.ToolbarVisible = false;
      this.CfgTerminal20_11Grid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgTerminal20_11Grid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgTerminal20_11Grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Terminal20_20
      // 
      this.tab_Terminal20_20.Controls.Add(this.CfgTerminal20_20CommGrid);
      this.tab_Terminal20_20.Controls.Add(this.CfgTerminal20_20Grid);
      this.tab_Terminal20_20.Location = new System.Drawing.Point(4, 27);
      this.tab_Terminal20_20.Name = "tab_Terminal20_20";
      this.tab_Terminal20_20.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Terminal20_20.Size = new System.Drawing.Size(1163, 488);
      this.tab_Terminal20_20.TabIndex = 15;
      this.tab_Terminal20_20.Text = "TERMINAL 20.20";
      this.tab_Terminal20_20.UseVisualStyleBackColor = true;
      // 
      // CfgTerminal20_20CommGrid
      // 
      this.CfgTerminal20_20CommGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgTerminal20_20CommGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgTerminal20_20CommGrid.Location = new System.Drawing.Point(593, 52);
      this.CfgTerminal20_20CommGrid.Name = "CfgTerminal20_20CommGrid";
      this.CfgTerminal20_20CommGrid.Size = new System.Drawing.Size(503, 386);
      this.CfgTerminal20_20CommGrid.TabIndex = 126;
      this.CfgTerminal20_20CommGrid.ToolbarVisible = false;
      this.CfgTerminal20_20CommGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgTerminal20_20CommGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgTerminal20_20CommGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgTerminal20_20Grid
      // 
      this.CfgTerminal20_20Grid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgTerminal20_20Grid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgTerminal20_20Grid.Location = new System.Drawing.Point(66, 52);
      this.CfgTerminal20_20Grid.Name = "CfgTerminal20_20Grid";
      this.CfgTerminal20_20Grid.Size = new System.Drawing.Size(510, 386);
      this.CfgTerminal20_20Grid.TabIndex = 125;
      this.CfgTerminal20_20Grid.ToolbarVisible = false;
      this.CfgTerminal20_20Grid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgTerminal20_20Grid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgTerminal20_20Grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_Terminal400
      // 
      this.tab_Terminal400.Controls.Add(this.CfgTerminal400CommGrid);
      this.tab_Terminal400.Controls.Add(this.CfgTerminal400Grid);
      this.tab_Terminal400.Location = new System.Drawing.Point(4, 27);
      this.tab_Terminal400.Name = "tab_Terminal400";
      this.tab_Terminal400.Padding = new System.Windows.Forms.Padding(3);
      this.tab_Terminal400.Size = new System.Drawing.Size(1163, 488);
      this.tab_Terminal400.TabIndex = 16;
      this.tab_Terminal400.Text = "TERMINAL 400";
      this.tab_Terminal400.UseVisualStyleBackColor = true;
      // 
      // CfgTerminal400CommGrid
      // 
      this.CfgTerminal400CommGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgTerminal400CommGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgTerminal400CommGrid.Location = new System.Drawing.Point(593, 52);
      this.CfgTerminal400CommGrid.Name = "CfgTerminal400CommGrid";
      this.CfgTerminal400CommGrid.Size = new System.Drawing.Size(503, 386);
      this.CfgTerminal400CommGrid.TabIndex = 128;
      this.CfgTerminal400CommGrid.ToolbarVisible = false;
      this.CfgTerminal400CommGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgTerminal400CommGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgTerminal400CommGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgTerminal400Grid
      // 
      this.CfgTerminal400Grid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgTerminal400Grid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgTerminal400Grid.Location = new System.Drawing.Point(66, 52);
      this.CfgTerminal400Grid.Name = "CfgTerminal400Grid";
      this.CfgTerminal400Grid.Size = new System.Drawing.Size(510, 386);
      this.CfgTerminal400Grid.TabIndex = 127;
      this.CfgTerminal400Grid.ToolbarVisible = false;
      this.CfgTerminal400Grid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgTerminal400Grid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgTerminal400Grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_NewEngineering
      // 
      this.tab_NewEngineering.Controls.Add(this.CfgStackerNECommsGrid);
      this.tab_NewEngineering.Controls.Add(this.CfgStackerNEGrid);
      this.tab_NewEngineering.Location = new System.Drawing.Point(4, 27);
      this.tab_NewEngineering.Name = "tab_NewEngineering";
      this.tab_NewEngineering.Padding = new System.Windows.Forms.Padding(3);
      this.tab_NewEngineering.Size = new System.Drawing.Size(1163, 488);
      this.tab_NewEngineering.TabIndex = 17;
      this.tab_NewEngineering.Text = "New Engineering";
      this.tab_NewEngineering.UseVisualStyleBackColor = true;
      // 
      // CfgStackerNECommsGrid
      // 
      this.CfgStackerNECommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerNECommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerNECommsGrid.Location = new System.Drawing.Point(593, 52);
      this.CfgStackerNECommsGrid.Name = "CfgStackerNECommsGrid";
      this.CfgStackerNECommsGrid.Size = new System.Drawing.Size(503, 386);
      this.CfgStackerNECommsGrid.TabIndex = 130;
      this.CfgStackerNECommsGrid.ToolbarVisible = false;
      this.CfgStackerNECommsGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStackerNECommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStackerNECommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgStackerNEGrid
      // 
      this.CfgStackerNEGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerNEGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerNEGrid.Location = new System.Drawing.Point(66, 52);
      this.CfgStackerNEGrid.Name = "CfgStackerNEGrid";
      this.CfgStackerNEGrid.Size = new System.Drawing.Size(510, 386);
      this.CfgStackerNEGrid.TabIndex = 129;
      this.CfgStackerNEGrid.ToolbarVisible = false;
      this.CfgStackerNEGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStackerNEGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStackerNEGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_FavalessaLRE
      // 
      this.tab_FavalessaLRE.Controls.Add(this.CfgStackerLRECommsGrid);
      this.tab_FavalessaLRE.Controls.Add(this.CfgStackerLREGrid);
      this.tab_FavalessaLRE.Location = new System.Drawing.Point(4, 27);
      this.tab_FavalessaLRE.Name = "tab_FavalessaLRE";
      this.tab_FavalessaLRE.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.tab_FavalessaLRE.Size = new System.Drawing.Size(1163, 488);
      this.tab_FavalessaLRE.TabIndex = 18;
      this.tab_FavalessaLRE.Text = "Stacker Favalessa LRE";
      this.tab_FavalessaLRE.UseVisualStyleBackColor = true;
      // 
      // CfgStackerLRECommsGrid
      // 
      this.CfgStackerLRECommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerLRECommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerLRECommsGrid.Location = new System.Drawing.Point(593, 52);
      this.CfgStackerLRECommsGrid.Name = "CfgStackerLRECommsGrid";
      this.CfgStackerLRECommsGrid.Size = new System.Drawing.Size(503, 386);
      this.CfgStackerLRECommsGrid.TabIndex = 130;
      this.CfgStackerLRECommsGrid.ToolbarVisible = false;
      this.CfgStackerLRECommsGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStackerLRECommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStackerLRECommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgStackerLREGrid
      // 
      this.CfgStackerLREGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerLREGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerLREGrid.Location = new System.Drawing.Point(66, 52);
      this.CfgStackerLREGrid.Name = "CfgStackerLREGrid";
      this.CfgStackerLREGrid.Size = new System.Drawing.Size(510, 386);
      this.CfgStackerLREGrid.TabIndex = 129;
      this.CfgStackerLREGrid.ToolbarVisible = false;
      this.CfgStackerLREGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStackerLREGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStackerLREGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_GenericCTRL
      // 
      this.tab_GenericCTRL.Controls.Add(this.CfgStackerGenericCTRL_CommsGrid);
      this.tab_GenericCTRL.Controls.Add(this.CfgStackerGenericCTRLGrid);
      this.tab_GenericCTRL.Location = new System.Drawing.Point(4, 27);
      this.tab_GenericCTRL.Name = "tab_GenericCTRL";
      this.tab_GenericCTRL.Padding = new System.Windows.Forms.Padding(3);
      this.tab_GenericCTRL.Size = new System.Drawing.Size(1163, 488);
      this.tab_GenericCTRL.TabIndex = 18;
      this.tab_GenericCTRL.Text = "Stacker Generic CTRL";
      this.tab_GenericCTRL.UseVisualStyleBackColor = true;
      // 
      // CfgStackerGenericCTRL_CommsGrid
      // 
      this.CfgStackerGenericCTRL_CommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerGenericCTRL_CommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerGenericCTRL_CommsGrid.Location = new System.Drawing.Point(593, 52);
      this.CfgStackerGenericCTRL_CommsGrid.Name = "CfgStackerGenericCTRL_CommsGrid";
      this.CfgStackerGenericCTRL_CommsGrid.Size = new System.Drawing.Size(503, 386);
      this.CfgStackerGenericCTRL_CommsGrid.TabIndex = 132;
      this.CfgStackerGenericCTRL_CommsGrid.ToolbarVisible = false;
      this.CfgStackerGenericCTRL_CommsGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStackerGenericCTRL_CommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStackerGenericCTRL_CommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgStackerGenericCTRLGrid
      // 
      this.CfgStackerGenericCTRLGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerGenericCTRLGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerGenericCTRLGrid.Location = new System.Drawing.Point(66, 52);
      this.CfgStackerGenericCTRLGrid.Name = "CfgStackerGenericCTRLGrid";
      this.CfgStackerGenericCTRLGrid.Size = new System.Drawing.Size(510, 386);
      this.CfgStackerGenericCTRLGrid.TabIndex = 131;
      this.CfgStackerGenericCTRLGrid.ToolbarVisible = false;
      this.CfgStackerGenericCTRLGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStackerGenericCTRLGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStackerGenericCTRLGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_StackerUniversal
      // 
      this.tab_StackerUniversal.Controls.Add(this.CfgStackerUniversalCTRLGrid);
      this.tab_StackerUniversal.Controls.Add(this.CfgStackerUniversalGrid);
      this.tab_StackerUniversal.Location = new System.Drawing.Point(4, 27);
      this.tab_StackerUniversal.Name = "tab_StackerUniversal";
      this.tab_StackerUniversal.Padding = new System.Windows.Forms.Padding(3);
      this.tab_StackerUniversal.Size = new System.Drawing.Size(1163, 488);
      this.tab_StackerUniversal.TabIndex = 19;
      this.tab_StackerUniversal.Text = "Stacker Universal";
      this.tab_StackerUniversal.UseVisualStyleBackColor = true;
      // 
      // CfgStackerUniversalCTRLGrid
      // 
      this.CfgStackerUniversalCTRLGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerUniversalCTRLGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerUniversalCTRLGrid.Location = new System.Drawing.Point(593, 52);
      this.CfgStackerUniversalCTRLGrid.Name = "CfgStackerUniversalCTRLGrid";
      this.CfgStackerUniversalCTRLGrid.Size = new System.Drawing.Size(503, 386);
      this.CfgStackerUniversalCTRLGrid.TabIndex = 126;
      this.CfgStackerUniversalCTRLGrid.ToolbarVisible = false;
      this.CfgStackerUniversalCTRLGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStackerUniversalCTRLGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStackerUniversalCTRLGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgStackerUniversalGrid
      // 
      this.CfgStackerUniversalGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerUniversalGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerUniversalGrid.Location = new System.Drawing.Point(66, 52);
      this.CfgStackerUniversalGrid.Name = "CfgStackerUniversalGrid";
      this.CfgStackerUniversalGrid.Size = new System.Drawing.Size(510, 386);
      this.CfgStackerUniversalGrid.TabIndex = 125;
      this.CfgStackerUniversalGrid.ToolbarVisible = false;
      this.CfgStackerUniversalGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStackerUniversalGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStackerUniversalGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_BSCOMGAPO
      // 
      this.tab_BSCOMGAPO.Controls.Add(this.CfgStackerBSCOMGAPOCTRLGrid);
      this.tab_BSCOMGAPO.Controls.Add(this.CfgStackerBSCOMGAPOGrid);
      this.tab_BSCOMGAPO.Location = new System.Drawing.Point(4, 27);
      this.tab_BSCOMGAPO.Name = "tab_BSCOMGAPO";
      this.tab_BSCOMGAPO.Size = new System.Drawing.Size(1163, 488);
      this.tab_BSCOMGAPO.TabIndex = 20;
      this.tab_BSCOMGAPO.Text = "Stacker BSCOM GAPO";
      this.tab_BSCOMGAPO.UseVisualStyleBackColor = true;
      // 
      // CfgStackerBSCOMGAPOCTRLGrid
      // 
      this.CfgStackerBSCOMGAPOCTRLGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerBSCOMGAPOCTRLGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerBSCOMGAPOCTRLGrid.Location = new System.Drawing.Point(593, 52);
      this.CfgStackerBSCOMGAPOCTRLGrid.Name = "CfgStackerBSCOMGAPOCTRLGrid";
      this.CfgStackerBSCOMGAPOCTRLGrid.Size = new System.Drawing.Size(503, 386);
      this.CfgStackerBSCOMGAPOCTRLGrid.TabIndex = 128;
      this.CfgStackerBSCOMGAPOCTRLGrid.ToolbarVisible = false;
      this.CfgStackerBSCOMGAPOCTRLGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStackerBSCOMGAPOCTRLGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStackerBSCOMGAPOCTRLGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // CfgStackerBSCOMGAPOGrid
      // 
      this.CfgStackerBSCOMGAPOGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerBSCOMGAPOGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerBSCOMGAPOGrid.Location = new System.Drawing.Point(66, 52);
      this.CfgStackerBSCOMGAPOGrid.Name = "CfgStackerBSCOMGAPOGrid";
      this.CfgStackerBSCOMGAPOGrid.Size = new System.Drawing.Size(510, 386);
      this.CfgStackerBSCOMGAPOGrid.TabIndex = 127;
      this.CfgStackerBSCOMGAPOGrid.ToolbarVisible = false;
      this.CfgStackerBSCOMGAPOGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStackerBSCOMGAPOGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStackerBSCOMGAPOGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tabFanfoldHoetten
      // 
      this.tabFanfoldHoetten.Controls.Add(this.CfgFanfoldHoettenCTRLGrid);
      this.tabFanfoldHoetten.Location = new System.Drawing.Point(4, 27);
      this.tabFanfoldHoetten.Name = "tabFanfoldHoetten";
      this.tabFanfoldHoetten.Size = new System.Drawing.Size(1163, 488);
      this.tabFanfoldHoetten.TabIndex = 21;
      this.tabFanfoldHoetten.Text = "Fanfold HOETTEN";
      this.tabFanfoldHoetten.UseVisualStyleBackColor = true;
      // 
      // CfgFanfoldHoettenCTRLGrid
      // 
      this.CfgFanfoldHoettenCTRLGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFanfoldHoettenCTRLGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgFanfoldHoettenCTRLGrid.Location = new System.Drawing.Point(326, 51);
      this.CfgFanfoldHoettenCTRLGrid.Name = "CfgFanfoldHoettenCTRLGrid";
      this.CfgFanfoldHoettenCTRLGrid.Size = new System.Drawing.Size(510, 386);
      this.CfgFanfoldHoettenCTRLGrid.TabIndex = 130;
      this.CfgFanfoldHoettenCTRLGrid.ToolbarVisible = false;
      this.CfgFanfoldHoettenCTRLGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgFanfoldHoettenCTRLGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
      // 
      // tab_StackerMarquip
      //
      this.tab_StackerMarquip.Controls.Add(this.CfgStackerMarquip_CommsGrid);
      this.tab_StackerMarquip.Controls.Add(this.CfgStackerMarquipGrid);
      this.tab_StackerMarquip.Location = new System.Drawing.Point(4, 27);
      this.tab_StackerMarquip.Name = "tab_StackerMarquip";
      this.tab_StackerMarquip.Size = new System.Drawing.Size(1163, 488);
      this.tab_StackerMarquip.TabIndex = 22;
      this.tab_StackerMarquip.Text = "Stacker Marquip";
      this.tab_StackerMarquip.UseVisualStyleBackColor = true;
      // 
      // CfgStackerMarquip_CommsGrid
      //
      this.CfgStackerMarquip_CommsGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerMarquip_CommsGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerMarquip_CommsGrid.Location = new System.Drawing.Point(593, 52);
      this.CfgStackerMarquip_CommsGrid.Name = "CfgStackerMarquip_CommsGrid";
      this.CfgStackerMarquip_CommsGrid.Size = new System.Drawing.Size(503, 386);
      this.CfgStackerMarquip_CommsGrid.TabIndex = 130;
      this.CfgStackerMarquip_CommsGrid.ToolbarVisible = false;
      this.CfgStackerMarquip_CommsGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStackerMarquip_CommsGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStackerMarquip_CommsGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);      
      // 
      // CfgStackerMarquipGrid
      //
      this.CfgStackerMarquipGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerMarquipGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.CfgStackerMarquipGrid.Location = new System.Drawing.Point(66, 52);
      this.CfgStackerMarquipGrid.Name = "CfgStackerMarquipGrid";
      this.CfgStackerMarquipGrid.Size = new System.Drawing.Size(510, 386);
      this.CfgStackerMarquipGrid.TabIndex = 129;
      this.CfgStackerMarquipGrid.ToolbarVisible = false;
      this.CfgStackerMarquipGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.CfgStackerMarquipGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.CfgStackerMarquipGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // Frm_StackerSettingsTask
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tab_Settings);
      this.Name = "Frm_StackerSettingsTask";
      this.Text = "Frm_StackerSettingsTask";
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
      this.tab_Terminal20_11.ResumeLayout(false);
      this.tab_Terminal20_20.ResumeLayout(false);
      this.tab_Terminal400.ResumeLayout(false);
      this.tab_NewEngineering.ResumeLayout(false);
      this.tab_FavalessaLRE.ResumeLayout(false);
      this.tab_GenericCTRL.ResumeLayout(false);
      this.tab_StackerUniversal.ResumeLayout(false);
      this.tab_BSCOMGAPO.ResumeLayout(false);
      this.tabFanfoldHoetten.ResumeLayout(false);
      //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
      this.tab_StackerMarquip.ResumeLayout(false);
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
					if (this.modSTK20_11)
					{
						cfg.StkTerminal20_20Settings = (STKTerminal20_20Settings)this.CfgTerminal20_11Grid.SelectedObject;
						cfg.StkTerminal20_20Settings.Save();
						if( index == 1)
						{
							cfg.StkTerminal20_11Level1Settings = (STKTerminal20_11CommsSettings)this.CfgTerminal20_11CommGrid.SelectedObject;
							cfg.StkTerminal20_11Level1Settings.Save();
						}
						else if (index == 2)
						{
							cfg.StkTerminal20_11Level2Settings = (STKTerminal20_11CommsSettings)this.CfgTerminal20_11CommGrid.SelectedObject;
							cfg.StkTerminal20_11Level2Settings.Save();
						}
						else
						{
							cfg.StkTerminal20_11Level3Settings = (STKTerminal20_11CommsSettings)this.CfgTerminal20_11CommGrid.SelectedObject;
							cfg.StkTerminal20_11Level3Settings.Save();
						}
					}

					if (this.modSTK20_20)
					{
						cfg.StkTerminal20_20Settings = (STKTerminal20_20Settings)this.CfgTerminal20_20Grid.SelectedObject;
						cfg.StkTerminal20_20Settings.Save();
						if (index == 1)
						{
							cfg.StkTerminal20_20Level1Settings = (STKTerminal20_20CommsSettings)this.CfgTerminal20_20CommGrid.SelectedObject;
							cfg.StkTerminal20_20Level1Settings.Save();
						}
						else if (index == 2)
						{
							cfg.StkTerminal20_20Level2Settings = (STKTerminal20_20CommsSettings)this.CfgTerminal20_20CommGrid.SelectedObject;
							cfg.StkTerminal20_20Level2Settings.Save();
						}
						else
						{
							cfg.StkTerminal20_20Level3Settings = (STKTerminal20_20CommsSettings)this.CfgTerminal20_20CommGrid.SelectedObject;
							cfg.StkTerminal20_20Level3Settings.Save();
						}
					}

					if (this.modSTK400)
					{
						cfg.StkTerminal20_20Settings = (STKTerminal20_20Settings)this.CfgTerminal400Grid.SelectedObject;
						cfg.StkTerminal20_20Settings.Save();
						if (index == 1)
						{
							cfg.StkTerminal400Level1Settings = (STKTerminal400CommsSettings)this.CfgTerminal400CommGrid.SelectedObject;
							cfg.StkTerminal400Level1Settings.Save();
						}
						else if (index == 2)
						{
							cfg.StkTerminal400Level2Settings = (STKTerminal400CommsSettings)this.CfgTerminal400CommGrid.SelectedObject;
							cfg.StkTerminal400Level2Settings.Save();
						}
						else
						{
							cfg.StkTerminal400Level3Settings = (STKTerminal400CommsSettings)this.CfgTerminal400CommGrid.SelectedObject;
							cfg.StkTerminal400Level3Settings.Save();
						}
					}

					if (this.modSTK_NE )
					{
						cfg.StkTerminal20_20Settings = (STKTerminal20_20Settings)this.CfgStackerNEGrid.SelectedObject;
						cfg.StkTerminal20_20Settings.Save();
						if (index == 1)
						{
							cfg.Stk_NE_Level1Settings = (STK_NE_CommsSettings)this.CfgStackerNECommsGrid.SelectedObject;
							cfg.Stk_NE_Level1Settings.Save();
						}
						else if (index == 2)
						{
							cfg.Stk_NE_Level2Settings = (STK_NE_CommsSettings)this.CfgStackerNECommsGrid.SelectedObject;
							cfg.Stk_NE_Level2Settings.Save();
						}
						else
						{
							cfg.Stk_NE_Level3Settings = (STK_NE_CommsSettings)this.CfgStackerNECommsGrid.SelectedObject;
							cfg.Stk_NE_Level3Settings.Save();
						}
					}
	        //ABorelli_FI01/06/2008_1
          if (this.modSTK_LRE)
          {
            cfg.StkTerminal20_20Settings = (STKTerminal20_20Settings)this.CfgStackerLREGrid.SelectedObject;
            cfg.StkTerminal20_20Settings.Save();
            if (index == 1)
            {
              cfg.Stk_LRE_Level1Settings = (STK_LRE_CommsSettings)this.CfgStackerLRECommsGrid.SelectedObject;
              cfg.Stk_LRE_Level1Settings.Save();
            }
            else if (index == 2)
            {
              cfg.Stk_LRE_Level2Settings = (STK_LRE_CommsSettings)this.CfgStackerLRECommsGrid.SelectedObject;
              cfg.Stk_LRE_Level2Settings.Save();
            }
            else
            {
              cfg.Stk_LRE_Level3Settings = (STK_LRE_CommsSettings)this.CfgStackerLRECommsGrid.SelectedObject;
              cfg.Stk_LRE_Level3Settings.Save();
            }
          }

          //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
          if (this.modSTK_GenericCTRL)
          {
            cfg.StkTerminal20_20Settings = (STKTerminal20_20Settings)this.CfgStackerGenericCTRLGrid.SelectedObject;
            cfg.StkTerminal20_20Settings.Save();
            if (index == 1)
            {
              cfg.STKGenericCTRL1Settings = (STK_GenericCTRLSettings)this.CfgStackerGenericCTRL_CommsGrid.SelectedObject;
              cfg.STKGenericCTRL1Settings.Save();
            }
            else if (index == 2)
            {
              cfg.STKGenericCTRL2Settings = (STK_GenericCTRLSettings)this.CfgStackerGenericCTRL_CommsGrid.SelectedObject;
              cfg.STKGenericCTRL2Settings.Save();
            }
            else
            {
              cfg.STKGenericCTRL3Settings = (STK_GenericCTRLSettings)this.CfgStackerGenericCTRL_CommsGrid.SelectedObject;
              cfg.STKGenericCTRL3Settings.Save();
            }
          }

          //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
          if (this.modFanFoldHOETTEN)
          {
            if (index == 1)
            {
              cfg.FanFoldHoettenCommsSettings1= (FanFoldHOETTENCommsSettings)this.CfgFanfoldHoettenCTRLGrid.SelectedObject;
              cfg.FanFoldHoettenCommsSettings1.Save();
            }
            else if (index == 2)
            {
              cfg.FanFoldHoettenCommsSettings2 = (FanFoldHOETTENCommsSettings)this.CfgFanfoldHoettenCTRLGrid.SelectedObject;
              cfg.FanFoldHoettenCommsSettings2.Save();
            }
            else
            {
              cfg.FanFoldHoettenCommsSettings3 = (FanFoldHOETTENCommsSettings)this.CfgFanfoldHoettenCTRLGrid.SelectedObject;
              cfg.FanFoldHoettenCommsSettings3.Save();
            }
          }

          if (this.modSTK_Universal)
          {
            cfg.StkStackerUniversalSettings = (STKStackerUniversalSettings)this.CfgStackerUniversalGrid.SelectedObject;
            cfg.StkStackerUniversalSettings.Save();

            if (index == 1)
            {
              cfg.StkStackerUniversalLevel1CtrlSettings = (STKStackerUniversalCtrlSettings)this.CfgStackerUniversalCTRLGrid.SelectedObject;
              cfg.StkStackerUniversalLevel1CtrlSettings.Save();
            }
            else if (index == 2)
            {
              cfg.StkStackerUniversalLevel2CtrlSettings = (STKStackerUniversalCtrlSettings)this.CfgStackerUniversalCTRLGrid.SelectedObject;
              cfg.StkStackerUniversalLevel2CtrlSettings.Save();
            }
            else
            {
              cfg.StkStackerUniversalLevel3CtrlSettings = (STKStackerUniversalCtrlSettings)this.CfgStackerUniversalCTRLGrid.SelectedObject;
              cfg.StkStackerUniversalLevel3CtrlSettings.Save();
            }
          }
          //agrassi_FI26-04-2010_1
          if (this.modSTK_BSCOM_GAPO)
          {
            cfg.StkTerminal20_20Settings = (STKTerminal20_20Settings)this.CfgStackerBSCOMGAPOGrid.SelectedObject;
            cfg.StkTerminal20_20Settings.Save();
            cfg.StkBSCOMGapoCommsSetting = (STKBSCOMGapoCommsSettings)this.CfgStackerBSCOMGAPOCTRLGrid.SelectedObject;
            cfg.StkBSCOMGapoCommsSetting.Save();
          }

          //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
          if (this.modSTK_Marquip)
          {
            cfg.StkTerminal20_20Settings = (STKTerminal20_20Settings)this.CfgStackerMarquipGrid.SelectedObject;
            cfg.StkTerminal20_20Settings.Save();
            cfg.StkMarquipCommsSetting = (STK_MarquipDeccCommsSettings)this.CfgStackerMarquip_CommsGrid.SelectedObject;
            cfg.StkMarquipCommsSetting.Save();
          }

					cfg.SendRelodSettings();
					this.refreshData();

          //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
          this.CfgStackerGenericCTRL_CommsGrid.ViewForeColor = Color.DimGray;
          this.CfgStackerGenericCTRLGrid.ViewForeColor = Color.DimGray;

          //PMASSARO_FI18/08/08 - 1 - Added To Manage Universal Stacker Of Job 3742_Drezdenko
          this.CfgStackerUniversalGrid.ViewForeColor = Color.DimGray;
          //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)
          this.CfgStackerUniversalCTRLGrid.ViewForeColor = Color.DimGray;

          //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
          this.CfgFanfoldHoettenCTRLGrid.ViewForeColor = Color.DimGray;

					this.CfgTerminal20_11Grid.ViewForeColor = Color.DimGray;
					this.CfgTerminal20_11CommGrid.ViewForeColor = Color.DimGray;
					this.CfgTerminal20_20Grid.ViewForeColor = Color.DimGray;
					this.CfgTerminal20_20CommGrid.ViewForeColor = Color.DimGray;
					this.CfgTerminal400Grid.ViewForeColor = Color.DimGray;
					this.CfgTerminal400CommGrid.ViewForeColor = Color.DimGray;
					this.CfgStackerNEGrid.ViewForeColor = Color.DimGray;
					this.CfgStackerNECommsGrid.ViewForeColor = Color.DimGray;
	        //ABorelli_FI01/06/2008_1
          this.CfgStackerLREGrid.ViewForeColor = Color.DimGray;
          this.CfgStackerLRECommsGrid.ViewForeColor = Color.DimGray;
          //agrassi_FI26-04-2010_1
          this.CfgStackerBSCOMGAPOCTRLGrid.ViewForeColor = Color.DimGray;
          this.CfgStackerBSCOMGAPOGrid.ViewForeColor = Color.DimGray;
          //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
          this.CfgStackerMarquipGrid.ViewForeColor = Color.DimGray;
          this.CfgStackerMarquip_CommsGrid.ViewForeColor = Color.DimGray;

          //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
          //correzione: rimaneva con sfondo errato
          //lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
          lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(152, 153, 199);

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

          //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
          this.CfgStackerGenericCTRL_CommsGrid.ViewForeColor = Color.Black;
          this.CfgStackerGenericCTRLGrid.ViewForeColor = Color.Black;

          //PMASSARO_FI18/08/08 - 1 - Added To Manage Universal Stacker Of Job 3742_Drezdenko
          this.CfgStackerUniversalGrid.ViewForeColor = Color.Black;
          //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)
          this.CfgStackerUniversalCTRLGrid.ViewForeColor = Color.Black;
          
          //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
          this.CfgFanfoldHoettenCTRLGrid.ViewForeColor = Color.Black;

					this.CfgTerminal20_11Grid.ViewForeColor = Color.Black ;
					this.CfgTerminal20_11CommGrid.ViewForeColor = Color.Black;
					this.CfgTerminal20_20Grid.ViewForeColor = Color.Black;
					this.CfgTerminal20_20CommGrid.ViewForeColor = Color.Black;
					this.CfgTerminal400Grid.ViewForeColor = Color.Black;
					this.CfgTerminal400CommGrid.ViewForeColor = Color.Black;
					this.CfgStackerNEGrid.ViewForeColor = Color.Black;
					this.CfgStackerNECommsGrid.ViewForeColor = Color.Black;
	        //ABorelli_FI01/06/2008_1
          this.CfgStackerLREGrid.ViewForeColor = Color.Black;
          this.CfgStackerLRECommsGrid.ViewForeColor = Color.Black;
          //agrassi_FI26-04-2010_1
          this.CfgStackerBSCOMGAPOCTRLGrid.ViewForeColor = Color.Black;
          this.CfgStackerBSCOMGAPOGrid.ViewForeColor = Color.Black;
          //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
          this.CfgStackerMarquipGrid.ViewForeColor = Color.Black;
          this.CfgStackerMarquip_CommsGrid.ViewForeColor = Color.Black;

					this.modify = true;
					this.modSTK20_11 = false;
					this.modSTK20_20 = false;
					this.modSTK400 = false;
					this.modSTK_NE = false;
	        //ABorelli_FI01/06/2008_1
          this.modSTK_LRE = false;
          //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
          this.modSTK_GenericCTRL = false;

          //PMASSARO_FI18/08/08 - 1 - Added To Manage Universal Stacker Of Job 3742_Drezdenko
          //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)
          this.modSTK_Universal = false;
          //agrassi_FI26-04-2010_1
          this.modSTK_BSCOM_GAPO = false;
          //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
          this.modSTK_Marquip = false;
          //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
          this.modFanFoldHOETTEN = false;
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
				this.CfgTerminal20_11Grid.ViewForeColor = Color.DimGray;
				this.CfgTerminal20_11CommGrid.ViewForeColor = Color.DimGray;
				this.CfgTerminal20_20Grid.ViewForeColor = Color.DimGray;
				this.CfgTerminal20_20CommGrid.ViewForeColor = Color.DimGray;
				this.CfgTerminal400Grid.ViewForeColor = Color.DimGray;
				this.CfgTerminal400CommGrid.ViewForeColor = Color.DimGray;
				this.CfgStackerNEGrid.ViewForeColor = Color.DimGray;
				this.CfgStackerNECommsGrid.ViewForeColor = Color.DimGray;
        //ABorelli_FI01/06/2008_1
        this.CfgStackerLREGrid.ViewForeColor = Color.DimGray;
        this.CfgStackerLRECommsGrid.ViewForeColor = Color.DimGray;
        //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
        this.CfgStackerGenericCTRL_CommsGrid.ViewForeColor = Color.DimGray;
        this.CfgStackerGenericCTRLGrid.ViewForeColor = Color.DimGray;
        
        //PMASSARO_FI18/08/08 - 1 - Added To Manage Universal Stacker Of Job 3742_Drezdenko
        this.CfgStackerUniversalGrid.ViewForeColor = Color.DimGray;
        //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)
        this.CfgStackerUniversalCTRLGrid.ViewForeColor = Color.DimGray;
        //agrassi_FI26-04-2010_1
        this.CfgStackerBSCOMGAPOCTRLGrid.ViewForeColor = Color.DimGray;
        this.CfgStackerBSCOMGAPOGrid.ViewForeColor = Color.DimGray;
        //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
        this.CfgStackerMarquipGrid.ViewForeColor = Color.DimGray;
        this.CfgStackerMarquip_CommsGrid.ViewForeColor = Color.DimGray;

        //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
        this.CfgFanfoldHoettenCTRLGrid.ViewForeColor = Color.DimGray;
        
        //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl
        //correzione: rimaneva lo sfondo errato
        //lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(224, 224, 224);
        lbl_CtrlF5_YY.BackColor = lbl_CtrlF5.BackColor = Color.FromArgb(152, 153, 199);
        this.refreshData();
			}
		}

		private void CfgGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
				if (((PropertyGrid)s).Equals(this.CfgTerminal20_11Grid) || ((PropertyGrid)s).Equals(this.CfgTerminal20_11CommGrid )  )
				{
					this.modSTK20_11 = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgTerminal20_20Grid) || ((PropertyGrid)s).Equals(this.CfgTerminal20_20CommGrid))
				{
					this.modSTK20_20 = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgTerminal400Grid) || ((PropertyGrid)s).Equals(this.CfgTerminal400CommGrid))
				{
					this.modSTK400 = true;
				}
				if (((PropertyGrid)s).Equals(this.CfgStackerNEGrid ) || ((PropertyGrid)s).Equals(this.CfgStackerNECommsGrid ))
				{
					this.modSTK_NE  = true;
				}       
        //ABorelli_FI01/06/2008_1
        if (((PropertyGrid)s).Equals(this.CfgStackerLREGrid) || ((PropertyGrid)s).Equals(this.CfgStackerLRECommsGrid))
        {
          this.modSTK_LRE = true;
        }
        
        //PMASSARO_FI02/05/08 - 6 - Added To Manage Pallet Print With Hardware Signals
        if (((PropertyGrid)s).Equals(this.CfgStackerGenericCTRLGrid) || ((PropertyGrid)s).Equals(this.CfgStackerGenericCTRL_CommsGrid))
        {
          this.modSTK_GenericCTRL = true;
        }
    
        //PMASSARO_FI18/08/08 - 1 - Added To Manage Universal Stacker Of Job 3742_Drezdenko
        //PMASSARO_FI18/08/08 - 2 - Added To Manage Universal Stacker Of Job 3742_Drezdenko (TASK COMMS)
        if (((PropertyGrid)s).Equals(this.CfgStackerUniversalGrid) || ((PropertyGrid)s).Equals(this.CfgStackerUniversalCTRLGrid))
        {
          this.modSTK_Universal = true;
        }

        //PMASSARO_FI24/08/10 - Aggiunti a grafica i settagi per il task FanFold HOETTEN Ctrl        
        if (((PropertyGrid)s).Equals(this.CfgFanfoldHoettenCTRLGrid) )
        {
          this.modFanFoldHOETTEN= true;
        }

        //agrassi_FI26-04-2010_1
        if (((PropertyGrid)s).Equals(this.CfgStackerBSCOMGAPOGrid) || ((PropertyGrid)s).Equals(this.CfgStackerBSCOMGAPOCTRLGrid))
        {
          this.modSTK_BSCOM_GAPO = true;
        }

        //graffaelli_FI25/10/2011 - Added Stacker Marquip Comms Task
        if (((PropertyGrid)s).Equals(this.CfgStackerMarquipGrid) || ((PropertyGrid)s).Equals(this.CfgStackerMarquip_CommsGrid))
        {
          this.modSTK_Marquip = true;
        }
			}
    }
	}

}

