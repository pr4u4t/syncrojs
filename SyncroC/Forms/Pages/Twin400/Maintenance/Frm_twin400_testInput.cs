using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using SyncroC.Controls;
using SyncroC.Network;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Twin400.Maintenance
{
	public class Frm_twin400_testInput : SyncroC.Forms.Pages.Twin400.Maintenance.Frm_twin400_maintenanceSubgroup
	{
    const int INPUT_CHANNEL_COUNT = 10;
    private int[] inputChannel;
    private IOChannel[] inShow;
    private IOChannel inChannel0;
    private IOChannel inChannel1;
    private IOChannel inChannel2;
    private IOChannel inChannel3;
    private IOChannel inChannel4;
    private IOChannel inChannel5;
    private IOChannel inChannel6;
    private IOChannel inChannel7;
    private IOChannel inChannel8;
    private IOChannel inChannel9;
    private string[] bitDescription1 = null, bitDescription2 = null, bitDescription3 = null, bitDescription4 = null,
      bitDescription5 = null, bitDescription6 = null, bitDescription7 = null, bitDescription8 = null,
      bitDescription9 = null, bitDescription10 = null;
    private string[][] bitDescriptions = null;
    private System.ComponentModel.IContainer components = null;
    private Twin400LogAnalyzer logAnalyzer;
    private bool debugState = false;
    private string ctrlF1Bck, ctrlF2Bck, ctrlF3Bck, ctrlF4Bck, ctrlF5Bck;
    private Timer tim_Log;
    private Label lab_path;
    protected Label lab_Time;

    public Frm_twin400_testInput()
		{
			InitializeComponent();

      #region Bit Descriptions

      //ABELLO_FI22/08/2008_1 Rivisti gli ingressi uscita per Versione di PLC 6
			//RFAZZI PROVA
			//if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion == SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver6)
			if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion == SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver6)
			{
        //IO_Logic_Cabinet_IN[0]
        this.bitDescription1 = new string[IOChannel.BITS_IN_WORD] 
        {
          "Electrical cabinet door open", //0
          "Thermostat inside cabinet", //1
          "Air conditioner Fault", //2
          "Thermal overload cooling system", //3
          "24 Vdc presence", //4
          "24 Vdc from Electrical Cabinet", //5
          "Presence 24Vdc Web Director", //6
          "Emergency on emergency - Quick Stop (norm_0)", //7
          "Thermal overload reels disconnection circuit", //8
          "Machine on emergency ( Enable )", //9
          "Anvil support fully raised", //10
          "Anvil support fully lowered", //11
          "Order change signal for Web Director", //12
          "Air Pressure diag IN_1", //13
          "Air Pressure diag IN_2", //14
          "Key Selector Tuning Blade procedure ON", //15
        };

        //IO_Logic_Cabinet_IN[1]
        this.bitDescription2 = new string[IOChannel.BITS_IN_WORD]
        {
          "Line run (aoc)", //0
          "Preparation Order Change (aoc)", //1
          "Start Order Change (aoc)", //2
          "Gap/NoGap Order Change (aoc)", //3
          "Board present under Measuring Weel (aoc)", //4
          "Order change reset (aoc)", //5
          "Cut completed Rotary Shear (aoc)", //6
          "SPARE", //7
          "Exchange blades button for Tuning sx", //8
          "Exchange blades button for Tuning dx", //9
          "Door SX open (inlet left door)", //10
          "Door DX open (inlet right door)", //11
          "Door SX open (outlet left door)", //12
          "Door DX open (outlet right door)", //13
          "Door Optional open (man aux shaft access door)", //14
          "SPARE", //15
        };

        //IO_Outlet_Console_IN[0]
        this.bitDescription3 = new string[IOChannel.BITS_IN_WORD]
        {
          "PB Machine Start/Insertion", //0
          "SPARE", //1
          "Arresto operativo (Normal stop button)", //2
          "Arresto Normale (Fast Stop Button)", //3
          "E-stop pushed", //4
          "Increase Speed", //5
          "Decrease Speed", //6
          "SPARE", //7
          "SPARE", //8
          "SPARE", //9
          "SPARE", //10
          "SPARE", //11
          "SPARE", //12
          "SPARE", //13
          "SPARE", //14
          "SPARE", //15
        };

        //IO_Outlet_Console_IN[1]
        this.bitDescription4 = new string[IOChannel.BITS_IN_WORD]
        {
          "Edge Guide Manual/Automatic", //0
          "Shifting of machine towards right", //1
          "Shifting of machine towards left", //2
          "Trim Chutes opening", //3
          "Trim Chutes closing", //4
          "Increase Scoring Pressure AX1", //5
          "Decrease Scoring Pressure AX1", //6
          "Increase Scoring Pressure AX2", //7
          "Decrease Scoring Pressure AX2", //8
          "Increase Auxiliary Scoring Pressure", //9
          "Decrease Auxiliary Scoring Pressure", //10
          "Opening of axis working", //11
          "SPARE", //12
          "SPARE", //13
          "SPARE", //14
          "SPARE", //15
        };

        //IO_Power_Cabinet_IN[0]
        this.bitDescription5 = new string[IOChannel.BITS_IN_WORD]
        {
          "Electrical Cabinet Door open", //0
          "Thermostat inside cabinet", //1
          "Air Conditioner fault", //2
          "Thermal overload conditioner/fan", //3
          "Thermal overload AC/DC converter", //4
          "Brake thermal overload plunge 1", //5
          "Brake thermal overload plunge 2", //6
          "Brake thermal overload plunge Auxiliary Shaft", //7
          "SPARE", //8
          "Thermal overload continuousTrim motor", //9
          "Thermal overload AX1 Drive", //10
          "Thermal overload AX2 Drive", //11
          "Thermal overload Auxiliay Shaft Drive", //12
          "Thermal overload Axis servo-fans", //13
          "Thermal overload machine lateral moviment", //14
          "Thermal overload scorers blow", //15
        };
        
        //IO_Power_Cabinet_IN[1]
        this.bitDescription6 = new string[IOChannel.BITS_IN_WORD]
        {
          "Drive ready AX1 rolls", //0
          "Trip active Drive AX1 rolls ", //1
          "Drive ready AX2 rolls ", //2
          "Trip active Drive AX2 rolls ", //3
          "Drive ready Aaux rolls", //4
          "Trip active Drive Aaux rolls ", //5
          "Trip active Edge Guide system", //6
          "Circuit Breaker Edge Guide Drive", //7
          "Laser Blade Regulation AX1", //8
          "Laser Blade Regulation AX2", //9
          "Regenerative Unit Fault", //10
          "Air Pressure diag IN_1", //11
          "Air Pressure diag IN_2", //12
          "Presence 24Vdc brake plunge", //13
          "Brake resistor AC Drive OK", //14
          "Ies Disconnect", //15
        };

        //IO_Sideframe_IN[0]
        this.bitDescription7 = new string[IOChannel.BITS_IN_WORD]
        {
          "Photocell jam-up AX1", //0
          "Photocell jam-up AX2", //1
          "SPARE", //2
          "SPARE", //3
          "Photocell EG internal right", //4
          "Photocell EG external right", //5
          "Photocell EG internal left", //6
          "Photocell EG external left", //7
          "Teflon check sensor AX1", //8
          "Teflon check sensor AX2", //9
          "Dust Removal System Running", //10
          "Dust Removal System Fault", //11
          "Edge Guide Max open", //12
          "Edge Guide Min close", //13
          "Manual Teflon Load AX1", //14
          "Manual Teflon Load AX2", //15
        };

        //IO_StandAlone_console_IN[0]
        this.bitDescription8 = new string[IOChannel.BITS_IN_WORD]
        {
          "Closed console door", //0
          "SPARE", //1
          "Normal stop button", //2
          "Arresto Normale (Fast Stop Button)", //3
          "E-stop pushed", //4
          "Air conditioner OK", //5
          "Thermostat max temperature", //6
          "SPARE", //7
          "SPARE", //8
          "SPARE", //9
          "SPARE", //10
          "SPARE", //11
          "SPARE", //12
          "SPARE", //13
          "SPARE", //14
          "SPARE", //15
        };
  
        //IO_StandAlone_console_IN[1]
        this.bitDescription9 = new string[IOChannel.BITS_IN_WORD]
        {
          "Manual automatic Edge Guide", //0
          "Machine manual move right", //1
          "Machine manual move left", //2
          "Open Trim Chutes Command", //3
          "Closed Trim Chutes Command", //4
          "Increase pressure Scorer AX1", //5
          "Decrease pressure Scorer AX1", //6
          "Increase pressure Scorer AX2", //7
          "Decrease pressure Scorer AX2", //8
          "Increase pressure Scorer Aaux", //9
          "Decrease pressure Scorer Aaux", //10
          "Increase speed", //11
          "Decrease speed", //12
          "Circuit breaker air fan OK", //13
          "Circuit breaker air conditioner OK", //14
          "SPARE", //15
        };

        //IO_Power_Cabinet_IN[2]
        this.bitDescription10 = new string[IOChannel.BITS_IN_WORD]
        {
          "Max movement machine right (NC)", //0
          "Max movement machine left (NC)", //1
          "Web PullRoll Drive Rdy", //2
          "Web PullRoll Drive Trip", //3
          "SPARE", //4
          "SPARE", //5
          "SPARE", //6
          "Circuit breaker continuous trim motor fan", //7
          "SPARE", //8
          "SPARE", //9
          "SPARE", //10
          "SPARE", //11
          "SPARE", //12
          "SPARE", //13
          "SPARE", //14
          "SPARE", //15
        };

      }
      else
      {
        this.bitDescription1 = new string[IOChannel.BITS_IN_WORD]
        {"Door closed", "High temperature", "Air conditioner fault", "Cooling system circuit breaker",
        "24V auxiliary", "24V DeviceNet", "24V Web Director", "Emergency to safety relay",
        "Section coils circuit breaker", "Emergency from safety relay", "Contact cut floor open NC", "Contact cut floor closed NC",
        "OC for Web Director", "Warning low air pressure", "Missing air pressure", "Key selector for tuning"};
				//RFAZZI PROVA
				//if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion == SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver1)
				if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion == SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver1)
					this.bitDescription2 = new string[IOChannel.BITS_IN_WORD]
          {"Line Run", "Prepare AOC", "Start order change", "No-gap order change",
          "Board presence at measure wheel", "Reset order change", "Rotary shear cut", "",
          "Carter cabinet side closed", "Carter 1 opp. cabinet side closed", "Carter 2 opp. cabinet side closed", "Carter 3 opp. cabinet side closed",
          "Carter 4 opp. cabinet side closed", "Carter web dir. cabinet side closed", "Carter scrap side closed", ""};
				//RFAZZI PROVA
				//else if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver2)
				else if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver2)
					this.bitDescription2 = new string[IOChannel.BITS_IN_WORD]
          {"Line Run", "Prepare AOC", "Start order change", "No-gap order change",
          "Board presence at measure wheel", "Reset order change", "Rotary shear cut", "",
          "Shift slits to left", "Shift slits to left", "Web director gate closed", "Power cabinet doors closed",
          "Logic cabinet doors closed", "Scrap gate closed", "Scrap gate closed", ""};
        this.bitDescription3 = new string[IOChannel.BITS_IN_WORD]
        {"Restore Machine", "Start Machine", "Normal Stop", "Fast stop",
        "E-Stop pushed", "Increase Speed", "Decrease Speed", "Maintenance key selector",
        "", "", "", "",
        "", "", "", ""};
        this.bitDescription4 = new string[IOChannel.BITS_IN_WORD]
        {"Automatic Edge Guide", "Move machine to right", "Move machine to left", "Open Trim Chutes",
         "Close Trim Chutes", "Decrease score gap shaft 1", "Increase score gap shaft 1", "Decrease score gap shaft 2",
         "Increase score gap shaft 2", "Decrease score gap shaft aux", "Increase score gap shaft aux", "Raise all shafts",
         "", "", "Open contact cut floor", "Close contact cut floor"};
        this.bitDescription5 = new string[IOChannel.BITS_IN_WORD]
        {"Door closed", "High temperature", "Air conditioner fault", "Air conditioner circuit breaker",
         "AC/DC converter circuit breaker", "Plunge 1 brake circuit breaker", "Plunge 2 brake circuit breaker", "Plunge aux brake circuit breaker", 
          "Trim chutes circuit breaker", "Continuous trim circuit breaker", "Shaft 1 drive circuit breaker", "Shaft 2 drive circuit breaker",
          "Shaft aux drive circuit breaker", "Slit motor fan circuit breaker", "Machine move circuit breaker", "Score motor fan circuit breaker"};
        this.bitDescription6 = new string[IOChannel.BITS_IN_WORD]
        {"Shaft 1 drive ready", "Shaft 1 drive trip", "Shaft 2 drive ready", "Shaft 2 drive trip",
         "Shaft aux drive ready", "Shaft aux drive trip", "Edge guid drive trip", "Circuit breaker Edge Guide drive",
         "Laser interrupted shaft 1", "Laser interrupted shaft 2", "Fault regen. supply unit", "Warning low air pressure",
         "Missing air pressure",  "24V plunge brake", "Fault breaking resistor AC drive", "IES disconnect"};
        this.bitDescription7 = new string[IOChannel.BITS_IN_WORD]
        {"Jam up photoeye shaft 1", "Jam up photoeye shaft 2", "Max right position proximity", "Max left position proximity",
         "Internal right photoeye", "External right photoeye", "Internal left photoeye", "External left photoeye",
         "", "", "", "",
         "Maximum open edge guide", "Maximum close edge guide", "", ""};
        this.bitDescription8 = new string[IOChannel.BITS_IN_WORD]
        {"Open console door", "Start Machine", "Normal Stop", "Fast stop",
         "E-Stop pushed", "Air conditioner fault", "Thermostat", "",
         "", "", "", "",
         "", "", "", ""};
        this.bitDescription9 = new string[IOChannel.BITS_IN_WORD]
        {"Automatic Edge Guide", "Move machine to right", "Move machine to left", "Open Trim Chutes",
         "Close Trim Chutes", "Decrease score gap shaft 1", "Increase score gap shaft 1", "Decrease score gap shaft 2",
         "Increase score gap shaft 2", "Decrease score gap shaft aux", "Increase score gap shaft aux", "Increase speed",
         "Decrease speed", "Circuit breaker transformer", "Circuit breaker heating", "Circuid breaker air conditioner"};
        this.bitDescription10 = new string[IOChannel.BITS_IN_WORD]
        {"", "", "", "",
         "", "", "", "",
         "", "", "", "",
         "", "", "", ""};
      }

			this.bitDescriptions = new string[INPUT_CHANNEL_COUNT][]
        {bitDescription1, bitDescription2, bitDescription3, bitDescription4,
         bitDescription5, bitDescription6, bitDescription7, bitDescription8,
         bitDescription9, bitDescription10};
      
      
      #endregion

      this.inputChannel = new int[INPUT_CHANNEL_COUNT];
      this.inShow = new IOChannel[INPUT_CHANNEL_COUNT];
      Type t = this.GetType();
      FieldInfo f = null;
      for (int ch = 0; ch < INPUT_CHANNEL_COUNT; ch++)
      {
        f = t.GetField("inChannel" + ch.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.inShow[ch] = (IOChannel)f.GetValue(this);
        this.inShow[ch].TabIndex = ch;
        this.inShow[ch].TabStop = false;
        this.inShow[ch].TranslateText = false;
        if (DBManager.getDBManager().syncroConfig.IsTwin400Only() ||
          ((ch != (int)IOInputChannels.INStandAloneConsole0 - 1) && (ch != (int)IOInputChannels.INStandAloneConsole1 - 1)))
          this.inShow[ch].Config(ch, this.bitDescriptions[ch], true, null);
        else
        {
          this.inShow[ch].Config("Not used", null, true, null);
          this.inShow[ch].Enabled = false;
        }
      }
      this.logAnalyzer = new Twin400LogAnalyzer(LogType.LogIO, this.lab_path, this.lab_Time, this.lbl_Title_YN, this.tim_Log, this);
    }


    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_testInput));
      this.inChannel0 = new SyncroC.Controls.IOChannel();
      this.inChannel1 = new SyncroC.Controls.IOChannel();
      this.inChannel2 = new SyncroC.Controls.IOChannel();
      this.inChannel3 = new SyncroC.Controls.IOChannel();
      this.inChannel4 = new SyncroC.Controls.IOChannel();
      this.inChannel5 = new SyncroC.Controls.IOChannel();
      this.inChannel6 = new SyncroC.Controls.IOChannel();
      this.inChannel7 = new SyncroC.Controls.IOChannel();
      this.inChannel8 = new SyncroC.Controls.IOChannel();
      this.inChannel9 = new SyncroC.Controls.IOChannel();
      this.tim_Log = new System.Windows.Forms.Timer(this.components);
      this.lab_path = new System.Windows.Forms.Label();
      this.lab_Time = new System.Windows.Forms.Label();
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
      this.tab_TabC.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      // 
      // tab_TabB
      // 
      this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabB.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
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
      // inChannel0
      // 
      this.inChannel0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.inChannel0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.inChannel0.Debug = false;
      this.inChannel0.Location = new System.Drawing.Point(72, 192);
      this.inChannel0.Name = "inChannel0";
      this.inChannel0.Size = new System.Drawing.Size(110, 271);
      this.inChannel0.TabIndex = 110;
      this.inChannel0.TranslateText = false;
      // 
      // inChannel1
      // 
      this.inChannel1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.inChannel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.inChannel1.Debug = false;
      this.inChannel1.Location = new System.Drawing.Point(177, 192);
      this.inChannel1.Name = "inChannel1";
      this.inChannel1.Size = new System.Drawing.Size(110, 271);
      this.inChannel1.TabIndex = 111;
      this.inChannel1.TranslateText = false;
      // 
      // inChannel2
      // 
      this.inChannel2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.inChannel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.inChannel2.Debug = false;
      this.inChannel2.Location = new System.Drawing.Point(285, 192);
      this.inChannel2.Name = "inChannel2";
      this.inChannel2.Size = new System.Drawing.Size(109, 271);
      this.inChannel2.TabIndex = 112;
      this.inChannel2.TranslateText = false;
      // 
      // inChannel3
      // 
      this.inChannel3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.inChannel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.inChannel3.Debug = false;
      this.inChannel3.Location = new System.Drawing.Point(392, 192);
      this.inChannel3.Name = "inChannel3";
      this.inChannel3.Size = new System.Drawing.Size(109, 271);
      this.inChannel3.TabIndex = 113;
      this.inChannel3.TranslateText = false;
      // 
      // inChannel4
      // 
      this.inChannel4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.inChannel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.inChannel4.Debug = false;
      this.inChannel4.Location = new System.Drawing.Point(499, 192);
      this.inChannel4.Name = "inChannel4";
      this.inChannel4.Size = new System.Drawing.Size(110, 271);
      this.inChannel4.TabIndex = 114;
      this.inChannel4.TranslateText = false;
      // 
      // inChannel5
      // 
      this.inChannel5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.inChannel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.inChannel5.Debug = false;
      this.inChannel5.Location = new System.Drawing.Point(607, 192);
      this.inChannel5.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.inChannel5.Name = "inChannel5";
      this.inChannel5.Size = new System.Drawing.Size(110, 271);
      this.inChannel5.TabIndex = 115;
      this.inChannel5.TranslateText = false;
      // 
      // inChannel6
      // 
      this.inChannel6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.inChannel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.inChannel6.Debug = false;
      this.inChannel6.Location = new System.Drawing.Point(716, 192);
      this.inChannel6.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.inChannel6.Name = "inChannel6";
      this.inChannel6.Size = new System.Drawing.Size(110, 271);
      this.inChannel6.TabIndex = 116;
      this.inChannel6.TranslateText = false;
      // 
      // inChannel7
      // 
      this.inChannel7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.inChannel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.inChannel7.Debug = false;
      this.inChannel7.Location = new System.Drawing.Point(823, 192);
      this.inChannel7.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.inChannel7.Name = "inChannel7";
      this.inChannel7.Size = new System.Drawing.Size(110, 271);
      this.inChannel7.TabIndex = 117;
      this.inChannel7.TranslateText = false;
      // 
      // inChannel8
      // 
      this.inChannel8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.inChannel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.inChannel8.Debug = false;
      this.inChannel8.Location = new System.Drawing.Point(935, 192);
      this.inChannel8.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.inChannel8.Name = "inChannel8";
      this.inChannel8.Size = new System.Drawing.Size(110, 271);
      this.inChannel8.TabIndex = 118;
      this.inChannel8.TranslateText = false;
      // 
      // inChannel9
      // 
      this.inChannel9.BackColor = System.Drawing.Color.WhiteSmoke;
      this.inChannel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.inChannel9.Debug = false;
      this.inChannel9.Location = new System.Drawing.Point(1047, 192);
      this.inChannel9.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.inChannel9.Name = "inChannel9";
      this.inChannel9.Size = new System.Drawing.Size(110, 271);
      this.inChannel9.TabIndex = 119;
      this.inChannel9.TranslateText = false;
      // 
      // tim_Log
      // 
      this.tim_Log.Interval = 1000;
      this.tim_Log.Tick += new System.EventHandler(this.tim_Log_Tick);
      // 
      // lab_path
      // 
      this.lab_path.Location = new System.Drawing.Point(320, 57);
      this.lab_path.Name = "lab_path";
      this.lab_path.Size = new System.Drawing.Size(613, 22);
      this.lab_path.TabIndex = 135;
      this.lab_path.Visible = false;
      // 
      // lab_Time
      // 
      this.lab_Time.Location = new System.Drawing.Point(744, 57);
      this.lab_Time.Name = "lab_Time";
      this.lab_Time.Size = new System.Drawing.Size(79, 24);
      this.lab_Time.TabIndex = 136;
      this.lab_Time.Visible = false;
      // 
      // Frm_twin400_testInput
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lab_Time);
      this.Controls.Add(this.lab_path);
      this.Controls.Add(this.inChannel9);
      this.Controls.Add(this.inChannel8);
      this.Controls.Add(this.inChannel7);
      this.Controls.Add(this.inChannel6);
      this.Controls.Add(this.inChannel5);
      this.Controls.Add(this.inChannel4);
      this.Controls.Add(this.inChannel3);
      this.Controls.Add(this.inChannel2);
      this.Controls.Add(this.inChannel1);
      this.Controls.Add(this.inChannel0);
      this.Name = "Frm_twin400_testInput";
      this.Text = "Frm_twin400_testInput";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.inChannel0, 0);
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
      this.Controls.SetChildIndex(this.inChannel1, 0);
      this.Controls.SetChildIndex(this.inChannel2, 0);
      this.Controls.SetChildIndex(this.inChannel3, 0);
      this.Controls.SetChildIndex(this.inChannel4, 0);
      this.Controls.SetChildIndex(this.inChannel5, 0);
      this.Controls.SetChildIndex(this.inChannel6, 0);
      this.Controls.SetChildIndex(this.inChannel7, 0);
      this.Controls.SetChildIndex(this.inChannel8, 0);
      this.Controls.SetChildIndex(this.inChannel9, 0);
      this.Controls.SetChildIndex(this.lab_path, 0);
      this.Controls.SetChildIndex(this.lab_Time, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    protected override void F11()
    { 
    }

    private void _RefreshChannels(string s)
    {
      if (this.logAnalyzer.logAnalyzing)
        return;
      string[] ioData=new string[INPUT_CHANNEL_COUNT];
      ioData = s.Split(Command.PARAMETER_SEPARATOR);
      for (int ch = 0; ch < INPUT_CHANNEL_COUNT; ch++)
      {
        this.inputChannel[ch] = intParse(ioData[ch]);
        this.inShow[ch].SetValue(this.inputChannel[ch]);
      }
    }

    private void ShowInputState(string s)
    {
      const int INPUT_STATUS_START = 3;
      if (s.Length < Twin400LogAnalyzer.START_INFO_LOG_INDEX + INPUT_STATUS_START + INPUT_CHANNEL_COUNT * 2)
        return;
      for (int ch = 0; ch < INPUT_CHANNEL_COUNT; ch++)
      {
        this.inputChannel[ch] = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + INPUT_STATUS_START + 4 * ch, 4), true);
        this.inShow[ch].SetValue(this.inputChannel[ch]);
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      int inChWidth = (this.rightHigh.X - this.leftHigh.X) / (INPUT_CHANNEL_COUNT/2);
      int chHeight = (this.rightLow.Y - this.rightHigh.Y) / 2;
      int tmpLeft = this.leftHigh.X;
      int tmpTop = this.leftHigh.Y;
      for (int ch = 0; ch < INPUT_CHANNEL_COUNT; ch++)
      {
        this.inShow[ch].Size = new Size(inChWidth, chHeight);
        this.inShow[ch].Location = new Point(tmpLeft, tmpTop);
        tmpLeft += inChWidth;
        if (ch == INPUT_CHANNEL_COUNT / 2 - 1)
        {
          tmpLeft = this.leftHigh.X;
          tmpTop += chHeight;
        }
      }
    }

    protected override void ControlD()
    {
      for (int i = 0; i < this.inShow.Length; i++)
        this.inShow[i].Debug = !this.inShow[i].Debug;
      if (!this.debugState)
      {
        this.ctrlF1Bck = this.lbl_CtrlF1_YY.Text;
        this.ctrlF2Bck = this.lbl_CtrlF2_YY.Text;
        this.ctrlF3Bck = this.lbl_CtrlF3_YY.Text;
        this.ctrlF4Bck = this.lbl_CtrlF4_YY.Text;
        this.ctrlF5Bck = this.lbl_CtrlF5_YY.Text;
        this.lbl_CtrlF1_YY.Text = "Log On/Off";
        this.lbl_CtrlF2_YY.Text = "Go To";
        this.lbl_CtrlF3_YY.Text = "Fwd/stop";
        this.lbl_CtrlF4_YY.Text = "Rev/stop";
        this.lbl_CtrlF5_YY.Text = "Log speed";
      }
      else
      {
        this.lbl_CtrlF1_YY.Text = this.ctrlF1Bck;
        this.lbl_CtrlF2_YY.Text = this.ctrlF2Bck;
        this.lbl_CtrlF3_YY.Text = this.ctrlF3Bck;
        this.lbl_CtrlF4_YY.Text = this.ctrlF4Bck;
        this.lbl_CtrlF5_YY.Text = this.ctrlF5Bck;
        if (this.logAnalyzer.logAnalyzing)
          this.logAnalyzer.CloseLogReader();
      }
      this.debugState = !this.debugState;
    }

    protected override void CtrlF1()
    {
      if (!this.debugState) return;
      if (this.logAnalyzer.logAnalyzing)
        this.logAnalyzer.CloseLogReader();
      else
        this.logAnalyzer.OpenLogReader(new Twin400LogAnalyzer.ShowState(this.ShowInputState));
    }

    protected override void DownArrow()
    {
      if (!this.debugState) return;
      this.logAnalyzer.AheadLog();
    }

    protected override void UpArrow()
    {
      if (!this.debugState) return;
      this.logAnalyzer.BackLog();
    }

    protected override void CtrlF3()
    {
      if (!this.debugState) return;
      this.logAnalyzer.FwdStop();
    }

    protected override void CtrlF2()
    {
      if (!this.debugState) return;
      this.logAnalyzer.ChangeDateTime();
    }

    protected override void CtrlF4()
    {
      if (!this.debugState) return;
      this.logAnalyzer.RevStop();
    }

    protected override void CtrlF5()
    {
      if (!this.debugState) return;
      this.logAnalyzer.ChangeLogSpeed();
    }

    private void tim_Log_Tick(object sender, EventArgs e)
    {
      this.logAnalyzer.tim_Log_Tick();
    }

    protected override bool IncludeComponentsIntoLanguageDB()
    {
      return false;
    }
  }
}