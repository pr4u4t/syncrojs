using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Controls;
using SyncroC.DataBase.EventArgs;
using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Controls.Colors;
using SyncroC.Forms.PopUp;
using SyncroC.Controls.Interfaces;

namespace SyncroC.Forms.Pages.Production.OrdersIns
{
  public class Frm_QuickInsDryOrder : SyncroC.Forms.Pages.Production.OrdersIns.Frm_OrdersInsSubGroup
  {
    private System.ComponentModel.IContainer components = null;
    //AGrassi_FI31/08/2007_3
    private Label ActiveCtrl;
    private Timer tim_debug;
    private Label NextPageState;
    private ImageList iml_valid;
    private System.Windows.Forms.Label PageState;
    private System.Windows.Forms.Panel pnl_Orders2;
    private SyncroC.Controls.TitledNumericUpDown nud_stacksXpallet2;
    private SyncroC.Controls.TitledNumericUpDown nud_sheetsXstack2;
    private SyncroC.Controls.TitledNumericUpDown nud_SheetsDesired2;
    private SyncroC.Controls.TitledNumericUpDown nud_Boxes2;
    private SyncroC.Controls.TitledNumericUpDown nud_length2;
    private SyncroC.Controls.TitledNumericUpDown nud_Width2;
    private SyncroC.Controls.TitledList cmb_sending2;
    private SyncroC.Controls.Title ttl_Sheets2;
    private SyncroC.Controls.Title ttl_Format2;
    private SyncroC.Controls.TitledList cmb_Type2;
    private SyncroC.Controls.Title ttl_Score2;
    private SyncroC.Controls.TitledList cmb_Positioning2;
    private SyncroC.Controls.Title ttl_ord2;
    private SyncroC.Controls.TitledTextBox ttb_CustName2;
    private SyncroC.Controls.TitledTextBox ttb_OrderNumber2;
    private SyncroC.Controls.TitledList cmb_Level2;
    private SyncroC.Controls.TitledTextBox ttb_Measures2;
    private System.Windows.Forms.Panel pnl_Orders1;
    private SyncroC.Controls.TitledNumericUpDown nud_stacksXpallet1;
    private SyncroC.Controls.TitledNumericUpDown nud_sheetsXstack1;
    private SyncroC.Controls.TitledNumericUpDown nud_SheetsDesired1;
    private SyncroC.Controls.TitledNumericUpDown nud_Boxes1;
    private SyncroC.Controls.TitledNumericUpDown nud_length1;
    private SyncroC.Controls.TitledNumericUpDown nud_Width1;
    private SyncroC.Controls.TitledList cmb_sending1;
    private SyncroC.Controls.Title ttl_Sheets1;
    private SyncroC.Controls.Title ttl_Format1;
    private SyncroC.Controls.TitledList cmb_Type1;
    private SyncroC.Controls.Title ttl_Score1;
    private SyncroC.Controls.TitledList cmb_Positioning1;
    private SyncroC.Controls.Title ttl_ord1;
    private SyncroC.Controls.TitledTextBox ttb_CustName1;
    private SyncroC.Controls.TitledTextBox ttb_OrderNumber1;
    private SyncroC.Controls.TitledList cmb_Level1;
    private SyncroC.Controls.TitledTextBox ttb_Measures1;
    private SyncroC.Controls.TitledTextBox ttb_SetUpSequence;
    private SyncroC.Controls.TitledTextBox ttb_ProgramNumber;
    private SyncroC.Controls.TitledList cmb_Flute;
    private System.Windows.Forms.Panel pnl_OrdersO;
    private SyncroC.Controls.TitledNumericUpDown nud_stacksXpallet0;
    private SyncroC.Controls.TitledNumericUpDown nud_sheetsXstack0;
    private SyncroC.Controls.TitledNumericUpDown nud_SheetsDesired0;
    private SyncroC.Controls.TitledNumericUpDown nud_Boxes0;
    private SyncroC.Controls.TitledNumericUpDown nud_length0;
    private SyncroC.Controls.TitledList cmb_sending0;
    private SyncroC.Controls.Title ttl_Sheets0;
    private SyncroC.Controls.Title ttl_Format0;
    private SyncroC.Controls.TitledList cmb_Type0;
    private SyncroC.Controls.Title ttl_Score0;
    private SyncroC.Controls.TitledList cmb_Positioning0;
    private SyncroC.Controls.Title ttl_ord0;
    private SyncroC.Controls.TitledTextBox ttb_OrderNumber0;
    private SyncroC.Controls.TitledList cmb_Level0;
    private SyncroC.Controls.TitledTextBox ttb_Measures0;
    private SyncroC.Controls.TitledNumericUpDown nud_Width0;
    private SyncroC.Controls.TitledTextBox ttb_SuggMin;
    private SyncroC.Controls.TitledList cmb_Quality;
    private SyncroC.Controls.TitledNumericUpDown nud_WebWidth;
    private SyncroC.Controls.TitledNumericUpDown nud_Trim;
    private SyncroC.Controls.TitledNumericUpDown nud_ScoreGap;
    private SyncroC.Controls.TitledList cmb_TrimType;
    private SyncroC.Controls.TitledTextBox ttb_index;
    private SyncroC.Controls.TitledTextBox ttb_SuggMax;
    private SyncroC.Controls.TitledTextBox ttb_CustName0;
    private TitledMaskTextBox mkt_Delivery0;
    private TitledMaskTextBox mkt_Delivery1;
    private TitledMaskTextBox mkt_Delivery2;

    //page status
    public enum State { Insert, Modify, Read };
    private State state, nextState;

    private DBManager dbm;

    //useful databale
    //private DataTable quality;
    //private DataTable flute;
    private DataTable order;
    //private DataTable cutToPattern;
    private SyncroC.DataBase.LineConfig.SlitterScorer ss;
    private SyncroC.DataBase.LineConfig.Knife knife;
    //RFAZZI_FI12/06/2008_1.Added eng.verb. for Continuous Stack and Pallet Insert into Stacker section	
    private SyncroC.DataBase.LineConfig.Stacker stacker1;
    private SyncroC.DataBase.LineConfig.Stacker stacker2;
    private SyncroC.DataBase.LineConfig.Stacker stacker3;

    private TitledNumericUpDown nud_SetupLength;

    //max number of sheets for each level according to the selected quality
    private int limitSheetsXStackFormat0 = 0; //mechanical limit: higher values won't be accepted
    private int limitSheetsXStackFormat1 = 0;
    private int limitSheetsXStackFormat2 = 0;
    private int maxSheetsXStackFormat0 = 0;//maximum according to destination: higher values will generate warning
    private int maxSheetsXStackFormat1 = 0;
    private int maxSheetsXStackFormat2 = 0;
    private int oldScoreGap;
    private SyncroC.Controls.CheckBox chk_CutToMark1;
    private SyncroC.Controls.CheckBox chk_CutToMark2;
    private Panel pnl_ScrollOrder;
    private Title ttl_DataToPrint0;
    private TitledTextBox ttb_DataToPrint0_1;
    private TitledTextBox ttb_DataToPrint0_3;
    private TitledTextBox ttb_DataToPrint0_2;
    private TitledTextBox ttb_StackerNote0;
    private SyncroC.Controls.CheckBox chk_StackerPallet0;
    private SyncroC.Controls.CheckBox chk_StackerContStack0;
    private Title ttl_stackerData0;
    private TitledList cmb_cutToPattern0;
    private Title ttl_KnifeData0;
    private SyncroC.Controls.CheckBox chk_CutToMark0;
    private TitledTextBox ttb_DataToPrint0_4;
    private TitledTextBox ttb_DataToPrint2_4;
    private TitledTextBox ttb_DataToPrint2_3;
    private TitledTextBox ttb_DataToPrint2_2;
    private TitledTextBox ttb_DataToPrint2_1;
    private Title ttl_DataToPrint2;
    private TitledTextBox ttb_DataToPrint1_4;
    private TitledTextBox ttb_DataToPrint1_3;
    private TitledTextBox ttb_DataToPrint1_2;
    private TitledTextBox ttb_DataToPrint1_1;
    private Title ttl_DataToPrint1;
    private TitledTextBox ttb_StackerNote1;
    private SyncroC.Controls.CheckBox chk_StackerPallet1;
    private SyncroC.Controls.CheckBox chk_StackerContStack1;
    private TitledTextBox ttb_StackerNote2;
    private SyncroC.Controls.CheckBox chk_StackerPallet2;
    private SyncroC.Controls.CheckBox chk_StackerContStack2;
    private Title ttl_stackerData2;
    private Title ttl_stackerData1;
    private TitledList cmb_cutToPattern2;
    private Title ttl_KnifeData2;
    private TitledList cmb_cutToPattern1;
    private Title ttl_KnifeData1;
    private Title ttl_mthData0;
    private TitledTextBox ttb_MthNextMachine0;
    private TitledTextBox ttb_MthCode0;
    private TitledTextBox ttb_CustAddress0;
    private TitledTextBox ttb_MthNextMachine2;
    private TitledTextBox ttb_MthCode2;
    private Title ttl_mthData2;
    private TitledTextBox ttb_MthNextMachine1;
    private TitledTextBox ttb_MthCode1;
    private Title ttl_mthData1;
    private TitledTextBox ttb_CustAddress1;
    private TitledTextBox ttb_CustAddress2;
    private Panel pnl_Debug;
    private Label lbl_DebugPastPrint2;
    private Label lbl_DebugPalletPrint2;
    private Label lbl_DebugPastPrint1;
    private Label lbl_DebugPalletPrint1;
    private Label lbl_DebugPastPrint0;
    private Label lbl_DebugPalletPrint0;
    private Label lbl_DebugSetupModId;
    private Label lbl_DebugSetupID;
    private Button btn_ScorersOptions0;
    private Button btn_ScorersOptions2;
    private Button btn_ScorersOptions1;
    private TitledList cmb_DischargeSide0;
    private TitledList cmb_DischargeSide2;
    protected internal TitledList cmb_DischargeSide1;
    private TitledList cmb_Balance1;
    private TitledList cmb_Balance0;
    private TitledList cmb_Balance2;
    //agrassi_FI01-06-2010_1
    private bool copyAndModify = false;
    private SyncroC.Controls.CheckBox chk_Bundle0;
    private SyncroC.Controls.CheckBox chk_Bundle2;
    private SyncroC.Controls.CheckBox chk_Bundle1;

    private bool orderAddRequested = false;

    public Frm_QuickInsDryOrder()
    {
      InitializeComponent();

      dbm = DBManager.getDBManager();
      //quality = dbm.quality.quality.Tables[Quality.QUALITY_LIST];
      //flute = dbm.flute.flute.Tables[Flute.FLUTE_LIST];
      order = dbm.order.order.Tables[Order.DRYEND_LIST];
      //cutToPattern = dbm.cutToPattern.cutToPattern.Tables[CutToPattern.CUTTOPATTERN_LIST];
      this.ss = dbm.lineConfig.SlitterScorerConfig;
      this.knife = dbm.lineConfig.KnifeConfig;
      //RFAZZI_FI12/06/2008_1.Added eng.verb. for Continuous Stack and Pallet Insert into Stacker section	
      this.stacker1 = dbm.lineConfig.Stacker1Config;
      this.stacker2 = dbm.lineConfig.Stacker2Config;
      this.stacker3 = dbm.lineConfig.Stacker3Config;


      //KNIFE
      //RFAZZI_FI.Changed the above value for level
      //this.chk_CutToMark0.Visible = this.chk_CutToMark1.Visible = this.chk_CutToMark2.Visible = this.knife.CutToMark;
      //this.cmb_cutToPattern0.Visible = this.cmb_cutToPattern1.Visible = this.cmb_cutToPattern2.Visible = this.knife.CutToPattern;
      //this.ttl_KnifeData0.Visible = this.ttl_KnifeData1.Visible = this.ttl_KnifeData2.Visible = (this.knife.CutToMark || this.knife.CutToPattern);
      this.chk_CutToMark0.Visible = this.chk_CutToMark1.Visible = this.chk_CutToMark2.Visible = (this.knife.KnifeLevelSettings[0].CutToMark ||
                                                                                                  this.knife.KnifeLevelSettings[1].CutToMark ||
                                                                                                  this.knife.KnifeLevelSettings[2].CutToMark);
      this.cmb_cutToPattern0.Visible = this.cmb_cutToPattern1.Visible = this.cmb_cutToPattern2.Visible = (this.knife.KnifeLevelSettings[0].CutToPattern ||
                                                                                                          this.knife.KnifeLevelSettings[1].CutToPattern ||
                                                                                                          this.knife.KnifeLevelSettings[2].CutToPattern);
      this.ttl_KnifeData0.Visible = this.ttl_KnifeData1.Visible = this.ttl_KnifeData2.Visible = (this.knife.KnifeLevelSettings[0].CutToMark ||
                                                                                                  this.knife.KnifeLevelSettings[1].CutToMark ||
                                                                                                  this.knife.KnifeLevelSettings[2].CutToMark ||
                                                                                                  this.knife.KnifeLevelSettings[0].CutToPattern ||
                                                                                                  this.knife.KnifeLevelSettings[1].CutToPattern ||
                                                                                                  this.knife.KnifeLevelSettings[2].CutToPattern);
      //RFAZZI_FI12/06/2008_1.Added eng.verb. for Continuous Stack and Pallet Insert into Stacker section	
      this.chk_StackerContStack0.Visible = this.chk_StackerContStack1.Visible = this.chk_StackerContStack2.Visible = (this.stacker1.ContinuousStack ||
                                                                                                                        this.stacker2.ContinuousStack ||
                                                                                                                        this.stacker3.ContinuousStack);
      this.chk_StackerPallet0.Visible = this.chk_StackerPallet1.Visible = this.chk_StackerPallet2.Visible = (this.stacker1.PalletInsertion ||
                                                                                                              this.stacker2.PalletInsertion ||
                                                                                                              this.stacker3.PalletInsertion);

      //Header
      setListCutToPatternItems(-1, true);
      setListFluteItems(-1, true);
      setListQualityItems(-1, null, true);
      setTitledListItems(cmb_TrimType, typeof(SyncroC.Utility.TrimType), new bool[] { true, true }, -1, true);
      //Order 0
      setTitledListItems(cmb_Level0, typeof(SyncroC.Utility.LevelName), new bool[] { false, true, (knife.LevelCount == 3), (knife.LevelCount >= 2) }, -1, true);
      setTitledListItems(cmb_sending0, typeof(SyncroC.Utility.PalletSending), new bool[] { true, true, true }, -1, true);
      setListPositioningItems(cmb_Positioning0, -1, true);
      setListScorerItems(cmb_Type0, null, -1, true);
      //agrassi_FI24-02-2010_3
      setTitledListItems(cmb_DischargeSide0, typeof(SyncroC.Utility.DischargeSide), new bool[] { true, true, true }, 0, true);
      setTitledListItems(cmb_Balance0, typeof(SyncroC.Utility.Balance), new bool[] { true, true, true }, 0, true);
      //Order 1
      setTitledListItems(cmb_Level1, typeof(SyncroC.Utility.LevelName), new bool[] { false, true, (knife.LevelCount == 3), (knife.LevelCount >= 2) }, -1, true);
      setTitledListItems(cmb_sending1, typeof(SyncroC.Utility.PalletSending), new bool[] { true, true, true }, -1, true);
      setListPositioningItems(cmb_Positioning1, -1, true);
      setListScorerItems(cmb_Type1, null, -1, true);
      //agrassi_FI24-02-2010_3
      setTitledListItems(cmb_DischargeSide1, typeof(SyncroC.Utility.DischargeSide), new bool[] { true, true, true }, 0, true);
      setTitledListItems(cmb_Balance1, typeof(SyncroC.Utility.Balance), new bool[] { true, true, true }, 0, true);
      //Order 2
      setTitledListItems(cmb_Level2, typeof(SyncroC.Utility.LevelName), new bool[] { false, true, (knife.LevelCount == 3), (knife.LevelCount >= 2) }, -1, true);
      setTitledListItems(cmb_sending2, typeof(SyncroC.Utility.PalletSending), new bool[] { true, true, true }, -1, true);
      setListPositioningItems(cmb_Positioning2, -1, true);
      setListScorerItems(cmb_Type2, null, -1, true);
      //agrassi_FI24-02-2010_3
      setTitledListItems(cmb_DischargeSide2, typeof(SyncroC.Utility.DischargeSide), new bool[] { true, true, true }, 0, true);
      setTitledListItems(cmb_Balance2, typeof(SyncroC.Utility.Balance), new bool[] { true, true, true }, 0, true);

      this.PageState.Text = "Page status: Read";
      this.NextPageState.Text = "Next status: Read";
      //AGrassi_FI31/08/2007_3
      this.tim_debug.Enabled = true;
      this.state = State.Read;
      this.nextState = State.Read;

      order.RowChanged += new DataRowChangeEventHandler(order_RowChanged);
      order.RowDeleted += new DataRowChangeEventHandler(order_RowChanged);
      dbm.OrderValidationSucceded += new EventHandler(OrderValidationSucceded);

      //quality.RowChanged +=new DataRowChangeEventHandler(quality_RowChanged);
      //quality.RowDeleted += new DataRowChangeEventHandler(quality_RowChanged);
      DBManager.getDBManager().quality.OnDBQualityChange += new Quality.DBChangedHandler(quality_RowChanged);

      //cutToPattern.RowChanged += new DataRowChangeEventHandler(cutToPattern_RowChanged);
      //cutToPattern.RowDeleted += new DataRowChangeEventHandler(cutToPattern_RowChanged);

      DBManager.getDBManager().cutToPattern.OnDBCutToPatternChange += new CutToPattern.DBChangedHandler(cutToPattern_RowChanged);

      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
      {
        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
        this.lbl_CtrlF5_YY.Text = "";
      }
    }

    void quality_RowChanged(object sender, DBChangedEventArgs e)
    {
      //agrassi_FI07-09-09_2
      try
      {
        switch (e.Changement)
        {
          case DBChangement.Modify:
          case DBChangement.Delete:
            break;
          case DBChangement.Insert:
            //if (e.Row[Quality.FLUTE_ID].ToString().Equals(cmb_Flute.SelectedItem))
            //  cmb_Quality.AddItem(e.Row[Quality.QUALITY_ID].ToString());
            this.setListQualityItems(cmb_Quality.SelectedIndex, cmb_Quality.SelectedItem, true);
            break;
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "Error in quality_RowChanged:" + ex.Message);
      }
    }

    void cutToPattern_RowChanged(object sender, DBChangedEventArgs e)
    {
      switch (e.Changement)
      {
        case DBChangement.Modify:
        case DBChangement.Delete:
          break;
        case DBChangement.Insert:
            cmb_cutToPattern0.AddItem(e.Key);
            cmb_cutToPattern1.AddItem(e.Key);
            cmb_cutToPattern2.AddItem(e.Key);
          break;
      }
    }

