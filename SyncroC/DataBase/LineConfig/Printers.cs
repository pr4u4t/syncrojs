using System;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Design;

using SyncroC.Utility;

namespace SyncroC.DataBase.LineConfig
{
	/// <summary>
	/// Summary description for PRINTERS
	/// </summary>
	/// 

	public enum LabelsToPrintType
	{
		NoLabelToPrint=0,
		LastPalletLabel=1,
		FirstAndLastPalletLabel=2,
		AllPalletLabels=3,
		AllDischargeLabels=4,
    AllStackLabels=5
	};

	public enum DateFormatType
	{
		NoDateFormat=0,
		ItalianDateFormat=1,
		EnglishDateFormat=2
	};

  [TypeConverter(typeof(Converters))]
  public class PRINTERS : SyncroC.DataBase.LineConfig.LineBase
	{

		bool enablePalletLabel;
      
      //PMASSARO_FI02/05/08 - 1 - Added To Manage Pallet Print With Hardware Signals
        bool printPalletLabelUsingDigitalInput;

		bool enablePastOrderLabel;
		bool enableShiftReport;
		bool enablePastRollLabel;								//enable Past Roll Label Printer

		string palletLabelPrinterLev1;
		string palletLabelPrinterLev2;
		string palletLabelPrinterLev3;
		string shiftReportPrinter;
		string duplicatedShiftReportPrinter;
		string pastOrderLabelPrinter1;
		string pastOrderLabelPrinter2;
		string pastOrderLabelPrinter3;
		string pastRollLabelPrinterSplicer1;
		string pastRollLabelPrinterSplicer2;
		string pastRollLabelPrinterSplicer3;
		string pastRollLabelPrinterSplicer4;
		string pastRollLabelPrinterSplicer5;
		string pastRollLabelPrinterSplicer6;
		string pastRollLabelPrinterSplicer7;
		string language;

		DateFormatType dateFormat;
		LabelsToPrintType deliveryLabelsToPrint;
		LabelsToPrintType transformLabelsToPrint;

    bool enableMultipleLayout;
    int numberOfPalletLabelCopies;
    int numberOfPastOrderLabelCopies;
    int numberOfPastRollLabelCopies;
    //agrassi_FI07-04-08_12
    int minQuantityRemainingForRollLabel;
    //agrassiFI14-04-08_1
    bool printBitmapIfRollEnded;

    //PMASSARO_FI27/05/08 - 1 - ADDED To Move All Datas Of The Labels
    int offsetXPalletLabelTransformation;
    int offsetYPalletLabelTransformation;
    int offsetXPalletLabelDelivery;
    int offsetYPalletLabelDelivery;
    int offsetXPastOrderLabelPrinting;
    int offsetYPastOrderLabelPrinting;
    int offsetXPastRollLabelPrinting;
    int offsetYPastRollLabelPrinting;

