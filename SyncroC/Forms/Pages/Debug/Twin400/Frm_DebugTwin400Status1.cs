using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

using SyncroC.Controls;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Debug.Twin400
{
	public class Frm_DebugTwin400Status1 : SyncroC.Forms.Pages.Debug.Twin400.Frm_DebugTwin400Subgroup
	{
    const int STATUS_CHANNEL_COUNT = 12;
    private int[] statusChannel;
    private IOChannel[] stShow;
    private string[] statusName=null;
    private string[] bitDescription1 = null, bitDescription2 = null, bitDescription3 = null, bitDescription4 = null,
      bitDescription5 = null, bitDescription6 = null, bitDescription7 = null, bitDescription8 = null,
      bitDescription9 = null, bitDescription10 = null, bitDescription11 = null, bitDescription12 = null;
    private string[][] bitDescriptions=null;
    private SyncroC.Controls.IOChannel stChannel9;
    private SyncroC.Controls.IOChannel stChannel8;
    private SyncroC.Controls.IOChannel stChannel7;
    private SyncroC.Controls.IOChannel stChannel6;
    private SyncroC.Controls.IOChannel stChannel5;
    private SyncroC.Controls.IOChannel stChannel4;
    private SyncroC.Controls.IOChannel stChannel3;
    private SyncroC.Controls.IOChannel stChannel2;
    private SyncroC.Controls.IOChannel stChannel1;
    private SyncroC.Controls.IOChannel stChannel0;
    private SyncroC.Controls.IOChannel stChannel11;
    private SyncroC.Controls.IOChannel stChannel10;
    private System.ComponentModel.IContainer components = null;

		public Frm_DebugTwin400Status1()
		{
			InitializeComponent();

      #region Bit Descriptions
      this.statusName = new string[STATUS_CHANNEL_COUNT] 
        {"Shaft status [8]", "Machine status [9]", "Setup status [10]", "Maintenance status [11]", 
        "Reset status [12]", "Sensor status [13]", "AOC status [14]", "Disable status [15]", 
        "Command status [16]", "Functionality status [17]", "Slit shaft 1 status [26]", "Slit shaft 2 status [27]"};
      this.bitDescription1 = new string[IOChannel.BITS_IN_WORD]
        {"Slit 1 plunged", "Slit 2 plunged", "Score 1 plunged", "Score 2 plunged",
        "Score Aux plunged", "Slit 1 positioning", "Slit 2 positioning", "Score 1 positioning",
        "Score 2 positioning", "Score Aux positioning", "Slit 1 positioned", "Slit 2 positioned",
        "Score 1 positioned", "Score 2 positioned", "Score Aux positioned", "Shafts inflated"};
      this.bitDescription2 = new string[IOChannel.BITS_IN_WORD]
        {"Configured", "Emergency", "Restored", "Reset running",
        "Reset", "Started", "Scanning slits", "Positioning",
        "Ready to run", "Running", "Grinding", "Positioning error",
        "Normal stop", "Board after Web", "Tail out", "Board present"};
      this.bitDescription3 = new string[IOChannel.BITS_IN_WORD]
        {"Current transferred", "Current positioning", "Current positioned", "Current plunged",
        "Current on Slit 1", "Current on Slit 2", "Current on Score 1", "Current on Score 2",
        "Current on Score Aux", "", "Next transferred", "Next positioning part 1",
        "Next positioning part 2", "Next positioned part 1", "Next fully positioned", "Third tranferred"};
      this.bitDescription4 = new string[IOChannel.BITS_IN_WORD]
        {"Maintenance", "Simulation", "AOC simulation", "No-Gap AOC simulation",
         "Test IO", "PositionedForGrinding", "LineSimulation", "",
         "Calibration", "", "", "",
         "", "", "", "Run clock"};
      this.bitDescription5 = new string[IOChannel.BITS_IN_WORD]
        {"Slit robot", "Upper left score robot", "Upper right score robot", "Lower left score robot",
         "Lower right score robot", "Plunge 1", "Plunge 2", "Plunge Aux", 
          "Trim chutes", "", "", "",
          "", "", "", "Machine enabled to reset"};
      this.bitDescription6 = new string[IOChannel.BITS_IN_WORD]
        {"Edge guide max right", "Edge guide max left", "Edge guide external right", "Edge guide internal right",
         "Edge guide external left", "Edge guide internal left", "", "",
         "", "", "", "",
         "", "", "", ""};
      this.bitDescription7 = new string[IOChannel.BITS_IN_WORD]
        {"Next tandem", "Ready to AOC", "Preparing AOC", "Next No-Gap",
         "Tandem AOC running", "Reset AOC active", "AOC running", "Slit 1 completed",
         "Slit 2 completed", "Score 1 completed", "Score 2 completed", "Score Aux completed",
         "All shafts completed", "Trim chuted completed", "Web director completed", "AOC completed"};
      this.bitDescription8 = new string[IOChannel.BITS_IN_WORD]
        {"Jam up photoeyes", "", "", "Internal flap",
         "Encoder Reference", "Slit 1 photoeye man", "Slit 2 photoeye man", "Force Slow Positioning",
         "", "", "", "",
         "", "", "Slit 1 photoeye auto", "Slit 2 photoeye auto"};
      this.bitDescription9 = new string[IOChannel.BITS_IN_WORD]
        {"Open trim chutes", "Close trim chutes", "Move machine to right", "Move machine to left",
         "Increase score 1 gap", "Decrease score 1 gap", "Increase score 2 gap", "Decrease score 2 gap",
         "Increase score aux gap", "Decrease score aux gap", "", "",
         "", "", "", ""};
      this.bitDescription10 = new string[IOChannel.BITS_IN_WORD]
    		//AGrassi_FI06/12/2007_1
        {"Automatic edge guide", "Internal flap plunged", "Web director flap plunged", "Slit Grind",
         "", "Re-enable grind", "", "",
         "", "", "Teflon cycle", "Teflon cycle with gap",
         "Load teflon 1", "Load teflon 2", "Left trim slit removed", "Right trim slit removed"};
      this.bitDescription11 = new string[IOChannel.BITS_IN_WORD]
        {"Slit 1 plunged", "Slit 2 plunged", "Slit 3 plunged", "Slit 4 plunged",
         "Slit 5 plunged", "Slit 6 plunged", "Slit 7 plunged", "Slit 8 plunged",
         "Slit 9 plunged", "", "", "",
         "", "", "", ""};
      this.bitDescription12 = this.bitDescription11;
      this.bitDescriptions = new string[STATUS_CHANNEL_COUNT][]
        {bitDescription1, bitDescription2, bitDescription3, bitDescription4,
         bitDescription5, bitDescription6, bitDescription7, bitDescription8,
         bitDescription9, bitDescription10, bitDescription11, bitDescription12};
      #endregion

      this.statusChannel = new int[STATUS_CHANNEL_COUNT];
      this.stShow = new IOChannel[STATUS_CHANNEL_COUNT];
      Type t = this.GetType();
      FieldInfo f = null;
      for (int ch = 0; ch < STATUS_CHANNEL_COUNT; ch++)
      {
        f = t.GetField("stChannel" + ch.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        this.stShow[ch] = (IOChannel)f.GetValue(this);
        this.stShow[ch].TabIndex = ch;
        this.stShow[ch].TabStop = false;
        this.stShow[ch].TranslateText = false;
        this.stShow[ch].Config(this.statusName[ch], this.bitDescriptions[ch], true, null);
        this.stShow[ch].Debug = true;
      }
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
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DebugTwin400Status1));
      this.stChannel9 = new SyncroC.Controls.IOChannel();
      this.stChannel8 = new SyncroC.Controls.IOChannel();
      this.stChannel7 = new SyncroC.Controls.IOChannel();
      this.stChannel6 = new SyncroC.Controls.IOChannel();
      this.stChannel5 = new SyncroC.Controls.IOChannel();
      this.stChannel4 = new SyncroC.Controls.IOChannel();
      this.stChannel3 = new SyncroC.Controls.IOChannel();
      this.stChannel2 = new SyncroC.Controls.IOChannel();
      this.stChannel1 = new SyncroC.Controls.IOChannel();
      this.stChannel0 = new SyncroC.Controls.IOChannel();
      this.stChannel11 = new SyncroC.Controls.IOChannel();
      this.stChannel10 = new SyncroC.Controls.IOChannel();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_Title_YN.Text = "MACHINE STATUS";
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
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      this.lbl_CtrlF4_YY.Text = "Rev/Stop";
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF1_YY.Text = "Log On/Off";
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF2_YY.Text = "Go To";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF5_YY.Text = "Log speed";
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF3_YY.Text = "Fwd/Stop";
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
      // stChannel9
      // 
      this.stChannel9.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stChannel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.stChannel9.Debug = false;
      this.stChannel9.Location = new System.Drawing.Point(1110, 194);
      this.stChannel9.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.stChannel9.Name = "stChannel9";
      this.stChannel9.Size = new System.Drawing.Size(110, 271);
      this.stChannel9.TabIndex = 129;
      this.stChannel9.TranslateText = false;
      // 
      // stChannel8
      // 
      this.stChannel8.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stChannel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.stChannel8.Debug = false;
      this.stChannel8.Location = new System.Drawing.Point(998, 194);
      this.stChannel8.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.stChannel8.Name = "stChannel8";
      this.stChannel8.Size = new System.Drawing.Size(110, 271);
      this.stChannel8.TabIndex = 128;
      this.stChannel8.TranslateText = false;
      // 
      // stChannel7
      // 
      this.stChannel7.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stChannel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.stChannel7.Debug = false;
      this.stChannel7.Location = new System.Drawing.Point(886, 194);
      this.stChannel7.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.stChannel7.Name = "stChannel7";
      this.stChannel7.Size = new System.Drawing.Size(110, 271);
      this.stChannel7.TabIndex = 127;
      this.stChannel7.TranslateText = false;
      // 
      // stChannel6
      // 
      this.stChannel6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stChannel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.stChannel6.Debug = false;
      this.stChannel6.Location = new System.Drawing.Point(779, 194);
      this.stChannel6.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.stChannel6.Name = "stChannel6";
      this.stChannel6.Size = new System.Drawing.Size(110, 271);
      this.stChannel6.TabIndex = 126;
      this.stChannel6.TranslateText = false;
      // 
      // stChannel5
      // 
      this.stChannel5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stChannel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.stChannel5.Debug = false;
      this.stChannel5.Location = new System.Drawing.Point(670, 194);
      this.stChannel5.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.stChannel5.Name = "stChannel5";
      this.stChannel5.Size = new System.Drawing.Size(110, 271);
      this.stChannel5.TabIndex = 125;
      this.stChannel5.TranslateText = false;
      // 
      // stChannel4
      // 
      this.stChannel4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stChannel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.stChannel4.Debug = false;
      this.stChannel4.Location = new System.Drawing.Point(562, 194);
      this.stChannel4.Name = "stChannel4";
      this.stChannel4.Size = new System.Drawing.Size(110, 271);
      this.stChannel4.TabIndex = 124;
      this.stChannel4.TranslateText = false;
      // 
      // stChannel3
      // 
      this.stChannel3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stChannel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.stChannel3.Debug = false;
      this.stChannel3.Location = new System.Drawing.Point(455, 194);
      this.stChannel3.Name = "stChannel3";
      this.stChannel3.Size = new System.Drawing.Size(109, 271);
      this.stChannel3.TabIndex = 123;
      this.stChannel3.TranslateText = false;
      // 
      // stChannel2
      // 
      this.stChannel2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stChannel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.stChannel2.Debug = false;
      this.stChannel2.Location = new System.Drawing.Point(348, 194);
      this.stChannel2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.stChannel2.Name = "stChannel2";
      this.stChannel2.Size = new System.Drawing.Size(109, 271);
      this.stChannel2.TabIndex = 122;
      this.stChannel2.TranslateText = false;
      // 
      // stChannel1
      // 
      this.stChannel1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stChannel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.stChannel1.Debug = false;
      this.stChannel1.Location = new System.Drawing.Point(240, 194);
      this.stChannel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.stChannel1.Name = "stChannel1";
      this.stChannel1.Size = new System.Drawing.Size(110, 271);
      this.stChannel1.TabIndex = 121;
      this.stChannel1.TranslateText = false;
      // 
      // stChannel0
      // 
      this.stChannel0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stChannel0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.stChannel0.Debug = false;
      this.stChannel0.Location = new System.Drawing.Point(135, 194);
      this.stChannel0.Name = "stChannel0";
      this.stChannel0.Size = new System.Drawing.Size(110, 271);
      this.stChannel0.TabIndex = 120;
      this.stChannel0.TranslateText = false;
      // 
      // stChannel11
      // 
      this.stChannel11.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stChannel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.stChannel11.Debug = false;
      this.stChannel11.Location = new System.Drawing.Point(244, 444);
      this.stChannel11.Margin = new System.Windows.Forms.Padding(1, 1, 3, 3);
      this.stChannel11.Name = "stChannel11";
      this.stChannel11.Size = new System.Drawing.Size(110, 271);
      this.stChannel11.TabIndex = 131;
      this.stChannel11.TranslateText = false;
      // 
      // stChannel10
      // 
      this.stChannel10.BackColor = System.Drawing.Color.WhiteSmoke;
      this.stChannel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.stChannel10.Debug = false;
      this.stChannel10.Location = new System.Drawing.Point(132, 444);
      this.stChannel10.Margin = new System.Windows.Forms.Padding(1, 1, 1, 3);
      this.stChannel10.Name = "stChannel10";
      this.stChannel10.Size = new System.Drawing.Size(110, 271);
      this.stChannel10.TabIndex = 130;
      this.stChannel10.TranslateText = false;
      // 
      // Frm_DebugTwin400Status1
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.stChannel11);
      this.Controls.Add(this.stChannel10);
      this.Controls.Add(this.stChannel9);
      this.Controls.Add(this.stChannel8);
      this.Controls.Add(this.stChannel7);
      this.Controls.Add(this.stChannel6);
      this.Controls.Add(this.stChannel5);
      this.Controls.Add(this.stChannel4);
      this.Controls.Add(this.stChannel3);
      this.Controls.Add(this.stChannel2);
      this.Controls.Add(this.stChannel1);
      this.Controls.Add(this.stChannel0);
      this.Name = "Frm_DebugTwin400Status1";
      this.Text = "Frm_DebugTwin400Status1";
      this.Controls.SetChildIndex(this.lab_path, 0);
      this.Controls.SetChildIndex(this.lab_Time, 0);
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
      this.Controls.SetChildIndex(this.stChannel0, 0);
      this.Controls.SetChildIndex(this.stChannel1, 0);
      this.Controls.SetChildIndex(this.stChannel2, 0);
      this.Controls.SetChildIndex(this.stChannel3, 0);
      this.Controls.SetChildIndex(this.stChannel4, 0);
      this.Controls.SetChildIndex(this.stChannel5, 0);
      this.Controls.SetChildIndex(this.stChannel6, 0);
      this.Controls.SetChildIndex(this.stChannel7, 0);
      this.Controls.SetChildIndex(this.stChannel8, 0);
      this.Controls.SetChildIndex(this.stChannel9, 0);
      this.Controls.SetChildIndex(this.stChannel10, 0);
      this.Controls.SetChildIndex(this.stChannel11, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    protected override void F10()
    {
    }

    private void _RefreshChannels(string s)
    {
      if (this.logAnalyzer.logAnalyzing)
        return;
      string[] stData = new string[STATUS_CHANNEL_COUNT];
      stData = s.Split(Command.PARAMETER_SEPARATOR);
      for (int ch = 0; ch < STATUS_CHANNEL_COUNT; ch++)
      {
        this.statusChannel[ch] = intParse(stData[ch]);
        this.stShow[ch].SetValue(this.statusChannel[ch]);
      }
    }

    private void ShowPLCState(string s)
    {
      const int MACHINE_STATUS_START = 8;
      const int SLIT_STATUS_START = 26;
      if (s.Length < Twin400LogAnalyzer.START_INFO_LOG_INDEX + (SLIT_STATUS_START + 2) * 4)
        return;
      for (int ch = 0; ch < STATUS_CHANNEL_COUNT-2; ch++)
      {
        this.statusChannel[ch] = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * (MACHINE_STATUS_START + ch), 4), true);
        this.stShow[ch].SetValue(this.statusChannel[ch]);
      }
      for (int i = 0; i < 2; i++)
      {
        this.statusChannel[STATUS_CHANNEL_COUNT - 2 - i] = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * (SLIT_STATUS_START + i), 4), true);
        this.stShow[STATUS_CHANNEL_COUNT - 2 - i].SetValue(this.statusChannel[STATUS_CHANNEL_COUNT - 2 - i]);
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      int inChWidth = (this.rightHigh.X - this.leftHigh.X) / (STATUS_CHANNEL_COUNT / 2);
      int chHeight = (this.rightLow.Y - this.rightHigh.Y) / 2;
      int tmpLeft = this.leftHigh.X;
      int tmpTop = this.leftHigh.Y;
      for (int ch = 0; ch < STATUS_CHANNEL_COUNT; ch++)
      {
        this.stShow[ch].Size = new Size(inChWidth, chHeight);
        this.stShow[ch].Location = new Point(tmpLeft, tmpTop);
        tmpLeft += inChWidth;
        if (ch == STATUS_CHANNEL_COUNT / 2 - 1)
        {
          tmpLeft = this.leftHigh.X;
          tmpTop += chHeight;
        }
      }
    }

    protected override void CtrlF2()
    {
      this.logAnalyzer.ChangeDateTime();
    }

    protected override void CtrlF1()
    {
      if (this.logAnalyzer.logAnalyzing)
        this.logAnalyzer.CloseLogReader();
      else
        this.logAnalyzer.OpenLogReader(new Twin400LogAnalyzer.ShowState(this.ShowPLCState));
    }

    protected override void DownArrow()
    {
      this.logAnalyzer.AheadLog();
    }

    protected override void UpArrow()
    {
      this.logAnalyzer.BackLog();
    }

    protected override void CtrlF3()
    {
      this.logAnalyzer.FwdStop();
    }

    protected override void CtrlF4()
    {
      this.logAnalyzer.RevStop();
    }

    protected override void CtrlF5()
    {
      this.logAnalyzer.ChangeLogSpeed();
    }
  }
}

