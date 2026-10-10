using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Text;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase;
using SyncroC.Forms.PopUp;
using SyncroC.Forms.Pages.Production.OrdersIns;

namespace SyncroC.Forms.Pages.Production.OrdersList
{
  public class Frm_DryPastOrdersList : SyncroC.Forms.Pages.Production.OrdersList.Frm_OrdersListSubGroup
  {
    private Label lbl_ModID;
    private Label lbl_SetupID;
    private System.ComponentModel.IContainer components = null;

    //colonne che avrebbe dovuto fare il designer.
    private DataGridViewColumn clm_Order_SetupCode;
    private DataGridViewColumn clm_Order_Quality;
    private DataGridViewColumn clm_Order_WebWidth;
    private DataGridViewColumn clm_Order_Trim;
    private DataGridViewColumn clm_Order_Length;
    private DataGridViewColumn clm_Order_ScoreGap;

    private SyncroC.Controls.GridView gdv_Orders;
    private SyncroC.Controls.Title lbl_DryEndTitle;
    private SyncroC.Controls.Bracket brk_Details;
    private SyncroC.Controls.Title ttl_Order0;
    private Label lbl_Sheets0;
    private Label lbl_Misc0;
    private SyncroC.Controls.Title ttl_Order1;
    private Label lbl_Misc1;
    private Label lbl_Sheets1;
    private SyncroC.Controls.Title ttl_Order2;
    private Label lbl_Sheets2;
    private Label lbl_Misc2;
    private DataGridViewColumn clm_Order_Time;

    private bool debugInfo = false;
    private string LENGTH_COL_NAME = "Length";
    private Label lbl_Customer0;
    private Label lbl_Customer1;
    private Label lbl_Customer2;
    private Label lbl_DataToWiew0_3;
    private Label lbl_DataToWiew0_2;
    private Label lbl_DataToWiew0_1;
    private Label lbl_DataToWiew0_0;
    private Label lbl_DataToWiew1_3;
    private Label lbl_DataToWiew1_2;
    private Label lbl_DataToWiew1_1;
    private Label lbl_DataToWiew1_0;
    private Label lbl_DataToWiew2_3;
    private Label lbl_DataToWiew2_2;
    private Label lbl_DataToWiew2_1;
    private Label lbl_DataToWiew2_0;
    private PictureBox pct_OrderPreprint1;
    private PictureBox pct_OrderPreprint2;
    private PictureBox pct_OrderPreprint0;
    private SyncroC.Controls.myLabel lbl_filter;
    private Label lbl_OfficeID;

    private string filter = "";

    public Frm_DryPastOrdersList()
    {
      InitializeComponent();

      //ABELLO_FI25/08/2010_1 se uso Postgres il doppio click sulla lista degli eseguiti andava a casaccio
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        this.gdv_Orders.DataSource = DBManager.getDBManager().pastOrder.getTable();

      //gdv_Orders
      clm_Order_Time = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_Time.Name = PastOrder.POL_BEGIN_TIME;
      clm_Order_Time.HeaderText = "Time";
      clm_Order_Time.DataPropertyName = PastOrder.POL_BEGIN_TIME;
      clm_Order_Time.Width = 200;

      clm_Order_SetupCode = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_SetupCode.Name = PastOrder.POL_SETUP_CODE;
      clm_Order_SetupCode.HeaderText = "Code";
      clm_Order_SetupCode.DataPropertyName = PastOrder.POL_SETUP_CODE;
      clm_Order_SetupCode.Width = 200;

      clm_Order_Quality = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_Quality.Name = PastOrder.POL_QUALITY_ID;
      clm_Order_Quality.HeaderText = "Quality";
      clm_Order_Quality.DataPropertyName = PastOrder.POL_QUALITY_ID;
      clm_Order_Quality.Width = 354;

      clm_Order_WebWidth = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_WebWidth.Name = PastOrder.POL_WEB_WIDTH;
      clm_Order_WebWidth.HeaderText = "Width";
      clm_Order_WebWidth.DataPropertyName = PastOrder.POL_WEB_WIDTH;
      clm_Order_WebWidth.Width = 105;

      clm_Order_Trim = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_Trim.Name = PastOrder.POL_TRIM;
      clm_Order_Trim.HeaderText = "Trim";
      clm_Order_Trim.DataPropertyName = PastOrder.POL_TRIM;
      clm_Order_Trim.Width = 90;

      clm_Order_Length = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_Length.Name = LENGTH_COL_NAME;
      clm_Order_Length.HeaderText = "Length";
      clm_Order_Length.DataPropertyName = PastOrder.POL_SHEET_LENGTH_0;
      clm_Order_Length.Width = 105;

      clm_Order_ScoreGap = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_ScoreGap.Name = PastOrder.POL_SCORE_GAP;
      clm_Order_ScoreGap.HeaderText = "Score Gap";
      clm_Order_ScoreGap.DataPropertyName = PastOrder.POL_SCORE_GAP;
      clm_Order_ScoreGap.Width = 90;

      gdv_Orders.Columns.Add(clm_Order_Time);
      gdv_Orders.Columns.Add(clm_Order_SetupCode);
      gdv_Orders.Columns.Add(clm_Order_Quality);
      gdv_Orders.Columns.Add(clm_Order_WebWidth);
      gdv_Orders.Columns.Add(clm_Order_Trim);
      gdv_Orders.Columns.Add(clm_Order_Length);
      gdv_Orders.Columns.Add(clm_Order_ScoreGap);

      gdv_Orders.Sort(clm_Order_Time, ListSortDirection.Descending);

      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
      {
        this.lbl_CtrlF2_YY.Text = "";
      }

      this.lbl_filter.Testo = (this.filter == "") ? "" : "!";

      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        this.lbl_filter.Visible = true;
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
      this.gdv_Orders.DataSource = null;
      base.Dispose(disposing);
    }