    void order_RowChanged(object sender, DataRowChangeEventArgs e)
    {
      switch (e.Action)
      {
        case DataRowAction.Change:
          if (e.Row[Order.DEL_SETUP_ID].Equals(this.lbl_DebugSetupID.Text))
          {//the currently showed order has been been modified (maybe a previous order has been added or deleted so, 
            //in case of the page is in Modify state, just the position has to be refreshed )
            indexOrderToShowDRY = Convert.ToInt32(e.Row[Order.DEL_POSITION]) - 1;
            if (state.Equals(State.Read))
              showOrder(indexOrderToShowDRY);
            else
              if (state.Equals(State.Modify))
              {
                if (indexOrderToShowDRY == 0)
                {
                  SetPageState(State.Read);
                  PageManager.getPageManager()._SetMessage("§SYCC_01", true);
                }
                else
                  showIndex(indexOrderToShowDRY);
              }
          }
          break;
        case DataRowAction.Add:
          if (orderAddRequested)
          {//a new order has been added by this client, if page is Read state need to show it
            orderAddRequested = false;
            if (state.Equals(State.Read))
            {
              indexOrderToShowDRY = Convert.ToInt32(e.Row[Order.DEL_POSITION]) - 1;
              showOrder(indexOrderToShowDRY);
            }
          }
          break;
        case DataRowAction.Delete:
          DataRow currentlyShowedRow = order.Rows.Find(this.lbl_DebugSetupID.Text);
          if (currentlyShowedRow == null)
          {//the currently showed order has been been deleted by someone else
            if (state.Equals(State.Read))
              //show next order in queue
              showOrder(indexOrderToShowDRY);
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

    private void OrderValidationSucceded(Object sender, System.EventArgs e)
    {
      if (!state.Equals(State.Read))
      {
        SetPageState(State.Read);
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
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }
      }
      order.RowChanged -= new DataRowChangeEventHandler(order_RowChanged);
      order.RowDeleted -= new DataRowChangeEventHandler(order_RowChanged);
      dbm.OrderValidationSucceded -= new EventHandler(OrderValidationSucceded);
      base.Dispose(disposing);
    }

    #region Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_QuickInsDryOrder));
      this.pnl_Orders2 = new System.Windows.Forms.Panel();
      this.chk_Bundle2 = new SyncroC.Controls.CheckBox();
      this.cmb_Balance2 = new SyncroC.Controls.TitledList();
      this.cmb_DischargeSide2 = new SyncroC.Controls.TitledList();
      this.btn_ScorersOptions2 = new System.Windows.Forms.Button();
      this.ttb_CustAddress2 = new SyncroC.Controls.TitledTextBox();
      this.ttb_MthNextMachine2 = new SyncroC.Controls.TitledTextBox();
      this.ttb_MthCode2 = new SyncroC.Controls.TitledTextBox();
      this.ttl_mthData2 = new SyncroC.Controls.Title();
      this.cmb_cutToPattern2 = new SyncroC.Controls.TitledList();
      this.ttl_KnifeData2 = new SyncroC.Controls.Title();
      this.ttb_StackerNote2 = new SyncroC.Controls.TitledTextBox();
      this.chk_StackerPallet2 = new SyncroC.Controls.CheckBox();
      this.chk_StackerContStack2 = new SyncroC.Controls.CheckBox();
      this.ttl_stackerData2 = new SyncroC.Controls.Title();
      this.ttb_DataToPrint2_4 = new SyncroC.Controls.TitledTextBox();
      this.ttb_DataToPrint2_3 = new SyncroC.Controls.TitledTextBox();
      this.chk_CutToMark2 = new SyncroC.Controls.CheckBox();
      this.ttb_DataToPrint2_2 = new SyncroC.Controls.TitledTextBox();
      this.mkt_Delivery2 = new SyncroC.Controls.TitledMaskTextBox();
      this.ttb_DataToPrint2_1 = new SyncroC.Controls.TitledTextBox();
      this.nud_stacksXpallet2 = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_DataToPrint2 = new SyncroC.Controls.Title();
      this.nud_sheetsXstack2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_SheetsDesired2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Boxes2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_length2 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Width2 = new SyncroC.Controls.TitledNumericUpDown();
      this.cmb_sending2 = new SyncroC.Controls.TitledList();
      this.ttl_Sheets2 = new SyncroC.Controls.Title();
      this.ttl_Format2 = new SyncroC.Controls.Title();
      this.cmb_Type2 = new SyncroC.Controls.TitledList();
      this.ttl_Score2 = new SyncroC.Controls.Title();
      this.cmb_Positioning2 = new SyncroC.Controls.TitledList();
      this.ttl_ord2 = new SyncroC.Controls.Title();
      this.ttb_CustName2 = new SyncroC.Controls.TitledTextBox();
      this.ttb_OrderNumber2 = new SyncroC.Controls.TitledTextBox();
      this.cmb_Level2 = new SyncroC.Controls.TitledList();
      this.ttb_Measures2 = new SyncroC.Controls.TitledTextBox();
      this.pnl_Orders1 = new System.Windows.Forms.Panel();
      this.chk_Bundle1 = new SyncroC.Controls.CheckBox();
      this.cmb_Balance1 = new SyncroC.Controls.TitledList();
      this.cmb_DischargeSide1 = new SyncroC.Controls.TitledList();
      this.btn_ScorersOptions1 = new System.Windows.Forms.Button();
      this.ttb_CustAddress1 = new SyncroC.Controls.TitledTextBox();
      this.ttb_MthNextMachine1 = new SyncroC.Controls.TitledTextBox();
      this.ttb_MthCode1 = new SyncroC.Controls.TitledTextBox();
      this.ttl_mthData1 = new SyncroC.Controls.Title();
      this.cmb_cutToPattern1 = new SyncroC.Controls.TitledList();
      this.ttl_KnifeData1 = new SyncroC.Controls.Title();
      this.ttb_StackerNote1 = new SyncroC.Controls.TitledTextBox();
      this.chk_StackerPallet1 = new SyncroC.Controls.CheckBox();
      this.chk_StackerContStack1 = new SyncroC.Controls.CheckBox();
      this.ttl_stackerData1 = new SyncroC.Controls.Title();
      this.ttb_DataToPrint1_4 = new SyncroC.Controls.TitledTextBox();
      this.ttb_DataToPrint1_2 = new SyncroC.Controls.TitledTextBox();
      this.ttb_DataToPrint1_1 = new SyncroC.Controls.TitledTextBox();
      this.ttl_DataToPrint1 = new SyncroC.Controls.Title();
      this.chk_CutToMark1 = new SyncroC.Controls.CheckBox();
      this.mkt_Delivery1 = new SyncroC.Controls.TitledMaskTextBox();
      this.nud_stacksXpallet1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_sheetsXstack1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_SheetsDesired1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Boxes1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_length1 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Width1 = new SyncroC.Controls.TitledNumericUpDown();
      this.cmb_sending1 = new SyncroC.Controls.TitledList();
      this.ttl_Sheets1 = new SyncroC.Controls.Title();
      this.ttl_Format1 = new SyncroC.Controls.Title();
      this.cmb_Type1 = new SyncroC.Controls.TitledList();
      this.ttl_Score1 = new SyncroC.Controls.Title();
      this.cmb_Positioning1 = new SyncroC.Controls.TitledList();
      this.ttl_ord1 = new SyncroC.Controls.Title();
      this.ttb_CustName1 = new SyncroC.Controls.TitledTextBox();
      this.ttb_OrderNumber1 = new SyncroC.Controls.TitledTextBox();
      this.cmb_Level1 = new SyncroC.Controls.TitledList();
      this.ttb_Measures1 = new SyncroC.Controls.TitledTextBox();
      this.ttb_DataToPrint1_3 = new SyncroC.Controls.TitledTextBox();
      this.ttb_SetUpSequence = new SyncroC.Controls.TitledTextBox();
      this.ttb_ProgramNumber = new SyncroC.Controls.TitledTextBox();
      this.cmb_Flute = new SyncroC.Controls.TitledList();
      this.pnl_OrdersO = new System.Windows.Forms.Panel();
      this.chk_Bundle0 = new SyncroC.Controls.CheckBox();
      this.cmb_Balance0 = new SyncroC.Controls.TitledList();
      this.cmb_DischargeSide0 = new SyncroC.Controls.TitledList();
      this.btn_ScorersOptions0 = new System.Windows.Forms.Button();
      this.ttb_CustAddress0 = new SyncroC.Controls.TitledTextBox();
      this.ttb_MthNextMachine0 = new SyncroC.Controls.TitledTextBox();
      this.ttb_MthCode0 = new SyncroC.Controls.TitledTextBox();
      this.ttl_mthData0 = new SyncroC.Controls.Title();
      this.ttb_DataToPrint0_4 = new SyncroC.Controls.TitledTextBox();
      this.ttb_StackerNote0 = new SyncroC.Controls.TitledTextBox();
      this.chk_StackerPallet0 = new SyncroC.Controls.CheckBox();
      this.chk_StackerContStack0 = new SyncroC.Controls.CheckBox();
      this.ttl_stackerData0 = new SyncroC.Controls.Title();
      this.cmb_cutToPattern0 = new SyncroC.Controls.TitledList();
      this.ttl_KnifeData0 = new SyncroC.Controls.Title();
      this.chk_CutToMark0 = new SyncroC.Controls.CheckBox();
      this.ttb_DataToPrint0_3 = new SyncroC.Controls.TitledTextBox();
      this.ttb_DataToPrint0_1 = new SyncroC.Controls.TitledTextBox();
      this.ttl_DataToPrint0 = new SyncroC.Controls.Title();
      this.mkt_Delivery0 = new SyncroC.Controls.TitledMaskTextBox();
      this.nud_stacksXpallet0 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_sheetsXstack0 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_SheetsDesired0 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Boxes0 = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_length0 = new SyncroC.Controls.TitledNumericUpDown();
      this.cmb_sending0 = new SyncroC.Controls.TitledList();
      this.ttl_Sheets0 = new SyncroC.Controls.Title();
      this.ttl_Format0 = new SyncroC.Controls.Title();
      this.cmb_Type0 = new SyncroC.Controls.TitledList();
      this.ttl_Score0 = new SyncroC.Controls.Title();
      this.cmb_Positioning0 = new SyncroC.Controls.TitledList();
      this.ttl_ord0 = new SyncroC.Controls.Title();
      this.ttb_CustName0 = new SyncroC.Controls.TitledTextBox();
      this.ttb_OrderNumber0 = new SyncroC.Controls.TitledTextBox();
      this.cmb_Level0 = new SyncroC.Controls.TitledList();
      this.ttb_Measures0 = new SyncroC.Controls.TitledTextBox();
      this.nud_Width0 = new SyncroC.Controls.TitledNumericUpDown();
      this.ttb_DataToPrint0_2 = new SyncroC.Controls.TitledTextBox();
      this.ttb_SuggMin = new SyncroC.Controls.TitledTextBox();
      this.cmb_Quality = new SyncroC.Controls.TitledList();
      this.nud_WebWidth = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_Trim = new SyncroC.Controls.TitledNumericUpDown();
      this.nud_ScoreGap = new SyncroC.Controls.TitledNumericUpDown();
      this.cmb_TrimType = new SyncroC.Controls.TitledList();
      this.ttb_index = new SyncroC.Controls.TitledTextBox();
      this.ttb_SuggMax = new SyncroC.Controls.TitledTextBox();
      this.PageState = new System.Windows.Forms.Label();
      this.iml_valid = new System.Windows.Forms.ImageList(this.components);
      this.NextPageState = new System.Windows.Forms.Label();
      this.nud_SetupLength = new SyncroC.Controls.TitledNumericUpDown();
      this.pnl_ScrollOrder = new System.Windows.Forms.Panel();
      this.pnl_Debug = new System.Windows.Forms.Panel();
      this.lbl_DebugPastPrint2 = new System.Windows.Forms.Label();
      this.lbl_DebugPalletPrint2 = new System.Windows.Forms.Label();
      this.lbl_DebugPastPrint1 = new System.Windows.Forms.Label();
      this.lbl_DebugPalletPrint1 = new System.Windows.Forms.Label();
      this.lbl_DebugPastPrint0 = new System.Windows.Forms.Label();
      this.lbl_DebugPalletPrint0 = new System.Windows.Forms.Label();
      this.lbl_DebugSetupModId = new System.Windows.Forms.Label();
      this.lbl_DebugSetupID = new System.Windows.Forms.Label();
      this.ActiveCtrl = new System.Windows.Forms.Label();
      this.tim_debug = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.pnl_Orders2.SuspendLayout();
      this.pnl_Orders1.SuspendLayout();
      this.pnl_OrdersO.SuspendLayout();
      this.pnl_ScrollOrder.SuspendLayout();
      this.pnl_Debug.SuspendLayout();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.TabIndex = 200;
      this.lbl_Title_YN.Text = "Orders entry";
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.Leave += new System.EventHandler(this.tab_Tab3_Leave);
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.TabIndex = 200;
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.TabIndex = 200;
      this.lbl_CtrlF1_YY.Text = "New";
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.TabIndex = 200;
      this.lbl_CtrlF2_YY.Text = "Copy && Modify";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.TabIndex = 200;
      this.lbl_CtrlF5_YY.Text = "Modify";
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.TabIndex = 200;
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
      this.iml_AccessibilityImages.Images.SetKeyName(0, "");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "");
      // 
      // pnl_Orders2
      // 
      this.pnl_Orders2.BackColor = System.Drawing.Color.LightGray;
      this.pnl_Orders2.Controls.Add(this.chk_Bundle2);
      this.pnl_Orders2.Controls.Add(this.cmb_Balance2);
      this.pnl_Orders2.Controls.Add(this.cmb_DischargeSide2);
      this.pnl_Orders2.Controls.Add(this.btn_ScorersOptions2);
      this.pnl_Orders2.Controls.Add(this.ttb_CustAddress2);
      this.pnl_Orders2.Controls.Add(this.ttb_MthNextMachine2);
      this.pnl_Orders2.Controls.Add(this.ttb_MthCode2);
      this.pnl_Orders2.Controls.Add(this.ttl_mthData2);
      this.pnl_Orders2.Controls.Add(this.cmb_cutToPattern2);
      this.pnl_Orders2.Controls.Add(this.ttl_KnifeData2);
      this.pnl_Orders2.Controls.Add(this.ttb_StackerNote2);
      this.pnl_Orders2.Controls.Add(this.chk_StackerPallet2);
      this.pnl_Orders2.Controls.Add(this.chk_StackerContStack2);
      this.pnl_Orders2.Controls.Add(this.ttl_stackerData2);
      this.pnl_Orders2.Controls.Add(this.ttb_DataToPrint2_4);
      this.pnl_Orders2.Controls.Add(this.ttb_DataToPrint2_3);
      this.pnl_Orders2.Controls.Add(this.chk_CutToMark2);
      this.pnl_Orders2.Controls.Add(this.ttb_DataToPrint2_2);
      this.pnl_Orders2.Controls.Add(this.mkt_Delivery2);
      this.pnl_Orders2.Controls.Add(this.ttb_DataToPrint2_1);
      this.pnl_Orders2.Controls.Add(this.nud_stacksXpallet2);
      this.pnl_Orders2.Controls.Add(this.ttl_DataToPrint2);
      this.pnl_Orders2.Controls.Add(this.nud_sheetsXstack2);
      this.pnl_Orders2.Controls.Add(this.nud_SheetsDesired2);
      this.pnl_Orders2.Controls.Add(this.nud_Boxes2);
      this.pnl_Orders2.Controls.Add(this.nud_length2);
      this.pnl_Orders2.Controls.Add(this.nud_Width2);
      this.pnl_Orders2.Controls.Add(this.cmb_sending2);
      this.pnl_Orders2.Controls.Add(this.ttl_Sheets2);
      this.pnl_Orders2.Controls.Add(this.ttl_Format2);
      this.pnl_Orders2.Controls.Add(this.cmb_Type2);
      this.pnl_Orders2.Controls.Add(this.ttl_Score2);
      this.pnl_Orders2.Controls.Add(this.cmb_Positioning2);
      this.pnl_Orders2.Controls.Add(this.ttl_ord2);
      this.pnl_Orders2.Controls.Add(this.ttb_CustName2);
      this.pnl_Orders2.Controls.Add(this.ttb_OrderNumber2);
      this.pnl_Orders2.Controls.Add(this.cmb_Level2);
      this.pnl_Orders2.Controls.Add(this.ttb_Measures2);
      this.pnl_Orders2.Location = new System.Drawing.Point(796, 21);
      this.pnl_Orders2.Name = "pnl_Orders2";
      this.pnl_Orders2.Size = new System.Drawing.Size(376, 963);
      this.pnl_Orders2.TabIndex = 112;
      // 
      // chk_Bundle2
      // 
      this.chk_Bundle2.BackColor = System.Drawing.Color.LightGray;
      this.chk_Bundle2.Checked = false;
      this.chk_Bundle2.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_Bundle2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Bundle2.ImagePassword = null;
      this.chk_Bundle2.Location = new System.Drawing.Point(7, 763);
      this.chk_Bundle2.Name = "chk_Bundle2";
      this.chk_Bundle2.ReadOnly = true;
      this.chk_Bundle2.Size = new System.Drawing.Size(80, 65);
      this.chk_Bundle2.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_Bundle2.TabIndex = 150;
      this.chk_Bundle2.TabStop = false;
      this.chk_Bundle2.Text = "Bundle";
      this.chk_Bundle2.TitleBackColor = System.Drawing.Color.LightGray;
      this.chk_Bundle2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_Bundle2.TranslateText = true;
      // 
      // cmb_Balance2
      // 
      this.cmb_Balance2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Balance2.CanCancel = false;
      this.cmb_Balance2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Balance2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Balance2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Balance2.ImagePassword = null;
      this.cmb_Balance2.Location = new System.Drawing.Point(0, 835);
      this.cmb_Balance2.Name = "cmb_Balance2";
      this.cmb_Balance2.ReadOnly = true;
      this.cmb_Balance2.SelectedIndex = -1;
      this.cmb_Balance2.SelectedItem = "";
      this.cmb_Balance2.SelectedItemData = "";
      this.cmb_Balance2.Size = new System.Drawing.Size(376, 50);
      this.cmb_Balance2.Sorted = false;
      this.cmb_Balance2.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_Balance2.TabIndex = 152;
      this.cmb_Balance2.TabStop = false;
      this.cmb_Balance2.Text = "Balance";
      this.cmb_Balance2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Balance2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Balance2.TranslateText = true;
      this.cmb_Balance2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // cmb_DischargeSide2
      // 
      this.cmb_DischargeSide2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_DischargeSide2.CanCancel = false;
      this.cmb_DischargeSide2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_DischargeSide2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_DischargeSide2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DischargeSide2.ImagePassword = null;
      this.cmb_DischargeSide2.Location = new System.Drawing.Point(7, 713);
      this.cmb_DischargeSide2.Name = "cmb_DischargeSide2";
      this.cmb_DischargeSide2.ReadOnly = true;
      this.cmb_DischargeSide2.SelectedIndex = -1;
      this.cmb_DischargeSide2.SelectedItem = "";
      this.cmb_DischargeSide2.SelectedItemData = "";
      this.cmb_DischargeSide2.Size = new System.Drawing.Size(164, 50);
      this.cmb_DischargeSide2.Sorted = false;
      this.cmb_DischargeSide2.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_DischargeSide2.TabIndex = 147;
      this.cmb_DischargeSide2.TabStop = false;
      this.cmb_DischargeSide2.Text = "Side";
      this.cmb_DischargeSide2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DischargeSide2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DischargeSide2.TranslateText = true;
      this.cmb_DischargeSide2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // btn_ScorersOptions2
      // 
      this.btn_ScorersOptions2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btn_ScorersOptions2.Font = new System.Drawing.Font("Arial Unicode MS", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_ScorersOptions2.Location = new System.Drawing.Point(352, 186);
      this.btn_ScorersOptions2.Margin = new System.Windows.Forms.Padding(0);
      this.btn_ScorersOptions2.Name = "btn_ScorersOptions2";
      this.btn_ScorersOptions2.Size = new System.Drawing.Size(24, 20);
      this.btn_ScorersOptions2.TabIndex = 142;
      this.btn_ScorersOptions2.Text = "...";
      this.btn_ScorersOptions2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btn_ScorersOptions2.UseVisualStyleBackColor = true;
      this.btn_ScorersOptions2.Visible = false;
      this.btn_ScorersOptions2.Click += new System.EventHandler(this.btn_ScorersOptions2_Click);
      // 
      // ttb_CustAddress2
      // 
      this.ttb_CustAddress2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CustAddress2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CustAddress2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustAddress2.ImagePassword = null;
      this.ttb_CustAddress2.ImeMode = System.Windows.Forms.ImeMode.On;
      this.ttb_CustAddress2.Location = new System.Drawing.Point(7, 130);
      this.ttb_CustAddress2.MaxChar = 30;
      this.ttb_CustAddress2.Multiline = true;
      this.ttb_CustAddress2.Name = "ttb_CustAddress2";
      this.ttb_CustAddress2.PasswordChar = '\0';
      this.ttb_CustAddress2.ReadOnly = true;
      this.ttb_CustAddress2.Size = new System.Drawing.Size(216, 50);
      this.ttb_CustAddress2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CustAddress2.TabIndex = 39;
      this.ttb_CustAddress2.TabStop = false;
      this.ttb_CustAddress2.Text = "Customer Address";
      this.ttb_CustAddress2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_CustAddress2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustAddress2.TranslateText = true;
      this.ttb_CustAddress2.Value = " ";
      this.ttb_CustAddress2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_MthNextMachine2
      // 
      this.ttb_MthNextMachine2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_MthNextMachine2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_MthNextMachine2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MthNextMachine2.ImagePassword = null;
      this.ttb_MthNextMachine2.Location = new System.Drawing.Point(8, 638);
      this.ttb_MthNextMachine2.MaxChar = 20;
      this.ttb_MthNextMachine2.Multiline = false;
      this.ttb_MthNextMachine2.Name = "ttb_MthNextMachine2";
      this.ttb_MthNextMachine2.PasswordChar = '\0';
      this.ttb_MthNextMachine2.ReadOnly = true;
      this.ttb_MthNextMachine2.Size = new System.Drawing.Size(211, 50);
      this.ttb_MthNextMachine2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_MthNextMachine2.TabIndex = 145;
      this.ttb_MthNextMachine2.TabStop = false;
      this.ttb_MthNextMachine2.Text = "Next Machine";
      this.ttb_MthNextMachine2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_MthNextMachine2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MthNextMachine2.TranslateText = true;
      this.ttb_MthNextMachine2.Value = "ABCDEFG";
      this.ttb_MthNextMachine2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_MthCode2
      // 
      this.ttb_MthCode2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_MthCode2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_MthCode2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MthCode2.ImagePassword = null;
      this.ttb_MthCode2.Location = new System.Drawing.Point(8, 587);
      this.ttb_MthCode2.MaxChar = 20;
      this.ttb_MthCode2.Multiline = false;
      this.ttb_MthCode2.Name = "ttb_MthCode2";
      this.ttb_MthCode2.PasswordChar = '\0';
      this.ttb_MthCode2.ReadOnly = true;
      this.ttb_MthCode2.Size = new System.Drawing.Size(211, 50);
      this.ttb_MthCode2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_MthCode2.TabIndex = 143;
      this.ttb_MthCode2.TabStop = false;
      this.ttb_MthCode2.Text = "Code";
      this.ttb_MthCode2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_MthCode2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MthCode2.TranslateText = true;
      this.ttb_MthCode2.Value = "ABCDEFG";
      this.ttb_MthCode2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_mthData2
      // 
      this.ttl_mthData2.BackColor = System.Drawing.Color.Black;
      this.ttl_mthData2.CausesValidation = false;
      this.ttl_mthData2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_mthData2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_mthData2.ForeColor = System.Drawing.Color.White;
      this.ttl_mthData2.Location = new System.Drawing.Point(0, 562);
      this.ttl_mthData2.Name = "ttl_mthData2";
      this.ttl_mthData2.Size = new System.Drawing.Size(376, 126);
      this.ttl_mthData2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_mthData2.TabIndex = 137;
      this.ttl_mthData2.TabStop = false;
      this.ttl_mthData2.Text = "MTH";
      this.ttl_mthData2.TranslateText = true;
      // 
      // cmb_cutToPattern2
      // 
      this.cmb_cutToPattern2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_cutToPattern2.CanCancel = true;
      this.cmb_cutToPattern2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_cutToPattern2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_cutToPattern2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_cutToPattern2.ImagePassword = null;
      this.cmb_cutToPattern2.Location = new System.Drawing.Point(104, 913);
      this.cmb_cutToPattern2.Name = "cmb_cutToPattern2";
      this.cmb_cutToPattern2.ReadOnly = true;
      this.cmb_cutToPattern2.SelectedIndex = -1;
      this.cmb_cutToPattern2.SelectedItem = "";
      this.cmb_cutToPattern2.SelectedItemData = "";
      this.cmb_cutToPattern2.Size = new System.Drawing.Size(270, 50);
      this.cmb_cutToPattern2.Sorted = true;
      this.cmb_cutToPattern2.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_cutToPattern2.TabIndex = 154;
      this.cmb_cutToPattern2.TabStop = false;
      this.cmb_cutToPattern2.Text = "Cut To Pattern";
      this.cmb_cutToPattern2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_cutToPattern2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_cutToPattern2.TranslateText = false;
      this.cmb_cutToPattern2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_KnifeData2
      // 
      this.ttl_KnifeData2.BackColor = System.Drawing.Color.Black;
      this.ttl_KnifeData2.CausesValidation = false;
      this.ttl_KnifeData2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_KnifeData2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_KnifeData2.ForeColor = System.Drawing.Color.White;
      this.ttl_KnifeData2.Location = new System.Drawing.Point(0, 890);
      this.ttl_KnifeData2.Name = "ttl_KnifeData2";
      this.ttl_KnifeData2.Size = new System.Drawing.Size(376, 73);
      this.ttl_KnifeData2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_KnifeData2.TabIndex = 133;
      this.ttl_KnifeData2.TabStop = false;
      this.ttl_KnifeData2.Text = "Knife";
      this.ttl_KnifeData2.TranslateText = true;
      // 
      // ttb_StackerNote2
      // 
      this.ttb_StackerNote2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_StackerNote2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_StackerNote2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_StackerNote2.ImagePassword = null;
      this.ttb_StackerNote2.Location = new System.Drawing.Point(93, 763);
      this.ttb_StackerNote2.MaxChar = 58;
      this.ttb_StackerNote2.Multiline = true;
      this.ttb_StackerNote2.Name = "ttb_StackerNote2";
      this.ttb_StackerNote2.PasswordChar = '\0';
      this.ttb_StackerNote2.ReadOnly = true;
      this.ttb_StackerNote2.Size = new System.Drawing.Size(281, 65);
      this.ttb_StackerNote2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_StackerNote2.TabIndex = 151;
      this.ttb_StackerNote2.TabStop = false;
      this.ttb_StackerNote2.Text = "Note";
      this.ttb_StackerNote2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_StackerNote2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_StackerNote2.TranslateText = true;
      this.ttb_StackerNote2.Value = " ";
      this.ttb_StackerNote2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // chk_StackerPallet2
      // 
      this.chk_StackerPallet2.BackColor = System.Drawing.Color.LightGray;
      this.chk_StackerPallet2.Checked = false;
      this.chk_StackerPallet2.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_StackerPallet2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_StackerPallet2.ImagePassword = null;
      this.chk_StackerPallet2.Location = new System.Drawing.Point(287, 714);
      this.chk_StackerPallet2.Name = "chk_StackerPallet2";
      this.chk_StackerPallet2.ReadOnly = true;
      this.chk_StackerPallet2.Size = new System.Drawing.Size(89, 50);
      this.chk_StackerPallet2.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_StackerPallet2.TabIndex = 149;
      this.chk_StackerPallet2.TabStop = false;
      this.chk_StackerPallet2.Text = "Pallet";
      this.chk_StackerPallet2.TitleBackColor = System.Drawing.Color.LightGray;
      this.chk_StackerPallet2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_StackerPallet2.TranslateText = true;
      // 
      // chk_StackerContStack2
      // 
      this.chk_StackerContStack2.BackColor = System.Drawing.Color.LightGray;
      this.chk_StackerContStack2.Checked = false;
      this.chk_StackerContStack2.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_StackerContStack2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_StackerContStack2.ImagePassword = null;
      this.chk_StackerContStack2.Location = new System.Drawing.Point(184, 714);
      this.chk_StackerContStack2.Name = "chk_StackerContStack2";
      this.chk_StackerContStack2.ReadOnly = true;
      this.chk_StackerContStack2.Size = new System.Drawing.Size(89, 50);
      this.chk_StackerContStack2.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_StackerContStack2.TabIndex = 148;
      this.chk_StackerContStack2.TabStop = false;
      this.chk_StackerContStack2.Text = "Cont. Stack";
      this.chk_StackerContStack2.TitleBackColor = System.Drawing.Color.LightGray;
      this.chk_StackerContStack2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_StackerContStack2.TranslateText = true;
      // 
      // ttl_stackerData2
      // 
      this.ttl_stackerData2.BackColor = System.Drawing.Color.Black;
      this.ttl_stackerData2.CausesValidation = false;
      this.ttl_stackerData2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_stackerData2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_stackerData2.ForeColor = System.Drawing.Color.White;
      this.ttl_stackerData2.Location = new System.Drawing.Point(0, 692);
      this.ttl_stackerData2.Name = "ttl_stackerData2";
      this.ttl_stackerData2.Size = new System.Drawing.Size(376, 137);
      this.ttl_stackerData2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_stackerData2.TabIndex = 129;
      this.ttl_stackerData2.TabStop = false;
      this.ttl_stackerData2.Text = "Stacker";
      this.ttl_stackerData2.TranslateText = true;
      // 
      // ttb_DataToPrint2_4
      // 
      this.ttb_DataToPrint2_4.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint2_4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
      this.ttb_DataToPrint2_4.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint2_4.ImagePassword = null;
      this.ttb_DataToPrint2_4.Location = new System.Drawing.Point(173, 505);
      this.ttb_DataToPrint2_4.MaxChar = 58;
      this.ttb_DataToPrint2_4.Multiline = true;
      this.ttb_DataToPrint2_4.Name = "ttb_DataToPrint2_4";
      this.ttb_DataToPrint2_4.PasswordChar = '\0';
      this.ttb_DataToPrint2_4.ReadOnly = true;
      this.ttb_DataToPrint2_4.Size = new System.Drawing.Size(200, 50);
      this.ttb_DataToPrint2_4.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint2_4.TabIndex = 142;
      this.ttb_DataToPrint2_4.TabStop = false;
      this.ttb_DataToPrint2_4.Text = "4";
      this.ttb_DataToPrint2_4.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint2_4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint2_4.TranslateText = false;
      this.ttb_DataToPrint2_4.Value = " ";
      this.ttb_DataToPrint2_4.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_DataToPrint2_3
      // 
      this.ttb_DataToPrint2_3.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint2_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
      this.ttb_DataToPrint2_3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint2_3.ImagePassword = null;
      this.ttb_DataToPrint2_3.Location = new System.Drawing.Point(8, 505);
      this.ttb_DataToPrint2_3.MaxChar = 58;
      this.ttb_DataToPrint2_3.Multiline = true;
      this.ttb_DataToPrint2_3.Name = "ttb_DataToPrint2_3";
      this.ttb_DataToPrint2_3.PasswordChar = '\0';
      this.ttb_DataToPrint2_3.ReadOnly = true;
      this.ttb_DataToPrint2_3.Size = new System.Drawing.Size(164, 50);
      this.ttb_DataToPrint2_3.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint2_3.TabIndex = 141;
      this.ttb_DataToPrint2_3.TabStop = false;
      this.ttb_DataToPrint2_3.Text = "3";
      this.ttb_DataToPrint2_3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint2_3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint2_3.TranslateText = false;
      this.ttb_DataToPrint2_3.Value = " ";
      this.ttb_DataToPrint2_3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // chk_CutToMark2
      // 
      this.chk_CutToMark2.BackColor = System.Drawing.Color.LightGray;
      this.chk_CutToMark2.Checked = false;
      this.chk_CutToMark2.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_CutToMark2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_CutToMark2.ImagePassword = null;
      this.chk_CutToMark2.Location = new System.Drawing.Point(8, 913);
      this.chk_CutToMark2.Name = "chk_CutToMark2";
      this.chk_CutToMark2.ReadOnly = true;
      this.chk_CutToMark2.Size = new System.Drawing.Size(89, 50);
      this.chk_CutToMark2.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_CutToMark2.TabIndex = 153;
      this.chk_CutToMark2.TabStop = false;
      this.chk_CutToMark2.Text = "Cut To Mark";
      this.chk_CutToMark2.TitleBackColor = System.Drawing.Color.LightGray;
      this.chk_CutToMark2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_CutToMark2.TranslateText = false;
      // 
      // ttb_DataToPrint2_2
      // 
      this.ttb_DataToPrint2_2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint2_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_DataToPrint2_2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint2_2.ImagePassword = null;
      this.ttb_DataToPrint2_2.Location = new System.Drawing.Point(8, 449);
      this.ttb_DataToPrint2_2.MaxChar = 58;
      this.ttb_DataToPrint2_2.Multiline = true;
      this.ttb_DataToPrint2_2.Name = "ttb_DataToPrint2_2";
      this.ttb_DataToPrint2_2.PasswordChar = '\0';
      this.ttb_DataToPrint2_2.ReadOnly = true;
      this.ttb_DataToPrint2_2.Size = new System.Drawing.Size(365, 50);
      this.ttb_DataToPrint2_2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint2_2.TabIndex = 140;
      this.ttb_DataToPrint2_2.TabStop = false;
      this.ttb_DataToPrint2_2.Text = "2";
      this.ttb_DataToPrint2_2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint2_2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint2_2.TranslateText = false;
      this.ttb_DataToPrint2_2.Value = " ";
      this.ttb_DataToPrint2_2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // mkt_Delivery2
      // 
      this.mkt_Delivery2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.mkt_Delivery2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.mkt_Delivery2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.mkt_Delivery2.ImagePassword = null;
      this.mkt_Delivery2.Location = new System.Drawing.Point(224, 638);
      this.mkt_Delivery2.Name = "mkt_Delivery2";
      this.mkt_Delivery2.PasswordChar = '\0';
      this.mkt_Delivery2.ReadOnly = true;
      this.mkt_Delivery2.Size = new System.Drawing.Size(149, 50);
      this.mkt_Delivery2.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.mkt_Delivery2.TabIndex = 146;
      this.mkt_Delivery2.TabStop = false;
      this.mkt_Delivery2.Text = "Delivery";
      this.mkt_Delivery2.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.Date;
      this.mkt_Delivery2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.mkt_Delivery2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.mkt_Delivery2.TranslateText = true;
      this.mkt_Delivery2.Value = " ";
      this.mkt_Delivery2.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.mkt_Delivery2.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.mkt_Delivery2.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.mkt_Delivery2.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.mkt_Delivery2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.mkt_Delivery2.Validating += new System.ComponentModel.CancelEventHandler(this.Delivery_Validating);
      this.mkt_Delivery2.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttb_DataToPrint2_1
      // 
      this.ttb_DataToPrint2_1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint2_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_DataToPrint2_1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint2_1.ImagePassword = null;
      this.ttb_DataToPrint2_1.Location = new System.Drawing.Point(8, 394);
      this.ttb_DataToPrint2_1.MaxChar = 58;
      this.ttb_DataToPrint2_1.Multiline = true;
      this.ttb_DataToPrint2_1.Name = "ttb_DataToPrint2_1";
      this.ttb_DataToPrint2_1.PasswordChar = '\0';
      this.ttb_DataToPrint2_1.ReadOnly = true;
      this.ttb_DataToPrint2_1.Size = new System.Drawing.Size(365, 50);
      this.ttb_DataToPrint2_1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint2_1.TabIndex = 139;
      this.ttb_DataToPrint2_1.TabStop = false;
      this.ttb_DataToPrint2_1.Text = "1";
      this.ttb_DataToPrint2_1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint2_1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint2_1.TranslateText = false;
      this.ttb_DataToPrint2_1.Value = " ";
      this.ttb_DataToPrint2_1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // nud_stacksXpallet2
      // 
      this.nud_stacksXpallet2.DecimalPlaces = 0;
      this.nud_stacksXpallet2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_stacksXpallet2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_stacksXpallet2.ImagePassword = null;
      this.nud_stacksXpallet2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_stacksXpallet2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_stacksXpallet2.Location = new System.Drawing.Point(116, 320);
      this.nud_stacksXpallet2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_stacksXpallet2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_stacksXpallet2.Name = "nud_stacksXpallet2";
      this.nud_stacksXpallet2.ReadOnly = true;
      this.nud_stacksXpallet2.Size = new System.Drawing.Size(110, 46);
      this.nud_stacksXpallet2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_stacksXpallet2.TabIndex = 46;
      this.nud_stacksXpallet2.TabStop = false;
      this.nud_stacksXpallet2.Text = "STACKS";
      this.nud_stacksXpallet2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_stacksXpallet2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_stacksXpallet2.TranslateText = true;
      this.nud_stacksXpallet2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_stacksXpallet2.Value = "0";
      this.nud_stacksXpallet2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_stacksXpallet2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_stacksXpallet2.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_stacksXpallet2.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttl_DataToPrint2
      // 
      this.ttl_DataToPrint2.BackColor = System.Drawing.Color.Black;
      this.ttl_DataToPrint2.CausesValidation = false;
      this.ttl_DataToPrint2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_DataToPrint2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DataToPrint2.ForeColor = System.Drawing.Color.White;
      this.ttl_DataToPrint2.Location = new System.Drawing.Point(0, 372);
      this.ttl_DataToPrint2.Name = "ttl_DataToPrint2";
      this.ttl_DataToPrint2.Size = new System.Drawing.Size(376, 185);
      this.ttl_DataToPrint2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_DataToPrint2.TabIndex = 124;
      this.ttl_DataToPrint2.TabStop = false;
      this.ttl_DataToPrint2.Text = "Data To Print";
      this.ttl_DataToPrint2.TranslateText = true;
      // 
      // nud_sheetsXstack2
      // 
      this.nud_sheetsXstack2.DecimalPlaces = 0;
      this.nud_sheetsXstack2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_sheetsXstack2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_sheetsXstack2.ImagePassword = null;
      this.nud_sheetsXstack2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_sheetsXstack2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_sheetsXstack2.Location = new System.Drawing.Point(124, 264);
      this.nud_sheetsXstack2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_sheetsXstack2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_sheetsXstack2.Name = "nud_sheetsXstack2";
      this.nud_sheetsXstack2.ReadOnly = true;
      this.nud_sheetsXstack2.Size = new System.Drawing.Size(102, 46);
      this.nud_sheetsXstack2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_sheetsXstack2.TabIndex = 45;
      this.nud_sheetsXstack2.TabStop = false;
      this.nud_sheetsXstack2.Text = "Per stack";
      this.nud_sheetsXstack2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_sheetsXstack2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_sheetsXstack2.TranslateText = true;
      this.nud_sheetsXstack2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_sheetsXstack2.Value = "0";
      this.nud_sheetsXstack2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_sheetsXstack2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_sheetsXstack2.Validating += new System.ComponentModel.CancelEventHandler(this.sheetsXstack2_Validating);
      this.nud_sheetsXstack2.Enter += new System.EventHandler(this.sheetsXstack2_Enter);
      // 
      // nud_SheetsDesired2
      // 
      this.nud_SheetsDesired2.DecimalPlaces = 0;
      this.nud_SheetsDesired2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_SheetsDesired2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SheetsDesired2.ImagePassword = null;
      this.nud_SheetsDesired2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SheetsDesired2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_SheetsDesired2.Location = new System.Drawing.Point(124, 216);
      this.nud_SheetsDesired2.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
      this.nud_SheetsDesired2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SheetsDesired2.Name = "nud_SheetsDesired2";
      this.nud_SheetsDesired2.ReadOnly = true;
      this.nud_SheetsDesired2.Size = new System.Drawing.Size(102, 46);
      this.nud_SheetsDesired2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_SheetsDesired2.TabIndex = 44;
      this.nud_SheetsDesired2.TabStop = false;
      this.nud_SheetsDesired2.Text = "Desired";
      this.nud_SheetsDesired2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_SheetsDesired2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SheetsDesired2.TranslateText = true;
      this.nud_SheetsDesired2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_SheetsDesired2.Value = "0";
      this.nud_SheetsDesired2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_SheetsDesired2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_SheetsDesired2.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_SheetsDesired2.Enter += new System.EventHandler(this.CleanSuggestionFields);
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
      this.nud_Boxes2.Location = new System.Drawing.Point(8, 320);
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
      this.nud_Boxes2.Size = new System.Drawing.Size(102, 46);
      this.nud_Boxes2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Boxes2.TabIndex = 43;
      this.nud_Boxes2.TabStop = false;
      this.nud_Boxes2.Text = "Boxes";
      this.nud_Boxes2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Boxes2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Boxes2.TranslateText = true;
      this.nud_Boxes2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Boxes2.Value = "0";
      this.nud_Boxes2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Boxes2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Boxes2.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_Boxes2.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // nud_length2
      // 
      this.nud_length2.DecimalPlaces = 0;
      this.nud_length2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_length2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_length2.ImagePassword = null;
      this.nud_length2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_length2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_length2.Location = new System.Drawing.Point(8, 264);
      this.nud_length2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_length2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_length2.Name = "nud_length2";
      this.nud_length2.ReadOnly = true;
      this.nud_length2.Size = new System.Drawing.Size(102, 46);
      this.nud_length2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_length2.TabIndex = 42;
      this.nud_length2.TabStop = false;
      this.nud_length2.Text = "Length";
      this.nud_length2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_length2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_length2.TranslateText = true;
      this.nud_length2.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_length2.Value = "0";
      this.nud_length2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_length2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_length2.Validating += new System.ComponentModel.CancelEventHandler(this.Flength_Validating);
      this.nud_length2.Enter += new System.EventHandler(this.Flength_Enter);
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
      this.nud_Width2.Location = new System.Drawing.Point(8, 216);
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
      this.nud_Width2.Size = new System.Drawing.Size(102, 46);
      this.nud_Width2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Width2.TabIndex = 41;
      this.nud_Width2.TabStop = false;
      this.nud_Width2.Text = "Width";
      this.nud_Width2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Width2.TranslateText = true;
      this.nud_Width2.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Width2.Value = "0";
      this.nud_Width2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Width2.Validating += new System.ComponentModel.CancelEventHandler(this.Fwidth2_Validating);
      this.nud_Width2.Leave += new System.EventHandler(this.cmb_Quality_OR_cmb_Level_OR_cmb_Sending_SelectedItemChanged);
      this.nud_Width2.Enter += new System.EventHandler(this.Fwidth2_Enter);
      // 
      // cmb_sending2
      // 
      this.cmb_sending2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_sending2.CanCancel = false;
      this.cmb_sending2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_sending2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_sending2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_sending2.ImagePassword = null;
      this.cmb_sending2.Location = new System.Drawing.Point(224, 587);
      this.cmb_sending2.Name = "cmb_sending2";
      this.cmb_sending2.ReadOnly = true;
      this.cmb_sending2.SelectedIndex = -1;
      this.cmb_sending2.SelectedItem = "";
      this.cmb_sending2.SelectedItemData = "";
      this.cmb_sending2.Size = new System.Drawing.Size(149, 50);
      this.cmb_sending2.Sorted = false;
      this.cmb_sending2.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_sending2.TabIndex = 144;
      this.cmb_sending2.TabStop = false;
      this.cmb_sending2.Text = "Sending";
      this.cmb_sending2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_sending2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_sending2.TranslateText = true;
      this.cmb_sending2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_sending2.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttl_Sheets2
      // 
      this.ttl_Sheets2.BackColor = System.Drawing.Color.Black;
      this.ttl_Sheets2.CausesValidation = false;
      this.ttl_Sheets2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Sheets2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Sheets2.ForeColor = System.Drawing.Color.White;
      this.ttl_Sheets2.Location = new System.Drawing.Point(116, 187);
      this.ttl_Sheets2.Name = "ttl_Sheets2";
      this.ttl_Sheets2.Size = new System.Drawing.Size(110, 124);
      this.ttl_Sheets2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Sheets2.TabIndex = 99;
      this.ttl_Sheets2.TabStop = false;
      this.ttl_Sheets2.Text = "SHEETS";
      this.ttl_Sheets2.TranslateText = true;
      // 
      // ttl_Format2
      // 
      this.ttl_Format2.BackColor = System.Drawing.Color.Black;
      this.ttl_Format2.CausesValidation = false;
      this.ttl_Format2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Format2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Format2.ForeColor = System.Drawing.Color.White;
      this.ttl_Format2.Location = new System.Drawing.Point(0, 187);
      this.ttl_Format2.Name = "ttl_Format2";
      this.ttl_Format2.Size = new System.Drawing.Size(110, 181);
      this.ttl_Format2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Format2.TabIndex = 98;
      this.ttl_Format2.TabStop = false;
      this.ttl_Format2.Text = "FORMAT";
      this.ttl_Format2.TranslateText = true;
      // 
      // cmb_Type2
      // 
      this.cmb_Type2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Type2.CanCancel = false;
      this.cmb_Type2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Type2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Type2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type2.ImagePassword = null;
      this.cmb_Type2.Location = new System.Drawing.Point(240, 130);
      this.cmb_Type2.Name = "cmb_Type2";
      this.cmb_Type2.ReadOnly = true;
      this.cmb_Type2.SelectedIndex = -1;
      this.cmb_Type2.SelectedItem = "";
      this.cmb_Type2.SelectedItemData = "";
      this.cmb_Type2.Size = new System.Drawing.Size(136, 50);
      this.cmb_Type2.Sorted = false;
      this.cmb_Type2.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Type2.TabIndex = 48;
      this.cmb_Type2.TabStop = false;
      this.cmb_Type2.Text = "Type";
      this.cmb_Type2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Type2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type2.TranslateText = true;
      this.cmb_Type2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Type2.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttl_Score2
      // 
      this.ttl_Score2.BackColor = System.Drawing.Color.Black;
      this.ttl_Score2.CausesValidation = false;
      this.ttl_Score2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Score2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Score2.ForeColor = System.Drawing.Color.White;
      this.ttl_Score2.Location = new System.Drawing.Point(232, 55);
      this.ttl_Score2.Name = "ttl_Score2";
      this.ttl_Score2.Size = new System.Drawing.Size(144, 312);
      this.ttl_Score2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Score2.TabIndex = 96;
      this.ttl_Score2.TabStop = false;
      this.ttl_Score2.Text = "SCORERS";
      this.ttl_Score2.TranslateText = true;
      // 
      // cmb_Positioning2
      // 
      this.cmb_Positioning2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Positioning2.CanCancel = false;
      this.cmb_Positioning2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Positioning2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Positioning2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning2.ImagePassword = null;
      this.cmb_Positioning2.Location = new System.Drawing.Point(240, 75);
      this.cmb_Positioning2.Name = "cmb_Positioning2";
      this.cmb_Positioning2.ReadOnly = true;
      this.cmb_Positioning2.SelectedIndex = -1;
      this.cmb_Positioning2.SelectedItem = "";
      this.cmb_Positioning2.SelectedItemData = "";
      this.cmb_Positioning2.Size = new System.Drawing.Size(136, 50);
      this.cmb_Positioning2.Sorted = false;
      this.cmb_Positioning2.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Positioning2.TabIndex = 47;
      this.cmb_Positioning2.TabStop = false;
      this.cmb_Positioning2.Text = "Positioning";
      this.cmb_Positioning2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning2.TranslateText = true;
      this.cmb_Positioning2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning2.SelectedItemChanged += new System.EventHandler(this.cmb_Positioning2_SelectedItemChanged);
      this.cmb_Positioning2.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttl_ord2
      // 
      this.ttl_ord2.BackColor = System.Drawing.Color.Black;
      this.ttl_ord2.CausesValidation = false;
      this.ttl_ord2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ord2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ord2.ForeColor = System.Drawing.Color.White;
      this.ttl_ord2.Location = new System.Drawing.Point(0, 0);
      this.ttl_ord2.Name = "ttl_ord2";
      this.ttl_ord2.Size = new System.Drawing.Size(226, 182);
      this.ttl_ord2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_ord2.TabIndex = 92;
      this.ttl_ord2.TabStop = false;
      this.ttl_ord2.Text = "ORDER 3";
      this.ttl_ord2.TranslateText = true;
      // 
      // ttb_CustName2
      // 
      this.ttb_CustName2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CustName2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CustName2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustName2.ImagePassword = null;
      this.ttb_CustName2.Location = new System.Drawing.Point(8, 76);
      this.ttb_CustName2.MaxChar = 30;
      this.ttb_CustName2.Multiline = false;
      this.ttb_CustName2.Name = "ttb_CustName2";
      this.ttb_CustName2.PasswordChar = '\0';
      this.ttb_CustName2.ReadOnly = true;
      this.ttb_CustName2.Size = new System.Drawing.Size(216, 50);
      this.ttb_CustName2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CustName2.TabIndex = 38;
      this.ttb_CustName2.TabStop = false;
      this.ttb_CustName2.Text = "Customer Name";
      this.ttb_CustName2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_CustName2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustName2.TranslateText = true;
      this.ttb_CustName2.Value = " ";
      this.ttb_CustName2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_CustName2.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttb_OrderNumber2
      // 
      this.ttb_OrderNumber2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_OrderNumber2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_OrderNumber2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_OrderNumber2.ImagePassword = null;
      this.ttb_OrderNumber2.Location = new System.Drawing.Point(8, 22);
      this.ttb_OrderNumber2.MaxChar = 20;
      this.ttb_OrderNumber2.Multiline = false;
      this.ttb_OrderNumber2.Name = "ttb_OrderNumber2";
      this.ttb_OrderNumber2.PasswordChar = '\0';
      this.ttb_OrderNumber2.ReadOnly = true;
      this.ttb_OrderNumber2.Size = new System.Drawing.Size(216, 50);
      this.ttb_OrderNumber2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_OrderNumber2.TabIndex = 37;
      this.ttb_OrderNumber2.TabStop = false;
      this.ttb_OrderNumber2.Text = "Number";
      this.ttb_OrderNumber2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_OrderNumber2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_OrderNumber2.TranslateText = true;
      this.ttb_OrderNumber2.Value = " ";
      this.ttb_OrderNumber2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_OrderNumber2.Validating += new System.ComponentModel.CancelEventHandler(this.ttb_NotNullString_Validating);
      this.ttb_OrderNumber2.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // cmb_Level2
      // 
      this.cmb_Level2.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Level2.CanCancel = false;
      this.cmb_Level2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Level2.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Level2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Level2.ImagePassword = null;
      this.cmb_Level2.Location = new System.Drawing.Point(232, 0);
      this.cmb_Level2.Name = "cmb_Level2";
      this.cmb_Level2.ReadOnly = true;
      this.cmb_Level2.SelectedIndex = -1;
      this.cmb_Level2.SelectedItem = "";
      this.cmb_Level2.SelectedItemData = "";
      this.cmb_Level2.Size = new System.Drawing.Size(144, 50);
      this.cmb_Level2.Sorted = false;
      this.cmb_Level2.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_Level2.TabIndex = 36;
      this.cmb_Level2.TabStop = false;
      this.cmb_Level2.Text = "LEVEL";
      this.cmb_Level2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Level2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Level2.TranslateText = true;
      this.cmb_Level2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Level2.SelectedItemChanged += new System.EventHandler(this.cmb_Quality_OR_cmb_Level_OR_cmb_Sending_SelectedItemChanged);
      this.cmb_Level2.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttb_Measures2
      // 
      this.ttb_Measures2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_Measures2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Measures2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Measures2.ImagePassword = null;
      this.ttb_Measures2.Location = new System.Drawing.Point(240, 185);
      //RFAZZI_FI03/12/2010_2. Aumentato il numero di cordoni massimo per scatola da 24 a 48. 
      //this.ttb_Measures2.MaxChar = 312;
      this.ttb_Measures2.MaxChar = 624;
      this.ttb_Measures2.Multiline = true;
      this.ttb_Measures2.Name = "ttb_Measures2";
      this.ttb_Measures2.PasswordChar = '\0';
      this.ttb_Measures2.ReadOnly = true;
      this.ttb_Measures2.Size = new System.Drawing.Size(136, 181);
      this.ttb_Measures2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_Measures2.TabIndex = 49;
      this.ttb_Measures2.TabStop = false;
      this.ttb_Measures2.Text = "Measures";
      this.ttb_Measures2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_Measures2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Measures2.TranslateText = true;
      this.ttb_Measures2.Value = " ";
      this.ttb_Measures2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_Measures2.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // pnl_Orders1
      // 
      this.pnl_Orders1.BackColor = System.Drawing.Color.LightGray;
      this.pnl_Orders1.Controls.Add(this.chk_Bundle1);
      this.pnl_Orders1.Controls.Add(this.cmb_Balance1);
      this.pnl_Orders1.Controls.Add(this.cmb_DischargeSide1);
      this.pnl_Orders1.Controls.Add(this.btn_ScorersOptions1);
      this.pnl_Orders1.Controls.Add(this.ttb_CustAddress1);
      this.pnl_Orders1.Controls.Add(this.ttb_MthNextMachine1);
      this.pnl_Orders1.Controls.Add(this.ttb_MthCode1);
      this.pnl_Orders1.Controls.Add(this.ttl_mthData1);
      this.pnl_Orders1.Controls.Add(this.cmb_cutToPattern1);
      this.pnl_Orders1.Controls.Add(this.ttl_KnifeData1);
      this.pnl_Orders1.Controls.Add(this.ttb_StackerNote1);
      this.pnl_Orders1.Controls.Add(this.chk_StackerPallet1);
      this.pnl_Orders1.Controls.Add(this.chk_StackerContStack1);
      this.pnl_Orders1.Controls.Add(this.ttl_stackerData1);
      this.pnl_Orders1.Controls.Add(this.ttb_DataToPrint1_4);
      this.pnl_Orders1.Controls.Add(this.ttb_DataToPrint1_2);
      this.pnl_Orders1.Controls.Add(this.ttb_DataToPrint1_1);
      this.pnl_Orders1.Controls.Add(this.ttl_DataToPrint1);
      this.pnl_Orders1.Controls.Add(this.chk_CutToMark1);
      this.pnl_Orders1.Controls.Add(this.mkt_Delivery1);
      this.pnl_Orders1.Controls.Add(this.nud_stacksXpallet1);
      this.pnl_Orders1.Controls.Add(this.nud_sheetsXstack1);
      this.pnl_Orders1.Controls.Add(this.nud_SheetsDesired1);
      this.pnl_Orders1.Controls.Add(this.nud_Boxes1);
      this.pnl_Orders1.Controls.Add(this.nud_length1);
      this.pnl_Orders1.Controls.Add(this.nud_Width1);
      this.pnl_Orders1.Controls.Add(this.cmb_sending1);
      this.pnl_Orders1.Controls.Add(this.ttl_Sheets1);
      this.pnl_Orders1.Controls.Add(this.ttl_Format1);
      this.pnl_Orders1.Controls.Add(this.cmb_Type1);
      this.pnl_Orders1.Controls.Add(this.ttl_Score1);
      this.pnl_Orders1.Controls.Add(this.cmb_Positioning1);
      this.pnl_Orders1.Controls.Add(this.ttl_ord1);
      this.pnl_Orders1.Controls.Add(this.ttb_CustName1);
      this.pnl_Orders1.Controls.Add(this.ttb_OrderNumber1);
      this.pnl_Orders1.Controls.Add(this.cmb_Level1);
      this.pnl_Orders1.Controls.Add(this.ttb_Measures1);
      this.pnl_Orders1.Controls.Add(this.ttb_DataToPrint1_3);
      this.pnl_Orders1.Location = new System.Drawing.Point(407, 21);
      this.pnl_Orders1.Name = "pnl_Orders1";
      this.pnl_Orders1.Size = new System.Drawing.Size(376, 963);
      this.pnl_Orders1.TabIndex = 111;
      // 
      // chk_Bundle1
      // 
      this.chk_Bundle1.BackColor = System.Drawing.Color.LightGray;
      this.chk_Bundle1.Checked = false;
      this.chk_Bundle1.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_Bundle1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Bundle1.ImagePassword = null;
      this.chk_Bundle1.Location = new System.Drawing.Point(7, 763);
      this.chk_Bundle1.Name = "chk_Bundle1";
      this.chk_Bundle1.ReadOnly = true;
      this.chk_Bundle1.Size = new System.Drawing.Size(80, 65);
      this.chk_Bundle1.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_Bundle1.TabIndex = 134;
      this.chk_Bundle1.TabStop = false;
      this.chk_Bundle1.Text = "Bundle";
      this.chk_Bundle1.TitleBackColor = System.Drawing.Color.LightGray;
      this.chk_Bundle1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_Bundle1.TranslateText = true;
      // 
      // cmb_Balance1
      // 
      this.cmb_Balance1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Balance1.CanCancel = false;
      this.cmb_Balance1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Balance1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Balance1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Balance1.ImagePassword = null;
      this.cmb_Balance1.Location = new System.Drawing.Point(0, 835);
      this.cmb_Balance1.Name = "cmb_Balance1";
      this.cmb_Balance1.ReadOnly = true;
      this.cmb_Balance1.SelectedIndex = -1;
      this.cmb_Balance1.SelectedItem = "";
      this.cmb_Balance1.SelectedItemData = "";
      this.cmb_Balance1.Size = new System.Drawing.Size(376, 50);
      this.cmb_Balance1.Sorted = false;
      this.cmb_Balance1.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_Balance1.TabIndex = 136;
      this.cmb_Balance1.TabStop = false;
      this.cmb_Balance1.Text = "Balance";
      this.cmb_Balance1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Balance1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Balance1.TranslateText = true;
      this.cmb_Balance1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // cmb_DischargeSide1
      // 
      this.cmb_DischargeSide1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_DischargeSide1.CanCancel = false;
      this.cmb_DischargeSide1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_DischargeSide1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_DischargeSide1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DischargeSide1.ImagePassword = null;
      this.cmb_DischargeSide1.Location = new System.Drawing.Point(7, 713);
      this.cmb_DischargeSide1.Name = "cmb_DischargeSide1";
      this.cmb_DischargeSide1.ReadOnly = true;
      this.cmb_DischargeSide1.SelectedIndex = -1;
      this.cmb_DischargeSide1.SelectedItem = "";
      this.cmb_DischargeSide1.SelectedItemData = "";
      this.cmb_DischargeSide1.Size = new System.Drawing.Size(164, 50);
      this.cmb_DischargeSide1.Sorted = false;
      this.cmb_DischargeSide1.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_DischargeSide1.TabIndex = 131;
      this.cmb_DischargeSide1.TabStop = false;
      this.cmb_DischargeSide1.Text = "Side";
      this.cmb_DischargeSide1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DischargeSide1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DischargeSide1.TranslateText = true;
      this.cmb_DischargeSide1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // btn_ScorersOptions1
      // 
      this.btn_ScorersOptions1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btn_ScorersOptions1.Font = new System.Drawing.Font("Arial Unicode MS", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_ScorersOptions1.Location = new System.Drawing.Point(352, 186);
      this.btn_ScorersOptions1.Margin = new System.Windows.Forms.Padding(0);
      this.btn_ScorersOptions1.Name = "btn_ScorersOptions1";
      this.btn_ScorersOptions1.Size = new System.Drawing.Size(24, 20);
      this.btn_ScorersOptions1.TabIndex = 133;
      this.btn_ScorersOptions1.Text = "...";
      this.btn_ScorersOptions1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btn_ScorersOptions1.UseVisualStyleBackColor = true;
      this.btn_ScorersOptions1.Visible = false;
      this.btn_ScorersOptions1.Click += new System.EventHandler(this.btn_ScorersOptions1_Click);
      // 
      // ttb_CustAddress1
      // 
      this.ttb_CustAddress1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CustAddress1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CustAddress1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustAddress1.ImagePassword = null;
      this.ttb_CustAddress1.ImeMode = System.Windows.Forms.ImeMode.On;
      this.ttb_CustAddress1.Location = new System.Drawing.Point(7, 130);
      this.ttb_CustAddress1.MaxChar = 30;
      this.ttb_CustAddress1.Multiline = true;
      this.ttb_CustAddress1.Name = "ttb_CustAddress1";
      this.ttb_CustAddress1.PasswordChar = '\0';
      this.ttb_CustAddress1.ReadOnly = true;
      this.ttb_CustAddress1.Size = new System.Drawing.Size(216, 50);
      this.ttb_CustAddress1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CustAddress1.TabIndex = 25;
      this.ttb_CustAddress1.TabStop = false;
      this.ttb_CustAddress1.Text = "Customer Address";
      this.ttb_CustAddress1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_CustAddress1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustAddress1.TranslateText = true;
      this.ttb_CustAddress1.Value = " ";
      this.ttb_CustAddress1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_MthNextMachine1
      // 
      this.ttb_MthNextMachine1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_MthNextMachine1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_MthNextMachine1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MthNextMachine1.ImagePassword = null;
      this.ttb_MthNextMachine1.Location = new System.Drawing.Point(8, 638);
      this.ttb_MthNextMachine1.MaxChar = 20;
      this.ttb_MthNextMachine1.Multiline = false;
      this.ttb_MthNextMachine1.Name = "ttb_MthNextMachine1";
      this.ttb_MthNextMachine1.PasswordChar = '\0';
      this.ttb_MthNextMachine1.ReadOnly = true;
      this.ttb_MthNextMachine1.Size = new System.Drawing.Size(211, 50);
      this.ttb_MthNextMachine1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_MthNextMachine1.TabIndex = 129;
      this.ttb_MthNextMachine1.TabStop = false;
      this.ttb_MthNextMachine1.Text = "Next Machine";
      this.ttb_MthNextMachine1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_MthNextMachine1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MthNextMachine1.TranslateText = true;
      this.ttb_MthNextMachine1.Value = "ABCDEFG";
      this.ttb_MthNextMachine1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_MthCode1
      // 
      this.ttb_MthCode1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_MthCode1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_MthCode1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MthCode1.ImagePassword = null;
      this.ttb_MthCode1.Location = new System.Drawing.Point(9, 588);
      this.ttb_MthCode1.MaxChar = 20;
      this.ttb_MthCode1.Multiline = false;
      this.ttb_MthCode1.Name = "ttb_MthCode1";
      this.ttb_MthCode1.PasswordChar = '\0';
      this.ttb_MthCode1.ReadOnly = true;
      this.ttb_MthCode1.Size = new System.Drawing.Size(211, 50);
      this.ttb_MthCode1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_MthCode1.TabIndex = 127;
      this.ttb_MthCode1.TabStop = false;
      this.ttb_MthCode1.Text = "Code";
      this.ttb_MthCode1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_MthCode1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MthCode1.TranslateText = true;
      this.ttb_MthCode1.Value = "ABCDEFG";
      this.ttb_MthCode1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_mthData1
      // 
      this.ttl_mthData1.BackColor = System.Drawing.Color.Black;
      this.ttl_mthData1.CausesValidation = false;
      this.ttl_mthData1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_mthData1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_mthData1.ForeColor = System.Drawing.Color.White;
      this.ttl_mthData1.Location = new System.Drawing.Point(0, 562);
      this.ttl_mthData1.Name = "ttl_mthData1";
      this.ttl_mthData1.Size = new System.Drawing.Size(376, 126);
      this.ttl_mthData1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_mthData1.TabIndex = 129;
      this.ttl_mthData1.TabStop = false;
      this.ttl_mthData1.Text = "MTH";
      this.ttl_mthData1.TranslateText = true;
      // 
      // cmb_cutToPattern1
      // 
      this.cmb_cutToPattern1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_cutToPattern1.CanCancel = true;
      this.cmb_cutToPattern1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_cutToPattern1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_cutToPattern1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_cutToPattern1.ImagePassword = null;
      this.cmb_cutToPattern1.Location = new System.Drawing.Point(104, 913);
      this.cmb_cutToPattern1.Name = "cmb_cutToPattern1";
      this.cmb_cutToPattern1.ReadOnly = true;
      this.cmb_cutToPattern1.SelectedIndex = -1;
      this.cmb_cutToPattern1.SelectedItem = "";
      this.cmb_cutToPattern1.SelectedItemData = "";
      this.cmb_cutToPattern1.Size = new System.Drawing.Size(270, 50);
      this.cmb_cutToPattern1.Sorted = true;
      this.cmb_cutToPattern1.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_cutToPattern1.TabIndex = 138;
      this.cmb_cutToPattern1.TabStop = false;
      this.cmb_cutToPattern1.Text = "Cut To Pattern";
      this.cmb_cutToPattern1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_cutToPattern1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_cutToPattern1.TranslateText = false;
      this.cmb_cutToPattern1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_KnifeData1
      // 
      this.ttl_KnifeData1.BackColor = System.Drawing.Color.Black;
      this.ttl_KnifeData1.CausesValidation = false;
      this.ttl_KnifeData1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_KnifeData1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_KnifeData1.ForeColor = System.Drawing.Color.White;
      this.ttl_KnifeData1.Location = new System.Drawing.Point(0, 890);
      this.ttl_KnifeData1.Name = "ttl_KnifeData1";
      this.ttl_KnifeData1.Size = new System.Drawing.Size(376, 73);
      this.ttl_KnifeData1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_KnifeData1.TabIndex = 128;
      this.ttl_KnifeData1.TabStop = false;
      this.ttl_KnifeData1.Text = "Knife";
      this.ttl_KnifeData1.TranslateText = true;
      // 
      // ttb_StackerNote1
      // 
      this.ttb_StackerNote1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_StackerNote1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_StackerNote1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_StackerNote1.ImagePassword = null;
      this.ttb_StackerNote1.Location = new System.Drawing.Point(93, 763);
      this.ttb_StackerNote1.MaxChar = 58;
      this.ttb_StackerNote1.Multiline = true;
      this.ttb_StackerNote1.Name = "ttb_StackerNote1";
      this.ttb_StackerNote1.PasswordChar = '\0';
      this.ttb_StackerNote1.ReadOnly = true;
      this.ttb_StackerNote1.Size = new System.Drawing.Size(281, 65);
      this.ttb_StackerNote1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_StackerNote1.TabIndex = 135;
      this.ttb_StackerNote1.TabStop = false;
      this.ttb_StackerNote1.Text = "Note";
      this.ttb_StackerNote1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_StackerNote1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_StackerNote1.TranslateText = true;
      this.ttb_StackerNote1.Value = " ";
      this.ttb_StackerNote1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // chk_StackerPallet1
      // 
      this.chk_StackerPallet1.BackColor = System.Drawing.Color.LightGray;
      this.chk_StackerPallet1.Checked = false;
      this.chk_StackerPallet1.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_StackerPallet1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_StackerPallet1.ImagePassword = null;
      this.chk_StackerPallet1.Location = new System.Drawing.Point(287, 714);
      this.chk_StackerPallet1.Name = "chk_StackerPallet1";
      this.chk_StackerPallet1.ReadOnly = true;
      this.chk_StackerPallet1.Size = new System.Drawing.Size(89, 50);
      this.chk_StackerPallet1.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_StackerPallet1.TabIndex = 133;
      this.chk_StackerPallet1.TabStop = false;
      this.chk_StackerPallet1.Text = "Pallet";
      this.chk_StackerPallet1.TitleBackColor = System.Drawing.Color.LightGray;
      this.chk_StackerPallet1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_StackerPallet1.TranslateText = true;
      // 
      // chk_StackerContStack1
      // 
      this.chk_StackerContStack1.BackColor = System.Drawing.Color.LightGray;
      this.chk_StackerContStack1.Checked = false;
      this.chk_StackerContStack1.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_StackerContStack1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_StackerContStack1.ImagePassword = null;
      this.chk_StackerContStack1.Location = new System.Drawing.Point(184, 714);
      this.chk_StackerContStack1.Name = "chk_StackerContStack1";
      this.chk_StackerContStack1.ReadOnly = true;
      this.chk_StackerContStack1.Size = new System.Drawing.Size(89, 50);
      this.chk_StackerContStack1.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_StackerContStack1.TabIndex = 132;
      this.chk_StackerContStack1.TabStop = false;
      this.chk_StackerContStack1.Text = "Cont. Stack";
      this.chk_StackerContStack1.TitleBackColor = System.Drawing.Color.LightGray;
      this.chk_StackerContStack1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_StackerContStack1.TranslateText = true;
      // 
      // ttl_stackerData1
      // 
      this.ttl_stackerData1.BackColor = System.Drawing.Color.Black;
      this.ttl_stackerData1.CausesValidation = false;
      this.ttl_stackerData1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_stackerData1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_stackerData1.ForeColor = System.Drawing.Color.White;
      this.ttl_stackerData1.Location = new System.Drawing.Point(0, 692);
      this.ttl_stackerData1.Name = "ttl_stackerData1";
      this.ttl_stackerData1.Size = new System.Drawing.Size(376, 137);
      this.ttl_stackerData1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_stackerData1.TabIndex = 124;
      this.ttl_stackerData1.TabStop = false;
      this.ttl_stackerData1.Text = "Stacker";
      this.ttl_stackerData1.TranslateText = true;
      // 
      // ttb_DataToPrint1_4
      // 
      this.ttb_DataToPrint1_4.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint1_4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
      this.ttb_DataToPrint1_4.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint1_4.ImagePassword = null;
      this.ttb_DataToPrint1_4.Location = new System.Drawing.Point(173, 505);
      this.ttb_DataToPrint1_4.MaxChar = 58;
      this.ttb_DataToPrint1_4.Multiline = true;
      this.ttb_DataToPrint1_4.Name = "ttb_DataToPrint1_4";
      this.ttb_DataToPrint1_4.PasswordChar = '\0';
      this.ttb_DataToPrint1_4.ReadOnly = true;
      this.ttb_DataToPrint1_4.Size = new System.Drawing.Size(200, 50);
      this.ttb_DataToPrint1_4.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint1_4.TabIndex = 126;
      this.ttb_DataToPrint1_4.TabStop = false;
      this.ttb_DataToPrint1_4.Text = "4";
      this.ttb_DataToPrint1_4.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint1_4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint1_4.TranslateText = false;
      this.ttb_DataToPrint1_4.Value = " ";
      this.ttb_DataToPrint1_4.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_DataToPrint1_2
      // 
      this.ttb_DataToPrint1_2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint1_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_DataToPrint1_2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint1_2.ImagePassword = null;
      this.ttb_DataToPrint1_2.Location = new System.Drawing.Point(8, 449);
      this.ttb_DataToPrint1_2.MaxChar = 58;
      this.ttb_DataToPrint1_2.Multiline = true;
      this.ttb_DataToPrint1_2.Name = "ttb_DataToPrint1_2";
      this.ttb_DataToPrint1_2.PasswordChar = '\0';
      this.ttb_DataToPrint1_2.ReadOnly = true;
      this.ttb_DataToPrint1_2.Size = new System.Drawing.Size(365, 50);
      this.ttb_DataToPrint1_2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint1_2.TabIndex = 124;
      this.ttb_DataToPrint1_2.TabStop = false;
      this.ttb_DataToPrint1_2.Text = "2";
      this.ttb_DataToPrint1_2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint1_2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint1_2.TranslateText = false;
      this.ttb_DataToPrint1_2.Value = " ";
      this.ttb_DataToPrint1_2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_DataToPrint1_1
      // 
      this.ttb_DataToPrint1_1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint1_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_DataToPrint1_1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint1_1.ImagePassword = null;
      this.ttb_DataToPrint1_1.Location = new System.Drawing.Point(8, 394);
      this.ttb_DataToPrint1_1.MaxChar = 58;
      this.ttb_DataToPrint1_1.Multiline = true;
      this.ttb_DataToPrint1_1.Name = "ttb_DataToPrint1_1";
      this.ttb_DataToPrint1_1.PasswordChar = '\0';
      this.ttb_DataToPrint1_1.ReadOnly = true;
      this.ttb_DataToPrint1_1.Size = new System.Drawing.Size(365, 50);
      this.ttb_DataToPrint1_1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint1_1.TabIndex = 123;
      this.ttb_DataToPrint1_1.TabStop = false;
      this.ttb_DataToPrint1_1.Text = "1";
      this.ttb_DataToPrint1_1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint1_1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint1_1.TranslateText = false;
      this.ttb_DataToPrint1_1.Value = " ";
      this.ttb_DataToPrint1_1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_DataToPrint1
      // 
      this.ttl_DataToPrint1.BackColor = System.Drawing.Color.Black;
      this.ttl_DataToPrint1.CausesValidation = false;
      this.ttl_DataToPrint1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_DataToPrint1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DataToPrint1.ForeColor = System.Drawing.Color.White;
      this.ttl_DataToPrint1.Location = new System.Drawing.Point(0, 372);
      this.ttl_DataToPrint1.Name = "ttl_DataToPrint1";
      this.ttl_DataToPrint1.Size = new System.Drawing.Size(376, 185);
      this.ttl_DataToPrint1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_DataToPrint1.TabIndex = 119;
      this.ttl_DataToPrint1.TabStop = false;
      this.ttl_DataToPrint1.Text = "Data To Print";
      this.ttl_DataToPrint1.TranslateText = true;
      // 
      // chk_CutToMark1
      // 
      this.chk_CutToMark1.BackColor = System.Drawing.Color.LightGray;
      this.chk_CutToMark1.Checked = false;
      this.chk_CutToMark1.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_CutToMark1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_CutToMark1.ImagePassword = null;
      this.chk_CutToMark1.Location = new System.Drawing.Point(8, 913);
      this.chk_CutToMark1.Name = "chk_CutToMark1";
      this.chk_CutToMark1.ReadOnly = true;
      this.chk_CutToMark1.Size = new System.Drawing.Size(89, 50);
      this.chk_CutToMark1.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_CutToMark1.TabIndex = 137;
      this.chk_CutToMark1.TabStop = false;
      this.chk_CutToMark1.Text = "Cut To Mark";
      this.chk_CutToMark1.TitleBackColor = System.Drawing.Color.LightGray;
      this.chk_CutToMark1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_CutToMark1.TranslateText = false;
      // 
      // mkt_Delivery1
      // 
      this.mkt_Delivery1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.mkt_Delivery1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.mkt_Delivery1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.mkt_Delivery1.ImagePassword = null;
      this.mkt_Delivery1.Location = new System.Drawing.Point(224, 638);
      this.mkt_Delivery1.Name = "mkt_Delivery1";
      this.mkt_Delivery1.PasswordChar = '\0';
      this.mkt_Delivery1.ReadOnly = true;
      this.mkt_Delivery1.Size = new System.Drawing.Size(149, 50);
      this.mkt_Delivery1.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.mkt_Delivery1.TabIndex = 130;
      this.mkt_Delivery1.TabStop = false;
      this.mkt_Delivery1.Text = "Delivery";
      this.mkt_Delivery1.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.Date;
      this.mkt_Delivery1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.mkt_Delivery1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.mkt_Delivery1.TranslateText = true;
      this.mkt_Delivery1.Value = " ";
      this.mkt_Delivery1.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.mkt_Delivery1.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.mkt_Delivery1.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.mkt_Delivery1.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.mkt_Delivery1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.mkt_Delivery1.Validating += new System.ComponentModel.CancelEventHandler(this.Delivery_Validating);
      this.mkt_Delivery1.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // nud_stacksXpallet1
      // 
      this.nud_stacksXpallet1.DecimalPlaces = 0;
      this.nud_stacksXpallet1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_stacksXpallet1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_stacksXpallet1.ImagePassword = null;
      this.nud_stacksXpallet1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_stacksXpallet1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_stacksXpallet1.Location = new System.Drawing.Point(116, 320);
      this.nud_stacksXpallet1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_stacksXpallet1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_stacksXpallet1.Name = "nud_stacksXpallet1";
      this.nud_stacksXpallet1.ReadOnly = true;
      this.nud_stacksXpallet1.Size = new System.Drawing.Size(110, 46);
      this.nud_stacksXpallet1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_stacksXpallet1.TabIndex = 32;
      this.nud_stacksXpallet1.TabStop = false;
      this.nud_stacksXpallet1.Text = "STACKS";
      this.nud_stacksXpallet1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_stacksXpallet1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_stacksXpallet1.TranslateText = true;
      this.nud_stacksXpallet1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_stacksXpallet1.Value = "0";
      this.nud_stacksXpallet1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_stacksXpallet1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_stacksXpallet1.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_stacksXpallet1.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // nud_sheetsXstack1
      // 
      this.nud_sheetsXstack1.DecimalPlaces = 0;
      this.nud_sheetsXstack1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_sheetsXstack1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_sheetsXstack1.ImagePassword = null;
      this.nud_sheetsXstack1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_sheetsXstack1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_sheetsXstack1.Location = new System.Drawing.Point(124, 264);
      this.nud_sheetsXstack1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_sheetsXstack1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_sheetsXstack1.Name = "nud_sheetsXstack1";
      this.nud_sheetsXstack1.ReadOnly = true;
      this.nud_sheetsXstack1.Size = new System.Drawing.Size(102, 46);
      this.nud_sheetsXstack1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_sheetsXstack1.TabIndex = 31;
      this.nud_sheetsXstack1.TabStop = false;
      this.nud_sheetsXstack1.Text = "Per stack";
      this.nud_sheetsXstack1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_sheetsXstack1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_sheetsXstack1.TranslateText = true;
      this.nud_sheetsXstack1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_sheetsXstack1.Value = "0";
      this.nud_sheetsXstack1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_sheetsXstack1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_sheetsXstack1.Validating += new System.ComponentModel.CancelEventHandler(this.sheetsXstack1_Validating);
      this.nud_sheetsXstack1.Enter += new System.EventHandler(this.sheetsXstack1_Enter);
      // 
      // nud_SheetsDesired1
      // 
      this.nud_SheetsDesired1.DecimalPlaces = 0;
      this.nud_SheetsDesired1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_SheetsDesired1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SheetsDesired1.ImagePassword = null;
      this.nud_SheetsDesired1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SheetsDesired1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_SheetsDesired1.Location = new System.Drawing.Point(124, 216);
      this.nud_SheetsDesired1.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
      this.nud_SheetsDesired1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SheetsDesired1.Name = "nud_SheetsDesired1";
      this.nud_SheetsDesired1.ReadOnly = true;
      this.nud_SheetsDesired1.Size = new System.Drawing.Size(102, 46);
      this.nud_SheetsDesired1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_SheetsDesired1.TabIndex = 30;
      this.nud_SheetsDesired1.TabStop = false;
      this.nud_SheetsDesired1.Text = "Desired";
      this.nud_SheetsDesired1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_SheetsDesired1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SheetsDesired1.TranslateText = true;
      this.nud_SheetsDesired1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_SheetsDesired1.Value = "0";
      this.nud_SheetsDesired1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_SheetsDesired1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_SheetsDesired1.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_SheetsDesired1.Enter += new System.EventHandler(this.CleanSuggestionFields);
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
      this.nud_Boxes1.Location = new System.Drawing.Point(8, 320);
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
      this.nud_Boxes1.Size = new System.Drawing.Size(102, 46);
      this.nud_Boxes1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Boxes1.TabIndex = 29;
      this.nud_Boxes1.TabStop = false;
      this.nud_Boxes1.Text = "Boxes";
      this.nud_Boxes1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Boxes1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Boxes1.TranslateText = true;
      this.nud_Boxes1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Boxes1.Value = "0";
      this.nud_Boxes1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Boxes1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Boxes1.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_Boxes1.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // nud_length1
      // 
      this.nud_length1.DecimalPlaces = 0;
      this.nud_length1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_length1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_length1.ImagePassword = null;
      this.nud_length1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_length1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_length1.Location = new System.Drawing.Point(8, 264);
      this.nud_length1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_length1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_length1.Name = "nud_length1";
      this.nud_length1.ReadOnly = true;
      this.nud_length1.Size = new System.Drawing.Size(102, 46);
      this.nud_length1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_length1.TabIndex = 28;
      this.nud_length1.TabStop = false;
      this.nud_length1.Text = "Length";
      this.nud_length1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_length1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_length1.TranslateText = true;
      this.nud_length1.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_length1.Value = "0";
      this.nud_length1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_length1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_length1.Validating += new System.ComponentModel.CancelEventHandler(this.Flength_Validating);
      this.nud_length1.Enter += new System.EventHandler(this.Flength_Enter);
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
      this.nud_Width1.Location = new System.Drawing.Point(8, 216);
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
      this.nud_Width1.Size = new System.Drawing.Size(102, 46);
      this.nud_Width1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Width1.TabIndex = 27;
      this.nud_Width1.TabStop = false;
      this.nud_Width1.Text = "Width";
      this.nud_Width1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Width1.TranslateText = true;
      this.nud_Width1.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Width1.Value = "0";
      this.nud_Width1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Width1.Validating += new System.ComponentModel.CancelEventHandler(this.Fwidth1_Validating);
      this.nud_Width1.Leave += new System.EventHandler(this.cmb_Quality_OR_cmb_Level_OR_cmb_Sending_SelectedItemChanged);
      this.nud_Width1.Enter += new System.EventHandler(this.Fwidth1_Enter);
      // 
      // cmb_sending1
      // 
      this.cmb_sending1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_sending1.CanCancel = false;
      this.cmb_sending1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_sending1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_sending1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_sending1.ImagePassword = null;
      this.cmb_sending1.Location = new System.Drawing.Point(224, 587);
      this.cmb_sending1.Name = "cmb_sending1";
      this.cmb_sending1.ReadOnly = true;
      this.cmb_sending1.SelectedIndex = -1;
      this.cmb_sending1.SelectedItem = "";
      this.cmb_sending1.SelectedItemData = "";
      this.cmb_sending1.Size = new System.Drawing.Size(149, 50);
      this.cmb_sending1.Sorted = false;
      this.cmb_sending1.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_sending1.TabIndex = 128;
      this.cmb_sending1.TabStop = false;
      this.cmb_sending1.Text = "Sending";
      this.cmb_sending1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_sending1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_sending1.TranslateText = true;
      this.cmb_sending1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_sending1.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttl_Sheets1
      // 
      this.ttl_Sheets1.BackColor = System.Drawing.Color.Black;
      this.ttl_Sheets1.CausesValidation = false;
      this.ttl_Sheets1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Sheets1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Sheets1.ForeColor = System.Drawing.Color.White;
      this.ttl_Sheets1.Location = new System.Drawing.Point(116, 187);
      this.ttl_Sheets1.Name = "ttl_Sheets1";
      this.ttl_Sheets1.Size = new System.Drawing.Size(110, 124);
      this.ttl_Sheets1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Sheets1.TabIndex = 99;
      this.ttl_Sheets1.TabStop = false;
      this.ttl_Sheets1.Text = "SHEETS";
      this.ttl_Sheets1.TranslateText = true;
      // 
      // ttl_Format1
      // 
      this.ttl_Format1.BackColor = System.Drawing.Color.Black;
      this.ttl_Format1.CausesValidation = false;
      this.ttl_Format1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Format1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Format1.ForeColor = System.Drawing.Color.White;
      this.ttl_Format1.Location = new System.Drawing.Point(0, 187);
      this.ttl_Format1.Name = "ttl_Format1";
      this.ttl_Format1.Size = new System.Drawing.Size(110, 181);
      this.ttl_Format1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Format1.TabIndex = 98;
      this.ttl_Format1.TabStop = false;
      this.ttl_Format1.Text = "FORMAT";
      this.ttl_Format1.TranslateText = true;
      // 
      // cmb_Type1
      // 
      this.cmb_Type1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Type1.CanCancel = false;
      this.cmb_Type1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Type1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Type1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type1.ImagePassword = null;
      this.cmb_Type1.Location = new System.Drawing.Point(240, 130);
      this.cmb_Type1.Name = "cmb_Type1";
      this.cmb_Type1.ReadOnly = true;
      this.cmb_Type1.SelectedIndex = -1;
      this.cmb_Type1.SelectedItem = "";
      this.cmb_Type1.SelectedItemData = "";
      this.cmb_Type1.Size = new System.Drawing.Size(136, 50);
      this.cmb_Type1.Sorted = false;
      this.cmb_Type1.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Type1.TabIndex = 34;
      this.cmb_Type1.TabStop = false;
      this.cmb_Type1.Text = "Type";
      this.cmb_Type1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Type1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type1.TranslateText = true;
      this.cmb_Type1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Type1.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttl_Score1
      // 
      this.ttl_Score1.BackColor = System.Drawing.Color.Black;
      this.ttl_Score1.CausesValidation = false;
      this.ttl_Score1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Score1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Score1.ForeColor = System.Drawing.Color.White;
      this.ttl_Score1.Location = new System.Drawing.Point(232, 55);
      this.ttl_Score1.Name = "ttl_Score1";
      this.ttl_Score1.Size = new System.Drawing.Size(144, 312);
      this.ttl_Score1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Score1.TabIndex = 96;
      this.ttl_Score1.TabStop = false;
      this.ttl_Score1.Text = "SCORERS";
      this.ttl_Score1.TranslateText = true;
      // 
      // cmb_Positioning1
      // 
      this.cmb_Positioning1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Positioning1.CanCancel = false;
      this.cmb_Positioning1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Positioning1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Positioning1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning1.ImagePassword = null;
      this.cmb_Positioning1.Location = new System.Drawing.Point(240, 75);
      this.cmb_Positioning1.Name = "cmb_Positioning1";
      this.cmb_Positioning1.ReadOnly = true;
      this.cmb_Positioning1.SelectedIndex = -1;
      this.cmb_Positioning1.SelectedItem = "";
      this.cmb_Positioning1.SelectedItemData = "";
      this.cmb_Positioning1.Size = new System.Drawing.Size(136, 50);
      this.cmb_Positioning1.Sorted = false;
      this.cmb_Positioning1.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Positioning1.TabIndex = 33;
      this.cmb_Positioning1.TabStop = false;
      this.cmb_Positioning1.Text = "Positioning";
      this.cmb_Positioning1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning1.TranslateText = true;
      this.cmb_Positioning1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning1.SelectedItemChanged += new System.EventHandler(this.cmb_Positioning1_SelectedItemChanged);
      this.cmb_Positioning1.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttl_ord1
      // 
      this.ttl_ord1.BackColor = System.Drawing.Color.Black;
      this.ttl_ord1.CausesValidation = false;
      this.ttl_ord1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ord1.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ord1.ForeColor = System.Drawing.Color.White;
      this.ttl_ord1.Location = new System.Drawing.Point(0, 0);
      this.ttl_ord1.Name = "ttl_ord1";
      this.ttl_ord1.Size = new System.Drawing.Size(226, 182);
      this.ttl_ord1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_ord1.TabIndex = 92;
      this.ttl_ord1.TabStop = false;
      this.ttl_ord1.Text = "ORDER 2";
      this.ttl_ord1.TranslateText = true;
      // 
      // ttb_CustName1
      // 
      this.ttb_CustName1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CustName1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CustName1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustName1.ImagePassword = null;
      this.ttb_CustName1.Location = new System.Drawing.Point(8, 76);
      this.ttb_CustName1.MaxChar = 30;
      this.ttb_CustName1.Multiline = false;
      this.ttb_CustName1.Name = "ttb_CustName1";
      this.ttb_CustName1.PasswordChar = '\0';
      this.ttb_CustName1.ReadOnly = true;
      this.ttb_CustName1.Size = new System.Drawing.Size(216, 50);
      this.ttb_CustName1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CustName1.TabIndex = 24;
      this.ttb_CustName1.TabStop = false;
      this.ttb_CustName1.Text = "Customer Name";
      this.ttb_CustName1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_CustName1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustName1.TranslateText = true;
      this.ttb_CustName1.Value = " ";
      this.ttb_CustName1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_CustName1.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttb_OrderNumber1
      // 
      this.ttb_OrderNumber1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_OrderNumber1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_OrderNumber1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_OrderNumber1.ImagePassword = null;
      this.ttb_OrderNumber1.Location = new System.Drawing.Point(8, 22);
      this.ttb_OrderNumber1.MaxChar = 20;
      this.ttb_OrderNumber1.Multiline = false;
      this.ttb_OrderNumber1.Name = "ttb_OrderNumber1";
      this.ttb_OrderNumber1.PasswordChar = '\0';
      this.ttb_OrderNumber1.ReadOnly = true;
      this.ttb_OrderNumber1.Size = new System.Drawing.Size(216, 50);
      this.ttb_OrderNumber1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_OrderNumber1.TabIndex = 23;
      this.ttb_OrderNumber1.TabStop = false;
      this.ttb_OrderNumber1.Text = "Number";
      this.ttb_OrderNumber1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_OrderNumber1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_OrderNumber1.TranslateText = true;
      this.ttb_OrderNumber1.Value = " ";
      this.ttb_OrderNumber1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_OrderNumber1.Validating += new System.ComponentModel.CancelEventHandler(this.ttb_NotNullString_Validating);
      this.ttb_OrderNumber1.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // cmb_Level1
      // 
      this.cmb_Level1.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Level1.CanCancel = false;
      this.cmb_Level1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Level1.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Level1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Level1.ImagePassword = null;
      this.cmb_Level1.Location = new System.Drawing.Point(232, 0);
      this.cmb_Level1.Name = "cmb_Level1";
      this.cmb_Level1.ReadOnly = true;
      this.cmb_Level1.SelectedIndex = -1;
      this.cmb_Level1.SelectedItem = "";
      this.cmb_Level1.SelectedItemData = "";
      this.cmb_Level1.Size = new System.Drawing.Size(144, 50);
      this.cmb_Level1.Sorted = false;
      this.cmb_Level1.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_Level1.TabIndex = 22;
      this.cmb_Level1.TabStop = false;
      this.cmb_Level1.Text = "LEVEL";
      this.cmb_Level1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Level1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Level1.TranslateText = true;
      this.cmb_Level1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Level1.SelectedItemChanged += new System.EventHandler(this.cmb_Quality_OR_cmb_Level_OR_cmb_Sending_SelectedItemChanged);
      this.cmb_Level1.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttb_Measures1
      // 
      this.ttb_Measures1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_Measures1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Measures1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Measures1.ImagePassword = null;
      this.ttb_Measures1.Location = new System.Drawing.Point(240, 185);
      //RFAZZI_FI03/12/2010_2. Aumentato il numero di cordoni massimo per scatola da 24 a 48. 
      //this.ttb_Measures1.MaxChar = 312;
      this.ttb_Measures1.MaxChar = 624;
      this.ttb_Measures1.Multiline = true;
      this.ttb_Measures1.Name = "ttb_Measures1";
      this.ttb_Measures1.PasswordChar = '\0';
      this.ttb_Measures1.ReadOnly = true;
      this.ttb_Measures1.Size = new System.Drawing.Size(136, 181);
      this.ttb_Measures1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_Measures1.TabIndex = 35;
      this.ttb_Measures1.TabStop = false;
      this.ttb_Measures1.Text = "Measures";
      this.ttb_Measures1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_Measures1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Measures1.TranslateText = true;
      this.ttb_Measures1.Value = " ";
      this.ttb_Measures1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_Measures1.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttb_DataToPrint1_3
      // 
      this.ttb_DataToPrint1_3.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint1_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
      this.ttb_DataToPrint1_3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint1_3.ImagePassword = null;
      this.ttb_DataToPrint1_3.Location = new System.Drawing.Point(8, 505);
      this.ttb_DataToPrint1_3.MaxChar = 58;
      this.ttb_DataToPrint1_3.Multiline = true;
      this.ttb_DataToPrint1_3.Name = "ttb_DataToPrint1_3";
      this.ttb_DataToPrint1_3.PasswordChar = '\0';
      this.ttb_DataToPrint1_3.ReadOnly = true;
      this.ttb_DataToPrint1_3.Size = new System.Drawing.Size(164, 50);
      this.ttb_DataToPrint1_3.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint1_3.TabIndex = 125;
      this.ttb_DataToPrint1_3.TabStop = false;
      this.ttb_DataToPrint1_3.Text = "3";
      this.ttb_DataToPrint1_3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint1_3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint1_3.TranslateText = false;
      this.ttb_DataToPrint1_3.Value = " ";
      this.ttb_DataToPrint1_3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_SetUpSequence
      // 
      this.ttb_SetUpSequence.CausesValidation = false;
      this.ttb_SetUpSequence.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_SetUpSequence.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_SetUpSequence.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_SetUpSequence.ImagePassword = null;
      this.ttb_SetUpSequence.Location = new System.Drawing.Point(485, 193);
      this.ttb_SetUpSequence.MaxChar = 2;
      this.ttb_SetUpSequence.Multiline = false;
      this.ttb_SetUpSequence.Name = "ttb_SetUpSequence";
      this.ttb_SetUpSequence.PasswordChar = '\0';
      this.ttb_SetUpSequence.ReadOnly = true;
      this.ttb_SetUpSequence.Size = new System.Drawing.Size(66, 60);
      this.ttb_SetUpSequence.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_SetUpSequence.TabIndex = 1;
      this.ttb_SetUpSequence.TabStop = false;
      this.ttb_SetUpSequence.Text = "SEQ";
      this.ttb_SetUpSequence.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_SetUpSequence.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_SetUpSequence.TranslateText = true;
      this.ttb_SetUpSequence.Value = " ";
      this.ttb_SetUpSequence.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_SetUpSequence.Validating += new System.ComponentModel.CancelEventHandler(this.ttb_NotNullString_Validating);
      this.ttb_SetUpSequence.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttb_ProgramNumber
      // 
      this.ttb_ProgramNumber.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_ProgramNumber.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_ProgramNumber.ForeColor = System.Drawing.Color.Black;
      this.ttb_ProgramNumber.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ProgramNumber.ImagePassword = null;
      this.ttb_ProgramNumber.ImeMode = System.Windows.Forms.ImeMode.On;
      this.ttb_ProgramNumber.Location = new System.Drawing.Point(207, 193);
      this.ttb_ProgramNumber.MaxChar = 18;
      this.ttb_ProgramNumber.Multiline = false;
      this.ttb_ProgramNumber.Name = "ttb_ProgramNumber";
      this.ttb_ProgramNumber.PasswordChar = '\0';
      this.ttb_ProgramNumber.ReadOnly = true;
      this.ttb_ProgramNumber.Size = new System.Drawing.Size(272, 60);
      this.ttb_ProgramNumber.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_ProgramNumber.TabIndex = 0;
      this.ttb_ProgramNumber.TabStop = false;
      this.ttb_ProgramNumber.Text = "PROGRAM NUMBER";
      this.ttb_ProgramNumber.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_ProgramNumber.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ProgramNumber.TranslateText = true;
      this.ttb_ProgramNumber.Value = " ";
      this.ttb_ProgramNumber.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_ProgramNumber.Validating += new System.ComponentModel.CancelEventHandler(this.ttb_NotNullString_Validating);
      this.ttb_ProgramNumber.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // cmb_Flute
      // 
      this.cmb_Flute.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_Flute.CanCancel = false;
      this.cmb_Flute.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Flute.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Flute.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Flute.ImagePassword = null;
      this.cmb_Flute.Location = new System.Drawing.Point(557, 193);
      this.cmb_Flute.Name = "cmb_Flute";
      this.cmb_Flute.ReadOnly = true;
      this.cmb_Flute.SelectedIndex = -1;
      this.cmb_Flute.SelectedItem = "";
      this.cmb_Flute.SelectedItemData = "";
      this.cmb_Flute.Size = new System.Drawing.Size(93, 60);
      this.cmb_Flute.Sorted = false;
      this.cmb_Flute.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_Flute.TabIndex = 2;
      this.cmb_Flute.TabStop = false;
      this.cmb_Flute.Text = "FLUTE";
      this.cmb_Flute.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Flute.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Flute.TranslateText = true;
      this.cmb_Flute.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Flute.SelectedItemChanged += new System.EventHandler(this.cmb_Flute_SelectedItemChanged);
      this.cmb_Flute.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // pnl_OrdersO
      // 
      this.pnl_OrdersO.BackColor = System.Drawing.Color.LightGray;
      this.pnl_OrdersO.Controls.Add(this.chk_Bundle0);
      this.pnl_OrdersO.Controls.Add(this.cmb_Balance0);
      this.pnl_OrdersO.Controls.Add(this.cmb_DischargeSide0);
      this.pnl_OrdersO.Controls.Add(this.btn_ScorersOptions0);
      this.pnl_OrdersO.Controls.Add(this.ttb_CustAddress0);
      this.pnl_OrdersO.Controls.Add(this.ttb_MthNextMachine0);
      this.pnl_OrdersO.Controls.Add(this.ttb_MthCode0);
      this.pnl_OrdersO.Controls.Add(this.ttl_mthData0);
      this.pnl_OrdersO.Controls.Add(this.ttb_DataToPrint0_4);
      this.pnl_OrdersO.Controls.Add(this.ttb_StackerNote0);
      this.pnl_OrdersO.Controls.Add(this.chk_StackerPallet0);
      this.pnl_OrdersO.Controls.Add(this.chk_StackerContStack0);
      this.pnl_OrdersO.Controls.Add(this.ttl_stackerData0);
      this.pnl_OrdersO.Controls.Add(this.cmb_cutToPattern0);
      this.pnl_OrdersO.Controls.Add(this.ttl_KnifeData0);
      this.pnl_OrdersO.Controls.Add(this.chk_CutToMark0);
      this.pnl_OrdersO.Controls.Add(this.ttb_DataToPrint0_3);
      this.pnl_OrdersO.Controls.Add(this.ttb_DataToPrint0_1);
      this.pnl_OrdersO.Controls.Add(this.ttl_DataToPrint0);
      this.pnl_OrdersO.Controls.Add(this.mkt_Delivery0);
      this.pnl_OrdersO.Controls.Add(this.nud_stacksXpallet0);
      this.pnl_OrdersO.Controls.Add(this.nud_sheetsXstack0);
      this.pnl_OrdersO.Controls.Add(this.nud_SheetsDesired0);
      this.pnl_OrdersO.Controls.Add(this.nud_Boxes0);
      this.pnl_OrdersO.Controls.Add(this.nud_length0);
      this.pnl_OrdersO.Controls.Add(this.cmb_sending0);
      this.pnl_OrdersO.Controls.Add(this.ttl_Sheets0);
      this.pnl_OrdersO.Controls.Add(this.ttl_Format0);
      this.pnl_OrdersO.Controls.Add(this.cmb_Type0);
      this.pnl_OrdersO.Controls.Add(this.ttl_Score0);
      this.pnl_OrdersO.Controls.Add(this.cmb_Positioning0);
      this.pnl_OrdersO.Controls.Add(this.ttl_ord0);
      this.pnl_OrdersO.Controls.Add(this.ttb_CustName0);
      this.pnl_OrdersO.Controls.Add(this.ttb_OrderNumber0);
      this.pnl_OrdersO.Controls.Add(this.cmb_Level0);
      this.pnl_OrdersO.Controls.Add(this.ttb_Measures0);
      this.pnl_OrdersO.Controls.Add(this.nud_Width0);
      this.pnl_OrdersO.Controls.Add(this.ttb_DataToPrint0_2);
      this.pnl_OrdersO.Location = new System.Drawing.Point(18, 21);
      this.pnl_OrdersO.Name = "pnl_OrdersO";
      this.pnl_OrdersO.Size = new System.Drawing.Size(376, 963);
      this.pnl_OrdersO.TabIndex = 110;
      // 
      // chk_Bundle0
      // 
      this.chk_Bundle0.BackColor = System.Drawing.Color.LightGray;
      this.chk_Bundle0.Checked = false;
      this.chk_Bundle0.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_Bundle0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_Bundle0.ImagePassword = null;
      this.chk_Bundle0.Location = new System.Drawing.Point(7, 763);
      this.chk_Bundle0.Name = "chk_Bundle0";
      this.chk_Bundle0.ReadOnly = true;
      this.chk_Bundle0.Size = new System.Drawing.Size(80, 65);
      this.chk_Bundle0.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_Bundle0.TabIndex = 118;
      this.chk_Bundle0.TabStop = false;
      this.chk_Bundle0.Text = "Bundle";
      this.chk_Bundle0.TitleBackColor = System.Drawing.Color.LightGray;
      this.chk_Bundle0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_Bundle0.TranslateText = true;
      // 
      // cmb_Balance0
      // 
      this.cmb_Balance0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Balance0.CanCancel = false;
      this.cmb_Balance0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Balance0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Balance0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Balance0.ImagePassword = null;
      this.cmb_Balance0.Location = new System.Drawing.Point(0, 835);
      this.cmb_Balance0.Name = "cmb_Balance0";
      this.cmb_Balance0.ReadOnly = true;
      this.cmb_Balance0.SelectedIndex = -1;
      this.cmb_Balance0.SelectedItem = "";
      this.cmb_Balance0.SelectedItemData = "";
      this.cmb_Balance0.Size = new System.Drawing.Size(376, 50);
      this.cmb_Balance0.Sorted = false;
      this.cmb_Balance0.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_Balance0.TabIndex = 120;
      this.cmb_Balance0.TabStop = false;
      this.cmb_Balance0.Text = "Balance";
      this.cmb_Balance0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Balance0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Balance0.TranslateText = true;
      this.cmb_Balance0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // cmb_DischargeSide0
      // 
      this.cmb_DischargeSide0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_DischargeSide0.CanCancel = false;
      this.cmb_DischargeSide0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_DischargeSide0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_DischargeSide0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DischargeSide0.ImagePassword = null;
      this.cmb_DischargeSide0.Location = new System.Drawing.Point(7, 713);
      this.cmb_DischargeSide0.Name = "cmb_DischargeSide0";
      this.cmb_DischargeSide0.ReadOnly = true;
      this.cmb_DischargeSide0.SelectedIndex = -1;
      this.cmb_DischargeSide0.SelectedItem = "";
      this.cmb_DischargeSide0.SelectedItemData = "";
      this.cmb_DischargeSide0.Size = new System.Drawing.Size(164, 50);
      this.cmb_DischargeSide0.Sorted = false;
      this.cmb_DischargeSide0.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_DischargeSide0.TabIndex = 115;
      this.cmb_DischargeSide0.TabStop = false;
      this.cmb_DischargeSide0.Text = "Side";
      this.cmb_DischargeSide0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_DischargeSide0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_DischargeSide0.TranslateText = true;
      this.cmb_DischargeSide0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // btn_ScorersOptions0
      // 
      this.btn_ScorersOptions0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btn_ScorersOptions0.Font = new System.Drawing.Font("Arial Unicode MS", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_ScorersOptions0.Location = new System.Drawing.Point(352, 186);
      this.btn_ScorersOptions0.Margin = new System.Windows.Forms.Padding(0);
      this.btn_ScorersOptions0.Name = "btn_ScorersOptions0";
      this.btn_ScorersOptions0.Size = new System.Drawing.Size(24, 20);
      this.btn_ScorersOptions0.TabIndex = 120;
      this.btn_ScorersOptions0.Text = "...";
      this.btn_ScorersOptions0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.btn_ScorersOptions0.UseVisualStyleBackColor = true;
      this.btn_ScorersOptions0.Visible = false;
      this.btn_ScorersOptions0.Click += new System.EventHandler(this.btn_ScorersOptions0_Click);
      // 
      // ttb_CustAddress0
      // 
      this.ttb_CustAddress0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CustAddress0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CustAddress0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustAddress0.ImagePassword = null;
      this.ttb_CustAddress0.ImeMode = System.Windows.Forms.ImeMode.On;
      this.ttb_CustAddress0.Location = new System.Drawing.Point(7, 130);
      this.ttb_CustAddress0.MaxChar = 30;
      this.ttb_CustAddress0.Multiline = true;
      this.ttb_CustAddress0.Name = "ttb_CustAddress0";
      this.ttb_CustAddress0.PasswordChar = '\0';
      this.ttb_CustAddress0.ReadOnly = true;
      this.ttb_CustAddress0.Size = new System.Drawing.Size(216, 50);
      this.ttb_CustAddress0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CustAddress0.TabIndex = 11;
      this.ttb_CustAddress0.TabStop = false;
      this.ttb_CustAddress0.Text = "Customer Address";
      this.ttb_CustAddress0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_CustAddress0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustAddress0.TranslateText = true;
      this.ttb_CustAddress0.Value = " ";
      this.ttb_CustAddress0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_MthNextMachine0
      // 
      this.ttb_MthNextMachine0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_MthNextMachine0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_MthNextMachine0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MthNextMachine0.ImagePassword = null;
      this.ttb_MthNextMachine0.Location = new System.Drawing.Point(8, 638);
      this.ttb_MthNextMachine0.MaxChar = 20;
      this.ttb_MthNextMachine0.Multiline = false;
      this.ttb_MthNextMachine0.Name = "ttb_MthNextMachine0";
      this.ttb_MthNextMachine0.PasswordChar = '\0';
      this.ttb_MthNextMachine0.ReadOnly = true;
      this.ttb_MthNextMachine0.Size = new System.Drawing.Size(211, 50);
      this.ttb_MthNextMachine0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_MthNextMachine0.TabIndex = 113;
      this.ttb_MthNextMachine0.TabStop = false;
      this.ttb_MthNextMachine0.Text = "Next Machine";
      this.ttb_MthNextMachine0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_MthNextMachine0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MthNextMachine0.TranslateText = true;
      this.ttb_MthNextMachine0.Value = "ABCDEFG";
      this.ttb_MthNextMachine0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_MthCode0
      // 
      this.ttb_MthCode0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_MthCode0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_MthCode0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MthCode0.ImagePassword = null;
      this.ttb_MthCode0.Location = new System.Drawing.Point(8, 587);
      this.ttb_MthCode0.MaxChar = 20;
      this.ttb_MthCode0.Multiline = false;
      this.ttb_MthCode0.Name = "ttb_MthCode0";
      this.ttb_MthCode0.PasswordChar = '\0';
      this.ttb_MthCode0.ReadOnly = true;
      this.ttb_MthCode0.Size = new System.Drawing.Size(211, 50);
      this.ttb_MthCode0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_MthCode0.TabIndex = 111;
      this.ttb_MthCode0.TabStop = false;
      this.ttb_MthCode0.Text = "Code";
      this.ttb_MthCode0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_MthCode0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_MthCode0.TranslateText = true;
      this.ttb_MthCode0.Value = "ABCDEFG";
      this.ttb_MthCode0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_mthData0
      // 
      this.ttl_mthData0.BackColor = System.Drawing.Color.Black;
      this.ttl_mthData0.CausesValidation = false;
      this.ttl_mthData0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_mthData0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_mthData0.ForeColor = System.Drawing.Color.White;
      this.ttl_mthData0.Location = new System.Drawing.Point(0, 562);
      this.ttl_mthData0.Name = "ttl_mthData0";
      this.ttl_mthData0.Size = new System.Drawing.Size(376, 126);
      this.ttl_mthData0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_mthData0.TabIndex = 116;
      this.ttl_mthData0.TabStop = false;
      this.ttl_mthData0.Text = "MTH";
      this.ttl_mthData0.TranslateText = true;
      // 
      // ttb_DataToPrint0_4
      // 
      this.ttb_DataToPrint0_4.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint0_4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_DataToPrint0_4.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint0_4.ImagePassword = null;
      this.ttb_DataToPrint0_4.Location = new System.Drawing.Point(173, 505);
      this.ttb_DataToPrint0_4.MaxChar = 58;
      this.ttb_DataToPrint0_4.Multiline = true;
      this.ttb_DataToPrint0_4.Name = "ttb_DataToPrint0_4";
      this.ttb_DataToPrint0_4.PasswordChar = '\0';
      this.ttb_DataToPrint0_4.ReadOnly = true;
      this.ttb_DataToPrint0_4.Size = new System.Drawing.Size(200, 50);
      this.ttb_DataToPrint0_4.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint0_4.TabIndex = 110;
      this.ttb_DataToPrint0_4.TabStop = false;
      this.ttb_DataToPrint0_4.Text = "4";
      this.ttb_DataToPrint0_4.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint0_4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint0_4.TranslateText = false;
      this.ttb_DataToPrint0_4.Value = " ";
      this.ttb_DataToPrint0_4.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_StackerNote0
      // 
      this.ttb_StackerNote0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_StackerNote0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_StackerNote0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_StackerNote0.ImagePassword = null;
      this.ttb_StackerNote0.Location = new System.Drawing.Point(93, 763);
      this.ttb_StackerNote0.MaxChar = 58;
      this.ttb_StackerNote0.Multiline = true;
      this.ttb_StackerNote0.Name = "ttb_StackerNote0";
      this.ttb_StackerNote0.PasswordChar = '\0';
      this.ttb_StackerNote0.ReadOnly = true;
      this.ttb_StackerNote0.Size = new System.Drawing.Size(281, 65);
      this.ttb_StackerNote0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_StackerNote0.TabIndex = 119;
      this.ttb_StackerNote0.TabStop = false;
      this.ttb_StackerNote0.Text = "Note";
      this.ttb_StackerNote0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_StackerNote0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_StackerNote0.TranslateText = true;
      this.ttb_StackerNote0.Value = " ";
      this.ttb_StackerNote0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // chk_StackerPallet0
      // 
      this.chk_StackerPallet0.BackColor = System.Drawing.Color.LightGray;
      this.chk_StackerPallet0.Checked = false;
      this.chk_StackerPallet0.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_StackerPallet0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_StackerPallet0.ImagePassword = null;
      this.chk_StackerPallet0.Location = new System.Drawing.Point(287, 713);
      this.chk_StackerPallet0.Name = "chk_StackerPallet0";
      this.chk_StackerPallet0.ReadOnly = true;
      this.chk_StackerPallet0.Size = new System.Drawing.Size(89, 50);
      this.chk_StackerPallet0.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_StackerPallet0.TabIndex = 117;
      this.chk_StackerPallet0.TabStop = false;
      this.chk_StackerPallet0.Text = "Pallet";
      this.chk_StackerPallet0.TitleBackColor = System.Drawing.Color.LightGray;
      this.chk_StackerPallet0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_StackerPallet0.TranslateText = true;
      // 
      // chk_StackerContStack0
      // 
      this.chk_StackerContStack0.BackColor = System.Drawing.Color.LightGray;
      this.chk_StackerContStack0.Checked = false;
      this.chk_StackerContStack0.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_StackerContStack0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_StackerContStack0.ImagePassword = null;
      this.chk_StackerContStack0.Location = new System.Drawing.Point(184, 713);
      this.chk_StackerContStack0.Name = "chk_StackerContStack0";
      this.chk_StackerContStack0.ReadOnly = true;
      this.chk_StackerContStack0.Size = new System.Drawing.Size(89, 50);
      this.chk_StackerContStack0.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_StackerContStack0.TabIndex = 116;
      this.chk_StackerContStack0.TabStop = false;
      this.chk_StackerContStack0.Text = "Cont. Stack";
      this.chk_StackerContStack0.TitleBackColor = System.Drawing.Color.LightGray;
      this.chk_StackerContStack0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_StackerContStack0.TranslateText = true;
      // 
      // ttl_stackerData0
      // 
      this.ttl_stackerData0.BackColor = System.Drawing.Color.Black;
      this.ttl_stackerData0.CausesValidation = false;
      this.ttl_stackerData0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_stackerData0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_stackerData0.ForeColor = System.Drawing.Color.White;
      this.ttl_stackerData0.Location = new System.Drawing.Point(0, 692);
      this.ttl_stackerData0.Name = "ttl_stackerData0";
      this.ttl_stackerData0.Size = new System.Drawing.Size(376, 137);
      this.ttl_stackerData0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_stackerData0.TabIndex = 114;
      this.ttl_stackerData0.TabStop = false;
      this.ttl_stackerData0.Text = "Stacker";
      this.ttl_stackerData0.TranslateText = true;
      // 
      // cmb_cutToPattern0
      // 
      this.cmb_cutToPattern0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_cutToPattern0.CanCancel = true;
      this.cmb_cutToPattern0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_cutToPattern0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_cutToPattern0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_cutToPattern0.ImagePassword = null;
      this.cmb_cutToPattern0.Location = new System.Drawing.Point(104, 913);
      this.cmb_cutToPattern0.Name = "cmb_cutToPattern0";
      this.cmb_cutToPattern0.ReadOnly = true;
      this.cmb_cutToPattern0.SelectedIndex = -1;
      this.cmb_cutToPattern0.SelectedItem = "";
      this.cmb_cutToPattern0.SelectedItemData = "";
      this.cmb_cutToPattern0.Size = new System.Drawing.Size(270, 50);
      this.cmb_cutToPattern0.Sorted = true;
      this.cmb_cutToPattern0.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_cutToPattern0.TabIndex = 122;
      this.cmb_cutToPattern0.TabStop = false;
      this.cmb_cutToPattern0.Text = "Cut To Pattern";
      this.cmb_cutToPattern0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_cutToPattern0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_cutToPattern0.TranslateText = false;
      this.cmb_cutToPattern0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_KnifeData0
      // 
      this.ttl_KnifeData0.BackColor = System.Drawing.Color.Black;
      this.ttl_KnifeData0.CausesValidation = false;
      this.ttl_KnifeData0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_KnifeData0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_KnifeData0.ForeColor = System.Drawing.Color.White;
      this.ttl_KnifeData0.Location = new System.Drawing.Point(0, 890);
      this.ttl_KnifeData0.Name = "ttl_KnifeData0";
      this.ttl_KnifeData0.Size = new System.Drawing.Size(376, 73);
      this.ttl_KnifeData0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_KnifeData0.TabIndex = 112;
      this.ttl_KnifeData0.TabStop = false;
      this.ttl_KnifeData0.Text = "Knife";
      this.ttl_KnifeData0.TranslateText = true;
      // 
      // chk_CutToMark0
      // 
      this.chk_CutToMark0.BackColor = System.Drawing.Color.LightGray;
      this.chk_CutToMark0.Checked = false;
      this.chk_CutToMark0.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_CutToMark0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_CutToMark0.ImagePassword = null;
      this.chk_CutToMark0.Location = new System.Drawing.Point(8, 913);
      this.chk_CutToMark0.Name = "chk_CutToMark0";
      this.chk_CutToMark0.ReadOnly = true;
      this.chk_CutToMark0.Size = new System.Drawing.Size(89, 50);
      this.chk_CutToMark0.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_CutToMark0.TabIndex = 121;
      this.chk_CutToMark0.TabStop = false;
      this.chk_CutToMark0.Text = "Cut To Mark";
      this.chk_CutToMark0.TitleBackColor = System.Drawing.Color.LightGray;
      this.chk_CutToMark0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_CutToMark0.TranslateText = false;
      // 
      // ttb_DataToPrint0_3
      // 
      this.ttb_DataToPrint0_3.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint0_3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_DataToPrint0_3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint0_3.ImagePassword = null;
      this.ttb_DataToPrint0_3.Location = new System.Drawing.Point(8, 505);
      this.ttb_DataToPrint0_3.MaxChar = 58;
      this.ttb_DataToPrint0_3.Multiline = true;
      this.ttb_DataToPrint0_3.Name = "ttb_DataToPrint0_3";
      this.ttb_DataToPrint0_3.PasswordChar = '\0';
      this.ttb_DataToPrint0_3.ReadOnly = true;
      this.ttb_DataToPrint0_3.Size = new System.Drawing.Size(164, 50);
      this.ttb_DataToPrint0_3.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint0_3.TabIndex = 109;
      this.ttb_DataToPrint0_3.TabStop = false;
      this.ttb_DataToPrint0_3.Text = "3";
      this.ttb_DataToPrint0_3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint0_3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint0_3.TranslateText = false;
      this.ttb_DataToPrint0_3.Value = " ";
      this.ttb_DataToPrint0_3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_DataToPrint0_1
      // 
      this.ttb_DataToPrint0_1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint0_1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_DataToPrint0_1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint0_1.ImagePassword = null;
      this.ttb_DataToPrint0_1.Location = new System.Drawing.Point(8, 394);
      this.ttb_DataToPrint0_1.MaxChar = 58;
      this.ttb_DataToPrint0_1.Multiline = true;
      this.ttb_DataToPrint0_1.Name = "ttb_DataToPrint0_1";
      this.ttb_DataToPrint0_1.PasswordChar = '\0';
      this.ttb_DataToPrint0_1.ReadOnly = true;
      this.ttb_DataToPrint0_1.Size = new System.Drawing.Size(365, 50);
      this.ttb_DataToPrint0_1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint0_1.TabIndex = 107;
      this.ttb_DataToPrint0_1.TabStop = false;
      this.ttb_DataToPrint0_1.Text = "1";
      this.ttb_DataToPrint0_1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint0_1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint0_1.TranslateText = false;
      this.ttb_DataToPrint0_1.Value = " ";
      this.ttb_DataToPrint0_1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttl_DataToPrint0
      // 
      this.ttl_DataToPrint0.BackColor = System.Drawing.Color.Black;
      this.ttl_DataToPrint0.CausesValidation = false;
      this.ttl_DataToPrint0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_DataToPrint0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_DataToPrint0.ForeColor = System.Drawing.Color.White;
      this.ttl_DataToPrint0.Location = new System.Drawing.Point(0, 372);
      this.ttl_DataToPrint0.Name = "ttl_DataToPrint0";
      this.ttl_DataToPrint0.Size = new System.Drawing.Size(376, 185);
      this.ttl_DataToPrint0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_DataToPrint0.TabIndex = 106;
      this.ttl_DataToPrint0.TabStop = false;
      this.ttl_DataToPrint0.Text = "Data To Print";
      this.ttl_DataToPrint0.TranslateText = true;
      // 
      // mkt_Delivery0
      // 
      this.mkt_Delivery0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.mkt_Delivery0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.mkt_Delivery0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.mkt_Delivery0.ImagePassword = null;
      this.mkt_Delivery0.Location = new System.Drawing.Point(224, 638);
      this.mkt_Delivery0.Name = "mkt_Delivery0";
      this.mkt_Delivery0.PasswordChar = '\0';
      this.mkt_Delivery0.ReadOnly = true;
      this.mkt_Delivery0.Size = new System.Drawing.Size(149, 50);
      this.mkt_Delivery0.Style = SyncroC.Controls.TitledMaskTextBoxStyles.Light;
      this.mkt_Delivery0.TabIndex = 114;
      this.mkt_Delivery0.TabStop = false;
      this.mkt_Delivery0.Text = "Delivery";
      this.mkt_Delivery0.TextBoxMask = SyncroC.Controls.TitledMaskTextBoxMask.Date;
      this.mkt_Delivery0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.mkt_Delivery0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.mkt_Delivery0.TranslateText = true;
      this.mkt_Delivery0.Value = " ";
      this.mkt_Delivery0.ValueAsLOCALDateTime = new System.DateTime(((long)(0)));
      this.mkt_Delivery0.ValueAsLOCALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.mkt_Delivery0.ValueAsUNIVERSALDateTime = new System.DateTime(((long)(0)));
      this.mkt_Delivery0.ValueAsUNIVERSALSyncroStyleDateTimeString = "1 01 01 0001 00 00 00 0";
      this.mkt_Delivery0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.mkt_Delivery0.Validating += new System.ComponentModel.CancelEventHandler(this.Delivery_Validating);
      this.mkt_Delivery0.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // nud_stacksXpallet0
      // 
      this.nud_stacksXpallet0.DecimalPlaces = 0;
      this.nud_stacksXpallet0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_stacksXpallet0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_stacksXpallet0.ImagePassword = null;
      this.nud_stacksXpallet0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_stacksXpallet0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_stacksXpallet0.Location = new System.Drawing.Point(117, 320);
      this.nud_stacksXpallet0.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_stacksXpallet0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_stacksXpallet0.Name = "nud_stacksXpallet0";
      this.nud_stacksXpallet0.ReadOnly = true;
      this.nud_stacksXpallet0.Size = new System.Drawing.Size(109, 46);
      this.nud_stacksXpallet0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_stacksXpallet0.TabIndex = 18;
      this.nud_stacksXpallet0.TabStop = false;
      this.nud_stacksXpallet0.Text = "STACKS";
      this.nud_stacksXpallet0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_stacksXpallet0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_stacksXpallet0.TranslateText = true;
      this.nud_stacksXpallet0.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_stacksXpallet0.Value = "0";
      this.nud_stacksXpallet0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_stacksXpallet0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_stacksXpallet0.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_stacksXpallet0.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // nud_sheetsXstack0
      // 
      this.nud_sheetsXstack0.DecimalPlaces = 0;
      this.nud_sheetsXstack0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_sheetsXstack0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_sheetsXstack0.ImagePassword = null;
      this.nud_sheetsXstack0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_sheetsXstack0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_sheetsXstack0.Location = new System.Drawing.Point(124, 264);
      this.nud_sheetsXstack0.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.nud_sheetsXstack0.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_sheetsXstack0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_sheetsXstack0.Name = "nud_sheetsXstack0";
      this.nud_sheetsXstack0.ReadOnly = true;
      this.nud_sheetsXstack0.Size = new System.Drawing.Size(102, 46);
      this.nud_sheetsXstack0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_sheetsXstack0.TabIndex = 17;
      this.nud_sheetsXstack0.TabStop = false;
      this.nud_sheetsXstack0.Text = "Per stack";
      this.nud_sheetsXstack0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_sheetsXstack0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_sheetsXstack0.TranslateText = true;
      this.nud_sheetsXstack0.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_sheetsXstack0.Value = "0";
      this.nud_sheetsXstack0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_sheetsXstack0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_sheetsXstack0.Validating += new System.ComponentModel.CancelEventHandler(this.sheetsXstack0_Validating);
      this.nud_sheetsXstack0.Enter += new System.EventHandler(this.sheetsXstack0_Enter);
      // 
      // nud_SheetsDesired0
      // 
      this.nud_SheetsDesired0.DecimalPlaces = 0;
      this.nud_SheetsDesired0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_SheetsDesired0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SheetsDesired0.ImagePassword = null;
      this.nud_SheetsDesired0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_SheetsDesired0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_SheetsDesired0.Location = new System.Drawing.Point(124, 216);
      this.nud_SheetsDesired0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.nud_SheetsDesired0.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
      this.nud_SheetsDesired0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_SheetsDesired0.Name = "nud_SheetsDesired0";
      this.nud_SheetsDesired0.ReadOnly = true;
      this.nud_SheetsDesired0.Size = new System.Drawing.Size(102, 46);
      this.nud_SheetsDesired0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_SheetsDesired0.TabIndex = 16;
      this.nud_SheetsDesired0.TabStop = false;
      this.nud_SheetsDesired0.Text = "Desired";
      this.nud_SheetsDesired0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_SheetsDesired0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SheetsDesired0.TranslateText = true;
      this.nud_SheetsDesired0.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_SheetsDesired0.Value = "0";
      this.nud_SheetsDesired0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_SheetsDesired0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_SheetsDesired0.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_SheetsDesired0.Enter += new System.EventHandler(this.CleanSuggestionFields);
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
      this.nud_Boxes0.Location = new System.Drawing.Point(8, 320);
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
      this.nud_Boxes0.Size = new System.Drawing.Size(102, 46);
      this.nud_Boxes0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Boxes0.TabIndex = 15;
      this.nud_Boxes0.TabStop = false;
      this.nud_Boxes0.Text = "Boxes";
      this.nud_Boxes0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Boxes0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Boxes0.TranslateText = true;
      this.nud_Boxes0.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.nud_Boxes0.Value = "0";
      this.nud_Boxes0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Boxes0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Boxes0.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_Boxes0.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // nud_length0
      // 
      this.nud_length0.DecimalPlaces = 0;
      this.nud_length0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_length0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_length0.ImagePassword = null;
      this.nud_length0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_length0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_length0.Location = new System.Drawing.Point(8, 264);
      this.nud_length0.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.nud_length0.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_length0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_length0.Name = "nud_length0";
      this.nud_length0.ReadOnly = true;
      this.nud_length0.Size = new System.Drawing.Size(102, 46);
      this.nud_length0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_length0.TabIndex = 14;
      this.nud_length0.TabStop = false;
      this.nud_length0.Text = "Length";
      this.nud_length0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_length0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_length0.TranslateText = true;
      this.nud_length0.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_length0.Value = "0";
      this.nud_length0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_length0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_length0.Validating += new System.ComponentModel.CancelEventHandler(this.Flength_Validating);
      this.nud_length0.Enter += new System.EventHandler(this.Flength_Enter);
      // 
      // cmb_sending0
      // 
      this.cmb_sending0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_sending0.CanCancel = false;
      this.cmb_sending0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_sending0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_sending0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_sending0.ImagePassword = null;
      this.cmb_sending0.Location = new System.Drawing.Point(224, 587);
      this.cmb_sending0.Name = "cmb_sending0";
      this.cmb_sending0.ReadOnly = true;
      this.cmb_sending0.SelectedIndex = -1;
      this.cmb_sending0.SelectedItem = "";
      this.cmb_sending0.SelectedItemData = "";
      this.cmb_sending0.Size = new System.Drawing.Size(149, 50);
      this.cmb_sending0.Sorted = false;
      this.cmb_sending0.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_sending0.TabIndex = 112;
      this.cmb_sending0.TabStop = false;
      this.cmb_sending0.Text = "Sending";
      this.cmb_sending0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_sending0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_sending0.TranslateText = true;
      this.cmb_sending0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_sending0.SelectedItemChanged += new System.EventHandler(this.cmb_Quality_OR_cmb_Level_OR_cmb_Sending_SelectedItemChanged);
      this.cmb_sending0.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttl_Sheets0
      // 
      this.ttl_Sheets0.BackColor = System.Drawing.Color.Black;
      this.ttl_Sheets0.CausesValidation = false;
      this.ttl_Sheets0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Sheets0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Sheets0.ForeColor = System.Drawing.Color.White;
      this.ttl_Sheets0.Location = new System.Drawing.Point(116, 187);
      this.ttl_Sheets0.Name = "ttl_Sheets0";
      this.ttl_Sheets0.Size = new System.Drawing.Size(110, 124);
      this.ttl_Sheets0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Sheets0.TabIndex = 99;
      this.ttl_Sheets0.TabStop = false;
      this.ttl_Sheets0.Text = "SHEETS";
      this.ttl_Sheets0.TranslateText = true;
      // 
      // ttl_Format0
      // 
      this.ttl_Format0.BackColor = System.Drawing.Color.Black;
      this.ttl_Format0.CausesValidation = false;
      this.ttl_Format0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Format0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Format0.ForeColor = System.Drawing.Color.White;
      this.ttl_Format0.Location = new System.Drawing.Point(0, 187);
      this.ttl_Format0.Name = "ttl_Format0";
      this.ttl_Format0.Size = new System.Drawing.Size(110, 181);
      this.ttl_Format0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Format0.TabIndex = 98;
      this.ttl_Format0.TabStop = false;
      this.ttl_Format0.Text = "FORMAT";
      this.ttl_Format0.TranslateText = true;
      // 
      // cmb_Type0
      // 
      this.cmb_Type0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Type0.CanCancel = false;
      this.cmb_Type0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Type0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Type0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type0.ImagePassword = null;
      this.cmb_Type0.Location = new System.Drawing.Point(240, 130);
      this.cmb_Type0.Name = "cmb_Type0";
      this.cmb_Type0.ReadOnly = true;
      this.cmb_Type0.SelectedIndex = -1;
      this.cmb_Type0.SelectedItem = "";
      this.cmb_Type0.SelectedItemData = "";
      this.cmb_Type0.Size = new System.Drawing.Size(136, 50);
      this.cmb_Type0.Sorted = false;
      this.cmb_Type0.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Type0.TabIndex = 20;
      this.cmb_Type0.TabStop = false;
      this.cmb_Type0.Text = "Type";
      this.cmb_Type0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Type0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Type0.TranslateText = true;
      this.cmb_Type0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Type0.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttl_Score0
      // 
      this.ttl_Score0.BackColor = System.Drawing.Color.Black;
      this.ttl_Score0.CausesValidation = false;
      this.ttl_Score0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Score0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Score0.ForeColor = System.Drawing.Color.White;
      this.ttl_Score0.Location = new System.Drawing.Point(232, 55);
      this.ttl_Score0.Name = "ttl_Score0";
      this.ttl_Score0.Size = new System.Drawing.Size(144, 312);
      this.ttl_Score0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Score0.TabIndex = 96;
      this.ttl_Score0.TabStop = false;
      this.ttl_Score0.Text = "SCORERS";
      this.ttl_Score0.TranslateText = true;
      // 
      // cmb_Positioning0
      // 
      this.cmb_Positioning0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Positioning0.CanCancel = false;
      this.cmb_Positioning0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Positioning0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Positioning0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning0.ImagePassword = null;
      this.cmb_Positioning0.Location = new System.Drawing.Point(240, 75);
      this.cmb_Positioning0.Name = "cmb_Positioning0";
      this.cmb_Positioning0.ReadOnly = true;
      this.cmb_Positioning0.SelectedIndex = -1;
      this.cmb_Positioning0.SelectedItem = "";
      this.cmb_Positioning0.SelectedItemData = "";
      this.cmb_Positioning0.Size = new System.Drawing.Size(136, 50);
      this.cmb_Positioning0.Sorted = false;
      this.cmb_Positioning0.Style = SyncroC.Controls.TitledListStyles.Light;
      this.cmb_Positioning0.TabIndex = 19;
      this.cmb_Positioning0.TabStop = false;
      this.cmb_Positioning0.Text = "Positioning";
      this.cmb_Positioning0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Positioning0.TranslateText = true;
      this.cmb_Positioning0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Positioning0.SelectedItemChanged += new System.EventHandler(this.cmb_Positioning0_SelectedItemChanged);
      this.cmb_Positioning0.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttl_ord0
      // 
      this.ttl_ord0.BackColor = System.Drawing.Color.Black;
      this.ttl_ord0.CausesValidation = false;
      this.ttl_ord0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ord0.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ord0.ForeColor = System.Drawing.Color.White;
      this.ttl_ord0.Location = new System.Drawing.Point(0, 0);
      this.ttl_ord0.Name = "ttl_ord0";
      this.ttl_ord0.Size = new System.Drawing.Size(226, 182);
      this.ttl_ord0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_ord0.TabIndex = 92;
      this.ttl_ord0.TabStop = false;
      this.ttl_ord0.Text = "ORDER 1";
      this.ttl_ord0.TranslateText = true;
      // 
      // ttb_CustName0
      // 
      this.ttb_CustName0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CustName0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CustName0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustName0.ImagePassword = null;
      this.ttb_CustName0.ImeMode = System.Windows.Forms.ImeMode.On;
      this.ttb_CustName0.Location = new System.Drawing.Point(8, 76);
      this.ttb_CustName0.MaxChar = 30;
      this.ttb_CustName0.Multiline = false;
      this.ttb_CustName0.Name = "ttb_CustName0";
      this.ttb_CustName0.PasswordChar = '\0';
      this.ttb_CustName0.ReadOnly = true;
      this.ttb_CustName0.Size = new System.Drawing.Size(216, 50);
      this.ttb_CustName0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CustName0.TabIndex = 10;
      this.ttb_CustName0.TabStop = false;
      this.ttb_CustName0.Text = "Customer Name";
      this.ttb_CustName0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_CustName0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CustName0.TranslateText = true;
      this.ttb_CustName0.Value = " ";
      this.ttb_CustName0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_CustName0.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttb_OrderNumber0
      // 
      this.ttb_OrderNumber0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_OrderNumber0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_OrderNumber0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_OrderNumber0.ImagePassword = null;
      this.ttb_OrderNumber0.ImeMode = System.Windows.Forms.ImeMode.On;
      this.ttb_OrderNumber0.Location = new System.Drawing.Point(8, 22);
      this.ttb_OrderNumber0.MaxChar = 20;
      this.ttb_OrderNumber0.Multiline = false;
      this.ttb_OrderNumber0.Name = "ttb_OrderNumber0";
      this.ttb_OrderNumber0.PasswordChar = '\0';
      this.ttb_OrderNumber0.ReadOnly = true;
      this.ttb_OrderNumber0.Size = new System.Drawing.Size(216, 50);
      this.ttb_OrderNumber0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_OrderNumber0.TabIndex = 9;
      this.ttb_OrderNumber0.TabStop = false;
      this.ttb_OrderNumber0.Text = "Number";
      this.ttb_OrderNumber0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_OrderNumber0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_OrderNumber0.TranslateText = true;
      this.ttb_OrderNumber0.Value = " ";
      this.ttb_OrderNumber0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_OrderNumber0.Validating += new System.ComponentModel.CancelEventHandler(this.ttb_NotNullString_Validating);
      this.ttb_OrderNumber0.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // cmb_Level0
      // 
      this.cmb_Level0.BackColor = System.Drawing.Color.LightGray;
      this.cmb_Level0.CanCancel = false;
      this.cmb_Level0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Level0.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_Level0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Level0.ImagePassword = null;
      this.cmb_Level0.Location = new System.Drawing.Point(232, 0);
      this.cmb_Level0.Name = "cmb_Level0";
      this.cmb_Level0.ReadOnly = true;
      this.cmb_Level0.SelectedIndex = -1;
      this.cmb_Level0.SelectedItem = "";
      this.cmb_Level0.SelectedItemData = "";
      this.cmb_Level0.Size = new System.Drawing.Size(144, 50);
      this.cmb_Level0.Sorted = false;
      this.cmb_Level0.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_Level0.TabIndex = 8;
      this.cmb_Level0.TabStop = false;
      this.cmb_Level0.Text = "LEVEL";
      this.cmb_Level0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Level0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Level0.TranslateText = true;
      this.cmb_Level0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.cmb_Level0.SelectedItemChanged += new System.EventHandler(this.cmb_Quality_OR_cmb_Level_OR_cmb_Sending_SelectedItemChanged);
      this.cmb_Level0.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttb_Measures0
      // 
      this.ttb_Measures0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_Measures0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Measures0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Measures0.ImagePassword = null;
      this.ttb_Measures0.Location = new System.Drawing.Point(240, 185);
      //RFAZZI_FI03/12/2010_2. Aumentato il numero di cordoni massimo per scatola da 24 a 48. 
      //this.ttb_Measures0.MaxChar = 312;
      this.ttb_Measures0.MaxChar = 624;
      this.ttb_Measures0.Multiline = true;
      this.ttb_Measures0.Name = "ttb_Measures0";
      this.ttb_Measures0.PasswordChar = '\0';
      this.ttb_Measures0.ReadOnly = true;
      this.ttb_Measures0.Size = new System.Drawing.Size(136, 181);
      this.ttb_Measures0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_Measures0.TabIndex = 21;
      this.ttb_Measures0.TabStop = false;
      this.ttb_Measures0.Text = "Measures";
      this.ttb_Measures0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_Measures0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Measures0.TranslateText = true;
      this.ttb_Measures0.Value = " ";
      this.ttb_Measures0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_Measures0.Enter += new System.EventHandler(this.CleanSuggestionFields);
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
      this.nud_Width0.Location = new System.Drawing.Point(8, 216);
      this.nud_Width0.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
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
      this.nud_Width0.Size = new System.Drawing.Size(102, 46);
      this.nud_Width0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_Width0.TabIndex = 13;
      this.nud_Width0.TabStop = false;
      this.nud_Width0.Text = "Width";
      this.nud_Width0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Width0.TranslateText = true;
      this.nud_Width0.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Width0.Value = "0";
      this.nud_Width0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.nud_Width0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_Width0.Validating += new System.ComponentModel.CancelEventHandler(this.Fwidth0_Validating);
      this.nud_Width0.Leave += new System.EventHandler(this.cmb_Quality_OR_cmb_Level_OR_cmb_Sending_SelectedItemChanged);
      this.nud_Width0.Enter += new System.EventHandler(this.Fwidth0_Enter);
      // 
      // ttb_DataToPrint0_2
      // 
      this.ttb_DataToPrint0_2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_DataToPrint0_2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_DataToPrint0_2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint0_2.ImagePassword = null;
      this.ttb_DataToPrint0_2.Location = new System.Drawing.Point(8, 449);
      this.ttb_DataToPrint0_2.MaxChar = 58;
      this.ttb_DataToPrint0_2.Multiline = true;
      this.ttb_DataToPrint0_2.Name = "ttb_DataToPrint0_2";
      this.ttb_DataToPrint0_2.PasswordChar = '\0';
      this.ttb_DataToPrint0_2.ReadOnly = true;
      this.ttb_DataToPrint0_2.Size = new System.Drawing.Size(365, 50);
      this.ttb_DataToPrint0_2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_DataToPrint0_2.TabIndex = 108;
      this.ttb_DataToPrint0_2.TabStop = false;
      this.ttb_DataToPrint0_2.Text = "2";
      this.ttb_DataToPrint0_2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_DataToPrint0_2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_DataToPrint0_2.TranslateText = false;
      this.ttb_DataToPrint0_2.Value = " ";
      this.ttb_DataToPrint0_2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      // 
      // ttb_SuggMin
      // 
      this.ttb_SuggMin.CausesValidation = false;
      this.ttb_SuggMin.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_SuggMin.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_SuggMin.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_SuggMin.ImagePassword = null;
      this.ttb_SuggMin.Location = new System.Drawing.Point(1030, 193);
      this.ttb_SuggMin.MaxChar = 32767;
      this.ttb_SuggMin.Multiline = false;
      this.ttb_SuggMin.Name = "ttb_SuggMin";
      this.ttb_SuggMin.PasswordChar = '\0';
      this.ttb_SuggMin.ReadOnly = true;
      this.ttb_SuggMin.Size = new System.Drawing.Size(171, 60);
      this.ttb_SuggMin.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_SuggMin.TabIndex = 201;
      this.ttb_SuggMin.TabStop = false;
      this.ttb_SuggMin.Text = "MIN";
      this.ttb_SuggMin.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_SuggMin.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_SuggMin.TranslateText = true;
      this.ttb_SuggMin.Value = " ";
      this.ttb_SuggMin.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // cmb_Quality
      // 
      this.cmb_Quality.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_Quality.CanCancel = false;
      this.cmb_Quality.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Quality.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Quality.ImagePassword = null;
      this.cmb_Quality.Location = new System.Drawing.Point(656, 193);
      this.cmb_Quality.Name = "cmb_Quality";
      this.cmb_Quality.ReadOnly = true;
      this.cmb_Quality.SelectedIndex = -1;
      this.cmb_Quality.SelectedItem = "";
      this.cmb_Quality.SelectedItemData = "";
      this.cmb_Quality.Size = new System.Drawing.Size(355, 60);
      this.cmb_Quality.Sorted = true;
      this.cmb_Quality.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_Quality.TabIndex = 3;
      this.cmb_Quality.TabStop = false;
      this.cmb_Quality.Text = "QUALITY";
      this.cmb_Quality.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Quality.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Quality.TranslateText = true;
      this.cmb_Quality.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Quality.SelectedItemChanged += new System.EventHandler(this.cmb_Quality_OR_cmb_Level_OR_cmb_Sending_SelectedItemChanged);
      this.cmb_Quality.Leave += new System.EventHandler(this.cmb_Quality_OR_cmb_Level_OR_cmb_Sending_SelectedItemChanged);
      this.cmb_Quality.Enter += new System.EventHandler(this.CleanSuggestionFields);
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
      this.nud_WebWidth.Location = new System.Drawing.Point(207, 260);
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
      this.nud_WebWidth.Size = new System.Drawing.Size(133, 60);
      this.nud_WebWidth.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_WebWidth.TabIndex = 4;
      this.nud_WebWidth.TabStop = false;
      this.nud_WebWidth.Text = "WEB WIDTH";
      this.nud_WebWidth.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_WebWidth.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_WebWidth.TranslateText = true;
      this.nud_WebWidth.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_WebWidth.Value = "";
      this.nud_WebWidth.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_WebWidth.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_WebWidth.Validating += new System.ComponentModel.CancelEventHandler(this.nud_WebWidth_Validating);
      this.nud_WebWidth.Enter += new System.EventHandler(this.nud_WebWidth_Enter);
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
      this.nud_Trim.Location = new System.Drawing.Point(878, 260);
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
      this.nud_Trim.Size = new System.Drawing.Size(133, 60);
      this.nud_Trim.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_Trim.TabIndex = 7;
      this.nud_Trim.TabStop = false;
      this.nud_Trim.Text = "TRIM";
      this.nud_Trim.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_Trim.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Trim.TranslateText = true;
      this.nud_Trim.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_Trim.Value = "0";
      this.nud_Trim.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_Trim.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // nud_ScoreGap
      // 
      this.nud_ScoreGap.DecimalPlaces = 0;
      this.nud_ScoreGap.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nud_ScoreGap.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap.ImagePassword = null;
      this.nud_ScoreGap.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_ScoreGap.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap.Location = new System.Drawing.Point(485, 260);
      this.nud_ScoreGap.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_ScoreGap.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_ScoreGap.Name = "nud_ScoreGap";
      this.nud_ScoreGap.ReadOnly = true;
      this.nud_ScoreGap.Size = new System.Drawing.Size(133, 60);
      this.nud_ScoreGap.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_ScoreGap.TabIndex = 5;
      this.nud_ScoreGap.TabStop = false;
      this.nud_ScoreGap.Text = "SCORE GAP";
      this.nud_ScoreGap.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_ScoreGap.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScoreGap.TranslateText = true;
      this.nud_ScoreGap.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_ScoreGap.Value = "0";
      this.nud_ScoreGap.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_ScoreGap.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_ScoreGap.Validating += new System.ComponentModel.CancelEventHandler(this.nud_PositiveValues_Validating);
      this.nud_ScoreGap.Enter += new System.EventHandler(this.nud_ScoreGap_Enter);
      // 
      // cmb_TrimType
      // 
      this.cmb_TrimType.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_TrimType.CanCancel = false;
      this.cmb_TrimType.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_TrimType.ForeColor = System.Drawing.SystemColors.ControlText;
      this.cmb_TrimType.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_TrimType.ImagePassword = null;
      this.cmb_TrimType.Location = new System.Drawing.Point(624, 260);
      this.cmb_TrimType.Name = "cmb_TrimType";
      this.cmb_TrimType.ReadOnly = true;
      this.cmb_TrimType.SelectedIndex = -1;
      this.cmb_TrimType.SelectedItem = "";
      this.cmb_TrimType.SelectedItemData = "";
      this.cmb_TrimType.Size = new System.Drawing.Size(248, 60);
      this.cmb_TrimType.Sorted = false;
      this.cmb_TrimType.Style = SyncroC.Controls.TitledListStyles.Normal;
      this.cmb_TrimType.TabIndex = 6;
      this.cmb_TrimType.TabStop = false;
      this.cmb_TrimType.Text = "TRIM TYPE";
      this.cmb_TrimType.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_TrimType.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_TrimType.TranslateText = true;
      this.cmb_TrimType.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_TrimType.Enter += new System.EventHandler(this.CleanSuggestionFields);
      // 
      // ttb_index
      // 
      this.ttb_index.CausesValidation = false;
      this.ttb_index.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_index.Font = new System.Drawing.Font("Arial Unicode MS", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_index.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_index.ImagePassword = null;
      this.ttb_index.Location = new System.Drawing.Point(30, 193);
      this.ttb_index.MaxChar = 2;
      this.ttb_index.Multiline = false;
      this.ttb_index.Name = "ttb_index";
      this.ttb_index.PasswordChar = '\0';
      this.ttb_index.ReadOnly = true;
      this.ttb_index.Size = new System.Drawing.Size(171, 126);
      this.ttb_index.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_index.TabIndex = 205;
      this.ttb_index.TabStop = false;
      this.ttb_index.Text = "QUEUE";
      this.ttb_index.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_index.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_index.TranslateText = true;
      this.ttb_index.Value = " ";
      this.ttb_index.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_SuggMax
      // 
      this.ttb_SuggMax.CausesValidation = false;
      this.ttb_SuggMax.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_SuggMax.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_SuggMax.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_SuggMax.ImagePassword = null;
      this.ttb_SuggMax.Location = new System.Drawing.Point(1030, 260);
      this.ttb_SuggMax.MaxChar = 32767;
      this.ttb_SuggMax.Multiline = false;
      this.ttb_SuggMax.Name = "ttb_SuggMax";
      this.ttb_SuggMax.PasswordChar = '\0';
      this.ttb_SuggMax.ReadOnly = true;
      this.ttb_SuggMax.Size = new System.Drawing.Size(171, 60);
      this.ttb_SuggMax.Style = SyncroC.Controls.TitledTextBoxStyles.Normal;
      this.ttb_SuggMax.TabIndex = 207;
      this.ttb_SuggMax.TabStop = false;
      this.ttb_SuggMax.Text = "MAX";
      this.ttb_SuggMax.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.ttb_SuggMax.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_SuggMax.TranslateText = true;
      this.ttb_SuggMax.Value = " ";
      this.ttb_SuggMax.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // PageState
      // 
      this.PageState.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.PageState.Location = new System.Drawing.Point(443, 57);
      this.PageState.Name = "PageState";
      this.PageState.Size = new System.Drawing.Size(232, 24);
      this.PageState.TabIndex = 208;
      this.PageState.Text = "Page state: ";
      this.PageState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.PageState.Visible = false;
      // 
      // iml_valid
      // 
      this.iml_valid.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_valid.ImageStream")));
      this.iml_valid.TransparentColor = System.Drawing.Color.Transparent;
      this.iml_valid.Images.SetKeyName(0, "valido.bmp");
      this.iml_valid.Images.SetKeyName(1, "allarme.bmp");
      // 
      // NextPageState
      // 
      this.NextPageState.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.NextPageState.Location = new System.Drawing.Point(744, 57);
      this.NextPageState.Name = "NextPageState";
      this.NextPageState.Size = new System.Drawing.Size(232, 24);
      this.NextPageState.TabIndex = 212;
      this.NextPageState.Text = "NextPage state: ";
      this.NextPageState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
      this.nud_SetupLength.Location = new System.Drawing.Point(346, 260);
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
      this.nud_SetupLength.Size = new System.Drawing.Size(133, 60);
      this.nud_SetupLength.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.nud_SetupLength.TabIndex = 213;
      this.nud_SetupLength.TabStop = false;
      this.nud_SetupLength.Text = "SETUP LENGTH";
      this.nud_SetupLength.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_SetupLength.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SetupLength.TranslateText = true;
      this.nud_SetupLength.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.nud_SetupLength.Value = "";
      this.nud_SetupLength.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_SetupLength.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // pnl_ScrollOrder
      // 
      this.pnl_ScrollOrder.AutoScroll = true;
      this.pnl_ScrollOrder.Controls.Add(this.pnl_Orders1);
      this.pnl_ScrollOrder.Controls.Add(this.pnl_OrdersO);
      this.pnl_ScrollOrder.Controls.Add(this.pnl_Orders2);
      this.pnl_ScrollOrder.Location = new System.Drawing.Point(12, 326);
      this.pnl_ScrollOrder.Name = "pnl_ScrollOrder";
      this.pnl_ScrollOrder.Size = new System.Drawing.Size(1208, 400);
      this.pnl_ScrollOrder.TabIndex = 214;
      // 
      // pnl_Debug
      // 
      this.pnl_Debug.AutoScroll = true;
      this.pnl_Debug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      this.pnl_Debug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnl_Debug.Controls.Add(this.lbl_DebugPastPrint2);
      this.pnl_Debug.Controls.Add(this.lbl_DebugPalletPrint2);
      this.pnl_Debug.Controls.Add(this.lbl_DebugPastPrint1);
      this.pnl_Debug.Controls.Add(this.lbl_DebugPalletPrint1);
      this.pnl_Debug.Controls.Add(this.lbl_DebugPastPrint0);
      this.pnl_Debug.Controls.Add(this.lbl_DebugPalletPrint0);
      this.pnl_Debug.Controls.Add(this.lbl_DebugSetupModId);
      this.pnl_Debug.Controls.Add(this.lbl_DebugSetupID);
      this.pnl_Debug.Location = new System.Drawing.Point(997, 172);
      this.pnl_Debug.Name = "pnl_Debug";
      this.pnl_Debug.Size = new System.Drawing.Size(207, 148);
      this.pnl_Debug.TabIndex = 267;
      this.pnl_Debug.Visible = false;
      // 
      // lbl_DebugPastPrint2
      // 
      this.lbl_DebugPastPrint2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DebugPastPrint2.CausesValidation = false;
      this.lbl_DebugPastPrint2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DebugPastPrint2.Location = new System.Drawing.Point(3, 141);
      this.lbl_DebugPastPrint2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.lbl_DebugPastPrint2.Name = "lbl_DebugPastPrint2";
      this.lbl_DebugPastPrint2.Size = new System.Drawing.Size(176, 18);
      this.lbl_DebugPastPrint2.TabIndex = 159;
      this.lbl_DebugPastPrint2.Text = "Past (Lay - Cpy) 3 ->";
      this.lbl_DebugPastPrint2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DebugPalletPrint2
      // 
      this.lbl_DebugPalletPrint2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DebugPalletPrint2.CausesValidation = false;
      this.lbl_DebugPalletPrint2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DebugPalletPrint2.Location = new System.Drawing.Point(3, 121);
      this.lbl_DebugPalletPrint2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.lbl_DebugPalletPrint2.Name = "lbl_DebugPalletPrint2";
      this.lbl_DebugPalletPrint2.Size = new System.Drawing.Size(176, 18);
      this.lbl_DebugPalletPrint2.TabIndex = 158;
      this.lbl_DebugPalletPrint2.Text = "Pal (Lay - Cpy) 3 ->";
      this.lbl_DebugPalletPrint2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DebugPastPrint1
      // 
      this.lbl_DebugPastPrint1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DebugPastPrint1.CausesValidation = false;
      this.lbl_DebugPastPrint1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DebugPastPrint1.Location = new System.Drawing.Point(3, 101);
      this.lbl_DebugPastPrint1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.lbl_DebugPastPrint1.Name = "lbl_DebugPastPrint1";
      this.lbl_DebugPastPrint1.Size = new System.Drawing.Size(176, 18);
      this.lbl_DebugPastPrint1.TabIndex = 157;
      this.lbl_DebugPastPrint1.Text = "Past (Lay - Cpy) 2 ->";
      this.lbl_DebugPastPrint1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DebugPalletPrint1
      // 
      this.lbl_DebugPalletPrint1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DebugPalletPrint1.CausesValidation = false;
      this.lbl_DebugPalletPrint1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DebugPalletPrint1.Location = new System.Drawing.Point(3, 81);
      this.lbl_DebugPalletPrint1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.lbl_DebugPalletPrint1.Name = "lbl_DebugPalletPrint1";
      this.lbl_DebugPalletPrint1.Size = new System.Drawing.Size(176, 18);
      this.lbl_DebugPalletPrint1.TabIndex = 156;
      this.lbl_DebugPalletPrint1.Text = "Pal (Lay - Cpy) 2 ->";
      this.lbl_DebugPalletPrint1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DebugPastPrint0
      // 
      this.lbl_DebugPastPrint0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DebugPastPrint0.CausesValidation = false;
      this.lbl_DebugPastPrint0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DebugPastPrint0.Location = new System.Drawing.Point(3, 61);
      this.lbl_DebugPastPrint0.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.lbl_DebugPastPrint0.Name = "lbl_DebugPastPrint0";
      this.lbl_DebugPastPrint0.Size = new System.Drawing.Size(176, 18);
      this.lbl_DebugPastPrint0.TabIndex = 155;
      this.lbl_DebugPastPrint0.Text = "Past (Lay - Cpy) 1 ->";
      this.lbl_DebugPastPrint0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DebugPalletPrint0
      // 
      this.lbl_DebugPalletPrint0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DebugPalletPrint0.CausesValidation = false;
      this.lbl_DebugPalletPrint0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DebugPalletPrint0.Location = new System.Drawing.Point(3, 41);
      this.lbl_DebugPalletPrint0.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.lbl_DebugPalletPrint0.Name = "lbl_DebugPalletPrint0";
      this.lbl_DebugPalletPrint0.Size = new System.Drawing.Size(176, 18);
      this.lbl_DebugPalletPrint0.TabIndex = 154;
      this.lbl_DebugPalletPrint0.Text = "Pal (Lay - Cpy) 1 ->";
      this.lbl_DebugPalletPrint0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DebugSetupModId
      // 
      this.lbl_DebugSetupModId.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DebugSetupModId.CausesValidation = false;
      this.lbl_DebugSetupModId.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DebugSetupModId.Location = new System.Drawing.Point(3, 21);
      this.lbl_DebugSetupModId.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.lbl_DebugSetupModId.Name = "lbl_DebugSetupModId";
      this.lbl_DebugSetupModId.Size = new System.Drawing.Size(176, 18);
      this.lbl_DebugSetupModId.TabIndex = 153;
      this.lbl_DebugSetupModId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_DebugSetupID
      // 
      this.lbl_DebugSetupID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.lbl_DebugSetupID.CausesValidation = false;
      this.lbl_DebugSetupID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_DebugSetupID.Location = new System.Drawing.Point(3, 1);
      this.lbl_DebugSetupID.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
      this.lbl_DebugSetupID.Name = "lbl_DebugSetupID";
      this.lbl_DebugSetupID.Size = new System.Drawing.Size(176, 18);
      this.lbl_DebugSetupID.TabIndex = 152;
      this.lbl_DebugSetupID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // ActiveCtrl
      // 
      this.ActiveCtrl.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ActiveCtrl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ActiveCtrl.Location = new System.Drawing.Point(110, 58);
      this.ActiveCtrl.Name = "ActiveCtrl";
      this.ActiveCtrl.Size = new System.Drawing.Size(344, 21);
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
      // Frm_QuickInsDryOrder
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.pnl_Debug);
      this.Controls.Add(this.ActiveCtrl);
      this.Controls.Add(this.NextPageState);
      this.Controls.Add(this.PageState);
      this.Controls.Add(this.ttb_SuggMax);
      this.Controls.Add(this.nud_Trim);
      this.Controls.Add(this.ttb_SuggMin);
      this.Controls.Add(this.pnl_ScrollOrder);
      this.Controls.Add(this.nud_SetupLength);
      this.Controls.Add(this.ttb_SetUpSequence);
      this.Controls.Add(this.cmb_Flute);
      this.Controls.Add(this.nud_ScoreGap);
      this.Controls.Add(this.cmb_TrimType);
      this.Controls.Add(this.cmb_Quality);
      this.Controls.Add(this.ttb_ProgramNumber);
      this.Controls.Add(this.nud_WebWidth);
      this.Controls.Add(this.ttb_index);
      this.DesignGridVerticalLines = 2;
      this.Name = "Frm_QuickInsDryOrder";
      this.Tag = "";
      this.Text = "Frm_QuickInsDryOrder";
      this.Activated += new System.EventHandler(this.Frm_QuickInsDryOrder_Activated);
      this.Controls.SetChildIndex(this.ttb_index, 0);
      this.Controls.SetChildIndex(this.nud_WebWidth, 0);
      this.Controls.SetChildIndex(this.ttb_ProgramNumber, 0);
      this.Controls.SetChildIndex(this.cmb_Quality, 0);
      this.Controls.SetChildIndex(this.cmb_TrimType, 0);
      this.Controls.SetChildIndex(this.nud_ScoreGap, 0);
      this.Controls.SetChildIndex(this.cmb_Flute, 0);
      this.Controls.SetChildIndex(this.ttb_SetUpSequence, 0);
      this.Controls.SetChildIndex(this.nud_SetupLength, 0);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.pnl_ScrollOrder, 0);
      this.Controls.SetChildIndex(this.ttb_SuggMin, 0);
      this.Controls.SetChildIndex(this.nud_Trim, 0);
      this.Controls.SetChildIndex(this.ttb_SuggMax, 0);
      this.Controls.SetChildIndex(this.PageState, 0);
      this.Controls.SetChildIndex(this.NextPageState, 0);
      this.Controls.SetChildIndex(this.ActiveCtrl, 0);
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
      this.Controls.SetChildIndex(this.pnl_Debug, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.pnl_Orders2.ResumeLayout(false);
      this.pnl_Orders1.ResumeLayout(false);
      this.pnl_OrdersO.ResumeLayout(false);
      this.pnl_ScrollOrder.ResumeLayout(false);
      this.pnl_Debug.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    #endregion

    /// <summary>
    /// Escape key behaviour: Abort the operation
    /// </summary>
    protected override void Escape()
    {
      if (state == State.Modify || state == State.Insert)
        SetPageState(State.Read);
      //agrassi_FI01-06-2010_1
      this.copyAndModify = false;
    }

    /// <summary>
    /// Toggle visibility of Debugging informations
    /// </summary>
    protected override void ControlD()
    {
      base.ControlD();
      PageState.Visible =
      NextPageState.Visible =
        //AGrassi_FI31/08/2007_3
      ActiveCtrl.Visible =
      this.pnl_Debug.Visible = !PageState.Visible;
    }
    //AGrassi_FI31/08/2007_3
    private void tim_debug_Tick(object sender, EventArgs e)
    {
      if (this.ActiveControl != null)
        this.ActiveCtrl.Text = "Active control: " + this.ActiveControl.Name + " (" + this.ActiveControl.TabIndex.ToString() + ")";
    }


    protected override void F10()
    {
    }

    /// <summary>
    /// Change the Return key behaviour when one of the Measures field is focused.
    /// </summary>
    protected override void Return()
    {
      if (ttb_Measures0.Focused)
      {
        if ((ttb_Measures0.SelectionLength > 0) || (ttb_Measures0.NotTrimmedValue.EndsWith("\r\n\r\n")))
        {
          //RFAZZI_FI.Changed the above value for level
          //if (this.knife.CutToMark)
          //agrassi_FI30-07-08_1
          //if (this.knife.KnifeLevelSettings[0].CutToMark || this.knife.KnifeLevelSettings[1].CutToMark || this.knife.KnifeLevelSettings[2].CutToMark)
          if (this.knife.KnifeLevelSettings[0].CutToMark)
            //AGrassi_FI31/08/2007_3
            this.chk_CutToMark0.Focus();
          //this.chk_CutToMark0.Focus();
          else
            //AGrassi_FI31/08/2007_3
            //this.ttb_OrderNumber1.Select();
            this.cmb_Level1.Focus();
        }
      }
      else if (ttb_Measures1.Focused)
      {
        if ((ttb_Measures1.SelectionLength > 0) || (ttb_Measures1.NotTrimmedValue.EndsWith("\r\n\r\n")))
        {
          //RFAZZI_FI.Changed the above value for level
          //if (this.knife.CutToMark)
          //agrassi_FI30-07-08_1
          //if (this.knife.KnifeLevelSettings[0].CutToMark || this.knife.KnifeLevelSettings[1].CutToMark || this.knife.KnifeLevelSettings[2].CutToMark)
          if (this.knife.KnifeLevelSettings[0].CutToMark || this.knife.KnifeLevelSettings[1].CutToMark || this.knife.KnifeLevelSettings[2].CutToMark)
            //AGrassi_FI31/08/2007_3
            //this.ttb_OrderNumber2.Select();
            this.chk_CutToMark1.Focus();
          else
            this.cmb_Level2.Focus();
        }
      }
      else if (ttb_Measures2.Focused)
      {
        if ((ttb_Measures2.SelectionLength > 0) || (ttb_Measures2.NotTrimmedValue.EndsWith("\r\n\r\n")))
        {
          //RFAZZI_FI.Changed the above value for level
          //if (this.knife.CutToMark)
          //agrassi_FI30-07-08_1
          //if (this.knife.KnifeLevelSettings[0].CutToMark || this.knife.KnifeLevelSettings[1].CutToMark || this.knife.KnifeLevelSettings[2].CutToMark)
          if (this.knife.KnifeLevelSettings[0].CutToMark || this.knife.KnifeLevelSettings[1].CutToMark || this.knife.KnifeLevelSettings[2].CutToMark)
            //AGrassi_FI31/08/2007_3
            this.chk_CutToMark2.Focus();
          else
            this.ttb_ProgramNumber.Focus();
        }
      }
      else
      {
        base.Return();
      }
    }

    /// <summary>
    /// Allow user to enter a new order.
    /// </summary>
    protected override void CtrlF1()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      SetPageState(State.Insert);
    }

    /// <summary>
    /// Allow user to enter a new order coping values from the currently showed order.
    /// </summary>
    protected override void CtrlF2()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;
      //agrassi_FI01-06-2010_1
      this.copyAndModify = true;
      SetPageState(State.Insert, true);//change state to State.Insert without clean any fields
    }

    //agrassi_FI11-11-2010_1
    /// <summary>
    /// Allow user to swap orders for the setup.
    /// </summary>
    protected override void CtrlF3()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;
      if (Frm_OrdersInsSubGroup.indexOrderToShowDRY == 0)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_01", true);
        return;
      }
      if (Frm_OrdersInsSubGroup.indexOrderToShowDRY >= order.Rows.Count)
        return;
      DataRow row = order.Rows[Frm_OrdersInsSubGroup.indexOrderToShowDRY];
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
        string cmd = Command.DB_ORDER + "." + Command.DB_CMD_SWAP_ORDERS + "(" + this.lbl_DebugSetupID.Text + ")";
        cm.SendTcp(cm.SYNCROS_DB_MANAGER, cmd);
      }
    }