		public PRINTERS(): base()
		{
			this.sectionName = "PRINTERS";

			this.enablePalletLabel = false;
            //PMASSARO_FI02/05/08 - 1 - Added To Manage Pallet Print With Hardware Signals
            this.printPalletLabelUsingDigitalInput = false;
			this.enablePastOrderLabel = false;
			this.enableShiftReport = false;
			this.enablePastRollLabel = false;

			this.palletLabelPrinterLev1 = "LABEL_STK_1";
			this.palletLabelPrinterLev2 = "LABEL_STK_2";
			this.palletLabelPrinterLev3 = "LABEL_STK_3";

			this.shiftReportPrinter = "REPORTS";

			this.duplicatedShiftReportPrinter = "NONE";

			this.pastOrderLabelPrinter1 = "PASTORDER_LABEL";
			this.pastOrderLabelPrinter2 = "PASTORDER_LABEL";
			this.pastOrderLabelPrinter3 = "PASTORDER_LABEL";

			this.pastRollLabelPrinterSplicer1 = "PASTROLL_LABEL1";
			this.pastRollLabelPrinterSplicer2 = "PASTROLL_LABEL2";
			this.pastRollLabelPrinterSplicer3 = "PASTROLL_LABEL3";
			this.pastRollLabelPrinterSplicer4 = "PASTROLL_LABEL4";
			this.pastRollLabelPrinterSplicer5 = "PASTROLL_LABEL5";
			this.pastRollLabelPrinterSplicer6 = "PASTROLL_LABEL6";
			this.pastRollLabelPrinterSplicer7 = "PASTROLL_LABEL7";

			this.deliveryLabelsToPrint = LabelsToPrintType.AllPalletLabels;
			this.transformLabelsToPrint = LabelsToPrintType.AllPalletLabels;
			this.language = "Italian";
			this.dateFormat = DateFormatType.ItalianDateFormat;
      this.enableMultipleLayout = false;
      this.numberOfPalletLabelCopies = 0;
      this.numberOfPastOrderLabelCopies = 0;
      this.numberOfPastRollLabelCopies = 0;
      //agrassi_FI07-04-08_12
      this.minQuantityRemainingForRollLabel=100;
      //agrassiFI14-04-08_1
      this.printBitmapIfRollEnded=false;
      //PMASSARO_FI27/05/08 - 1 - ADDED To Move All Datas Of The Labels
      this.offsetXPalletLabelTransformation = 0;
      this.offsetYPalletLabelTransformation = 0;
      this.offsetXPalletLabelDelivery = 0;
      this.offsetYPalletLabelDelivery = 0;
      this.offsetXPastOrderLabelPrinting = 0;
      this.offsetYPastOrderLabelPrinting = 0;
      this.offsetXPastRollLabelPrinting = 0;
      this.offsetYPastRollLabelPrinting = 0; 
    }

    //agrassi_FI18-05-2010_1
		// ****************************  PALLET LABEL SETTINGS  **********************************************

		[Category("2. PALLET LABEL SETTINGS"), PropertyOrder(10), Description("Enable Pallet Label Printer")]
		public bool EnablePalletLabel
		{
			get { return this.enablePalletLabel; }
			set { this.enablePalletLabel = value; }
		}

    //PMASSARO_FI02/05/08 - 5 - Added To Manage Pallet Print With Hardware Signals
    [Category("2. PALLET LABEL SETTINGS"), PropertyOrder(20), Description("Print Pallet Label Using Digital Input. WARNING!!! Cancel AOC Must Be OFF And Task STK_GENERIC_CTRL Must Be Enabled.")]
    public bool PrintPalletLabelUsingDigitalInput
    {
        get { return this.printPalletLabelUsingDigitalInput; }
        set { this.printPalletLabelUsingDigitalInput = value; }
    }

    //PMASSARO_FI27/05/08 - 1 - ADDED To Move All Datas Of The Labels
    [Category("2. PALLET LABEL SETTINGS"), PropertyOrder(30), Description("Offset To Move All Data Into Labels. Negative = LEFT, Positive = RIGHT")]
    public int OffsetXPalletLabelTransformation
    {
        get { return this.offsetXPalletLabelTransformation; }
        set { this.offsetXPalletLabelTransformation = value; }
    }

    //PMASSARO_FI27/05/08 - 1 - ADDED To Move All Datas Of The Labels
    [Category("2. PALLET LABEL SETTINGS"), PropertyOrder(40), Description("Offset To Move All Data Into Labels. Negative = UP, Positive = DOWN")]
    public int OffsetYPalletLabelTransformation
    {
        get { return this.offsetYPalletLabelTransformation; }
        set { this.offsetYPalletLabelTransformation = value; }
    }

    //PMASSARO_FI27/05/08 - 1 - ADDED To Move All Datas Of The Labels
    [Category("2. PALLET LABEL SETTINGS"), PropertyOrder(50), Description("Offset To Move All Data Into Labels. Negative = LEFT, Positive = RIGHT")]
    public int OffsetXPalletLabelDelivery
    {
        get { return this.offsetXPalletLabelDelivery; }
        set { this.offsetXPalletLabelDelivery = value; }
    }