    public override void refreshMySQLData()
    {
      try
      {
        this.lbl_filter.Testo = (this.filter == "") ? "" : "!";

        DataRow row = null;
        int FirstDisplayedScrollingRowIndex = this.gdv_Orders.FirstDisplayedScrollingRowIndex;

        if (this.gdv_Orders != null)
          if (this.gdv_Orders.SelectedRows != null)
            if (this.gdv_Orders.SelectedRows.Count != 0)
              row = ((DataRowView)this.gdv_Orders.SelectedRows[0].DataBoundItem).Row;

        this.gdv_Orders.SuspendLayout();

        //ABELLO_FI25/08/2010_1 se uso Postgres il doppio click sulla lista degli eseguiti andava a casaccio
        this.gdv_Orders.DataSource = DBManager.getDBManager().pastOrder.getTable(this.filter);

        for (int i = 0; i < this.gdv_Orders.Rows.Count; i++)
        {
          if (row != null)
          {
            string a = ((DataRowView)this.gdv_Orders.Rows[i].DataBoundItem).Row[PastOrder.POL_SETUP_ID].ToString().Trim();
            string b = row[PastOrder.POL_SETUP_ID].ToString().Trim();
            if (a == b)
            {
              this.gdv_Orders.Rows[i].Selected = true;
            }
          }
        }

        if ((row == null) && (this.gdv_Orders.Rows.Count > 0))
          this.gdv_Orders.Rows[0].Selected = true;

        if ((FirstDisplayedScrollingRowIndex >= 0) && (FirstDisplayedScrollingRowIndex <= DBManager.getDBManager().pastOrder.getCount(this.filter)))
          this.gdv_Orders.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex;

        this.gdv_Orders.ResumeLayout();
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    private string setupCodeFilter = "";
    private string customerNameFilter = "";
    private string orderNumberFilter = "";
    private void lbl_filter_Click(object sender, EventArgs e)
    {
      try
      {
        Frm_FilterPastOrder askFilterPastOrder = new Frm_FilterPastOrder();
        FosberDialogResult ret = askFilterPastOrder.ShowDialog(this, this.setupCodeFilter, this.orderNumberFilter, this.customerNameFilter);
        if (ret.Dialog == DialogResult.OK)
        {
          this.filter = "";

          this.setupCodeFilter = ((string)ret.Data[0]).Trim();
          this.orderNumberFilter = ((string)ret.Data[1]).Trim();
          this.customerNameFilter = ((string)ret.Data[2]).Trim();

          if (this.setupCodeFilter != "")
            this.filter = String.Format("(setupcode LIKE '{0}%')", this.setupCodeFilter);

          if (this.orderNumberFilter != "")
          {
            if (this.filter != "")
              this.filter += String.Format("AND (ordernumber0 LIKE '{0}%' OR ordernumber1 LIKE '{0}%' OR ordernumber2 LIKE '{0}%')", this.orderNumberFilter);
            else
              this.filter = String.Format("(ordernumber0 LIKE '{0}%' OR ordernumber1 LIKE '{0}%' OR ordernumber2 LIKE '{0}%')", this.orderNumberFilter);
          }

          if (this.customerNameFilter != "")
          {
            if (this.filter != "")
              this.filter += String.Format("AND (customername0 LIKE '{0}%' OR customername1 LIKE '{0}%' OR customername2 LIKE '{0}%')", this.customerNameFilter);
            else
              this.filter = String.Format("(customername0 LIKE '{0}%' OR customername1 LIKE '{0}%' OR customername2 LIKE '{0}%')", this.customerNameFilter);
          }
        }

        this.gdv_Orders.SuspendLayout();
        this.gdv_Orders.DataSource = DBManager.getDBManager().pastOrder.getTable(this.filter);
        if (this.gdv_Orders.Rows.Count > 0)
          this.gdv_Orders.Rows[0].Selected = true;
        this.gdv_Orders.ResumeLayout();

        this.lbl_filter.Testo = (this.filter == "") ? "" : "!";
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    protected override void F13()
    {
    }

    /// <summary>
    /// Ctrl + F2 key behaviour
    /// </summary>
    protected override void CtrlF2()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      if (gdv_Orders.SelectedRows.Count == 1)
      {
        Frm_Confirm popUp = new Frm_Confirm();
        string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_22");
        if (popUp.ShowDialog(this, text) == DialogResult.OK)
        {
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          DataRow dataRow = ((DataRowView)gdv_Orders.SelectedRows[0].DataBoundItem).Row;
          cm.SendTcp(cm.SYNCROS_DB_MANAGER,
            Command.DB_PAST_ORDER + "." + Command.DB_CMD_RESTORE + "(" +
            dataRow[PastOrder.POL_SETUP_ID] + Command.PARAMETER_SEPARATOR +
            SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeString((DateTime)dataRow[PastOrder.POL_BEGIN_TIME]) + ")");
        }
      }
    }

    #region Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DryPastOrdersList));
      this.lbl_ModID = new System.Windows.Forms.Label();
      this.lbl_SetupID = new System.Windows.Forms.Label();
      this.gdv_Orders = new SyncroC.Controls.GridView();
      this.lbl_DryEndTitle = new SyncroC.Controls.Title();
      this.brk_Details = new SyncroC.Controls.Bracket();
      this.ttl_Order0 = new SyncroC.Controls.Title();
      this.lbl_Sheets0 = new System.Windows.Forms.Label();
      this.lbl_Misc0 = new System.Windows.Forms.Label();
      this.ttl_Order1 = new SyncroC.Controls.Title();
      this.lbl_Misc1 = new System.Windows.Forms.Label();
      this.lbl_Sheets1 = new System.Windows.Forms.Label();
      this.ttl_Order2 = new SyncroC.Controls.Title();
      this.lbl_Sheets2 = new System.Windows.Forms.Label();
      this.lbl_Misc2 = new System.Windows.Forms.Label();
      this.lbl_Customer0 = new System.Windows.Forms.Label();
      this.lbl_Customer1 = new System.Windows.Forms.Label();
      this.lbl_Customer2 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew0_3 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew0_2 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew0_1 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew0_0 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew1_3 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew1_2 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew1_1 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew1_0 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew2_3 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew2_2 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew2_1 = new System.Windows.Forms.Label();
      this.lbl_DataToWiew2_0 = new System.Windows.Forms.Label();
      this.pct_OrderPreprint1 = new System.Windows.Forms.PictureBox();
      this.pct_OrderPreprint2 = new System.Windows.Forms.PictureBox();
      this.pct_OrderPreprint0 = new System.Windows.Forms.PictureBox();
      this.lbl_OfficeID = new System.Windows.Forms.Label();
      this.lbl_filter = new SyncroC.Controls.myLabel();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint0)).BeginInit();
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
      // tab_TabD
      // 
      this.tab_TabD.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      this.lbl_CtrlF4_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 2);
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF2_YY.Text = "Restore";
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
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // lbl_ModID
      // 
      this.lbl_ModID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_ModID.CausesValidation = false;
      this.lbl_ModID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ModID.Location = new System.Drawing.Point(1078, 171);
      this.lbl_ModID.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
      this.lbl_ModID.Name = "lbl_ModID";
      this.lbl_ModID.Size = new System.Drawing.Size(124, 18);
      this.lbl_ModID.TabIndex = 152;
      this.lbl_ModID.Text = "ModID";
      this.lbl_ModID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_ModID.Visible = false;
      // 
      // lbl_SetupID
      // 
      this.lbl_SetupID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_SetupID.CausesValidation = false;
      this.lbl_SetupID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SetupID.Location = new System.Drawing.Point(937, 171);
      this.lbl_SetupID.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
      this.lbl_SetupID.Name = "lbl_SetupID";
      this.lbl_SetupID.Size = new System.Drawing.Size(124, 18);
      this.lbl_SetupID.TabIndex = 151;
      this.lbl_SetupID.Text = "SetUpID";
      this.lbl_SetupID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_SetupID.Visible = false;
      // 
      // gdv_Orders
      // 
      this.gdv_Orders.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Orders.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
      this.gdv_Orders.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.AutoGenerateColumns = false;
      this.gdv_Orders.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Orders.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.gdv_Orders.DataMember = "";
      this.gdv_Orders.DataSource = null;
      this.gdv_Orders.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_Orders.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.ImagePassword = null;
      this.gdv_Orders.Location = new System.Drawing.Point(42, 225);
      this.gdv_Orders.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.gdv_Orders.MultiSelect = false;
      this.gdv_Orders.Name = "gdv_Orders";
      this.gdv_Orders.RowsHeight = 32;
      this.gdv_Orders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Orders.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
      this.gdv_Orders.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.Size = new System.Drawing.Size(1159, 317);
      this.gdv_Orders.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Orders.TabIndex = 169;
      this.gdv_Orders.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Orders.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.TranslateText = true;
      this.gdv_Orders.DoubleClick += new System.EventHandler(this.gdv_Orders_DoubleClick);
      this.gdv_Orders.SelectionChanged += new System.EventHandler(this.gdv_Orders_SelectionChanged);
      this.gdv_Orders.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Orders_CellFormatting);
      // 
      // lbl_DryEndTitle
      // 
      this.lbl_DryEndTitle.BackColor = System.Drawing.Color.Black;
      this.lbl_DryEndTitle.CausesValidation = false;
      this.lbl_DryEndTitle.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.lbl_DryEndTitle.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DryEndTitle.ForeColor = System.Drawing.Color.White;
      this.lbl_DryEndTitle.Location = new System.Drawing.Point(30, 193);
      this.lbl_DryEndTitle.Name = "lbl_DryEndTitle";
      this.lbl_DryEndTitle.Size = new System.Drawing.Size(1171, 348);
      this.lbl_DryEndTitle.Style = SyncroC.Controls.TitleStyles.Big;
      this.lbl_DryEndTitle.TabIndex = 168;
      this.lbl_DryEndTitle.Text = "Dry End Orders List";
      this.lbl_DryEndTitle.TranslateText = true;
      // 
      // brk_Details
      // 
      this.brk_Details.BackColor = System.Drawing.Color.LightBlue;
      this.brk_Details.color = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
      this.brk_Details.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_Details.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_Details.Location = new System.Drawing.Point(30, 547);
      this.brk_Details.Name = "brk_Details";
      this.brk_Details.Position = SyncroC.Controls.BracketPosition.Bottom;
      this.brk_Details.ShowArrow = false;
      this.brk_Details.Size = new System.Drawing.Size(1171, 182);
      this.brk_Details.TabIndex = 177;
      this.brk_Details.Text = "Details";
      this.brk_Details.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_Details.TranslateText = true;
      // 
      // ttl_Order0
      // 
      this.ttl_Order0.BackColor = System.Drawing.Color.Black;
      this.ttl_Order0.CausesValidation = false;
      this.ttl_Order0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order0.ForeColor = System.Drawing.Color.White;
      this.ttl_Order0.Location = new System.Drawing.Point(42, 575);
      this.ttl_Order0.Name = "ttl_Order0";
      this.ttl_Order0.Size = new System.Drawing.Size(374, 154);
      this.ttl_Order0.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Order0.TabIndex = 170;
      this.ttl_Order0.TranslateText = false;
      // 
      // lbl_Sheets0
      // 
      this.lbl_Sheets0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Sheets0.CausesValidation = false;
      this.lbl_Sheets0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Sheets0.Location = new System.Drawing.Point(317, 635);
      this.lbl_Sheets0.Name = "lbl_Sheets0";
      this.lbl_Sheets0.Size = new System.Drawing.Size(99, 32);
      this.lbl_Sheets0.TabIndex = 172;
      this.lbl_Sheets0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_Misc0
      // 
      this.lbl_Misc0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Misc0.CausesValidation = false;
      this.lbl_Misc0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Misc0.Location = new System.Drawing.Point(50, 635);
      this.lbl_Misc0.Name = "lbl_Misc0";
      this.lbl_Misc0.Size = new System.Drawing.Size(234, 32);
      this.lbl_Misc0.TabIndex = 171;
      this.lbl_Misc0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ttl_Order1
      // 
      this.ttl_Order1.BackColor = System.Drawing.Color.Black;
      this.ttl_Order1.CausesValidation = false;
      this.ttl_Order1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order1.ForeColor = System.Drawing.Color.White;
      this.ttl_Order1.Location = new System.Drawing.Point(429, 575);
      this.ttl_Order1.Name = "ttl_Order1";
      this.ttl_Order1.Size = new System.Drawing.Size(374, 154);
      this.ttl_Order1.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Order1.TabIndex = 165;
      this.ttl_Order1.TranslateText = false;
      // 
      // lbl_Misc1
      // 
      this.lbl_Misc1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Misc1.CausesValidation = false;
      this.lbl_Misc1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Misc1.Location = new System.Drawing.Point(437, 635);
      this.lbl_Misc1.Name = "lbl_Misc1";
      this.lbl_Misc1.Size = new System.Drawing.Size(234, 32);
      this.lbl_Misc1.TabIndex = 173;
      this.lbl_Misc1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_Sheets1
      // 
      this.lbl_Sheets1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Sheets1.CausesValidation = false;
      this.lbl_Sheets1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Sheets1.Location = new System.Drawing.Point(704, 635);
      this.lbl_Sheets1.Name = "lbl_Sheets1";
      this.lbl_Sheets1.Size = new System.Drawing.Size(99, 32);
      this.lbl_Sheets1.TabIndex = 174;
      this.lbl_Sheets1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // ttl_Order2
      // 
      this.ttl_Order2.BackColor = System.Drawing.Color.Black;
      this.ttl_Order2.CausesValidation = false;
      this.ttl_Order2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order2.ForeColor = System.Drawing.Color.White;
      this.ttl_Order2.Location = new System.Drawing.Point(816, 575);
      this.ttl_Order2.Name = "ttl_Order2";
      this.ttl_Order2.Size = new System.Drawing.Size(374, 154);
      this.ttl_Order2.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_Order2.TabIndex = 166;
      this.ttl_Order2.TranslateText = false;
      // 
      // lbl_Sheets2
      // 
      this.lbl_Sheets2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Sheets2.CausesValidation = false;
      this.lbl_Sheets2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Sheets2.Location = new System.Drawing.Point(1091, 635);
      this.lbl_Sheets2.Name = "lbl_Sheets2";
      this.lbl_Sheets2.Size = new System.Drawing.Size(99, 32);
      this.lbl_Sheets2.TabIndex = 176;
      this.lbl_Sheets2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_Misc2
      // 
      this.lbl_Misc2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Misc2.CausesValidation = false;
      this.lbl_Misc2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Misc2.Location = new System.Drawing.Point(824, 635);
      this.lbl_Misc2.Name = "lbl_Misc2";
      this.lbl_Misc2.Size = new System.Drawing.Size(234, 32);
      this.lbl_Misc2.TabIndex = 175;
      this.lbl_Misc2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_Customer0
      // 
      this.lbl_Customer0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Customer0.CausesValidation = false;
      this.lbl_Customer0.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Customer0.Location = new System.Drawing.Point(50, 602);
      this.lbl_Customer0.Name = "lbl_Customer0";
      this.lbl_Customer0.Size = new System.Drawing.Size(365, 30);
      this.lbl_Customer0.TabIndex = 178;
      this.lbl_Customer0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_Customer1
      // 
      this.lbl_Customer1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Customer1.CausesValidation = false;
      this.lbl_Customer1.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Customer1.Location = new System.Drawing.Point(437, 602);
      this.lbl_Customer1.Name = "lbl_Customer1";
      this.lbl_Customer1.Size = new System.Drawing.Size(365, 30);
      this.lbl_Customer1.TabIndex = 179;
      this.lbl_Customer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_Customer2
      // 
      this.lbl_Customer2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Customer2.CausesValidation = false;
      this.lbl_Customer2.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Customer2.Location = new System.Drawing.Point(824, 602);
      this.lbl_Customer2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.lbl_Customer2.Name = "lbl_Customer2";
      this.lbl_Customer2.Size = new System.Drawing.Size(365, 30);
      this.lbl_Customer2.TabIndex = 180;
      this.lbl_Customer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew0_3
      // 
      this.lbl_DataToWiew0_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew0_3.CausesValidation = false;
      this.lbl_DataToWiew0_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew0_3.Location = new System.Drawing.Point(257, 702);
      this.lbl_DataToWiew0_3.Name = "lbl_DataToWiew0_3";
      this.lbl_DataToWiew0_3.Size = new System.Drawing.Size(159, 27);
      this.lbl_DataToWiew0_3.TabIndex = 184;
      this.lbl_DataToWiew0_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew0_2
      // 
      this.lbl_DataToWiew0_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew0_2.CausesValidation = false;
      this.lbl_DataToWiew0_2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew0_2.Location = new System.Drawing.Point(50, 702);
      this.lbl_DataToWiew0_2.Name = "lbl_DataToWiew0_2";
      this.lbl_DataToWiew0_2.Size = new System.Drawing.Size(200, 27);
      this.lbl_DataToWiew0_2.TabIndex = 183;
      this.lbl_DataToWiew0_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew0_1
      // 
      this.lbl_DataToWiew0_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew0_1.CausesValidation = false;
      this.lbl_DataToWiew0_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew0_1.Location = new System.Drawing.Point(257, 671);
      this.lbl_DataToWiew0_1.Name = "lbl_DataToWiew0_1";
      this.lbl_DataToWiew0_1.Size = new System.Drawing.Size(159, 27);
      this.lbl_DataToWiew0_1.TabIndex = 182;
      this.lbl_DataToWiew0_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew0_0
      // 
      this.lbl_DataToWiew0_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew0_0.CausesValidation = false;
      this.lbl_DataToWiew0_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew0_0.Location = new System.Drawing.Point(50, 671);
      this.lbl_DataToWiew0_0.Name = "lbl_DataToWiew0_0";
      this.lbl_DataToWiew0_0.Size = new System.Drawing.Size(200, 27);
      this.lbl_DataToWiew0_0.TabIndex = 181;
      this.lbl_DataToWiew0_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew1_3
      // 
      this.lbl_DataToWiew1_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew1_3.CausesValidation = false;
      this.lbl_DataToWiew1_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew1_3.Location = new System.Drawing.Point(644, 702);
      this.lbl_DataToWiew1_3.Name = "lbl_DataToWiew1_3";
      this.lbl_DataToWiew1_3.Size = new System.Drawing.Size(159, 27);
      this.lbl_DataToWiew1_3.TabIndex = 188;
      this.lbl_DataToWiew1_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew1_2
      // 
      this.lbl_DataToWiew1_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew1_2.CausesValidation = false;
      this.lbl_DataToWiew1_2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew1_2.Location = new System.Drawing.Point(437, 702);
      this.lbl_DataToWiew1_2.Name = "lbl_DataToWiew1_2";
      this.lbl_DataToWiew1_2.Size = new System.Drawing.Size(200, 27);
      this.lbl_DataToWiew1_2.TabIndex = 187;
      this.lbl_DataToWiew1_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew1_1
      // 
      this.lbl_DataToWiew1_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew1_1.CausesValidation = false;
      this.lbl_DataToWiew1_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew1_1.Location = new System.Drawing.Point(644, 671);
      this.lbl_DataToWiew1_1.Name = "lbl_DataToWiew1_1";
      this.lbl_DataToWiew1_1.Size = new System.Drawing.Size(159, 27);
      this.lbl_DataToWiew1_1.TabIndex = 186;
      this.lbl_DataToWiew1_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew1_0
      // 
      this.lbl_DataToWiew1_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew1_0.CausesValidation = false;
      this.lbl_DataToWiew1_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew1_0.Location = new System.Drawing.Point(437, 671);
      this.lbl_DataToWiew1_0.Name = "lbl_DataToWiew1_0";
      this.lbl_DataToWiew1_0.Size = new System.Drawing.Size(200, 27);
      this.lbl_DataToWiew1_0.TabIndex = 185;
      this.lbl_DataToWiew1_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew2_3
      // 
      this.lbl_DataToWiew2_3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew2_3.CausesValidation = false;
      this.lbl_DataToWiew2_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew2_3.Location = new System.Drawing.Point(1031, 702);
      this.lbl_DataToWiew2_3.Name = "lbl_DataToWiew2_3";
      this.lbl_DataToWiew2_3.Size = new System.Drawing.Size(159, 27);
      this.lbl_DataToWiew2_3.TabIndex = 192;
      this.lbl_DataToWiew2_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew2_2
      // 
      this.lbl_DataToWiew2_2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew2_2.CausesValidation = false;
      this.lbl_DataToWiew2_2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew2_2.Location = new System.Drawing.Point(824, 702);
      this.lbl_DataToWiew2_2.Name = "lbl_DataToWiew2_2";
      this.lbl_DataToWiew2_2.Size = new System.Drawing.Size(200, 27);
      this.lbl_DataToWiew2_2.TabIndex = 191;
      this.lbl_DataToWiew2_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew2_1
      // 
      this.lbl_DataToWiew2_1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew2_1.CausesValidation = false;
      this.lbl_DataToWiew2_1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew2_1.Location = new System.Drawing.Point(1031, 671);
      this.lbl_DataToWiew2_1.Name = "lbl_DataToWiew2_1";
      this.lbl_DataToWiew2_1.Size = new System.Drawing.Size(159, 27);
      this.lbl_DataToWiew2_1.TabIndex = 190;
      this.lbl_DataToWiew2_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lbl_DataToWiew2_0
      // 
      this.lbl_DataToWiew2_0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DataToWiew2_0.CausesValidation = false;
      this.lbl_DataToWiew2_0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DataToWiew2_0.Location = new System.Drawing.Point(824, 671);
      this.lbl_DataToWiew2_0.Name = "lbl_DataToWiew2_0";
      this.lbl_DataToWiew2_0.Size = new System.Drawing.Size(200, 27);
      this.lbl_DataToWiew2_0.TabIndex = 189;
      this.lbl_DataToWiew2_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pct_OrderPreprint1
      // 
      this.pct_OrderPreprint1.Image = ((System.Drawing.Image)(resources.GetObject("pct_OrderPreprint1.Image")));
      this.pct_OrderPreprint1.Location = new System.Drawing.Point(769, 578);
      this.pct_OrderPreprint1.Name = "pct_OrderPreprint1";
      this.pct_OrderPreprint1.Size = new System.Drawing.Size(30, 18);
      this.pct_OrderPreprint1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_OrderPreprint1.TabIndex = 265;
      this.pct_OrderPreprint1.TabStop = false;
      this.pct_OrderPreprint1.Visible = false;
      // 
      // pct_OrderPreprint2
      // 
      this.pct_OrderPreprint2.Image = ((System.Drawing.Image)(resources.GetObject("pct_OrderPreprint2.Image")));
      this.pct_OrderPreprint2.Location = new System.Drawing.Point(1155, 578);
      this.pct_OrderPreprint2.Name = "pct_OrderPreprint2";
      this.pct_OrderPreprint2.Size = new System.Drawing.Size(30, 18);
      this.pct_OrderPreprint2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_OrderPreprint2.TabIndex = 264;
      this.pct_OrderPreprint2.TabStop = false;
      this.pct_OrderPreprint2.Visible = false;
      // 
      // pct_OrderPreprint0
      // 
      this.pct_OrderPreprint0.Image = ((System.Drawing.Image)(resources.GetObject("pct_OrderPreprint0.Image")));
      this.pct_OrderPreprint0.Location = new System.Drawing.Point(381, 578);
      this.pct_OrderPreprint0.Name = "pct_OrderPreprint0";
      this.pct_OrderPreprint0.Size = new System.Drawing.Size(30, 18);
      this.pct_OrderPreprint0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_OrderPreprint0.TabIndex = 263;
      this.pct_OrderPreprint0.TabStop = false;
      this.pct_OrderPreprint0.Visible = false;
      // 
      // lbl_OfficeID
      // 
      this.lbl_OfficeID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_OfficeID.CausesValidation = false;
      this.lbl_OfficeID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_OfficeID.Location = new System.Drawing.Point(795, 172);
      this.lbl_OfficeID.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
      this.lbl_OfficeID.Name = "lbl_OfficeID";
      this.lbl_OfficeID.Size = new System.Drawing.Size(124, 18);
      this.lbl_OfficeID.TabIndex = 266;
      this.lbl_OfficeID.Text = "OfficeID";
      this.lbl_OfficeID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_OfficeID.Visible = false;
      // 
      // lbl_filter
      // 
      this.lbl_filter.BackcolorTesto = System.Drawing.Color.Transparent;
      this.lbl_filter.BackgroundImage = global::SyncroC.Properties.Resources.filter;
      this.lbl_filter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.lbl_filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_filter.Colore1 = System.Drawing.Color.Red;
      this.lbl_filter.Colore2 = System.Drawing.Color.Red;
      this.lbl_filter.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_filter.Font = new System.Drawing.Font("Arial Unicode MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_filter.Location = new System.Drawing.Point(362, 171);
      this.lbl_filter.Name = "lbl_filter";
      this.lbl_filter.OutLineText = true;
      this.lbl_filter.OutLineTextcolore = System.Drawing.Color.Black;
      this.lbl_filter.Size = new System.Drawing.Size(75, 45);
      this.lbl_filter.TabIndex = 270;
      this.lbl_filter.Testo = "!";
      this.lbl_filter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_filter.TranslateText = false;
      this.lbl_filter.Visible = false;
      this.lbl_filter.Click += new System.EventHandler(this.lbl_filter_Click);
      // 
      // Frm_DryPastOrdersList
      // 
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lbl_filter);
      this.Controls.Add(this.lbl_OfficeID);
      this.Controls.Add(this.pct_OrderPreprint1);
      this.Controls.Add(this.pct_OrderPreprint2);
      this.Controls.Add(this.pct_OrderPreprint0);
      this.Controls.Add(this.lbl_DataToWiew2_3);
      this.Controls.Add(this.lbl_DataToWiew2_2);
      this.Controls.Add(this.lbl_DataToWiew2_1);
      this.Controls.Add(this.lbl_DataToWiew2_0);
      this.Controls.Add(this.lbl_DataToWiew1_3);
      this.Controls.Add(this.lbl_DataToWiew1_2);
      this.Controls.Add(this.lbl_DataToWiew1_1);
      this.Controls.Add(this.lbl_DataToWiew1_0);
      this.Controls.Add(this.lbl_DataToWiew0_3);
      this.Controls.Add(this.lbl_DataToWiew0_2);
      this.Controls.Add(this.lbl_DataToWiew0_1);
      this.Controls.Add(this.lbl_DataToWiew0_0);
      this.Controls.Add(this.lbl_Customer1);
      this.Controls.Add(this.lbl_Customer2);
      this.Controls.Add(this.lbl_Customer0);
      this.Controls.Add(this.gdv_Orders);
      this.Controls.Add(this.lbl_DryEndTitle);
      this.Controls.Add(this.brk_Details);
      this.Controls.Add(this.ttl_Order0);
      this.Controls.Add(this.lbl_Sheets0);
      this.Controls.Add(this.lbl_Misc0);
      this.Controls.Add(this.ttl_Order1);
      this.Controls.Add(this.lbl_Misc1);
      this.Controls.Add(this.lbl_Sheets1);
      this.Controls.Add(this.ttl_Order2);
      this.Controls.Add(this.lbl_Sheets2);
      this.Controls.Add(this.lbl_Misc2);
      this.Controls.Add(this.lbl_ModID);
      this.Controls.Add(this.lbl_SetupID);
      this.DesignGridHorizontalLines = 2;
      this.DesignGridVerticalLines = 3;
      this.Name = "Frm_DryPastOrdersList";
      this.Text = "Frm_DryPastOrdersList";
      this.Load += new System.EventHandler(this.Frm_DryPastOrdersList_Load);
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
      this.Controls.SetChildIndex(this.lbl_SetupID, 0);
      this.Controls.SetChildIndex(this.lbl_ModID, 0);
      this.Controls.SetChildIndex(this.lbl_Misc2, 0);
      this.Controls.SetChildIndex(this.lbl_Sheets2, 0);
      this.Controls.SetChildIndex(this.ttl_Order2, 0);
      this.Controls.SetChildIndex(this.lbl_Sheets1, 0);
      this.Controls.SetChildIndex(this.lbl_Misc1, 0);
      this.Controls.SetChildIndex(this.ttl_Order1, 0);
      this.Controls.SetChildIndex(this.lbl_Misc0, 0);
      this.Controls.SetChildIndex(this.lbl_Sheets0, 0);
      this.Controls.SetChildIndex(this.ttl_Order0, 0);
      this.Controls.SetChildIndex(this.brk_Details, 0);
      this.Controls.SetChildIndex(this.lbl_DryEndTitle, 0);
      this.Controls.SetChildIndex(this.gdv_Orders, 0);
      this.Controls.SetChildIndex(this.lbl_Customer0, 0);
      this.Controls.SetChildIndex(this.lbl_Customer2, 0);
      this.Controls.SetChildIndex(this.lbl_Customer1, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew0_0, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew0_1, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew0_2, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew0_3, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew1_0, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew1_1, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew1_2, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew1_3, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew2_0, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew2_1, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew2_2, 0);
      this.Controls.SetChildIndex(this.lbl_DataToWiew2_3, 0);
      this.Controls.SetChildIndex(this.pct_OrderPreprint0, 0);
      this.Controls.SetChildIndex(this.pct_OrderPreprint2, 0);
      this.Controls.SetChildIndex(this.pct_OrderPreprint1, 0);
      this.Controls.SetChildIndex(this.lbl_OfficeID, 0);
      this.Controls.SetChildIndex(this.lbl_filter, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_OrderPreprint0)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    private string getDataToView(int level, int number, DataRow dataRow)
    {
      string res = "";
      switch (number)
      {
        case 0:
        default:
          res = dataRow[PastOrder.POL_MTH_CODE_n + level.ToString()].ToString();
          break;
        case 1:
          res = dataRow[PastOrder.POL_MTH_NEXT_MACHINE_n + level.ToString()].ToString();
          break;
        case 2:
          res = dataRow[PastOrder.POL_STACKER_NOTE_n + level.ToString()].ToString();
          break;
        case 3:
          res = dataRow[PastOrder.POL_CUT_TO_PATTERN_n + level.ToString()].ToString();
          break;
        case 4:
          res = dataRow[PastOrder.POL_DATA_TO_PRINT_n_m + level.ToString() + "_1"].ToString();
          break;
        case 5:
          res = dataRow[PastOrder.POL_DATA_TO_PRINT_n_m + level.ToString() + "_2"].ToString();
          break;
        case 6:
          res = dataRow[PastOrder.POL_DATA_TO_PRINT_n_m + level.ToString() + "_3"].ToString();
          break;
        case 7:
          res = dataRow[PastOrder.POL_DATA_TO_PRINT_n_m + level.ToString() + "_4"].ToString();
          break;
      }
      return res;
    }

