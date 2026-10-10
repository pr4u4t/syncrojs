using System;
using System.Data;
using System.Security;
using System.IO;
using System.Windows.Forms;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.DataBase
{
	/// <summary>
	/// Summary description for Order.
	/// </summary>
	/// 

	//RFAZZI_FI03/09/2008_1.Changed the SlitterScorer lineConfig structure for old Fosber SS machines.

	public class Order : SyncroC.DataBase.Interfaces.IDatabase
	{
    public readonly static int NUM_ORDERS_PER_SETUP = 3;
		private const string XML_FILE_NAME = "Order.xml";
		private string fullPathXmlFile = null;

		/// <summary>String to be sent as version in case of error</summary>
		public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;

		#region DryEndList table constants

		/// <summary>Dataset DryEnd table name.</summary>
		public readonly static string DRYEND_LIST = "DryEndList";

		//header
		/// <summary>DryEndList column name for the position.</summary>
		public readonly static string DEL_POSITION = "Position";
		/// <summary>DryEndList column name for data: "SetupData.setupId".</summary>
		public readonly static string DEL_SETUP_ID = "SetupID";
		/// <summary>DryEndList column name for data: "SetupData.modId".</summary>
		public readonly static string DEL_SETUP_MODID = "ModID";
		/// <summary>DryEndList column name for data: "SetupData.setupCode".</summary>
		public readonly static string DEL_SETUP_CODE = "SetupCode";
		/// <summary>DryEndList column name for data: "SetupData.webWidth".</summary>
		public readonly static string DEL_WEB_WIDTH = "WebWidth";
		/// <summary>DryEndList column name for data: "SetupData.trimType".</summary>
		public readonly static string DEL_TRIM_TYPE = "TrimType";
		/// <summary>DryEndList column name for data: "SetupData.valid".</summary>
		public readonly static string DEL_VALID = "Valid";
		/// <summary>DryEndList column name for data: "SetupData.scoreGap".</summary>
		public readonly static string DEL_SCORE_GAP = "ScoreGap";
		/// <summary>DryEndList column name for data: "SetupData.quality.qualityID".</summary>
		public readonly static string DEL_QUALITY_ID = "QualityID";
		/// <summary>DryEndList column name for data: "SetupData.validationData.trim".</summary>
		public readonly static string DEL_TRIM = "Trim";
		/// <summary>DryEndList column name for data: "SetupData.calculationData.slitsTandem".</summary>
		public readonly static string DEL_SLITS_TANDEM = "SlitsTandem";
		/// <summary>DryEndList column name for data: "SetupData.calculationData.scoresTandem".</summary>
		public readonly static string DEL_SCORES_TANDEM = "ScoresTandem";
		/// <summary>DryEndList column name for data: "SetupData.calculationData.shaftMask".</summary>
		public readonly static string DEL_SHAFT_MASK = "ShaftMask";
		/// <summary>DryEndList column name for data: "SetupData.AOCType.constantSystem".</summary>
		public readonly static string DEL_CONSTANT_SYSTEM = "constantSystem";
		/// <summary>DryEndList column name for data: "SetupData.AOCType.tandem".</summary>
		public readonly static string DEL_TANDEM = "Tandem";
		/// <summary>DryEndList column name for data: "SetupData.AOCType.knifeOnly".</summary>
		public readonly static string DEL_KNIFE_ONLY = "KnifeOnly";
    /// <summary>DryEndList column name for data: "SetupData.calculationData.StackerValidCancelAOC[0]".</summary>
    public readonly static string DEL_NOCHANGE_STK_0 = "NoChange_STK_0";
    /// <summary>DryEndList column name for data: "SetupData.calculationData.StackerValidCancelAOC[1]".</summary>
    public readonly static string DEL_NOCHANGE_STK_1 = "NoChange_STK_1";
    /// <summary>DryEndList column name for data: "SetupData.calculationData.StackerValidCancelAOC[2]".</summary>
    public readonly static string DEL_NOCHANGE_STK_2 = "NoChange_STK_2";
    /// <summary>DryEndList column name for data: SetupData.getSetupLength().</summary>
		public readonly static string DEL_FREE3 = "Free3";
		/// <summary>DryEndList column name for data: .</summary>
		public readonly static string DEL_FREE2 = "Free2";
		/// <summary>DryEndList column name for data: .</summary>
		public readonly static string DEL_FREE1 = "Free1";

    //Order n
    /// <summary>DryEndList column name for data: "SetupData.order.knifeData.segmentSheetCount", order 0.</summary>
    public readonly static string DEL_SEGMENT_SHEET_COUNT_n = "SegmentSheetCount";
    /// <summary>DryEndList column name for data: "SetupData.order.orderNumber", order x.</summary>
    public readonly static string DEL_ORDER_NUMBER_n = "OrderNumber";
    /// <summary>DryEndList column name for data: "SetupData.order.knifeData.knifeLevel", order n.</summary>
    public readonly static string DEL_LEVEL_NAME_n = "LevelName";
    /// <summary>DryEndList column name for data: "SetupData.order.twinData.outs", order n.</summary>
    public readonly static string DEL_OUTS_n = "Outs";
    public readonly static string DEL_MTH_CODE_n = "MTHCode";
    public readonly static string DEL_MTH_NEXT_MACHINE_n = "MTHNextMachine";
    public readonly static string DEL_STACKER_NOTE_n = "StackerNote";
    public readonly static string DEL_CUT_TO_PATTERN_n = "CutToPattern";
    public readonly static string DEL_DATA_TO_PRINT_n_m = "DataToPrint";
    public readonly static string DEL_CUSTOMER_NAME_n = "CustomerName";
    public readonly static string DEL_SHEET_WIDTH_n = "SheetWidth";
    public readonly static string DEL_SHEET_LENGTH_n = "SheetLength";

    //ZETA 01
    public readonly static string DEL_PREPRINT_n = "PrePrint";

		//order 0
    /// <summary>DryEndList column name for data: "SetupData.order.orderNumber", order 0.</summary>
		public readonly static string DEL_ORDER_NUMBER_0 = "OrderNumber0";
		/// <summary>DryEndList column name for data: "SetupData.order.delivery", order 0.</summary>
		public readonly static string DEL_DELIVERY_0 = "Delivery0";
		/// <summary>DryEndList column name for data: "SetupData.order.customerData.name", order 0.</summary>
		public readonly static string DEL_CUSTOMER_NAME_0 = "CustomerName0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.sheetWidth", order 0.</summary>
		public readonly static string DEL_SHEET_WIDTH_0 = "SheetWidth0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.outs", order 0.</summary>
		public readonly static string DEL_OUTS_0 = "Outs0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreMeasures", order 0.</summary>
		public readonly static string DEL_SCORE_MEASURES_0 = "ScoreMeasures0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreType", order 0.</summary>
		public readonly static string DEL_SCORE_TYPE_0 = "ScoreType0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.positType", order 0.</summary>
		public readonly static string DEL_POSIT_TYPE_0 = "PositType0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.teletwinOffset", order 0.</summary>
		public readonly static string DEL_TELETWIN_OFFSET_0 = "TeleTwinOffset0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreGap", order 0.</summary>
		public readonly static string DEL_SCORE_GAP_0 = "ScoreGap0";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.knifeLevel", order 0.</summary>
		public readonly static string DEL_LEVEL_NAME_0 = "LevelName0";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.sheetLength", order 0.</summary>
		public readonly static string DEL_SHEET_LENGTH_0 = "SheetLength0";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.segmentSheetCount", order 0.</summary>
		public readonly static string DEL_SEGMENT_SHEET_COUNT_0 = "SegmentSheetCount0";
		/// <summary>DryEndList column name for data: "SetupData.order.stackerData.sheetsPerStack", order 0.</summary>
		public readonly static string DEL_SHEETS_PER_STACK_0 = "SheetsPerStack0";
		/// <summary>DryEndList column name for data: "SetupData.order.materialHandlingData.stacksPerPallet", order 0.</summary>
		public readonly static string DEL_STACKS_PER_PALLET_0 = "StacksPerPallet0";
		/// <summary>DryEndList column name for data: "SetupData.order.materialHandlingData.sending", order 0.</summary>
		public readonly static string DEL_SENDING_0 = "Sending0";
		/// <summary>DryEndList column name for data: "SetupData.order.knife.cutToMark", order 0.</summary>
		public readonly static string DEL_CUT_TO_MARK_0 = "CutToMark0";

    public readonly static string DEL_CUT_TO_PATTERN_0 = "CutToPattern0";
    public readonly static string DEL_DATA_TO_PRINT_0_1 = "DataToPrint0_1";
    public readonly static string DEL_DATA_TO_PRINT_0_2 = "DataToPrint0_2";
    public readonly static string DEL_DATA_TO_PRINT_0_3 = "DataToPrint0_3";
    public readonly static string DEL_DATA_TO_PRINT_0_4 = "DataToPrint0_4";
    public readonly static string DEL_STACKER_CONT_STACK_0 = "StackerContStack0";
    public readonly static string DEL_STACKER_PALLET_0 = "StackerPallet0";
    public readonly static string DEL_STACKER_NOTE_0 = "StackerNote0";
    public readonly static string DEL_CUSTOMER_ADDRESS_0 = "CustAddress0";
    public readonly static string DEL_MTH_CODE_0 = "MTHCode0";
    public readonly static string DEL_MTH_NEXT_MACHINE_0 = "MTHNextMachine0";
    public readonly static string DEL_PALLET_LAYOUT_0 = "numberOfPalletLayout0";
    public readonly static string DEL_AMOUNT_PALLET_COPY_0 = "amountOfPalletCopy0";
    public readonly static string DEL_PASTORDER_LAYOUT_0 = "numberOfPastOrderLayout0";
    public readonly static string DEL_AMOUNT_PASTORDER_COPY_0 = "amountOfPastOrderCopy0";
    public readonly static string DEL_PREPRINT_0 = "PrePrint0";
    //agrassi_FI24-02-2010_3
    public readonly static string DEL_DISCHARGE_SIDE_0 = "DischargeSide0";
    public readonly static string DEL_BALANCE_0 = "Balance0";
    //agrassi_FI02-09-2010_1
    public readonly static string DEL_BUNDLE_0 = "StackOrBundle0";

		//order 1
		/// <summary>DryEndList column name for data: "SetupData.order.orderNumber", order 1.</summary>
		public readonly static string DEL_ORDER_NUMBER_1 = "OrderNumber1";
		/// <summary>DryEndList column name for data: "SetupData.order.delivery", order 1.</summary>
		public readonly static string DEL_DELIVERY_1 = "Delivery1";
		/// <summary>DryEndList column name for data: "SetupData.order.customerData.name", order 1.</summary>
		public readonly static string DEL_CUSTOMER_NAME_1 = "CustomerName1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.sheetWidth", order 1.</summary>
		public readonly static string DEL_SHEET_WIDTH_1 = "SheetWidth1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.outs", order 1.</summary>
		public readonly static string DEL_OUTS_1 = "Outs1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreMeasures", order 1.</summary>
		public readonly static string DEL_SCORE_MEASURES_1 = "ScoreMeasures1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreType", order 1.</summary>
		public readonly static string DEL_SCORE_TYPE_1 = "ScoreType1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.positType", order 1.</summary>
		public readonly static string DEL_POSIT_TYPE_1 = "PositType1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.teletwinOffset", order 1.</summary>
		public readonly static string DEL_TELETWIN_OFFSET_1 = "TeleTwinOffset1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreGap", order 1.</summary>
		public readonly static string DEL_SCORE_GAP_1 = "ScoreGap1";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.knifeLevel", order 1.</summary>
		public readonly static string DEL_LEVEL_NAME_1 = "LevelName1";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.sheetLength", order 1.</summary>
		public readonly static string DEL_SHEET_LENGTH_1 = "SheetLength1";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.segmentSheetCount", order 1.</summary>
		public readonly static string DEL_SEGMENT_SHEET_COUNT_1 = "SegmentSheetCount1";
		/// <summary>DryEndList column name for data: "SetupData.order.stackerData.sheetsPerStack", order 1.</summary>
		public readonly static string DEL_SHEETS_PER_STACK_1 = "SheetsPerStack1";
		/// <summary>DryEndList column name for data: "SetupData.order.materialHandlingData.stacksPerPallet", order 1.</summary>
		public readonly static string DEL_STACKS_PER_PALLET_1 = "StacksPerPallet1";
		/// <summary>DryEndList column name for data: "SetupData.order.materialHandlingData.sending", order 1.</summary>
		public readonly static string DEL_SENDING_1 = "Sending1";
		/// <summary>DryEndList column name for data: "SetupData.order.knife.cutToMark", order 1.</summary>
		public readonly static string DEL_CUT_TO_MARK_1 = "CutToMark1";

    public readonly static string DEL_CUT_TO_PATTERN_1 = "CutToPattern1";
    public readonly static string DEL_DATA_TO_PRINT_1_1 = "DataToPrint1_1";
    public readonly static string DEL_DATA_TO_PRINT_1_2 = "DataToPrint1_2";
    public readonly static string DEL_DATA_TO_PRINT_1_3 = "DataToPrint1_3";
    public readonly static string DEL_DATA_TO_PRINT_1_4 = "DataToPrint1_4";
    public readonly static string DEL_STACKER_CONT_STACK_1 = "StackerContStack1";
    public readonly static string DEL_STACKER_PALLET_1 = "StackerPallet1";
    public readonly static string DEL_STACKER_NOTE_1 = "StackerNote1";
    public readonly static string DEL_CUSTOMER_ADDRESS_1 = "CustAddress1";
    public readonly static string DEL_MTH_CODE_1 = "MTHCode1";
    public readonly static string DEL_MTH_NEXT_MACHINE_1 = "MTHNextMachine1";
    public readonly static string DEL_PALLET_LAYOUT_1 = "numberOfPalletLayout1";
    public readonly static string DEL_AMOUNT_PALLET_COPY_1 = "amountOfPalletCopy1";
    public readonly static string DEL_PASTORDER_LAYOUT_1 = "numberOfPastOrderLayout1";
    public readonly static string DEL_AMOUNT_PASTORDER_COPY_1 = "amountOfPastOrderCopy1";
    public readonly static string DEL_PREPRINT_1 = "PrePrint1";
    //agrassi_FI24-02-2010_3
    public readonly static string DEL_DISCHARGE_SIDE_1 = "DischargeSide1";
    public readonly static string DEL_BALANCE_1 = "Balance1";
    //agrassi_FI02-09-2010_1
    public readonly static string DEL_BUNDLE_1 = "StackOrBundle1";

    //order 2
		/// <summary>DryEndList column name for data: "SetupData.order.orderNumber", order 2.</summary>
		public readonly static string DEL_ORDER_NUMBER_2 = "OrderNumber2";
		/// <summary>DryEndList column name for data: "SetupData.order.delivery", order 2.</summary>
		public readonly static string DEL_DELIVERY_2 = "Deliver2";
		/// <summary>DryEndList column name for data: "SetupData.order.customerData.name", order 2.</summary>
		public readonly static string DEL_CUSTOMER_NAME_2 = "CustomerName2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.sheetWidth", order 2.</summary>
		public readonly static string DEL_SHEET_WIDTH_2 = "SheetWidth2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.outs", order 2.</summary>
		public readonly static string DEL_OUTS_2 = "Outs2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreMeasures", order 2.</summary>
		public readonly static string DEL_SCORE_MEASURES_2 = "ScoreMeasures2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreType", order 2.</summary>
		public readonly static string DEL_SCORE_TYPE_2 = "ScoreType2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.positType", order 2.</summary>
		public readonly static string DEL_POSIT_TYPE_2 = "PositType2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.teletwinOffset", order 2.</summary>
		public readonly static string DEL_TELETWIN_OFFSET_2 = "TeleTwinOffset2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreGap", order 2.</summary>
		public readonly static string DEL_SCORE_GAP_2 = "ScoreGap2";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.knifeLevel", order 2.</summary>
		public readonly static string DEL_LEVEL_NAME_2 = "LevelName2";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.sheetLength", order 2.</summary>
		public readonly static string DEL_SHEET_LENGTH_2 = "SheetLength2";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.segmentSheetCount", order 2.</summary>
		public readonly static string DEL_SEGMENT_SHEET_COUNT_2 = "SegmentSheetCount2";
		/// <summary>DryEndList column name for data: "SetupData.order.stackerData.sheetsPerStack", order 2.</summary>
		public readonly static string DEL_SHEETS_PER_STACK_2 = "SheetsPerStack2";
		/// <summary>DryEndList column name for data: "SetupData.order.MaterialHandlingData.stacksPerPallet", order 2.</summary>
		public readonly static string DEL_STACKS_PER_PALLET_2 = "StacksPerPallet2";
		/// <summary>DryEndList column name for data: "SetupData.order.MaterialHandlingData.palletSending", order 2.</summary>
		public readonly static string DEL_SENDING_2 = "Sending2";
		/// <summary>DryEndList column name for data: "SetupData.order.knife.cutToMark", order 2.</summary>
		public readonly static string DEL_CUT_TO_MARK_2 = "CutToMark2";

    public readonly static string DEL_CUT_TO_PATTERN_2 = "CutToPattern2";
    public readonly static string DEL_DATA_TO_PRINT_2_1 = "DataToPrint2_1";
    public readonly static string DEL_DATA_TO_PRINT_2_2 = "DataToPrint2_2";
    public readonly static string DEL_DATA_TO_PRINT_2_3 = "DataToPrint2_3";
    public readonly static string DEL_DATA_TO_PRINT_2_4 = "DataToPrint2_4";
    public readonly static string DEL_STACKER_CONT_STACK_2 = "StackerContStack2";
    public readonly static string DEL_STACKER_PALLET_2 = "StackerPallet2";
    public readonly static string DEL_STACKER_NOTE_2 = "StackerNote2";
    public readonly static string DEL_CUSTOMER_ADDRESS_2 = "CustAddress2";
    public readonly static string DEL_MTH_CODE_2 = "MTHCode2";
    public readonly static string DEL_MTH_NEXT_MACHINE_2 = "MTHNextMachine2";
    public readonly static string DEL_PALLET_LAYOUT_2 = "numberOfPalletLayout2";
    public readonly static string DEL_AMOUNT_PALLET_COPY_2 = "amountOfPalletCopy2";
    public readonly static string DEL_PASTORDER_LAYOUT_2 = "numberOfPastOrderLayout2";
    public readonly static string DEL_AMOUNT_PASTORDER_COPY_2 = "amountOfPastOrderCopy2";
    public readonly static string DEL_PREPRINT_2 = "PrePrint2";
    //agrassi_FI24-02-2010_3
    public readonly static string DEL_DISCHARGE_SIDE_2 = "DischargeSide2";
    public readonly static string DEL_BALANCE_2 = "Balance2";
    //agrassi_FI02-09-2010_1
    public readonly static string DEL_BUNDLE_2 = "StackOrBundle2";

    /// <summary>DryEndList names for the slit positions. Use it this way: DEL_SLIT_POSITIONn:m, where 'n' is shaft number and 'm' is slit number.</summary>
		public readonly static string DEL_SLIT_POSITION = "SlitPosition";
		/// <summary>DryEndList names for the score positions. Use it this way: DEL_SCORE_POSITIONn:m, where 'n' is shaft number and 'm' is score number.</summary>
		public readonly static string DEL_SCORE_POSITION = "ScorePosition";
		/// <summary>DryEndList names for the upper relatives score positions. Use it this way: DEL_UPREL_SCORE_POSITIONn:m, where 'n' is shaft number and 'm' is score number.</summary>
    //agrassi_FI04-02-09
    //public readonly static string DEL_UPREL_SCORE_POSITION = "UpRelScorePosition";
    ///// <summary>DryEndList names for the left score positions. Use it this way: DEL_LEFT_SCORE_POSITIONn:m, where 'n' is shaft number and 'm' is score number.</summary>
    //public readonly static string DEL_LEFT_SCORE_POSITION = "LeftScorePosition";
    ///// <summary>DryEndList names for the score positioning types. Use it this way: DEL_SCORE_POSIT_TYPEn:m, where 'n' is shaft number and 'm' is score number.</summary>
    //public readonly static string DEL_SCORE_POSIT_TYPE = "ScorePositType";
    ///// <summary>DryEndList names for the scoring gaps. Use it this way: DEL_SCORE_SCORING_GAPn:m, where 'n' is shaft number and 'm' is score number.</summary>
    //public readonly static string DEL_SCORE_SCORING_GAP = "ScoreScoringGap";
    ///// <summary>DryEndList names for the shaft gaps. Use it this way: DEL_SHAFT_GAPn:m, where 'n' is shaft number and 'm' is score number.</summary>
    //public readonly static string DEL_SHAFT_GAP = "ShaftGap";
    ///// <summary>DryEndList names for the measure indexes. Use it this way: DEL_SCORE_MEASURE_INDEXn:m, where 'n' is shaft number and 'm' is score number.</summary>
    //public readonly static string DEL_SCORE_MEASURE_INDEX = "ScoreMeasureIndex";
		/// <summary>DryEndList names for the plunged slit masks. Use it this way: DEL_PLUNGED_SLIT_MASKn, where 'n' is shaft number.</summary>
		public readonly static string DEL_PLUNGED_SLIT_MASK = "PlungedSlitMask";
		/// <summary>DryEndList names for the dummy slit masks. Use it this way: DEL_DUMMY_SLIT_MASKn, where 'n' is shaft number.</summary>
    //agrassi_FI04-02-09
    //public readonly static string DEL_DUMMY_SLIT_MASK = "DummySlitMask";
    /// <summary>DryEndList names for the separation slit masks. Use it this way: DEL_SEP_SLIT_MASKn, where 'n' is shaft number.</summary>
    public readonly static string DEL_SEP_SLIT_MASK = "SepSlitMask";
		/// <summary>DryEndList names for the start scorers. Use it this way: DEL_START_SCORERn, where 'n' is shaft number.</summary>
		public readonly static string DEL_START_SCORER = "StartScorer";
		/// <summary>DryEndList names for the score counts. Use it this way: DEL_SCORE_COUNTn, where 'n' is shaft number.</summary>
		public readonly static string DEL_SCORE_COUNT = "ScoreCount";
		/// <summary>DryEndList names for the support scores. Use it this way: DEL_SUPPORT_SCOREn, where 'n' is shaft number.</summary>
		public readonly static string DEL_SUPPORT_SCORE= "SupportScore";
		/// <summary>DryEndList names for the two-level Web Director Mask.</summary>
		public readonly static string DEL_2LEVEL_WEBDIR_MASK = "2LevelWebDirMask";
		/// <summary>DryEndList names for the three-level Web Director Mask.</summary>
		public readonly static string DEL_3LEVEL_WEBDIR_MASK = "3LevelWebDirMask";
		/// <summary>DryEndList names for the Rotary sector mask.</summary>
		public readonly static string DEL_RCS_SECTOR_MASK = "RCSSectorMask";
    /// <summary>DryEndList names for the tear tape's number. </summary>
    public readonly static string DEL_TEAR_TAPE_COUNT = "TearTapeCount";
    /// <summary>DryEndList names for the tear tapes. Use it this way: DEL_TEAR_TAPE_POSITIONn, where 'n' is tear tape index.</summary>
    public readonly static string DEL_TEAR_TAPE_POSITION = "TearTapePosition";

    //agrassi_FI26-05-08_1
    public readonly static string DEL_OFFICE_ID = "OfficeID";
    //public readonly static string DEL_SPARE_1 = "Spare1";
    //agrassi_FI05-10-2010_1
    //agrassi_FI06-04-2011_1
    //public readonly static string DEL_FLUTE_ID = "FluteID";
    public readonly static string DEL_FLUTE_ID = "Flute";


		#endregion

		#region WetEndList table constants

		/// <summary>Dataset WetEnd table name.</summary>
		public readonly static string WETEND_LIST = "WetEndList";

		/// <summary>WetEndList column name for the position.</summary>
		public readonly static string WEL_POSITION = "Position";
		/// <summary>WetEndList column name for data: "ProgramData.programCode".</summary>
		public readonly static string WEL_PROGRAM_CODE = "ProgramCode";
		/// <summary>WetEndList column name for data: "ProgramData.programID".</summary>
		public readonly static string WEL_PROGRAM_ID = "ProgramID";
		/// <summary>WetEndList column name for data: "ProgramData.modID".</summary>
		public readonly static string WEL_PROGRAM_MODID = "ModID";
    /// <summary>WetEndList column name for data: "ProgramData.corrDryIDs".</summary>
    public readonly static string WEL_DRY_IDs = "CorrDryIDs";
    /// <summary>WetEndList column name for data: "ProgramData.paperWidth_UDm".</summary>
		public readonly static string WEL_PAPER_WIDTH = "PaperWidth";
		/// <summary>WetEndList column name for data: "ProgramData.OrderLength_UDm".</summary>
		public readonly static string WEL_PROGRAM_LENGTH = "ProgramLength";
		/// <summary>WetEndList column name for data: "ProgramData.programQuality".</summary>
		public readonly static string WEL_PROGRAM_QUALITY = "ProgramQuality";
		/// <summary>WetEndList column name for data: "ProgramData.SFPosition".</summary>
		public readonly static string WEL_PROGRAM_SF_POSITION = "SFPosition";
		/// <summary>WetEndList column name for data: "ProgramData.Note".</summary>
		public readonly static string WEL_PROGRAM_NOTE = "Note";
		
		/// <summary>WetEndList column name for data: "SPARE1".</summary>
		public readonly static string WEL_PROGRAM_SPARE1 = "SPARE1";
		/// <summary>WetEndList column name for data: "SPARE2".</summary>
		public readonly static string WEL_PROGRAM_SPARE2 = "SPARE2";
		/// <summary>WetEndList column name for data: "SPARE3".</summary>
		public readonly static string WEL_PROGRAM_SPARE3 = "SPARE3";
		/// <summary>WetEndList column name for data: "SPARE4".</summary>
		public readonly static string WEL_PROGRAM_SPARE4 = "SPARE4";
		/// <summary>WetEndList column name for data: "SPARE5".</summary>
		public readonly static string WEL_PROGRAM_SPARE5 = "SPARE5";
		/// <summary>WetEndList column name for data: "SPARE6".</summary>
		public readonly static string WEL_PROGRAM_SPARE6 = "SPARE6";
		/// <summary>WetEndList column name for data: "SPARE7".</summary>
		public readonly static string WEL_PROGRAM_SPARE7 = "SPARE7";
		/// <summary>WetEndList column name for data: "SPARE8".</summary>
		public readonly static string WEL_PROGRAM_SPARE8 = "SPARE8";
		/// <summary>WetEndList column name for data: "SPARE9".</summary>
		public readonly static string WEL_PROGRAM_SPARE9 = "SPARE9";
		/// <summary>WetEndList column name for data: "SPARE10".</summary>
		public readonly static string WEL_PROGRAM_SPARE10 = "SPARE10";
		/// <summary>WetEndList column name for data: "SPARE11".</summary>
		public readonly static string WEL_PROGRAM_SPARE11 = "SPARE11";
		/// <summary>WetEndList column name for data: "SPARE12".</summary>
		public readonly static string WEL_PROGRAM_SPARE12 = "SPARE12";
		/// <summary>WetEndList column name for data: "SPARE13".</summary>
		public readonly static string WEL_PROGRAM_SPARE13 = "SPARE13";


		#endregion

		/// <summary>
		/// orders dataset reference
		/// </summary>
		public DataSet order = null;

		/// <summary>
		/// Delegate for handling OnDBChange events.
		/// </summary>
		public delegate void DBChangedHandler(object sender, DBChangedEventArgs args);

		/// <summary>
		/// Event raised when a change (delete, add or modify of a record) occur into Order database.
		/// </summary>
		public event DBChangedHandler OnDBOrderChange;

		public Order()
		{
			Settings s = Settings.getSettings();
			fullPathXmlFile = s.paths.ClientData + XML_FILE_NAME;

			order = new DataSet();
			order.CaseSensitive = true;

			#region DryEndList table

					//table
					order.Tables.Add(Order.DRYEND_LIST);

					//columns: header
					/*0*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_POSITION);
							 order.Tables[Order.DRYEND_LIST].Columns[Order.DEL_POSITION].AutoIncrement = true;
					/*1*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SETUP_ID);							  //SetupData.setupId
					/*2*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SETUP_MODID);						//SetupData.modId
					/*3*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SETUP_CODE);						  //SetupData.setupCode
					/*4*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_WEB_WIDTH);							//SetupData.webWidth
					/*5*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_TRIM_TYPE);							//SetupData.trimType
					/*6*/order.Tables[Order.DRYEND_LIST].Columns.Add(new DataColumn(Order.DEL_VALID, typeof(bool)));	//SetupData.valid
					/*7*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_GAP);							//SetupData.scoreGap
					/*8*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_QUALITY_ID);						  //SetupData.quality.qualityID
					/*9*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_TRIM);									  //SetupData.validationData.trim
					/*10*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SLITS_TANDEM);					//SetupData.calculationData.slitsTandem
					/*11*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORES_TANDEM);					//SetupData.calculationData.scoresTandem
					/*12*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SHAFT_MASK);						//SetupData.calculationData.shaftMask
					/*13*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CONSTANT_SYSTEM);				//SetupData.AOCType.constantSystem
					/*14*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_TANDEM);								//SetupData.AOCType.tandem
					/*15*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_KNIFE_ONLY);						//SetupData.AOCType.knifeOnly
					/*16*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_NOCHANGE_STK_0);				//SetupData.calculationData.StackerValidCancelAOC[0]
					/*17*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_NOCHANGE_STK_1);				//SetupData.calculationData.StackerValidCancelAOC[1]
					/*18*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_NOCHANGE_STK_2);				//SetupData.calculationData.StackerValidCancelAOC[2]
					/*19*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CUT_TO_MARK_0);	              	//SetupData.order.knife.cutToMark 0
					/*20*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CUT_TO_MARK_1);		              //SetupData.order.knife.cutToMark 1
					/*21*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CUT_TO_MARK_2);		              //SetupData.order.knife.cutToMark 2

					//columns: order 0
					/*22*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_ORDER_NUMBER_0);				//SetupData.order.orderNumber
					/*23*/order.Tables[Order.DRYEND_LIST].Columns.Add(new DataColumn(Order.DEL_DELIVERY_0, typeof(DateTime)));	//SetupData.order.delivery
					/*24*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CUSTOMER_NAME_0);				//SetupData.order.customerData.name
					/*25*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SHEET_WIDTH_0);					//SetupData.order.twinData.sheetWidth
					/*26*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_OUTS_0);								//SetupData.order.twinData.outs
					/*27*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_MEASURES_0);			//SetupData.order.twinData.scoreMeasures
					/*28*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_TYPE_0);					//SetupData.order.twinData.scoreType
					/*29*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_POSIT_TYPE_0);					//SetupData.order.twinData.positType
					/*30*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_TELETWIN_OFFSET_0);			//SetupData.order.twinData.teletwinOffset
					/*31*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_GAP_0);						//SetupData.order.twinData.scoreGap
					/*32*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_LEVEL_NAME_0);					//SetupData.order.knife.knifeLevel
					/*33*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SHEET_LENGTH_0);				//SetupData.order.knife.sheetLength
					/*34*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SEGMENT_SHEET_COUNT_0);	//SetupData.order.knife.segmentSheetCount
					/*35*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SHEETS_PER_STACK_0);		//SetupData.order.knife.sheetPerStack
					/*36*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_STACKS_PER_PALLET_0);		//SetupData.order.knife.stacksPerPallet
					/*37*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SENDING_0);							//SetupData.order.knife.palletSending

					//columns: order 1
					/*38*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_ORDER_NUMBER_1);				//SetupData.order.orderNumber
					/*39*/order.Tables[Order.DRYEND_LIST].Columns.Add(new DataColumn(Order.DEL_DELIVERY_1, typeof(DateTime)));	//SetupData.order.delivery
					/*40*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CUSTOMER_NAME_1);				//SetupData.order.customerData.name
					/*41*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SHEET_WIDTH_1);					//SetupData.order.twinData.sheetWidth
					/*42*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_OUTS_1);								//SetupData.order.twinData.outs
					/*43*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_MEASURES_1);			//SetupData.order.twinData.scoreMeasures
					/*44*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_TYPE_1);					//SetupData.order.twinData.scoreType
					/*45*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_POSIT_TYPE_1);					//SetupData.order.twinData.positType
					/*46*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_TELETWIN_OFFSET_1);			//SetupData.order.twinData.teletwinOffset
					/*47*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_GAP_1);						//SetupData.order.twinData.scoreGap
					/*48*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_LEVEL_NAME_1);					//SetupData.order.knife.knifeLevel
					/*49*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SHEET_LENGTH_1);				//SetupData.order.knife.sheetLength
					/*50*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SEGMENT_SHEET_COUNT_1);	//SetupData.order.knife.segmentSheetCount
					/*51*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SHEETS_PER_STACK_1);		//SetupData.order.knife.sheetPerStack
					/*52*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_STACKS_PER_PALLET_1);		//SetupData.order.knife.stacksPerPallet
					/*53*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SENDING_1);							//SetupData.order.knife.palletSending

					//columns: order 2
					/*54*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_ORDER_NUMBER_2);				//SetupData.order.orderNumber
					/*55*/order.Tables[Order.DRYEND_LIST].Columns.Add(new DataColumn(Order.DEL_DELIVERY_2, typeof(DateTime)));	//SetupData.order.delivery
					/*56*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CUSTOMER_NAME_2);				//SetupData.order.customerData.name
					/*57*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SHEET_WIDTH_2);					//SetupData.order.twinData.sheetWidth
					/*58*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_OUTS_2);								//SetupData.order.twinData.outs
					/*59*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_MEASURES_2);			//SetupData.order.twinData.scoreMeasures
					/*60*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_TYPE_2);					//SetupData.order.twinData.scoreType
					/*61*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_POSIT_TYPE_2);					//SetupData.order.twinData.positType
					/*62*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_TELETWIN_OFFSET_2);			//SetupData.order.twinData.teletwinOffset
					/*63*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_GAP_2);						//SetupData.order.twinData.scoreGap
					/*64*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_LEVEL_NAME_2);					//SetupData.order.knife.knifeLevel
					/*65*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SHEET_LENGTH_2);				//SetupData.order.knife.sheetLength
					/*66*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SEGMENT_SHEET_COUNT_2);	//SetupData.order.knife.segmentSheetCount
					/*67*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SHEETS_PER_STACK_2);		//SetupData.order.knife.sheetPerStack
					/*68*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_STACKS_PER_PALLET_2);		//SetupData.order.knife.stacksPerPallet
					/*69*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SENDING_2);							//SetupData.order.knife.palletSending


          //agrassi_FI04-02-09
          //for (int sh = 0; sh < LineConfig.SlitterScorerHeader.NUM_SLIT_SHAFTS; sh++)
          //{
          //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_PLUNGED_SLIT_MASK + sh.ToString());
          //  //order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DUMMY_SLIT_MASK + sh.ToString());
          //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SEP_SLIT_MASK + sh.ToString());
          //  for (int slt=0; slt<LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; slt++)
          //    order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SLIT_POSITION + sh.ToString()+":"+slt.ToString());
          //}
          /*70*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_PLUNGED_SLIT_MASK);
          /*71*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SEP_SLIT_MASK);
          /*72*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SLIT_POSITION);

          //agrassi_FI04-02-09
          //for (int sh = 0; sh < LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS; sh++)
          //{
          //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_START_SCORER + sh.ToString());
          //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_COUNT + sh.ToString());
          //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SUPPORT_SCORE + sh.ToString());
          //  for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
          //    order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_POSITION + sh.ToString()+":"+scr.ToString());
          //  //for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
          //  //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_UPREL_SCORE_POSITION + sh.ToString() + ":" + scr.ToString());
          //  //for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
          //  //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_LEFT_SCORE_POSITION + sh.ToString() + ":" + scr.ToString());
          //  //for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
          //  //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_POSIT_TYPE+ sh.ToString() + ":" + scr.ToString());
          //  //for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
          //  //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_SCORING_GAP + sh.ToString() + ":" + scr.ToString());
          //  //for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
          //  //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SHAFT_GAP + sh.ToString() + ":" + scr.ToString());
          //  //for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
          //  //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_MEASURE_INDEX + sh.ToString() + ":" + scr.ToString());
          //}
            /*73*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_START_SCORER);
            /*74*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_COUNT);
            /*75*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SUPPORT_SCORE);
            /*76*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_POSITION);
            //agrassi_FI04-02-09
            //for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
            //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_UPREL_SCORE_POSITION + sh.ToString() + ":" + scr.ToString());
            //for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
            //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_LEFT_SCORE_POSITION + sh.ToString() + ":" + scr.ToString());
            //for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
            //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_POSIT_TYPE+ sh.ToString() + ":" + scr.ToString());
            //for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
            //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_SCORING_GAP + sh.ToString() + ":" + scr.ToString());
            //for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
            //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SHAFT_GAP + sh.ToString() + ":" + scr.ToString());
            //for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
            //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SCORE_MEASURE_INDEX + sh.ToString() + ":" + scr.ToString());
          //}
          /*77*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_2LEVEL_WEBDIR_MASK);
					/*78*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_3LEVEL_WEBDIR_MASK);
					/*79*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_RCS_SECTOR_MASK);
          //for (int tt = 0; tt < 2 * LineConfig.SlitterScorerHeader.MAX_SLITS_PER_SHAFT; tt++)
          //  order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_TEAR_TAPE_POSITION + tt.ToString());
          /*80*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_TEAR_TAPE_POSITION);
          /*81*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_TEAR_TAPE_COUNT);

          /*82*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CUT_TO_PATTERN_0);
          /*83*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DATA_TO_PRINT_0_1);
          /*84*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DATA_TO_PRINT_0_2);
          /*85*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DATA_TO_PRINT_0_3);
          /*86*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DATA_TO_PRINT_0_4);
          /*87*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_STACKER_CONT_STACK_0);
          /*88*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_STACKER_PALLET_0);
          /*89*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_STACKER_NOTE_0);
          /*90*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CUSTOMER_ADDRESS_0);
          /*91*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_MTH_CODE_0);
          /*92*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_MTH_NEXT_MACHINE_0);
          /*93*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_PALLET_LAYOUT_0);
          /*94*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_AMOUNT_PALLET_COPY_0);
          /*95*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_PASTORDER_LAYOUT_0);
          /*96*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_AMOUNT_PASTORDER_COPY_0);
          /*97*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_PREPRINT_0);
          //agrassi_FI24-02-2010_3
          /*98*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DISCHARGE_SIDE_0);
          /*99*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_BALANCE_0);
          //agrassi_FI02-09-2010_1
          /*100*/order.Tables[Order.DRYEND_LIST].Columns.Add(new DataColumn(Order.DEL_BUNDLE_0, typeof(bool)));
          /*101*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CUT_TO_PATTERN_1);
          /*102*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DATA_TO_PRINT_1_1);
          /*103*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DATA_TO_PRINT_1_2);
          /*104*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DATA_TO_PRINT_1_3);
          /*105*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DATA_TO_PRINT_1_4);
          /*106*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_STACKER_CONT_STACK_1);
          /*107*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_STACKER_PALLET_1);
          /*108*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_STACKER_NOTE_1);
          /*109*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CUSTOMER_ADDRESS_1);
          /*110*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_MTH_CODE_1);
          /*111*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_MTH_NEXT_MACHINE_1);
          /*112*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_PALLET_LAYOUT_1);
          /*113*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_AMOUNT_PALLET_COPY_1);
          /*114*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_PASTORDER_LAYOUT_1);
          /*115*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_AMOUNT_PASTORDER_COPY_1);
          /*116*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_PREPRINT_1);
          //agrassi_FI24-02-2010_3
          /*117*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DISCHARGE_SIDE_1);
          /*118*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_BALANCE_1);
          //agrassi_FI02-09-2010_1
          /*119*/order.Tables[Order.DRYEND_LIST].Columns.Add(new DataColumn(Order.DEL_BUNDLE_1, typeof(bool)));
          /*120*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CUT_TO_PATTERN_2);
          /*121*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DATA_TO_PRINT_2_1);
          /*122*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DATA_TO_PRINT_2_2);
          /*123*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DATA_TO_PRINT_2_3);
          /*124*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DATA_TO_PRINT_2_4);
          /*125*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_STACKER_CONT_STACK_2);
          /*126*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_STACKER_PALLET_2);
          /*127*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_STACKER_NOTE_2);
          /*128*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_CUSTOMER_ADDRESS_2);
          /*129*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_MTH_CODE_2);
          /*130*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_MTH_NEXT_MACHINE_2);
          /*131*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_PALLET_LAYOUT_2);
          /*132*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_AMOUNT_PALLET_COPY_2);
          /*133*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_PASTORDER_LAYOUT_2);
          /*134*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_AMOUNT_PASTORDER_COPY_2);
          /*135*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_PREPRINT_2);
          //agrassi_FI24-02-2010_3
          /*136*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_DISCHARGE_SIDE_2);
          /*137*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_BALANCE_2);
          //agrassi_FI02-09-2010_1
          /*138*/order.Tables[Order.DRYEND_LIST].Columns.Add(new DataColumn(Order.DEL_BUNDLE_2, typeof(bool)));
          //agrassi_FI26-05-08_1
          /*139*/order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_OFFICE_ID);
          //order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_SPARE_1);
          //agrassi_FI05-10-2010_1
          /*140*/
          order.Tables[Order.DRYEND_LIST].Columns.Add(Order.DEL_FLUTE_ID);
					//key
          /*141*/order.Tables[Order.DRYEND_LIST].PrimaryKey = new DataColumn[] { order.Tables[Order.DRYEND_LIST].Columns[Order.DEL_SETUP_ID] };

					#endregion

			#region WetEndList table

					//table
					order.Tables.Add(Order.WETEND_LIST);

					//columns
					/*0*/
          order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_POSITION);							
									order.Tables[Order.WETEND_LIST].Columns[Order.WEL_POSITION].AutoIncrement = true;
					/*1*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_CODE);			//ProgramData.programCode
					/*2*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_ID);				//ProgramData.programID
          /*3*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_MODID);			//ProgramData.modID
          /*4*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_DRY_IDs);			//ProgramData.corrDryIDs
          /*5*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PAPER_WIDTH);				//ProgramData.paperWidth_UDm
					/*6*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_LENGTH);		//ProgramData.OrderLength_UDm
					/*7*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_QUALITY);		//ProgramData.programQuality
					/*8*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SF_POSITION);		//ProgramData.SFposition
					/*9*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_NOTE);		//ProgramData.Note
					/*10*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE1);		//SPARE 1
					/*11*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE2);		//SPARE 2
					/*12*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE3);		//SPARE 3
					/*13*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE4);		//SPARE 4
					/*14*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE5);		//SPARE 5
					/*15*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE6);		//SPARE 6
					/*16*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE7);		//SPARE 7
					/*17*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE8);		//SPARE 8
					/*18*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE9);		//SPARE 9
					/*19*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE10);		//SPARE 10
					/*20*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE11);		//SPARE 11
					/*21*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE12);		//SPARE 12
					/*22*/order.Tables[Order.WETEND_LIST].Columns.Add(Order.WEL_PROGRAM_SPARE13);		//SPARE 13

					//key
					order.Tables[Order.WETEND_LIST].PrimaryKey =
							new DataColumn[] { order.Tables[Order.WETEND_LIST].Columns[Order.WEL_PROGRAM_ID] };

					#endregion
		}

    //agrassi_FI04-02-09
    /*
     * The simple arrays are stored in the following way: dato0,dato1,dato2,dato3,dato4,dato5
     * The double arrays are stored in the following way: dato00,dato01,dato02,dato03,dato04,dato05;dato10,dato11,dato12,dato13,dato14,dato15;dato20,dato21,dato22,dato23,dato24,dato25
     */
    private string GetField(string dato, int ind, char sep)
    {
      int comma1 = -1;
      for (int i = 0; i < ind; i++)
      {
        comma1 = dato.IndexOf(sep, comma1 + 1);
        if (comma1 < 0)
          return string.Empty;
      }
      int comma2 = dato.IndexOf(sep, comma1 + 1);
      if (comma2 >= 0)
        return dato.Substring(comma1 + 1, comma2 - comma1 - 1);
      return dato.Substring(comma1 + 1);
    }

    public string GetDryOrderField(int pos, string fieldName)
    {
      if (this.order.Tables[Order.DRYEND_LIST].Rows.Count>pos)
        if (this.order.Tables[Order.DRYEND_LIST].Columns.Contains(fieldName))
          return this.order.Tables[Order.DRYEND_LIST].Rows[pos][fieldName].ToString();
      return string.Empty;
    }

    public string GetDryOrderField(int pos, string fieldName, int ind)
    {
      return this.GetField(this.GetDryOrderField(pos, fieldName), ind, ',');
    }

    public string GetDryOrderField(int pos, string fieldName, int ind1, int ind2)
    {
      return this.GetField(this.GetField(this.GetDryOrderField(pos, fieldName), ind1, ';'), ind2, ',');
    }

    #region Dry End methods
    public DataRow getRow(string setupID, string modID, LevelName level, ref int i)
    {
      DataRow[] rows = null;
      if (order != null && order.Tables.Contains(Order.DRYEND_LIST))
      {
          rows = order.Tables[Order.DRYEND_LIST].Select(Order.DEL_SETUP_ID + " = '" + setupID + "' AND " + Order.DEL_SETUP_MODID + " = '" + modID + "' AND " + Order.DEL_LEVEL_NAME_0  +" = '" + level.ToString()  + "'");
          if (rows != null && rows.Length > 0)
          {
            i = 0;
            return rows[0];
          }
          rows = order.Tables[Order.DRYEND_LIST].Select(Order.DEL_SETUP_ID + " = '" + setupID + "' AND " + Order.DEL_SETUP_MODID + " = '" + modID + "' AND " + Order.DEL_LEVEL_NAME_1 + " = '" + level.ToString() + "'");
          if (rows != null && rows.Length > 0)
          {
            i = 1;
            return rows[0];
          }
          rows = order.Tables[Order.DRYEND_LIST].Select(Order.DEL_SETUP_ID + " = '" + setupID + "' AND " + Order.DEL_SETUP_MODID + " = '" + modID + "' AND " + Order.DEL_LEVEL_NAME_2 + " = '" + level.ToString() + "'");
          if (rows != null && rows.Length > 0)
          {
            i = 2;
            return rows[0];
          }
        }
      return null;
    }

		public DataRow getRow(string setupID, string modID)
		{
			DataRow[] rows = null;
			if (order != null && order.Tables.Contains(Order.DRYEND_LIST))
			{
				rows = order.Tables[Order.DRYEND_LIST].Select(Order.DEL_SETUP_ID + " = '" + setupID + "' AND " + Order.DEL_SETUP_MODID + " = '" + modID + "'");
				if (rows != null && rows.Length > 0)
				{
					return rows[0];
				}
			}
			return null;
		}

    public DataRow getRow(string setupID)
    {
      DataRow[] rows = null;
      if (order != null && order.Tables.Contains(Order.DRYEND_LIST))
      {
        rows = order.Tables[Order.DRYEND_LIST].Select(Order.DEL_SETUP_ID + " = '" + setupID + "'");
        if (rows != null && rows.Length > 0)
        {
          return rows[0];
        }
      }
      return null;
    }

		public DataRow getRowBySetupCode(string setupCode)
		{
			if (order == null || !order.Tables.Contains(Order.DRYEND_LIST))
				return null;

			foreach (DataRow row in order.Tables[Order.DRYEND_LIST].Rows)
			{
				if (String.Equals(row[Order.DEL_SETUP_CODE].ToString(), setupCode, StringComparison.Ordinal))
					return row;
			}
			return null;
		}

    public DataRow getRow(int position)
    {
      if (order != null && order.Tables.Contains(Order.DRYEND_LIST))
      {
        //agrassi_FI11-04-2011_1
        //if (order.Tables[Order.DRYEND_LIST] != null && order.Tables[Order.DRYEND_LIST].Rows.Count > 0)
        if (order.Tables[Order.DRYEND_LIST] != null && order.Tables[Order.DRYEND_LIST].Rows.Count > position)
        {
          return order.Tables[Order.DRYEND_LIST].Rows[position];
        }
      }
      return null;
    }

		/// <summary>
		/// Creates an array of string to be inserted into the dataset order (Dry End table) from the values sent by the server.
		/// </summary>
		/// <param name="parameters">array of parameters.</param>
		/// <param name="position">index in the table of the affected record</param>
		/// <returns>null if some invalid parameters encountered.</returns>
		private object[] CreateDryEndElementsArray(string[] parameters, int position)
		{
			//check number of parameters that must be 1 minus of the columns.count because in the table there is also di position column
			if (parameters == null || order.Tables[Order.DRYEND_LIST].Columns.Count - 1 != parameters.Length)
			{
				Logger.LogError(this, "CreateDryEndElementsArray - Cannot Create Elements Array record because the number of the parameters is incompatible with the table.");
				return null;
			}

			object[] elements = new object[parameters.Length + 1];
			//copy all elements
			Array.Copy(parameters, 0, elements, 1, parameters.Length);
			//override column DEL_POSITION
			elements[0] = position.ToString();
			try
			{
				//override column DEL_TRIM_TYPE
				elements[5] = (Enum.Format(typeof(TrimType), Enum.Parse(typeof(TrimType), parameters[4]), "g"));
				//override column DEL_VALID
				elements[6] = Convert.ToBoolean(parameters[5]);
				//override column DEL_DELIVERY_0
				elements[23] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[22]);
				//override column DEL_POSIT_TYPE_0
				elements[29] = (Enum.Format(typeof(PositType), Enum.Parse(typeof(PositType), parameters[28]), "g"));
				//override column DEL_KNIFE_LEVEL_0
				elements[32] = (Enum.Format(typeof(LevelName), Enum.Parse(typeof(LevelName), parameters[31]), "g"));
				//override column DEL_SENDING_0
				elements[37] = (Enum.Format(typeof(PalletSending), Enum.Parse(typeof(PalletSending), parameters[36]),"g"));
				//override column DEL_DELIVERY_1
				elements[39] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[38]);
				//override column DEL_POSIT_TYPE_1
				elements[45] = (Enum.Format(typeof(PositType), Enum.Parse(typeof(PositType), parameters[44]), "g"));
				//override column DEL_KNIFE_LEVEL_1
				elements[48] = (Enum.Format(typeof(LevelName), Enum.Parse(typeof(LevelName), parameters[47]), "g"));
				//override column DEL_SENDING_1
				elements[53] = (Enum.Format(typeof(PalletSending), Enum.Parse(typeof(PalletSending), parameters[52]), "g"));
				//override column DEL_DELIVERY_0
				elements[55] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[54]);
				//override column DEL_POSIT_TYPE_2
				elements[61] = (Enum.Format(typeof(PositType), Enum.Parse(typeof(PositType), parameters[60]), "g"));
				//override column DEL_KNIFE_LEVEL_2
				elements[64] = (Enum.Format(typeof(LevelName), Enum.Parse(typeof(LevelName), parameters[63]), "g"));
				//override column DEL_SENDING_2
				elements[69] = (Enum.Format(typeof(PalletSending), Enum.Parse(typeof(PalletSending), parameters[68]), "g"));
        //agrassi_FI04-02-09
        //for (int sh = 0; sh < LineConfig.SlitterScorerHeader.MAX_NUM_SCORE_SHAFTS; sh++)
        //{
        //  for (int scr = 0; scr < LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT; scr++)
        //  {
        //    int index = 177 + scr + sh * (LineConfig.SlitterScorerHeader.MAX_SCORES_PER_SHAFT * 7 + 3);
        //    elements[index] =(Enum.Format(typeof(PositType), Enum.Parse(typeof(PositType), parameters[index-1]), "g"));
        //  }
        //}
        //agrassi_FI24-02-2010_3
        //override column DEL_DISCHARGE_SIDE_0
        elements[98] = (Enum.Format(typeof(DischargeSide), Enum.Parse(typeof(DischargeSide), parameters[97]), "g"));
        //override column DEL_BALANCE_0
        elements[99] = (Enum.Format(typeof(Balance), Enum.Parse(typeof(Balance), parameters[98]), "g"));
        //agrassi_FI02-09-2010_1
        //override column DEL_DISCHARGE_SIDE_1
        elements[117] = (Enum.Format(typeof(DischargeSide), Enum.Parse(typeof(DischargeSide), parameters[116]), "g"));
        //override column DEL_BALANCE_1
        elements[118] = (Enum.Format(typeof(Balance), Enum.Parse(typeof(Balance), parameters[117]), "g"));
        //override column DEL_DISCHARGE_SIDE_2
        elements[136] = (Enum.Format(typeof(DischargeSide), Enum.Parse(typeof(DischargeSide), parameters[135]), "g"));
        //override column DEL_BALANCE_2
        elements[137] = (Enum.Format(typeof(Balance), Enum.Parse(typeof(Balance), parameters[136]), "g"));
      }
			catch (Exception ex)
			{ 
				Logger.LogError(this, "CreateDryEndElementsArray - Invalid parameter encountered.", ex);
				return null;
			}
			return elements;
		}

		private void ManageError(string text)
		{
			DBManager.getDBManager().DBError = true;
			try
			{
				File.Delete(fullPathXmlFile);
			}
			catch (Exception)
			{
			}
			DialogResult ret = MessageBox.Show(text,"SYNCRO DATABASE ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
			CommunicationManager.GetCommunicationManager().ShutDown(false,true);
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Add a new record to the database.
		/// </summary>
		/// <param name="values">a string representing the values.</param>
		public void Add(string values)
		{
			string[] parameters = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
			DataRowCollection rows = order.Tables[Order.DRYEND_LIST].Rows;
			object[] elements = CreateDryEndElementsArray(parameters, rows.Count + 1);
			if (elements != null)
			{
				try
				{
					rows.Add(elements);
          Logger.LogError(this, "ADD " + parameters[0]);
          if (OnDBOrderChange != null)
							OnDBOrderChange(this, new DBChangedEventArgs((string)elements[0], DBChangement.Insert));
				}
				catch (System.Data.ConstraintException ex)
				{
					string log = "DRYAdd (Order DB) - Error adding record to the DryEndList for ConstraintException.";
					Logger.LogError(this, log, ex);
				}
				catch (Exception ex)
				{
					string log = "Add (Order DB) - Error adding record to the DryEndList.";
					Logger.LogError(this, log, ex);
					ManageError(log);
				}
				finally
				{
					Application.DoEvents();
				}
			}
			else
				Logger.LogError(this, "Add - Invalid parameters list.");
      Logger.LogError(this, this.GetFirstOrderIDs());
		}

		/// <summary>
		/// Delete the specified range of records 
		/// </summary>
		/// <param name="keys">a string containing the list of the keys of the orders to delete. </param>
		public void Del(string keys)
		{
			string[] parameters = keys.Split(new Char[] { Command.PARAMETER_SEPARATOR });
			if (parameters.Length < 1)
			{
				Logger.LogError(this, "Del - invalid parameter: at least one order has to be specified.");
				return;
			}
			try
			{
				DataTable dt = order.Tables[Order.DRYEND_LIST];
				if (dt != null)
				{
					for (int i = 0; i < parameters.Length; i++)
					{
						DataRow dr = dt.Rows.Find(parameters[i]);
						if (dr != null)
						{
							int ind = Convert.ToInt32(dr[Order.DEL_POSITION]) - 1;
							dr.Delete();
							UpdatePositionsFromIndex(ind, true);
							if (OnDBOrderChange != null)
									OnDBOrderChange(this, new DBChangedEventArgs(parameters[i], DBChangement.Delete));
						}
					}
          Logger.LogError(this, "DEL " + keys);
        }
				else
				{
					Logger.LogError(this, "Del - The Dry end table was null.");
				}
			}
			catch (Exception ex)
			{
				string log = "Del (Order DB) - Error Deleting Record.";
				Logger.LogError(this, log, ex);
				ManageError(log);
			}
			finally
			{
				Application.DoEvents();
			}
      Logger.LogError(this, this.GetFirstOrderIDs());
    }

		/// <summary>
		/// Delete the specified range of records 
		/// </summary>
		/// <param name="destinationAndKeys">a string containing the destination position of of the
		/// orders which keys are specified as following parameters.</param>
		public void Move(string destinationAndKeys)
		{
			string[] parameters = destinationAndKeys.Split(new Char[] { Command.PARAMETER_SEPARATOR });
			if (parameters.Length < 2)
			{
				Logger.LogError(this, "Move - invalid parameters: first parameter must be the destination position and then at least one order key must follow.");
				return;
			}
			//destination of moving rows into the grid (from 1 to rows.count)
			int destinationPos = 0; 
			try
			{
				destinationPos = Convert.ToInt32(parameters[0]);
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "Move - invalid parameter destination", ex);
				return;
			}
			//destination of moving rows into the collection (from 0 to rows.count - 1)
			int destinationIndex = destinationPos - 1;
			try
			{
				DataTable dt = order.Tables[Order.DRYEND_LIST];
				if (dt != null)
				{
					if (destinationIndex < 0 || destinationIndex >= dt.Rows.Count + 1)
					{
						Logger.LogError(this, "Move - invalid parameter destination.");
						return;
					}
					DataRow[] movingRows = new DataRow[parameters.Length - 1];
					for (int i = 0; i < movingRows.Length; i++)
					{
						//save moving rows in temporary array
						DataRow oldRow = dt.Rows.Find(parameters[i + 1]);
						if (i == 0)
						{
							int index = dt.Rows.IndexOf(oldRow);
							if (index < destinationIndex)
								destinationIndex -= movingRows.Length;
						}
						movingRows[i] = dt.NewRow();
						object[] tmp = new Object[movingRows[i].ItemArray.Length];
						Array.Copy(oldRow.ItemArray, tmp, movingRows[i].ItemArray.Length);
						movingRows[i].ItemArray = tmp;
						//delete all moving rows from the collection
						dt.Rows.Remove(oldRow);
						if (OnDBOrderChange != null)
							OnDBOrderChange(this, new DBChangedEventArgs(parameters[i], DBChangement.Delete));
						Application.DoEvents();
					}
					//reinsert all moving rows into the collection from the temporary array
					for (int i = 0; i < movingRows.Length; i++)
					{
						dt.Rows.InsertAt(movingRows[i], destinationIndex++);
						if (OnDBOrderChange != null)
							OnDBOrderChange(this, new DBChangedEventArgs(parameters[i], DBChangement.Insert));
						Application.DoEvents();
					}
          Logger.LogError(this, "MOVE " + destinationAndKeys);
          UpdatePositionsFromIndex(0, true);
				}
				else
				{
					Logger.LogError(this, "Move - The Dry end table was null.");
				}
			}
			catch (Exception ex)
			{
				string log = "Move (Order DB) - Error moving Record.";
				Logger.LogError(this, log, ex);
				ManageError(log);
			}
			finally
			{
				Application.DoEvents();
			}
      Logger.LogError(this, this.GetFirstOrderIDs());
    }

		/// <summary>
		/// Insert new order into the list at the specified position
		/// </summary>
		/// <param name="values">a string specifing the index (first token) where to insert
		/// the new setup which values are specified in the second part (from second token) of the string</param>
		public void InsertInPosition(string values)
		{
			int firstSeparatorPosition = values.IndexOf(Command.PARAMETER_SEPARATOR);
			if (firstSeparatorPosition == -1)
			{
				Logger.LogError(this, "InsertAfterSetup - Invalid parameters, no separator found.");
				return;
			}
			int destinationIndex = 0;
			try
			{
        //agrassi_FI23-05-08_4
        destinationIndex = Convert.ToInt32(values.Substring(0, firstSeparatorPosition)) - 1;
        //destinationIndex = Convert.ToInt32(values.Substring(0, firstSeparatorPosition));
      }
			catch (Exception e)
			{
				Logger.LogError(this, "InsertAfterSetup - error converting position (" + values.Substring(0, firstSeparatorPosition) + ")", e);
				return;
			}
			DataTable dt = order.Tables[Order.DRYEND_LIST];
			string[] parameters = values.Substring(firstSeparatorPosition + 1).Split(new Char[] { Command.PARAMETER_SEPARATOR });
			object[] elements = CreateDryEndElementsArray(parameters, destinationIndex);
			if (elements != null)
			{
				try
				{
					DataRow newRow = dt.NewRow();
					newRow.ItemArray = elements;
					dt.Rows.InsertAt(newRow, destinationIndex);
          Logger.LogError(this, "INSERT " + parameters[0] + " in pos " + (destinationIndex+1).ToString());
          if (OnDBOrderChange != null)
						OnDBOrderChange(this, new DBChangedEventArgs((string)elements[0], DBChangement.Insert));
					UpdatePositionsFromIndex(destinationIndex, true);
				}
				catch (Exception ex)
				{
					string log = "InsertAfterSetup (Order DB) - Error adding record to the DryEndList.";
					Logger.LogError(this, log, ex);
					ManageError(log);
				}
				finally
				{
					Application.DoEvents();
				}
			}
			else
				Logger.LogError(this, "InsertAfterSetup - Invalid parameters list.");
      Logger.LogError(this, this.GetFirstOrderIDs());
    }

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Modify an existing record.
		/// </summary>
		/// <param name="values">a string representing the new values</param>
		public void Mod(string values)
		{
			string[] parameters = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
			DataRow row = order.Tables[Order.DRYEND_LIST].Rows.Find(parameters[0]);
			if (row != null)
			{
				object[] elements = CreateDryEndElementsArray(parameters, Convert.ToInt32(row[Order.DEL_POSITION]));
				if (elements != null)
				{
					try
					{
						row.ItemArray = elements;
            Logger.LogError(this, "MOD " + parameters[0]);
            if (OnDBOrderChange != null)
							OnDBOrderChange(this, new DBChangedEventArgs((string)elements[0], DBChangement.Modify));
					}
					catch (Exception ex)
					{
						string log = "Mod (Order DB) - Error modifying record of the DryEndList.";
						Logger.LogError(this, log, ex);
						ManageError(log);
					}
					finally
					{
						Application.DoEvents();
					}
				}
				else
					Logger.LogError(this, "Mod - Invalid parameters list.");
			}
			else
				Logger.LogError(this, "Mod - No order in list to modify!");
      Logger.LogError(this, this.GetFirstOrderIDs());
    }

		/// <summary>
		/// Overrides the current setup
		/// </summary>
		/// <param name="values">string representig values</param>
		public void OverrideCurrentSetup(string values)
		{
			string[] parameters = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
			if (order.Tables.Contains(Order.DRYEND_LIST) && order.Tables[Order.DRYEND_LIST].Rows.Count > 0)
			{
				DataRow row = order.Tables[Order.DRYEND_LIST].Rows[0];
				object[] elements = CreateDryEndElementsArray(parameters, Convert.ToInt32(row[Order.DEL_POSITION]));
				if (elements != null)
				{
					try
					{
						row.ItemArray = elements;
						if (OnDBOrderChange != null)
							OnDBOrderChange(this, new DBChangedEventArgs((string)elements[0], DBChangement.Modify));
					}
					catch (Exception ex)
					{
						string log = "OverrideCurrentSetup (Order DB) - Error modifying record of the DryEndList.";
						Logger.LogError(this, log, ex);
						ManageError(log);
					}
					finally
					{
						Application.DoEvents();
					}
				}
				else
					Logger.LogError(this, "OverrideCurrentSetup - Invalid parameters list.");
			}
			else
				Logger.LogError(this, "OverrideCurrentSetup - No order in list to modify!");
		}

		/// <summary>
		/// Shift the queue for a change order.
		/// </summary>
		/// <param name="key">Key of the record to be deleted.</param>
		public void Shift(string key)
		{
      //agrassi_FI23-05-08_5
      //Del(key);
      string keys = "", id = "";
      int cnt=0;
      while ((id=this.order.Tables[Order.DRYEND_LIST].Rows[cnt][Order.DEL_SETUP_ID].ToString())!=key)
      {
        keys+=id+Command.PARAMETER_SEPARATOR;
        cnt++;
        Logger.LogError(this, "WARNING! SHIFT "+key+": removing also id " + id + " in pos " + cnt.ToString());
      }
      keys += key;
			this.Del(keys);
      if (OnDBOrderChange != null)
        OnDBOrderChange(this, new DBChangedEventArgs(key, DBChangement.Shift));
    }

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Clean the dataset.
		/// </summary>
		public void Clr()
		{
			if (order != null)
			{
				//ABello_FI25/01/2007_1 Wet end Queue bug in Client side
				//order.Clear();
				order.Tables[DRYEND_LIST].Clear();
				//call the refresh data on every instantiated form
				PageManager.getPageManager().refreshAllFormsData();
        if (OnDBOrderChange != null)
          OnDBOrderChange(this, new DBChangedEventArgs(string.Empty, DBChangement.Delete));
				Application.DoEvents();
      }
			else
				Logger.LogError(this, "Clr - Cannot complete, dataset was null");
		}

		#endregion

		#region Wet End methods

			/// <summary>
			/// Add a Program List record to the database.
			/// </summary>
			/// <param name="values">a string representing the values</param>
			public void WETAdd(string values)
			{
				string[] parameters = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
				if (order.Tables[WETEND_LIST].Columns.Count - 1 != parameters.Length)
				{
					Logger.LogError(this, "WETAdd - Cannot Add record because the number of the parameters is incompatible with the table");
					return;
				}
				string[] elements = new string[parameters.Length + 1];
				Array.Copy(parameters, 0, elements, 1, parameters.Length);
				elements[0] = (order.Tables[WETEND_LIST].Rows.Count + 1).ToString();
				try
				{
					//Reverse the Flute position in the single facer
					char[] SFPosition = elements[8].ToCharArray();
					Array.Reverse(SFPosition);
					elements[8] = new string(SFPosition);

					order.Tables[WETEND_LIST].Rows.Add(elements);
				}
				catch (System.Data.ConstraintException ex)
				{
					string log = "WETAdd (Order DB) - Error adding record to the WetEndList for ConstraintException.";
					Logger.LogError(this, log, ex);
				}
				catch (Exception ex)
				{
					string log = "WETAdd (Order DB) - Error adding record to the WetEndList.";
					Logger.LogError(this, log, ex);
					ManageError(log);
				}
				finally
				{
					Application.DoEvents();
				}
			}

		public void WETShift(string key)
		{
			WETDel(key);
		}

			/// <summary>
			/// Shift the WetEnd list
			/// </summary>
			/// <param name="key">Key of the record to be deleted.</param>
			public void WETDel(string key)
			{
				System.Data.DataRow dataRow = null;
				if (order != null && order.Tables.Contains(WETEND_LIST))
				{
				  dataRow = order.Tables[WETEND_LIST].Rows.Find(new object[] { key });
				}
				try
				{
				  if (dataRow != null)
				  {
						int ind = 0;
						try
						{
							ind = Convert.ToInt32(dataRow[Order.WEL_POSITION]) - 1;
						}
						catch (Exception excep)
						{
							string log = "WETDel - error converting position (" + dataRow[Order.WEL_POSITION] + ")";
							Logger.LogError(this, log, excep);
							ManageError(log);
						}
				    dataRow.Delete();
						UpdatePositionsFromIndex(ind, false);
				  }
				  else
				  {
						string log = "WETDel - specified key (" + key + ") not found.";
						Logger.LogError(this, log);
						//ManageError(log);
				  }
				}
				catch (Exception ex)
				{
					string log = "WETDel (Order DB) - Error shifting the WetEnd list.";
					Logger.LogError(this, log , ex);
					ManageError(log);
				}
				finally
				{
					Application.DoEvents();
				}
			}

			/// <summary>
			/// Implementation of the IDatabase interface.
			/// Clean the dataset.
			/// </summary>
			public void WETClr()
			{
				if (order != null)
				{
					order.Tables[WETEND_LIST].Clear();
					Application.DoEvents();
				}
				else
					Logger.LogError(this, "WETClr - Cannot complete, dataset was null");
			}

			/// <summary>
			/// Implementation of the IDatabase interface.
			/// Search record into dataset.
			/// </summary>
			public DataRow WETGetRow(string programID, string programModID)
			{
				DataRow[] rows = null;
				if (order != null && order.Tables.Contains(Order.WETEND_LIST))
				{
					rows = order.Tables[Order.WETEND_LIST].Select(Order.WEL_PROGRAM_ID + " = '" + programID + "' AND " + Order.WEL_PROGRAM_MODID + " = '" + programModID + "'");
					if (rows != null && rows.Length > 0)
					{
						return rows[0];
					}
				}
				return null;
			}

			#endregion

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Compare the given cheksum with the database checksum.
		/// </summary>
		/// <param name="checksum">the checksum to compare</param>
		public void Chk(string checksum)
		{
			Logger.LogError(this, "The method \"Chk\" has been called but it is not implemented yet.");
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Load the databse.
		/// </summary>
		public void Load()
		{
			if (order != null)
			{
				order.Clear();
				try
				{
					order.ReadXml(fullPathXmlFile, XmlReadMode.IgnoreSchema);
					//call the refresh data on every instantiated form
					PageManager.getPageManager().refreshAllFormsData();
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "Load - Error during reading local order database", ex);
				}
				finally
				{
					Application.DoEvents();
				}
			}
			else
				Logger.LogError(this, "Load - Cannot complete, datset was null");
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Save permanently the database.
		/// </summary>
		public void Save()
		{
			if (order != null)
			{
				try
				{
					order.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
				}
				catch (SecurityException ex)
				{
					Logger.LogError(this, "Save - an error occured during XML serialization of the order database;", ex);
				}
				finally
				{
					Application.DoEvents();
				}
			}
			else
				Logger.LogError(this, "Save - Cannot complete, dataset was null");
		}

		/// <summary>
		/// Set the database version.
		/// Implementation of the IDatabase interface.
		/// </summary>
		/// <param name="ver">a Syncro style DateTime string that represent the date when the version was created.</param>
		public void SetVer(string ver)
		{
			if (!ver.Equals(GetVer()))
			{
				//firstly save the database
				Save();
				//update file date
				DateTime data = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(ver);
				try
				{
					File.SetLastWriteTimeUtc(fullPathXmlFile, data);
				}
				catch (FileNotFoundException ex)
				{
					Logger.LogError(this, "SetVer - can't find the file \"" + fullPathXmlFile + "\".", ex);
					return;
				}
				catch (UnauthorizedAccessException ex)
				{
					Logger.LogError(this, "SetVer - can't access the file \"" + fullPathXmlFile + "\".", ex);
					return;
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "SetVer - error trying to change the date of the file \"" + fullPathXmlFile + "\".", ex);
					return;
				}
				finally
				{
					Application.DoEvents();
				}
			}
		}

		/// <summary>
		/// Get the database version as a string.
		/// Implementation of the IDatabase interface.
		/// </summary>
		/// <returns>a Syncro style DateTime string that represent the date when the version was created.
		/// In case of error return the following string "1 1 1 1 1 1 1 1"</returns>
		public string GetVer()
		{
			DateTime data;
			try
			{
				data = File.GetLastWriteTimeUtc(fullPathXmlFile);
			}
			catch (IOException ex)
			{
				Logger.LogError(this, "GetVer - can't find the file \"" + fullPathXmlFile + "\".", ex);
				return WRONG_VERSION;
			}
			catch (UnauthorizedAccessException ex)
			{
				Logger.LogError(this, "GetVer - can't access the file \"" + fullPathXmlFile + "\".", ex);
				return WRONG_VERSION;
			}
			catch (Exception ex)
			{
				Logger.LogError(this, "GetVer - error trying to get the date of the file \"" + fullPathXmlFile + "\".", ex);
				return WRONG_VERSION;
			}
			finally
			{
				Application.DoEvents();
			}
			return SyncroConverter.ConvertDateTimeToSyncroStyleDateTimeStringNoZerosInFront(data);
		}

		/// <summary>
		/// Update the position column of every row.
		/// </summary>
		/// <param name="index">Index of the row from start to update column position.</param>
		private void UpdatePositionsFromIndex(int index, bool dry)
		{
			if (index < 0)
			{
				Logger.LogError(this, "UpdatePositionsDRYFromIndex - Invalid index.");
				return;
			}
			DataRowCollection rows = order.Tables[(dry) ? Order.DRYEND_LIST : Order.WETEND_LIST].Rows;
			for (int i = index; i < rows.Count; i++)
			{
				rows[i][(dry) ? Order.DEL_POSITION : Order.WEL_POSITION] = i + 1;
			}
		}

    private string GetFirstOrderIDs()
    {
      const int MAX_NUMBER_OF_IDs=10;
      string ids = "DRY list(tot "+this.order.Tables[Order.DRYEND_LIST].Rows.Count.ToString()+"): ";
      int numberOfIDs=Math.Min(MAX_NUMBER_OF_IDs, this.order.Tables[Order.DRYEND_LIST].Rows.Count);
      for (int i = 0; i < numberOfIDs; i++)
        ids += this.order.Tables[Order.DRYEND_LIST].Rows[i][Order.DEL_SETUP_ID] + " ";
      if (MAX_NUMBER_OF_IDs < this.order.Tables[Order.DRYEND_LIST].Rows.Count)
        ids += "...";
      return ids;
    }
  }
}

