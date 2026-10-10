using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;

using SyncroC.DataBase;

namespace SyncroC.Forms.Pages.Debug
{
	public class Frm_DebugDatabase : SyncroC.Forms.Pages.Debug.Frm_DebugGroup
	{
		private SyncroC.Controls.Title ttl_Order;
		private Button btn_OrderLocalReload;
		private Button btn_OrderRemoteReload;
		private Button btn_OrderClear;
		private SyncroC.Controls.TitledLabel tlb_OrderCount;
		private SyncroC.Controls.TitledLabel tlb_PastOrderCount;
		private Button btn_PastOrderClear;
		private Button btn_PastOrderRemoteReload;
		private Button btn_PastOrderLocalReload;
		private SyncroC.Controls.Title ttl_PastOrder;
		private SyncroC.Controls.TitledLabel tlb_FluteCount;
		private Button btn_FluteClear;
		private Button btn_FluteRemoteReload;
		private Button btn_FluteLocalReload;
		private SyncroC.Controls.Title ttl_Flute;
		private SyncroC.Controls.TitledLabel tlb_PaperCount;
		private Button btn_PaperClear;
		private Button btn_PaperRemoteReload;
		private Button btn_PaperLocalReload;
		private SyncroC.Controls.Title ttl_Paper;
		private SyncroC.Controls.TitledLabel tlb_QualityCount;
		private Button btn_QualityClear;
		private Button btn_QualityRemoteReload;
		private Button btn_QualityLocalReload;
		private SyncroC.Controls.Title ttl_Quality;
		private Button btn_AllClear;
		private Button btn_AllRemoteReload;
		private Button btn_AllLocalReload;
		private SyncroC.Controls.Title ttl_All;
		private System.ComponentModel.IContainer components = null;
		private Button btn_AllSendVersions;
		private SyncroC.Controls.TitledLabel tlb_DowTimeCount;
		private Button btn_DownTimeClear;
		private Button btn_DownTimeRemoteReload;
		private Button btn_DownTimeLocalReload;
		private SyncroC.Controls.Title ttl_downtime;
		private SyncroC.Controls.TitledLabel tlb_ShiftCount;
		private Button btn_ShiftClear;
		private Button btn_ShiftRemoteReload;
		private Button btn_ShiftLocalReload;
		private SyncroC.Controls.Title ttl_Shift;
		private SyncroC.Controls.TitledLabel tlb_CutToMarkCount;
		private Button btn_CutToMarkClear;
		private Button btn_CutToMarkRemoteReload;
		private Button btn_CutToMarkLocalReload;
		private SyncroC.Controls.Title ttl_cutToMark;

		private DBManager dbm;

		private DataTable tableOrder;
		//private DataTable tablePastOrder;
		//private DataTable tableFlute;
		//private DataTable tablePaper;
		//private DataTable tableQuality;
		//private DataTable tableShift;
		private Button btn_AlarmClear;
		private Button btn_AlarmLocalReload;
		private SyncroC.Controls.Title ttl_Alarm;
		private SyncroC.Controls.Title ttl_DowntimeReasons;
		private Button btn_ForceReloadSettings;
		private Button btn_Alignment;
		private Button btn_PastRollLocalReload;
		private Button btn_PastRollRemoteReload;
		private Button btn_PastRollClear;
		private SyncroC.Controls.Title ttl_PastRoll;
		private SyncroC.Controls.TitledLabel tlb_PastRollCount;
		//private DataTable tableDownTime;
		//private DataTable tableCutToMark;
		private SyncroC.Controls.TitledLabel tlb_DBCommandCount;
		private Timer timer1;
		private SyncroC.Controls.Title ttl_CommandsQueuesMonitor;
		private SyncroC.Controls.TitledLabel tlb_PMCommandCount;
		private SyncroC.Controls.TitledLabel tlb_RTCCommandCount;
		//private DataTable tablePastRolls;

		public Frm_DebugDatabase()
		{
			InitializeComponent();

			dbm = DBManager.getDBManager();
			setCountLabels();

			tableOrder = dbm.order.order.Tables[Order.DRYEND_LIST];
			if (tableOrder != null)
			{
				tableOrder.RowChanged += new DataRowChangeEventHandler(setCountLabels);
				tableOrder.RowDeleted += new DataRowChangeEventHandler(setCountLabels);
				tableOrder.TableCleared += new DataTableClearEventHandler(setCountLabels);
			}
			
      //tablePastOrder = dbm.pastOrder.pastOrder.Tables[PastOrder.PAST_ORDER_LIST];
      //if (tablePastOrder != null)
      //{
      //  tablePastOrder.RowChanged += new DataRowChangeEventHandler(setCountLabels);
      //  tablePastOrder.RowDeleted += new DataRowChangeEventHandler(setCountLabels);
      //  tablePastOrder.TableCleared += new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.pastOrder.OnDBPastOrderChange += new PastOrder.DBChangedHandler(pastOrder_OnDBPastOrderChange);
			
      //tableFlute = dbm.flute.flute.Tables[Flute.FLUTE_LIST];
      //if (tableFlute != null)
      //{
      //  tableFlute.RowChanged += new DataRowChangeEventHandler(setCountLabels);
      //  tableFlute.RowDeleted += new DataRowChangeEventHandler(setCountLabels);
      //  tableFlute.TableCleared += new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.flute.OnDBFluteChange += new Flute.DBChangedHandler(flute_OnDBFluteChange);

      //tablePaper = dbm.paper.paper.Tables[Paper.PAPER_LIST];
      //if (tablePaper != null)
      //{
      //  tablePaper.RowChanged += new DataRowChangeEventHandler(setCountLabels);
      //  tablePaper.RowDeleted += new DataRowChangeEventHandler(setCountLabels);
      //  tablePaper.TableCleared += new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.paper.OnDBPaperChange += new Paper.DBChangedHandler(paper_OnDBPaperChange);

      //tableQuality = dbm.quality.quality.Tables[Quality.QUALITY_LIST];
      //if (tableQuality != null)
      //{
      //  tableQuality.RowChanged += new DataRowChangeEventHandler(setCountLabels);
      //  tableQuality.RowDeleted += new DataRowChangeEventHandler(setCountLabels);
      //  tableQuality.TableCleared += new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.quality.OnDBQualityChange += new Quality.DBChangedHandler(quality_OnDBQualityChange);
			
      //tableShift = dbm.shift.shift.Tables[Shift.SHIFT_LIST];
      //if (tableShift != null)
      //{
      //  tableShift.RowChanged += new DataRowChangeEventHandler(setCountLabels);
      //  tableShift.RowDeleted += new DataRowChangeEventHandler(setCountLabels);
      //  tableShift.TableCleared += new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.shift.OnDBShiftChange += new Shift.DBChangedHandler(shift_OnDBShiftChange);

      //tableDownTime = dbm.downTime.downTime.Tables[DownTime.DOWNTIME_LIST];
      //if (tableDownTime != null)
      //{
      //  tableDownTime.RowChanged += new DataRowChangeEventHandler(setCountLabels);
      //  tableDownTime.RowDeleted += new DataRowChangeEventHandler(setCountLabels);
      //  tableDownTime.TableCleared += new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.downTime.OnDBDowntimeChange += new DownTime.DBChangedHandler(downTime_OnDBDowntimeChange);

      //tableCutToMark = dbm.cutToPattern.cutToPattern.Tables[CutToPattern.CUTTOPATTERN_LIST];
      //if (tableCutToMark != null)
      //{
      //  tableCutToMark.RowChanged += new DataRowChangeEventHandler(setCountLabels);
      //  tableCutToMark.RowDeleted += new DataRowChangeEventHandler(setCountLabels);
      //  tableCutToMark.TableCleared += new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.cutToPattern.OnDBCutToPatternChange += new CutToPattern.DBChangedHandler(cutToPattern_OnDBCutToPatternChange);

      //tablePastRolls = dbm.pastRolls.pastRoll.Tables[PastRoll.PASTROLL_LIST];
      //if (tablePastRolls != null)
      //{
      //  tablePastRolls.RowChanged += new DataRowChangeEventHandler(setCountLabels);
      //  tablePastRolls.RowDeleted += new DataRowChangeEventHandler(setCountLabels);
      //  tablePastRolls.TableCleared += new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.pastRolls.OnDBPastRollChange += new PastRoll.DBChangedHandler(pastRolls_OnDBPastRollChange);
		}

    void shift_OnDBShiftChange(object sender, SyncroC.DataBase.EventArgs.DBChangedEventArgs args)
    {
      setCountLabels();
    }

    void downTime_OnDBDowntimeChange(object sender, SyncroC.DataBase.EventArgs.DBChangedEventArgs args)
    {
      setCountLabels();
    }

    void cutToPattern_OnDBCutToPatternChange(object sender, SyncroC.DataBase.EventArgs.DBChangedEventArgs args)
    {
      setCountLabels();
    }

    void pastRolls_OnDBPastRollChange(object sender, SyncroC.DataBase.EventArgs.DBChangedEventArgs args)
    {
      setCountLabels();
    }

    void paper_OnDBPaperChange(object sender, SyncroC.DataBase.EventArgs.DBChangedEventArgs args)
    {
      setCountLabels();
    }

    void flute_OnDBFluteChange(object sender, SyncroC.DataBase.EventArgs.DBChangedEventArgs args)
    {
      setCountLabels();
    }

    void pastOrder_OnDBPastOrderChange(object sender, SyncroC.DataBase.EventArgs.DBChangedEventArgs args)
    {
      setCountLabels();
    }