    private void gdv_Orders_SelectionChanged(object sender, EventArgs e)
    {
      if (gdv_Orders.SelectedRows.Count == 1)
      {
        DataRow dataRow = ((DataRowView)gdv_Orders.SelectedRows[0].DataBoundItem).Row;
        //order 0
        ttl_Order0.Text = DBManager.getDBManager().language.getTranslation(
          typeof(LevelName).Name + "." + dataRow[PastOrder.POL_LEVEL_NAME_0].ToString()) +
          " - " + dataRow[PastOrder.POL_ORDER_NUMBER_0].ToString();
        lbl_Misc0.Text = SyncroConverter.ConvertString(dataRow[PastOrder.POL_SHEET_WIDTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)
          + " X " + SyncroConverter.ConvertString(dataRow[PastOrder.POL_SHEET_LENGTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)
          + " X " + dataRow[PastOrder.POL_OUTS_0].ToString();
        int sheets = 0;
        try
        {
          sheets = Convert.ToInt32(dataRow[PastOrder.POL_SEGMENT_SHEET_COUNT_0].ToString());
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting PastOrder.POL_SEGMENT_SHEET_COUNT_0 or PastOrder.POL_OUTS_0", ex);
        }
        lbl_Sheets0.Text = sheets.ToString();
        lbl_Customer0.Text = dataRow[PastOrder.POL_CUSTOMER_NAME_0].ToString().Trim();
        this.lbl_DataToWiew0_0.Text = this.getDataToView(0, 0, dataRow);
        this.lbl_DataToWiew0_1.Text = this.getDataToView(0, 1, dataRow);
        this.lbl_DataToWiew0_2.Text = this.getDataToView(0, 2, dataRow);
        this.lbl_DataToWiew0_3.Text = this.getDataToView(0, 3, dataRow);
        this.pct_OrderPreprint0.Visible = Convert.ToBoolean(dataRow[PastOrder.POL_PREPRINT_0].ToString());

        //order 1 
        if (Convert.ToInt64(dataRow[PastOrder.POL_SHEET_LENGTH_1].ToString()) != 0)
        {
          ttl_Order1.Text = DBManager.getDBManager().language.getTranslation(
            typeof(LevelName).Name + "." + dataRow[PastOrder.POL_LEVEL_NAME_1].ToString()) +
            " - " + dataRow[PastOrder.POL_ORDER_NUMBER_1].ToString();
          lbl_Misc1.Text = SyncroConverter.ConvertString(dataRow[PastOrder.POL_SHEET_WIDTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)
            + " X " + SyncroConverter.ConvertString(dataRow[PastOrder.POL_SHEET_LENGTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)
            + " X " + dataRow[PastOrder.POL_OUTS_1].ToString();
          sheets = 0;
          try
          {
            sheets = Convert.ToInt32(dataRow[PastOrder.POL_SEGMENT_SHEET_COUNT_1].ToString());
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting PastOrder.POL_SEGMENT_SHEET_COUNT_1 or PastOrder.POL_OUTS_1", ex);
          }
          lbl_Sheets1.Text = sheets.ToString();
          lbl_Customer1.Text = dataRow[PastOrder.POL_CUSTOMER_NAME_1].ToString().Trim();
          this.lbl_DataToWiew1_0.Text = this.getDataToView(1, 0, dataRow);
          this.lbl_DataToWiew1_1.Text = this.getDataToView(1, 1, dataRow);
          this.lbl_DataToWiew1_2.Text = this.getDataToView(1, 2, dataRow);
          this.lbl_DataToWiew1_3.Text = this.getDataToView(1, 3, dataRow);
          this.pct_OrderPreprint1.Visible = Convert.ToBoolean(dataRow[PastOrder.POL_PREPRINT_1].ToString());
        }
        else
        {
          CleanOrderDetails(false, true, false);
        }

        //order 2
        if (Convert.ToInt64(dataRow[Order.DEL_SHEET_LENGTH_2].ToString()) != 0)
        {
          ttl_Order2.Text = DBManager.getDBManager().language.getTranslation(
            typeof(LevelName).Name + "." + dataRow[PastOrder.POL_LEVEL_NAME_2].ToString()) +
            " - " + dataRow[PastOrder.POL_ORDER_NUMBER_2].ToString();
          lbl_Misc2.Text = SyncroConverter.ConvertString(dataRow[PastOrder.POL_SHEET_WIDTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)
            + " X " + SyncroConverter.ConvertString(dataRow[PastOrder.POL_SHEET_LENGTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)
            + " X " + dataRow[PastOrder.POL_OUTS_2].ToString();
          sheets = 0;
          try
          {
            sheets = Convert.ToInt32(dataRow[PastOrder.POL_SEGMENT_SHEET_COUNT_2].ToString());
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting PastOrder.POL_SEGMENT_SHEET_COUNT_2 or PastOrder.POL_OUTS_2", ex);
          }
          lbl_Sheets2.Text = sheets.ToString();
          lbl_Customer2.Text = dataRow[PastOrder.POL_CUSTOMER_NAME_2].ToString().Trim();
          this.lbl_DataToWiew2_0.Text = this.getDataToView(2, 0, dataRow);
          this.lbl_DataToWiew2_1.Text = this.getDataToView(2, 1, dataRow);
          this.lbl_DataToWiew2_2.Text = this.getDataToView(2, 2, dataRow);
          this.lbl_DataToWiew2_3.Text = this.getDataToView(2, 3, dataRow);
          this.pct_OrderPreprint2.Visible = Convert.ToBoolean(dataRow[PastOrder.POL_PREPRINT_2].ToString());
        }
        else
        {
          CleanOrderDetails(false, false, true);
        }

        //debug info
        lbl_SetupID.Text = "SetUpID: " + dataRow[PastOrder.POL_SETUP_ID].ToString();
        lbl_ModID.Text = "ModID: " + dataRow[PastOrder.POL_SETUP_MODID].ToString();
        //agrassi_FI26-05-08_3
        lbl_OfficeID.Text = "OfficeID: " + dataRow[PastOrder.POL_OFFICE_ID].ToString();
      }
      else
      {
        CleanOrderDetails(true, true, true);
      }
    }

    /// <summary>
    /// Clean all controls for order details
    /// </summary>
    private void CleanOrderDetails(bool level0, bool level1, bool level2)
    {
      if (level0)
      {
        ttl_Order0.Text = lbl_Customer0.Text =
        lbl_Misc0.Text = lbl_Sheets0.Text = String.Empty;
        this.lbl_DataToWiew0_0.Text = String.Empty;
        this.lbl_DataToWiew0_1.Text = String.Empty;
        this.lbl_DataToWiew0_2.Text = String.Empty;
        this.lbl_DataToWiew0_3.Text = String.Empty;
        this.pct_OrderPreprint0.Visible = false;
      }
      if (level1)
      {
        ttl_Order1.Text = lbl_Customer1.Text =
        lbl_Misc1.Text = lbl_Sheets1.Text = String.Empty;
        this.lbl_DataToWiew1_0.Text = String.Empty;
        this.lbl_DataToWiew1_1.Text = String.Empty;
        this.lbl_DataToWiew1_2.Text = String.Empty;
        this.lbl_DataToWiew1_3.Text = String.Empty;
        this.pct_OrderPreprint1.Visible = false;
      }
      if (level2)
      {
        ttl_Order2.Text = lbl_Customer2.Text =
        lbl_Misc2.Text = lbl_Sheets2.Text = String.Empty;
        this.lbl_DataToWiew2_0.Text = String.Empty;
        this.lbl_DataToWiew2_1.Text = String.Empty;
        this.lbl_DataToWiew2_2.Text = String.Empty;
        this.lbl_DataToWiew2_3.Text = String.Empty;
        this.pct_OrderPreprint2.Visible = false;
      }
      if (level0 && level1 && level2)
      {
        lbl_SetupID.Text = "SetupID: ";
        lbl_ModID.Text = "ModID: ";
        //agrassi_FI26-05-08_3
        lbl_OfficeID.Text = "OfficeID: ";
      }
    }

    /// <summary>
    /// Show / Hide debug info.
    /// </summary>
    protected override void ControlD()
    {
      base.ControlD();
      debugInfo = !debugInfo;
      //agrassi_FI26-05-08_3
      lbl_SetupID.Visible = lbl_ModID.Visible = lbl_OfficeID.Visible = debugInfo;
      //lbl_SetupID.Visible = lbl_ModID.Visible = debugInfo;
      gdv_Orders.Invalidate();
    }

    private void Frm_DryPastOrdersList_Load(object sender, EventArgs e)
    {
      if (gdv_Orders.Rows.Count > 0)
      {
        this.gdv_Orders.Focus();
        gdv_Orders.Rows[0].Selected = true;
      }
    }

    private void gdv_Orders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      DataRow dataRow = ((DataRowView)gdv_Orders.Rows[e.RowIndex].DataBoundItem).Row;

      if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_QUALITY_ID
            && e.Value != null)
      {
        e.Value = e.Value.ToString().Trim();
      }

      if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_WEB_WIDTH
            && e.Value != null)
      {
        try
        {
          e.Value = SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_Orders_CellFormatting - " + PastOrder.POL_WEB_WIDTH + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
        }
      }

      if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_TRIM
            && e.Value != null)
      {
        try
        {
          e.Value = SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_Orders_CellFormatting - " + PastOrder.POL_TRIM + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
        }
      }

      if (this.gdv_Orders.Columns[e.ColumnIndex].Name == LENGTH_COL_NAME
            && e.Value != null)
      {
        try
        {
          e.Value = ((Convert.ToInt32(e.Value) *
            ((Convert.ToInt32(dataRow[PastOrder.POL_SEGMENT_SHEET_COUNT_0]) /
            Convert.ToInt32(dataRow[PastOrder.POL_OUTS_0])))) / 10000);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_Orders_CellFormatting - " + LENGTH_COL_NAME + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
        }
      }

      if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_BEGIN_TIME
            && e.Value != null)
      {
        try
        {
          string formattedTime = ((DateTime)e.Value).ToLocalTime().ToString();
          if (formattedTime != null && formattedTime.Length >= 16)
          {
            formattedTime = formattedTime.Replace('.', ':');
          }
          e.Value = formattedTime;
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_Orders_CellFormatting - " + PastOrder.POL_BEGIN_TIME + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
        }
      }
    }

    private void gdv_Orders_DoubleClick(object sender, EventArgs e)
    {
      if (gdv_Orders.SelectedRows.Count == 1)
      {
        DataRow dataRow = ((DataRowView)gdv_Orders.SelectedRows[0].DataBoundItem).Row;
        int position = 1;
        if (dataRow != null)
        {
          try
          {
            position = Convert.ToInt32(dataRow[PastOrder.POL_POSITION].ToString());
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_DoubleClick - error converting value.", ex);
          }
        }
        else
        {
          Logger.LogError(this, "gdv_Orders_DoubleClick - datarow was null");
        }
        Frm_OrdersInsSubGroup.positionPastOrderToShowDRY = position;
        Frm_OrdersInsSubGroup.PastOrderfiltro = this.filter;
      }
      askForPage(typeof(Frm_DryPastOrders));
    }

  }
}

