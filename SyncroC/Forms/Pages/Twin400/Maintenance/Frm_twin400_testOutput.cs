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
	public class Frm_twin400_testOutput : SyncroC.Forms.Pages.Twin400.Maintenance.Frm_twin400_maintenanceSubgroup
	{
    const int OUTPUT_CHANNEL_COUNT = 10;
    const int OUTPUT_CHANNEL_EFF = 8;
    private int outIndex;
    private bool testIOMode, testIOModeChanged;
    private IOChannelChangeSelection changeFunction;
    private int[] outputChannel;
    private IOChannel[] outShow;
    private IOChannel outChannel0;
    private IOChannel outChannel1;
    private IOChannel outChannel2;
    private IOChannel outChannel3;
    private IOChannel outChannel4;
    private IOChannel outChannel5;
    private IOChannel outChannel6;
    private IOChannel outChannel7;
    private IOChannel outChannel8;
    private IOChannel outChannel9;
    private string[] bitDescription1 = null, bitDescription2 = null, bitDescription3 = null, bitDescription4 = null,
      bitDescription5 = null, bitDescription6 = null, bitDescription7 = null, bitDescription8 = null,
      bitDescription9 = null, bitDescription10 = null;
    private string[][] bitDescriptions = null;
    protected Label lab_Time;
    private Label lab_path;
    private Timer tim_Log;
    private Twin400LogAnalyzer logAnalyzer;
    private bool debugState = false;
    private string ctrlF1Bck, ctrlF2Bck, ctrlF3Bck, ctrlF4Bck, ctrlF5Bck;
    private System.ComponentModel.IContainer components = null;

    public Frm_twin400_testOutput()
		{
			InitializeComponent();

      #region Bit Descriptions

      //ABELLO_FI22/08/2008_1 Rivisti gli ingressi uscita per Versione di PLC 6
			//RFAZZI PROVA
			//if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion == SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver6)
			if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver6)
			{

        //IO_Logic_Cabinet_OUT[0]
        this.bitDescription1 = new string[IOChannel.BITS_IN_WORD] 
        {
          "Emergency from PLC (NC)", //0
          "Enable Air conditioning", //1
          "Enable order change (for Lenze)", //2
          "Maintenance Order Change (for lenze)", //3
          "Alarm signal - Red color", //4
          "Warnig signal - Yellow color", //5
          "Machine OK - Green color", //6
          "Buzzer alarm", //7
          "CT Blades on work", //8
          "CT blades not in work", //9
          "Buzzer for blades regulation", //10
          "Dust removal on axis 1", //11
          "Dust removal on axis 2", //12
          "Dust removal on open air", //13
          "CMD Machine Start/Insertion", //14
          "Enable bypass x tuning blade", //15
        };

        //IO_Logic_Cabinet_OUT[1]
        this.bitDescription2 = new string[IOChannel.BITS_IN_WORD] 
        {
          "Machine in emergecy (to aoc)", //0
          "N-Stop (to aoc)", //1
          "Increase line speed (to aoc)", //2
          "Decrease line speed (to aoc)", //3
          "Machine ready to run (to aoc)", //4
          "Machine ready to order change (to aoc)", //5
          "Order change in progress (to aoc)", //6
          "Machine ready to Gap AOC (to aoc)", //7
          "Machine ready to NO-Gap AOC (to aoc)", //8
          "Next order change is tandem (to aoc)", //9
          "Rotary shear has cut (to E+L)", //10
          "Lateral adjust machine SX (to E+L)", //11
          "Lateral adjust machine DX (to E+L)", //12
          "SPARE", //13
          "Order change in progress (to E+L)", //14
          "Line run (to ext.)", //15
        };

        //SPARE
        this.bitDescription3 = new string[IOChannel.BITS_IN_WORD] 
        {
          "SPARE", //0
          "SPARE", //1
          "SPARE", //2
          "SPARE", //3
          "SPARE", //4
          "SPARE", //5
          "SPARE", //6
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

        //IO_Outlet_Console_OUT[0]
        this.bitDescription4 = new string[IOChannel.BITS_IN_WORD] 
        {
          "Machine started - Green light", //0
          "SPARE", //1
          "Edge Guide in automatic mode - Blu light", //2
          "All axis open - White light", //3
          "Emergency machine - Red light", //4
          "Contact Plate Open - Blu light", //5
          "SPARE", //6
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

        //IO_Power_Cabinet_OUT[0]
        this.bitDescription5 = new string[IOChannel.BITS_IN_WORD] 
        {
          "Disable cabinet air conditioner", //0
          "Machine movement to right", //1
          "Machine movement to left", //2
          "SPARE", //3
          "SPARE", //4
          "Run Photoeyes Edge Guide", //5
          "Open / Closed select (on=Closed)", //6
          "Trip reset Edge Guide", //7
          "Run/stop Drive Rolls AX1", //8
          "JOG1 - AX1", //9
          "Trip Reset AX1", //10
          "Run/stop Drive Rolls AX2", //11
          "JOG1 - AX2", //12
          "Trip Reset AX2)", //13
          "Run/stop Drive Rolls Aaux", //14
          "JOG1 - Aaux", //15
        };

         //IO_Power_Cabinet_OUT[1]
        this.bitDescription6 = new string[IOChannel.BITS_IN_WORD] 
        {
          "Trip Reset Aaux", //0
          "Start/Stop Inverter PullRoll on WebDir", //1
          "Trip reset on PullRoll WebDir", //2
          "Tail Out in Progress for WebDir Drive", //3
          "Red light alarm", //4
          "Yellow light alarm", //5
          "Green light alarm", //6
          "Buzzer alarm", //7
          "Web Present at MW for Tear Tape", //8
          "NoGap AOC for Tear Tape", //9
          "Board on WebDir for Tear Tape", //10
          "Slit tool overspeed axis 1 drive", //11
          "Slit tool overspeed axis 2 drive", //12
          "Slit tool overspeed aux axis drive", //13
          "SPARE", //14
          "Enable robot's inverter", //15
        };

        //IO_Sideframe_OUT[0]
        this.bitDescription7 = new string[IOChannel.BITS_IN_WORD] 
        {
          "Deflate blades AX1 Command", //0
          "Deflate blades AX2 Command", //1
          "Deflate Scorers AX1 Command", //2
          "Deflate Scorers AX2 Command", //3
          "Deflate Scorers Aaux Command", //4
          "Command blown edge guide photocell", //5
          "SPARE", //6
          "CMD remove anvil support safety", //7
          "CMD raise Anvil support", //8
          "CMD lower Anvil support", //9
          "Cmd Open mobile Bar", //10
          "CMD internal flap", //11
          "Air Pressure Flap", //12
          //agrassi_FI06-05-2011_1
          (DBManager.getDBManager().syncroConfig.GeneralSetting.Job == "3873") ?  "Line Run for Dust Removal" : "SPARE", //13
          "Charge teflon AX1", //14
          "Charge teflon AX2", //15
        };

        //IO_StandAlone_console_OUT[0]
        this.bitDescription8 = new string[IOChannel.BITS_IN_WORD] 
        {
          "SPARE", //0
          "SPARE", //1
          "Blu light Edge Guide in automatic mode", //2
          "SPARE", //3
          "Red light emergency machine", //4
          "SPARE", //5
          "Disable elettrical cabinet air conditioner", //6
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

        //SPARE
        this.bitDescription9 = new string[IOChannel.BITS_IN_WORD] 
        {
          "SPARE", //0
          "SPARE", //1
          "SPARE", //2
          "SPARE", //3
          "SPARE", //4
          "SPARE", //5
          "SPARE", //6
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

        //SPARE
        this.bitDescription10 = new string[IOChannel.BITS_IN_WORD] 
        {
          "SPARE", //0
          "SPARE", //1
          "SPARE", //2
          "SPARE", //3
          "SPARE", //4
          "SPARE", //5
          "SPARE", //6
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
      }
      else
      {
				//RFAZZI PROVA
				//if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion == SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver1)
				if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion == SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver1)
					this.bitDescription1 = new string[IOChannel.BITS_IN_WORD]
          {"Emergency from PLC", "Enable cooling system", "Enable OC to Lenze", "Start OC for Lenze",
           "Red light alarm", "White light alarm", "Green light alarm", "Command buzzer alarm",
           "", "", "", "",
           "", "", "", "Enable key selector for tuning"};
				//RFAZZI PROVA
				//else if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver2)
				else if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver2)
          this.bitDescription1 = new string[IOChannel.BITS_IN_WORD]
          {"Emergency from PLC", "Enable cooling system", "Enable OC to Lenze", "Start OC for Lenze",
           "Red light alarm", "White light alarm", "Green light alarm", "Command buzzer alarm",
           "EV Lower CTS blades", "EV Raise CTS blades", "", "",
           "", "Flap1 command", "Flap2 command", "Enable key selector for tuning"};
        this.bitDescription2 = new string[IOChannel.BITS_IN_WORD]
        {"Machine in emergency", "Normal Stop", "Increase line speed", "Decrease line speed",
        "Machine ready to run", "Machine ready for OC", "Order change in progress", "Machine ready to GAP OC",
        "Machine ready to No-GAP OC", "Next OC is tandem", "Rotary has cut", "Lateral adj. machine SX (EL)",
        "Lateral adj. machine DX (EL)", "Automatic Edge Guide (EL)", "Order change in progress (EL)", "Line Run (EL)"};
				//RFAZZI PROVA
				//if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion == SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver1)
				if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion == SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver1)
          this.bitDescription3 = new string[IOChannel.BITS_IN_WORD]
          {"Blade 1 or 9 shaft 1", "Blade 2 shaft 1", "Blade 3 shaft 1", "Blade 4 shaft 1",
          "Blade 5 shaft 1", "Blade 6 shaft 1", "Blade 7 shaft 1", "Blade 8 shaft 1",
          "Blade 1 or 9 shaft 2", "Blade 2 shaft 2", "Blade 3 shaft 2", "Blade 4 shaft 2",
          "Blade 5 shaft 2", "Blade 6 shaft 2", "Blade 7 shaft 2", "Blade 8 shaft 2"};
				//RFAZZI PROVA
				//else if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver2)
				else if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver2)
          this.bitDescription3 = new string[IOChannel.BITS_IN_WORD]
          {"Blade 1 shaft 1", "Blade 2 shaft 1", "Blade 3 shaft 1", "Blade 4 shaft 1",
          "Blade 5 shaft 1", "Blade 6 shaft 1", "Blade 7 shaft 1", "Blade 8 shaft 1",
          "Blade 1 shaft 2", "Blade 2 shaft 2", "Blade 3 shaft 2", "Blade 4 shaft 2",
          "Blade 5 shaft 2", "Blade 6 shaft 2", "Blade 7 shaft 2", "Blade 8 shaft 2"};
        this.bitDescription4 = new string[IOChannel.BITS_IN_WORD]
        {"Green Light Machine Restored", "White Light Machine started", "Blu light edge guide in automatic ", "White light raise all shafts",
         "Red light emergency", "Blu light contact cut floor", "", "",
         "", "", "", "",
         "", "", "", ""};
        this.bitDescription5 = new string[IOChannel.BITS_IN_WORD]
        {"Enable air conditioner", "Move machine to left", "Move machine to right", "",
         "", "Run edge guide's photoeye", "Open/Close edge guide's photoeye", "Trip Reset edge guide", 
          "Run/Stop Drive shaft 1", "Jog Drive shaft 1", "Trip Reset shaft 1", "Run/Stop Drive shaft 2",
          "Jog Drive shaft 2", "Trip Reset shaft 2", "Run/Stop Drive shaft aux", "Jog Drive shaft aux"};
        this.bitDescription6 = new string[IOChannel.BITS_IN_WORD]
        {"Trip Reset shaft aux", "", "", "",
         "Red light alarm", "White light alarm", "Green light alarm", "Command buzzer alarm",
         "", "", "", "",
         "", "", "", "Enable robot's inverter"};
				//RFAZZI PROVA
				//if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion == SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver1)
				if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion == SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver1)
          this.bitDescription7 = new string[IOChannel.BITS_IN_WORD]
          {"Deflate slit shaft 1", "Deflate slit shaft 2", "Deflate score shaft 1", "Deflate score shaft 2",
           "Deflate scores shaft Aux", "Blow edge guide's photoeye", "Flap", "Release contact cut pivoto",
           "Open contact cut floor", "Close contact cut floor", "Close anvils shaft 1", "Close anvils shaft 2",
           "Lower continuous trim blades", "Raise continuous trim blades", "Load teflon shaft 1", "Load teflon shaft 1"};
				//RFAZZI PROVA
				//else if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver2)
				else if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver2)
          this.bitDescription7 = new string[IOChannel.BITS_IN_WORD]
          {"Deflate slit shaft 1", "Deflate slit shaft 2", "Deflate score shaft 1", "Deflate score shaft 2",
           "Deflate scores shaft Aux", "Breath edge guide's photoeye", "", "Release contact cut pivot",
           "EV Open contact cut floor", "EV Close contact cut floor", "EV floor Shaft2", "Breath on trim",
           "Lube blade 9 shaft 1", "Lube blade 9 shaft 2", "EV Load teflon shaft 1", "EV Load teflon shaft 2"};
        this.bitDescription8 = new string[IOChannel.BITS_IN_WORD]
        {"", "White Light Machine started", "Blu light edge guide in automatic", "",
        "Red light emergency", "", "Enable air conditioner", "",
        "", "", "", "",
        "", "", "", ""};
        this.bitDescription9 = new string[IOChannel.BITS_IN_WORD]
        {"", "", "", "",
         "", "", "", "",
         "", "", "", "",
         "", "", "", ""};
        this.bitDescription10 = new string[IOChannel.BITS_IN_WORD]
        {"", "", "", "",
         "", "", "", "",
         "", "", "", "",
         "", "", "", ""};
      }
      
      this.bitDescriptions = new string[OUTPUT_CHANNEL_COUNT][]
        {bitDescription1, bitDescription2, bitDescription3, bitDescription4,
         bitDescription5, bitDescription6, bitDescription7, bitDescription8,
         bitDescription9, bitDescription10};
      
      #endregion

      this.changeFunction = new IOChannelChangeSelection(this.MoveSelection);
      this.outputChannel = new int[OUTPUT_CHANNEL_COUNT];
      this.outShow = new IOChannel[OUTPUT_CHANNEL_COUNT];
      Type t = this.GetType();
      FieldInfo f = null;
      for (int ch = 0; ch < OUTPUT_CHANNEL_COUNT; ch++)
      {
        f = t.GetField("outChannel" + ch.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.outShow[ch] = (IOChannel)f.GetValue(this);
        this.outShow[ch].TabIndex = ch;
        this.outShow[ch].TabStop = true;
        this.outShow[ch].TranslateText = false;
        if (DBManager.getDBManager().syncroConfig.IsTwin400Only() || (ch != (int)IOOutputChannels.OUTStandAloneConsole - 1))
          this.outShow[ch].Config(ch, this.bitDescriptions[ch], false, this.changeFunction);
        else
        {
          this.outShow[ch].Enabled = false;
          this.outShow[ch].Config("Not used", null, false, this.changeFunction);
        }
      }
      this.testIOMode = false;
      this.testIOModeChanged = false;
      this.outIndex = 0;
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_testOutput));
      this.outChannel0 = new SyncroC.Controls.IOChannel();
      this.outChannel1 = new SyncroC.Controls.IOChannel();
      this.outChannel2 = new SyncroC.Controls.IOChannel();
      this.outChannel3 = new SyncroC.Controls.IOChannel();
      this.outChannel4 = new SyncroC.Controls.IOChannel();
      this.outChannel5 = new SyncroC.Controls.IOChannel();
      this.outChannel6 = new SyncroC.Controls.IOChannel();
      this.outChannel7 = new SyncroC.Controls.IOChannel();
      this.outChannel8 = new SyncroC.Controls.IOChannel();
      this.outChannel9 = new SyncroC.Controls.IOChannel();
      this.lab_Time = new System.Windows.Forms.Label();
      this.lab_path = new System.Windows.Forms.Label();
      this.tim_Log = new System.Windows.Forms.Timer(this.components);
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
      this.tab_TabC.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // tab_TabB
      // 
      this.tab_TabB.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
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
      this.lbl_CtrlF4_YY.Text = "Force value";
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
      this.lbl_CtrlF3_YY.Text = "TestIO mode";
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
      // outChannel0
      // 
      this.outChannel0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.outChannel0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.outChannel0.Debug = false;
      this.outChannel0.Location = new System.Drawing.Point(74, 460);
      this.outChannel0.Name = "outChannel0";
      this.outChannel0.Size = new System.Drawing.Size(110, 252);
      this.outChannel0.TabIndex = 120;
      this.outChannel0.TranslateText = false;
      // 
      // outChannel1
      // 
      this.outChannel1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.outChannel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.outChannel1.Debug = false;
      this.outChannel1.Location = new System.Drawing.Point(179, 460);
      this.outChannel1.Name = "outChannel1";
      this.outChannel1.Size = new System.Drawing.Size(110, 252);
      this.outChannel1.TabIndex = 121;
      this.outChannel1.TranslateText = false;
      // 
      // outChannel2
      // 
      this.outChannel2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.outChannel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.outChannel2.Debug = false;
      this.outChannel2.Location = new System.Drawing.Point(287, 460);
      this.outChannel2.Name = "outChannel2";
      this.outChannel2.Size = new System.Drawing.Size(110, 252);
      this.outChannel2.TabIndex = 122;
      this.outChannel2.TranslateText = false;
      // 
      // outChannel3
      // 
      this.outChannel3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.outChannel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.outChannel3.Debug = false;
      this.outChannel3.Location = new System.Drawing.Point(394, 460);
      this.outChannel3.Name = "outChannel3";
      this.outChannel3.Size = new System.Drawing.Size(110, 252);
      this.outChannel3.TabIndex = 123;
      this.outChannel3.TranslateText = false;
      // 
      // outChannel4
      // 
      this.outChannel4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.outChannel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.outChannel4.Debug = false;
      this.outChannel4.Location = new System.Drawing.Point(501, 460);
      this.outChannel4.Name = "outChannel4";
      this.outChannel4.Size = new System.Drawing.Size(110, 252);
      this.outChannel4.TabIndex = 124;
      this.outChannel4.TranslateText = false;
      // 
      // outChannel5
      // 
      this.outChannel5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.outChannel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.outChannel5.Debug = false;
      this.outChannel5.Location = new System.Drawing.Point(609, 460);
      this.outChannel5.Name = "outChannel5";
      this.outChannel5.Size = new System.Drawing.Size(110, 252);
      this.outChannel5.TabIndex = 125;
      this.outChannel5.TranslateText = false;
      // 
      // outChannel6
      // 
      this.outChannel6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.outChannel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.outChannel6.Debug = false;
      this.outChannel6.Location = new System.Drawing.Point(718, 460);
      this.outChannel6.Name = "outChannel6";
      this.outChannel6.Size = new System.Drawing.Size(110, 252);
      this.outChannel6.TabIndex = 126;
      this.outChannel6.TranslateText = false;
      // 
      // outChannel7
      // 
      this.outChannel7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.outChannel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.outChannel7.Debug = false;
      this.outChannel7.Location = new System.Drawing.Point(825, 460);
      this.outChannel7.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.outChannel7.Name = "outChannel7";
      this.outChannel7.Size = new System.Drawing.Size(110, 252);
      this.outChannel7.TabIndex = 127;
      this.outChannel7.TranslateText = false;
      // 
      // outChannel8
      // 
      this.outChannel8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.outChannel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.outChannel8.Debug = false;
      this.outChannel8.Location = new System.Drawing.Point(934, 460);
      this.outChannel8.Name = "outChannel8";
      this.outChannel8.Size = new System.Drawing.Size(110, 252);
      this.outChannel8.TabIndex = 128;
      this.outChannel8.TranslateText = false;
      // 
      // outChannel9
      // 
      this.outChannel9.BackColor = System.Drawing.Color.WhiteSmoke;
      this.outChannel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.outChannel9.Debug = false;
      this.outChannel9.Location = new System.Drawing.Point(1049, 460);
      this.outChannel9.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.outChannel9.Name = "outChannel9";
      this.outChannel9.Size = new System.Drawing.Size(110, 252);
      this.outChannel9.TabIndex = 129;
      this.outChannel9.TranslateText = false;
      // 
      // lab_Time
      // 
      this.lab_Time.Location = new System.Drawing.Point(744, 57);
      this.lab_Time.Name = "lab_Time";
      this.lab_Time.Size = new System.Drawing.Size(79, 24);
      this.lab_Time.TabIndex = 138;
      this.lab_Time.Visible = false;
      // 
      // lab_path
      // 
      this.lab_path.Location = new System.Drawing.Point(309, 57);
      this.lab_path.Name = "lab_path";
      this.lab_path.Size = new System.Drawing.Size(613, 22);
      this.lab_path.TabIndex = 137;
      this.lab_path.Visible = false;
      // 
      // tim_Log
      // 
      this.tim_Log.Interval = 1000;
      this.tim_Log.Tick += new System.EventHandler(this.tim_Log_Tick);
      // 
      // Frm_twin400_testOutput
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lab_Time);
      this.Controls.Add(this.lab_path);
      this.Controls.Add(this.outChannel9);
      this.Controls.Add(this.outChannel8);
      this.Controls.Add(this.outChannel7);
      this.Controls.Add(this.outChannel6);
      this.Controls.Add(this.outChannel5);
      this.Controls.Add(this.outChannel4);
      this.Controls.Add(this.outChannel3);
      this.Controls.Add(this.outChannel2);
      this.Controls.Add(this.outChannel1);
      this.Controls.Add(this.outChannel0);
      this.Name = "Frm_twin400_testOutput";
      this.Text = "Frm_twin400_testOutput";
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
      this.Controls.SetChildIndex(this.outChannel0, 0);
      this.Controls.SetChildIndex(this.outChannel1, 0);
      this.Controls.SetChildIndex(this.outChannel2, 0);
      this.Controls.SetChildIndex(this.outChannel3, 0);
      this.Controls.SetChildIndex(this.outChannel4, 0);
      this.Controls.SetChildIndex(this.outChannel5, 0);
      this.Controls.SetChildIndex(this.outChannel6, 0);
      this.Controls.SetChildIndex(this.outChannel7, 0);
      this.Controls.SetChildIndex(this.outChannel8, 0);
      this.Controls.SetChildIndex(this.outChannel9, 0);
      this.Controls.SetChildIndex(this.lab_path, 0);
      this.Controls.SetChildIndex(this.lab_Time, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    protected override void F12()
    { 
    }

    private void _RefreshChannels(string s)
    {
      if (this.logAnalyzer.logAnalyzing)
        return;
      string[] ioData = new string[OUTPUT_CHANNEL_COUNT + 1];
      ioData = s.Split(Command.PARAMETER_SEPARATOR);
      bool oldTestIOMode=this.testIOMode;
      this.testIOMode = bool.Parse(ioData[0]);
      this.testIOModeChanged = (this.testIOMode != oldTestIOMode);
      for (int ch = 0; ch < OUTPUT_CHANNEL_COUNT; ch++)
      {
        this.outputChannel[ch] = intParse(ioData[ch+1]);
        this.outShow[ch].SetValue(this.outputChannel[ch]);
      }
      this.Invalidate();
    }

    private void ShowOutputState(string s)
    {
      const int OUTPUT_STATUS_START = 43;
      if (s.Length < Twin400LogAnalyzer.START_INFO_LOG_INDEX + OUTPUT_STATUS_START + OUTPUT_CHANNEL_COUNT * 2)
        return;
      for (int ch = 0; ch < OUTPUT_CHANNEL_COUNT; ch++)
      {
        this.outputChannel[ch] = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + OUTPUT_STATUS_START + 4 * ch, 4), true);
        this.outShow[ch].SetValue(this.outputChannel[ch]);
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      int outChWidth = (this.rightHigh.X - this.leftHigh.X) / (OUTPUT_CHANNEL_COUNT/2);
      int chHeight = (this.rightLow.Y - this.rightHigh.Y) / 2;
      int tmpLeft = this.leftHigh.X;
      int tmpTop = this.leftHigh.Y;
      for (int ch = 0; ch < OUTPUT_CHANNEL_COUNT; ch++)
      {
        this.outShow[ch].Size = new Size(outChWidth, chHeight);
        this.outShow[ch].Location = new Point(tmpLeft, tmpTop);
        if (this.testIOModeChanged)
          this.outShow[ch].SetWriteMode(this.testIOMode && (this.outIndex == ch));
        tmpLeft += outChWidth;
        if (ch==OUTPUT_CHANNEL_COUNT / 2 - 1)
        {
          tmpLeft = this.leftHigh.X;
          tmpTop += chHeight;
        }
      }
      this.testIOModeChanged = false;
    }

    protected override void LeftArrow()
    {
      if (this.testIOMode)
      {
        if (this.outIndex > 0)
        {
          this.outIndex--;
          this.testIOModeChanged = true;
          this.Invalidate();
        }
      }
    }

    protected override void RightArrow()
    {
      if (this.testIOMode)
      {
        if (this.outIndex < OUTPUT_CHANNEL_COUNT - 1)
        {
          this.outIndex++;
          this.testIOModeChanged = true;
          this.Invalidate();
        }
      }
    }

    protected override void UpArrow()
    {
      if (this.testIOMode)
      {
        this.outShow[this.outIndex].DecBitIndex();
      }
      else if (this.debugState)
      {
        this.logAnalyzer.BackLog();
      }
    }

    protected override void DownArrow()
    {
      if (this.testIOMode)
      {
        this.outShow[this.outIndex].IncBitIndex();
      }
      else if (this.debugState)
      {
        this.logAnalyzer.AheadLog();
      }
    }

    protected override void Return()
    {
      if (this.testIOMode)
      {
        this.outShow[this.outIndex].ToggleBit();
      }
    }


    private void MoveSelection(int selIndex)
    {
      if (this.testIOMode)
      {
        if ((selIndex >= 0) && (selIndex < OUTPUT_CHANNEL_COUNT) && (selIndex!=this.outIndex))
        {
          this.outIndex = selIndex;
          this.testIOModeChanged = true;
          this.Invalidate();
        }
      }
    }

    protected override void CtrlF4()
    {
      if (!this.debugState)
      {
        if (Accessibility.getAccessibility().isLevelHigh())
        {
          if (this.testIOMode)
          {
            if (this.outIndex < OUTPUT_CHANNEL_EFF)
            {
              CommunicationManager cm = CommunicationManager.GetCommunicationManager();
              cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
                PageManager.getRequestCommand(typeof(Frm_twin400_testOutput)) + "." + Command.SET_OUT_CH +
                "(" + (this.outIndex + 1).ToString() + "," + this.outShow[this.outIndex].GetTmpValue() + ")");
              this.outShow[this.outIndex].ResetDisalignment();
            }
            else
              PageManager.getPageManager()._SetMessage("§SYCC_12");
          }
          else
            PageManager.getPageManager()._SetMessage("§SYCC_13");
        }
        else
        {
          PageManager.getPageManager()._SetMessage("§SYCC_11");
        }
      }
      else
      {
        this.logAnalyzer.RevStop();
      }
    }

    protected override void CtrlF3()
    {
      if (!this.debugState)
      {
        if (Accessibility.getAccessibility().isLevelHigh())
        {
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          cm.SendTcp(cm.SYNCROS_RTC_MANAGER, PageManager.getRequestCommand(typeof(Frm_twin400_testOutput)) + "." + Command.TEST_IO + "()");
          this.outIndex = 0;
        }
        else
        {
          PageManager.getPageManager()._SetMessage("§SYCC_11");
        }
      }
      else
      {
        this.logAnalyzer.FwdStop();
      }
    }


    protected override void Escape()
    {
      if (this.testIOMode)
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER, PageManager.getRequestCommand(typeof(Frm_twin400_testOutput)) + "." + Command.TEST_IO + "()");
      }
    }

    protected override void ControlD()
    {
      for (int i = 0; i < this.outShow.Length; i++)
        this.outShow[i].Debug = !this.outShow[i].Debug;
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
        this.lbl_CtrlF1_YY.Text = this.ctrlF2Bck;
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
        this.logAnalyzer.OpenLogReader(new Twin400LogAnalyzer.ShowState(this.ShowOutputState));
    }

    protected override void CtrlF2()
    {
      if (!this.debugState) return;
      this.logAnalyzer.ChangeDateTime();
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