    //PMASSARO_FI27/05/08 - 1 - ADDED To Move All Datas Of The Labels
    [Category("2. PALLET LABEL SETTINGS"), PropertyOrder(60), Description("Offset To Move All Data Into Labels. Negative = UP, Positive = DOWN")]
    public int OffsetYPalletLabelDelivery
    {
        get { return this.offsetYPalletLabelDelivery; }
        set { this.offsetYPalletLabelDelivery = value; }
    }

    [Category("2. PALLET LABEL SETTINGS"), PropertyOrder(70), TypeConverter(typeof(PrinterNamesConverter)), Description("Pallet Label Printer Name - Knife Level 1")]
		public string PalletLabelPrinterLev1
		{
			get { return this.palletLabelPrinterLev1; }
			set { this.palletLabelPrinterLev1 = value; }
		}

    [Category("2. PALLET LABEL SETTINGS"), PropertyOrder(80), TypeConverter(typeof(PrinterNamesConverter)), Description("Pallet Label Printer Name - Knife Level 2")]
		public string PalletLabelPrinterLev2
		{
			get { return this.palletLabelPrinterLev2; }
			set { this.palletLabelPrinterLev2 = value; }
		}

    [Category("2. PALLET LABEL SETTINGS"), PropertyOrder(90), TypeConverter(typeof(PrinterNamesConverter)), Description("Pallet Label Printer Name - Knife Level 3")]
		public string PalletLabelPrinterLev3
		{
			get { return this.palletLabelPrinterLev3; }
			set { this.palletLabelPrinterLev3 = value; }
		}

		[Category("2. PALLET LABEL SETTINGS"), PropertyOrder(100), Description("Delivery Labels To Print")]
		public LabelsToPrintType DeliveryLabelsToPrint
		{
			get { return this.deliveryLabelsToPrint; }
			set { this.deliveryLabelsToPrint = value; }
		}

		[Category("2. PALLET LABEL SETTINGS"), PropertyOrder(110), Description("Transform Labels To Print")]
		public LabelsToPrintType TransformLabelsToPrint
		{
			get { return this.transformLabelsToPrint; }
			set { this.transformLabelsToPrint = value; }
		}

    [Category("2. PALLET LABEL SETTINGS"), PropertyOrder(120), Description("Number of copies of each pallet label")]
    public int NumberOfPalletLabelCopies
    {
      get { return this.numberOfPalletLabelCopies; }
      set { this.numberOfPalletLabelCopies = value; }
    }

		// ****************************  SHIFT REPORT SETTINGS  **********************************************

		[Category("3. SHIFT REPORT SETTINGS"), PropertyOrder(10), Description("Enable Shift Report Printer")]
		public bool EnableShiftReport
		{
			get { return this.enableShiftReport; }
			set { this.enableShiftReport = value; }
		}

    [Category("3. SHIFT REPORT SETTINGS"), PropertyOrder(20), TypeConverter(typeof(PrinterNamesConverter)), Description("Shift Report Printer Name")]
		public string ShiftReportPrinter
		{
			get { return this.shiftReportPrinter; }
			set { this.shiftReportPrinter = value; }
		}

    [Category("3. SHIFT REPORT SETTINGS"), Browsable(false), PropertyOrder(30), TypeConverter(typeof(PrinterNamesConverter)), Description("Duplicated Shift Report Printer Name")]
		public string DuplicateShiftReportPrinter
		{
			get { return this.duplicatedShiftReportPrinter; }
			set { this.duplicatedShiftReportPrinter = value; }
		}
	


		// ****************************  PAST ORDER LABEL REPORT SETTINGS  **********************************************