    void quality_OnDBQualityChange(object sender, SyncroC.DataBase.EventArgs.DBChangedEventArgs args)
    {
      setCountLabels();
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
			if (tableOrder != null)
			{
				tableOrder.RowChanged -= new DataRowChangeEventHandler(setCountLabels);
				tableOrder.RowDeleted -= new DataRowChangeEventHandler(setCountLabels);
				tableOrder.TableCleared -= new DataTableClearEventHandler(setCountLabels);
			}
			
      //if (tablePastOrder != null)
      //{
      //  tablePastOrder.RowChanged -= new DataRowChangeEventHandler(setCountLabels);
      //  tablePastOrder.RowDeleted -= new DataRowChangeEventHandler(setCountLabels);
      //  tablePastOrder.TableCleared -= new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.pastOrder.OnDBPastOrderChange -= new PastOrder.DBChangedHandler(pastOrder_OnDBPastOrderChange);

      //if (tableFlute != null)
      //{
      //  tableFlute.RowChanged -= new DataRowChangeEventHandler(setCountLabels);
      //  tableFlute.RowDeleted -= new DataRowChangeEventHandler(setCountLabels);
      //  tableFlute.TableCleared -= new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.flute.OnDBFluteChange -= new Flute.DBChangedHandler(flute_OnDBFluteChange);

      //if (tablePaper != null)
      //{
      //  tablePaper.RowChanged -= new DataRowChangeEventHandler(setCountLabels);
      //  tablePaper.RowDeleted -= new DataRowChangeEventHandler(setCountLabels);
      //  tablePaper.TableCleared -= new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.paper.OnDBPaperChange -= new Paper.DBChangedHandler(paper_OnDBPaperChange);

      //if (tableQuality != null)
      //{
      //  tableQuality.RowChanged -= new DataRowChangeEventHandler(setCountLabels);
      //  tableQuality.RowDeleted -= new DataRowChangeEventHandler(setCountLabels);
      //  tableQuality.TableCleared -= new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.quality.OnDBQualityChange -= new Quality.DBChangedHandler(quality_OnDBQualityChange);

      //if (tableShift != null)
      //{
      //  tableShift.RowChanged -= new DataRowChangeEventHandler(setCountLabels);
      //  tableShift.RowDeleted -= new DataRowChangeEventHandler(setCountLabels);
      //  tableShift.TableCleared -= new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.shift.OnDBShiftChange -= new Shift.DBChangedHandler(shift_OnDBShiftChange);

      //if (tableDownTime != null)
      //{
      //  tableDownTime.RowChanged -= new DataRowChangeEventHandler(setCountLabels);
      //  tableDownTime.RowDeleted -= new DataRowChangeEventHandler(setCountLabels);
      //  tableDownTime.TableCleared -= new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.downTime.OnDBDowntimeChange -= new DownTime.DBChangedHandler(downTime_OnDBDowntimeChange);

      //if (tableCutToMark != null)
      //{
      //  tableCutToMark.RowChanged -= new DataRowChangeEventHandler(setCountLabels);
      //  tableCutToMark.RowDeleted -= new DataRowChangeEventHandler(setCountLabels);
      //  tableCutToMark.TableCleared -= new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.cutToPattern.OnDBCutToPatternChange -= new CutToPattern.DBChangedHandler(cutToPattern_OnDBCutToPatternChange);

      //if (tablePastRolls != null)
      //{
      //  tablePastRolls.RowChanged -= new DataRowChangeEventHandler(setCountLabels);
      //  tablePastRolls.RowDeleted -= new DataRowChangeEventHandler(setCountLabels);
      //  tablePastRolls.TableCleared -= new DataTableClearEventHandler(setCountLabels);
      //}

      dbm.pastRolls.OnDBPastRollChange -= new PastRoll.DBChangedHandler(pastRolls_OnDBPastRollChange);
			
      base.Dispose( disposing );
		}

