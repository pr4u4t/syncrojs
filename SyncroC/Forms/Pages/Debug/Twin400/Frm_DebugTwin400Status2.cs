using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Controls.IncDecButton;

namespace SyncroC.Forms.Pages.Debug.Twin400
{
	public class Frm_DebugTwin400Status2 : SyncroC.Forms.Pages.Debug.Twin400.Frm_DebugTwin400Subgroup
	{
    private int taskStatus, slitPositionsStatus, alarmStatus;
    private SyncroC.Controls.IOChannel taskShow, slitPositionsShow, alarmShow;
    private string[] bitDescriptionsTask, bitDescriptionsSlitPositions, bitDescriptionsAlarm;
    private Label label1;
    private Label lab_NotifyArea;
    private Label label4;
    private Label lab_LastMessageNumber;
    private Label label5;
    private Label lab_LastMessageNumberCMD;
    private Label label7;
    private Label lab_TransmissionError;
    private Label label8;
    private Label lab_TransmissionErrorCode;
    private Label label10;
    private Label lab_ID1;
    private Label label9;
    private Label lab_ID2;
    private Label label11;
    private Label lab_ID3;
    private Label label12;
    private Label lab_ScoreGap1;
    private Label label13;
    private Label lab_ScoreGap2;
    private Label lab_ScoreGap3;
    private Label label2;
    private Label lab_ChutesPosition;
    private Label label3;
    private Label lab_2levWebDirLow;
    private Label label6;
    private Label lab_2levWebDirHigh;
    private Label label15;
    private Label lab_3levWebDirLow;
    private Label label16;
    private Label lab_3levWebDirHigh;
    private Button btn_SendSetup0;
    private Button btn_SendSetup1;
    private Button btn_SendSetup2;
    private Label label17;
    private Label lab_ScoreAngle3;
    private Label label19;
    private Label lab_ScoreAngle2;
    private Label label21;
    private Label lab_ScoreAngle1;
    private Label lbl_UM22;
    private Label label18;
    private Label label20;
    private Label lbl_UM12;
    private Label label14;
    private Label label22;
    private Label label23;
    private Label label24;
    private Label label25;
    private Label label26;
    private Label label27;
    private Label lab_ShaftGap3;
    private Label label29;
    private Label lab_ShaftGap2;
    private Label label31;
    private Label lab_ShaftGap1;
    private Label label28;
    private Label lab_Speed;
    private Label label55;
    private Label lab_GrindDuration;
    private Label label30;
    private Label lab_LubeDuration;
    private Label label32;
    private Label lab_GrindToolMeters;
    private Label label33;
    private Label lab_Pos11;
    private Label lab_Pos12;
    private Label lab_Pos13;
    private Label lab_Pos14;
    private Label lab_Pos15;
    private Label lab_Pos16;
    private Label lab_Pos17;
    private Label lab_Pos18;
    private Label lab_Pos19;
    private Label lab_Pos29;
    private Label lab_Pos28;
    private Label lab_Pos27;
    private Label lab_Pos26;
    private Label lab_Pos25;
    private Label lab_Pos24;
    private Label lab_Pos23;
    private Label lab_Pos22;
    private Label lab_Pos21;
    private Label label51;
    private Button btn_SimulatePLC;
    private Label label34;
    private Label lab_TeflonCounter;
    //agrassi_FI31-07-08
    private IncDecButton WebBlow;
    private System.ComponentModel.IContainer components = null;