		[Category("4. PAST ORDER LABEL REPORT SETTINGS"), PropertyOrder(10), Description("Enable Past Order Report Printer")]
		public bool EnablePastOrderLabel
		{
			get { return this.enablePastOrderLabel; }
			set { this.enablePastOrderLabel = value; }
		}

    //PMASSARO_FI27/05/08 - 1 - ADDED To Move All Datas Of The Labels
    [Category("4. PAST ORDER LABEL REPORT SETTINGS"), PropertyOrder(20), Description("Offset To Move All Data Into Labels. Negative = LEFT, Positive = RIGHT")]
    public int OffsetXPastOrderLabelPrinting
    {
        get { return this.offsetXPastOrderLabelPrinting; }
        set { this.offsetXPastOrderLabelPrinting = value; }
    }

    //PMASSARO_FI27/05/08 - 1 - ADDED To Move All Datas Of The Labels
    [Category("4. PAST ORDER LABEL REPORT SETTINGS"), PropertyOrder(30), Description("Offset To Move All Data Into Labels. Negative = UP, Positive = DOWN")]
    public int OffsetYPastOrderLabelPrinting
    {
        get { return this.offsetYPastOrderLabelPrinting; }
        set { this.offsetYPastOrderLabelPrinting = value; }
    }

    [Category("4. PAST ORDER LABEL REPORT SETTINGS"), PropertyOrder(40), TypeConverter(typeof(PrinterNamesConverter)), Description("Past Order Label Printer Name - Knife Level 1")]
		public string PastOrderLabelPrinter1
		{
			get { return this.pastOrderLabelPrinter1; }
			set { this.pastOrderLabelPrinter1 = value; }
		}

    [Category("4. PAST ORDER LABEL REPORT SETTINGS"), PropertyOrder(50), TypeConverter(typeof(PrinterNamesConverter)), Description("Past Order Label Printer Name - Knife Level 2")]
		public string PastOrderLabelPrinter2
		{
			get { return this.pastOrderLabelPrinter2; }
			set { this.pastOrderLabelPrinter2 = value; }
		}

    [Category("4. PAST ORDER LABEL REPORT SETTINGS"), PropertyOrder(60), TypeConverter(typeof(PrinterNamesConverter)), Description("Past Order Label Printer Name - Knife Level 3")]
		public string PastOrderLabelPrinter3
		{
			get { return this.pastOrderLabelPrinter3; }
			set { this.pastOrderLabelPrinter3 = value; }
		}

    [Category("4. PAST ORDER LABEL REPORT SETTINGS"), PropertyOrder(70), Description("Number of copies of each past order label")]
    public int NumberOfPastOrderLabelCopies
    {
      get { return this.numberOfPastOrderLabelCopies; }
      set { this.numberOfPastOrderLabelCopies = value; }
    }

		// ****************************  PAST ROLLS SETTINGS  **********************************************

		[Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(10), Description("Enable Past Rolls Report Printer")]
		public bool EnablePastRollLabel
		{
			get { return enablePastRollLabel; }
			set { enablePastRollLabel = value; }
		}

    //PMASSARO_FI27/05/08 - 1 - ADDED To Move All Datas Of The Labels
    [Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(20), Description("Offset To Move All Data Into Labels. Negative = LEFT, Positive = RIGHT")]
    public int OffsetXPastRollLabelPrinting
    {
        get { return this.offsetXPastRollLabelPrinting; }
        set { this.offsetXPastRollLabelPrinting = value; }
    }

    //PMASSARO_FI27/05/08 - 1 - ADDED To Move All Datas Of The Labels
    [Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(30), Description("Offset To Move All Data Into Labels. Negative = UP, Positive = DOWN")]
    public int OffsetYPastRollLabelPrinting
    {
        get { return this.offsetYPastRollLabelPrinting; }
        set { this.offsetYPastRollLabelPrinting = value; }
    }    

