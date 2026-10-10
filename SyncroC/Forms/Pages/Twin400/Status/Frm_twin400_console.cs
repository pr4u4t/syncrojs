using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Controls;
using SyncroC.DataBase;
using SyncroC.Controls.IncDecButton;
using SyncroC.Forms.PopUp;
using SyncroC.DataBase.LineConfig;

//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
//namespace SyncroC.Forms.Pages.Twin400.Production
//agrassi_FI25-08-08_2 (rinominati i panel degli assi)
namespace SyncroC.Forms.Pages.Twin400.Status
{
	//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
	//public class Frm_twin400_console : SyncroC.Forms.Pages.Twin400.Production.Frm_twin400_productionSubgroup
	public class Frm_twin400_console : SyncroC.Forms.Pages.Twin400.Status.Frm_twin400_statusSubgroup 
	{
		private SyncroC.Controls.StateButton JamupPhotoeye;
    private StateButton SendSetup0;
    private StateButton RecoverPositioning;
		private StateButton ManualAOC;
    private StateButton ForceReset;
		private StateButton ForceScan;
    private StateButton RemoteControl;
    private StateButton LiftAllShafts;
    private StateButton SendSetup1;
    private StateButton LoadTeflon1;
		private StateButton LoadTeflon0;
    private StateButton TeflonCycleStart;
		private System.ComponentModel.IContainer components = null;
    //AGrassi_FI06/12/2007_1
		private bool teflonCyclePossible;
		private Panel panel1;
		private StateButton TrimSlitterSxAx1;
		private StateButton TrimSlitterDxAx1;
		private StateButton TrimSlitterSxAx2;
    private string teflonReasonCode;

		private DBManager dbm;
		private StateButton WebFlap;
		private Label SlitterAx1;
		private Label SlitterAx2;
		private StateButton TrimSlitterDxAx2;
		private Panel Shaft2;
		private Panel Shaft1;
		private Label ModifyScoreGap1;
		private Label ModifyScoreGap3;
		private IncDecButtonKit ScoreGap2;
		private Label ModifyScoreGap2;
		private Label lbl_Chutes;
		private IncDecButtonKit ScoreGap1;
		private IncDecButtonKit ScoreGap3;
		private IncDecButton MoveMachineRight;
		private IncDecButton MoveMachineLeft;
		private StateButton ManualAutomaticMachineMove;
		private SyncroC.Controls.Photoeye.Photoeye InternalRightMachineMove;
		private SyncroC.Controls.Photoeye.Photoeye ExternalRightMachineMove;
		private SyncroC.Controls.Photoeye.Photoeye RightLimitMachineMove;
		private SyncroC.Controls.Photoeye.Photoeye ExternalLeftMachineMove;
		private SyncroC.Controls.Photoeye.Photoeye InternalLeftMachineMove;
		private SyncroC.Controls.Photoeye.Photoeye LeftLimitMachineMove;
		private IncDecButton OpenTrimChutes;
		private IncDecButton CloseTrimChutes;
		private TitledLabel ChutesSpan;
		private Panel Shaft4;
		private Panel Shaft3;
		private Panel Shaft5;
		private SyncroC.DataBase.LineConfig.SlitterScorer ss;

