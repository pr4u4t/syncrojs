using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.DataBase;
using SyncroC.Utility;
using SyncroC.DataBase.LineConfig;
using SyncroC.Controls.Colors;
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.Twin400.Settings
{
	public class Frm_twin400_machineSettings : SyncroC.Forms.Pages.Twin400.Settings.Frm_twin400_settingsSubgroup
	{
    const int NUM_DECIMAL_DIGITS = 5;
		private bool modify = false;
    private bool modPres = false;
    private bool modTwin = false;
		private bool modWeb = false;
		private bool modRotary = false;
    private bool modLevelCount = false;
    private bool modMeasWheel = false;
    private string[] modScoreTypes = new string[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORE_TYPES_MACHINE];
    private Label ConfigVer;
    private Label ConfigVerForValidation;
		private System.ComponentModel.IContainer components = null;
    private TabControl tab_Settings;
    private TabPage tab_MachineSettings;
    private PropertyGrid TwinCfgGrid;
    private TabPage tab_ScorerProfilesAndRotaryFormats;
    private SyncroC.Controls.TitledTextBox ttb_MeasWheel;
    private Label lbl_MeasWheel;
    private SyncroC.Controls.TitledList ttl_LevelCount;
    private Label lbl_LevelCount;
    private SyncroC.Controls.CheckBox chk_RSPresent;
    private SyncroC.Controls.CheckBox chk_WebDirPresent;
    private PropertyGrid RotaryCfgGrid;
    private PropertyGrid WebCfgGrid;
    private PropertyGrid RotaryFormatGrid;
    private PropertyGrid ScorerProfileGrid;
		private PropertyGrid TwinHeaderCfgGrid;
    private bool PLCCommOK = true;

		public Frm_twin400_machineSettings()
		{
			InitializeComponent();
      for (int i = 0; i < 3/*TODO: mettere la costante*/; i++)
        this.ttl_LevelCount.AddItem((i+1).ToString());
      RTCManager.getRTCManager().TwinConfigValidationSucceeded+=new EventHandler(TwinConfigValidationSucceeded);
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
      RTCManager.getRTCManager().TwinConfigValidationSucceeded -= new EventHandler(TwinConfigValidationSucceeded);
		}
		

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_machineSettings));
      this.ConfigVer = new System.Windows.Forms.Label();
      this.ConfigVerForValidation = new System.Windows.Forms.Label();
      this.tab_Settings = new System.Windows.Forms.TabControl();
      this.tab_MachineSettings = new System.Windows.Forms.TabPage();
      this.TwinHeaderCfgGrid = new System.Windows.Forms.PropertyGrid();
      this.ttb_MeasWheel = new SyncroC.Controls.TitledTextBox();
      this.lbl_MeasWheel = new System.Windows.Forms.Label();
      this.ttl_LevelCount = new SyncroC.Controls.TitledList();
      this.lbl_LevelCount = new System.Windows.Forms.Label();
      this.chk_RSPresent = new SyncroC.Controls.CheckBox();
      this.chk_WebDirPresent = new SyncroC.Controls.CheckBox();
      this.RotaryCfgGrid = new System.Windows.Forms.PropertyGrid();
      this.WebCfgGrid = new System.Windows.Forms.PropertyGrid();
      this.TwinCfgGrid = new System.Windows.Forms.PropertyGrid();
      this.tab_ScorerProfilesAndRotaryFormats = new System.Windows.Forms.TabPage();
      this.RotaryFormatGrid = new System.Windows.Forms.PropertyGrid();
      this.ScorerProfileGrid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_Settings.SuspendLayout();
      this.tab_MachineSettings.SuspendLayout();
      this.tab_ScorerProfilesAndRotaryFormats.SuspendLayout();
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
      // tab_TabB
      // 
      this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // lbl_CtrlF1
      // 
      this.lbl_CtrlF1.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 2);
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
      this.lbl_CtrlF5_YY.Text = "Modify";
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
      // ConfigVer
      // 
      this.ConfigVer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.ConfigVer.Location = new System.Drawing.Point(123, 58);
      this.ConfigVer.Name = "ConfigVer";
      this.ConfigVer.Size = new System.Drawing.Size(77, 20);
      this.ConfigVer.TabIndex = 116;
      this.ConfigVer.Visible = false;
      // 
      // ConfigVerForValidation
      // 
      this.ConfigVerForValidation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
      this.ConfigVerForValidation.Location = new System.Drawing.Point(226, 58);
      this.ConfigVerForValidation.Name = "ConfigVerForValidation";
      this.ConfigVerForValidation.Size = new System.Drawing.Size(64, 20);
      this.ConfigVerForValidation.TabIndex = 117;
      this.ConfigVerForValidation.Visible = false;
      // 
      // tab_Settings
      // 
      this.tab_Settings.Controls.Add(this.tab_MachineSettings);
      this.tab_Settings.Controls.Add(this.tab_ScorerProfilesAndRotaryFormats);
      this.tab_Settings.Location = new System.Drawing.Point(30, 175);
      this.tab_Settings.Name = "tab_Settings";
      this.tab_Settings.SelectedIndex = 0;
      this.tab_Settings.Size = new System.Drawing.Size(1171, 562);
      this.tab_Settings.TabIndex = 118;
      // 
      // tab_MachineSettings
      // 
      this.tab_MachineSettings.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tab_MachineSettings.Controls.Add(this.TwinHeaderCfgGrid);
      this.tab_MachineSettings.Controls.Add(this.ttb_MeasWheel);
      this.tab_MachineSettings.Controls.Add(this.lbl_MeasWheel);
      this.tab_MachineSettings.Controls.Add(this.ttl_LevelCount);
      this.tab_MachineSettings.Controls.Add(this.lbl_LevelCount);
      this.tab_MachineSettings.Controls.Add(this.chk_RSPresent);
      this.tab_MachineSettings.Controls.Add(this.chk_WebDirPresent);
      this.tab_MachineSettings.Controls.Add(this.RotaryCfgGrid);
      this.tab_MachineSettings.Controls.Add(this.WebCfgGrid);
      this.tab_MachineSettings.Controls.Add(this.TwinCfgGrid);
      this.tab_MachineSettings.Location = new System.Drawing.Point(4, 22);
      this.tab_MachineSettings.Name = "tab_MachineSettings";
      this.tab_MachineSettings.Padding = new System.Windows.Forms.Padding(3);
      this.tab_MachineSettings.Size = new System.Drawing.Size(1163, 536);
      this.tab_MachineSettings.TabIndex = 0;
      this.tab_MachineSettings.Text = "Machine Settings";
      // 
      // TwinHeaderCfgGrid
      // 
      this.TwinHeaderCfgGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.TwinHeaderCfgGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.TwinHeaderCfgGrid.Location = new System.Drawing.Point(14, 3);
      this.TwinHeaderCfgGrid.Name = "TwinHeaderCfgGrid";
      this.TwinHeaderCfgGrid.Size = new System.Drawing.Size(544, 285);
      this.TwinHeaderCfgGrid.TabIndex = 124;
      this.TwinHeaderCfgGrid.ToolbarVisible = false;
      this.TwinHeaderCfgGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.TwinHeaderCfgGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.TwinHeaderCfgGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // ttb_MeasWheel
      // 
      this.ttb_MeasWheel.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ttb_MeasWheel.Enabled = false;
      this.ttb_MeasWheel.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MeasWheel.ImagePassword = null;
      this.ttb_MeasWheel.Location = new System.Drawing.Point(1073, 502);
      this.ttb_MeasWheel.MaxChar = 32767;
      this.ttb_MeasWheel.Multiline = false;
      this.ttb_MeasWheel.Name = "ttb_MeasWheel";
      this.ttb_MeasWheel.PasswordChar = '\0';
      this.ttb_MeasWheel.ReadOnly = false;
      this.ttb_MeasWheel.Size = new System.Drawing.Size(59, 28);
      this.ttb_MeasWheel.Style = SyncroC.Controls.TitledTextBoxStyles.NoTitle;
      this.ttb_MeasWheel.TabIndex = 123;
      this.ttb_MeasWheel.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MeasWheel.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_MeasWheel.TranslateText = false;
      this.ttb_MeasWheel.Value = " ";
      this.ttb_MeasWheel.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_MeasWheel.ValueChanged += new System.EventHandler(this.ttb_MeasWheel_ValueChanged);
      // 
      // lbl_MeasWheel
      // 
      this.lbl_MeasWheel.Enabled = false;
      this.lbl_MeasWheel.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_MeasWheel.Location = new System.Drawing.Point(853, 506);
      this.lbl_MeasWheel.Name = "lbl_MeasWheel";
      this.lbl_MeasWheel.Size = new System.Drawing.Size(203, 21);
      this.lbl_MeasWheel.TabIndex = 122;
      this.lbl_MeasWheel.Text = "Measure Wheel mm per pulse:";
      // 
      // ttl_LevelCount
      // 
      this.ttl_LevelCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_LevelCount.CanCancel = false;
      this.ttl_LevelCount.Enabled = false;
      this.ttl_LevelCount.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_LevelCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_LevelCount.ImagePassword = null;
      this.ttl_LevelCount.Location = new System.Drawing.Point(762, 501);
      this.ttl_LevelCount.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.ttl_LevelCount.Name = "ttl_LevelCount";
      this.ttl_LevelCount.ReadOnly = true;
      this.ttl_LevelCount.SelectedIndex = -1;
      this.ttl_LevelCount.SelectedItem = "";
      this.ttl_LevelCount.SelectedItemData = "";
      this.ttl_LevelCount.Size = new System.Drawing.Size(35, 28);
      this.ttl_LevelCount.Sorted = false;
      this.ttl_LevelCount.Style = SyncroC.Controls.TitledListStyles.NoTitle;
      this.ttl_LevelCount.TabIndex = 118;
      this.ttl_LevelCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_LevelCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_LevelCount.TranslateText = false;
      this.ttl_LevelCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_LevelCount.SelectedItemChanged += new System.EventHandler(this.ttl_LevelCount_SelectedItemChanged);
      // 
      // lbl_LevelCount
      // 
      this.lbl_LevelCount.Enabled = false;
      this.lbl_LevelCount.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_LevelCount.Location = new System.Drawing.Point(637, 505);
      this.lbl_LevelCount.Name = "lbl_LevelCount";
      this.lbl_LevelCount.Size = new System.Drawing.Size(120, 21);
      this.lbl_LevelCount.TabIndex = 121;
      this.lbl_LevelCount.Text = "Level Count:";
      // 
      // chk_RSPresent
      // 
      this.chk_RSPresent.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_RSPresent.Checked = true;
      this.chk_RSPresent.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_RSPresent.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_RSPresent.ImagePassword = null;
      this.chk_RSPresent.Location = new System.Drawing.Point(622, 220);
      this.chk_RSPresent.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.chk_RSPresent.Name = "chk_RSPresent";
      this.chk_RSPresent.ReadOnly = true;
      this.chk_RSPresent.Size = new System.Drawing.Size(510, 38);
      this.chk_RSPresent.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_RSPresent.TabIndex = 120;
      this.chk_RSPresent.Text = "Rotary Shear Present";
      this.chk_RSPresent.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_RSPresent.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.chk_RSPresent.TranslateText = false;
      this.chk_RSPresent.OnCheckedChanged += new System.EventHandler(this.chk_ChkPresence_OnCheckedChanged);
      // 
      // chk_WebDirPresent
      // 
      this.chk_WebDirPresent.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_WebDirPresent.Checked = true;
      this.chk_WebDirPresent.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_WebDirPresent.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_WebDirPresent.ImagePassword = null;
      this.chk_WebDirPresent.Location = new System.Drawing.Point(622, 5);
      this.chk_WebDirPresent.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.chk_WebDirPresent.Name = "chk_WebDirPresent";
      this.chk_WebDirPresent.ReadOnly = true;
      this.chk_WebDirPresent.Size = new System.Drawing.Size(510, 38);
      this.chk_WebDirPresent.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_WebDirPresent.TabIndex = 119;
      this.chk_WebDirPresent.Text = "Web Director Present";
      this.chk_WebDirPresent.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_WebDirPresent.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.chk_WebDirPresent.TranslateText = false;
      this.chk_WebDirPresent.OnCheckedChanged += new System.EventHandler(this.chk_ChkPresence_OnCheckedChanged);
      // 
      // RotaryCfgGrid
      // 
      this.RotaryCfgGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.RotaryCfgGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.RotaryCfgGrid.Location = new System.Drawing.Point(622, 260);
      this.RotaryCfgGrid.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.RotaryCfgGrid.Name = "RotaryCfgGrid";
      this.RotaryCfgGrid.Size = new System.Drawing.Size(510, 236);
      this.RotaryCfgGrid.TabIndex = 117;
      this.RotaryCfgGrid.ToolbarVisible = false;
      this.RotaryCfgGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.RotaryCfgGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.RotaryCfgGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // WebCfgGrid
      // 
      this.WebCfgGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.WebCfgGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.WebCfgGrid.Location = new System.Drawing.Point(622, 43);
      this.WebCfgGrid.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.WebCfgGrid.Name = "WebCfgGrid";
      this.WebCfgGrid.Size = new System.Drawing.Size(510, 174);
      this.WebCfgGrid.TabIndex = 116;
      this.WebCfgGrid.ToolbarVisible = false;
      this.WebCfgGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.WebCfgGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.WebCfgGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // TwinCfgGrid
      // 
      this.TwinCfgGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.TwinCfgGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.TwinCfgGrid.Location = new System.Drawing.Point(14, 292);
      this.TwinCfgGrid.Name = "TwinCfgGrid";
      this.TwinCfgGrid.Size = new System.Drawing.Size(544, 240);
      this.TwinCfgGrid.TabIndex = 1;
      this.TwinCfgGrid.ToolbarVisible = false;
      this.TwinCfgGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.TwinCfgGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.TwinCfgGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // tab_ScorerProfilesAndRotaryFormats
      // 
      this.tab_ScorerProfilesAndRotaryFormats.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tab_ScorerProfilesAndRotaryFormats.Controls.Add(this.RotaryFormatGrid);
      this.tab_ScorerProfilesAndRotaryFormats.Controls.Add(this.ScorerProfileGrid);
      this.tab_ScorerProfilesAndRotaryFormats.Location = new System.Drawing.Point(4, 22);
      this.tab_ScorerProfilesAndRotaryFormats.Name = "tab_ScorerProfilesAndRotaryFormats";
      this.tab_ScorerProfilesAndRotaryFormats.Padding = new System.Windows.Forms.Padding(3);
      this.tab_ScorerProfilesAndRotaryFormats.Size = new System.Drawing.Size(1106, 536);
      this.tab_ScorerProfilesAndRotaryFormats.TabIndex = 1;
      this.tab_ScorerProfilesAndRotaryFormats.Text = "Scorer Profiles and Rotary Formats";
      // 
      // RotaryFormatGrid
      // 
      this.RotaryFormatGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.RotaryFormatGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.RotaryFormatGrid.Location = new System.Drawing.Point(566, 13);
      this.RotaryFormatGrid.Name = "RotaryFormatGrid";
      this.RotaryFormatGrid.Size = new System.Drawing.Size(521, 519);
      this.RotaryFormatGrid.TabIndex = 112;
      this.RotaryFormatGrid.ToolbarVisible = false;
      this.RotaryFormatGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.RotaryFormatGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // ScorerProfileGrid
      // 
      this.ScorerProfileGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.ScorerProfileGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.ScorerProfileGrid.Location = new System.Drawing.Point(20, 13);
      this.ScorerProfileGrid.Name = "ScorerProfileGrid";
      this.ScorerProfileGrid.Size = new System.Drawing.Size(521, 519);
      this.ScorerProfileGrid.TabIndex = 111;
      this.ScorerProfileGrid.ToolbarVisible = false;
      this.ScorerProfileGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.ScorerProfileGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // Frm_twin400_machineSettings
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.ConfigVerForValidation);
      this.Controls.Add(this.ConfigVer);
      this.Controls.Add(this.tab_Settings);
      this.Name = "Frm_twin400_machineSettings";
      this.Tag = "";
      this.Text = "Frm_twin400_machineSettings";
      this.Load += new System.EventHandler(this.Frm_twin400_machineSettings_Load);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.tab_Settings, 0);
      this.Controls.SetChildIndex(this.ConfigVer, 0);
      this.Controls.SetChildIndex(this.ConfigVerForValidation, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
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
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.tab_Settings.ResumeLayout(false);
      this.tab_MachineSettings.ResumeLayout(false);
      this.tab_ScorerProfilesAndRotaryFormats.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

		private void Frm_twin400_machineSettings_Load(object sender, System.EventArgs e)
		{
			for (int scr = 0; scr < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORE_TYPES_MACHINE; scr++)
				this.modScoreTypes[scr] = DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.ScoreTypes[scr].Scorer.ScoreId;
      this.refreshData();
		}

		protected override void CtrlF4()
		{
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        if (this.tab_Settings.SelectedTab.Equals(this.tab_MachineSettings))
        {
          if (this.modify)
          {
            Frm_Confirm conf = new Frm_Confirm();
            string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_172");
            if (conf.ShowDialog(this, text).Equals(DialogResult.OK))
            {
              LineConfig cfgCopy;
              CommunicationManager cm = CommunicationManager.GetCommunicationManager();
              if (this.modPres)
              {
                cfgCopy = (LineConfig)DBManager.getDBManager().lineConfig.CreateCopy();
                cfgCopy.WebDirector = this.chk_WebDirPresent.Checked;
                cfgCopy.Rotary = this.chk_RSPresent.Checked;
                cfgCopy.SlitterScorer = true;
                cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.CONF_LINE + "." + Command.CONF_CONFIG + "("
                  + cfgCopy.SerializeProperties() + ")");
              }

							if (this.modTwin)
								cm.SendTcp(cm.SYNCROS_RTC_MANAGER, PageManager.getRequestCommand(typeof(Frm_twin400_machineSettings)) + "." + Command.CONF_CONFIG + "("
									+ ((SyncroC.DataBase.LineConfig.SlitterScorerHeader)this.TwinHeaderCfgGrid.SelectedObject).SerializeProperties()
									+ ((SyncroC.DataBase.LineConfig.Twin400)this.TwinCfgGrid.SelectedObject).SerializeProperties()
									+ ")");

              if (this.modWeb)
                cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.FRM_WEBDIRECTOR_MACHINESETTINGS + "." + Command.CONF_CONFIG + "(" +
                  ((WebDirector)this.WebCfgGrid.SelectedObject).SerializeProperties() + ")");
              if (this.modRotary)
                cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.FRM_ROTARY_MACHINESETTINGS + "." + Command.CONF_CONFIG + "(" +
                  ((Rotary)this.RotaryCfgGrid.SelectedObject).SerializeProperties() + ")");
              if (this.modLevelCount)
                cm.SendTcp(cm.SYNCROS_RTC_MANAGER, PageManager.getRequestCommand(typeof(Frm_twin400_machineSettings)) + "." + Command.CONF_LEVEL_COUNT + "(" +
                  this.ttl_LevelCount.SelectedItem + ")");
              if (this.modMeasWheel)
                cm.SendTcp(cm.SYNCROS_RTC_MANAGER, PageManager.getRequestCommand(typeof(Frm_twin400_machineSettings)) + "." + Command.CONF_MEASURE_WHEEL + "(" +
                  this.ttb_MeasWheel.Value + ")");
              if (!this.modTwin)
                this.TwinConfigValidationSucceeded(this, new EventArgs());
            }
            else
              TwinConfigValidationSucceeded(this, new EventArgs());
          }
        }
        else
        {
          PageManager.getPageManager()._SetMessage("These configurations are not changeable from graphics!");
        }
      }
      else
      {
        PageManager.getPageManager()._SetMessage("§SYCC_11");
      }
		}

    private void TwinConfigValidationSucceeded(object sender, System.EventArgs e)
    {
      this.refreshData();
			this.TwinHeaderCfgGrid.ViewForeColor = Color.DimGray;
      this.TwinCfgGrid.ViewForeColor = Color.DimGray;
      this.WebCfgGrid.ViewForeColor = Color.DimGray;
      this.RotaryCfgGrid.ViewForeColor = Color.DimGray;
      this.chk_WebDirPresent.ReadOnly = true;
      this.chk_RSPresent.ReadOnly = true;
      this.chk_WebDirPresent.TitleForeColor = SyncroColor.LightGray;
      this.chk_RSPresent.TitleForeColor = SyncroColor.LightGray;
      this.ttl_LevelCount.ReadOnly = true;
      this.ttl_LevelCount.Enabled = false;
      this.lbl_LevelCount.Enabled = false;
      this.ttb_MeasWheel.Enabled = false;
      this.lbl_MeasWheel.Enabled = false;
      this.modify = false;
      this.modScoreTypes.Initialize();
      lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
        lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
    }

		protected override void CtrlF5()
		{
			if (Accessibility.getAccessibility().isLevelHigh())
      {
        if (this.tab_Settings.SelectedTab.Equals(this.tab_MachineSettings))
        {
          //agrassi_FI10-12-08
          //if (this.PLCCommOK)
          //{
					this.TwinHeaderCfgGrid.ViewForeColor = Color.Black;
          this.TwinCfgGrid.ViewForeColor = Color.Black;
          if (DBManager.getDBManager().lineConfig.WebDirector)
            this.WebCfgGrid.ViewForeColor = Color.Black;
          if (DBManager.getDBManager().lineConfig.Rotary)
            this.RotaryCfgGrid.ViewForeColor = Color.Black;
          this.chk_WebDirPresent.ReadOnly = false;
          this.chk_WebDirPresent.TitleForeColor = SyncroColor.DarkGreen;
          this.chk_RSPresent.ReadOnly = false;
          this.chk_RSPresent.TitleForeColor = SyncroColor.DarkGreen;
          this.ttl_LevelCount.ReadOnly = false;
          this.ttl_LevelCount.Enabled = true;
          this.lbl_LevelCount.Enabled = true;
          this.ttb_MeasWheel.Enabled = true;
          this.lbl_MeasWheel.Enabled = true;
          this.modify = true;
          this.modTwin = false;
          this.modWeb = false;
          this.modRotary = false;
          this.modPres = false;
          this.modLevelCount = false;
          this.modMeasWheel = false;
          lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
          //}
          //else
          //{
          //  PageManager.getPageManager()._SetMessage("Communication with PLC is necessary!");
          //}
        }
        else
        {
          PageManager.getPageManager()._SetMessage("These configurations are not changeable from graphics!");
        }
      }
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
    }

    protected override void F11()
		{
		}

		public override void refreshData()
		{
      LineConfig cfg = DBManager.getDBManager().lineConfig;
			this.TwinHeaderCfgGrid.SelectedObject = (SyncroC.DataBase.LineConfig.SlitterScorerHeader)cfg.SlitterScorerConfig.SlitterScorerHeader.CreateCopy();
			this.TwinCfgGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Twin400)cfg.SlitterScorerConfig.Twin400.CreateCopy();
      this.chk_WebDirPresent.Checked = cfg.WebDirector ;
      this.WebCfgGrid.SelectedObject = (WebDirector)cfg.WebDirectorConfig.CreateCopy();
      this.chk_RSPresent.Checked = cfg.Rotary ;
      this.RotaryCfgGrid.SelectedObject = (Rotary)cfg.RotaryConfig.CreateCopy();
      this.ttl_LevelCount.SelectedItem = cfg.KnifeConfig.LevelCount.ToString();
      this.ttb_MeasWheel.Value = cfg.EncodersConfig.MMPerPulseMeasureWheel.ToString("n", SyncroConverter.NumberFormat(NUM_DECIMAL_DIGITS));
      this.ScorerProfileGrid.SelectedObject = DBManager.getDBManager().scorerProfile.CreateCopy();
      this.RotaryFormatGrid.SelectedObject = DBManager.getDBManager().rotaryFormat.CreateCopy();
      this.WebCfgGrid.ExpandAllGridItems();
      this.RotaryCfgGrid.ExpandAllGridItems();
      this.ScorerProfileGrid.ExpandAllGridItems();
      this.RotaryFormatGrid.ExpandAllGridItems();
      if (SyncroC.Utility.Settings.getSettings().configuration.IsPmac400())
      {
        this.lbl_LevelCount.Visible = true;
        this.ttl_LevelCount.Visible = true;
        this.ttb_MeasWheel.Visible = true;
        this.lbl_MeasWheel.Visible = true;
        this.chk_RSPresent.Visible = true;
        this.RotaryCfgGrid.Visible = true;
      }
      else
      {
        this.lbl_LevelCount.Visible = false;
        this.ttl_LevelCount.Visible = false;
        this.ttb_MeasWheel.Visible = false;
        this.lbl_MeasWheel.Visible = false;
        this.chk_RSPresent.Visible = false;
        this.RotaryCfgGrid.Visible = false;
      }
    }

    private void CfgGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			if (!this.modify)
				this.refreshData();
			else
			{
        LineConfig cfg = DBManager.getDBManager().lineConfig;
				if (((PropertyGrid)s).Equals(this.TwinHeaderCfgGrid))
				{
					this.modTwin = true;
					for (int scr = 0; scr < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORE_TYPES_MACHINE; scr++)
					{
						string newScoreId = (((SyncroC.DataBase.LineConfig.SlitterScorerHeader)((PropertyGrid)s).SelectedObject).ScoreTypes[scr].Scorer.ScoreId);
						if (newScoreId != this.modScoreTypes[scr])
						{
							cfg.SlitterScorerConfig.CheckScoreProfiles(((PropertyGrid)s).SelectedObject, scr);
							this.modScoreTypes[scr] = newScoreId;
							((PropertyGrid)s).Refresh();
							break;
						}

					}
				}
				else if (((PropertyGrid)s).Equals(this.TwinCfgGrid))
				{
					this.modTwin=true;
					//for (int scr = 0; scr < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORE_TYPES_MACHINE; scr++)
					//{
					//  string newScoreId = (((SyncroC.DataBase.LineConfig.SlitterScorer)((PropertyGrid)s).SelectedObject).SlitterScorerHeader.ScoreTypes[scr].Scorer.ScoreId);
					//  if (newScoreId!=this.modScoreTypes[scr])
					//  {
					//    cfg.SlitterScorerConfig.CheckScoreProfiles(((PropertyGrid)s).SelectedObject, scr);
					//    this.modScoreTypes[scr] = newScoreId;
					//    ((PropertyGrid)s).Refresh();
					//    break;
					//  }

					//}
				}
        else if (((PropertyGrid)s).Equals(this.WebCfgGrid))
        {
          if (this.chk_WebDirPresent.Checked)
            this.modWeb = true;
          else
            this.WebCfgGrid.SelectedObject = (WebDirector)cfg.WebDirectorConfig.CreateCopy();
        }
        else if (((PropertyGrid)s).Equals(this.RotaryCfgGrid))
        {
          if (this.chk_RSPresent.Checked)
          {
            this.modRotary = true;
            cfg.RotaryConfig.CheckFormats(((PropertyGrid)s).SelectedObject);
            ((PropertyGrid)s).Refresh();
          }
          else
            this.RotaryCfgGrid.SelectedObject = (Rotary)cfg.RotaryConfig.CreateCopy();
        }
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
				lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
						lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
				this.TwinHeaderCfgGrid.ViewForeColor = Color.DimGray;
        this.TwinCfgGrid.ViewForeColor = Color.DimGray;
        this.WebCfgGrid.ViewForeColor = Color.DimGray;
        this.RotaryCfgGrid.ViewForeColor = Color.DimGray;
        this.chk_WebDirPresent.ReadOnly = true;
        this.chk_RSPresent.ReadOnly = true;
        this.chk_WebDirPresent.TitleForeColor = SyncroColor.LightGray;
        this.chk_RSPresent.TitleForeColor = SyncroColor.LightGray;
        this.ttl_LevelCount.ReadOnly = true;
        this.ttl_LevelCount.Enabled = false;
        this.lbl_LevelCount.Enabled = false;
        this.ttb_MeasWheel.Enabled = false;
        this.lbl_MeasWheel.Enabled = false;
        this.modScoreTypes.Initialize();
        this.refreshData();
      }
    }

    private void chk_ChkPresence_OnCheckedChanged(object sender, EventArgs e)
    {
      if (this.modify)
        this.modPres = true;
    }

    private void ttl_LevelCount_SelectedItemChanged(object sender, EventArgs e)
    {
      if (SyncroC.Utility.Settings.getSettings().configuration.IsPmac400())
        if (this.modify)
          this.modLevelCount = true;
    }

		private void ttb_MeasWheel_ValueChanged(object sender, EventArgs e)
		{
			if (SyncroC.Utility.Settings.getSettings().configuration.IsPmac400())
				if (this.modify)
					this.modMeasWheel = true;
		}

		private void _SetLevelCount(string levelCount)
    {
      this.ttl_LevelCount.SelectedItem = levelCount;
      DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount = this.intParse(levelCount);
    }

    private void _SetMeasureWheel(string measWheel)
    {
      this.ttb_MeasWheel.Value = measWheel;
      DBManager.getDBManager().lineConfig.EncodersConfig.MMPerPulseMeasureWheel = this.doubleParse(measWheel, NUM_DECIMAL_DIGITS);
    }

    protected override void RightArrow()
    {
    }

    protected override void LeftArrow()
    {
    }

    protected override void ControlD()
    {
      this.ConfigVer.Visible = !this.ConfigVer.Visible;
      this.ConfigVerForValidation.Visible = !this.ConfigVerForValidation.Visible;
    }

    private void _SetPLCComm(bool val)
    {
      this.PLCCommOK = val;
    }

    protected override bool IncludeComponentsIntoLanguageDB()
    {
      return false;
    }
  }
}

