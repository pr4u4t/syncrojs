using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.Utility;
using SyncroC.DataBase;
using SyncroC.Controls.Colors;
using SyncroC.Network;
using SyncroC.DataBase.EventArgs;

namespace SyncroC.Forms.Pages.Statistics
{
	public class Frm_DowntimeReport : SyncroC.Forms.Pages.Statistics.Frm_StatisticsGroup
	{
		private SyncroC.Controls.GridView gdv_Reasons;
		private SyncroC.Controls.GridView gdv_Zones;
		private SyncroC.Controls.Title ttl_reasons;
		private SyncroC.Controls.Title ttl_Zones;
		private SyncroC.Controls.TitledMaskTextBox mkt_EndTime;
		private SyncroC.Controls.TitledMaskTextBox mkt_StartTime;
		private SyncroC.Controls.Title ttl_Downtimes;
		private SyncroC.Controls.GridView gdv_Downtimes;
		private SyncroC.Controls.Bracket brk_Details;
		private SyncroC.Controls.TitledTextBox ttb_Description;
		private Label lbl_ModID;
		private Label lbl_ModIDDescription;
		private Label lbl_SetupID;
		private Label lbl_SetupIDDescription;
		private System.ComponentModel.IContainer components = null;

		//downtime grid columns
		private DataGridViewColumn clm_Downtime_ShiftID;
		private DataGridViewColumn clm_Downtime_StartTime;
		//zones grid columns
		private DataGridViewColumn clm_Zones_Description;
		private DataGridViewColumn clm_Zones_Code;
    //MSTAGETTI_FI-25-06-2010
    private DataGridViewColumn clm_Zones_CustomerKey;
		//reasons grid columns
		private DataGridViewColumn clm_Reasons_Description;
		private DataGridViewColumn clm_Reasons_Code;
    //MSTAGETTI_FI-25-06-2010
    private DataGridViewColumn clm_Reasons_CustomerKey;

		private DataTable tableZones;
		private DataTable tableReasons;
		//private DataTable tableDownTime;
		private DataView dataviewZones;
		private DataView dataviewReasons;
		private Label lbl_pageStatus;
		private Label lbl_pageStatusDescription;

		private Label lbl_ReasonsFilter;
		private Label lbl_ReasonsFilterDescr;
		private Label lbl_ZonesFilter;
		private Label lbl_ZonesFilterDescr;

		private int COLUMN_CODE_DEBUG_WIDTH = 70;
		private bool debugOptions = false;

		//page status
		private enum State {Modify, Read };
		private State state;

		//DBManager dbm;

		private int IndexZoneFirstLanguage;
		private Label lbl_OrderNumber1;
		private Label lbl_OrderNumber2;
		private Label lbl_OrderNumber3;
		private Label lbl_FluteQuality;
		private Label lbl_SetupCode;
    private SyncroC.Controls.Title ttl_DowntimeStats;
    private SyncroC.Controls.TitledTextBox ttb_Duration;
		private int IndexReasonFirstLanguage;

		public Frm_DowntimeReport()
		{
			InitializeComponent();

			//dbm = DBManager.getDBManager();
      //agrassi_FI07-07-08
      //IndexZoneFirstLanguage = dbm.downTimeReason.getIndexZoneFromFirstLanguage();
      //IndexReasonFirstLanguage = dbm.downTimeReason.getIndexReasonFromFirstLanguage();
      IndexZoneFirstLanguage = -1;
      IndexReasonFirstLanguage = -1;

      if (DBManager.getDBManager().downTimeReason.downTimeReason.Tables.Count < 2)
      {
        Logger.LogError(this, "Frm_DowntimeReport - downtimeReason table does not exist");
      }
      else
      {
        //tableDownTime = dbm.downTime.downTime.Tables[DownTime.DOWNTIME_LIST];
        //this.gdv_Downtimes.DataSource = tableDownTime;

        this.gdv_Downtimes.DataSource = DBManager.getDBManager().downTime.getTable(false);

      //agrassi_FI07-07-08
      //  tableZones = dbm.downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_ZONE];
      //  dataviewZones = new DataView(tableZones);
      //  setDataViewZonesFilter("");
      //  this.gdv_Zones.DataSource = dataviewZones;

      //  tableReasons = dbm.downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_REASON];
      //  dataviewReasons = new DataView(tableReasons);
      //  setDataViewReasonsFilter("");
      //  this.gdv_Reasons.DataSource = dataviewReasons;

        #region gdv_Downtimes initialization

        clm_Downtime_ShiftID = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Downtime_ShiftID.HeaderText = "Shift";
        clm_Downtime_ShiftID.Name = DownTime.SHIFT_ID;
        clm_Downtime_ShiftID.DataPropertyName = DownTime.SHIFT_ID;
        clm_Downtime_ShiftID.Width = 80;

        clm_Downtime_StartTime = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Downtime_StartTime.HeaderText = "Start";
        clm_Downtime_StartTime.Name = DownTime.START_TIME;
        clm_Downtime_StartTime.DataPropertyName = DownTime.START_TIME;
        clm_Downtime_StartTime.Width = 180;

        gdv_Downtimes.Columns.Add(clm_Downtime_ShiftID);
        gdv_Downtimes.Columns.Add(clm_Downtime_StartTime);
        gdv_Downtimes.Sort(clm_Downtime_StartTime, ListSortDirection.Descending);

        #endregion

      //agrassi_FI07-07-08
      //  #region gdv_Zones initialization

      //  clm_Zones_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
      //  clm_Zones_Description.HeaderText = "Description";
      //  clm_Zones_Description.Name = tableZones.Columns[IndexZoneFirstLanguage].ColumnName;
      //  clm_Zones_Description.DataPropertyName = tableZones.Columns[IndexZoneFirstLanguage].ColumnName;
      //  clm_Zones_Description.Width = 330;

      //  clm_Zones_Code = new DataGridViewColumn(new DataGridViewTextBoxCell());
      //  clm_Zones_Code.HeaderText = "Code";
      //  clm_Zones_Code.Name = tableZones.Columns[DownTimeReason.INDEX_ZONE_KEY].ColumnName;
      //  clm_Zones_Code.DataPropertyName = tableZones.Columns[DownTimeReason.INDEX_ZONE_KEY].ColumnName;
      //  clm_Zones_Code.Visible = false;
      //  clm_Zones_Code.Width = COLUMN_CODE_DEBUG_WIDTH;

      //  gdv_Zones.Columns.Add(clm_Zones_Description);
      //  gdv_Zones.Columns.Add(clm_Zones_Code);
      //  gdv_Zones.Sort(clm_Zones_Code, ListSortDirection.Ascending);

      //  #endregion

      //  #region gdv_Reasons initialization

      //  clm_Reasons_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
      //  clm_Reasons_Description.HeaderText = "Description";
      //  clm_Reasons_Description.Name = tableReasons.Columns[IndexReasonFirstLanguage].ColumnName;
      //  clm_Reasons_Description.DataPropertyName = tableReasons.Columns[IndexReasonFirstLanguage].ColumnName;
      //  clm_Reasons_Description.Width = 330;

      //  clm_Reasons_Code = new DataGridViewColumn(new DataGridViewTextBoxCell());
      //  clm_Reasons_Code.HeaderText = "Code";
      //  clm_Reasons_Code.Name = tableReasons.Columns[DownTimeReason.INDEX_REASON_KEY].ColumnName;
      //  clm_Reasons_Code.DataPropertyName = tableReasons.Columns[DownTimeReason.INDEX_REASON_KEY].ColumnName;
      //  clm_Reasons_Code.Visible = false;
      //  clm_Reasons_Code.Width = COLUMN_CODE_DEBUG_WIDTH;

      //  gdv_Reasons.Columns.Add(clm_Reasons_Description);
      //  gdv_Reasons.Columns.Add(clm_Reasons_Code);
      //  gdv_Reasons.Sort(clm_Reasons_Code, ListSortDirection.Ascending);

      //  #endregion

      }
      this.SetTables();

      //tableDownTime.RowChanged += new DataRowChangeEventHandler(tableDownTime_RowChanged);
      DBManager.getDBManager().downTime.OnDBDowntimeChange += new DownTime.DBChangedHandler(tableDownTime_RowChanged);
      
      if (!SyncroC.Utility.Settings.getSettings().facility.AssignDowntimes)
			{
				this.lbl_CtrlF2_YY.Text = "";
				this.lbl_CtrlF4_YY.Text = "";
			}
		}

