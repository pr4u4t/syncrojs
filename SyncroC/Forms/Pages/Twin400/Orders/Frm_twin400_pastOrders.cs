using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.DataBase;
using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Forms.PopUp;
using SyncroC.DataBase.LineConfig;

namespace SyncroC.Forms.Pages.Twin400.Orders
{
  public class Frm_twin400_pastOrders : SyncroC.Forms.Pages.Twin400.Orders.Frm_twin400_ordersSubgroup
  {
    const int PAST_SETUPS = 500;
    private System.ComponentModel.IContainer components = null;
    protected SyncroC.Controls.TitledLabel ttl_Gap2;
    protected SyncroC.Controls.TitledLabel ttl_Positioning2;
    protected SyncroC.Controls.TitledLabel ttl_Score2;
    protected SyncroC.Controls.TitledLabel ttl_ScoreMeasures2;
    protected SyncroC.Controls.TitledLabel ttl_Width2;
    protected SyncroC.Controls.TitledLabel ttl_Outs2;
    protected SyncroC.Controls.TitledLabel ttl_Customer2;
    protected SyncroC.Controls.TitledLabel ttl_OrderNumber2;
    protected SyncroC.Controls.TitledLabel ttl_Level2;
    protected SyncroC.Controls.TitledLabel ttl_Gap0;
    protected SyncroC.Controls.TitledLabel ttl_Positioning0;
    protected SyncroC.Controls.TitledLabel ttl_Score0;
    protected SyncroC.Controls.TitledLabel ttl_ScoreMeasures0;
    protected SyncroC.Controls.TitledLabel ttl_Width0;
    protected SyncroC.Controls.TitledLabel ttl_Outs0;
    protected SyncroC.Controls.TitledLabel ttl_Customer0;
    protected SyncroC.Controls.TitledLabel ttl_OrderNumber0;
    protected SyncroC.Controls.TitledLabel ttl_Level0;
    protected SyncroC.Controls.TitledLabel ttl_Gap1;
    protected SyncroC.Controls.TitledLabel ttl_Positioning1;
    protected SyncroC.Controls.TitledLabel ttl_Score1;
    protected SyncroC.Controls.TitledLabel ttl_ScoreMeasures1;
    protected SyncroC.Controls.TitledLabel ttl_Width1;
    protected SyncroC.Controls.TitledLabel ttl_Outs1;
    protected SyncroC.Controls.TitledLabel ttl_Customer1;
    protected SyncroC.Controls.TitledLabel ttl_OrderNumber1;
    protected SyncroC.Controls.TitledLabel ttl_Level1;
    protected Label lbl_Pos33;
    protected Label lbl_Pos32;
    protected Label lbl_Pos31;
    protected Label lbl_Pos23;
    protected Label lbl_Pos22;
    protected Label lbl_Pos21;
    protected Label lbl_Pos13;
    protected Label lbl_Pos12;
    protected Label lbl_Pos11;
    protected SyncroC.Controls.Title ttl_Order0;
    protected SyncroC.Controls.Title ttl_Order1;
    protected SyncroC.Controls.Title ttl_Order2;
    private SyncroC.Controls.Bracket brk_Details;
    private SyncroC.Controls.GridView gdv_Orders;
    private SyncroC.Controls.Title lbl_DryEndTitle;
    private bool online = false;
    private DataGridViewColumn clm_Order_SetupCode;
    private DataGridViewColumn clm_Order_Flute;
    private DataGridViewColumn clm_Order_Quality;
    private DataGridViewColumn clm_Order_WebWidth;
    private DataGridViewColumn clm_Order_TrimType;
    private DataGridViewColumn clm_Order_Trim;
    private DataGridViewColumn clm_Order_ScoreGap;
    private DataGridViewColumn clm_Order_Length;
    private DataGridViewColumn clm_Order_Group;
    private DataGridViewColumn clm_Order_DateAndTime;
    private DataGridViewColumn clm_Order_Id;
    private DataGridViewColumn clm_Order_Mod;
    private DataGridViewColumn clm_Order_Shafts;
    protected bool HiddenCol = true;
    Font bckFont;