		public Frm_DebugTwin400Status2()
		{
			InitializeComponent();

      this.bitDescriptionsTask = new string[]
    		//AGrassi_FI06/12/2007_1
       {"PLC communication", "PLC running", "Supervisor communication", "Current setup aligned",
        "Next setup aligned", "Third setup aligned", "Message aligned", "Wait for man. sending current",
        "Wait for man. sending next", "Remote Control", "SimulatePLCActive", "Order sending enabled",
        "Recover Positioning Pushed", "Teflon cycle possible", "Teflon setup to send", ""
       };
      this.taskShow.Config("Task Status", this.bitDescriptionsTask, true, null);
      this.taskShow.Debug = true;
      this.bitDescriptionsSlitPositions = new string[]
       {"Remove board", "Last positioning OK slit shaft 1", "Last positioning OK slit shaft 2", "Wait for good slit 1 positions", 
        "Wait for good slit 2 positions", "Wait for definite slit 1 positions", "Wait for definite slit 2 positions", "Slit positions 1 not correct", 
        "Slit positions 2 not correct", "Order not valid (BHS)", "Wait for change of state (BHS)", "Writing not permitted (BHS)",
        "", "", "", ""
       };
      this.slitPositionsShow.Config("Slit Positions Status", this.bitDescriptionsSlitPositions, true, null);
      this.slitPositionsShow.Debug = true;
      this.bitDescriptionsAlarm = new string[]
       {"Generic", "Hardware", "Power", "Logic",
        "BoardMachine", "Consolle", "Warning", "Drives",
        "DeviceNET", "", "", "",
        "", "", "", "Software"
       };
      this.alarmShow.Config("Alarm zones", this.bitDescriptionsAlarm, true, null);
      this.alarmShow.Debug = true;
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

    protected override void F11()
    {
    }

    //private void _RefreshStatus(int taskStatus, int slitPositionsStatus)
    //{
    //  this.taskStatus = taskStatus;
    //  this.slitPositionsStatus = slitPositionsStatus;
    //  this.taskShow.SetValue(this.taskStatus);
    //  this.slitPositionsShow.SetValue(this.slitPositionsStatus);
    //}

    private void _RefreshStatus(string s)
    {
      if (this.logAnalyzer.logAnalyzing)
        return;
      string[] stData;
      stData = s.Split(Command.PARAMETER_SEPARATOR);
      if (stData.Length != 53)
        return;
      int cnt = 0;
      this.lab_NotifyArea.Text = "0x" + this.intParse(stData[cnt++]).ToString("X4");
      this.lab_LastMessageNumber.Text = stData[cnt++];
      this.lab_LastMessageNumberCMD.Text = stData[cnt++];
      this.lab_TransmissionError.Text = stData[cnt++];
      this.lab_TransmissionErrorCode.Text = stData[cnt++];
      this.lab_ID1.Text = stData[cnt++];
      this.lab_ID1.Text += " - "+stData[cnt++];
      this.lab_ID2.Text = stData[cnt++];
      this.lab_ID2.Text += " - " + stData[cnt++];
      this.lab_ID3.Text = stData[cnt++];
      this.lab_ID3.Text += " - " + stData[cnt++];
      this.lab_ChutesPosition.Text = stData[cnt++];
      this.lab_Speed.Text = stData[cnt++];
      this.lab_GrindDuration.Text = stData[cnt++];
      this.lab_LubeDuration.Text = stData[cnt++];
      this.lab_GrindToolMeters.Text = stData[cnt++];
      this.lab_2levWebDirLow.Text = "0x" + this.intParse(stData[cnt++]).ToString("X4");
      this.lab_2levWebDirHigh.Text = "0x" + this.intParse(stData[cnt++]).ToString("X4");
      this.lab_3levWebDirLow.Text = "0x" + this.intParse(stData[cnt++]).ToString("X4");
      this.lab_3levWebDirHigh.Text = "0x" + this.intParse(stData[cnt++]).ToString("X4");
      //AGrassi_FI06/12/2007_1
      this.lab_TeflonCounter.Text = stData[cnt++];
      this.lab_ScoreAngle1.Text = stData[cnt++];
      this.lab_ShaftGap1.Text = stData[cnt++];
      this.lab_ScoreGap1.Text = stData[cnt++];
      this.lab_ScoreAngle2.Text = stData[cnt++];
      this.lab_ShaftGap2.Text = stData[cnt++];
      this.lab_ScoreGap2.Text = stData[cnt++];
      this.lab_ScoreAngle3.Text = stData[cnt++];
      this.lab_ShaftGap3.Text = stData[cnt++];
      this.lab_ScoreGap3.Text = stData[cnt++];
      this.lab_Pos11.Text = stData[cnt++];
      this.lab_Pos12.Text = stData[cnt++];
      this.lab_Pos13.Text = stData[cnt++];
      this.lab_Pos14.Text = stData[cnt++];
      this.lab_Pos15.Text = stData[cnt++];
      this.lab_Pos16.Text = stData[cnt++];
      this.lab_Pos17.Text = stData[cnt++];
      this.lab_Pos18.Text = stData[cnt++];
      this.lab_Pos19.Text = stData[cnt++];
      cnt++;//lab_pos110 not defined client side
      this.lab_Pos21.Text = stData[cnt++];
      this.lab_Pos22.Text = stData[cnt++];
      this.lab_Pos23.Text = stData[cnt++];
      this.lab_Pos24.Text = stData[cnt++];
      this.lab_Pos25.Text = stData[cnt++];
      this.lab_Pos26.Text = stData[cnt++];
      this.lab_Pos27.Text = stData[cnt++];
      this.lab_Pos28.Text = stData[cnt++];
      this.lab_Pos29.Text = stData[cnt++];
      cnt++;//lab_pos210 not defined client side
      this.taskStatus = this.intParse(stData[cnt++]);
      this.slitPositionsStatus = this.intParse(stData[cnt++]);
      this.alarmStatus = this.intParse(stData[cnt++]);
      this.taskShow.SetValue(this.taskStatus);
      this.slitPositionsShow.SetValue(this.slitPositionsStatus);
      this.alarmShow.SetValue(this.alarmStatus);
    }

    private void ShowPLCState(string s)
    {
      if (s.Length < Twin400LogAnalyzer.START_INFO_LOG_INDEX+100 * 4)
        return;
      this.lab_NotifyArea.Text = "0x" + s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 0, 4);
      this.lab_LastMessageNumber.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 3, 4), true).ToString();
      this.lab_LastMessageNumberCMD.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 4, 4), true).ToString();
      this.lab_TransmissionError.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 6, 4), true).ToString();
      this.lab_TransmissionErrorCode.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 7, 4), true).ToString();
      this.lab_ID1.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 20, 4), true).ToString();
      this.lab_ID1.Text += " - " + intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 21, 4), true).ToString();
      this.lab_ID2.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 22, 4), true).ToString();
      this.lab_ID2.Text += " - " + intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 23, 4), true).ToString();
      this.lab_ID3.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 24, 4), true).ToString();
      this.lab_ID3.Text += " - " + intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 25, 4), true).ToString();
      this.lab_ChutesPosition.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 34, 4), true).ToString();
      this.lab_Speed.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 28, 4), true).ToString();
      this.lab_GrindDuration.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 32, 4), true).ToString();
      this.lab_LubeDuration.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 33, 4), true).ToString();
      this.lab_GrindToolMeters.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 36, 4) + s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 35, 4), true).ToString();
      this.lab_2levWebDirLow.Text = "0x" + s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 37, 4);
      this.lab_2levWebDirHigh.Text = "0x" + s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 38, 4);
      this.lab_3levWebDirLow.Text = "0x" + s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 39, 4);
      this.lab_3levWebDirHigh.Text = "0x" + s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 40, 4);
      this.lab_ScoreAngle1.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 29, 4), true).ToString() + "0";
      this.lab_ShaftGap1.Text = "";
      this.lab_ScoreGap1.Text = "";
      this.lab_ScoreAngle2.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 30, 4), true).ToString() + "0";
      this.lab_ShaftGap2.Text = "";
      this.lab_ScoreGap2.Text = "";
      this.lab_ScoreAngle3.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 31, 4), true).ToString() + "0";
      this.lab_ShaftGap3.Text = "";
      this.lab_ScoreGap3.Text = "";
      this.lab_Pos11.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 80, 4), true).ToString();
      this.lab_Pos12.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 81, 4), true).ToString();
      this.lab_Pos13.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 82, 4), true).ToString();
      this.lab_Pos14.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 83, 4), true).ToString();
      this.lab_Pos15.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 84, 4), true).ToString();
      this.lab_Pos16.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 85, 4), true).ToString();
      this.lab_Pos17.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 86, 4), true).ToString();
      this.lab_Pos18.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 87, 4), true).ToString();
      this.lab_Pos19.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 88, 4), true).ToString();
      this.lab_Pos21.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 90, 4), true).ToString();
      this.lab_Pos22.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 91, 4), true).ToString();
      this.lab_Pos23.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 92, 4), true).ToString();
      this.lab_Pos24.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 93, 4), true).ToString();
      this.lab_Pos25.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 94, 4), true).ToString();
      this.lab_Pos26.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 95, 4), true).ToString();
      this.lab_Pos27.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 96, 4), true).ToString();
      this.lab_Pos28.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 97, 4), true).ToString();
      this.lab_Pos29.Text = intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 98, 4), true).ToString();
      this.taskStatus = 0;
      this.slitPositionsStatus = 0;
      this.alarmStatus = 0;
      if (s.Length > Twin400LogAnalyzer.START_INFO_LOG_INDEX + 100 * 4 + 1) 
      {
        this.taskStatus = this.intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 100, 4), true);
        this.slitPositionsStatus = this.intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 101, 4), true);
        this.alarmStatus = this.intParse(s.Substring(Twin400LogAnalyzer.START_INFO_LOG_INDEX + 4 * 41, 4), true);
      }
      this.taskShow.SetValue(this.taskStatus);
      this.slitPositionsShow.SetValue(this.slitPositionsStatus);
      this.alarmShow.SetValue(this.alarmStatus);
    }

    #region Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DebugTwin400Status2));
      this.taskShow = new SyncroC.Controls.IOChannel();
      this.slitPositionsShow = new SyncroC.Controls.IOChannel();
      this.label1 = new System.Windows.Forms.Label();
      this.lab_NotifyArea = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.lab_LastMessageNumber = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.lab_LastMessageNumberCMD = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.lab_TransmissionError = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.lab_TransmissionErrorCode = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.lab_ID1 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.lab_ID2 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.lab_ID3 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.lab_ScoreGap1 = new System.Windows.Forms.Label();
      this.label13 = new System.Windows.Forms.Label();
      this.lab_ScoreGap2 = new System.Windows.Forms.Label();
      this.lab_ScoreGap3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lab_ChutesPosition = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.lab_2levWebDirLow = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.lab_2levWebDirHigh = new System.Windows.Forms.Label();
      this.label15 = new System.Windows.Forms.Label();
      this.lab_3levWebDirLow = new System.Windows.Forms.Label();
      this.label16 = new System.Windows.Forms.Label();
      this.lab_3levWebDirHigh = new System.Windows.Forms.Label();
      this.btn_SendSetup0 = new System.Windows.Forms.Button();
      this.btn_SendSetup1 = new System.Windows.Forms.Button();
      this.btn_SendSetup2 = new System.Windows.Forms.Button();
      this.label17 = new System.Windows.Forms.Label();
      this.lab_ScoreAngle3 = new System.Windows.Forms.Label();
      this.label19 = new System.Windows.Forms.Label();
      this.lab_ScoreAngle2 = new System.Windows.Forms.Label();
      this.label21 = new System.Windows.Forms.Label();
      this.lab_ScoreAngle1 = new System.Windows.Forms.Label();
      this.lbl_UM22 = new System.Windows.Forms.Label();
      this.label18 = new System.Windows.Forms.Label();
      this.label20 = new System.Windows.Forms.Label();
      this.lbl_UM12 = new System.Windows.Forms.Label();
      this.label14 = new System.Windows.Forms.Label();
      this.label22 = new System.Windows.Forms.Label();
      this.label23 = new System.Windows.Forms.Label();
      this.label24 = new System.Windows.Forms.Label();
      this.label25 = new System.Windows.Forms.Label();
      this.label26 = new System.Windows.Forms.Label();
      this.label27 = new System.Windows.Forms.Label();
      this.lab_ShaftGap3 = new System.Windows.Forms.Label();
      this.label29 = new System.Windows.Forms.Label();
      this.lab_ShaftGap2 = new System.Windows.Forms.Label();
      this.label31 = new System.Windows.Forms.Label();
      this.lab_ShaftGap1 = new System.Windows.Forms.Label();
      this.label28 = new System.Windows.Forms.Label();
      this.lab_Speed = new System.Windows.Forms.Label();
      this.label55 = new System.Windows.Forms.Label();
      this.lab_GrindDuration = new System.Windows.Forms.Label();
      this.label30 = new System.Windows.Forms.Label();
      this.lab_LubeDuration = new System.Windows.Forms.Label();
      this.label32 = new System.Windows.Forms.Label();
      this.lab_GrindToolMeters = new System.Windows.Forms.Label();
      this.alarmShow = new SyncroC.Controls.IOChannel();
      this.label33 = new System.Windows.Forms.Label();
      this.lab_Pos11 = new System.Windows.Forms.Label();
      this.lab_Pos12 = new System.Windows.Forms.Label();
      this.lab_Pos13 = new System.Windows.Forms.Label();
      this.lab_Pos14 = new System.Windows.Forms.Label();
      this.lab_Pos15 = new System.Windows.Forms.Label();
      this.lab_Pos16 = new System.Windows.Forms.Label();
      this.lab_Pos17 = new System.Windows.Forms.Label();
      this.lab_Pos18 = new System.Windows.Forms.Label();
      this.lab_Pos19 = new System.Windows.Forms.Label();
      this.lab_Pos29 = new System.Windows.Forms.Label();
      this.lab_Pos28 = new System.Windows.Forms.Label();
      this.lab_Pos27 = new System.Windows.Forms.Label();
      this.lab_Pos26 = new System.Windows.Forms.Label();
      this.lab_Pos25 = new System.Windows.Forms.Label();
      this.lab_Pos24 = new System.Windows.Forms.Label();
      this.lab_Pos23 = new System.Windows.Forms.Label();
      this.lab_Pos22 = new System.Windows.Forms.Label();
      this.lab_Pos21 = new System.Windows.Forms.Label();
      this.label51 = new System.Windows.Forms.Label();
      this.btn_SimulatePLC = new System.Windows.Forms.Button();
      this.label34 = new System.Windows.Forms.Label();
      this.lab_TeflonCounter = new System.Windows.Forms.Label();
      this.WebBlow = new SyncroC.Controls.IncDecButton.IncDecButton();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_Title_YN.Text = "MACHINE STATUS AND TASK STATUS";
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
      this.lbl_CtrlF5.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
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
      // taskShow
      // 
      this.taskShow.BackColor = System.Drawing.Color.WhiteSmoke;
      this.taskShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.taskShow.Debug = false;
      this.taskShow.Location = new System.Drawing.Point(793, 187);
      this.taskShow.Name = "taskShow";
      this.taskShow.Size = new System.Drawing.Size(197, 283);
      this.taskShow.TabIndex = 110;
      this.taskShow.TranslateText = false;
      // 
      // slitPositionsShow
      // 
      this.slitPositionsShow.BackColor = System.Drawing.Color.WhiteSmoke;
      this.slitPositionsShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.slitPositionsShow.Debug = false;
      this.slitPositionsShow.Location = new System.Drawing.Point(990, 187);
      this.slitPositionsShow.Name = "slitPositionsShow";
      this.slitPositionsShow.Size = new System.Drawing.Size(216, 283);
      this.slitPositionsShow.TabIndex = 110;
      this.slitPositionsShow.TranslateText = false;
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label1.Location = new System.Drawing.Point(131, 174);
      this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(223, 23);
      this.label1.TabIndex = 111;
      this.label1.Text = "Notify Area:";
      // 
      // lab_NotifyArea
      // 
      this.lab_NotifyArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_NotifyArea.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_NotifyArea.Location = new System.Drawing.Point(355, 174);
      this.lab_NotifyArea.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_NotifyArea.Name = "lab_NotifyArea";
      this.lab_NotifyArea.Size = new System.Drawing.Size(93, 23);
      this.lab_NotifyArea.TabIndex = 112;
      // 
      // label4
      // 
      this.label4.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label4.Location = new System.Drawing.Point(131, 204);
      this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(223, 23);
      this.label4.TabIndex = 117;
      this.label4.Text = "Last Message Number:";
      // 
      // lab_LastMessageNumber
      // 
      this.lab_LastMessageNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_LastMessageNumber.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_LastMessageNumber.Location = new System.Drawing.Point(355, 204);
      this.lab_LastMessageNumber.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_LastMessageNumber.Name = "lab_LastMessageNumber";
      this.lab_LastMessageNumber.Size = new System.Drawing.Size(93, 23);
      this.lab_LastMessageNumber.TabIndex = 118;
      // 
      // label5
      // 
      this.label5.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label5.Location = new System.Drawing.Point(131, 234);
      this.label5.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(223, 23);
      this.label5.TabIndex = 119;
      this.label5.Text = "Last Message Number CMD:";
      // 
      // lab_LastMessageNumberCMD
      // 
      this.lab_LastMessageNumberCMD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_LastMessageNumberCMD.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_LastMessageNumberCMD.Location = new System.Drawing.Point(355, 234);
      this.lab_LastMessageNumberCMD.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_LastMessageNumberCMD.Name = "lab_LastMessageNumberCMD";
      this.lab_LastMessageNumberCMD.Size = new System.Drawing.Size(93, 23);
      this.lab_LastMessageNumberCMD.TabIndex = 120;
      // 
      // label7
      // 
      this.label7.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label7.Location = new System.Drawing.Point(131, 264);
      this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(223, 23);
      this.label7.TabIndex = 123;
      this.label7.Text = "Transmission Error:";
      // 
      // lab_TransmissionError
      // 
      this.lab_TransmissionError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_TransmissionError.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_TransmissionError.Location = new System.Drawing.Point(355, 264);
      this.lab_TransmissionError.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_TransmissionError.Name = "lab_TransmissionError";
      this.lab_TransmissionError.Size = new System.Drawing.Size(93, 23);
      this.lab_TransmissionError.TabIndex = 124;
      // 
      // label8
      // 
      this.label8.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label8.Location = new System.Drawing.Point(131, 294);
      this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(223, 23);
      this.label8.TabIndex = 125;
      this.label8.Text = "Transmission Error Code:";
      // 
      // lab_TransmissionErrorCode
      // 
      this.lab_TransmissionErrorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_TransmissionErrorCode.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_TransmissionErrorCode.Location = new System.Drawing.Point(355, 294);
      this.lab_TransmissionErrorCode.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_TransmissionErrorCode.Name = "lab_TransmissionErrorCode";
      this.lab_TransmissionErrorCode.Size = new System.Drawing.Size(93, 23);
      this.lab_TransmissionErrorCode.TabIndex = 126;
      // 
      // label10
      // 
      this.label10.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label10.Location = new System.Drawing.Point(131, 324);
      this.label10.Margin = new System.Windows.Forms.Padding(3, 3, 0, 1);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(223, 23);
      this.label10.TabIndex = 127;
      this.label10.Text = "ID Current:";
      // 
      // lab_ID1
      // 
      this.lab_ID1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ID1.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ID1.Location = new System.Drawing.Point(355, 324);
      this.lab_ID1.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_ID1.Name = "lab_ID1";
      this.lab_ID1.Size = new System.Drawing.Size(93, 23);
      this.lab_ID1.TabIndex = 128;
      // 
      // label9
      // 
      this.label9.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label9.Location = new System.Drawing.Point(131, 354);
      this.label9.Margin = new System.Windows.Forms.Padding(3, 1, 0, 3);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(223, 23);
      this.label9.TabIndex = 129;
      this.label9.Text = "ID Next:";
      // 
      // lab_ID2
      // 
      this.lab_ID2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ID2.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ID2.Location = new System.Drawing.Point(355, 354);
      this.lab_ID2.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_ID2.Name = "lab_ID2";
      this.lab_ID2.Size = new System.Drawing.Size(93, 23);
      this.lab_ID2.TabIndex = 130;
      // 
      // label11
      // 
      this.label11.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label11.Location = new System.Drawing.Point(131, 384);
      this.label11.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(223, 23);
      this.label11.TabIndex = 131;
      this.label11.Text = "ID third:";
      // 
      // lab_ID3
      // 
      this.lab_ID3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ID3.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ID3.Location = new System.Drawing.Point(355, 384);
      this.lab_ID3.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_ID3.Name = "lab_ID3";
      this.lab_ID3.Size = new System.Drawing.Size(93, 23);
      this.lab_ID3.TabIndex = 132;
      // 
      // label12
      // 
      this.label12.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label12.Location = new System.Drawing.Point(478, 369);
      this.label12.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(142, 23);
      this.label12.TabIndex = 133;
      this.label12.Text = "Act Score Gap 1:";
      // 
      // lab_ScoreGap1
      // 
      this.lab_ScoreGap1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ScoreGap1.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ScoreGap1.Location = new System.Drawing.Point(621, 369);
      this.lab_ScoreGap1.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_ScoreGap1.Name = "lab_ScoreGap1";
      this.lab_ScoreGap1.Size = new System.Drawing.Size(73, 23);
      this.lab_ScoreGap1.TabIndex = 134;
      // 
      // label13
      // 
      this.label13.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label13.Location = new System.Drawing.Point(478, 398);
      this.label13.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(142, 23);
      this.label13.TabIndex = 135;
      this.label13.Text = "Act Score Gap 2:";
      // 
      // lab_ScoreGap2
      // 
      this.lab_ScoreGap2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ScoreGap2.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ScoreGap2.Location = new System.Drawing.Point(621, 398);
      this.lab_ScoreGap2.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_ScoreGap2.Name = "lab_ScoreGap2";
      this.lab_ScoreGap2.Size = new System.Drawing.Size(73, 23);
      this.lab_ScoreGap2.TabIndex = 136;
      // 
      // lab_ScoreGap3
      // 
      this.lab_ScoreGap3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ScoreGap3.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ScoreGap3.Location = new System.Drawing.Point(621, 428);
      this.lab_ScoreGap3.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_ScoreGap3.Name = "lab_ScoreGap3";
      this.lab_ScoreGap3.Size = new System.Drawing.Size(73, 23);
      this.lab_ScoreGap3.TabIndex = 138;
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label2.Location = new System.Drawing.Point(130, 417);
      this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(223, 23);
      this.label2.TabIndex = 139;
      this.label2.Text = "Trim Chutes Position:";
      // 
      // lab_ChutesPosition
      // 
      this.lab_ChutesPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ChutesPosition.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ChutesPosition.Location = new System.Drawing.Point(355, 417);
      this.lab_ChutesPosition.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_ChutesPosition.Name = "lab_ChutesPosition";
      this.lab_ChutesPosition.Size = new System.Drawing.Size(93, 23);
      this.lab_ChutesPosition.TabIndex = 140;
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label3.Location = new System.Drawing.Point(130, 568);
      this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(223, 23);
      this.label3.TabIndex = 141;
      this.label3.Text = "2-lev Web Director Low Mask:";
      // 
      // lab_2levWebDirLow
      // 
      this.lab_2levWebDirLow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_2levWebDirLow.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_2levWebDirLow.Location = new System.Drawing.Point(355, 568);
      this.lab_2levWebDirLow.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_2levWebDirLow.Name = "lab_2levWebDirLow";
      this.lab_2levWebDirLow.Size = new System.Drawing.Size(93, 23);
      this.lab_2levWebDirLow.TabIndex = 142;
      // 
      // label6
      // 
      this.label6.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label6.Location = new System.Drawing.Point(130, 598);
      this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(223, 23);
      this.label6.TabIndex = 143;
      this.label6.Text = "2-lev Web Director High Mask:";
      // 
      // lab_2levWebDirHigh
      // 
      this.lab_2levWebDirHigh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_2levWebDirHigh.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_2levWebDirHigh.Location = new System.Drawing.Point(355, 598);
      this.lab_2levWebDirHigh.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_2levWebDirHigh.Name = "lab_2levWebDirHigh";
      this.lab_2levWebDirHigh.Size = new System.Drawing.Size(93, 23);
      this.lab_2levWebDirHigh.TabIndex = 144;
      // 
      // label15
      // 
      this.label15.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label15.Location = new System.Drawing.Point(130, 628);
      this.label15.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(223, 23);
      this.label15.TabIndex = 145;
      this.label15.Text = "3-lev Web Director Low Mask:";
      // 
      // lab_3levWebDirLow
      // 
      this.lab_3levWebDirLow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_3levWebDirLow.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_3levWebDirLow.Location = new System.Drawing.Point(355, 628);
      this.lab_3levWebDirLow.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_3levWebDirLow.Name = "lab_3levWebDirLow";
      this.lab_3levWebDirLow.Size = new System.Drawing.Size(93, 23);
      this.lab_3levWebDirLow.TabIndex = 146;
      // 
      // label16
      // 
      this.label16.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label16.Location = new System.Drawing.Point(130, 658);
      this.label16.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(223, 23);
      this.label16.TabIndex = 147;
      this.label16.Text = "3-lev Web Director High Mask:";
      // 
      // lab_3levWebDirHigh
      // 
      this.lab_3levWebDirHigh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_3levWebDirHigh.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_3levWebDirHigh.Location = new System.Drawing.Point(355, 658);
      this.lab_3levWebDirHigh.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_3levWebDirHigh.Name = "lab_3levWebDirHigh";
      this.lab_3levWebDirHigh.Size = new System.Drawing.Size(93, 23);
      this.lab_3levWebDirHigh.TabIndex = 148;
      // 
      // btn_SendSetup0
      // 
      this.btn_SendSetup0.FlatAppearance.BorderSize = 2;
      this.btn_SendSetup0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_SendSetup0.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_SendSetup0.Location = new System.Drawing.Point(478, 607);
      this.btn_SendSetup0.Name = "btn_SendSetup0";
      this.btn_SendSetup0.Size = new System.Drawing.Size(142, 34);
      this.btn_SendSetup0.TabIndex = 149;
      this.btn_SendSetup0.Text = "SEND CURRENT";
      this.btn_SendSetup0.Click += new System.EventHandler(this.btn_SendSetup_Click);
      // 
      // btn_SendSetup1
      // 
      this.btn_SendSetup1.FlatAppearance.BorderSize = 2;
      this.btn_SendSetup1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_SendSetup1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_SendSetup1.Location = new System.Drawing.Point(627, 607);
      this.btn_SendSetup1.Name = "btn_SendSetup1";
      this.btn_SendSetup1.Size = new System.Drawing.Size(142, 34);
      this.btn_SendSetup1.TabIndex = 150;
      this.btn_SendSetup1.Text = "SEND NEXT";
      this.btn_SendSetup1.Click += new System.EventHandler(this.btn_SendSetup_Click);
      // 
      // btn_SendSetup2
      // 
      this.btn_SendSetup2.FlatAppearance.BorderSize = 2;
      this.btn_SendSetup2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_SendSetup2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_SendSetup2.Location = new System.Drawing.Point(776, 607);
      this.btn_SendSetup2.Name = "btn_SendSetup2";
      this.btn_SendSetup2.Size = new System.Drawing.Size(142, 34);
      this.btn_SendSetup2.TabIndex = 151;
      this.btn_SendSetup2.Text = "SEND THIRD";
      this.btn_SendSetup2.Click += new System.EventHandler(this.btn_SendSetup_Click);
      // 
      // label17
      // 
      this.label17.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label17.Location = new System.Drawing.Point(477, 248);
      this.label17.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(127, 23);
      this.label17.TabIndex = 156;
      this.label17.Text = "Score Angle Aux:";
      // 
      // lab_ScoreAngle3
      // 
      this.lab_ScoreAngle3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ScoreAngle3.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ScoreAngle3.Location = new System.Drawing.Point(606, 248);
      this.lab_ScoreAngle3.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_ScoreAngle3.Name = "lab_ScoreAngle3";
      this.lab_ScoreAngle3.Size = new System.Drawing.Size(88, 23);
      this.lab_ScoreAngle3.TabIndex = 157;
      // 
      // label19
      // 
      this.label19.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label19.Location = new System.Drawing.Point(478, 218);
      this.label19.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label19.Name = "label19";
      this.label19.Size = new System.Drawing.Size(127, 23);
      this.label19.TabIndex = 154;
      this.label19.Text = "Score Angle 2:";
      // 
      // lab_ScoreAngle2
      // 
      this.lab_ScoreAngle2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ScoreAngle2.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ScoreAngle2.Location = new System.Drawing.Point(606, 218);
      this.lab_ScoreAngle2.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_ScoreAngle2.Name = "lab_ScoreAngle2";
      this.lab_ScoreAngle2.Size = new System.Drawing.Size(88, 23);
      this.lab_ScoreAngle2.TabIndex = 155;
      // 
      // label21
      // 
      this.label21.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label21.Location = new System.Drawing.Point(478, 189);
      this.label21.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label21.Name = "label21";
      this.label21.Size = new System.Drawing.Size(127, 23);
      this.label21.TabIndex = 152;
      this.label21.Text = "Score Angle 1:";
      // 
      // lab_ScoreAngle1
      // 
      this.lab_ScoreAngle1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ScoreAngle1.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ScoreAngle1.Location = new System.Drawing.Point(606, 189);
      this.lab_ScoreAngle1.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_ScoreAngle1.Name = "lab_ScoreAngle1";
      this.lab_ScoreAngle1.Size = new System.Drawing.Size(88, 23);
      this.lab_ScoreAngle1.TabIndex = 153;
      // 
      // lbl_UM22
      // 
      this.lbl_UM22.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_UM22.Location = new System.Drawing.Point(697, 188);
      this.lbl_UM22.Name = "lbl_UM22";
      this.lbl_UM22.Size = new System.Drawing.Size(68, 23);
      this.lbl_UM22.TabIndex = 158;
      this.lbl_UM22.Text = "[° /100]";
      this.lbl_UM22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label18
      // 
      this.label18.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label18.Location = new System.Drawing.Point(697, 216);
      this.label18.Name = "label18";
      this.label18.Size = new System.Drawing.Size(68, 23);
      this.label18.TabIndex = 159;
      this.label18.Text = "[° /100]";
      this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label20
      // 
      this.label20.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label20.Location = new System.Drawing.Point(697, 247);
      this.label20.Name = "label20";
      this.label20.Size = new System.Drawing.Size(68, 23);
      this.label20.TabIndex = 160;
      this.label20.Text = "[° /100]";
      this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lbl_UM12
      // 
      this.lbl_UM12.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_UM12.Location = new System.Drawing.Point(697, 366);
      this.lbl_UM12.Name = "lbl_UM12";
      this.lbl_UM12.Size = new System.Drawing.Size(68, 23);
      this.lbl_UM12.TabIndex = 161;
      this.lbl_UM12.Text = "[mm/10]";
      this.lbl_UM12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label14
      // 
      this.label14.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label14.Location = new System.Drawing.Point(477, 428);
      this.label14.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(143, 23);
      this.label14.TabIndex = 137;
      this.label14.Text = "Act Score Gap Aux:";
      // 
      // label22
      // 
      this.label22.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label22.Location = new System.Drawing.Point(697, 396);
      this.label22.Name = "label22";
      this.label22.Size = new System.Drawing.Size(68, 23);
      this.label22.TabIndex = 162;
      this.label22.Text = "[mm/10]";
      this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label23
      // 
      this.label23.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label23.Location = new System.Drawing.Point(697, 427);
      this.label23.Name = "label23";
      this.label23.Size = new System.Drawing.Size(68, 23);
      this.label23.TabIndex = 163;
      this.label23.Text = "[mm/10]";
      this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label24
      // 
      this.label24.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label24.Location = new System.Drawing.Point(697, 336);
      this.label24.Name = "label24";
      this.label24.Size = new System.Drawing.Size(68, 23);
      this.label24.TabIndex = 172;
      this.label24.Text = "[mm/10]";
      this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label25
      // 
      this.label25.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label25.Location = new System.Drawing.Point(697, 305);
      this.label25.Name = "label25";
      this.label25.Size = new System.Drawing.Size(68, 23);
      this.label25.TabIndex = 171;
      this.label25.Text = "[mm/10]";
      this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label26
      // 
      this.label26.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label26.Location = new System.Drawing.Point(697, 275);
      this.label26.Name = "label26";
      this.label26.Size = new System.Drawing.Size(68, 23);
      this.label26.TabIndex = 170;
      this.label26.Text = "[mm/10]";
      this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label27
      // 
      this.label27.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label27.Location = new System.Drawing.Point(477, 337);
      this.label27.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label27.Name = "label27";
      this.label27.Size = new System.Drawing.Size(122, 23);
      this.label27.TabIndex = 168;
      this.label27.Text = "Shaft Gap Aux:";
      // 
      // lab_ShaftGap3
      // 
      this.lab_ShaftGap3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ShaftGap3.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ShaftGap3.Location = new System.Drawing.Point(601, 337);
      this.lab_ShaftGap3.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_ShaftGap3.Name = "lab_ShaftGap3";
      this.lab_ShaftGap3.Size = new System.Drawing.Size(93, 23);
      this.lab_ShaftGap3.TabIndex = 169;
      // 
      // label29
      // 
      this.label29.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label29.Location = new System.Drawing.Point(478, 307);
      this.label29.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label29.Name = "label29";
      this.label29.Size = new System.Drawing.Size(122, 23);
      this.label29.TabIndex = 166;
      this.label29.Text = "Shaft Gap 2:";
      // 
      // lab_ShaftGap2
      // 
      this.lab_ShaftGap2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ShaftGap2.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ShaftGap2.Location = new System.Drawing.Point(601, 307);
      this.lab_ShaftGap2.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_ShaftGap2.Name = "lab_ShaftGap2";
      this.lab_ShaftGap2.Size = new System.Drawing.Size(93, 23);
      this.lab_ShaftGap2.TabIndex = 167;
      // 
      // label31
      // 
      this.label31.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label31.Location = new System.Drawing.Point(478, 278);
      this.label31.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label31.Name = "label31";
      this.label31.Size = new System.Drawing.Size(122, 23);
      this.label31.TabIndex = 164;
      this.label31.Text = "Shaft Gap 1:";
      // 
      // lab_ShaftGap1
      // 
      this.lab_ShaftGap1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_ShaftGap1.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_ShaftGap1.Location = new System.Drawing.Point(601, 278);
      this.lab_ShaftGap1.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_ShaftGap1.Name = "lab_ShaftGap1";
      this.lab_ShaftGap1.Size = new System.Drawing.Size(93, 23);
      this.lab_ShaftGap1.TabIndex = 165;
      // 
      // label28
      // 
      this.label28.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label28.Location = new System.Drawing.Point(130, 449);
      this.label28.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label28.Name = "label28";
      this.label28.Size = new System.Drawing.Size(223, 23);
      this.label28.TabIndex = 173;
      this.label28.Text = "Speed:";
      // 
      // lab_Speed
      // 
      this.lab_Speed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Speed.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Speed.Location = new System.Drawing.Point(355, 449);
      this.lab_Speed.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_Speed.Name = "lab_Speed";
      this.lab_Speed.Size = new System.Drawing.Size(93, 23);
      this.lab_Speed.TabIndex = 174;
      // 
      // label55
      // 
      this.label55.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label55.Location = new System.Drawing.Point(130, 478);
      this.label55.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label55.Name = "label55";
      this.label55.Size = new System.Drawing.Size(223, 23);
      this.label55.TabIndex = 175;
      this.label55.Text = "Grinding duration (m):";
      // 
      // lab_GrindDuration
      // 
      this.lab_GrindDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_GrindDuration.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_GrindDuration.Location = new System.Drawing.Point(355, 478);
      this.lab_GrindDuration.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_GrindDuration.Name = "lab_GrindDuration";
      this.lab_GrindDuration.Size = new System.Drawing.Size(93, 23);
      this.lab_GrindDuration.TabIndex = 176;
      // 
      // label30
      // 
      this.label30.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label30.Location = new System.Drawing.Point(130, 507);
      this.label30.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label30.Name = "label30";
      this.label30.Size = new System.Drawing.Size(223, 23);
      this.label30.TabIndex = 177;
      this.label30.Text = "Lube duration (ms):";
      // 
      // lab_LubeDuration
      // 
      this.lab_LubeDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_LubeDuration.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_LubeDuration.Location = new System.Drawing.Point(355, 507);
      this.lab_LubeDuration.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_LubeDuration.Name = "lab_LubeDuration";
      this.lab_LubeDuration.Size = new System.Drawing.Size(93, 23);
      this.lab_LubeDuration.TabIndex = 178;
      // 
      // label32
      // 
      this.label32.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label32.Location = new System.Drawing.Point(130, 536);
      this.label32.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label32.Name = "label32";
      this.label32.Size = new System.Drawing.Size(223, 23);
      this.label32.TabIndex = 179;
      this.label32.Text = "Grind tool run meters:";
      // 
      // lab_GrindToolMeters
      // 
      this.lab_GrindToolMeters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_GrindToolMeters.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_GrindToolMeters.Location = new System.Drawing.Point(355, 536);
      this.lab_GrindToolMeters.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_GrindToolMeters.Name = "lab_GrindToolMeters";
      this.lab_GrindToolMeters.Size = new System.Drawing.Size(93, 23);
      this.lab_GrindToolMeters.TabIndex = 180;
      // 
      // alarmShow
      // 
      this.alarmShow.BackColor = System.Drawing.Color.WhiteSmoke;
      this.alarmShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.alarmShow.Debug = false;
      this.alarmShow.Location = new System.Drawing.Point(990, 471);
      this.alarmShow.Name = "alarmShow";
      this.alarmShow.Size = new System.Drawing.Size(216, 263);
      this.alarmShow.TabIndex = 181;
      this.alarmShow.TranslateText = false;
      // 
      // label33
      // 
      this.label33.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label33.Location = new System.Drawing.Point(477, 467);
      this.label33.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.label33.Name = "label33";
      this.label33.Size = new System.Drawing.Size(223, 23);
      this.label33.TabIndex = 182;
      this.label33.Text = "Slit Shaft 1 Positions";
      // 
      // lab_Pos11
      // 
      this.lab_Pos11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos11.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos11.Location = new System.Drawing.Point(480, 496);
      this.lab_Pos11.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos11.Name = "lab_Pos11";
      this.lab_Pos11.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos11.TabIndex = 183;
      // 
      // lab_Pos12
      // 
      this.lab_Pos12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos12.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos12.Location = new System.Drawing.Point(534, 496);
      this.lab_Pos12.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos12.Name = "lab_Pos12";
      this.lab_Pos12.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos12.TabIndex = 184;
      // 
      // lab_Pos13
      // 
      this.lab_Pos13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos13.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos13.Location = new System.Drawing.Point(588, 496);
      this.lab_Pos13.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos13.Name = "lab_Pos13";
      this.lab_Pos13.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos13.TabIndex = 185;
      // 
      // lab_Pos14
      // 
      this.lab_Pos14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos14.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos14.Location = new System.Drawing.Point(642, 496);
      this.lab_Pos14.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos14.Name = "lab_Pos14";
      this.lab_Pos14.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos14.TabIndex = 186;
      // 
      // lab_Pos15
      // 
      this.lab_Pos15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos15.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos15.Location = new System.Drawing.Point(696, 496);
      this.lab_Pos15.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos15.Name = "lab_Pos15";
      this.lab_Pos15.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos15.TabIndex = 187;
      // 
      // lab_Pos16
      // 
      this.lab_Pos16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos16.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos16.Location = new System.Drawing.Point(750, 496);
      this.lab_Pos16.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos16.Name = "lab_Pos16";
      this.lab_Pos16.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos16.TabIndex = 188;
      // 
      // lab_Pos17
      // 
      this.lab_Pos17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos17.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos17.Location = new System.Drawing.Point(804, 496);
      this.lab_Pos17.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos17.Name = "lab_Pos17";
      this.lab_Pos17.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos17.TabIndex = 189;
      // 
      // lab_Pos18
      // 
      this.lab_Pos18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos18.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos18.Location = new System.Drawing.Point(858, 496);
      this.lab_Pos18.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos18.Name = "lab_Pos18";
      this.lab_Pos18.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos18.TabIndex = 190;
      // 
      // lab_Pos19
      // 
      this.lab_Pos19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos19.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos19.Location = new System.Drawing.Point(912, 496);
      this.lab_Pos19.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos19.Name = "lab_Pos19";
      this.lab_Pos19.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos19.TabIndex = 191;
      // 
      // lab_Pos29
      // 
      this.lab_Pos29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos29.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos29.Location = new System.Drawing.Point(912, 560);
      this.lab_Pos29.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos29.Name = "lab_Pos29";
      this.lab_Pos29.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos29.TabIndex = 201;
      // 
      // lab_Pos28
      // 
      this.lab_Pos28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos28.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos28.Location = new System.Drawing.Point(858, 560);
      this.lab_Pos28.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos28.Name = "lab_Pos28";
      this.lab_Pos28.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos28.TabIndex = 200;
      // 
      // lab_Pos27
      // 
      this.lab_Pos27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos27.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos27.Location = new System.Drawing.Point(804, 560);
      this.lab_Pos27.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos27.Name = "lab_Pos27";
      this.lab_Pos27.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos27.TabIndex = 199;
      // 
      // lab_Pos26
      // 
      this.lab_Pos26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos26.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos26.Location = new System.Drawing.Point(750, 560);
      this.lab_Pos26.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos26.Name = "lab_Pos26";
      this.lab_Pos26.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos26.TabIndex = 198;
      // 
      // lab_Pos25
      // 
      this.lab_Pos25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos25.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos25.Location = new System.Drawing.Point(696, 560);
      this.lab_Pos25.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos25.Name = "lab_Pos25";
      this.lab_Pos25.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos25.TabIndex = 197;
      // 
      // lab_Pos24
      // 
      this.lab_Pos24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos24.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos24.Location = new System.Drawing.Point(642, 560);
      this.lab_Pos24.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos24.Name = "lab_Pos24";
      this.lab_Pos24.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos24.TabIndex = 196;
      // 
      // lab_Pos23
      // 
      this.lab_Pos23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos23.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos23.Location = new System.Drawing.Point(588, 560);
      this.lab_Pos23.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos23.Name = "lab_Pos23";
      this.lab_Pos23.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos23.TabIndex = 195;
      // 
      // lab_Pos22
      // 
      this.lab_Pos22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos22.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos22.Location = new System.Drawing.Point(534, 560);
      this.lab_Pos22.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos22.Name = "lab_Pos22";
      this.lab_Pos22.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos22.TabIndex = 194;
      // 
      // lab_Pos21
      // 
      this.lab_Pos21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_Pos21.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_Pos21.Location = new System.Drawing.Point(480, 560);
      this.lab_Pos21.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.lab_Pos21.Name = "lab_Pos21";
      this.lab_Pos21.Size = new System.Drawing.Size(50, 23);
      this.lab_Pos21.TabIndex = 193;
      // 
      // label51
      // 
      this.label51.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label51.Location = new System.Drawing.Point(477, 531);
      this.label51.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.label51.Name = "label51";
      this.label51.Size = new System.Drawing.Size(223, 23);
      this.label51.TabIndex = 192;
      this.label51.Text = "Slit Shaft 2 Positions";
      // 
      // btn_SimulatePLC
      // 
      this.btn_SimulatePLC.FlatAppearance.BorderSize = 2;
      this.btn_SimulatePLC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_SimulatePLC.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_SimulatePLC.Location = new System.Drawing.Point(478, 661);
      this.btn_SimulatePLC.Name = "btn_SimulatePLC";
      this.btn_SimulatePLC.Size = new System.Drawing.Size(142, 34);
      this.btn_SimulatePLC.TabIndex = 202;
      this.btn_SimulatePLC.Text = "Simulate PLC";
      this.btn_SimulatePLC.Click += new System.EventHandler(this.btn_SimulatePLC_Click);
      // 
      // label34
      // 
      this.label34.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.label34.Location = new System.Drawing.Point(130, 687);
      this.label34.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.label34.Name = "label34";
      this.label34.Size = new System.Drawing.Size(223, 23);
      this.label34.TabIndex = 203;
      this.label34.Text = "Teflon counter:";
      // 
      // lab_TeflonCounter
      // 
      this.lab_TeflonCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lab_TeflonCounter.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lab_TeflonCounter.Location = new System.Drawing.Point(355, 687);
      this.lab_TeflonCounter.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
      this.lab_TeflonCounter.Name = "lab_TeflonCounter";
      this.lab_TeflonCounter.Size = new System.Drawing.Size(93, 23);
      this.lab_TeflonCounter.TabIndex = 204;
      // 
      // WebBlow
      // 
      this.WebBlow.BackColor = System.Drawing.Color.White;
      this.WebBlow.Cursor = System.Windows.Forms.Cursors.Hand;
      this.WebBlow.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
      this.WebBlow.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.Text;
      this.WebBlow.Location = new System.Drawing.Point(626, 658);
      this.WebBlow.LowLevelEnabled = true;
      this.WebBlow.Name = "WebBlow";
      this.WebBlow.Size = new System.Drawing.Size(143, 37);
      this.WebBlow.TabIndex = 8;
      this.WebBlow.Text = "Web Brush";
      this.WebBlow.TimerInterval = 1000;
      this.WebBlow.TranslateText = true;
      this.WebBlow.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.OnIncDecButtonDown);
      // 
      // Frm_DebugTwin400Status2
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.label34);
      this.Controls.Add(this.lab_TeflonCounter);
      this.Controls.Add(this.btn_SimulatePLC);
      this.Controls.Add(this.lab_Pos29);
      this.Controls.Add(this.lab_Pos28);
      this.Controls.Add(this.lab_Pos27);
      this.Controls.Add(this.lab_Pos26);
      this.Controls.Add(this.lab_Pos25);
      this.Controls.Add(this.lab_Pos24);
      this.Controls.Add(this.lab_Pos23);
      this.Controls.Add(this.lab_Pos22);
      this.Controls.Add(this.lab_Pos21);
      this.Controls.Add(this.label51);
      this.Controls.Add(this.lab_Pos19);
      this.Controls.Add(this.lab_Pos18);
      this.Controls.Add(this.lab_Pos17);
      this.Controls.Add(this.lab_Pos16);
      this.Controls.Add(this.lab_Pos15);
      this.Controls.Add(this.lab_Pos14);
      this.Controls.Add(this.lab_Pos13);
      this.Controls.Add(this.lab_Pos12);
      this.Controls.Add(this.lab_Pos11);
      this.Controls.Add(this.label33);
      this.Controls.Add(this.alarmShow);
      this.Controls.Add(this.label32);
      this.Controls.Add(this.lab_GrindToolMeters);
      this.Controls.Add(this.label30);
      this.Controls.Add(this.lab_LubeDuration);
      this.Controls.Add(this.label55);
      this.Controls.Add(this.lab_GrindDuration);
      this.Controls.Add(this.label28);
      this.Controls.Add(this.lab_Speed);
      this.Controls.Add(this.label24);
      this.Controls.Add(this.label25);
      this.Controls.Add(this.label26);
      this.Controls.Add(this.label27);
      this.Controls.Add(this.lab_ShaftGap3);
      this.Controls.Add(this.label29);
      this.Controls.Add(this.lab_ShaftGap2);
      this.Controls.Add(this.label31);
      this.Controls.Add(this.lab_ShaftGap1);
      this.Controls.Add(this.label23);
      this.Controls.Add(this.label22);
      this.Controls.Add(this.lbl_UM12);
      this.Controls.Add(this.label20);
      this.Controls.Add(this.label18);
      this.Controls.Add(this.lbl_UM22);
      this.Controls.Add(this.label17);
      this.Controls.Add(this.lab_ScoreAngle3);
      this.Controls.Add(this.label19);
      this.Controls.Add(this.lab_ScoreAngle2);
      this.Controls.Add(this.label21);
      this.Controls.Add(this.lab_ScoreAngle1);
      this.Controls.Add(this.btn_SendSetup2);
      this.Controls.Add(this.btn_SendSetup1);
      this.Controls.Add(this.btn_SendSetup0);
      this.Controls.Add(this.label16);
      this.Controls.Add(this.lab_3levWebDirHigh);
      this.Controls.Add(this.label15);
      this.Controls.Add(this.lab_3levWebDirLow);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.lab_2levWebDirHigh);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.lab_2levWebDirLow);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lab_ChutesPosition);
      this.Controls.Add(this.label14);
      this.Controls.Add(this.lab_ScoreGap3);
      this.Controls.Add(this.label13);
      this.Controls.Add(this.lab_ScoreGap2);
      this.Controls.Add(this.label12);
      this.Controls.Add(this.lab_ScoreGap1);
      this.Controls.Add(this.label11);
      this.Controls.Add(this.lab_ID3);
      this.Controls.Add(this.label9);
      this.Controls.Add(this.lab_ID2);
      this.Controls.Add(this.label10);
      this.Controls.Add(this.lab_ID1);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.lab_TransmissionErrorCode);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.lab_TransmissionError);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.lab_LastMessageNumberCMD);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.lab_LastMessageNumber);
      this.Controls.Add(this.slitPositionsShow);
      this.Controls.Add(this.taskShow);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lab_NotifyArea);
      this.Controls.Add(this.WebBlow);
      this.Name = "Frm_DebugTwin400Status2";
      this.Text = "Frm_DebugTwin400Status2";
      this.Controls.SetChildIndex(this.WebBlow, 0);
      this.Controls.SetChildIndex(this.lab_path, 0);
      this.Controls.SetChildIndex(this.lab_Time, 0);
      this.Controls.SetChildIndex(this.lab_NotifyArea, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.taskShow, 0);
      this.Controls.SetChildIndex(this.slitPositionsShow, 0);
      this.Controls.SetChildIndex(this.lab_LastMessageNumber, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.lab_LastMessageNumberCMD, 0);
      this.Controls.SetChildIndex(this.label5, 0);
      this.Controls.SetChildIndex(this.lab_TransmissionError, 0);
      this.Controls.SetChildIndex(this.label7, 0);
      this.Controls.SetChildIndex(this.lab_TransmissionErrorCode, 0);
      this.Controls.SetChildIndex(this.label8, 0);
      this.Controls.SetChildIndex(this.lab_ID1, 0);
      this.Controls.SetChildIndex(this.label10, 0);
      this.Controls.SetChildIndex(this.lab_ID2, 0);
      this.Controls.SetChildIndex(this.label9, 0);
      this.Controls.SetChildIndex(this.lab_ID3, 0);
      this.Controls.SetChildIndex(this.label11, 0);
      this.Controls.SetChildIndex(this.lab_ScoreGap1, 0);
      this.Controls.SetChildIndex(this.label12, 0);
      this.Controls.SetChildIndex(this.lab_ScoreGap2, 0);
      this.Controls.SetChildIndex(this.label13, 0);
      this.Controls.SetChildIndex(this.lab_ScoreGap3, 0);
      this.Controls.SetChildIndex(this.label14, 0);
      this.Controls.SetChildIndex(this.lab_ChutesPosition, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.lab_2levWebDirLow, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.lab_2levWebDirHigh, 0);
      this.Controls.SetChildIndex(this.label6, 0);
      this.Controls.SetChildIndex(this.lab_3levWebDirLow, 0);
      this.Controls.SetChildIndex(this.label15, 0);
      this.Controls.SetChildIndex(this.lab_3levWebDirHigh, 0);
      this.Controls.SetChildIndex(this.label16, 0);
      this.Controls.SetChildIndex(this.btn_SendSetup0, 0);
      this.Controls.SetChildIndex(this.btn_SendSetup1, 0);
      this.Controls.SetChildIndex(this.btn_SendSetup2, 0);
      this.Controls.SetChildIndex(this.lab_ScoreAngle1, 0);
      this.Controls.SetChildIndex(this.label21, 0);
      this.Controls.SetChildIndex(this.lab_ScoreAngle2, 0);
      this.Controls.SetChildIndex(this.label19, 0);
      this.Controls.SetChildIndex(this.lab_ScoreAngle3, 0);
      this.Controls.SetChildIndex(this.label17, 0);
      this.Controls.SetChildIndex(this.lbl_UM22, 0);
      this.Controls.SetChildIndex(this.label18, 0);
      this.Controls.SetChildIndex(this.label20, 0);
      this.Controls.SetChildIndex(this.lbl_UM12, 0);
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
      this.Controls.SetChildIndex(this.label22, 0);
      this.Controls.SetChildIndex(this.label23, 0);
      this.Controls.SetChildIndex(this.lab_ShaftGap1, 0);
      this.Controls.SetChildIndex(this.label31, 0);
      this.Controls.SetChildIndex(this.lab_ShaftGap2, 0);
      this.Controls.SetChildIndex(this.label29, 0);
      this.Controls.SetChildIndex(this.lab_ShaftGap3, 0);
      this.Controls.SetChildIndex(this.label27, 0);
      this.Controls.SetChildIndex(this.label26, 0);
      this.Controls.SetChildIndex(this.label25, 0);
      this.Controls.SetChildIndex(this.label24, 0);
      this.Controls.SetChildIndex(this.lab_Speed, 0);
      this.Controls.SetChildIndex(this.label28, 0);
      this.Controls.SetChildIndex(this.lab_GrindDuration, 0);
      this.Controls.SetChildIndex(this.label55, 0);
      this.Controls.SetChildIndex(this.lab_LubeDuration, 0);
      this.Controls.SetChildIndex(this.label30, 0);
      this.Controls.SetChildIndex(this.lab_GrindToolMeters, 0);
      this.Controls.SetChildIndex(this.label32, 0);
      this.Controls.SetChildIndex(this.alarmShow, 0);
      this.Controls.SetChildIndex(this.label33, 0);
      this.Controls.SetChildIndex(this.lab_Pos11, 0);
      this.Controls.SetChildIndex(this.lab_Pos12, 0);
      this.Controls.SetChildIndex(this.lab_Pos13, 0);
      this.Controls.SetChildIndex(this.lab_Pos14, 0);
      this.Controls.SetChildIndex(this.lab_Pos15, 0);
      this.Controls.SetChildIndex(this.lab_Pos16, 0);
      this.Controls.SetChildIndex(this.lab_Pos17, 0);
      this.Controls.SetChildIndex(this.lab_Pos18, 0);
      this.Controls.SetChildIndex(this.lab_Pos19, 0);
      this.Controls.SetChildIndex(this.label51, 0);
      this.Controls.SetChildIndex(this.lab_Pos21, 0);
      this.Controls.SetChildIndex(this.lab_Pos22, 0);
      this.Controls.SetChildIndex(this.lab_Pos23, 0);
      this.Controls.SetChildIndex(this.lab_Pos24, 0);
      this.Controls.SetChildIndex(this.lab_Pos25, 0);
      this.Controls.SetChildIndex(this.lab_Pos26, 0);
      this.Controls.SetChildIndex(this.lab_Pos27, 0);
      this.Controls.SetChildIndex(this.lab_Pos28, 0);
      this.Controls.SetChildIndex(this.lab_Pos29, 0);
      this.Controls.SetChildIndex(this.btn_SimulatePLC, 0);
      this.Controls.SetChildIndex(this.lab_TeflonCounter, 0);
      this.Controls.SetChildIndex(this.label34, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private void btn_SendSetup_Click(object sender, EventArgs e)
    {
      string n=((Button)sender).Name;
      int pos = n.ToCharArray()[n.Length - 1] - '0';
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(Frm_DebugTwin400Status2)) + "." + Command.SETUP_DEBUG +  "("+pos.ToString()+")");
    }

    protected override void CtrlF1()
    {
      if (this.logAnalyzer.logAnalyzing)
      {
        this.logAnalyzer.CloseLogReader();
        this.btn_SendSetup0.Enabled = true;
        this.btn_SendSetup1.Enabled = true;
        this.btn_SendSetup2.Enabled = true;
      }
      else
      {
        this.logAnalyzer.OpenLogReader(new Twin400LogAnalyzer.ShowState(this.ShowPLCState));
        this.btn_SendSetup0.Enabled = false;
        this.btn_SendSetup1.Enabled = false;
        this.btn_SendSetup2.Enabled = false;
      }
    }

    protected override void DownArrow()
    {
      this.logAnalyzer.AheadLog();
    }

    protected override void UpArrow()
    {
      this.logAnalyzer.BackLog();
    }

    protected override void CtrlF2()
    {
      this.logAnalyzer.ChangeDateTime();
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

    private void btn_SimulatePLC_Click(object sender, EventArgs e)
    {
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(Frm_DebugTwin400Status2)) + "." + Command.SIMULATE_PLC + "()");
    }

    //agrassi_FI31-07-08
    private void OnIncDecButtonDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonDownEventArgs args)
    {
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.Twin400.Status.Frm_twin400_console)) + "." + ((Control)sender).Name + "." + Command.INCDEC_BUTTON_SIGNAL +
          "(" + args.Frequency + ")");
    }
  }
}