    public override void refreshMySQLData()
    {
      try
      {
        DataRow row = null;
        int FirstDisplayedScrollingRowIndex = this.gdv_Downtimes.FirstDisplayedScrollingRowIndex;

        if (this.gdv_Downtimes != null)
          if (this.gdv_Downtimes.SelectedRows != null)
            if (this.gdv_Downtimes.SelectedRows.Count != 0)
              row = ((DataRowView)this.gdv_Downtimes.SelectedRows[0].DataBoundItem).Row;

        this.gdv_Downtimes.SuspendLayout();

        this.gdv_Downtimes.DataSource = DBManager.getDBManager().downTime.getTable(false);

        for (int i = 0; i < this.gdv_Downtimes.Rows.Count; i++)
        {
          if (row != null)
          {
            string a = ((DataRowView)this.gdv_Downtimes.Rows[i].DataBoundItem).Row[DownTime.START_TIME].ToString().Trim();
            string b = row[DownTime.START_TIME].ToString().Trim();
            if (a == b)
            {
              this.gdv_Downtimes.Rows[i].Selected = true;
            }
          }
        }

        if ((row == null) && (this.gdv_Downtimes.Rows.Count > 0))
          this.gdv_Downtimes.Rows[0].Selected = true;

        if ((FirstDisplayedScrollingRowIndex >= 0) && (FirstDisplayedScrollingRowIndex <= DBManager.getDBManager().downTime.getCount()))
        {
          if (this.gdv_Downtimes.FirstDisplayedScrollingRowIndex != FirstDisplayedScrollingRowIndex)
            this.gdv_Downtimes.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex;
        }

        this.gdv_Downtimes.ResumeLayout();

        this.gdv_Downtimes.Focus();
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    //agrassi_FI07-07-08
    void SetTables()
    {
      if ((this.IndexZoneFirstLanguage == DBManager.getDBManager().downTimeReason.getIndexZoneFromFirstLanguage()) &&
         (this.IndexReasonFirstLanguage == DBManager.getDBManager().downTimeReason.getIndexReasonFromFirstLanguage()))
        return;
      IndexZoneFirstLanguage = DBManager.getDBManager().downTimeReason.getIndexZoneFromFirstLanguage();
      IndexReasonFirstLanguage = DBManager.getDBManager().downTimeReason.getIndexReasonFromFirstLanguage();
      if (DBManager.getDBManager().downTimeReason.downTimeReason.Tables.Count < 2)
      {
        Logger.LogError(this, "Frm_DowntimeReport - downtimeReason table does not exist");
      }
      else
      {
        tableZones = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_ZONE];
        dataviewZones = new DataView(tableZones);
        setDataViewZonesFilter("");
        this.gdv_Zones.DataSource = dataviewZones;

        tableReasons = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_REASON];
        dataviewReasons = new DataView(tableReasons);
        setDataViewReasonsFilter("");
        this.gdv_Reasons.DataSource = dataviewReasons;

        #region gdv_Zones initialization

        clm_Zones_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Zones_Description.HeaderText = "Description";
        //agrassiFI02-07-09_1
        if (IndexZoneFirstLanguage < tableZones.Columns.Count)
        {
          clm_Zones_Description.Name = tableZones.Columns[IndexZoneFirstLanguage].ColumnName;
          clm_Zones_Description.DataPropertyName = tableZones.Columns[IndexZoneFirstLanguage].ColumnName;
        }
        clm_Zones_Description.Width = 400;

        clm_Zones_Code = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Zones_Code.HeaderText = "Code";
        clm_Zones_Code.Name = tableZones.Columns[DownTimeReason.INDEX_ZONE_KEY].ColumnName;
        clm_Zones_Code.DataPropertyName = tableZones.Columns[DownTimeReason.INDEX_ZONE_KEY].ColumnName;
        clm_Zones_Code.Visible = false;
        clm_Zones_Code.Width = COLUMN_CODE_DEBUG_WIDTH;

        //MSTAGETTI_FI-25-06-2010
        clm_Zones_CustomerKey = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Zones_CustomerKey.HeaderText = "CustomerCode";
        clm_Zones_CustomerKey.Name = tableReasons.Columns[DownTimeReason.INDEX_ZONE_CUSTOMER_KEY].ColumnName;
        clm_Zones_CustomerKey.DataPropertyName = tableReasons.Columns[DownTimeReason.INDEX_ZONE_CUSTOMER_KEY].ColumnName;
        clm_Zones_CustomerKey.Visible = false;
        clm_Zones_CustomerKey.Width = COLUMN_CODE_DEBUG_WIDTH;

        gdv_Zones.Columns.Clear();
        gdv_Zones.Columns.Add(clm_Zones_Description);
        gdv_Zones.Columns.Add(clm_Zones_Code);
        //MSTAGETTI_FI-25-06-2010
        gdv_Zones.Columns.Add(clm_Zones_CustomerKey);
        gdv_Zones.Sort(clm_Zones_Code, ListSortDirection.Ascending);

        #endregion

        #region gdv_Reasons initialization

        clm_Reasons_Description = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Reasons_Description.HeaderText = "Description";
        //agrassiFI02-07-09_1
        if (IndexReasonFirstLanguage < tableReasons.Columns.Count)
        {
          clm_Reasons_Description.Name = tableReasons.Columns[IndexReasonFirstLanguage].ColumnName;
          clm_Reasons_Description.DataPropertyName = tableReasons.Columns[IndexReasonFirstLanguage].ColumnName;
        }
        clm_Reasons_Description.Width = 400;

        clm_Reasons_Code = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Reasons_Code.HeaderText = "Code";
        clm_Reasons_Code.Name = tableReasons.Columns[DownTimeReason.INDEX_REASON_KEY].ColumnName;
        clm_Reasons_Code.DataPropertyName = tableReasons.Columns[DownTimeReason.INDEX_REASON_KEY].ColumnName;
        clm_Reasons_Code.Visible = false;
        clm_Reasons_Code.Width = COLUMN_CODE_DEBUG_WIDTH;

        //MSTAGETTI_FI-25-06-2010
        clm_Reasons_CustomerKey = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_Reasons_CustomerKey.HeaderText = "CustomerCode";
        clm_Reasons_CustomerKey.Name = tableReasons.Columns[DownTimeReason.INDEX_REASON_CUSTOMER_KEY].ColumnName;
        clm_Reasons_CustomerKey.DataPropertyName = tableReasons.Columns[DownTimeReason.INDEX_REASON_CUSTOMER_KEY].ColumnName;
        clm_Reasons_CustomerKey.Visible = false;
        clm_Reasons_CustomerKey.Width = COLUMN_CODE_DEBUG_WIDTH;

        gdv_Reasons.Columns.Clear();
        gdv_Reasons.Columns.Add(clm_Reasons_Description);
        gdv_Reasons.Columns.Add(clm_Reasons_Code);
        //MSTAGETTI_FI-25-06-2010
        gdv_Reasons.Columns.Add(clm_Reasons_CustomerKey);
        gdv_Reasons.Sort(clm_Reasons_Code, ListSortDirection.Ascending);

        #endregion

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

    void tableDownTime_RowChanged(object sender, DBChangedEventArgs e)
		{
			showSelectedDowntime();
		}

		/// <summary>
		/// Create and apply the filter at the zones dataview. 
		/// The filter is the default on the available column AND the passed filter as parameter.
		/// </summary>
		private void setDataViewZonesFilter(string filter)
		{
			string s = tableZones.Columns[DownTimeReason.INDEX_ZONE_AVAILABLE].ColumnName + " = true"; ;
			if (filter != null && filter != string.Empty)
				s += " AND " + filter;
			try 
			{
				dataviewZones.RowFilter = s;
				lbl_ZonesFilter.Text = s;
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "setDataViewZonesFilter - Error appling filter \"" + s + "\"", ex);
				dataviewZones.RowFilter = string.Empty;
				lbl_ZonesFilter.Text = string.Empty;
			}
		}

		/// <summary>
		/// Create and apply the filter at the reasons dataview. 
		/// The filter is the default on the available column AND the passed filter as parameter.
		/// </summary>
		private void setDataViewReasonsFilter(string filter)
		{
			string s = tableReasons.Columns[DownTimeReason.INDEX_REASON_AVAILABLE].ColumnName + " = true";
			if (filter != null && filter != string.Empty)
				s += " AND " + filter;
			try 
			{
				dataviewReasons.RowFilter = s;
				lbl_ReasonsFilter.Text = s;
        for (int i = 0; i < gdv_Reasons.Rows.Count; i++)
        {
          string rKey = gdv_Reasons.Rows[i].Cells[1].Value.ToString();
          DataRow r = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_REASON].Rows.Find(rKey);
          if (r != null)
          {
            bool flagFosber = (bool)r[DownTimeReason.INDEX_REASON_FOSBER];
            gdv_Reasons.Rows[i].DefaultCellStyle.SelectionForeColor = (flagFosber ? Color.White : Color.DarkBlue);
            gdv_Reasons.Rows[i].DefaultCellStyle.ForeColor = (flagFosber ? Color.Black : Color.DarkBlue);
          }
        }
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "setDataViewReasonsFilter - Error appling filter \"" + s + "\"", ex);
				dataviewReasons.RowFilter = string.Empty;
				lbl_ReasonsFilter.Text = string.Empty;
			}
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