		public Frm_twin400_console()
		{
			InitializeComponent();

			dbm = DBManager.getDBManager();
			this.ss = dbm.lineConfig.SlitterScorerConfig;

      this.refreshData();
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
      {
        this.ScoreGap1.LowLevelEnabled = false;
        this.ScoreGap2.LowLevelEnabled = false;
        this.ScoreGap3.LowLevelEnabled = false;
        this.MoveMachineLeft.LowLevelEnabled = false;
        this.MoveMachineRight.LowLevelEnabled = false;
        this.CloseTrimChutes.LowLevelEnabled = false;
        this.OpenTrimChutes.LowLevelEnabled = false;
        this.ManualAutomaticMachineMove.IsButton = false;
        this.JamupPhotoeye.IsButton = false;
        this.RecoverPositioning.IsButton = false;
        this.ManualAOC.IsButton = false;
        this.ForceReset.IsButton = false;
        this.ForceScan.IsButton = false;
        this.LiftAllShafts.IsButton = false;
        this.LoadTeflon1.IsButton = false;
        this.LoadTeflon0.IsButton = false;
				this.TrimSlitterDxAx1.IsButton = false;
				this.TrimSlitterDxAx2.IsButton = false;
				this.TrimSlitterSxAx1.IsButton = false;
				this.TrimSlitterSxAx2.IsButton = false;
      }
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
      {
        this.SendSetup0.IsButton = false;
        this.SendSetup1.IsButton = false;
        this.RemoteControl.IsButton = false;
        this.TeflonCycleStart.IsButton = false;
      }
      //agrassi_FI11-12-08
			//RFAZZI_FI19/06/2008_1.
			//RFAZZI PROVA
			//if (ss.PLCInterfaceVersion < SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver5)
      //if (ss.Twin400.PLCInterfaceVersion < SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver5)
      //{
      //  this.TrimSlitterDxAx1.Visible = false;
      //  this.TrimSlitterDxAx2.Visible = false;
      //  this.TrimSlitterSxAx1.Visible = false;
      //  this.TrimSlitterSxAx2.Visible = false;
      //}

			//RFAZZI_FI06/08/2009_1. Fatto controllo per disabilitare il pulsante di EdgeGuide Auto/Man se non interno alla macchina. 
			if ( !this.ss.Twin400.PlcConfig.EdgeGuidePresent )
				this.ManualAutomaticMachineMove.IsButton = false;


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

		//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
		//protected override void F10()
		protected override void F11()
		{
		}

		//RFAZZI_FI01/08/2009_1. Aggiunta possibilità di invertire la pagina grafica in Verticale
		public override void MachineDirectionVisualizationInverted()
		{

			int upLimit = this.panel1.Location.Y;
			int lowerLimit = this.panel1.Location.Y + this.panel1.Height;

			int leftLimit = this.panel1.Location.X;
			int rightLimit = this.panel1.Location.X + this.panel1.Width;

			//Spostamento Macchina
			VerticalLocationMirror(this.MoveMachineLeft, upLimit, lowerLimit);
			HorizontalLocationMirror(this.MoveMachineLeft, leftLimit, rightLimit);
			this.MoveMachineLeft.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.RightArrow;
			VerticalLocationMirror(this.MoveMachineRight, upLimit, lowerLimit);
			HorizontalLocationMirror(this.MoveMachineRight, leftLimit, rightLimit);
			this.MoveMachineRight.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.LeftArrow;
			VerticalLocationMirror(this.ManualAutomaticMachineMove, upLimit, lowerLimit);
			VerticalLocationMirror(this.LeftLimitMachineMove, upLimit, lowerLimit);
			VerticalLocationMirror(this.InternalLeftMachineMove, upLimit, lowerLimit);
			VerticalLocationMirror(this.ExternalLeftMachineMove, upLimit, lowerLimit);
			VerticalLocationMirror(this.RightLimitMachineMove, upLimit, lowerLimit);
			VerticalLocationMirror(this.InternalRightMachineMove, upLimit, lowerLimit);
			VerticalLocationMirror(this.ExternalRightMachineMove, upLimit, lowerLimit);

			//Cordoni Aux
			VerticalLocationMirror(this.ModifyScoreGap3, upLimit, lowerLimit);
			VerticalLocationMirror(this.ScoreGap3, upLimit, lowerLimit);
			VerticalLocationMirror(this.Shaft5, upLimit, lowerLimit);

			//Cordoni AX1
			VerticalLocationMirror(this.ModifyScoreGap1, upLimit, lowerLimit);
			VerticalLocationMirror(this.ScoreGap1, upLimit, lowerLimit);
			VerticalLocationMirror(this.Shaft3, upLimit, lowerLimit);

			//Cordoni AX2
			VerticalLocationMirror(this.ModifyScoreGap2, upLimit, lowerLimit);
			VerticalLocationMirror(this.ScoreGap2, upLimit, lowerLimit);
			VerticalLocationMirror(this.Shaft4, upLimit, lowerLimit);

			//Coltelli AX1
			VerticalLocationMirror(this.SlitterAx1, upLimit, lowerLimit);
			VerticalLocationMirror(this.Shaft1, upLimit, lowerLimit);
			VerticalLocationMirror(this.TrimSlitterSxAx1, upLimit, lowerLimit);
			HorizontalLocationMirror(this.TrimSlitterSxAx1, leftLimit, rightLimit);
			VerticalLocationMirror(this.TrimSlitterDxAx1, upLimit, lowerLimit);
			HorizontalLocationMirror(this.TrimSlitterDxAx1, leftLimit, rightLimit);
			VerticalLocationMirror(this.LoadTeflon0, upLimit, lowerLimit);

			//Coltelli AX2
			VerticalLocationMirror(this.SlitterAx2, upLimit, lowerLimit);
			VerticalLocationMirror(this.Shaft2, upLimit, lowerLimit);
			VerticalLocationMirror(this.TrimSlitterSxAx2, upLimit, lowerLimit);
			HorizontalLocationMirror(this.TrimSlitterSxAx2, leftLimit, rightLimit);
			VerticalLocationMirror(this.TrimSlitterDxAx2, upLimit, lowerLimit);
			HorizontalLocationMirror(this.TrimSlitterDxAx2, leftLimit, rightLimit);
			VerticalLocationMirror(this.LoadTeflon1, upLimit, lowerLimit);

			//Trim Chutes
			VerticalLocationMirror(this.lbl_Chutes, upLimit, lowerLimit);
			VerticalLocationMirror(this.CloseTrimChutes, upLimit, lowerLimit);
			VerticalLocationMirror(this.OpenTrimChutes, upLimit, lowerLimit);
			VerticalLocationMirror(this.ChutesSpan, upLimit, lowerLimit);

			//
			VerticalLocationMirror(this.ForceScan, upLimit, lowerLimit);
			VerticalLocationMirror(this.TeflonCycleStart, upLimit, lowerLimit);
		}

    //PMASSARO_FI11/03/09 - ShutDown Commando From Twin400 Page
    protected override void CtrlF1()
    {
      if (!DBManager.getDBManager().syncroConfig.IsTwin400Only())
        return;

      bool isOnServer = SyncroC.Utility.Settings.getSettings().configuration.getOnServer();

      Frm_Confirm conf = new Frm_Confirm();
      string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_196#");

      if (conf.ShowDialog(this, text).Equals(DialogResult.OK))
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();

        if (isOnServer)
        {
          cm.SendUdp(new UDPMessage(cm.STARTER_DEBUG_REQ, Command.SHUTDOWN_AND_OFF_PC_BY_OPERATOR));
        }
        else
        {
          PageManager.getPageManager()._SetMessage("§SYCC_00");
          if (cm.CanSendTCP())
          {
            //ask the server to shout down the client
            cm.SendTcp(cm.SYNCROS_COMM_MANAGER, Command.COMMUNICATION_MANAGER + "." + Command.SHUTDOWN + "()");
          }
          else
          {
            Logger.LogError(this, "TCP connection down: Client shuts down by itself.");
            //probably the server is already down... so just shout down.
          }

          System.Threading.Thread.Sleep(5000);

          Logger.LogError(this, "Client ShutDown Requested By Operator.");
          Utility.Shoutdown.ShoutdownWindows(false);
        }
      }
    }
    
		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_console));
      this.JamupPhotoeye = new SyncroC.Controls.StateButton();
      this.SendSetup0 = new SyncroC.Controls.StateButton();
      this.RecoverPositioning = new SyncroC.Controls.StateButton();
      this.ManualAOC = new SyncroC.Controls.StateButton();
      this.ForceReset = new SyncroC.Controls.StateButton();
      this.ForceScan = new SyncroC.Controls.StateButton();
      this.RemoteControl = new SyncroC.Controls.StateButton();
      this.LiftAllShafts = new SyncroC.Controls.StateButton();
      this.SendSetup1 = new SyncroC.Controls.StateButton();
      this.LoadTeflon1 = new SyncroC.Controls.StateButton();
      this.LoadTeflon0 = new SyncroC.Controls.StateButton();
      this.TeflonCycleStart = new SyncroC.Controls.StateButton();
      this.panel1 = new System.Windows.Forms.Panel();
      this.TrimSlitterSxAx1 = new SyncroC.Controls.StateButton();
      this.TrimSlitterDxAx1 = new SyncroC.Controls.StateButton();
      this.TrimSlitterSxAx2 = new SyncroC.Controls.StateButton();
      this.WebFlap = new SyncroC.Controls.StateButton();
      this.SlitterAx1 = new System.Windows.Forms.Label();
      this.SlitterAx2 = new System.Windows.Forms.Label();
      this.TrimSlitterDxAx2 = new SyncroC.Controls.StateButton();
      this.Shaft2 = new System.Windows.Forms.Panel();
      this.Shaft1 = new System.Windows.Forms.Panel();
      this.ModifyScoreGap1 = new System.Windows.Forms.Label();
      this.ModifyScoreGap3 = new System.Windows.Forms.Label();
      this.ScoreGap2 = new SyncroC.Controls.IncDecButton.IncDecButtonKit();
      this.ModifyScoreGap2 = new System.Windows.Forms.Label();
      this.lbl_Chutes = new System.Windows.Forms.Label();
      this.ScoreGap1 = new SyncroC.Controls.IncDecButton.IncDecButtonKit();
      this.ScoreGap3 = new SyncroC.Controls.IncDecButton.IncDecButtonKit();
      this.MoveMachineRight = new SyncroC.Controls.IncDecButton.IncDecButton();
      this.MoveMachineLeft = new SyncroC.Controls.IncDecButton.IncDecButton();
      this.ManualAutomaticMachineMove = new SyncroC.Controls.StateButton();
      this.InternalRightMachineMove = new SyncroC.Controls.Photoeye.Photoeye();
      this.ExternalRightMachineMove = new SyncroC.Controls.Photoeye.Photoeye();
      this.RightLimitMachineMove = new SyncroC.Controls.Photoeye.Photoeye();
      this.ExternalLeftMachineMove = new SyncroC.Controls.Photoeye.Photoeye();
      this.InternalLeftMachineMove = new SyncroC.Controls.Photoeye.Photoeye();
      this.LeftLimitMachineMove = new SyncroC.Controls.Photoeye.Photoeye();
      this.OpenTrimChutes = new SyncroC.Controls.IncDecButton.IncDecButton();
      this.CloseTrimChutes = new SyncroC.Controls.IncDecButton.IncDecButton();
      this.ChutesSpan = new SyncroC.Controls.TitledLabel();
      this.Shaft4 = new System.Windows.Forms.Panel();
      this.Shaft3 = new System.Windows.Forms.Panel();
      this.Shaft5 = new System.Windows.Forms.Panel();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      this.lbl_Title_YN.Text = "TWIN 400 CONSOLE";
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
      this.lbl_CtrlF1_YY.Text = "Shutdown PC";
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
      // JamupPhotoeye
      // 
      this.JamupPhotoeye.Active = false;
      this.JamupPhotoeye.Alarmed = false;
      this.JamupPhotoeye.Auto = false;
      this.JamupPhotoeye.BackColor = System.Drawing.Color.WhiteSmoke;
      this.JamupPhotoeye.Cursor = System.Windows.Forms.Cursors.Hand;
      this.JamupPhotoeye.FillBackColorWithImage = false;
      this.JamupPhotoeye.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.JamupPhotoeye.ForeColor = System.Drawing.Color.Black;
      this.JamupPhotoeye.FramePresent = true;
      this.JamupPhotoeye.HighLightEnable = false;
      this.JamupPhotoeye.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.JamupPhotoeye.ImagePassword = null;
      this.JamupPhotoeye.Inemergency = false;
      this.JamupPhotoeye.InLocal = false;
      this.JamupPhotoeye.IsButton = true;
      this.JamupPhotoeye.IsForceToInvisible = false;
      this.JamupPhotoeye.Location = new System.Drawing.Point(30, 301);
      this.JamupPhotoeye.Name = "JamupPhotoeye";
      this.JamupPhotoeye.OnLine = false;
      this.JamupPhotoeye.ShowImage = false;
      this.JamupPhotoeye.ShowText = true;
      this.JamupPhotoeye.Size = new System.Drawing.Size(120, 90);
      this.JamupPhotoeye.Switching = false;
      this.JamupPhotoeye.TabIndex = 6;
      this.JamupPhotoeye.Text = "Enable Jamup Photoeye";
      this.JamupPhotoeye.TranslateText = false;
      this.JamupPhotoeye.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("JamupPhotoeye.UserDefinedActiveImage")));
      this.JamupPhotoeye.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("JamupPhotoeye.UserDefinedImage")));
      this.JamupPhotoeye.Warned = false;
      this.JamupPhotoeye.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // SendSetup0
      // 
      this.SendSetup0.Active = false;
      this.SendSetup0.Alarmed = false;
      this.SendSetup0.Auto = false;
      this.SendSetup0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendSetup0.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendSetup0.FillBackColorWithImage = true;
      this.SendSetup0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendSetup0.ForeColor = System.Drawing.Color.Black;
      this.SendSetup0.FramePresent = true;
      this.SendSetup0.HighLightEnable = false;
      this.SendSetup0.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendSetup0.ImagePassword = null;
      this.SendSetup0.Inemergency = false;
      this.SendSetup0.InLocal = false;
      this.SendSetup0.IsButton = true;
      this.SendSetup0.IsForceToInvisible = false;
      this.SendSetup0.Location = new System.Drawing.Point(1081, 623);
      this.SendSetup0.Name = "SendSetup0";
      this.SendSetup0.OnLine = false;
      this.SendSetup0.ShowImage = true;
      this.SendSetup0.ShowText = true;
      this.SendSetup0.Size = new System.Drawing.Size(120, 90);
      this.SendSetup0.Switching = false;
      this.SendSetup0.TabIndex = 3;
      this.SendSetup0.Text = "1";
      this.SendSetup0.TranslateText = false;
      this.SendSetup0.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendSetup0.UserDefinedActiveImage")));
      this.SendSetup0.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendSetup0.UserDefinedImage")));
      this.SendSetup0.Warned = false;
      this.SendSetup0.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // RecoverPositioning
      // 
      this.RecoverPositioning.Active = false;
      this.RecoverPositioning.Alarmed = false;
      this.RecoverPositioning.Auto = false;
      this.RecoverPositioning.BackColor = System.Drawing.Color.WhiteSmoke;
      this.RecoverPositioning.Cursor = System.Windows.Forms.Cursors.Hand;
      this.RecoverPositioning.FillBackColorWithImage = false;
      this.RecoverPositioning.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RecoverPositioning.ForeColor = System.Drawing.Color.Black;
      this.RecoverPositioning.FramePresent = true;
      this.RecoverPositioning.HighLightEnable = true;
      this.RecoverPositioning.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.RecoverPositioning.ImagePassword = null;
      this.RecoverPositioning.Inemergency = false;
      this.RecoverPositioning.InLocal = false;
      this.RecoverPositioning.IsButton = true;
      this.RecoverPositioning.IsForceToInvisible = false;
      this.RecoverPositioning.Location = new System.Drawing.Point(1081, 410);
      this.RecoverPositioning.Name = "RecoverPositioning";
      this.RecoverPositioning.OnLine = false;
      this.RecoverPositioning.ShowImage = false;
      this.RecoverPositioning.ShowText = true;
      this.RecoverPositioning.Size = new System.Drawing.Size(120, 90);
      this.RecoverPositioning.Switching = false;
      this.RecoverPositioning.TabIndex = 1;
      this.RecoverPositioning.Text = "Recover Positioning";
      this.RecoverPositioning.TranslateText = true;
      this.RecoverPositioning.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("RecoverPositioning.UserDefinedActiveImage")));
      this.RecoverPositioning.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("RecoverPositioning.UserDefinedImage")));
      this.RecoverPositioning.Warned = false;
      this.RecoverPositioning.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // ManualAOC
      // 
      this.ManualAOC.Active = false;
      this.ManualAOC.Alarmed = false;
      this.ManualAOC.Auto = false;
      this.ManualAOC.BackColor = System.Drawing.Color.LightGray;
      this.ManualAOC.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ManualAOC.FillBackColorWithImage = false;
      this.ManualAOC.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ManualAOC.ForeColor = System.Drawing.Color.Black;
      this.ManualAOC.FramePresent = true;
      this.ManualAOC.HighLightEnable = false;
      this.ManualAOC.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.ManualAOC.ImagePassword = null;
      this.ManualAOC.Inemergency = false;
      this.ManualAOC.InLocal = false;
      this.ManualAOC.IsButton = true;
      this.ManualAOC.IsForceToInvisible = false;
      this.ManualAOC.Location = new System.Drawing.Point(948, 193);
      this.ManualAOC.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.ManualAOC.Name = "ManualAOC";
      this.ManualAOC.OnLine = false;
      this.ManualAOC.ShowImage = false;
      this.ManualAOC.ShowText = true;
      this.ManualAOC.Size = new System.Drawing.Size(120, 90);
      this.ManualAOC.Switching = false;
      this.ManualAOC.TabIndex = 0;
      this.ManualAOC.Text = "Manual Change";
      this.ManualAOC.TranslateText = true;
      this.ManualAOC.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ManualAOC.UserDefinedActiveImage")));
      this.ManualAOC.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ManualAOC.UserDefinedImage")));
      this.ManualAOC.Warned = false;
      this.ManualAOC.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // ForceReset
      // 
      this.ForceReset.Active = false;
      this.ForceReset.Alarmed = false;
      this.ForceReset.Auto = false;
      this.ForceReset.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ForceReset.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ForceReset.FillBackColorWithImage = false;
      this.ForceReset.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ForceReset.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ForceReset.FramePresent = true;
      this.ForceReset.HighLightEnable = false;
      this.ForceReset.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.ForceReset.ImagePassword = null;
      this.ForceReset.Inemergency = false;
      this.ForceReset.InLocal = false;
      this.ForceReset.IsButton = true;
      this.ForceReset.IsForceToInvisible = false;
      this.ForceReset.Location = new System.Drawing.Point(30, 622);
      this.ForceReset.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.ForceReset.Name = "ForceReset";
      this.ForceReset.OnLine = false;
      this.ForceReset.ShowImage = false;
      this.ForceReset.ShowText = true;
      this.ForceReset.Size = new System.Drawing.Size(120, 90);
      this.ForceReset.Switching = false;
      this.ForceReset.TabIndex = 2;
      this.ForceReset.Text = "Reset";
      this.ForceReset.TranslateText = true;
      this.ForceReset.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ForceReset.UserDefinedActiveImage")));
      this.ForceReset.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ForceReset.UserDefinedImage")));
      this.ForceReset.Warned = false;
      this.ForceReset.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // ForceScan
      // 
      this.ForceScan.Active = false;
      this.ForceScan.Alarmed = false;
      this.ForceScan.Auto = false;
      this.ForceScan.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ForceScan.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ForceScan.FillBackColorWithImage = false;
      this.ForceScan.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ForceScan.ForeColor = System.Drawing.Color.Black;
      this.ForceScan.FramePresent = true;
      this.ForceScan.HighLightEnable = false;
      this.ForceScan.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.ForceScan.ImagePassword = null;
      this.ForceScan.Inemergency = false;
      this.ForceScan.InLocal = false;
      this.ForceScan.IsButton = true;
      this.ForceScan.IsForceToInvisible = false;
      this.ForceScan.Location = new System.Drawing.Point(163, 301);
      this.ForceScan.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.ForceScan.Name = "ForceScan";
      this.ForceScan.OnLine = false;
      this.ForceScan.ShowImage = false;
      this.ForceScan.ShowText = true;
      this.ForceScan.Size = new System.Drawing.Size(120, 90);
      this.ForceScan.Switching = false;
      this.ForceScan.TabIndex = 4;
      this.ForceScan.Text = "Slit Scan";
      this.ForceScan.TranslateText = true;
      this.ForceScan.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ForceScan.UserDefinedActiveImage")));
      this.ForceScan.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ForceScan.UserDefinedImage")));
      this.ForceScan.Warned = false;
      this.ForceScan.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // RemoteControl
      // 
      this.RemoteControl.Active = false;
      this.RemoteControl.Alarmed = false;
      this.RemoteControl.Auto = false;
      this.RemoteControl.BackColor = System.Drawing.Color.LightGray;
      this.RemoteControl.Cursor = System.Windows.Forms.Cursors.Hand;
      this.RemoteControl.FillBackColorWithImage = false;
      this.RemoteControl.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RemoteControl.ForeColor = System.Drawing.Color.Black;
      this.RemoteControl.FramePresent = true;
      this.RemoteControl.HighLightEnable = false;
      this.RemoteControl.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.RemoteControl.ImagePassword = null;
      this.RemoteControl.Inemergency = false;
      this.RemoteControl.InLocal = false;
      this.RemoteControl.IsButton = true;
      this.RemoteControl.IsForceToInvisible = false;
      this.RemoteControl.Location = new System.Drawing.Point(1081, 193);
      this.RemoteControl.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.RemoteControl.Name = "RemoteControl";
      this.RemoteControl.OnLine = false;
      this.RemoteControl.ShowImage = false;
      this.RemoteControl.ShowText = true;
      this.RemoteControl.Size = new System.Drawing.Size(120, 90);
      this.RemoteControl.Switching = false;
      this.RemoteControl.TabIndex = 193;
      this.RemoteControl.Text = "Remote Control";
      this.RemoteControl.TranslateText = false;
      this.RemoteControl.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("RemoteControl.UserDefinedActiveImage")));
      this.RemoteControl.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("RemoteControl.UserDefinedImage")));
      this.RemoteControl.Warned = false;
      this.RemoteControl.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // LiftAllShafts
      // 
      this.LiftAllShafts.Active = false;
      this.LiftAllShafts.Alarmed = false;
      this.LiftAllShafts.Auto = false;
      this.LiftAllShafts.BackColor = System.Drawing.Color.WhiteSmoke;
      this.LiftAllShafts.Cursor = System.Windows.Forms.Cursors.Hand;
      this.LiftAllShafts.FillBackColorWithImage = false;
      this.LiftAllShafts.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.LiftAllShafts.ForeColor = System.Drawing.Color.Black;
      this.LiftAllShafts.FramePresent = true;
      this.LiftAllShafts.HighLightEnable = false;
      this.LiftAllShafts.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.LiftAllShafts.ImagePassword = null;
      this.LiftAllShafts.Inemergency = false;
      this.LiftAllShafts.InLocal = false;
      this.LiftAllShafts.IsButton = true;
      this.LiftAllShafts.IsForceToInvisible = false;
      this.LiftAllShafts.Location = new System.Drawing.Point(163, 622);
      this.LiftAllShafts.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.LiftAllShafts.Name = "LiftAllShafts";
      this.LiftAllShafts.OnLine = false;
      this.LiftAllShafts.ShowImage = false;
      this.LiftAllShafts.ShowText = true;
      this.LiftAllShafts.Size = new System.Drawing.Size(120, 90);
      this.LiftAllShafts.Switching = false;
      this.LiftAllShafts.TabIndex = 194;
      this.LiftAllShafts.Text = "Lift Up All Shafts";
      this.LiftAllShafts.TranslateText = false;
      this.LiftAllShafts.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("LiftAllShafts.UserDefinedActiveImage")));
      this.LiftAllShafts.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("LiftAllShafts.UserDefinedImage")));
      this.LiftAllShafts.Warned = false;
      this.LiftAllShafts.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // SendSetup1
      // 
      this.SendSetup1.Active = false;
      this.SendSetup1.Alarmed = false;
      this.SendSetup1.Auto = false;
      this.SendSetup1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendSetup1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendSetup1.FillBackColorWithImage = true;
      this.SendSetup1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendSetup1.ForeColor = System.Drawing.Color.Black;
      this.SendSetup1.FramePresent = true;
      this.SendSetup1.HighLightEnable = false;
      this.SendSetup1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendSetup1.ImagePassword = null;
      this.SendSetup1.Inemergency = false;
      this.SendSetup1.InLocal = false;
      this.SendSetup1.IsButton = true;
      this.SendSetup1.IsForceToInvisible = false;
      this.SendSetup1.Location = new System.Drawing.Point(1081, 515);
      this.SendSetup1.Name = "SendSetup1";
      this.SendSetup1.OnLine = false;
      this.SendSetup1.ShowImage = true;
      this.SendSetup1.ShowText = true;
      this.SendSetup1.Size = new System.Drawing.Size(120, 90);
      this.SendSetup1.Switching = false;
      this.SendSetup1.TabIndex = 196;
      this.SendSetup1.Text = "2";
      this.SendSetup1.TranslateText = false;
      this.SendSetup1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendSetup1.UserDefinedActiveImage")));
      this.SendSetup1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendSetup1.UserDefinedImage")));
      this.SendSetup1.Warned = false;
      this.SendSetup1.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // LoadTeflon1
      // 
      this.LoadTeflon1.Active = false;
      this.LoadTeflon1.Alarmed = false;
      this.LoadTeflon1.Auto = false;
      this.LoadTeflon1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.LoadTeflon1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.LoadTeflon1.FillBackColorWithImage = true;
      this.LoadTeflon1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.LoadTeflon1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.LoadTeflon1.FramePresent = true;
      this.LoadTeflon1.HighLightEnable = false;
      this.LoadTeflon1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.LoadTeflon1.ImagePassword = null;
      this.LoadTeflon1.Inemergency = false;
      this.LoadTeflon1.InLocal = false;
      this.LoadTeflon1.IsButton = true;
      this.LoadTeflon1.IsForceToInvisible = false;
      this.LoadTeflon1.Location = new System.Drawing.Point(563, 289);
      this.LoadTeflon1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.LoadTeflon1.Name = "LoadTeflon1";
      this.LoadTeflon1.OnLine = false;
      this.LoadTeflon1.ShowImage = true;
      this.LoadTeflon1.ShowText = false;
      this.LoadTeflon1.Size = new System.Drawing.Size(93, 55);
      this.LoadTeflon1.Switching = false;
      this.LoadTeflon1.TabIndex = 203;
      this.LoadTeflon1.Text = "Load Teflon";
      this.LoadTeflon1.TranslateText = true;
      this.LoadTeflon1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("LoadTeflon1.UserDefinedActiveImage")));
      this.LoadTeflon1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("LoadTeflon1.UserDefinedImage")));
      this.LoadTeflon1.Warned = false;
      this.LoadTeflon1.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // LoadTeflon0
      // 
      this.LoadTeflon0.Active = false;
      this.LoadTeflon0.Alarmed = false;
      this.LoadTeflon0.Auto = false;
      this.LoadTeflon0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.LoadTeflon0.Cursor = System.Windows.Forms.Cursors.Hand;
      this.LoadTeflon0.FillBackColorWithImage = true;
      this.LoadTeflon0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.LoadTeflon0.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.LoadTeflon0.FramePresent = true;
      this.LoadTeflon0.HighLightEnable = false;
      this.LoadTeflon0.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.LoadTeflon0.ImagePassword = null;
      this.LoadTeflon0.Inemergency = false;
      this.LoadTeflon0.InLocal = false;
      this.LoadTeflon0.IsButton = true;
      this.LoadTeflon0.IsForceToInvisible = false;
      this.LoadTeflon0.Location = new System.Drawing.Point(563, 361);
      this.LoadTeflon0.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.LoadTeflon0.Name = "LoadTeflon0";
      this.LoadTeflon0.OnLine = false;
      this.LoadTeflon0.ShowImage = true;
      this.LoadTeflon0.ShowText = false;
      this.LoadTeflon0.Size = new System.Drawing.Size(93, 55);
      this.LoadTeflon0.Switching = false;
      this.LoadTeflon0.TabIndex = 204;
      this.LoadTeflon0.Text = "Teflon";
      this.LoadTeflon0.TranslateText = true;
      this.LoadTeflon0.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("LoadTeflon0.UserDefinedActiveImage")));
      this.LoadTeflon0.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("LoadTeflon0.UserDefinedImage")));
      this.LoadTeflon0.Warned = false;
      this.LoadTeflon0.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // TeflonCycleStart
      // 
      this.TeflonCycleStart.Active = false;
      this.TeflonCycleStart.Alarmed = false;
      this.TeflonCycleStart.Auto = false;
      this.TeflonCycleStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.TeflonCycleStart.Cursor = System.Windows.Forms.Cursors.Hand;
      this.TeflonCycleStart.FillBackColorWithImage = true;
      this.TeflonCycleStart.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TeflonCycleStart.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.TeflonCycleStart.FramePresent = true;
      this.TeflonCycleStart.HighLightEnable = false;
      this.TeflonCycleStart.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.TeflonCycleStart.ImagePassword = null;
      this.TeflonCycleStart.Inemergency = false;
      this.TeflonCycleStart.InLocal = false;
      this.TeflonCycleStart.IsButton = true;
      this.TeflonCycleStart.IsForceToInvisible = false;
      this.TeflonCycleStart.Location = new System.Drawing.Point(948, 305);
      this.TeflonCycleStart.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
      this.TeflonCycleStart.Name = "TeflonCycleStart";
      this.TeflonCycleStart.OnLine = false;
      this.TeflonCycleStart.ShowImage = true;
      this.TeflonCycleStart.ShowText = true;
      this.TeflonCycleStart.Size = new System.Drawing.Size(120, 90);
      this.TeflonCycleStart.Switching = false;
      this.TeflonCycleStart.TabIndex = 209;
      this.TeflonCycleStart.Text = "Cycle Start";
      this.TeflonCycleStart.TranslateText = true;
      this.TeflonCycleStart.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("TeflonCycleStart.UserDefinedActiveImage")));
      this.TeflonCycleStart.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("TeflonCycleStart.UserDefinedImage")));
      this.TeflonCycleStart.Warned = false;
      this.TeflonCycleStart.Click += new System.EventHandler(this.TeflonCycleStart_Click);
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.BurlyWood;
      this.panel1.Location = new System.Drawing.Point(333, 193);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(554, 520);
      this.panel1.TabIndex = 215;
      // 
      // TrimSlitterSxAx1
      // 
      this.TrimSlitterSxAx1.Active = false;
      this.TrimSlitterSxAx1.Alarmed = false;
      this.TrimSlitterSxAx1.Auto = false;
      this.TrimSlitterSxAx1.AutoSize = true;
      this.TrimSlitterSxAx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.TrimSlitterSxAx1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.TrimSlitterSxAx1.FillBackColorWithImage = true;
      this.TrimSlitterSxAx1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TrimSlitterSxAx1.ForeColor = System.Drawing.Color.Black;
      this.TrimSlitterSxAx1.FramePresent = true;
      this.TrimSlitterSxAx1.HighLightEnable = false;
      this.TrimSlitterSxAx1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.TrimSlitterSxAx1.ImagePassword = null;
      this.TrimSlitterSxAx1.Inemergency = false;
      this.TrimSlitterSxAx1.InLocal = false;
      this.TrimSlitterSxAx1.IsButton = true;
      this.TrimSlitterSxAx1.IsForceToInvisible = false;
      this.TrimSlitterSxAx1.Location = new System.Drawing.Point(471, 361);
      this.TrimSlitterSxAx1.Name = "TrimSlitterSxAx1";
      this.TrimSlitterSxAx1.OnLine = false;
      this.TrimSlitterSxAx1.ShowImage = true;
      this.TrimSlitterSxAx1.ShowText = false;
      this.TrimSlitterSxAx1.Size = new System.Drawing.Size(47, 55);
      this.TrimSlitterSxAx1.Switching = false;
      this.TrimSlitterSxAx1.TabIndex = 217;
      this.TrimSlitterSxAx1.TranslateText = false;
      this.TrimSlitterSxAx1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("TrimSlitterSxAx1.UserDefinedActiveImage")));
      this.TrimSlitterSxAx1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("TrimSlitterSxAx1.UserDefinedImage")));
      this.TrimSlitterSxAx1.Warned = false;
      this.TrimSlitterSxAx1.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // TrimSlitterDxAx1
      // 
      this.TrimSlitterDxAx1.Active = false;
      this.TrimSlitterDxAx1.Alarmed = false;
      this.TrimSlitterDxAx1.Auto = false;
      this.TrimSlitterDxAx1.AutoSize = true;
      this.TrimSlitterDxAx1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.TrimSlitterDxAx1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.TrimSlitterDxAx1.FillBackColorWithImage = true;
      this.TrimSlitterDxAx1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TrimSlitterDxAx1.ForeColor = System.Drawing.Color.Black;
      this.TrimSlitterDxAx1.FramePresent = true;
      this.TrimSlitterDxAx1.HighLightEnable = false;
      this.TrimSlitterDxAx1.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.TrimSlitterDxAx1.ImagePassword = null;
      this.TrimSlitterDxAx1.Inemergency = false;
      this.TrimSlitterDxAx1.InLocal = false;
      this.TrimSlitterDxAx1.IsButton = true;
      this.TrimSlitterDxAx1.IsForceToInvisible = false;
      this.TrimSlitterDxAx1.Location = new System.Drawing.Point(702, 361);
      this.TrimSlitterDxAx1.Name = "TrimSlitterDxAx1";
      this.TrimSlitterDxAx1.OnLine = false;
      this.TrimSlitterDxAx1.ShowImage = true;
      this.TrimSlitterDxAx1.ShowText = false;
      this.TrimSlitterDxAx1.Size = new System.Drawing.Size(47, 55);
      this.TrimSlitterDxAx1.Switching = false;
      this.TrimSlitterDxAx1.TabIndex = 218;
      this.TrimSlitterDxAx1.TranslateText = false;
      this.TrimSlitterDxAx1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("TrimSlitterDxAx1.UserDefinedActiveImage")));
      this.TrimSlitterDxAx1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("TrimSlitterDxAx1.UserDefinedImage")));
      this.TrimSlitterDxAx1.Warned = false;
      this.TrimSlitterDxAx1.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // TrimSlitterSxAx2
      // 
      this.TrimSlitterSxAx2.Active = false;
      this.TrimSlitterSxAx2.Alarmed = false;
      this.TrimSlitterSxAx2.Auto = false;
      this.TrimSlitterSxAx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.TrimSlitterSxAx2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.TrimSlitterSxAx2.FillBackColorWithImage = true;
      this.TrimSlitterSxAx2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TrimSlitterSxAx2.ForeColor = System.Drawing.Color.Black;
      this.TrimSlitterSxAx2.FramePresent = true;
      this.TrimSlitterSxAx2.HighLightEnable = false;
      this.TrimSlitterSxAx2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.TrimSlitterSxAx2.ImagePassword = null;
      this.TrimSlitterSxAx2.Inemergency = false;
      this.TrimSlitterSxAx2.InLocal = false;
      this.TrimSlitterSxAx2.IsButton = true;
      this.TrimSlitterSxAx2.IsForceToInvisible = false;
      this.TrimSlitterSxAx2.Location = new System.Drawing.Point(471, 288);
      this.TrimSlitterSxAx2.Name = "TrimSlitterSxAx2";
      this.TrimSlitterSxAx2.OnLine = false;
      this.TrimSlitterSxAx2.ShowImage = true;
      this.TrimSlitterSxAx2.ShowText = false;
      this.TrimSlitterSxAx2.Size = new System.Drawing.Size(47, 55);
      this.TrimSlitterSxAx2.Switching = false;
      this.TrimSlitterSxAx2.TabIndex = 219;
      this.TrimSlitterSxAx2.TranslateText = false;
      this.TrimSlitterSxAx2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("TrimSlitterSxAx2.UserDefinedActiveImage")));
      this.TrimSlitterSxAx2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("TrimSlitterSxAx2.UserDefinedImage")));
      this.TrimSlitterSxAx2.Warned = false;
      this.TrimSlitterSxAx2.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // WebFlap
      // 
      this.WebFlap.Active = false;
      this.WebFlap.Alarmed = false;
      this.WebFlap.Auto = false;
      this.WebFlap.BackColor = System.Drawing.Color.WhiteSmoke;
      this.WebFlap.Cursor = System.Windows.Forms.Cursors.Hand;
      this.WebFlap.FillBackColorWithImage = false;
      this.WebFlap.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.WebFlap.ForeColor = System.Drawing.Color.Black;
      this.WebFlap.FramePresent = true;
      this.WebFlap.HighLightEnable = false;
      this.WebFlap.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.WebFlap.ImagePassword = null;
      this.WebFlap.Inemergency = false;
      this.WebFlap.InLocal = false;
      this.WebFlap.IsButton = true;
      this.WebFlap.IsForceToInvisible = false;
      this.WebFlap.Location = new System.Drawing.Point(30, 193);
      this.WebFlap.Name = "WebFlap";
      this.WebFlap.OnLine = false;
      this.WebFlap.ShowImage = false;
      this.WebFlap.ShowText = true;
      this.WebFlap.Size = new System.Drawing.Size(120, 90);
      this.WebFlap.Switching = false;
      this.WebFlap.TabIndex = 220;
      this.WebFlap.Text = "Pull up Web Flap";
      this.WebFlap.TranslateText = false;
      this.WebFlap.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("WebFlap.UserDefinedActiveImage")));
      this.WebFlap.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("WebFlap.UserDefinedImage")));
      this.WebFlap.Warned = false;
      this.WebFlap.Click += new System.EventHandler(this.OnStateButtonPushDuration);
      // 
      // SlitterAx1
      // 
      this.SlitterAx1.Image = global::SyncroC.Properties.Resources.Slitter;
      this.SlitterAx1.Location = new System.Drawing.Point(375, 362);
      this.SlitterAx1.Name = "SlitterAx1";
      this.SlitterAx1.Size = new System.Drawing.Size(42, 51);
      this.SlitterAx1.TabIndex = 256;
      // 
      // SlitterAx2
      // 
      this.SlitterAx2.Image = global::SyncroC.Properties.Resources.Slitter;
      this.SlitterAx2.Location = new System.Drawing.Point(375, 291);
      this.SlitterAx2.Name = "SlitterAx2";
      this.SlitterAx2.Size = new System.Drawing.Size(42, 51);
      this.SlitterAx2.TabIndex = 255;
      // 
      // TrimSlitterDxAx2
      // 
      this.TrimSlitterDxAx2.Active = false;
      this.TrimSlitterDxAx2.Alarmed = false;
      this.TrimSlitterDxAx2.Auto = false;
      this.TrimSlitterDxAx2.AutoSize = true;
      this.TrimSlitterDxAx2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.TrimSlitterDxAx2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.TrimSlitterDxAx2.FillBackColorWithImage = true;
      this.TrimSlitterDxAx2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TrimSlitterDxAx2.ForeColor = System.Drawing.Color.Black;
      this.TrimSlitterDxAx2.FramePresent = true;
      this.TrimSlitterDxAx2.HighLightEnable = false;
      this.TrimSlitterDxAx2.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.TrimSlitterDxAx2.ImagePassword = null;
      this.TrimSlitterDxAx2.Inemergency = false;
      this.TrimSlitterDxAx2.InLocal = false;
      this.TrimSlitterDxAx2.IsButton = true;
      this.TrimSlitterDxAx2.IsForceToInvisible = false;
      this.TrimSlitterDxAx2.Location = new System.Drawing.Point(703, 288);
      this.TrimSlitterDxAx2.Name = "TrimSlitterDxAx2";
      this.TrimSlitterDxAx2.OnLine = false;
      this.TrimSlitterDxAx2.ShowImage = true;
      this.TrimSlitterDxAx2.ShowText = false;
      this.TrimSlitterDxAx2.Size = new System.Drawing.Size(47, 55);
      this.TrimSlitterDxAx2.Switching = false;
      this.TrimSlitterDxAx2.TabIndex = 254;
      this.TrimSlitterDxAx2.TranslateText = false;
      this.TrimSlitterDxAx2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("TrimSlitterDxAx2.UserDefinedActiveImage")));
      this.TrimSlitterDxAx2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("TrimSlitterDxAx2.UserDefinedImage")));
      this.TrimSlitterDxAx2.Warned = false;
      this.TrimSlitterDxAx2.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // Shaft2
      // 
      this.Shaft2.BackColor = System.Drawing.Color.Silver;
      this.Shaft2.Location = new System.Drawing.Point(351, 301);
      this.Shaft2.Name = "Shaft2";
      this.Shaft2.Size = new System.Drawing.Size(472, 30);
      this.Shaft2.TabIndex = 253;
      // 
      // Shaft1
      // 
      this.Shaft1.BackColor = System.Drawing.Color.Silver;
      this.Shaft1.Location = new System.Drawing.Point(351, 373);
      this.Shaft1.Name = "Shaft1";
      this.Shaft1.Size = new System.Drawing.Size(472, 30);
      this.Shaft1.TabIndex = 252;
      // 
      // ModifyScoreGap1
      // 
      this.ModifyScoreGap1.Image = global::SyncroC.Properties.Resources.OptionGap;
      this.ModifyScoreGap1.Location = new System.Drawing.Point(373, 507);
      this.ModifyScoreGap1.Name = "ModifyScoreGap1";
      this.ModifyScoreGap1.Size = new System.Drawing.Size(42, 42);
      this.ModifyScoreGap1.TabIndex = 248;
      this.ModifyScoreGap1.Click += new System.EventHandler(this.Gap1Value_Click);
      // 
      // ModifyScoreGap3
      // 
      this.ModifyScoreGap3.Image = global::SyncroC.Properties.Resources.OptionGap;
      this.ModifyScoreGap3.Location = new System.Drawing.Point(373, 577);
      this.ModifyScoreGap3.Name = "ModifyScoreGap3";
      this.ModifyScoreGap3.Size = new System.Drawing.Size(42, 42);
      this.ModifyScoreGap3.TabIndex = 247;
      this.ModifyScoreGap3.Click += new System.EventHandler(this.Gap3Value_Click);
      // 
      // ScoreGap2
      // 
      this.ScoreGap2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ScoreGap2.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ScoreGap2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.ScoreGap2.Location = new System.Drawing.Point(444, 431);
      this.ScoreGap2.LowLevelEnabled = true;
      this.ScoreGap2.Name = "ScoreGap2";
      this.ScoreGap2.Position = SyncroC.Controls.IncDecButton.IncDecButtonPositionType.Horizontal;
      this.ScoreGap2.Size = new System.Drawing.Size(335, 55);
      this.ScoreGap2.TabIndex = 246;
      this.ScoreGap2.TranslateText = false;
      this.ScoreGap2.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.ScoreGap2.Value = "0";
      this.ScoreGap2.ValueVisible = true;
      this.ScoreGap2.OnIncDecButtonKitDown += new SyncroC.Controls.IncDecButton.IncDecButtonKitDownHandler(this.OnIncDecButtonKitDown);
      // 
      // ModifyScoreGap2
      // 
      this.ModifyScoreGap2.Image = global::SyncroC.Properties.Resources.OptionGap;
      this.ModifyScoreGap2.Location = new System.Drawing.Point(374, 431);
      this.ModifyScoreGap2.Name = "ModifyScoreGap2";
      this.ModifyScoreGap2.Size = new System.Drawing.Size(42, 51);
      this.ModifyScoreGap2.TabIndex = 245;
      this.ModifyScoreGap2.Click += new System.EventHandler(this.Gap2Value_Click);
      // 
      // lbl_Chutes
      // 
      this.lbl_Chutes.Image = ((System.Drawing.Image)(resources.GetObject("lbl_Chutes.Image")));
      this.lbl_Chutes.Location = new System.Drawing.Point(351, 207);
      this.lbl_Chutes.Name = "lbl_Chutes";
      this.lbl_Chutes.Size = new System.Drawing.Size(81, 55);
      this.lbl_Chutes.TabIndex = 244;
      // 
      // ScoreGap1
      // 
      this.ScoreGap1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ScoreGap1.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ScoreGap1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.ScoreGap1.Location = new System.Drawing.Point(445, 502);
      this.ScoreGap1.LowLevelEnabled = true;
      this.ScoreGap1.Name = "ScoreGap1";
      this.ScoreGap1.Position = SyncroC.Controls.IncDecButton.IncDecButtonPositionType.Horizontal;
      this.ScoreGap1.Size = new System.Drawing.Size(334, 55);
      this.ScoreGap1.TabIndex = 236;
      this.ScoreGap1.TranslateText = false;
      this.ScoreGap1.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.ScoreGap1.Value = "0";
      this.ScoreGap1.ValueVisible = true;
      this.ScoreGap1.OnIncDecButtonKitDown += new SyncroC.Controls.IncDecButton.IncDecButtonKitDownHandler(this.OnIncDecButtonKitDown);
      // 
      // ScoreGap3
      // 
      this.ScoreGap3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ScoreGap3.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ScoreGap3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.ScoreGap3.Location = new System.Drawing.Point(445, 573);
      this.ScoreGap3.LowLevelEnabled = true;
      this.ScoreGap3.Name = "ScoreGap3";
      this.ScoreGap3.Position = SyncroC.Controls.IncDecButton.IncDecButtonPositionType.Horizontal;
      this.ScoreGap3.Size = new System.Drawing.Size(334, 55);
      this.ScoreGap3.TabIndex = 237;
      this.ScoreGap3.TranslateText = false;
      this.ScoreGap3.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.ScoreGap3.Value = "0";
      this.ScoreGap3.ValueVisible = true;
      this.ScoreGap3.OnIncDecButtonKitDown += new SyncroC.Controls.IncDecButton.IncDecButtonKitDownHandler(this.OnIncDecButtonKitDown);
      // 
      // MoveMachineRight
      // 
      this.MoveMachineRight.BackColor = System.Drawing.Color.WhiteSmoke;
      this.MoveMachineRight.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MoveMachineRight.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MoveMachineRight.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.RightArrow;
      this.MoveMachineRight.Location = new System.Drawing.Point(678, 651);
      this.MoveMachineRight.LowLevelEnabled = true;
      this.MoveMachineRight.Name = "MoveMachineRight";
      this.MoveMachineRight.Size = new System.Drawing.Size(100, 55);
      this.MoveMachineRight.TabIndex = 232;
      this.MoveMachineRight.TimerInterval = 1000;
      this.MoveMachineRight.TranslateText = false;
      this.MoveMachineRight.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.OnIncDecButtonDown);
      // 
      // MoveMachineLeft
      // 
      this.MoveMachineLeft.BackColor = System.Drawing.Color.WhiteSmoke;
      this.MoveMachineLeft.Cursor = System.Windows.Forms.Cursors.Hand;
      this.MoveMachineLeft.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MoveMachineLeft.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.LeftArrow;
      this.MoveMachineLeft.Location = new System.Drawing.Point(445, 651);
      this.MoveMachineLeft.LowLevelEnabled = true;
      this.MoveMachineLeft.Name = "MoveMachineLeft";
      this.MoveMachineLeft.Size = new System.Drawing.Size(100, 55);
      this.MoveMachineLeft.TabIndex = 231;
      this.MoveMachineLeft.TimerInterval = 1000;
      this.MoveMachineLeft.TranslateText = false;
      this.MoveMachineLeft.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.OnIncDecButtonDown);
      // 
      // ManualAutomaticMachineMove
      // 
      this.ManualAutomaticMachineMove.Active = false;
      this.ManualAutomaticMachineMove.Alarmed = false;
      this.ManualAutomaticMachineMove.Auto = false;
      this.ManualAutomaticMachineMove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.ManualAutomaticMachineMove.Cursor = System.Windows.Forms.Cursors.Hand;
      this.ManualAutomaticMachineMove.FillBackColorWithImage = false;
      this.ManualAutomaticMachineMove.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.ManualAutomaticMachineMove.ForeColor = System.Drawing.Color.Black;
      this.ManualAutomaticMachineMove.FramePresent = true;
      this.ManualAutomaticMachineMove.HighLightEnable = true;
      this.ManualAutomaticMachineMove.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.ManualAutomaticMachineMove.ImagePassword = null;
      this.ManualAutomaticMachineMove.Inemergency = false;
      this.ManualAutomaticMachineMove.InLocal = false;
      this.ManualAutomaticMachineMove.IsButton = true;
      this.ManualAutomaticMachineMove.IsForceToInvisible = false;
      this.ManualAutomaticMachineMove.Location = new System.Drawing.Point(563, 651);
      this.ManualAutomaticMachineMove.Name = "ManualAutomaticMachineMove";
      this.ManualAutomaticMachineMove.OnLine = false;
      this.ManualAutomaticMachineMove.ShowImage = true;
      this.ManualAutomaticMachineMove.ShowText = false;
      this.ManualAutomaticMachineMove.Size = new System.Drawing.Size(93, 55);
      this.ManualAutomaticMachineMove.Switching = false;
      this.ManualAutomaticMachineMove.TabIndex = 230;
      this.ManualAutomaticMachineMove.Text = "Auto";
      this.ManualAutomaticMachineMove.TranslateText = false;
      this.ManualAutomaticMachineMove.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("ManualAutomaticMachineMove.UserDefinedActiveImage")));
      this.ManualAutomaticMachineMove.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("ManualAutomaticMachineMove.UserDefinedImage")));
      this.ManualAutomaticMachineMove.Warned = false;
      this.ManualAutomaticMachineMove.Click += new System.EventHandler(this.OnStateButtonPushDuration);
      // 
      // InternalRightMachineMove
      // 
      this.InternalRightMachineMove.BackColor = System.Drawing.Color.Green;
      this.InternalRightMachineMove.Busy = false;
      this.InternalRightMachineMove.Location = new System.Drawing.Point(801, 651);
      this.InternalRightMachineMove.Name = "InternalRightMachineMove";
      this.InternalRightMachineMove.Size = new System.Drawing.Size(10, 50);
      this.InternalRightMachineMove.TabIndex = 243;
      this.InternalRightMachineMove.TranslateText = false;
      // 
      // ExternalRightMachineMove
      // 
      this.ExternalRightMachineMove.BackColor = System.Drawing.Color.Green;
      this.ExternalRightMachineMove.Busy = false;
      this.ExternalRightMachineMove.Location = new System.Drawing.Point(817, 651);
      this.ExternalRightMachineMove.Name = "ExternalRightMachineMove";
      this.ExternalRightMachineMove.Size = new System.Drawing.Size(10, 50);
      this.ExternalRightMachineMove.TabIndex = 241;
      this.ExternalRightMachineMove.TranslateText = false;
      // 
      // RightLimitMachineMove
      // 
      this.RightLimitMachineMove.BackColor = System.Drawing.Color.Green;
      this.RightLimitMachineMove.Busy = false;
      this.RightLimitMachineMove.Location = new System.Drawing.Point(833, 668);
      this.RightLimitMachineMove.Name = "RightLimitMachineMove";
      this.RightLimitMachineMove.Size = new System.Drawing.Size(10, 17);
      this.RightLimitMachineMove.TabIndex = 239;
      this.RightLimitMachineMove.TranslateText = false;
      // 
      // ExternalLeftMachineMove
      // 
      this.ExternalLeftMachineMove.BackColor = System.Drawing.Color.Green;
      this.ExternalLeftMachineMove.Busy = false;
      this.ExternalLeftMachineMove.Location = new System.Drawing.Point(392, 652);
      this.ExternalLeftMachineMove.Name = "ExternalLeftMachineMove";
      this.ExternalLeftMachineMove.Size = new System.Drawing.Size(10, 49);
      this.ExternalLeftMachineMove.TabIndex = 240;
      this.ExternalLeftMachineMove.TranslateText = false;
      // 
      // InternalLeftMachineMove
      // 
      this.InternalLeftMachineMove.BackColor = System.Drawing.Color.Green;
      this.InternalLeftMachineMove.Busy = false;
      this.InternalLeftMachineMove.Location = new System.Drawing.Point(408, 652);
      this.InternalLeftMachineMove.Name = "InternalLeftMachineMove";
      this.InternalLeftMachineMove.Size = new System.Drawing.Size(10, 49);
      this.InternalLeftMachineMove.TabIndex = 242;
      this.InternalLeftMachineMove.TranslateText = false;
      // 
      // LeftLimitMachineMove
      // 
      this.LeftLimitMachineMove.BackColor = System.Drawing.Color.Green;
      this.LeftLimitMachineMove.Busy = false;
      this.LeftLimitMachineMove.Location = new System.Drawing.Point(376, 666);
      this.LeftLimitMachineMove.Name = "LeftLimitMachineMove";
      this.LeftLimitMachineMove.Size = new System.Drawing.Size(10, 19);
      this.LeftLimitMachineMove.TabIndex = 238;
      this.LeftLimitMachineMove.TranslateText = false;
      // 
      // OpenTrimChutes
      // 
      this.OpenTrimChutes.BackColor = System.Drawing.Color.WhiteSmoke;
      this.OpenTrimChutes.Cursor = System.Windows.Forms.Cursors.Hand;
      this.OpenTrimChutes.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.OpenTrimChutes.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.WidenArrows;
      this.OpenTrimChutes.Location = new System.Drawing.Point(678, 207);
      this.OpenTrimChutes.LowLevelEnabled = true;
      this.OpenTrimChutes.Name = "OpenTrimChutes";
      this.OpenTrimChutes.Size = new System.Drawing.Size(101, 55);
      this.OpenTrimChutes.TabIndex = 235;
      this.OpenTrimChutes.TimerInterval = 1000;
      this.OpenTrimChutes.TranslateText = false;
      this.OpenTrimChutes.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.OnIncDecButtonDown);
      // 
      // CloseTrimChutes
      // 
      this.CloseTrimChutes.BackColor = System.Drawing.Color.WhiteSmoke;
      this.CloseTrimChutes.Cursor = System.Windows.Forms.Cursors.Hand;
      this.CloseTrimChutes.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CloseTrimChutes.Image = SyncroC.Controls.IncDecButton.IncDecButtonArrowImage.PressArrows;
      this.CloseTrimChutes.Location = new System.Drawing.Point(446, 207);
      this.CloseTrimChutes.LowLevelEnabled = true;
      this.CloseTrimChutes.Name = "CloseTrimChutes";
      this.CloseTrimChutes.Size = new System.Drawing.Size(100, 55);
      this.CloseTrimChutes.TabIndex = 233;
      this.CloseTrimChutes.TimerInterval = 1000;
      this.CloseTrimChutes.TranslateText = false;
      this.CloseTrimChutes.OnIncDecButtonDown += new SyncroC.Controls.IncDecButton.IncDecButtonDownHandler(this.OnIncDecButtonDown);
      // 
      // ChutesSpan
      // 
      this.ChutesSpan.BackColor = System.Drawing.Color.WhiteSmoke;
      this.ChutesSpan.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ChutesSpan.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ChutesSpan.ImagePassword = null;
      this.ChutesSpan.Location = new System.Drawing.Point(564, 207);
      this.ChutesSpan.Name = "ChutesSpan";
      this.ChutesSpan.Size = new System.Drawing.Size(93, 55);
      this.ChutesSpan.Style = SyncroC.Controls.TitledLabelStyles.Light;
      this.ChutesSpan.TabIndex = 234;
      this.ChutesSpan.TabStop = false;
      this.ChutesSpan.Text = "Span";
      this.ChutesSpan.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ChutesSpan.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ChutesSpan.TranslateText = true;
      this.ChutesSpan.Value = "735";
      this.ChutesSpan.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ChutesSpan.ValueMultiline = true;
      // 
      // Shaft4
      // 
      this.Shaft4.BackColor = System.Drawing.Color.Silver;
      this.Shaft4.Location = new System.Drawing.Point(351, 443);
      this.Shaft4.Name = "Shaft4";
      this.Shaft4.Size = new System.Drawing.Size(472, 30);
      this.Shaft4.TabIndex = 249;
      // 
      // Shaft3
      // 
      this.Shaft3.BackColor = System.Drawing.Color.Silver;
      this.Shaft3.Location = new System.Drawing.Point(351, 512);
      this.Shaft3.Name = "Shaft3";
      this.Shaft3.Size = new System.Drawing.Size(472, 30);
      this.Shaft3.TabIndex = 250;
      // 
      // Shaft5
      // 
      this.Shaft5.BackColor = System.Drawing.Color.Silver;
      this.Shaft5.Location = new System.Drawing.Point(352, 582);
      this.Shaft5.Name = "Shaft5";
      this.Shaft5.Size = new System.Drawing.Size(471, 30);
      this.Shaft5.TabIndex = 251;
      // 
      // Frm_twin400_console
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.SlitterAx1);
      this.Controls.Add(this.SlitterAx2);
      this.Controls.Add(this.TrimSlitterDxAx2);
      this.Controls.Add(this.ModifyScoreGap1);
      this.Controls.Add(this.ModifyScoreGap3);
      this.Controls.Add(this.ScoreGap2);
      this.Controls.Add(this.ModifyScoreGap2);
      this.Controls.Add(this.lbl_Chutes);
      this.Controls.Add(this.ScoreGap1);
      this.Controls.Add(this.ScoreGap3);
      this.Controls.Add(this.MoveMachineRight);
      this.Controls.Add(this.MoveMachineLeft);
      this.Controls.Add(this.ManualAutomaticMachineMove);
      this.Controls.Add(this.InternalRightMachineMove);
      this.Controls.Add(this.ExternalRightMachineMove);
      this.Controls.Add(this.RightLimitMachineMove);
      this.Controls.Add(this.ExternalLeftMachineMove);
      this.Controls.Add(this.InternalLeftMachineMove);
      this.Controls.Add(this.LeftLimitMachineMove);
      this.Controls.Add(this.OpenTrimChutes);
      this.Controls.Add(this.CloseTrimChutes);
      this.Controls.Add(this.ChutesSpan);
      this.Controls.Add(this.Shaft4);
      this.Controls.Add(this.Shaft3);
      this.Controls.Add(this.Shaft5);
      this.Controls.Add(this.WebFlap);
      this.Controls.Add(this.TrimSlitterSxAx2);
      this.Controls.Add(this.TrimSlitterDxAx1);
      this.Controls.Add(this.TrimSlitterSxAx1);
      this.Controls.Add(this.ForceReset);
      this.Controls.Add(this.LiftAllShafts);
      this.Controls.Add(this.ForceScan);
      this.Controls.Add(this.JamupPhotoeye);
      this.Controls.Add(this.SendSetup0);
      this.Controls.Add(this.SendSetup1);
      this.Controls.Add(this.RecoverPositioning);
      this.Controls.Add(this.RemoteControl);
      this.Controls.Add(this.ManualAOC);
      this.Controls.Add(this.TeflonCycleStart);
      this.Controls.Add(this.LoadTeflon1);
      this.Controls.Add(this.LoadTeflon0);
      this.Controls.Add(this.Shaft2);
      this.Controls.Add(this.Shaft1);
      this.Controls.Add(this.panel1);
      this.DesignGridHorizontalLines = 5;
      this.DesignGridVerticalLines = 9;
      this.Name = "Frm_twin400_console";
      this.Tag = "";
      this.Text = "Frm_twin400_console";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.Shaft1, 0);
      this.Controls.SetChildIndex(this.Shaft2, 0);
      this.Controls.SetChildIndex(this.LoadTeflon0, 0);
      this.Controls.SetChildIndex(this.LoadTeflon1, 0);
      this.Controls.SetChildIndex(this.TeflonCycleStart, 0);
      this.Controls.SetChildIndex(this.ManualAOC, 0);
      this.Controls.SetChildIndex(this.RemoteControl, 0);
      this.Controls.SetChildIndex(this.RecoverPositioning, 0);
      this.Controls.SetChildIndex(this.SendSetup1, 0);
      this.Controls.SetChildIndex(this.SendSetup0, 0);
      this.Controls.SetChildIndex(this.JamupPhotoeye, 0);
      this.Controls.SetChildIndex(this.ForceScan, 0);
      this.Controls.SetChildIndex(this.LiftAllShafts, 0);
      this.Controls.SetChildIndex(this.ForceReset, 0);
      this.Controls.SetChildIndex(this.TrimSlitterSxAx1, 0);
      this.Controls.SetChildIndex(this.TrimSlitterDxAx1, 0);
      this.Controls.SetChildIndex(this.TrimSlitterSxAx2, 0);
      this.Controls.SetChildIndex(this.WebFlap, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
      this.Controls.SetChildIndex(this.pct_Home_NY, 0);
      this.Controls.SetChildIndex(this.pct_Back_NY, 0);
      this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
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
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.Shaft5, 0);
      this.Controls.SetChildIndex(this.Shaft3, 0);
      this.Controls.SetChildIndex(this.Shaft4, 0);
      this.Controls.SetChildIndex(this.ChutesSpan, 0);
      this.Controls.SetChildIndex(this.CloseTrimChutes, 0);
      this.Controls.SetChildIndex(this.OpenTrimChutes, 0);
      this.Controls.SetChildIndex(this.LeftLimitMachineMove, 0);
      this.Controls.SetChildIndex(this.InternalLeftMachineMove, 0);
      this.Controls.SetChildIndex(this.ExternalLeftMachineMove, 0);
      this.Controls.SetChildIndex(this.RightLimitMachineMove, 0);
      this.Controls.SetChildIndex(this.ExternalRightMachineMove, 0);
      this.Controls.SetChildIndex(this.InternalRightMachineMove, 0);
      this.Controls.SetChildIndex(this.ManualAutomaticMachineMove, 0);
      this.Controls.SetChildIndex(this.MoveMachineLeft, 0);
      this.Controls.SetChildIndex(this.MoveMachineRight, 0);
      this.Controls.SetChildIndex(this.ScoreGap3, 0);
      this.Controls.SetChildIndex(this.ScoreGap1, 0);
      this.Controls.SetChildIndex(this.lbl_Chutes, 0);
      this.Controls.SetChildIndex(this.ModifyScoreGap2, 0);
      this.Controls.SetChildIndex(this.ScoreGap2, 0);
      this.Controls.SetChildIndex(this.ModifyScoreGap3, 0);
      this.Controls.SetChildIndex(this.ModifyScoreGap1, 0);
      this.Controls.SetChildIndex(this.TrimSlitterDxAx2, 0);
      this.Controls.SetChildIndex(this.SlitterAx2, 0);
      this.Controls.SetChildIndex(this.SlitterAx1, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }
		#endregion

		/// <summary>
		/// Handles event raised by every IncDecButtonKit of the page, 
		/// build the proper command and send it to the RTC_MANAGER.
		/// </summary>
		private void OnIncDecButtonKitDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonKitDownEventArgs args)
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER, 
				PageManager.getRequestCommand(typeof(Frm_twin400_console)) + "." + 
				((Control)sender).Name + "." + ((args.buttonType == IncDecButtonType.Increase) ? Command.INCREASE : Command.DECREASE) + "(" + 
				args.Frequency + ")");
		}

		/// <summary>
		/// Handles event raised by every IncDecButton of the page, 
		/// build the proper command and send it to the RTC_MANAGER.
		/// </summary>
		private void OnIncDecButtonDown(object sender, SyncroC.Controls.IncDecButton.IncDecButtonDownEventArgs args)
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER, 
				PageManager.getRequestCommand(typeof(Frm_twin400_console)) + "." + ((Control)sender).Name + "." + Command.INCDEC_BUTTON_SIGNAL+
					"(" + args.Frequency + ")");
		}

		/// <summary>
		/// Handles event raised by StateButton associated with commands with duration that are in the page, 
		/// build the proper command and send it to the RTC_MANAGER.
		/// </summary>
		private void OnStateButtonPushDuration(object sender, System.EventArgs e)
		{
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER, 
				PageManager.getRequestCommand(typeof(Frm_twin400_console)) + "." + ((Control)sender).Name +	"(-1)");
		}

		/// <summary>
		/// Handles event raised by generic button that are in the page, 
		/// build the proper command and send it to the RTC_MANAGER.
		/// </summary>
		private void OnButtonPush(object sender, System.EventArgs e)
		{
      //agrassi_FI12-06-08
      if ((!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled) && (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending))
        return;
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER, 
				PageManager.getRequestCommand(typeof(Frm_twin400_console)) + "." + ((Control)sender).Name + "()");
		}

    public override void refreshData()
    {
      //agrassi_FI11-12-08
      if (ss.Twin400.PLCInterfaceVersion < SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver5)
      {
        this.TrimSlitterDxAx1.Visible = false;
        this.TrimSlitterDxAx2.Visible = false;
        this.TrimSlitterSxAx1.Visible = false;
        this.TrimSlitterSxAx2.Visible = false;
      }
      this.ManualAOC.Visible = this.RemoteControl.Visible = DBManager.getDBManager().syncroConfig.IsTwin400Only();
      //PMASSARO_FI03/02/09 - ShutDown From Operator Page
      if (!DBManager.getDBManager().syncroConfig.IsTwin400Only())
      {
        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF1_YY.Enabled = false ;
      }

      //AGrassi_FI06/12/2007_1
			//RFAZZI PROVA
			//this.TeflonCycleStart.Visible = (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver4);
			this.TeflonCycleStart.Visible = (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver4);
			this._SetChutesSpan(this.intParse(this.ChutesSpan.Value));
    }

    private void _SetChutesSpan(int span)
    { 
			this.ChutesSpan.Value=span.ToString();
			//RFAZZI PROVA
			//int maxSpan = DBManager.getDBManager().lineConfig.SlitterScorerConfig.PlcConfig.ChutesResetPosition;
			int maxSpan = DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PlcConfig.ChutesResetPosition;
			//agrassi_FI 25-09-2006
      //int minSpan = DBManager.getDBManager().lineConfig.SlitterScorerConfig.MinChutesPosition;
			int minSpan = (int)SyncroConverter.Convert(DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.MinChutesPosition, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
    }

    private void Gap1Value_Click(object sender, EventArgs e)
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
			//RFAZZI PROVA
      //if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion < SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver3)
      if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion < SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver3)
        return;
      Frm_AskValue frm = new Frm_AskValue();
      FosberDialogResult ret=frm.ShowDialog(this, "", this.intParse(this.ScoreGap1.Value));
      if (ret.Dialog == DialogResult.OK)
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_twin400_console)) + "." + ((Control)sender).Name + "(" + ret.Data[0].ToString() + ")");
      }
    }

    private void Gap2Value_Click(object sender, EventArgs e)
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
			//RFAZZI PROVA
			//if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion < SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver3)
			if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion < SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver3)
          return;
      Frm_AskValue frm = new Frm_AskValue();
      FosberDialogResult ret = frm.ShowDialog(this, "", this.intParse(this.ScoreGap2.Value));
      if (ret.Dialog == DialogResult.OK)
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_twin400_console)) + "." + ((Control)sender).Name + "(" + ret.Data[0].ToString() + ")");
      }
    }

    private void Gap3Value_Click(object sender, EventArgs e)
    {
      //agrassi_FI12-06-08
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;
			//RFAZZI PROVA
			//if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.PLCInterfaceVersion < SyncroC.DataBase.LineConfig.SlitterScorer.MachineVersion.Ver3)
			if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.PLCInterfaceVersion < SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver3)
          return;
      Frm_AskValue frm = new Frm_AskValue();
      FosberDialogResult ret = frm.ShowDialog(this, "", this.intParse(this.ScoreGap3.Value));
      if (ret.Dialog == DialogResult.OK)
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
          PageManager.getRequestCommand(typeof(Frm_twin400_console)) + "." + ((Control)sender).Name + "(" + ret.Data[0].ToString() + ")");
      }
    }

    //AGrassi_FI06/12/2007_1
    private void TeflonCycleStart_Click(object sender, EventArgs e)
    {
      if (!this.teflonCyclePossible)
      {
        Frm_Alert frm = new Frm_Alert();
        string text=DBManager.getDBManager().message.getTranslatedMessage("§SYCC_179")+"\r\n"+
                    DBManager.getDBManager().message.getTranslatedMessage("§"+this.teflonReasonCode);
        frm.ShowDialog(text);
        return;
      }
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
        PageManager.getRequestCommand(typeof(Frm_twin400_console)) + "." + ((Control)sender).Name + "()");
    }

    private void _SetTeflonPossible(bool possible, string reasonCode)
    {
      this.teflonCyclePossible = possible;
      this.teflonReasonCode = reasonCode;
    }

    //agrassi_FI07-07-08_1
    private void ModifyScoreGap_MouseEnter(object sender, EventArgs e)
    {
      this.Cursor = Cursors.Hand;
    }

    private void ModifyScoreGap_MouseLeave(object sender, EventArgs e)
    {
      this.Cursor = Cursors.Arrow;
    }

    //agrassi_FI25-11-2009_1
    private void _SetWebFlapText(string flapTextKey, string pullRollTextKey)
    {
      if (DBManager.getDBManager().lineConfig.WebDirectorConfig.PullRollPresent)
        this.WebFlap.Text = DBManager.getDBManager().message.getTranslatedMessage("§" + pullRollTextKey);
      else
        this.WebFlap.Text = DBManager.getDBManager().message.getTranslatedMessage("§" + flapTextKey);
    }
  }
}