    /// <summary>
    /// Show previous order.
    /// </summary>
    protected override void PageUp()
    {
      if (indexOrderToShowDRY == 0 || state != State.Read)
        return;
      showOrder(--indexOrderToShowDRY);
    }

    /// <summary>
    /// Show next order.
    /// </summary>
    protected override void PageDown()
    {
      if (indexOrderToShowDRY == order.Rows.Count - 1 || state != State.Read)
        return;
      showOrder(++indexOrderToShowDRY);
    }

    /// <summary>
    /// Save.
    /// </summary>
    protected override void CtrlF4()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      if (state != State.Read)
      {
        //Quick validation
        if (!this.CheckAndUpdateTrim(null, true))
          return;


        //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
        if (!CheckSheetsPerStack())
          return;

        DataRow modifingRow = null;

        string cmd = Command.DB_ORDER + ".";
        if (state == State.Insert)
          cmd += Command.DB_CMD_ADD;
        else if (state == State.Modify)
        {
          modifingRow = order.Rows.Find(this.lbl_DebugSetupID.Text);
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
        ScoreMeasure defValues = new ScoreMeasure();

        /**	
         * HEADER
         */
        if ((state == State.Modify))
          cmd += lbl_DebugSetupID.Text + Command.PARAMETER_SEPARATOR;//0 SetupID		
        else if (state == State.Insert)
          cmd += " " + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(SetupCode) + Command.PARAMETER_SEPARATOR;								//1	SetupCode
        cmd += this.NotEmptyString(this.nud_WebWidth.Value) + Command.PARAMETER_SEPARATOR;	//2 WebWidth
        if (this.cmb_TrimType.SelectedItemData != string.Empty)		//3 TrimType
          cmd += Enum.Format(typeof(SyncroC.Utility.TrimType), Enum.Parse(typeof(SyncroC.Utility.TrimType), this.cmb_TrimType.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.nud_ScoreGap.Value) + Command.PARAMETER_SEPARATOR;	//4 ScoreGap
        cmd += this.NotEmptyString(this.cmb_Quality.Value) + Command.PARAMETER_SEPARATOR;		//5 QualityID
        //agrassi_FI05-10-2010_1
        cmd += this.NotEmptyString(this.cmb_Flute.Value) + Command.PARAMETER_SEPARATOR;		//5bis FluteID
				//RFAZZI_28/01/2009_1.Sent to SyncroS the "Quality Weight" and "Quality Thickness" when a order is inserted or modified. Required for Setup Validation . 
				DataRow qualityRow = DBManager.getDBManager().quality.getRow(this.cmb_Quality.Value);
        if(qualityRow == null)
        {
          PageManager.getPageManager()._SetMessage("§SYCC_79", true);
          SetPageState(State.Read);
          return;
        }

				//qualityRow = quality.Rows.Find(this.cmb_Quality.Value);
        if (qualityRow == null)
        {
          PageManager.getPageManager()._SetMessage("§SYCC_79", true);
          SetPageState(State.Read);
          return;
        }
				cmd += this.NotEmptyString(qualityRow[Quality.WEIGHT_FACTOR].ToString()) + Command.PARAMETER_SEPARATOR;			//6 Quality Weight
				cmd += this.NotEmptyString(qualityRow[Quality.THICKNESS_UDdmm].ToString()) + Command.PARAMETER_SEPARATOR;			//7 Quality Thickness
        cmd += this.NotEmptyString(this.nud_Trim.Value) + Command.PARAMETER_SEPARATOR;			//8 Trim
        /**
         * ORDER 0
         */
        cmd += this.NotEmptyString(this.ttb_OrderNumber0.Value) + Command.PARAMETER_SEPARATOR; //9 orderNumber
        cmd += this.NotEmptyString(SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeString(this.mkt_Delivery0.ValueAsUNIVERSALDateTime)) + Command.PARAMETER_SEPARATOR; //10 delivery
        cmd += this.NotEmptyString(this.ttb_CustName0.Value) + Command.PARAMETER_SEPARATOR;		//11 customerName      
        cmd += this.NotEmptyString(this.nud_Width0.Value) + Command.PARAMETER_SEPARATOR;			//12 SheetWidth0
        cmd += this.NotEmptyString(this.nud_Boxes0.Value) + Command.PARAMETER_SEPARATOR;			//13 Outs0
        cmd += Measures_componentToDb(ttb_Measures0.Value) + Command.PARAMETER_SEPARATOR;				//14 scoreMeasures0
        cmd += this.NotEmptyString(this.cmb_Type0.Value) + Command.PARAMETER_SEPARATOR;				//15 ScoreType0
        if (this.cmb_Positioning0.SelectedItemData != string.Empty) //16 PositType0
          cmd += Enum.Format(typeof(SyncroC.Utility.PositType), Enum.Parse(typeof(SyncroC.Utility.PositType), this.cmb_Positioning0.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;

        if (state == State.Modify)
        {
          cmd += this.NotEmptyString(modifingRow[Order.DEL_TELETWIN_OFFSET_0].ToString()) + Command.PARAMETER_SEPARATOR;	//17 TeleTwinOffset0
          string gap = modifingRow[Order.DEL_SCORE_GAP_0].ToString();
          if ((this.intParse(gap) == this.oldScoreGap))
            cmd += this.NotEmptyString(this.nud_ScoreGap.Value) + Command.PARAMETER_SEPARATOR;	//18 ScoreGap0
          else
            cmd += this.NotEmptyString(gap) + Command.PARAMETER_SEPARATOR;	//18 ScoreGap0
        }
        else
        {
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
        }
        if (this.cmb_Level0.SelectedItemData != string.Empty)		//19 LevelName0
          cmd += Enum.Format(typeof(SyncroC.Utility.LevelName), Enum.Parse(typeof(SyncroC.Utility.LevelName), this.cmb_Level0.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.nud_length0.Value) + Command.PARAMETER_SEPARATOR;					//20 sheetLenght0
        cmd += this.NotEmptyString(this.nud_SheetsDesired0.Value) + Command.PARAMETER_SEPARATOR;	//21 segmentSheetCount0
        cmd += this.NotEmptyString(this.nud_sheetsXstack0.Value) + Command.PARAMETER_SEPARATOR;		//22 sheetsPerStack0
        cmd += this.NotEmptyString(this.nud_stacksXpallet0.Value) + Command.PARAMETER_SEPARATOR;	//23 stacksPerPallet0
        if (this.cmb_sending0.SelectedItemData != string.Empty)	//24 Sending0
          cmd += Enum.Format(typeof(SyncroC.Utility.PalletSending), Enum.Parse(typeof(SyncroC.Utility.PalletSending), this.cmb_sending0.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR; //sending
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        cmd += this.chk_CutToMark0.Checked.ToString() + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.cmb_cutToPattern0.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_DataToPrint0_1.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_DataToPrint0_2.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_DataToPrint0_3.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_DataToPrint0_4.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.chk_StackerContStack0.Checked.ToString() + Command.PARAMETER_SEPARATOR;
        cmd += this.chk_StackerPallet0.Checked.ToString() + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_StackerNote0.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_CustAddress0.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_MthCode0.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_MthNextMachine0.Value) + Command.PARAMETER_SEPARATOR;
        //agrassi_FI24-02-2010_3
        if (this.cmb_DischargeSide0.SelectedItemData != string.Empty)		
          cmd += Enum.Format(typeof(SyncroC.Utility.DischargeSide), Enum.Parse(typeof(SyncroC.Utility.DischargeSide), this.cmb_DischargeSide0.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        if (this.cmb_Balance0.SelectedItemData != string.Empty)		
          cmd += Enum.Format(typeof(SyncroC.Utility.Balance), Enum.Parse(typeof(SyncroC.Utility.Balance), this.cmb_Balance0.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        //agrassi_FI02-09-2010_1
        //agrassi_FI29-09-2010_1
        //if (this.state == State.Modify)
        //  cmd += modifingRow[Order.DEL_BUNDLE_0].ToString() + Command.PARAMETER_SEPARATOR;
        //else
        //  cmd += "false" + Command.PARAMETER_SEPARATOR;
        cmd += this.chk_Bundle0.Checked.ToString() + Command.PARAMETER_SEPARATOR;
        /**
         * ORDER 1
         */
        cmd += this.NotEmptyString(this.ttb_OrderNumber1.Value) + Command.PARAMETER_SEPARATOR; //9 orderNumber
        cmd += this.NotEmptyString(SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeString(this.mkt_Delivery1.ValueAsUNIVERSALDateTime)) + Command.PARAMETER_SEPARATOR; //10 delivery
        cmd += this.NotEmptyString(this.ttb_CustName1.Value) + Command.PARAMETER_SEPARATOR;	// 11 customerName      
        cmd += this.NotEmptyString(this.nud_Width1.Value) + Command.PARAMETER_SEPARATOR;		//12 SheetWidth1
        cmd += this.NotEmptyString(this.nud_Boxes1.Value) + Command.PARAMETER_SEPARATOR;		//13 Outs1
        cmd += Measures_componentToDb(ttb_Measures1.Value) + Command.PARAMETER_SEPARATOR;				//14 scoreMeasures1
        cmd += this.NotEmptyString(this.cmb_Type1.Value) + Command.PARAMETER_SEPARATOR;			//15 ScoreType1
        if (this.cmb_Positioning1.SelectedItemData != string.Empty)		//16 PositType1
          cmd += Enum.Format(typeof(SyncroC.Utility.PositType), Enum.Parse(typeof(SyncroC.Utility.PositType), this.cmb_Positioning1.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;

        if (state == State.Modify)
        {
          cmd += this.NotEmptyString(modifingRow[Order.DEL_TELETWIN_OFFSET_1].ToString()) + Command.PARAMETER_SEPARATOR;	//17 TeleTwinOffset1
          string gap = modifingRow[Order.DEL_SCORE_GAP_1].ToString();
          if ((this.intParse(gap) == this.oldScoreGap))
            cmd += this.NotEmptyString(this.nud_ScoreGap.Value) + Command.PARAMETER_SEPARATOR;	//18 ScoreGap0
          else
            cmd += this.NotEmptyString(gap) + Command.PARAMETER_SEPARATOR;	//18 ScoreGap0
        }
        else
        {
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
        }
        if (this.cmb_Level1.SelectedItemData != string.Empty)		//19 LevelName1
          cmd += Enum.Format(typeof(SyncroC.Utility.LevelName), Enum.Parse(typeof(SyncroC.Utility.LevelName), this.cmb_Level1.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.nud_length1.Value) + Command.PARAMETER_SEPARATOR;					//20 sheetLenght1
        cmd += this.NotEmptyString(this.nud_SheetsDesired1.Value) + Command.PARAMETER_SEPARATOR;	//21 segmentSheetCount1
        cmd += this.NotEmptyString(this.nud_sheetsXstack1.Value) + Command.PARAMETER_SEPARATOR;		//22 sheetsPerStack1
        cmd += this.NotEmptyString(this.nud_stacksXpallet1.Value) + Command.PARAMETER_SEPARATOR;	//23 stacksPerPallet1
        if (this.cmb_sending1.SelectedItemData != string.Empty)	//24 Sending1
          cmd += Enum.Format(typeof(SyncroC.Utility.PalletSending), Enum.Parse(typeof(SyncroC.Utility.PalletSending), this.cmb_sending1.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR; //sending
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        cmd += this.chk_CutToMark1.Checked.ToString() + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.cmb_cutToPattern1.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_DataToPrint1_1.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_DataToPrint1_2.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_DataToPrint1_3.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_DataToPrint1_4.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.chk_StackerContStack1.Checked.ToString() + Command.PARAMETER_SEPARATOR;
        cmd += this.chk_StackerPallet1.Checked.ToString() + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_StackerNote1.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_CustAddress1.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_MthCode1.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_MthNextMachine1.Value) + Command.PARAMETER_SEPARATOR;
        //agrassi_FI24-02-2010_3
        if (this.cmb_DischargeSide1.SelectedItemData != string.Empty)		
          cmd += Enum.Format(typeof(SyncroC.Utility.DischargeSide), Enum.Parse(typeof(SyncroC.Utility.DischargeSide), this.cmb_DischargeSide1.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        if (this.cmb_Balance1.SelectedItemData != string.Empty)		
          cmd += Enum.Format(typeof(SyncroC.Utility.Balance), Enum.Parse(typeof(SyncroC.Utility.Balance), this.cmb_Balance1.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        //agrassi_FI02-09-2010_1
        //agrassi_FI29-09-2010_1
        //if (this.state == State.Modify)
        //  cmd += modifingRow[Order.DEL_BUNDLE_1].ToString() + Command.PARAMETER_SEPARATOR;
        //else
        //  cmd += "false" + Command.PARAMETER_SEPARATOR;
        cmd += this.chk_Bundle1.Checked.ToString() + Command.PARAMETER_SEPARATOR;
        /**
          * ORDER 2
          */
        cmd += this.NotEmptyString(this.ttb_OrderNumber2.Value) + Command.PARAMETER_SEPARATOR; //9 orderNumber2
        cmd += this.NotEmptyString(SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeString(this.mkt_Delivery2.ValueAsUNIVERSALDateTime)) + Command.PARAMETER_SEPARATOR; //10 delivery
        cmd += this.NotEmptyString(this.ttb_CustName2.Value) + Command.PARAMETER_SEPARATOR;		//11 customerName2
        cmd += this.NotEmptyString(this.nud_Width2.Value) + Command.PARAMETER_SEPARATOR;			//12 SheetWidth2
        cmd += this.NotEmptyString(this.nud_Boxes2.Value) + Command.PARAMETER_SEPARATOR;			//13 Outs2
        cmd += Measures_componentToDb(ttb_Measures2.Value) + Command.PARAMETER_SEPARATOR;				//14 scoreMeasures2
        cmd += this.NotEmptyString(this.cmb_Type2.Value) + Command.PARAMETER_SEPARATOR;    		//15 ScoreType2
        if (this.cmb_Positioning2.SelectedItemData != string.Empty)	//16 PositType2
          cmd += Enum.Format(typeof(SyncroC.Utility.PositType), Enum.Parse(typeof(SyncroC.Utility.PositType), this.cmb_Positioning2.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;

        if (state == State.Modify)
        {
          cmd += this.NotEmptyString(modifingRow[Order.DEL_TELETWIN_OFFSET_2].ToString()) + Command.PARAMETER_SEPARATOR;	//17 TeleTwinOffset2
          string gap = modifingRow[Order.DEL_SCORE_GAP_2].ToString();
          if ((this.intParse(gap) == this.oldScoreGap))
            cmd += this.NotEmptyString(this.nud_ScoreGap.Value) + Command.PARAMETER_SEPARATOR;	//18 ScoreGap0
          else
            cmd += this.NotEmptyString(gap) + Command.PARAMETER_SEPARATOR;	//18 ScoreGap0
        }
        else
        {
          cmd += " " + Command.PARAMETER_SEPARATOR;
          cmd += " " + Command.PARAMETER_SEPARATOR;
        }
        if (this.cmb_Level2.SelectedItemData != string.Empty)	//19 LevelName2
          cmd += Enum.Format(typeof(SyncroC.Utility.LevelName), Enum.Parse(typeof(SyncroC.Utility.LevelName), this.cmb_Level2.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.nud_length2.Value) + Command.PARAMETER_SEPARATOR;					//20 sheetLenght2
        cmd += this.NotEmptyString(this.nud_SheetsDesired2.Value) + Command.PARAMETER_SEPARATOR;	//21 segmentSheetCount2
        cmd += this.NotEmptyString(this.nud_sheetsXstack2.Value) + Command.PARAMETER_SEPARATOR;		//22 sheetsPerStack2
        cmd += this.NotEmptyString(this.nud_stacksXpallet2.Value) + Command.PARAMETER_SEPARATOR;	//23 stacksPerPallet2
        if (this.cmb_sending2.SelectedItemData != string.Empty)		//24 Sending2
          cmd += Enum.Format(typeof(SyncroC.Utility.PalletSending), Enum.Parse(typeof(SyncroC.Utility.PalletSending), this.cmb_sending2.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR; //sending
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        cmd += this.chk_CutToMark2.Checked.ToString() + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.cmb_cutToPattern2.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_DataToPrint2_1.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_DataToPrint2_2.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_DataToPrint2_3.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_DataToPrint2_4.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.chk_StackerContStack2.Checked.ToString() + Command.PARAMETER_SEPARATOR;
        cmd += this.chk_StackerPallet2.Checked.ToString() + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_StackerNote2.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_CustAddress2.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_MthCode2.Value) + Command.PARAMETER_SEPARATOR;
        cmd += this.NotEmptyString(this.ttb_MthNextMachine2.Value) + Command.PARAMETER_SEPARATOR;
        //agrassi_FI24-02-2010_3
        if (this.cmb_DischargeSide2.SelectedItemData != string.Empty)		
          cmd += Enum.Format(typeof(SyncroC.Utility.DischargeSide), Enum.Parse(typeof(SyncroC.Utility.DischargeSide), this.cmb_DischargeSide2.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        if (this.cmb_Balance2.SelectedItemData != string.Empty)		
          cmd += Enum.Format(typeof(SyncroC.Utility.Balance), Enum.Parse(typeof(SyncroC.Utility.Balance), this.cmb_Balance2.SelectedItemData), "d") + Command.PARAMETER_SEPARATOR;
        else
          cmd += " " + Command.PARAMETER_SEPARATOR;
        //agrassi_FI02-09-2010_1
        //agrassi_FI29-09-2010_1
        //if (this.state == State.Modify)
        //  cmd += modifingRow[Order.DEL_BUNDLE_2].ToString() + Command.PARAMETER_SEPARATOR;
        //else
        //  cmd += "false" + Command.PARAMETER_SEPARATOR;
        cmd += this.chk_Bundle2.Checked.ToString() + Command.PARAMETER_SEPARATOR;

        cmd += ")";

        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        Logger.LogError("Order: ", cmd);
        cm.SendTcp(cm.SYNCROS_DB_MANAGER, cmd);
        orderAddRequested = true;
        //agrassi_FI01-06-2010_1
        this.copyAndModify = false;
      }
    }

    /// <summary>
    /// Modify.
    /// </summary>
    protected override void CtrlF5()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      if (Frm_OrdersInsSubGroup.indexOrderToShowDRY == 0)
      {
        PageManager.getPageManager()._SetMessage("§SYCC_01", true);
        return;
      }
      if (this.state != State.Modify)
        SetPageState(State.Modify);
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
    private void CleanSuggestionFields(object sender, System.EventArgs e)
    {
      CleanSuggestionFields();
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
    /// Clean the value of every component of the page.
    /// </summary>
    private void cleanAllFields(bool cleanLists)
    {
      resetAllErrors();
      CleanSuggestionFields();
      //DEBUG
      this.lbl_DebugSetupID.Text = this.lbl_DebugSetupID.Text = String.Empty;
      this.lbl_DebugSetupModId.Text = this.lbl_DebugSetupModId.Text = String.Empty;
      this.lbl_DebugPalletPrint0.Text = this.lbl_DebugPalletPrint0.Text.Substring(0, this.lbl_DebugPalletPrint0.Text.IndexOf('>') + 1) + " " + String.Empty;
      this.lbl_DebugPalletPrint1.Text = this.lbl_DebugPalletPrint1.Text.Substring(0, this.lbl_DebugPalletPrint1.Text.IndexOf('>') + 1) + " " + String.Empty;
      this.lbl_DebugPalletPrint2.Text = this.lbl_DebugPalletPrint2.Text.Substring(0, this.lbl_DebugPalletPrint2.Text.IndexOf('>') + 1) + " " + String.Empty;
      this.lbl_DebugPastPrint0.Text = this.lbl_DebugPastPrint0.Text.Substring(0, this.lbl_DebugPastPrint0.Text.IndexOf('>') + 1) + " " + String.Empty;
      this.lbl_DebugPastPrint1.Text = this.lbl_DebugPastPrint1.Text.Substring(0, this.lbl_DebugPastPrint1.Text.IndexOf('>') + 1) + " " + String.Empty;
      this.lbl_DebugPastPrint2.Text = this.lbl_DebugPastPrint2.Text.Substring(0, this.lbl_DebugPastPrint2.Text.IndexOf('>') + 1) + " " + String.Empty;

      //HEADER
      ttb_ProgramNumber.Value = String.Empty;
      ttb_SetUpSequence.Value = String.Empty;
      if (cleanLists)
      {
        cmb_Flute.UnSelectItem();
        cmb_Quality.UnSelectItem();
        cmb_TrimType.UnSelectItem();
      }
      nud_WebWidth.Value = String.Empty;
      nud_Trim.Value = String.Empty;
      nud_ScoreGap.Value = String.Empty;
      nud_SetupLength.Value = string.Empty;

      //ORDER 0
      ttl_ord0.ForeColor = Color.WhiteSmoke;
      ttb_OrderNumber0.Value = String.Empty;
      ttb_CustName0.Value = String.Empty;
      cmb_sending0.UnSelectItem();
      mkt_Delivery0.Value = String.Empty;
      nud_Width0.Value = String.Empty;
      nud_length0.Value = String.Empty;
      nud_Boxes0.Value = String.Empty;
      nud_SheetsDesired0.Value = String.Empty;
      nud_sheetsXstack0.Value = String.Empty;
      nud_stacksXpallet0.Value = String.Empty;
      cmb_Level0.UnSelectItem();
      cmb_Positioning0.UnSelectItem();
      cmb_Type0.UnSelectItem();
      ttb_Measures0.Value = String.Empty;
      this.chk_CutToMark0.Checked = false;
      if (cleanLists)
        this.cmb_cutToPattern0.UnSelectItem();
      this.ttb_DataToPrint0_1.Value = String.Empty;
      this.ttb_DataToPrint0_2.Value = String.Empty;
      this.ttb_DataToPrint0_3.Value = String.Empty;
      this.ttb_DataToPrint0_4.Value = String.Empty;
      this.chk_StackerContStack0.Checked = false;
      this.chk_StackerPallet0.Checked = false;
      this.ttb_StackerNote0.Value = String.Empty;
      this.ttb_CustAddress0.Value = String.Empty;
      this.ttb_MthCode0.Value = String.Empty;
      this.ttb_MthNextMachine0.Value = String.Empty;
      //agrassi_FI24-02-2010_3
      this.cmb_Balance0.UnSelectItem();
      this.cmb_DischargeSide0.UnSelectItem();
      //agrassi_FI29-09-2010_1
      this.chk_Bundle0.Checked = false;

      //ORDER 1
      ttl_ord1.ForeColor = Color.WhiteSmoke;
      ttb_OrderNumber1.Value = String.Empty;
      ttb_CustName1.Value = String.Empty;
      cmb_sending1.UnSelectItem();
      mkt_Delivery1.Value = String.Empty;
      nud_Width1.Value = String.Empty;
      nud_length1.Value = String.Empty;
      nud_length1.SetError(String.Empty);
      nud_Boxes1.Value = String.Empty;
      nud_SheetsDesired1.Value = String.Empty;
      nud_sheetsXstack1.Value = String.Empty;
      nud_stacksXpallet1.Value = String.Empty;
      cmb_Level1.UnSelectItem();
      cmb_Positioning1.UnSelectItem();
      cmb_Type1.UnSelectItem();
      ttb_Measures1.Value = String.Empty;
      this.chk_CutToMark1.Checked = false;
      if (cleanLists)
        this.cmb_cutToPattern1.UnSelectItem();
      this.ttb_DataToPrint1_1.Value = String.Empty;
      this.ttb_DataToPrint1_2.Value = String.Empty;
      this.ttb_DataToPrint1_3.Value = String.Empty;
      this.ttb_DataToPrint1_4.Value = String.Empty;
      this.chk_StackerContStack1.Checked = false;
      this.chk_StackerPallet1.Checked = false;
      this.ttb_StackerNote1.Value = String.Empty;
      this.ttb_CustAddress1.Value = String.Empty;
      this.ttb_MthCode1.Value = String.Empty;
      this.ttb_MthNextMachine1.Value = String.Empty;
      //agrassi_FI24-02-2010_3
      this.cmb_Balance1.UnSelectItem();
      this.cmb_DischargeSide1.UnSelectItem();
      //agrassi_FI29-09-2010_1
      this.chk_Bundle1.Checked = false;

      //ORDER 2
      ttl_ord2.ForeColor = Color.WhiteSmoke;
      ttb_OrderNumber2.Value = String.Empty;
      ttb_CustName2.Value = String.Empty;
      cmb_sending2.UnSelectItem();
      mkt_Delivery2.Value = String.Empty;
      nud_Width2.Value = String.Empty;
      nud_length2.Value = String.Empty;
      nud_Boxes2.Value = String.Empty;
      nud_SheetsDesired2.Value = String.Empty;
      nud_sheetsXstack2.Value = String.Empty;
      nud_stacksXpallet2.Value = String.Empty;
      cmb_Level2.UnSelectItem();
      cmb_Positioning2.UnSelectItem();
      cmb_Type2.UnSelectItem();
      ttb_Measures2.Value = String.Empty;
      this.chk_CutToMark2.Checked = false;
      if (cleanLists)
        this.cmb_cutToPattern2.UnSelectItem();
      this.ttb_DataToPrint2_1.Value = String.Empty;
      this.ttb_DataToPrint2_2.Value = String.Empty;
      this.ttb_DataToPrint2_3.Value = String.Empty;
      this.ttb_DataToPrint2_4.Value = String.Empty;
      this.chk_StackerContStack2.Checked = false;
      this.chk_StackerPallet2.Checked = false;
      this.ttb_StackerNote2.Value = String.Empty;
      this.ttb_CustAddress2.Value = String.Empty;
      this.ttb_MthCode2.Value = String.Empty;
      this.ttb_MthNextMachine2.Value = String.Empty;
      //agrassi_FI24-02-2010_3
      this.cmb_Balance2.UnSelectItem();
      this.cmb_DischargeSide2.UnSelectItem();
      //agrassi_FI29-09-2010_1
      this.chk_Bundle2.Checked = false;
    }

    /// <summary>
    /// Reset all the error providers
    /// </summary>
    private void resetAllErrors()
    {
      //HEADER
      ttb_ProgramNumber.SetError(String.Empty);
      ttb_SetUpSequence.SetError(String.Empty);
      nud_WebWidth.SetError(String.Empty);
      nud_ScoreGap.SetError(String.Empty);
      cmb_Flute.SetError(String.Empty);
      cmb_Quality.SetError(String.Empty);
      cmb_TrimType.SetError(String.Empty);
      nud_Trim.SetError(String.Empty);
      nud_SetupLength.SetError(string.Empty);
      //ORDER 0
      ttb_OrderNumber0.SetError(String.Empty);
      ttb_CustName0.SetError(String.Empty);
      mkt_Delivery0.SetError(String.Empty);
      nud_Width0.SetError(String.Empty);
      nud_length0.SetError(String.Empty);
      nud_Boxes0.SetError(String.Empty);
      nud_SheetsDesired0.SetError(String.Empty);
      nud_sheetsXstack0.SetError(String.Empty);
      nud_stacksXpallet0.SetError(String.Empty);
      ttb_Measures0.SetError(String.Empty);
      //ORDER 1
      ttb_OrderNumber1.SetError(String.Empty);
      ttb_CustName1.SetError(String.Empty);
      mkt_Delivery1.SetError(String.Empty);
      nud_Width1.SetError(String.Empty);
      nud_length1.SetError(String.Empty);
      nud_Boxes1.SetError(String.Empty);
      nud_SheetsDesired1.SetError(String.Empty);
      nud_sheetsXstack1.SetError(String.Empty);
      nud_stacksXpallet1.SetError(String.Empty);
      ttb_Measures1.SetError(String.Empty);
      //ORDER 2
      ttb_OrderNumber2.SetError(String.Empty);
      ttb_CustName2.SetError(String.Empty);
      mkt_Delivery2.SetError(String.Empty);
      nud_Width2.SetError(String.Empty);
      nud_length2.SetError(String.Empty);
      nud_Boxes2.SetError(String.Empty);
      nud_SheetsDesired2.SetError(String.Empty);
      nud_sheetsXstack2.SetError(String.Empty);
      nud_stacksXpallet2.SetError(String.Empty);
      ttb_Measures2.SetError(String.Empty);
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
        nud_ScoreGap.ReadOnly = cmb_TrimType.ReadOnly =
        //order0
        ttb_OrderNumber0.ReadOnly = ttb_CustName0.ReadOnly =
        cmb_sending0.ReadOnly = mkt_Delivery0.ReadOnly =
        nud_Width0.ReadOnly = nud_length0.ReadOnly =
        nud_Boxes0.ReadOnly = nud_SheetsDesired0.ReadOnly =
        nud_sheetsXstack0.ReadOnly = nud_stacksXpallet0.ReadOnly =
        cmb_Level0.ReadOnly = cmb_Positioning0.ReadOnly =
        cmb_Type0.ReadOnly = ttb_Measures0.ReadOnly =
        this.ttb_DataToPrint0_1.ReadOnly =
        this.ttb_DataToPrint0_2.ReadOnly =
        this.ttb_DataToPrint0_3.ReadOnly =
        this.ttb_DataToPrint0_4.ReadOnly =
        this.chk_StackerContStack0.ReadOnly =
        this.chk_StackerPallet0.ReadOnly =
        this.ttb_StackerNote0.ReadOnly =
        this.ttb_CustAddress0.ReadOnly =
        this.ttb_MthCode0.ReadOnly =
        this.ttb_MthNextMachine0.ReadOnly =
        //agrassi_FI24-02-2010_3
        this.cmb_Balance0.ReadOnly=
        this.cmb_DischargeSide0.ReadOnly =
        //agrassi_FI29-09-2010_1
        this.chk_Bundle0.ReadOnly =
        //order1
        ttb_OrderNumber1.ReadOnly = ttb_CustName1.ReadOnly =
        cmb_sending1.ReadOnly = mkt_Delivery1.ReadOnly =
        nud_Width1.ReadOnly = nud_length1.ReadOnly =
        nud_Boxes1.ReadOnly = nud_SheetsDesired1.ReadOnly =
        nud_sheetsXstack1.ReadOnly = nud_stacksXpallet1.ReadOnly =
        cmb_Level1.ReadOnly = cmb_Positioning1.ReadOnly =
        cmb_Type1.ReadOnly = ttb_Measures1.ReadOnly =
        this.ttb_DataToPrint1_1.ReadOnly =
        this.ttb_DataToPrint1_2.ReadOnly =
        this.ttb_DataToPrint1_3.ReadOnly =
        this.ttb_DataToPrint1_4.ReadOnly =
        this.chk_StackerContStack1.ReadOnly =
        this.chk_StackerPallet1.ReadOnly =
        this.ttb_StackerNote1.ReadOnly =
        this.ttb_CustAddress1.ReadOnly =
        this.ttb_MthCode1.ReadOnly =
        this.ttb_MthNextMachine1.ReadOnly =
        //agrassi_FI24-02-2010_3
        this.cmb_Balance1.ReadOnly =
        this.cmb_DischargeSide1.ReadOnly =
        //agrassi_FI29-09-2010_1
        this.chk_Bundle1.ReadOnly =
        //order2
        ttb_OrderNumber2.ReadOnly = ttb_CustName2.ReadOnly =
        cmb_sending2.ReadOnly = mkt_Delivery2.ReadOnly =
        nud_Width2.ReadOnly = nud_length2.ReadOnly =
        nud_Boxes2.ReadOnly = nud_SheetsDesired2.ReadOnly =
        nud_sheetsXstack2.ReadOnly = nud_stacksXpallet2.ReadOnly =
        cmb_Level2.ReadOnly = cmb_Positioning2.ReadOnly =
        cmb_Type2.ReadOnly = ttb_Measures2.ReadOnly =
        this.ttb_DataToPrint2_1.ReadOnly =
        this.ttb_DataToPrint2_2.ReadOnly =
        this.ttb_DataToPrint2_3.ReadOnly =
        this.ttb_DataToPrint2_4.ReadOnly =
        this.chk_StackerContStack2.ReadOnly =
        this.chk_StackerPallet2.ReadOnly =
        this.ttb_StackerNote2.ReadOnly =
        this.ttb_CustAddress2.ReadOnly =
        this.ttb_MthCode2.ReadOnly =
        this.ttb_MthNextMachine2.ReadOnly =
        //agrassi_FI24-02-2010_3
        this.cmb_Balance2.ReadOnly =
        this.cmb_DischargeSide2.ReadOnly =
        //agrassi_FI29-09-2010_1
        this.chk_Bundle2.ReadOnly =

        //value	
        isReadOnly;

      //TabStop
      //header
      ttb_ProgramNumber.TabStop = ttb_SetUpSequence.TabStop =
        cmb_Flute.TabStop = cmb_Quality.TabStop =
        nud_WebWidth.TabStop =
        nud_ScoreGap.TabStop = cmb_TrimType.TabStop =
        //order0
        ttb_OrderNumber0.TabStop = ttb_CustName0.TabStop =
        cmb_sending0.TabStop = mkt_Delivery0.TabStop =
        nud_Width0.TabStop = nud_length0.TabStop =
        nud_Boxes0.TabStop = nud_SheetsDesired0.TabStop =
        nud_sheetsXstack0.TabStop = nud_stacksXpallet0.TabStop =
        cmb_Level0.TabStop = cmb_Positioning0.TabStop =
        cmb_Type0.TabStop = ttb_Measures0.TabStop =
        this.ttb_DataToPrint0_1.TabStop =
        this.ttb_DataToPrint0_2.TabStop =
        this.ttb_DataToPrint0_3.TabStop =
        this.ttb_DataToPrint0_4.TabStop =
        this.chk_StackerContStack0.TabStop =
        this.chk_StackerPallet0.TabStop =
        this.ttb_StackerNote0.TabStop =
        this.ttb_CustAddress0.TabStop =
        this.ttb_MthCode0.TabStop =
        this.ttb_MthNextMachine0.TabStop =
        //agrassi_FI24-02-2010_3
        this.cmb_Balance0.TabStop =
        this.cmb_DischargeSide0.TabStop =
        //order1
        ttb_OrderNumber1.TabStop = ttb_CustName1.TabStop =
        cmb_sending1.TabStop = mkt_Delivery1.TabStop =
        nud_Width1.TabStop = nud_length1.TabStop =
        nud_Boxes1.TabStop = nud_SheetsDesired1.TabStop =
        nud_sheetsXstack1.TabStop = nud_stacksXpallet1.TabStop =
        cmb_Level1.TabStop = cmb_Positioning1.TabStop =
        cmb_Type1.TabStop = ttb_Measures1.TabStop =
        this.ttb_DataToPrint1_1.TabStop =
        this.ttb_DataToPrint1_2.TabStop =
        this.ttb_DataToPrint1_3.TabStop =
        this.ttb_DataToPrint1_4.TabStop =
        this.chk_StackerContStack1.TabStop =
        this.chk_StackerPallet1.TabStop =
        this.ttb_StackerNote1.TabStop =
        this.ttb_CustAddress1.TabStop =
        this.ttb_MthCode1.TabStop =
        this.ttb_MthNextMachine1.TabStop =
        //agrassi_FI24-02-2010_3
        this.cmb_Balance1.TabStop =
        this.cmb_DischargeSide1.TabStop =
        //order2
        ttb_OrderNumber2.TabStop = ttb_CustName2.TabStop =
        cmb_sending2.TabStop = mkt_Delivery2.TabStop =
        nud_Width2.TabStop = nud_length2.TabStop =
        nud_Boxes2.TabStop = nud_SheetsDesired2.TabStop =
        nud_sheetsXstack2.TabStop = nud_stacksXpallet2.TabStop =
        cmb_Level2.TabStop = cmb_Positioning2.TabStop =
        cmb_Type2.TabStop = ttb_Measures2.TabStop =
        this.ttb_DataToPrint2_1.TabStop =
        this.ttb_DataToPrint2_2.TabStop =
        this.ttb_DataToPrint2_3.TabStop =
        this.ttb_DataToPrint2_4.TabStop =
        this.chk_StackerContStack2.TabStop =
        this.chk_StackerPallet2.TabStop =
        this.ttb_StackerNote2.TabStop =
        this.ttb_CustAddress2.TabStop =
        this.ttb_MthCode2.TabStop =
        this.ttb_MthNextMachine2.TabStop =
        //agrassi_FI24-02-2010_3
        this.cmb_Balance2.TabStop =
        this.cmb_DischargeSide2.TabStop =
        //value
        !isReadOnly;

      //RFAZZI_FI.Changed the above value for level
      //if (this.knife.CutToMark)
      if (this.knife.KnifeLevelSettings[0].CutToMark || this.knife.KnifeLevelSettings[1].CutToMark || this.knife.KnifeLevelSettings[2].CutToMark)
      {
        this.chk_CutToMark0.ReadOnly = this.chk_CutToMark1.ReadOnly = this.chk_CutToMark2.ReadOnly = isReadOnly;
        this.chk_CutToMark1.TabStop = this.chk_CutToMark0.TabStop = this.chk_CutToMark2.TabStop = !isReadOnly;
      }
      else
      {
        this.chk_CutToMark0.ReadOnly = this.chk_CutToMark1.ReadOnly = this.chk_CutToMark2.ReadOnly = true;
        this.chk_CutToMark1.TabStop = this.chk_CutToMark0.TabStop = this.chk_CutToMark2.TabStop = false;
      }

      //RFAZZI_FI.Changed the above value for level
      //if (this.knife.CutToPattern)
      if (this.knife.KnifeLevelSettings[0].CutToPattern || this.knife.KnifeLevelSettings[1].CutToPattern || this.knife.KnifeLevelSettings[2].CutToPattern)
      {
        this.cmb_cutToPattern0.ReadOnly = this.cmb_cutToPattern1.ReadOnly = this.cmb_cutToPattern2.ReadOnly = isReadOnly;
        this.cmb_cutToPattern1.TabStop = this.cmb_cutToPattern0.TabStop = this.cmb_cutToPattern2.TabStop = !isReadOnly;
      }
      else
      {
        this.cmb_cutToPattern0.ReadOnly = this.cmb_cutToPattern1.ReadOnly = this.cmb_cutToPattern2.ReadOnly = true;
        this.cmb_cutToPattern1.TabStop = this.cmb_cutToPattern0.TabStop = this.cmb_cutToPattern2.TabStop = false;
      }
    }

    private void cmb_Flute_SelectedItemChanged(object sender, System.EventArgs e)
    {
      if (cmb_Flute.SelectedIndex >= 0)
        setListQualityItems(0, ((TitledList)sender).SelectedItem, true);
    }

    private void cmb_Positioning0_SelectedItemChanged(object sender, System.EventArgs e)
    {
      setListScorerItems(cmb_Type0, ((TitledList)sender).SelectedItemData, -1, true);
    }

    private void cmb_Positioning1_SelectedItemChanged(object sender, System.EventArgs e)
    {
      setListScorerItems(cmb_Type1, ((TitledList)sender).SelectedItemData, -1, true);
    }

    private void cmb_Positioning2_SelectedItemChanged(object sender, System.EventArgs e)
    {
      setListScorerItems(cmb_Type2, ((TitledList)sender).SelectedItemData, -1, true);
    }

    //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
    private bool CheckSheetsPerStack()
    {
      //Is Not Important Check Sheets Per Stack If Other Fields Are Not Correctly Sets, So.
      //return True: Sheets Per Stack Ore OK or Others Important Fields Are Not Correctly Valorized
      //return False: Sheets Per Stack Ore Not OK For Valorized Order

      bool Order0OK = true;
      bool Order1OK = true;
      bool Order2OK = true;

      if (cmb_Quality.SelectedIndex == -1 || cmb_Quality.SelectedItem.Equals(string.Empty))
      { //no quality has been selected yet
        Logger.LogError(this, "CheckSheetsPerStack - Quality not Yet Selected");
        return true; // true cause is not necessary check the sheetsperstack if this is not valorized
      }
      else
      { //quality already selected 
        //DataRow row = quality.Rows.Find(cmb_Quality.SelectedItem);
        DataRow row = DBManager.getDBManager().quality.getRow(this.cmb_Quality.SelectedItem);
        if (row == null)
        {
          Logger.LogError(this, "CheckSheetsPerStack - Cannot find selected quality \"" + cmb_Quality.SelectedItem + "\" into Quality database");
          return true; // true cause is not necessary check the sheetsperstack if this is not valorized
        }
        else
        {
          UpdateMaxHeightControlValue(3); //Ensure I Have All Data UpDated In Order To Compare 

          int thickness = 0;
          try
          {
            thickness = Convert.ToInt32(row[Quality.THICKNESS_UDdmm].ToString());
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "CheckSheetsPerStacks, value " + row[Quality.THICKNESS_UDdmm].ToString(), ex);
          }
          if (thickness == 0)
          {
            Logger.LogError(this, "CheckSheetsPerStack - Quality \"" + cmb_Quality.SelectedItem + "\" thickness is 0");
          }
          else
          {
            //
            //format 0
            //
            if (cmb_Level0.SelectedIndex == -1 || cmb_Level0.SelectedItemData.Equals(string.Empty))
            { //no level has been selected for format 0 yet 
              Logger.LogError(this, "CheckSheetsPerStack - No Selected Item For cmb_Level0");
            }
            else if (intParse(this.nud_Width0.Value) <= 0)
            { //no level has been selected for format 0 yet 
              Logger.LogError(this, "CheckSheetsPerStack - Value Of nud_Width0 <= 0");
            }
            else if (intParse(this.nud_Boxes0.Value) <= 0)
            { //no level has been selected for format 0 yet 
              Logger.LogError(this, "CheckSheetsPerStack - Value Of nud_Boxes0 <= 0");
            }
            else if (intParse(this.nud_length0.Value) <= 0)
            { //no level has been selected for format 0 yet 
              Logger.LogError(this, "CheckSheetsPerStack - Value Of nud_length0 <= 0");
            }
            else
            {
              bool NoProblem = true;
              SyncroC.DataBase.LineConfig.Stacker stacker = null;

              if (cmb_Level0.SelectedItemData.Equals(LevelName.Lower.ToString()))
              {
                stacker = dbm.lineConfig.Stacker1Config;
              }
              else if (cmb_Level0.SelectedItemData.Equals(LevelName.Middle.ToString()))
              {
                stacker = dbm.lineConfig.Stacker2Config;
              }
              else if (cmb_Level0.SelectedItemData.Equals(LevelName.Upper.ToString()))
              {
                switch (knife.LevelCount)
                {
                  case 1:
                    Logger.LogError(this, "CheckSheetsPerStack (format 0) - Invalid level selected, cannot select Higher level in a single knife dry end");
                    NoProblem = false;
                    break;
                  case 2:
                    stacker = dbm.lineConfig.Stacker2Config;
                    break;
                  case 3:
                    stacker = dbm.lineConfig.Stacker3Config;
                    break;
                }
              }
              else
              {
                Logger.LogError(this, "CheckSheetsPerStack (format 0) - Error identifing selected level " + cmb_Level0.SelectedItemData.ToString());
                NoProblem = false;
              }

              if (NoProblem)
              {
                //agrassi_FI02-09-2010_1
                //agrassi_FI26-11-2010_1
                //DataRow rowSetup = DBManager.getDBManager().order.order.Tables[0].Rows[indexOrderToShowDRY];
                int maxLimit = Convert.ToInt32(Math.Floor((double)
                  //agrassi_FI02-09-2010_1
                  //((stacker.MaxStackHeight * 10) / thickness)));
                  //agrassi_FI26-11-2010_1
                  //(((rowSetup[Order.DEL_BUNDLE_0].ToString().Equals(true.ToString()) ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));
                  (((this.chk_Bundle0.Checked ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));

                if (nud_sheetsXstack0.Value == "")
                  nud_sheetsXstack0.Value = "0";

                string error = string.Empty;
                if (Convert.ToInt32(nud_sheetsXstack0.Value) <= 0)
                {
                  error = dbm.message.getTranslatedMessage("§SYCC_87#");
                  PageManager.getPageManager()._SetMessage(error);
                  Order0OK = false;
                }

                if (Convert.ToInt32(nud_sheetsXstack0.Value) > maxLimit)
                {
                  error = dbm.message.getTranslatedMessage("§SYCC_80#" + maxLimit.ToString());
                  PageManager.getPageManager()._SetMessage(error);
                  Order0OK = false;
                }

                nud_sheetsXstack0.SetError(error);
              }
            }

            //
            //format 1
            //
            if (cmb_Level1.SelectedIndex == -1 || cmb_Level1.SelectedItemData.Equals(string.Empty))
            { //no level has been selected for format 1 yet 
              Logger.LogError(this, "CheckSheetsPerStack - No Selected Item For cmb_Level1");
            }
            else if (intParse(this.nud_Width1.Value) <= 0)
            { //no level has been selected for format 0 yet 
              Logger.LogError(this, "CheckSheetsPerStack - Value Of nud_Width1 <= 0");
            }
            else if (intParse(this.nud_Boxes1.Value) <= 0)
            { //no level has been selected for format 0 yet 
              Logger.LogError(this, "CheckSheetsPerStack - Value Of nud_Boxes1 <= 0");
            }
            else if (intParse(this.nud_length1.Value) <= 0)
            { //no level has been selected for format 0 yet 
              Logger.LogError(this, "CheckSheetsPerStack - Value Of nud_length1 <= 0");
            }
            else
            {
              bool NoProblem = true;
              SyncroC.DataBase.LineConfig.Stacker stacker = null;

              if (cmb_Level1.SelectedItemData.Equals(LevelName.Lower.ToString()))
              {
                stacker = dbm.lineConfig.Stacker1Config;
              }
              else if (cmb_Level1.SelectedItemData.Equals(LevelName.Middle.ToString()))
              {
                stacker = dbm.lineConfig.Stacker2Config;
              }
              else if (cmb_Level1.SelectedItemData.Equals(LevelName.Upper.ToString()))
              {
                switch (knife.LevelCount)
                {
                  case 1:
                    Logger.LogError(this, "CheckSheetsPerStack (format 1) - Invalid level selected, cannot select Higher level in a single knife dry end");
                    NoProblem = false;
                    break;
                  case 2:
                    stacker = dbm.lineConfig.Stacker2Config;
                    break;
                  case 3:
                    stacker = dbm.lineConfig.Stacker3Config;
                    break;
                }
              }
              else
              {
                Logger.LogError(this, "CheckSheetsPerStack (format 1) - Error identifing selected level " + cmb_Level1.SelectedItemData.ToString());
                NoProblem = false;
              }

              if (NoProblem)
              {
                //agrassi_FI02-09-2010_1
                //agrassi_FI26-11-2010_1
                //DataRow rowSetup = DBManager.getDBManager().order.order.Tables[0].Rows[indexOrderToShowDRY];
                int maxLimit = Convert.ToInt32(Math.Floor((double)
                  //agrassi_FI02-09-2010_1
                  //((stacker.MaxStackHeight * 10) / thickness)));
                  //agrassi_FI26-11-2010_1
                  //(((rowSetup[Order.DEL_BUNDLE_1].ToString().Equals(true.ToString()) ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));
                  (((this.chk_Bundle1.Checked ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));

                if (nud_sheetsXstack1.Value == "")
                  nud_sheetsXstack1.Value = "0";

                string error = string.Empty;
                if (Convert.ToInt32(nud_sheetsXstack1.Value) <= 0)
                {
                  error = dbm.message.getTranslatedMessage("§SYCC_87#");
                  PageManager.getPageManager()._SetMessage(error);
                  Order1OK = false;
                }

                if (Convert.ToInt32(nud_sheetsXstack1.Value) > maxLimit)
                {
                  error = dbm.message.getTranslatedMessage("§SYCC_80#" + maxLimit.ToString());
                  PageManager.getPageManager()._SetMessage(error);
                  Order1OK = false;
                }

                nud_sheetsXstack1.SetError(error);
              }
            }
            //
            //format 2
            //
            if (cmb_Level2.SelectedIndex == -1 || cmb_Level2.SelectedItemData.Equals(string.Empty))
            { //no level has been selected for format 2 yet 
              Logger.LogError(this, "CheckSheetsPerStack - No Selected Item For cmb_Level2");
            }
            else if (intParse(this.nud_Width2.Value) <= 0)
            { //no level has been selected for format 0 yet 
              Logger.LogError(this, "CheckSheetsPerStack - Value Of nud_Width2 <= 0");
            }
            else if (intParse(this.nud_Boxes2.Value) <= 0)
            { //no level has been selected for format 0 yet 
              Logger.LogError(this, "CheckSheetsPerStack - Value Of nud_Boxes2 <= 0");
            }
            else if (intParse(this.nud_length2.Value) <= 0)
            { //no level has been selected for format 0 yet 
              Logger.LogError(this, "CheckSheetsPerStack - Value Of nud_length2 <= 0");
            }
            else
            {
              bool NoProblem = true;
              SyncroC.DataBase.LineConfig.Stacker stacker = null;

              if (cmb_Level2.SelectedItemData.Equals(LevelName.Lower.ToString()))
              {
                stacker = dbm.lineConfig.Stacker1Config;
              }
              else if (cmb_Level2.SelectedItemData.Equals(LevelName.Middle.ToString()))
              {
                stacker = dbm.lineConfig.Stacker2Config;
              }
              else if (cmb_Level2.SelectedItemData.Equals(LevelName.Upper.ToString()))
              {
                switch (knife.LevelCount)
                {
                  case 1:
                    Logger.LogError(this, "CheckSheetsPerStack (format 2) - Invalid level selected, cannot select Higher level in a single knife dry end");
                    NoProblem = false;
                    break;
                  case 2:
                    stacker = dbm.lineConfig.Stacker2Config;
                    break;
                  case 3:
                    stacker = dbm.lineConfig.Stacker3Config;
                    break;
                }
              }
              else
              {
                Logger.LogError(this, "CheckSheetsPerStack (format 2) - Error identifing selected level " + cmb_Level2.SelectedItemData.ToString());
                NoProblem = false;
              }

              if (NoProblem)
              {
                //agrassi_FI02-09-2010_1
                //agrassi_FI26-11-2010_1
                //DataRow rowSetup = DBManager.getDBManager().order.order.Tables[0].Rows[indexOrderToShowDRY];
                int maxLimit = Convert.ToInt32(Math.Floor((double)
                  //agrassi_FI02-09-2010_1
                  //((stacker.MaxStackHeight * 10) / thickness)));
                  //agrassi_FI26-11-2010_1
                  //(((rowSetup[Order.DEL_BUNDLE_2].ToString().Equals(true.ToString()) ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));
                  (((this.chk_Bundle2.Checked ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));

                if (nud_sheetsXstack2.Value == "")
                  nud_sheetsXstack2.Value = "0";

                string error = string.Empty;
                if (Convert.ToInt32(nud_sheetsXstack2.Value) <= 0)
                {
                  error = dbm.message.getTranslatedMessage("§SYCC_87#");
                  PageManager.getPageManager()._SetMessage(error);
                  Order2OK = false;
                }
                if (Convert.ToInt32(nud_sheetsXstack2.Value) > maxLimit)
                {
                  error = dbm.message.getTranslatedMessage("§SYCC_80#" + maxLimit.ToString());
                  PageManager.getPageManager()._SetMessage(error);
                  Order2OK = false;
                }

                nud_sheetsXstack2.SetError(error);
              }
            }
          }
        }
      }
      return (Order0OK && Order1OK && Order2OK);
    }


    //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
    // This function is necessary to update limitSheetsXStackFormat0/1/2 and maxSheetsXStackFormat0/1/2
    // else are vaolirzed only after is called cmb_Quality_OR_cmb_Level_OR_cmb_Sending_SelectedItemChanged
    private void UpdateMaxHeightControlValue(int num)
    {
      //3 = ALL Order
      if (num < 0 || num > 3)
      {
        Logger.LogError(this, "UpdateMaxHeightControlValue - Wrong Index Parameter (" + num.ToString() + ")+ Is Not Between 0 and 3");
        return;
      }
      if (cmb_Quality.SelectedIndex == -1 || cmb_Quality.SelectedItem.Equals(string.Empty))
      { //no quality has been selected yet
        Logger.LogError(this, "UpdateMaxHeightControlValue - Quality not Yet Selected");
        return;
      }
      else
      { //quality already selected 
        DataRow row = DBManager.getDBManager().quality.getRow(this.cmb_Quality.SelectedItem);
        //DataRow row = quality.Rows.Find(cmb_Quality.SelectedItem);
        if (row == null)
        {
          Logger.LogError(this, "UpdateMaxHeightControlValue - Cannot find selected quality \"" + cmb_Quality.SelectedItem + "\" into Quality database");
          return;
        }
        else
        {
          int thickness = 0;
          try
          {
            thickness = Convert.ToInt32(row[Quality.THICKNESS_UDdmm].ToString());
          }
          catch (Exception ex)
          {
            Logger.LogError(this, " - Error converting thickness, value " + row[Quality.THICKNESS_UDdmm].ToString(), ex);
          }
          if (thickness == 0)
          {
            Logger.LogError(this, "UpdateMaxHeightControlValue - Quality \"" + cmb_Quality.SelectedItem + "\" thickness is 0");
          }
          else
          {	//
            //format 0
            //
            if (num == 0 || num == 3)
            {
              limitSheetsXStackFormat0 = 0;
              maxSheetsXStackFormat0 = 0;

              if (cmb_Level0.SelectedIndex == -1 || cmb_Level0.SelectedItemData.Equals(string.Empty))
              { //no level has been selected for format 0 yet 
                Logger.LogError(this, "UpdateMaxHeightControlValue - No Selected Item For cmb_Level0");
                if (num == 0)
                  return;
              }
              else
              { //level for format 0 already selected
                SyncroC.DataBase.LineConfig.Stacker stacker = null;
                if (cmb_Level0.SelectedItemData.Equals(LevelName.Lower.ToString()))
                {
                  stacker = dbm.lineConfig.Stacker1Config;
                }
                else if (cmb_Level0.SelectedItemData.Equals(LevelName.Middle.ToString()))
                {
                  stacker = dbm.lineConfig.Stacker2Config;
                }
                else if (cmb_Level0.SelectedItemData.Equals(LevelName.Upper.ToString()))
                {
                  switch (knife.LevelCount)
                  {
                    case 1:
                      Logger.LogError(this, "UpdateMaxHeightControlValue (format 0) - Invalid level selected, cannot select Higher level in a single knife dry end");
                      break;
                    case 2:
                      stacker = dbm.lineConfig.Stacker2Config;
                      break;
                    case 3:
                      stacker = dbm.lineConfig.Stacker3Config;
                      break;
                  }
                }
                else
                {
                  Logger.LogError(this, "UpdateMaxHeightControlValue (format0) - Error identifing selected level " + cmb_Level0.SelectedItemData.ToString());
                  if (num == 0)
                    return;
                }
                //agrassi_FI02-09-2010_1
                //agrassi_FI26-11-2010_1
                //DataRow rowSetup = DBManager.getDBManager().order.order.Tables[0].Rows[indexOrderToShowDRY];
                limitSheetsXStackFormat0 = Convert.ToInt32(Math.Floor((double)
                  //agrassi_FI02-09-2010_1
                  //((stacker.MaxStackHeight * 10) / thickness)));
                  //agrassi_FI26-11-2010_1
                  //(((rowSetup[Order.DEL_BUNDLE_0].ToString().Equals(true.ToString()) ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));
                  (((this.chk_Bundle0.Checked ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));

                int maxSending = limitSheetsXStackFormat0;
                if (!(cmb_sending0.SelectedIndex == -1 || cmb_sending0.SelectedItemData.Equals(PalletSending.Other.ToString())))
                {//pallet sending already selected
                  if (cmb_sending0.SelectedItemData.Equals(PalletSending.Delivery.ToString()))
                  {
                    maxSending = Convert.ToInt32(Math.Floor((double)
                      ((stacker.MaxDeliveryStackHeight * 10) / thickness)));
                  }
                  else if (cmb_sending0.SelectedItemData.Equals(PalletSending.Transform.ToString()))
                  {
                    maxSending = Convert.ToInt32(Math.Floor((double)
                      ((stacker.MaxTransformStackHeight * 10) / thickness)));
                  }
                }
                int maxStability = limitSheetsXStackFormat0;
                if (!(nud_Width0.Value.Equals(string.Empty)))
                {	//Width already edited
                  int width = 0;
                  try
                  {
                    width = Convert.ToInt32(nud_Width0.Value);
                  }
                  catch (Exception ex)
                  {
                    Logger.LogError(this, "UpdateMaxHeightControlValue (format0) - Error converting width " + nud_Width0.Value, ex);
                    if (num == 0)
                      return;
                  }
                  if (width != 0)
                  {
                    maxStability = Convert.ToInt32(Math.Floor((double)
                      ((width * stacker.StabilityStackFactor) / thickness)));
                    //agrassi_FI01-06-2010_1
                    //if (this.state == State.Insert)
                    if ((this.state == State.Insert) && (!this.copyAndModify))
                    {
                      this.nud_stacksXpallet0.Value = (stacker.OptimizationWidth * 10 / width).ToString();
                      //AGrassi_FI31/08/2007_3
                      if (ttb_Measures0.Value.Trim() == "")
                        this.ttb_Measures0.Value = (width / 10).ToString();
                    }
                  }
                }
                maxSheetsXStackFormat0 = Math.Min(maxSending, maxStability);
              }
            }
            //
            //format 1
            //
            if (num == 1 || num == 3)
            {
              limitSheetsXStackFormat1 = 0;
              maxSheetsXStackFormat1 = 0;

              if (cmb_Level1.SelectedIndex == -1 || cmb_Level1.SelectedItemData.Equals(string.Empty))
              { //no level has been selected for format 0 yet 
                Logger.LogError(this, "UpdateMaxHeightControlValue - No Selected Item For cmb_Level1");
                if (num == 1)
                  return;
              }
              else
              { //level for format 1 already selected
                SyncroC.DataBase.LineConfig.Stacker stacker = null;
                if (cmb_Level1.SelectedItemData.Equals(LevelName.Lower.ToString()))
                {
                  stacker = dbm.lineConfig.Stacker1Config;
                }
                else if (cmb_Level1.SelectedItemData.Equals(LevelName.Middle.ToString()))
                {
                  stacker = dbm.lineConfig.Stacker2Config;
                }
                else if (cmb_Level1.SelectedItemData.Equals(LevelName.Upper.ToString()))
                {
                  switch (knife.LevelCount)
                  {
                    case 1:
                      Logger.LogError(this, "UpdateMaxHeightControlValue (format 1) - Invalid level selected, cannot select Higher level in a single knife dry end");
                      break;
                    case 2:
                      stacker = dbm.lineConfig.Stacker2Config;
                      break;
                    case 3:
                      stacker = dbm.lineConfig.Stacker3Config;
                      break;
                  }
                }
                else
                {
                  Logger.LogError(this, "UpdateMaxHeightControlValue (format 1) - Error identifing selected level " + cmb_Level1.SelectedItemData.ToString());
                  if (num == 1)
                    return;
                }
                //agrassi_FI02-09-2010_1
                //agrassi_FI26-11-2010_1
                //DataRow rowSetup = DBManager.getDBManager().order.order.Tables[0].Rows[indexOrderToShowDRY];
                limitSheetsXStackFormat1 = Convert.ToInt32(Math.Floor((double)
                  //agrassi_FI02-09-2010_1
                  //((stacker.MaxStackHeight * 10) / thickness)));
                  //agrassi_FI26-11-2010_1
                  //(((rowSetup[Order.DEL_BUNDLE_1].ToString().Equals(true.ToString()) ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));
                  (((this.chk_Bundle1.Checked ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));

                int maxSending = limitSheetsXStackFormat1;
                if (!(cmb_sending1.SelectedIndex == -1 || cmb_sending1.SelectedItemData.Equals(PalletSending.Other.ToString())))
                {//pallet sending already selected
                  if (cmb_sending1.SelectedItemData.Equals(PalletSending.Delivery.ToString()))
                  {
                    maxSending = Convert.ToInt32(Math.Floor((double)
                      ((stacker.MaxDeliveryStackHeight * 10) / thickness)));
                  }
                  else if (cmb_sending1.SelectedItemData.Equals(PalletSending.Transform.ToString()))
                  {
                    maxSending = Convert.ToInt32(Math.Floor((double)
                      ((stacker.MaxTransformStackHeight * 10) / thickness)));
                  }
                }
                int maxStability = limitSheetsXStackFormat1;
                if (!(nud_Width0.Value.Equals(string.Empty)))
                {	//Width already edited
                  int width = 0;
                  try
                  {
                    width = Convert.ToInt32(nud_Width1.Value);
                  }
                  catch (Exception ex)
                  {
                    Logger.LogError(this, "UpdateMaxHeightControlValue (format 1) - Error converting width " + nud_Width0.Value, ex);
                    if (num == 1)
                      return;
                  }
                  if (width != 0)
                  {
                    maxStability = Convert.ToInt32(Math.Floor((double)
                      ((width * stacker.StabilityStackFactor) / thickness)));
                    //agrassi_FI01-06-2010_1
                    //if (this.state == State.Insert)
                    if ((this.state == State.Insert) && (!this.copyAndModify))
                    {
                      this.nud_stacksXpallet1.Value = (stacker.OptimizationWidth * 10 / width).ToString();
                      //AGrassi_FI31/08/2007_3
                      if (ttb_Measures1.Value.Trim() == "")
                        this.ttb_Measures1.Value = (width / 10).ToString();
                    }
                  }
                }
                maxSheetsXStackFormat1 = Math.Min(maxSending, maxStability);
              }
            }
            //
            //format 2
            //
            if (num == 2 || num == 3)
            {
              limitSheetsXStackFormat2 = 0;
              maxSheetsXStackFormat2 = 0;

              if (cmb_Level2.SelectedIndex == -1 || cmb_Level2.SelectedItemData.Equals(string.Empty))
              { //no level has been selected for format 2 yet 
                Logger.LogError(this, "UpdateMaxHeightControlValue - No Selected Item For cmb_Level2");
                return;
              }
              else
              { //level for format 2 already selected
                SyncroC.DataBase.LineConfig.Stacker stacker = null;
                if (cmb_Level2.SelectedItemData.Equals(LevelName.Lower.ToString()))
                {
                  stacker = dbm.lineConfig.Stacker1Config;
                }
                else if (cmb_Level2.SelectedItemData.Equals(LevelName.Middle.ToString()))
                {
                  stacker = dbm.lineConfig.Stacker2Config;
                }
                else if (cmb_Level2.SelectedItemData.Equals(LevelName.Upper.ToString()))
                {
                  switch (knife.LevelCount)
                  {
                    case 1:
                      Logger.LogError(this, "UpdateMaxHeightControlValue (format 2) - Invalid level selected, cannot select Higher level in a single knife dry end");
                      break;
                    case 2:
                      stacker = dbm.lineConfig.Stacker2Config;
                      break;
                    case 3:
                      stacker = dbm.lineConfig.Stacker3Config;
                      break;
                  }
                }
                else
                {
                  Logger.LogError(this, "UpdateMaxHeightControlValue (format 2) - Error identifing selected level " + cmb_Level2.SelectedItemData.ToString());
                  return;
                }
                //agrassi_FI02-09-2010_1
                //agrassi_FI26-11-2010_1
                //DataRow rowSetup = DBManager.getDBManager().order.order.Tables[0].Rows[indexOrderToShowDRY];
                limitSheetsXStackFormat2 = Convert.ToInt32(Math.Floor((double)
                  //agrassi_FI02-09-2010_1
                  //((stacker.MaxStackHeight * 10) / thickness)));
                  //agrassi_FI26-11-2010_1
                  //(((rowSetup[Order.DEL_BUNDLE_2].ToString().Equals(true.ToString()) ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));
                  (((this.chk_Bundle2.Checked ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));

                int maxSending = limitSheetsXStackFormat2;
                if (!(cmb_sending2.SelectedIndex == -1 || cmb_sending2.SelectedItemData.Equals(PalletSending.Other.ToString())))
                {//pallet sending already selected
                  if (cmb_sending2.SelectedItemData.Equals(PalletSending.Delivery.ToString()))
                  {
                    maxSending = Convert.ToInt32(Math.Floor((double)
                      ((stacker.MaxDeliveryStackHeight * 10) / thickness)));
                  }
                  else if (cmb_sending2.SelectedItemData.Equals(PalletSending.Transform.ToString()))
                  {
                    maxSending = Convert.ToInt32(Math.Floor((double)
                      ((stacker.MaxTransformStackHeight * 10) / thickness)));
                  }
                }
                int maxStability = limitSheetsXStackFormat2;
                if (!(nud_Width2.Value.Equals(string.Empty)))
                {	//Width already edited
                  int width = 0;
                  try
                  {
                    width = Convert.ToInt32(nud_Width2.Value);
                  }
                  catch (Exception ex)
                  {
                    Logger.LogError(this, "UpdateMaxHeightControlValue (format 2) - Error converting width " + nud_Width2.Value, ex);
                    return;
                  }
                  if (width != 0)
                  {
                    maxStability = Convert.ToInt32(Math.Floor((double)
                      ((width * stacker.StabilityStackFactor) / thickness)));
                    //agrassi_FI01-06-2010_1
                    //if (this.state == State.Insert)
                    if ((this.state == State.Insert) && (!this.copyAndModify))
                    {
                      this.nud_stacksXpallet2.Value = (stacker.OptimizationWidth * 10 / width).ToString();
                      //AGrassi_FI31/08/2007_3
                      if (ttb_Measures2.Value.Trim() == "")
                        this.ttb_Measures2.Value = (width / 10).ToString();
                    }
                  }
                }
                maxSheetsXStackFormat2 = Math.Min(maxSending, maxStability);
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Calculates max number of sheets per stack for each format according to the level and quality selected
    /// </summary>
    private void cmb_Quality_OR_cmb_Level_OR_cmb_Sending_SelectedItemChanged(object sender, EventArgs e)
    {
      if (this.state == State.Read)
        return;

      //PMASSARO_FI28/06/10 - Aggiunta selezione automatica della lunghezza di taglio durante l'inserimento ordine manuale
      if (this.state == State.Insert || this.state == State.Modify)
      {
        int idx = -1;

        TitledList titl = null;
        TitledNumericUpDown titNum_Length = null;

        if (sender.Equals(cmb_Level0))
        {
          titl = cmb_Level0;
          titNum_Length = nud_length0;
        }
        else if (sender.Equals(cmb_Level1))
        {
          titl = cmb_Level1;
          titNum_Length = nud_length1;
        }
        else if (sender.Equals(cmb_Level2))
        {
          titl = cmb_Level2;
          titNum_Length = nud_length2;
        }

        if (titl != null)
        {
          if (titl.SelectedItemData.Equals(LevelName.Lower.ToString()) && knife.LevelCount > 0)
            idx = 0;
          if (titl.SelectedItemData.Equals(LevelName.Middle.ToString()) && knife.LevelCount > 2)
            idx = 1;
          if (titl.SelectedItemData.Equals(LevelName.Upper.ToString()))
          {
            if (knife.LevelCount == 2)
              idx = 1;
            else if (knife.LevelCount > 2 )
              idx = 2;
          }
        }

        if (titNum_Length != null)
        {
          switch (idx)
          {
            case 0:
              if (knife.KnifeLevelSettings[idx].MaxCutLength == knife.KnifeLevelSettings[idx].MinCutLength)
              { titNum_Length.Value = knife.KnifeLevelSettings[idx].MaxCutLength.ToString(); }
              break;
            case 1:
              if (knife.KnifeLevelSettings[idx].MaxCutLength == knife.KnifeLevelSettings[idx].MinCutLength)
              { titNum_Length.Value = knife.KnifeLevelSettings[idx].MaxCutLength.ToString(); }
              break;
            case 2:
              if (knife.KnifeLevelSettings[idx].MaxCutLength == knife.KnifeLevelSettings[idx].MinCutLength)
              { titNum_Length.Value = knife.KnifeLevelSettings[idx].MaxCutLength.ToString(); }
              break;
          }
        }
      }

      if (cmb_Quality.SelectedIndex == -1 || cmb_Quality.SelectedItem.Equals(string.Empty))
      { //no quality has been selected yet
        nud_sheetsXstack0.SetError(string.Empty);
        nud_sheetsXstack1.SetError(string.Empty);
        nud_sheetsXstack2.SetError(string.Empty);
      }
      else
      { //quality already selected 
        //DataRow row = quality.Rows.Find(cmb_Quality.SelectedItem);
        DataRow row = DBManager.getDBManager().quality.getRow(this.cmb_Quality.SelectedItem);
        if (row == null)
        {
          Logger.LogError(this, "cmb_Quality_OR_cmb_Level_SelectedItemChanged - Cannot find selected quality \"" + cmb_Quality.SelectedItem + "\" into Quality database");
          nud_sheetsXstack0.SetError(string.Empty);
          nud_sheetsXstack1.SetError(string.Empty);
          nud_sheetsXstack2.SetError(string.Empty);
        }
        else
        {
          //AGrassi_FI31/08/2007_3 
          //PMASSARO_FI22/02/08_2. In the old case the value was evaluated just the first time, so if the operator
          //changed another time the quality, the pressure didn't changed
          //if (this.intParse(this.nud_ScoreGap.Value)==0)
          if ((this.intParse(this.nud_ScoreGap.Value) == 0) || (this.intParse(this.nud_ScoreGap.Value) != Convert.ToInt32(row[Quality.SCOREGAP_UDdmm].ToString())))
            this.nud_ScoreGap.Value = row[Quality.SCOREGAP_UDdmm].ToString();
          int thickness = 0;
          try
          {
            thickness = Convert.ToInt32(row[Quality.THICKNESS_UDdmm].ToString());
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "cmb_Quality_OR_cmb_Level_SelectedItemChanged - Error converting thickness, value " + row[Quality.THICKNESS_UDdmm].ToString(), ex);
          }
          if (thickness == 0)
          {
            Logger.LogError(this, "cmb_Quality_OR_cmb_Level_SelectedItemChanged - Quality \"" + cmb_Quality.SelectedItem + "\" thickness is 0");
            nud_sheetsXstack0.SetError(string.Empty);
            nud_sheetsXstack1.SetError(string.Empty);
            nud_sheetsXstack2.SetError(string.Empty);
          }
          else
          {	//
            //format 0
            //
            if (cmb_Level0.SelectedIndex == -1 || cmb_Level0.SelectedItemData.Equals(string.Empty))
            { //no level has been selected for format 0 yet 
              nud_sheetsXstack0.SetError(string.Empty);
            }
            else
            { //level for format 0 already selected
              SyncroC.DataBase.LineConfig.Stacker stacker = null;
              if (cmb_Level0.SelectedItemData.Equals(LevelName.Lower.ToString()))
              {
                stacker = dbm.lineConfig.Stacker1Config;
              }
              else if (cmb_Level0.SelectedItemData.Equals(LevelName.Middle.ToString()))
              {
                stacker = dbm.lineConfig.Stacker2Config;
              }
              else if (cmb_Level0.SelectedItemData.Equals(LevelName.Upper.ToString()))
              {
                switch (knife.LevelCount)
                {
                  case 1:
                    Logger.LogError(this, "cmb_Quality_OR_cmb_Level_SelectedItemChanged (format 0) - Invalid level selected, cannot select Higher level in a single knife dry end");
                    break;
                  case 2:
                    stacker = dbm.lineConfig.Stacker2Config;
                    break;
                  case 3:
                    stacker = dbm.lineConfig.Stacker3Config;
                    break;
                }
              }
              else
              {
                Logger.LogError(this, "cmb_Quality_OR_cmb_Level_SelectedItemChanged (format0) - Error identifing selected level " + cmb_Level0.SelectedItemData.ToString());
              }
              //agrassi_FI02-09-2010_1
              //agrassi_FI26-11-2010_1
              //DataRow rowSetup = DBManager.getDBManager().order.order.Tables[0].Rows[indexOrderToShowDRY];
              limitSheetsXStackFormat0 = Convert.ToInt32(Math.Floor((double)
                //agrassi_FI02-09-2010_1
                //((stacker.MaxStackHeight * 10) / thickness)));
                //agrassi_FI26-11-2010_1
                //(((rowSetup[Order.DEL_BUNDLE_0].ToString().Equals(true.ToString()) ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));
                (((this.chk_Bundle0.Checked ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));

              int maxSending = limitSheetsXStackFormat0;
              if (!(cmb_sending0.SelectedIndex == -1 || cmb_sending0.SelectedItemData.Equals(PalletSending.Other.ToString())))
              {//pallet sending already selected
                if (cmb_sending0.SelectedItemData.Equals(PalletSending.Delivery.ToString()))
                {
                  maxSending = Convert.ToInt32(Math.Floor((double)
                    ((stacker.MaxDeliveryStackHeight * 10) / thickness)));
                }
                else if (cmb_sending0.SelectedItemData.Equals(PalletSending.Transform.ToString()))
                {
                  maxSending = Convert.ToInt32(Math.Floor((double)
                    ((stacker.MaxTransformStackHeight * 10) / thickness)));
                }
              }
              int maxStability = limitSheetsXStackFormat0;
              if (!(nud_Width0.Value.Equals(string.Empty)))
              {	//Width already edited
                int width = 0;
                try
                {
                  width = Convert.ToInt32(nud_Width0.Value);
                }
                catch (Exception ex)
                {
                  Logger.LogError(this, "cmb_Quality_OR_cmb_Level_SelectedItemChanged (format0) - Error converting width " + nud_Width0.Value, ex);
                }
                if (width != 0)
                {
                  maxStability = Convert.ToInt32(Math.Floor((double)
                    ((width * stacker.StabilityStackFactor) / thickness)));
                  //agrassi_FI01-06-2010_1
                  //if (this.state == State.Insert)
                  if ((this.state == State.Insert) && (!this.copyAndModify))
                  {
                    this.nud_stacksXpallet0.Value = (stacker.OptimizationWidth * 10 / width).ToString();
                    //AGrassi_FI31/08/2007_3
                    if (ttb_Measures0.Value.Trim() == "")
                      this.ttb_Measures0.Value = (width / 10).ToString();
                  }
                }
              }
              maxSheetsXStackFormat0 = Math.Min(maxSending, maxStability);

              sheetsXstack0_Validating(sender, null);
            }
            //
            //format 1
            //
            if (cmb_Level1.SelectedIndex == -1 || cmb_Level1.SelectedItemData.Equals(string.Empty))
            { //any level has been selected for format 1 yet 
              nud_sheetsXstack1.SetError(string.Empty);
            }
            else
            { //level for format 1 already selected
              SyncroC.DataBase.LineConfig.Stacker stacker = null;
              if (cmb_Level1.SelectedItemData.Equals(LevelName.Lower.ToString()))
              {
                stacker = dbm.lineConfig.Stacker1Config;
              }
              else if (cmb_Level1.SelectedItemData.Equals(LevelName.Middle.ToString()))
              {
                stacker = dbm.lineConfig.Stacker2Config;
              }
              else if (cmb_Level1.SelectedItemData.Equals(LevelName.Upper.ToString()))
              {
                switch (knife.LevelCount)
                {
                  case 1:
                    Logger.LogError(this, "cmb_Quality_OR_cmb_Level_SelectedItemChanged (format 1) - Invalid level selected, cannot select Higher level in a single knife dry end");
                    break;
                  case 2:
                    stacker = dbm.lineConfig.Stacker2Config;
                    break;
                  case 3:
                    stacker = dbm.lineConfig.Stacker3Config;
                    break;
                }
              }
              else
              {
                Logger.LogError(this, "cmb_Quality_OR_cmb_Level_SelectedItemChanged (format1) - Error identifing selected level " + cmb_Level1.SelectedItemData.ToString());
              }
              //agrassi_FI02-09-2010_1
              //agrassi_FI26-11-2010_1
              //DataRow rowSetup = DBManager.getDBManager().order.order.Tables[0].Rows[indexOrderToShowDRY];
              limitSheetsXStackFormat1 = Convert.ToInt32(Math.Floor((double)
                //agrassi_FI02-09-2010_1
                //((stacker.MaxStackHeight * 10) / thickness)));
                //agrassi_FI26-11-2010_1
                //(((rowSetup[Order.DEL_BUNDLE_1].ToString().Equals(true.ToString()) ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));
                (((this.chk_Bundle1.Checked ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));

              int maxSending = limitSheetsXStackFormat1;
              if (!(cmb_sending1.SelectedIndex == -1 || cmb_sending1.SelectedItemData.Equals(PalletSending.Other.ToString())))
              {//pallet sending already selected
                if (cmb_sending1.SelectedItemData.Equals(PalletSending.Delivery.ToString()))
                {
                  maxSending = Convert.ToInt32(Math.Floor((double)
                    ((stacker.MaxDeliveryStackHeight * 10) / thickness)));
                }
                else if (cmb_sending0.SelectedItemData.Equals(PalletSending.Transform.ToString()))
                {
                  maxSending = Convert.ToInt32(Math.Floor((double)
                    ((stacker.MaxTransformStackHeight * 10) / thickness)));
                }
              }
              int maxStability = limitSheetsXStackFormat1;
              if (!(nud_Width1.Value.Equals(string.Empty)))
              {	//Width already edited
                int width = 0;
                try
                {
                  width = Convert.ToInt32(nud_Width1.Value);
                }
                catch (Exception ex)
                {
                  Logger.LogError(this, "cmb_Quality_OR_cmb_Level_SelectedItemChanged (format1) - Error converting width " + nud_Width1.Value, ex);
                }
                if (width != 0)
                {
                  maxStability = Convert.ToInt32(Math.Floor((double)
                    ((width * stacker.StabilityStackFactor) / thickness)));
                  //agrassi_FI01-06-2010_1
                  //if (this.state == State.Insert)
                  if ((this.state == State.Insert) && (!this.copyAndModify))
                  {
                    this.nud_stacksXpallet1.Value = (stacker.OptimizationWidth * 10 / width).ToString();
                    //AGrassi_FI31/08/2007_3
                    if (ttb_Measures1.Value.Trim() == "")
                      this.ttb_Measures1.Value = (width / 10).ToString();
                  }
                }
              }
              maxSheetsXStackFormat1 = Math.Min(maxSending, maxStability);

              sheetsXstack1_Validating(sender, null);
            }
            //
            //format 2
            //
            if (cmb_Level2.SelectedIndex == -1 || cmb_Level2.SelectedItemData.Equals(string.Empty))
            { //any level has been selected for format 1 yet 
              nud_sheetsXstack2.SetError(string.Empty);
            }
            else
            { //level for format 2 already selected
              SyncroC.DataBase.LineConfig.Stacker stacker = null;
              if (cmb_Level2.SelectedItemData.Equals(LevelName.Lower.ToString()))
              {
                stacker = dbm.lineConfig.Stacker1Config;
              }
              else if (cmb_Level2.SelectedItemData.Equals(LevelName.Middle.ToString()))
              {
                stacker = dbm.lineConfig.Stacker2Config;
              }
              else if (cmb_Level2.SelectedItemData.Equals(LevelName.Upper.ToString()))
              {
                switch (knife.LevelCount)
                {
                  case 1:
                    Logger.LogError(this, "cmb_Quality_OR_cmb_Level_SelectedItemChanged (format 2) - Invalid level selected, cannot select Higher level in a single knife dry end");
                    break;
                  case 2:
                    stacker = dbm.lineConfig.Stacker2Config;
                    break;
                  case 3:
                    stacker = dbm.lineConfig.Stacker3Config;
                    break;
                }
              }
              else
              {
                Logger.LogError(this, "cmb_Quality_OR_cmb_Level_SelectedItemChanged (format2) - Error identifing selected level " + cmb_Level2.SelectedItemData.ToString());
              }
              //agrassi_FI02-09-2010_1
              //agrassi_FI26-11-2010_1
              //DataRow rowSetup = DBManager.getDBManager().order.order.Tables[0].Rows[indexOrderToShowDRY];
              limitSheetsXStackFormat2 = Convert.ToInt32(Math.Floor((double)
                //agrassi_FI02-09-2010_1
                //((stacker.MaxStackHeight * 10) / thickness)));
                //agrassi_FI26-11-2010_1
                //(((rowSetup[Order.DEL_BUNDLE_2].ToString().Equals(true.ToString()) ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));
                (((this.chk_Bundle2.Checked ? stacker.MaxBundleHeight : stacker.MaxStackHeight) * 10) / thickness)));

              int maxSending = limitSheetsXStackFormat2;
              if (!(cmb_sending2.SelectedIndex == -1 || cmb_sending2.SelectedItemData.Equals(PalletSending.Other.ToString())))
              {//pallet sending already selected
                if (cmb_sending2.SelectedItemData.Equals(PalletSending.Delivery.ToString()))
                {
                  maxSending = Convert.ToInt32(Math.Floor((double)
                    ((stacker.MaxDeliveryStackHeight * 10) / thickness)));
                }
                else if (cmb_sending0.SelectedItemData.Equals(PalletSending.Transform.ToString()))
                {
                  maxSending = Convert.ToInt32(Math.Floor((double)
                    ((stacker.MaxTransformStackHeight * 10) / thickness)));
                }
              }
              int maxStability = limitSheetsXStackFormat2;
              if (!(nud_Width2.Value.Equals(string.Empty)))
              {	//Width already edited
                int width = 0;
                try
                {
                  width = Convert.ToInt32(nud_Width2.Value);
                }
                catch (Exception ex)
                {
                  Logger.LogError(this, "cmb_Quality_OR_cmb_Level_SelectedItemChanged (format2) - Error converting width " + nud_Width2.Value, ex);
                }
                if (width != 0)
                {
                  maxStability = Convert.ToInt32(Math.Floor((double)
                    ((width * stacker.StabilityStackFactor) / thickness)));
                  //agrassi_FI01-06-2010_1
                  //if (this.state == State.Insert)
                  if ((this.state == State.Insert) && (!this.copyAndModify))
                  {
                    this.nud_stacksXpallet2.Value = (stacker.OptimizationWidth * 10 / width).ToString();
                    //AGrassi_FI31/08/2007_3
                    if (ttb_Measures2.Value.Trim() == "")
                      this.ttb_Measures2.Value = (width / 10).ToString();
                  }
                }
              }
              maxSheetsXStackFormat2 = Math.Min(maxSending, maxStability);

              sheetsXstack2_Validating(sender, null);
            }
          }
        }
      }
    }

    /// <summary>
    /// Fill the items of the CutToPattern list using the flute database.
    /// </summary>
    /// <param name="indexToSelect">Index of the item to select into the list.</param>
    /// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
    private void setListCutToPatternItems(int indexToSelect, bool Clean)
    {
      if (Clean)
      {
        this.cmb_cutToPattern0.ClearItems();
        this.cmb_cutToPattern1.ClearItems();
        this.cmb_cutToPattern2.ClearItems();
      }
      DataTable cutToPattern = DBManager.getDBManager().cutToPattern.getTable();
      for (int i = 0; i < cutToPattern.Rows.Count; i++)
      {
        if (!this.cmb_cutToPattern0.Contains(cutToPattern.Rows[i][CutToPattern.PATTERN_ID].ToString()))
          this.cmb_cutToPattern0.AddItem(cutToPattern.Rows[i][CutToPattern.PATTERN_ID].ToString());
        if (!this.cmb_cutToPattern1.Contains(cutToPattern.Rows[i][CutToPattern.PATTERN_ID].ToString()))
          this.cmb_cutToPattern1.AddItem(cutToPattern.Rows[i][CutToPattern.PATTERN_ID].ToString());
        if (!this.cmb_cutToPattern2.Contains(cutToPattern.Rows[i][CutToPattern.PATTERN_ID].ToString()))
          this.cmb_cutToPattern2.AddItem(cutToPattern.Rows[i][CutToPattern.PATTERN_ID].ToString());
      }
      if (indexToSelect >= 0 && indexToSelect <= cmb_cutToPattern0.Count - 1 && Clean)
        this.cmb_cutToPattern0.SelectedIndex = indexToSelect;
      if (indexToSelect >= 0 && indexToSelect <= cmb_cutToPattern1.Count - 1 && Clean)
        this.cmb_cutToPattern1.SelectedIndex = indexToSelect;
      if (indexToSelect >= 0 && indexToSelect <= cmb_cutToPattern2.Count - 1 && Clean)
        this.cmb_cutToPattern2.SelectedIndex = indexToSelect;
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
    /// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
    private void setListQualityItems(int indexToSelect, string SelectedFlute, bool Clean)
    {
      if (Clean)
        this.cmb_Quality.ClearItems();

      if (SelectedFlute != null && !SelectedFlute.Equals(String.Empty))
      {
        
        DataRow[] rows = DBManager.getDBManager().quality.getTable().Select(Quality.FLUTE_ID + " = '" + SelectedFlute + "'");

        string[] stringRows = new string[rows.Length];
        int i = 0;
        foreach (System.Data.DataRow r in rows)
          stringRows[i++] = r[Quality.QUALITY_ID].ToString();
        this.cmb_Quality.AddRangeItem(stringRows);
      }

      if (indexToSelect >= 0 && indexToSelect <= this.cmb_Quality.Count - 1 && Clean)
        this.cmb_Quality.SelectedIndex = indexToSelect;
    }

    /// <summary>
    /// Fill the specified TitledList with the scorer positioning type according to the current line configuration.
    /// </summary>
    /// <param name="titledList"></param>
    /// <param name="indexToSelect"></param>
    /// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
    private void setListPositioningItems(SyncroC.Controls.TitledList titledList, int indexToSelect, bool Clean)
    {
      Language lang = DBManager.getDBManager().language;
      string typeName = typeof(SyncroC.Utility.PositType).Name + ".";
      if (Clean)
        titledList.ClearItems();
      ScoreType[] scoreType = this.ss.SlitterScorerHeader.ScoreTypes;
      string[] positTypes = Enum.GetNames(typeof(SyncroC.Utility.PositType));
      for (int i = 0; i < scoreType.Length; i++)
      {
        //agrassi_07-01-09
        if ((scoreType[i].Letter != ' ') && (!titledList.Contains(scoreType[i].Letter.ToString())) &&
          this.ss.SlitterScorerHeader.IsScorerUsed(scoreType[i].Letter))
        {
          for (int j = 1; j < positTypes.Length; j++) //j starts from 1 because the 0 enum is "NoPositType"
          {
            if (ScorerProfileType.IsPositioningAvailable(scoreType[i].Scorer.SClass, (PositType)j) && (!titledList.Contains(positTypes[j])))
              //RFAZZI PROVA
              //if (this.ss.PositioningOptions.Scores.ShowNormalReverse || ((PositType)j != PositType.NormalReverse))
              //  titledList.AddItem(lang.getTranslation(typeName + positTypes[j]), positTypes[j]);
              if (this.ss.SlitterScorerHeader.PositioningOptions.Scores.ShowNormalReverse || ((PositType)j != PositType.NormalReverse))
                titledList.AddItem(lang.getTranslation(typeName + positTypes[j]), positTypes[j]);

          }
        }
      }
      if (indexToSelect >= 0 && indexToSelect <= titledList.Count - 1 && Clean)
        titledList.SelectedIndex = indexToSelect;
      //agrassi_07-01-09
      else if (titledList.SelectedIndex == -1)
        titledList.SelectedIndex = 0;
    }

    /// <summary>
    /// Fill the specified TitledList with the scorer database according to the selected Posit Type
    /// </summary>
    /// <param name="titledList">The titledList to fill.</param>
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
        //agrassi_07-01-09
        //if ((ScorerProfileType.IsPositioningAvailable(scoreType[i].Scorer.SClass, positType) || all) &&
        //  (scoreType[i].Letter != ' ') && (!titledList.Contains(scoreType[i].Letter.ToString())))
        if ((ScorerProfileType.IsPositioningAvailable(scoreType[i].Scorer.SClass, positType) || all) &&
          (scoreType[i].Letter != ' ') && (!titledList.Contains(scoreType[i].Letter.ToString())) &&
          this.ss.SlitterScorerHeader.IsScorerUsed(scoreType[i].Letter))
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


    private void ttb_NotNullString_Validating(object sender, CancelEventArgs e)
    {
      if (this.state != State.Read)
      {
        string s = ((TitledTextBox)sender).Value.Trim();
        if (s.Length == 0)
        {
          string error = PageManager.getPageManager()._SetMessage("§SYCC_02", true); ;
          ((SyncroC.Controls.SyncroUserControl)sender).SetError(error);
        }
        else
          ((SyncroC.Controls.SyncroUserControl)sender).SetError("");
      }
    }

    private void nud_PositiveValues_Validating(object sender, CancelEventArgs e)
    {
      if (this.state != State.Read)
      {
        string s = ((TitledNumericUpDown)sender).Value.Trim();
        if (intParse(s) <= 0)
        {
          string error = PageManager.getPageManager()._SetMessage("§SYCC_03§SYCC_04#0", true);
          ((SyncroC.Controls.SyncroUserControl)sender).SetError(error);
        }
        else
        {
          ((SyncroC.Controls.SyncroUserControl)sender).SetError("");
          this.CheckAndUpdateTrim(e, false);
        }
      }
    }

    //Web width
    private void nud_WebWidth_Enter(object sender, System.EventArgs e)
    {
      SetSuggestionFields(SyncroConverter.Convert(this.ss.SlitterScorerHeader.MinTrim + this.ss.SlitterScorerHeader.MinChutesPosition,
                                    ((TitledNumericUpDown)sender).InternalUnitMeasure,
                                    ((TitledNumericUpDown)sender).UnitMeasure).ToString(),
                          SyncroConverter.Convert(this.ss.SlitterScorerHeader.MaxWebWidth,
                                    ((TitledNumericUpDown)sender).InternalUnitMeasure,
                                    ((TitledNumericUpDown)sender).UnitMeasure).ToString());
    }
    private void nud_WebWidth_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (this.state != State.Read)
      {
        string s = ((TitledNumericUpDown)sender).Value.Trim();
        if ((intParse(s) < this.ss.SlitterScorerHeader.MinTrim + this.ss.SlitterScorerHeader.MinChutesPosition) || (intParse(s) > this.ss.SlitterScorerHeader.MaxWebWidth))
        {
          string error = PageManager.getPageManager()._SetMessage("SYCC_06", true);
          ((SyncroC.Controls.SyncroUserControl)sender).SetError(error);
        }
        else
        {
          ((SyncroC.Controls.SyncroUserControl)sender).SetError("");
          this.CheckAndUpdateTrim(e, false);
        }
      }
    }

    //ScoreGap
    private void nud_ScoreGap_Enter(object sender, EventArgs e)
    {
      SetSuggestionFields("0", "");
    }

    //Delivery		
    private void Delivery_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (state == State.Insert || state == State.Modify)
      {
        TitledMaskTextBox maskTextBox = ((TitledMaskTextBox)sender);
        if (!maskTextBox.IsValidDataEntered())
        {
          string text = PageManager.getPageManager()._SetMessage("§SYCC_32");
          maskTextBox.SetError(text);
        }
        else
          maskTextBox.SetError("");
      }
    }

    //Width 0
    private void Fwidth0_Enter(object sender, System.EventArgs e)
    {
      CleanSuggestionFields();
    }
    private void Fwidth0_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (state == State.Insert || state == State.Modify)
      {
        this.CheckAndUpdateTrim(null, false);
      }
    }

    //Width 1
    private void Fwidth1_Enter(object sender, System.EventArgs e)
    {
      //  int max = Math.Min(maxFWidth, Convert.ToInt32(nud_WebWidth.Value) - Convert.ToInt32(nud_Width0.Value) - Convert.ToInt32(nud_Width2.Value));
      //  SetSuggestionFields(minFWidth.ToString(), max.ToString());
      CleanSuggestionFields();
    }
    private void Fwidth1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (state == State.Insert || state == State.Modify)
      {
        this.CheckAndUpdateTrim(null, false);
      }
    }

    //Width 2
    private void Fwidth2_Enter(object sender, System.EventArgs e)
    {
      CleanSuggestionFields();
    }
    private void Fwidth2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (state == State.Insert || state == State.Modify)
      {
        this.CheckAndUpdateTrim(null, false);
      }
    }

    //Length
    private void Flength_Enter(object sender, System.EventArgs e)
    {
      CleanSuggestionFields();
    }
    private void Flength_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (state == State.Insert || state == State.Modify)
      {
        IValidableControl control = ((IValidableControl)sender);
        control.SetError("");
      }
    }

    //Sheets X Stack 0
    private void sheetsXstack0_Enter(object sender, System.EventArgs e)
    {
      if ((state == State.Insert || state == State.Modify))
      {
        SetSuggestionFields(
          string.Empty,
          (maxSheetsXStackFormat0 != 0) ? maxSheetsXStackFormat0.ToString() : string.Empty);
        if ((!nud_Boxes0.Value.Equals("0") || !nud_Boxes0.Value.Equals(string.Empty)) && (nud_sheetsXstack0.Value.Equals("0") || nud_sheetsXstack0.Value.Equals(string.Empty)))
        {//format 0 is used for this order && user has not inserted any value into the sheetsPerStack field
          //so set the field with the suggested value.
          nud_sheetsXstack0.Value = maxSheetsXStackFormat0.ToString();
        }
      }
    }
    private void sheetsXstack0_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    //(e == null) means the function has not been called by the usual associated event BUT
    //it has been called by other local function
    {
      if (state == State.Insert || state == State.Modify)
      {
        //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
        if (limitSheetsXStackFormat0 == 0 || maxSheetsXStackFormat0 == 0) //ensure was calculated
          UpdateMaxHeightControlValue(0);

        int sheetsXStack = 0;
        try
        {
          if (!nud_sheetsXstack0.Value.Equals(string.Empty))
            sheetsXStack = Convert.ToInt32(nud_sheetsXstack0.Value);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "sheetsXstack0_Validating - Error converting value \"" + nud_sheetsXstack0.Value + "\"", ex);
        }
        if ((e == null && sheetsXStack != 0) || (e != null))
        {
          //check for error
          if (sheetsXStack > limitSheetsXStackFormat0)
          {
            string error = dbm.message.getTranslatedMessage("§SYCC_80#(" + limitSheetsXStackFormat0.ToString() + ")");
            if (e != null)
            {
              PageManager.getPageManager()._SetMessage(error, true);

              //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
              nud_sheetsXstack0.Value = limitSheetsXStackFormat0.ToString();
              PageManager.getPageManager()._SetMessage(error, false);
            }

            //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
            /*
            nud_sheetsXstack0.SetError(error);
            */
            nud_sheetsXstack0.SetError(string.Empty);
          }
          else
          {
            //check for warning
            //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
            if (sheetsXStack <= 0)
            {
              nud_sheetsXstack0.Value = maxSheetsXStackFormat0.ToString();
            }

            if (sheetsXStack > maxSheetsXStackFormat0)
            {
              string error = dbm.message.getTranslatedMessage("§SYCC_81#" + maxSheetsXStackFormat0.ToString());
              if (e != null)
              {
                PageManager.getPageManager()._SetMessage(error);
              }
              nud_sheetsXstack0.SetError(error);
            }
            else
            {//no error nor warning
              nud_sheetsXstack0.SetError(string.Empty);
            }
          }
        }
      }
    }
    //Sheets X Stack 1
    private void sheetsXstack1_Enter(object sender, System.EventArgs e)
    {
      if ((state == State.Insert || state == State.Modify))
      {
        SetSuggestionFields(
          string.Empty,
          (maxSheetsXStackFormat1 != 0) ? maxSheetsXStackFormat1.ToString() : string.Empty);
        if ((!nud_Boxes1.Value.Equals("0") || !nud_Boxes1.Value.Equals(string.Empty)) && (nud_sheetsXstack1.Value.Equals("0") || nud_sheetsXstack1.Value.Equals(string.Empty)))
        {//format 1 is used for this order && user has not inserted any value into the sheetsPerStack field
          //so set the field with the suggested value.
          nud_sheetsXstack1.Value = maxSheetsXStackFormat1.ToString();
        }
      }
    }
    private void sheetsXstack1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    //(e == null) means the function has not been called by the usual associated event BUT
    //it has been called by other local function
    {

      if (state == State.Insert || state == State.Modify)
      {
        //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
        if (limitSheetsXStackFormat1 == 0 || maxSheetsXStackFormat1 == 0) //ensure was calculated
          UpdateMaxHeightControlValue(1);

        int sheetsXStack = 0;
        try
        {
          if (!nud_sheetsXstack1.Value.Equals(string.Empty))
            sheetsXStack = Convert.ToInt32(nud_sheetsXstack1.Value);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "sheetsXstack1_Validating - Error converting value \"" + nud_sheetsXstack1.Value + "\"", ex);
        }
        if ((e == null && sheetsXStack != 0) || (e != null))
        {
          //check for error
          if (sheetsXStack > limitSheetsXStackFormat1)
          {
            string error = dbm.message.getTranslatedMessage("§SYCC_80#(" + limitSheetsXStackFormat1.ToString() + ")");
            if (e != null)
            {
              PageManager.getPageManager()._SetMessage(error, true);
              //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
              nud_sheetsXstack1.Value = limitSheetsXStackFormat1.ToString();
              PageManager.getPageManager()._SetMessage(error, false);
            }

            //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
            /*
            nud_sheetsXstack1.SetError(error);
            */
            nud_sheetsXstack1.SetError(string.Empty);
          }
          else
          {
            //check for warning
            //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
            if (sheetsXStack <= 0)
            {
              nud_sheetsXstack1.Value = maxSheetsXStackFormat1.ToString();
            }

            if (sheetsXStack > maxSheetsXStackFormat1)
            {
              string error = dbm.message.getTranslatedMessage("§SYCC_81#" + maxSheetsXStackFormat1.ToString());
              if (e != null)
              {
                PageManager.getPageManager()._SetMessage(error);
              }
              nud_sheetsXstack0.SetError(error);
            }
            else
            {//no error nor warning
              nud_sheetsXstack1.SetError(string.Empty);
            }
          }
        }
      }
    }
    //Sheets X Stack 2
    private void sheetsXstack2_Enter(object sender, System.EventArgs e)
    {
      if ((state == State.Insert || state == State.Modify))
      {
        SetSuggestionFields(
          string.Empty,
          (maxSheetsXStackFormat2 != 0) ? maxSheetsXStackFormat2.ToString() : string.Empty);
        if ((!nud_Boxes2.Value.Equals("0") || !nud_Boxes2.Value.Equals(string.Empty)) && (nud_sheetsXstack2.Value.Equals("0") || nud_sheetsXstack2.Value.Equals(string.Empty)))
        {//format 2 is used for this order && user has not inserted any value into the sheetsPerStack field
          //so set the field with the suggested value.
          nud_sheetsXstack2.Value = maxSheetsXStackFormat2.ToString();
        }
      }
    }
    private void sheetsXstack2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    //(e == null) means the function has not been called by the usual associated event BUT
    //it has been called by other local function
    {
      if (state == State.Insert || state == State.Modify)
      {
        if (limitSheetsXStackFormat2 == 0 || maxSheetsXStackFormat2 == 0) //ensure was calculated
          UpdateMaxHeightControlValue(2);

        int sheetsXStack = 0;
        try
        {
          if (!nud_sheetsXstack2.Value.Equals(string.Empty))
            sheetsXStack = Convert.ToInt32(nud_sheetsXstack2.Value);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "sheetsXstack0_Validating - Error converting value \"" + nud_sheetsXstack2.Value + "\"", ex);
        }
        if ((e == null && sheetsXStack != 0) || (e != null))
        {
          //check for error
          if (sheetsXStack > limitSheetsXStackFormat2)
          {
            string error = dbm.message.getTranslatedMessage("§SYCC_80#(" + limitSheetsXStackFormat2.ToString() + ")");
            if (e != null)
            {
              PageManager.getPageManager()._SetMessage(error, true);
              //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
              nud_sheetsXstack2.Value = limitSheetsXStackFormat2.ToString();
              PageManager.getPageManager()._SetMessage(error, false);
            }

            //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
            /*
             nud_sheetsXstack2.SetError(error);
            */
            nud_sheetsXstack2.SetError(string.Empty);
          }
          else
          {
            //check for warning
            //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
            if (sheetsXStack <= 0)
            {
              nud_sheetsXstack2.Value = maxSheetsXStackFormat2.ToString();
            }

            if (sheetsXStack > maxSheetsXStackFormat2)
            {
              string error = dbm.message.getTranslatedMessage("§SYCC_81#" + maxSheetsXStackFormat2.ToString());
              if (e != null)
              {
                PageManager.getPageManager()._SetMessage(error);
              }
              nud_sheetsXstack2.SetError(error);
            }
            else
            {//no error nor warning
              nud_sheetsXstack2.SetError(string.Empty);
            }
          }
        }
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

    /// <summary>
    /// Set the state of the page
    /// </summary>
    /// <param name="newState">new state the page has to assume</param>
    /// <param name="dontCleanAnyFields">Specify, if the new state is insert, wether any fields has to be cleaned</param>
    private void SetPageState(State newState, bool dontCleanAnyFields)
    {
      this.nextState = newState;
      NextPageState.Text = "Next status: " + Enum.Format(typeof(State), nextState, "g");
      //agrassi_FI30-05-08
      if (this.nextState != State.Read)
        //RFAZZI PROVA
        //this.btn_ScorersOptions0.Visible = this.btn_ScorersOptions1.Visible =
        //  this.btn_ScorersOptions2.Visible = this.ss.EnableScorerOptionsInsertion && this.ss.IsTwin400();
        this.btn_ScorersOptions0.Visible = this.btn_ScorersOptions1.Visible =
          this.btn_ScorersOptions2.Visible = this.ss.Twin400.EnableScorerOptionsInsertion && this.ss.IsTwin400();
      switch (newState)
      {
        case State.Read:
          readOnly(true);
          showOrder(indexOrderToShowDRY);

          this.tab_Tab3.Focus();
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
            cleanAllFields(true);
            this.SelectFirstItem();
            lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor = lbl_Status.BackColor;
          }
          readOnly(false);
          this.ttb_ProgramNumber.Focus();
          break;
        case State.Modify:
          readOnly(false);
          this.ttb_ProgramNumber.Focus();
          this.oldScoreGap = this.intParse(this.nud_ScoreGap.Value);
          lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
          break;
      }
      CleanSuggestionFields();
      resetAllErrors();
      state = newState;
      PageState.Text = "Page status: " + Enum.Format(typeof(State), state, "g");
    }

    private void SelectFirstItem()
    {
      //PMASSARO_FI10/06/08 - 1 - Added To Manage The First Visualization Of Sending Into Order Field 
      SyncroC.DataBase.LineConfig.Syncro SyncSet = null;
      SyncSet = dbm.lineConfig.SyncroConfig;

      //Header
      ttb_index.Value = String.Empty;
      this.cmb_Flute.SelectedIndex = 0;
      this.cmb_Quality.SelectedIndex = 0;
      this.cmb_TrimType.SelectedIndex = 0;
      //Order 0
      this.cmb_Level0.SelectedIndex = 0;

      //PMASSARO_FI10/06/08 - 1 - Added To Manage The First Visualization Of Sending Into Order Field 
      /*
        this.cmb_sending0.SelectedIndex = 0;
      */
      this.cmb_sending0.SelectedIndex = SyncSet.FirstSendingSelection0;

      this.cmb_Positioning0.SelectedIndex = 0;
      this.cmb_Type0.SelectedIndex = 0;

      //RFAZZI_FI22/07/2008.Bug Fix - Not needed the above inizialization for the CutToPattern field ( prevent validation error when the CutToPattern is not enabled and the Pattern DBase is valorized )
      //this.cmb_cutToPattern0.SelectedIndex = 0;

      //Order 1
      this.cmb_Level1.SelectedIndex = 0;

      //PMASSARO_FI10/06/08 - 1 - Added To Manage The First Visualization Of Sending Into Order Field 
      /*
        this.cmb_sending1.SelectedIndex = 0;
      */
      this.cmb_sending1.SelectedIndex = SyncSet.FirstSendingSelection1;

      this.cmb_Positioning1.SelectedIndex = 0;
      this.cmb_Type1.SelectedIndex = 0;

      //RFAZZI_FI22/07/2008.Bug Fix - Not needed the above inizialization for the CutToPattern field ( prevent validation error when the CutToPattern is not enabled and the Pattern DBase is valorized )
      //this.cmb_cutToPattern1.SelectedIndex = 0;

      //Order 2
      this.cmb_Level2.SelectedIndex = 0;

      //PMASSARO_FI10/06/08 - 1 - Added To Manage The First Visualization Of Sending Into Order Field 
      /*
        this.cmb_sending2.SelectedIndex = 0;
      */
      this.cmb_sending2.SelectedIndex = SyncSet.FirstSendingSelection2;

      this.cmb_Positioning2.SelectedIndex = 0;
      this.cmb_Type2.SelectedIndex = 0;
      //RFAZZI_FI22/07/2008.Bug Fix - Not needed the above inizialization for the CutToPattern field ( prevent validation error when the CutToPattern is not enabled and the Pattern DBase is valorized )
      //this.cmb_cutToPattern2.SelectedIndex = 0;

      //PMASSARO_FI30/11/08 - Menaged First Selection For Upper Level
      if (ss.IsCompact2())
      {
        //PMASSARO_FI30/04/09 - Personalization For OndaPack BARI
        if (DBManager.getDBManager().syncroConfig.GeneralSetting.Job == "3298")
        {
          if (knife.LevelCount == 2)
          {
            this.cmb_Level0.SelectedIndex = 0;
            this.cmb_Level1.SelectedIndex = 1;
            this.cmb_Level2.SelectedIndex = 0;
          }
        }
        else
        {
          if (knife.LevelCount == 2)
          {
            this.cmb_Level0.SelectedIndex = 1;
            this.cmb_Level1.SelectedIndex = 0;
            this.cmb_Level2.SelectedIndex = 1;
          }
        }
      }

    }

    /// <summary>
    /// Calculate the trim accordingly to the currently inserted width.
    /// </summary>
    /// <returns></returns>
    private bool CheckAndUpdateTrim(CancelEventArgs e, bool final)
    {
      int trim = intParse(nud_WebWidth.Value) -
        (intParse(this.nud_Width0.Value) * intParse(this.nud_Boxes0.Value) +
         intParse(this.nud_Width1.Value) * intParse(this.nud_Boxes1.Value) +
         intParse(this.nud_Width2.Value) * intParse(this.nud_Boxes2.Value));
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
      bool under = (trim < ss.SlitterScorerHeader.MinTrim), over = (trim > ss.SlitterScorerHeader.MaxTrim);
      bool res = (under || (final && over));

      if (this.cmb_TrimType.SelectedItemData == SyncroC.Utility.TrimType.NoTrim.ToString())
      {
        if (trim == 0)
        {
          this.nud_Trim.SetError("");
          return true;
        }
        else
        {
          this.nud_Trim.SetError("§SYCC_06");
          return false;
        }
      }

      if (res)
      {
        if (under)
          PageManager.getPageManager()._SetMessage("§SYCC_03§SYCC_04#" + minimumTrim_UDmm.ToString(), true);
        else
          PageManager.getPageManager()._SetMessage("§SYCC_03§SYCC_05#" + maximumTrim_UDmm.ToString(), true);
        this.nud_Trim.SetError("§SYCC_06");
      }
      else
        this.nud_Trim.SetError("");
      return !res;
    }

    public void _ShowOrder(int indexOrderToShow)
    {
      if (state == State.Read)
      {
        Frm_OrdersInsSubGroup.indexOrderToShowDRY = indexOrderToShow;
        showOrder(Frm_OrdersInsSubGroup.indexOrderToShowDRY);
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
      cleanAllFields(false);

      //PMASSARO_FI03/09/08 - Bug Fix. Was accepted an order with sheet per stack over than fisical limit Or Zero Value
      //Else Syncro Will Use The Data Of Previous Order Shown
      limitSheetsXStackFormat0 = 0;
      maxSheetsXStackFormat0 = 0;
      limitSheetsXStackFormat1 = 0;
      maxSheetsXStackFormat1 = 0;
      limitSheetsXStackFormat2 = 0;
      maxSheetsXStackFormat2 = 0;

      if (order.Rows.Count == 0)
      {
        Logger.LogError(this, "showOrder - Order queue is empty.");
        return;
      }
      if (index < 0)
      {
        index = 0;
      }
      if (index >= order.Rows.Count)
      {
        index = order.Rows.Count - 1;
      }
      DataRow row = order.Rows[index];

      this.ttb_index.Value = row[Order.DEL_POSITION].ToString();

      if (row[Order.DEL_VALID].ToString().Equals(true.ToString()))
        ttb_index.ForeColor = ColConv.Convert(SyncroColor.Black);
      else
        ttb_index.ForeColor = ColConv.Convert(SyncroColor.AlarmRed);

      //DEBUG INFO
      this.lbl_DebugSetupID.Text = this.lbl_DebugSetupID.Text = row[Order.DEL_SETUP_ID].ToString();
      this.lbl_DebugSetupModId.Text = this.lbl_DebugSetupModId.Text = row[Order.DEL_SETUP_MODID].ToString();
      this.lbl_DebugPalletPrint0.Text = this.lbl_DebugPalletPrint0.Text.Substring(0, this.lbl_DebugPalletPrint0.Text.IndexOf('>') + 1) + " " + row[Order.DEL_PALLET_LAYOUT_0].ToString() + " " + row[Order.DEL_AMOUNT_PALLET_COPY_0].ToString();
      this.lbl_DebugPalletPrint1.Text = this.lbl_DebugPalletPrint1.Text.Substring(0, this.lbl_DebugPalletPrint1.Text.IndexOf('>') + 1) + " " + row[Order.DEL_PALLET_LAYOUT_1].ToString() + " " + row[Order.DEL_AMOUNT_PALLET_COPY_1].ToString();
      this.lbl_DebugPalletPrint2.Text = this.lbl_DebugPalletPrint2.Text.Substring(0, this.lbl_DebugPalletPrint2.Text.IndexOf('>') + 1) + " " + row[Order.DEL_PALLET_LAYOUT_2].ToString() + " " + row[Order.DEL_AMOUNT_PALLET_COPY_2].ToString();
      this.lbl_DebugPastPrint0.Text = this.lbl_DebugPastPrint0.Text.Substring(0, this.lbl_DebugPastPrint0.Text.IndexOf('>') + 1) + " " + row[Order.DEL_PASTORDER_LAYOUT_0].ToString() + " " + row[Order.DEL_AMOUNT_PASTORDER_COPY_0].ToString();
      this.lbl_DebugPastPrint1.Text = this.lbl_DebugPastPrint1.Text.Substring(0, this.lbl_DebugPastPrint1.Text.IndexOf('>') + 1) + " " + row[Order.DEL_PASTORDER_LAYOUT_1].ToString() + " " + row[Order.DEL_AMOUNT_PASTORDER_COPY_1].ToString();
      this.lbl_DebugPastPrint2.Text = this.lbl_DebugPastPrint2.Text.Substring(0, this.lbl_DebugPastPrint2.Text.IndexOf('>') + 1) + " " + row[Order.DEL_PASTORDER_LAYOUT_2].ToString() + " " + row[Order.DEL_AMOUNT_PASTORDER_COPY_2].ToString();

      //HEADER
      string setUpCode = row[Order.DEL_SETUP_CODE].ToString();
      ttb_ProgramNumber.Value = setUpCode.Substring(0, setUpCode.Length - 2);
      ttb_SetUpSequence.Value = setUpCode.Substring(setUpCode.Length - 2);
      this.nud_WebWidth.Value = row[Order.DEL_WEB_WIDTH].ToString();
      this.cmb_TrimType.SelectedItemData = row[Order.DEL_TRIM_TYPE].ToString();
      this.nud_ScoreGap.Value = row[Order.DEL_SCORE_GAP].ToString();
      DataRow qualityRow = DBManager.getDBManager().quality.getRow(row[Order.DEL_QUALITY_ID].ToString());
      //DataRow qualityRow = this.quality.Rows.Find(row[Order.DEL_QUALITY_ID].ToString());
      //agrassi_FI05-10-2010_1
      if (row[Order.DEL_FLUTE_ID].ToString().Trim()!=String.Empty)
        this.cmb_Flute.SelectedItem = row[Order.DEL_FLUTE_ID].ToString();
      else if (qualityRow != null)
        this.cmb_Flute.SelectedItem = qualityRow[Quality.FLUTE_ID].ToString();
      this.cmb_Quality.SelectedItem = row[Order.DEL_QUALITY_ID].ToString();
      this.nud_Trim.Value = row[Order.DEL_TRIM].ToString();
      int setupLength = this.intParse(row[Order.DEL_SHEET_LENGTH_0].ToString()) *
         this.intParse(row[Order.DEL_SEGMENT_SHEET_COUNT_0].ToString()) / this.intParse(row[Order.DEL_OUTS_0].ToString());
      setupLength = (int)SyncroConverter.Convert(setupLength, UnitMeasure.DeciMilliMetre, UnitMeasure.Metre);
      this.nud_SetupLength.Value = setupLength.ToString();
      //agrassi_FI30-05-08
      this.btn_ScorersOptions0.Visible = this.btn_ScorersOptions1.Visible = this.btn_ScorersOptions2.Visible = false;
      //ORDER 0
      if (int.Parse(row[Order.DEL_OUTS_0].ToString()) > 0)
      {
        ttl_ord0.ForeColor = Color.LightGreen;
        this.ttb_OrderNumber0.Value = row[Order.DEL_ORDER_NUMBER_0].ToString();
        this.mkt_Delivery0.ValueAsUNIVERSALDateTime = (DateTime)row[Order.DEL_DELIVERY_0];
        this.ttb_CustName0.Value = row[Order.DEL_CUSTOMER_NAME_0].ToString();
        this.nud_Width0.Value = row[Order.DEL_SHEET_WIDTH_0].ToString();
        this.nud_Boxes0.Value = row[Order.DEL_OUTS_0].ToString();
        this.ttb_Measures0.Value = Measures_dbToComponent(row[Order.DEL_SCORE_MEASURES_0].ToString());
        //agrassi_FI30-05-08
        //RFAZZI PROVA
        //this.btn_ScorersOptions0.Visible = this.ss.EnableScorerOptionsInsertion && this.ss.IsTwin400();
        this.btn_ScorersOptions0.Visible = this.ss.Twin400.EnableScorerOptionsInsertion && this.ss.IsTwin400();
        this.btn_ScorersOptions0.Visible = this.btn_ScorersOptions0.Visible &&
                  (!row[Order.DEL_SCORE_MEASURES_0].ToString().Equals(row[Order.DEL_SHEET_WIDTH_0].ToString()));
        this.cmb_Positioning0.SelectedItemData = row[Order.DEL_POSIT_TYPE_0].ToString();
        this.cmb_Type0.SelectedItem = row[Order.DEL_SCORE_TYPE_0].ToString();
        this.cmb_Level0.SelectedItemData = row[Order.DEL_LEVEL_NAME_0].ToString();
        this.nud_length0.Value = row[Order.DEL_SHEET_LENGTH_0].ToString();
        this.nud_SheetsDesired0.Value = row[Order.DEL_SEGMENT_SHEET_COUNT_0].ToString();
        this.nud_sheetsXstack0.Value = row[Order.DEL_SHEETS_PER_STACK_0].ToString();
        this.nud_stacksXpallet0.Value = row[Order.DEL_STACKS_PER_PALLET_0].ToString();
        this.cmb_sending0.SelectedItemData = row[Order.DEL_SENDING_0].ToString();
        this.chk_CutToMark0.Checked = Convert.ToBoolean(row[Order.DEL_CUT_TO_MARK_0].ToString());

        this.cmb_cutToPattern0.SelectedItem = row[Order.DEL_CUT_TO_PATTERN_0].ToString();
        this.ttb_DataToPrint0_1.Value = row[Order.DEL_DATA_TO_PRINT_0_1].ToString();
        this.ttb_DataToPrint0_2.Value = row[Order.DEL_DATA_TO_PRINT_0_2].ToString();
        this.ttb_DataToPrint0_3.Value = row[Order.DEL_DATA_TO_PRINT_0_3].ToString();
        this.ttb_DataToPrint0_4.Value = row[Order.DEL_DATA_TO_PRINT_0_4].ToString();
        this.chk_StackerContStack0.Checked = Convert.ToBoolean(row[Order.DEL_STACKER_CONT_STACK_0].ToString());
        this.chk_StackerPallet0.Checked = Convert.ToBoolean(row[Order.DEL_STACKER_PALLET_0].ToString());
        this.ttb_StackerNote0.Value = row[Order.DEL_STACKER_NOTE_0].ToString();
        this.ttb_CustAddress0.Value = row[Order.DEL_CUSTOMER_ADDRESS_0].ToString();
        this.ttb_MthCode0.Value = row[Order.DEL_MTH_CODE_0].ToString();
        this.ttb_MthNextMachine0.Value = row[Order.DEL_MTH_NEXT_MACHINE_0].ToString();
        //agrassi_FI24-02-2010_3
        this.cmb_Balance0.SelectedItemData =row[Order.DEL_BALANCE_0].ToString();
        if (row[Order.DEL_BALANCE_0].ToString().ToString() == Balance.Scrap.ToString())
          this.ttl_ord0.ForeColor = Color.Yellow;
        this.cmb_DischargeSide0.SelectedItemData = row[Order.DEL_DISCHARGE_SIDE_0].ToString();
        //agrassi_FI29-09-2010_1
        this.chk_Bundle0.Checked = bool.Parse(row[Order.DEL_BUNDLE_0].ToString());

      }
      //ORDER 1
      if (int.Parse(row[Order.DEL_OUTS_1].ToString()) > 0)
      {
        ttl_ord1.ForeColor = Color.LightGreen;
        this.ttb_OrderNumber1.Value = row[Order.DEL_ORDER_NUMBER_1].ToString();
        this.mkt_Delivery1.ValueAsUNIVERSALDateTime = (DateTime)row[Order.DEL_DELIVERY_1];
        this.ttb_CustName1.Value = row[Order.DEL_CUSTOMER_NAME_1].ToString();
        this.nud_Width1.Value = row[Order.DEL_SHEET_WIDTH_1].ToString();
        this.nud_Boxes1.Value = row[Order.DEL_OUTS_1].ToString();
        this.ttb_Measures1.Value = Measures_dbToComponent(row[Order.DEL_SCORE_MEASURES_1].ToString());
        //agrassi_FI30-05-08
        //RFAZZI PROVA
        //this.btn_ScorersOptions1.Visible = this.ss.EnableScorerOptionsInsertion && this.ss.IsTwin400();
        this.btn_ScorersOptions1.Visible = this.ss.Twin400.EnableScorerOptionsInsertion && this.ss.IsTwin400();
        this.btn_ScorersOptions1.Visible = this.btn_ScorersOptions1.Visible &&
                  (!row[Order.DEL_SCORE_MEASURES_1].ToString().Equals(row[Order.DEL_SHEET_WIDTH_1].ToString()));
        this.cmb_Positioning1.SelectedItemData = row[Order.DEL_POSIT_TYPE_1].ToString();
        this.cmb_Type1.SelectedItem = row[Order.DEL_SCORE_TYPE_1].ToString();
        this.cmb_Level1.SelectedItemData = row[Order.DEL_LEVEL_NAME_1].ToString();
        this.nud_length1.Value = row[Order.DEL_SHEET_LENGTH_1].ToString();
        this.nud_SheetsDesired1.Value = row[Order.DEL_SEGMENT_SHEET_COUNT_1].ToString();
        this.nud_sheetsXstack1.Value = row[Order.DEL_SHEETS_PER_STACK_1].ToString();
        this.nud_stacksXpallet1.Value = row[Order.DEL_STACKS_PER_PALLET_1].ToString();
        this.cmb_sending1.SelectedItemData = row[Order.DEL_SENDING_1].ToString();
        this.chk_CutToMark1.Checked = Convert.ToBoolean(row[Order.DEL_CUT_TO_MARK_1].ToString());

        this.cmb_cutToPattern1.SelectedItem = row[Order.DEL_CUT_TO_PATTERN_1].ToString();
        this.ttb_DataToPrint1_1.Value = row[Order.DEL_DATA_TO_PRINT_1_1].ToString();
        this.ttb_DataToPrint1_2.Value = row[Order.DEL_DATA_TO_PRINT_1_2].ToString();
        this.ttb_DataToPrint1_3.Value = row[Order.DEL_DATA_TO_PRINT_1_3].ToString();
        this.ttb_DataToPrint1_4.Value = row[Order.DEL_DATA_TO_PRINT_1_4].ToString();
        this.chk_StackerContStack1.Checked = Convert.ToBoolean(row[Order.DEL_STACKER_CONT_STACK_1].ToString());
        this.chk_StackerPallet1.Checked = Convert.ToBoolean(row[Order.DEL_STACKER_PALLET_1].ToString());
        this.ttb_StackerNote1.Value = row[Order.DEL_STACKER_NOTE_1].ToString();
        this.ttb_CustAddress1.Value = row[Order.DEL_CUSTOMER_ADDRESS_1].ToString();
        this.ttb_MthCode1.Value = row[Order.DEL_MTH_CODE_1].ToString();
        this.ttb_MthNextMachine1.Value = row[Order.DEL_MTH_NEXT_MACHINE_1].ToString();
        //agrassi_FI24-02-2010_3
        this.cmb_Balance1.SelectedItemData = row[Order.DEL_BALANCE_1].ToString();
        if (row[Order.DEL_BALANCE_1].ToString().ToString() == Balance.Scrap.ToString())
          this.ttl_ord1.ForeColor = Color.Yellow;
        this.cmb_DischargeSide1.SelectedItemData = row[Order.DEL_DISCHARGE_SIDE_1].ToString();
        //agrassi_FI29-09-2010_1
        this.chk_Bundle1.Checked = bool.Parse(row[Order.DEL_BUNDLE_1].ToString());
      }
      //ORDER 2
      if (int.Parse(row[Order.DEL_OUTS_2].ToString()) > 0)
      {
        ttl_ord2.ForeColor = Color.LightGreen;
        this.ttb_OrderNumber2.Value = row[Order.DEL_ORDER_NUMBER_2].ToString();
        this.mkt_Delivery2.ValueAsUNIVERSALDateTime = (DateTime)row[Order.DEL_DELIVERY_2];
        this.ttb_CustName2.Value = row[Order.DEL_CUSTOMER_NAME_2].ToString();
        this.nud_Width2.Value = row[Order.DEL_SHEET_WIDTH_2].ToString();
        this.nud_Boxes2.Value = row[Order.DEL_OUTS_2].ToString();
        this.ttb_Measures2.Value = Measures_dbToComponent(row[Order.DEL_SCORE_MEASURES_2].ToString());
        //agrassi_FI30-05-08
        //RFAZZI PROVA
        //this.btn_ScorersOptions2.Visible = this.ss.EnableScorerOptionsInsertion && this.ss.IsTwin400();
        this.btn_ScorersOptions2.Visible = this.ss.Twin400.EnableScorerOptionsInsertion && this.ss.IsTwin400();
        this.btn_ScorersOptions2.Visible = this.btn_ScorersOptions2.Visible &&
                  (!row[Order.DEL_SCORE_MEASURES_2].ToString().Equals(row[Order.DEL_SHEET_WIDTH_2].ToString()));
        this.cmb_Positioning2.SelectedItemData = row[Order.DEL_POSIT_TYPE_2].ToString();
        this.cmb_Type2.SelectedItem = row[Order.DEL_SCORE_TYPE_2].ToString();
        this.cmb_Level2.SelectedItemData = row[Order.DEL_LEVEL_NAME_2].ToString();
        this.nud_length2.Value = row[Order.DEL_SHEET_LENGTH_2].ToString();
        this.nud_SheetsDesired2.Value = row[Order.DEL_SEGMENT_SHEET_COUNT_2].ToString();
        this.nud_sheetsXstack2.Value = row[Order.DEL_SHEETS_PER_STACK_2].ToString();
        this.nud_stacksXpallet2.Value = row[Order.DEL_STACKS_PER_PALLET_2].ToString();
        this.cmb_sending2.SelectedItemData = row[Order.DEL_SENDING_2].ToString();
        this.chk_CutToMark2.Checked = Convert.ToBoolean(row[Order.DEL_CUT_TO_MARK_2].ToString());

        this.cmb_cutToPattern2.SelectedItem = row[Order.DEL_CUT_TO_PATTERN_2].ToString();
        this.ttb_DataToPrint2_1.Value = row[Order.DEL_DATA_TO_PRINT_2_1].ToString();
        this.ttb_DataToPrint2_2.Value = row[Order.DEL_DATA_TO_PRINT_2_2].ToString();
        this.ttb_DataToPrint2_3.Value = row[Order.DEL_DATA_TO_PRINT_2_3].ToString();
        this.ttb_DataToPrint2_4.Value = row[Order.DEL_DATA_TO_PRINT_2_4].ToString();
        this.chk_StackerContStack2.Checked = Convert.ToBoolean(row[Order.DEL_STACKER_CONT_STACK_2].ToString());
        this.chk_StackerPallet2.Checked = Convert.ToBoolean(row[Order.DEL_STACKER_PALLET_2].ToString());
        this.ttb_StackerNote2.Value = row[Order.DEL_STACKER_NOTE_2].ToString();
        this.ttb_CustAddress2.Value = row[Order.DEL_CUSTOMER_ADDRESS_2].ToString();
        this.ttb_MthCode2.Value = row[Order.DEL_MTH_CODE_2].ToString();
        this.ttb_MthNextMachine2.Value = row[Order.DEL_MTH_NEXT_MACHINE_2].ToString();
        //agrassi_FI24-02-2010_3
        this.cmb_Balance2.SelectedItemData = row[Order.DEL_BALANCE_2].ToString();
        if (row[Order.DEL_BALANCE_2].ToString().ToString() == Balance.Scrap.ToString())
          this.ttl_ord2.ForeColor = Color.Yellow;
        this.cmb_DischargeSide2.SelectedItemData = row[Order.DEL_DISCHARGE_SIDE_2].ToString();
        //agrassi_FI29-09-2010_1
        this.chk_Bundle2.Checked = bool.Parse(row[Order.DEL_BUNDLE_2].ToString());
      }
    }

    /// <summary>
    /// Prepares a string measure as it is stored in the database to be passed to a titledTextBox to be showed
    /// </summary>
    private string Measures_dbToComponent(string measures)
    {
      string s = SyncroConverter.ConvertScoreMeasures(measures, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
      s = s.Replace("   ", " ");
      s = s.Replace("  ", " ");
      s = s.Replace(" ", "\r\n");
      return s;
    }

    /// <summary>
    /// prepares a string measure taken from a TitledTextBox to be stored into the database.
    /// </summary>
    private string Measures_componentToDb(string measures)
    {
      string s = SyncroConverter.ConvertScoreMeasures(
          measures, UnitMeasure.MilliMetre, UnitMeasure.DeciMilliMetre);
      s = s.Replace('\n', ' ');
      s = s.Replace('\r', ' ');
      s = s.Replace("   ", " ");
      s = s.Replace("  ", " ");
      return NotEmptyString(s);
    }

    private void tab_Tab3_Leave(object sender, EventArgs e)
    {
      if (state.Equals(State.Read) && (nextState.Equals(State.Read)))
        tab_Tab3.Focus();
    }

    private void Frm_QuickInsDryOrder_Activated(object sender, EventArgs e)
    {
      if (this.state == State.Read)
      {
        this.showOrder(Frm_OrdersInsSubGroup.indexOrderToShowDRY);
        this.tab_Tab3.Focus();
      }
    }
    //agrassi_FI30-05-08
    private void btn_ScorersOptions0_Click(object sender, EventArgs e)
    {
      string scoreString = this.Measures_componentToDb(this.ttb_Measures0.Value);
      Frm_ScorersOptions frm_options = new Frm_ScorersOptions();
      FosberDialogResult res = frm_options.ShowDialog(this, "1", scoreString, this.state, this.nextState,
                                                      this.cmb_Type0.SelectedItem, this.cmb_Positioning0.SelectedItemData,
                                                      this.nud_ScoreGap.Value, this.nud_Width0.Value,
                                                      new Frm_ScorersOptions.SetScorers(this.setListScorerItems),
                                                      new Frm_ScorersOptions.SetPositionings(this.setListPositioningItems),
                                                      new Frm_ScorersOptions.SetList(this.setTitledListItems));
      if ((this.state != State.Read) && (res.Dialog == DialogResult.OK))
        this.ttb_Measures0.Value = this.Measures_dbToComponent(res.Data[0].ToString());
    }

    private void btn_ScorersOptions1_Click(object sender, EventArgs e)
    {
      string scoreString = this.Measures_componentToDb(this.ttb_Measures1.Value);
      Frm_ScorersOptions frm_options = new Frm_ScorersOptions();
      FosberDialogResult res = frm_options.ShowDialog(this, "2", scoreString, this.state, this.nextState,
                                                      this.cmb_Type1.SelectedItem, this.cmb_Positioning1.SelectedItemData,
                                                      this.nud_ScoreGap.Value, this.nud_Width1.Value,
                                                      new Frm_ScorersOptions.SetScorers(this.setListScorerItems),
                                                      new Frm_ScorersOptions.SetPositionings(this.setListPositioningItems),
                                                      new Frm_ScorersOptions.SetList(this.setTitledListItems));
      if ((this.state != State.Read) && (res.Dialog == DialogResult.OK))
        this.ttb_Measures1.Value = this.Measures_dbToComponent(res.Data[0].ToString());
    }

    private void btn_ScorersOptions2_Click(object sender, EventArgs e)
    {
      string scoreString = this.Measures_componentToDb(this.ttb_Measures2.Value);
      Frm_ScorersOptions frm_options = new Frm_ScorersOptions();
      FosberDialogResult res = frm_options.ShowDialog(this, "3", scoreString, this.state, this.nextState,
                                                      this.cmb_Type2.SelectedItem, this.cmb_Positioning2.SelectedItemData,
                                                      this.nud_ScoreGap.Value, this.nud_Width2.Value,
                                                      new Frm_ScorersOptions.SetScorers(this.setListScorerItems),
                                                      new Frm_ScorersOptions.SetPositionings(this.setListPositioningItems),
                                                      new Frm_ScorersOptions.SetList(this.setTitledListItems));
      if ((this.state != State.Read) && (res.Dialog == DialogResult.OK))
        this.ttb_Measures2.Value = this.Measures_dbToComponent(res.Data[0].ToString());
    }
  }
}