			this.gdv_Downtimes.DataSource = null;
			this.gdv_Zones.DataSource = null;
			this.gdv_Reasons.DataSource = null;
      DBManager.getDBManager().downTime.OnDBDowntimeChange -= new DownTime.DBChangedHandler(tableDownTime_RowChanged);

			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DowntimeReport));
      this.gdv_Reasons = new SyncroC.Controls.GridView();
      this.gdv_Zones = new SyncroC.Controls.GridView();
      this.ttl_reasons = new SyncroC.Controls.Title();
      this.ttl_Zones = new SyncroC.Controls.Title();
      this.mkt_EndTime = new SyncroC.Controls.TitledMaskTextBox();
      this.mkt_StartTime = new SyncroC.Controls.TitledMaskTextBox();
      this.ttl_Downtimes = new SyncroC.Controls.Title();
      this.gdv_Downtimes = new SyncroC.Controls.GridView();
      this.brk_Details = new SyncroC.Controls.Bracket();
      this.ttb_Description = new SyncroC.Controls.TitledTextBox();
      this.lbl_ModID = new System.Windows.Forms.Label();
      this.lbl_ModIDDescription = new System.Windows.Forms.Label();
      this.lbl_SetupID = new System.Windows.Forms.Label();
      this.lbl_SetupIDDescription = new System.Windows.Forms.Label();
      this.lbl_pageStatus = new System.Windows.Forms.Label();
      this.lbl_pageStatusDescription = new System.Windows.Forms.Label();
      this.lbl_ReasonsFilter = new System.Windows.Forms.Label();
      this.lbl_ReasonsFilterDescr = new System.Windows.Forms.Label();
      this.lbl_ZonesFilter = new System.Windows.Forms.Label();
      this.lbl_ZonesFilterDescr = new System.Windows.Forms.Label();
      this.lbl_OrderNumber1 = new System.Windows.Forms.Label();
      this.lbl_OrderNumber2 = new System.Windows.Forms.Label();
      this.lbl_OrderNumber3 = new System.Windows.Forms.Label();
      this.lbl_FluteQuality = new System.Windows.Forms.Label();
      this.lbl_SetupCode = new System.Windows.Forms.Label();
      this.ttl_DowntimeStats = new SyncroC.Controls.Title();
      this.ttb_Duration = new SyncroC.Controls.TitledTextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_Tab2
      // 
      this.tab_Tab2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab2.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "SAVE";
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Text = "ASSIGN";
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
      this.gdv_Reasons.Location = new System.Drawing.Point(802, 408);
      this.gdv_Reasons.MultiSelect = false;
      this.gdv_Reasons.Name = "gdv_Reasons";
      this.gdv_Reasons.RowsHeight = 32;
      this.gdv_Reasons.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Reasons.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Reasons.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Reasons.Size = new System.Drawing.Size(400, 200);
      this.gdv_Reasons.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Reasons.TabIndex = 117;
      this.gdv_Reasons.TabStop = false;
      this.gdv_Reasons.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Reasons.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Reasons.TranslateText = true;
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
      this.gdv_Zones.Location = new System.Drawing.Point(377, 408);
      this.gdv_Zones.MultiSelect = false;
      this.gdv_Zones.Name = "gdv_Zones";
      this.gdv_Zones.RowsHeight = 32;
      this.gdv_Zones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Zones.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Zones.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Zones.Size = new System.Drawing.Size(400, 200);
      this.gdv_Zones.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Zones.TabIndex = 116;
      this.gdv_Zones.TabStop = false;
      this.gdv_Zones.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Zones.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Zones.TranslateText = true;
      this.gdv_Zones.SelectionChanged += new System.EventHandler(this.gdv_Zones_SelectionChanged);
      // 
      // ttl_reasons
      // 
      this.ttl_reasons.BackColor = System.Drawing.Color.Black;
      this.ttl_reasons.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_reasons.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_reasons.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_reasons.Location = new System.Drawing.Point(792, 380);
      this.ttl_reasons.Name = "ttl_reasons";
      this.ttl_reasons.Size = new System.Drawing.Size(408, 228);
      this.ttl_reasons.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_reasons.TabIndex = 115;
      this.ttl_reasons.Text = "Downtime reasons";
      this.ttl_reasons.TranslateText = true;
      // 
      // ttl_Zones
      // 
      this.ttl_Zones.BackColor = System.Drawing.Color.Black;
      this.ttl_Zones.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Zones.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Zones.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Zones.Location = new System.Drawing.Point(367, 380);
      this.ttl_Zones.Name = "ttl_Zones";
      this.ttl_Zones.Size = new System.Drawing.Size(416, 228);
      this.ttl_Zones.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Zones.TabIndex = 114;
      this.ttl_Zones.Text = "Downtime zones";
      this.ttl_Zones.TranslateText = true;
      // 
      // mkt_EndTime
      // 
      this.mkt_EndTime.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.mkt_EndTime.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.mkt_EndTime.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.mkt_EndTime.ImagePassword = null;
      this.mkt_EndTime.Location = new System.Drawing.Point(367, 271);
      this.mkt_EndTime.Name = "mkt_EndTime";
      this.mkt_EndTime.PasswordChar = '\0';
      this.mkt_EndTime.ReadOnly = true;
      this.mkt_EndTime.Size = new System.Drawing.Size(261, 54);
      this.mkt_EndTime.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.mkt_EndTime.TabIndex = 135;
      this.mkt_EndTime.TabStop = false;
      this.mkt_EndTime.Text = "End time";
      this.mkt_EndTime.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.TimeAndDate;
      this.mkt_EndTime.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.mkt_EndTime.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.mkt_EndTime.TranslateText = true;
      this.mkt_EndTime.Value = " ";
      this.mkt_EndTime.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.mkt_EndTime.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.mkt_EndTime.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.mkt_EndTime.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.mkt_EndTime.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // mkt_StartTime
      // 
      this.mkt_StartTime.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.mkt_StartTime.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.mkt_StartTime.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.mkt_StartTime.ImagePassword = null;
      this.mkt_StartTime.Location = new System.Drawing.Point(367, 217);
      this.mkt_StartTime.Name = "mkt_StartTime";
      this.mkt_StartTime.PasswordChar = '\0';
      this.mkt_StartTime.ReadOnly = true;
      this.mkt_StartTime.Size = new System.Drawing.Size(261, 54);
      this.mkt_StartTime.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.mkt_StartTime.TabIndex = 134;
      this.mkt_StartTime.TabStop = false;
      this.mkt_StartTime.Text = "Start time";
      this.mkt_StartTime.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.TimeAndDate;
      this.mkt_StartTime.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.mkt_StartTime.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.mkt_StartTime.TranslateText = true;
      this.mkt_StartTime.Value = " ";
      this.mkt_StartTime.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.mkt_StartTime.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.mkt_StartTime.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.mkt_StartTime.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.mkt_StartTime.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttl_Downtimes
      // 
      this.ttl_Downtimes.BackColor = System.Drawing.Color.Black;
      this.ttl_Downtimes.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Downtimes.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Downtimes.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Downtimes.Location = new System.Drawing.Point(30, 193);
      this.ttl_Downtimes.Name = "ttl_Downtimes";
      this.ttl_Downtimes.Size = new System.Drawing.Size(290, 519);
      this.ttl_Downtimes.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Downtimes.TabIndex = 136;
      this.ttl_Downtimes.Text = "DOWN TIMES";
      this.ttl_Downtimes.TranslateText = true;
      // 
      // gdv_Downtimes
      // 
      this.gdv_Downtimes.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Downtimes.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Downtimes.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Downtimes.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Downtimes.AutoGenerateColumns = false;
      this.gdv_Downtimes.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Downtimes.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Large;
      this.gdv_Downtimes.DataMember = "";
      this.gdv_Downtimes.DataSource = null;
      this.gdv_Downtimes.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.gdv_Downtimes.ForeColor = System.Drawing.Color.Black;
      this.gdv_Downtimes.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Downtimes.ImagePassword = null;
      this.gdv_Downtimes.Location = new System.Drawing.Point(40, 221);
      this.gdv_Downtimes.MultiSelect = false;
      this.gdv_Downtimes.Name = "gdv_Downtimes";
      this.gdv_Downtimes.RowsHeight = 32;
      this.gdv_Downtimes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Downtimes.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Downtimes.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Downtimes.Size = new System.Drawing.Size(280, 491);
      this.gdv_Downtimes.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Downtimes.TabIndex = 137;
      this.gdv_Downtimes.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Downtimes.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Downtimes.TranslateText = true;
      this.gdv_Downtimes.SelectionChanged += new System.EventHandler(this.gdv_Downtimes_SelectionChanged);
      this.gdv_Downtimes.Leave += new System.EventHandler(this.gdv_Downtimes_Leave);
      this.gdv_Downtimes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Downtimes_CellFormatting);
      // 
      // brk_Details
      // 
      this.brk_Details.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.brk_Details.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.brk_Details.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_Details.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_Details.Location = new System.Drawing.Point(326, 193);
      this.brk_Details.Name = "brk_Details";
      this.brk_Details.Position = SyncroC.Controls.BracketPosition.Right;
      this.brk_Details.ShowArrow = true;
      this.brk_Details.Size = new System.Drawing.Size(875, 519);
      this.brk_Details.TabIndex = 138;
      this.brk_Details.Text = "DETAILS";
      this.brk_Details.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_Details.TranslateText = true;
      // 
      // ttb_Description
      // 
      this.ttb_Description.AutoScroll = true;
      this.ttb_Description.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_Description.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.ttb_Description.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Description.ImagePassword = null;
      this.ttb_Description.Location = new System.Drawing.Point(367, 611);
      this.ttb_Description.MaxChar = 500;
      this.ttb_Description.Multiline = true;
      this.ttb_Description.Name = "ttb_Description";
      this.ttb_Description.PasswordChar = '\0';
      this.ttb_Description.ReadOnly = true;
      this.ttb_Description.Size = new System.Drawing.Size(834, 73);
      this.ttb_Description.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_Description.TabIndex = 139;
      this.ttb_Description.TabStop = false;
      this.ttb_Description.Text = "Description";
      this.ttb_Description.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_Description.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Description.TranslateText = true;
      this.ttb_Description.Value = " ";
      this.ttb_Description.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // lbl_ModID
      // 
      this.lbl_ModID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ModID.Font = new System.Drawing.Font("Arial Unicode MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_ModID.Location = new System.Drawing.Point(743, 56);
      this.lbl_ModID.Name = "lbl_ModID";
      this.lbl_ModID.Size = new System.Drawing.Size(179, 20);
      this.lbl_ModID.TabIndex = 146;
      this.lbl_ModID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_ModID.Visible = false;
      // 
      // lbl_ModIDDescription
      // 
      this.lbl_ModIDDescription.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ModIDDescription.Font = new System.Drawing.Font("Arial Unicode MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_ModIDDescription.Location = new System.Drawing.Point(657, 56);
      this.lbl_ModIDDescription.Name = "lbl_ModIDDescription";
      this.lbl_ModIDDescription.Size = new System.Drawing.Size(86, 20);
      this.lbl_ModIDDescription.TabIndex = 145;
      this.lbl_ModIDDescription.Text = "ModID:";
      this.lbl_ModIDDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_ModIDDescription.Visible = false;
      // 
      // lbl_SetupID
      // 
      this.lbl_SetupID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_SetupID.Font = new System.Drawing.Font("Arial Unicode MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_SetupID.Location = new System.Drawing.Point(443, 56);
      this.lbl_SetupID.Name = "lbl_SetupID";
      this.lbl_SetupID.Size = new System.Drawing.Size(208, 20);
      this.lbl_SetupID.TabIndex = 144;
      this.lbl_SetupID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_SetupID.Visible = false;
      // 
      // lbl_SetupIDDescription
      // 
      this.lbl_SetupIDDescription.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_SetupIDDescription.Font = new System.Drawing.Font("Arial Unicode MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_SetupIDDescription.Location = new System.Drawing.Point(333, 56);
      this.lbl_SetupIDDescription.Name = "lbl_SetupIDDescription";
      this.lbl_SetupIDDescription.Size = new System.Drawing.Size(110, 20);
      this.lbl_SetupIDDescription.TabIndex = 143;
      this.lbl_SetupIDDescription.Text = "Setup ID:";
      this.lbl_SetupIDDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_SetupIDDescription.Visible = false;
      // 
      // lbl_pageStatus
      // 
      this.lbl_pageStatus.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_pageStatus.Font = new System.Drawing.Font("Arial Unicode MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_pageStatus.Location = new System.Drawing.Point(1078, 56);
      this.lbl_pageStatus.Name = "lbl_pageStatus";
      this.lbl_pageStatus.Size = new System.Drawing.Size(179, 20);
      this.lbl_pageStatus.TabIndex = 148;
      this.lbl_pageStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_pageStatus.Visible = false;
      // 
      // lbl_pageStatusDescription
      // 
      this.lbl_pageStatusDescription.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_pageStatusDescription.Font = new System.Drawing.Font("Arial Unicode MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_pageStatusDescription.Location = new System.Drawing.Point(954, 56);
      this.lbl_pageStatusDescription.Name = "lbl_pageStatusDescription";
      this.lbl_pageStatusDescription.Size = new System.Drawing.Size(119, 20);
      this.lbl_pageStatusDescription.TabIndex = 147;
      this.lbl_pageStatusDescription.Text = "Page status:";
      this.lbl_pageStatusDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_pageStatusDescription.Visible = false;
      // 
      // lbl_ReasonsFilter
      // 
      this.lbl_ReasonsFilter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ReasonsFilter.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_ReasonsFilter.Location = new System.Drawing.Point(789, -5);
      this.lbl_ReasonsFilter.Name = "lbl_ReasonsFilter";
      this.lbl_ReasonsFilter.Size = new System.Drawing.Size(500, 20);
      this.lbl_ReasonsFilter.TabIndex = 152;
      this.lbl_ReasonsFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_ReasonsFilter.Visible = false;
      // 
      // lbl_ReasonsFilterDescr
      // 
      this.lbl_ReasonsFilterDescr.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ReasonsFilterDescr.Font = new System.Drawing.Font("Arial Unicode MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_ReasonsFilterDescr.Location = new System.Drawing.Point(660, -5);
      this.lbl_ReasonsFilterDescr.Name = "lbl_ReasonsFilterDescr";
      this.lbl_ReasonsFilterDescr.Size = new System.Drawing.Size(123, 20);
      this.lbl_ReasonsFilterDescr.TabIndex = 151;
      this.lbl_ReasonsFilterDescr.Text = "Reasons filter:";
      this.lbl_ReasonsFilterDescr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_ReasonsFilterDescr.Visible = false;
      // 
      // lbl_ZonesFilter
      // 
      this.lbl_ZonesFilter.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ZonesFilter.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_ZonesFilter.Location = new System.Drawing.Point(108, -5);
      this.lbl_ZonesFilter.Name = "lbl_ZonesFilter";
      this.lbl_ZonesFilter.Size = new System.Drawing.Size(546, 20);
      this.lbl_ZonesFilter.TabIndex = 150;
      this.lbl_ZonesFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_ZonesFilter.Visible = false;
      // 
      // lbl_ZonesFilterDescr
      // 
      this.lbl_ZonesFilterDescr.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ZonesFilterDescr.Font = new System.Drawing.Font("Arial Unicode MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_ZonesFilterDescr.Location = new System.Drawing.Point(1, -5);
      this.lbl_ZonesFilterDescr.Name = "lbl_ZonesFilterDescr";
      this.lbl_ZonesFilterDescr.Size = new System.Drawing.Size(104, 20);
      this.lbl_ZonesFilterDescr.TabIndex = 149;
      this.lbl_ZonesFilterDescr.Text = "Zones filter:";
      this.lbl_ZonesFilterDescr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_ZonesFilterDescr.Visible = false;
      // 
      // lbl_OrderNumber1
      // 
      this.lbl_OrderNumber1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_OrderNumber1.Location = new System.Drawing.Point(950, 322);
      this.lbl_OrderNumber1.Name = "lbl_OrderNumber1";
      this.lbl_OrderNumber1.Size = new System.Drawing.Size(250, 30);
      this.lbl_OrderNumber1.TabIndex = 157;
      this.lbl_OrderNumber1.Text = "Commessa 1";
      this.lbl_OrderNumber1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_OrderNumber2
      // 
      this.lbl_OrderNumber2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_OrderNumber2.Location = new System.Drawing.Point(950, 291);
      this.lbl_OrderNumber2.Name = "lbl_OrderNumber2";
      this.lbl_OrderNumber2.Size = new System.Drawing.Size(250, 30);
      this.lbl_OrderNumber2.TabIndex = 156;
      this.lbl_OrderNumber2.Text = "Commessa 2";
      this.lbl_OrderNumber2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_OrderNumber3
      // 
      this.lbl_OrderNumber3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_OrderNumber3.Location = new System.Drawing.Point(950, 260);
      this.lbl_OrderNumber3.Name = "lbl_OrderNumber3";
      this.lbl_OrderNumber3.Size = new System.Drawing.Size(250, 30);
      this.lbl_OrderNumber3.TabIndex = 155;
      this.lbl_OrderNumber3.Text = "Commessa 3";
      this.lbl_OrderNumber3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_FluteQuality
      // 
      this.lbl_FluteQuality.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_FluteQuality.Location = new System.Drawing.Point(668, 310);
      this.lbl_FluteQuality.Name = "lbl_FluteQuality";
      this.lbl_FluteQuality.Size = new System.Drawing.Size(276, 30);
      this.lbl_FluteQuality.TabIndex = 154;
      this.lbl_FluteQuality.Text = "Flute - Quality";
      this.lbl_FluteQuality.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_SetupCode
      // 
      this.lbl_SetupCode.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupCode.Location = new System.Drawing.Point(668, 276);
      this.lbl_SetupCode.Name = "lbl_SetupCode";
      this.lbl_SetupCode.Size = new System.Drawing.Size(276, 30);
      this.lbl_SetupCode.TabIndex = 153;
      this.lbl_SetupCode.Text = "SetupCode";
      this.lbl_SetupCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // ttl_DowntimeStats
      // 
      this.ttl_DowntimeStats.BackColor = System.Drawing.Color.Black;
      this.ttl_DowntimeStats.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_DowntimeStats.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DowntimeStats.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_DowntimeStats.Location = new System.Drawing.Point(656, 221);
      this.ttl_DowntimeStats.Name = "ttl_DowntimeStats";
      this.ttl_DowntimeStats.Size = new System.Drawing.Size(545, 152);
      this.ttl_DowntimeStats.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_DowntimeStats.TabIndex = 158;
      this.ttl_DowntimeStats.Text = "ShiftId - OraDataTurno";
      this.ttl_DowntimeStats.TranslateText = false;
      // 
      // ttb_Duration
      // 
      this.ttb_Duration.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_Duration.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ttb_Duration.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Duration.ImagePassword = null;
      this.ttb_Duration.Location = new System.Drawing.Point(367, 323);
      this.ttb_Duration.MaxChar = 32767;
      this.ttb_Duration.Multiline = false;
      this.ttb_Duration.Name = "ttb_Duration";
      this.ttb_Duration.PasswordChar = '\0';
      this.ttb_Duration.ReadOnly = true;
      this.ttb_Duration.Size = new System.Drawing.Size(261, 54);
      this.ttb_Duration.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_Duration.TabIndex = 159;
      this.ttb_Duration.Text = "Duration";
      this.ttb_Duration.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_Duration.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Duration.TranslateText = true;
      this.ttb_Duration.Value = " ";
      this.ttb_Duration.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // Frm_DowntimeReport
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.ttb_Duration);
      this.Controls.Add(this.ttl_DowntimeStats);
      this.Controls.Add(this.lbl_OrderNumber1);
      this.Controls.Add(this.lbl_OrderNumber2);
      this.Controls.Add(this.lbl_OrderNumber3);
      this.Controls.Add(this.lbl_FluteQuality);
      this.Controls.Add(this.lbl_SetupCode);
      this.Controls.Add(this.lbl_ReasonsFilter);
      this.Controls.Add(this.lbl_ReasonsFilterDescr);
      this.Controls.Add(this.lbl_ZonesFilter);
      this.Controls.Add(this.lbl_ZonesFilterDescr);
      this.Controls.Add(this.lbl_pageStatus);
      this.Controls.Add(this.lbl_pageStatusDescription);
      this.Controls.Add(this.lbl_ModID);
      this.Controls.Add(this.lbl_ModIDDescription);
      this.Controls.Add(this.lbl_SetupID);
      this.Controls.Add(this.lbl_SetupIDDescription);
      this.Controls.Add(this.ttb_Description);
      this.Controls.Add(this.brk_Details);
      this.Controls.Add(this.gdv_Downtimes);
      this.Controls.Add(this.ttl_Downtimes);
      this.Controls.Add(this.mkt_EndTime);
      this.Controls.Add(this.mkt_StartTime);
      this.Controls.Add(this.gdv_Reasons);
      this.Controls.Add(this.gdv_Zones);
      this.Controls.Add(this.ttl_reasons);
      this.Controls.Add(this.ttl_Zones);
      this.Name = "Frm_DowntimeReport";
      this.Text = "Frm_DowntimeReport";
      this.Load += new System.EventHandler(this.Frm_DowntimeReport_Load);
      this.Activated += new System.EventHandler(this.Frm_DowntimeReport_Activated);
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
      this.Controls.SetChildIndex(this.ttl_Zones, 0);
      this.Controls.SetChildIndex(this.ttl_reasons, 0);
      this.Controls.SetChildIndex(this.gdv_Zones, 0);
      this.Controls.SetChildIndex(this.gdv_Reasons, 0);
      this.Controls.SetChildIndex(this.mkt_StartTime, 0);
      this.Controls.SetChildIndex(this.mkt_EndTime, 0);
      this.Controls.SetChildIndex(this.ttl_Downtimes, 0);
      this.Controls.SetChildIndex(this.gdv_Downtimes, 0);
      this.Controls.SetChildIndex(this.brk_Details, 0);
      this.Controls.SetChildIndex(this.ttb_Description, 0);
      this.Controls.SetChildIndex(this.lbl_SetupIDDescription, 0);
      this.Controls.SetChildIndex(this.lbl_SetupID, 0);
      this.Controls.SetChildIndex(this.lbl_ModIDDescription, 0);
      this.Controls.SetChildIndex(this.lbl_ModID, 0);
      this.Controls.SetChildIndex(this.lbl_pageStatusDescription, 0);
      this.Controls.SetChildIndex(this.lbl_pageStatus, 0);
      this.Controls.SetChildIndex(this.lbl_ZonesFilterDescr, 0);
      this.Controls.SetChildIndex(this.lbl_ZonesFilter, 0);
      this.Controls.SetChildIndex(this.lbl_ReasonsFilterDescr, 0);
      this.Controls.SetChildIndex(this.lbl_ReasonsFilter, 0);
      this.Controls.SetChildIndex(this.lbl_SetupCode, 0);
      this.Controls.SetChildIndex(this.lbl_FluteQuality, 0);
      this.Controls.SetChildIndex(this.lbl_OrderNumber3, 0);
      this.Controls.SetChildIndex(this.lbl_OrderNumber2, 0);
      this.Controls.SetChildIndex(this.lbl_OrderNumber1, 0);
      this.Controls.SetChildIndex(this.ttl_DowntimeStats, 0);
      this.Controls.SetChildIndex(this.ttb_Duration, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// F2 key behaviour: must not do nothing
		/// </summary>
		protected override void F2()
		{
		}

		/// <summary>
		/// Escape key behaviour: go back to the read mode.
		/// </summary>
		protected override void Escape()
		{
			setState(State.Read);
		}

		/// <summary>
		/// Ctrl+F2 key behaviour: Assign a downtime
		/// </summary>
		protected override void CtrlF2()
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.AssignDowntimes)
				return;

			setState(State.Modify);
		}

		/// <summary>
		/// Ctrl+F4 key behaviour: Save
		/// </summary>
		protected override void CtrlF4()
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.AssignDowntimes)
			{
				this.setState(State.Read);
				return;
			}

			if (state.Equals(State.Modify))
			{
				if (gdv_Zones.SelectedRows.Count != 1)
				{
					PageManager.getPageManager()._SetMessage("§SYCC_08", true);
				}
				else if (gdv_Reasons.SelectedRows.Count != 1)
				{
					PageManager.getPageManager()._SetMessage("§SYCC_09", true);
        }
        else if (gdv_Downtimes.SelectedRows.Count < 1)
        {
          PageManager.getPageManager()._SetMessage("§SYCC_09", true);
        }
        else
        {
          //DataRow dataRow = tableDownTime.Rows.Find(new object[] {
          //  (string)gdv_Downtimes.SelectedRows[0].Cells[0].Value,
          //  (DateTime)gdv_Downtimes.SelectedRows[0].Cells[1].Value});

          DataRow dataRow = DBManager.getDBManager().downTime.getRow((string)gdv_Downtimes.SelectedRows[0].Cells[0].Value, (DateTime)gdv_Downtimes.SelectedRows[0].Cells[1].Value);

          string command = Command.DB_DOWNTIME + "." + Command.DB_CMD_ADD + "(" +
            dataRow[DownTime.SHIFT_ID].ToString() + Command.PARAMETER_SEPARATOR +
            SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeString(
              (System.DateTime)dataRow[DownTime.START_TIME]).Trim() + Command.PARAMETER_SEPARATOR +
            SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeString(
              (System.DateTime)dataRow[DownTime.FINISH_TIME]).Trim() + Command.PARAMETER_SEPARATOR +
            gdv_Reasons.SelectedRows[0].Cells[1].Value.ToString().Trim() + Command.PARAMETER_SEPARATOR +
            gdv_Reasons.SelectedRows[0].Cells[0].Value.ToString().Trim() + Command.PARAMETER_SEPARATOR +
            gdv_Zones.SelectedRows[0].Cells[1].Value.ToString().Trim() + Command.PARAMETER_SEPARATOR +
            gdv_Zones.SelectedRows[0].Cells[0].Value.ToString().Trim() + Command.PARAMETER_SEPARATOR +
            ttb_Description.Value + Command.PARAMETER_SEPARATOR +
            dataRow[DownTime.SETUP_ID].ToString().Trim() + Command.PARAMETER_SEPARATOR +
            //MSTAGETTI_FI-25-06-2010
            //dataRow[DownTime.SETUP_MODID].ToString().Trim() +
            dataRow[DownTime.SETUP_MODID].ToString().Trim() + Command.PARAMETER_SEPARATOR +
            gdv_Zones.SelectedRows[0].Cells[2].Value.ToString().Trim() + Command.PARAMETER_SEPARATOR +
            gdv_Reasons.SelectedRows[0].Cells[2].Value.ToString().Trim() +
            ")";
          CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_DB_MANAGER, command);

          setState(State.Read);
        }
			}
		}

		protected override void ControlD()
		{
			debugOptions = !debugOptions;

			lbl_ModIDDescription.Visible = lbl_ModID.Visible =
							lbl_SetupIDDescription.Visible = lbl_SetupID.Visible =
							lbl_pageStatus.Visible = lbl_pageStatusDescription.Visible =
							lbl_ZonesFilterDescr.Visible = lbl_ZonesFilter.Visible =
							lbl_ReasonsFilterDescr.Visible = lbl_ReasonsFilter.Visible =
							debugOptions;

			if (debugOptions)
			{//show code columns
				clm_Reasons_Description.Width -= COLUMN_CODE_DEBUG_WIDTH;
				clm_Reasons_Code.Visible = true;
				clm_Zones_Description.Width -= COLUMN_CODE_DEBUG_WIDTH;
				clm_Zones_Code.Visible = true;
			}
			else
			{//hide code columns
				clm_Reasons_Description.Width += COLUMN_CODE_DEBUG_WIDTH;
				clm_Reasons_Code.Visible = false;
				clm_Zones_Description.Width += COLUMN_CODE_DEBUG_WIDTH;
				clm_Zones_Code.Visible = false;
			}
			gdv_Reasons.Invalidate();
			gdv_Zones.Invalidate();
		}

		private void gdv_Downtimes_SelectionChanged(object sender, EventArgs e)
		{
			showSelectedDowntime();
		}

		private void showSelectedDowntime()
		{
      if (gdv_Downtimes.SelectedRows.Count == 1)
      {
        //agrassi_FI07-07-08
        if (gdv_Downtimes.SelectedRows[0].Cells.Count > 1)
        {
          //DataRow downtimeRow = tableDownTime.Rows.Find(new object[] {
          //(string)gdv_Downtimes.SelectedRows[0].Cells[0].Value,
          //(DateTime)gdv_Downtimes.SelectedRows[0].Cells[1].Value});

          DataRow downtimeRow = DBManager.getDBManager().downTime.getRow((string)gdv_Downtimes.SelectedRows[0].Cells[0].Value, (DateTime)gdv_Downtimes.SelectedRows[0].Cells[1].Value);
          if (downtimeRow == null)
            return;

          DataRow rowOrder = DBManager.getDBManager().pastOrder.getRow(downtimeRow[DownTime.SETUP_ID].ToString(), downtimeRow[DownTime.SETUP_MODID].ToString());
          if (rowOrder != null)
          {
            this.lbl_SetupCode.Text = rowOrder[PastOrder.POL_SETUP_CODE].ToString();
            this.lbl_FluteQuality.Text = rowOrder[PastOrder.POL_FLUTE].ToString() + " - " + rowOrder[PastOrder.POL_QUALITY_ID].ToString();
            string level1 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + rowOrder[PastOrder.POL_LEVEL_NAME_0].ToString());
            string level2 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + rowOrder[PastOrder.POL_LEVEL_NAME_1].ToString());
            string level3 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + rowOrder[PastOrder.POL_LEVEL_NAME_2].ToString());
            this.lbl_OrderNumber1.Text = level1 + " - " + rowOrder[PastOrder.POL_ORDER_NUMBER_0].ToString();
            this.lbl_OrderNumber2.Text = level2 + " - " + rowOrder[PastOrder.POL_ORDER_NUMBER_1].ToString();
            this.lbl_OrderNumber3.Text = level3 + " - " + rowOrder[PastOrder.POL_ORDER_NUMBER_2].ToString();

            if (rowOrder[PastOrder.POL_LEVEL_NAME_0].ToString() == "Nolevel")
              this.lbl_OrderNumber1.Text = "";
            if (rowOrder[PastOrder.POL_LEVEL_NAME_1].ToString() == "Nolevel")
              this.lbl_OrderNumber2.Text = "";
            if (rowOrder[PastOrder.POL_LEVEL_NAME_2].ToString() == "Nolevel")
              this.lbl_OrderNumber3.Text = "";
          }
          else
          {
            rowOrder = DBManager.getDBManager().order.getRow(downtimeRow[DownTime.SETUP_ID].ToString(), downtimeRow[DownTime.SETUP_MODID].ToString());
            if (rowOrder != null)
            {
              DataRow rowQuality = DBManager.getDBManager().quality.getRow(rowOrder[Order.DEL_QUALITY_ID].ToString());
              string flute = "";
              if (rowQuality != null)
                flute = rowQuality[Quality.FLUTE_ID].ToString();
              this.lbl_SetupCode.Text = rowOrder[Order.DEL_SETUP_CODE].ToString();
              this.lbl_FluteQuality.Text = flute + " - " + rowOrder[Order.DEL_QUALITY_ID].ToString();
              string level1 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + rowOrder[Order.DEL_LEVEL_NAME_0].ToString());
              string level2 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + rowOrder[Order.DEL_LEVEL_NAME_1].ToString());
              string level3 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + rowOrder[Order.DEL_LEVEL_NAME_2].ToString());
              this.lbl_OrderNumber1.Text = level1 + " - " + rowOrder[Order.DEL_ORDER_NUMBER_0].ToString();
              this.lbl_OrderNumber2.Text = level2 + " - " + rowOrder[Order.DEL_ORDER_NUMBER_1].ToString();
              this.lbl_OrderNumber3.Text = level3 + " - " + rowOrder[Order.DEL_ORDER_NUMBER_2].ToString();

              if (rowOrder[Order.DEL_LEVEL_NAME_0].ToString() == "Nolevel")
                this.lbl_OrderNumber1.Text = "";
              if (rowOrder[Order.DEL_LEVEL_NAME_1].ToString() == "Nolevel")
                this.lbl_OrderNumber2.Text = "";
              if (rowOrder[Order.DEL_LEVEL_NAME_2].ToString() == "Nolevel")
                this.lbl_OrderNumber3.Text = "";
            }
            else
            {
              this.lbl_SetupCode.Text = "";
              this.lbl_FluteQuality.Text = "";
              this.lbl_OrderNumber1.Text = "";
              this.lbl_OrderNumber2.Text = "";
              this.lbl_OrderNumber3.Text = "";
            }
          }

          string dateTime = SyncroC.Utility.SyncroConverter.ConvertDateTimeToStrForDataGridView(((DateTime)downtimeRow[DownTime.SHIFT_START_TIME]).ToLocalTime());
          this.ttl_DowntimeStats.Text = downtimeRow[DownTime.SHIFT_ID].ToString() + " - " + dateTime;
          mkt_StartTime.ValueAsUNIVERSALDateTime = ((System.DateTime)downtimeRow[DownTime.START_TIME]);
          if (((System.DateTime)downtimeRow[DownTime.FINISH_TIME]).Year == 2001)
            mkt_EndTime.Value = "";
          else
            mkt_EndTime.ValueAsUNIVERSALDateTime = ((System.DateTime)downtimeRow[DownTime.FINISH_TIME]);
          //AGrassi_FI04/07/2007_1
          if ((mkt_EndTime.Value.Trim() != "") && (mkt_EndTime.ValueAsUNIVERSALDateTime.Year!=2001))
          {
            TimeSpan t = ((System.DateTime)downtimeRow[DownTime.FINISH_TIME]) - ((System.DateTime)downtimeRow[DownTime.START_TIME]);
            this.ttb_Duration.Value = t.Hours.ToString("D2") + ":" + t.Minutes.ToString("D2") + ":" + t.Seconds.ToString("D2"); ;
          }
          else
            this.ttb_Duration.Value = "";
          //zone grid selection
          string zoneKey = downtimeRow[DownTime.ZONE_KEY].ToString();
          //agrassi_FI13-03-08_1
          //if (zoneKey != null && !zoneKey.Trim().Equals(string.Empty))
          if (zoneKey != null && !zoneKey.Trim().Equals(string.Empty) && (!zoneKey.Equals("00")))
          {
            string filter = string.Empty;
            if (state.Equals(State.Read))
              filter = "Key = '" + zoneKey + "'";
            setDataViewZonesFilter(filter);
            //agrassi_FI07-07-08
            //foreach (DataGridViewRow row in gdv_Zones.Rows)
            for (int i=0; i<gdv_Zones.Rows.Count; i++)
            {
              //agrassi_FI07-07-08
              if (gdv_Zones.Rows[i].Cells.Count > 1)
              {
                if (gdv_Zones.Rows[i].Cells[1].Value.ToString().Equals(zoneKey))
                {
                  gdv_Zones.Rows[i].Selected = true;
                  gdv_Zones.FirstDisplayedScrollingRowIndex = gdv_Zones.Rows[i].Index;
                  //agrassi_FI24-07-08
                  DataRow r = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_ZONE].Rows.Find(zoneKey);
                  if (r != null)
                  {
                    bool flagFosber = (bool)r[DownTimeReason.INDEX_ZONE_FOSBER];
                    gdv_Zones.Rows[i].DefaultCellStyle.SelectionForeColor = (flagFosber ? Color.White : Color.DarkBlue);
                  }
                  break;
                }
              }
            }
          }
          else
          {
            setDataViewZonesFilter(string.Empty);
            for (int k = gdv_Zones.Rows.Count - 1; k >= 0; k--)
            {
              //agrassi_FI24-07-08
              string zKey = gdv_Zones.Rows[k].Cells[1].Value.ToString();
              DataRow r = DBManager.getDBManager().downTimeReason.downTimeReason.Tables[DownTimeReason.INDEX_TABLE_ZONE].Rows.Find(zKey);
              if (r != null)
              {
                bool flagFosber = (bool)r[DownTimeReason.INDEX_ZONE_FOSBER];
                gdv_Zones.Rows[k].DefaultCellStyle.SelectionForeColor = (flagFosber ? Color.White : Color.DarkBlue);
                gdv_Zones.Rows[k].DefaultCellStyle.ForeColor = (flagFosber ? Color.Black : Color.DarkBlue);
              }
              gdv_Zones.Rows[k].Selected = (k == 0 && state.Equals(State.Modify));
            }
          }
          //reason grid selection
          string reasonKey = downtimeRow[DownTime.REASON_KEY].ToString();
          //if (reasonKey != null && !reasonKey.Trim().Equals(string.Empty))
          if (reasonKey != null && !reasonKey.Trim().Equals(string.Empty) && (!reasonKey.Equals("0000")))
          {
            string filter = string.Empty;
            if (state.Equals(State.Read))
              filter = "Key = '" + reasonKey + "'";
            else
              filter = "ZoneKey = '" + gdv_Zones.SelectedRows[0].Cells[1].Value.ToString() + "'";
            setDataViewReasonsFilter(filter);
            //agrassi_FI24-07-08
            //foreach (DataGridViewRow row in gdv_Reasons.Rows)
            for (int i = 0; i < gdv_Reasons.Rows.Count; i++)
            {
              if (gdv_Reasons.Rows[i].Cells[1].Value.ToString().Equals(reasonKey))
              {
                gdv_Reasons.Rows[i].Selected = true;
                gdv_Reasons.FirstDisplayedScrollingRowIndex = gdv_Reasons.Rows[i].Index;
                break;
              }
            }
          }
          else
          {
            if (gdv_Zones.SelectedRows.Count != 0)
            {
              setDataViewReasonsFilter("ZoneKey = '" + gdv_Zones.SelectedRows[0].Cells[1].Value.ToString() + "'");
            }
            else
            {
              //a query with empty result will be generated
              setDataViewReasonsFilter(tableReasons.Columns[DownTimeReason.INDEX_REASON_AVAILABLE].ColumnName + " = false");
              foreach (DataGridViewRow row in gdv_Reasons.Rows)
                row.Selected = false;
            }
          }
          //AGrassi_FI04/07/2007_1
          this.ttb_Description.Value = downtimeRow[DownTime.TEXT].ToString();
          lbl_SetupID.Text = downtimeRow[DownTime.SETUP_ID].ToString();
          lbl_ModID.Text = downtimeRow[DownTime.SETUP_MODID].ToString();
        }
      }
		}

		private void gdv_Zones_SelectionChanged(object sender, EventArgs e)
		{
			if (gdv_Zones.SelectedRows.Count == 1)
        //agrassi_FI07-07-08
        if (gdv_Zones.SelectedRows[0].Cells.Count>1)
				  setDataViewReasonsFilter("ZoneKey = '" + gdv_Zones.SelectedRows[0].Cells[1].Value.ToString() + "'");
		}

		private void readOnly(bool readOnly)
		{	//readonly
			ttb_Description.ReadOnly = readOnly;
			gdv_Zones.Enabled = !readOnly;
			gdv_Reasons.Enabled = !readOnly;
			//tabstop
			ttb_Description.TabStop = ! readOnly;
			gdv_Zones.TabStop = !readOnly;
			gdv_Reasons.TabStop = !readOnly;
		}

		private void setState(State newState)
		{
			if (this.state != newState)
			{
				switch (newState)
				{
					case State.Modify:
            if (DBManager.getDBManager().downTime.getCount()!= 0)
						{
							readOnly(false);
							gdv_Downtimes.Enabled = false;
							gdv_Downtimes.TabStop = false;
							this.state = newState;
							showSelectedDowntime();
							gdv_Zones.Focus();
							lbl_CtrlF2.BackColor = lbl_CtrlF2_YY.BackColor = lbl_Status.BackColor;
						}
						break;
					case State.Read:
						readOnly(true);
						gdv_Downtimes.Enabled = true;
						gdv_Downtimes.TabStop = true;
						this.state = newState;
						showSelectedDowntime();
						gdv_Downtimes.Focus();
						lbl_CtrlF2.BackColor = lbl_CtrlF2_YY.BackColor = lbl_Title_YN.BackColor;
						break;
				}
				lbl_pageStatus.Text = this.state.ToString();
			}
		}

		private void Frm_DowntimeReport_Load(object sender, EventArgs e)
		{
			if (gdv_Downtimes.Rows.Count > 0)
				gdv_Downtimes.Rows[0].Selected = true;
			setState(State.Read);
		}

		private void gdv_Downtimes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
      DataRow dataRow = ((DataRowView)gdv_Downtimes.Rows[e.RowIndex].DataBoundItem).Row;
      //agrassi_FI07-07-08
      if (gdv_Downtimes.Rows[e.RowIndex].Cells.Count <= 1)
        return;
      if (this.gdv_Downtimes.Columns[e.ColumnIndex].Name == DownTime.SHIFT_ID
						&& e.Value != null)
			{
				try
				{
          //DataRow dt = tableDownTime.Rows.Find(new object[]{
          //  (string)gdv_Downtimes.Rows[e.RowIndex].Cells[0].Value,
          //  (DateTime)gdv_Downtimes.Rows[e.RowIndex].Cells[1].Value});

          DataRow dt = DBManager.getDBManager().downTime.getRow((string)gdv_Downtimes.Rows[e.RowIndex].Cells[0].Value, (DateTime)gdv_Downtimes.Rows[e.RowIndex].Cells[1].Value);

					if (dt != null && (((DateTime)dt[DownTime.FINISH_TIME]).Equals(DateTime.MinValue)))
					{ //active downtime, assigned or unassignes
						gdv_Downtimes.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Orange;
						gdv_Downtimes.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Orange;
					}
					else
					{	//finished downtime
            //agrassi_FI13-03-08_1
            //if (dt[DownTime.REASON_KEY].ToString().Trim().Equals(string.Empty) ||
            //    dt[DownTime.ZONE_KEY].ToString().Trim().Equals(string.Empty))
            if (dt[DownTime.REASON_KEY].ToString().Trim().Equals(string.Empty) ||
                dt[DownTime.ZONE_KEY].ToString().Trim().Equals(string.Empty) ||
                dt[DownTime.ZONE_KEY].ToString().Trim().Equals("00"))
            {//finished downtime, unassigned
						gdv_Downtimes.Rows[e.RowIndex].DefaultCellStyle.ForeColor = ColConv.Convert(SyncroColor.AlarmRed);
						gdv_Downtimes.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = ColConv.Convert(SyncroColor.AlarmRed);
						}
						else
						{//finished downtime, assigned
							gdv_Downtimes.Rows[e.RowIndex].DefaultCellStyle.ForeColor = ColConv.Convert(SyncroColor.Black);
							gdv_Downtimes.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = ColConv.Convert(SyncroColor.White);
						}
					}
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_Downtimes_CellFormatting - " + DownTime.SHIFT_ID, ex);
				}
			}
			if (this.gdv_Downtimes.Columns[e.ColumnIndex].Name == DownTime.START_TIME
						&& e.Value != null)
			{
				try
				{
					//FOS2 ABello
					//string formattedTime = ((DateTime)e.Value).ToLocalTime().ToString();
					//if (formattedTime != null && formattedTime.Length >= 16)
					//{
					//  formattedTime = formattedTime.Substring(0, formattedTime.Length - 3).Replace('.', ':'); //remove seconds
					//}
					//e.Value = formattedTime;
					e.Value = SyncroC.Utility.SyncroConverter.ConvertDateTimeToStrForDataGridView(((DateTime)e.Value).ToLocalTime());
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_Downtimes_CellFormatting - " + DownTime.START_TIME, ex);
				}
			}
		}

		private void Frm_DowntimeReport_Activated(object sender, EventArgs e)
		{
			if (state.Equals(State.Read))
			{
				gdv_Downtimes.Focus();
			}
		}

		private void gdv_Downtimes_Leave(object sender, EventArgs e)
		{
			if (state.Equals(State.Read))
			{
				gdv_Downtimes.Focus();
			}
		}
	}
}