    public Frm_twin400_pastOrders()
    {
      InitializeComponent();

      //this.orderDataSet = DBManager.getDBManager().pastOrder.pastOrder;
      
      this.ordType = typeof(DataBase.PastOrder);
      this.ordInst = (DataBase.PastOrder)new DataBase.PastOrder();
      this.orderDbName = PastOrder.PAST_ORDER_LIST;
      this.orderDbAcronym = "POL";

      this.gdv_Orders.DataSource = DBManager.getDBManager().pastOrder.getTable();

      this.clm_Order_SetupCode = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_SetupCode.Name = PastOrder.POL_SETUP_CODE;
      this.clm_Order_SetupCode.HeaderText = "Code";
      this.clm_Order_SetupCode.DataPropertyName = PastOrder.POL_SETUP_CODE;
      this.clm_Order_SetupCode.Width = 170;

      this.clm_Order_Flute = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_Flute.Name = PastOrder.POL_FLUTE;
      this.clm_Order_Flute.HeaderText = "Flute";
      this.clm_Order_Flute.DataPropertyName = PastOrder.POL_FLUTE;
      this.clm_Order_Flute.Width = 55;

      this.clm_Order_Quality = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_Quality.Name = PastOrder.POL_QUALITY_ID;
      this.clm_Order_Quality.HeaderText = "Quality";
      this.clm_Order_Quality.DataPropertyName = PastOrder.POL_QUALITY_ID;
      this.clm_Order_Quality.Width = 222;

      this.clm_Order_WebWidth = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_WebWidth.Name = PastOrder.POL_WEB_WIDTH;
      this.clm_Order_WebWidth.HeaderText = "Width";
      this.clm_Order_WebWidth.DataPropertyName = PastOrder.POL_WEB_WIDTH;
      this.clm_Order_WebWidth.Width = 69;

      this.clm_Order_TrimType = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_TrimType.Name = PastOrder.POL_TRIM_TYPE;
      this.clm_Order_TrimType.HeaderText = "TrimType";
      this.clm_Order_TrimType.DataPropertyName = PastOrder.POL_TRIM_TYPE;
      this.clm_Order_TrimType.Width = 101;

      this.clm_Order_Trim = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_Trim.Name = PastOrder.POL_TRIM;
      this.clm_Order_Trim.HeaderText = "Trim";
      this.clm_Order_Trim.DataPropertyName = PastOrder.POL_TRIM;
      this.clm_Order_Trim.Width = 59;

      this.clm_Order_ScoreGap = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_ScoreGap.Name = PastOrder.POL_SCORE_GAP;
      this.clm_Order_ScoreGap.HeaderText = "Score Gap";
      this.clm_Order_ScoreGap.DataPropertyName = PastOrder.POL_SCORE_GAP;
      this.clm_Order_ScoreGap.Width = 56;

      this.clm_Order_Group = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_Group.Name = PastOrder.POL_DELIVERY_0;
      this.clm_Order_Group.HeaderText = "Group";
      this.clm_Order_Group.DataPropertyName = PastOrder.POL_DELIVERY_0;
      this.clm_Order_Group.Width = 55;

      this.clm_Order_Length = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_Length.Name = PastOrder.POL_SHEET_LENGTH_0;
      this.clm_Order_Length.HeaderText = "Length";
      this.clm_Order_Length.DataPropertyName = PastOrder.POL_SHEET_LENGTH_0;
      this.clm_Order_Length.Width = 90;

      this.clm_Order_DateAndTime = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_DateAndTime.Name = PastOrder.POL_FINISH_TIME;
      this.clm_Order_DateAndTime.HeaderText = "Date and Time";
      this.clm_Order_DateAndTime.DataPropertyName = PastOrder.POL_FINISH_TIME;
      this.clm_Order_DateAndTime.Width = 220;

      this.clm_Order_Id = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_Id.Name = PastOrder.POL_SETUP_ID;
      this.clm_Order_Id.HeaderText = "Id";
      this.clm_Order_Id.DataPropertyName = PastOrder.POL_SETUP_ID;
      this.clm_Order_Id.Width = 44;
      this.clm_Order_Id.Visible = false;

      this.clm_Order_Mod = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_Mod.Name = PastOrder.POL_SETUP_MODID;
      this.clm_Order_Mod.HeaderText = "Mod";
      this.clm_Order_Mod.DataPropertyName = PastOrder.POL_SETUP_MODID;
      this.clm_Order_Mod.Width = 23;
      this.clm_Order_Mod.Visible = false;

      this.clm_Order_Shafts = new DataGridViewColumn(new DataGridViewTextBoxCell());
      this.clm_Order_Shafts.Name = PastOrder.POL_SHAFT_MASK;
      this.clm_Order_Shafts.HeaderText = "Shafts";
      this.clm_Order_Shafts.DataPropertyName = PastOrder.POL_SHAFT_MASK;
      this.clm_Order_Shafts.Width = 118;
      this.clm_Order_Shafts.Visible = false;

      this.gdv_Orders.Columns.Add(this.clm_Order_SetupCode);
      this.gdv_Orders.Columns.Add(this.clm_Order_Flute);
      this.gdv_Orders.Columns.Add(this.clm_Order_Quality);
      this.gdv_Orders.Columns.Add(this.clm_Order_WebWidth);
      this.gdv_Orders.Columns.Add(this.clm_Order_TrimType);
      this.gdv_Orders.Columns.Add(this.clm_Order_Trim);
      this.gdv_Orders.Columns.Add(this.clm_Order_ScoreGap);
      this.gdv_Orders.Columns.Add(this.clm_Order_Length);
      this.gdv_Orders.Columns.Add(this.clm_Order_DateAndTime);
      this.gdv_Orders.Columns.Add(this.clm_Order_Id);
      this.gdv_Orders.Columns.Add(this.clm_Order_Mod);
      this.gdv_Orders.Columns.Add(this.clm_Order_Shafts);

      this.gdv_Orders.Sort(this.clm_Order_DateAndTime, ListSortDirection.Descending);
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
      {
        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
        this.lbl_CtrlF5_YY.Text = "";
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
      this.gdv_Orders.DataSource = null;
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_pastOrders));
      this.ttl_Gap2 = new SyncroC.Controls.TitledLabel();
      this.ttl_Positioning2 = new SyncroC.Controls.TitledLabel();
      this.ttl_Score2 = new SyncroC.Controls.TitledLabel();
      this.ttl_ScoreMeasures2 = new SyncroC.Controls.TitledLabel();
      this.ttl_Width2 = new SyncroC.Controls.TitledLabel();
      this.ttl_Outs2 = new SyncroC.Controls.TitledLabel();
      this.ttl_Customer2 = new SyncroC.Controls.TitledLabel();
      this.ttl_OrderNumber2 = new SyncroC.Controls.TitledLabel();
      this.ttl_Level2 = new SyncroC.Controls.TitledLabel();
      this.ttl_Gap0 = new SyncroC.Controls.TitledLabel();
      this.ttl_Positioning0 = new SyncroC.Controls.TitledLabel();
      this.ttl_Score0 = new SyncroC.Controls.TitledLabel();
      this.ttl_ScoreMeasures0 = new SyncroC.Controls.TitledLabel();
      this.ttl_Width0 = new SyncroC.Controls.TitledLabel();
      this.ttl_Outs0 = new SyncroC.Controls.TitledLabel();
      this.ttl_Customer0 = new SyncroC.Controls.TitledLabel();
      this.ttl_OrderNumber0 = new SyncroC.Controls.TitledLabel();
      this.ttl_Level0 = new SyncroC.Controls.TitledLabel();
      this.ttl_Gap1 = new SyncroC.Controls.TitledLabel();
      this.ttl_Positioning1 = new SyncroC.Controls.TitledLabel();
      this.ttl_Score1 = new SyncroC.Controls.TitledLabel();
      this.ttl_ScoreMeasures1 = new SyncroC.Controls.TitledLabel();
      this.ttl_Width1 = new SyncroC.Controls.TitledLabel();
      this.ttl_Outs1 = new SyncroC.Controls.TitledLabel();
      this.ttl_Customer1 = new SyncroC.Controls.TitledLabel();
      this.ttl_OrderNumber1 = new SyncroC.Controls.TitledLabel();
      this.ttl_Level1 = new SyncroC.Controls.TitledLabel();
      this.lbl_Pos33 = new System.Windows.Forms.Label();
      this.lbl_Pos32 = new System.Windows.Forms.Label();
      this.lbl_Pos31 = new System.Windows.Forms.Label();
      this.lbl_Pos23 = new System.Windows.Forms.Label();
      this.lbl_Pos22 = new System.Windows.Forms.Label();
      this.lbl_Pos21 = new System.Windows.Forms.Label();
      this.lbl_Pos13 = new System.Windows.Forms.Label();
      this.lbl_Pos12 = new System.Windows.Forms.Label();
      this.lbl_Pos11 = new System.Windows.Forms.Label();
      this.ttl_Order0 = new SyncroC.Controls.Title();
      this.ttl_Order1 = new SyncroC.Controls.Title();
      this.ttl_Order2 = new SyncroC.Controls.Title();
      this.brk_Details = new SyncroC.Controls.Bracket();
      this.gdv_Orders = new SyncroC.Controls.GridView();
      this.lbl_DryEndTitle = new SyncroC.Controls.Title();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
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
      // tab_TabC
      // 
      this.tab_TabC.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // ttl_Gap2
      // 
      this.ttl_Gap2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Gap2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Gap2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Gap2.ImagePassword = null;
      this.ttl_Gap2.Location = new System.Drawing.Point(1141, 649);
      this.ttl_Gap2.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.ttl_Gap2.Name = "ttl_Gap2";
      this.ttl_Gap2.Size = new System.Drawing.Size(54, 59);
      this.ttl_Gap2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Gap2.TabIndex = 434;
      this.ttl_Gap2.Text = "Gap";
      this.ttl_Gap2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Gap2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Gap2.TranslateText = true;
      this.ttl_Gap2.Value = "";
      this.ttl_Gap2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Gap2.ValueMultiline = true;
      // 
      // ttl_Positioning2
      // 
      this.ttl_Positioning2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Positioning2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Positioning2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Positioning2.ImagePassword = null;
      this.ttl_Positioning2.Location = new System.Drawing.Point(1007, 649);
      this.ttl_Positioning2.Name = "ttl_Positioning2";
      this.ttl_Positioning2.Size = new System.Drawing.Size(132, 59);
      this.ttl_Positioning2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Positioning2.TabIndex = 433;
      this.ttl_Positioning2.Text = "Positioning";
      this.ttl_Positioning2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Positioning2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Positioning2.TranslateText = true;
      this.ttl_Positioning2.Value = "";
      this.ttl_Positioning2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Positioning2.ValueMultiline = true;
      // 
      // ttl_Score2
      // 
      this.ttl_Score2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Score2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Score2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Score2.ImagePassword = null;
      this.ttl_Score2.Location = new System.Drawing.Point(957, 649);
      this.ttl_Score2.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.ttl_Score2.Name = "ttl_Score2";
      this.ttl_Score2.Size = new System.Drawing.Size(48, 59);
      this.ttl_Score2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Score2.TabIndex = 432;
      this.ttl_Score2.Text = "Score";
      this.ttl_Score2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Score2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Score2.TranslateText = true;
      this.ttl_Score2.Value = "";
      this.ttl_Score2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Score2.ValueMultiline = true;
      // 
      // ttl_ScoreMeasures2
      // 
      this.ttl_ScoreMeasures2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ScoreMeasures2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ScoreMeasures2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ScoreMeasures2.ImagePassword = null;
      this.ttl_ScoreMeasures2.Location = new System.Drawing.Point(630, 649);
      this.ttl_ScoreMeasures2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.ttl_ScoreMeasures2.Name = "ttl_ScoreMeasures2";
      this.ttl_ScoreMeasures2.Size = new System.Drawing.Size(325, 59);
      this.ttl_ScoreMeasures2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_ScoreMeasures2.TabIndex = 431;
      this.ttl_ScoreMeasures2.Text = "ScoreMeasures";
      this.ttl_ScoreMeasures2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_ScoreMeasures2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ScoreMeasures2.TranslateText = true;
      this.ttl_ScoreMeasures2.Value = "";
      this.ttl_ScoreMeasures2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_ScoreMeasures2.ValueMultiline = true;
      // 
      // ttl_Width2
      // 
      this.ttl_Width2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Width2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Width2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Width2.ImagePassword = null;
      this.ttl_Width2.Location = new System.Drawing.Point(561, 649);
      this.ttl_Width2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.ttl_Width2.Name = "ttl_Width2";
      this.ttl_Width2.Size = new System.Drawing.Size(67, 59);
      this.ttl_Width2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Width2.TabIndex = 430;
      this.ttl_Width2.Text = "Width";
      this.ttl_Width2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Width2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Width2.TranslateText = true;
      this.ttl_Width2.Value = "";
      this.ttl_Width2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Width2.ValueMultiline = true;
      // 
      // ttl_Outs2
      // 
      this.ttl_Outs2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Outs2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Outs2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Outs2.ImagePassword = null;
      this.ttl_Outs2.Location = new System.Drawing.Point(520, 649);
      this.ttl_Outs2.Margin = new System.Windows.Forms.Padding(1, 3, 2, 3);
      this.ttl_Outs2.Name = "ttl_Outs2";
      this.ttl_Outs2.Size = new System.Drawing.Size(39, 59);
      this.ttl_Outs2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Outs2.TabIndex = 429;
      this.ttl_Outs2.Text = "Outs";
      this.ttl_Outs2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Outs2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Outs2.TranslateText = true;
      this.ttl_Outs2.Value = "";
      this.ttl_Outs2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Outs2.ValueMultiline = true;
      // 
      // ttl_Customer2
      // 
      this.ttl_Customer2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Customer2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Customer2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Customer2.ImagePassword = null;
      this.ttl_Customer2.Location = new System.Drawing.Point(277, 649);
      this.ttl_Customer2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.ttl_Customer2.Name = "ttl_Customer2";
      this.ttl_Customer2.Size = new System.Drawing.Size(241, 59);
      this.ttl_Customer2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Customer2.TabIndex = 428;
      this.ttl_Customer2.Text = "Customer";
      this.ttl_Customer2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Customer2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Customer2.TranslateText = true;
      this.ttl_Customer2.Value = "";
      this.ttl_Customer2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Customer2.ValueMultiline = true;
      // 
      // ttl_OrderNumber2
      // 
      this.ttl_OrderNumber2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_OrderNumber2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_OrderNumber2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_OrderNumber2.ImagePassword = null;
      this.ttl_OrderNumber2.Location = new System.Drawing.Point(151, 649);
      this.ttl_OrderNumber2.Margin = new System.Windows.Forms.Padding(1, 3, 2, 3);
      this.ttl_OrderNumber2.Name = "ttl_OrderNumber2";
      this.ttl_OrderNumber2.Size = new System.Drawing.Size(124, 63);
      this.ttl_OrderNumber2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_OrderNumber2.TabIndex = 427;
      this.ttl_OrderNumber2.Text = "OrderNumber";
      this.ttl_OrderNumber2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_OrderNumber2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_OrderNumber2.TranslateText = true;
      this.ttl_OrderNumber2.Value = "";
      this.ttl_OrderNumber2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_OrderNumber2.ValueMultiline = true;
      // 
      // ttl_Level2
      // 
      this.ttl_Level2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Level2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Level2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Level2.ImagePassword = null;
      this.ttl_Level2.Location = new System.Drawing.Point(42, 649);
      this.ttl_Level2.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.ttl_Level2.Name = "ttl_Level2";
      this.ttl_Level2.Size = new System.Drawing.Size(107, 63);
      this.ttl_Level2.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Level2.TabIndex = 426;
      this.ttl_Level2.Text = "Level";
      this.ttl_Level2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Level2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Level2.TranslateText = true;
      this.ttl_Level2.Value = "";
      this.ttl_Level2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Level2.ValueMultiline = true;
      // 
      // ttl_Gap0
      // 
      this.ttl_Gap0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Gap0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Gap0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Gap0.ImagePassword = null;
      this.ttl_Gap0.Location = new System.Drawing.Point(1141, 475);
      this.ttl_Gap0.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.ttl_Gap0.Name = "ttl_Gap0";
      this.ttl_Gap0.Size = new System.Drawing.Size(54, 59);
      this.ttl_Gap0.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Gap0.TabIndex = 424;
      this.ttl_Gap0.Text = "Gap";
      this.ttl_Gap0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Gap0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Gap0.TranslateText = true;
      this.ttl_Gap0.Value = "";
      this.ttl_Gap0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Gap0.ValueMultiline = true;
      // 
      // ttl_Positioning0
      // 
      this.ttl_Positioning0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Positioning0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Positioning0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Positioning0.ImagePassword = null;
      this.ttl_Positioning0.Location = new System.Drawing.Point(1007, 475);
      this.ttl_Positioning0.Name = "ttl_Positioning0";
      this.ttl_Positioning0.Size = new System.Drawing.Size(132, 59);
      this.ttl_Positioning0.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Positioning0.TabIndex = 423;
      this.ttl_Positioning0.Text = "Positioning";
      this.ttl_Positioning0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Positioning0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Positioning0.TranslateText = true;
      this.ttl_Positioning0.Value = "";
      this.ttl_Positioning0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Positioning0.ValueMultiline = true;
      // 
      // ttl_Score0
      // 
      this.ttl_Score0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Score0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Score0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Score0.ImagePassword = null;
      this.ttl_Score0.Location = new System.Drawing.Point(957, 475);
      this.ttl_Score0.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.ttl_Score0.Name = "ttl_Score0";
      this.ttl_Score0.Size = new System.Drawing.Size(48, 59);
      this.ttl_Score0.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Score0.TabIndex = 422;
      this.ttl_Score0.Text = "Score";
      this.ttl_Score0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Score0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Score0.TranslateText = true;
      this.ttl_Score0.Value = "";
      this.ttl_Score0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Score0.ValueMultiline = true;
      // 
      // ttl_ScoreMeasures0
      // 
      this.ttl_ScoreMeasures0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ScoreMeasures0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ScoreMeasures0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ScoreMeasures0.ImagePassword = null;
      this.ttl_ScoreMeasures0.Location = new System.Drawing.Point(630, 475);
      this.ttl_ScoreMeasures0.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.ttl_ScoreMeasures0.Name = "ttl_ScoreMeasures0";
      this.ttl_ScoreMeasures0.Size = new System.Drawing.Size(325, 59);
      this.ttl_ScoreMeasures0.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_ScoreMeasures0.TabIndex = 421;
      this.ttl_ScoreMeasures0.Text = "ScoreMeasures";
      this.ttl_ScoreMeasures0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_ScoreMeasures0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ScoreMeasures0.TranslateText = true;
      this.ttl_ScoreMeasures0.Value = "";
      this.ttl_ScoreMeasures0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_ScoreMeasures0.ValueMultiline = true;
      // 
      // ttl_Width0
      // 
      this.ttl_Width0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Width0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Width0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Width0.ImagePassword = null;
      this.ttl_Width0.Location = new System.Drawing.Point(561, 475);
      this.ttl_Width0.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.ttl_Width0.Name = "ttl_Width0";
      this.ttl_Width0.Size = new System.Drawing.Size(67, 59);
      this.ttl_Width0.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Width0.TabIndex = 420;
      this.ttl_Width0.Text = "Width";
      this.ttl_Width0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Width0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Width0.TranslateText = true;
      this.ttl_Width0.Value = "";
      this.ttl_Width0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Width0.ValueMultiline = true;
      // 
      // ttl_Outs0
      // 
      this.ttl_Outs0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Outs0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Outs0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Outs0.ImagePassword = null;
      this.ttl_Outs0.Location = new System.Drawing.Point(520, 475);
      this.ttl_Outs0.Margin = new System.Windows.Forms.Padding(1, 3, 2, 3);
      this.ttl_Outs0.Name = "ttl_Outs0";
      this.ttl_Outs0.Size = new System.Drawing.Size(39, 59);
      this.ttl_Outs0.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Outs0.TabIndex = 419;
      this.ttl_Outs0.Text = "Outs";
      this.ttl_Outs0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Outs0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Outs0.TranslateText = true;
      this.ttl_Outs0.Value = "";
      this.ttl_Outs0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Outs0.ValueMultiline = true;
      // 
      // ttl_Customer0
      // 
      this.ttl_Customer0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Customer0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Customer0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Customer0.ImagePassword = null;
      this.ttl_Customer0.Location = new System.Drawing.Point(277, 475);
      this.ttl_Customer0.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.ttl_Customer0.Name = "ttl_Customer0";
      this.ttl_Customer0.Size = new System.Drawing.Size(241, 59);
      this.ttl_Customer0.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Customer0.TabIndex = 418;
      this.ttl_Customer0.Text = "Customer";
      this.ttl_Customer0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Customer0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Customer0.TranslateText = true;
      this.ttl_Customer0.Value = "";
      this.ttl_Customer0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Customer0.ValueMultiline = true;
      // 
      // ttl_OrderNumber0
      // 
      this.ttl_OrderNumber0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_OrderNumber0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_OrderNumber0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_OrderNumber0.ImagePassword = null;
      this.ttl_OrderNumber0.Location = new System.Drawing.Point(151, 475);
      this.ttl_OrderNumber0.Margin = new System.Windows.Forms.Padding(1, 3, 2, 3);
      this.ttl_OrderNumber0.Name = "ttl_OrderNumber0";
      this.ttl_OrderNumber0.Size = new System.Drawing.Size(124, 63);
      this.ttl_OrderNumber0.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_OrderNumber0.TabIndex = 417;
      this.ttl_OrderNumber0.Text = "OrderNumber";
      this.ttl_OrderNumber0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_OrderNumber0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_OrderNumber0.TranslateText = true;
      this.ttl_OrderNumber0.Value = "";
      this.ttl_OrderNumber0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_OrderNumber0.ValueMultiline = true;
      // 
      // ttl_Level0
      // 
      this.ttl_Level0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Level0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Level0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Level0.ImagePassword = null;
      this.ttl_Level0.Location = new System.Drawing.Point(42, 475);
      this.ttl_Level0.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.ttl_Level0.Name = "ttl_Level0";
      this.ttl_Level0.Size = new System.Drawing.Size(107, 63);
      this.ttl_Level0.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Level0.TabIndex = 416;
      this.ttl_Level0.Text = "Level";
      this.ttl_Level0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Level0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Level0.TranslateText = true;
      this.ttl_Level0.Value = "";
      this.ttl_Level0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Level0.ValueMultiline = true;
      // 
      // ttl_Gap1
      // 
      this.ttl_Gap1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Gap1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Gap1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Gap1.ImagePassword = null;
      this.ttl_Gap1.Location = new System.Drawing.Point(1141, 563);
      this.ttl_Gap1.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.ttl_Gap1.Name = "ttl_Gap1";
      this.ttl_Gap1.Size = new System.Drawing.Size(54, 59);
      this.ttl_Gap1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Gap1.TabIndex = 414;
      this.ttl_Gap1.Text = "Gap";
      this.ttl_Gap1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Gap1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Gap1.TranslateText = true;
      this.ttl_Gap1.Value = "";
      this.ttl_Gap1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Gap1.ValueMultiline = true;
      // 
      // ttl_Positioning1
      // 
      this.ttl_Positioning1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Positioning1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Positioning1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Positioning1.ImagePassword = null;
      this.ttl_Positioning1.Location = new System.Drawing.Point(1007, 563);
      this.ttl_Positioning1.Name = "ttl_Positioning1";
      this.ttl_Positioning1.Size = new System.Drawing.Size(132, 59);
      this.ttl_Positioning1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Positioning1.TabIndex = 413;
      this.ttl_Positioning1.Text = "Positioning";
      this.ttl_Positioning1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Positioning1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Positioning1.TranslateText = true;
      this.ttl_Positioning1.Value = "";
      this.ttl_Positioning1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Positioning1.ValueMultiline = true;
      // 
      // ttl_Score1
      // 
      this.ttl_Score1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Score1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Score1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Score1.ImagePassword = null;
      this.ttl_Score1.Location = new System.Drawing.Point(957, 563);
      this.ttl_Score1.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.ttl_Score1.Name = "ttl_Score1";
      this.ttl_Score1.Size = new System.Drawing.Size(48, 59);
      this.ttl_Score1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Score1.TabIndex = 411;
      this.ttl_Score1.Text = "Score";
      this.ttl_Score1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Score1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Score1.TranslateText = true;
      this.ttl_Score1.Value = "";
      this.ttl_Score1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Score1.ValueMultiline = true;
      // 
      // ttl_ScoreMeasures1
      // 
      this.ttl_ScoreMeasures1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ScoreMeasures1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ScoreMeasures1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ScoreMeasures1.ImagePassword = null;
      this.ttl_ScoreMeasures1.Location = new System.Drawing.Point(630, 563);
      this.ttl_ScoreMeasures1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.ttl_ScoreMeasures1.Name = "ttl_ScoreMeasures1";
      this.ttl_ScoreMeasures1.Size = new System.Drawing.Size(325, 59);
      this.ttl_ScoreMeasures1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_ScoreMeasures1.TabIndex = 409;
      this.ttl_ScoreMeasures1.Text = "ScoreMeasures";
      this.ttl_ScoreMeasures1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_ScoreMeasures1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ScoreMeasures1.TranslateText = true;
      this.ttl_ScoreMeasures1.Value = "";
      this.ttl_ScoreMeasures1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_ScoreMeasures1.ValueMultiline = true;
      // 
      // ttl_Width1
      // 
      this.ttl_Width1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Width1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Width1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Width1.ImagePassword = null;
      this.ttl_Width1.Location = new System.Drawing.Point(561, 563);
      this.ttl_Width1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.ttl_Width1.Name = "ttl_Width1";
      this.ttl_Width1.Size = new System.Drawing.Size(67, 59);
      this.ttl_Width1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Width1.TabIndex = 407;
      this.ttl_Width1.Text = "Width";
      this.ttl_Width1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Width1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Width1.TranslateText = true;
      this.ttl_Width1.Value = "";
      this.ttl_Width1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Width1.ValueMultiline = true;
      // 
      // ttl_Outs1
      // 
      this.ttl_Outs1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Outs1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Outs1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Outs1.ImagePassword = null;
      this.ttl_Outs1.Location = new System.Drawing.Point(520, 563);
      this.ttl_Outs1.Margin = new System.Windows.Forms.Padding(1, 3, 2, 3);
      this.ttl_Outs1.Name = "ttl_Outs1";
      this.ttl_Outs1.Size = new System.Drawing.Size(39, 59);
      this.ttl_Outs1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Outs1.TabIndex = 405;
      this.ttl_Outs1.Text = "Outs";
      this.ttl_Outs1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Outs1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Outs1.TranslateText = true;
      this.ttl_Outs1.Value = "";
      this.ttl_Outs1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Outs1.ValueMultiline = true;
      // 
      // ttl_Customer1
      // 
      this.ttl_Customer1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Customer1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Customer1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Customer1.ImagePassword = null;
      this.ttl_Customer1.Location = new System.Drawing.Point(277, 563);
      this.ttl_Customer1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.ttl_Customer1.Name = "ttl_Customer1";
      this.ttl_Customer1.Size = new System.Drawing.Size(241, 59);
      this.ttl_Customer1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Customer1.TabIndex = 403;
      this.ttl_Customer1.Text = "Customer";
      this.ttl_Customer1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Customer1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Customer1.TranslateText = true;
      this.ttl_Customer1.Value = "";
      this.ttl_Customer1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Customer1.ValueMultiline = true;
      // 
      // ttl_OrderNumber1
      // 
      this.ttl_OrderNumber1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_OrderNumber1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_OrderNumber1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_OrderNumber1.ImagePassword = null;
      this.ttl_OrderNumber1.Location = new System.Drawing.Point(151, 563);
      this.ttl_OrderNumber1.Margin = new System.Windows.Forms.Padding(1, 3, 2, 3);
      this.ttl_OrderNumber1.Name = "ttl_OrderNumber1";
      this.ttl_OrderNumber1.Size = new System.Drawing.Size(124, 63);
      this.ttl_OrderNumber1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_OrderNumber1.TabIndex = 402;
      this.ttl_OrderNumber1.Text = "OrderNumber";
      this.ttl_OrderNumber1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_OrderNumber1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_OrderNumber1.TranslateText = true;
      this.ttl_OrderNumber1.Value = "";
      this.ttl_OrderNumber1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_OrderNumber1.ValueMultiline = true;
      // 
      // ttl_Level1
      // 
      this.ttl_Level1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Level1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Level1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Level1.ImagePassword = null;
      this.ttl_Level1.Location = new System.Drawing.Point(42, 563);
      this.ttl_Level1.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.ttl_Level1.Name = "ttl_Level1";
      this.ttl_Level1.Size = new System.Drawing.Size(107, 63);
      this.ttl_Level1.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ttl_Level1.TabIndex = 399;
      this.ttl_Level1.Text = "Level";
      this.ttl_Level1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Level1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Level1.TranslateText = true;
      this.ttl_Level1.Value = "";
      this.ttl_Level1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttl_Level1.ValueMultiline = true;
      // 
      // lbl_Pos33
      // 
      this.lbl_Pos33.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Pos33.Location = new System.Drawing.Point(94, 631);
      this.lbl_Pos33.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lbl_Pos33.Name = "lbl_Pos33";
      this.lbl_Pos33.Size = new System.Drawing.Size(23, 14);
      this.lbl_Pos33.TabIndex = 412;
      // 
      // lbl_Pos32
      // 
      this.lbl_Pos32.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Pos32.Location = new System.Drawing.Point(69, 631);
      this.lbl_Pos32.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_Pos32.Name = "lbl_Pos32";
      this.lbl_Pos32.Size = new System.Drawing.Size(23, 14);
      this.lbl_Pos32.TabIndex = 410;
      // 
      // lbl_Pos31
      // 
      this.lbl_Pos31.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Pos31.Location = new System.Drawing.Point(44, 631);
      this.lbl_Pos31.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_Pos31.Name = "lbl_Pos31";
      this.lbl_Pos31.Size = new System.Drawing.Size(23, 14);
      this.lbl_Pos31.TabIndex = 408;
      // 
      // lbl_Pos23
      // 
      this.lbl_Pos23.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Pos23.Location = new System.Drawing.Point(94, 545);
      this.lbl_Pos23.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lbl_Pos23.Name = "lbl_Pos23";
      this.lbl_Pos23.Size = new System.Drawing.Size(23, 14);
      this.lbl_Pos23.TabIndex = 406;
      // 
      // lbl_Pos22
      // 
      this.lbl_Pos22.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Pos22.Location = new System.Drawing.Point(69, 545);
      this.lbl_Pos22.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_Pos22.Name = "lbl_Pos22";
      this.lbl_Pos22.Size = new System.Drawing.Size(23, 14);
      this.lbl_Pos22.TabIndex = 404;
      // 
      // lbl_Pos21
      // 
      this.lbl_Pos21.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Pos21.Location = new System.Drawing.Point(44, 545);
      this.lbl_Pos21.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_Pos21.Name = "lbl_Pos21";
      this.lbl_Pos21.Size = new System.Drawing.Size(23, 14);
      this.lbl_Pos21.TabIndex = 401;
      // 
      // lbl_Pos13
      // 
      this.lbl_Pos13.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Pos13.Location = new System.Drawing.Point(94, 457);
      this.lbl_Pos13.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lbl_Pos13.Name = "lbl_Pos13";
      this.lbl_Pos13.Size = new System.Drawing.Size(23, 14);
      this.lbl_Pos13.TabIndex = 400;
      // 
      // lbl_Pos12
      // 
      this.lbl_Pos12.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Pos12.Location = new System.Drawing.Point(69, 457);
      this.lbl_Pos12.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_Pos12.Name = "lbl_Pos12";
      this.lbl_Pos12.Size = new System.Drawing.Size(23, 14);
      this.lbl_Pos12.TabIndex = 397;
      // 
      // lbl_Pos11
      // 
      this.lbl_Pos11.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_Pos11.Location = new System.Drawing.Point(44, 457);
      this.lbl_Pos11.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_Pos11.Name = "lbl_Pos11";
      this.lbl_Pos11.Size = new System.Drawing.Size(23, 14);
      this.lbl_Pos11.TabIndex = 396;
      // 
      // ttl_Order0
      // 
      this.ttl_Order0.BackColor = System.Drawing.Color.Black;
      this.ttl_Order0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order0.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Order0.Location = new System.Drawing.Point(36, 454);
      this.ttl_Order0.Name = "ttl_Order0";
      this.ttl_Order0.Size = new System.Drawing.Size(1159, 84);
      this.ttl_Order0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Order0.TabIndex = 415;
      this.ttl_Order0.TranslateText = false;
      // 
      // ttl_Order1
      // 
      this.ttl_Order1.BackColor = System.Drawing.Color.Black;
      this.ttl_Order1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Order1.Location = new System.Drawing.Point(36, 542);
      this.ttl_Order1.Name = "ttl_Order1";
      this.ttl_Order1.Size = new System.Drawing.Size(1159, 84);
      this.ttl_Order1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Order1.TabIndex = 398;
      this.ttl_Order1.TranslateText = false;
      // 
      // ttl_Order2
      // 
      this.ttl_Order2.BackColor = System.Drawing.Color.Black;
      this.ttl_Order2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Order2.Location = new System.Drawing.Point(36, 628);
      this.ttl_Order2.Name = "ttl_Order2";
      this.ttl_Order2.Size = new System.Drawing.Size(1159, 84);
      this.ttl_Order2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Order2.TabIndex = 425;
      this.ttl_Order2.TranslateText = false;
      // 
      // brk_Details
      // 
      this.brk_Details.BackColor = System.Drawing.Color.LightBlue;
      this.brk_Details.color = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
      this.brk_Details.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_Details.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_Details.Location = new System.Drawing.Point(30, 430);
      this.brk_Details.Name = "brk_Details";
      this.brk_Details.Position = SyncroC.Controls.BracketPosition.Bottom;
      this.brk_Details.ShowArrow = false;
      this.brk_Details.Size = new System.Drawing.Size(1171, 282);
      this.brk_Details.TabIndex = 395;
      this.brk_Details.Text = "Details";
      this.brk_Details.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_Details.TranslateText = true;
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
      this.gdv_Orders.Location = new System.Drawing.Point(42, 224);
      this.gdv_Orders.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.gdv_Orders.MultiSelect = true;
      this.gdv_Orders.Name = "gdv_Orders";
      this.gdv_Orders.RowsHeight = 30;
      this.gdv_Orders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Orders.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.AOCInProgressColor;
      this.gdv_Orders.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Orders.Size = new System.Drawing.Size(1158, 201);
      this.gdv_Orders.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Orders.TabIndex = 394;
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
      this.lbl_DryEndTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DryEndTitle.Location = new System.Drawing.Point(30, 193);
      this.lbl_DryEndTitle.Name = "lbl_DryEndTitle";
      this.lbl_DryEndTitle.Size = new System.Drawing.Size(1171, 237);
      this.lbl_DryEndTitle.Style = SyncroC.Controls.TitleStyles.Big;
      this.lbl_DryEndTitle.TabIndex = 393;
      this.lbl_DryEndTitle.Text = "Dry End Orders List";
      this.lbl_DryEndTitle.TranslateText = true;
      // 
      // Frm_twin400_pastOrders
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.ttl_Gap2);
      this.Controls.Add(this.ttl_Positioning2);
      this.Controls.Add(this.ttl_Score2);
      this.Controls.Add(this.ttl_ScoreMeasures2);
      this.Controls.Add(this.ttl_Width2);
      this.Controls.Add(this.ttl_Outs2);
      this.Controls.Add(this.ttl_Customer2);
      this.Controls.Add(this.ttl_OrderNumber2);
      this.Controls.Add(this.ttl_Level2);
      this.Controls.Add(this.ttl_Gap0);
      this.Controls.Add(this.ttl_Positioning0);
      this.Controls.Add(this.ttl_Score0);
      this.Controls.Add(this.ttl_ScoreMeasures0);
      this.Controls.Add(this.ttl_Width0);
      this.Controls.Add(this.ttl_Outs0);
      this.Controls.Add(this.ttl_Customer0);
      this.Controls.Add(this.ttl_OrderNumber0);
      this.Controls.Add(this.ttl_Level0);
      this.Controls.Add(this.ttl_Gap1);
      this.Controls.Add(this.ttl_Positioning1);
      this.Controls.Add(this.ttl_Score1);
      this.Controls.Add(this.ttl_ScoreMeasures1);
      this.Controls.Add(this.ttl_Width1);
      this.Controls.Add(this.ttl_Outs1);
      this.Controls.Add(this.ttl_Customer1);
      this.Controls.Add(this.ttl_OrderNumber1);
      this.Controls.Add(this.ttl_Level1);
      this.Controls.Add(this.lbl_Pos33);
      this.Controls.Add(this.lbl_Pos32);
      this.Controls.Add(this.lbl_Pos31);
      this.Controls.Add(this.lbl_Pos23);
      this.Controls.Add(this.lbl_Pos22);
      this.Controls.Add(this.lbl_Pos21);
      this.Controls.Add(this.lbl_Pos13);
      this.Controls.Add(this.lbl_Pos12);
      this.Controls.Add(this.lbl_Pos11);
      this.Controls.Add(this.ttl_Order0);
      this.Controls.Add(this.ttl_Order1);
      this.Controls.Add(this.ttl_Order2);
      this.Controls.Add(this.brk_Details);
      this.Controls.Add(this.gdv_Orders);
      this.Controls.Add(this.lbl_DryEndTitle);
      this.Name = "Frm_twin400_pastOrders";
      this.Tag = "";
      this.Text = "Frm_twin400_pastOrders";
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
      this.Controls.SetChildIndex(this.lbl_DryEndTitle, 0);
      this.Controls.SetChildIndex(this.gdv_Orders, 0);
      this.Controls.SetChildIndex(this.brk_Details, 0);
      this.Controls.SetChildIndex(this.ttl_Order2, 0);
      this.Controls.SetChildIndex(this.ttl_Order1, 0);
      this.Controls.SetChildIndex(this.ttl_Order0, 0);
      this.Controls.SetChildIndex(this.lbl_Pos11, 0);
      this.Controls.SetChildIndex(this.lbl_Pos12, 0);
      this.Controls.SetChildIndex(this.lbl_Pos13, 0);
      this.Controls.SetChildIndex(this.lbl_Pos21, 0);
      this.Controls.SetChildIndex(this.lbl_Pos22, 0);
      this.Controls.SetChildIndex(this.lbl_Pos23, 0);
      this.Controls.SetChildIndex(this.lbl_Pos31, 0);
      this.Controls.SetChildIndex(this.lbl_Pos32, 0);
      this.Controls.SetChildIndex(this.lbl_Pos33, 0);
      this.Controls.SetChildIndex(this.ttl_Level1, 0);
      this.Controls.SetChildIndex(this.ttl_OrderNumber1, 0);
      this.Controls.SetChildIndex(this.ttl_Customer1, 0);
      this.Controls.SetChildIndex(this.ttl_Outs1, 0);
      this.Controls.SetChildIndex(this.ttl_Width1, 0);
      this.Controls.SetChildIndex(this.ttl_ScoreMeasures1, 0);
      this.Controls.SetChildIndex(this.ttl_Score1, 0);
      this.Controls.SetChildIndex(this.ttl_Positioning1, 0);
      this.Controls.SetChildIndex(this.ttl_Gap1, 0);
      this.Controls.SetChildIndex(this.ttl_Level0, 0);
      this.Controls.SetChildIndex(this.ttl_OrderNumber0, 0);
      this.Controls.SetChildIndex(this.ttl_Customer0, 0);
      this.Controls.SetChildIndex(this.ttl_Outs0, 0);
      this.Controls.SetChildIndex(this.ttl_Width0, 0);
      this.Controls.SetChildIndex(this.ttl_ScoreMeasures0, 0);
      this.Controls.SetChildIndex(this.ttl_Score0, 0);
      this.Controls.SetChildIndex(this.ttl_Positioning0, 0);
      this.Controls.SetChildIndex(this.ttl_Gap0, 0);
      this.Controls.SetChildIndex(this.ttl_Level2, 0);
      this.Controls.SetChildIndex(this.ttl_OrderNumber2, 0);
      this.Controls.SetChildIndex(this.ttl_Customer2, 0);
      this.Controls.SetChildIndex(this.ttl_Outs2, 0);
      this.Controls.SetChildIndex(this.ttl_Width2, 0);
      this.Controls.SetChildIndex(this.ttl_ScoreMeasures2, 0);
      this.Controls.SetChildIndex(this.ttl_Score2, 0);
      this.Controls.SetChildIndex(this.ttl_Positioning2, 0);
      this.Controls.SetChildIndex(this.ttl_Gap2, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    private void gdv_Orders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.RowIndex >= PAST_SETUPS)
        return;
      DataRow dataRow = ((DataRowView)gdv_Orders.Rows[e.RowIndex].DataBoundItem).Row;
      if (e.Value != null)
      {
        if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_QUALITY_ID)
        {
          e.Value = e.Value.ToString().Trim();
        }
        if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_FLUTE)
        {
          string fluteID = "";

          //DataRow qualityRow = DBManager.getDBManager().quality.quality.Tables[Quality.QUALITY_LIST].Rows.Find(dataRow[Order.DEL_QUALITY_ID].ToString());
          DataRow qualityRow = DBManager.getDBManager().quality.getRow(dataRow[Order.DEL_QUALITY_ID].ToString());
          
          if (qualityRow != null)
            fluteID = qualityRow[Quality.FLUTE_ID].ToString();
          e.Value = fluteID;
        }
        if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_WEB_WIDTH)
        {
          try
          {
            e.Value = SyncroConverter.ConvertString(dataRow[PastOrder.POL_WEB_WIDTH].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + PastOrder.POL_WEB_WIDTH + " - Error converting \"" + e.Value.ToString(), ex);
          }
        }
        if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_TRIM_TYPE)
        {
          try
          {
            Type t = typeof(SyncroC.Utility.TrimType);
            object o = Enum.Parse(t, dataRow[PastOrder.POL_TRIM_TYPE].ToString());
            string trimType = DBManager.getDBManager().language.getTranslation(t.Name + "." + Enum.Format(t, o, "g"));
            e.Value = trimType;
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + PastOrder.POL_TRIM_TYPE + " - Error converting \"" + e.Value.ToString(), ex);
          }
        }
        if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_TRIM)
        {
          try
          {
            e.Value = SyncroConverter.ConvertString(dataRow[PastOrder.POL_TRIM].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + PastOrder.POL_TRIM + " - Error converting \"" + e.Value.ToString(), ex);
          }
        }
        if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_SHEET_LENGTH_0)
        {
          try
          {
            int cutCount = this.intParse(dataRow[PastOrder.POL_SEGMENT_SHEET_COUNT_0].ToString()) / this.intParse(dataRow[PastOrder.POL_OUTS_0].ToString());
            int setupLength = (int)Math.Round((double)SyncroConverter.Convert((decimal)(this.intParse(dataRow[PastOrder.POL_SHEET_LENGTH_0].ToString())), UnitMeasure.DeciMilliMetre, UnitMeasure.Metre) * (double)cutCount);
            e.Value = setupLength.ToString();
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + "Length" + " - Error converting \"" + e.Value.ToString(), ex);
          }
        }
        if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_DELIVERY_0)
        {
          string grpS = "";
          ShaftGroup grp = DataBase.LineConfig.SlitterScorer.GetShaftGroup(this.intParse(dataRow[PastOrder.POL_SHAFT_MASK].ToString()));
          if (grp == ShaftGroup.NoGroup)
            grpS = "?";
          else if ((grp == ShaftGroup.Group1) || (grp == ShaftGroup.Group2))
            grpS = ((int)grp).ToString();
          else if ((grp == ShaftGroup.Group1A) || (grp == ShaftGroup.Group2A))
            grpS = ((int)grp - 2).ToString() + "+A";
          else
            grpS = "TND";
          e.Value = grpS;
        }
        if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_SHAFT_MASK)
        {
          string shafts = "";
          for (Shaft sh = Shaft.Slit1; sh <= Shaft.ScoreAux; sh++)
          {
            if ((int.Parse(dataRow[PastOrder.POL_SHAFT_MASK].ToString()) & SyncroC.DataBase.LineConfig.SlitterScorer.BIT((int)sh)) > 0)
              shafts += SyncroC.DataBase.LineConfig.SlitterScorer.AbbreviateShaftName(Enum.Format(typeof(Shaft), sh, "g")) + " ";
          }
          e.Value = shafts;
        }
      }
    }

    /// <summary>
    /// Clean all controls for order details
    /// </summary>
    private void CleanOrderDetails()
    {
      //order 0
      ttl_Order0.Text = ttl_Level0.Text = ttl_Outs0.Text = ttl_Customer0.Text =
        //order 1 
      ttl_Order1.Text = ttl_Level1.Text = ttl_Outs1.Text = ttl_Customer1.Text =
        //order 2
      ttl_Order2.Text = ttl_Level2.Text = ttl_Outs2.Text = ttl_Customer2.Text =
      String.Empty;
    }

    protected override void CtrlF2()
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;
      if (this.online)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_41", true);
        return;
      }
      int restoreInd = this.gdv_Orders.SelectedRows[0].Index;
      DataRow dataRow = ((DataRowView)gdv_Orders.SelectedRows[0].DataBoundItem).Row;
      if ((this.gdv_Orders.SelectedRows.Count == 1) && (restoreInd < this.gdv_Orders.Rows.Count) && (restoreInd >= 0))
      {
        Frm_Confirm popUp = new Frm_Confirm();
        string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_22") + " ";
        if (popUp.ShowDialog(this, text) == DialogResult.OK)
        {
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_PAST_ORDER + "." + Command.DB_CMD_RESTORE + "(" +
                      dataRow[PastOrder.POL_SETUP_ID] + Command.PARAMETER_SEPARATOR +
                      SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeString((DateTime)dataRow[PastOrder.POL_BEGIN_TIME]) + ")");
        }
      }
      else
        PageManager.getPageManager()._SetMessage("§SYCC_16", true);
    }

    private void gdv_Orders_SelectionChanged(object sender, System.EventArgs e)
    {
      if (this.gdv_Orders.SelectedRows.Count == 1)
      {
        DataRow dataRow = ((DataRowView)gdv_Orders.SelectedRows[0].DataBoundItem).Row;
        this.selectedIndex=DBManager.getDBManager().pastOrder.getIndex(dataRow);
      }
      else
        this.selectedIndex = -1;
      this.FillOrders();
    }

    private void _SetOnline(bool online)
    {
      this.online = online && (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer() || DBManager.getDBManager().syncroConfig.IsTwin400Only());
    }

    private void gdv_Orders_Load(object sender, EventArgs e)
    {
      if (gdv_Orders.Rows.Count > 0)
      {
        this.gdv_Orders.Focus();
        gdv_Orders.Rows[0].Selected = true;
      }
    }

    protected override void ControlD()
    {
      base.ControlD();
      this.ToggleHiddenColums();
    }

    private void ToggleHiddenColums()
    {
      const int COLUMN_RED = 17;
      const int HIDDEN_COLUMNS = 3;
      if (this.HiddenCol)
      {
        this.bckFont = this.gdv_Orders.Font;
        this.gdv_Orders.Font = new Font("Arial", 8);
        for (int i = 0; i < this.gdv_Orders.Columns.Count; i++)
        {
          if (i >= this.gdv_Orders.Columns.Count - HIDDEN_COLUMNS)
            this.gdv_Orders.Columns[i].Visible = true;
          else
            this.gdv_Orders.Columns[i].Width -= COLUMN_RED;
        }
      }
      else
      {
        this.gdv_Orders.Font = this.bckFont;
        for (int i = 0; i < this.gdv_Orders.Columns.Count; i++)
        {
          if (i >= this.gdv_Orders.Columns.Count - HIDDEN_COLUMNS)
            this.gdv_Orders.Columns[i].Visible = false;
          else
            this.gdv_Orders.Columns[i].Width += COLUMN_RED;
        }
      }
      this.HiddenCol = !this.HiddenCol;
      this.CenterGrid();
      this.gdv_Orders.Invalidate();
    }

    private void CenterGrid()
    {
      int width = 0;
      for (int i = 0; i < this.gdv_Orders.Columns.Count; i++)
        if (this.gdv_Orders.Columns[i].Visible)
          width += this.gdv_Orders.Columns[i].Width;
      this.gdv_Orders.Width = width;
      this.gdv_Orders.Left = this.internalLeftHigh.X +
        (this.internalRightHigh.X - this.internalLeftHigh.X - this.gdv_Orders.Width) / 2;
    }

    private void gdv_Orders_DoubleClick(object sender, EventArgs e)
    {
      long count = DBManager.getDBManager().pastOrder.getCount();

      if (gdv_Orders.SelectedRows.Count == 1 &&
          count >= gdv_Orders.SelectedRows[0].Index)
      {
        Frm_twin400_orderDetails.externState = Frm_twin400_orderDetails.State.Read;
        Frm_twin400_orderDetails.externPast = true;
				//RFAZZI_FI31/07/2009_1. Corretto problema di visualizzazione dettaglio ordine eseguito da Pagine TW4.
				//DataRow dataRow = ((DataRowView)gdv_Orders.SelectedRows[0].DataBoundItem).Row;
				//Frm_twin400_ordersSubgroup.externIndex = DBManager.getDBManager().pastOrder.getIndex(dataRow);
				Frm_twin400_ordersSubgroup.externIndex = gdv_Orders.SelectedRows[0].Index;
      }
      askForPage(typeof(Frm_twin400_orderDetails));
    }
  }
}

