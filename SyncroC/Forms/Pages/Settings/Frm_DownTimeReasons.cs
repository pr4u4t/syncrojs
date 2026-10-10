using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.Utility;
using SyncroC.DataBase;
using SyncroC.Controls.Colors;
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.Settings
{
	public class Frm_DownTimeReasons : SyncroC.Forms.Pages.Settings.Frm_SettingsGroup
	{
		private SyncroC.Controls.Title ttl_Zones;
		private SyncroC.Controls.Title ttl_reasons;
		private SyncroC.Controls.GridView gdv_Zones;
		private SyncroC.Controls.GridView gdv_Reasons;

		private DataGridViewColumn clm_Zones_CustomerKey;
		private DataGridViewColumn clm_Zones_Description;
		private DataGridViewColumn clm_Zones_FosberKey;
		private DataGridViewColumn clm_Reasons_CustomerKey;
		private DataGridViewColumn clm_Reasons_Description;
		private DataGridViewColumn clm_Reasons_FosberKey;

		private System.ComponentModel.IContainer components = null;

		private DataTable tableZones;
		private DataTable tableReasons;
		private SyncroC.Controls.Bracket brk_Zones;
		private SyncroC.Controls.Bracket bracket1;
		private SyncroC.Controls.TitledLabel tlb_ZonesFosberKey;
		private SyncroC.Controls.TitledTextBox ttb_ZonesCustomerKey;
		private SyncroC.Controls.CheckBox chk_ZonesAvailable;
		private SyncroC.Controls.TitledTextBox ttb_ZonesDescription;
		private SyncroC.Controls.CheckBox chk_ZonesStandard;
		private SyncroC.Controls.CheckBox chk_ReasonsStandard;
		private SyncroC.Controls.TitledTextBox ttb_ReasonsDescription;
		private SyncroC.Controls.CheckBox chk_ReasonsAvailable;
		private SyncroC.Controls.TitledTextBox ttb_ReasonsCustomerKey;
		private SyncroC.Controls.TitledLabel tlb_ReasonsFosberKey;
		private DataView dataviewReasons;
		private Label lbl_PageStatus;
		private SyncroC.Controls.TitledTextBox ttb_ReasonKeyZone;

		private int DEBUG_FOSBER_CODE_COLUMN_WIDTH = 80;

		//page status
		private enum State { ModifyZone, ModifyReason, Read };
		private State state;
    //agrassi_FI19-03-08_1
    //private bool debugOptions = true;

		DBManager dbm;

		private int IndexZoneFirstLanguage;
    private SyncroC.Controls.CheckBox chk_FosberZone;
    private SyncroC.Controls.CheckBox chk_FosberReason;
		private int IndexReasonFirstLanguage;

		public Frm_DownTimeReasons()
		{
			InitializeComponent();
			
			dbm = DBManager.getDBManager();

      //agrassi_FI07-07-08
      //IndexZoneFirstLanguage = dbm.downTimeReason.getIndexZoneFromFirstLanguage();
      //IndexReasonFirstLanguage = dbm.downTimeReason.getIndexReasonFromFirstLanguage();
      IndexZoneFirstLanguage = -1;
      IndexReasonFirstLanguage = -1;
      //agrassi_FI19-03-08_1
      this.Activated += new EventHandler(Frm_DownTimeReasons_Activated);

      //agrassi_FI07-07-08
      //if (dbm.downTimeReason.downTimeReason.Tables.Count < 2)
      //{
      //  Logger.LogError(this, "Frm_DownTimeReasons - downtimeReason table does not exist");
      //}
      //else
      //{
      //  tableZones = dbm.downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_ZONE];
      //  this.gdv_Zones.DataSource = tableZones;

      //  tableReasons = dbm.downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_REASON];
      //  dataviewReasons = new DataView(tableReasons);
      //  this.gdv_Reasons.DataSource = dataviewReasons;

      //  //gdv_Zones initialization
      //  clm_Zones_CustomerKey = new DataGridViewColumn(new DataGridViewTextBoxCell());
      //  clm_Zones_CustomerKey.HeaderText = "Code";
      //  clm_Zones_CustomerKey.Name = tableZones.Columns[DownTimeReason.INDEX_ZONE_CUSTOMER_KEY].ColumnName;
      //  clm_Zones_CustomerKey.DataPropertyName = tableZones.Columns[DownTimeReason.INDEX_ZONE_CUSTOMER_KEY].ColumnName;
      //  clm_Zones_CustomerKey.Width = 80;

      //  clm_Zones_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
      //  clm_Zones_Description.HeaderText = "Description";
      //  clm_Zones_Description.Name = tableZones.Columns[IndexZoneFirstLanguage].ColumnName;
      //  clm_Zones_Description.DataPropertyName = tableZones.Columns[IndexZoneFirstLanguage].ColumnName;
      //  //agrassi_FI19-03-08_1
      //  clm_Zones_Description.Width = 330 + DEBUG_FOSBER_CODE_COLUMN_WIDTH;

      //  clm_Zones_FosberKey = new DataGridViewColumn(new DataGridViewTextBoxCell());
      //  clm_Zones_FosberKey.HeaderText = "Code Fosber";
      //  clm_Zones_FosberKey.Name = tableZones.Columns[DownTimeReason.INDEX_ZONE_KEY].ColumnName;
      //  clm_Zones_FosberKey.DataPropertyName = tableZones.Columns[DownTimeReason.INDEX_ZONE_KEY].ColumnName;
      //  clm_Zones_FosberKey.Visible = false;
      //  clm_Zones_FosberKey.Width = DEBUG_FOSBER_CODE_COLUMN_WIDTH;

      //  gdv_Zones.Columns.Add(clm_Zones_CustomerKey);
      //  gdv_Zones.Columns.Add(clm_Zones_Description);
      //  gdv_Zones.Columns.Add(clm_Zones_FosberKey);
      //  gdv_Zones.Sort(clm_Zones_FosberKey, ListSortDirection.Ascending);

      //  //gdv_Zones initialization
      //  clm_Reasons_CustomerKey = new DataGridViewColumn(new DataGridViewTextBoxCell());
      //  clm_Reasons_CustomerKey.HeaderText = "Code";
      //  clm_Reasons_CustomerKey.Name = tableReasons.Columns[DownTimeReason.INDEX_REASON_CUSTOMER_KEY].ColumnName;
      //  clm_Reasons_CustomerKey.DataPropertyName = tableReasons.Columns[DownTimeReason.INDEX_REASON_CUSTOMER_KEY].ColumnName;
      //  clm_Reasons_CustomerKey.Width = 80;

      //  clm_Reasons_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
      //  clm_Reasons_Description.HeaderText = "Description";
      //  clm_Reasons_Description.Name = tableReasons.Columns[IndexReasonFirstLanguage].ColumnName;
      //  clm_Reasons_Description.DataPropertyName = tableReasons.Columns[IndexReasonFirstLanguage].ColumnName;
      //  //agrassi_FI19-03-08_1
      //  clm_Reasons_Description.Width = 330 + DEBUG_FOSBER_CODE_COLUMN_WIDTH;

      //  clm_Reasons_FosberKey = new DataGridViewColumn(new DataGridViewTextBoxCell());
      //  clm_Reasons_FosberKey.HeaderText = "Code Fosber";
      //  clm_Reasons_FosberKey.Name = tableReasons.Columns[DownTimeReason.INDEX_REASON_KEY].ColumnName;
      //  clm_Reasons_FosberKey.DataPropertyName = tableReasons.Columns[DownTimeReason.INDEX_REASON_KEY].ColumnName;
      //  clm_Reasons_FosberKey.Visible = false;
      //  clm_Reasons_FosberKey.Width = DEBUG_FOSBER_CODE_COLUMN_WIDTH;

      //  gdv_Reasons.Columns.Add(clm_Reasons_CustomerKey);
      //  gdv_Reasons.Columns.Add(clm_Reasons_Description);
      //  gdv_Reasons.Columns.Add(clm_Reasons_FosberKey);
      //  gdv_Reasons.Sort(clm_Reasons_FosberKey, ListSortDirection.Ascending);

      //  //agrassi_FI19-03-08_1
      //  //ControlD();
      //  SetPageState(State.Read);
      //}
      this.SetTables();
		}

    //agrassi_FI07-07-08
    void SetTables()
    {
      if ((this.IndexZoneFirstLanguage == dbm.downTimeReason.getIndexZoneFromFirstLanguage()) &&
         (this.IndexReasonFirstLanguage == dbm.downTimeReason.getIndexReasonFromFirstLanguage()))
        return;
      IndexZoneFirstLanguage = dbm.downTimeReason.getIndexZoneFromFirstLanguage();
      IndexReasonFirstLanguage = dbm.downTimeReason.getIndexReasonFromFirstLanguage();
      if (dbm.downTimeReason.downTimeReason.Tables.Count < 2)
      {
        Logger.LogError(this, "Frm_DownTimeReasons - downtimeReason table does not exist");
      }
      else
      {
        tableZones = dbm.downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_ZONE];
        this.gdv_Zones.DataSource = tableZones;

        tableReasons = dbm.downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_REASON];
        dataviewReasons = new DataView(tableReasons);
        this.gdv_Reasons.DataSource = dataviewReasons;

        //gdv_Zones initialization
        clm_Zones_CustomerKey = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Zones_CustomerKey.HeaderText = "Code";
        clm_Zones_CustomerKey.Name = tableZones.Columns[DownTimeReason.INDEX_ZONE_CUSTOMER_KEY].ColumnName;
        clm_Zones_CustomerKey.DataPropertyName = tableZones.Columns[DownTimeReason.INDEX_ZONE_CUSTOMER_KEY].ColumnName;
        clm_Zones_CustomerKey.Width = 80;

        clm_Zones_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Zones_Description.HeaderText = "Description";
        clm_Zones_Description.Name = tableZones.Columns[IndexZoneFirstLanguage].ColumnName;
        clm_Zones_Description.DataPropertyName = tableZones.Columns[IndexZoneFirstLanguage].ColumnName;
        //agrassi_FI19-03-08_1
        clm_Zones_Description.Width = 330 + DEBUG_FOSBER_CODE_COLUMN_WIDTH;

        clm_Zones_FosberKey = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Zones_FosberKey.HeaderText = "Code Fosber";
        clm_Zones_FosberKey.Name = tableZones.Columns[DownTimeReason.INDEX_ZONE_KEY].ColumnName;
        clm_Zones_FosberKey.DataPropertyName = tableZones.Columns[DownTimeReason.INDEX_ZONE_KEY].ColumnName;
        clm_Zones_FosberKey.Visible = false;
        clm_Zones_FosberKey.Width = DEBUG_FOSBER_CODE_COLUMN_WIDTH;

        gdv_Zones.Columns.Clear();
        gdv_Zones.Columns.Add(clm_Zones_CustomerKey);
        gdv_Zones.Columns.Add(clm_Zones_Description);
        gdv_Zones.Columns.Add(clm_Zones_FosberKey);
        gdv_Zones.Sort(clm_Zones_FosberKey, ListSortDirection.Ascending);

        //gdv_Zones initialization
        clm_Reasons_CustomerKey = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Reasons_CustomerKey.HeaderText = "Code";
        clm_Reasons_CustomerKey.Name = tableReasons.Columns[DownTimeReason.INDEX_REASON_CUSTOMER_KEY].ColumnName;
        clm_Reasons_CustomerKey.DataPropertyName = tableReasons.Columns[DownTimeReason.INDEX_REASON_CUSTOMER_KEY].ColumnName;
        clm_Reasons_CustomerKey.Width = 80;

        clm_Reasons_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Reasons_Description.HeaderText = "Description";
        clm_Reasons_Description.Name = tableReasons.Columns[IndexReasonFirstLanguage].ColumnName;
        clm_Reasons_Description.DataPropertyName = tableReasons.Columns[IndexReasonFirstLanguage].ColumnName;
        //agrassi_FI19-03-08_1
        clm_Reasons_Description.Width = 330 + DEBUG_FOSBER_CODE_COLUMN_WIDTH;

        clm_Reasons_FosberKey = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Reasons_FosberKey.HeaderText = "Code Fosber";
        clm_Reasons_FosberKey.Name = tableReasons.Columns[DownTimeReason.INDEX_REASON_KEY].ColumnName;
        clm_Reasons_FosberKey.DataPropertyName = tableReasons.Columns[DownTimeReason.INDEX_REASON_KEY].ColumnName;
        clm_Reasons_FosberKey.Visible = false;
        clm_Reasons_FosberKey.Width = DEBUG_FOSBER_CODE_COLUMN_WIDTH;

        gdv_Reasons.Columns.Clear();
        gdv_Reasons.Columns.Add(clm_Reasons_CustomerKey);
        gdv_Reasons.Columns.Add(clm_Reasons_Description);
        gdv_Reasons.Columns.Add(clm_Reasons_FosberKey);
        gdv_Reasons.Sort(clm_Reasons_FosberKey, ListSortDirection.Ascending);
        SetPageState(State.Read);
        this.gdv_Zones_SelectionChanged(null, null);

      }
    }

    //agrassi_FI07-07-08
    protected override void setBoundsAndTranslations(Hashtable bounds,
      System.Windows.Forms.Control.ControlCollection controls, double orizontalResizing,
      double verticalResizing, float fontResizing, string language, bool setBounds, bool updateDB, bool isIncludeComponentsIntoLanguageDB)
    {
      this.SetTables();
      base.setBoundsAndTranslations(bounds, controls, orizontalResizing, verticalResizing, fontResizing, language, setBounds, updateDB, isIncludeComponentsIntoLanguageDB);
    }

    //agrassi_FI19-03-08_1
    void Frm_DownTimeReasons_Activated(object sender, EventArgs e)
    {
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        this.chk_FosberZone.Visible = this.chk_FosberReason.Visible = true;
        ttb_ZonesDescription.Width = 397;
        ttb_ReasonsDescription.Width = 290;
        clm_Zones_Description.Width = 330;
        clm_Zones_FosberKey.Visible = true;
        clm_Reasons_Description.Width = 330;
        clm_Reasons_FosberKey.Visible = true;
        this.gdv_Reasons.Invalidate();
        this.gdv_Zones.Invalidate();
      }
      else
      {
        this.chk_FosberZone.Visible = this.chk_FosberReason.Visible = false;
        ttb_ZonesDescription.Width = 500;
        ttb_ReasonsDescription.Width = 500;
        clm_Zones_Description.Width = 330 + DEBUG_FOSBER_CODE_COLUMN_WIDTH;
        clm_Zones_FosberKey.Visible = false;
        clm_Reasons_Description.Width = 330 + DEBUG_FOSBER_CODE_COLUMN_WIDTH;
        clm_Reasons_FosberKey.Visible = false;
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
			this.gdv_Zones.DataSource = null;
			this.gdv_Reasons.DataSource = null;
			base.Dispose(disposing);
		}

		public override void ConfigPage(SyncroC.Utility.Settings.Configuration config)
		{
			base.ConfigPage(config);
			if (!config.getOnServer())
			{
				this.CtrlFKeyEnable[(int)FKEYS.F1] = false;
				lbl_CtrlF1_YY.Text = string.Empty;
				this.CtrlFKeyEnable[(int)FKEYS.F2] = false;
				lbl_CtrlF2_YY.Text = string.Empty;
				this.CtrlFKeyEnable[(int)FKEYS.F4] = false;
				lbl_CtrlF4_YY.Text = string.Empty;
			}
		}

		/// <summary>
		/// F4 key behaviour
		/// </summary>
		protected override void F4()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DownTimeReasons));
      this.ttl_Zones = new SyncroC.Controls.Title();
      this.ttl_reasons = new SyncroC.Controls.Title();
      this.gdv_Zones = new SyncroC.Controls.GridView();
      this.gdv_Reasons = new SyncroC.Controls.GridView();
      this.brk_Zones = new SyncroC.Controls.Bracket();
      this.bracket1 = new SyncroC.Controls.Bracket();
      this.tlb_ZonesFosberKey = new SyncroC.Controls.TitledLabel();
      this.ttb_ZonesCustomerKey = new SyncroC.Controls.TitledTextBox();
      this.chk_ZonesAvailable = new SyncroC.Controls.CheckBox();
      this.ttb_ZonesDescription = new SyncroC.Controls.TitledTextBox();
      this.chk_ZonesStandard = new SyncroC.Controls.CheckBox();
      this.chk_ReasonsStandard = new SyncroC.Controls.CheckBox();
      this.ttb_ReasonsDescription = new SyncroC.Controls.TitledTextBox();
      this.chk_ReasonsAvailable = new SyncroC.Controls.CheckBox();
      this.ttb_ReasonsCustomerKey = new SyncroC.Controls.TitledTextBox();
      this.tlb_ReasonsFosberKey = new SyncroC.Controls.TitledLabel();
      this.lbl_PageStatus = new System.Windows.Forms.Label();
      this.ttb_ReasonKeyZone = new SyncroC.Controls.TitledTextBox();
      this.chk_FosberZone = new SyncroC.Controls.CheckBox();
      this.chk_FosberReason = new SyncroC.Controls.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_Tab4
      // 
      this.tab_Tab4.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab4.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Text = "Modify Zone";
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Text = "Modify Reason";
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
      // ttl_Zones
      // 
      this.ttl_Zones.BackColor = System.Drawing.Color.Black;
      this.ttl_Zones.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Zones.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Zones.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Zones.Location = new System.Drawing.Point(80, 193);
      this.ttl_Zones.Name = "ttl_Zones";
      this.ttl_Zones.Size = new System.Drawing.Size(520, 339);
      this.ttl_Zones.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Zones.TabIndex = 110;
      this.ttl_Zones.Text = "DOWNTIME ZONES";
      this.ttl_Zones.TranslateText = true;
      // 
      // ttl_reasons
      // 
      this.ttl_reasons.BackColor = System.Drawing.Color.Black;
      this.ttl_reasons.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_reasons.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_reasons.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_reasons.Location = new System.Drawing.Point(628, 193);
      this.ttl_reasons.Name = "ttl_reasons";
      this.ttl_reasons.Size = new System.Drawing.Size(519, 339);
      this.ttl_reasons.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_reasons.TabIndex = 111;
      this.ttl_reasons.Text = "DOWNTIME REASONS";
      this.ttl_reasons.TranslateText = true;
      // 
      // gdv_Zones
      // 
      this.gdv_Zones.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Zones.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Zones.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Zones.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Zones.AutoGenerateColumns = false;
      this.gdv_Zones.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Zones.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Large;
      this.gdv_Zones.DataMember = "";
      this.gdv_Zones.DataSource = null;
      this.gdv_Zones.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.gdv_Zones.ForeColor = System.Drawing.Color.Black;
      this.gdv_Zones.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Zones.ImagePassword = null;
      this.gdv_Zones.Location = new System.Drawing.Point(90, 221);
      this.gdv_Zones.MultiSelect = false;
      this.gdv_Zones.Name = "gdv_Zones";
      this.gdv_Zones.RowsHeight = 32;
      this.gdv_Zones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Zones.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Zones.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Zones.Size = new System.Drawing.Size(510, 311);
      this.gdv_Zones.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Zones.TabIndex = 112;
      this.gdv_Zones.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Zones.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Zones.TranslateText = true;
      this.gdv_Zones.SelectionChanged += new System.EventHandler(this.gdv_Zones_SelectionChanged);
      this.gdv_Zones.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Zones_CellFormatting);
      // 
      // gdv_Reasons
      // 
      this.gdv_Reasons.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Reasons.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Reasons.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Reasons.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Reasons.AutoGenerateColumns = false;
      this.gdv_Reasons.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Reasons.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Large;
      this.gdv_Reasons.DataMember = "";
      this.gdv_Reasons.DataSource = null;
      this.gdv_Reasons.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.gdv_Reasons.ForeColor = System.Drawing.Color.Black;
      this.gdv_Reasons.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Reasons.ImagePassword = null;
      this.gdv_Reasons.Location = new System.Drawing.Point(638, 221);
      this.gdv_Reasons.MultiSelect = false;
      this.gdv_Reasons.Name = "gdv_Reasons";
      this.gdv_Reasons.RowsHeight = 32;
      this.gdv_Reasons.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Reasons.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Reasons.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Reasons.Size = new System.Drawing.Size(509, 311);
      this.gdv_Reasons.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Reasons.TabIndex = 113;
      this.gdv_Reasons.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Reasons.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Reasons.TranslateText = true;
      this.gdv_Reasons.SelectionChanged += new System.EventHandler(this.gdv_Reasons_SelectionChanged);
      this.gdv_Reasons.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_reasons_CellFormatting);
      // 
      // brk_Zones
      // 
      this.brk_Zones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.brk_Zones.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.brk_Zones.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_Zones.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_Zones.Location = new System.Drawing.Point(80, 554);
      this.brk_Zones.Name = "brk_Zones";
      this.brk_Zones.Position = SyncroC.Controls.BracketPosition.Bottom;
      this.brk_Zones.ShowArrow = true;
      this.brk_Zones.Size = new System.Drawing.Size(520, 158);
      this.brk_Zones.TabIndex = 115;
      this.brk_Zones.Text = "ZONES DETAILS";
      this.brk_Zones.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_Zones.TranslateText = true;
      // 
      // bracket1
      // 
      this.bracket1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.bracket1.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.bracket1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bracket1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.bracket1.Location = new System.Drawing.Point(628, 554);
      this.bracket1.Name = "bracket1";
      this.bracket1.Position = SyncroC.Controls.BracketPosition.Bottom;
      this.bracket1.ShowArrow = true;
      this.bracket1.Size = new System.Drawing.Size(519, 158);
      this.bracket1.TabIndex = 116;
      this.bracket1.Text = "REASONS DETAILS";
      this.bracket1.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.bracket1.TranslateText = true;
      // 
      // tlb_ZonesFosberKey
      // 
      this.tlb_ZonesFosberKey.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
      this.tlb_ZonesFosberKey.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ZonesFosberKey.ImagePassword = null;
      this.tlb_ZonesFosberKey.Location = new System.Drawing.Point(90, 579);
      this.tlb_ZonesFosberKey.Name = "tlb_ZonesFosberKey";
      this.tlb_ZonesFosberKey.Size = new System.Drawing.Size(130, 60);
      this.tlb_ZonesFosberKey.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_ZonesFosberKey.TabIndex = 117;
      this.tlb_ZonesFosberKey.Text = "Fosber key";
      this.tlb_ZonesFosberKey.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ZonesFosberKey.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ZonesFosberKey.TranslateText = true;
      this.tlb_ZonesFosberKey.Value = "";
      this.tlb_ZonesFosberKey.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ZonesFosberKey.ValueMultiline = true;
      // 
      // ttb_ZonesCustomerKey
      // 
      this.ttb_ZonesCustomerKey.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_ZonesCustomerKey.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
      this.ttb_ZonesCustomerKey.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ZonesCustomerKey.ImagePassword = null;
      this.ttb_ZonesCustomerKey.Location = new System.Drawing.Point(234, 579);
      this.ttb_ZonesCustomerKey.MaxChar = 4;
      this.ttb_ZonesCustomerKey.Multiline = false;
      this.ttb_ZonesCustomerKey.Name = "ttb_ZonesCustomerKey";
      this.ttb_ZonesCustomerKey.PasswordChar = '\0';
      this.ttb_ZonesCustomerKey.ReadOnly = true;
      this.ttb_ZonesCustomerKey.Size = new System.Drawing.Size(135, 60);
      this.ttb_ZonesCustomerKey.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_ZonesCustomerKey.TabIndex = 118;
      this.ttb_ZonesCustomerKey.Text = "Key";
      this.ttb_ZonesCustomerKey.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ZonesCustomerKey.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ZonesCustomerKey.TranslateText = true;
      this.ttb_ZonesCustomerKey.Value = " ";
      this.ttb_ZonesCustomerKey.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // chk_ZonesAvailable
      // 
      this.chk_ZonesAvailable.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ZonesAvailable.Checked = true;
      this.chk_ZonesAvailable.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_ZonesAvailable.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_ZonesAvailable.ImagePassword = null;
      this.chk_ZonesAvailable.Location = new System.Drawing.Point(379, 579);
      this.chk_ZonesAvailable.Name = "chk_ZonesAvailable";
      this.chk_ZonesAvailable.ReadOnly = true;
      this.chk_ZonesAvailable.Size = new System.Drawing.Size(102, 60);
      this.chk_ZonesAvailable.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_ZonesAvailable.TabIndex = 119;
      this.chk_ZonesAvailable.Text = "Available";
      this.chk_ZonesAvailable.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ZonesAvailable.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_ZonesAvailable.TranslateText = true;
      // 
      // ttb_ZonesDescription
      // 
      this.ttb_ZonesDescription.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_ZonesDescription.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
      this.ttb_ZonesDescription.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ZonesDescription.ImagePassword = null;
      this.ttb_ZonesDescription.Location = new System.Drawing.Point(91, 652);
      this.ttb_ZonesDescription.MaxChar = 32767;
      this.ttb_ZonesDescription.Multiline = false;
      this.ttb_ZonesDescription.Name = "ttb_ZonesDescription";
      this.ttb_ZonesDescription.PasswordChar = '\0';
      this.ttb_ZonesDescription.ReadOnly = true;
      this.ttb_ZonesDescription.Size = new System.Drawing.Size(397, 60);
      this.ttb_ZonesDescription.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_ZonesDescription.TabIndex = 120;
      this.ttb_ZonesDescription.Text = "Description";
      this.ttb_ZonesDescription.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ZonesDescription.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ZonesDescription.TranslateText = true;
      this.ttb_ZonesDescription.Value = " ";
      this.ttb_ZonesDescription.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // chk_ZonesStandard
      // 
      this.chk_ZonesStandard.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ZonesStandard.Checked = true;
      this.chk_ZonesStandard.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_ZonesStandard.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_ZonesStandard.ImagePassword = null;
      this.chk_ZonesStandard.Location = new System.Drawing.Point(487, 579);
      this.chk_ZonesStandard.Name = "chk_ZonesStandard";
      this.chk_ZonesStandard.ReadOnly = true;
      this.chk_ZonesStandard.Size = new System.Drawing.Size(104, 60);
      this.chk_ZonesStandard.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_ZonesStandard.TabIndex = 121;
      this.chk_ZonesStandard.Text = "Fosber std";
      this.chk_ZonesStandard.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ZonesStandard.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_ZonesStandard.TranslateText = false;
      // 
      // chk_ReasonsStandard
      // 
      this.chk_ReasonsStandard.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ReasonsStandard.Checked = true;
      this.chk_ReasonsStandard.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_ReasonsStandard.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_ReasonsStandard.ImagePassword = null;
      this.chk_ReasonsStandard.Location = new System.Drawing.Point(1035, 579);
      this.chk_ReasonsStandard.Name = "chk_ReasonsStandard";
      this.chk_ReasonsStandard.ReadOnly = true;
      this.chk_ReasonsStandard.Size = new System.Drawing.Size(104, 60);
      this.chk_ReasonsStandard.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_ReasonsStandard.TabIndex = 126;
      this.chk_ReasonsStandard.Text = "Fosber std";
      this.chk_ReasonsStandard.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ReasonsStandard.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_ReasonsStandard.TranslateText = false;
      // 
      // ttb_ReasonsDescription
      // 
      this.ttb_ReasonsDescription.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_ReasonsDescription.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
      this.ttb_ReasonsDescription.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ReasonsDescription.ImagePassword = null;
      this.ttb_ReasonsDescription.Location = new System.Drawing.Point(638, 652);
      this.ttb_ReasonsDescription.MaxChar = 32767;
      this.ttb_ReasonsDescription.Multiline = false;
      this.ttb_ReasonsDescription.Name = "ttb_ReasonsDescription";
      this.ttb_ReasonsDescription.PasswordChar = '\0';
      this.ttb_ReasonsDescription.ReadOnly = true;
      this.ttb_ReasonsDescription.Size = new System.Drawing.Size(290, 60);
      this.ttb_ReasonsDescription.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_ReasonsDescription.TabIndex = 125;
      this.ttb_ReasonsDescription.Text = "Description";
      this.ttb_ReasonsDescription.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ReasonsDescription.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ReasonsDescription.TranslateText = true;
      this.ttb_ReasonsDescription.Value = " ";
      this.ttb_ReasonsDescription.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // chk_ReasonsAvailable
      // 
      this.chk_ReasonsAvailable.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ReasonsAvailable.Checked = true;
      this.chk_ReasonsAvailable.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_ReasonsAvailable.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_ReasonsAvailable.ImagePassword = null;
      this.chk_ReasonsAvailable.Location = new System.Drawing.Point(927, 579);
      this.chk_ReasonsAvailable.Name = "chk_ReasonsAvailable";
      this.chk_ReasonsAvailable.ReadOnly = true;
      this.chk_ReasonsAvailable.Size = new System.Drawing.Size(102, 60);
      this.chk_ReasonsAvailable.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_ReasonsAvailable.TabIndex = 124;
      this.chk_ReasonsAvailable.Text = "Available";
      this.chk_ReasonsAvailable.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_ReasonsAvailable.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_ReasonsAvailable.TranslateText = true;
      // 
      // ttb_ReasonsCustomerKey
      // 
      this.ttb_ReasonsCustomerKey.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_ReasonsCustomerKey.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
      this.ttb_ReasonsCustomerKey.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ReasonsCustomerKey.ImagePassword = null;
      this.ttb_ReasonsCustomerKey.Location = new System.Drawing.Point(782, 579);
      this.ttb_ReasonsCustomerKey.MaxChar = 4;
      this.ttb_ReasonsCustomerKey.Multiline = false;
      this.ttb_ReasonsCustomerKey.Name = "ttb_ReasonsCustomerKey";
      this.ttb_ReasonsCustomerKey.PasswordChar = '\0';
      this.ttb_ReasonsCustomerKey.ReadOnly = true;
      this.ttb_ReasonsCustomerKey.Size = new System.Drawing.Size(135, 60);
      this.ttb_ReasonsCustomerKey.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_ReasonsCustomerKey.TabIndex = 123;
      this.ttb_ReasonsCustomerKey.Text = "Key";
      this.ttb_ReasonsCustomerKey.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ReasonsCustomerKey.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ReasonsCustomerKey.TranslateText = true;
      this.ttb_ReasonsCustomerKey.Value = " ";
      this.ttb_ReasonsCustomerKey.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // tlb_ReasonsFosberKey
      // 
      this.tlb_ReasonsFosberKey.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
      this.tlb_ReasonsFosberKey.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ReasonsFosberKey.ImagePassword = null;
      this.tlb_ReasonsFosberKey.Location = new System.Drawing.Point(638, 579);
      this.tlb_ReasonsFosberKey.Name = "tlb_ReasonsFosberKey";
      this.tlb_ReasonsFosberKey.Size = new System.Drawing.Size(130, 60);
      this.tlb_ReasonsFosberKey.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_ReasonsFosberKey.TabIndex = 122;
      this.tlb_ReasonsFosberKey.Text = "Fosber key";
      this.tlb_ReasonsFosberKey.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ReasonsFosberKey.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ReasonsFosberKey.TranslateText = true;
      this.tlb_ReasonsFosberKey.Value = "";
      this.tlb_ReasonsFosberKey.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ReasonsFosberKey.ValueMultiline = true;
      // 
      // lbl_PageStatus
      // 
      this.lbl_PageStatus.BackColor = System.Drawing.Color.LightGray;
      this.lbl_PageStatus.Font = new System.Drawing.Font("Arial Unicode MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.lbl_PageStatus.Location = new System.Drawing.Point(936, 57);
      this.lbl_PageStatus.Name = "lbl_PageStatus";
      this.lbl_PageStatus.Size = new System.Drawing.Size(320, 22);
      this.lbl_PageStatus.TabIndex = 127;
      this.lbl_PageStatus.Text = "Page status:";
      this.lbl_PageStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_PageStatus.Visible = false;
      // 
      // ttb_ReasonKeyZone
      // 
      this.ttb_ReasonKeyZone.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_ReasonKeyZone.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
      this.ttb_ReasonKeyZone.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ReasonKeyZone.ImagePassword = null;
      this.ttb_ReasonKeyZone.Location = new System.Drawing.Point(934, 652);
      this.ttb_ReasonKeyZone.MaxChar = 32767;
      this.ttb_ReasonKeyZone.Multiline = false;
      this.ttb_ReasonKeyZone.Name = "ttb_ReasonKeyZone";
      this.ttb_ReasonKeyZone.PasswordChar = '\0';
      this.ttb_ReasonKeyZone.ReadOnly = true;
      this.ttb_ReasonKeyZone.Size = new System.Drawing.Size(95, 60);
      this.ttb_ReasonKeyZone.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_ReasonKeyZone.TabIndex = 128;
      this.ttb_ReasonKeyZone.Text = "Zone Key";
      this.ttb_ReasonKeyZone.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ReasonKeyZone.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ReasonKeyZone.TranslateText = true;
      this.ttb_ReasonKeyZone.Value = " ";
      this.ttb_ReasonKeyZone.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // chk_FosberZone
      // 
      this.chk_FosberZone.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_FosberZone.Checked = true;
      this.chk_FosberZone.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_FosberZone.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_FosberZone.ImagePassword = null;
      this.chk_FosberZone.Location = new System.Drawing.Point(494, 652);
      this.chk_FosberZone.Name = "chk_FosberZone";
      this.chk_FosberZone.ReadOnly = true;
      this.chk_FosberZone.Size = new System.Drawing.Size(97, 60);
      this.chk_FosberZone.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_FosberZone.TabIndex = 129;
      this.chk_FosberZone.Text = "Fosber";
      this.chk_FosberZone.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_FosberZone.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_FosberZone.TranslateText = false;
      this.chk_FosberZone.Visible = false;
      // 
      // chk_FosberReason
      // 
      this.chk_FosberReason.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_FosberReason.Checked = true;
      this.chk_FosberReason.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_FosberReason.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_FosberReason.ImagePassword = null;
      this.chk_FosberReason.Location = new System.Drawing.Point(1040, 652);
      this.chk_FosberReason.Name = "chk_FosberReason";
      this.chk_FosberReason.ReadOnly = true;
      this.chk_FosberReason.Size = new System.Drawing.Size(97, 60);
      this.chk_FosberReason.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_FosberReason.TabIndex = 130;
      this.chk_FosberReason.Text = "Fosber";
      this.chk_FosberReason.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_FosberReason.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_FosberReason.TranslateText = false;
      this.chk_FosberReason.Visible = false;
      // 
      // Frm_DownTimeReasons
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.chk_FosberReason);
      this.Controls.Add(this.chk_FosberZone);
      this.Controls.Add(this.ttb_ReasonsDescription);
      this.Controls.Add(this.ttb_ZonesDescription);
      this.Controls.Add(this.lbl_PageStatus);
      this.Controls.Add(this.chk_ReasonsAvailable);
      this.Controls.Add(this.ttb_ReasonsCustomerKey);
      this.Controls.Add(this.tlb_ReasonsFosberKey);
      this.Controls.Add(this.chk_ZonesAvailable);
      this.Controls.Add(this.ttb_ZonesCustomerKey);
      this.Controls.Add(this.tlb_ZonesFosberKey);
      this.Controls.Add(this.bracket1);
      this.Controls.Add(this.brk_Zones);
      this.Controls.Add(this.gdv_Reasons);
      this.Controls.Add(this.gdv_Zones);
      this.Controls.Add(this.ttl_reasons);
      this.Controls.Add(this.ttl_Zones);
      this.Controls.Add(this.chk_ZonesStandard);
      this.Controls.Add(this.ttb_ReasonKeyZone);
      this.Controls.Add(this.chk_ReasonsStandard);
      this.Name = "Frm_DownTimeReasons";
      this.Text = "Frm_DownTimeReasons";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.chk_ReasonsStandard, 0);
      this.Controls.SetChildIndex(this.ttb_ReasonKeyZone, 0);
      this.Controls.SetChildIndex(this.chk_ZonesStandard, 0);
      this.Controls.SetChildIndex(this.ttl_Zones, 0);
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
      this.Controls.SetChildIndex(this.ttl_reasons, 0);
      this.Controls.SetChildIndex(this.gdv_Zones, 0);
      this.Controls.SetChildIndex(this.gdv_Reasons, 0);
      this.Controls.SetChildIndex(this.brk_Zones, 0);
      this.Controls.SetChildIndex(this.bracket1, 0);
      this.Controls.SetChildIndex(this.tlb_ZonesFosberKey, 0);
      this.Controls.SetChildIndex(this.ttb_ZonesCustomerKey, 0);
      this.Controls.SetChildIndex(this.chk_ZonesAvailable, 0);
      this.Controls.SetChildIndex(this.tlb_ReasonsFosberKey, 0);
      this.Controls.SetChildIndex(this.ttb_ReasonsCustomerKey, 0);
      this.Controls.SetChildIndex(this.chk_ReasonsAvailable, 0);
      this.Controls.SetChildIndex(this.lbl_PageStatus, 0);
      this.Controls.SetChildIndex(this.ttb_ZonesDescription, 0);
      this.Controls.SetChildIndex(this.ttb_ReasonsDescription, 0);
      this.Controls.SetChildIndex(this.chk_FosberZone, 0);
      this.Controls.SetChildIndex(this.chk_FosberReason, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		private void gdv_Zones_SelectionChanged(object sender, EventArgs e)
		{
			if (gdv_Zones.SelectedRows.Count == 1)
			{
        if (gdv_Zones.SelectedRows[0].Cells.Count > 2)
        {
          string zoneKey = gdv_Zones.SelectedRows[0].Cells[2].Value.ToString();
          string filter = "ZoneKey = '" + zoneKey + "'";
          try
          {
            dataviewReasons.RowFilter = filter;
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Zones_SelectionChanged - Error appling filter \"" + filter + "\"", ex);
          }
          FillZoneDetails();
          //select reasons grid first row 
          for (int k = gdv_Reasons.Rows.Count - 1; k >= 0; k--)
            gdv_Reasons.Rows[k].Selected = (k == 0);

          //PMASSARO_FI26/08/10 - Andava in errore il client qualora una zona DW era abilitata, ma non erano presenti causali 
          if (gdv_Reasons.Rows.Count > 0)
            gdv_Reasons.FirstDisplayedScrollingRowIndex = 0;
        }
			}
		}

		private void FillZoneDetails()
		{
			if (gdv_Zones.SelectedRows.Count == 1)
			{
        if (gdv_Zones.SelectedRows[0].Cells.Count > 2)
        {
          string zoneKey = gdv_Zones.SelectedRows[0].Cells[2].Value.ToString();
          DataRow row = tableZones.Rows.Find(zoneKey);
          if (row != null)
          {
            tlb_ZonesFosberKey.Value = row[DownTimeReason.INDEX_ZONE_KEY].ToString();
            ttb_ZonesCustomerKey.Value = row[DownTimeReason.INDEX_ZONE_CUSTOMER_KEY].ToString();
            chk_ZonesAvailable.Checked = (bool)row[DownTimeReason.INDEX_ZONE_AVAILABLE];
            ttb_ZonesDescription.Value = row[IndexZoneFirstLanguage].ToString();
            chk_ZonesStandard.Checked = (bool)row[DownTimeReason.INDEX_ZONE_STANDARD];
            //agrassi_FI19-03-08_1
            if (Accessibility.getAccessibility().isLevelHigh())
              this.chk_FosberZone.Checked = (bool)row[DownTimeReason.INDEX_ZONE_FOSBER];

          }
          else
          {
            tlb_ZonesFosberKey.Value = ttb_ZonesCustomerKey.Value = ttb_ZonesDescription.Value = string.Empty;
            chk_ZonesAvailable.Checked = chk_ZonesStandard.Checked = false;
          }
        }
			}
		}

		private void gdv_Reasons_SelectionChanged(object sender, EventArgs e)
		{
			FillReasonDetails();
		}

		private void FillReasonDetails()
		{
			if (gdv_Reasons.SelectedRows.Count == 1)
			{
        if (gdv_Reasons.SelectedRows[0].Cells.Count > 2)
        {
          string reasonKey = gdv_Reasons.SelectedRows[0].Cells[2].Value.ToString();
          DataRow row = tableReasons.Rows.Find(reasonKey);
          if (row != null)
          {
            tlb_ReasonsFosberKey.Value = row[DownTimeReason.INDEX_REASON_KEY].ToString();
            ttb_ReasonsCustomerKey.Value = row[DownTimeReason.INDEX_REASON_CUSTOMER_KEY].ToString();
            ttb_ReasonKeyZone.Value = row[DownTimeReason.INDEX_REASON_ZONEKEY].ToString();
            chk_ReasonsAvailable.Checked = (bool)row[DownTimeReason.INDEX_REASON_AVAILABLE];
            ttb_ReasonsDescription.Value = row[IndexReasonFirstLanguage].ToString();
            chk_ReasonsStandard.Checked = (bool)row[DownTimeReason.INDEX_REASON_STANDARD];
            //agrassi_FI19-03-08_1
            if (Accessibility.getAccessibility().isLevelHigh())
              this.chk_FosberReason.Checked = (bool)row[DownTimeReason.INDEX_REASON_FOSBER];
          }
          else
          {
            tlb_ReasonsFosberKey.Value = ttb_ReasonsCustomerKey.Value = ttb_ReasonKeyZone.Value = ttb_ReasonsDescription.Value = string.Empty;
            chk_ReasonsAvailable.Checked = chk_ReasonsStandard.Checked = false;
          }
        }
			}
		}

		private void gdv_Zones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
      
      DataRow dataRow = ((DataRowView)gdv_Zones.Rows[e.RowIndex].DataBoundItem).Row;

			if (this.gdv_Zones.Columns[e.ColumnIndex].Name == tableZones.Columns[DownTimeReason.INDEX_ZONE_KEY].ColumnName
						&& e.Value != null)
			{
				try
				{
          if (gdv_Zones.Rows[e.RowIndex].Cells.Count > 2)
          {
            DataRow dt = tableZones.Rows.Find(gdv_Zones.Rows[e.RowIndex].Cells[2].Value.ToString());
            if (dt != null && !(bool)dt[DownTimeReason.INDEX_ZONE_STANDARD])
              gdv_Zones.Rows[e.RowIndex].DefaultCellStyle.ForeColor =
                gdv_Zones.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = ColConv.Convert(SyncroColor.NoStandardDownTimeReasonColor);
          }
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_Zones_CellFormatting - " + DownTimeReason.INDEX_ZONE_KEY, ex);
				}
			}
		}

		private void gdv_reasons_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
 
      DataRow dataRow = ((DataRowView)gdv_Reasons.Rows[e.RowIndex].DataBoundItem).Row;

			if (this.gdv_Reasons.Columns[e.ColumnIndex].Name == tableReasons.Columns[DownTimeReason.INDEX_REASON_KEY].ColumnName
						&& e.Value != null)
			{
				try
				{
          if (gdv_Reasons.Rows[e.RowIndex].Cells.Count > 2)
          {
            DataRow dt = tableReasons.Rows.Find(gdv_Reasons.Rows[e.RowIndex].Cells[2].Value.ToString());
            if (dt != null &&
              !(bool)dt[DownTimeReason.INDEX_REASON_STANDARD])
              gdv_Reasons.Rows[e.RowIndex].DefaultCellStyle.ForeColor =
                gdv_Reasons.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = ColConv.Convert(SyncroColor.NoStandardDownTimeReasonColor);
          }
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_reasons_CellFormatting - " + tableReasons.Columns[DownTimeReason.INDEX_REASON_KEY].ColumnName, ex);
				}
			}
		}

    //agrassi_FI19-03-08_1
    //protected override void ControlD()
    //{
    //  debugOptions = !debugOptions;
    //  if (debugOptions)
    //  {
    //    ttb_ZonesDescription.Width = 326;
    //    ttb_ReasonsDescription.Width = 155;
    //    clm_Zones_Description.Width -= DEBUG_FOSBER_CODE_COLUMN_WIDTH;
    //    clm_Zones_FosberKey.Visible = true;
    //    clm_Reasons_Description.Width -= DEBUG_FOSBER_CODE_COLUMN_WIDTH;
    //    clm_Reasons_FosberKey.Visible = true;
    //  }
    //  else
    //  {
    //    ttb_ZonesDescription.Width = 500;
    //    ttb_ReasonsDescription.Width = 500;
    //    clm_Zones_Description.Width += DEBUG_FOSBER_CODE_COLUMN_WIDTH;
    //    clm_Zones_FosberKey.Visible = false;
    //    clm_Reasons_Description.Width += DEBUG_FOSBER_CODE_COLUMN_WIDTH;
    //    clm_Reasons_FosberKey.Visible = false;
    //  }
    //  gdv_Zones.Invalidate();
    //  gdv_Reasons.Invalidate();
    //}

		protected override void CtrlF1()
		{
			if (Accessibility.getAccessibility().isLevelAtLeastLow())
			{
				SetPageState(State.ModifyZone);
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

		protected override void CtrlF2()
		{
			if (Accessibility.getAccessibility().isLevelAtLeastLow())
			{
				SetPageState(State.ModifyReason);
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

		protected override void CtrlF4()
		{
			if (Accessibility.getAccessibility().isLevelAtLeastLow())
			{
				Frm_Confirm popup;
				switch (state)
				{
					case State.Read:
						break;

					case State.ModifyZone:
						popup = new Frm_Confirm();
						if (popup.ShowDialog(this, dbm.message.getTranslatedMessage("§SYCC_82")).Equals(DialogResult.OK))
						{
							//save selected zone
							if (gdv_Zones.SelectedRows.Count == 1)
							{
                if (gdv_Zones.SelectedRows[0].Cells.Count > 2)
                {
                  string zoneKey = gdv_Zones.SelectedRows[0].Cells[2].Value.ToString();
                  DataRow row = tableZones.Rows.Find(zoneKey);
                  if (row != null)
                  {
                    object[] items = row.ItemArray;
                    items[DownTimeReason.INDEX_ZONE_CUSTOMER_KEY] = ttb_ZonesCustomerKey.Value;
                    items[DownTimeReason.INDEX_ZONE_AVAILABLE] = chk_ZonesAvailable.Checked;
                    if (!((bool)row[DownTimeReason.INDEX_ZONE_STANDARD]))
                      items[IndexZoneFirstLanguage] = ttb_ZonesDescription.Value;
                    //agrassi_FI19-03-08_1
                    if (Accessibility.getAccessibility().isLevelHigh())
                      items[DownTimeReason.INDEX_ZONE_FOSBER] = this.chk_FosberZone.Checked;
                    row.ItemArray = items;
                    dbm.downTimeReason.Save();
                  }
                  else
                  {
                    Logger.LogError(this, "CtrlF4 (ModifyZone) - connot find row with key \"" + zoneKey + "\" into table Zones");
                  }
                }
							}
						}
						else
						{
							Escape();
						}
						break;

					case State.ModifyReason:
						popup = new Frm_Confirm();
						if (popup.ShowDialog(this, dbm.message.getTranslatedMessage("§SYCC_82")).Equals(DialogResult.OK))
						{
							//save selected reason
							if (gdv_Reasons.SelectedRows.Count == 1)
							{
                if (gdv_Reasons.SelectedRows[0].Cells.Count > 2)
                {
                  string reasonKey = gdv_Reasons.SelectedRows[0].Cells[2].Value.ToString();
                  DataRow row = tableReasons.Rows.Find(reasonKey);
                  if (row != null)
                  {
                    object[] items = row.ItemArray;
                    items[DownTimeReason.INDEX_REASON_CUSTOMER_KEY] = ttb_ReasonsCustomerKey.Value;
                    items[DownTimeReason.INDEX_REASON_AVAILABLE] = chk_ReasonsAvailable.Checked;
                    if (!((bool)row[DownTimeReason.INDEX_REASON_STANDARD]))
                      items[IndexReasonFirstLanguage] = ttb_ReasonsDescription.Value;
                    //agrassi_FI19-03-08_1
                    if (Accessibility.getAccessibility().isLevelHigh())
                      items[DownTimeReason.INDEX_REASON_FOSBER] = this.chk_FosberReason.Checked;
                    row.ItemArray = items;
                    dbm.downTimeReason.Save();
                  }
                  else
                  {
                    Logger.LogError(this, "CtrlF4 (ModifyReason) - connot find row with key \"" + reasonKey + "\" into table Reasons");
                  }
                }
							}
						}
						else
						{
							Escape();
						}
						break;
				}
				SetPageState(State.Read);
			}
			else
			{
				PageManager.getPageManager()._SetMessage("§SYCC_11");
			}
		}

		protected override void Escape()
		{
			SetPageState(State.Read);
		}

		private void SetPageState(State newState)
		{
			if (state != newState)
			{
				switch (newState)
				{ 
					case State.Read:
						gdv_Zones.Enabled = gdv_Zones.TabStop = gdv_Reasons.Enabled = gdv_Reasons.TabStop = true;
						if (state == State.ModifyZone)
							gdv_Zones.Focus();
						else // (state == State.ModifyZone)
							gdv_Reasons.Focus();
						ttb_ZonesCustomerKey.ReadOnly = chk_ZonesAvailable.ReadOnly = ttb_ZonesDescription.ReadOnly = true;
						ttb_ReasonsCustomerKey.ReadOnly = chk_ReasonsAvailable.ReadOnly = ttb_ReasonsDescription.ReadOnly = true;
            //agrassi_FI19-03-08_1
            chk_FosberZone.ReadOnly = chk_FosberReason.ReadOnly = true;
            FillReasonDetails();
						FillZoneDetails();
						lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
						lbl_CtrlF2.BackColor = lbl_CtrlF2_YY.BackColor = lbl_Title_YN.BackColor;
						break;

					case State.ModifyZone:
						gdv_Zones.Enabled = gdv_Zones.TabStop = gdv_Reasons.Enabled = gdv_Reasons.TabStop = false;
						ttb_ZonesCustomerKey.ReadOnly = chk_ZonesAvailable.ReadOnly = ttb_ZonesDescription.ReadOnly = false;
						ttb_ZonesCustomerKey.Focus();
						ttb_ReasonsCustomerKey.ReadOnly = chk_ReasonsAvailable.ReadOnly = ttb_ReasonsDescription.ReadOnly = true;
            //agrassi_FI19-03-08_1
            chk_FosberZone.ReadOnly = false;
            chk_FosberReason.ReadOnly = true;
            lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor = lbl_Status.BackColor;
						if (state == State.ModifyReason)
							FillReasonDetails();
						break;
					
					case State.ModifyReason:	
						gdv_Zones.Enabled = gdv_Zones.TabStop = gdv_Reasons.Enabled = gdv_Reasons.TabStop = false;
						ttb_ZonesCustomerKey.ReadOnly = chk_ZonesAvailable.ReadOnly = ttb_ZonesDescription.ReadOnly = true;
						ttb_ReasonsCustomerKey.ReadOnly = chk_ReasonsAvailable.ReadOnly = ttb_ReasonsDescription.ReadOnly = false;
            //agrassi_FI19-03-08_1
            chk_FosberZone.ReadOnly = true;
            chk_FosberReason.ReadOnly = false;
            lbl_CtrlF2.BackColor = lbl_CtrlF2_YY.BackColor = lbl_Status.BackColor;
						ttb_ReasonsCustomerKey.Focus();
						if (state == State.ModifyZone)
							FillZoneDetails();
						break;
				}
				state = newState;
				lbl_PageStatus.Text = "Page status: " + state.ToString();
			}
		}
	}
}

