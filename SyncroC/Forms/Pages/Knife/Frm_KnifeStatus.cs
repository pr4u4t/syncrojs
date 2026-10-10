using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Data;

using SyncroC.Controls;
using SyncroC.DataBase.LineConfig;
using SyncroC.DataBase;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.Knife
{
	public class Frm_KnifeStatus : SyncroC.Forms.Pages.Knife.Frm_KnifeGroup
  {
		private SyncroC.Controls.TitledTextBox ttb_CurrentSheets2;
    private SyncroC.Controls.TitledTextBox ttb_CurrentBadCuts2;
    private SyncroC.Controls.TitledTextBox ttb_CurrentDefaultCuts2;
    private SyncroC.Controls.TitledTextBox ttb_CurrentGoodSheets2;
		private PictureBox pct_KnifeLevel2;
    private SyncroC.Controls.TitledTextBox ttb_NextSheets2;
    private SyncroC.Controls.TitledTextBox ttb_CurrentBadCuts1;
    private SyncroC.Controls.TitledTextBox ttb_CurrentGoodSheets1;
    private SyncroC.Controls.TitledTextBox ttb_CurrentDefaultCuts1;
		private SyncroC.Controls.TitledTextBox ttb_CurrentSheets1;
    private SyncroC.Controls.TitledTextBox ttb_CurrentBadCuts0;
    private SyncroC.Controls.TitledTextBox ttb_CurrentGoodSheets0;
    private SyncroC.Controls.TitledTextBox ttb_CurrentDefaultCuts0;
		private SyncroC.Controls.TitledTextBox ttb_CurrentSheets0;
		private SyncroC.Controls.TitledTextBox ttb_NextSheets1;
		private SyncroC.Controls.TitledTextBox ttb_NextSheets0;
    private PictureBox pct_KnifeLevel1;
    private PictureBox pct_KnifeLevel0;
		private System.ComponentModel.IContainer components = null;
    private Title ttl_NextOrder2;
    private Title ttl_NextOrder1;
    private Title ttl_NextOrder0;
    private Title ttl_NextSetup;
    private Title ttl_CurrentSetup;
    private Title ttl_CurrentOrder2;
    private Title ttl_CurrentOrder1;
    private Title ttl_CurrentOrder0;
		private Panel panel2;
		private Panel panel1;
		private Panel panel0;
		private Label lbl_DebugInfo;

		private bool showDebugOptions = false;

		private SyncroC.DataBase.LineConfig.Knife knife;
		private StateButton stb_AutoMan;
		private TitledNumericUpDown nud_CurrentCutLenght2;
		private TitledNumericUpDown nud_NextCutLenght2;
		private TitledNumericUpDown nud_CurrentCutLenght1;
		private TitledNumericUpDown nud_NextCutLenght1;
		private TitledNumericUpDown nud_CurrentCutLenght0;
		private TitledNumericUpDown nud_NextCutLenght0;
		private Button btn_AddBadCuts2;
		private Button btn_AddBadCuts1;
		private Button btn_AddBadCuts0;
    private Button btn_AddExtraSheets2;
    private Button btn_AddExtraSheets1;
    private Button btn_AddExtraSheets0;
		private DataTable ordersTable;
    private int currId=0, currModId=0, nextId=0, nextModId=0;
		private Panel pnl_NextOrder2;
		private Panel pnl_NextOrder1;
		private Panel pnl_NextOrder0;
		private Panel pnl_CurrentOrder2;
		private Panel pnl_CurrentOrder1;
		private StateButton SendCurrentOrder;
		private StateButton SendNextOrder;
		private Panel pnl_CurrentOrder0;

		public Frm_KnifeStatus()
		{
			InitializeComponent();
			DBManager dbm = DBManager.getDBManager();
			knife = dbm.lineConfig.KnifeConfig;
            
            ////ZETA 01
            //if(DBManager.getDBManager().syncroConfig.GeneralSetting.Job == "3703")
            //    ArrangePanels(knife.LevelCount-1);
            //else
                ArrangePanels(knife.LevelCount);

      DBManager.getDBManager().order.OnDBOrderChange+=new Order.DBChangedHandler(OnDBOrderChanged);
			ordersTable = dbm.order.order.Tables[Order.DRYEND_LIST];
			refreshData();

      if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
      {
        this.SendCurrentOrder.IsButton = false;
        this.SendNextOrder.IsButton = false;
      }

			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
			{
				this.btn_AddBadCuts0.Visible = false;
				this.btn_AddBadCuts1.Visible = false;
				this.btn_AddBadCuts2.Visible = false;

				this.btn_AddExtraSheets0.Visible = false;
				this.btn_AddExtraSheets1.Visible = false;
				this.btn_AddExtraSheets2.Visible = false;
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_KnifeStatus));
      this.ttb_CurrentSheets2 = new SyncroC.Controls.TitledTextBox();
      this.ttb_CurrentBadCuts2 = new SyncroC.Controls.TitledTextBox();
      this.ttb_CurrentGoodSheets2 = new SyncroC.Controls.TitledTextBox();
      this.pct_KnifeLevel2 = new System.Windows.Forms.PictureBox();
      this.ttb_NextSheets2 = new SyncroC.Controls.TitledTextBox();
      this.ttb_CurrentDefaultCuts2 = new SyncroC.Controls.TitledTextBox();
      this.ttb_CurrentBadCuts1 = new SyncroC.Controls.TitledTextBox();
      this.ttb_CurrentGoodSheets1 = new SyncroC.Controls.TitledTextBox();
      this.ttb_CurrentDefaultCuts1 = new SyncroC.Controls.TitledTextBox();
      this.ttb_CurrentSheets1 = new SyncroC.Controls.TitledTextBox();
      this.ttb_CurrentBadCuts0 = new SyncroC.Controls.TitledTextBox();
      this.ttb_CurrentGoodSheets0 = new SyncroC.Controls.TitledTextBox();
      this.ttb_CurrentDefaultCuts0 = new SyncroC.Controls.TitledTextBox();
      this.ttb_CurrentSheets0 = new SyncroC.Controls.TitledTextBox();
      this.ttb_NextSheets1 = new SyncroC.Controls.TitledTextBox();
      this.ttb_NextSheets0 = new SyncroC.Controls.TitledTextBox();
      this.pct_KnifeLevel1 = new System.Windows.Forms.PictureBox();
      this.pct_KnifeLevel0 = new System.Windows.Forms.PictureBox();
      this.ttl_NextOrder2 = new SyncroC.Controls.Title();
      this.ttl_NextOrder1 = new SyncroC.Controls.Title();
      this.ttl_NextOrder0 = new SyncroC.Controls.Title();
      this.ttl_NextSetup = new SyncroC.Controls.Title();
      this.ttl_CurrentSetup = new SyncroC.Controls.Title();
      this.ttl_CurrentOrder2 = new SyncroC.Controls.Title();
      this.ttl_CurrentOrder1 = new SyncroC.Controls.Title();
      this.ttl_CurrentOrder0 = new SyncroC.Controls.Title();
      this.panel2 = new System.Windows.Forms.Panel();
      this.pnl_CurrentOrder2 = new System.Windows.Forms.Panel();
      this.btn_AddExtraSheets2 = new System.Windows.Forms.Button();
      this.btn_AddBadCuts2 = new System.Windows.Forms.Button();
      this.nud_CurrentCutLenght2 = new SyncroC.Controls.TitledNumericUpDown();
      this.pnl_NextOrder2 = new System.Windows.Forms.Panel();
      this.nud_NextCutLenght2 = new SyncroC.Controls.TitledNumericUpDown();
      this.panel1 = new System.Windows.Forms.Panel();
      this.pnl_CurrentOrder1 = new System.Windows.Forms.Panel();
      this.btn_AddBadCuts1 = new System.Windows.Forms.Button();
      this.btn_AddExtraSheets1 = new System.Windows.Forms.Button();
      this.nud_CurrentCutLenght1 = new SyncroC.Controls.TitledNumericUpDown();
      this.pnl_NextOrder1 = new System.Windows.Forms.Panel();
      this.nud_NextCutLenght1 = new SyncroC.Controls.TitledNumericUpDown();
      this.panel0 = new System.Windows.Forms.Panel();
      this.pnl_CurrentOrder0 = new System.Windows.Forms.Panel();
      this.btn_AddBadCuts0 = new System.Windows.Forms.Button();
      this.btn_AddExtraSheets0 = new System.Windows.Forms.Button();
      this.nud_CurrentCutLenght0 = new SyncroC.Controls.TitledNumericUpDown();
      this.pnl_NextOrder0 = new System.Windows.Forms.Panel();
      this.nud_NextCutLenght0 = new SyncroC.Controls.TitledNumericUpDown();
      this.lbl_DebugInfo = new System.Windows.Forms.Label();
      this.stb_AutoMan = new SyncroC.Controls.StateButton();
      this.SendCurrentOrder = new SyncroC.Controls.StateButton();
      this.SendNextOrder = new SyncroC.Controls.StateButton();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_KnifeLevel2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_KnifeLevel1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_KnifeLevel0)).BeginInit();
      this.panel2.SuspendLayout();
      this.pnl_CurrentOrder2.SuspendLayout();
      this.pnl_NextOrder2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.pnl_CurrentOrder1.SuspendLayout();
      this.pnl_NextOrder1.SuspendLayout();
      this.panel0.SuspendLayout();
      this.pnl_CurrentOrder0.SuspendLayout();
      this.pnl_NextOrder0.SuspendLayout();
      this.SuspendLayout();
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab3.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // ttb_CurrentSheets2
      // 
      this.ttb_CurrentSheets2.CausesValidation = false;
      this.ttb_CurrentSheets2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentSheets2.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentSheets2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentSheets2.ImagePassword = null;
      this.ttb_CurrentSheets2.Location = new System.Drawing.Point(11, 30);
      this.ttb_CurrentSheets2.MaxChar = 32767;
      this.ttb_CurrentSheets2.Multiline = false;
      this.ttb_CurrentSheets2.Name = "ttb_CurrentSheets2";
      this.ttb_CurrentSheets2.PasswordChar = '\0';
      this.ttb_CurrentSheets2.ReadOnly = true;
      this.ttb_CurrentSheets2.Size = new System.Drawing.Size(174, 60);
      this.ttb_CurrentSheets2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentSheets2.TabIndex = 139;
      this.ttb_CurrentSheets2.Text = "Sheets Required";
      this.ttb_CurrentSheets2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentSheets2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentSheets2.TranslateText = true;
      this.ttb_CurrentSheets2.Value = " ";
      this.ttb_CurrentSheets2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_CurrentBadCuts2
      // 
      this.ttb_CurrentBadCuts2.CausesValidation = false;
      this.ttb_CurrentBadCuts2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentBadCuts2.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentBadCuts2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentBadCuts2.ImagePassword = null;
      this.ttb_CurrentBadCuts2.Location = new System.Drawing.Point(280, 93);
      this.ttb_CurrentBadCuts2.MaxChar = 32767;
      this.ttb_CurrentBadCuts2.Multiline = false;
      this.ttb_CurrentBadCuts2.Name = "ttb_CurrentBadCuts2";
      this.ttb_CurrentBadCuts2.PasswordChar = '\0';
      this.ttb_CurrentBadCuts2.ReadOnly = true;
      this.ttb_CurrentBadCuts2.Size = new System.Drawing.Size(129, 60);
      this.ttb_CurrentBadCuts2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentBadCuts2.TabIndex = 136;
      this.ttb_CurrentBadCuts2.Text = "Bad Sheets";
      this.ttb_CurrentBadCuts2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentBadCuts2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentBadCuts2.TranslateText = true;
      this.ttb_CurrentBadCuts2.Value = " ";
      this.ttb_CurrentBadCuts2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentBadCuts2.Visible = false;
      // 
      // ttb_CurrentGoodSheets2
      // 
      this.ttb_CurrentGoodSheets2.CausesValidation = false;
      this.ttb_CurrentGoodSheets2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentGoodSheets2.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentGoodSheets2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentGoodSheets2.ImagePassword = null;
      this.ttb_CurrentGoodSheets2.Location = new System.Drawing.Point(11, 93);
      this.ttb_CurrentGoodSheets2.MaxChar = 32767;
      this.ttb_CurrentGoodSheets2.Multiline = false;
      this.ttb_CurrentGoodSheets2.Name = "ttb_CurrentGoodSheets2";
      this.ttb_CurrentGoodSheets2.PasswordChar = '\0';
      this.ttb_CurrentGoodSheets2.ReadOnly = true;
      this.ttb_CurrentGoodSheets2.Size = new System.Drawing.Size(129, 60);
      this.ttb_CurrentGoodSheets2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentGoodSheets2.TabIndex = 134;
      this.ttb_CurrentGoodSheets2.Text = "Good Sheets";
      this.ttb_CurrentGoodSheets2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentGoodSheets2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentGoodSheets2.TranslateText = true;
      this.ttb_CurrentGoodSheets2.Value = " ";
      this.ttb_CurrentGoodSheets2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentGoodSheets2.Visible = false;
      // 
      // pct_KnifeLevel2
      // 
      this.pct_KnifeLevel2.Image = global::SyncroC.Properties.Resources.KnifeLevel;
      this.pct_KnifeLevel2.Location = new System.Drawing.Point(363, 0);
      this.pct_KnifeLevel2.Name = "pct_KnifeLevel2";
      this.pct_KnifeLevel2.Size = new System.Drawing.Size(325, 154);
      this.pct_KnifeLevel2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_KnifeLevel2.TabIndex = 0;
      this.pct_KnifeLevel2.TabStop = false;
      // 
      // ttb_NextSheets2
      // 
      this.ttb_NextSheets2.CausesValidation = false;
      this.ttb_NextSheets2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_NextSheets2.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_NextSheets2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_NextSheets2.ImagePassword = null;
      this.ttb_NextSheets2.Location = new System.Drawing.Point(10, 29);
      this.ttb_NextSheets2.MaxChar = 32767;
      this.ttb_NextSheets2.Multiline = false;
      this.ttb_NextSheets2.Name = "ttb_NextSheets2";
      this.ttb_NextSheets2.PasswordChar = '\0';
      this.ttb_NextSheets2.ReadOnly = true;
      this.ttb_NextSheets2.Size = new System.Drawing.Size(158, 60);
      this.ttb_NextSheets2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_NextSheets2.TabIndex = 183;
      this.ttb_NextSheets2.Text = "Sheets Required";
      this.ttb_NextSheets2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_NextSheets2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_NextSheets2.TranslateText = true;
      this.ttb_NextSheets2.Value = " ";
      this.ttb_NextSheets2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_NextSheets2.Visible = false;
      // 
      // ttb_CurrentDefaultCuts2
      // 
      this.ttb_CurrentDefaultCuts2.CausesValidation = false;
      this.ttb_CurrentDefaultCuts2.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentDefaultCuts2.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentDefaultCuts2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentDefaultCuts2.ImagePassword = null;
      this.ttb_CurrentDefaultCuts2.Location = new System.Drawing.Point(146, 93);
      this.ttb_CurrentDefaultCuts2.MaxChar = 32767;
      this.ttb_CurrentDefaultCuts2.Multiline = false;
      this.ttb_CurrentDefaultCuts2.Name = "ttb_CurrentDefaultCuts2";
      this.ttb_CurrentDefaultCuts2.PasswordChar = '\0';
      this.ttb_CurrentDefaultCuts2.ReadOnly = true;
      this.ttb_CurrentDefaultCuts2.Size = new System.Drawing.Size(129, 60);
      this.ttb_CurrentDefaultCuts2.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentDefaultCuts2.TabIndex = 135;
      this.ttb_CurrentDefaultCuts2.Text = "Default Sheets";
      this.ttb_CurrentDefaultCuts2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentDefaultCuts2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentDefaultCuts2.TranslateText = true;
      this.ttb_CurrentDefaultCuts2.Value = " ";
      this.ttb_CurrentDefaultCuts2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentDefaultCuts2.Visible = false;
      // 
      // ttb_CurrentBadCuts1
      // 
      this.ttb_CurrentBadCuts1.CausesValidation = false;
      this.ttb_CurrentBadCuts1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentBadCuts1.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentBadCuts1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentBadCuts1.ImagePassword = null;
      this.ttb_CurrentBadCuts1.Location = new System.Drawing.Point(282, 95);
      this.ttb_CurrentBadCuts1.MaxChar = 32767;
      this.ttb_CurrentBadCuts1.Multiline = false;
      this.ttb_CurrentBadCuts1.Name = "ttb_CurrentBadCuts1";
      this.ttb_CurrentBadCuts1.PasswordChar = '\0';
      this.ttb_CurrentBadCuts1.ReadOnly = true;
      this.ttb_CurrentBadCuts1.Size = new System.Drawing.Size(129, 60);
      this.ttb_CurrentBadCuts1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentBadCuts1.TabIndex = 188;
      this.ttb_CurrentBadCuts1.Text = "Bad Sheets";
      this.ttb_CurrentBadCuts1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentBadCuts1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentBadCuts1.TranslateText = true;
      this.ttb_CurrentBadCuts1.Value = " ";
      this.ttb_CurrentBadCuts1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentBadCuts1.Visible = false;
      // 
      // ttb_CurrentGoodSheets1
      // 
      this.ttb_CurrentGoodSheets1.CausesValidation = false;
      this.ttb_CurrentGoodSheets1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentGoodSheets1.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentGoodSheets1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentGoodSheets1.ImagePassword = null;
      this.ttb_CurrentGoodSheets1.Location = new System.Drawing.Point(11, 95);
      this.ttb_CurrentGoodSheets1.MaxChar = 32767;
      this.ttb_CurrentGoodSheets1.Multiline = false;
      this.ttb_CurrentGoodSheets1.Name = "ttb_CurrentGoodSheets1";
      this.ttb_CurrentGoodSheets1.PasswordChar = '\0';
      this.ttb_CurrentGoodSheets1.ReadOnly = true;
      this.ttb_CurrentGoodSheets1.Size = new System.Drawing.Size(129, 60);
      this.ttb_CurrentGoodSheets1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentGoodSheets1.TabIndex = 186;
      this.ttb_CurrentGoodSheets1.Text = "Good Sheets";
      this.ttb_CurrentGoodSheets1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentGoodSheets1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentGoodSheets1.TranslateText = true;
      this.ttb_CurrentGoodSheets1.Value = " ";
      this.ttb_CurrentGoodSheets1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentGoodSheets1.Visible = false;
      // 
      // ttb_CurrentDefaultCuts1
      // 
      this.ttb_CurrentDefaultCuts1.CausesValidation = false;
      this.ttb_CurrentDefaultCuts1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentDefaultCuts1.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentDefaultCuts1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentDefaultCuts1.ImagePassword = null;
      this.ttb_CurrentDefaultCuts1.Location = new System.Drawing.Point(146, 95);
      this.ttb_CurrentDefaultCuts1.MaxChar = 32767;
      this.ttb_CurrentDefaultCuts1.Multiline = false;
      this.ttb_CurrentDefaultCuts1.Name = "ttb_CurrentDefaultCuts1";
      this.ttb_CurrentDefaultCuts1.PasswordChar = '\0';
      this.ttb_CurrentDefaultCuts1.ReadOnly = true;
      this.ttb_CurrentDefaultCuts1.Size = new System.Drawing.Size(129, 60);
      this.ttb_CurrentDefaultCuts1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentDefaultCuts1.TabIndex = 187;
      this.ttb_CurrentDefaultCuts1.Text = "Default Sheets";
      this.ttb_CurrentDefaultCuts1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentDefaultCuts1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentDefaultCuts1.TranslateText = true;
      this.ttb_CurrentDefaultCuts1.Value = " ";
      this.ttb_CurrentDefaultCuts1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentDefaultCuts1.Visible = false;
      // 
      // ttb_CurrentSheets1
      // 
      this.ttb_CurrentSheets1.CausesValidation = false;
      this.ttb_CurrentSheets1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentSheets1.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentSheets1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentSheets1.ImagePassword = null;
      this.ttb_CurrentSheets1.Location = new System.Drawing.Point(11, 31);
      this.ttb_CurrentSheets1.MaxChar = 32767;
      this.ttb_CurrentSheets1.Multiline = false;
      this.ttb_CurrentSheets1.Name = "ttb_CurrentSheets1";
      this.ttb_CurrentSheets1.PasswordChar = '\0';
      this.ttb_CurrentSheets1.ReadOnly = true;
      this.ttb_CurrentSheets1.Size = new System.Drawing.Size(174, 60);
      this.ttb_CurrentSheets1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentSheets1.TabIndex = 191;
      this.ttb_CurrentSheets1.Text = "Sheets Required";
      this.ttb_CurrentSheets1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentSheets1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentSheets1.TranslateText = true;
      this.ttb_CurrentSheets1.Value = " ";
      this.ttb_CurrentSheets1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_CurrentBadCuts0
      // 
      this.ttb_CurrentBadCuts0.CausesValidation = false;
      this.ttb_CurrentBadCuts0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentBadCuts0.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentBadCuts0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentBadCuts0.ImagePassword = null;
      this.ttb_CurrentBadCuts0.Location = new System.Drawing.Point(282, 95);
      this.ttb_CurrentBadCuts0.MaxChar = 32767;
      this.ttb_CurrentBadCuts0.Multiline = false;
      this.ttb_CurrentBadCuts0.Name = "ttb_CurrentBadCuts0";
      this.ttb_CurrentBadCuts0.PasswordChar = '\0';
      this.ttb_CurrentBadCuts0.ReadOnly = true;
      this.ttb_CurrentBadCuts0.Size = new System.Drawing.Size(129, 60);
      this.ttb_CurrentBadCuts0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentBadCuts0.TabIndex = 194;
      this.ttb_CurrentBadCuts0.Text = "Bad Sheets";
      this.ttb_CurrentBadCuts0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentBadCuts0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentBadCuts0.TranslateText = true;
      this.ttb_CurrentBadCuts0.Value = " ";
      this.ttb_CurrentBadCuts0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentBadCuts0.Visible = false;
      // 
      // ttb_CurrentGoodSheets0
      // 
      this.ttb_CurrentGoodSheets0.CausesValidation = false;
      this.ttb_CurrentGoodSheets0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentGoodSheets0.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentGoodSheets0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentGoodSheets0.ImagePassword = null;
      this.ttb_CurrentGoodSheets0.Location = new System.Drawing.Point(10, 95);
      this.ttb_CurrentGoodSheets0.MaxChar = 32767;
      this.ttb_CurrentGoodSheets0.Multiline = false;
      this.ttb_CurrentGoodSheets0.Name = "ttb_CurrentGoodSheets0";
      this.ttb_CurrentGoodSheets0.PasswordChar = '\0';
      this.ttb_CurrentGoodSheets0.ReadOnly = true;
      this.ttb_CurrentGoodSheets0.Size = new System.Drawing.Size(129, 60);
      this.ttb_CurrentGoodSheets0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentGoodSheets0.TabIndex = 192;
      this.ttb_CurrentGoodSheets0.Text = "Good Sheets";
      this.ttb_CurrentGoodSheets0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentGoodSheets0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentGoodSheets0.TranslateText = true;
      this.ttb_CurrentGoodSheets0.Value = " ";
      this.ttb_CurrentGoodSheets0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentGoodSheets0.Visible = false;
      // 
      // ttb_CurrentDefaultCuts0
      // 
      this.ttb_CurrentDefaultCuts0.CausesValidation = false;
      this.ttb_CurrentDefaultCuts0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentDefaultCuts0.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentDefaultCuts0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentDefaultCuts0.ImagePassword = null;
      this.ttb_CurrentDefaultCuts0.Location = new System.Drawing.Point(145, 95);
      this.ttb_CurrentDefaultCuts0.MaxChar = 32767;
      this.ttb_CurrentDefaultCuts0.Multiline = false;
      this.ttb_CurrentDefaultCuts0.Name = "ttb_CurrentDefaultCuts0";
      this.ttb_CurrentDefaultCuts0.PasswordChar = '\0';
      this.ttb_CurrentDefaultCuts0.ReadOnly = true;
      this.ttb_CurrentDefaultCuts0.Size = new System.Drawing.Size(129, 60);
      this.ttb_CurrentDefaultCuts0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentDefaultCuts0.TabIndex = 193;
      this.ttb_CurrentDefaultCuts0.Text = "Default Sheets";
      this.ttb_CurrentDefaultCuts0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentDefaultCuts0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentDefaultCuts0.TranslateText = true;
      this.ttb_CurrentDefaultCuts0.Value = " ";
      this.ttb_CurrentDefaultCuts0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentDefaultCuts0.Visible = false;
      // 
      // ttb_CurrentSheets0
      // 
      this.ttb_CurrentSheets0.CausesValidation = false;
      this.ttb_CurrentSheets0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_CurrentSheets0.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_CurrentSheets0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentSheets0.ImagePassword = null;
      this.ttb_CurrentSheets0.Location = new System.Drawing.Point(10, 31);
      this.ttb_CurrentSheets0.MaxChar = 32767;
      this.ttb_CurrentSheets0.Multiline = false;
      this.ttb_CurrentSheets0.Name = "ttb_CurrentSheets0";
      this.ttb_CurrentSheets0.PasswordChar = '\0';
      this.ttb_CurrentSheets0.ReadOnly = true;
      this.ttb_CurrentSheets0.Size = new System.Drawing.Size(174, 60);
      this.ttb_CurrentSheets0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_CurrentSheets0.TabIndex = 197;
      this.ttb_CurrentSheets0.Text = "Sheets Required";
      this.ttb_CurrentSheets0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_CurrentSheets0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_CurrentSheets0.TranslateText = true;
      this.ttb_CurrentSheets0.Value = " ";
      this.ttb_CurrentSheets0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_NextSheets1
      // 
      this.ttb_NextSheets1.CausesValidation = false;
      this.ttb_NextSheets1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_NextSheets1.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_NextSheets1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_NextSheets1.ImagePassword = null;
      this.ttb_NextSheets1.Location = new System.Drawing.Point(10, 29);
      this.ttb_NextSheets1.MaxChar = 32767;
      this.ttb_NextSheets1.Multiline = false;
      this.ttb_NextSheets1.Name = "ttb_NextSheets1";
      this.ttb_NextSheets1.PasswordChar = '\0';
      this.ttb_NextSheets1.ReadOnly = true;
      this.ttb_NextSheets1.Size = new System.Drawing.Size(158, 60);
      this.ttb_NextSheets1.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_NextSheets1.TabIndex = 200;
      this.ttb_NextSheets1.Text = "Sheets Required";
      this.ttb_NextSheets1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_NextSheets1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_NextSheets1.TranslateText = true;
      this.ttb_NextSheets1.Value = " ";
      this.ttb_NextSheets1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_NextSheets1.Visible = false;
      // 
      // ttb_NextSheets0
      // 
      this.ttb_NextSheets0.CausesValidation = false;
      this.ttb_NextSheets0.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_NextSheets0.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_NextSheets0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_NextSheets0.ImagePassword = null;
      this.ttb_NextSheets0.Location = new System.Drawing.Point(10, 29);
      this.ttb_NextSheets0.MaxChar = 32767;
      this.ttb_NextSheets0.Multiline = false;
      this.ttb_NextSheets0.Name = "ttb_NextSheets0";
      this.ttb_NextSheets0.PasswordChar = '\0';
      this.ttb_NextSheets0.ReadOnly = true;
      this.ttb_NextSheets0.Size = new System.Drawing.Size(158, 60);
      this.ttb_NextSheets0.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_NextSheets0.TabIndex = 203;
      this.ttb_NextSheets0.Text = "Sheets Required";
      this.ttb_NextSheets0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_NextSheets0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_NextSheets0.TranslateText = true;
      this.ttb_NextSheets0.Value = " ";
      this.ttb_NextSheets0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_NextSheets0.Visible = false;
      // 
      // pct_KnifeLevel1
      // 
      this.pct_KnifeLevel1.Image = global::SyncroC.Properties.Resources.KnifeLevel;
      this.pct_KnifeLevel1.Location = new System.Drawing.Point(363, 0);
      this.pct_KnifeLevel1.Name = "pct_KnifeLevel1";
      this.pct_KnifeLevel1.Size = new System.Drawing.Size(325, 154);
      this.pct_KnifeLevel1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_KnifeLevel1.TabIndex = 204;
      this.pct_KnifeLevel1.TabStop = false;
      // 
      // pct_KnifeLevel0
      // 
      this.pct_KnifeLevel0.Image = global::SyncroC.Properties.Resources.KnifeLevel;
      this.pct_KnifeLevel0.Location = new System.Drawing.Point(363, 0);
      this.pct_KnifeLevel0.Name = "pct_KnifeLevel0";
      this.pct_KnifeLevel0.Size = new System.Drawing.Size(325, 154);
      this.pct_KnifeLevel0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_KnifeLevel0.TabIndex = 205;
      this.pct_KnifeLevel0.TabStop = false;
      // 
      // ttl_NextOrder2
      // 
      this.ttl_NextOrder2.BackColor = System.Drawing.Color.Black;
      this.ttl_NextOrder2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextOrder2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextOrder2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextOrder2.Location = new System.Drawing.Point(1, 1);
      this.ttl_NextOrder2.Name = "ttl_NextOrder2";
      this.ttl_NextOrder2.Size = new System.Drawing.Size(330, 154);
      this.ttl_NextOrder2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_NextOrder2.TabIndex = 206;
      this.ttl_NextOrder2.TranslateText = false;
      // 
      // ttl_NextOrder1
      // 
      this.ttl_NextOrder1.BackColor = System.Drawing.Color.Black;
      this.ttl_NextOrder1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextOrder1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextOrder1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextOrder1.Location = new System.Drawing.Point(1, 1);
      this.ttl_NextOrder1.Name = "ttl_NextOrder1";
      this.ttl_NextOrder1.Size = new System.Drawing.Size(330, 154);
      this.ttl_NextOrder1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_NextOrder1.TabIndex = 207;
      this.ttl_NextOrder1.TranslateText = false;
      // 
      // ttl_NextOrder0
      // 
      this.ttl_NextOrder0.BackColor = System.Drawing.Color.Black;
      this.ttl_NextOrder0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextOrder0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextOrder0.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextOrder0.Location = new System.Drawing.Point(1, 1);
      this.ttl_NextOrder0.Name = "ttl_NextOrder0";
      this.ttl_NextOrder0.Size = new System.Drawing.Size(330, 154);
      this.ttl_NextOrder0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_NextOrder0.TabIndex = 208;
      this.ttl_NextOrder0.TranslateText = false;
      // 
      // ttl_NextSetup
      // 
      this.ttl_NextSetup.BackColor = System.Drawing.Color.Black;
      this.ttl_NextSetup.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_NextSetup.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_NextSetup.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_NextSetup.Location = new System.Drawing.Point(30, 192);
      this.ttl_NextSetup.Name = "ttl_NextSetup";
      this.ttl_NextSetup.Size = new System.Drawing.Size(346, 519);
      this.ttl_NextSetup.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_NextSetup.TabIndex = 209;
      this.ttl_NextSetup.Text = "NEXT SETUP: Empty";
      this.ttl_NextSetup.TranslateText = true;
      // 
      // ttl_CurrentSetup
      // 
      this.ttl_CurrentSetup.BackColor = System.Drawing.Color.Black;
      this.ttl_CurrentSetup.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentSetup.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentSetup.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_CurrentSetup.Location = new System.Drawing.Point(783, 192);
      this.ttl_CurrentSetup.Name = "ttl_CurrentSetup";
      this.ttl_CurrentSetup.Size = new System.Drawing.Size(419, 519);
      this.ttl_CurrentSetup.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_CurrentSetup.TabIndex = 210;
      this.ttl_CurrentSetup.Text = "CURRENT SETUP: Empty";
      this.ttl_CurrentSetup.TranslateText = true;
      // 
      // ttl_CurrentOrder2
      // 
      this.ttl_CurrentOrder2.BackColor = System.Drawing.Color.Black;
      this.ttl_CurrentOrder2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentOrder2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentOrder2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_CurrentOrder2.Location = new System.Drawing.Point(5, 1);
      this.ttl_CurrentOrder2.Name = "ttl_CurrentOrder2";
      this.ttl_CurrentOrder2.Size = new System.Drawing.Size(410, 154);
      this.ttl_CurrentOrder2.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_CurrentOrder2.TabIndex = 211;
      this.ttl_CurrentOrder2.TranslateText = false;
      // 
      // ttl_CurrentOrder1
      // 
      this.ttl_CurrentOrder1.BackColor = System.Drawing.Color.Black;
      this.ttl_CurrentOrder1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentOrder1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentOrder1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_CurrentOrder1.Location = new System.Drawing.Point(1, 1);
      this.ttl_CurrentOrder1.Name = "ttl_CurrentOrder1";
      this.ttl_CurrentOrder1.Size = new System.Drawing.Size(413, 154);
      this.ttl_CurrentOrder1.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_CurrentOrder1.TabIndex = 212;
      this.ttl_CurrentOrder1.TranslateText = false;
      // 
      // ttl_CurrentOrder0
      // 
      this.ttl_CurrentOrder0.BackColor = System.Drawing.Color.Black;
      this.ttl_CurrentOrder0.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_CurrentOrder0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_CurrentOrder0.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_CurrentOrder0.Location = new System.Drawing.Point(1, 1);
      this.ttl_CurrentOrder0.Name = "ttl_CurrentOrder0";
      this.ttl_CurrentOrder0.Size = new System.Drawing.Size(411, 154);
      this.ttl_CurrentOrder0.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_CurrentOrder0.TabIndex = 213;
      this.ttl_CurrentOrder0.TranslateText = false;
      // 
      // panel2
      // 
      this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.panel2.Controls.Add(this.pnl_CurrentOrder2);
      this.panel2.Controls.Add(this.pnl_NextOrder2);
      this.panel2.Controls.Add(this.pct_KnifeLevel2);
      this.panel2.Location = new System.Drawing.Point(42, 224);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(1158, 154);
      this.panel2.TabIndex = 214;
      // 
      // pnl_CurrentOrder2
      // 
      this.pnl_CurrentOrder2.Controls.Add(this.ttl_CurrentOrder2);
      this.pnl_CurrentOrder2.Controls.Add(this.btn_AddExtraSheets2);
      this.pnl_CurrentOrder2.Controls.Add(this.ttb_CurrentDefaultCuts2);
      this.pnl_CurrentOrder2.Controls.Add(this.btn_AddBadCuts2);
      this.pnl_CurrentOrder2.Controls.Add(this.ttb_CurrentGoodSheets2);
      this.pnl_CurrentOrder2.Controls.Add(this.nud_CurrentCutLenght2);
      this.pnl_CurrentOrder2.Controls.Add(this.ttb_CurrentBadCuts2);
      this.pnl_CurrentOrder2.Controls.Add(this.ttb_CurrentSheets2);
      this.pnl_CurrentOrder2.Location = new System.Drawing.Point(748, 0);
      this.pnl_CurrentOrder2.Name = "pnl_CurrentOrder2";
      this.pnl_CurrentOrder2.Size = new System.Drawing.Size(410, 154);
      this.pnl_CurrentOrder2.TabIndex = 219;
      // 
      // btn_AddExtraSheets2
      // 
      this.btn_AddExtraSheets2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_AddExtraSheets2.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btn_AddExtraSheets2.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_AddExtraSheets2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
      this.btn_AddExtraSheets2.Location = new System.Drawing.Point(139, 23);
      this.btn_AddExtraSheets2.Name = "btn_AddExtraSheets2";
      this.btn_AddExtraSheets2.Size = new System.Drawing.Size(40, 25);
      this.btn_AddExtraSheets2.TabIndex = 217;
      this.btn_AddExtraSheets2.Text = "+";
      this.btn_AddExtraSheets2.Click += new System.EventHandler(this.btn_AddExtraSheets2_Click);
      // 
      // btn_AddBadCuts2
      // 
      this.btn_AddBadCuts2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_AddBadCuts2.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btn_AddBadCuts2.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_AddBadCuts2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
      this.btn_AddBadCuts2.Location = new System.Drawing.Point(370, 86);
      this.btn_AddBadCuts2.Name = "btn_AddBadCuts2";
      this.btn_AddBadCuts2.Size = new System.Drawing.Size(40, 25);
      this.btn_AddBadCuts2.TabIndex = 216;
      this.btn_AddBadCuts2.Text = "+";
      this.btn_AddBadCuts2.Click += new System.EventHandler(this.btn_AddBadCuts2_Click);
      // 
      // nud_CurrentCutLenght2
      // 
      this.nud_CurrentCutLenght2.DecimalPlaces = 0;
      this.nud_CurrentCutLenght2.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.nud_CurrentCutLenght2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CurrentCutLenght2.ImagePassword = null;
      this.nud_CurrentCutLenght2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CurrentCutLenght2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_CurrentCutLenght2.Location = new System.Drawing.Point(190, 29);
      this.nud_CurrentCutLenght2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_CurrentCutLenght2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_CurrentCutLenght2.Name = "nud_CurrentCutLenght2";
      this.nud_CurrentCutLenght2.ReadOnly = true;
      this.nud_CurrentCutLenght2.Size = new System.Drawing.Size(139, 60);
      this.nud_CurrentCutLenght2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_CurrentCutLenght2.TabIndex = 215;
      this.nud_CurrentCutLenght2.Text = "Cut Length";
      this.nud_CurrentCutLenght2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_CurrentCutLenght2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CurrentCutLenght2.TranslateText = true;
      this.nud_CurrentCutLenght2.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CurrentCutLenght2.Value = "";
      this.nud_CurrentCutLenght2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_CurrentCutLenght2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // pnl_NextOrder2
      // 
      this.pnl_NextOrder2.Controls.Add(this.ttl_NextOrder2);
      this.pnl_NextOrder2.Controls.Add(this.ttb_NextSheets2);
      this.pnl_NextOrder2.Controls.Add(this.nud_NextCutLenght2);
      this.pnl_NextOrder2.Location = new System.Drawing.Point(0, 0);
      this.pnl_NextOrder2.Name = "pnl_NextOrder2";
      this.pnl_NextOrder2.Size = new System.Drawing.Size(330, 154);
      this.pnl_NextOrder2.TabIndex = 218;
      // 
      // nud_NextCutLenght2
      // 
      this.nud_NextCutLenght2.DecimalPlaces = 0;
      this.nud_NextCutLenght2.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.nud_NextCutLenght2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_NextCutLenght2.ImagePassword = null;
      this.nud_NextCutLenght2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_NextCutLenght2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_NextCutLenght2.Location = new System.Drawing.Point(206, 28);
      this.nud_NextCutLenght2.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_NextCutLenght2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_NextCutLenght2.Name = "nud_NextCutLenght2";
      this.nud_NextCutLenght2.ReadOnly = true;
      this.nud_NextCutLenght2.Size = new System.Drawing.Size(119, 60);
      this.nud_NextCutLenght2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_NextCutLenght2.TabIndex = 212;
      this.nud_NextCutLenght2.Text = "Cut Length";
      this.nud_NextCutLenght2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_NextCutLenght2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_NextCutLenght2.TranslateText = true;
      this.nud_NextCutLenght2.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_NextCutLenght2.Value = "";
      this.nud_NextCutLenght2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_NextCutLenght2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_NextCutLenght2.Visible = false;
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.panel1.Controls.Add(this.pnl_CurrentOrder1);
      this.panel1.Controls.Add(this.pnl_NextOrder1);
      this.panel1.Controls.Add(this.pct_KnifeLevel1);
      this.panel1.Location = new System.Drawing.Point(42, 390);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1158, 154);
      this.panel1.TabIndex = 215;
      // 
      // pnl_CurrentOrder1
      // 
      this.pnl_CurrentOrder1.Controls.Add(this.btn_AddBadCuts1);
      this.pnl_CurrentOrder1.Controls.Add(this.ttb_CurrentBadCuts1);
      this.pnl_CurrentOrder1.Controls.Add(this.ttl_CurrentOrder1);
      this.pnl_CurrentOrder1.Controls.Add(this.btn_AddExtraSheets1);
      this.pnl_CurrentOrder1.Controls.Add(this.ttb_CurrentGoodSheets1);
      this.pnl_CurrentOrder1.Controls.Add(this.nud_CurrentCutLenght1);
      this.pnl_CurrentOrder1.Controls.Add(this.ttb_CurrentDefaultCuts1);
      this.pnl_CurrentOrder1.Controls.Add(this.ttb_CurrentSheets1);
      this.pnl_CurrentOrder1.Location = new System.Drawing.Point(748, 1);
      this.pnl_CurrentOrder1.Name = "pnl_CurrentOrder1";
      this.pnl_CurrentOrder1.Size = new System.Drawing.Size(410, 154);
      this.pnl_CurrentOrder1.TabIndex = 218;
      // 
      // btn_AddBadCuts1
      // 
      this.btn_AddBadCuts1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_AddBadCuts1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btn_AddBadCuts1.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_AddBadCuts1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
      this.btn_AddBadCuts1.Location = new System.Drawing.Point(369, 88);
      this.btn_AddBadCuts1.Name = "btn_AddBadCuts1";
      this.btn_AddBadCuts1.Size = new System.Drawing.Size(40, 25);
      this.btn_AddBadCuts1.TabIndex = 217;
      this.btn_AddBadCuts1.Text = "+";
      this.btn_AddBadCuts1.Click += new System.EventHandler(this.btn_AddBadCuts1_Click);
      // 
      // btn_AddExtraSheets1
      // 
      this.btn_AddExtraSheets1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_AddExtraSheets1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btn_AddExtraSheets1.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_AddExtraSheets1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
      this.btn_AddExtraSheets1.Location = new System.Drawing.Point(139, 24);
      this.btn_AddExtraSheets1.Name = "btn_AddExtraSheets1";
      this.btn_AddExtraSheets1.Size = new System.Drawing.Size(40, 25);
      this.btn_AddExtraSheets1.TabIndex = 218;
      this.btn_AddExtraSheets1.Text = "+";
      this.btn_AddExtraSheets1.Click += new System.EventHandler(this.btn_AddExtraSheets1_Click);
      // 
      // nud_CurrentCutLenght1
      // 
      this.nud_CurrentCutLenght1.DecimalPlaces = 0;
      this.nud_CurrentCutLenght1.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.nud_CurrentCutLenght1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CurrentCutLenght1.ImagePassword = null;
      this.nud_CurrentCutLenght1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CurrentCutLenght1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_CurrentCutLenght1.Location = new System.Drawing.Point(189, 30);
      this.nud_CurrentCutLenght1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_CurrentCutLenght1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_CurrentCutLenght1.Name = "nud_CurrentCutLenght1";
      this.nud_CurrentCutLenght1.ReadOnly = true;
      this.nud_CurrentCutLenght1.Size = new System.Drawing.Size(139, 60);
      this.nud_CurrentCutLenght1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_CurrentCutLenght1.TabIndex = 216;
      this.nud_CurrentCutLenght1.Text = "Cut Length";
      this.nud_CurrentCutLenght1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_CurrentCutLenght1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CurrentCutLenght1.TranslateText = true;
      this.nud_CurrentCutLenght1.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CurrentCutLenght1.Value = "";
      this.nud_CurrentCutLenght1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_CurrentCutLenght1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // pnl_NextOrder1
      // 
      this.pnl_NextOrder1.Controls.Add(this.ttl_NextOrder1);
      this.pnl_NextOrder1.Controls.Add(this.ttb_NextSheets1);
      this.pnl_NextOrder1.Controls.Add(this.nud_NextCutLenght1);
      this.pnl_NextOrder1.Location = new System.Drawing.Point(1, 0);
      this.pnl_NextOrder1.Name = "pnl_NextOrder1";
      this.pnl_NextOrder1.Size = new System.Drawing.Size(330, 154);
      this.pnl_NextOrder1.TabIndex = 218;
      // 
      // nud_NextCutLenght1
      // 
      this.nud_NextCutLenght1.DecimalPlaces = 0;
      this.nud_NextCutLenght1.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.nud_NextCutLenght1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_NextCutLenght1.ImagePassword = null;
      this.nud_NextCutLenght1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_NextCutLenght1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_NextCutLenght1.Location = new System.Drawing.Point(205, 28);
      this.nud_NextCutLenght1.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_NextCutLenght1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_NextCutLenght1.Name = "nud_NextCutLenght1";
      this.nud_NextCutLenght1.ReadOnly = true;
      this.nud_NextCutLenght1.Size = new System.Drawing.Size(119, 60);
      this.nud_NextCutLenght1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_NextCutLenght1.TabIndex = 213;
      this.nud_NextCutLenght1.Text = "Cut Length";
      this.nud_NextCutLenght1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_NextCutLenght1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_NextCutLenght1.TranslateText = true;
      this.nud_NextCutLenght1.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_NextCutLenght1.Value = "";
      this.nud_NextCutLenght1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_NextCutLenght1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_NextCutLenght1.Visible = false;
      // 
      // panel0
      // 
      this.panel0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.panel0.Controls.Add(this.pnl_CurrentOrder0);
      this.panel0.Controls.Add(this.pnl_NextOrder0);
      this.panel0.Controls.Add(this.pct_KnifeLevel0);
      this.panel0.Location = new System.Drawing.Point(42, 557);
      this.panel0.Name = "panel0";
      this.panel0.Size = new System.Drawing.Size(1158, 154);
      this.panel0.TabIndex = 215;
      // 
      // pnl_CurrentOrder0
      // 
      this.pnl_CurrentOrder0.Controls.Add(this.btn_AddBadCuts0);
      this.pnl_CurrentOrder0.Controls.Add(this.ttl_CurrentOrder0);
      this.pnl_CurrentOrder0.Controls.Add(this.btn_AddExtraSheets0);
      this.pnl_CurrentOrder0.Controls.Add(this.ttb_CurrentBadCuts0);
      this.pnl_CurrentOrder0.Controls.Add(this.ttb_CurrentGoodSheets0);
      this.pnl_CurrentOrder0.Controls.Add(this.nud_CurrentCutLenght0);
      this.pnl_CurrentOrder0.Controls.Add(this.ttb_CurrentDefaultCuts0);
      this.pnl_CurrentOrder0.Controls.Add(this.ttb_CurrentSheets0);
      this.pnl_CurrentOrder0.Location = new System.Drawing.Point(748, 1);
      this.pnl_CurrentOrder0.Name = "pnl_CurrentOrder0";
      this.pnl_CurrentOrder0.Size = new System.Drawing.Size(410, 154);
      this.pnl_CurrentOrder0.TabIndex = 218;
      // 
      // btn_AddBadCuts0
      // 
      this.btn_AddBadCuts0.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_AddBadCuts0.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btn_AddBadCuts0.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_AddBadCuts0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
      this.btn_AddBadCuts0.Location = new System.Drawing.Point(370, 88);
      this.btn_AddBadCuts0.Name = "btn_AddBadCuts0";
      this.btn_AddBadCuts0.Size = new System.Drawing.Size(40, 25);
      this.btn_AddBadCuts0.TabIndex = 218;
      this.btn_AddBadCuts0.Text = "+";
      this.btn_AddBadCuts0.Click += new System.EventHandler(this.btn_AddBadCuts0_Click);
      // 
      // btn_AddExtraSheets0
      // 
      this.btn_AddExtraSheets0.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btn_AddExtraSheets0.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.btn_AddExtraSheets0.Font = new System.Drawing.Font("Arial Unicode MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.btn_AddExtraSheets0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
      this.btn_AddExtraSheets0.Location = new System.Drawing.Point(139, 24);
      this.btn_AddExtraSheets0.Name = "btn_AddExtraSheets0";
      this.btn_AddExtraSheets0.Size = new System.Drawing.Size(40, 25);
      this.btn_AddExtraSheets0.TabIndex = 219;
      this.btn_AddExtraSheets0.Text = "+";
      this.btn_AddExtraSheets0.Click += new System.EventHandler(this.btn_AddExtraSheets0_Click);
      // 
      // nud_CurrentCutLenght0
      // 
      this.nud_CurrentCutLenght0.DecimalPlaces = 0;
      this.nud_CurrentCutLenght0.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.nud_CurrentCutLenght0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CurrentCutLenght0.ImagePassword = null;
      this.nud_CurrentCutLenght0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_CurrentCutLenght0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_CurrentCutLenght0.Location = new System.Drawing.Point(190, 30);
      this.nud_CurrentCutLenght0.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_CurrentCutLenght0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_CurrentCutLenght0.Name = "nud_CurrentCutLenght0";
      this.nud_CurrentCutLenght0.ReadOnly = true;
      this.nud_CurrentCutLenght0.Size = new System.Drawing.Size(139, 60);
      this.nud_CurrentCutLenght0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_CurrentCutLenght0.TabIndex = 217;
      this.nud_CurrentCutLenght0.Text = "Cut Length";
      this.nud_CurrentCutLenght0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_CurrentCutLenght0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_CurrentCutLenght0.TranslateText = true;
      this.nud_CurrentCutLenght0.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_CurrentCutLenght0.Value = "";
      this.nud_CurrentCutLenght0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_CurrentCutLenght0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // pnl_NextOrder0
      // 
      this.pnl_NextOrder0.Controls.Add(this.ttl_NextOrder0);
      this.pnl_NextOrder0.Controls.Add(this.ttb_NextSheets0);
      this.pnl_NextOrder0.Controls.Add(this.nud_NextCutLenght0);
      this.pnl_NextOrder0.Location = new System.Drawing.Point(1, 0);
      this.pnl_NextOrder0.Name = "pnl_NextOrder0";
      this.pnl_NextOrder0.Size = new System.Drawing.Size(330, 154);
      this.pnl_NextOrder0.TabIndex = 219;
      // 
      // nud_NextCutLenght0
      // 
      this.nud_NextCutLenght0.DecimalPlaces = 0;
      this.nud_NextCutLenght0.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
      this.nud_NextCutLenght0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_NextCutLenght0.ImagePassword = null;
      this.nud_NextCutLenght0.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nud_NextCutLenght0.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.nud_NextCutLenght0.Location = new System.Drawing.Point(205, 28);
      this.nud_NextCutLenght0.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
      this.nud_NextCutLenght0.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nud_NextCutLenght0.Name = "nud_NextCutLenght0";
      this.nud_NextCutLenght0.ReadOnly = true;
      this.nud_NextCutLenght0.Size = new System.Drawing.Size(119, 60);
      this.nud_NextCutLenght0.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.nud_NextCutLenght0.TabIndex = 214;
      this.nud_NextCutLenght0.Text = "Cut Length";
      this.nud_NextCutLenght0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_NextCutLenght0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.nud_NextCutLenght0.TranslateText = true;
      this.nud_NextCutLenght0.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.nud_NextCutLenght0.Value = "";
      this.nud_NextCutLenght0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.nud_NextCutLenght0.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.nud_NextCutLenght0.Visible = false;
      // 
      // lbl_DebugInfo
      // 
      this.lbl_DebugInfo.BackColor = System.Drawing.Color.Gainsboro;
      this.lbl_DebugInfo.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.lbl_DebugInfo.Location = new System.Drawing.Point(305, 56);
      this.lbl_DebugInfo.Name = "lbl_DebugInfo";
      this.lbl_DebugInfo.Size = new System.Drawing.Size(746, 23);
      this.lbl_DebugInfo.TabIndex = 216;
      this.lbl_DebugInfo.Text = "Press: CTRL+F1..F3 to show 1..3 levels; CTRL+F4 to show LineConfig levels";
      this.lbl_DebugInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_DebugInfo.Visible = false;
      // 
      // stb_AutoMan
      // 
      this.stb_AutoMan.Active = false;
      this.stb_AutoMan.Alarmed = false;
      this.stb_AutoMan.Auto = false;
      this.stb_AutoMan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.stb_AutoMan.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.stb_AutoMan.FillBackColorWithImage = false;
      this.stb_AutoMan.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
      this.stb_AutoMan.FramePresent = false;
      this.stb_AutoMan.HighLightEnable = false;
      this.stb_AutoMan.Image = SyncroC.Controls.StateButtonImage.Auto_Manual;
      this.stb_AutoMan.ImagePassword = null;
      this.stb_AutoMan.Inemergency = false;
      this.stb_AutoMan.InLocal = false;
      this.stb_AutoMan.IsButton = false;
      this.stb_AutoMan.IsForceToInvisible = false;
      this.stb_AutoMan.Location = new System.Drawing.Point(500, 170);
      this.stb_AutoMan.Name = "stb_AutoMan";
      this.stb_AutoMan.OnLine = false;
      this.stb_AutoMan.ShowImage = true;
      this.stb_AutoMan.ShowText = false;
      this.stb_AutoMan.Size = new System.Drawing.Size(111, 53);
      this.stb_AutoMan.Switching = false;
      this.stb_AutoMan.TabIndex = 217;
      this.stb_AutoMan.Text = "stateButton1";
      this.stb_AutoMan.TranslateText = false;
      this.stb_AutoMan.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("stb_AutoMan.UserDefinedActiveImage")));
      this.stb_AutoMan.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("stb_AutoMan.UserDefinedImage")));
      this.stb_AutoMan.Warned = false;
      // 
      // SendCurrentOrder
      // 
      this.SendCurrentOrder.Active = false;
      this.SendCurrentOrder.Alarmed = false;
      this.SendCurrentOrder.Auto = false;
      this.SendCurrentOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendCurrentOrder.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendCurrentOrder.FillBackColorWithImage = true;
      this.SendCurrentOrder.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendCurrentOrder.ForeColor = System.Drawing.Color.Black;
      this.SendCurrentOrder.FramePresent = true;
      this.SendCurrentOrder.HighLightEnable = false;
      this.SendCurrentOrder.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendCurrentOrder.ImagePassword = null;
      this.SendCurrentOrder.Inemergency = false;
      this.SendCurrentOrder.InLocal = false;
      this.SendCurrentOrder.IsButton = true;
      this.SendCurrentOrder.IsForceToInvisible = false;
      this.SendCurrentOrder.Location = new System.Drawing.Point(1102, 168);
      this.SendCurrentOrder.Name = "SendCurrentOrder";
      this.SendCurrentOrder.OnLine = false;
      this.SendCurrentOrder.ShowImage = true;
      this.SendCurrentOrder.ShowText = false;
      this.SendCurrentOrder.Size = new System.Drawing.Size(100, 51);
      this.SendCurrentOrder.Switching = false;
      this.SendCurrentOrder.TabIndex = 218;
      this.SendCurrentOrder.Text = "1";
      this.SendCurrentOrder.TranslateText = false;
      this.SendCurrentOrder.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendCurrentOrder.UserDefinedActiveImage")));
      this.SendCurrentOrder.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendCurrentOrder.UserDefinedImage")));
      this.SendCurrentOrder.Warned = false;
      this.SendCurrentOrder.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // SendNextOrder
      // 
      this.SendNextOrder.Active = false;
      this.SendNextOrder.Alarmed = false;
      this.SendNextOrder.Auto = false;
      this.SendNextOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
      this.SendNextOrder.Cursor = System.Windows.Forms.Cursors.Hand;
      this.SendNextOrder.FillBackColorWithImage = true;
      this.SendNextOrder.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SendNextOrder.ForeColor = System.Drawing.Color.Black;
      this.SendNextOrder.FramePresent = true;
      this.SendNextOrder.HighLightEnable = false;
      this.SendNextOrder.Image = SyncroC.Controls.StateButtonImage.UserDefined;
      this.SendNextOrder.ImagePassword = null;
      this.SendNextOrder.Inemergency = false;
      this.SendNextOrder.InLocal = false;
      this.SendNextOrder.IsButton = true;
      this.SendNextOrder.IsForceToInvisible = false;
      this.SendNextOrder.Location = new System.Drawing.Point(276, 168);
      this.SendNextOrder.Name = "SendNextOrder";
      this.SendNextOrder.OnLine = false;
      this.SendNextOrder.ShowImage = true;
      this.SendNextOrder.ShowText = false;
      this.SendNextOrder.Size = new System.Drawing.Size(100, 51);
      this.SendNextOrder.Switching = false;
      this.SendNextOrder.TabIndex = 219;
      this.SendNextOrder.Text = "2";
      this.SendNextOrder.TranslateText = false;
      this.SendNextOrder.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("SendNextOrder.UserDefinedActiveImage")));
      this.SendNextOrder.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("SendNextOrder.UserDefinedImage")));
      this.SendNextOrder.Warned = false;
      this.SendNextOrder.Click += new System.EventHandler(this.OnButtonPush);
      // 
      // Frm_KnifeStatus
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.SendNextOrder);
      this.Controls.Add(this.SendCurrentOrder);
      this.Controls.Add(this.ttl_CurrentSetup);
      this.Controls.Add(this.ttl_NextSetup);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel0);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.stb_AutoMan);
      this.Controls.Add(this.lbl_DebugInfo);
      this.Name = "Frm_KnifeStatus";
      this.Text = "Frm_KnifeStatus";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.lbl_DebugInfo, 0);
      this.Controls.SetChildIndex(this.stb_AutoMan, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.panel0, 0);
      this.Controls.SetChildIndex(this.panel2, 0);
      this.Controls.SetChildIndex(this.ttl_NextSetup, 0);
      this.Controls.SetChildIndex(this.ttl_CurrentSetup, 0);
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
      this.Controls.SetChildIndex(this.SendCurrentOrder, 0);
      this.Controls.SetChildIndex(this.SendNextOrder, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_KnifeLevel2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_KnifeLevel1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_KnifeLevel0)).EndInit();
      this.panel2.ResumeLayout(false);
      this.pnl_CurrentOrder2.ResumeLayout(false);
      this.pnl_NextOrder2.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.pnl_CurrentOrder1.ResumeLayout(false);
      this.pnl_NextOrder1.ResumeLayout(false);
      this.panel0.ResumeLayout(false);
      this.pnl_CurrentOrder0.ResumeLayout(false);
      this.pnl_NextOrder0.ResumeLayout(false);
      this.ResumeLayout(false);

		}
		#endregion

		protected override void F3()
    { 
		}

		public void _setCurrentSetup(int setupId, int modId)
		{
      this.currId = setupId;
      this.currModId = modId;
      if (this.currId == 0)
      {
        //agrassi_FI08-04-2010_1
        ttl_CurrentSetup.Text =
          ttb_CurrentSheets0.Value = ttb_CurrentSheets1.Value = ttb_CurrentSheets2.Value =
          nud_CurrentCutLenght0.Value = nud_CurrentCutLenght1.Value = nud_CurrentCutLenght2.Value =
          ttl_CurrentOrder0.Text = ttl_CurrentOrder1.Text = ttl_CurrentOrder2.Text = "";
        return;
      }
      DataRow row = null;
      int index = 0;
      SyncroC.DataBase.Message msg = DBManager.getDBManager().message;

      row = DBManager.getDBManager().order.getRow(setupId.ToString(), modId.ToString(), LevelName.Lower, ref index);
      if (row != null)
      {//valorizzazione con ordine
        ttl_CurrentSetup.Text = msg.getTranslatedMessage("§SYCC_34") + ":" + row[Order.DEL_SETUP_CODE].ToString();
        ttl_CurrentOrder0.Text = row[Order.DEL_ORDER_NUMBER_n + index.ToString()].ToString();
        ttb_CurrentSheets0.Value = row[Order.DEL_SEGMENT_SHEET_COUNT_n + index.ToString()].ToString();
      }
      else
      {//ordine non presente nella lista ordini controllo presenza in past order
        row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString(), modId.ToString(), LevelName.Lower, ref index);
        if (row != null)
        {//valorizzazione con ordine di past order
          ttl_CurrentSetup.Text = msg.getTranslatedMessage("§SYCC_34") + ":" + row[PastOrder.POL_SETUP_CODE].ToString();
          ttl_CurrentOrder0.Text = row[PastOrder.POL_ORDER_NUMBER_n + index.ToString()].ToString();
          ttb_CurrentSheets0.Value = row[PastOrder.POL_SEGMENT_SHEET_COUNT_n + index.ToString()].ToString();
        }
        else
        {//ordine non presente tra gli ordini e nemmeno tra i past orders: svuotare tutto
          //ttl_CurrentSetup.Text = msg.getTranslatedMessage("§SYCC_34");
          ttl_CurrentOrder0.Text = ttb_CurrentSheets0.Value = string.Empty;
        }
      }

      row = null;
      if (knife.LevelCount == 2)
        row = DBManager.getDBManager().order.getRow(setupId.ToString(), modId.ToString(), LevelName.Upper, ref index);
      else if (knife.LevelCount == 3 )
        row = DBManager.getDBManager().order.getRow(setupId.ToString(), modId.ToString(), LevelName.Middle, ref index);
      if (row != null)
      {//valorizzazione con ordine
        ttl_CurrentSetup.Text = msg.getTranslatedMessage("§SYCC_34") + ":" + row[Order.DEL_SETUP_CODE].ToString();
        ttl_CurrentOrder1.Text = row[Order.DEL_ORDER_NUMBER_n + index.ToString()].ToString();
        ttb_CurrentSheets1.Value = row[Order.DEL_SEGMENT_SHEET_COUNT_n + index.ToString()].ToString();
      }
      else
      {
          //ZETA 01
          //ordine non presente nella lista ordini controllo presenza in past order
          if (knife.LevelCount == 2)
              row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString(), modId.ToString(), LevelName.Upper, ref index);
          else if (knife.LevelCount == 3)
              row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString(), modId.ToString(), LevelName.Middle, ref index);
        //row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString(), modId.ToString(), LevelName.Middle, ref index);

        if (row != null)
        {//valorizzazione con ordine di past order
          ttl_CurrentSetup.Text = msg.getTranslatedMessage("§SYCC_34") + ":" + row[PastOrder.POL_SETUP_CODE].ToString();
          ttl_CurrentOrder1.Text = row[PastOrder.POL_ORDER_NUMBER_n + index.ToString()].ToString();
          ttb_CurrentSheets1.Value = row[PastOrder.POL_SEGMENT_SHEET_COUNT_n + index.ToString()].ToString();
        }
        else
        {//ordine non presente tra gli ordini e nemmeno tra i past orders: svuotare tutto
          //ttl_CurrentSetup.Text = msg.getTranslatedMessage("§SYCC_34");
          ttl_CurrentOrder1.Text = ttb_CurrentSheets1.Value = string.Empty;
        }
      }

      if (knife.LevelCount == 3)
      {
        row = DBManager.getDBManager().order.getRow(setupId.ToString(), modId.ToString(), LevelName.Upper, ref index);
        if (row != null)
        {//valorizzazione con ordine
          ttl_CurrentSetup.Text = msg.getTranslatedMessage("§SYCC_34") + ":" + row[Order.DEL_SETUP_CODE].ToString();
          ttl_CurrentOrder2.Text = row[Order.DEL_ORDER_NUMBER_n + index.ToString()].ToString();
          ttb_CurrentSheets2.Value = row[Order.DEL_SEGMENT_SHEET_COUNT_n + index.ToString()].ToString();
        }
        else
        {//ordine non presente nella lista ordini controllo presenza in past order
          row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString(), modId.ToString(), LevelName.Upper, ref index);
          if (row != null)
          {//valorizzazione con ordine di past order
            ttl_CurrentSetup.Text = msg.getTranslatedMessage("§SYCC_34") + ":" + row[PastOrder.POL_SETUP_CODE].ToString();
            ttl_CurrentOrder2.Text = row[PastOrder.POL_ORDER_NUMBER_n + index.ToString()].ToString();
            ttb_CurrentSheets2.Value = row[PastOrder.POL_SEGMENT_SHEET_COUNT_n + index.ToString()].ToString();
          }
          else
          {//ordine non presente tra gli ordini e nemmeno tra i past orders: svuotare tutto
            //ttl_CurrentSetup.Text = msg.getTranslatedMessage("§SYCC_34");
            ttl_CurrentOrder2.Text = ttb_CurrentSheets2.Value = string.Empty;
          }
        }
      }

		}

		public void _setNextSetup(int setupId, int modId)
		{
      this.nextId = setupId;
      this.nextModId = modId;
      if (this.nextId == 0)
      {
        //agrassi_FI08-04-2010_1
        ttl_NextSetup.Text =
          ttb_NextSheets0.Value = ttb_NextSheets1.Value = ttb_NextSheets2.Value =
          nud_NextCutLenght0.Value = nud_NextCutLenght1.Value = nud_NextCutLenght2.Value =
          ttl_NextOrder0.Text = ttl_NextOrder1.Text = ttl_NextOrder2.Text = "";
        return;
      }
      DataRow row = null;
			SyncroC.DataBase.Message msg = DBManager.getDBManager().message;
      int index = 0;

      row = DBManager.getDBManager().order.getRow(setupId.ToString(), modId.ToString(), LevelName.Lower ,ref index);
			if (row != null)
			{//valorizzazione con ordine
				ttl_NextSetup.Text = msg.getTranslatedMessage("§SYCC_35") + ":" + row[Order.DEL_SETUP_CODE].ToString();
				ttl_NextOrder0.Text = row[Order.DEL_ORDER_NUMBER_n + index.ToString()].ToString();
        ttb_NextSheets0.Value = row[Order.DEL_SEGMENT_SHEET_COUNT_n + index.ToString()].ToString();
			}
			else
			{//ordine non presente nella lista ordini controllo presenza in past order
        row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString(), modId.ToString(), LevelName.Lower, ref index);
				if (row != null)
				{//valorizzazione con ordine di past order
					ttl_NextSetup.Text = msg.getTranslatedMessage("§SYCC_35") + ":" + row[PastOrder.POL_SETUP_CODE].ToString();
          ttl_NextOrder0.Text = row[PastOrder.POL_ORDER_NUMBER_n + index.ToString()].ToString();
					ttb_NextSheets0.Value = row[PastOrder.POL_SEGMENT_SHEET_COUNT_n + index.ToString()].ToString();
				}
				else
				{//ordine non presente tra gli ordini e nemmeno tra i past orders: svuotare tutto
					//ttl_NextSetup.Text = msg.getTranslatedMessage("§SYCC_35");
					ttl_NextOrder0.Text =	ttb_NextSheets0.Value = string.Empty;
				}
			}


      row = null;
      if (knife.LevelCount == 2)
        row = DBManager.getDBManager().order.getRow(setupId.ToString(), modId.ToString(), LevelName.Upper, ref index);
      else if (knife.LevelCount == 3)
        row = DBManager.getDBManager().order.getRow(setupId.ToString(), modId.ToString(), LevelName.Middle, ref index);
      if (row != null)
      {//valorizzazione con ordine
        ttl_NextSetup.Text = msg.getTranslatedMessage("§SYCC_35") + ":" + row[Order.DEL_SETUP_CODE].ToString();
        ttl_NextOrder1.Text = row[Order.DEL_ORDER_NUMBER_n + index.ToString()].ToString();
        ttb_NextSheets1.Value = row[Order.DEL_SEGMENT_SHEET_COUNT_n + index.ToString()].ToString();
      }
      else
      {
          //ZETA 01
          //ordine non presente nella lista ordini controllo presenza in past order
          if (knife.LevelCount == 2)
              row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString(), modId.ToString(), LevelName.Upper, ref index);
          else if (knife.LevelCount == 3)
              row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString(), modId.ToString(), LevelName.Middle, ref index);
          //row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString(), modId.ToString(), LevelName.Middle, ref index);
        
          if (row != null)
        {//valorizzazione con ordine di past order
          ttl_NextSetup.Text = msg.getTranslatedMessage("§SYCC_35") + ":" + row[PastOrder.POL_SETUP_CODE].ToString();
          ttl_NextOrder1.Text = row[PastOrder.POL_ORDER_NUMBER_n + index.ToString()].ToString();
          ttb_NextSheets1.Value = row[PastOrder.POL_SEGMENT_SHEET_COUNT_n + index.ToString()].ToString();
        }
        else
        {//ordine non presente tra gli ordini e nemmeno tra i past orders: svuotare tutto
          //ttl_NextSetup.Text = msg.getTranslatedMessage("§SYCC_35");
          ttl_NextOrder1.Text = ttb_NextSheets1.Value = string.Empty;
        }
      }


      if (knife.LevelCount == 3)
      {
        row = DBManager.getDBManager().order.getRow(setupId.ToString(), modId.ToString(), LevelName.Upper, ref index);
        if (row != null)
        {//valorizzazione con ordine
          ttl_NextSetup.Text = msg.getTranslatedMessage("§SYCC_35") + ":" + row[Order.DEL_SETUP_CODE].ToString();
          ttl_NextOrder2.Text = row[Order.DEL_ORDER_NUMBER_n + index.ToString()].ToString();
          ttb_NextSheets2.Value = row[Order.DEL_SEGMENT_SHEET_COUNT_n + index.ToString()].ToString();
        }
        else
        {//ordine non presente nella lista ordini controllo presenza in past order
          row = DBManager.getDBManager().pastOrder.getRow(setupId.ToString(), modId.ToString(), LevelName.Upper, ref index);
          if (row != null)
          {//valorizzazione con ordine di past order
            ttl_NextSetup.Text = msg.getTranslatedMessage("§SYCC_35") + ":" + row[PastOrder.POL_SETUP_CODE].ToString();
            ttl_NextOrder2.Text = row[PastOrder.POL_ORDER_NUMBER_n + index.ToString()].ToString();
            ttb_NextSheets2.Value = row[PastOrder.POL_SEGMENT_SHEET_COUNT_n + index.ToString()].ToString();
          }
          else
          {//ordine non presente tra gli ordini e nemmeno tra i past orders: svuotare tutto
            //ttl_NextSetup.Text = msg.getTranslatedMessage("§SYCC_35");
            ttl_NextOrder2.Text = ttb_NextSheets2.Value = string.Empty;
          }
        }
      }
    }

    public void ArrangePanels(int NrOfLevels)
    {
			int SPACING = 12;
			int TOP = ttl_NextSetup.Location.Y + ttl_NextSetup.getCurrentTitleHeight();
			int BOTTOM = TOP + ttl_NextSetup.Size.Height;
			int TOT_SPACE = BOTTOM - TOP;
			
			switch (NrOfLevels)
			{
				case 1:
					panel0.Location = new Point(panel0.Location.X, Convert.ToInt32(TOP + ((TOT_SPACE - panel0.Height) / 2)));
					panel0.Visible = true;
					panel1.Visible = panel2.Visible = false;
					break;
				case 2:
					panel1.Location = new Point(panel1.Location.X, Convert.ToInt32(TOP + (((TOT_SPACE - (2 * panel0.Height))) / 3)));
					panel0.Location = new Point(panel0.Location.X, Convert.ToInt32(panel1.Location.Y + panel1.Height + (((TOT_SPACE - (2 * panel0.Height))) / 3)));
					panel0.Visible = panel1.Visible = true;
					panel2.Visible = false;
					break;
				case 3:
					panel2.Location = new Point(panel2.Location.X, TOP + SPACING);
					panel1.Location = new Point(panel1.Location.X, panel2.Location.Y + panel2.Height + SPACING);
					panel0.Location = new Point(panel0.Location.X, panel1.Location.Y + panel1.Height + SPACING);
					panel0.Visible = panel1.Visible = panel2.Visible = true;
					break;
			}
    }

		protected override void CtrlF1()
		{
			if (showDebugOptions)
				ArrangePanels(1);
		}
		protected override void CtrlF2()
		{
			if (showDebugOptions)
				ArrangePanels(2);
		}
		protected override void CtrlF3()
		{
			if (showDebugOptions)
				ArrangePanels(3);
		}
		protected override void CtrlF4()
		{
			if (showDebugOptions)
				ArrangePanels(knife.LevelCount);
		}

		protected override void ControlD()
		{
			showDebugOptions = !showDebugOptions;

			lbl_DebugInfo.Visible = 
			ttb_CurrentGoodSheets0.Visible = ttb_CurrentGoodSheets1.Visible = ttb_CurrentGoodSheets2.Visible =
			ttb_CurrentDefaultCuts0.Visible = ttb_CurrentDefaultCuts1.Visible = ttb_CurrentDefaultCuts2.Visible =
			ttb_CurrentBadCuts0.Visible = ttb_CurrentBadCuts1.Visible = ttb_CurrentBadCuts2.Visible =
			ttb_CurrentSheets0.Visible = ttb_CurrentSheets1.Visible = ttb_CurrentSheets2.Visible =
			nud_CurrentCutLenght0.Visible = nud_CurrentCutLenght1.Visible = nud_CurrentCutLenght2.Visible =
			ttb_NextSheets0.Visible = ttb_NextSheets1.Visible = ttb_NextSheets2.Visible =
			nud_NextCutLenght0.Visible = nud_NextCutLenght1.Visible = nud_NextCutLenght2.Visible = 
						showDebugOptions;

			if (!showDebugOptions)
				ArrangePanels(knife.LevelCount);
		}

		public override void CorrugatorDirectionLeft()
		{
			int leftLimit = 0;
			int rightLimit = panel0.Width;

			//panel0
			pct_KnifeLevel0.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pnl_NextOrder0, leftLimit, rightLimit);
			HorizontalLocationMirror(pct_KnifeLevel0, leftLimit, rightLimit);
			HorizontalLocationMirror(pnl_CurrentOrder0, leftLimit, rightLimit);
			
			//panel1
			pct_KnifeLevel1.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pnl_NextOrder1, leftLimit, rightLimit);
			HorizontalLocationMirror(pct_KnifeLevel1, leftLimit, rightLimit);
			HorizontalLocationMirror(pnl_CurrentOrder1, leftLimit, rightLimit);

			//panel2
			pct_KnifeLevel2.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
			HorizontalLocationMirror(pnl_NextOrder2, leftLimit, rightLimit);
			HorizontalLocationMirror(pct_KnifeLevel2, leftLimit, rightLimit);
			HorizontalLocationMirror(pnl_CurrentOrder2, leftLimit, rightLimit);

			//current & next titles
			leftLimit = ttl_NextSetup.Location.X;
			rightLimit = ttl_CurrentSetup.Location.X + ttl_CurrentSetup.Width;

			HorizontalLocationMirror(ttl_NextSetup, leftLimit, rightLimit);
			HorizontalLocationMirror(ttl_CurrentSetup, leftLimit, rightLimit);
			HorizontalLocationMirror(stb_AutoMan, leftLimit, rightLimit);
      this.SendCurrentOrder.Location = new Point(this.ttl_CurrentSetup.Location.X + this.ttl_CurrentSetup.Width - this.SendCurrentOrder.Width, this.SendCurrentOrder.Location.Y);
      this.SendNextOrder.Location = new Point(this.ttl_NextSetup.Location.X + this.ttl_NextSetup.Width - this.SendNextOrder.Width, this.SendNextOrder.Location.Y);
		}

		private void btn_AddBadCuts2_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				return;

      if (DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows.Count == 0)
        return;
      KnifeLevel knifeLevel = KnifeLevel.Level3;
      LevelName levelName = DBManager.getDBManager().lineConfig.GetLevelName(knifeLevel);
      Frm_AskValue askValue = new Frm_AskValue();
      string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_74");
      FosberDialogResult ret = askValue.ShowDialog(this, text, 0);

      if (ret.Dialog == DialogResult.OK)
      {
        DataRow current = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows[0];
        //RFAZZI_FI24/07/2010_1. Corretto errore in fase di inserimento dei fogli di scarto. Adesso si tiene in considerazione se ci sono più ordini sul livello
        //int i;
        //for (i = 0; i < Order.NUM_ORDERS_PER_SETUP; i++)
        //  if (current[Order.DEL_LEVEL_NAME_n + i.ToString()].ToString() == levelName.ToString())
        //    break;
        //if (i >= Order.NUM_ORDERS_PER_SETUP)
        //  return;
        int i;
        int numOutTot = 0;
        for (i = 0; i < Order.NUM_ORDERS_PER_SETUP; i++)
          if (current[Order.DEL_LEVEL_NAME_n + i.ToString()].ToString() == levelName.ToString())
            numOutTot += this.intParse(current[Order.DEL_OUTS_n + i.ToString()].ToString());
        if (i == 0)
          return;
        if ((int)ret.Data[0] > this.intParse(this.ttb_CurrentGoodSheets2.Value))
        {
          PageManager.getPageManager()._SetMessage("§SYCC_76");
          return;
        }
        //RFAZZI_FI24/07/2010_1. Corretto errore in fase di inserimento dei fogli di scarto. Adesso si tiene in considerazione se ci sono più ordini sul livello
        //int badCuts = (int)ret.Data[0] / this.intParse(current[Order.DEL_OUTS_n + i.ToString()].ToString());
        int badCuts = (int)ret.Data[0] / numOutTot;
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.KNIFE + ".addBadCutsOnLevel(3" + Command.PARAMETER_SEPARATOR +
          badCuts + ")");
      }
    }

		private void btn_AddBadCuts1_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				return;

      if (DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows.Count==0)
        return;
      KnifeLevel knifeLevel=KnifeLevel.Level2;
      LevelName levelName=DBManager.getDBManager().lineConfig.GetLevelName(knifeLevel);
			Frm_AskValue askValue = new Frm_AskValue();
			string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_74");
			FosberDialogResult ret = askValue.ShowDialog(this, text, 0);

			if (ret.Dialog == DialogResult.OK)
			{
        DataRow current = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows[0];
        //RFAZZI_FI24/07/2010_1. Corretto errore in fase di inserimento dei fogli di scarto. Adesso si tiene in considerazione se ci sono più ordini sul livello
        //int i;
        //for (i = 0; i < Order.NUM_ORDERS_PER_SETUP; i++)
        //  if (current[Order.DEL_LEVEL_NAME_n + i.ToString()].ToString() == levelName.ToString())
        //    break;
        //if (i >= Order.NUM_ORDERS_PER_SETUP)
        //  return;
        int i;
        int numOutTot = 0;
        for (i = 0; i < Order.NUM_ORDERS_PER_SETUP; i++)
          if (current[Order.DEL_LEVEL_NAME_n + i.ToString()].ToString() == levelName.ToString())
            numOutTot += this.intParse(current[Order.DEL_OUTS_n + i.ToString()].ToString());
        if (i == 0)
          return;
        if ((int)ret.Data[0] > this.intParse(this.ttb_CurrentGoodSheets1.Value))
        {
          PageManager.getPageManager()._SetMessage("§SYCC_76");
          return;
        }
        //RFAZZI_FI24/07/2010_1. Corretto errore in fase di inserimento dei fogli di scarto. Adesso si tiene in considerazione se ci sono più ordini sul livello
        //int badCuts = (int)ret.Data[0] / this.intParse(current[Order.DEL_OUTS_n + i.ToString()].ToString());
        int badCuts = (int)ret.Data[0] / numOutTot;
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.KNIFE + ".addBadCutsOnLevel(2" + Command.PARAMETER_SEPARATOR +
          badCuts + ")");
			}
		}

		private void btn_AddBadCuts0_Click(object sender, EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				return;

      if (DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows.Count == 0)
        return;
      KnifeLevel knifeLevel = KnifeLevel.Level1;
      LevelName levelName = DBManager.getDBManager().lineConfig.GetLevelName(knifeLevel);
      Frm_AskValue askValue = new Frm_AskValue();
      string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_74");
      FosberDialogResult ret = askValue.ShowDialog(this, text, 0);

      if (ret.Dialog == DialogResult.OK)
      {
        DataRow current = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows[0];
        //RFAZZI_FI24/07/2010_1. Corretto errore in fase di inserimento dei fogli di scarto. Adesso si tiene in considerazione se ci sono più ordini sul livello
        //int i;
        //for (i = 0; i < Order.NUM_ORDERS_PER_SETUP; i++)
        //  if (current[Order.DEL_LEVEL_NAME_n + i.ToString()].ToString() == levelName.ToString())
        //    break;
        //if (i >= Order.NUM_ORDERS_PER_SETUP)
        //  return;
        int i;
        int numOutTot = 0;
        for (i = 0; i < Order.NUM_ORDERS_PER_SETUP; i++)
          if (current[Order.DEL_LEVEL_NAME_n + i.ToString()].ToString() == levelName.ToString())
            numOutTot += this.intParse(current[Order.DEL_OUTS_n + i.ToString()].ToString());
        if (i == 0)
          return;
        if ((int)ret.Data[0] > this.intParse(this.ttb_CurrentGoodSheets0.Value))
        {
          PageManager.getPageManager()._SetMessage("§SYCC_76");
          return;
        }
        //RFAZZI_FI24/07/2010_1. Corretto errore in fase di inserimento dei fogli di scarto. Adesso si tiene in considerazione se ci sono più ordini sul livello
        //int badCuts = (int)ret.Data[0] / this.intParse(current[Order.DEL_OUTS_n + i.ToString()].ToString());
        int badCuts = (int)ret.Data[0] / numOutTot;
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_RTC_MANAGER, Command.KNIFE + ".addBadCutsOnLevel(1" + Command.PARAMETER_SEPARATOR +
          badCuts + ")");
      }
    }

    private void btn_AddExtraSheets0_Click(object sender, EventArgs e)
    {
			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				return;

      Frm_AskValue askValue = new Frm_AskValue();
      string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_75");
      FosberDialogResult ret = askValue.ShowDialog(this, text, 0);

      if (ret.Dialog == DialogResult.OK)
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_ORDER + ".ExtraSheetsInCurrent(1" + Command.PARAMETER_SEPARATOR +
          (int)ret.Data[0] + ")");
      }
    }

    private void btn_AddExtraSheets1_Click(object sender, EventArgs e)
    {
			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				return;

      Frm_AskValue askValue = new Frm_AskValue();
      string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_75");
      FosberDialogResult ret = askValue.ShowDialog(this, text, 0);

      if (ret.Dialog == DialogResult.OK)
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_ORDER + ".ExtraSheetsInCurrent(2" + Command.PARAMETER_SEPARATOR +
          (int)ret.Data[0] + ")");
      }
    }

    private void btn_AddExtraSheets2_Click(object sender, EventArgs e)
    {
			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
				return;

      Frm_AskValue askValue = new Frm_AskValue();
      string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_75");
      FosberDialogResult ret = askValue.ShowDialog(this, text, 0);

      if (ret.Dialog == DialogResult.OK)
      {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_ORDER + ".ExtraSheetsInCurrent(3" + Command.PARAMETER_SEPARATOR +
          (int)ret.Data[0] + ")");
      }
    }

    private void OnDBOrderChanged(object sender, DBChangedEventArgs e)
    {
      this._setCurrentSetup(this.currId, this.currModId);
      this._setNextSetup(this.nextId, this.nextModId);
    }

		private void OnButtonPush(object sender, System.EventArgs e)
		{
			if (!SyncroC.Utility.Settings.getSettings().facility.ManualOrdersSending)
				return;

			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_RTC_MANAGER,
				Command.KNIFE + "." + ((Control)sender).Name + "()");
		}

		private void _CommunicationError(bool isError)
		{
			this.SendCurrentOrder.Alarmed = isError;
			this.SendNextOrder.Alarmed = isError;
		}

		private void Frm_KnifeStatus_Load(object sender, EventArgs e)
		{
			this._CommunicationError(false);
		}
  }
}

