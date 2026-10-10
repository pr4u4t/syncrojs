using System;
using System.Data;
using System.Security;
using System.IO;
using System.Windows.Forms;
using System.Text;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.DataBase
{
	/// <summary>
	/// PastOrder database.
	/// </summary>
	public class PastOrder : SyncroC.DataBase.Interfaces.IDatabase
	{
		public const string XML_FILE_NAME = "PastOrder.xml";
		private string fullPathXmlFile = null;

		/// <summary>String to be sent as version in case of error</summary>
		public static readonly string WRONG_VERSION = SyncroConverter.SYNCRO_STYLE_DATETIME_WRONG_STRING;
		private const int MAX_PAST_ORDER = 500;

		#region PastOrderList table constants

		/// <summary>Dataset Past order table name.</summary>
		public readonly static string PAST_ORDER_LIST = "PastOrderList";

		//header
		/// <summary>DryEndList column name for the position.</summary>
		public static string POL_POSITION = "position";
		/// <summary>DryEndList column name for data: "SetupData.setupId".</summary>
		public readonly static string POL_SETUP_ID = "SetupID";
		/// <summary>DryEndList column name for data: "SetupData.modId".</summary>
		public readonly static string POL_SETUP_MODID = "ModID";
		/// <summary>DryEndList column name for data: "SetupData.setupCode".</summary>
		public readonly static string POL_SETUP_CODE = "SetupCode";
		/// <summary>DryEndList column name for data: "SetupData.webWidth".</summary>
		public readonly static string POL_WEB_WIDTH = "WebWidth";
		/// <summary>DryEndList column name for data: "SetupData.trimType".</summary>
		public readonly static string POL_TRIM_TYPE = "TrimType";
		/// <summary>DryEndList column name for data: "SetupData.valid".</summary>
		public readonly static string POL_VALID = "Valid";
		/// <summary>DryEndList column name for data: "SetupData.scoreGap".</summary>
		public readonly static string POL_SCORE_GAP = "ScoreGap";
		/// <summary>DryEndList column name for data: "SetupData.quality.qualityID".</summary>
		public readonly static string POL_QUALITY_ID = "QualityID";
		/// <summary>DryEndList column name for data: "SetupData.validationData.trim".</summary>
		public readonly static string POL_TRIM = "Trim";
		/// <summary>DryEndList column name for data: "SetupData.calculationData.slitsTandem".</summary>
		public readonly static string POL_SLITS_TANDEM = "SlitsTandem";
		/// <summary>DryEndList column name for data: "SetupData.calculationData.scoresTandem".</summary>
		public readonly static string POL_SCORES_TANDEM = "ScoresTandem";
		/// <summary>DryEndList column name for data: "SetupData.calculationData.shaftMask".</summary>
		public readonly static string POL_SHAFT_MASK = "ShaftMask";
		/// <summary>DryEndList column name for data: "SetupData.AOCType.constantSystem".</summary>
		public readonly static string POL_CONSTANT_SYSTEM = "constantSystem";
		/// <summary>DryEndList column name for data: "SetupData.AOCType.tandem".</summary>
		public readonly static string POL_TANDEM = "Tandem";
		/// <summary>DryEndList column name for data: "SetupData.AOCType.knifeOnly".</summary>
		public readonly static string POL_KNIFE_ONLY = "KnifeOnly";
		/// <summary>DryEndList column name for data: "SetupData.calculationData.StackerValidCancelAOC[0]".</summary>
		public readonly static string POL_NOCHANGE_STK_0 = "NoChange_STK_0";
		/// <summary>DryEndList column name for data: "SetupData.calculationData.StackerValidCancelAOC[1]".</summary>
		public readonly static string POL_NOCHANGE_STK_1 = "NoChange_STK_1";
		/// <summary>DryEndList column name for data: "SetupData.calculationData.StackerValidCancelAOC[2]".</summary>
		public readonly static string POL_NOCHANGE_STK_2 = "NoChange_STK_2";
		/// <summary>DryEndList column name for data: "".</summary>
		public readonly static string POL_STOPTIME = "StopTime";
		/// <summary>DryEndList column name for data: "".</summary>
		public readonly static string POL_AVERAGE_SPEED = "AverageSpeed";
		/// <summary>DryEndList column name for data: "".</summary>
		public readonly static string SCRAP_RSH_UDm = "ScrapRSH_UDm";
		/// <summary>DryEndList column name for data: "".</summary>
		public readonly static string POL_FLUTE = "Flute";

		//Order n
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.segmentSheetCount", order 0.</summary>
		public readonly static string POL_SEGMENT_SHEET_COUNT_n = "SegmentSheetCount";
		/// <summary>DryEndList column name for data: "SetupData.order.orderNumber", order x.</summary>
		public readonly static string POL_ORDER_NUMBER_n = "OrderNumber";
    public readonly static string POL_CUSTOMER_NAME_n = "CustomerName";
    public readonly static string POL_STACKER_NOTE_n = "StackerNote";
    public readonly static string POL_MTH_CODE_n = "MTHCode";
    public readonly static string POL_MTH_NEXT_MACHINE_n = "MTHNextMachine";
    public readonly static string POL_LEVEL_NAME_n = "LevelName";
    public readonly static string POL_OUTS_n = "Outs";
    public readonly static string POL_CUT_TO_PATTERN_n = "CutToPattern";
    public readonly static string POL_DATA_TO_PRINT_n_m = "DataToPrint";
    //ZETA 01
    public readonly static string POL_PREPRINT_n = "PrePrint";

		//order 0
		/// <summary>DryEndList column name for data: "SetupData.order.orderNumber", order 0.</summary>
		public readonly static string POL_ORDER_NUMBER_0 = "OrderNumber0";
		/// <summary>DryEndList column name for data: "SetupData.order.delivery", order 0.</summary>
		public readonly static string POL_DELIVERY_0 = "Delivery0";
		/// <summary>DryEndList column name for data: "SetupData.order.customerData.name", order 0.</summary>
		public readonly static string POL_CUSTOMER_NAME_0 = "CustomerName0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.sheetWidth", order 0.</summary>
		public readonly static string POL_SHEET_WIDTH_0 = "SheetWidth0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.outs", order 0.</summary>
		public readonly static string POL_OUTS_0 = "Outs0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreMeasures", order 0.</summary>
		public readonly static string POL_SCORE_MEASURES_0 = "ScoreMeasures0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreType", order 0.</summary>
		public readonly static string POL_SCORE_TYPE_0 = "ScoreType0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.positType", order 0.</summary>
		public readonly static string POL_POSIT_TYPE_0 = "PositType0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.teletwinOffset", order 0.</summary>
		public readonly static string POL_TELETWIN_OFFSET_0 = "TeleTwinOffset0";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreGap", order 0.</summary>
		public readonly static string POL_SCORE_GAP_0 = "ScoreGap0";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.knifeLevel", order 0.</summary>
		public readonly static string POL_LEVEL_NAME_0 = "LevelName0";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.sheetLength", order 0.</summary>
		public readonly static string POL_SHEET_LENGTH_0 = "SheetLength0";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.segmentSheetCount", order 0.</summary>
		public readonly static string POL_SEGMENT_SHEET_COUNT_0 = "SegmentSheetCount0";
		/// <summary>DryEndList column name for data: "SetupData.order.stackerData.sheetsPerStack", order 0.</summary>
		public readonly static string POL_SHEETS_PER_STACK_0 = "SheetsPerStack0";
		/// <summary>DryEndList column name for data: "SetupData.order.materialHandlingData.stacksPerPallet", order 0.</summary>
		public readonly static string POL_STACKS_PER_PALLET_0 = "StacksPerPallet0";
		/// <summary>DryEndList column name for data: "SetupData.order.materialHandlingData.sending", order 0.</summary>
		public readonly static string POL_SENDING_0 = "Sending0";
		/// <summary>DryEndList column name for data: "", order 0.</summary>
		public readonly static string POL_CUTS_PRODUCED_0 = "CutsProduced0";
		/// <summary>DryEndList column name for data: "", order 0.</summary>
		public readonly static string POL_CUTS_PRODUCED_BAD_0 = "ProducedBad0";
		/// <summary>DryEndList column name for data: "", order 0.</summary>
    public readonly static string POL_CUTS_PRODUCED_DEFAULT_0 = "ProducedDefault0";
    //RFAZZI_FI07/06/2011_3. Aggiunto campo metri scartati nel gruppo di scarto della Master per livello nelle statistiche degli ordini prodotti. 
    /// <summary>DryEndList column name for data: "", order 0.</summary>
    public readonly static string POL_SCRAP_MADE_DISCHARGE_GROUP_0 = "ScrapMadeDischargeGroup0";
    //RFAZZI_FI07/06/2011_2. Adesso lato client utiliziamo la lunghezza di taglio a default memorizzata nei PastOrders anzichè la lunghezza prevista nel LineConfig. 
    /// <summary>DryEndList column name for data: "", order 0.</summary>
    public readonly static string POL_CUT_LENGTH_DEFAULT_0 = "cutlenghtdefault0_udmm";
    /// <summary>DryEndList column name for data: "SetupData.order.knife.cutToMark", order 0.</summary>
		public readonly static string POL_CUT_TO_MARK_0 = "CutToMark0";

    public readonly static string POL_CUT_TO_PATTERN_0 = "CutToPattern0";
    public readonly static string POL_DATA_TO_PRINT_0_1 = "DataToPrint0_1";
    public readonly static string POL_DATA_TO_PRINT_0_2 = "DataToPrint0_2";
    public readonly static string POL_DATA_TO_PRINT_0_3 = "DataToPrint0_3";
    public readonly static string POL_DATA_TO_PRINT_0_4 = "DataToPrint0_4";
    public readonly static string POL_STACKER_CONT_STACK_0 = "StackerContStack0";
    public readonly static string POL_STACKER_PALLET_0 = "StackerPallet0";
    public readonly static string POL_STACKER_NOTE_0 = "StackerNote0";
    public readonly static string POL_CUSTOMER_ADDRESS_0 = "CustAddress0";
    public readonly static string POL_MTH_CODE_0 = "MTHCode0";
    public readonly static string POL_MTH_NEXT_MACHINE_0 = "MTHNextMachine0";
    public readonly static string POL_PALLET_LAYOUT_0 = "numberOfPalletLayout0";
    public readonly static string POL_AMOUNT_PALLET_COPY_0 = "amountOfPalletCopy0";
    public readonly static string POL_PASTORDER_LAYOUT_0 = "numberOfPastOrderLayout0";
    public readonly static string POL_AMOUNT_PASTORDER_COPY_0 = "amountOfPastOrderCopy0";
    public readonly static string POL_PREPRINT_0 = "PrePrint0";
    //agrassi_FI24-02-2010_3
    public readonly static string POL_DISCHARGE_SIDE_0 = "DischargeSide0";
    public readonly static string POL_BALANCE_0 = "Balance0";

    //order 1
		/// <summary>DryEndList column name for data: "SetupData.order.orderNumber", order 1.</summary>
		public readonly static string POL_ORDER_NUMBER_1 = "OrderNumber1";
		/// <summary>DryEndList column name for data: "SetupData.order.delivery", order 1.</summary>
		public readonly static string POL_DELIVERY_1 = "Delivery1";
		/// <summary>DryEndList column name for data: "SetupData.order.customerData.name", order 1.</summary>
		public readonly static string POL_CUSTOMER_NAME_1 = "CustomerName1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.sheetWidth", order 1.</summary>
		public readonly static string POL_SHEET_WIDTH_1 = "SheetWidth1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.outs", order 1.</summary>
		public readonly static string POL_OUTS_1 = "Outs1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreMeasures", order 1.</summary>
		public readonly static string POL_SCORE_MEASURES_1 = "ScoreMeasures1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreType", order 1.</summary>
		public readonly static string POL_SCORE_TYPE_1 = "ScoreType1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.positType", order 1.</summary>
		public readonly static string POL_POSIT_TYPE_1 = "PositType1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.teletwinOffset", order 1.</summary>
		public readonly static string POL_TELETWIN_OFFSET_1 = "TeleTwinOffset1";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreGap", order 1.</summary>
		public readonly static string POL_SCORE_GAP_1 = "ScoreGap1";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.knifeLevel", order 1.</summary>
		public readonly static string POL_LEVEL_NAME_1 = "LevelName1";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.sheetLength", order 1.</summary>
		public readonly static string POL_SHEET_LENGTH_1 = "SheetLength1";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.segmentSheetCount", order 1.</summary>
		public readonly static string POL_SEGMENT_SHEET_COUNT_1 = "SegmentSheetCount1";
		/// <summary>DryEndList column name for data: "SetupData.order.stackerData.sheetsPerStack", order 1.</summary>
		public readonly static string POL_SHEETS_PER_STACK_1 = "SheetsPerStack1";
		/// <summary>DryEndList column name for data: "SetupData.order.materialHandlingData.stacksPerPallet", order 1.</summary>
		public readonly static string POL_STACKS_PER_PALLET_1 = "StacksPerPallet1";
		/// <summary>DryEndList column name for data: "SetupData.order.materialHandlingData.sending", order 1.</summary>
		public readonly static string POL_SENDING_1 = "Sending1";
		/// <summary>DryEndList column name for data: "", order 1.</summary>
		public readonly static string POL_CUTS_PRODUCED_1 = "CutsProduced1";
		/// <summary>DryEndList column name for data: "", order 1.</summary>
		public readonly static string POL_CUTS_PRODUCED_BAD_1 = "ProducedBad1";
		/// <summary>DryEndList column name for data: "", order 1.</summary>
		public readonly static string POL_CUTS_PRODUCED_DEFAULT_1 = "ProducedDefault1";
    //RFAZZI_FI07/06/2011_3. Aggiunto campo metri scartati nel gruppo di scarto della Master per livello nelle statistiche degli ordini prodotti. 
    /// <summary>DryEndList column name for data: "", order 1.</summary>
    public readonly static string POL_SCRAP_MADE_DISCHARGE_GROUP_1 = "ScrapMadeDischargeGroup1";
    //RFAZZI_FI07/06/2011_2. Adesso lato client utiliziamo la lunghezza di taglio a default memorizzata nei PastOrders anzichè la lunghezza prevista nel LineConfig. 
    /// <summary>DryEndList column name for data: "", order 1.</summary>
    public readonly static string POL_CUT_LENGTH_DEFAULT_1 = "cutlenghtdefault1_udmm";
    /// <summary>DryEndList column name for data: "SetupData.order.knife.cutToMark", order 1.</summary>
		public readonly static string POL_CUT_TO_MARK_1 = "CutToMark1";

    public readonly static string POL_CUT_TO_PATTERN_1 = "CutToPattern1";
    public readonly static string POL_DATA_TO_PRINT_1_1 = "DataToPrint1_1";
    public readonly static string POL_DATA_TO_PRINT_1_2 = "DataToPrint1_2";
    public readonly static string POL_DATA_TO_PRINT_1_3 = "DataToPrint1_3";
    public readonly static string POL_DATA_TO_PRINT_1_4 = "DataToPrint1_4";
    public readonly static string POL_STACKER_CONT_STACK_1 = "StackerContStack1";
    public readonly static string POL_STACKER_PALLET_1 = "StackerPallet1";
    public readonly static string POL_STACKER_NOTE_1 = "StackerNote1";
    public readonly static string POL_CUSTOMER_ADDRESS_1 = "CustAddress1";
    public readonly static string POL_MTH_CODE_1 = "MTHCode1";
    public readonly static string POL_MTH_NEXT_MACHINE_1 = "MTHNextMachine1";
    public readonly static string POL_PALLET_LAYOUT_1 = "numberOfPalletLayout1";
    public readonly static string POL_AMOUNT_PALLET_COPY_1 = "amountOfPalletCopy1";
    public readonly static string POL_PASTORDER_LAYOUT_1 = "numberOfPastOrderLayout1";
    public readonly static string POL_AMOUNT_PASTORDER_COPY_1 = "amountOfPastOrderCopy1";
    public readonly static string POL_PREPRINT_1 = "PrePrint1";
    //agrassi_FI24-02-2010_3
    public readonly static string POL_DISCHARGE_SIDE_1 = "DischargeSide1";
    public readonly static string POL_BALANCE_1 = "Balance1";
    
    //order 2
		/// <summary>DryEndList column name for data: "SetupData.order.orderNumber", order 2.</summary>
		public readonly static string POL_ORDER_NUMBER_2 = "OrderNumber2";
		/// <summary>DryEndList column name for data: "SetupData.order.delivery", order 2.</summary>
		public readonly static string POL_DELIVERY_2 = "Deliver2";
		/// <summary>DryEndList column name for data: "SetupData.order.customerData.name", order 2.</summary>
		public readonly static string POL_CUSTOMER_NAME_2 = "CustomerName2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.sheetWidth", order 2.</summary>
		public readonly static string POL_SHEET_WIDTH_2 = "SheetWidth2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.outs", order 2.</summary>
		public readonly static string POL_OUTS_2 = "Outs2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreMeasures", order 2.</summary>
		public readonly static string POL_SCORE_MEASURES_2 = "ScoreMeasures2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreType", order 2.</summary>
		public readonly static string POL_SCORE_TYPE_2 = "ScoreType2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.positType", order 2.</summary>
		public readonly static string POL_POSIT_TYPE_2 = "PositType2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.teletwinOffset", order 2.</summary>
		public readonly static string POL_TELETWIN_OFFSET_2 = "TeleTwinOffset2";
		/// <summary>DryEndList column name for data: "SetupData.order.twinData.scoreGap", order 2.</summary>
		public readonly static string POL_SCORE_GAP_2 = "ScoreGap2";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.knifeLevel", order 2.</summary>
		public readonly static string POL_LEVEL_NAME_2 = "LevelName2";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.sheetLength", order 2.</summary>
		public readonly static string POL_SHEET_LENGTH_2 = "SheetLength2";
		/// <summary>DryEndList column name for data: "SetupData.order.knifeData.segmentSheetCount", order 2.</summary>
		public readonly static string POL_SEGMENT_SHEET_COUNT_2 = "SegmentSheetCount2";
		/// <summary>DryEndList column name for data: "SetupData.order.stackerData.sheetsPerStack", order 2.</summary>
		public readonly static string POL_SHEETS_PER_STACK_2 = "SheetsPerStack2";
		/// <summary>DryEndList column name for data: "SetupData.order.MaterialHandlingData.stacksPerPallet", order 2.</summary>
		public readonly static string POL_STACKS_PER_PALLET_2 = "StacksPerPallet2";
		/// <summary>DryEndList column name for data: "SetupData.order.MaterialHandlingData.palletSending", order 2.</summary>
		public readonly static string POL_SENDING_2 = "Sending2";
		/// <summary>DryEndList column name for data: "", order 2.</summary>
		public readonly static string POL_CUTS_PRODUCED_2 = "CutsProduced2";
		/// <summary>DryEndList column name for data: "", order 2.</summary>
		public readonly static string POL_CUTS_PRODUCED_BAD_2 = "ProducedBad2";
		/// <summary>DryEndList column name for data: "", order 2.</summary>
		public readonly static string POL_CUTS_PRODUCED_DEFAULT_2 = "ProducedDefault2";
    //RFAZZI_FI07/06/2011_3. Aggiunto campo metri scartati nel gruppo di scarto della Master per livello nelle statistiche degli ordini prodotti. 
    /// <summary>DryEndList column name for data: "", order 2.</summary>
    public readonly static string POL_SCRAP_MADE_DISCHARGE_GROUP_2 = "ScrapMadeDischargeGroup2";
    //RFAZZI_FI07/06/2011_2. Adesso lato client utiliziamo la lunghezza di taglio a default memorizzata nei PastOrders anzichè la lunghezza prevista nel LineConfig. 
    /// <summary>DryEndList column name for data: "", order 2.</summary>
    public readonly static string POL_CUT_LENGTH_DEFAULT_2 = "cutlenghtdefault2_udmm";
    /// <summary>DryEndList column name for data: "SetupData.order.knife.cutToMark", order 2.</summary>
		public readonly static string POL_CUT_TO_MARK_2 = "CutToMark2";

    public readonly static string POL_CUT_TO_PATTERN_2 = "CutToPattern2";
    public readonly static string POL_DATA_TO_PRINT_2_1 = "DataToPrint2_1";
    public readonly static string POL_DATA_TO_PRINT_2_2 = "DataToPrint2_2";
    public readonly static string POL_DATA_TO_PRINT_2_3 = "DataToPrint2_3";
    public readonly static string POL_DATA_TO_PRINT_2_4 = "DataToPrint2_4";
    public readonly static string POL_STACKER_CONT_STACK_2 = "StackerContStack2";
    public readonly static string POL_STACKER_PALLET_2 = "StackerPallet2";
    public readonly static string POL_STACKER_NOTE_2 = "StackerNote2";
    public readonly static string POL_CUSTOMER_ADDRESS_2 = "CustAddress2";
    public readonly static string POL_MTH_CODE_2 = "MTHCode2";
    public readonly static string POL_MTH_NEXT_MACHINE_2 = "MTHNextMachine2";
    public readonly static string POL_PALLET_LAYOUT_2 = "numberOfPalletLayout2";
    public readonly static string POL_AMOUNT_PALLET_COPY_2 = "amountOfPalletCopy2";
    public readonly static string POL_PASTORDER_LAYOUT_2 = "numberOfPastOrderLayout2";
    public readonly static string POL_AMOUNT_PASTORDER_COPY_2 = "amountOfPastOrderCopy2";
    public readonly static string POL_PREPRINT_2 = "PrePrint2";
    //agrassi_FI24-02-2010_3
    public readonly static string POL_DISCHARGE_SIDE_2 = "DischargeSide2";
    public readonly static string POL_BALANCE_2 = "Balance2";

		//other
		/// <summary>DryEndList column name for Begin time.</summary>
		public readonly static string POL_BEGIN_TIME = "BeginTime";
		/// <summary>DryEndList column name for Finish time.</summary>
		public readonly static string POL_FINISH_TIME = "FinishTime";
		/// <summary>DryEndList column name for Shift start time.</summary>
		public readonly static string POL_SHIFT_START_TIME = "ShiftStartTime";
		/// <summary>DryEndList column name for .</summary>
		public readonly static string POL_SPARE_DATETIME_1 = "SpareDateTime1";
		/// <summary>DryEndList column name for .</summary>
		public readonly static string POL_SPARE_DATETIME_2 = "SpareDateTime2";
		/// <summary>DryEndList column name for .</summary>
		public readonly static string POL_SPARE_DATETIME_3 = "SpareDateTime3";
		/// <summary>DryEndList column name for .</summary>
		public readonly static string POL_SPARE_DATETIME_4 = "SpareDateTime4";
		/// <summary>DryEndList column name for Shift ID.</summary>
		public readonly static string POL_SHIFT_ID = "ShiftID";
		/// <summary>DryEndList column name for Job.</summary>
		public readonly static string POL_JOB = "Job";
		/// <summary>DryEndList column name for Comm.</summary>
		public readonly static string POL_COMM = "Comm";
    /// <summary>DryEndList column name for .</summary>
    //agrassi_FI26-05-08_3
    public readonly static string POL_OFFICE_ID = "OfficeID";
    public readonly static string POL_SPARE_4 = "Spare4";
		/// <summary>DryEndList column name for .</summary>
		//public readonly static string POL_SPARE_5 = "Spare5";
		/// <summary>DryEndList column name for .</summary>
		public readonly static string POL_SPARE_6 = "Spare6";
		/// <summary>DryEndList column name for .</summary>
		public readonly static string POL_SPARE_7 = "Spare7";
		/// <summary>DryEndList column name for .</summary>
		public readonly static string POL_SPARE_8 = "Spare8";
		/// <summary>DryEndList column name for .</summary>
		public readonly static string POL_SPARE_9 = "Spare9";
		/// <summary>DryEndList column name for .</summary>
		public readonly static string POL_SPARE_10 = "Spare10";

		#endregion

		/// <summary>
		/// PastOrders dataset reference
		/// </summary>
		private DataSet pastOrder = null;

		/// <summary>
		/// Delegate for handling OnDBChange events.
		/// </summary>
		public delegate void DBChangedHandler(object sender, DBChangedEventArgs args);

		/// <summary>
		/// Event raised when a changement (delete, add or modify of a record) occurs into PastOrder database.
		/// </summary>
		public event DBChangedHandler OnDBPastOrderChange;

		public PastOrder()
		{
			this.fullPathXmlFile = Settings.getSettings().paths.ClientData + XML_FILE_NAME;
		}

    private DataSet generateDataSet()
    {
      DataSet pastOrder = new DataSet();
      pastOrder.CaseSensitive = true;

      //table
      pastOrder.Tables.Add(PastOrder.PAST_ORDER_LIST);

      #region PastOrderList table

      //columns: header
      /*0*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(new DataColumn(PastOrder.POL_POSITION, typeof(int)));
      /*1*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SETUP_ID);							//SetupData.setupId
      /*2*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SETUP_MODID);						//SetupData.modId
      /*3*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SETUP_CODE);						//SetupData.setupCode
      /*4*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_WEB_WIDTH);							//SetupData.webWidth
      /*5*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_TRIM_TYPE);							//SetupData.trimType
      /*6*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_VALID);									//SetupData.valid
      /*7*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCORE_GAP);							//SetupData.scoreGap
      /*8*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_QUALITY_ID);						//SetupData.quality.qualityID
      /*9*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_TRIM);									//SetupData.validationData.trim
      /*10*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SLITS_TANDEM);					//SetupData.calculationData.slitsTandem
      /*11*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCORES_TANDEM);					//SetupData.calculationData.scoresTandem
      /*12*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SHAFT_MASK);						//SetupData.calculationData.shaftMask
      /*13*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CONSTANT_SYSTEM);				//SetupData.AOCType.constantSystem
      /*14*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_TANDEM);								//SetupData.AOCType.tandem
      /*15*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_KNIFE_ONLY);						//SetupData.AOCType.knifeOnly
      /*16*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_NOCHANGE_STK_0);				//SetupData.calculationData.StackerValidCancelAOC[0]
      /*17*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_NOCHANGE_STK_1);				//SetupData.calculationData.StackerValidCancelAOC[1]
      /*18*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_NOCHANGE_STK_2);				//SetupData.calculationData.StackerValidCancelAOC[2]

      /*19*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STOPTIME);							//
      /*20*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_AVERAGE_SPEED);					//
      /*21*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.SCRAP_RSH_UDm);								//
      /*22*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_FLUTE);								//

      //columns: order 0
      /*23*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_ORDER_NUMBER_0);				//SetupData.order.orderNumber
      /*24*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(new DataColumn(PastOrder.POL_DELIVERY_0, typeof(DateTime)));
      /*25*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUSTOMER_NAME_0);				//SetupData.order.customerData.name
      /*26*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SHEET_WIDTH_0);					//SetupData.order.twinData.sheetWidth
      /*27*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_OUTS_0);								//SetupData.order.twinData.outs
      /*28*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCORE_MEASURES_0);			//SetupData.order.twinData.scoreMeasures
      /*29*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCORE_TYPE_0);					//SetupData.order.twinData.scoreType
      /*30*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_POSIT_TYPE_0);					//SetupData.order.twinData.positType
      /*31*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_TELETWIN_OFFSET_0);			//SetupData.order.twinData.teletwinOffset
      /*32*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCORE_GAP_0);						//SetupData.order.twinData.scoreGap
      /*33*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_LEVEL_NAME_0);					//SetupData.order.knife.knifeLevel
      /*34*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SHEET_LENGTH_0);				//SetupData.order.knife.sheetLength
      /*35*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SEGMENT_SHEET_COUNT_0);	//SetupData.order.knife.segmentSheetCount
      /*36*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SHEETS_PER_STACK_0);		//SetupData.order.knife.sheetPerStack
      /*37*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STACKS_PER_PALLET_0);		//SetupData.order.knife.stacksPerPallet
      /*38*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SENDING_0);							//SetupData.order.knife.palletSending
      /*39*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUTS_PRODUCED_0);				//
      /*40*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUTS_PRODUCED_BAD_0);								//
      /*41*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUTS_PRODUCED_DEFAULT_0);								//

      //columns: order 1
      /*42*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_ORDER_NUMBER_1);				//SetupData.order.orderNumber
      /*43*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(new DataColumn(PastOrder.POL_DELIVERY_1, typeof(DateTime)));
      /*44*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUSTOMER_NAME_1);				//SetupData.order.customerData.name
      /*45*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SHEET_WIDTH_1);					//SetupData.order.twinData.sheetWidth
      /*46*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_OUTS_1);								//SetupData.order.twinData.outs
      /*47*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCORE_MEASURES_1);			//SetupData.order.twinData.scoreMeasures
      /*48*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCORE_TYPE_1);					//SetupData.order.twinData.scoreType
      /*49*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_POSIT_TYPE_1);					//SetupData.order.twinData.positType
      /*50*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_TELETWIN_OFFSET_1);			//SetupData.order.twinData.teletwinOffset
      /*51*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCORE_GAP_1);						//SetupData.order.twinData.scoreGap
      /*52*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_LEVEL_NAME_1);					//SetupData.order.knife.knifeLevel
      /*53*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SHEET_LENGTH_1);				//SetupData.order.knife.sheetLength
      /*54*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SEGMENT_SHEET_COUNT_1);	//SetupData.order.knife.segmentSheetCount
      /*55*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SHEETS_PER_STACK_1);		//SetupData.order.knife.sheetPerStack
      /*56*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STACKS_PER_PALLET_1);		//SetupData.order.knife.stacksPerPallet
      /*57*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SENDING_1);							//SetupData.order.knife.palletSending
      /*58*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUTS_PRODUCED_1);
      /*59*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUTS_PRODUCED_BAD_1);								//
      /*60*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUTS_PRODUCED_DEFAULT_1);								//

      //columns: order 2
      /*61*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_ORDER_NUMBER_2);				//SetupData.order.orderNumber
      /*62*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(new DataColumn(PastOrder.POL_DELIVERY_2, typeof(DateTime)));
      /*63*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUSTOMER_NAME_2);				//SetupData.order.customerData.name
      /*64*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SHEET_WIDTH_2);					//SetupData.order.twinData.sheetWidth
      /*65*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_OUTS_2);								//SetupData.order.twinData.outs
      /*66*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCORE_MEASURES_2);			//SetupData.order.twinData.scoreMeasures
      /*67*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCORE_TYPE_2);					//SetupData.order.twinData.scoreType
      /*68*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_POSIT_TYPE_2);					//SetupData.order.twinData.positType
      /*69*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_TELETWIN_OFFSET_2);			//SetupData.order.twinData.teletwinOffset
      /*70*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCORE_GAP_2);						//SetupData.order.twinData.scoreGap
      /*71*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_LEVEL_NAME_2);					//SetupData.order.knife.knifeLevel
      /*72*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SHEET_LENGTH_2);				//SetupData.order.knife.sheetLength
      /*73*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SEGMENT_SHEET_COUNT_2);	//SetupData.order.knife.segmentSheetCount
      /*74*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SHEETS_PER_STACK_2);		//SetupData.order.knife.sheetPerStack
      /*75*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STACKS_PER_PALLET_2);		//SetupData.order.knife.stacksPerPallet
      /*76*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SENDING_2);							//SetupData.order.knife.palletSending
      /*77*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUTS_PRODUCED_2);
      /*78*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUTS_PRODUCED_BAD_2);								//
      /*79*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUTS_PRODUCED_DEFAULT_2);								//

      //others
      /*80*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(new DataColumn(PastOrder.POL_BEGIN_TIME, typeof(DateTime)));
      /*81*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(new DataColumn(PastOrder.POL_FINISH_TIME, typeof(DateTime)));
      /*82*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(new DataColumn(PastOrder.POL_SHIFT_START_TIME, typeof(DateTime)));
      /*83*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUT_TO_MARK_0);
      /*84*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUT_TO_MARK_1);
      /*85*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUT_TO_MARK_2);
      /*86*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(new DataColumn(PastOrder.POL_SPARE_DATETIME_4, typeof(DateTime)));
      /*87*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SHIFT_ID);
      /*88*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_JOB);
      /*89*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_COMM);
      /*90*/
      //agrassi_FI26-05-08_3
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_OFFICE_ID);
      //pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SPARE_5);
      /*91*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SPARE_6);
      /*92*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SPARE_7);
      /*93*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SPARE_8);
      /*94*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SPARE_9);
      /*95*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SPARE_10);
      /*96*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUT_TO_PATTERN_0);
      /*97*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DATA_TO_PRINT_0_1);
      /*98*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DATA_TO_PRINT_0_2);
      /*99*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DATA_TO_PRINT_0_3);
      /*100*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DATA_TO_PRINT_0_4);
      /*101*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STACKER_CONT_STACK_0);
      /*102*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STACKER_PALLET_0);
      /*103*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STACKER_NOTE_0);
      /*104*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUSTOMER_ADDRESS_0);
      /*105*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_MTH_CODE_0);
      /*106*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_MTH_NEXT_MACHINE_0);
      /*107*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_PALLET_LAYOUT_0);
      /*108*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_AMOUNT_PALLET_COPY_0);
      /*109*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_PASTORDER_LAYOUT_0);
      /*110*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_AMOUNT_PASTORDER_COPY_0);
      /*111*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_PREPRINT_0);
      //agrassi_FI24-02-2010_3
      /*112*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DISCHARGE_SIDE_0);
      /*113*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_BALANCE_0);

      /*114*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUT_TO_PATTERN_1);
      /*115*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DATA_TO_PRINT_1_1);
      /*116*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DATA_TO_PRINT_1_2);
      /*117*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DATA_TO_PRINT_1_3);
      /*118*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DATA_TO_PRINT_1_4);
      /*119*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STACKER_CONT_STACK_1);
      /*120*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STACKER_PALLET_1);
      /*121*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STACKER_NOTE_1);
      /*122*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUSTOMER_ADDRESS_1);
      /*123*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_MTH_CODE_1);
      /*124*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_MTH_NEXT_MACHINE_1);
      /*125*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_PALLET_LAYOUT_1);
      /*126*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_AMOUNT_PALLET_COPY_1);
      /*127*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_PASTORDER_LAYOUT_1);
      /*128*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_AMOUNT_PASTORDER_COPY_1);
      /*129*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_PREPRINT_1);

      //agrassi_FI24-02-2010_3
      /*130*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DISCHARGE_SIDE_1);
      /*131*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_BALANCE_1);

      /*132*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUT_TO_PATTERN_2);
      /*133*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DATA_TO_PRINT_2_1);
      /*134*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DATA_TO_PRINT_2_2);
      /*135*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DATA_TO_PRINT_2_3);
      /*136*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DATA_TO_PRINT_2_4);
      /*137*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STACKER_CONT_STACK_2);
      /*138*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STACKER_PALLET_2);
      /*139*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_STACKER_NOTE_2);
      /*140*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUSTOMER_ADDRESS_2);
      /*141*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_MTH_CODE_2);
      /*142*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_MTH_NEXT_MACHINE_2);
      /*143*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_PALLET_LAYOUT_2);
      /*144*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_AMOUNT_PALLET_COPY_2);
      /*145*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_PASTORDER_LAYOUT_2);
      /*146*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_AMOUNT_PASTORDER_COPY_2);
      /*147*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_PREPRINT_2);
      //agrassi_FI24-02-2010_3
      /*148*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_DISCHARGE_SIDE_2);
      /*149*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_BALANCE_2);


      /*150*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SPARE_4);

      /*151*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUT_LENGTH_DEFAULT_0);  //RFAZZI_FI07/06/2011_2. Adesso lato client utiliziamo la lunghezza di taglio a default memorizzata nei PastOrders anzichè la lunghezza prevista nel LineConfig. 
      /*152*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUT_LENGTH_DEFAULT_1);  //RFAZZI_FI07/06/2011_2. Adesso lato client utiliziamo la lunghezza di taglio a default memorizzata nei PastOrders anzichè la lunghezza prevista nel LineConfig. 
      /*153*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_CUT_LENGTH_DEFAULT_2);  //RFAZZI_FI07/06/2011_2. Adesso lato client utiliziamo la lunghezza di taglio a default memorizzata nei PastOrders anzichè la lunghezza prevista nel LineConfig. 
      
      //RFAZZI_FI07/06/2011_3. Aggiunto campo metri scartati nel gruppo di scarto della Master per livello nelle statistiche degli ordini prodotti. 
      /*154*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCRAP_MADE_DISCHARGE_GROUP_0);  //RFAZZI_FI07/06/2011_2. Adesso lato client utiliziamo la lunghezza di taglio a default memorizzata nei PastOrders anzichè la lunghezza prevista nel LineConfig. 
      /*155*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCRAP_MADE_DISCHARGE_GROUP_1);  //RFAZZI_FI07/06/2011_2. Adesso lato client utiliziamo la lunghezza di taglio a default memorizzata nei PastOrders anzichè la lunghezza prevista nel LineConfig. 
      /*156*/
      pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Columns.Add(PastOrder.POL_SCRAP_MADE_DISCHARGE_GROUP_2);  //RFAZZI_FI07/06/2011_2. Adesso lato client utiliziamo la lunghezza di taglio a default memorizzata nei PastOrders anzichè la lunghezza prevista nel LineConfig. 
      
      //key
      /*157*/
      pastOrder.Tables[PAST_ORDER_LIST].PrimaryKey = new DataColumn[]{
			/*158*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns[PastOrder.POL_SETUP_ID],
			/*159*/
      pastOrder.Tables[PAST_ORDER_LIST].Columns[PastOrder.POL_BEGIN_TIME]};

      #endregion

      return pastOrder;
    }

    public long getCount()
    {
      return this.getCount("");
    }

    public long getCount(string filter)
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.pastOrder == null)
          this.pastOrder = this.generateDataSet();

        return this.pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Rows.Count;
      }
      else
      {
        if (filter.Trim() == "")
          return SyncroC.DataBase.SqlManager.getSqlManager().getNumberOfResultForSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders LIMIT 500;");
        else
          return SyncroC.DataBase.SqlManager.getSqlManager().getNumberOfResultForSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders WHERE (" + filter + ") LIMIT 500;");
      }
    }

    public DataTable getTable(string filter)
    {
      if (filter.Trim() == "")
        return this.getTable();

      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.pastOrder == null)
          this.pastOrder = this.generateDataSet();

        return this.pastOrder.Tables[PastOrder.PAST_ORDER_LIST];
      }
      else
      {
        PastOrder.POL_POSITION = "nextval";
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("DROP SEQUENCE IF EXISTS position; CREATE TEMP SEQUENCE position START 1; SELECT nextval('position'),* FROM ( SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders WHERE (" + filter + ") ORDER BY begintime DESC LIMIT 500) AS foo;");
        dt.TableName = PastOrder.PAST_ORDER_LIST;
        return dt;
      }
    }

    public DataTable getTable()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.pastOrder == null)
          this.pastOrder = this.generateDataSet();

        return this.pastOrder.Tables[PastOrder.PAST_ORDER_LIST];
      }
      else
      {
        PastOrder.POL_POSITION = "nextval";
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("DROP SEQUENCE IF EXISTS position; CREATE TEMP SEQUENCE position START 1; SELECT nextval('position'),* FROM ( SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders ORDER BY begintime DESC LIMIT 500) AS foo;");
        dt.TableName = PastOrder.PAST_ORDER_LIST;
        return dt;
      }
    }

		public DataRow getRow(string setupID, string modID, LevelName level, ref int i)
		{
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.pastOrder == null)
          this.pastOrder = this.generateDataSet();

        DataRow[] rows = null;
        if (this.pastOrder != null && this.pastOrder.Tables.Contains(PastOrder.PAST_ORDER_LIST))
        {
          rows = this.pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Select(PastOrder.POL_SETUP_ID + " = '" + setupID + "' AND " + PastOrder.POL_SETUP_MODID + " = '" + modID + "' AND " + PastOrder.POL_LEVEL_NAME_0 + " = '" + level.ToString() + "'");
          if (rows != null && rows.Length > 0)
          {
            i = 0;
            return rows[0];
          }
          rows = this.pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Select(PastOrder.POL_SETUP_ID + " = '" + setupID + "' AND " + PastOrder.POL_SETUP_MODID + " = '" + modID + "' AND " + PastOrder.POL_LEVEL_NAME_1 + " = '" + level.ToString() + "'");
          if (rows != null && rows.Length > 0)
          {
            i = 1;
            return rows[0];
          }
          rows = this.pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Select(PastOrder.POL_SETUP_ID + " = '" + setupID + "' AND " + PastOrder.POL_SETUP_MODID + " = '" + modID + "' AND " + PastOrder.POL_LEVEL_NAME_2 + " = '" + level.ToString() + "'");
          if (rows != null && rows.Length > 0)
          {
            i = 2;
            return rows[0];
          }
        }
        return null;
      }
      else
      {
        string select = PastOrder.POL_SETUP_ID + " = '" + setupID + "' AND " + PastOrder.POL_SETUP_MODID + " = '" + modID + "' AND " + PastOrder.POL_LEVEL_NAME_0 + " = '" + level.ToString() + "'";
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders WHERE (" + select + ") ORDER BY begintime DESC LIMIT 500;");
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            i = 0;
            return dt.Rows[0];
          }
        }

        select = PastOrder.POL_SETUP_ID + " = '" + setupID + "' AND " + PastOrder.POL_SETUP_MODID + " = '" + modID + "' AND " + PastOrder.POL_LEVEL_NAME_1 + " = '" + level.ToString() + "'";
        dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders WHERE (" + select + ") ORDER BY begintime DESC LIMIT 500;");
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            i = 0;
            return dt.Rows[0];
          }
        }

        select = PastOrder.POL_SETUP_ID + " = '" + setupID + "' AND " + PastOrder.POL_SETUP_MODID + " = '" + modID + "' AND " + PastOrder.POL_LEVEL_NAME_2 + " = '" + level.ToString() + "'";
        dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders WHERE (" + select + ") ORDER BY begintime DESC LIMIT 500;");
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            i = 0;
            return dt.Rows[0];
          }
        }

        return null;
      }
		}

		public DataRow getRow(string setupID, string modID)
		{
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.pastOrder == null)
          this.pastOrder = this.generateDataSet();

        DataRow[] rows = null;
        if (this.pastOrder != null && this.pastOrder.Tables.Contains(PastOrder.PAST_ORDER_LIST))
        {
          rows = this.pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Select(PastOrder.POL_SETUP_ID + " = '" + setupID + "' AND " + PastOrder.POL_SETUP_MODID + " = '" + modID + "'");
          if (rows != null && rows.Length > 0)
          {
            return rows[0];
          }
        }
        return null;
      }
      else
      {
        string select = PastOrder.POL_SETUP_ID + " = '" + setupID + "' AND " + PastOrder.POL_SETUP_MODID + " = '" + modID + "'";
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders WHERE (" + select + ") ORDER BY begintime DESC LIMIT 500;");
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            return dt.Rows[0];
          }
        }
        return null;
      }
		}

		public DataRow getRow(string setupID)
		{
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.pastOrder == null)
          this.pastOrder = this.generateDataSet();

        DataRow[] rows = null;
        if (this.pastOrder != null && this.pastOrder.Tables.Contains(PastOrder.PAST_ORDER_LIST))
        {
          rows = this.pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Select(PastOrder.POL_SETUP_ID + " = '" + setupID + "'");
          if (rows != null && rows.Length > 0)
          {
            return rows[0];
          }
        }
        return null;
      }
      else
      {
        string select = PastOrder.POL_SETUP_ID + " = '" + setupID + "'";
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders WHERE (" + select + ") ORDER BY begintime DESC LIMIT 500;");
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            return dt.Rows[0];
          }
        }
        return null;
      }
		}

    public DataRow getRow(int position)
    {
      return this.getRow(position, "");
    }

    public DataRow getRow(int position, string filter)
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.pastOrder == null)
          this.pastOrder = this.generateDataSet();

        DataRow[] rows = null;
        if (this.pastOrder != null && this.pastOrder.Tables.Contains(PastOrder.PAST_ORDER_LIST))
        {
          rows = this.pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Select(PastOrder.POL_POSITION + " = " + position.ToString());
          if (rows != null && rows.Length > 0)
          {
            return rows[0];
          }
        }
        return null;
      }
      else
      {
        DataTable dt = 
          (filter.Trim()=="") ? 
          SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders ORDER BY begintime DESC;", 1, position - 1)
          :
          SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".pastorders WHERE (" + filter + ") ORDER BY begintime DESC;", 1, position - 1);
        if (dt != null)
        {
          if (dt.Rows.Count > 0)
          {
            return dt.Rows[0];
          }
        }
        return null;
      }
    }

		public int getIndex(DataRow dataRow)
		{
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.pastOrder == null)
          this.pastOrder = this.generateDataSet();

        if (this.pastOrder != null && pastOrder.Tables.Contains(PastOrder.PAST_ORDER_LIST))
        {
          for (int i = 0; i < this.pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Rows.Count; i++)
          {
            if (this.pastOrder.Tables[PastOrder.PAST_ORDER_LIST].Rows[i].Equals(dataRow))
              return i;
          }
        }
        return -1;
      }
      else
      {
        DataTable dt = this.getTable();
        DataRow[] rows = dt.Select(PastOrder.POL_SETUP_ID + " = " + dataRow[PastOrder.POL_SETUP_ID].ToString());

        if (rows != null)
          return Convert.ToInt32(rows[0][PastOrder.POL_POSITION]);
        else
         return -1;
      }
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
			DialogResult ret = MessageBox.Show(text, "SYNCRO DATABASE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			CommunicationManager.GetCommunicationManager().ShutDown(false,true);
		}

		/// <summary>
		/// Creates an array of string to be inserted into the dataset order (Dry End table) from the values sent by the server.
		/// </summary>
		/// <param name="parameters">array of parameters.</param>
		/// <returns>null if some invalid parameters encountered.</returns>
		private object[] CreateDryEndElementsArray(string[] parameters)
		{
			//check number of parameters that must be 3 minus of the columns.count 
			//because in the table there is also di "position", "job", "comm" columns which are valorized from the SyncroC
			if (pastOrder.Tables[PAST_ORDER_LIST].Columns.Count - 3 != parameters.Length)
			{
				Logger.LogError(this, "CreateDryEndElementsArray - Cannot Create Elements Array record because the number of the parameters is incompatible with the table.");
				return null;
			}

			object[] elements = new object[parameters.Length + 3];
			elements[0] = 1; //any "new" pastOrder which is added always must be in first position
      Array.Copy(parameters, 0, elements, 1, 87);
      try
			{
				//override column POL_TRIM_TYPE
				elements[5] = (Enum.Format(typeof(TrimType), Enum.Parse(typeof(TrimType), parameters[4]), "g"));

				//override column POL_DELIVERY_0
				elements[24] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[23]);

				//override column POL_POSIT_TYPE_0
				elements[30] = (Enum.Format(typeof(PositType), Enum.Parse(typeof(PositType), parameters[29]), "g"));
				//override column POL_KNIFE_LEVEL_0
				elements[33] = (Enum.Format(typeof(LevelName), Enum.Parse(typeof(LevelName), parameters[32]), "g"));
				//override column POL_SENDING_0
				elements[38] = (Enum.Format(typeof(PalletSending), Enum.Parse(typeof(PalletSending), parameters[37]), "g"));

				//override column POL_DELIVERY_0
				elements[43] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[42]);

				//override column POL_POSIT_TYPE_1
				elements[49] = (Enum.Format(typeof(PositType), Enum.Parse(typeof(PositType), parameters[48]), "g"));
				//override column POL_KNIFE_LEVEL_1
				elements[52] = (Enum.Format(typeof(LevelName), Enum.Parse(typeof(LevelName), parameters[51]), "g"));
				//override column POL_SENDING_1
				elements[57] = (Enum.Format(typeof(PalletSending), Enum.Parse(typeof(PalletSending), parameters[56]), "g"));

				//override column POL_DELIVERY_0
				elements[62] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[61]);

				//override column POL_POSIT_TYPE_2
				elements[68] = (Enum.Format(typeof(PositType), Enum.Parse(typeof(PositType), parameters[67]), "g"));
				//override column POL_KNIFE_LEVEL_2
				elements[71] = (Enum.Format(typeof(LevelName), Enum.Parse(typeof(LevelName), parameters[70]), "g"));
				//override column POL_SENDING_2
				elements[76] = (Enum.Format(typeof(PalletSending), Enum.Parse(typeof(PalletSending), parameters[75]), "g"));
				//override column POL_BEGIN_TIME
				elements[80] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[79]);
				//override column POL_FINISH_TIME
				elements[81] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[80]);
				//override column POL_SHIFT_START_TIME
				elements[82] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[81]);
				//override column POL_SPARE_DATETIME_4
				elements[86] = SyncroConverter.ConvertSyncroStyleDateTimeStringToDateTime(parameters[85]);

        //agrassi_FI24-02-2010_3
        elements[88] = DBManager.getDBManager().syncroConfig.GeneralSetting.Job;
        elements[89] = DBManager.getDBManager().syncroConfig.GeneralSetting.Comm;
        Array.Copy(parameters, 87, elements, 90, (parameters.Length - 87));

        //override column POL_DISCHARGE_SIDE_0
        elements[112] = (Enum.Format(typeof(DischargeSide), Enum.Parse(typeof(DischargeSide), parameters[109]), "g"));
        //override column POL_BALANCE_0
        elements[113] = (Enum.Format(typeof(Balance), Enum.Parse(typeof(Balance), parameters[110]), "g"));
        //override column POL_DISCHARGE_SIDE_1
        elements[130] = (Enum.Format(typeof(DischargeSide), Enum.Parse(typeof(DischargeSide), parameters[127]), "g"));
        //override column POL_BALANCE_1
        elements[131] = (Enum.Format(typeof(Balance), Enum.Parse(typeof(Balance), parameters[128]), "g"));
        //override column POL_DISCHARGE_SIDE_2
        elements[148] = (Enum.Format(typeof(DischargeSide), Enum.Parse(typeof(DischargeSide), parameters[145]), "g"));
        //override column POL_BALANCE_2
        elements[149] = (Enum.Format(typeof(Balance), Enum.Parse(typeof(Balance), parameters[146]), "g"));
      }
			catch (Exception ex)
			{
				Logger.LogError(this, "CreateDryEndElementsArray - Invalid parameter encountered.", ex);
				return null;
			}
			return elements;
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Add a new record to the database.
		/// </summary>
		/// <param name="values">a string representing the values.</param>
		public void Add(string values)
		{
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        PageManager.getPageManager().refreshAllFormsMySQLData();

        if (OnDBPastOrderChange != null)
          OnDBPastOrderChange(this, new DBChangedEventArgs("1", DBChangement.Insert));

        return;
      }
      else
      {
        if (this.pastOrder == null)
          this.pastOrder = this.generateDataSet();
      }

			string[] parameters = values.Split(new Char[] { Command.PARAMETER_SEPARATOR });
			DataRowCollection rows = pastOrder.Tables[PAST_ORDER_LIST].Rows;
			object[] elements = CreateDryEndElementsArray(parameters);
			if (elements != null)
			{
				//update the position of each row already present into the database and ensure database limit is not exceded
				DateTime DateTimeOldest = DateTime.MaxValue;
				int IndexOldest = -1;
				//foreach (DataRow row in rows)
				for (int i = 0; i < rows.Count; i++)
				{
					try
					{
						rows[i][PastOrder.POL_POSITION] = (int)rows[i][PastOrder.POL_POSITION] + 1;
					}
					catch (Exception e)
					{
						Logger.LogError(this, "Add (PastOrder DB) - Error incrementing other rows position", e);
					}
					if (((DateTime)rows[i][PastOrder.POL_BEGIN_TIME]) < DateTimeOldest)
					{
						DateTimeOldest = (DateTime)rows[i][PastOrder.POL_BEGIN_TIME];
						IndexOldest = i;
					}
				}
				if (rows.Count >= MAX_PAST_ORDER && IndexOldest >= 0 && IndexOldest <= rows.Count)
				{
					rows[IndexOldest].Delete();
				}

				//add new row
				try
				{
					rows.Add(elements);
					if (OnDBPastOrderChange != null)
						OnDBPastOrderChange(this, new DBChangedEventArgs(elements[0].ToString(), DBChangement.Insert));

          PageManager.getPageManager().refreshAllFormsMySQLData();
				}
				catch (System.Data.ConstraintException ex)
				{
					string log = "Add (PastOrder DB) - Error adding record to the PastOrder database for ConstraintException.";
					Logger.LogError(this, log, ex);
				}
				catch (Exception ex)
				{
					StringBuilder log = new StringBuilder("Add (PastOrder DB) - Error adding new record [");
					for (int k = 0; k < elements.Length; k++)
					{
						log.Append(k.ToString() + ": " + elements[k].ToString() + " | ");
					}
					log.Append("].");
					Logger.LogError(this, log.ToString(), ex);
					ManageError(log.ToString());
				}
				finally
				{
					Application.DoEvents();
				}
			}
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Clean the dataset.
		/// </summary>
		public void Clr()
		{
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        return;
      else
      {
        if (this.pastOrder == null)
          this.pastOrder = this.generateDataSet();
      }

			if (pastOrder != null)
			{
				pastOrder.Clear();
				//call the refresh data on every instantiated form
				PageManager.getPageManager().refreshAllFormsData();
				Application.DoEvents();
			}
			else
				Logger.LogError(this, "Clr - Cannot complete, dataset was null");
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Delete a record in the dataSet.
		/// </summary>
		public void Del(string a)
		{
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Modify a record in the dataSet.
		/// </summary>
		public void Mod(string a)
		{
		}

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
      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        return;
      else
      {
        if (this.pastOrder == null)
          this.pastOrder = this.generateDataSet();
      }

			if (pastOrder != null)
			{
				pastOrder.Clear();
				try
				{
					pastOrder.ReadXml(fullPathXmlFile, XmlReadMode.IgnoreSchema);
					//call the refresh data on every instantiated form
					PageManager.getPageManager().refreshAllFormsData();
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "Load - Error during reading local PastOrder database", ex);
				}
				finally
				{
					Application.DoEvents();
				}
			}
			else
				Logger.LogError(this, "Load - Cannot complete, dataset was null");
		}

		/// <summary>
		/// Implementation of the IDatabase interface.
		/// Save permanently the database.
		/// </summary>
    public void Save()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        if (this.pastOrder == null)
          this.pastOrder = this.generateDataSet();

        if (pastOrder != null)
        {
          try
          {
            pastOrder.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
          }
          catch (SecurityException ex)
          {
            Logger.LogError(this, "Save - an error occured during XML serialization of the PastOrder database;", ex);
          }
          finally
          {
            Application.DoEvents();
          }
        }
        else
          Logger.LogError(this, "Save - Cannot complete, dataset was null");
      }
      else
      {
        try
        {
          DataSet pastOrderMySql = new DataSet();
          pastOrderMySql.Tables.Add(this.getTable());
          pastOrderMySql.WriteXml(fullPathXmlFile, XmlWriteMode.IgnoreSchema);
        }
        catch (SecurityException ex)
        {
          Logger.LogError(this, "Save from MYSQL - an error occured during XML serialization of the PastOrder database;", ex);
        }
        finally
        {
          Application.DoEvents();
        }
      }
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
				this.Save();
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
	}
}
