using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;  
using System.Data;
using System.Windows.Forms;
using System.Reflection;

using SyncroC.Forms.Pages;
using SyncroC.DataBase.EventArgs;
using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Controls.Colors;
using SyncroC.Forms.Pages.Twin400.Orders;
using SyncroC.Forms.Pages.Twin400.Tools;
using SyncroC.Forms.Pages.Twin400.Alarms;
using SyncroC.Forms.Pages.Twin400.Settings;
using SyncroC.Forms.Pages.Twin400.Status;
//RFAZZI_FI26/01/2011_2. Aggiunta gestione Warning su grafica Twin400 di Syncro. 
using SyncroC.Forms.Pages.Twin400.Warnings;



namespace SyncroC.Forms.StatusBar
{
	public class Frm_StatusBarTwin400 : SyncroC.Forms.StatusBar.Frm_StatusBar
	{
		private const int  WM_KEYDOWN   =  0x0100;
    private SyncroC.Controls.SlitterScorerShaftStatus shaftStatus_YN;
    private SyncroC.Controls.TitledLabel tlb_Queue;
		private System.Windows.Forms.ImageList iml_Scores;
		private System.Windows.Forms.ImageList iml_Trim;
    private System.Windows.Forms.ImageList iml_Tandem;
		private System.Windows.Forms.ImageList iml_SetupState;
		DataRowCollection setupRows;
    private int[] machId;
    private int[] machMod;
    private System.Windows.Forms.ImageList iml_SyncroComm;
		private System.Windows.Forms.ImageList iml_PlcComm;
    private SyncroC.Controls.TitledLabel tlb_OrderChangeStatus;
		private SyncroC.Controls.TitledLabel tlb_MachineStatus;
		private SyncroC.Controls.StateButton Hardware;
		private SyncroC.Controls.StateButton Power;
		private SyncroC.Controls.StateButton Logic;
		private SyncroC.Controls.StateButton OnBoard;
		private SyncroC.Controls.StateButton Console;
		private SyncroC.Controls.StateButton Drives;
    private SyncroC.Controls.StateButton Warnings;
    private Type formType;
    private SyncroC.Controls.StateButton Software;
    private SyncroC.Controls.StateButton PLCComm;
    private SyncroC.Controls.StateButton SupervisorComm;
    private MachineStatus machStatus;
    private OrderChangeStatus aocStatus;
    private SlitPositionsStatus slitPosStatus;
    private SlitPositioningStatus slitPositioningStatus;
    private SyncroC.Controls.TitledLabel tlb_Speed;
    private SyncroC.Controls.TitledLabel tlb_SlitPositionsStatus;
    private SyncroC.Controls.TitledLabel tlb_OrderChangeType;
    private System.ComponentModel.IContainer components = null;
    //agrassi_FI09-11-2009_1
    //private bool[] narrowToWide, wideToNarrow;
    private SyncroC.Controls.TitledLabel tlb_SlitPositioningStatus;
    private SyncroC.Controls.StateButton DeviceNET;
    private SyncroC.DataBase.Message msg;
    private SyncroC.DataBase.Language lgg;
    private Timer timTandemOrderChange;
    private SyncroC.Controls.SlitterScorerSetupStatus setupStatus_YN;