    [Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(40), TypeConverter(typeof(PrinterNamesConverter)), Description("Past Roll Label Printer Name - Splicer 1")]
		public string PastRollLabelPrinterSplicer1
		{
			get { return pastRollLabelPrinterSplicer1; }
			set { pastRollLabelPrinterSplicer1 = value; }
		}

    [Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(50), TypeConverter(typeof(PrinterNamesConverter)), Description("Past Roll Label Printer Name - Splicer 2")]
		public string PastRollLabelPrinterSplicer2
		{
			get { return pastRollLabelPrinterSplicer2; }
			set { pastRollLabelPrinterSplicer2 = value; }
		}

    [Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(60), TypeConverter(typeof(PrinterNamesConverter)), Description("Past Roll Label Printer Name - Splicer 3")]
		public string PastRollLabelPrinterSplicer3
		{
			get { return pastRollLabelPrinterSplicer3; }
			set { pastRollLabelPrinterSplicer3 = value; }
		}

    [Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(70), TypeConverter(typeof(PrinterNamesConverter)), Description("Past Roll Label Printer Name - Splicer 4")]
		public string PastRollLabelPrinterSplicer4
		{
			get { return pastRollLabelPrinterSplicer4; }
			set { pastRollLabelPrinterSplicer4 = value; }
		}

    [Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(80), TypeConverter(typeof(PrinterNamesConverter)), Description("Past Roll Label Printer Name - Splicer 5")]
		public string PastRollLabelPrinterSplicer5
		{
			get { return pastRollLabelPrinterSplicer5; }
			set { pastRollLabelPrinterSplicer5 = value; }
		}

    [Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(90), TypeConverter(typeof(PrinterNamesConverter)), Description("Past Roll Label Printer Name - Splicer 6")]
		public string PastRollLabelPrinterSplicer6
		{
			get { return pastRollLabelPrinterSplicer6; }
			set { pastRollLabelPrinterSplicer6 = value; }
		}

    [Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(100), TypeConverter(typeof(PrinterNamesConverter)), Description("Past Roll Label Printer Name - Splicer 7")]
		public string PastRollLabelPrinterSplicer7
		{
			get { return pastRollLabelPrinterSplicer7; }
			set { pastRollLabelPrinterSplicer7 = value; }
		}

    [Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(110), Description("Number of copies of each past roll label")]
    public int NumberOfPastRollLabelCopies
    {
      get { return this.numberOfPastRollLabelCopies; }
      set { this.numberOfPastRollLabelCopies = value; }
    }

    //agrassi_FI07-04-08_12
    [Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(120), Description("Minimum quantity (in mm) remaining in the past roll to print the roll label")]
    public int MinQuantityRemainingForRollLabel
    {
      get { return this.minQuantityRemainingForRollLabel; }
      set { this.minQuantityRemainingForRollLabel = value; }
    }

    //agrassiFI14-04-08_1
    [Category("5. PAST ROLL LABEL REPORT SETTINGS"), PropertyOrder(130), Description("Set to true to print only the bitmap when the label is not printed")]
    public bool PrintBitmapIfRollEnded
    {
      get { return this.printBitmapIfRollEnded; }
      set { this.printBitmapIfRollEnded = value; }
    }

		// ****************************  GENERAL SETTINGS  **********************************************

		[Category("1. GENERAL SETTINGS"), PropertyOrder(20), Description("Date Format: Italian=dd/mm/yy English=mm/dd/yy")]
		public DateFormatType DateFormat
		{
			get { return this.dateFormat; }
			set { this.dateFormat = value; }
		}

		[Category("1. GENERAL SETTINGS"), PropertyOrder(10), TypeConverter(typeof(LanguageConverter)), Description("Language of Text")]
		public string Language
		{
			get { return this.language; }
			set { this.language = value; }
		}

    [Category("1. GENERAL SETTINGS"), PropertyOrder(30), Description("Enable multiple layout printings")]
    public bool EnableMultipleLayout
    {
      get { return this.enableMultipleLayout; }
      set { this.enableMultipleLayout = value; }
    }
  }
}