		protected override void F1()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DebugDatabase));
      this.ttl_Order = new SyncroC.Controls.Title();
      this.btn_OrderLocalReload = new System.Windows.Forms.Button();
      this.btn_OrderRemoteReload = new System.Windows.Forms.Button();
      this.btn_OrderClear = new System.Windows.Forms.Button();
      this.tlb_OrderCount = new SyncroC.Controls.TitledLabel();
      this.tlb_PastOrderCount = new SyncroC.Controls.TitledLabel();
      this.btn_PastOrderClear = new System.Windows.Forms.Button();
      this.btn_PastOrderRemoteReload = new System.Windows.Forms.Button();
      this.btn_PastOrderLocalReload = new System.Windows.Forms.Button();
      this.ttl_PastOrder = new SyncroC.Controls.Title();
      this.tlb_FluteCount = new SyncroC.Controls.TitledLabel();
      this.btn_FluteClear = new System.Windows.Forms.Button();
      this.btn_FluteRemoteReload = new System.Windows.Forms.Button();
      this.btn_FluteLocalReload = new System.Windows.Forms.Button();
      this.ttl_Flute = new SyncroC.Controls.Title();
      this.tlb_PaperCount = new SyncroC.Controls.TitledLabel();
      this.btn_PaperClear = new System.Windows.Forms.Button();
      this.btn_PaperRemoteReload = new System.Windows.Forms.Button();
      this.btn_PaperLocalReload = new System.Windows.Forms.Button();
      this.ttl_Paper = new SyncroC.Controls.Title();
      this.tlb_QualityCount = new SyncroC.Controls.TitledLabel();
      this.btn_QualityClear = new System.Windows.Forms.Button();
      this.btn_QualityRemoteReload = new System.Windows.Forms.Button();
      this.btn_QualityLocalReload = new System.Windows.Forms.Button();
      this.ttl_Quality = new SyncroC.Controls.Title();
      this.btn_AllClear = new System.Windows.Forms.Button();
      this.btn_AllRemoteReload = new System.Windows.Forms.Button();
      this.btn_AllLocalReload = new System.Windows.Forms.Button();
      this.ttl_All = new SyncroC.Controls.Title();
      this.btn_AllSendVersions = new System.Windows.Forms.Button();
      this.tlb_DowTimeCount = new SyncroC.Controls.TitledLabel();
      this.btn_DownTimeClear = new System.Windows.Forms.Button();
      this.btn_DownTimeRemoteReload = new System.Windows.Forms.Button();
      this.btn_DownTimeLocalReload = new System.Windows.Forms.Button();
      this.ttl_downtime = new SyncroC.Controls.Title();
      this.tlb_ShiftCount = new SyncroC.Controls.TitledLabel();
      this.btn_ShiftClear = new System.Windows.Forms.Button();
      this.btn_ShiftRemoteReload = new System.Windows.Forms.Button();
      this.btn_ShiftLocalReload = new System.Windows.Forms.Button();
      this.ttl_Shift = new SyncroC.Controls.Title();
      this.tlb_CutToMarkCount = new SyncroC.Controls.TitledLabel();
      this.btn_CutToMarkClear = new System.Windows.Forms.Button();
      this.btn_CutToMarkRemoteReload = new System.Windows.Forms.Button();
      this.btn_CutToMarkLocalReload = new System.Windows.Forms.Button();
      this.ttl_cutToMark = new SyncroC.Controls.Title();
      this.btn_AlarmClear = new System.Windows.Forms.Button();
      this.btn_AlarmLocalReload = new System.Windows.Forms.Button();
      this.ttl_Alarm = new SyncroC.Controls.Title();
      this.ttl_DowntimeReasons = new SyncroC.Controls.Title();
      this.btn_ForceReloadSettings = new System.Windows.Forms.Button();
      this.btn_Alignment = new System.Windows.Forms.Button();
      this.btn_PastRollLocalReload = new System.Windows.Forms.Button();
      this.btn_PastRollRemoteReload = new System.Windows.Forms.Button();
      this.btn_PastRollClear = new System.Windows.Forms.Button();
      this.ttl_PastRoll = new SyncroC.Controls.Title();
      this.tlb_PastRollCount = new SyncroC.Controls.TitledLabel();
      this.tlb_DBCommandCount = new SyncroC.Controls.TitledLabel();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.ttl_CommandsQueuesMonitor = new SyncroC.Controls.Title();
      this.tlb_PMCommandCount = new SyncroC.Controls.TitledLabel();
      this.tlb_RTCCommandCount = new SyncroC.Controls.TitledLabel();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_Title_YN.Text = "DATABASE DEBUG UTILITIES";
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
      // tab_Tab1
      // 
      this.tab_Tab1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab1.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // tab_TabA
      // 
      this.tab_TabA.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
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
      this.lbl_CtrlF1_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
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
      // ttl_Order
      // 
      this.ttl_Order.BackColor = System.Drawing.Color.Black;
      this.ttl_Order.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Order.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Order.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Order.Location = new System.Drawing.Point(347, 193);
      this.ttl_Order.Name = "ttl_Order";
      this.ttl_Order.Size = new System.Drawing.Size(193, 160);
      this.ttl_Order.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Order.TabIndex = 110;
      this.ttl_Order.Text = "Order";
      this.ttl_Order.TranslateText = false;
      // 
      // btn_OrderLocalReload
      // 
      this.btn_OrderLocalReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OrderLocalReload.FlatAppearance.BorderSize = 2;
      this.btn_OrderLocalReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OrderLocalReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OrderLocalReload.Location = new System.Drawing.Point(359, 225);
      this.btn_OrderLocalReload.Name = "btn_OrderLocalReload";
      this.btn_OrderLocalReload.Size = new System.Drawing.Size(86, 55);
      this.btn_OrderLocalReload.TabIndex = 111;
      this.btn_OrderLocalReload.Text = "LOCAL RELOAD";
      this.btn_OrderLocalReload.Click += new System.EventHandler(this.btn_OrderLocalReload_Click);
      // 
      // btn_OrderRemoteReload
      // 
      this.btn_OrderRemoteReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OrderRemoteReload.FlatAppearance.BorderSize = 2;
      this.btn_OrderRemoteReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OrderRemoteReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OrderRemoteReload.Location = new System.Drawing.Point(454, 225);
      this.btn_OrderRemoteReload.Name = "btn_OrderRemoteReload";
      this.btn_OrderRemoteReload.Size = new System.Drawing.Size(86, 55);
      this.btn_OrderRemoteReload.TabIndex = 112;
      this.btn_OrderRemoteReload.Text = "REMOTE RELOAD";
      this.btn_OrderRemoteReload.Click += new System.EventHandler(this.btn_OrderRemoteReload_Click);
      // 
      // btn_OrderClear
      // 
      this.btn_OrderClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_OrderClear.FlatAppearance.BorderSize = 2;
      this.btn_OrderClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_OrderClear.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OrderClear.Location = new System.Drawing.Point(359, 298);
      this.btn_OrderClear.Name = "btn_OrderClear";
      this.btn_OrderClear.Size = new System.Drawing.Size(86, 55);
      this.btn_OrderClear.TabIndex = 113;
      this.btn_OrderClear.Text = "CLEAR";
      this.btn_OrderClear.Click += new System.EventHandler(this.btn_OrderClear_Click);
      // 
      // tlb_OrderCount
      // 
      this.tlb_OrderCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_OrderCount.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_OrderCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_OrderCount.Location = new System.Drawing.Point(454, 298);
      this.tlb_OrderCount.Name = "tlb_OrderCount";
      this.tlb_OrderCount.Size = new System.Drawing.Size(86, 55);
      this.tlb_OrderCount.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_OrderCount.TabIndex = 114;
      this.tlb_OrderCount.Text = "Count";
      this.tlb_OrderCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_OrderCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_OrderCount.TranslateText = false;
      this.tlb_OrderCount.Value = "";
      this.tlb_OrderCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_OrderCount.ValueMultiline = true;
      // 
      // tlb_PastOrderCount
      // 
      this.tlb_PastOrderCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_PastOrderCount.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_PastOrderCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_PastOrderCount.Location = new System.Drawing.Point(667, 298);
      this.tlb_PastOrderCount.Name = "tlb_PastOrderCount";
      this.tlb_PastOrderCount.Size = new System.Drawing.Size(86, 55);
      this.tlb_PastOrderCount.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_PastOrderCount.TabIndex = 119;
      this.tlb_PastOrderCount.Text = "Count";
      this.tlb_PastOrderCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_PastOrderCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_PastOrderCount.TranslateText = false;
      this.tlb_PastOrderCount.Value = "";
      this.tlb_PastOrderCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_PastOrderCount.ValueMultiline = true;
      // 
      // btn_PastOrderClear
      // 
      this.btn_PastOrderClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_PastOrderClear.FlatAppearance.BorderSize = 2;
      this.btn_PastOrderClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_PastOrderClear.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_PastOrderClear.Location = new System.Drawing.Point(571, 298);
      this.btn_PastOrderClear.Name = "btn_PastOrderClear";
      this.btn_PastOrderClear.Size = new System.Drawing.Size(86, 55);
      this.btn_PastOrderClear.TabIndex = 118;
      this.btn_PastOrderClear.Text = "CLEAR";
      this.btn_PastOrderClear.Click += new System.EventHandler(this.btn_PastOrderClear_Click);
      // 
      // btn_PastOrderRemoteReload
      // 
      this.btn_PastOrderRemoteReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_PastOrderRemoteReload.FlatAppearance.BorderSize = 2;
      this.btn_PastOrderRemoteReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_PastOrderRemoteReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_PastOrderRemoteReload.Location = new System.Drawing.Point(667, 225);
      this.btn_PastOrderRemoteReload.Name = "btn_PastOrderRemoteReload";
      this.btn_PastOrderRemoteReload.Size = new System.Drawing.Size(86, 55);
      this.btn_PastOrderRemoteReload.TabIndex = 117;
      this.btn_PastOrderRemoteReload.Text = "REMOTE RELOAD";
      this.btn_PastOrderRemoteReload.Click += new System.EventHandler(this.btn_PastOrderRemoteReload_Click);
      // 
      // btn_PastOrderLocalReload
      // 
      this.btn_PastOrderLocalReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_PastOrderLocalReload.FlatAppearance.BorderSize = 2;
      this.btn_PastOrderLocalReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_PastOrderLocalReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_PastOrderLocalReload.Location = new System.Drawing.Point(571, 225);
      this.btn_PastOrderLocalReload.Name = "btn_PastOrderLocalReload";
      this.btn_PastOrderLocalReload.Size = new System.Drawing.Size(86, 55);
      this.btn_PastOrderLocalReload.TabIndex = 116;
      this.btn_PastOrderLocalReload.Text = "LOCAL RELOAD";
      this.btn_PastOrderLocalReload.Click += new System.EventHandler(this.btn_PastOrderLocalReload_Click);
      // 
      // ttl_PastOrder
      // 
      this.ttl_PastOrder.BackColor = System.Drawing.Color.Black;
      this.ttl_PastOrder.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PastOrder.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PastOrder.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_PastOrder.Location = new System.Drawing.Point(560, 193);
      this.ttl_PastOrder.Name = "ttl_PastOrder";
      this.ttl_PastOrder.Size = new System.Drawing.Size(193, 160);
      this.ttl_PastOrder.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_PastOrder.TabIndex = 115;
      this.ttl_PastOrder.Text = "PastOrder";
      this.ttl_PastOrder.TranslateText = false;
      // 
      // tlb_FluteCount
      // 
      this.tlb_FluteCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_FluteCount.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_FluteCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_FluteCount.Location = new System.Drawing.Point(454, 471);
      this.tlb_FluteCount.Name = "tlb_FluteCount";
      this.tlb_FluteCount.Size = new System.Drawing.Size(86, 55);
      this.tlb_FluteCount.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_FluteCount.TabIndex = 129;
      this.tlb_FluteCount.Text = "Count";
      this.tlb_FluteCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_FluteCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_FluteCount.TranslateText = false;
      this.tlb_FluteCount.Value = "";
      this.tlb_FluteCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_FluteCount.ValueMultiline = true;
      // 
      // btn_FluteClear
      // 
      this.btn_FluteClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_FluteClear.FlatAppearance.BorderSize = 2;
      this.btn_FluteClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_FluteClear.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_FluteClear.Location = new System.Drawing.Point(359, 471);
      this.btn_FluteClear.Name = "btn_FluteClear";
      this.btn_FluteClear.Size = new System.Drawing.Size(86, 55);
      this.btn_FluteClear.TabIndex = 128;
      this.btn_FluteClear.Text = "CLEAR";
      this.btn_FluteClear.Click += new System.EventHandler(this.btn_FluteClear_Click);
      // 
      // btn_FluteRemoteReload
      // 
      this.btn_FluteRemoteReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_FluteRemoteReload.FlatAppearance.BorderSize = 2;
      this.btn_FluteRemoteReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_FluteRemoteReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_FluteRemoteReload.Location = new System.Drawing.Point(454, 398);
      this.btn_FluteRemoteReload.Name = "btn_FluteRemoteReload";
      this.btn_FluteRemoteReload.Size = new System.Drawing.Size(86, 55);
      this.btn_FluteRemoteReload.TabIndex = 127;
      this.btn_FluteRemoteReload.Text = "REMOTE RELOAD";
      this.btn_FluteRemoteReload.Click += new System.EventHandler(this.btn_FluteRemoteReload_Click);
      // 
      // btn_FluteLocalReload
      // 
      this.btn_FluteLocalReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_FluteLocalReload.FlatAppearance.BorderSize = 2;
      this.btn_FluteLocalReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_FluteLocalReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_FluteLocalReload.Location = new System.Drawing.Point(359, 398);
      this.btn_FluteLocalReload.Name = "btn_FluteLocalReload";
      this.btn_FluteLocalReload.Size = new System.Drawing.Size(86, 55);
      this.btn_FluteLocalReload.TabIndex = 126;
      this.btn_FluteLocalReload.Text = "LOCAL RELOAD";
      this.btn_FluteLocalReload.Click += new System.EventHandler(this.btn_FluteLocalReload_Click);
      // 
      // ttl_Flute
      // 
      this.ttl_Flute.BackColor = System.Drawing.Color.Black;
      this.ttl_Flute.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Flute.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Flute.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Flute.Location = new System.Drawing.Point(347, 366);
      this.ttl_Flute.Name = "ttl_Flute";
      this.ttl_Flute.Size = new System.Drawing.Size(193, 160);
      this.ttl_Flute.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Flute.TabIndex = 125;
      this.ttl_Flute.Text = "Flute";
      this.ttl_Flute.TranslateText = false;
      // 
      // tlb_PaperCount
      // 
      this.tlb_PaperCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_PaperCount.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_PaperCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_PaperCount.Location = new System.Drawing.Point(667, 471);
      this.tlb_PaperCount.Name = "tlb_PaperCount";
      this.tlb_PaperCount.Size = new System.Drawing.Size(86, 55);
      this.tlb_PaperCount.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_PaperCount.TabIndex = 134;
      this.tlb_PaperCount.Text = "Count";
      this.tlb_PaperCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_PaperCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_PaperCount.TranslateText = false;
      this.tlb_PaperCount.Value = "";
      this.tlb_PaperCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_PaperCount.ValueMultiline = true;
      // 
      // btn_PaperClear
      // 
      this.btn_PaperClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_PaperClear.FlatAppearance.BorderSize = 2;
      this.btn_PaperClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_PaperClear.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_PaperClear.Location = new System.Drawing.Point(571, 471);
      this.btn_PaperClear.Name = "btn_PaperClear";
      this.btn_PaperClear.Size = new System.Drawing.Size(86, 55);
      this.btn_PaperClear.TabIndex = 133;
      this.btn_PaperClear.Text = "CLEAR";
      this.btn_PaperClear.Click += new System.EventHandler(this.btn_PaperClear_Click);
      // 
      // btn_PaperRemoteReload
      // 
      this.btn_PaperRemoteReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_PaperRemoteReload.FlatAppearance.BorderSize = 2;
      this.btn_PaperRemoteReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_PaperRemoteReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_PaperRemoteReload.Location = new System.Drawing.Point(667, 398);
      this.btn_PaperRemoteReload.Name = "btn_PaperRemoteReload";
      this.btn_PaperRemoteReload.Size = new System.Drawing.Size(86, 55);
      this.btn_PaperRemoteReload.TabIndex = 132;
      this.btn_PaperRemoteReload.Text = "REMOTE RELOAD";
      this.btn_PaperRemoteReload.Click += new System.EventHandler(this.btn_PaperRemoteReload_Click);
      // 
      // btn_PaperLocalReload
      // 
      this.btn_PaperLocalReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_PaperLocalReload.FlatAppearance.BorderSize = 2;
      this.btn_PaperLocalReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_PaperLocalReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_PaperLocalReload.Location = new System.Drawing.Point(571, 398);
      this.btn_PaperLocalReload.Name = "btn_PaperLocalReload";
      this.btn_PaperLocalReload.Size = new System.Drawing.Size(86, 55);
      this.btn_PaperLocalReload.TabIndex = 131;
      this.btn_PaperLocalReload.Text = "LOCAL RELOAD";
      this.btn_PaperLocalReload.Click += new System.EventHandler(this.btn_PaperLocalReload_Click);
      // 
      // ttl_Paper
      // 
      this.ttl_Paper.BackColor = System.Drawing.Color.Black;
      this.ttl_Paper.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Paper.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Paper.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Paper.Location = new System.Drawing.Point(560, 366);
      this.ttl_Paper.Name = "ttl_Paper";
      this.ttl_Paper.Size = new System.Drawing.Size(193, 160);
      this.ttl_Paper.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Paper.TabIndex = 130;
      this.ttl_Paper.Text = "Paper";
      this.ttl_Paper.TranslateText = false;
      // 
      // tlb_QualityCount
      // 
      this.tlb_QualityCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_QualityCount.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_QualityCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_QualityCount.Location = new System.Drawing.Point(880, 471);
      this.tlb_QualityCount.Name = "tlb_QualityCount";
      this.tlb_QualityCount.Size = new System.Drawing.Size(86, 55);
      this.tlb_QualityCount.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_QualityCount.TabIndex = 139;
      this.tlb_QualityCount.Text = "Count";
      this.tlb_QualityCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_QualityCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_QualityCount.TranslateText = false;
      this.tlb_QualityCount.Value = "";
      this.tlb_QualityCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_QualityCount.ValueMultiline = true;
      // 
      // btn_QualityClear
      // 
      this.btn_QualityClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_QualityClear.FlatAppearance.BorderSize = 2;
      this.btn_QualityClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_QualityClear.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_QualityClear.Location = new System.Drawing.Point(785, 471);
      this.btn_QualityClear.Name = "btn_QualityClear";
      this.btn_QualityClear.Size = new System.Drawing.Size(86, 55);
      this.btn_QualityClear.TabIndex = 138;
      this.btn_QualityClear.Text = "CLEAR";
      this.btn_QualityClear.Click += new System.EventHandler(this.btn_QualityClear_Click);
      // 
      // btn_QualityRemoteReload
      // 
      this.btn_QualityRemoteReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_QualityRemoteReload.FlatAppearance.BorderSize = 2;
      this.btn_QualityRemoteReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_QualityRemoteReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_QualityRemoteReload.Location = new System.Drawing.Point(880, 398);
      this.btn_QualityRemoteReload.Name = "btn_QualityRemoteReload";
      this.btn_QualityRemoteReload.Size = new System.Drawing.Size(86, 55);
      this.btn_QualityRemoteReload.TabIndex = 137;
      this.btn_QualityRemoteReload.Text = "REMOTE RELOAD";
      this.btn_QualityRemoteReload.Click += new System.EventHandler(this.btn_QualityRemoteReload_Click);
      // 
      // btn_QualityLocalReload
      // 
      this.btn_QualityLocalReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_QualityLocalReload.FlatAppearance.BorderSize = 2;
      this.btn_QualityLocalReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_QualityLocalReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_QualityLocalReload.Location = new System.Drawing.Point(785, 398);
      this.btn_QualityLocalReload.Name = "btn_QualityLocalReload";
      this.btn_QualityLocalReload.Size = new System.Drawing.Size(86, 55);
      this.btn_QualityLocalReload.TabIndex = 136;
      this.btn_QualityLocalReload.Text = "LOCAL RELOAD";
      this.btn_QualityLocalReload.Click += new System.EventHandler(this.btn_QualityLocalReload_Click);
      // 
      // ttl_Quality
      // 
      this.ttl_Quality.BackColor = System.Drawing.Color.Black;
      this.ttl_Quality.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Quality.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Quality.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Quality.Location = new System.Drawing.Point(773, 366);
      this.ttl_Quality.Name = "ttl_Quality";
      this.ttl_Quality.Size = new System.Drawing.Size(193, 160);
      this.ttl_Quality.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Quality.TabIndex = 135;
      this.ttl_Quality.Text = "Quality";
      this.ttl_Quality.TranslateText = false;
      // 
      // btn_AllClear
      // 
      this.btn_AllClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_AllClear.FlatAppearance.BorderSize = 2;
      this.btn_AllClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_AllClear.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_AllClear.Location = new System.Drawing.Point(150, 469);
      this.btn_AllClear.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.btn_AllClear.Name = "btn_AllClear";
      this.btn_AllClear.Size = new System.Drawing.Size(177, 70);
      this.btn_AllClear.TabIndex = 143;
      this.btn_AllClear.Text = "CLEAR";
      this.btn_AllClear.Click += new System.EventHandler(this.btn_AllClear_Click);
      // 
      // btn_AllRemoteReload
      // 
      this.btn_AllRemoteReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_AllRemoteReload.FlatAppearance.BorderSize = 2;
      this.btn_AllRemoteReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_AllRemoteReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_AllRemoteReload.Location = new System.Drawing.Point(150, 305);
      this.btn_AllRemoteReload.Name = "btn_AllRemoteReload";
      this.btn_AllRemoteReload.Size = new System.Drawing.Size(177, 70);
      this.btn_AllRemoteReload.TabIndex = 142;
      this.btn_AllRemoteReload.Text = "REMOTE RELOAD";
      this.btn_AllRemoteReload.Click += new System.EventHandler(this.btn_AllRemoteReload_Click);
      // 
      // btn_AllLocalReload
      // 
      this.btn_AllLocalReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_AllLocalReload.FlatAppearance.BorderSize = 2;
      this.btn_AllLocalReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_AllLocalReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_AllLocalReload.Location = new System.Drawing.Point(150, 225);
      this.btn_AllLocalReload.Name = "btn_AllLocalReload";
      this.btn_AllLocalReload.Size = new System.Drawing.Size(177, 70);
      this.btn_AllLocalReload.TabIndex = 141;
      this.btn_AllLocalReload.Text = "LOCAL RELOAD";
      this.btn_AllLocalReload.Click += new System.EventHandler(this.btn_AllLocalReload_Click);
      // 
      // ttl_All
      // 
      this.ttl_All.BackColor = System.Drawing.Color.Black;
      this.ttl_All.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_All.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_All.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_All.Location = new System.Drawing.Point(134, 193);
      this.ttl_All.Name = "ttl_All";
      this.ttl_All.Size = new System.Drawing.Size(193, 506);
      this.ttl_All.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_All.TabIndex = 140;
      this.ttl_All.Text = "All databases";
      this.ttl_All.TranslateText = false;
      // 
      // btn_AllSendVersions
      // 
      this.btn_AllSendVersions.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_AllSendVersions.FlatAppearance.BorderSize = 2;
      this.btn_AllSendVersions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_AllSendVersions.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_AllSendVersions.Location = new System.Drawing.Point(150, 386);
      this.btn_AllSendVersions.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.btn_AllSendVersions.Name = "btn_AllSendVersions";
      this.btn_AllSendVersions.Size = new System.Drawing.Size(177, 70);
      this.btn_AllSendVersions.TabIndex = 144;
      this.btn_AllSendVersions.Text = "SEND VERSIONS";
      this.btn_AllSendVersions.Click += new System.EventHandler(this.btn_AllSendVersions_Click);
      // 
      // tlb_DowTimeCount
      // 
      this.tlb_DowTimeCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_DowTimeCount.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_DowTimeCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_DowTimeCount.Location = new System.Drawing.Point(1093, 298);
      this.tlb_DowTimeCount.Name = "tlb_DowTimeCount";
      this.tlb_DowTimeCount.Size = new System.Drawing.Size(86, 55);
      this.tlb_DowTimeCount.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_DowTimeCount.TabIndex = 154;
      this.tlb_DowTimeCount.Text = "Count";
      this.tlb_DowTimeCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_DowTimeCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_DowTimeCount.TranslateText = false;
      this.tlb_DowTimeCount.Value = "";
      this.tlb_DowTimeCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_DowTimeCount.ValueMultiline = true;
      // 
      // btn_DownTimeClear
      // 
      this.btn_DownTimeClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_DownTimeClear.FlatAppearance.BorderSize = 2;
      this.btn_DownTimeClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_DownTimeClear.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_DownTimeClear.Location = new System.Drawing.Point(997, 298);
      this.btn_DownTimeClear.Name = "btn_DownTimeClear";
      this.btn_DownTimeClear.Size = new System.Drawing.Size(86, 55);
      this.btn_DownTimeClear.TabIndex = 153;
      this.btn_DownTimeClear.Text = "CLEAR";
      this.btn_DownTimeClear.Click += new System.EventHandler(this.btn_DownTimeClear_Click);
      // 
      // btn_DownTimeRemoteReload
      // 
      this.btn_DownTimeRemoteReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_DownTimeRemoteReload.FlatAppearance.BorderSize = 2;
      this.btn_DownTimeRemoteReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_DownTimeRemoteReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_DownTimeRemoteReload.Location = new System.Drawing.Point(1093, 225);
      this.btn_DownTimeRemoteReload.Name = "btn_DownTimeRemoteReload";
      this.btn_DownTimeRemoteReload.Size = new System.Drawing.Size(86, 55);
      this.btn_DownTimeRemoteReload.TabIndex = 152;
      this.btn_DownTimeRemoteReload.Text = "REMOTE RELOAD";
      this.btn_DownTimeRemoteReload.Click += new System.EventHandler(this.btn_DownTimeRemoteReload_Click);
      // 
      // btn_DownTimeLocalReload
      // 
      this.btn_DownTimeLocalReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_DownTimeLocalReload.FlatAppearance.BorderSize = 2;
      this.btn_DownTimeLocalReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_DownTimeLocalReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_DownTimeLocalReload.Location = new System.Drawing.Point(997, 225);
      this.btn_DownTimeLocalReload.Name = "btn_DownTimeLocalReload";
      this.btn_DownTimeLocalReload.Size = new System.Drawing.Size(86, 55);
      this.btn_DownTimeLocalReload.TabIndex = 151;
      this.btn_DownTimeLocalReload.Text = "LOCAL RELOAD";
      this.btn_DownTimeLocalReload.Click += new System.EventHandler(this.btn_DownTimeLocalReload_Click);
      // 
      // ttl_downtime
      // 
      this.ttl_downtime.BackColor = System.Drawing.Color.Black;
      this.ttl_downtime.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_downtime.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_downtime.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_downtime.Location = new System.Drawing.Point(986, 193);
      this.ttl_downtime.Name = "ttl_downtime";
      this.ttl_downtime.Size = new System.Drawing.Size(193, 160);
      this.ttl_downtime.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_downtime.TabIndex = 150;
      this.ttl_downtime.Text = "DownTime";
      this.ttl_downtime.TranslateText = false;
      // 
      // tlb_ShiftCount
      // 
      this.tlb_ShiftCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_ShiftCount.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_ShiftCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ShiftCount.Location = new System.Drawing.Point(880, 298);
      this.tlb_ShiftCount.Name = "tlb_ShiftCount";
      this.tlb_ShiftCount.Size = new System.Drawing.Size(86, 55);
      this.tlb_ShiftCount.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_ShiftCount.TabIndex = 149;
      this.tlb_ShiftCount.Text = "Count";
      this.tlb_ShiftCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ShiftCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_ShiftCount.TranslateText = false;
      this.tlb_ShiftCount.Value = "";
      this.tlb_ShiftCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_ShiftCount.ValueMultiline = true;
      // 
      // btn_ShiftClear
      // 
      this.btn_ShiftClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_ShiftClear.FlatAppearance.BorderSize = 2;
      this.btn_ShiftClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_ShiftClear.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_ShiftClear.Location = new System.Drawing.Point(785, 298);
      this.btn_ShiftClear.Name = "btn_ShiftClear";
      this.btn_ShiftClear.Size = new System.Drawing.Size(86, 55);
      this.btn_ShiftClear.TabIndex = 148;
      this.btn_ShiftClear.Text = "CLEAR";
      this.btn_ShiftClear.Click += new System.EventHandler(this.btn_ShiftClear_Click);
      // 
      // btn_ShiftRemoteReload
      // 
      this.btn_ShiftRemoteReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_ShiftRemoteReload.FlatAppearance.BorderSize = 2;
      this.btn_ShiftRemoteReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_ShiftRemoteReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_ShiftRemoteReload.Location = new System.Drawing.Point(880, 225);
      this.btn_ShiftRemoteReload.Name = "btn_ShiftRemoteReload";
      this.btn_ShiftRemoteReload.Size = new System.Drawing.Size(86, 55);
      this.btn_ShiftRemoteReload.TabIndex = 147;
      this.btn_ShiftRemoteReload.Text = "REMOTE RELOAD";
      this.btn_ShiftRemoteReload.Click += new System.EventHandler(this.btn_ShiftRemoteReload_Click);
      // 
      // btn_ShiftLocalReload
      // 
      this.btn_ShiftLocalReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_ShiftLocalReload.FlatAppearance.BorderSize = 2;
      this.btn_ShiftLocalReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_ShiftLocalReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_ShiftLocalReload.Location = new System.Drawing.Point(785, 225);
      this.btn_ShiftLocalReload.Name = "btn_ShiftLocalReload";
      this.btn_ShiftLocalReload.Size = new System.Drawing.Size(86, 55);
      this.btn_ShiftLocalReload.TabIndex = 146;
      this.btn_ShiftLocalReload.Text = "LOCAL RELOAD";
      this.btn_ShiftLocalReload.Click += new System.EventHandler(this.btn_ShiftLocalReload_Click);
      // 
      // ttl_Shift
      // 
      this.ttl_Shift.BackColor = System.Drawing.Color.Black;
      this.ttl_Shift.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Shift.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Shift.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Shift.Location = new System.Drawing.Point(773, 193);
      this.ttl_Shift.Name = "ttl_Shift";
      this.ttl_Shift.Size = new System.Drawing.Size(193, 160);
      this.ttl_Shift.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Shift.TabIndex = 145;
      this.ttl_Shift.Text = "Shift";
      this.ttl_Shift.TranslateText = false;
      // 
      // tlb_CutToMarkCount
      // 
      this.tlb_CutToMarkCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_CutToMarkCount.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_CutToMarkCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CutToMarkCount.Location = new System.Drawing.Point(454, 644);
      this.tlb_CutToMarkCount.Name = "tlb_CutToMarkCount";
      this.tlb_CutToMarkCount.Size = new System.Drawing.Size(86, 55);
      this.tlb_CutToMarkCount.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_CutToMarkCount.TabIndex = 159;
      this.tlb_CutToMarkCount.Text = "Count";
      this.tlb_CutToMarkCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CutToMarkCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_CutToMarkCount.TranslateText = false;
      this.tlb_CutToMarkCount.Value = "";
      this.tlb_CutToMarkCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_CutToMarkCount.ValueMultiline = true;
      // 
      // btn_CutToMarkClear
      // 
      this.btn_CutToMarkClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_CutToMarkClear.FlatAppearance.BorderSize = 2;
      this.btn_CutToMarkClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_CutToMarkClear.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_CutToMarkClear.ForeColor = System.Drawing.Color.Black;
      this.btn_CutToMarkClear.Location = new System.Drawing.Point(359, 644);
      this.btn_CutToMarkClear.Name = "btn_CutToMarkClear";
      this.btn_CutToMarkClear.Size = new System.Drawing.Size(86, 55);
      this.btn_CutToMarkClear.TabIndex = 158;
      this.btn_CutToMarkClear.Text = "CLEAR";
      this.btn_CutToMarkClear.Click += new System.EventHandler(this.btn_CutToMarkClear_Click);
      // 
      // btn_CutToMarkRemoteReload
      // 
      this.btn_CutToMarkRemoteReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_CutToMarkRemoteReload.FlatAppearance.BorderSize = 2;
      this.btn_CutToMarkRemoteReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_CutToMarkRemoteReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_CutToMarkRemoteReload.ForeColor = System.Drawing.Color.Black;
      this.btn_CutToMarkRemoteReload.Location = new System.Drawing.Point(454, 571);
      this.btn_CutToMarkRemoteReload.Name = "btn_CutToMarkRemoteReload";
      this.btn_CutToMarkRemoteReload.Size = new System.Drawing.Size(86, 55);
      this.btn_CutToMarkRemoteReload.TabIndex = 157;
      this.btn_CutToMarkRemoteReload.Text = "REMOTE RELOAD";
      this.btn_CutToMarkRemoteReload.Click += new System.EventHandler(this.btn_CutToMarkRemoteReload_Click);
      // 
      // btn_CutToMarkLocalReload
      // 
      this.btn_CutToMarkLocalReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_CutToMarkLocalReload.FlatAppearance.BorderSize = 2;
      this.btn_CutToMarkLocalReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_CutToMarkLocalReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_CutToMarkLocalReload.ForeColor = System.Drawing.Color.Black;
      this.btn_CutToMarkLocalReload.Location = new System.Drawing.Point(359, 571);
      this.btn_CutToMarkLocalReload.Name = "btn_CutToMarkLocalReload";
      this.btn_CutToMarkLocalReload.Size = new System.Drawing.Size(86, 55);
      this.btn_CutToMarkLocalReload.TabIndex = 156;
      this.btn_CutToMarkLocalReload.Text = "LOCAL RELOAD";
      this.btn_CutToMarkLocalReload.Click += new System.EventHandler(this.btn_CutToMarkLocalReload_Click);
      // 
      // ttl_cutToMark
      // 
      this.ttl_cutToMark.BackColor = System.Drawing.Color.Black;
      this.ttl_cutToMark.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_cutToMark.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_cutToMark.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_cutToMark.Location = new System.Drawing.Point(347, 539);
      this.ttl_cutToMark.Name = "ttl_cutToMark";
      this.ttl_cutToMark.Size = new System.Drawing.Size(193, 160);
      this.ttl_cutToMark.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_cutToMark.TabIndex = 155;
      this.ttl_cutToMark.Text = "CutToMark";
      this.ttl_cutToMark.TranslateText = false;
      // 
      // btn_AlarmClear
      // 
      this.btn_AlarmClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_AlarmClear.FlatAppearance.BorderSize = 2;
      this.btn_AlarmClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_AlarmClear.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_AlarmClear.ForeColor = System.Drawing.Color.Black;
      this.btn_AlarmClear.Location = new System.Drawing.Point(998, 471);
      this.btn_AlarmClear.Name = "btn_AlarmClear";
      this.btn_AlarmClear.Size = new System.Drawing.Size(86, 55);
      this.btn_AlarmClear.TabIndex = 123;
      this.btn_AlarmClear.Text = "CLEAR";
      this.btn_AlarmClear.Click += new System.EventHandler(this.btn_AlarmClear_Click);
      // 
      // btn_AlarmLocalReload
      // 
      this.btn_AlarmLocalReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_AlarmLocalReload.FlatAppearance.BorderSize = 2;
      this.btn_AlarmLocalReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_AlarmLocalReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_AlarmLocalReload.ForeColor = System.Drawing.Color.Black;
      this.btn_AlarmLocalReload.Location = new System.Drawing.Point(998, 398);
      this.btn_AlarmLocalReload.Name = "btn_AlarmLocalReload";
      this.btn_AlarmLocalReload.Size = new System.Drawing.Size(86, 55);
      this.btn_AlarmLocalReload.TabIndex = 121;
      this.btn_AlarmLocalReload.Text = "LOCAL RELOAD";
      this.btn_AlarmLocalReload.Click += new System.EventHandler(this.btn_AlarmLocalReload_Click);
      // 
      // ttl_Alarm
      // 
      this.ttl_Alarm.BackColor = System.Drawing.Color.Black;
      this.ttl_Alarm.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Alarm.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Alarm.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Alarm.Location = new System.Drawing.Point(986, 366);
      this.ttl_Alarm.Name = "ttl_Alarm";
      this.ttl_Alarm.Size = new System.Drawing.Size(193, 160);
      this.ttl_Alarm.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Alarm.TabIndex = 120;
      this.ttl_Alarm.Text = "AlarmHistory";
      this.ttl_Alarm.TranslateText = false;
      // 
      // ttl_DowntimeReasons
      // 
      this.ttl_DowntimeReasons.BackColor = System.Drawing.Color.Black;
      this.ttl_DowntimeReasons.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_DowntimeReasons.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DowntimeReasons.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_DowntimeReasons.Location = new System.Drawing.Point(773, 539);
      this.ttl_DowntimeReasons.Name = "ttl_DowntimeReasons";
      this.ttl_DowntimeReasons.Size = new System.Drawing.Size(123, 160);
      this.ttl_DowntimeReasons.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_DowntimeReasons.TabIndex = 160;
      this.ttl_DowntimeReasons.Text = "DowntimeReasons";
      this.ttl_DowntimeReasons.TranslateText = false;
      // 
      // btn_ForceReloadSettings
      // 
      this.btn_ForceReloadSettings.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_ForceReloadSettings.FlatAppearance.BorderSize = 2;
      this.btn_ForceReloadSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_ForceReloadSettings.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_ForceReloadSettings.ForeColor = System.Drawing.Color.Black;
      this.btn_ForceReloadSettings.Location = new System.Drawing.Point(150, 630);
      this.btn_ForceReloadSettings.Name = "btn_ForceReloadSettings";
      this.btn_ForceReloadSettings.Size = new System.Drawing.Size(177, 70);
      this.btn_ForceReloadSettings.TabIndex = 161;
      this.btn_ForceReloadSettings.Text = "FORCE RELOAD SETTINGS";
      this.btn_ForceReloadSettings.Click += new System.EventHandler(this.btn_SyncEveryClient_Click);
      // 
      // btn_Alignment
      // 
      this.btn_Alignment.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_Alignment.FlatAppearance.BorderSize = 2;
      this.btn_Alignment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_Alignment.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Alignment.Location = new System.Drawing.Point(150, 551);
      this.btn_Alignment.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.btn_Alignment.Name = "btn_Alignment";
      this.btn_Alignment.Size = new System.Drawing.Size(177, 70);
      this.btn_Alignment.TabIndex = 162;
      this.btn_Alignment.Text = "FORCE VERSIONS ALIGNMENT";
      this.btn_Alignment.Click += new System.EventHandler(this.btn_Alignment_Click);
      // 
      // btn_PastRollLocalReload
      // 
      this.btn_PastRollLocalReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_PastRollLocalReload.FlatAppearance.BorderSize = 2;
      this.btn_PastRollLocalReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_PastRollLocalReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_PastRollLocalReload.ForeColor = System.Drawing.Color.Black;
      this.btn_PastRollLocalReload.Location = new System.Drawing.Point(571, 571);
      this.btn_PastRollLocalReload.Name = "btn_PastRollLocalReload";
      this.btn_PastRollLocalReload.Size = new System.Drawing.Size(86, 55);
      this.btn_PastRollLocalReload.TabIndex = 163;
      this.btn_PastRollLocalReload.Text = "LOCAL RELOAD";
      this.btn_PastRollLocalReload.Click += new System.EventHandler(this.btn_PastRollLocalReload_Click);
      // 
      // btn_PastRollRemoteReload
      // 
      this.btn_PastRollRemoteReload.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_PastRollRemoteReload.FlatAppearance.BorderSize = 2;
      this.btn_PastRollRemoteReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_PastRollRemoteReload.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_PastRollRemoteReload.ForeColor = System.Drawing.Color.Black;
      this.btn_PastRollRemoteReload.Location = new System.Drawing.Point(667, 571);
      this.btn_PastRollRemoteReload.Name = "btn_PastRollRemoteReload";
      this.btn_PastRollRemoteReload.Size = new System.Drawing.Size(86, 55);
      this.btn_PastRollRemoteReload.TabIndex = 164;
      this.btn_PastRollRemoteReload.Text = "REMOTE RELOAD";
      this.btn_PastRollRemoteReload.Click += new System.EventHandler(this.btn_PastRollRemoteReload_Click);
      // 
      // btn_PastRollClear
      // 
      this.btn_PastRollClear.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_PastRollClear.FlatAppearance.BorderSize = 2;
      this.btn_PastRollClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_PastRollClear.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_PastRollClear.ForeColor = System.Drawing.Color.Black;
      this.btn_PastRollClear.Location = new System.Drawing.Point(571, 644);
      this.btn_PastRollClear.Name = "btn_PastRollClear";
      this.btn_PastRollClear.Size = new System.Drawing.Size(86, 55);
      this.btn_PastRollClear.TabIndex = 165;
      this.btn_PastRollClear.Text = "CLEAR";
      this.btn_PastRollClear.Click += new System.EventHandler(this.btn_PastRollClear_Click);
      // 
      // ttl_PastRoll
      // 
      this.ttl_PastRoll.BackColor = System.Drawing.Color.Black;
      this.ttl_PastRoll.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PastRoll.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PastRoll.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_PastRoll.Location = new System.Drawing.Point(560, 539);
      this.ttl_PastRoll.Name = "ttl_PastRoll";
      this.ttl_PastRoll.Size = new System.Drawing.Size(193, 160);
      this.ttl_PastRoll.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_PastRoll.TabIndex = 166;
      this.ttl_PastRoll.Text = "PastRoll";
      this.ttl_PastRoll.TranslateText = false;
      // 
      // tlb_PastRollCount
      // 
      this.tlb_PastRollCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_PastRollCount.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_PastRollCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_PastRollCount.Location = new System.Drawing.Point(667, 644);
      this.tlb_PastRollCount.Name = "tlb_PastRollCount";
      this.tlb_PastRollCount.Size = new System.Drawing.Size(86, 55);
      this.tlb_PastRollCount.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_PastRollCount.TabIndex = 167;
      this.tlb_PastRollCount.Text = "Count";
      this.tlb_PastRollCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_PastRollCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_PastRollCount.TranslateText = false;
      this.tlb_PastRollCount.Value = "";
      this.tlb_PastRollCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_PastRollCount.ValueMultiline = true;
      // 
      // tlb_DBCommandCount
      // 
      this.tlb_DBCommandCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_DBCommandCount.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_DBCommandCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_DBCommandCount.Location = new System.Drawing.Point(1000, 561);
      this.tlb_DBCommandCount.Name = "tlb_DBCommandCount";
      this.tlb_DBCommandCount.Size = new System.Drawing.Size(179, 50);
      this.tlb_DBCommandCount.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_DBCommandCount.TabIndex = 168;
      this.tlb_DBCommandCount.Text = "DB Command Count";
      this.tlb_DBCommandCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_DBCommandCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_DBCommandCount.TranslateText = false;
      this.tlb_DBCommandCount.Value = "";
      this.tlb_DBCommandCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_DBCommandCount.ValueMultiline = true;
      // 
      // timer1
      // 
      this.timer1.Enabled = true;
      this.timer1.Interval = 500;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // ttl_CommandsQueuesMonitor
      // 
      this.ttl_CommandsQueuesMonitor.BackColor = System.Drawing.Color.Black;
      this.ttl_CommandsQueuesMonitor.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CommandsQueuesMonitor.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CommandsQueuesMonitor.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_CommandsQueuesMonitor.Location = new System.Drawing.Point(986, 539);
      this.ttl_CommandsQueuesMonitor.Name = "ttl_CommandsQueuesMonitor";
      this.ttl_CommandsQueuesMonitor.Size = new System.Drawing.Size(193, 172);
      this.ttl_CommandsQueuesMonitor.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_CommandsQueuesMonitor.TabIndex = 169;
      this.ttl_CommandsQueuesMonitor.Text = "Commands Queue Monitor";
      this.ttl_CommandsQueuesMonitor.TranslateText = false;
      // 
      // tlb_PMCommandCount
      // 
      this.tlb_PMCommandCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_PMCommandCount.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_PMCommandCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_PMCommandCount.Location = new System.Drawing.Point(1000, 611);
      this.tlb_PMCommandCount.Name = "tlb_PMCommandCount";
      this.tlb_PMCommandCount.Size = new System.Drawing.Size(179, 50);
      this.tlb_PMCommandCount.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_PMCommandCount.TabIndex = 170;
      this.tlb_PMCommandCount.Text = "PM Command Count";
      this.tlb_PMCommandCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_PMCommandCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_PMCommandCount.TranslateText = false;
      this.tlb_PMCommandCount.Value = "";
      this.tlb_PMCommandCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_PMCommandCount.ValueMultiline = true;
      // 
      // tlb_RTCCommandCount
      // 
      this.tlb_RTCCommandCount.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_RTCCommandCount.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.tlb_RTCCommandCount.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_RTCCommandCount.Location = new System.Drawing.Point(1000, 661);
      this.tlb_RTCCommandCount.Name = "tlb_RTCCommandCount";
      this.tlb_RTCCommandCount.Size = new System.Drawing.Size(179, 50);
      this.tlb_RTCCommandCount.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.tlb_RTCCommandCount.TabIndex = 171;
      this.tlb_RTCCommandCount.Text = "RTC Command Count";
      this.tlb_RTCCommandCount.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_RTCCommandCount.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_RTCCommandCount.TranslateText = false;
      this.tlb_RTCCommandCount.Value = "";
      this.tlb_RTCCommandCount.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_RTCCommandCount.ValueMultiline = true;
      // 
      // Frm_DebugDatabase
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tlb_RTCCommandCount);
      this.Controls.Add(this.tlb_PMCommandCount);
      this.Controls.Add(this.ttl_CommandsQueuesMonitor);
      this.Controls.Add(this.tlb_DBCommandCount);
      this.Controls.Add(this.tlb_PastRollCount);
      this.Controls.Add(this.ttl_PastRoll);
      this.Controls.Add(this.btn_PastRollLocalReload);
      this.Controls.Add(this.btn_PastRollRemoteReload);
      this.Controls.Add(this.btn_PastRollClear);
      this.Controls.Add(this.btn_Alignment);
      this.Controls.Add(this.btn_ForceReloadSettings);
      this.Controls.Add(this.ttl_DowntimeReasons);
      this.Controls.Add(this.tlb_DowTimeCount);
      this.Controls.Add(this.btn_DownTimeClear);
      this.Controls.Add(this.btn_DownTimeRemoteReload);
      this.Controls.Add(this.btn_DownTimeLocalReload);
      this.Controls.Add(this.ttl_downtime);
      this.Controls.Add(this.tlb_ShiftCount);
      this.Controls.Add(this.btn_ShiftClear);
      this.Controls.Add(this.btn_ShiftRemoteReload);
      this.Controls.Add(this.btn_ShiftLocalReload);
      this.Controls.Add(this.ttl_Shift);
      this.Controls.Add(this.btn_AllSendVersions);
      this.Controls.Add(this.btn_AllClear);
      this.Controls.Add(this.btn_AllRemoteReload);
      this.Controls.Add(this.btn_AllLocalReload);
      this.Controls.Add(this.ttl_All);
      this.Controls.Add(this.tlb_QualityCount);
      this.Controls.Add(this.btn_QualityClear);
      this.Controls.Add(this.btn_QualityRemoteReload);
      this.Controls.Add(this.btn_QualityLocalReload);
      this.Controls.Add(this.ttl_Quality);
      this.Controls.Add(this.tlb_PaperCount);
      this.Controls.Add(this.btn_PaperClear);
      this.Controls.Add(this.btn_PaperRemoteReload);
      this.Controls.Add(this.btn_PaperLocalReload);
      this.Controls.Add(this.ttl_Paper);
      this.Controls.Add(this.tlb_FluteCount);
      this.Controls.Add(this.btn_FluteClear);
      this.Controls.Add(this.btn_FluteRemoteReload);
      this.Controls.Add(this.btn_FluteLocalReload);
      this.Controls.Add(this.ttl_Flute);
      this.Controls.Add(this.tlb_PastOrderCount);
      this.Controls.Add(this.btn_PastOrderClear);
      this.Controls.Add(this.btn_PastOrderRemoteReload);
      this.Controls.Add(this.btn_PastOrderLocalReload);
      this.Controls.Add(this.ttl_PastOrder);
      this.Controls.Add(this.tlb_OrderCount);
      this.Controls.Add(this.btn_OrderClear);
      this.Controls.Add(this.btn_OrderRemoteReload);
      this.Controls.Add(this.btn_OrderLocalReload);
      this.Controls.Add(this.ttl_Order);
      this.Controls.Add(this.ttl_cutToMark);
      this.Controls.Add(this.btn_CutToMarkLocalReload);
      this.Controls.Add(this.btn_CutToMarkRemoteReload);
      this.Controls.Add(this.btn_CutToMarkClear);
      this.Controls.Add(this.tlb_CutToMarkCount);
      this.Controls.Add(this.ttl_Alarm);
      this.Controls.Add(this.btn_AlarmLocalReload);
      this.Controls.Add(this.btn_AlarmClear);
      this.DesignGridHorizontalLines = 2;
      this.Name = "Frm_DebugDatabase";
      this.Text = "Frm_DebugDatabase";
      this.Load += new System.EventHandler(this.Frm_DebugDatabase_Load);
      this.Controls.SetChildIndex(this.btn_AlarmClear, 0);
      this.Controls.SetChildIndex(this.btn_AlarmLocalReload, 0);
      this.Controls.SetChildIndex(this.ttl_Alarm, 0);
      this.Controls.SetChildIndex(this.tlb_CutToMarkCount, 0);
      this.Controls.SetChildIndex(this.btn_CutToMarkClear, 0);
      this.Controls.SetChildIndex(this.btn_CutToMarkRemoteReload, 0);
      this.Controls.SetChildIndex(this.btn_CutToMarkLocalReload, 0);
      this.Controls.SetChildIndex(this.ttl_cutToMark, 0);
      this.Controls.SetChildIndex(this.ttl_Order, 0);
      this.Controls.SetChildIndex(this.btn_OrderLocalReload, 0);
      this.Controls.SetChildIndex(this.btn_OrderRemoteReload, 0);
      this.Controls.SetChildIndex(this.btn_OrderClear, 0);
      this.Controls.SetChildIndex(this.tlb_OrderCount, 0);
      this.Controls.SetChildIndex(this.ttl_PastOrder, 0);
      this.Controls.SetChildIndex(this.btn_PastOrderLocalReload, 0);
      this.Controls.SetChildIndex(this.btn_PastOrderRemoteReload, 0);
      this.Controls.SetChildIndex(this.btn_PastOrderClear, 0);
      this.Controls.SetChildIndex(this.tlb_PastOrderCount, 0);
      this.Controls.SetChildIndex(this.ttl_Flute, 0);
      this.Controls.SetChildIndex(this.btn_FluteLocalReload, 0);
      this.Controls.SetChildIndex(this.btn_FluteRemoteReload, 0);
      this.Controls.SetChildIndex(this.btn_FluteClear, 0);
      this.Controls.SetChildIndex(this.tlb_FluteCount, 0);
      this.Controls.SetChildIndex(this.ttl_Paper, 0);
      this.Controls.SetChildIndex(this.btn_PaperLocalReload, 0);
      this.Controls.SetChildIndex(this.btn_PaperRemoteReload, 0);
      this.Controls.SetChildIndex(this.btn_PaperClear, 0);
      this.Controls.SetChildIndex(this.tlb_PaperCount, 0);
      this.Controls.SetChildIndex(this.ttl_Quality, 0);
      this.Controls.SetChildIndex(this.btn_QualityLocalReload, 0);
      this.Controls.SetChildIndex(this.btn_QualityRemoteReload, 0);
      this.Controls.SetChildIndex(this.btn_QualityClear, 0);
      this.Controls.SetChildIndex(this.tlb_QualityCount, 0);
      this.Controls.SetChildIndex(this.ttl_All, 0);
      this.Controls.SetChildIndex(this.btn_AllLocalReload, 0);
      this.Controls.SetChildIndex(this.btn_AllRemoteReload, 0);
      this.Controls.SetChildIndex(this.btn_AllClear, 0);
      this.Controls.SetChildIndex(this.btn_AllSendVersions, 0);
      this.Controls.SetChildIndex(this.ttl_Shift, 0);
      this.Controls.SetChildIndex(this.btn_ShiftLocalReload, 0);
      this.Controls.SetChildIndex(this.btn_ShiftRemoteReload, 0);
      this.Controls.SetChildIndex(this.btn_ShiftClear, 0);
      this.Controls.SetChildIndex(this.tlb_ShiftCount, 0);
      this.Controls.SetChildIndex(this.ttl_downtime, 0);
      this.Controls.SetChildIndex(this.btn_DownTimeLocalReload, 0);
      this.Controls.SetChildIndex(this.btn_DownTimeRemoteReload, 0);
      this.Controls.SetChildIndex(this.btn_DownTimeClear, 0);
      this.Controls.SetChildIndex(this.tlb_DowTimeCount, 0);
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
      this.Controls.SetChildIndex(this.ttl_DowntimeReasons, 0);
      this.Controls.SetChildIndex(this.btn_ForceReloadSettings, 0);
      this.Controls.SetChildIndex(this.btn_Alignment, 0);
      this.Controls.SetChildIndex(this.btn_PastRollClear, 0);
      this.Controls.SetChildIndex(this.btn_PastRollRemoteReload, 0);
      this.Controls.SetChildIndex(this.btn_PastRollLocalReload, 0);
      this.Controls.SetChildIndex(this.ttl_PastRoll, 0);
      this.Controls.SetChildIndex(this.tlb_PastRollCount, 0);
      this.Controls.SetChildIndex(this.tlb_DBCommandCount, 0);
      this.Controls.SetChildIndex(this.ttl_CommandsQueuesMonitor, 0);
      this.Controls.SetChildIndex(this.tlb_PMCommandCount, 0);
      this.Controls.SetChildIndex(this.tlb_RTCCommandCount, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		private void setCountLabels(object sender, DataRowChangeEventArgs e)
		{
			setCountLabels();
		}

		private void setCountLabels(object sender, DataTableClearEventArgs e)
		{
			setCountLabels();
		}

		private void setCountLabels()
		{
			tlb_OrderCount.Value = dbm.order.order.Tables[Order.DRYEND_LIST].Rows.Count.ToString();
			
      //tlb_PastOrderCount.Value = dbm.pastOrder.pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Rows.Count.ToString();
      tlb_PastOrderCount.Value = dbm.pastOrder.getCount().ToString();
			
      //tlb_FluteCount.Value = dbm.flute.flute.Tables[Flute.FLUTE_LIST].Rows.Count.ToString();
      tlb_FluteCount.Value = dbm.flute.getCount().ToString();

			//tlb_PaperCount.Value = dbm.paper.paper.Tables[Paper.PAPER_LIST].Rows.Count.ToString();
      tlb_PaperCount.Value = dbm.paper.getCount().ToString();
			
      //tlb_QualityCount.Value = dbm.quality.quality.Tables[Quality.QUALITY_LIST].Rows.Count.ToString();
      tlb_QualityCount.Value = dbm.quality.getCount().ToString();
			
      //tlb_ShiftCount.Value = dbm.shift.shift.Tables[Shift.SHIFT_LIST].Rows.Count.ToString();
      tlb_ShiftCount.Value = dbm.shift.getCount().ToString();
			
      //tlb_DowTimeCount.Value = dbm.downTime.downTime.Tables[DownTime.DOWNTIME_LIST].Rows.Count.ToString();
      tlb_DowTimeCount.Value = dbm.downTime.getCount().ToString();
			
      //tlb_CutToMarkCount.Value = dbm.cutToPattern.cutToPattern.Tables[CutToPattern.CUTTOPATTERN_LIST].Rows.Count.ToString();
      tlb_CutToMarkCount.Value = dbm.cutToPattern.getCount().ToString();
			
      //tlb_PastRollCount.Value = dbm.pastRolls.pastRoll.Tables[PastRoll.PASTROLL_LIST].Rows.Count.ToString();
      tlb_PastRollCount.Value = dbm.pastRolls.getCount().ToString();
		}

		private void btn_AllLocalReload_Click(object sender, EventArgs e)
		{
			dbm.order.Load();
			dbm.pastOrder.Load();
			dbm.flute.Load();
			dbm.paper.Load();
			dbm.quality.Load();
			dbm.shift.Load();
			dbm.downTime.Load();
			dbm.cutToPattern.Load();
			dbm.pastRolls.Load();
		}

		private void btn_AllRemoteReload_Click(object sender, EventArgs e)
		{
			dbm.SendVersions(false, false, false, false, false, false, false, false, false, false);
		}

		private void btn_AllSendVersions_Click(object sender, EventArgs e)
		{
			dbm.SendVersions();
		}

		private void btn_AllClear_Click(object sender, EventArgs e)
		{
			dbm.order.Clr();
			dbm.pastOrder.Clr();
			dbm.flute.Clr();
			dbm.paper.Clr();
			dbm.quality.Clr();
			dbm.shift.Clr();
			dbm.downTime.Clr();
			dbm.cutToPattern.Clr();
			dbm.pastRolls.Clr();
		}

		//ORDER
		private void btn_OrderLocalReload_Click(object sender, EventArgs e)
		{
			dbm.order.Load();
		}

		private void btn_OrderRemoteReload_Click(object sender, EventArgs e)
		{
			dbm.SendVersions(false, true, true, true, true, true, true, true, true, false);
		}

		private void btn_OrderClear_Click(object sender, EventArgs e)
		{
			dbm.order.Clr();
		}

		//PAST ORDER
		private void btn_PastOrderLocalReload_Click(object sender, EventArgs e)
		{
			dbm.pastOrder.Load();
		}

		private void btn_PastOrderRemoteReload_Click(object sender, EventArgs e)
		{
			dbm.SendVersions(true, true, true, true, false, true, true, true, true, false);
		}

		private void btn_PastOrderClear_Click(object sender, EventArgs e)
		{
			dbm.pastOrder.Clr();
		}

		//ALARM
		private void btn_AlarmLocalReload_Click(object sender, EventArgs e)
		{
			dbm.alarmHistory.Load();
		}

		private void btn_AlarmClear_Click(object sender, EventArgs e)
		{
			dbm.alarmHistory.Clr();
		}

		//FLUTE
		private void btn_FluteLocalReload_Click(object sender, EventArgs e)
		{
			dbm.flute.Load();
		}

		private void btn_FluteRemoteReload_Click(object sender, EventArgs e)
		{
			dbm.SendVersions(true, false, true, true, true, true, true, true, true, false);
		}

		private void btn_FluteClear_Click(object sender, EventArgs e)
		{
			dbm.flute.Clr();
		}

		//PAPER
		private void btn_PaperLocalReload_Click(object sender, EventArgs e)
		{
			dbm.paper.Load();
		}

		private void btn_PaperRemoteReload_Click(object sender, EventArgs e)
		{
			dbm.SendVersions(true, true, false, true, true, true, true, true, true, false);
		}

		private void btn_PaperClear_Click(object sender, EventArgs e)
		{
			dbm.paper.Clr();
		}

		//QUALITY
		private void btn_QualityLocalReload_Click(object sender, EventArgs e)
		{
			dbm.quality.Load();
		}

		private void btn_QualityRemoteReload_Click(object sender, EventArgs e)
		{
			dbm.SendVersions(true, true, true, false, true, true, true, true, true, false);
		}

		private void btn_QualityClear_Click(object sender, EventArgs e)
		{
			dbm.quality.Clr();
		}

		private void btn_ShiftLocalReload_Click(object sender, EventArgs e)
		{
			dbm.shift.Load();
		}

		private void btn_DownTimeLocalReload_Click(object sender, EventArgs e)
		{
			dbm.downTime.Load();
		}

		private void btn_CutToMarkLocalReload_Click(object sender, EventArgs e)
		{
			dbm.cutToPattern.Load();
		}

		private void btn_PastRollLocalReload_Click(object sender, EventArgs e)
		{
			dbm.pastRolls.Load();
		}

		

		private void btn_ShiftRemoteReload_Click(object sender, EventArgs e)
		{
			dbm.SendVersions(true, true, true, true, true, false, true, true, true, false);
		}

		private void btn_DownTimeRemoteReload_Click(object sender, EventArgs e)
		{
			dbm.SendVersions(true, true, true, true, true, true, false, true, true, false);
		}

		private void btn_CutToMarkRemoteReload_Click(object sender, EventArgs e)
		{
			dbm.SendVersions(true, true, true, true, true, true, true, false, true, false);
		}

		private void btn_PastRollRemoteReload_Click(object sender, EventArgs e)
		{
			dbm.SendVersions(true, true, true, true, true, true, true, true, false, false);
		}

		

		private void btn_ShiftClear_Click(object sender, EventArgs e)
		{
			dbm.shift.Clr();
		}

		private void btn_DownTimeClear_Click(object sender, EventArgs e)
		{
			dbm.downTime.Clr();
		}

		private void btn_CutToMarkClear_Click(object sender, EventArgs e)
		{
			dbm.cutToPattern.Clr();
		}

		private void btn_PastRollClear_Click(object sender, EventArgs e)
		{
			dbm.pastRolls.Clr();
		}

		private void btn_SyncEveryClient_Click(object sender, EventArgs e)
		{
			DBManager.getDBManager().syncroConfig.SendRelodSettings();
		}

		private void Frm_DebugDatabase_Load(object sender, EventArgs e)
		{
			if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
			{
				ttl_DowntimeReasons.BackColor = Color.Black;
				btn_ForceReloadSettings.ForeColor = Color.Black;
				btn_ForceReloadSettings.Enabled = true;
        this.btn_ForceReloadSettings.Cursor = Cursors.Hand;
			}
			else
			{
				ttl_DowntimeReasons.BackColor = Color.LightGray;
				btn_ForceReloadSettings.ForeColor = Color.LightGray;
				btn_ForceReloadSettings.Enabled = false;
        this.btn_ForceReloadSettings.Cursor = Cursors.Arrow;
			}
		}

		private void btn_Alignment_Click(object sender, EventArgs e)
		{
			//send the Alignment command to the server in order to make all db versions saved
			dbm.SendAlignmentCommand(false);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			tlb_DBCommandCount.Value = SyncroC.Network.CommunicationManager.GetCommunicationManager().DBMcommandsQueue.Count.ToString();
			tlb_PMCommandCount.Value = SyncroC.Network.CommunicationManager.GetCommunicationManager().PMcommandsQueue.Count.ToString();
			tlb_RTCCommandCount.Value = SyncroC.Network.CommunicationManager.GetCommunicationManager().RTCMcommandsQueue.Count.ToString();
		}
	}
}