		/// <summary>
		/// builder.
		/// </summary>
		public Frm_StatusBarTwin400()
		{
			InitializeComponent();
      this.timTandemOrderChange.Interval = 400;
      this.shaftStatus_YN.SetExternalTimerFunction(this.PositioningBlink);
      this.formType = this.GetType();
      this.machId = new int[SlitterScorerHeader.SS_SETUPS];
      this.machMod = new int[SlitterScorerHeader.SS_SETUPS];
      DBManager.getDBManager().order.OnDBOrderChange += new SyncroC.DataBase.Order.DBChangedHandler(this.SetupListChange);
      this.setupRows=DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows;
      this.msg = DBManager.getDBManager().message;
      this.lgg = DBManager.getDBManager().language;
      //agrassi_FI09-11-2009_1
      //this.narrowToWide = new bool[SlitterScorerHeader.SS_SETUPS];
      //this.wideToNarrow = new bool[SlitterScorerHeader.SS_SETUPS];

      this.shaftStatus_YN.SetDescriptions(msg.getTranslatedMessage("§SYCC_43"), msg.getTranslatedMessage("§SYCC_44"),
                                              msg.getTranslatedMessage("§SYCC_45"), msg.getTranslatedMessage("§SYCC_46"),
                                              msg.getTranslatedMessage("§SYCC_47"), msg.getTranslatedMessage("§SYCC_48"));
      this.setupStatus_YN.Config(this.shaftStatus_YN.GetSetupColor);

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
      DBManager.getDBManager().order.OnDBOrderChange -= new SyncroC.DataBase.Order.DBChangedHandler(this.SetupListChange);
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StatusBarTwin400));
      this.shaftStatus_YN = new SyncroC.Controls.SlitterScorerShaftStatus();
      this.tlb_Queue = new SyncroC.Controls.TitledLabel();
      this.iml_SetupState = new System.Windows.Forms.ImageList(this.components);
      this.iml_Tandem = new System.Windows.Forms.ImageList(this.components);
      this.iml_Trim = new System.Windows.Forms.ImageList(this.components);
      this.iml_Scores = new System.Windows.Forms.ImageList(this.components);
      this.iml_SyncroComm = new System.Windows.Forms.ImageList(this.components);
      this.iml_PlcComm = new System.Windows.Forms.ImageList(this.components);
      this.tlb_OrderChangeStatus = new SyncroC.Controls.TitledLabel();
      this.tlb_MachineStatus = new SyncroC.Controls.TitledLabel();
      this.Hardware = new SyncroC.Controls.StateButton();
      this.Power = new SyncroC.Controls.StateButton();
      this.Logic = new SyncroC.Controls.StateButton();
      this.OnBoard = new SyncroC.Controls.StateButton();
      this.Console = new SyncroC.Controls.StateButton();
      this.Drives = new SyncroC.Controls.StateButton();
      this.Warnings = new SyncroC.Controls.StateButton();
      this.Software = new SyncroC.Controls.StateButton();
      this.PLCComm = new SyncroC.Controls.StateButton();
      this.SupervisorComm = new SyncroC.Controls.StateButton();
      this.tlb_Speed = new SyncroC.Controls.TitledLabel();
      this.tlb_SlitPositionsStatus = new SyncroC.Controls.TitledLabel();
      this.tlb_OrderChangeType = new SyncroC.Controls.TitledLabel();
      this.tlb_SlitPositioningStatus = new SyncroC.Controls.TitledLabel();
      this.DeviceNET = new SyncroC.Controls.StateButton();
      this.timTandemOrderChange = new System.Windows.Forms.Timer(this.components);
      this.setupStatus_YN = new SyncroC.Controls.SlitterScorerSetupStatus();
      this.SuspendLayout();
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // shaftStatus_YN
      // 
      this.shaftStatus_YN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.shaftStatus_YN.Location = new System.Drawing.Point(918, 30);
      this.shaftStatus_YN.MinimumSize = new System.Drawing.Size(358, 229);
      this.shaftStatus_YN.Name = "shaftStatus_YN";
      this.shaftStatus_YN.Size = new System.Drawing.Size(358, 239);
      this.shaftStatus_YN.TabIndex = 110;
      this.shaftStatus_YN.TranslateText = false;
      this.shaftStatus_YN.ClickAny += new System.EventHandler(this.Shaft_Click);
      // 
      // tlb_Queue
      // 
      this.tlb_Queue.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_Queue.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_Queue.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tlb_Queue.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_Queue.ImagePassword = null;
      this.tlb_Queue.Location = new System.Drawing.Point(747, 148);
      this.tlb_Queue.Name = "tlb_Queue";
      this.tlb_Queue.Size = new System.Drawing.Size(77, 50);
      this.tlb_Queue.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_Queue.TabIndex = 192;
      this.tlb_Queue.Text = "QUEUE";
      this.tlb_Queue.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_Queue.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_Queue.TranslateText = true;
      this.tlb_Queue.Value = "5";
      this.tlb_Queue.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Queue.ValueMultiline = true;
      this.tlb_Queue.Click += new System.EventHandler(this.Queue_Click);
      // 
      // iml_SetupState
      // 
      this.iml_SetupState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_SetupState.ImageStream")));
      this.iml_SetupState.TransparentColor = System.Drawing.Color.Transparent;
      this.iml_SetupState.Images.SetKeyName(0, "");
      this.iml_SetupState.Images.SetKeyName(1, "");
      this.iml_SetupState.Images.SetKeyName(2, "");
      this.iml_SetupState.Images.SetKeyName(3, "");
      this.iml_SetupState.Images.SetKeyName(4, "");
      // 
      // iml_Tandem
      // 
      this.iml_Tandem.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_Tandem.ImageStream")));
      this.iml_Tandem.TransparentColor = System.Drawing.Color.Transparent;
      this.iml_Tandem.Images.SetKeyName(0, "");
      // 
      // iml_Trim
      // 
      this.iml_Trim.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_Trim.ImageStream")));
      this.iml_Trim.TransparentColor = System.Drawing.Color.Transparent;
      this.iml_Trim.Images.SetKeyName(0, "");
      this.iml_Trim.Images.SetKeyName(1, "");
      // 
      // iml_Scores
      // 
      this.iml_Scores.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_Scores.ImageStream")));
      this.iml_Scores.TransparentColor = System.Drawing.Color.Transparent;
      this.iml_Scores.Images.SetKeyName(0, "");
      this.iml_Scores.Images.SetKeyName(1, "");
      this.iml_Scores.Images.SetKeyName(2, "");
      // 
      // iml_SyncroComm
      // 
      this.iml_SyncroComm.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_SyncroComm.ImageStream")));
      this.iml_SyncroComm.TransparentColor = System.Drawing.Color.Transparent;
      this.iml_SyncroComm.Images.SetKeyName(0, "");
      this.iml_SyncroComm.Images.SetKeyName(1, "");
      // 
      // iml_PlcComm
      // 
      this.iml_PlcComm.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_PlcComm.ImageStream")));
      this.iml_PlcComm.TransparentColor = System.Drawing.Color.Transparent;
      this.iml_PlcComm.Images.SetKeyName(0, "");
      this.iml_PlcComm.Images.SetKeyName(1, "");
      // 
      // tlb_OrderChangeStatus
      // 
      this.tlb_OrderChangeStatus.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_OrderChangeStatus.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_OrderChangeStatus.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tlb_OrderChangeStatus.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_OrderChangeStatus.ImagePassword = null;
      this.tlb_OrderChangeStatus.Location = new System.Drawing.Point(572, 30);
      this.tlb_OrderChangeStatus.Name = "tlb_OrderChangeStatus";
      this.tlb_OrderChangeStatus.Size = new System.Drawing.Size(174, 50);
      this.tlb_OrderChangeStatus.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_OrderChangeStatus.TabIndex = 199;
      this.tlb_OrderChangeStatus.Text = "ORDER CHANGE STATUS";
      this.tlb_OrderChangeStatus.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_OrderChangeStatus.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_OrderChangeStatus.TranslateText = true;
      this.tlb_OrderChangeStatus.Value = "Ready";
      this.tlb_OrderChangeStatus.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_OrderChangeStatus.ValueMultiline = false;
      // 
      // tlb_MachineStatus
      // 
      this.tlb_MachineStatus.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_MachineStatus.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_MachineStatus.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tlb_MachineStatus.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_MachineStatus.ImagePassword = null;
      this.tlb_MachineStatus.Location = new System.Drawing.Point(572, 148);
      this.tlb_MachineStatus.Margin = new System.Windows.Forms.Padding(1, 3, 2, 3);
      this.tlb_MachineStatus.Name = "tlb_MachineStatus";
      this.tlb_MachineStatus.Size = new System.Drawing.Size(174, 50);
      this.tlb_MachineStatus.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_MachineStatus.TabIndex = 200;
      this.tlb_MachineStatus.Text = "MACHINE STATUS";
      this.tlb_MachineStatus.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_MachineStatus.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_MachineStatus.TranslateText = true;
      this.tlb_MachineStatus.Value = "Positioning";
      this.tlb_MachineStatus.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_MachineStatus.ValueMultiline = false;
      // 
      // Hardware
      // 
      this.Hardware.Active = false;
      this.Hardware.Alarmed = false;
      this.Hardware.Auto = false;
      this.Hardware.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Hardware.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Hardware.FillBackColorWithImage = false;
      this.Hardware.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Hardware.FramePresent = false;
      this.Hardware.HighLightEnable = false;
      this.Hardware.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Hardware.ImagePassword = null;
      this.Hardware.Inemergency = false;
      this.Hardware.InLocal = false;
      this.Hardware.IsButton = true;
      this.Hardware.IsForceToInvisible = false;
      this.Hardware.Location = new System.Drawing.Point(172, 202);
      this.Hardware.Name = "Hardware";
      this.Hardware.OnLine = false;
      this.Hardware.ShowImage = true;
      this.Hardware.ShowText = true;
      this.Hardware.Size = new System.Drawing.Size(72, 67);
      this.Hardware.Switching = false;
      this.Hardware.TabIndex = 201;
      this.Hardware.Text = "Hardware";
      this.Hardware.TranslateText = true;
      this.Hardware.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Hardware.UserDefinedActiveImage")));
      this.Hardware.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Hardware.UserDefinedImage")));
      this.Hardware.Warned = false;
      this.Hardware.Click += new System.EventHandler(this.Alarm_Click);
      // 
      // Power
      // 
      this.Power.Active = false;
      this.Power.Alarmed = false;
      this.Power.Auto = false;
      this.Power.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Power.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Power.FillBackColorWithImage = false;
      this.Power.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Power.FramePresent = false;
      this.Power.HighLightEnable = false;
      this.Power.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Power.ImagePassword = null;
      this.Power.Inemergency = false;
      this.Power.InLocal = false;
      this.Power.IsButton = true;
      this.Power.IsForceToInvisible = false;
      this.Power.Location = new System.Drawing.Point(256, 202);
      this.Power.Name = "Power";
      this.Power.OnLine = false;
      this.Power.ShowImage = true;
      this.Power.ShowText = true;
      this.Power.Size = new System.Drawing.Size(72, 67);
      this.Power.Switching = false;
      this.Power.TabIndex = 202;
      this.Power.Text = "Power";
      this.Power.TranslateText = true;
      this.Power.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Power.UserDefinedActiveImage")));
      this.Power.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Power.UserDefinedImage")));
      this.Power.Warned = false;
      this.Power.Click += new System.EventHandler(this.Alarm_Click);
      // 
      // Logic
      // 
      this.Logic.Active = false;
      this.Logic.Alarmed = false;
      this.Logic.Auto = false;
      this.Logic.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Logic.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Logic.FillBackColorWithImage = false;
      this.Logic.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Logic.FramePresent = false;
      this.Logic.HighLightEnable = false;
      this.Logic.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Logic.ImagePassword = null;
      this.Logic.Inemergency = false;
      this.Logic.InLocal = false;
      this.Logic.IsButton = true;
      this.Logic.IsForceToInvisible = false;
      this.Logic.Location = new System.Drawing.Point(340, 202);
      this.Logic.Name = "Logic";
      this.Logic.OnLine = false;
      this.Logic.ShowImage = true;
      this.Logic.ShowText = true;
      this.Logic.Size = new System.Drawing.Size(72, 67);
      this.Logic.Switching = false;
      this.Logic.TabIndex = 203;
      this.Logic.Text = "Logic";
      this.Logic.TranslateText = true;
      this.Logic.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Logic.UserDefinedActiveImage")));
      this.Logic.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Logic.UserDefinedImage")));
      this.Logic.Warned = false;
      this.Logic.Click += new System.EventHandler(this.Alarm_Click);
      // 
      // OnBoard
      // 
      this.OnBoard.Active = false;
      this.OnBoard.Alarmed = false;
      this.OnBoard.Auto = false;
      this.OnBoard.BackColor = System.Drawing.Color.WhiteSmoke;
      this.OnBoard.Cursor = System.Windows.Forms.Cursors.Hand;
      this.OnBoard.FillBackColorWithImage = false;
      this.OnBoard.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.OnBoard.FramePresent = false;
      this.OnBoard.HighLightEnable = false;
      this.OnBoard.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.OnBoard.ImagePassword = null;
      this.OnBoard.Inemergency = false;
      this.OnBoard.InLocal = false;
      this.OnBoard.IsButton = true;
      this.OnBoard.IsForceToInvisible = false;
      this.OnBoard.Location = new System.Drawing.Point(424, 202);
      this.OnBoard.Name = "OnBoard";
      this.OnBoard.OnLine = false;
      this.OnBoard.ShowImage = true;
      this.OnBoard.ShowText = true;
      this.OnBoard.Size = new System.Drawing.Size(72, 67);
      this.OnBoard.Switching = false;
      this.OnBoard.TabIndex = 204;
      this.OnBoard.Text = "On Board";
      this.OnBoard.TranslateText = true;
      this.OnBoard.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("OnBoard.UserDefinedActiveImage")));
      this.OnBoard.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("OnBoard.UserDefinedImage")));
      this.OnBoard.Warned = false;
      this.OnBoard.Click += new System.EventHandler(this.Alarm_Click);
      // 
      // Console
      // 
      this.Console.Active = false;
      this.Console.Alarmed = false;
      this.Console.Auto = false;
      this.Console.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Console.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Console.FillBackColorWithImage = false;
      this.Console.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Console.FramePresent = false;
      this.Console.HighLightEnable = false;
      this.Console.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Console.ImagePassword = null;
      this.Console.Inemergency = false;
      this.Console.InLocal = false;
      this.Console.IsButton = true;
      this.Console.IsForceToInvisible = false;
      this.Console.Location = new System.Drawing.Point(508, 202);
      this.Console.Name = "Console";
      this.Console.OnLine = false;
      this.Console.ShowImage = true;
      this.Console.ShowText = true;
      this.Console.Size = new System.Drawing.Size(72, 67);
      this.Console.Switching = false;
      this.Console.TabIndex = 205;
      this.Console.Text = "Console";
      this.Console.TranslateText = true;
      this.Console.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Console.UserDefinedActiveImage")));
      this.Console.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Console.UserDefinedImage")));
      this.Console.Warned = false;
      this.Console.Click += new System.EventHandler(this.Alarm_Click);
      // 
      // Drives
      // 
      this.Drives.Active = false;
      this.Drives.Alarmed = false;
      this.Drives.Auto = false;
      this.Drives.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Drives.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Drives.FillBackColorWithImage = false;
      this.Drives.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Drives.FramePresent = false;
      this.Drives.HighLightEnable = false;
      this.Drives.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Drives.ImagePassword = null;
      this.Drives.Inemergency = false;
      this.Drives.InLocal = false;
      this.Drives.IsButton = true;
      this.Drives.IsForceToInvisible = false;
      this.Drives.Location = new System.Drawing.Point(592, 202);
      this.Drives.Name = "Drives";
      this.Drives.OnLine = false;
      this.Drives.ShowImage = true;
      this.Drives.ShowText = true;
      this.Drives.Size = new System.Drawing.Size(72, 67);
      this.Drives.Switching = false;
      this.Drives.TabIndex = 206;
      this.Drives.Text = "Drives";
      this.Drives.TranslateText = true;
      this.Drives.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Drives.UserDefinedActiveImage")));
      this.Drives.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Drives.UserDefinedImage")));
      this.Drives.Warned = false;
      this.Drives.Click += new System.EventHandler(this.Alarm_Click);
      // 
      // Warnings
      // 
      this.Warnings.Active = false;
      this.Warnings.Alarmed = false;
      this.Warnings.Auto = false;
      this.Warnings.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Warnings.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Warnings.FillBackColorWithImage = false;
      this.Warnings.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Warnings.FramePresent = false;
      this.Warnings.HighLightEnable = false;
      this.Warnings.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Warnings.ImagePassword = null;
      this.Warnings.Inemergency = false;
      this.Warnings.InLocal = false;
      this.Warnings.IsButton = true;
      this.Warnings.IsForceToInvisible = false;
      this.Warnings.Location = new System.Drawing.Point(760, 202);
      this.Warnings.Name = "Warnings";
      this.Warnings.OnLine = false;
      this.Warnings.ShowImage = true;
      this.Warnings.ShowText = true;
      this.Warnings.Size = new System.Drawing.Size(72, 67);
      this.Warnings.Switching = false;
      this.Warnings.TabIndex = 207;
      this.Warnings.Text = "Warnings";
      this.Warnings.TranslateText = true;
      this.Warnings.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Warnings.UserDefinedActiveImage")));
      this.Warnings.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Warnings.UserDefinedImage")));
      this.Warnings.Warned = false;
      this.Warnings.Click += new System.EventHandler(this.Warning_Click);
      // 
      // Software
      // 
      this.Software.Active = false;
      this.Software.Alarmed = false;
      this.Software.Auto = false;
      this.Software.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Software.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Software.FillBackColorWithImage = false;
      this.Software.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.Software.FramePresent = false;
      this.Software.HighLightEnable = false;
      this.Software.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.Software.ImagePassword = null;
      this.Software.Inemergency = false;
      this.Software.InLocal = false;
      this.Software.IsButton = true;
      this.Software.IsForceToInvisible = false;
      this.Software.Location = new System.Drawing.Point(844, 202);
      this.Software.Name = "Software";
      this.Software.OnLine = false;
      this.Software.ShowImage = true;
      this.Software.ShowText = true;
      this.Software.Size = new System.Drawing.Size(72, 67);
      this.Software.Switching = false;
      this.Software.TabIndex = 208;
      this.Software.Text = "Software";
      this.Software.TranslateText = true;
      this.Software.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("Software.UserDefinedActiveImage")));
      this.Software.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("Software.UserDefinedImage")));
      this.Software.Warned = false;
      this.Software.Click += new System.EventHandler(this.Alarm_Click);
      // 
      // PLCComm
      // 
      this.PLCComm.Active = false;
      this.PLCComm.Alarmed = true;
      this.PLCComm.Auto = false;
      this.PLCComm.BackColor = System.Drawing.Color.WhiteSmoke;
      this.PLCComm.Cursor = System.Windows.Forms.Cursors.Hand;
      this.PLCComm.FillBackColorWithImage = false;
      this.PLCComm.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.PLCComm.FramePresent = false;
      this.PLCComm.HighLightEnable = false;
      this.PLCComm.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.PLCComm.ImagePassword = null;
      this.PLCComm.Inemergency = false;
      this.PLCComm.InLocal = false;
      this.PLCComm.IsButton = true;
      this.PLCComm.IsForceToInvisible = false;
      this.PLCComm.Location = new System.Drawing.Point(88, 202);
      this.PLCComm.Name = "PLCComm";
      this.PLCComm.OnLine = false;
      this.PLCComm.ShowImage = true;
      this.PLCComm.ShowText = false;
      this.PLCComm.Size = new System.Drawing.Size(72, 67);
      this.PLCComm.Switching = false;
      this.PLCComm.TabIndex = 209;
      this.PLCComm.TranslateText = true;
      this.PLCComm.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("PLCComm.UserDefinedActiveImage")));
      this.PLCComm.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("PLCComm.UserDefinedImage")));
      this.PLCComm.Warned = false;
      this.PLCComm.Click += new System.EventHandler(this.Twin_Click);
      // 
      // SupervisorComm
      // 
      this.SupervisorComm.Active = false;
      this.SupervisorComm.Alarmed = true;
      this.SupervisorComm.Auto = false;
      this.SupervisorComm.BackColor = System.Drawing.Color.WhiteSmoke;
      this.SupervisorComm.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SupervisorComm.FillBackColorWithImage = false;
      this.SupervisorComm.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.SupervisorComm.FramePresent = false;
      this.SupervisorComm.HighLightEnable = false;
      this.SupervisorComm.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SupervisorComm.ImagePassword = null;
      this.SupervisorComm.Inemergency = false;
      this.SupervisorComm.InLocal = false;
      this.SupervisorComm.IsButton = true;
      this.SupervisorComm.IsForceToInvisible = false;
      this.SupervisorComm.Location = new System.Drawing.Point(5, 202);
      this.SupervisorComm.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
      this.SupervisorComm.Name = "SupervisorComm";
      this.SupervisorComm.OnLine = false;
      this.SupervisorComm.ShowImage = true;
      this.SupervisorComm.ShowText = false;
      this.SupervisorComm.Size = new System.Drawing.Size(72, 67);
      this.SupervisorComm.Switching = false;
      this.SupervisorComm.TabIndex = 210;
      this.SupervisorComm.TranslateText = false;
      this.SupervisorComm.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SupervisorComm.UserDefinedActiveImage")));
      this.SupervisorComm.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SupervisorComm.UserDefinedImage")));
      this.SupervisorComm.Warned = false;
      this.SupervisorComm.Click += new System.EventHandler(this.SupervisorCommIcon_Click);
      // 
      // tlb_Speed
      // 
      this.tlb_Speed.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_Speed.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_Speed.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tlb_Speed.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_Speed.ImagePassword = null;
      this.tlb_Speed.Location = new System.Drawing.Point(825, 148);
      this.tlb_Speed.Name = "tlb_Speed";
      this.tlb_Speed.Size = new System.Drawing.Size(92, 50);
      this.tlb_Speed.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_Speed.TabIndex = 211;
      this.tlb_Speed.Text = "SPEED";
      this.tlb_Speed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_Speed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_Speed.TranslateText = true;
      this.tlb_Speed.Value = "110";
      this.tlb_Speed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_Speed.ValueMultiline = true;
      // 
      // tlb_SlitPositionsStatus
      // 
      this.tlb_SlitPositionsStatus.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_SlitPositionsStatus.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_SlitPositionsStatus.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tlb_SlitPositionsStatus.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_SlitPositionsStatus.ImagePassword = null;
      this.tlb_SlitPositionsStatus.Location = new System.Drawing.Point(572, 89);
      this.tlb_SlitPositionsStatus.Name = "tlb_SlitPositionsStatus";
      this.tlb_SlitPositionsStatus.Size = new System.Drawing.Size(174, 50);
      this.tlb_SlitPositionsStatus.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_SlitPositionsStatus.TabIndex = 212;
      this.tlb_SlitPositionsStatus.Text = "SLIT POSITIONS";
      this.tlb_SlitPositionsStatus.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_SlitPositionsStatus.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_SlitPositionsStatus.TranslateText = true;
      this.tlb_SlitPositionsStatus.Value = "OK";
      this.tlb_SlitPositionsStatus.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_SlitPositionsStatus.ValueMultiline = false;
      this.tlb_SlitPositionsStatus.Click += new System.EventHandler(this.Positions_Click);
      // 
      // tlb_OrderChangeType
      // 
      this.tlb_OrderChangeType.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_OrderChangeType.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_OrderChangeType.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tlb_OrderChangeType.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_OrderChangeType.ImagePassword = null;
      this.tlb_OrderChangeType.Location = new System.Drawing.Point(747, 30);
      this.tlb_OrderChangeType.Name = "tlb_OrderChangeType";
      this.tlb_OrderChangeType.Size = new System.Drawing.Size(170, 50);
      this.tlb_OrderChangeType.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_OrderChangeType.TabIndex = 213;
      this.tlb_OrderChangeType.Text = "AOC TYPE";
      this.tlb_OrderChangeType.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_OrderChangeType.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_OrderChangeType.TranslateText = true;
      this.tlb_OrderChangeType.Value = "KnifeOnly";
      this.tlb_OrderChangeType.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_OrderChangeType.ValueMultiline = false;
      // 
      // tlb_SlitPositioningStatus
      // 
      this.tlb_SlitPositioningStatus.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlb_SlitPositioningStatus.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlb_SlitPositioningStatus.ForeColor = System.Drawing.SystemColors.WindowText;
      this.tlb_SlitPositioningStatus.FrameColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_SlitPositioningStatus.ImagePassword = null;
      this.tlb_SlitPositioningStatus.Location = new System.Drawing.Point(747, 89);
      this.tlb_SlitPositioningStatus.Name = "tlb_SlitPositioningStatus";
      this.tlb_SlitPositioningStatus.Size = new System.Drawing.Size(170, 50);
      this.tlb_SlitPositioningStatus.Style = SyncroC.Controls.TitledLabelStyles.Framed;
      this.tlb_SlitPositioningStatus.TabIndex = 214;
      this.tlb_SlitPositioningStatus.Text = "POSITIONING";
      this.tlb_SlitPositioningStatus.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGreen;
      this.tlb_SlitPositioningStatus.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlb_SlitPositioningStatus.TranslateText = true;
      this.tlb_SlitPositioningStatus.Value = "OK";
      this.tlb_SlitPositioningStatus.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlb_SlitPositioningStatus.ValueMultiline = false;
      // 
      // DeviceNET
      // 
      this.DeviceNET.Active = false;
      this.DeviceNET.Alarmed = false;
      this.DeviceNET.Auto = false;
      this.DeviceNET.BackColor = System.Drawing.Color.WhiteSmoke;
      this.DeviceNET.Cursor = System.Windows.Forms.Cursors.Hand;
      this.DeviceNET.FillBackColorWithImage = false;
      this.DeviceNET.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.DeviceNET.FramePresent = false;
      this.DeviceNET.HighLightEnable = false;
      this.DeviceNET.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.DeviceNET.ImagePassword = null;
      this.DeviceNET.Inemergency = false;
      this.DeviceNET.InLocal = false;
      this.DeviceNET.IsButton = true;
      this.DeviceNET.IsForceToInvisible = false;
      this.DeviceNET.Location = new System.Drawing.Point(676, 202);
      this.DeviceNET.Name = "DeviceNET";
      this.DeviceNET.OnLine = false;
      this.DeviceNET.ShowImage = true;
      this.DeviceNET.ShowText = true;
      this.DeviceNET.Size = new System.Drawing.Size(72, 67);
      this.DeviceNET.Switching = false;
      this.DeviceNET.TabIndex = 215;
      this.DeviceNET.Text = "DeviceNET";
      this.DeviceNET.TranslateText = true;
      this.DeviceNET.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("DeviceNET.UserDefinedActiveImage")));
      this.DeviceNET.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("DeviceNET.UserDefinedImage")));
      this.DeviceNET.Warned = false;
      this.DeviceNET.Click += new System.EventHandler(this.Alarm_Click);
      // 
      // timTandemOrderChange
      // 
      this.timTandemOrderChange.Tick += new System.EventHandler(this.timTandemOrderChange_Tick);
      // 
      // setupStatus_YN
      // 
      this.setupStatus_YN.BackColor = System.Drawing.Color.WhiteSmoke;
      this.setupStatus_YN.Location = new System.Drawing.Point(5, 30);
      this.setupStatus_YN.Name = "setupStatus_YN";
      this.setupStatus_YN.Size = new System.Drawing.Size(566, 170);
      this.setupStatus_YN.TabIndex = 216;
      this.setupStatus_YN.TranslateText = false;
      this.setupStatus_YN.Click += new SyncroC.Controls.SlitterScorerSetupStatus.SlitterScorerStatusHandler(this.setupStatus_YN_Click);
      // 
      // Frm_StatusBarTwin400
      // 
      this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
      this.ClientSize = new System.Drawing.Size(1280, 286);
      this.Controls.Add(this.setupStatus_YN);
      this.Controls.Add(this.shaftStatus_YN);
      this.Controls.Add(this.DeviceNET);
      this.Controls.Add(this.tlb_SlitPositioningStatus);
      this.Controls.Add(this.tlb_OrderChangeType);
      this.Controls.Add(this.tlb_SlitPositionsStatus);
      this.Controls.Add(this.tlb_Speed);
      this.Controls.Add(this.SupervisorComm);
      this.Controls.Add(this.PLCComm);
      this.Controls.Add(this.Software);
      this.Controls.Add(this.Warnings);
      this.Controls.Add(this.Drives);
      this.Controls.Add(this.Console);
      this.Controls.Add(this.OnBoard);
      this.Controls.Add(this.Logic);
      this.Controls.Add(this.Power);
      this.Controls.Add(this.Hardware);
      this.Controls.Add(this.tlb_MachineStatus);
      this.Controls.Add(this.tlb_OrderChangeStatus);
      this.Controls.Add(this.tlb_Queue);
      this.Location = new System.Drawing.Point(0, 0);
      this.Name = "Frm_StatusBarTwin400";
      this.Text = "Frm_StatusBarTwin400";
      this.ResumeLayout(false);

    }
		#endregion

		void SetupListChange(object sender, DBChangedEventArgs args)
		{
			this.UpdateSetupInfo();
		}

		void UpdateSetupInfo()
		{
			int setupCount=DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows.Count;
      this.tlb_Queue.Value = setupCount.ToString();
      this.setupStatus_YN.SetStatus(this.machId, this.machMod, this.machStatus);

      if (setupCount > 1)
      {
        if (setupRows[1][Order.DEL_TANDEM].ToString().Equals(bool.TrueString))
        {
          this.tlb_OrderChangeType.Value = msg.getTranslatedMessage("§SYCC_53");
          this.tlb_OrderChangeType.ForeColor = Color.Orange;
          this.timTandemOrderChange.Enabled = true;
        }
        //agrassi_FI09-11-2009_1
        //else if (this.narrowToWide[1])
        //{
        //  this.tlb_OrderChangeType.Value = msg.getTranslatedMessage("§SYCC_54");
        //  this.tlb_OrderChangeType.ForeColor = Color.Orange;
        //  this.timTandemOrderChange.Enabled = true;
        //}
        //else if (this.wideToNarrow[1])
        //{
        //  this.tlb_OrderChangeType.Value = msg.getTranslatedMessage("§SYCC_55");
        //  this.tlb_OrderChangeType.ForeColor = Color.Orange;
        //  this.timTandemOrderChange.Enabled = true;
        //}
        else if (setupRows[1][Order.DEL_CONSTANT_SYSTEM].ToString().Equals(bool.TrueString))
        {
          this.tlb_OrderChangeType.Value = msg.getTranslatedMessage("§SYCC_51");
          this.tlb_OrderChangeType.ForeColor = Color.Black;
          this.timTandemOrderChange.Enabled = false;
        }
        else if (setupRows[1][Order.DEL_KNIFE_ONLY].ToString().Equals(bool.TrueString))
        {
          this.tlb_OrderChangeType.Value = msg.getTranslatedMessage("§SYCC_52");
          this.tlb_OrderChangeType.ForeColor = Color.Black;
          this.timTandemOrderChange.Enabled = false;
        }
        else
        {
          this.tlb_OrderChangeType.Value = msg.getTranslatedMessage("§SYCC_50");
          this.tlb_OrderChangeType.ForeColor = Color.Black;
          this.timTandemOrderChange.Enabled = false;
        }
      }
      else
        this.tlb_OrderChangeType.Value = "";
    }

		void _ShowShaftStatus(int stSlt1, int stSlt2, int stScr1, int stScr2, int stScrA,
													int stpSlt1, int stpSlt2, int stpScr1, int stpScr2, int stpScrA,
													int id1, int mod1, int id2, int mod2, int id3, int mod3)
		{
      this.machId[0] = id1;
      this.machMod[0] = mod1;
      this.machId[1] = id2;
      this.machMod[1] = mod2;
      this.machId[2] = id3;
      this.machMod[2] = mod3;
      this.shaftStatus_YN.SetStatus(stSlt1, stSlt2, stScr1, stScr2, stScrA,
                          stpSlt1, stpSlt2, stpScr1, stpScr2, stpScrA,
                          this.machId, this.machMod);
      this.setupStatus_YN.SetStatus(this.machId, this.machMod, this.machStatus);
    }

    public override void refreshData()
    {
      SyncroC.DataBase.LineConfig.SlitterScorer ssConfig = DBManager.getDBManager().lineConfig.SlitterScorerConfig;
      this.shaftStatus_YN.Config(ssConfig.SlitterScorerHeader.AuxPresent, ssConfig.SlitterScorerHeader.SlitShafts[0].BrokenBlades != 0, ssConfig.SlitterScorerHeader.SlitShafts[1].BrokenBlades != 0,
															ssConfig.SlitterScorerHeader.SlitShafts[0].Enabled, ssConfig.SlitterScorerHeader.SlitShafts[1].Enabled,
															ssConfig.SlitterScorerHeader.ScoreShafts[0].Enabled, ssConfig.SlitterScorerHeader.ScoreShafts[1].Enabled,
															ssConfig.SlitterScorerHeader.ScoreShafts[2].Enabled);
      this.UpdateSetupInfo();
    }

    private void _SetGap(int gap1, int gap2, int gapA, int actGap1, int actGap2, int actGapA)
    {
      this.shaftStatus_YN.SetGap(gap1, gap2, gapA, actGap1, actGap2, actGapA);
    }

    private void _SetWaitForManualCommandCurr(bool flag)
    {
      this.setupStatus_YN.SetWaitForManualCommandCurr(flag);
    }

    private void _SetWaitForManualCommandNext(bool flag)
    {
      this.setupStatus_YN.SetWaitForManualCommandNext(flag);
    }

    private void _SetPositioningFailed(bool flag)
    {
      this.setupStatus_YN.SetPositioningFailed(flag);
    }

    public void _SetMachineStatus(int ms)
    {
      this.machStatus=(MachineStatus)ms;
      this.setupStatus_YN.SetStatus(this.machId, this.machMod, this.machStatus);
      string sKey = typeof(MachineStatus).Name + "." + Enum.Format(typeof(MachineStatus), ms, "g");
      this.tlb_MachineStatus.Value = this.lgg.getTranslation(sKey);
      Color c = Color.Black;
      if (machStatus == MachineStatus.PLCNotRunning)
        c = Color.OrangeRed;
      else if ((machStatus == MachineStatus.Emergency) || (machStatus == MachineStatus.PLCCommError) ||
               (machStatus == MachineStatus.PositioningFailed))
        c = Color.Red;
      else if ((machStatus == MachineStatus.ResetRunning) || (machStatus == MachineStatus.ResetDone))
        c = Color.Orange;
      else if (machStatus == MachineStatus.ScanningSlits)
        c = Color.Blue;
      else if (machStatus == MachineStatus.Grinding)
        c = Color.Olive;
      else if ((machStatus == MachineStatus.Running) || (machStatus == MachineStatus.Restored) ||
                (machStatus == MachineStatus.Started) || (machStatus == MachineStatus.ReadyToRun))
        c = Color.Green;
      else if ((machStatus == MachineStatus.Maintenance) || (machStatus == MachineStatus.TestIO) ||
                (machStatus == MachineStatus.Simulation) || (machStatus == MachineStatus.AOCSimulation) ||
           (machStatus == MachineStatus.NoGapSimulation) || (machStatus == MachineStatus.Calibration))
        c = Color.Turquoise;
      //AGrassi_FI06/12/2007_1
      else if (machStatus == MachineStatus.TeflonCycle)
        c = Color.Gold;
      //else if (machStatus == MachineStatus.SimulatePLCActive)
      //  c = Color.Fuchsia;
      this.tlb_MachineStatus.ForeColor = c;
      this.UpdateSetupInfo();
    }

    public void _SetOrderChangeStatus(int ocs)
    {
      this.aocStatus = (OrderChangeStatus)ocs;
      string sKey = typeof(OrderChangeStatus).Name + "." + Enum.Format(typeof(OrderChangeStatus), ocs, "g");
      this.tlb_OrderChangeStatus.Value = this.lgg.getTranslation(sKey);
      Color c = Color.Black;
      if ((aocStatus == OrderChangeStatus.ResetAOC) || (aocStatus == OrderChangeStatus.AOCCompleted))
        c = Color.Orange;
      else if (aocStatus == OrderChangeStatus.NotReadyForAOC)
        c = Color.Red;
      else if (aocStatus == OrderChangeStatus.PreparingAOC)
        c = Color.Turquoise;
      else if ((aocStatus == OrderChangeStatus.AOCRunning) || (aocStatus == OrderChangeStatus.NoGapAOC) ||
                (aocStatus == OrderChangeStatus.TandemAOC))
        c = Color.Blue;
      else if (aocStatus == OrderChangeStatus.ReadyForAOC)
        c = Color.Green;
      this.tlb_OrderChangeStatus.ForeColor = c;
    }

    public void _SetSlitPositionsStatus(int sps)
    {
      this.slitPosStatus = (SlitPositionsStatus)sps;
      string sKey = typeof(SlitPositionsStatus).Name+"."+ Enum.Format(typeof(SlitPositionsStatus), sps, "g");
      this.tlb_SlitPositionsStatus.Value = this.lgg.getTranslation(sKey);
      Color c = Color.Green;
      if (this.slitPosStatus > SlitPositionsStatus.PositionsOK)
        c = Color.Red;
      this.tlb_SlitPositionsStatus.ForeColor = c;
    }

    public void _SetSlitPositioningStatus(int spgs)
    {
      this.slitPositioningStatus = (SlitPositioningStatus)spgs;
      //agrassi_FI09-11-2009_1
      //if (((SlitPositioningStatus)spgs) == SlitPositioningStatus.RemoveBoard)
      //  PageManager.getPageManager()._SetMessage("§SYCC_72", true, true);
      string sKey = typeof(SlitPositioningStatus).Name + "." + Enum.Format(typeof(SlitPositioningStatus), spgs, "g");
      this.tlb_SlitPositioningStatus.Value = this.lgg.getTranslation(sKey);
      Color c = Color.Green;
      //AGrassi_FI06/12/2007_1
      if (slitPositioningStatus == SlitPositioningStatus.SimulatePLCActive)
        c = Color.Fuchsia; 
      else if (this.slitPositioningStatus > SlitPositioningStatus.PositioningOK)
        c = Color.Red;
      this.tlb_SlitPositioningStatus.ForeColor = c;
    }

    private void PositioningBlink(int positioningSetup)
    {
      Color presentColor = Color.WhiteSmoke;
      this.tlb_MachineStatus.ForeColor = (this.tlb_MachineStatus.ForeColor == Color.Black ? Color.Gold : Color.Black);
      this.setupStatus_YN.PositioningBlink(positioningSetup);
    }

    private void setupStatus_YN_Click(object sender, SyncroC.Controls.ControlEventArgs.SlitterScorerStatusEventArgs args)
    {
      if (DBManager.getDBManager().syncroConfig.IsTwin400Only())
      {
        if (DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows.Count > args.ind)
        {
          Frm_twin400_ordersSubgroup.externIndex = args.ind;
          Frm_twin400_orderDetails.externPast = false;
          Frm_twin400_orderDetails.externState = Frm_twin400_orderDetails.State.Read;
          askForPage(typeof(Frm_twin400_orderDetails));
        }
      }
    }

    private void Alarm_Click(object sender, EventArgs e)
    {
      askForPage(typeof(Frm_twin400_alarm1));
    }

    //RFAZZI_FI26/01/2011_2. Aggiunta gestione Warning su grafica Twin400 di Syncro. 
    private void Warning_Click(object sender, EventArgs e)
    {
      askForPage(typeof(Frm_twin400_warning1));
    }

    private void Shaft_Click(object sender, EventArgs e)
    {
      Frm_twin400_bladesAndScorers.externIndex = 0;
      askForPage(typeof(Frm_twin400_bladesAndScorers));
    }

    private void SupervisorCommIcon_Click(object sender, System.EventArgs e)
    {
      if (DBManager.getDBManager().syncroConfig.IsTwin400Only())
      {
        if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
        {
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
            PageManager.getRequestCommand(typeof(Frm_StatusBarTwin400)) + "." + Command.REMOTE_CONTROL + "()");
        }
      }
    }

    //agrassi_FI09-11-2009_1
    //private void _SetSetupSituation(bool WtN0, bool WtN1, bool WtN2, bool NtW0, bool NtW1, bool NtW2)
    //{
      //this.wideToNarrow[0] = WtN0;
      //this.wideToNarrow[1] = WtN1;
      //this.wideToNarrow[2] = WtN2;
      //this.narrowToWide[0] = NtW0;
      //this.narrowToWide[1] = NtW1;
      //this.narrowToWide[2] = NtW2;
      //this.UpdateSetupInfo();
    //}

    private void Twin_Click(object sender, EventArgs e)
    {
      //ABELLO_FI14/08/2010_1. Riviste le password
      //if (Accessibility.getAccessibility().Level >= AccessibilityLevel.Medium)
      if (Accessibility.getAccessibility().isLevelAtLeastLow())
        askForPage(typeof(Frm_twin400_machineSettings));
    }

    private void Queue_Click(object sender, EventArgs e)
    {
      if (DBManager.getDBManager().syncroConfig.IsTwin400Only())
        askForPage(typeof(Frm_twin400_ordersList));
    }

    private void Positions_Click(object sender, EventArgs e)
    {
      askForPage(typeof(Frm_twin400_blades));
    }

		//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
		//private void Positioning_Click(object sender, EventArgs e)
		//{
		//  if (Accessibility.getAccessibility().Level >= AccessibilityLevel.Medium)
		//    askForPage(typeof(Frm_twin400_positioning));
		//}

    private void timTandemOrderChange_Tick(object sender, EventArgs e)
    {
      this.tlb_OrderChangeType.ForeColor = (this.tlb_OrderChangeType.ForeColor == Color.Black ? Color.Orange : Color.Black);
    }
   }
} 
