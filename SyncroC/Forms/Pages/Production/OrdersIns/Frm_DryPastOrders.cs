using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Controls;
using SyncroC.DataBase.EventArgs;
using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Controls.Colors;

using SyncroC.Controls.Interfaces;

namespace SyncroC.Forms.Pages.Production.OrdersIns
{
	public partial class Frm_DryPastOrders : Frm_OrdersInsSubGroup
	{
		//private DataTable quality;
		//private DataTable flute;
    //private DataTable cutToPattern;

		public Frm_DryPastOrders()
		{
			InitializeComponent();

			//this.quality = DBManager.getDBManager().quality.quality.Tables[Quality.QUALITY_LIST];
			//this.flute = DBManager.getDBManager().flute.flute.Tables[Flute.FLUTE_LIST];
      //this.cutToPattern = DBManager.getDBManager().cutToPattern.cutToPattern.Tables[CutToPattern.CUTTOPATTERN_LIST];

      //KNIFE
			//RFAZZI_FI.Changed the above value for level
			//this.chk_CutToMark0.Visible = this.chk_CutToMark1.Visible = this.chk_CutToMark2.Visible = this.knife.CutToMark;
			//this.cmb_cutToPattern0.Visible = this.cmb_cutToPattern1.Visible = this.cmb_cutToPattern2.Visible = this.knife.CutToPattern;
			//this.ttl_KnifeData0.Visible = this.ttl_KnifeData1.Visible = this.ttl_KnifeData2.Visible = (this.knife.CutToMark || this.knife.CutToPattern);
			this.chk_CutToMark0.Visible = this.chk_CutToMark1.Visible = this.chk_CutToMark2.Visible = (	DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].CutToMark || 
																																																	DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].CutToMark || 
																																																	DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].CutToMark);
			this.cmb_cutToPattern0.Visible = this.cmb_cutToPattern1.Visible = this.cmb_cutToPattern2.Visible = (DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].CutToPattern || 
																																																					DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].CutToPattern || 
																																																					DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].CutToPattern );
			this.ttl_KnifeData0.Visible = this.ttl_KnifeData1.Visible = this.ttl_KnifeData2.Visible = (	DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].CutToMark || 
																																																	DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].CutToMark || 
																																																	DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].CutToMark || 
																																																	DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].CutToPattern || 
																																																	DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].CutToPattern || 
																																																	DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].CutToPattern);

      //RFAZZI_FI12/06/2008_1.Added eng.verb. for Continuous Stack and Pallet Insert into Stacker section	
      this.chk_StackerContStack0.Visible = this.chk_StackerContStack1.Visible = this.chk_StackerContStack2.Visible = (DBManager.getDBManager().lineConfig.Stacker1Config.ContinuousStack ||
                                                                                                                        DBManager.getDBManager().lineConfig.Stacker2Config.ContinuousStack ||
                                                                                                                        DBManager.getDBManager().lineConfig.Stacker3Config.ContinuousStack);
      this.chk_StackerPallet0.Visible = this.chk_StackerPallet1.Visible = this.chk_StackerPallet2.Visible = (DBManager.getDBManager().lineConfig.Stacker1Config.PalletInsertion ||
                                                                                                              DBManager.getDBManager().lineConfig.Stacker2Config.PalletInsertion ||
                                                                                                              DBManager.getDBManager().lineConfig.Stacker3Config.PalletInsertion);
      this.cutToPattern_RowFill(null, null);
			this.quality_RowFill(null, null);
			this.setListFluteItems(-1, true);

			this.setTitledListItems(this.cmb_TrimType, typeof(SyncroC.Utility.TrimType), new bool[] { true, true }, -1, true);
			//Order 0
      this.setTitledListItems(this.cmb_Level0, typeof(SyncroC.Utility.LevelName), new bool[] { false, true, (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount == 3), (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount >= 2) }, -1, true);
			this.setTitledListItems(this.cmb_sending0, typeof(SyncroC.Utility.PalletSending), new bool[] { true, true, true }, -1, true);
			this.setListPositioningItems(this.cmb_Positioning0, -1, true);
			this.setListScorerItems(this.cmb_Type0, null, -1, true);
      //agrassi_FI24-02-2010_3
      setTitledListItems(cmb_DischargeSide0, typeof(SyncroC.Utility.DischargeSide), new bool[] { true, true, true }, 0, true);
      setTitledListItems(cmb_Balance0, typeof(SyncroC.Utility.Balance), new bool[] { true, true, true }, 0, true);
      //Order 1
      this.setTitledListItems(this.cmb_Level1, typeof(SyncroC.Utility.LevelName), new bool[] { false, true, (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount == 3), (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount >= 2) }, -1, true);
			this.setTitledListItems(this.cmb_sending1, typeof(SyncroC.Utility.PalletSending), new bool[] { true, true, true }, -1, true);
			this.setListPositioningItems(this.cmb_Positioning1, -1, true);
			this.setListScorerItems(this.cmb_Type1, null, -1, true);
      //agrassi_FI24-02-2010_3
      setTitledListItems(cmb_DischargeSide1, typeof(SyncroC.Utility.DischargeSide), new bool[] { true, true, true }, 0, true);
      setTitledListItems(cmb_Balance1, typeof(SyncroC.Utility.Balance), new bool[] { true, true, true }, 0, true);
      //Order 2
      this.setTitledListItems(this.cmb_Level2, typeof(SyncroC.Utility.LevelName), new bool[] { false, true, (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount == 3), (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount >= 2) }, -1, true);
			this.setTitledListItems(this.cmb_sending2, typeof(SyncroC.Utility.PalletSending), new bool[] { true, true, true }, -1, true);
			this.setListPositioningItems(this.cmb_Positioning2, -1, true);
			this.setListScorerItems(this.cmb_Type2, null, -1, true);
      //agrassi_FI24-02-2010_3
      setTitledListItems(cmb_DischargeSide2, typeof(SyncroC.Utility.DischargeSide), new bool[] { true, true, true }, 0, true);
      setTitledListItems(cmb_Balance2, typeof(SyncroC.Utility.Balance), new bool[] { true, true, true }, 0, true);

      //quality.RowChanged += new DataRowChangeEventHandler(quality_RowFill);
      //quality.RowDeleted += new DataRowChangeEventHandler(quality_RowFill);

      DBManager.getDBManager().quality.OnDBQualityChange += new Quality.DBChangedHandler(quality_RowFill);

      //cutToPattern.RowChanged += new DataRowChangeEventHandler(cutToPattern_RowFill);
      //cutToPattern.RowDeleted += new DataRowChangeEventHandler(cutToPattern_RowFill);

      DBManager.getDBManager().cutToPattern.OnDBCutToPatternChange += new CutToPattern.DBChangedHandler(cutToPattern_RowFill);
		}

		protected override void F13()
		{
		}

    /// <summary>
    /// Show / Hide debug info.
    /// </summary>
    protected override void ControlD()
    {
      base.ControlD();
      this.pnl_Debug.Visible = !this.pnl_Debug.Visible;
    }

    /// <summary>
    /// Fill the items of the CutToPattern list using the flute database.
    /// </summary>
    /// <param name="indexToSelect">Index of the item to select into the list.</param>
    /// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
    private void cutToPattern_RowFill(object sender, DBChangedEventArgs e)
    {
      DataTable cutToPattern = DBManager.getDBManager().cutToPattern.getTable();

      if (cutToPattern.Rows.Count == 0)
      {
        this.cmb_cutToPattern0.ClearItems();
        this.cmb_cutToPattern1.ClearItems();
        this.cmb_cutToPattern2.ClearItems();
        return;
      }

      string[] stringRows = new string[cutToPattern.Rows.Count];
      int i = 0;
      foreach (System.Data.DataRow r in cutToPattern.Rows)
        stringRows[i++] = r[CutToPattern.PATTERN_ID].ToString();

      this.cmb_cutToPattern0.AddRangeItem(stringRows);
      this.cmb_cutToPattern1.AddRangeItem(stringRows);
      this.cmb_cutToPattern2.AddRangeItem(stringRows);
    }

    void quality_RowFill(object sender, DBChangedEventArgs e)
		{
      //agrassi_FI07-09-09_2
      try
      {
        DataTable quality = DBManager.getDBManager().quality.getTable();
        //Create the quality list
        this.cmb_Quality.ClearItems();
        string[] stringRows = new string[quality.Rows.Count];
        int i = 0;
        foreach (System.Data.DataRow r in quality.Rows)
          stringRows[i++] = r[Quality.QUALITY_ID].ToString();
        this.cmb_Quality.AddRangeItem(stringRows);
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "Error in quality_RowFill:" + ex.Message);
      }
		}

		/// <summary>
		/// Show previous order.
		/// </summary>
		protected override void PageUp()
		{
			if (Frm_OrdersInsSubGroup.positionPastOrderToShowDRY == 0)
				return;
      this.showOrder(--Frm_OrdersInsSubGroup.positionPastOrderToShowDRY, Frm_OrdersInsSubGroup.PastOrderfiltro);
		}

		/// <summary>
		/// Show next order.
		/// </summary>
		protected override void PageDown()
		{
      if (Frm_OrdersInsSubGroup.positionPastOrderToShowDRY >= DBManager.getDBManager().pastOrder.getCount(Frm_OrdersInsSubGroup.PastOrderfiltro))
				return;
      this.showOrder(++Frm_OrdersInsSubGroup.positionPastOrderToShowDRY, Frm_OrdersInsSubGroup.PastOrderfiltro);
		}

		/// <summary>
		/// Show the specified order into the screen.
		/// </summary>
		/// <param name="position">Position of the order to be showed.</param>
		private void showOrder(int position, string filtro)
		{
      long count = DBManager.getDBManager().pastOrder.getCount(filtro);

			this.cleanAllFields(false);

      if (count == 0)
			{
				Logger.LogError(this, "showOrder - PastOrder queue is empty.");
				return;
			}
			if (position < 1)
			{
				position = 1;
			}
      if (position > count)
			{
        position = (int)count;
			}

      //ABELLO_FI25/08/2010_1 se uso Postgres il doppio click sulla lista degli eseguiti andava a casaccio
      DataRow row = DBManager.getDBManager().pastOrder.getRow(position, filtro);

			if (row != null)
			{
        this.ttb_index.Value = position.ToString();

        //DEBUG INFO
        this.lbl_DebugSetupID.Text = this.lbl_DebugSetupID.Text = row[PastOrder.POL_SETUP_ID].ToString();
        this.lbl_DebugSetupModId.Text = this.lbl_DebugSetupModId.Text = row[PastOrder.POL_SETUP_MODID].ToString();
        this.lbl_DebugPalletPrint0.Text = this.lbl_DebugPalletPrint0.Text.Substring(0, this.lbl_DebugPalletPrint0.Text.IndexOf('>') + 1) + " " + row[PastOrder.POL_PALLET_LAYOUT_0].ToString() + " " + row[PastOrder.POL_AMOUNT_PALLET_COPY_0].ToString();
        this.lbl_DebugPalletPrint1.Text = this.lbl_DebugPalletPrint1.Text.Substring(0, this.lbl_DebugPalletPrint1.Text.IndexOf('>') + 1) + " " + row[PastOrder.POL_PALLET_LAYOUT_1].ToString() + " " + row[PastOrder.POL_AMOUNT_PALLET_COPY_1].ToString();
        this.lbl_DebugPalletPrint2.Text = this.lbl_DebugPalletPrint2.Text.Substring(0, this.lbl_DebugPalletPrint2.Text.IndexOf('>') + 1) + " " + row[PastOrder.POL_PALLET_LAYOUT_2].ToString() + " " + row[PastOrder.POL_AMOUNT_PALLET_COPY_2].ToString();
        this.lbl_DebugPastPrint0.Text = this.lbl_DebugPastPrint0.Text.Substring(0, this.lbl_DebugPastPrint0.Text.IndexOf('>') + 1) + " " + row[PastOrder.POL_PASTORDER_LAYOUT_0].ToString() + " " + row[PastOrder.POL_AMOUNT_PASTORDER_COPY_0].ToString();
        this.lbl_DebugPastPrint1.Text = this.lbl_DebugPastPrint1.Text.Substring(0, this.lbl_DebugPastPrint1.Text.IndexOf('>') + 1) + " " + row[PastOrder.POL_PASTORDER_LAYOUT_1].ToString() + " " + row[PastOrder.POL_AMOUNT_PASTORDER_COPY_1].ToString();
        this.lbl_DebugPastPrint2.Text = this.lbl_DebugPastPrint2.Text.Substring(0, this.lbl_DebugPastPrint2.Text.IndexOf('>') + 1) + " " + row[PastOrder.POL_PASTORDER_LAYOUT_2].ToString() + " " + row[PastOrder.POL_AMOUNT_PASTORDER_COPY_2].ToString();
        
        //HEADER
				string setUpCode = row[PastOrder.POL_SETUP_CODE].ToString();
				ttb_ProgramNumber.Value = setUpCode.Substring(0, setUpCode.Length - 2);
				ttb_SetUpSequence.Value = setUpCode.Substring(setUpCode.Length - 2);
				this.nud_WebWidth.Value = row[PastOrder.POL_WEB_WIDTH].ToString();
				this.cmb_TrimType.SelectedItemData = row[PastOrder.POL_TRIM_TYPE].ToString();
				this.nud_ScoreGap.Value = row[PastOrder.POL_SCORE_GAP].ToString();
				DataRow qualityRow = DBManager.getDBManager().quality.getRow(row[PastOrder.POL_QUALITY_ID].ToString());
				if (qualityRow != null)
					this.cmb_Flute.SelectedItem = qualityRow[Quality.FLUTE_ID].ToString();
				this.cmb_Quality.SelectedItem = row[PastOrder.POL_QUALITY_ID].ToString();
				this.nud_Trim.Value = row[PastOrder.POL_TRIM].ToString();
				int setupLength = this.intParse(row[PastOrder.POL_SHEET_LENGTH_0].ToString()) *
					 this.intParse(row[PastOrder.POL_SEGMENT_SHEET_COUNT_0].ToString()) / this.intParse(row[PastOrder.POL_OUTS_0].ToString());
				setupLength = (int)SyncroConverter.Convert(setupLength, UnitMeasure.DeciMilliMetre, UnitMeasure.Metre);
				this.nud_SetupLength.Value = setupLength.ToString();
				this.mkt_BeginTime.ValueAsUNIVERSALDateTime = (DateTime)row[PastOrder.POL_BEGIN_TIME];
				string formattedTime = string.Empty;
				try
				{
					formattedTime = SyncroConverter.ConvertToFormattedTime(Convert.ToInt32(row[PastOrder.POL_STOPTIME].ToString()));
				}
				catch (Exception e)
				{
					Logger.LogError(this, "showOrder - error formatting time", e);
				}

        //ORDER 0
				if (int.Parse(row[PastOrder.POL_OUTS_0].ToString()) > 0)
				{
					ttl_ord0.ForeColor = Color.LightGreen;
					this.ttb_OrderNumber0.Value = row[PastOrder.POL_ORDER_NUMBER_0].ToString();
					this.mkt_Delivery0.ValueAsUNIVERSALDateTime = (DateTime)row[PastOrder.POL_DELIVERY_0];
					this.ttb_CustName0.Value = row[PastOrder.POL_CUSTOMER_NAME_0].ToString();
					this.ttb_Measures0.Value = Measures_dbToComponent(row[PastOrder.POL_SCORE_MEASURES_0].ToString());
					this.cmb_Positioning0.SelectedItemData = row[PastOrder.POL_POSIT_TYPE_0].ToString();
					this.cmb_Type0.SelectedItem = row[PastOrder.POL_SCORE_TYPE_0].ToString();
					this.cmb_Level0.SelectedItemData = row[PastOrder.POL_LEVEL_NAME_0].ToString();
					this.nud_sheetsXstack0.Value = row[PastOrder.POL_SHEETS_PER_STACK_0].ToString();
					this.nud_stacksXpallet0.Value = row[PastOrder.POL_STACKS_PER_PALLET_0].ToString();
					this.cmb_sending0.SelectedItemData = row[PastOrder.POL_SENDING_0].ToString();
					nud_Width0.Value = row[PastOrder.POL_SHEET_WIDTH_0].ToString();
					nud_length0.Value = row[PastOrder.POL_SHEET_LENGTH_0].ToString();
					nud_Boxes0.Value = row[PastOrder.POL_OUTS_0].ToString();
					nud_SheetsDesired0.Value = row[PastOrder.POL_SEGMENT_SHEET_COUNT_0].ToString();
					this.chk_CutToMark0.Checked = Convert.ToBoolean(row[PastOrder.POL_CUT_TO_MARK_0].ToString());
          this.cmb_cutToPattern0.SelectedItem = row[PastOrder.POL_CUT_TO_PATTERN_0].ToString();
          this.ttb_DataToPrint0_1.Value = row[PastOrder.POL_DATA_TO_PRINT_0_1].ToString();
          this.ttb_DataToPrint0_2.Value = row[PastOrder.POL_DATA_TO_PRINT_0_2].ToString();
          this.ttb_DataToPrint0_3.Value = row[PastOrder.POL_DATA_TO_PRINT_0_3].ToString();
          this.ttb_DataToPrint0_4.Value = row[PastOrder.POL_DATA_TO_PRINT_0_4].ToString();
          this.chk_StackerContStack0.Checked = Convert.ToBoolean(row[PastOrder.POL_STACKER_CONT_STACK_0].ToString());
          this.chk_StackerPallet0.Checked = Convert.ToBoolean(row[PastOrder.POL_STACKER_PALLET_0].ToString());
          this.ttb_StackerNote0.Value = row[PastOrder.POL_STACKER_NOTE_0].ToString();
          this.ttb_CustAddress0.Value = row[PastOrder.POL_CUSTOMER_ADDRESS_0].ToString();
          this.ttb_MthCode0.Value = row[PastOrder.POL_MTH_CODE_0].ToString();
          this.ttb_MthNextMachine0.Value = row[PastOrder.POL_MTH_NEXT_MACHINE_0].ToString();
          //agrassi_FI24-02-2010_3
          this.cmb_Balance0.SelectedItemData = row[PastOrder.POL_BALANCE_0].ToString();
          if (row[PastOrder.POL_BALANCE_0].ToString().ToString() == Balance.Scrap.ToString())
            this.ttl_ord0.ForeColor = Color.Yellow;
          this.cmb_DischargeSide0.SelectedItemData = row[PastOrder.POL_DISCHARGE_SIDE_0].ToString();
        }

				//ORDER 1
				if (int.Parse(row[PastOrder.POL_OUTS_1].ToString()) > 0)
				{
					ttl_ord1.ForeColor = Color.LightGreen;
					this.ttb_OrderNumber1.Value = row[PastOrder.POL_ORDER_NUMBER_1].ToString();
					this.mkt_Delivery1.ValueAsUNIVERSALDateTime = (DateTime)row[PastOrder.POL_DELIVERY_1];
					this.ttb_CustName1.Value = row[PastOrder.POL_CUSTOMER_NAME_1].ToString();
					this.ttb_Measures1.Value = Measures_dbToComponent(row[PastOrder.POL_SCORE_MEASURES_1].ToString());
					this.cmb_Positioning1.SelectedItemData = row[PastOrder.POL_POSIT_TYPE_1].ToString();
					this.cmb_Type1.SelectedItem = row[PastOrder.POL_SCORE_TYPE_1].ToString();
					this.cmb_Level1.SelectedItemData = row[PastOrder.POL_LEVEL_NAME_1].ToString();
					this.nud_sheetsXstack1.Value = row[PastOrder.POL_SHEETS_PER_STACK_1].ToString();
					this.nud_stacksXpallet1.Value = row[PastOrder.POL_STACKS_PER_PALLET_1].ToString();
					this.cmb_sending1.SelectedItemData = row[PastOrder.POL_SENDING_1].ToString();
					nud_Width1.Value = row[PastOrder.POL_SHEET_WIDTH_1].ToString();
					nud_length1.Value = row[PastOrder.POL_SHEET_LENGTH_1].ToString();
					nud_Boxes1.Value = row[PastOrder.POL_OUTS_1].ToString();
					nud_SheetsDesired1.Value = row[PastOrder.POL_SEGMENT_SHEET_COUNT_1].ToString();
					this.chk_CutToMark1.Checked = Convert.ToBoolean(row[PastOrder.POL_CUT_TO_MARK_1].ToString());
          this.cmb_cutToPattern1.SelectedItem = row[PastOrder.POL_CUT_TO_PATTERN_1].ToString();
          this.ttb_DataToPrint1_1.Value = row[PastOrder.POL_DATA_TO_PRINT_1_1].ToString();
          this.ttb_DataToPrint1_2.Value = row[PastOrder.POL_DATA_TO_PRINT_1_2].ToString();
          this.ttb_DataToPrint1_3.Value = row[PastOrder.POL_DATA_TO_PRINT_1_3].ToString();
          this.ttb_DataToPrint1_4.Value = row[PastOrder.POL_DATA_TO_PRINT_1_4].ToString();
          this.chk_StackerContStack1.Checked = Convert.ToBoolean(row[PastOrder.POL_STACKER_CONT_STACK_1].ToString());
          this.chk_StackerPallet1.Checked = Convert.ToBoolean(row[PastOrder.POL_STACKER_PALLET_1].ToString());
          this.ttb_StackerNote1.Value = row[PastOrder.POL_STACKER_NOTE_1].ToString();
          this.ttb_CustAddress1.Value = row[PastOrder.POL_CUSTOMER_ADDRESS_1].ToString();
          this.ttb_MthCode1.Value = row[PastOrder.POL_MTH_CODE_1].ToString();
          this.ttb_MthNextMachine1.Value = row[PastOrder.POL_MTH_NEXT_MACHINE_1].ToString();
          //agrassi_FI24-02-2010_3
          this.cmb_Balance1.SelectedItemData = row[PastOrder.POL_BALANCE_1].ToString();
          if (row[PastOrder.POL_BALANCE_1].ToString().ToString() == Balance.Scrap.ToString())
            this.ttl_ord1.ForeColor = Color.Yellow;
          this.cmb_DischargeSide1.SelectedItemData = row[PastOrder.POL_DISCHARGE_SIDE_1].ToString();
        }

				//ORDER 2
				if (int.Parse(row[PastOrder.POL_OUTS_2].ToString()) > 0)
				{
					ttl_ord2.ForeColor = Color.LightGreen;
					this.ttb_OrderNumber2.Value = row[PastOrder.POL_ORDER_NUMBER_2].ToString();
					this.mkt_Delivery2.ValueAsUNIVERSALDateTime = (DateTime)row[PastOrder.POL_DELIVERY_2];
					this.ttb_CustName2.Value = row[PastOrder.POL_CUSTOMER_NAME_2].ToString();
					this.ttb_Measures2.Value = Measures_dbToComponent(row[PastOrder.POL_SCORE_MEASURES_2].ToString());
					this.cmb_Positioning2.SelectedItemData = row[PastOrder.POL_POSIT_TYPE_2].ToString();
					this.cmb_Type2.SelectedItem = row[PastOrder.POL_SCORE_TYPE_2].ToString();
					this.cmb_Level2.SelectedItemData = row[PastOrder.POL_LEVEL_NAME_2].ToString();
					this.nud_sheetsXstack2.Value = row[PastOrder.POL_SHEETS_PER_STACK_2].ToString();
					this.nud_stacksXpallet2.Value = row[PastOrder.POL_STACKS_PER_PALLET_2].ToString();
					this.cmb_sending2.SelectedItemData = row[PastOrder.POL_SENDING_2].ToString();
					nud_Width2.Value = row[PastOrder.POL_SHEET_WIDTH_2].ToString();
					nud_length2.Value = row[PastOrder.POL_SHEET_LENGTH_2].ToString();
					nud_Boxes2.Value = row[PastOrder.POL_OUTS_2].ToString();
					nud_SheetsDesired2.Value = row[PastOrder.POL_SEGMENT_SHEET_COUNT_2].ToString();
					this.chk_CutToMark2.Checked = Convert.ToBoolean(row[PastOrder.POL_CUT_TO_MARK_2].ToString());
          this.cmb_cutToPattern2.SelectedItem = row[PastOrder.POL_CUT_TO_PATTERN_2].ToString();
          this.ttb_DataToPrint2_1.Value = row[PastOrder.POL_DATA_TO_PRINT_2_1].ToString();
          this.ttb_DataToPrint2_2.Value = row[PastOrder.POL_DATA_TO_PRINT_2_2].ToString();
          this.ttb_DataToPrint2_3.Value = row[PastOrder.POL_DATA_TO_PRINT_2_3].ToString();
          this.ttb_DataToPrint2_4.Value = row[PastOrder.POL_DATA_TO_PRINT_2_4].ToString();
          this.chk_StackerContStack2.Checked = Convert.ToBoolean(row[PastOrder.POL_STACKER_CONT_STACK_2].ToString());
          this.chk_StackerPallet2.Checked = Convert.ToBoolean(row[PastOrder.POL_STACKER_PALLET_2].ToString());
          this.ttb_StackerNote2.Value = row[PastOrder.POL_STACKER_NOTE_2].ToString();
          this.ttb_CustAddress2.Value = row[PastOrder.POL_CUSTOMER_ADDRESS_2].ToString();
          this.ttb_MthCode2.Value = row[PastOrder.POL_MTH_CODE_2].ToString();
          this.ttb_MthNextMachine2.Value = row[PastOrder.POL_MTH_NEXT_MACHINE_2].ToString();
          //agrassi_FI24-02-2010_3
          this.cmb_Balance2.SelectedItemData = row[PastOrder.POL_BALANCE_2].ToString();
          if (row[PastOrder.POL_BALANCE_2].ToString().ToString() == Balance.Scrap.ToString())
            this.ttl_ord2.ForeColor = Color.Yellow;
          this.cmb_DischargeSide2.SelectedItemData = row[PastOrder.POL_DISCHARGE_SIDE_2].ToString();
        }
			}
		}

		/// <summary>
		/// Clean the value of every component of the page.
		/// </summary>
		private void cleanAllFields(bool cleanLists)
		{
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
			nud_sheetsXstack0.Value = String.Empty;
			nud_stacksXpallet0.Value = String.Empty;
			cmb_Level0.UnSelectItem();
			cmb_Positioning0.UnSelectItem();
			cmb_Type0.UnSelectItem();
			ttb_Measures0.Value = String.Empty;
			nud_Width0.Value = String.Empty;
			nud_length0.Value = String.Empty;
			nud_Boxes0.Value = String.Empty;
			nud_SheetsDesired0.Value = String.Empty;
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

      //ORDER 1
			ttl_ord1.ForeColor = Color.WhiteSmoke;
			ttb_OrderNumber1.Value = String.Empty;
			ttb_CustName1.Value = String.Empty;
			cmb_sending1.UnSelectItem();
			mkt_Delivery1.Value = String.Empty;
			nud_sheetsXstack1.Value = String.Empty;
			nud_stacksXpallet1.Value = String.Empty;
			cmb_Level1.UnSelectItem();
			cmb_Positioning1.UnSelectItem();
			cmb_Type1.UnSelectItem();
			ttb_Measures1.Value = String.Empty;
			nud_Width1.Value = String.Empty;
			nud_length1.Value = String.Empty;
			nud_Boxes1.Value = String.Empty;
			nud_SheetsDesired1.Value = String.Empty;
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

			//ORDER 2
			ttl_ord2.ForeColor = Color.WhiteSmoke;
			ttb_OrderNumber2.Value = String.Empty;
			ttb_CustName2.Value = String.Empty;
			cmb_sending2.UnSelectItem();
			mkt_Delivery2.Value = String.Empty;
			nud_sheetsXstack2.Value = String.Empty;
			nud_stacksXpallet2.Value = String.Empty;
			cmb_Level2.UnSelectItem();
			cmb_Positioning2.UnSelectItem();
			cmb_Type2.UnSelectItem();
			ttb_Measures2.Value = String.Empty;
			nud_Width2.Value = String.Empty;
			nud_length2.Value = String.Empty;
			nud_Boxes2.Value = String.Empty;
			nud_SheetsDesired2.Value = String.Empty;
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
      ScoreType[] scoreType = DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.ScoreTypes;
			string[] positTypes = Enum.GetNames(typeof(SyncroC.Utility.PositType));
			for (int i = 0; i < scoreType.Length; i++)
			{
				for (int j = 1; j < positTypes.Length; j++) //j starts from 1 because the 0 enum is "NoPositType"
				{
					if (ScorerProfileType.IsPositioningAvailable(scoreType[i].Scorer.SClass, (PositType)j) && (!titledList.Contains(positTypes[j])))
            if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.PositioningOptions.Scores.ShowNormalReverse || ((PositType)j != PositType.NormalReverse))
							titledList.AddItem(lang.getTranslation(typeName + positTypes[j]), positTypes[j]);

				}
			}
			if (indexToSelect >= 0 && indexToSelect <= titledList.Count - 1 && Clean)
				titledList.SelectedIndex = indexToSelect;
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
			if (SelectedPositType == null || SelectedPositType.Equals(String.Empty))
				all = true;
			else
				positType = (PositType)Enum.Parse(typeof(SyncroC.Utility.PositType), SelectedPositType);
      ScoreType[] scoreType = DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.ScoreTypes;
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

		private void Frm_DryPastOrders_Activated(object sender, EventArgs e)
		{
      this.showOrder(Frm_OrdersInsSubGroup.positionPastOrderToShowDRY, Frm_OrdersInsSubGroup.PastOrderfiltro);
		}

	}
}