using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Controls;
using SyncroC.DataBase.EventArgs;
using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Forms.Pages.Twin400.Tools;
using SyncroC.Controls.Colors;
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.Twin400.Orders
{
	public class Frm_twin400_orderDetails : SyncroC.Forms.Pages.Twin400.Orders.Frm_twin400_ordersSubgroup
	{
    const int NUM_ORDERS_IN_SETUP = 3;
    public static State externState;
    public static bool externPast;
    //score options ovveride symbols for tables
    //agrassi_FI28-07-08_1
    //private string POSITIONING_OVERRIDDEN = " 1 ";
    //private string SCORETYPE_OVERRIDDEN = " 2 ";
    //private string GAP_OVERRIDDEN = " 3 ";
    //private string TELETWINOFFSET_OVERRIDDEN = " 4 ";
    //private string AXIS_OVERRIDDEN = " 5 ";

		private Panel pnl_OrdersO;
		private SyncroC.Controls.TitledList cmb_Positioning0;
		private SyncroC.Controls.TitledList cmb_Type0;
		private SyncroC.Controls.TitledList cmb_Shaft0;
		private SyncroC.Controls.Title ttl_ScorerOptions0;
		private SyncroC.Controls.Title ttl_DefaultScorerOptions0;
		private SyncroC.Controls.TitledList cmb_Level0;
		private SyncroC.Controls.Title ttl_ord0;
		private SyncroC.Controls.TitledTextBox ttb_SuggMax;
		private SyncroC.Controls.TitledTextBox ttb_index;
		private SyncroC.Controls.TitledList cmb_TrimType;
		private SyncroC.Controls.TitledNumericUpDown nud_ScoreGapHeader;
		private SyncroC.Controls.TitledNumericUpDown nud_Trim;
		private SyncroC.Controls.TitledNumericUpDown nud_WebWidth;
		private SyncroC.Controls.TitledList cmb_Quality;
		private SyncroC.Controls.TitledTextBox ttb_SuggMin;
    private SyncroC.Controls.TitledTextBox ttb_SetUpSequence;
    private SyncroC.Controls.TitledList cmb_Flute;
		private SyncroC.Controls.GridView grv_ScorerOptions0;
		private SyncroC.Controls.TitledList cmb_DefaultType0;
		private SyncroC.Controls.TitledList cmb_DefaultPositioning0;
		private SyncroC.Controls.TitledNumericUpDown nud_Boxes0;
		private SyncroC.Controls.TitledNumericUpDown nud_DefaultScoreGap0;
		private SyncroC.Controls.TitledNumericUpDown nud_DefaultOffset0;
		private SyncroC.Controls.TitledNumericUpDown nud_Measure0;
		private SyncroC.Controls.TitledNumericUpDown nud_ScoreGap0;
    private SyncroC.Controls.TitledNumericUpDown nud_Offset0;
    private Panel pnl_Orders1;
		private SyncroC.Controls.TitledNumericUpDown nud_Offset1;
		private SyncroC.Controls.TitledNumericUpDown nud_ScoreGap1;
		private SyncroC.Controls.TitledNumericUpDown nud_Measure1;
		private SyncroC.Controls.TitledNumericUpDown nud_DefaultScoreGap1;
		private SyncroC.Controls.TitledNumericUpDown nud_DefaultOffset1;
		private SyncroC.Controls.TitledNumericUpDown nud_Boxes1;
		private SyncroC.Controls.TitledList cmb_DefaultType1;
		private SyncroC.Controls.TitledList cmb_DefaultPositioning1;
		private SyncroC.Controls.GridView grv_ScorerOptions1;
		private SyncroC.Controls.TitledList cmb_Positioning1;
		private SyncroC.Controls.TitledList cmb_Type1;
		private SyncroC.Controls.TitledList cmb_Shaft1;
		private SyncroC.Controls.Title ttl_ScorerOptions1;
		private SyncroC.Controls.Title ttl_DefaultScorerOptions1;
		private SyncroC.Controls.TitledList cmb_Level1;
		private SyncroC.Controls.Title ttl_ord1;
		private Label ModId;
		private Label SetupId;
		private Label PageState;
		private TitledNumericUpDown nud_Width0;
    private TitledNumericUpDown nud_Width1;
    private SyncroC.Controls.GridView grv_ActiveScorerOptions;
    private System.ComponentModel.IContainer components = null;

    //Controls' array
    private Title[] ttl_ord;
    private TitledTextBox[] ttb_OrderNumber;
    private TitledTextBox[] ttb_CustomerName;
    private TitledList[] cmb_Level;
    private TitledNumericUpDown[] nud_Boxes;
    private TitledNumericUpDown[] nud_Width;
    private TitledList[] cmb_DefaultPositioning;
    private TitledList[] cmb_DefaultType;
    private TitledNumericUpDown[] nud_DefaultScoreGap;
    private TitledNumericUpDown[] nud_DefaultOffset;
    private TitledNumericUpDown[] nud_Measure;
    private TitledList[] cmb_Positioning;
    private TitledList[] cmb_Type;
    private TitledNumericUpDown[] nud_ScoreGap;
    private TitledNumericUpDown[] nud_Offset;
    private TitledList[] cmb_Shaft;
    private GridView[] grv_ScorerOptions;
    private DataGridViewColumn[] clm_order_measures;
    //agrassi_FI28-07-08_1
    //private DataGridViewColumn[] clm_order_options;
    private DataGridViewImageColumn[] clm_order_optionPositioning, clm_order_optionType, clm_order_optionGap, clm_order_optionShaft;


		private DBManager dbm;

		//page status
		public enum State { Insert, Modify, Read };
		private State state, nextState;

		//useful DataTable
		//private DataTable quality;
		//private DataTable flute;
    private DataTable order;
   // private DataTable pastOrder;

		/// <summary>Index of the order currently showed on the screen.</summary>
    private int indexOrderToShow = 0;
    private TitledTextBox ttb_ProgramNumber;
    private Label ActiveCtrl;
    private Timer tim_debug;
    private Panel pnl_Orders2;
    private TitledNumericUpDown nud_Width2;
    private TitledNumericUpDown nud_Offset2;
    private TitledNumericUpDown nud_ScoreGap2;
    private TitledNumericUpDown nud_DefaultScoreGap2;
    private TitledNumericUpDown nud_DefaultOffset2;
    private TitledNumericUpDown nud_Boxes2;
    private TitledList cmb_DefaultType2;
    private TitledList cmb_DefaultPositioning2;
    private TitledList cmb_Level2;
    private Title ttl_ord2;
    private TitledList cmb_Shaft2;
    private TitledList cmb_Type2;
    private TitledNumericUpDown nud_Measure2;
    private Title ttl_ScorerOptions2;
    private Title ttl_DefaultScorerOptions2;
    private GridView grv_ScorerOptions2;
    private TitledList cmb_Positioning2;
    private Label NextPageState;
    /// <summary>Parsed data measures of the current showed order.</summary>
    private SyncroC.DataBase.LineConfig.SlitterScorer ss;
    private TitledNumericUpDown nud_SetupLength;
    private ArrayList[] measures;
    private TitledTextBox ttb_OrderNumber0;
    private TitledTextBox ttb_CustomerName0;
    private TitledTextBox ttb_CustomerName1;
    private TitledTextBox ttb_OrderNumber1;
    private TitledTextBox ttb_CustomerName2;
    private TitledTextBox ttb_OrderNumber2;
    private bool online = false, currentUnchangeable = false;
    private int oldScoreGapHeader;
    private int[] oldDefaultScoreGap;
    private char[] oldDefaultScoreType;
    private PositType[] oldDefaultPositType;
    private Label OfficeId;
    private Label lab_setupModId;
    private Label lab_officeId;

		private bool orderAddRequested = false;

    public Frm_twin400_orderDetails()
		{
			InitializeComponent();

      this.measures = new ArrayList[3];
      dbm = DBManager.getDBManager();
      this.ss = dbm.lineConfig.SlitterScorerConfig;
      
      //this.quality = dbm.quality.quality.Tables[Quality.QUALITY_LIST];
      
      //this.flute = dbm.flute.flute.Tables[Flute.FLUTE_LIST];
      this.order = dbm.order.order.Tables[Order.DRYEND_LIST];
      //this.pastOrder = dbm.pastOrder.pastOrder.Tables[PastOrder.PAST_ORDER_LIST];

      this.orderDataSet = DBManager.getDBManager().order.order;
      this.ordType = typeof(DataBase.Order);
      this.ordInst = (DataBase.Order)new DataBase.Order();
      this.orderDbName = Order.DRYEND_LIST;
      this.orderDbAcronym = "DEL";

      this.oldDefaultScoreGap=new int[NUM_ORDERS_IN_SETUP];
      this.oldDefaultScoreType = new char[NUM_ORDERS_IN_SETUP];
      this.oldDefaultPositType = new PositType[NUM_ORDERS_IN_SETUP];
      this.ttl_ord = new Title[NUM_ORDERS_IN_SETUP];
      this.ttb_OrderNumber = new TitledTextBox[NUM_ORDERS_IN_SETUP];
      this.ttb_CustomerName = new TitledTextBox[NUM_ORDERS_IN_SETUP];
      this.cmb_Level = new TitledList[NUM_ORDERS_IN_SETUP];
      this.cmb_DefaultPositioning = new TitledList[NUM_ORDERS_IN_SETUP];
      this.cmb_DefaultType = new TitledList[NUM_ORDERS_IN_SETUP];
      this.cmb_Positioning = new TitledList[NUM_ORDERS_IN_SETUP];
      this.cmb_Type = new TitledList[NUM_ORDERS_IN_SETUP];
      this.cmb_Shaft = new TitledList[NUM_ORDERS_IN_SETUP];
      this.nud_Boxes = new TitledNumericUpDown[NUM_ORDERS_IN_SETUP];
      this.nud_Width = new TitledNumericUpDown[NUM_ORDERS_IN_SETUP];
      this.nud_DefaultScoreGap = new TitledNumericUpDown[NUM_ORDERS_IN_SETUP];
      this.nud_DefaultOffset = new TitledNumericUpDown[NUM_ORDERS_IN_SETUP];
      this.nud_Measure = new TitledNumericUpDown[NUM_ORDERS_IN_SETUP];
      this.nud_ScoreGap = new TitledNumericUpDown[NUM_ORDERS_IN_SETUP];
      this.nud_Offset = new TitledNumericUpDown[NUM_ORDERS_IN_SETUP];
      this.grv_ScorerOptions = new GridView[NUM_ORDERS_IN_SETUP];
      this.clm_order_measures = new DataGridViewColumn[NUM_ORDERS_IN_SETUP];
      //agrassi_FI28-07-08_1
      //this.clm_order_options = new DataGridViewColumn[NUM_ORDERS_IN_SETUP];
      this.clm_order_optionGap = new DataGridViewImageColumn[NUM_ORDERS_IN_SETUP];
      this.clm_order_optionPositioning = new DataGridViewImageColumn[NUM_ORDERS_IN_SETUP];
      this.clm_order_optionShaft = new DataGridViewImageColumn[NUM_ORDERS_IN_SETUP];
      this.clm_order_optionType = new DataGridViewImageColumn[NUM_ORDERS_IN_SETUP];

      Type t = this.GetType();
      for (int i = 0; i < NUM_ORDERS_IN_SETUP; i++)
      {
        this.ttl_ord[i] = (Title)t.GetField("ttl_ord" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.ttb_OrderNumber[i] = (TitledTextBox)t.GetField("ttb_OrderNumber" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.ttb_CustomerName[i] = (TitledTextBox)t.GetField("ttb_CustomerName" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.cmb_Level[i] = (TitledList)t.GetField("cmb_Level" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.cmb_DefaultPositioning[i] = (TitledList)t.GetField("cmb_DefaultPositioning" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.cmb_DefaultType[i] = (TitledList)t.GetField("cmb_DefaultType" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.cmb_Positioning[i] = (TitledList)t.GetField("cmb_Positioning" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.cmb_Type[i] = (TitledList)t.GetField("cmb_Type" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.cmb_Shaft[i] = (TitledList)t.GetField("cmb_Shaft" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.nud_Boxes[i] = (TitledNumericUpDown)t.GetField("nud_Boxes" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.nud_Width[i] = (TitledNumericUpDown)t.GetField("nud_Width" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.nud_DefaultScoreGap[i] = (TitledNumericUpDown)t.GetField("nud_DefaultScoreGap" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.nud_DefaultOffset[i] = (TitledNumericUpDown)t.GetField("nud_DefaultOffset" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.nud_Measure[i] = (TitledNumericUpDown)t.GetField("nud_Measure" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.nud_ScoreGap[i] = (TitledNumericUpDown)t.GetField("nud_ScoreGap" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.nud_Offset[i] = (TitledNumericUpDown)t.GetField("nud_Offset" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
        this.grv_ScorerOptions[i] = (GridView)t.GetField("grv_ScorerOptions" + i.ToString(), BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
      }

			//grv_ScorerOptions
      for (int i = 0; i < NUM_ORDERS_IN_SETUP; i++)
      {
        clm_order_measures[i] = new DataGridViewColumn(new DataGridViewTextBoxCell());
        clm_order_measures[i].HeaderText = "Measures";
        clm_order_measures[i].Width = 80;
        clm_order_measures[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //agrassi_FI28-07-08_1
        //clm_order_options[i] = new DataGridViewColumn(new DataGridViewTextBoxCell());
        //clm_order_options[i].HeaderText = "Options";
        //clm_order_options[i].Width = 150;
        //clm_order_options[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        clm_order_optionGap[i] = new DataGridViewImageColumn();
        clm_order_optionGap[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        clm_order_optionGap[i].ImageLayout = DataGridViewImageCellLayout.Stretch;
        clm_order_optionGap[i].DefaultCellStyle.NullValue = null;
        clm_order_optionGap[i].Width = 38;

        clm_order_optionPositioning[i] = new DataGridViewImageColumn();
        clm_order_optionPositioning[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        clm_order_optionPositioning[i].ImageLayout = DataGridViewImageCellLayout.Stretch;
        clm_order_optionPositioning[i].DefaultCellStyle.NullValue = null;
        clm_order_optionPositioning[i].Width = 38;

        clm_order_optionShaft[i] = new DataGridViewImageColumn();
        clm_order_optionShaft[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        clm_order_optionShaft[i].ImageLayout = DataGridViewImageCellLayout.Stretch;
        clm_order_optionShaft[i].DefaultCellStyle.NullValue = null;
        clm_order_optionShaft[i].Width = 38;

        clm_order_optionType[i] = new DataGridViewImageColumn();
        clm_order_optionType[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        clm_order_optionType[i].ImageLayout = DataGridViewImageCellLayout.Stretch;
        clm_order_optionType[i].DefaultCellStyle.NullValue = null;
        clm_order_optionType[i].Width = 38;

        grv_ScorerOptions[i].Columns.Add(clm_order_measures[i]);
        //agrassi_FI28-07-08_1
        //grv_ScorerOptions[i].Columns.Add(clm_order_options[i]);
        grv_ScorerOptions[i].Columns.Add(clm_order_optionGap[i]);
        grv_ScorerOptions[i].Columns.Add(clm_order_optionPositioning[i]);
        grv_ScorerOptions[i].Columns.Add(clm_order_optionShaft[i]);
        grv_ScorerOptions[i].Columns.Add(clm_order_optionType[i]);
      }
      
      //Header
      setListFluteItems(-1, true);
      setListQualityItems(-1, null, true);
      setTitledListItems(cmb_TrimType, typeof(SyncroC.Utility.TrimType), new bool[] { true, true }, -1, true);

      //Orders
      int levelCount = DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount;
      for (int i = 0; i < NUM_ORDERS_IN_SETUP; i++)
      {
        setTitledListItems(cmb_Level[i], typeof(SyncroC.Utility.LevelName), new bool[] { false, true, (levelCount == 3), (levelCount >= 2) }, -1, true);
        setListPositioningItems(cmb_DefaultPositioning[i], -1, true, false);
        setListScorerItems(cmb_DefaultType[i], null, -1, true);
        setListPositioningItems(cmb_Positioning[i], -1, true, true);
        setListScorerItems(cmb_Type[i], null, -1, true);
        setTitledListItems(cmb_Shaft[i], typeof(SyncroC.Utility.ScorerShaft), new bool[] { true, true, true, true }, -1, true);
      }

      this.grv_ActiveScorerOptions = this.grv_ScorerOptions0;
      this.tim_debug.Enabled = true;
      this.PageState.Text = "Page status: Read";
      this.NextPageState.Text = "Next status: Read";
      this.state = State.Read;
      this.nextState = State.Read;

			order.RowChanged += new DataRowChangeEventHandler(order_RowChanged);
			order.RowDeleted += new DataRowChangeEventHandler(order_RowChanged);
			dbm.OrderValidationSucceded += new EventHandler(OrderValidationSucceded);
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

		private void OrderValidationSucceded(Object sender, System.EventArgs e)
		{
			if (!state.Equals(State.Read))
			{
				SetPageState(State.Read, false);
			}
		}

    //agrassi_FI10-12-2010_1
    /// <summary>
		/// The page has to be kept into cache when insert or modify
		/// </summary>
    //public override bool IsRemovableFromCache()
    //{
    //  return this.state == State.Read;
    //}

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
			order.RowChanged -= new DataRowChangeEventHandler(order_RowChanged);
			order.RowDeleted -= new DataRowChangeEventHandler(order_RowChanged);
			dbm.OrderValidationSucceded -= new EventHandler(OrderValidationSucceded);
			base.Dispose( disposing );
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_orderDetails));
      this.pnl_OrdersO = new System.Windows.Forms.Panel();
      this.ttb_CustomerName0 = new SyncroC.Controls.TitledTextBox();
      this.ttb_OrderNumber0 = new SyncroC.Controls.TitledTextBox();
      this.ttl_ord0 = new SyncroC.Controls.Title();
      this.nud_Width0 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Offset0 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Measure0 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_ScoreGap0 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_DefaultScoreGap0 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_DefaultOffset0 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Boxes0 = new SyncroC.Controls.TitledNumericUpDown();
      this.cmb_DefaultType0 = new SyncroC.Controls.TitledList();
      this.cmb_DefaultPositioning0 = new SyncroC.Controls.TitledList();
      this.grv_ScorerOptions0 = new SyncroC.Controls.GridView();
      this.cmb_Positioning0 = new SyncroC.Controls.TitledList();
      this.cmb_Type0 = new SyncroC.Controls.TitledList();
      this.ttl_ScorerOptions0 = new SyncroC.Controls.Title();
      this.ttl_DefaultScorerOptions0 = new SyncroC.Controls.Title();
      this.cmb_Level0 = new SyncroC.Controls.TitledList();
      this.cmb_Shaft0 = new SyncroC.Controls.TitledList();
      this.ttb_SuggMax = new SyncroC.Controls.TitledTextBox();
      this.ttb_index = new SyncroC.Controls.TitledTextBox();
      this.cmb_TrimType = new SyncroC.Controls.TitledList();
      this.nud_ScoreGapHeader = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Trim = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_WebWidth = new SyncroC.Controls.TitledNumericUpDown();
      this.cmb_Quality = new SyncroC.Controls.TitledList();
      this.ttb_SuggMin = new SyncroC.Controls.TitledTextBox();
      this.ttb_SetUpSequence = new SyncroC.Controls.TitledTextBox();
      this.cmb_Flute = new SyncroC.Controls.TitledList();
      this.pnl_Orders1 = new System.Windows.Forms.Panel();
      this.ttb_CustomerName1 = new SyncroC.Controls.TitledTextBox();
      this.ttb_OrderNumber1 = new SyncroC.Controls.TitledTextBox();
      this.nud_Width1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Offset1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_ScoreGap1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Measure1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_DefaultScoreGap1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_DefaultOffset1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Boxes1 = new SyncroC.Controls.TitledNumericUpDown();
      this.cmb_DefaultType1 = new SyncroC.Controls.TitledList();
      this.cmb_DefaultPositioning1 = new SyncroC.Controls.TitledList();
      this.grv_ScorerOptions1 = new SyncroC.Controls.GridView();
      this.cmb_Positioning1 = new SyncroC.Controls.TitledList();
      this.cmb_Type1 = new SyncroC.Controls.TitledList();
      this.cmb_Shaft1 = new SyncroC.Controls.TitledList();
      this.ttl_ScorerOptions1 = new SyncroC.Controls.Title();
      this.ttl_DefaultScorerOptions1 = new SyncroC.Controls.Title();
      this.cmb_Level1 = new SyncroC.Controls.TitledList();
      this.ttl_ord1 = new SyncroC.Controls.Title();
      this.ModId = new System.Windows.Forms.Label();
      this.SetupId = new System.Windows.Forms.Label();
      this.PageState = new System.Windows.Forms.Label();
      this.ttb_ProgramNumber = new SyncroC.Controls.TitledTextBox();
      this.ActiveCtrl = new System.Windows.Forms.Label();
      this.tim_debug = new System.Windows.Forms.Timer(this.components);
      this.pnl_Orders2 = new System.Windows.Forms.Panel();
      this.ttb_CustomerName2 = new SyncroC.Controls.TitledTextBox();
      this.ttb_OrderNumber2 = new SyncroC.Controls.TitledTextBox();
      this.nud_Width2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Offset2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_ScoreGap2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_DefaultScoreGap2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_DefaultOffset2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Boxes2 = new SyncroC.Controls.TitledNumericUpDown();
      this.cmb_DefaultType2 = new SyncroC.Controls.TitledList();
      this.cmb_DefaultPositioning2 = new SyncroC.Controls.TitledList();
      this.cmb_Level2 = new SyncroC.Controls.TitledList();
      this.ttl_ord2 = new SyncroC.Controls.Title();
      this.cmb_Shaft2 = new SyncroC.Controls.TitledList();
      this.cmb_Type2 = new SyncroC.Controls.TitledList();
      this.nud_Measure2 = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_ScorerOptions2 = new SyncroC.Controls.Title();
      this.ttl_DefaultScorerOptions2 = new SyncroC.Controls.Title();
      this.grv_ScorerOptions2 = new SyncroC.Controls.GridView();
      this.cmb_Positioning2 = new SyncroC.Controls.TitledList();
      this.NextPageState = new System.Windows.Forms.Label();
      this.nud_SetupLength = new SyncroC.Controls.TitledNumericUpDown();
      this.OfficeId = new System.Windows.Forms.Label();
      this.lab_setupModId = new System.Windows.Forms.Label();
      this.lab_officeId = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.pnl_OrdersO.SuspendLayout();
      this.pnl_Orders1.SuspendLayout();
      this.pnl_Orders2.SuspendLayout();
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
      // tab_Tab2
      // 
      this.tab_Tab2.Leave += new System.EventHandler(this.tab_Tab2_Leave);
      // 
      // tab_TabA
      // 
      this.tab_TabA.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      // 
      // tab_TabC
      // 
      this.tab_TabC.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
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
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF1_YY.Text = "New";
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF2_YY.Text = "Copy & Modify";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF5_YY.Text = "Modify";
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF3_YY.Text = "Swap";
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
      // pnl_OrdersO
      // 
      this.pnl_OrdersO.BackColor = System.Drawing.Color.LightGray;
      this.pnl_OrdersO.Controls.Add(this.ttb_CustomerName0);
      this.pnl_OrdersO.Controls.Add(this.ttb_OrderNumber0);
      this.pnl_OrdersO.Controls.Add(this.ttl_ord0);
      this.pnl_OrdersO.Controls.Add(this.nud_Width0);
      this.pnl_OrdersO.Controls.Add(this.nud_Offset0);
      this.pnl_OrdersO.Controls.Add(this.nud_Measure0);
      this.pnl_OrdersO.Controls.Add(this.nud_ScoreGap0);
      this.pnl_OrdersO.Controls.Add(this.nud_DefaultScoreGap0);
      this.pnl_OrdersO.Controls.Add(this.nud_DefaultOffset0);
      this.pnl_OrdersO.Controls.Add(this.nud_Boxes0);
      this.pnl_OrdersO.Controls.Add(this.cmb_DefaultType0);
      this.pnl_OrdersO.Controls.Add(this.cmb_DefaultPositioning0);
      this.pnl_OrdersO.Controls.Add(this.grv_ScorerOptions0);
      this.pnl_OrdersO.Controls.Add(this.cmb_Positioning0);
      this.pnl_OrdersO.Controls.Add(this.cmb_Type0);
      this.pnl_OrdersO.Controls.Add(this.ttl_ScorerOptions0);
      this.pnl_OrdersO.Controls.Add(this.ttl_DefaultScorerOptions0);
      this.pnl_OrdersO.Controls.Add(this.cmb_Level0);
      this.pnl_OrdersO.Controls.Add(this.cmb_Shaft0);
      this.pnl_OrdersO.Location = new System.Drawing.Point(30, 279);
      this.pnl_OrdersO.Name = "pnl_OrdersO";
      this.pnl_OrdersO.Size = new System.Drawing.Size(384, 456);
      this.pnl_OrdersO.TabIndex = 352;
      // 
      // ttb_CustomerName0
      // 
      this.ttb_CustomerName0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CustomerName0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CustomerName0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustomerName0.ImagePassword = null;
      this.ttb_CustomerName0.Location = new System.Drawing.Point(135, 21);
      this.ttb_CustomerName0.MaxChar = 32767;
      this.ttb_CustomerName0.Multiline = false;
      this.ttb_CustomerName0.Name = "ttb_CustomerName0";
      this.ttb_CustomerName0.PasswordChar = '\0';
      this.ttb_CustomerName0.ReadOnly = true;
      this.ttb_CustomerName0.Size = new System.Drawing.Size(246, 52);
      this.ttb_CustomerName0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CustomerName0.TabIndex = 10;
      this.ttb_CustomerName0.Text = "Customer name";
      this.ttb_CustomerName0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_CustomerName0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustomerName0.TranslateText = true;
      this.ttb_CustomerName0.Value = " ";
      this.ttb_CustomerName0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_OrderNumber0
      // 
      this.ttb_OrderNumber0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_OrderNumber0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_OrderNumber0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_OrderNumber0.ImagePassword = null;
      this.ttb_OrderNumber0.Location = new System.Drawing.Point(17, 21);
      this.ttb_OrderNumber0.MaxChar = 32767;
      this.ttb_OrderNumber0.Multiline = false;
      this.ttb_OrderNumber0.Name = "ttb_OrderNumber0";
      this.ttb_OrderNumber0.PasswordChar = '\0';
      this.ttb_OrderNumber0.ReadOnly = true;
      this.ttb_OrderNumber0.Size = new System.Drawing.Size(114, 52);
      this.ttb_OrderNumber0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_OrderNumber0.TabIndex = 9;
      this.ttb_OrderNumber0.Text = "Order number";
      this.ttb_OrderNumber0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_OrderNumber0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_OrderNumber0.TranslateText = true;
      this.ttb_OrderNumber0.Value = " ";
      this.ttb_OrderNumber0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_OrderNumber0.GotFocus += new System.EventHandler(this.ttb_OrderNumber_Focus);
      // 
      // ttl_ord0
      // 
      this.ttl_ord0.BackColor = System.Drawing.Color.Black;
      this.ttl_ord0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ord0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ord0.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ord0.Location = new System.Drawing.Point(0, 1);
      this.ttl_ord0.Name = "ttl_ord0";
      this.ttl_ord0.Size = new System.Drawing.Size(384, 454);
      this.ttl_ord0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_ord0.TabIndex = 352;
      this.ttl_ord0.TabStop = false;
      this.ttl_ord0.Text = "ORDER 1";
      this.ttl_ord0.TranslateText = true;
      // 
      // nud_Width0
      // 
      this.nud_Width0.DecimalPlaces = 0;
      this.nud_Width0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Width0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Width0.ImagePassword = null;
      this.nud_Width0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Width0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_Width0.Location = new System.Drawing.Point(221, 72);
      this.nud_Width0.Margin = new System.Windows.Forms.Padding(2, 3, 3, 1);
      this.nud_Width0.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Width0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Width0.Name = "nud_Width0";
      this.nud_Width0.ReadOnly = true;
      this.nud_Width0.Size = new System.Drawing.Size(127, 52);
      this.nud_Width0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Width0.TabIndex = 13;
      this.nud_Width0.Text = "Width";
      this.nud_Width0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Width0.TranslateText = true;
      this.nud_Width0.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Width0.UnitMeasureVisible = false;
      this.nud_Width0.Value = "";
      this.nud_Width0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Width0.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_Width0.Leave += new System.EventHandler(this.nud_Width_Leave);
      // 
      // nud_Offset0
      // 
      this.nud_Offset0.DecimalPlaces = 0;
      this.nud_Offset0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Offset0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Offset0.ImagePassword = null;
      this.nud_Offset0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Offset0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Offset0.Location = new System.Drawing.Point(17, 344);
      this.nud_Offset0.Margin = new System.Windows.Forms.Padding(2, 0, 1, 1);
      this.nud_Offset0.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Offset0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Offset0.Name = "nud_Offset0";
      this.nud_Offset0.ReadOnly = true;
      this.nud_Offset0.Size = new System.Drawing.Size(80, 52);
      this.nud_Offset0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Offset0.TabIndex = 22;
      this.nud_Offset0.Text = "Offset";
      this.nud_Offset0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Offset0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Offset0.TranslateText = true;
      this.nud_Offset0.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Offset0.Value = "";
      this.nud_Offset0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Offset0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_Measure0
      // 
      this.nud_Measure0.DecimalPlaces = 0;
      this.nud_Measure0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Measure0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Measure0.ImagePassword = null;
      this.nud_Measure0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Measure0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_Measure0.Location = new System.Drawing.Point(17, 228);
      this.nud_Measure0.Margin = new System.Windows.Forms.Padding(3, 0, 1, 1);
      this.nud_Measure0.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Measure0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Measure0.Name = "nud_Measure0";
      this.nud_Measure0.ReadOnly = true;
      this.nud_Measure0.Size = new System.Drawing.Size(80, 52);
      this.nud_Measure0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Measure0.TabIndex = 18;
      this.nud_Measure0.Text = "Measure";
      this.nud_Measure0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Measure0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Measure0.TranslateText = true;
      this.nud_Measure0.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Measure0.UnitMeasureVisible = false;
      this.nud_Measure0.Value = "";
      this.nud_Measure0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Measure0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Measure0.Validating += new System.ComponentModel.CancelEventHandler(this.nud_Measure_Validating);
      // 
      // nud_ScoreGap0
      // 
      this.nud_ScoreGap0.DecimalPlaces = 0;
      this.nud_ScoreGap0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScoreGap0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap0.ImagePassword = null;
      this.nud_ScoreGap0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScoreGap0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap0.Location = new System.Drawing.Point(17, 286);
      this.nud_ScoreGap0.Margin = new System.Windows.Forms.Padding(1, 0, 3, 1);
      this.nud_ScoreGap0.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_ScoreGap0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScoreGap0.Name = "nud_ScoreGap0";
      this.nud_ScoreGap0.ReadOnly = true;
      this.nud_ScoreGap0.Size = new System.Drawing.Size(80, 52);
      this.nud_ScoreGap0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_ScoreGap0.TabIndex = 21;
      this.nud_ScoreGap0.Text = "Gap";
      this.nud_ScoreGap0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_ScoreGap0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap0.TranslateText = true;
      this.nud_ScoreGap0.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap0.UnitMeasureVisible = false;
      this.nud_ScoreGap0.Value = "";
      this.nud_ScoreGap0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_ScoreGap0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_DefaultScoreGap0
      // 
      this.nud_DefaultScoreGap0.DecimalPlaces = 0;
      this.nud_DefaultScoreGap0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DefaultScoreGap0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultScoreGap0.ImagePassword = null;
      this.nud_DefaultScoreGap0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DefaultScoreGap0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_DefaultScoreGap0.Location = new System.Drawing.Point(254, 149);
      this.nud_DefaultScoreGap0.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.nud_DefaultScoreGap0.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_DefaultScoreGap0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DefaultScoreGap0.Name = "nud_DefaultScoreGap0";
      this.nud_DefaultScoreGap0.ReadOnly = true;
      this.nud_DefaultScoreGap0.Size = new System.Drawing.Size(62, 52);
      this.nud_DefaultScoreGap0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_DefaultScoreGap0.TabIndex = 16;
      this.nud_DefaultScoreGap0.Text = "Gap";
      this.nud_DefaultScoreGap0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultScoreGap0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultScoreGap0.TranslateText = true;
      this.nud_DefaultScoreGap0.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_DefaultScoreGap0.UnitMeasureVisible = false;
      this.nud_DefaultScoreGap0.Value = "";
      this.nud_DefaultScoreGap0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultScoreGap0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_DefaultScoreGap0.ValueChanged += new System.EventHandler(this.nud_DefaultScoreGap_ValueChanged);
      this.nud_DefaultScoreGap0.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_DefaultScoreGap0.Leave += new System.EventHandler(this.nud_DefaultScoreGap_Leave);
      // 
      // nud_DefaultOffset0
      // 
      this.nud_DefaultOffset0.DecimalPlaces = 0;
      this.nud_DefaultOffset0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DefaultOffset0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultOffset0.ImagePassword = null;
      this.nud_DefaultOffset0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DefaultOffset0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DefaultOffset0.Location = new System.Drawing.Point(321, 149);
      this.nud_DefaultOffset0.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.nud_DefaultOffset0.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_DefaultOffset0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DefaultOffset0.Name = "nud_DefaultOffset0";
      this.nud_DefaultOffset0.ReadOnly = true;
      this.nud_DefaultOffset0.Size = new System.Drawing.Size(62, 52);
      this.nud_DefaultOffset0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_DefaultOffset0.TabIndex = 17;
      this.nud_DefaultOffset0.Text = "Offset";
      this.nud_DefaultOffset0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultOffset0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultOffset0.TranslateText = true;
      this.nud_DefaultOffset0.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DefaultOffset0.Value = "";
      this.nud_DefaultOffset0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultOffset0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_DefaultOffset0.Leave += new System.EventHandler(this.nud_DefaultOffset_Leave);
      // 
      // nud_Boxes0
      // 
      this.nud_Boxes0.DecimalPlaces = 0;
      this.nud_Boxes0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Boxes0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Boxes0.ImagePassword = null;
      this.nud_Boxes0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Boxes0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Boxes0.Location = new System.Drawing.Point(155, 72);
      this.nud_Boxes0.Margin = new System.Windows.Forms.Padding(1, 3, 2, 1);
      this.nud_Boxes0.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Boxes0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Boxes0.Name = "nud_Boxes0";
      this.nud_Boxes0.ReadOnly = true;
      this.nud_Boxes0.Size = new System.Drawing.Size(62, 52);
      this.nud_Boxes0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Boxes0.TabIndex = 12;
      this.nud_Boxes0.Text = "Boxes";
      this.nud_Boxes0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Boxes0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Boxes0.TranslateText = true;
      this.nud_Boxes0.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Boxes0.Value = "";
      this.nud_Boxes0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Boxes0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Boxes0.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      // 
      // cmb_DefaultType0
      // 
      this.cmb_DefaultType0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_DefaultType0.CanCancel = false;
      this.cmb_DefaultType0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_DefaultType0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_DefaultType0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DefaultType0.ImagePassword = null;
      this.cmb_DefaultType0.Location = new System.Drawing.Point(169, 149);
      this.cmb_DefaultType0.Margin = new System.Windows.Forms.Padding(1, 3, 2, 3);
      this.cmb_DefaultType0.Name = "cmb_DefaultType0";
      this.cmb_DefaultType0.ReadOnly = true;
      this.cmb_DefaultType0.SelectedIndex = -1;
      this.cmb_DefaultType0.SelectedItem = "";
      this.cmb_DefaultType0.SelectedItemData = "";
      this.cmb_DefaultType0.Size = new System.Drawing.Size(82, 52);
      this.cmb_DefaultType0.Sorted = false;
      this.cmb_DefaultType0.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_DefaultType0.TabIndex = 15;
      this.cmb_DefaultType0.Text = "Type";
      this.cmb_DefaultType0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DefaultType0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DefaultType0.TranslateText = true;
      this.cmb_DefaultType0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DefaultType0.SelectedItemChanged += new System.EventHandler(this.cmb_DefaultScoreType_SelectedItemChanged);
      this.cmb_DefaultType0.Leave += new System.EventHandler(this.cmb_DefaultType_Leave);
      // 
      // cmb_DefaultPositioning0
      // 
      this.cmb_DefaultPositioning0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_DefaultPositioning0.CanCancel = false;
      this.cmb_DefaultPositioning0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_DefaultPositioning0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_DefaultPositioning0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DefaultPositioning0.ImagePassword = null;
      this.cmb_DefaultPositioning0.Location = new System.Drawing.Point(17, 149);
      this.cmb_DefaultPositioning0.Margin = new System.Windows.Forms.Padding(3, 1, 2, 1);
      this.cmb_DefaultPositioning0.Name = "cmb_DefaultPositioning0";
      this.cmb_DefaultPositioning0.ReadOnly = true;
      this.cmb_DefaultPositioning0.SelectedIndex = -1;
      this.cmb_DefaultPositioning0.SelectedItem = "";
      this.cmb_DefaultPositioning0.SelectedItemData = "";
      this.cmb_DefaultPositioning0.Size = new System.Drawing.Size(148, 52);
      this.cmb_DefaultPositioning0.Sorted = false;
      this.cmb_DefaultPositioning0.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_DefaultPositioning0.TabIndex = 14;
      this.cmb_DefaultPositioning0.Text = "Positioning";
      this.cmb_DefaultPositioning0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DefaultPositioning0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DefaultPositioning0.TranslateText = true;
      this.cmb_DefaultPositioning0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DefaultPositioning0.SelectedItemChanged += new System.EventHandler(this.cmb_DefaultScorePositioning_SelectedItemChanged);
      this.cmb_DefaultPositioning0.Leave += new System.EventHandler(this.cmb_DefaultPositioning_Leave);
      // 
      // grv_ScorerOptions0
      // 
      this.grv_ScorerOptions0.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.grv_ScorerOptions0.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grv_ScorerOptions0.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.grv_ScorerOptions0.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grv_ScorerOptions0.AutoGenerateColumns = false;
      this.grv_ScorerOptions0.BackColor = System.Drawing.Color.LightGray;
      this.grv_ScorerOptions0.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.grv_ScorerOptions0.DataMember = "";
      this.grv_ScorerOptions0.DataSource = null;
      this.grv_ScorerOptions0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grv_ScorerOptions0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.grv_ScorerOptions0.ImagePassword = null;
      this.grv_ScorerOptions0.Location = new System.Drawing.Point(102, 284);
      this.grv_ScorerOptions0.Margin = new System.Windows.Forms.Padding(0, 1, 2, 3);
      this.grv_ScorerOptions0.MultiSelect = false;
      this.grv_ScorerOptions0.Name = "grv_ScorerOptions0";
      this.grv_ScorerOptions0.RowsHeight = 32;
      this.grv_ScorerOptions0.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.grv_ScorerOptions0.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.grv_ScorerOptions0.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grv_ScorerOptions0.Size = new System.Drawing.Size(247, 171);
      this.grv_ScorerOptions0.Style = SyncroC.Controls.GridStyles.Light;
      this.grv_ScorerOptions0.TabIndex = 877;
      this.grv_ScorerOptions0.TabStop = false;
      this.grv_ScorerOptions0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.grv_ScorerOptions0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.grv_ScorerOptions0.TranslateText = true;
      this.grv_ScorerOptions0.SelectionChanged += new System.EventHandler(this.grv_ScorerOptions_SelectionChanged);
      // 
      // cmb_Positioning0
      // 
      this.cmb_Positioning0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Positioning0.CanCancel = false;
      this.cmb_Positioning0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Positioning0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Positioning0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning0.ImagePassword = null;
      this.cmb_Positioning0.Location = new System.Drawing.Point(101, 228);
      this.cmb_Positioning0.Margin = new System.Windows.Forms.Padding(2, 0, 2, 1);
      this.cmb_Positioning0.Name = "cmb_Positioning0";
      this.cmb_Positioning0.ReadOnly = true;
      this.cmb_Positioning0.SelectedIndex = -1;
      this.cmb_Positioning0.SelectedItem = "";
      this.cmb_Positioning0.SelectedItemData = "";
      this.cmb_Positioning0.Size = new System.Drawing.Size(182, 52);
      this.cmb_Positioning0.Sorted = false;
      this.cmb_Positioning0.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Positioning0.TabIndex = 19;
      this.cmb_Positioning0.Text = "Positioning";
      this.cmb_Positioning0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning0.TranslateText = true;
      this.cmb_Positioning0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning0.SelectedItemChanged += new System.EventHandler(this.cmb_Positioning_SelectedItemChanged);
      // 
      // cmb_Type0
      // 
      this.cmb_Type0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Type0.CanCancel = false;
      this.cmb_Type0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Type0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Type0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type0.ImagePassword = null;
      this.cmb_Type0.Location = new System.Drawing.Point(288, 228);
      this.cmb_Type0.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
      this.cmb_Type0.Name = "cmb_Type0";
      this.cmb_Type0.ReadOnly = true;
      this.cmb_Type0.SelectedIndex = -1;
      this.cmb_Type0.SelectedItem = "";
      this.cmb_Type0.SelectedItemData = "";
      this.cmb_Type0.Size = new System.Drawing.Size(96, 52);
      this.cmb_Type0.Sorted = false;
      this.cmb_Type0.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Type0.TabIndex = 20;
      this.cmb_Type0.Text = "Type";
      this.cmb_Type0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Type0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type0.TranslateText = true;
      this.cmb_Type0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_ScorerOptions0
      // 
      this.ttl_ScorerOptions0.BackColor = System.Drawing.Color.Black;
      this.ttl_ScorerOptions0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ScorerOptions0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ScorerOptions0.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ScorerOptions0.Location = new System.Drawing.Point(0, 207);
      this.ttl_ScorerOptions0.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
      this.ttl_ScorerOptions0.Name = "ttl_ScorerOptions0";
      this.ttl_ScorerOptions0.Size = new System.Drawing.Size(384, 25);
      this.ttl_ScorerOptions0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_ScorerOptions0.TabIndex = 361;
      this.ttl_ScorerOptions0.TabStop = false;
      this.ttl_ScorerOptions0.Text = "SCORER OPTIONS";
      this.ttl_ScorerOptions0.TranslateText = true;
      // 
      // ttl_DefaultScorerOptions0
      // 
      this.ttl_DefaultScorerOptions0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(205)))), ((int)(((byte)(203)))));
      this.ttl_DefaultScorerOptions0.color = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.ttl_DefaultScorerOptions0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DefaultScorerOptions0.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_DefaultScorerOptions0.Location = new System.Drawing.Point(9, 129);
      this.ttl_DefaultScorerOptions0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
      this.ttl_DefaultScorerOptions0.Name = "ttl_DefaultScorerOptions0";
      this.ttl_DefaultScorerOptions0.Size = new System.Drawing.Size(375, 71);
      this.ttl_DefaultScorerOptions0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_DefaultScorerOptions0.TabIndex = 360;
      this.ttl_DefaultScorerOptions0.Text = "Default scorer options";
      this.ttl_DefaultScorerOptions0.TranslateText = true;
      // 
      // cmb_Level0
      // 
      this.cmb_Level0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Level0.CanCancel = false;
      this.cmb_Level0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Level0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Level0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Level0.ImagePassword = null;
      this.cmb_Level0.Location = new System.Drawing.Point(17, 72);
      this.cmb_Level0.Margin = new System.Windows.Forms.Padding(3, 3, 2, 0);
      this.cmb_Level0.Name = "cmb_Level0";
      this.cmb_Level0.ReadOnly = true;
      this.cmb_Level0.SelectedIndex = -1;
      this.cmb_Level0.SelectedItem = "";
      this.cmb_Level0.SelectedItemData = "";
      this.cmb_Level0.Size = new System.Drawing.Size(134, 52);
      this.cmb_Level0.Sorted = false;
      this.cmb_Level0.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Level0.TabIndex = 11;
      this.cmb_Level0.Text = "Level";
      this.cmb_Level0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Level0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Level0.TranslateText = true;
      this.cmb_Level0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // cmb_Shaft0
      // 
      this.cmb_Shaft0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Shaft0.CanCancel = false;
      this.cmb_Shaft0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Shaft0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Shaft0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Shaft0.ImagePassword = null;
      this.cmb_Shaft0.Location = new System.Drawing.Point(17, 403);
      this.cmb_Shaft0.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.cmb_Shaft0.Name = "cmb_Shaft0";
      this.cmb_Shaft0.ReadOnly = true;
      this.cmb_Shaft0.SelectedIndex = -1;
      this.cmb_Shaft0.SelectedItem = "";
      this.cmb_Shaft0.SelectedItemData = "";
      this.cmb_Shaft0.Size = new System.Drawing.Size(80, 52);
      this.cmb_Shaft0.Sorted = false;
      this.cmb_Shaft0.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Shaft0.TabIndex = 23;
      this.cmb_Shaft0.Text = "Shaft";
      this.cmb_Shaft0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Shaft0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Shaft0.TranslateText = true;
      this.cmb_Shaft0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_SuggMax
      // 
      this.ttb_SuggMax.CausesValidation = false;
      this.ttb_SuggMax.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_SuggMax.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_SuggMax.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_SuggMax.ImagePassword = null;
      this.ttb_SuggMax.Location = new System.Drawing.Point(1030, 228);
      this.ttb_SuggMax.MaxChar = 32767;
      this.ttb_SuggMax.Multiline = false;
      this.ttb_SuggMax.Name = "ttb_SuggMax";
      this.ttb_SuggMax.PasswordChar = '\0';
      this.ttb_SuggMax.ReadOnly = true;
      this.ttb_SuggMax.Size = new System.Drawing.Size(171, 50);
      this.ttb_SuggMax.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_SuggMax.TabIndex = 363;
      this.ttb_SuggMax.TabStop = false;
      this.ttb_SuggMax.Text = "MAX";
      this.ttb_SuggMax.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_SuggMax.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_SuggMax.TranslateText = true;
      this.ttb_SuggMax.Value = " ";
      this.ttb_SuggMax.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_index
      // 
      this.ttb_index.CausesValidation = false;
      this.ttb_index.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_index.Font = new System.Drawing.Font("Arial Unicode MS", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_index.ForeColor = System.Drawing.Color.Black;
      this.ttb_index.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_index.ImagePassword = null;
      this.ttb_index.Location = new System.Drawing.Point(30, 176);
      this.ttb_index.MaxChar = 2;
      this.ttb_index.Multiline = false;
      this.ttb_index.Name = "ttb_index";
      this.ttb_index.PasswordChar = '\0';
      this.ttb_index.ReadOnly = true;
      this.ttb_index.Size = new System.Drawing.Size(171, 102);
      this.ttb_index.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_index.TabIndex = 362;
      this.ttb_index.TabStop = false;
      this.ttb_index.Text = "QUEUE";
      this.ttb_index.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_index.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_index.TranslateText = true;
      this.ttb_index.Value = " ";
      this.ttb_index.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // cmb_TrimType
      // 
      this.cmb_TrimType.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_TrimType.CanCancel = false;
      this.cmb_TrimType.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_TrimType.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_TrimType.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_TrimType.ImagePassword = null;
      this.cmb_TrimType.Location = new System.Drawing.Point(624, 228);
      this.cmb_TrimType.Name = "cmb_TrimType";
      this.cmb_TrimType.ReadOnly = true;
      this.cmb_TrimType.SelectedIndex = -1;
      this.cmb_TrimType.SelectedItem = "";
      this.cmb_TrimType.SelectedItemData = "";
      this.cmb_TrimType.Size = new System.Drawing.Size(248, 50);
      this.cmb_TrimType.Sorted = false;
      this.cmb_TrimType.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_TrimType.TabIndex = 8;
      this.cmb_TrimType.Text = "TRIM TYPE";
      this.cmb_TrimType.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_TrimType.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_TrimType.TranslateText = true;
      this.cmb_TrimType.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // nud_ScoreGapHeader
      // 
      this.nud_ScoreGapHeader.DecimalPlaces = 0;
      this.nud_ScoreGapHeader.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScoreGapHeader.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGapHeader.ImagePassword = null;
      this.nud_ScoreGapHeader.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScoreGapHeader.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGapHeader.Location = new System.Drawing.Point(485, 228);
      this.nud_ScoreGapHeader.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_ScoreGapHeader.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScoreGapHeader.Name = "nud_ScoreGapHeader";
      this.nud_ScoreGapHeader.ReadOnly = true;
      this.nud_ScoreGapHeader.Size = new System.Drawing.Size(133, 50);
      this.nud_ScoreGapHeader.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_ScoreGapHeader.TabIndex = 7;
      this.nud_ScoreGapHeader.Text = "SCORE GAP";
      this.nud_ScoreGapHeader.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGapHeader.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScoreGapHeader.TranslateText = true;
      this.nud_ScoreGapHeader.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGapHeader.UnitMeasureVisible = false;
      this.nud_ScoreGapHeader.Value = "";
      this.nud_ScoreGapHeader.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScoreGapHeader.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_ScoreGapHeader.ValueChanged += new System.EventHandler(this.nud_ScoreGapHeader_ValueChanged);
      this.nud_ScoreGapHeader.Leave += new System.EventHandler(this.nud_ScoreGap_Leave);
      this.nud_ScoreGapHeader.Enter += new System.EventHandler(this.nud_ScoreGap_Enter);
      // 
      // nud_Trim
      // 
      this.nud_Trim.DecimalPlaces = 0;
      this.nud_Trim.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Trim.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Trim.ImagePassword = null;
      this.nud_Trim.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Trim.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_Trim.Location = new System.Drawing.Point(878, 228);
      this.nud_Trim.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.nud_Trim.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Trim.Minimum = new decimal(new int[] {
            100000000,
            0,
            0,
            -2147483648});
      this.nud_Trim.Name = "nud_Trim";
      this.nud_Trim.ReadOnly = true;
      this.nud_Trim.Size = new System.Drawing.Size(133, 50);
      this.nud_Trim.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_Trim.TabIndex = 555;
      this.nud_Trim.TabStop = false;
      this.nud_Trim.Text = "TRIM";
      this.nud_Trim.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Trim.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Trim.TranslateText = true;
      this.nud_Trim.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Trim.UnitMeasureVisible = false;
      this.nud_Trim.Value = "";
      this.nud_Trim.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Trim.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_WebWidth
      // 
      this.nud_WebWidth.DecimalPlaces = 0;
      this.nud_WebWidth.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_WebWidth.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_WebWidth.ImagePassword = null;
      this.nud_WebWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_WebWidth.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_WebWidth.Location = new System.Drawing.Point(207, 228);
      this.nud_WebWidth.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_WebWidth.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_WebWidth.Name = "nud_WebWidth";
      this.nud_WebWidth.ReadOnly = true;
      this.nud_WebWidth.Size = new System.Drawing.Size(133, 50);
      this.nud_WebWidth.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_WebWidth.TabIndex = 5;
      this.nud_WebWidth.Text = "WEB WIDTH";
      this.nud_WebWidth.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_WebWidth.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_WebWidth.TranslateText = true;
      this.nud_WebWidth.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_WebWidth.UnitMeasureVisible = false;
      this.nud_WebWidth.Value = "";
      this.nud_WebWidth.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_WebWidth.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_WebWidth.Validating += new System.ComponentModel.CancelEventHandler(this.nud_WebWidth_Validating);
      // 
      // cmb_Quality
      // 
      this.cmb_Quality.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_Quality.CanCancel = false;
      this.cmb_Quality.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Quality.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Quality.ImagePassword = null;
      this.cmb_Quality.Location = new System.Drawing.Point(656, 176);
      this.cmb_Quality.Name = "cmb_Quality";
      this.cmb_Quality.ReadOnly = true;
      this.cmb_Quality.SelectedIndex = -1;
      this.cmb_Quality.SelectedItem = "";
      this.cmb_Quality.SelectedItemData = "";
      this.cmb_Quality.Size = new System.Drawing.Size(355, 50);
      this.cmb_Quality.Sorted = true;
      this.cmb_Quality.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_Quality.TabIndex = 4;
      this.cmb_Quality.Text = "QUALITY";
      this.cmb_Quality.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Quality.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Quality.TranslateText = true;
      this.cmb_Quality.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Quality.SelectedItemChanged += new System.EventHandler(this.cmb_Quality_SelectedItemChanged);
      this.cmb_Quality.Leave += new System.EventHandler(this.cmb_Quality_SelectedItemChanged);
      // 
      // ttb_SuggMin
      // 
      this.ttb_SuggMin.CausesValidation = false;
      this.ttb_SuggMin.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_SuggMin.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_SuggMin.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_SuggMin.ImagePassword = null;
      this.ttb_SuggMin.Location = new System.Drawing.Point(1030, 176);
      this.ttb_SuggMin.MaxChar = 32767;
      this.ttb_SuggMin.Multiline = false;
      this.ttb_SuggMin.Name = "ttb_SuggMin";
      this.ttb_SuggMin.PasswordChar = '\0';
      this.ttb_SuggMin.ReadOnly = true;
      this.ttb_SuggMin.Size = new System.Drawing.Size(171, 50);
      this.ttb_SuggMin.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_SuggMin.TabIndex = 361;
      this.ttb_SuggMin.TabStop = false;
      this.ttb_SuggMin.Text = "MIN";
      this.ttb_SuggMin.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_SuggMin.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_SuggMin.TranslateText = true;
      this.ttb_SuggMin.Value = " ";
      this.ttb_SuggMin.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_SetUpSequence
      // 
      this.ttb_SetUpSequence.CausesValidation = false;
      this.ttb_SetUpSequence.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_SetUpSequence.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_SetUpSequence.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_SetUpSequence.ImagePassword = null;
      this.ttb_SetUpSequence.Location = new System.Drawing.Point(485, 176);
      this.ttb_SetUpSequence.MaxChar = 2;
      this.ttb_SetUpSequence.Multiline = false;
      this.ttb_SetUpSequence.Name = "ttb_SetUpSequence";
      this.ttb_SetUpSequence.PasswordChar = '\0';
      this.ttb_SetUpSequence.ReadOnly = true;
      this.ttb_SetUpSequence.Size = new System.Drawing.Size(66, 50);
      this.ttb_SetUpSequence.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_SetUpSequence.TabIndex = 2;
      this.ttb_SetUpSequence.Text = "SEQ";
      this.ttb_SetUpSequence.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_SetUpSequence.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_SetUpSequence.TranslateText = true;
      this.ttb_SetUpSequence.Value = " ";
      this.ttb_SetUpSequence.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_SetUpSequence.Validating += new System.ComponentModel.CancelEventHandler(this.ttb_NotNullString_Validating);
      // 
      // cmb_Flute
      // 
      this.cmb_Flute.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_Flute.CanCancel = false;
      this.cmb_Flute.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Flute.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Flute.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Flute.ImagePassword = null;
      this.cmb_Flute.Location = new System.Drawing.Point(557, 176);
      this.cmb_Flute.Name = "cmb_Flute";
      this.cmb_Flute.ReadOnly = true;
      this.cmb_Flute.SelectedIndex = -1;
      this.cmb_Flute.SelectedItem = "";
      this.cmb_Flute.SelectedItemData = "";
      this.cmb_Flute.Size = new System.Drawing.Size(93, 50);
      this.cmb_Flute.Sorted = false;
      this.cmb_Flute.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_Flute.TabIndex = 3;
      this.cmb_Flute.Text = "FLUTE";
      this.cmb_Flute.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Flute.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Flute.TranslateText = true;
      this.cmb_Flute.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Flute.SelectedItemChanged += new System.EventHandler(this.cmb_Flute_SelectedItemChanged);
      // 
      // pnl_Orders1
      // 
      this.pnl_Orders1.BackColor = System.Drawing.Color.LightGray;
      this.pnl_Orders1.Controls.Add(this.ttb_CustomerName1);
      this.pnl_Orders1.Controls.Add(this.ttb_OrderNumber1);
      this.pnl_Orders1.Controls.Add(this.nud_Width1);
      this.pnl_Orders1.Controls.Add(this.nud_Offset1);
      this.pnl_Orders1.Controls.Add(this.nud_ScoreGap1);
      this.pnl_Orders1.Controls.Add(this.nud_Measure1);
      this.pnl_Orders1.Controls.Add(this.nud_DefaultScoreGap1);
      this.pnl_Orders1.Controls.Add(this.nud_DefaultOffset1);
      this.pnl_Orders1.Controls.Add(this.nud_Boxes1);
      this.pnl_Orders1.Controls.Add(this.cmb_DefaultType1);
      this.pnl_Orders1.Controls.Add(this.cmb_DefaultPositioning1);
      this.pnl_Orders1.Controls.Add(this.grv_ScorerOptions1);
      this.pnl_Orders1.Controls.Add(this.cmb_Positioning1);
      this.pnl_Orders1.Controls.Add(this.cmb_Type1);
      this.pnl_Orders1.Controls.Add(this.cmb_Shaft1);
      this.pnl_Orders1.Controls.Add(this.ttl_ScorerOptions1);
      this.pnl_Orders1.Controls.Add(this.ttl_DefaultScorerOptions1);
      this.pnl_Orders1.Controls.Add(this.cmb_Level1);
      this.pnl_Orders1.Controls.Add(this.ttl_ord1);
      this.pnl_Orders1.Location = new System.Drawing.Point(425, 279);
      this.pnl_Orders1.Name = "pnl_Orders1";
      this.pnl_Orders1.Size = new System.Drawing.Size(384, 456);
      this.pnl_Orders1.TabIndex = 366;
      // 
      // ttb_CustomerName1
      // 
      this.ttb_CustomerName1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CustomerName1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CustomerName1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustomerName1.ImagePassword = null;
      this.ttb_CustomerName1.Location = new System.Drawing.Point(135, 21);
      this.ttb_CustomerName1.MaxChar = 32767;
      this.ttb_CustomerName1.Multiline = false;
      this.ttb_CustomerName1.Name = "ttb_CustomerName1";
      this.ttb_CustomerName1.PasswordChar = '\0';
      this.ttb_CustomerName1.ReadOnly = true;
      this.ttb_CustomerName1.Size = new System.Drawing.Size(246, 52);
      this.ttb_CustomerName1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CustomerName1.TabIndex = 25;
      this.ttb_CustomerName1.Text = "Customer name";
      this.ttb_CustomerName1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_CustomerName1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustomerName1.TranslateText = true;
      this.ttb_CustomerName1.Value = " ";
      this.ttb_CustomerName1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_OrderNumber1
      // 
      this.ttb_OrderNumber1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_OrderNumber1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_OrderNumber1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_OrderNumber1.ImagePassword = null;
      this.ttb_OrderNumber1.Location = new System.Drawing.Point(17, 21);
      this.ttb_OrderNumber1.MaxChar = 32767;
      this.ttb_OrderNumber1.Multiline = false;
      this.ttb_OrderNumber1.Name = "ttb_OrderNumber1";
      this.ttb_OrderNumber1.PasswordChar = '\0';
      this.ttb_OrderNumber1.ReadOnly = true;
      this.ttb_OrderNumber1.Size = new System.Drawing.Size(114, 52);
      this.ttb_OrderNumber1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_OrderNumber1.TabIndex = 24;
      this.ttb_OrderNumber1.Text = "Order number";
      this.ttb_OrderNumber1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_OrderNumber1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_OrderNumber1.TranslateText = true;
      this.ttb_OrderNumber1.Value = " ";
      this.ttb_OrderNumber1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_OrderNumber1.GotFocus += new System.EventHandler(this.ttb_OrderNumber_Focus);
      // 
      // nud_Width1
      // 
      this.nud_Width1.DecimalPlaces = 0;
      this.nud_Width1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Width1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Width1.ImagePassword = null;
      this.nud_Width1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Width1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_Width1.Location = new System.Drawing.Point(223, 72);
      this.nud_Width1.Margin = new System.Windows.Forms.Padding(2, 3, 3, 1);
      this.nud_Width1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Width1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Width1.Name = "nud_Width1";
      this.nud_Width1.ReadOnly = true;
      this.nud_Width1.Size = new System.Drawing.Size(127, 52);
      this.nud_Width1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Width1.TabIndex = 28;
      this.nud_Width1.Text = "Width";
      this.nud_Width1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Width1.TranslateText = true;
      this.nud_Width1.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Width1.UnitMeasureVisible = false;
      this.nud_Width1.Value = "";
      this.nud_Width1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Width1.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_Width1.Leave += new System.EventHandler(this.nud_Width_Leave);
      // 
      // nud_Offset1
      // 
      this.nud_Offset1.DecimalPlaces = 0;
      this.nud_Offset1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Offset1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Offset1.ImagePassword = null;
      this.nud_Offset1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Offset1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Offset1.Location = new System.Drawing.Point(17, 344);
      this.nud_Offset1.Margin = new System.Windows.Forms.Padding(2, 0, 3, 3);
      this.nud_Offset1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Offset1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Offset1.Name = "nud_Offset1";
      this.nud_Offset1.ReadOnly = true;
      this.nud_Offset1.Size = new System.Drawing.Size(80, 52);
      this.nud_Offset1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Offset1.TabIndex = 37;
      this.nud_Offset1.Text = "Offset";
      this.nud_Offset1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Offset1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Offset1.TranslateText = true;
      this.nud_Offset1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Offset1.Value = "";
      this.nud_Offset1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Offset1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_ScoreGap1
      // 
      this.nud_ScoreGap1.DecimalPlaces = 0;
      this.nud_ScoreGap1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScoreGap1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap1.ImagePassword = null;
      this.nud_ScoreGap1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScoreGap1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap1.Location = new System.Drawing.Point(17, 286);
      this.nud_ScoreGap1.Margin = new System.Windows.Forms.Padding(1, 0, 2, 1);
      this.nud_ScoreGap1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_ScoreGap1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScoreGap1.Name = "nud_ScoreGap1";
      this.nud_ScoreGap1.ReadOnly = true;
      this.nud_ScoreGap1.Size = new System.Drawing.Size(80, 52);
      this.nud_ScoreGap1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_ScoreGap1.TabIndex = 36;
      this.nud_ScoreGap1.Text = "Gap";
      this.nud_ScoreGap1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_ScoreGap1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap1.TranslateText = true;
      this.nud_ScoreGap1.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap1.UnitMeasureVisible = false;
      this.nud_ScoreGap1.Value = "";
      this.nud_ScoreGap1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_ScoreGap1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_Measure1
      // 
      this.nud_Measure1.DecimalPlaces = 0;
      this.nud_Measure1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Measure1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Measure1.ImagePassword = null;
      this.nud_Measure1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Measure1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_Measure1.Location = new System.Drawing.Point(17, 228);
      this.nud_Measure1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 1);
      this.nud_Measure1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Measure1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Measure1.Name = "nud_Measure1";
      this.nud_Measure1.ReadOnly = true;
      this.nud_Measure1.Size = new System.Drawing.Size(80, 52);
      this.nud_Measure1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Measure1.TabIndex = 33;
      this.nud_Measure1.Text = "Measure";
      this.nud_Measure1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Measure1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Measure1.TranslateText = true;
      this.nud_Measure1.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Measure1.UnitMeasureVisible = false;
      this.nud_Measure1.Value = "";
      this.nud_Measure1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Measure1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Measure1.Validating += new System.ComponentModel.CancelEventHandler(this.nud_Measure_Validating);
      // 
      // nud_DefaultScoreGap1
      // 
      this.nud_DefaultScoreGap1.DecimalPlaces = 0;
      this.nud_DefaultScoreGap1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DefaultScoreGap1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultScoreGap1.ImagePassword = null;
      this.nud_DefaultScoreGap1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DefaultScoreGap1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_DefaultScoreGap1.Location = new System.Drawing.Point(254, 149);
      this.nud_DefaultScoreGap1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_DefaultScoreGap1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DefaultScoreGap1.Name = "nud_DefaultScoreGap1";
      this.nud_DefaultScoreGap1.ReadOnly = true;
      this.nud_DefaultScoreGap1.Size = new System.Drawing.Size(62, 52);
      this.nud_DefaultScoreGap1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_DefaultScoreGap1.TabIndex = 31;
      this.nud_DefaultScoreGap1.Text = "Gap";
      this.nud_DefaultScoreGap1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultScoreGap1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultScoreGap1.TranslateText = true;
      this.nud_DefaultScoreGap1.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_DefaultScoreGap1.UnitMeasureVisible = false;
      this.nud_DefaultScoreGap1.Value = "";
      this.nud_DefaultScoreGap1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultScoreGap1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_DefaultScoreGap1.ValueChanged += new System.EventHandler(this.nud_DefaultScoreGap_ValueChanged);
      this.nud_DefaultScoreGap1.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_DefaultScoreGap1.Leave += new System.EventHandler(this.nud_DefaultScoreGap_Leave);
      // 
      // nud_DefaultOffset1
      // 
      this.nud_DefaultOffset1.DecimalPlaces = 0;
      this.nud_DefaultOffset1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DefaultOffset1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultOffset1.ImagePassword = null;
      this.nud_DefaultOffset1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DefaultOffset1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DefaultOffset1.Location = new System.Drawing.Point(321, 149);
      this.nud_DefaultOffset1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_DefaultOffset1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DefaultOffset1.Name = "nud_DefaultOffset1";
      this.nud_DefaultOffset1.ReadOnly = true;
      this.nud_DefaultOffset1.Size = new System.Drawing.Size(62, 52);
      this.nud_DefaultOffset1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_DefaultOffset1.TabIndex = 32;
      this.nud_DefaultOffset1.Text = "Offset";
      this.nud_DefaultOffset1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultOffset1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultOffset1.TranslateText = true;
      this.nud_DefaultOffset1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DefaultOffset1.Value = "";
      this.nud_DefaultOffset1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultOffset1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_DefaultOffset1.Leave += new System.EventHandler(this.nud_DefaultOffset_Leave);
      // 
      // nud_Boxes1
      // 
      this.nud_Boxes1.DecimalPlaces = 0;
      this.nud_Boxes1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Boxes1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Boxes1.ImagePassword = null;
      this.nud_Boxes1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Boxes1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Boxes1.Location = new System.Drawing.Point(155, 72);
      this.nud_Boxes1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.nud_Boxes1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Boxes1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Boxes1.Name = "nud_Boxes1";
      this.nud_Boxes1.ReadOnly = true;
      this.nud_Boxes1.Size = new System.Drawing.Size(62, 52);
      this.nud_Boxes1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Boxes1.TabIndex = 27;
      this.nud_Boxes1.Text = "Boxes";
      this.nud_Boxes1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Boxes1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Boxes1.TranslateText = true;
      this.nud_Boxes1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Boxes1.Value = "";
      this.nud_Boxes1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Boxes1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Boxes1.Validating += new System.ComponentModel.CancelEventHandler(this.nud_NotNegativeValues_Validating);
      // 
      // cmb_DefaultType1
      // 
      this.cmb_DefaultType1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_DefaultType1.CanCancel = false;
      this.cmb_DefaultType1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_DefaultType1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_DefaultType1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DefaultType1.ImagePassword = null;
      this.cmb_DefaultType1.Location = new System.Drawing.Point(169, 149);
      this.cmb_DefaultType1.Name = "cmb_DefaultType1";
      this.cmb_DefaultType1.ReadOnly = true;
      this.cmb_DefaultType1.SelectedIndex = -1;
      this.cmb_DefaultType1.SelectedItem = "";
      this.cmb_DefaultType1.SelectedItemData = "";
      this.cmb_DefaultType1.Size = new System.Drawing.Size(82, 52);
      this.cmb_DefaultType1.Sorted = false;
      this.cmb_DefaultType1.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_DefaultType1.TabIndex = 30;
      this.cmb_DefaultType1.Text = "Type";
      this.cmb_DefaultType1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DefaultType1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DefaultType1.TranslateText = true;
      this.cmb_DefaultType1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DefaultType1.SelectedItemChanged += new System.EventHandler(this.cmb_DefaultScoreType_SelectedItemChanged);
      this.cmb_DefaultType1.Leave += new System.EventHandler(this.cmb_DefaultType_Leave);
      // 
      // cmb_DefaultPositioning1
      // 
      this.cmb_DefaultPositioning1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_DefaultPositioning1.CanCancel = false;
      this.cmb_DefaultPositioning1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_DefaultPositioning1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_DefaultPositioning1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DefaultPositioning1.ImagePassword = null;
      this.cmb_DefaultPositioning1.Location = new System.Drawing.Point(17, 149);
      this.cmb_DefaultPositioning1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.cmb_DefaultPositioning1.Name = "cmb_DefaultPositioning1";
      this.cmb_DefaultPositioning1.ReadOnly = true;
      this.cmb_DefaultPositioning1.SelectedIndex = -1;
      this.cmb_DefaultPositioning1.SelectedItem = "";
      this.cmb_DefaultPositioning1.SelectedItemData = "";
      this.cmb_DefaultPositioning1.Size = new System.Drawing.Size(148, 52);
      this.cmb_DefaultPositioning1.Sorted = false;
      this.cmb_DefaultPositioning1.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_DefaultPositioning1.TabIndex = 29;
      this.cmb_DefaultPositioning1.Text = "Positioning";
      this.cmb_DefaultPositioning1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DefaultPositioning1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DefaultPositioning1.TranslateText = true;
      this.cmb_DefaultPositioning1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DefaultPositioning1.SelectedItemChanged += new System.EventHandler(this.cmb_DefaultScorePositioning_SelectedItemChanged);
      this.cmb_DefaultPositioning1.Leave += new System.EventHandler(this.cmb_DefaultPositioning_Leave);
      // 
      // grv_ScorerOptions1
      // 
      this.grv_ScorerOptions1.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.grv_ScorerOptions1.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grv_ScorerOptions1.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.grv_ScorerOptions1.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grv_ScorerOptions1.AutoGenerateColumns = false;
      this.grv_ScorerOptions1.BackColor = System.Drawing.Color.LightGray;
      this.grv_ScorerOptions1.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.grv_ScorerOptions1.DataMember = "";
      this.grv_ScorerOptions1.DataSource = null;
      this.grv_ScorerOptions1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grv_ScorerOptions1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.grv_ScorerOptions1.ImagePassword = null;
      this.grv_ScorerOptions1.Location = new System.Drawing.Point(102, 284);
      this.grv_ScorerOptions1.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.grv_ScorerOptions1.MultiSelect = false;
      this.grv_ScorerOptions1.Name = "grv_ScorerOptions1";
      this.grv_ScorerOptions1.RowsHeight = 32;
      this.grv_ScorerOptions1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.grv_ScorerOptions1.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.grv_ScorerOptions1.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grv_ScorerOptions1.Size = new System.Drawing.Size(247, 171);
      this.grv_ScorerOptions1.Style = SyncroC.Controls.GridStyles.Light;
      this.grv_ScorerOptions1.TabIndex = 878;
      this.grv_ScorerOptions1.TabStop = false;
      this.grv_ScorerOptions1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.grv_ScorerOptions1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.grv_ScorerOptions1.TranslateText = true;
      this.grv_ScorerOptions1.SelectionChanged += new System.EventHandler(this.grv_ScorerOptions_SelectionChanged);
      // 
      // cmb_Positioning1
      // 
      this.cmb_Positioning1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Positioning1.CanCancel = false;
      this.cmb_Positioning1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Positioning1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Positioning1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning1.ImagePassword = null;
      this.cmb_Positioning1.Location = new System.Drawing.Point(101, 228);
      this.cmb_Positioning1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.cmb_Positioning1.Name = "cmb_Positioning1";
      this.cmb_Positioning1.ReadOnly = true;
      this.cmb_Positioning1.SelectedIndex = -1;
      this.cmb_Positioning1.SelectedItem = "";
      this.cmb_Positioning1.SelectedItemData = "";
      this.cmb_Positioning1.Size = new System.Drawing.Size(182, 52);
      this.cmb_Positioning1.Sorted = false;
      this.cmb_Positioning1.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Positioning1.TabIndex = 34;
      this.cmb_Positioning1.Text = "Positioning";
      this.cmb_Positioning1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning1.TranslateText = true;
      this.cmb_Positioning1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning1.SelectedItemChanged += new System.EventHandler(this.cmb_Positioning_SelectedItemChanged);
      // 
      // cmb_Type1
      // 
      this.cmb_Type1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Type1.CanCancel = false;
      this.cmb_Type1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Type1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Type1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type1.ImagePassword = null;
      this.cmb_Type1.Location = new System.Drawing.Point(288, 228);
      this.cmb_Type1.Margin = new System.Windows.Forms.Padding(1, 1, 3, 1);
      this.cmb_Type1.Name = "cmb_Type1";
      this.cmb_Type1.ReadOnly = true;
      this.cmb_Type1.SelectedIndex = -1;
      this.cmb_Type1.SelectedItem = "";
      this.cmb_Type1.SelectedItemData = "";
      this.cmb_Type1.Size = new System.Drawing.Size(96, 52);
      this.cmb_Type1.Sorted = false;
      this.cmb_Type1.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Type1.TabIndex = 35;
      this.cmb_Type1.Text = "Type";
      this.cmb_Type1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Type1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type1.TranslateText = true;
      this.cmb_Type1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // cmb_Shaft1
      // 
      this.cmb_Shaft1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Shaft1.CanCancel = false;
      this.cmb_Shaft1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Shaft1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Shaft1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Shaft1.ImagePassword = null;
      this.cmb_Shaft1.Location = new System.Drawing.Point(17, 403);
      this.cmb_Shaft1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.cmb_Shaft1.Name = "cmb_Shaft1";
      this.cmb_Shaft1.ReadOnly = true;
      this.cmb_Shaft1.SelectedIndex = -1;
      this.cmb_Shaft1.SelectedItem = "";
      this.cmb_Shaft1.SelectedItemData = "";
      this.cmb_Shaft1.Size = new System.Drawing.Size(80, 52);
      this.cmb_Shaft1.Sorted = false;
      this.cmb_Shaft1.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Shaft1.TabIndex = 38;
      this.cmb_Shaft1.Text = "Shaft";
      this.cmb_Shaft1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Shaft1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Shaft1.TranslateText = true;
      this.cmb_Shaft1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_ScorerOptions1
      // 
      this.ttl_ScorerOptions1.BackColor = System.Drawing.Color.Black;
      this.ttl_ScorerOptions1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ScorerOptions1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ScorerOptions1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ScorerOptions1.Location = new System.Drawing.Point(0, 208);
      this.ttl_ScorerOptions1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.ttl_ScorerOptions1.Name = "ttl_ScorerOptions1";
      this.ttl_ScorerOptions1.Size = new System.Drawing.Size(384, 25);
      this.ttl_ScorerOptions1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_ScorerOptions1.TabIndex = 361;
      this.ttl_ScorerOptions1.TabStop = false;
      this.ttl_ScorerOptions1.Text = "SCORER OPTIONS";
      this.ttl_ScorerOptions1.TranslateText = true;
      // 
      // ttl_DefaultScorerOptions1
      // 
      this.ttl_DefaultScorerOptions1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(205)))), ((int)(((byte)(203)))));
      this.ttl_DefaultScorerOptions1.color = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.ttl_DefaultScorerOptions1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DefaultScorerOptions1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_DefaultScorerOptions1.Location = new System.Drawing.Point(9, 130);
      this.ttl_DefaultScorerOptions1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
      this.ttl_DefaultScorerOptions1.Name = "ttl_DefaultScorerOptions1";
      this.ttl_DefaultScorerOptions1.Size = new System.Drawing.Size(375, 71);
      this.ttl_DefaultScorerOptions1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_DefaultScorerOptions1.TabIndex = 360;
      this.ttl_DefaultScorerOptions1.Text = "Default scorer options";
      this.ttl_DefaultScorerOptions1.TranslateText = true;
      // 
      // cmb_Level1
      // 
      this.cmb_Level1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Level1.CanCancel = false;
      this.cmb_Level1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Level1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Level1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Level1.ImagePassword = null;
      this.cmb_Level1.Location = new System.Drawing.Point(17, 72);
      this.cmb_Level1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
      this.cmb_Level1.Name = "cmb_Level1";
      this.cmb_Level1.ReadOnly = true;
      this.cmb_Level1.SelectedIndex = -1;
      this.cmb_Level1.SelectedItem = "";
      this.cmb_Level1.SelectedItemData = "";
      this.cmb_Level1.Size = new System.Drawing.Size(134, 52);
      this.cmb_Level1.Sorted = false;
      this.cmb_Level1.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Level1.TabIndex = 26;
      this.cmb_Level1.Text = "Level";
      this.cmb_Level1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Level1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Level1.TranslateText = true;
      this.cmb_Level1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_ord1
      // 
      this.ttl_ord1.BackColor = System.Drawing.Color.Black;
      this.ttl_ord1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ord1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ord1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ord1.Location = new System.Drawing.Point(0, 1);
      this.ttl_ord1.Name = "ttl_ord1";
      this.ttl_ord1.Size = new System.Drawing.Size(384, 454);
      this.ttl_ord1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_ord1.TabIndex = 352;
      this.ttl_ord1.TabStop = false;
      this.ttl_ord1.Text = "ORDER 2";
      this.ttl_ord1.TranslateText = true;
      // 
      // ModId
      // 
      this.ModId.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ModId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ModId.Location = new System.Drawing.Point(1034, 55);
      this.ModId.Name = "ModId";
      this.ModId.Size = new System.Drawing.Size(34, 20);
      this.ModId.TabIndex = 369;
      this.ModId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ModId.Visible = false;
      // 
      // SetupId
      // 
      this.SetupId.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SetupId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.SetupId.Location = new System.Drawing.Point(932, 55);
      this.SetupId.Name = "SetupId";
      this.SetupId.Size = new System.Drawing.Size(71, 20);
      this.SetupId.TabIndex = 368;
      this.SetupId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.SetupId.Visible = false;
      // 
      // PageState
      // 
      this.PageState.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.PageState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.PageState.Location = new System.Drawing.Point(545, 55);
      this.PageState.Name = "PageState";
      this.PageState.Size = new System.Drawing.Size(141, 20);
      this.PageState.TabIndex = 367;
      this.PageState.Text = "Page status: ";
      this.PageState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.PageState.Visible = false;
      // 
      // ttb_ProgramNumber
      // 
      this.ttb_ProgramNumber.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_ProgramNumber.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_ProgramNumber.ForeColor = System.Drawing.Color.Black;
      this.ttb_ProgramNumber.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ProgramNumber.ImagePassword = null;
      this.ttb_ProgramNumber.Location = new System.Drawing.Point(207, 176);
      this.ttb_ProgramNumber.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
      this.ttb_ProgramNumber.MaxChar = 18;
      this.ttb_ProgramNumber.Multiline = false;
      this.ttb_ProgramNumber.Name = "ttb_ProgramNumber";
      this.ttb_ProgramNumber.PasswordChar = '\0';
      this.ttb_ProgramNumber.ReadOnly = true;
      this.ttb_ProgramNumber.Size = new System.Drawing.Size(272, 50);
      this.ttb_ProgramNumber.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_ProgramNumber.TabIndex = 1;
      this.ttb_ProgramNumber.Text = "PROGRAM NUMBER";
      this.ttb_ProgramNumber.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ProgramNumber.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ProgramNumber.TranslateText = true;
      this.ttb_ProgramNumber.Value = " ";
      this.ttb_ProgramNumber.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ProgramNumber.Validating += new System.ComponentModel.CancelEventHandler(this.ttb_NotNullString_Validating);
      // 
      // ActiveCtrl
      // 
      this.ActiveCtrl.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ActiveCtrl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ActiveCtrl.Location = new System.Drawing.Point(309, 55);
      this.ActiveCtrl.Name = "ActiveCtrl";
      this.ActiveCtrl.Size = new System.Drawing.Size(224, 21);
      this.ActiveCtrl.TabIndex = 371;
      this.ActiveCtrl.Text = "Active control: ";
      this.ActiveCtrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ActiveCtrl.Visible = false;
      // 
      // tim_debug
      // 
      this.tim_debug.Interval = 500;
      this.tim_debug.Tick += new System.EventHandler(this.tim_debug_Tick);
      // 
      // pnl_Orders2
      // 
      this.pnl_Orders2.BackColor = System.Drawing.Color.LightGray;
      this.pnl_Orders2.Controls.Add(this.ttb_CustomerName2);
      this.pnl_Orders2.Controls.Add(this.ttb_OrderNumber2);
      this.pnl_Orders2.Controls.Add(this.nud_Width2);
      this.pnl_Orders2.Controls.Add(this.nud_Offset2);
      this.pnl_Orders2.Controls.Add(this.nud_ScoreGap2);
      this.pnl_Orders2.Controls.Add(this.nud_DefaultScoreGap2);
      this.pnl_Orders2.Controls.Add(this.nud_DefaultOffset2);
      this.pnl_Orders2.Controls.Add(this.nud_Boxes2);
      this.pnl_Orders2.Controls.Add(this.cmb_DefaultType2);
      this.pnl_Orders2.Controls.Add(this.cmb_DefaultPositioning2);
      this.pnl_Orders2.Controls.Add(this.cmb_Level2);
      this.pnl_Orders2.Controls.Add(this.ttl_ord2);
      this.pnl_Orders2.Controls.Add(this.cmb_Shaft2);
      this.pnl_Orders2.Controls.Add(this.cmb_Type2);
      this.pnl_Orders2.Controls.Add(this.nud_Measure2);
      this.pnl_Orders2.Controls.Add(this.ttl_ScorerOptions2);
      this.pnl_Orders2.Controls.Add(this.ttl_DefaultScorerOptions2);
      this.pnl_Orders2.Controls.Add(this.grv_ScorerOptions2);
      this.pnl_Orders2.Controls.Add(this.cmb_Positioning2);
      this.pnl_Orders2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.pnl_Orders2.Location = new System.Drawing.Point(817, 279);
      this.pnl_Orders2.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.pnl_Orders2.Name = "pnl_Orders2";
      this.pnl_Orders2.Size = new System.Drawing.Size(384, 456);
      this.pnl_Orders2.TabIndex = 372;
      // 
      // ttb_CustomerName2
      // 
      this.ttb_CustomerName2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CustomerName2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CustomerName2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustomerName2.ImagePassword = null;
      this.ttb_CustomerName2.Location = new System.Drawing.Point(135, 21);
      this.ttb_CustomerName2.MaxChar = 32767;
      this.ttb_CustomerName2.Multiline = false;
      this.ttb_CustomerName2.Name = "ttb_CustomerName2";
      this.ttb_CustomerName2.PasswordChar = '\0';
      this.ttb_CustomerName2.ReadOnly = true;
      this.ttb_CustomerName2.Size = new System.Drawing.Size(246, 52);
      this.ttb_CustomerName2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CustomerName2.TabIndex = 40;
      this.ttb_CustomerName2.Text = "Customer name";
      this.ttb_CustomerName2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_CustomerName2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustomerName2.TranslateText = true;
      this.ttb_CustomerName2.Value = " ";
      this.ttb_CustomerName2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_OrderNumber2
      // 
      this.ttb_OrderNumber2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_OrderNumber2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_OrderNumber2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_OrderNumber2.ImagePassword = null;
      this.ttb_OrderNumber2.Location = new System.Drawing.Point(17, 21);
      this.ttb_OrderNumber2.MaxChar = 32767;
      this.ttb_OrderNumber2.Multiline = false;
      this.ttb_OrderNumber2.Name = "ttb_OrderNumber2";
      this.ttb_OrderNumber2.PasswordChar = '\0';
      this.ttb_OrderNumber2.ReadOnly = true;
      this.ttb_OrderNumber2.Size = new System.Drawing.Size(114, 52);
      this.ttb_OrderNumber2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_OrderNumber2.TabIndex = 39;
      this.ttb_OrderNumber2.Text = "Order number";
      this.ttb_OrderNumber2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_OrderNumber2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_OrderNumber2.TranslateText = true;
      this.ttb_OrderNumber2.Value = " ";
      this.ttb_OrderNumber2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_OrderNumber2.GotFocus += new System.EventHandler(this.ttb_OrderNumber_Focus);
      // 
      // nud_Width2
      // 
      this.nud_Width2.DecimalPlaces = 0;
      this.nud_Width2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Width2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Width2.ImagePassword = null;
      this.nud_Width2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Width2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_Width2.Location = new System.Drawing.Point(223, 72);
      this.nud_Width2.Margin = new System.Windows.Forms.Padding(2, 3, 3, 1);
      this.nud_Width2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Width2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Width2.Name = "nud_Width2";
      this.nud_Width2.ReadOnly = true;
      this.nud_Width2.Size = new System.Drawing.Size(127, 52);
      this.nud_Width2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Width2.TabIndex = 43;
      this.nud_Width2.Text = "Width";
      this.nud_Width2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Width2.TranslateText = true;
      this.nud_Width2.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Width2.UnitMeasureVisible = false;
      this.nud_Width2.Value = "";
      this.nud_Width2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Width2.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_Width2.Leave += new System.EventHandler(this.nud_Width_Leave);
      // 
      // nud_Offset2
      // 
      this.nud_Offset2.DecimalPlaces = 0;
      this.nud_Offset2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Offset2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Offset2.ImagePassword = null;
      this.nud_Offset2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Offset2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Offset2.Location = new System.Drawing.Point(17, 344);
      this.nud_Offset2.Margin = new System.Windows.Forms.Padding(2, 0, 3, 3);
      this.nud_Offset2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Offset2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Offset2.Name = "nud_Offset2";
      this.nud_Offset2.ReadOnly = true;
      this.nud_Offset2.Size = new System.Drawing.Size(80, 52);
      this.nud_Offset2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Offset2.TabIndex = 52;
      this.nud_Offset2.Text = "Offset";
      this.nud_Offset2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Offset2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Offset2.TranslateText = true;
      this.nud_Offset2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Offset2.Value = "";
      this.nud_Offset2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Offset2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_ScoreGap2
      // 
      this.nud_ScoreGap2.DecimalPlaces = 0;
      this.nud_ScoreGap2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScoreGap2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap2.ImagePassword = null;
      this.nud_ScoreGap2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScoreGap2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap2.Location = new System.Drawing.Point(17, 286);
      this.nud_ScoreGap2.Margin = new System.Windows.Forms.Padding(1, 0, 2, 1);
      this.nud_ScoreGap2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_ScoreGap2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScoreGap2.Name = "nud_ScoreGap2";
      this.nud_ScoreGap2.ReadOnly = true;
      this.nud_ScoreGap2.Size = new System.Drawing.Size(80, 52);
      this.nud_ScoreGap2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_ScoreGap2.TabIndex = 51;
      this.nud_ScoreGap2.Text = "Gap";
      this.nud_ScoreGap2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_ScoreGap2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap2.TranslateText = true;
      this.nud_ScoreGap2.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap2.UnitMeasureVisible = false;
      this.nud_ScoreGap2.Value = "";
      this.nud_ScoreGap2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_ScoreGap2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_DefaultScoreGap2
      // 
      this.nud_DefaultScoreGap2.DecimalPlaces = 0;
      this.nud_DefaultScoreGap2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DefaultScoreGap2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultScoreGap2.ImagePassword = null;
      this.nud_DefaultScoreGap2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DefaultScoreGap2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_DefaultScoreGap2.Location = new System.Drawing.Point(254, 149);
      this.nud_DefaultScoreGap2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.nud_DefaultScoreGap2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_DefaultScoreGap2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DefaultScoreGap2.Name = "nud_DefaultScoreGap2";
      this.nud_DefaultScoreGap2.ReadOnly = true;
      this.nud_DefaultScoreGap2.Size = new System.Drawing.Size(62, 52);
      this.nud_DefaultScoreGap2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_DefaultScoreGap2.TabIndex = 46;
      this.nud_DefaultScoreGap2.Text = "Gap";
      this.nud_DefaultScoreGap2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultScoreGap2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultScoreGap2.TranslateText = true;
      this.nud_DefaultScoreGap2.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_DefaultScoreGap2.UnitMeasureVisible = false;
      this.nud_DefaultScoreGap2.Value = "";
      this.nud_DefaultScoreGap2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultScoreGap2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_DefaultScoreGap2.ValueChanged += new System.EventHandler(this.nud_DefaultScoreGap_ValueChanged);
      this.nud_DefaultScoreGap2.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_DefaultScoreGap2.Leave += new System.EventHandler(this.nud_DefaultScoreGap_Leave);
      // 
      // nud_DefaultOffset2
      // 
      this.nud_DefaultOffset2.DecimalPlaces = 0;
      this.nud_DefaultOffset2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_DefaultOffset2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultOffset2.ImagePassword = null;
      this.nud_DefaultOffset2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_DefaultOffset2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DefaultOffset2.Location = new System.Drawing.Point(321, 149);
      this.nud_DefaultOffset2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.nud_DefaultOffset2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_DefaultOffset2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_DefaultOffset2.Name = "nud_DefaultOffset2";
      this.nud_DefaultOffset2.ReadOnly = true;
      this.nud_DefaultOffset2.Size = new System.Drawing.Size(62, 52);
      this.nud_DefaultOffset2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_DefaultOffset2.TabIndex = 47;
      this.nud_DefaultOffset2.Text = "Offset";
      this.nud_DefaultOffset2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultOffset2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_DefaultOffset2.TranslateText = true;
      this.nud_DefaultOffset2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_DefaultOffset2.Value = "";
      this.nud_DefaultOffset2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_DefaultOffset2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_DefaultOffset2.Leave += new System.EventHandler(this.nud_DefaultOffset_Leave);
      // 
      // nud_Boxes2
      // 
      this.nud_Boxes2.DecimalPlaces = 0;
      this.nud_Boxes2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Boxes2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Boxes2.ImagePassword = null;
      this.nud_Boxes2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Boxes2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Boxes2.Location = new System.Drawing.Point(155, 72);
      this.nud_Boxes2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.nud_Boxes2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Boxes2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Boxes2.Name = "nud_Boxes2";
      this.nud_Boxes2.ReadOnly = true;
      this.nud_Boxes2.Size = new System.Drawing.Size(62, 52);
      this.nud_Boxes2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Boxes2.TabIndex = 42;
      this.nud_Boxes2.Text = "Boxes";
      this.nud_Boxes2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Boxes2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Boxes2.TranslateText = true;
      this.nud_Boxes2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Boxes2.Value = "";
      this.nud_Boxes2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Boxes2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Boxes2.Validating += new System.ComponentModel.CancelEventHandler(this.nud_NotNegativeValues_Validating);
      // 
      // cmb_DefaultType2
      // 
      this.cmb_DefaultType2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_DefaultType2.CanCancel = false;
      this.cmb_DefaultType2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_DefaultType2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_DefaultType2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DefaultType2.ImagePassword = null;
      this.cmb_DefaultType2.Location = new System.Drawing.Point(169, 149);
      this.cmb_DefaultType2.Name = "cmb_DefaultType2";
      this.cmb_DefaultType2.ReadOnly = true;
      this.cmb_DefaultType2.SelectedIndex = -1;
      this.cmb_DefaultType2.SelectedItem = "";
      this.cmb_DefaultType2.SelectedItemData = "";
      this.cmb_DefaultType2.Size = new System.Drawing.Size(82, 52);
      this.cmb_DefaultType2.Sorted = false;
      this.cmb_DefaultType2.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_DefaultType2.TabIndex = 45;
      this.cmb_DefaultType2.Text = "Type";
      this.cmb_DefaultType2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DefaultType2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DefaultType2.TranslateText = true;
      this.cmb_DefaultType2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DefaultType2.SelectedItemChanged += new System.EventHandler(this.cmb_DefaultScoreType_SelectedItemChanged);
      this.cmb_DefaultType2.Leave += new System.EventHandler(this.cmb_DefaultType_Leave);
      // 
      // cmb_DefaultPositioning2
      // 
      this.cmb_DefaultPositioning2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_DefaultPositioning2.CanCancel = false;
      this.cmb_DefaultPositioning2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_DefaultPositioning2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_DefaultPositioning2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DefaultPositioning2.ImagePassword = null;
      this.cmb_DefaultPositioning2.Location = new System.Drawing.Point(17, 149);
      this.cmb_DefaultPositioning2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.cmb_DefaultPositioning2.Name = "cmb_DefaultPositioning2";
      this.cmb_DefaultPositioning2.ReadOnly = true;
      this.cmb_DefaultPositioning2.SelectedIndex = -1;
      this.cmb_DefaultPositioning2.SelectedItem = "";
      this.cmb_DefaultPositioning2.SelectedItemData = "";
      this.cmb_DefaultPositioning2.Size = new System.Drawing.Size(148, 52);
      this.cmb_DefaultPositioning2.Sorted = false;
      this.cmb_DefaultPositioning2.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_DefaultPositioning2.TabIndex = 44;
      this.cmb_DefaultPositioning2.Text = "Positioning";
      this.cmb_DefaultPositioning2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DefaultPositioning2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DefaultPositioning2.TranslateText = true;
      this.cmb_DefaultPositioning2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DefaultPositioning2.SelectedItemChanged += new System.EventHandler(this.cmb_DefaultScorePositioning_SelectedItemChanged);
      this.cmb_DefaultPositioning2.Leave += new System.EventHandler(this.cmb_DefaultPositioning_Leave);
      // 
      // cmb_Level2
      // 
      this.cmb_Level2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Level2.CanCancel = false;
      this.cmb_Level2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Level2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Level2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Level2.ImagePassword = null;
      this.cmb_Level2.Location = new System.Drawing.Point(17, 72);
      this.cmb_Level2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
      this.cmb_Level2.Name = "cmb_Level2";
      this.cmb_Level2.ReadOnly = true;
      this.cmb_Level2.SelectedIndex = -1;
      this.cmb_Level2.SelectedItem = "";
      this.cmb_Level2.SelectedItemData = "";
      this.cmb_Level2.Size = new System.Drawing.Size(134, 52);
      this.cmb_Level2.Sorted = false;
      this.cmb_Level2.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Level2.TabIndex = 41;
      this.cmb_Level2.Text = "Level";
      this.cmb_Level2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Level2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Level2.TranslateText = true;
      this.cmb_Level2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_ord2
      // 
      this.ttl_ord2.BackColor = System.Drawing.Color.Black;
      this.ttl_ord2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ord2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ord2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ord2.Location = new System.Drawing.Point(0, 1);
      this.ttl_ord2.Name = "ttl_ord2";
      this.ttl_ord2.Size = new System.Drawing.Size(384, 454);
      this.ttl_ord2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_ord2.TabIndex = 352;
      this.ttl_ord2.TabStop = false;
      this.ttl_ord2.Text = "ORDER 3";
      this.ttl_ord2.TranslateText = true;
      // 
      // cmb_Shaft2
      // 
      this.cmb_Shaft2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Shaft2.CanCancel = false;
      this.cmb_Shaft2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Shaft2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Shaft2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Shaft2.ImagePassword = null;
      this.cmb_Shaft2.Location = new System.Drawing.Point(17, 403);
      this.cmb_Shaft2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.cmb_Shaft2.Name = "cmb_Shaft2";
      this.cmb_Shaft2.ReadOnly = true;
      this.cmb_Shaft2.SelectedIndex = -1;
      this.cmb_Shaft2.SelectedItem = "";
      this.cmb_Shaft2.SelectedItemData = "";
      this.cmb_Shaft2.Size = new System.Drawing.Size(80, 52);
      this.cmb_Shaft2.Sorted = false;
      this.cmb_Shaft2.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Shaft2.TabIndex = 53;
      this.cmb_Shaft2.Text = "Shaft";
      this.cmb_Shaft2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Shaft2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Shaft2.TranslateText = true;
      this.cmb_Shaft2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // cmb_Type2
      // 
      this.cmb_Type2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Type2.CanCancel = false;
      this.cmb_Type2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Type2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Type2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type2.ImagePassword = null;
      this.cmb_Type2.Location = new System.Drawing.Point(288, 228);
      this.cmb_Type2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.cmb_Type2.Name = "cmb_Type2";
      this.cmb_Type2.ReadOnly = true;
      this.cmb_Type2.SelectedIndex = -1;
      this.cmb_Type2.SelectedItem = "";
      this.cmb_Type2.SelectedItemData = "";
      this.cmb_Type2.Size = new System.Drawing.Size(96, 52);
      this.cmb_Type2.Sorted = false;
      this.cmb_Type2.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Type2.TabIndex = 50;
      this.cmb_Type2.Text = "Type";
      this.cmb_Type2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Type2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type2.TranslateText = true;
      this.cmb_Type2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // nud_Measure2
      // 
      this.nud_Measure2.DecimalPlaces = 0;
      this.nud_Measure2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_Measure2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Measure2.ImagePassword = null;
      this.nud_Measure2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_Measure2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_Measure2.Location = new System.Drawing.Point(17, 228);
      this.nud_Measure2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 1);
      this.nud_Measure2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_Measure2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_Measure2.Name = "nud_Measure2";
      this.nud_Measure2.ReadOnly = true;
      this.nud_Measure2.Size = new System.Drawing.Size(80, 52);
      this.nud_Measure2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Measure2.TabIndex = 48;
      this.nud_Measure2.Text = "Measure";
      this.nud_Measure2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Measure2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Measure2.TranslateText = true;
      this.nud_Measure2.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Measure2.UnitMeasureVisible = false;
      this.nud_Measure2.Value = "";
      this.nud_Measure2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Measure2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Measure2.Validating += new System.ComponentModel.CancelEventHandler(this.nud_Measure_Validating);
      // 
      // ttl_ScorerOptions2
      // 
      this.ttl_ScorerOptions2.BackColor = System.Drawing.Color.Black;
      this.ttl_ScorerOptions2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ScorerOptions2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ScorerOptions2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ScorerOptions2.Location = new System.Drawing.Point(0, 208);
      this.ttl_ScorerOptions2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.ttl_ScorerOptions2.Name = "ttl_ScorerOptions2";
      this.ttl_ScorerOptions2.Size = new System.Drawing.Size(384, 25);
      this.ttl_ScorerOptions2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_ScorerOptions2.TabIndex = 361;
      this.ttl_ScorerOptions2.TabStop = false;
      this.ttl_ScorerOptions2.Text = "SCORER OPTIONS";
      this.ttl_ScorerOptions2.TranslateText = true;
      // 
      // ttl_DefaultScorerOptions2
      // 
      this.ttl_DefaultScorerOptions2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(205)))), ((int)(((byte)(203)))));
      this.ttl_DefaultScorerOptions2.color = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.ttl_DefaultScorerOptions2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DefaultScorerOptions2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_DefaultScorerOptions2.Location = new System.Drawing.Point(9, 130);
      this.ttl_DefaultScorerOptions2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
      this.ttl_DefaultScorerOptions2.Name = "ttl_DefaultScorerOptions2";
      this.ttl_DefaultScorerOptions2.Size = new System.Drawing.Size(375, 71);
      this.ttl_DefaultScorerOptions2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_DefaultScorerOptions2.TabIndex = 360;
      this.ttl_DefaultScorerOptions2.Text = "Default scorer options";
      this.ttl_DefaultScorerOptions2.TranslateText = true;
      // 
      // grv_ScorerOptions2
      // 
      this.grv_ScorerOptions2.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.grv_ScorerOptions2.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grv_ScorerOptions2.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.grv_ScorerOptions2.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grv_ScorerOptions2.AutoGenerateColumns = false;
      this.grv_ScorerOptions2.BackColor = System.Drawing.Color.LightGray;
      this.grv_ScorerOptions2.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.grv_ScorerOptions2.DataMember = "";
      this.grv_ScorerOptions2.DataSource = null;
      this.grv_ScorerOptions2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grv_ScorerOptions2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.grv_ScorerOptions2.ImagePassword = null;
      this.grv_ScorerOptions2.Location = new System.Drawing.Point(102, 284);
      this.grv_ScorerOptions2.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.grv_ScorerOptions2.MultiSelect = false;
      this.grv_ScorerOptions2.Name = "grv_ScorerOptions2";
      this.grv_ScorerOptions2.RowsHeight = 32;
      this.grv_ScorerOptions2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.grv_ScorerOptions2.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.DarkGreen;
      this.grv_ScorerOptions2.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.grv_ScorerOptions2.Size = new System.Drawing.Size(247, 171);
      this.grv_ScorerOptions2.Style = SyncroC.Controls.GridStyles.Light;
      this.grv_ScorerOptions2.TabIndex = 879;
      this.grv_ScorerOptions2.TabStop = false;
      this.grv_ScorerOptions2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.grv_ScorerOptions2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.grv_ScorerOptions2.TranslateText = true;
      this.grv_ScorerOptions2.SelectionChanged += new System.EventHandler(this.grv_ScorerOptions_SelectionChanged);
      // 
      // cmb_Positioning2
      // 
      this.cmb_Positioning2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Positioning2.CanCancel = false;
      this.cmb_Positioning2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Positioning2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Positioning2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning2.ImagePassword = null;
      this.cmb_Positioning2.Location = new System.Drawing.Point(101, 228);
      this.cmb_Positioning2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
      this.cmb_Positioning2.Name = "cmb_Positioning2";
      this.cmb_Positioning2.ReadOnly = true;
      this.cmb_Positioning2.SelectedIndex = -1;
      this.cmb_Positioning2.SelectedItem = "";
      this.cmb_Positioning2.SelectedItemData = "";
      this.cmb_Positioning2.Size = new System.Drawing.Size(182, 52);
      this.cmb_Positioning2.Sorted = false;
      this.cmb_Positioning2.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Positioning2.TabIndex = 49;
      this.cmb_Positioning2.Text = "Positioning";
      this.cmb_Positioning2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning2.TranslateText = true;
      this.cmb_Positioning2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning2.SelectedItemChanged += new System.EventHandler(this.cmb_Positioning_SelectedItemChanged);
      // 
      // NextPageState
      // 
      this.NextPageState.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.NextPageState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.NextPageState.Location = new System.Drawing.Point(681, 55);
      this.NextPageState.Name = "NextPageState";
      this.NextPageState.Size = new System.Drawing.Size(129, 20);
      this.NextPageState.TabIndex = 373;
      this.NextPageState.Text = "Next status: ";
      this.NextPageState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.NextPageState.Visible = false;
      // 
      // nud_SetupLength
      // 
      this.nud_SetupLength.DecimalPlaces = 0;
      this.nud_SetupLength.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_SetupLength.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SetupLength.ImagePassword = null;
      this.nud_SetupLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SetupLength.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_SetupLength.Location = new System.Drawing.Point(346, 228);
      this.nud_SetupLength.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_SetupLength.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SetupLength.Name = "nud_SetupLength";
      this.nud_SetupLength.ReadOnly = true;
      this.nud_SetupLength.Size = new System.Drawing.Size(133, 50);
      this.nud_SetupLength.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_SetupLength.TabIndex = 6;
      this.nud_SetupLength.Text = "SETUP LENGTH";
      this.nud_SetupLength.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SetupLength.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SetupLength.TranslateText = true;
      this.nud_SetupLength.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_SetupLength.UnitMeasureVisible = false;
      this.nud_SetupLength.Value = "";
      this.nud_SetupLength.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SetupLength.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // OfficeId
      // 
      this.OfficeId.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.OfficeId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.OfficeId.Location = new System.Drawing.Point(1152, 57);
      this.OfficeId.Name = "OfficeId";
      this.OfficeId.Size = new System.Drawing.Size(73, 20);
      this.OfficeId.TabIndex = 556;
      this.OfficeId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.OfficeId.Visible = false;
      // 
      // lab_setupModId
      // 
      this.lab_setupModId.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lab_setupModId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lab_setupModId.Location = new System.Drawing.Point(829, 57);
      this.lab_setupModId.Name = "lab_setupModId";
      this.lab_setupModId.Size = new System.Drawing.Size(97, 20);
      this.lab_setupModId.TabIndex = 557;
      this.lab_setupModId.Text = "SetupId+ModId:";
      this.lab_setupModId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lab_setupModId.Visible = false;
      // 
      // lab_officeId
      // 
      this.lab_officeId.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lab_officeId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lab_officeId.Location = new System.Drawing.Point(1078, 58);
      this.lab_officeId.Name = "lab_officeId";
      this.lab_officeId.Size = new System.Drawing.Size(68, 20);
      this.lab_officeId.TabIndex = 558;
      this.lab_officeId.Text = "OfficeId:";
      this.lab_officeId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lab_officeId.Visible = false;
      // 
      // Frm_twin400_orderDetails
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lab_setupModId);
      this.Controls.Add(this.nud_SetupLength);
      this.Controls.Add(this.pnl_OrdersO);
      this.Controls.Add(this.pnl_Orders1);
      this.Controls.Add(this.pnl_Orders2);
      this.Controls.Add(this.ActiveCtrl);
      this.Controls.Add(this.ttb_ProgramNumber);
      this.Controls.Add(this.PageState);
      this.Controls.Add(this.ttb_SuggMax);
      this.Controls.Add(this.ttb_index);
      this.Controls.Add(this.cmb_TrimType);
      this.Controls.Add(this.nud_ScoreGapHeader);
      this.Controls.Add(this.nud_Trim);
      this.Controls.Add(this.nud_WebWidth);
      this.Controls.Add(this.ttb_SuggMin);
      this.Controls.Add(this.ttb_SetUpSequence);
      this.Controls.Add(this.cmb_Flute);
      this.Controls.Add(this.cmb_Quality);
      this.Controls.Add(this.OfficeId);
      this.Controls.Add(this.lab_officeId);
      this.Controls.Add(this.NextPageState);
      this.Controls.Add(this.SetupId);
      this.Controls.Add(this.ModId);
      this.DesignGridVerticalLines = 2;
      this.Location = new System.Drawing.Point(13, -141);
      this.Name = "Frm_twin400_orderDetails";
      this.Tag = "";
      this.Text = "Frm_twin400_orderDetails";
      this.Activated += new System.EventHandler(this.Frm_twin400_orderDetails_Activated);
      this.Controls.SetChildIndex(this.ModId, 0);
      this.Controls.SetChildIndex(this.SetupId, 0);
      this.Controls.SetChildIndex(this.NextPageState, 0);
      this.Controls.SetChildIndex(this.lab_officeId, 0);
      this.Controls.SetChildIndex(this.OfficeId, 0);
      this.Controls.SetChildIndex(this.cmb_Quality, 0);
      this.Controls.SetChildIndex(this.cmb_Flute, 0);
      this.Controls.SetChildIndex(this.ttb_SetUpSequence, 0);
      this.Controls.SetChildIndex(this.ttb_SuggMin, 0);
      this.Controls.SetChildIndex(this.nud_WebWidth, 0);
      this.Controls.SetChildIndex(this.nud_Trim, 0);
      this.Controls.SetChildIndex(this.nud_ScoreGapHeader, 0);
      this.Controls.SetChildIndex(this.cmb_TrimType, 0);
      this.Controls.SetChildIndex(this.ttb_index, 0);
      this.Controls.SetChildIndex(this.ttb_SuggMax, 0);
      this.Controls.SetChildIndex(this.PageState, 0);
      this.Controls.SetChildIndex(this.ttb_ProgramNumber, 0);
      this.Controls.SetChildIndex(this.ActiveCtrl, 0);
      this.Controls.SetChildIndex(this.pnl_Orders2, 0);
      this.Controls.SetChildIndex(this.pnl_Orders1, 0);
      this.Controls.SetChildIndex(this.pnl_OrdersO, 0);
      this.Controls.SetChildIndex(this.nud_SetupLength, 0);
      this.Controls.SetChildIndex(this.lab_setupModId, 0);
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
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.pnl_OrdersO.ResumeLayout(false);
      this.pnl_Orders1.ResumeLayout(false);
      this.pnl_Orders2.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

		/// <summary>
		/// Escape key behaviour: Abort the operation
		/// </summary>
		protected override void Escape()
		{
			if (state == State.Modify || state == State.Insert)
				SetPageState(State.Read, false);
		}

		/// <summary>
		/// Toggle visibility of Debugging informations
		/// </summary>
		protected override void ControlD()
		{
			base.ControlD();
      PageState.Visible =
      NextPageState.Visible =
      SetupId.Visible =
      ModId.Visible =
      ActiveCtrl.Visible =
      //agrassi_FI04-02-09_1
      lab_setupModId.Visible =
      lab_officeId.Visible =
      OfficeId.Visible =
      !PageState.Visible;
		}

		/// <summary>
		/// Allow user to enter a new order.
		/// </summary>
		protected override void CtrlF1()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;
      if (this.online)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_41", true);
        return;
      }
      if (externPast)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_155", true);
        return;
      }
      SetPageState(State.Insert, false);
		}

    /// <summary>
    /// Allow user to enter a new order.
    /// </summary>
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
      if (externPast)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_155", true);
        return;
      }
      SetPageState(State.Insert, true);
    }

    //agrassi_FI11-11-2010_1
    /// <summary>
    /// Allow user to swap orders for the setup.
    /// </summary>
    protected override void CtrlF3()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;
      if (indexOrderToShow == 0)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_01", true);
        return;
      }
      if (indexOrderToShow >= order.Rows.Count)
        return;
      DataRow row = order.Rows[indexOrderToShow];
      if (this.intParse(row[Order.DEL_OUTS_1].ToString()) <= 0)
      {
        Frm_Alert alert = new Frm_Alert();
        alert.ShowDialog(this, dbm.message.getTranslatedMessage("§SYCC_19"));
        return;
      }
      Frm_Swap swapOrders = new Frm_Swap();
      DialogResult ret = swapOrders.ShowDialog(this, row);
      if (ret == DialogResult.OK)
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        string cmd = Command.DB_ORDER + "." + Command.DB_CMD_SWAP_ORDERS + "(" + this.SetupId.Text + ")";
        cm.SendTcp(cm.SYNCROS_DB_MANAGER, cmd);
      }
    }

    /// <summary>
		/// Show previous order.
		/// </summary>
		protected override void PageUp()
		{
			if (indexOrderToShow == 0 || state != State.Read)
				return;
			showOrder(--indexOrderToShow);
      externIndex = this.indexOrderToShow;
      Frm_twin400_toolsSubgroup.externIndex = this.indexOrderToShow;
    }

    /// <summary>
		/// Show next order.
		/// </summary>
		protected override void PageDown()
		{
			if (indexOrderToShow == order.Rows.Count - 1 || state != State.Read)
				return;
			showOrder(++indexOrderToShow);
      externIndex = this.indexOrderToShow;
      Frm_twin400_toolsSubgroup.externIndex = this.indexOrderToShow;
    }

    /// <summary>
		/// Modify.
		/// </summary>
		protected override void CtrlF5()
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;
      if (this.online)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_41", true);
        return;
      }
      if (this.currentUnchangeable && (this.indexOrderToShow == 0))
      {
        PageManager.getPageManager()._SetMessage("§SYCC_01", true);
        return;
      }
      if (externPast)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_155", true);
        return;
      }
      if (this.state != State.Modify)
			  SetPageState(State.Modify, false);
		}

    /// <summary>
    /// Save.
    /// </summary>
    protected override void CtrlF4()
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;
      if (this.online)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_41", true);
        return;
      }
      //AGrassi_FI08/11/2007_1
      if (this.currentUnchangeable && (this.indexOrderToShow == 0) && (this.state == State.Modify))
      {
        PageManager.getPageManager()._SetMessage("§SYCC_01", true);
        return;
      }
      if (externPast)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_155", true);
        return;
      }
      if (state != State.Read)
      {
        //Quick validation
        if (!this.CheckAndUpdateTrim(null, true))
          return;
        if (!this.CheckBoxesWidth(null, true))
          return;

				DataRow modifingRow = null;

        string cmd = Command.DB_ORDER + ".";
        if (state == State.Insert)
          cmd += Command.DB_CMD_ADD;
        else if (state == State.Modify)
				{
					modifingRow = order.Rows.Find(SetupId.Text);
					if (modifingRow == null)
					{
						PageManager.getPageManager()._SetMessage("§SYCC_79", true);
						SetPageState(State.Read);
						return;
					}
					cmd += Command.DB_CMD_MOD;
				}
				cmd += "(";
        //SetupCode formatting
        string SetupCode = ttb_ProgramNumber.Value;
        while (ttb_SetUpSequence.Value.Length < 2)
          ttb_SetUpSequence.Value = "0" + ttb_SetUpSequence.Value;
        SetupCode += ttb_SetUpSequence.Value;
        ScoreMeasure defValues=new ScoreMeasure();

        //compose and send the command
        //HEADER
        if ((state == State.Modify) /*&& (this.indexOrderToShow >= 0)  && (this.indexOrderToShow<order.Rows.Count)*/)
					cmd += this.SetupId.Text + Command.PARAMETER_SEPARATOR;//0 SetupID	
        else if (state == State.Insert)
          cmd += " " + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(SetupCode) + Command.PARAMETER_SEPARATOR;								//1 SetupCode
        cmd += this.NotEmptyString(this.nud_WebWidth.Value) + Command.PARAMETER_SEPARATOR;	//2 WebWidth
				if (this.cmb_TrimType.SelectedItemData != string.Empty)	//4 TrimType
          cmd += Enum.Format(typeof(SyncroC.Utility.TrimType), Enum.Parse(typeof(SyncroC.Utility.TrimType), this.cmb_TrimType.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR; 
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.nud_ScoreGapHeader.Value) + Command.PARAMETER_SEPARATOR;    //4 ScoreGap
        cmd += this.NotEmptyString(this.cmb_Quality.Value) + Command.PARAMETER_SEPARATOR;			//5 QualityID
				//RFAZZI_28/01/2009_1.Sent to SyncroS the "Quality Weight" and "Quality Thickness" when a order is inserted or modified. Required for Setup Validation . 
				DataRow qualityRow = DBManager.getDBManager().quality.getRow(this.cmb_Quality.Value);
        //agrassi_FI05-10-2010_1
        cmd += this.NotEmptyString(this.cmb_Flute.Value) + Command.PARAMETER_SEPARATOR;		//5bis FluteID
        //qualityRow = quality.Rows.Find(this.cmb_Quality.Value);
        if (qualityRow == null)
        {
          PageManager.getPageManager()._SetMessage("§SYCC_79", true);
          SetPageState(State.Read);
          return;
        }
				cmd += this.NotEmptyString(qualityRow[Quality.WEIGHT_FACTOR].ToString()) + Command.PARAMETER_SEPARATOR;			//6 Quality Weight
				cmd += this.NotEmptyString(qualityRow[Quality.THICKNESS_UDdmm].ToString()) + Command.PARAMETER_SEPARATOR;			//7 Quality Thickness
				
				cmd += this.NotEmptyString(this.nud_Trim.Value) + Command.PARAMETER_SEPARATOR;    		//8 Trim

        //ORDERS
        for (int i = 0; i < NUM_ORDERS_IN_SETUP; i++)
        {
          cmd += this.NotEmptyString(this.ttb_OrderNumber[i].Value) + Command.PARAMETER_SEPARATOR;	//9 OrderNumber0
          //if (this.indexOrderToShow >= 0 && this.indexOrderToShow < order.Rows.Count)
					if (state == State.Modify)
            cmd += this.NotEmptyString(SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeString((DateTime)this.GetSetupObject(modifingRow, this.orderDbAcronym+"_DELIVERY_", i))) + Command.PARAMETER_SEPARATOR;			//10 Delivery0
          else
            cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += this.NotEmptyString(this.ttb_CustomerName[i].Value) + Command.PARAMETER_SEPARATOR;//11 customerName0
          cmd += this.NotEmptyString(this.nud_Width[i].Value) + Command.PARAMETER_SEPARATOR;    				  //12 SheetWidth0
          cmd += this.NotEmptyString(this.nud_Boxes[i].Value) + Command.PARAMETER_SEPARATOR;    					//13 Outs0
          defValues.SetValues(i, this.cmb_DefaultPositioning[i].SelectedItemData, this.cmb_DefaultType[i].SelectedItem,
                              this.nud_DefaultScoreGap[i].Value, this.nud_DefaultOffset[i].Value, ScorerShaft.Auto);
          cmd += ScoreMeasure.ComposeScoreMeasures(this.measures[i], defValues).Replace('\n', ' ') + Command.PARAMETER_SEPARATOR; //14 scoreMeasures
          cmd += this.NotEmptyString(this.cmb_DefaultType[i].Value) + Command.PARAMETER_SEPARATOR;    		//15 ScoreType0
          if (this.cmb_DefaultPositioning[i].SelectedItemData != string.Empty)		//16 PositType0
            cmd += Enum.Format(typeof(SyncroC.Utility.PositType), Enum.Parse(typeof(SyncroC.Utility.PositType), this.cmb_DefaultPositioning[i].SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
          else
            cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += this.NotEmptyString(this.nud_DefaultOffset[i].Value) + Command.PARAMETER_SEPARATOR;		//17 TeleTwinOffset0
          cmd += this.NotEmptyString(this.nud_DefaultScoreGap[i].Value) + Command.PARAMETER_SEPARATOR;	//18 ScoreGap0
          if (this.cmb_Level[i].SelectedItemData != string.Empty)		//19 LevelName0
            cmd += Enum.Format(typeof(SyncroC.Utility.LevelName), Enum.Parse(typeof(SyncroC.Utility.LevelName), this.cmb_Level[i].SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
          else
            cmd += " " + Command.PARAMETER_SEPARATOR;
          //if (this.indexOrderToShow >= 0 && this.indexOrderToShow < order.Rows.Count)
					if (state == State.Modify)
          {
            string sheetLength = this.GetSetupValue(modifingRow, this.orderDbAcronym + "_SHEET_LENGTH_", i);
            if (this.intParse(sheetLength) <= 0)
              sheetLength = "10000";
            cmd += this.NotEmptyString(sheetLength) + Command.PARAMETER_SEPARATOR;	//20 sheetLength
						//RFazzi_FI01/04/2008_1.Added control in the Twin400 insert order page for send to the server a valorized Segment_Sheet:Count value
						//cmd += this.NotEmptyString(this.GetSetupValue(modifingRow, this.orderDbAcronym + "_SEGMENT_SHEET_COUNT_", i)) + Command.PARAMETER_SEPARATOR;	//19 sheetCount
						string segmentSheetCount = this.GetSetupValue(modifingRow, this.orderDbAcronym + "_SEGMENT_SHEET_COUNT_", i);
						if (this.intParse(segmentSheetCount) <= 0)
							segmentSheetCount = "300";
            //agrassi_FI04-03-09_1
            int setupLength_UDm = this.intParse(this.nud_SetupLength.Value);
            int outs = this.intParse(this.nud_Boxes[i].Value);
            if ((setupLength_UDm > 0) && (outs > 0))
            {
              segmentSheetCount = (((int)SyncroConverter.Convert(setupLength_UDm, UnitMeasure.Metre, UnitMeasure.DeciMilliMetre)) * outs / this.intParse(sheetLength)).ToString();
            }
            cmd += this.NotEmptyString(segmentSheetCount) + Command.PARAMETER_SEPARATOR;	//21 sheetCount
						cmd += this.NotEmptyString(this.GetSetupValue(modifingRow, this.orderDbAcronym + "_SHEETS_PER_STACK_", i)) + Command.PARAMETER_SEPARATOR;			//22 sheetPerStack
						cmd += this.NotEmptyString(this.GetSetupValue(modifingRow, this.orderDbAcronym + "_STACKS_PER_PALLET_", i)) + Command.PARAMETER_SEPARATOR;		//23 stacksPerPallet
            cmd += this.NotEmptyString(this.GetSetupValue(modifingRow, this.orderDbAcronym + "_SENDING_", i)) + Command.PARAMETER_SEPARATOR;							//24 sending
            cmd += this.NotEmptyString(this.GetSetupValue(modifingRow, this.orderDbAcronym + "_CUT_TO_MARK_", i)) + Command.PARAMETER_SEPARATOR;					//25 cut to mark
          }
          else
          {
            //agrassi_FI04-03-09_1
            int sheetLength_UDdmm = 10000;
            int setupLength_UDm=this.intParse(this.nud_SetupLength.Value);
            int outs=this.intParse(this.nud_Boxes[i].Value);
            int segmentSheetCount = 300;
            if ((setupLength_UDm>0) && (outs>0))
            {
              segmentSheetCount = ((int)SyncroConverter.Convert(setupLength_UDm, UnitMeasure.Metre, UnitMeasure.DeciMilliMetre)) * outs / sheetLength_UDdmm;
            }
            //agrassi_FI04-03-09_1
            //cmd += "10000" + Command.PARAMETER_SEPARATOR;
            //cmd += "100" + Command.PARAMETER_SEPARATOR;
            cmd += sheetLength_UDdmm.ToString() + Command.PARAMETER_SEPARATOR;
            //RFazzi_FI01/04/2008_1.Added control in the Twin400 insert order page for send to the server a valorized Segment_Sheet:Count value
            cmd += segmentSheetCount.ToString() + Command.PARAMETER_SEPARATOR;
            cmd += " " + Command.PARAMETER_SEPARATOR;
            cmd += " " + Command.PARAMETER_SEPARATOR;
            cmd += " " + Command.PARAMETER_SEPARATOR;
            cmd += " " + Command.PARAMETER_SEPARATOR;
          }
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
          //agrassi_FI24-02-2010_3
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
          //agrassi_FI02-09-2010_1
          cmd += "false" + Command.PARAMETER_SEPARATOR;
        }
        cmd += ")";
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_DB_MANAGER, cmd);
				orderAddRequested = true;
      }
    }

    /// <summary>
    /// Clean suggestions field.
		/// </summary>
		private void CleanSuggestionFields()
		{
			if (state == State.Insert || state == State.Modify)
			{
				ttb_SuggMin.Value = String.Empty;
				ttb_SuggMax.Value = String.Empty;
			}
		}

		/// <summary>
		/// Set the suggestion field values only if the state is Insert or Modify
		/// </summary>
		/// <param name="min">value to be set in the min suggestion.</param>
		/// <param name="max">value to be set in the max suggestion.</param>
		private void SetSuggestionFields(string min, string max)
		{
			if (state == State.Insert || state == State.Modify)
			{
				ttb_SuggMin.Value = min;
				ttb_SuggMax.Value = max;
			}
		}

		/// <summary>
		/// Set the state of the page
		/// </summary>
		/// <param name="newState">new state the page has to assume</param>
		private void SetPageState(State newState)
		{
			SetPageState(newState, false);
		}

    private void SetPageState(State newState, bool dontCleanAnyFields)
		{
      this.nextState = newState;
      NextPageState.Text = "Next status: " + Enum.Format(typeof(State), nextState, "g");
      switch (newState)
      {
				case State.Read:
					showOrder(indexOrderToShow);
					readOnly(true);
					lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
						lbl_CtrlF2.BackColor = lbl_CtrlF2_YY.BackColor =
						lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
					break;
				case State.Insert:
          if (dontCleanAnyFields)
          {
            ttb_index.Value = "";
            resetAllErrors();
						lbl_CtrlF2.BackColor = lbl_CtrlF2_YY.BackColor = lbl_Status.BackColor;
          }
          else
          {
            cleanAllFields();
            this.SelectFirstItem(-1);
						lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor = lbl_Status.BackColor;
          }
          this.PutScoreOptionsInModify();
          readOnly(false);
          break;
				case State.Modify:
          this.PutScoreOptionsInModify();
          readOnly(false);
          this.oldScoreGapHeader = this.intParse(this.nud_ScoreGapHeader.Value);
          for (int i = 0; i < NUM_ORDERS_IN_SETUP; i++)
          {
            this.oldDefaultScoreGap[i] = this.intParse(this.nud_DefaultScoreGap[i].Value);
            if (this.cmb_DefaultType[i].Value.Length>0)
              this.oldDefaultScoreType[i] = this.cmb_DefaultType[i].Value.ToCharArray()[0];
            if (this.cmb_DefaultPositioning[i].SelectedIndex>=0)
              this.oldDefaultPositType[i] = (PositType)Enum.Parse(typeof(PositType), this.cmb_DefaultPositioning[i].SelectedItemData);
          }
					lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
          break;
			}
			CleanSuggestionFields();
			resetAllErrors();
			this.state = newState;
      externState = newState;
      if (state==State.Read)
        this.tab_Tab2.Focus();
      else
        this.ttb_ProgramNumber.Focus();
      PageState.Text = "Page status: " + Enum.Format(typeof(State), state, "g");
    }

    private void SelectFirstItem(int ind)
    {
      if (ind == -1)
      {
        ttb_index.Value = String.Empty;
        this.cmb_Flute.SelectedIndex = 0;
        this.cmb_Quality.SelectedIndex = 0;
        this.cmb_TrimType.SelectedIndex = 0;
      }
      for (int i=0; i<NUM_ORDERS_IN_SETUP; i++)
      {
        if ((i == ind) || (ind == -1))
        {
          this.cmb_Level[i].SelectedIndex = 0;
          this.cmb_DefaultPositioning[i].SelectedIndex = 0;
          this.cmb_DefaultType[i].SelectedIndex = 0;
          this.cmb_Positioning[i].SelectedIndex = 0;
          this.cmb_Type[i].SelectedIndex = 0;
          this.cmb_Shaft[i].SelectedIndex = 0;
        }
      }
    }

    private void PutScoreOptionsInModify()
    {
      for (int i=0; i<NUM_ORDERS_IN_SETUP; i++)
      {
        if ((this.grv_ScorerOptions[i].Rows.Count <= 1) && (this.nextState == State.Modify))
          this.SetDefault(i);
        this.grv_ScorerOptions[i].Rows.Add();
        if (measures[i] == null)
          this.measures[i] = new ArrayList();
        this.measures[i].Add(new ScoreMeasure());
        this.grv_ScorerOptions[i].Rows[0].Selected = true;
      }
    }

	
		/// <summary>
		/// Calculate the trim accordingli to the currently insertted width.
		/// </summary>
		/// <returns></returns>
    private bool CheckAndUpdateTrim(CancelEventArgs e, bool final)
    {
			int trim=intParse(nud_WebWidth.Value) -
        (intParse(this.nud_Width[0].Value)*intParse(this.nud_Boxes[0].Value) +
         intParse(this.nud_Width[1].Value) * intParse(this.nud_Boxes[1].Value) + 
         intParse(this.nud_Width[2].Value)*intParse(this.nud_Boxes[2].Value));
      this.nud_Trim.Value = trim.ToString();
      bool noTrim = false;
      if ((this.cmb_TrimType.SelectedItemData != null) && (this.cmb_TrimType.SelectedItemData != ""))
      {
        try
        {
          TrimType tt = (TrimType)Enum.Parse(typeof(TrimType), this.cmb_TrimType.SelectedItemData);
          noTrim = (tt == TrimType.NoTrim);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, ex.Message);
          noTrim = false;
        }
      }
			int minimumTrim_UDdmm = (noTrim ? 0 : this.ss.SlitterScorerHeader.MinTrim);
			int maximumTrim_UDdmm = (noTrim ? 0 : this.ss.SlitterScorerHeader.MaxTrim);
      int minimumTrim_UDmm = (int)SyncroConverter.Convert(minimumTrim_UDdmm, this.nud_Trim.InternalUnitMeasure, this.nud_Trim.UnitMeasure);
      int maximumTrim_UDmm = (int)SyncroConverter.Convert(maximumTrim_UDdmm, this.nud_Trim.InternalUnitMeasure, this.nud_Trim.UnitMeasure);
      bool under = (trim < minimumTrim_UDdmm), over = (trim > maximumTrim_UDdmm);
      bool res=(under || (final && over));
      if (res)
      {
        if (under)
					PageManager.getPageManager()._SetMessage("§SYCC_03§SYCC_04#" + minimumTrim_UDmm.ToString(), true);
        else
          PageManager.getPageManager()._SetMessage("§SYCC_03§SYCC_05#" + maximumTrim_UDmm.ToString(), true);
        if (e!=null) e.Cancel = true;
				this.nud_Trim.SetError("§SYCC_06");
      }
      else
        this.nud_Trim.SetError("");
      return !res;
    }

    private bool CheckBoxesWidth(CancelEventArgs e, bool final)
    {
			SyncroC.DataBase.Message message = DBManager.getDBManager().message;
      int boxWidth, ordNum=-1;
      bool[] boxOk=new bool[NUM_ORDERS_IN_SETUP];
      bool boxOK;
      if ((this.measures[0] == null) || (this.measures[1] == null) || (this.measures[2] == null))
        return true;
      for (int i=0; i<NUM_ORDERS_IN_SETUP; i++)
      {
        boxWidth = 0;
        for (int m = 0; m < this.measures[i].Count; m++)
          boxWidth += ((ScoreMeasure)this.measures[i][m]).measure;
        boxOk[i] = (boxWidth==intParse(this.nud_Width[i].Value));
			  string text = boxOk[i] ? string.Empty : message.getTranslatedMessage("§SYCC_15");
        this.nud_Width[i].SetError(text);
        if (!boxOk[i]) ordNum = i;
      }
      boxOK = boxOk[0] && boxOk[1] && boxOk[2];
      if (!boxOK)
      {
        if ((e != null) && (!final))
          e.Cancel = true;
        else
					PageManager.getPageManager()._SetMessage("§SYCC_14#" + (ordNum + 1).ToString() + "§SYCC_15", true);
      }
      return boxOK;
    }

    private void FillInMeasure(int ord, int ind, int meas, PositType posType, char scoreType, int gap, int ttwOffset, ScorerShaft sh)
    {
      if (ord < this.measures.Length)
      {
        //agrassi_FI28-07-08_1
        //if (ind < this.measures[ord].Count)
        if (ind == this.measures[ord].Count)
          this.measures[ord].Add(new ScoreMeasure());
        else if (ind > this.measures[ord].Count)
          return;
        ((ScoreMeasure)this.measures[ord][ind]).measure = meas;
        ((ScoreMeasure)this.measures[ord][ind]).positType = posType;
        ((ScoreMeasure)this.measures[ord][ind]).scoreTypeLetter = scoreType;
        ((ScoreMeasure)this.measures[ord][ind]).gap = gap;
        ((ScoreMeasure)this.measures[ord][ind]).offset = ttwOffset;
        ((ScoreMeasure)this.measures[ord][ind]).scorerShaft = sh;
      }
    }

		private void showIndex(int index)
		{
			if (index < 0)
			{
				index = 0;
			}
			if (index >= order.Rows.Count)
			{
				index = order.Rows.Count - 1;
			}
			this.ttb_index.Value = order.Rows[index][Order.DEL_POSITION].ToString();
		}

    /// <summary>
    /// Show the specified order into the screen.
    /// </summary>
    /// <param name="index">Index of the order to be showed.</param>
    private void showOrder(int index)
    {
      long Count = DBManager.getDBManager().pastOrder.getCount();
      cleanAllFields();
			if (((order.Rows.Count == 0) && (!externPast)) || ((Count == 0) && externPast))
			{
				Logger.LogError(this, "showOrder - Order queue is empty.");
				return;
			}
			if (index < 0)
			{//todo: rimuovere il log sotto.
				Logger.LogError(this, "showOrder - invalid parameter index: no order at index " + index + "; the order at index 0 will be showed.");
				index = 0;
			}
			if (((index >= order.Rows.Count) && (!externPast)) || ((index >= Count) && externPast))
			{//todo: rimuovere il log sotto.
				Logger.LogError(this, "showOrder - invalid parameter index: no order at index " + index + "; the order at index " + (order.Rows.Count - 1).ToString() + " will be showed.");
				index = order.Rows.Count - 1;
			}

      DataRow row = null;
      if (!externPast)
        row = order.Rows[index];
      else
				//RFAZZI_FI31/07/2009_1. Corretto problema di visualizzazione dettaglio ordine eseguito da Pagine TW4.
				//row = DBManager.getDBManager().pastOrder.getRow(index); //pastOrder.Rows[index];
				row = DBManager.getDBManager().pastOrder.getRow(index + 1); //pastOrder.Rows[index];

      //ttb_index.Value = (index + 1).ToString();
      Font fBase = this.ttb_index.Font;
      Font f = null;
      if (externPast)
      {
        f = new Font(fBase.FontFamily, 20, fBase.Style);
        this.ttb_index.Value = row[PastOrder.POL_FINISH_TIME].ToString();
      }
      else
      {
        f = new Font(fBase.FontFamily, 48, fBase.Style);
        this.ttb_index.Value = row[Order.DEL_POSITION].ToString();
      }
      this.ttb_index.Font = f;

      SyncroColor cFrame = externPast ? SyncroColor.AOCInProgressColor : SyncroColor.Green;
      ttb_index.ForeColor = ColConv.Convert(SyncroColor.Black);
      ttb_index.TitleBackColor = cFrame;
      ttb_index.FrameColor = cFrame;
      if (!row[Order.DEL_VALID].ToString().Equals(true.ToString()))
        ttb_index.ForeColor = ColConv.Convert(SyncroColor.AlarmRed);

			PositType defaultPositType;
			char defaultScoreLetter;
			int defaultGap;
			int defaultOffset;
			ScorerShaft defaultScorerShaft;

			//HEADER
			this.SetupId.Text = row[Order.DEL_SETUP_ID].ToString();
			this.ModId.Text = row[Order.DEL_SETUP_MODID].ToString();
      //agrassi_FI04-02-09_1
      this.OfficeId.Text = row[Order.DEL_OFFICE_ID].ToString();
			string setUpCode = row[Order.DEL_SETUP_CODE].ToString();
			ttb_ProgramNumber.Value = setUpCode.Substring(0, setUpCode.Length - 2);
			ttb_SetUpSequence.Value = setUpCode.Substring(setUpCode.Length - 2);
			this.nud_WebWidth.Value = row[Order.DEL_WEB_WIDTH].ToString();
			this.cmb_TrimType.SelectedItemData=row[Order.DEL_TRIM_TYPE].ToString();
			this.nud_ScoreGapHeader.Value = row[Order.DEL_SCORE_GAP].ToString();
      
      //DataRow qualityRow = this.quality.Rows.Find(row[Order.DEL_QUALITY_ID].ToString());
      DataRow qualityRow = DBManager.getDBManager().quality.getRow(row[Order.DEL_QUALITY_ID].ToString());
      //agrassi_FI05-10-2010_1
      if (row[Order.DEL_FLUTE_ID].ToString().Trim() != String.Empty)
        this.cmb_Flute.SelectedItem = row[Order.DEL_FLUTE_ID].ToString();
      else if (qualityRow != null)
        this.cmb_Flute.SelectedItem = qualityRow[Quality.FLUTE_ID].ToString();

      this.cmb_Quality.SelectedItem = row[Order.DEL_QUALITY_ID].ToString();
      this.nud_Trim.Value = row[Order.DEL_TRIM].ToString();
      int cutCount = this.intParse(row[Order.DEL_SEGMENT_SHEET_COUNT_0].ToString()) / this.intParse(row[Order.DEL_OUTS_0].ToString());
      int setupLength = (int)Math.Round((double)SyncroConverter.Convert((decimal)(this.intParse(row[Order.DEL_SHEET_LENGTH_0].ToString())), UnitMeasure.DeciMilliMetre, UnitMeasure.Metre) * (double)cutCount);
      this.nud_SetupLength.Value = setupLength.ToString();

      for (int i=0; i<NUM_ORDERS_IN_SETUP; i++)
      {
        if (int.Parse(this.GetSetupValue(row, this.orderDbAcronym+"_OUTS_", i)) > 0)
        {
          ttl_ord[i].ForeColor = Color.LightGreen;
          this.ttb_OrderNumber[i].Value = this.GetSetupValue(row, this.orderDbAcronym + "_ORDER_NUMBER_", i);
          this.ttb_CustomerName[i].Value = this.GetSetupValue(row, this.orderDbAcronym + "_CUSTOMER_NAME_", i);
          this.cmb_Level[i].SelectedItemData = this.GetSetupValue(row, this.orderDbAcronym + "_LEVEL_NAME_", i);
          this.nud_Boxes[i].Value = this.GetSetupValue(row, this.orderDbAcronym + "_OUTS_", i);
          this.nud_Width[i].Value = this.GetSetupValue(row, this.orderDbAcronym + "_SHEET_WIDTH_", i);

          string positType=this.GetSetupValue(row, this.orderDbAcronym+"_POSIT_TYPE_",i);
          string scoreType = this.GetSetupValue(row, this.orderDbAcronym + "_SCORE_TYPE_", i);
          string scoreGap = this.GetSetupValue(row, this.orderDbAcronym + "_SCORE_GAP_", i);
          string ttwOffset=this.GetSetupValue(row, this.orderDbAcronym+"_TELETWIN_OFFSET_",i);
          this.cmb_DefaultPositioning[i].SelectedItemData = positType;
          this.cmb_DefaultType[i].SelectedItem = scoreType;
          this.nud_DefaultScoreGap[i].Value = scoreGap;
          this.nud_DefaultOffset[i].Value = ttwOffset;

          try
          {
            defaultPositType = (PositType)Enum.Parse(typeof(PositType), positType);
            defaultScoreLetter = (scoreType.Length > 0) ? scoreType[0] : ' ';
            defaultGap = this.intParse(scoreGap);
            defaultOffset = this.intParse(ttwOffset);
            defaultScorerShaft = ScorerShaft.Auto;
          }
          catch (Exception e)
          {
            Logger.LogError(this, "showOrder - error parsing defaults (order " + i + ")", e);
            defaultPositType = PositType.NoPositType;
            defaultScoreLetter = ' ';
            defaultGap = 0;
            defaultOffset = 0;
            defaultScorerShaft = ScorerShaft.Auto;
          }
          measures[i] = ScoreMeasure.ParseMeasures(this.GetSetupValue(row, this.orderDbAcronym+"_SCORE_MEASURES_", i),
            defaultPositType,
            defaultScoreLetter,
            defaultGap,
            defaultOffset,
            defaultScorerShaft);

          this.grv_ScorerOptions[i].Rows.Clear();
          for (int j = 0; j < measures[i].Count; j++)
          {
            //agrassi_FI28-07-08_1
            //object[] rowOv = new object[] {
            //      SyncroConverter.Convert(((ScoreMeasure)measures[i][j]).measure, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre),
            //      this.ScoreOverrides(j, i, defaultPositType, defaultScoreLetter, defaultGap, defaultOffset)};
            object[] rowOv = this.ScoreOverrides((int)SyncroConverter.Convert(((ScoreMeasure)measures[i][j]).measure, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre),
                                                  i, j, defaultPositType, defaultScoreLetter, defaultGap, defaultOffset, defaultScorerShaft);
            if (rowOv != null)
            {
              try
              {
                this.grv_ScorerOptions[i].Rows.Add(rowOv);
              }
              catch (Exception ex)
              {
                Logger.LogError(this, "rowOv.Length=" + rowOv.Length + ", rowOv[i]=" + rowOv[i].ToString(), ex);
              }
            }
          }
          if (grv_ScorerOptions[i].Rows.Count > 0)
            grv_ScorerOptions[i].Rows[0].Selected = true;
        }
      }
    }

    //private string ScoreOverrides(int ind, int ord, PositType defPosType, char defScorer, int defGap, int defOffset)
    //{
      //agrassi_FI28-07-08_1
      //string overrides = " ";
      //if ((this.state!=State.Read) || (ind < measures[ord].Count - 1))
      //{
      //  overrides += (defPosType.Equals(((ScoreMeasure)measures[ord][ind]).positType)) ? string.Empty : POSITIONING_OVERRIDDEN;
      //  overrides += (defScorer.Equals(((ScoreMeasure)measures[ord][ind]).scoreTypeLetter)) ? string.Empty : SCORETYPE_OVERRIDDEN;
      //  overrides += (defGap.Equals(((ScoreMeasure)measures[ord][ind]).gap)) ? string.Empty : GAP_OVERRIDDEN;
      //  overrides += (defOffset.Equals(((ScoreMeasure)measures[ord][ind]).offset)) ? string.Empty : TELETWINOFFSET_OVERRIDDEN;
      //  overrides += (((ScoreMeasure)measures[ord][ind]).scorerShaft == ScorerShaft.Auto) ? string.Empty : AXIS_OVERRIDDEN;
      //}
      //return overrides;
    //}
    private object[] ScoreOverrides(int measure, int ord, int ind, PositType defPosType, char defScorer, int defGap, int defOffset, ScorerShaft defShaft)
    {
      int MAX_NUM_OPTIONS = 4;
      object[] overrides = new object[MAX_NUM_OPTIONS + 1];
      int optionIndex = 0;
      overrides[optionIndex++] = measure;
      if ((this.state != State.Read) || (ind < measures[ord].Count - 1))
      {
        if (measure > 0)
        {
          if (!defScorer.Equals(((ScoreMeasure)measures[ord][ind]).scoreTypeLetter))
            overrides[optionIndex++] = global::SyncroC.Properties.Resources.OptionType;
          if (!defPosType.Equals(((ScoreMeasure)measures[ord][ind]).positType))
            overrides[optionIndex++] = global::SyncroC.Properties.Resources.OptionPositioning;
          if (!defGap.Equals(((ScoreMeasure)measures[ord][ind]).gap))
            overrides[optionIndex++] = global::SyncroC.Properties.Resources.OptionGap;
          if (!defShaft.Equals(((ScoreMeasure)measures[ord][ind]).scorerShaft))
            overrides[optionIndex++] = global::SyncroC.Properties.Resources.OptionShaft;
        }
      }
      return overrides;
    }

		/// <summary>
		/// Clean the value of every component of the page.
		/// </summary>
		private void cleanAllFields()
		{
			resetAllErrors();
			CleanSuggestionFields();
			SetupId.Text = string.Empty;
			ModId.Text = string.Empty;
      //agrassi_FI04-02-09_1
      OfficeId.Text = string.Empty;

			//HEADER
			ttb_ProgramNumber.Value = String.Empty;
			ttb_SetUpSequence.Value = String.Empty;
      cmb_Flute.SelectedIndex = -1;
      cmb_Quality.SelectedIndex = -1;
      cmb_TrimType.SelectedIndex = -1;
      nud_WebWidth.Value = String.Empty;
      nud_ScoreGapHeader.Value = String.Empty;
			nud_Trim.Value = String.Empty;
      nud_SetupLength.Value = string.Empty;
      for (int i=0; i<NUM_ORDERS_IN_SETUP; i++)
      {
        ttl_ord[i].ForeColor = Color.WhiteSmoke;
        ttb_OrderNumber[i].Value = string.Empty;
        ttb_CustomerName[i].Value = string.Empty;
        cmb_Level[i].UnSelectItem();
        nud_Boxes[i].Value = String.Empty;
			  nud_Width[i].Value = String.Empty;
        cmb_DefaultPositioning[i].UnSelectItem();
        cmb_DefaultType[i].UnSelectItem();
        nud_DefaultScoreGap[i].Value = String.Empty;
        nud_DefaultOffset[i].Value = String.Empty;
        nud_Measure[i].Value = String.Empty;
        cmb_Positioning[i].UnSelectItem();
        cmb_Type[i].UnSelectItem();
        nud_ScoreGap[i].Value = String.Empty;
        nud_Offset[i].Value = String.Empty;
        grv_ScorerOptions[i].Rows.Clear();
        measures[i] = null;
      }
    }

    /// <summary>
		/// Reset all the error providers
		/// </summary>
		private void resetAllErrors()
		{
			//HEADER
      ttb_ProgramNumber.SetError(string.Empty);
      ttb_SetUpSequence.SetError(string.Empty);
      nud_WebWidth.SetError(string.Empty);
      nud_ScoreGapHeader.SetError(string.Empty);
			cmb_Flute.SetError(String.Empty);
			cmb_Quality.SetError(String.Empty);
			cmb_TrimType.SetError(String.Empty);
			nud_Trim.SetError(String.Empty);
      nud_SetupLength.SetError(string.Empty);
      for (int i=0; i<NUM_ORDERS_IN_SETUP; i++)
      {
        ttb_OrderNumber[i].SetError(string.Empty);
        ttb_CustomerName[i].SetError(string.Empty);
        cmb_Level[i].SetError(String.Empty);
			  nud_Boxes[i].SetError(String.Empty);
			  nud_Width[i].SetError(String.Empty);
			  nud_Measure[i].SetError(String.Empty);
			  cmb_DefaultPositioning[i].SetError(String.Empty);
			  cmb_DefaultType[i].SetError(String.Empty);
			  nud_DefaultScoreGap[i].SetError(String.Empty);
			  nud_DefaultOffset[i].SetError(String.Empty);
			  cmb_Type[i].SetError(String.Empty);
			  cmb_Positioning[i].SetError(String.Empty);
			  nud_ScoreGap[i].SetError(String.Empty);
			  cmb_Shaft[i].SetError(String.Empty);
			  nud_Offset[i].SetError(String.Empty);
      }
		}

		/// <summary>
		/// Set di ReadOnly property of every components to the isRedonly parameter value 
		/// and the CausesValidation property of every components to the ! isReadOnly parameter value
		/// </summary>
		/// <param name="isReadOnly"></param>
		private void readOnly(bool isReadOnly)
		{
			//readonly
			//header
      ttb_ProgramNumber.ReadOnly = ttb_SetUpSequence.ReadOnly =
      cmb_Flute.ReadOnly = cmb_Quality.ReadOnly =
      nud_WebWidth.ReadOnly =
      nud_ScoreGapHeader.ReadOnly = cmb_TrimType.ReadOnly =
      nud_SetupLength.ReadOnly = isReadOnly;
      for (int i=0; i<NUM_ORDERS_IN_SETUP; i++)
      {
        ttb_OrderNumber[i].ReadOnly = ttb_CustomerName[i].ReadOnly =
        cmb_Level[i].ReadOnly = nud_Boxes[i].ReadOnly =
        nud_Measure[i].ReadOnly = nud_Width[i].ReadOnly =
        cmb_DefaultPositioning[i].ReadOnly = cmb_DefaultType[i].ReadOnly =
        nud_DefaultScoreGap[i].ReadOnly = nud_DefaultOffset[i].ReadOnly =
        cmb_Type[i].ReadOnly = cmb_Positioning[i].ReadOnly =
        nud_ScoreGap[i].ReadOnly = cmb_Shaft[i].ReadOnly =
        nud_Offset[i].ReadOnly = isReadOnly;
      }

      //TabStop
			//header
      ttb_ProgramNumber.TabStop = ttb_SetUpSequence.TabStop =
      cmb_Flute.TabStop = cmb_Quality.TabStop =
      nud_WebWidth.TabStop =
      nud_ScoreGapHeader.TabStop = cmb_TrimType.TabStop =
      nud_SetupLength.TabStop = !isReadOnly;
      for (int i=0; i<NUM_ORDERS_IN_SETUP; i++)
      {
        ttb_OrderNumber[i].TabStop = ttb_CustomerName[i].TabStop =
        cmb_Level[i].TabStop = nud_Boxes[i].TabStop =
        nud_Measure[i].TabStop = nud_Width[i].TabStop =
        cmb_DefaultPositioning[i].TabStop = cmb_DefaultType[i].TabStop =
        nud_DefaultScoreGap[i].TabStop = nud_DefaultOffset[i].TabStop =
        cmb_Type[i].TabStop = cmb_Positioning[i].TabStop =
        nud_ScoreGap[i].TabStop = cmb_Shaft[i].TabStop =
        nud_Offset[i].TabStop = !isReadOnly;
      }

    }

    //header
		private void cmb_Flute_SelectedItemChanged(object sender, System.EventArgs e)
		{
      setListQualityItems(0, ((TitledList)sender).SelectedItem, true);
    }

		private void cmb_DefaultScorePositioning_SelectedItemChanged(object sender, System.EventArgs e)
		{
      string name=((Control)sender).Name;
      int ind=this.intParse(name.Substring(name.Length-1));
      string scorePositType = ((TitledList)sender).SelectedItemData;
      setListScorerItems(cmb_DefaultType[ind], ((TitledList)sender).SelectedItemData, -1, true);
      this.nud_DefaultOffset[ind].Enabled = scorePositType.Equals(PositType.TeletwinP.ToString());
      if ((this.state == State.Read) && (this.nextState == State.Read))
        return;
      if ((this.measures == null) || (this.measures[ind] == null))
        return;
      for (int i = 0; i < this.measures[ind].Count; i++)
      {
        if ((((ScoreMeasure)this.measures[ind][i]).positType == this.oldDefaultPositType[ind]) || (this.state == State.Insert))
          if (this.cmb_DefaultPositioning[ind].SelectedIndex>=0)
          {
            ((ScoreMeasure)this.measures[ind][i]).positType = (PositType)Enum.Parse(typeof(PositType), this.cmb_DefaultPositioning[ind].SelectedItemData);
            if (i == 0)
              this.cmb_Positioning[ind].SelectedItem = this.cmb_DefaultPositioning[ind].SelectedItem;
          }
      }
      if ((this.cmb_DefaultPositioning[ind].SelectedItemData!=null) && (this.cmb_DefaultPositioning[ind].SelectedItemData!=""))
        this.oldDefaultPositType[ind] = (PositType)Enum.Parse(typeof(PositType), this.cmb_DefaultPositioning[ind].SelectedItemData);
    }

		private void cmb_Positioning_SelectedItemChanged(object sender, EventArgs e)
		{
      string name=((Control)sender).Name;
      int ind=this.intParse(name.Substring(name.Length-1));
      string scorePositType = ((TitledList)sender).SelectedItemData;
      setListScorerItems(cmb_Type[ind], scorePositType, -1, true);
      this.nud_Offset[ind].Enabled = scorePositType.Equals(PositType.TeletwinP.ToString());
      this.cmb_Type[ind].Enabled = !scorePositType.Equals(PositType.TearTape.ToString());
      this.nud_ScoreGap[ind].Enabled = !scorePositType.Equals(PositType.TearTape.ToString());
    }

		/// <summary>
		/// Fill the items of the Flute list using the flute database.
		/// </summary>
		/// <param name="indexToSelect">Index of the item to select into the list.</param>
		/// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
		private void setListFluteItems(int indexToSelect, bool Clean)
		{
			if (Clean)
				this.cmb_Flute.ClearItems();
      DataTable flute = DBManager.getDBManager().flute.getTable();
			for (int i = 0; i < flute.Rows.Count; i++)
			{
				if (!cmb_Flute.Contains(flute.Rows[i][Flute.FLUTE_ID].ToString()))
					this.cmb_Flute.AddItem(flute.Rows[i][Flute.FLUTE_ID].ToString());
			}
			if (indexToSelect >= 0 && indexToSelect <= cmb_Flute.Count - 1 && Clean)
				this.cmb_Flute.SelectedIndex = indexToSelect;
		}

		/// <summary>
		/// Fill the items of the Quality list using the quality database according to the selected flute.
		/// </summary>
		/// <param name="indexToSelect">Index of the item to select into the list.</param>
		/// <param name="SelectedFlute">Flute selected. Qualities are selected according to this flute parameter.</param>
		/// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
		private void setListQualityItems(int indexToSelect, string SelectedFlute, bool Clean)
		{
      bool all = false;
      if (Clean)
				this.cmb_Quality.ClearItems();
      if (SelectedFlute == null || SelectedFlute.Equals(String.Empty))
        all = true; ;
      DataTable quality = DBManager.getDBManager().quality.getTable();
      for (int i = 0; i < quality.Rows.Count; i++)
			{
				if ((all || quality.Rows[i][Quality.FLUTE_ID].Equals(SelectedFlute)) &&
					(!cmb_Quality.Contains(quality.Rows[i][Quality.QUALITY_ID].ToString())))
					this.cmb_Quality.AddItem(quality.Rows[i][Quality.QUALITY_ID].ToString());
			}
			if (indexToSelect >= 0 && indexToSelect <= this.cmb_Quality.Count - 1 && Clean)
				this.cmb_Quality.SelectedIndex = indexToSelect;
		}

		/// <summary>
		/// Fill the specified TitledList with the scorer positioning type according to the current line configuration.
		/// </summary>
		/// <param name="titledList">The titledList to fill.</param>
		/// <param name="indexToSelect">Index of the item to select into the list.</param>
		/// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
		private void setListPositioningItems(SyncroC.Controls.TitledList titledList, int indexToSelect, bool Clean, bool tearTape)
		{
      Language lang = DBManager.getDBManager().language;
      string typeName = typeof(SyncroC.Utility.PositType).Name + ".";
			if (Clean)
				titledList.ClearItems();
			ScoreType[] scoreType = this.ss.SlitterScorerHeader.ScoreTypes;
      string[] positTypes = Enum.GetNames(typeof(SyncroC.Utility.PositType));
			for (int i = 0; i < scoreType.Length; i++)
			{
        if (scoreType[i].Letter != ' ')
        {
          for (int j = 1; j < positTypes.Length; j++) //j starts from 1 because the 0 enum is "NoPositType"
          {
            if (ScorerProfileType.IsPositioningAvailable(scoreType[i].Scorer.SClass, (PositType)j) && (!titledList.Contains(positTypes[j])))
							//RFAZZI PROVA
							//if (this.ss.PositioningOptions.Scores.ShowNormalReverse || ((PositType)j != PositType.NormalReverse))
							if (this.ss.SlitterScorerHeader.PositioningOptions.Scores.ShowNormalReverse || ((PositType)j != PositType.NormalReverse))
                titledList.AddItem(lang.getTranslation(typeName + positTypes[j]), positTypes[j]);
          }
        }
			}
      if (tearTape)
        titledList.AddItem(lang.getTranslation(typeName + PositType.TearTape.ToString()), PositType.TearTape.ToString());
      if (indexToSelect >= 0 && indexToSelect <= titledList.Count - 1 && Clean)
        titledList.SelectedIndex = indexToSelect;
    }

		/// <summary>
		/// Fill the specified TitledList with the scorer database according to the selected Posit Type
		/// </summary>
		/// <param name="titledList">The titledList to fill.</param>
		/// <param name="SelectedPositType">The posit Type according with the scorers to be inserted are selected</param>
		/// <param name="indexToSelect">Index of the item to select into the list.</param>
		/// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
		private void setListScorerItems(SyncroC.Controls.TitledList titledList, string SelectedPositType, int indexToSelect, bool Clean)
		{
      bool all = false;
      if (Clean)
				titledList.ClearItems();
      PositType positType = PositType.NoPositType;
      if (SelectedPositType == null || SelectedPositType.Equals(String.Empty)/*?? || SelectedPositType == NoPositType???*/)
        all = true;
      else
  			positType = (PositType)Enum.Parse(typeof(SyncroC.Utility.PositType), SelectedPositType);
			ScoreType[] scoreType = this.ss.SlitterScorerHeader.ScoreTypes;
      for (int i = 0; i < scoreType.Length; i++)
			{
        if ((ScorerProfileType.IsPositioningAvailable(scoreType[i].Scorer.SClass, positType) || all) &&
          (scoreType[i].Letter != ' ') && (!titledList.Contains(scoreType[i].Letter.ToString())))
        {
					titledList.AddItem(scoreType[i].Letter.ToString());
				}
			}
      if (indexToSelect >= 0 && indexToSelect <= titledList.Count - 1 && Clean)
        titledList.SelectedIndex = indexToSelect;
      else if (titledList.SelectedIndex == -1)
        titledList.SelectedIndex = 0;
    }

		/// <summary>
		/// Fill the specified TitledList with the constant names of the specified Enum.
		/// </summary>
		/// <param name="titledList">The TitledList to be filled with the items.</param>
		/// <param name="type">Enum to get the constant names from.</param>
		/// <param name="includeMask">Specifie which constants has to be included into the list.</param>
		/// <param name="indexToSelect">Index of the item to select into the list.</param>
		/// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
		private void setTitledListItems(SyncroC.Controls.TitledList titledList, Type type, bool[] includeMask, int indexToSelect, bool Clean)
		{
			if (type == null || !type.IsEnum)
			{
				Logger.LogError(this, "setTitledListItems - invalid parameter type: it must be an Enum");
				return;
			}
			string[] items = Enum.GetNames(type);
			if (items.Length != includeMask.Length)
			{
				Logger.LogError(this, "setTitledListItems - parameter \"type\" and parameter \"includeMask\" are incompatibles: they must have same length.");
				return;
			}
      Language lang = DBManager.getDBManager().language;
      string typeName = type.Name + ".";
      if (Clean)
				titledList.ClearItems();
			for (int i = 0; i < items.Length; i++)
			{
				if (includeMask[i] &&
					(!titledList.Contains(items[i])))
          titledList.AddItem(lang.getTranslation(typeName + items[i]), items[i]);
			}
      if (indexToSelect >= 0 && indexToSelect <= titledList.Count - 1 && Clean)
        titledList.SelectedIndex = indexToSelect;
    }

		private void grv_ScorerOptions_SelectionChanged(object sender, EventArgs e)
		{
      string name=((Control)sender).Name;
      int ind=this.intParse(name.Substring(name.Length-1));
      if (((GridView)sender).SelectedRows.Count != 1)
      {
        this.nud_Measure[ind].Value = string.Empty;
        this.nud_ScoreGap[ind].Value = string.Empty;
        this.nud_Offset[ind].Value = string.Empty;
        //if ((((GridView)sender).Rows.Count > 0) && (((GridView)sender).SelectedRows.Count == 0))
        //{
        //  ((GridView)sender).Rows[0].Selected = true;
        //  return;
        //}
      }
      if (((GridView)sender).SelectedRows.Count == 1)
      {
				int index = ((GridView)sender).SelectedRows[0].Index;
        //agrassi_FI28_07-08_1
        //if (measures[ind] != null)
        if ((measures[ind] != null) && (measures[ind].Count > index))
          this.nud_Measure[ind].Value = ((ScoreMeasure)measures[ind][index]).measure.ToString();
        /*if ((
            ((this.nextState == State.Read) && (this.state != State.Read))) &&
            (index == ((GridView)sender).Rows.Count - 1))
        {
          this.cmb_Type[ind].SelectedItem = "";
          this.cmb_Positioning[ind].SelectedItem = "";
          this.nud_ScoreGap[ind].Value = "";
          this.cmb_Shaft[ind].SelectedItem = "";
          this.nud_Offset[ind].Value = "";
        }
        else*/ if (((this.state == State.Modify) && (index >= ((GridView)sender).Rows.Count - 2) && (this.nextState!=State.Read)) ||
                  (this.state == State.Insert) || (this.nextState == State.Insert))
        {
          this.SetDefault(ind);
          int boxWidth = intParse(this.nud_Width[ind].Value);
          for (int i = 0; i < index; i++)
            boxWidth -= ((ScoreMeasure)measures[ind][i]).measure;
          if (boxWidth < 0)
          {
						this.nud_Measure[ind].SetError("§SYCC_33");
            boxWidth = 0;
          }
          else
            this.nud_Measure[ind].SetError("");
          this.nud_Measure[ind].Value = boxWidth.ToString();
        }
        else if (measures[ind]!=null)
        {
          this.cmb_Type[ind].SelectedItem = ((ScoreMeasure)measures[ind][index]).scoreTypeLetter.ToString();
          if (((ScoreMeasure)measures[ind][index]).positType==PositType.NoPositType)
            this.cmb_Positioning[ind].SelectedItem = "";
          else
            this.cmb_Positioning[ind].SelectedItemData = ((ScoreMeasure)measures[ind][index]).positType.ToString();
          this.nud_ScoreGap[ind].Value = ((ScoreMeasure)measures[ind][index]).gap.ToString();
          this.cmb_Shaft[ind].SelectedItemData = ((ScoreMeasure)measures[ind][index]).scorerShaft.ToString();
          this.nud_Offset[ind].Value = ((ScoreMeasure)measures[ind][index]).offset.ToString();
        }
      }
		}

    protected override void Return()
    {
      if ((this.state != State.Modify) && (this.state != State.Insert))
        return;
      for (int i = 0; i < NUM_ORDERS_IN_SETUP; i++)
      {
        if (this.cmb_Shaft[i].Focused)
        {
          int index = this.grv_ScorerOptions[i].SelectedRows[0].Index;
          this.FillInMeasure(i, index, (this.nud_Measure[i].Value == "" ? 0 : intParse(this.nud_Measure[i].Value)),
                          (this.cmb_Positioning[i].SelectedItem.Length > 0) ? (PositType)Enum.Parse(typeof(PositType), this.cmb_Positioning[i].SelectedItemData, true) : PositType.NoPositType,
                          (this.cmb_Type[i].SelectedItem.Length > 0) ? this.cmb_Type[i].SelectedItem[0] : ' ',
                          (this.nud_ScoreGap[i].Value == "" ? 0 : intParse(this.nud_ScoreGap[i].Value)),
                          (this.nud_Offset[i].Value == "" ? 0 : intParse(this.nud_Offset[i].Value)),
                          (ScorerShaft)Enum.Parse(typeof(ScorerShaft), this.cmb_Shaft[i].SelectedItemData, true));
          PositType defPosType = (this.cmb_DefaultPositioning[i].SelectedItem.Length > 0) ? (PositType)Enum.Parse(typeof(PositType), this.cmb_DefaultPositioning[i].SelectedItemData, true) : PositType.NoPositType;
          char defScorer = (this.cmb_DefaultType[i].SelectedItem.Length > 0) ? this.cmb_DefaultType[i].SelectedItem[0] : ' ';
          int defGap = intParse(this.nud_DefaultScoreGap[i].Value);
          int defOffset = intParse(this.nud_DefaultOffset[i].Value);
          //agrassi_FI28-07-08_1
          //this.grv_ScorerOptions[i].Rows[index].SetValues(new object[] {
          //          SyncroConverter.Convert(((ScoreMeasure)measures[i][index]).measure, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre),
          //          this.ScoreOverrides(index, i, defPosType, defScorer, defGap, defOffset)});
          //this.grv_ScorerOptions.Rows[index].SetValues(this.ScoreOverrides((int)SyncroConverter.Convert(((ScoreMeasure)measures[index]).measure, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre),
          //                                          index, this.defPositType, this.defScoreType, this.defScoreGap, defOffset, defShaft));
          this.grv_ScorerOptions[i].Rows[index].SetValues(this.ScoreOverrides((int)SyncroConverter.Convert(((ScoreMeasure)measures[i][index]).measure, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre),
                                                i, index, defPosType, defScorer, defGap, defOffset, ScorerShaft.Auto));
          if (index == this.grv_ScorerOptions[i].Rows.Count - 1)
          {
            this.grv_ScorerOptions[i].Rows.Add();
            this.measures[i].Add(new ScoreMeasure());
          }
          this.grv_ScorerOptions[i].Rows[index + 1].Selected = true;
          this.nud_Measure[i].Focus();
          return;
        }
        else if (this.nud_Measure[i].Focused)
        {
          if (this.nud_Measure[i].Value.Equals(String.Empty) || this.intParse(this.nud_Measure[i].Value) <= 0)
          {
            int newInd = (i + 1) % NUM_ORDERS_IN_SETUP;
            this.ttb_OrderNumber[newInd].Focus();
            int index = this.grv_ScorerOptions[i].SelectedRows[0].Index;
            this.FillInMeasure(i, index, 0, PositType.NoPositType, ' ', 0, 0, ScorerShaft.Auto);
            //((ScoreMeasure)measures[i][index]).measure = 0;
            //((ScoreMeasure)measures[i][index]).positType = PositType.NoPositType;
            //((ScoreMeasure)measures[i][index]).scoreTypeLetter = ' ';
            //((ScoreMeasure)measures[i][index]).gap = 0;
            //((ScoreMeasure)measures[i][index]).offset = 0;
            //((ScoreMeasure)measures[i][index]).scorerShaft = ScorerShaft.Auto;
            PositType defPosType = (this.cmb_DefaultPositioning[i].SelectedItem.Length > 0) ? (PositType)Enum.Parse(typeof(PositType), this.cmb_DefaultPositioning[i].SelectedItemData, true) : PositType.NoPositType;
            //agrassi_FI28-07-08_1
            //this.grv_ScorerOptions[i].Rows[index].SetValues(new object[] {
            //        SyncroConverter.Convert(((ScoreMeasure)measures[i][index]).measure, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre),
            //        this.ScoreOverrides(index, i, PositType.NoPositType, ' ', 0, 0)});
            this.grv_ScorerOptions[i].Rows[index].SetValues(this.ScoreOverrides((int)SyncroConverter.Convert(((ScoreMeasure)measures[i][index]).measure, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre),
                                          i, index, PositType.NoPositType, ' ', 0, 0, ScorerShaft.Auto));
            for (int ind = this.grv_ScorerOptions[i].Rows.Count - 1; ind >= index + 1; ind--)
            {
              //agrassi_FI28-07-08_1
              if (grv_ScorerOptions[i].Rows.Count>ind)
                this.grv_ScorerOptions[i].Rows.RemoveAt(ind);
              if (this.measures[i].Count>ind)
                this.measures[i].RemoveAt(ind);
            }
          }
          else
            base.Return();
          return;
        }
      }
      if (this.cmb_TrimType.Focused)
      {
        this.ttb_OrderNumber[0].Focus();
      }
      else
        base.Return();
    }

    protected override void RightArrow()
    {
      if (this.state == State.Read)
      {
        for (int i=0; i<NUM_ORDERS_IN_SETUP; i++)
        {
          if (this.grv_ActiveScorerOptions == this.grv_ScorerOptions[i])
          {
            int newInd=(i+1) % NUM_ORDERS_IN_SETUP;
            if (this.grv_ScorerOptions[newInd].Rows.Count > 1)
              this.grv_ActiveScorerOptions = this.grv_ScorerOptions[newInd];
            return;
          }
        }
        this.Return();
      }
    }

    protected override void LeftArrow()
    {
      if (this.state == State.Read)
      {
        for (int i=0; i<NUM_ORDERS_IN_SETUP; i++)
        {
          if (this.grv_ActiveScorerOptions == this.grv_ScorerOptions[i])
          {
            int newInd=(i+NUM_ORDERS_IN_SETUP-1) % NUM_ORDERS_IN_SETUP;
            if (this.grv_ScorerOptions[newInd].Rows.Count > 1)
              this.grv_ActiveScorerOptions = this.grv_ScorerOptions[newInd];
            return;
          }
        }
      }
      else
      {
        for (int i=0; i<NUM_ORDERS_IN_SETUP; i++)
        {
          if (this.nud_Measure[i].Focused)
          {
            int index = this.grv_ScorerOptions[i].SelectedRows[0].Index;
            if (index > 0)
              this.grv_ScorerOptions[i].Rows[index - 1].Selected = true;
            else
              this.nud_DefaultOffset[i].Focus();
            return;
          }
        }
        if (this.ttb_OrderNumber[1].Focused)
          this.nud_Measure[0].Focus();
        else if (this.ttb_OrderNumber[2].Focused)
          this.nud_Measure1.Focus();
        else if (this.ttb_OrderNumber[0].Focused)
          this.cmb_TrimType.Focus();
        else
          base.LeftArrow();
      }
    }

    protected override void UpArrow()
    {
      if (this.state == State.Read)
      {
        if (this.grv_ActiveScorerOptions.SelectedRows.Count > 0)
        {
          int ind = this.grv_ActiveScorerOptions.SelectedRows[0].Index;
          if (ind > 0)
            this.grv_ActiveScorerOptions.Rows[ind - 1].Selected = true;
        }
      }
    }

    protected override void DownArrow()
    {
      if (this.state == State.Read)
      {
        if (this.grv_ActiveScorerOptions.SelectedRows.Count > 0)
        {
          int ind = this.grv_ActiveScorerOptions.SelectedRows[0].Index;
          if (ind < this.grv_ActiveScorerOptions.Rows.Count - 1)
            this.grv_ActiveScorerOptions.Rows[ind + 1].Selected = true;
        }
      }
    }

    private void SetDefault(int i)
    {
      
      this.cmb_Type[i].SelectedIndex = this.cmb_DefaultType[i].SelectedIndex;
      this.cmb_Positioning[i].SelectedIndex = this.cmb_DefaultPositioning[i].SelectedIndex;
      this.nud_ScoreGap[i].Value = this.nud_DefaultScoreGap[i].Value;
      this.cmb_Shaft[i].SelectedItemData = Enum.Format(typeof(ScorerShaft), ScorerShaft.Auto, "g");
      this.nud_Offset[i].Value = this.nud_DefaultOffset[i].Value;
    }

		private void order_RowChanged(object sender, DataRowChangeEventArgs e)
    {
			switch (e.Action)
			{
				case DataRowAction.Change:
					if (e.Row[Order.DEL_SETUP_ID].Equals(this.SetupId.Text))
					{//the currently showed order has been been modified (maybe a previous order has been added or deleted so, 
						//in case of the page is in Modify state, just the position has to be refreshed )
						indexOrderToShow = Convert.ToInt32(e.Row[Order.DEL_POSITION]) - 1;
						if (state.Equals(State.Read))
							showOrder(indexOrderToShow);
						else
							if (state.Equals(State.Modify))
							{
								if (indexOrderToShow == 0)
								{
									SetPageState(State.Read);
									PageManager.getPageManager()._SetMessage("§SYCC_01", true);
								}
								else
									showIndex(indexOrderToShow);
							}
					}
					break;
				case DataRowAction.Add:
					if (orderAddRequested)
					{//a new order has been added by this client, if page is Read state need to show it
						orderAddRequested = false;
						if (state.Equals(State.Read))
						{
							indexOrderToShow = Convert.ToInt32(e.Row[Order.DEL_POSITION]) - 1;
							showOrder(indexOrderToShow);
						}
					}
					break;
				case DataRowAction.Delete:
					DataRow currentlyShowedRow = order.Rows.Find(this.SetupId.Text);
					if (currentlyShowedRow == null)
					{//the currently showed order has been been deleted by someone else
						if (state.Equals(State.Read))
							//show next order in queue
							showOrder(indexOrderToShow);
						else
							if (state.Equals(State.Modify))
							{	//show warning and change page status
								PageManager.getPageManager()._SetMessage("§SYCC_79", false);
								SetPageState(State.Read);
							}
					}
					break;
			}
    }

    private void Frm_twin400_orderDetails_Activated(object sender, EventArgs e)
    {
      if (externState != this.state)
        this.SetPageState(externState, false);
      if (this.state==State.Read)
      {
        this.indexOrderToShow = externIndex;
        Frm_twin400_toolsSubgroup.externIndex = this.indexOrderToShow;
        this.showOrder(this.indexOrderToShow);
        this.tab_Tab2.Focus();
      }
    }

    private void tab_Tab2_Leave(object sender, EventArgs e)
    {
      if (this.state == State.Read)
      {
        this.tab_Tab2.Focus();
      }
    }

    private void tim_debug_Tick(object sender, EventArgs e)
    {
      if (this.ActiveControl!=null)
        this.ActiveCtrl.Text = "Active control: " + this.ActiveControl.Name+" ("+this.ActiveControl.TabIndex.ToString()+")";
    }

    private void nud_ScoreGap_Leave(object sender, EventArgs e)
    {
      if (this.state != State.Read)
      {
        if (intParse(this.nud_ScoreGapHeader.Value) > 0)
        {
          if (this.measures[0].Count == 1)
            this.nud_DefaultScoreGap[0].Value = this.nud_ScoreGapHeader.Value;
          if (this.measures[1].Count == 1)
            this.nud_DefaultScoreGap[1].Value = this.nud_ScoreGapHeader.Value;
          if (this.measures[2].Count == 1)
            this.nud_DefaultScoreGap[2].Value = this.nud_ScoreGapHeader.Value;
        }
      }
    }

    private void cmb_DefaultPositioning_Leave(object sender, EventArgs e)
    {
      if (this.state != State.Read)
      {
        string name=((Control)sender).Name;
        int ind=this.intParse(name.Substring(name.Length-1));
        if (this.cmb_DefaultPositioning[ind].SelectedIndex >= 0)
          if (this.measures[ind].Count == 0)
            this.cmb_Positioning[ind].SelectedIndex = this.cmb_DefaultPositioning[ind].SelectedIndex;
      }
    }


    private void cmb_DefaultType_Leave(object sender, EventArgs e)
    {
      if (this.state != State.Read)
      {
        string name=((Control)sender).Name;
        int ind=this.intParse(name.Substring(name.Length-1));
        if (this.cmb_DefaultType[ind].SelectedIndex>0)
          if (this.measures[ind].Count == 0)
            this.cmb_Type[ind].SelectedIndex = this.cmb_DefaultType[ind].SelectedIndex;
      }
    }

    private void nud_DefaultScoreGap_Leave(object sender, EventArgs e)
    {
      if (this.state != State.Read)
      {
        string name=((Control)sender).Name;
        int ind=this.intParse(name.Substring(name.Length-1));
        if (intParse(this.nud_DefaultScoreGap[ind].Value) > 0)
          if (this.measures[ind].Count == 0)
            this.nud_ScoreGap[ind].Value = this.nud_DefaultScoreGap[ind].Value;
      }
    }

    private void nud_DefaultOffset_Leave(object sender, EventArgs e)
    {
      if (this.state != State.Read)
      {
        string name=((Control)sender).Name;
        int ind=this.intParse(name.Substring(name.Length-1));
        this.SetDefault(ind);
        if (intParse(this.nud_DefaultOffset[ind].Value) > 0)
          if (this.measures[ind].Count == 0)
            this.nud_Offset[ind].Value = this.nud_DefaultOffset[ind].Value;
      }
    }

    private void nud_Width_Leave(object sender, EventArgs e)
    {
      if (this.state != State.Read)
      {
        if ((sender == this.nud_Width0) && (this.measures[0].Count==1))
          this.nud_Measure0.Value = this.nud_Width0.Value;
        else if ((sender == this.nud_Width1) && (this.measures[1].Count == 1))
          this.nud_Measure1.Value = this.nud_Width1.Value;
        else if ((sender == this.nud_Width2) && (this.measures[2].Count == 1))
          this.nud_Measure2.Value = this.nud_Width2.Value;
      }
    }

    private void ttb_NotNullString_Validating(object sender, CancelEventArgs e)
    {
      if (this.state != State.Read)
      {
        string s=((TitledTextBox)sender).Value.Trim();
        if (s.Length == 0)
        {
          e.Cancel = true;
					PageManager.getPageManager()._SetMessage("§SYCC_02", true);
        }
      }
    }

    private void nud_WebWidth_Validating(object sender, CancelEventArgs e)
    {
      if (this.state != State.Read)
      {
        string s = ((TitledNumericUpDown)sender).Value.Trim();
        //umbro 22-08-06
        //if ((intParse(s) < this.ss.MinWebWidth) || (intParse(s) > this.ss.MaxWebWidth))
				if ((intParse(s) < this.ss.SlitterScorerHeader.MinTrim + this.ss.SlitterScorerHeader.MinChutesPosition) || (intParse(s) > this.ss.SlitterScorerHeader.MaxWebWidth))
        {
          e.Cancel = true;
          //umbro   22-06-08
          //this.ttb_SuggMin.Value = SyncroConverter.Convert(this.ss.MinWebWidth,
          //                          ((TitledNumericUpDown)sender).InternalUnitMeasure,
          //                          ((TitledNumericUpDown)sender).UnitMeasure).ToString();
					this.ttb_SuggMin.Value = SyncroConverter.Convert(this.ss.SlitterScorerHeader.MinTrim + this.ss.SlitterScorerHeader.MinChutesPosition,
                                    ((TitledNumericUpDown)sender).InternalUnitMeasure,
                                    ((TitledNumericUpDown)sender).UnitMeasure).ToString();
					this.ttb_SuggMax.Value = SyncroConverter.Convert(this.ss.SlitterScorerHeader.MaxWebWidth,
                                    ((TitledNumericUpDown)sender).InternalUnitMeasure,
                                    ((TitledNumericUpDown)sender).UnitMeasure).ToString();
					PageManager.getPageManager()._SetMessage("§SYCC_06", true);
        }
        else
          this.CheckAndUpdateTrim(e, false);
      }
    }

    private void nud_PositiveValues_Validating(object sender, CancelEventArgs e)
    {
      if (this.state != State.Read)
      {
        string s = ((TitledNumericUpDown)sender).Value.Trim();
        if (intParse(s) <= 0)
        {
          e.Cancel = true;
					PageManager.getPageManager()._SetMessage("§SYCC_03§SYCC_04#0", true);
        }
        else
          this.CheckAndUpdateTrim(e, false);
      }
    }

    private void nud_NotNegativeValues_Validating(object sender, CancelEventArgs e)
    {
      if (this.state != State.Read)
      {
        string s = ((TitledNumericUpDown)sender).Value.Trim();
        if (intParse(s) < 0)
        {
          e.Cancel = true;
          PageManager.getPageManager()._SetMessage("§SYCC_03#>=0", true);
        }
        else
          this.CheckAndUpdateTrim(e, false);
      }
    }

    private void nud_ScoreGap_Enter(object sender, EventArgs e)
    {
      this.CleanSuggestionFields();
    }

    private void nud_Measure_Validating(object sender, CancelEventArgs e)
    {
      if (intParse(((TitledNumericUpDown)sender).Value) == 0)
        this.CheckBoxesWidth(e, false);
    }

    private void _SetOnline(bool online)
    {
      this.online = online && (!SyncroC.Utility.Settings.getSettings().configuration.getOnServer() || DBManager.getDBManager().syncroConfig.IsTwin400Only());
    }

    private void ttb_OrderNumber_Focus(object sender, EventArgs e)
    { 
      if (this.state == State.Modify)
      {
        string name = ((Control)sender).Name;
        int ind = this.intParse(name.Substring(name.Length - 1));
        int outs = this.intParse(this.GetSetupValue(this.order.Rows[this.indexOrderToShow], this.orderDbAcronym + "_OUTS_", ind));
        if (outs <= 0)
          this.SelectFirstItem(ind);
      }
    }

    private void cmb_Quality_SelectedItemChanged(object sender, EventArgs e)
    {
      if (this.state == State.Read)
        return;

      //DataRow qualityRow = this.quality.Rows.Find(this.cmb_Quality.SelectedItem);
      DataRow qualityRow = DBManager.getDBManager().quality.getRow(this.cmb_Quality.SelectedItem);
      
      if (qualityRow != null)
        this.nud_ScoreGapHeader.Value = qualityRow[Quality.SCOREGAP_UDdmm].ToString();
    }

    private void nud_ScoreGapHeader_ValueChanged(object sender, EventArgs e)
    {
      if ((this.state == State.Read) && (this.nextState == State.Read))
        return;
      for (int i = 0; i < NUM_ORDERS_IN_SETUP; i++)
      {
        if (this.intParse(this.nud_DefaultScoreGap[i].Value) == this.oldScoreGapHeader)
          this.nud_DefaultScoreGap[i].Value = this.nud_ScoreGapHeader.Value;
      }
    }

    private void nud_DefaultScoreGap_ValueChanged(object sender, EventArgs e)
    {
      if ((this.state == State.Read) && (this.nextState == State.Read))
        return;
      string name = ((Control)sender).Name;
      int ind = this.intParse(name.Substring(name.Length - 1));
      if ((this.measures == null) || (this.measures[ind] == null))
        return;
      for (int i = 0; i < this.measures[ind].Count; i++)
      {
        if ((((ScoreMeasure)this.measures[ind][i]).gap == this.oldDefaultScoreGap[ind]) || (this.state==State.Insert))
        {
          ((ScoreMeasure)this.measures[ind][i]).gap = this.intParse(this.nud_DefaultScoreGap[ind].Value);
          if (i == 0)
            this.nud_ScoreGap[ind].Value = this.nud_DefaultScoreGap[ind].Value;
        }
      }
    }

    private void cmb_DefaultScoreType_SelectedItemChanged(object sender, EventArgs e)
    {
      if ((this.state == State.Read) && (this.nextState == State.Read))
        return;
      string name = ((Control)sender).Name;
      int ind = this.intParse(name.Substring(name.Length - 1));
      if ((this.measures == null) || (this.measures[ind] == null))
        return;
      for (int i = 0; i < this.measures[ind].Count; i++)
      {
        if ((((ScoreMeasure)this.measures[ind][i]).scoreTypeLetter == this.oldDefaultScoreType[ind]) || (this.state==State.Insert))
        {
          ((ScoreMeasure)this.measures[ind][i]).scoreTypeLetter = this.cmb_DefaultType[ind].Value.ToCharArray()[0];
          if (i == 0)
            this.cmb_Type[ind].SelectedItem = this.cmb_DefaultType[ind].SelectedItem;
        }
      }
      this.oldDefaultScoreType[ind] = this.cmb_DefaultType[ind].Value.ToCharArray()[0];
    }

    private void _SetCurrentUnchangeable(bool currentUnchangeable)
    {
      this.currentUnchangeable = currentUnchangeable;
    }
  }
}

