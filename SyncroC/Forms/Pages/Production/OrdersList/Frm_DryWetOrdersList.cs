using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Text;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase;
using SyncroC.Forms.PopUp;
using SyncroC.Forms.Pages.Production.OrdersIns;
using SyncroC.Controls.Colors;

namespace SyncroC.Forms.Pages.Production.OrdersList
{
	public class Frm_DryWetOrdersList : SyncroC.Forms.Pages.Production.OrdersList.Frm_OrdersListSubGroup
	{

		private System.ComponentModel.IContainer components = null;
    private SyncroC.Controls.Title ttl_Order1;
		private SyncroC.Controls.Title ttl_Order2;
    private SyncroC.Controls.Title lbl_DryEndTitle;
    private SyncroC.Controls.Title lbl_WetEndTitle;
		private SyncroC.Controls.GridView gdv_Programs;

		private Label lbl_SetupID;
		private Label lbl_ModID;

		private DataSet order;
		private DBManager dbm;

    //colonne che avrebbe dovuto fare il designer.
    private DataGridViewColumn clm_Prog_Pos;
    private DataGridViewColumn clm_Prog_ProgramCode;
    private DataGridViewColumn clm_Prog_PaperWidth;
    private DataGridViewColumn clm_Prog_ProgramLength;
    private DataGridViewColumn clm_Prog_ProgramQuality;

    private DataGridViewColumn clm_Order_Pos;
    private DataGridViewColumn clm_Order_SetupCode;
		private DataGridViewColumn clm_Order_Meters;
		private DataGridViewColumn clm_Order_Trim;
		private SyncroC.Controls.Title ttl_Order0;
		private Label lbl_Sheets0;
		private Label lbl_Misc0;
		private Label lbl_Customer0;
		private Label lbl_Misc1;
		private Label lbl_Sheets1;
		private Label lbl_Customer1;
		private Label lbl_Sheets2;
		private Label lbl_Misc2;
		private Label lbl_Customer2;
		private SyncroC.Controls.GridView gdv_Orders;
    private Label lbl_DryIDs;
    private Label label1;
    private Label lbl_ProgramID;
		private SyncroC.Controls.Bracket bracket1;
    private int oldFirstDryIndex=0, oldDryLength=1, oldDrySelection=0;
    private Label lab_Sel;
    private Label lab_Start;
    private Label lab_Len;
    private int oldWetIndex = 0;

		public Frm_DryWetOrdersList()
		{
			InitializeComponent();

			dbm = DBManager.getDBManager();

      order = DBManager.getDBManager().order.order;
      this.gdv_Programs.DataSource = order.Tables[Order.WETEND_LIST];
      this.gdv_Orders.DataSource = order.Tables[Order.DRYEND_LIST];

      //
      //colonne che avrebbe dovuto fare il designer.
      //

      //gdv_Programs 
      clm_Prog_Pos = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Prog_Pos.Name = Order.WEL_POSITION;
      clm_Prog_Pos.HeaderText = "";
      clm_Prog_Pos.DataPropertyName = "Position";
      clm_Prog_Pos.Width = 50;

      clm_Prog_ProgramCode = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Prog_ProgramCode.Name = Order.WEL_PROGRAM_CODE;
      clm_Prog_ProgramCode.HeaderText = "Code";
      clm_Prog_ProgramCode.DataPropertyName = "ProgramCode";
      clm_Prog_ProgramCode.Width = 110;

      clm_Prog_PaperWidth = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Prog_PaperWidth.Name = Order.WEL_PAPER_WIDTH;
      clm_Prog_PaperWidth.HeaderText = "Width";
      clm_Prog_PaperWidth.DataPropertyName = "PaperWidth";
      clm_Prog_PaperWidth.Width = 100;

      clm_Prog_ProgramLength = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Prog_ProgramLength.Name = Order.WEL_PROGRAM_LENGTH;
      clm_Prog_ProgramLength.HeaderText = "Length";
      clm_Prog_ProgramLength.DataPropertyName = "ProgramLength";
      clm_Prog_ProgramLength.Width = 100;

      clm_Prog_ProgramQuality = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Prog_ProgramQuality.Name = Order.WEL_PROGRAM_QUALITY;
      clm_Prog_ProgramQuality.HeaderText = "Quality";
      clm_Prog_ProgramQuality.DataPropertyName = "ProgramQuality";
      //clm_Prog_ProgramQuality.Width = 250;
      clm_Prog_ProgramQuality.Width = 354;

      gdv_Programs.Columns.Add(clm_Prog_Pos);
      gdv_Programs.Columns.Add(clm_Prog_ProgramCode);
      gdv_Programs.Columns.Add(clm_Prog_PaperWidth);
      gdv_Programs.Columns.Add(clm_Prog_ProgramLength);
      gdv_Programs.Columns.Add(clm_Prog_ProgramQuality);

			gdv_Programs.Sort(clm_Prog_Pos, ListSortDirection.Ascending);

      //gdv_Orders
      clm_Order_Pos = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Order_Pos.Name = Order.DEL_POSITION;
      clm_Order_Pos.HeaderText = "";
      clm_Order_Pos.DataPropertyName = "Position";
      clm_Order_Pos.Width = 50;

      clm_Order_SetupCode = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Order_SetupCode.Name = Order.DEL_SETUP_CODE;
      clm_Order_SetupCode.HeaderText = "Code";
      clm_Order_SetupCode.DataPropertyName = "SetupCode";
      clm_Order_SetupCode.Width = 130;

			clm_Order_Meters = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Order_Meters.Name = Order.DEL_SHEET_LENGTH_0;
			clm_Order_Meters.HeaderText = "Length";
			clm_Order_Meters.DataPropertyName = Order.DEL_SHEET_LENGTH_0;
			clm_Order_Meters.Width = 100;

      clm_Order_Trim = new DataGridViewColumn(new DataGridViewTextBoxCell());
			clm_Order_Trim.Name = Order.DEL_TRIM;
      clm_Order_Trim.HeaderText = "Trim";
      clm_Order_Trim.DataPropertyName = "Trim";
      clm_Order_Trim.Width = 100;

      gdv_Orders.Columns.Add(clm_Order_Pos);
      gdv_Orders.Columns.Add(clm_Order_SetupCode);
      gdv_Orders.Columns.Add(clm_Order_Meters);
      gdv_Orders.Columns.Add(clm_Order_Trim);
			gdv_Orders.Sort(clm_Order_Pos, ListSortDirection.Ascending);

			if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
			{
				this.lbl_CtrlF2_YY.Text = "";
				this.lbl_CtrlF3_YY.Text = "";
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
			this.gdv_Programs.DataSource = null;
			this.gdv_Orders.DataSource = null;
			base.Dispose( disposing );
		}

		 #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DryWetOrdersList));
          this.ttl_Order1 = new SyncroC.Controls.Title();
          this.ttl_Order2 = new SyncroC.Controls.Title();
          this.lbl_DryEndTitle = new SyncroC.Controls.Title();
          this.lbl_WetEndTitle = new SyncroC.Controls.Title();
          this.gdv_Programs = new SyncroC.Controls.GridView();
          this.lbl_SetupID = new System.Windows.Forms.Label();
          this.lbl_ModID = new System.Windows.Forms.Label();
          this.ttl_Order0 = new SyncroC.Controls.Title();
          this.bracket1 = new SyncroC.Controls.Bracket();
          this.lbl_Sheets0 = new System.Windows.Forms.Label();
          this.lbl_Misc0 = new System.Windows.Forms.Label();
          this.lbl_Customer0 = new System.Windows.Forms.Label();
          this.lbl_Misc1 = new System.Windows.Forms.Label();
          this.lbl_Sheets1 = new System.Windows.Forms.Label();
          this.lbl_Customer1 = new System.Windows.Forms.Label();
          this.lbl_Sheets2 = new System.Windows.Forms.Label();
          this.lbl_Misc2 = new System.Windows.Forms.Label();
          this.lbl_Customer2 = new System.Windows.Forms.Label();
          this.gdv_Orders = new SyncroC.Controls.GridView();
          this.lbl_DryIDs = new System.Windows.Forms.Label();
          this.label1 = new System.Windows.Forms.Label();
          this.lbl_ProgramID = new System.Windows.Forms.Label();
          this.lab_Sel = new System.Windows.Forms.Label();
          this.lab_Start = new System.Windows.Forms.Label();
          this.lab_Len = new System.Windows.Forms.Label();
          ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
          this.SuspendLayout();
          // 
          // lbl_Title_YN
          // 
          this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
          this.lbl_Title_YN.Text = "Program - Setup list";
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
          this.lbl_CtrlF5.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
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
          this.lbl_CtrlF4_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 2);
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
          // ttl_Order1
          // 
          this.ttl_Order1.BackColor = System.Drawing.Color.Black;
          this.ttl_Order1.CausesValidation = false;
          this.ttl_Order1.color = SyncroC.Controls.Colors.SyncroColor.Black;
          this.ttl_Order1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ttl_Order1.ForeColor = System.Drawing.Color.WhiteSmoke;
          this.ttl_Order1.Location = new System.Drawing.Point(429, 578);
          this.ttl_Order1.Name = "ttl_Order1";
          this.ttl_Order1.Size = new System.Drawing.Size(374, 134);
          this.ttl_Order1.Style = SyncroC.Controls.TitleStyles.Big;
          this.ttl_Order1.TabIndex = 123;
          this.ttl_Order1.TranslateText = false;
          // 
          // ttl_Order2
          // 
          this.ttl_Order2.BackColor = System.Drawing.Color.Black;
          this.ttl_Order2.CausesValidation = false;
          this.ttl_Order2.color = SyncroC.Controls.Colors.SyncroColor.Black;
          this.ttl_Order2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ttl_Order2.ForeColor = System.Drawing.Color.WhiteSmoke;
          this.ttl_Order2.Location = new System.Drawing.Point(816, 578);
          this.ttl_Order2.Name = "ttl_Order2";
          this.ttl_Order2.Size = new System.Drawing.Size(374, 134);
          this.ttl_Order2.Style = SyncroC.Controls.TitleStyles.Big;
          this.ttl_Order2.TabIndex = 124;
          this.ttl_Order2.TranslateText = false;
          // 
          // lbl_DryEndTitle
          // 
          this.lbl_DryEndTitle.BackColor = System.Drawing.Color.Black;
          this.lbl_DryEndTitle.CausesValidation = false;
          this.lbl_DryEndTitle.color = SyncroC.Controls.Colors.SyncroColor.Black;
          this.lbl_DryEndTitle.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_DryEndTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_DryEndTitle.Location = new System.Drawing.Point(788, 193);
          this.lbl_DryEndTitle.Name = "lbl_DryEndTitle";
          this.lbl_DryEndTitle.Size = new System.Drawing.Size(413, 343);
          this.lbl_DryEndTitle.Style = SyncroC.Controls.TitleStyles.Big;
          this.lbl_DryEndTitle.TabIndex = 126;
          this.lbl_DryEndTitle.Text = "Dry End Orders List";
          this.lbl_DryEndTitle.TranslateText = true;
          // 
          // lbl_WetEndTitle
          // 
          this.lbl_WetEndTitle.BackColor = System.Drawing.Color.Black;
          this.lbl_WetEndTitle.CausesValidation = false;
          this.lbl_WetEndTitle.color = SyncroC.Controls.Colors.SyncroColor.Black;
          this.lbl_WetEndTitle.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_WetEndTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_WetEndTitle.Location = new System.Drawing.Point(30, 193);
          this.lbl_WetEndTitle.Name = "lbl_WetEndTitle";
          this.lbl_WetEndTitle.Size = new System.Drawing.Size(752, 343);
          this.lbl_WetEndTitle.Style = SyncroC.Controls.TitleStyles.Big;
          this.lbl_WetEndTitle.TabIndex = 127;
          this.lbl_WetEndTitle.Text = "Program List";
          this.lbl_WetEndTitle.TranslateText = true;
          // 
          // gdv_Programs
          // 
          this.gdv_Programs.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
          this.gdv_Programs.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.gdv_Programs.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.NoStandardDownTimeReasonColor;
          this.gdv_Programs.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
          this.gdv_Programs.AutoGenerateColumns = false;
          this.gdv_Programs.BackColor = System.Drawing.Color.WhiteSmoke;
          this.gdv_Programs.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
          this.gdv_Programs.DataMember = "";
          this.gdv_Programs.DataSource = null;
          this.gdv_Programs.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.gdv_Programs.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.gdv_Programs.ImagePassword = null;
          this.gdv_Programs.Location = new System.Drawing.Point(42, 221);
          this.gdv_Programs.MultiSelect = false;
          this.gdv_Programs.Name = "gdv_Programs";
          this.gdv_Programs.RowsHeight = 32;
          this.gdv_Programs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
          this.gdv_Programs.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.NoStandardDownTimeReasonColor;
          this.gdv_Programs.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
          this.gdv_Programs.Size = new System.Drawing.Size(739, 315);
          this.gdv_Programs.Style = SyncroC.Controls.GridStyles.Light;
          this.gdv_Programs.TabIndex = 130;
          this.gdv_Programs.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
          this.gdv_Programs.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.gdv_Programs.TranslateText = true;
          this.gdv_Programs.DoubleClick += new System.EventHandler(this.gdv_Programs_DoubleClick);
          this.gdv_Programs.SelectionChanged += new System.EventHandler(this.gdv_Programs_SelectionChanged);
          this.gdv_Programs.Leave += new System.EventHandler(this.gdv_Programs_Leave);
          this.gdv_Programs.Enter += new System.EventHandler(this.gdv_Programs_Enter);
          this.gdv_Programs.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Programs_CellFormatting);
          // 
          // lbl_SetupID
          // 
          this.lbl_SetupID.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_SetupID.CausesValidation = false;
          this.lbl_SetupID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_SetupID.Location = new System.Drawing.Point(936, 171);
          this.lbl_SetupID.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
          this.lbl_SetupID.Name = "lbl_SetupID";
          this.lbl_SetupID.Size = new System.Drawing.Size(124, 18);
          this.lbl_SetupID.TabIndex = 134;
          this.lbl_SetupID.Text = "SetUpID";
          this.lbl_SetupID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          this.lbl_SetupID.Visible = false;
          // 
          // lbl_ModID
          // 
          this.lbl_ModID.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_ModID.CausesValidation = false;
          this.lbl_ModID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_ModID.Location = new System.Drawing.Point(1077, 171);
          this.lbl_ModID.Name = "lbl_ModID";
          this.lbl_ModID.Size = new System.Drawing.Size(124, 18);
          this.lbl_ModID.TabIndex = 135;
          this.lbl_ModID.Text = "ModID";
          this.lbl_ModID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          this.lbl_ModID.Visible = false;
          // 
          // ttl_Order0
          // 
          this.ttl_Order0.BackColor = System.Drawing.Color.Black;
          this.ttl_Order0.CausesValidation = false;
          this.ttl_Order0.color = SyncroC.Controls.Colors.SyncroColor.Black;
          this.ttl_Order0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ttl_Order0.ForeColor = System.Drawing.Color.WhiteSmoke;
          this.ttl_Order0.Location = new System.Drawing.Point(42, 578);
          this.ttl_Order0.Name = "ttl_Order0";
          this.ttl_Order0.Size = new System.Drawing.Size(374, 134);
          this.ttl_Order0.Style = SyncroC.Controls.TitleStyles.Big;
          this.ttl_Order0.TabIndex = 132;
          this.ttl_Order0.TranslateText = false;
          // 
          // bracket1
          // 
          this.bracket1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
          this.bracket1.color = SyncroC.Controls.Colors.SyncroColor.Green;
          this.bracket1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.bracket1.ForeColor = System.Drawing.Color.WhiteSmoke;
          this.bracket1.Location = new System.Drawing.Point(30, 548);
          this.bracket1.Name = "bracket1";
          this.bracket1.Position = SyncroC.Controls.BracketPosition.Bottom;
          this.bracket1.ShowArrow = true;
          this.bracket1.Size = new System.Drawing.Size(1171, 164);
          this.bracket1.TabIndex = 133;
          this.bracket1.TabStop = false;
          this.bracket1.Text = "Details";
          this.bracket1.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
          this.bracket1.TranslateText = true;
          // 
          // lbl_Sheets0
          // 
          this.lbl_Sheets0.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_Sheets0.CausesValidation = false;
          this.lbl_Sheets0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_Sheets0.Location = new System.Drawing.Point(321, 679);
          this.lbl_Sheets0.Name = "lbl_Sheets0";
          this.lbl_Sheets0.Size = new System.Drawing.Size(95, 32);
          this.lbl_Sheets0.TabIndex = 158;
          this.lbl_Sheets0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // lbl_Misc0
          // 
          this.lbl_Misc0.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_Misc0.CausesValidation = false;
          this.lbl_Misc0.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_Misc0.Location = new System.Drawing.Point(50, 679);
          this.lbl_Misc0.Name = "lbl_Misc0";
          this.lbl_Misc0.Size = new System.Drawing.Size(234, 32);
          this.lbl_Misc0.TabIndex = 157;
          this.lbl_Misc0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // lbl_Customer0
          // 
          this.lbl_Customer0.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_Customer0.CausesValidation = false;
          this.lbl_Customer0.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_Customer0.Location = new System.Drawing.Point(54, 616);
          this.lbl_Customer0.Name = "lbl_Customer0";
          this.lbl_Customer0.Size = new System.Drawing.Size(362, 30);
          this.lbl_Customer0.TabIndex = 156;
          this.lbl_Customer0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // lbl_Misc1
          // 
          this.lbl_Misc1.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_Misc1.CausesValidation = false;
          this.lbl_Misc1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_Misc1.Location = new System.Drawing.Point(437, 679);
          this.lbl_Misc1.Name = "lbl_Misc1";
          this.lbl_Misc1.Size = new System.Drawing.Size(234, 32);
          this.lbl_Misc1.TabIndex = 160;
          this.lbl_Misc1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // lbl_Sheets1
          // 
          this.lbl_Sheets1.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_Sheets1.CausesValidation = false;
          this.lbl_Sheets1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_Sheets1.Location = new System.Drawing.Point(708, 679);
          this.lbl_Sheets1.Name = "lbl_Sheets1";
          this.lbl_Sheets1.Size = new System.Drawing.Size(95, 32);
          this.lbl_Sheets1.TabIndex = 161;
          this.lbl_Sheets1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // lbl_Customer1
          // 
          this.lbl_Customer1.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_Customer1.CausesValidation = false;
          this.lbl_Customer1.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_Customer1.Location = new System.Drawing.Point(437, 616);
          this.lbl_Customer1.Name = "lbl_Customer1";
          this.lbl_Customer1.Size = new System.Drawing.Size(365, 30);
          this.lbl_Customer1.TabIndex = 159;
          this.lbl_Customer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // lbl_Sheets2
          // 
          this.lbl_Sheets2.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_Sheets2.CausesValidation = false;
          this.lbl_Sheets2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_Sheets2.Location = new System.Drawing.Point(1097, 679);
          this.lbl_Sheets2.Name = "lbl_Sheets2";
          this.lbl_Sheets2.Size = new System.Drawing.Size(93, 32);
          this.lbl_Sheets2.TabIndex = 164;
          this.lbl_Sheets2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // lbl_Misc2
          // 
          this.lbl_Misc2.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_Misc2.CausesValidation = false;
          this.lbl_Misc2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_Misc2.Location = new System.Drawing.Point(824, 679);
          this.lbl_Misc2.Name = "lbl_Misc2";
          this.lbl_Misc2.Size = new System.Drawing.Size(234, 32);
          this.lbl_Misc2.TabIndex = 163;
          this.lbl_Misc2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // lbl_Customer2
          // 
          this.lbl_Customer2.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_Customer2.CausesValidation = false;
          this.lbl_Customer2.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_Customer2.Location = new System.Drawing.Point(824, 616);
          this.lbl_Customer2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
          this.lbl_Customer2.Name = "lbl_Customer2";
          this.lbl_Customer2.Size = new System.Drawing.Size(365, 30);
          this.lbl_Customer2.TabIndex = 162;
          this.lbl_Customer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
          // 
          // gdv_Orders
          // 
          this.gdv_Orders.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
          this.gdv_Orders.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.gdv_Orders.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
          this.gdv_Orders.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
          this.gdv_Orders.AutoGenerateColumns = false;
          this.gdv_Orders.BackColor = System.Drawing.Color.WhiteSmoke;
          this.gdv_Orders.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
          this.gdv_Orders.DataMember = "";
          this.gdv_Orders.DataSource = null;
          this.gdv_Orders.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.gdv_Orders.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.gdv_Orders.ImagePassword = null;
          this.gdv_Orders.Location = new System.Drawing.Point(799, 221);
          this.gdv_Orders.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
          this.gdv_Orders.MultiSelect = true;
          this.gdv_Orders.Name = "gdv_Orders";
          this.gdv_Orders.RowsHeight = 32;
          this.gdv_Orders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
          this.gdv_Orders.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
          this.gdv_Orders.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
          this.gdv_Orders.Size = new System.Drawing.Size(401, 315);
          this.gdv_Orders.Style = SyncroC.Controls.GridStyles.Light;
          this.gdv_Orders.TabIndex = 165;
          this.gdv_Orders.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
          this.gdv_Orders.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.gdv_Orders.TranslateText = true;
          this.gdv_Orders.DoubleClick += new System.EventHandler(this.gdv_Orders_DoubleClick);
          this.gdv_Orders.SelectionChanged += new System.EventHandler(this.gdv_Orders_SelectionChanged);
          this.gdv_Orders.Leave += new System.EventHandler(this.gdv_Orders_Leave);
          this.gdv_Orders.Enter += new System.EventHandler(this.gdv_Orders_Enter);
          this.gdv_Orders.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Orders_CellFormatting);
          // 
          // lbl_DryIDs
          // 
          this.lbl_DryIDs.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_DryIDs.CausesValidation = false;
          this.lbl_DryIDs.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_DryIDs.Location = new System.Drawing.Point(326, 171);
          this.lbl_DryIDs.Name = "lbl_DryIDs";
          this.lbl_DryIDs.Size = new System.Drawing.Size(732, 18);
          this.lbl_DryIDs.TabIndex = 168;
          this.lbl_DryIDs.Text = "DryIDs";
          this.lbl_DryIDs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          this.lbl_DryIDs.Visible = false;
          // 
          // label1
          // 
          this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
          this.label1.CausesValidation = false;
          this.label1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label1.Location = new System.Drawing.Point(184, 171);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(124, 18);
          this.label1.TabIndex = 167;
          this.label1.Text = "ModID";
          this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          this.label1.Visible = false;
          // 
          // lbl_ProgramID
          // 
          this.lbl_ProgramID.BackColor = System.Drawing.Color.WhiteSmoke;
          this.lbl_ProgramID.CausesValidation = false;
          this.lbl_ProgramID.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.lbl_ProgramID.Location = new System.Drawing.Point(27, 171);
          this.lbl_ProgramID.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
          this.lbl_ProgramID.Name = "lbl_ProgramID";
          this.lbl_ProgramID.Size = new System.Drawing.Size(140, 18);
          this.lbl_ProgramID.TabIndex = 166;
          this.lbl_ProgramID.Text = "ProgramID";
          this.lbl_ProgramID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          this.lbl_ProgramID.Visible = false;
          // 
          // lab_Sel
          // 
          this.lab_Sel.BackColor = System.Drawing.Color.Gainsboro;
          this.lab_Sel.Location = new System.Drawing.Point(1205, 220);
          this.lab_Sel.Name = "lab_Sel";
          this.lab_Sel.Size = new System.Drawing.Size(19, 18);
          this.lab_Sel.TabIndex = 169;
          this.lab_Sel.Visible = false;
          // 
          // lab_Start
          // 
          this.lab_Start.BackColor = System.Drawing.Color.Gainsboro;
          this.lab_Start.Location = new System.Drawing.Point(1205, 274);
          this.lab_Start.Name = "lab_Start";
          this.lab_Start.Size = new System.Drawing.Size(19, 18);
          this.lab_Start.TabIndex = 170;
          this.lab_Start.Visible = false;
          // 
          // lab_Len
          // 
          this.lab_Len.BackColor = System.Drawing.Color.Gainsboro;
          this.lab_Len.Location = new System.Drawing.Point(1205, 305);
          this.lab_Len.Name = "lab_Len";
          this.lab_Len.Size = new System.Drawing.Size(19, 18);
          this.lab_Len.TabIndex = 171;
          this.lab_Len.Visible = false;
          // 
          // Frm_DryWetOrdersList
          // 
          this.ClientSize = new System.Drawing.Size(1280, 738);
          this.Controls.Add(this.lab_Len);
          this.Controls.Add(this.lab_Start);
          this.Controls.Add(this.lab_Sel);
          this.Controls.Add(this.lbl_DryIDs);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.lbl_ProgramID);
          this.Controls.Add(this.gdv_Orders);
          this.Controls.Add(this.lbl_Sheets2);
          this.Controls.Add(this.lbl_Misc2);
          this.Controls.Add(this.lbl_Customer2);
          this.Controls.Add(this.lbl_Misc1);
          this.Controls.Add(this.lbl_Sheets1);
          this.Controls.Add(this.lbl_Customer1);
          this.Controls.Add(this.lbl_Sheets0);
          this.Controls.Add(this.lbl_Misc0);
          this.Controls.Add(this.lbl_Customer0);
          this.Controls.Add(this.lbl_ModID);
          this.Controls.Add(this.lbl_SetupID);
          this.Controls.Add(this.gdv_Programs);
          this.Controls.Add(this.lbl_WetEndTitle);
          this.Controls.Add(this.bracket1);
          this.Controls.Add(this.ttl_Order0);
          this.Controls.Add(this.lbl_DryEndTitle);
          this.Controls.Add(this.ttl_Order1);
          this.Controls.Add(this.ttl_Order2);
          this.Name = "Frm_DryWetOrdersList";
          this.Tag = "";
          this.Text = "Frm_DryWetOrdersList";
          this.Load += new System.EventHandler(this.Frm_SetupList_Load);
          this.Controls.SetChildIndex(this.tab_Tab9, 0);
          this.Controls.SetChildIndex(this.ttl_Order2, 0);
          this.Controls.SetChildIndex(this.ttl_Order1, 0);
          this.Controls.SetChildIndex(this.lbl_DryEndTitle, 0);
          this.Controls.SetChildIndex(this.ttl_Order0, 0);
          this.Controls.SetChildIndex(this.bracket1, 0);
          this.Controls.SetChildIndex(this.lbl_WetEndTitle, 0);
          this.Controls.SetChildIndex(this.gdv_Programs, 0);
          this.Controls.SetChildIndex(this.lbl_SetupID, 0);
          this.Controls.SetChildIndex(this.lbl_ModID, 0);
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
          this.Controls.SetChildIndex(this.lbl_Customer0, 0);
          this.Controls.SetChildIndex(this.lbl_Misc0, 0);
          this.Controls.SetChildIndex(this.lbl_Sheets0, 0);
          this.Controls.SetChildIndex(this.lbl_Customer1, 0);
          this.Controls.SetChildIndex(this.lbl_Sheets1, 0);
          this.Controls.SetChildIndex(this.lbl_Misc1, 0);
          this.Controls.SetChildIndex(this.lbl_Customer2, 0);
          this.Controls.SetChildIndex(this.lbl_Misc2, 0);
          this.Controls.SetChildIndex(this.lbl_Sheets2, 0);
          this.Controls.SetChildIndex(this.gdv_Orders, 0);
          this.Controls.SetChildIndex(this.lbl_ProgramID, 0);
          this.Controls.SetChildIndex(this.label1, 0);
          this.Controls.SetChildIndex(this.lbl_DryIDs, 0);
          this.Controls.SetChildIndex(this.lab_Sel, 0);
          this.Controls.SetChildIndex(this.lab_Start, 0);
          this.Controls.SetChildIndex(this.lab_Len, 0);
          ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
          this.ResumeLayout(false);

				}
        #endregion

		protected override void F11()
		{
		}

		/// <summary>
    /// Move selected group of orders in a different position.
    /// </summary>
    protected override void CtrlF2()
    {
      //agrassi_FI29-01-09_1
      //if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
      //  return;

      //if (gdv_Programs.Rows.Count <= 0)
      //  return;
      //Frm_MoveOrders movePrograms = new Frm_MoveOrders();
      //int first = 0;
      //int last = 0;
      //if (gdv_Programs.SelectedRows.Count > 0)
      //{
      //  first = gdv_Programs.SelectedRows.Count > 0 ? gdv_Programs.SelectedRows[gdv_Programs.SelectedRows.Count - 1].Index + 1 : 1;
      //  last = gdv_Programs.SelectedRows.Count > 0 ? gdv_Programs.SelectedRows[0].Index + 1 : 1;
      //}
      //FosberDialogResult ret = movePrograms.ShowDialog(this,
      //    Math.Min(first, last), Math.Max(first, last));
      //if (ret.Dialog == DialogResult.OK)
      //{
      //  int firstProgramToMovePos = (int)ret.Data[0];
      //  int lastProgramToMovePos = (int)ret.Data[1];
      //  int destinationPos = (int)ret.Data[2];
      //  if ((firstProgramToMovePos != -1) && (lastProgramToMovePos != -1) && (destinationPos != -1) &&//no conversion errors occured
      //    (firstProgramToMovePos >= 2) && (firstProgramToMovePos <= gdv_Programs.Rows.Count) &&				//firstOrderToMovePos is consistent
      //    (lastProgramToMovePos >= 1) && (lastProgramToMovePos <= gdv_Programs.Rows.Count) &&					//lastOrderToMovePos is consistent
      //    (firstProgramToMovePos <= lastProgramToMovePos) &&																				//firstOrderToMovePos and lastOrderToMovePos are compatible		 
      //    (destinationPos >= 2) && (destinationPos <= gdv_Programs.Rows.Count + 1) &&									//destinationPos is consistent
      //    ((destinationPos > lastProgramToMovePos+1) || (destinationPos < firstProgramToMovePos)))	//destinationPos, firstOrderToMovePos, lastOrderToMovePos are comatble
      //  {
      //    DBManager dbm = DBManager.getDBManager();
      //    int destinationDryPos = -1;
      //    //agrassi_FI21-05-08_1
      //    string[] dryIDs = null;
      //    if (destinationPos <= dbm.order.order.Tables[Order.WETEND_LIST].Rows.Count)
      //    {
      //      dryIDs = dbm.order.order.Tables[Order.WETEND_LIST].Rows[destinationPos - 1][Order.WEL_DRY_IDs].ToString().Split(new char[] { '-' });
      //      if (dryIDs.Length > 0)
      //      {
      //        DataRow dryOrd = dbm.order.order.Tables[Order.DRYEND_LIST].Rows.Find(dryIDs[0]);
      //        destinationDryPos = this.intParse(dryOrd[Order.DEL_POSITION].ToString());
      //      }
      //    }
      //    else
      //      destinationDryPos = dbm.order.order.Tables[Order.DRYEND_LIST].Rows.Count + 1;
      //    CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      //    StringBuilder command = new StringBuilder(Command.DB_ORDER + "." + Command.DB_CMD_MOVE + "(" +
      //        destinationDryPos.ToString() + Command.PARAMETER_SEPARATOR);
      //    for (int i = firstProgramToMovePos - 1; i <= lastProgramToMovePos - 1; i++)
      //    {
      //      dryIDs = dbm.order.order.Tables[Order.WETEND_LIST].Rows[i][Order.WEL_DRY_IDs].ToString().Split(new char[] { '-' });
      //      for (int j = 0; j < dryIDs.Length; j++)
      //      {
      //        command.Append(dryIDs[j]);
      //        if ((i != lastProgramToMovePos - 1) || (j != dryIDs.Length - 1))
      //          command.Append(Command.PARAMETER_SEPARATOR);
      //      }
      //    }
      //    command.Append(")");
      //    cm.SendTcp(cm.SYNCROS_DB_MANAGER, command.ToString());
      //  }
      //  //agrassi_FI21-05-08_1
      //  else
      //  {
      //    Frm_Alert frm_alert = new Frm_Alert();
      //    frm_alert.ShowDialog(this, dbm.message.getTranslatedMessage("§SYCC_10"));
      //  }
      //}
    }


    /// <summary>
		/// Delete
		/// </summary>
		protected override void CtrlF3()
		{
      //agrassi_FI29-01-09_1
      //if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
      //  return;

      //if (gdv_Programs.Rows.Count <= 0)
      //  return;
      //DBManager dbm = DBManager.getDBManager();
      //StringBuilder command = new StringBuilder(Command.DB_ORDER + "." + Command.DB_CMD_DEL + "(");
      //bool send = false;

      //if (this.gdv_Programs.IsConsecutiveSelection())
      //{
      //  Frm_MultipleDelete popUp = new Frm_MultipleDelete();
      //  int first = gdv_Programs.SelectedRows.Count > 0 ? gdv_Programs.SelectedRows[gdv_Programs.SelectedRows.Count - 1].Index + 1 : 1;
      //  int last = gdv_Programs.SelectedRows.Count > 0 ? gdv_Programs.SelectedRows[0].Index + 1 : 1;
      //  FosberDialogResult ret = popUp.ShowDialog(this, Math.Min(first, last), Math.Max(first, last));
      //  if (ret.Dialog == DialogResult.OK)
      //  {
      //    int firstProgramToDeletePos = (int)ret.Data[0];
      //    int lastProgramToDeletePos = (int)ret.Data[1];
      //    if ((firstProgramToDeletePos != -1) && (lastProgramToDeletePos != -1) &&										//no conversion errors occured
      //      (firstProgramToDeletePos >= 2) && (firstProgramToDeletePos <= gdv_Programs.Rows.Count) &&	//firstOrderToDeletePos is consistent
      //      (lastProgramToDeletePos >= 1) && (lastProgramToDeletePos <= gdv_Programs.Rows.Count) &&     //lastOrderToDeletePos is consistent
      //      (firstProgramToDeletePos <= lastProgramToDeletePos))																			//firstOrderToDeletePos and lastOrderToDeletePos are compatible		 
      //    {
      //      for (int i = firstProgramToDeletePos - 1; i <= lastProgramToDeletePos - 1; i++)
      //      {
      //        string[] dryIDs = dbm.order.order.Tables[Order.WETEND_LIST].Rows[i][Order.WEL_DRY_IDs].ToString().Split(new char[] { '-' });
      //        for (int j = 0; j < dryIDs.Length; j++)
      //        {
      //          command.Append(dryIDs[j]);
      //          if ((i != lastProgramToDeletePos - 1) || (j != dryIDs.Length - 1))
      //            command.Append(Command.PARAMETER_SEPARATOR);
      //          send = true;
      //        }
      //      }
      //    }
      //    //agrassi_FI21-05-08_1
      //    else
      //    {
      //      Frm_Alert frm_alert = new Frm_Alert();
      //      frm_alert.ShowDialog(this, dbm.message.getTranslatedMessage("§SYCC_10"));
      //    }
      //  }
      //}
      //else
      //{
      //  Frm_Confirm popUp = new Frm_Confirm();
      //  string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_20") + "\n";
      //  DataRowCollection rows = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows;
      //  for (int i = gdv_Programs.SelectedRows.Count - 1; i >= 0; i--)
      //  {
      //    text += (gdv_Programs.SelectedRows[i].Index + 1).ToString() + (i > 0 ? ", " : "");
      //    string[] dryIDs = dbm.order.order.Tables[Order.WETEND_LIST].Rows[gdv_Programs.SelectedRows[i].Index][Order.WEL_DRY_IDs].ToString().Split(new char[] { '-' });
      //    for (int j = 0; j < dryIDs.Length; j++)
      //    {
      //      command.Append(dryIDs[j]);
      //      if ((i != 0) || (j != dryIDs.Length - 1))
      //        command.Append(Command.PARAMETER_SEPARATOR);
      //    }
      //  }
      //  send = (popUp.ShowDialog(this, text) == DialogResult.OK);
      //}
      //command.Append(")");
      //if (send)
      //{
      //  CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      //  cm.SendTcp(cm.SYNCROS_DB_MANAGER, command.ToString());
      //}
    }

    private void FillInOrderDetails()
    {
      if (gdv_Orders.SelectedRows.Count == 1)
      {
        DataRow dataRow = ((DataRowView)gdv_Orders.SelectedRows[0].DataBoundItem).Row;
        if (dataRow != null && dataRow.RowState != DataRowState.Detached)
        {
          try
          {
            //order 0
            ttl_Order0.Text = dbm.language.getTranslation(
              typeof(LevelName).Name + "." + dataRow[Order.DEL_LEVEL_NAME_0].ToString()) +
              " - " + dataRow[Order.DEL_ORDER_NUMBER_0].ToString();
            lbl_Misc0.Text = SyncroConverter.ConvertString(dataRow[Order.DEL_SHEET_WIDTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
              " X " + SyncroConverter.ConvertString(dataRow[Order.DEL_SHEET_LENGTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
              " X " + dataRow[Order.DEL_OUTS_0].ToString();
            int sheets = 0;
            try
            {
              //CM8 29-09-2005
              sheets = Convert.ToInt32(dataRow[Order.DEL_SEGMENT_SHEET_COUNT_0].ToString());
              //*Convert.ToInt32(dataRow[Order.DEL_OUTS_0].ToString());
            }
            catch (Exception ex)
            {
              Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting Order.DEL_SEGMENT_SHEET_COUNT_0 or Order.DEL_OUTS_0", ex);
            }
            lbl_Sheets0.Text = sheets.ToString();
            lbl_Customer0.Text = dataRow[Order.DEL_CUSTOMER_NAME_0].ToString();
            //order 1 
            if (Convert.ToInt64(dataRow[Order.DEL_SHEET_LENGTH_1].ToString()) != 0)
            {
              ttl_Order1.Text = dbm.language.getTranslation(
              typeof(LevelName).Name + "." + dataRow[Order.DEL_LEVEL_NAME_1].ToString()) +
              " - " + dataRow[Order.DEL_ORDER_NUMBER_1].ToString();
              lbl_Misc1.Text = SyncroConverter.ConvertString(dataRow[Order.DEL_SHEET_WIDTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
                " X " + SyncroConverter.ConvertString(dataRow[Order.DEL_SHEET_LENGTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
                " X " + dataRow[Order.DEL_OUTS_1].ToString();
              sheets = 0;
              try
              {
                //CM8 29-09-2005
                sheets = Convert.ToInt32(dataRow[Order.DEL_SEGMENT_SHEET_COUNT_1].ToString());
                //*Convert.ToInt32(dataRow[Order.DEL_OUTS_1].ToString());
              }
              catch (Exception ex)
              {
                Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting Order.DEL_SEGMENT_SHEET_COUNT_1 or Order.DEL_OUTS_1", ex);
              }
              lbl_Sheets1.Text = sheets.ToString();
              lbl_Customer1.Text = dataRow[Order.DEL_CUSTOMER_NAME_1].ToString();
            }
            else
            {
              CleanOrderDetails(false, true, false);
            }
            //order 2
            if (Convert.ToInt64(dataRow[Order.DEL_SHEET_LENGTH_2].ToString()) != 0)
            {
              ttl_Order2.Text = dbm.language.getTranslation(
                typeof(LevelName).Name + "." + dataRow[Order.DEL_LEVEL_NAME_2].ToString()) +
                " - " + dataRow[Order.DEL_ORDER_NUMBER_2].ToString();
              lbl_Misc2.Text = SyncroConverter.ConvertString(dataRow[Order.DEL_SHEET_WIDTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
                " X " + SyncroConverter.ConvertString(dataRow[Order.DEL_SHEET_LENGTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
                " X " + dataRow[Order.DEL_OUTS_2].ToString();
              sheets = 0;
              try
              {
                //CM8 29-09-2005
                sheets = Convert.ToInt32(dataRow[Order.DEL_SEGMENT_SHEET_COUNT_2].ToString());
                //*Convert.ToInt32(dataRow[Order.DEL_OUTS_2].ToString());
              }
              catch (Exception ex)
              {
                Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting Order.DEL_SEGMENT_SHEET_COUNT_2 or Order.DEL_OUTS_2", ex);
              }
              lbl_Sheets2.Text = sheets.ToString();
              lbl_Customer2.Text = dataRow[Order.DEL_CUSTOMER_NAME_2].ToString();
            }
            else
            {
              CleanOrderDetails(false, false, true);
            }
            //debug info
            lbl_SetupID.Text = "SetUpID: " + dataRow[Order.DEL_SETUP_ID].ToString();
            lbl_ModID.Text = "ModID: " + dataRow[Order.DEL_SETUP_MODID].ToString();
          }
          catch (RowNotInTableException ex)
          {
            Logger.LogError(this, "gdv_Orders_SelectionChanged - the rowState is not Detached (it is \"" + dataRow.RowState.ToString() + "\" but the RowNotInTableException has been thrown anyway....", ex);
          }
        }
      }
      else
      {
        CleanOrderDetails(true, true, true);
      }
    }

    private void ResetOrdersGroupSelection(int index)
    {
      if ((this.oldDrySelection>=0) && (this.oldDrySelection< gdv_Orders.Rows.Count))
        gdv_Orders.Rows[this.oldDrySelection].Selected = false;
      this.oldDrySelection = index;
      this.lab_Sel.Text = this.oldDrySelection.ToString();
      for (int i = this.oldFirstDryIndex; i < this.oldFirstDryIndex + this.oldDryLength; i++)
      {
        if (i < gdv_Orders.Rows.Count)
        {
          gdv_Orders.Rows[i].DefaultCellStyle.BackColor = (((i % 2) == 0) ? ColConv.Convert(SyncroColor.White) : ColConv.Convert(SyncroColor.LightGray));
        }
      }
    }

    private void PerformOrdersGroupSelection(int start, int length, bool selectFirst)
    {
      if ((start < 0) || (start + length > gdv_Orders.Rows.Count)) return;
      for (int j = start; j < start + length; j++)
        gdv_Orders.Rows[j].DefaultCellStyle.BackColor = (((j % 2) == 0) ? Color.LightGoldenrodYellow : Color.Khaki);
      if (selectFirst)
      {
        gdv_Orders.Rows[start].Selected = true;
        this.oldDrySelection = start;
        this.lab_Sel.Text = this.oldDrySelection.ToString();
      }
      this.oldFirstDryIndex = start;
      this.oldDryLength = length;
      this.lab_Start.Text = this.oldFirstDryIndex.ToString();
      this.lab_Len.Text = this.oldDryLength.ToString();
    }

		/// <summary>
		/// Fill the dry end details with new selected order
		/// </summary>
		private void gdv_Orders_SelectionChanged(object sender, EventArgs e)
		{
      this.FillInOrderDetails();
      if (!((Control)sender).Focused) return;
      if (this.oldWetIndex < gdv_Programs.Rows.Count)
        gdv_Programs.Rows[this.oldWetIndex].Selected = false;
      bool resetSelection = false;
      if (gdv_Orders.SelectedRows.Count == 1)
      {
        int index = gdv_Orders.SelectedRows[0].Index;
        if ((index < this.oldFirstDryIndex) || (index >= this.oldFirstDryIndex + this.oldDryLength))
        {
          this.ResetOrdersGroupSelection(index);
          resetSelection = true;
        }
        else
        {
          this.oldDrySelection = index;
          this.lab_Sel.Text = this.oldDrySelection.ToString();
        }
        if (gdv_Orders.SelectedRows.Count > 0)
        {
          DataRow dryOrd = ((DataRowView)gdv_Orders.SelectedRows[0].DataBoundItem).Row;
          string dryID = dryOrd[Order.DEL_SETUP_ID].ToString();
          for (int i = 0; i < gdv_Programs.Rows.Count; i++)
          {
            string dryIDs = ((DataRowView)gdv_Programs.Rows[i].DataBoundItem).Row[Order.WEL_DRY_IDs].ToString();
            int indID = dryIDs.IndexOf(dryID);
            if (indID >= 0)
            {
              gdv_Programs.Rows[i].Selected = true;
              gdv_Programs.FirstDisplayedScrollingRowIndex = ((i > 3) ? (i - 4) : 0);
              this.oldWetIndex = i;
              if (resetSelection)
              {
                int lengthReduction = 0;
                int start = index - indID / 5;
                if (start < 0)
                {
                  lengthReduction = -start;
                  start = 0;
                }
                this.PerformOrdersGroupSelection(start, (dryIDs.Length + 1) / 5 - lengthReduction, false);
              }
              break;
            }
          }
        }
      }
    }

		/// <summary>
		/// Clean all controls for order details
		/// </summary>
		private void CleanOrderDetails(bool level0, bool level1, bool level2)
		{
			if (level0)
				ttl_Order0.Text = lbl_Customer0.Text = lbl_Misc0.Text = lbl_Sheets0.Text = String.Empty;
			if (level1)
				ttl_Order1.Text = lbl_Customer1.Text = lbl_Misc1.Text = lbl_Sheets1.Text = String.Empty;
			if (level2)
				ttl_Order2.Text = lbl_Customer2.Text = lbl_Misc2.Text = lbl_Sheets2.Text = String.Empty;
			if (level0 && level1 && level2)
			{//debug info
				lbl_SetupID.Text = "SetupID: ";
				lbl_ModID.Text = "ModID: ";
			}
		}

    /// <summary>
    /// Clean selection of programs grid
    /// </summary>
    private void CleanProgramSelection()
    {
      if (gdv_Programs.Rows.Count > 0)
          gdv_Programs.Rows[0].Selected = false;
    }

    /// <summary>
    /// Select first row of both list and give the focus to the Wet end list.
    /// </summary>
    private void Frm_SetupList_Load(object sender, EventArgs e)
    {
			if (this.gdv_Programs.Rows.Count > 0)
			{
				this.gdv_Programs.Focus();
				this.gdv_Programs.Rows[0].Selected = true;
			}
    }

    /// <summary>
    /// Show / Hide debug info.
    /// </summary>
    protected override void ControlD()
    {
      base.ControlD();
      lbl_ProgramID.Visible = lbl_ModID.Visible = lbl_DryIDs.Visible =
      lab_Sel.Visible = lab_Start.Visible = lab_Len.Visible = !lbl_ProgramID.Visible;
    }

    /// <summary>
    /// 
    /// </summary>
    /// //TODO: change appearance (backcolor or forecolor) of Dry end orders belonging to the selected wet end program
    private void gdv_Programs_SelectionChanged(object sender, EventArgs e)
    {
      if (gdv_Programs.SelectedRows.Count == 1)
      {
        int index=gdv_Programs.SelectedRows[0].Index;
        DataRow wetOrd = ((DataRowView)gdv_Programs.SelectedRows[0].DataBoundItem).Row;
        if (wetOrd != null && wetOrd.RowState != DataRowState.Detached)
        {
          //debug info
          lbl_ProgramID.Text = "ProgramID: " + wetOrd[Order.WEL_PROGRAM_ID].ToString();
          lbl_ModID.Text = "ModID: " + wetOrd[Order.WEL_PROGRAM_MODID].ToString();
          lbl_DryIDs.Text = "DryIDs: " + wetOrd[Order.WEL_DRY_IDs].ToString();
        }
        if (!((Control)sender).Focused) return;
        string[] dryIDs = wetOrd[Order.WEL_DRY_IDs].ToString().Split(new char[] { '-' });
        for (int j = 0; j < dryIDs.Length; j++)
        {
          int i;
          for (i = index; i < gdv_Orders.Rows.Count; i++)
          {
            if (((DataRowView)gdv_Orders.Rows[i].DataBoundItem).Row[Order.DEL_SETUP_ID].ToString() == dryIDs[j])
            {
              this.ResetOrdersGroupSelection(i);
              this.PerformOrdersGroupSelection(i, dryIDs.Length-j, true);
              //gdv_Orders.Rows[i].Selected = true;
              gdv_Orders.FirstDisplayedScrollingRowIndex = ((i > 0) ? (i - 1) : i);
              this.oldFirstDryIndex = i;
              this.oldDryLength = dryIDs.Length-j;
              this.lab_Start.Text = this.oldFirstDryIndex.ToString();
              this.lab_Len.Text = this.oldDryLength.ToString();
              break;
            }
          }
          if (i < gdv_Orders.Rows.Count) break;
        }
      }
    }

    protected override void CtrlF4()
    {
    }

		private void gdv_Orders_DoubleClick(object sender, EventArgs e)
		{
			if (gdv_Orders.SelectedRows.Count == 1 &&
					order.Tables[Order.DRYEND_LIST].Rows.Count >= gdv_Orders.SelectedRows[0].Index)
			{
				Frm_OrdersInsSubGroup.indexOrderToShowDRY = gdv_Orders.SelectedRows[0].Index;
			}
			askForPage(typeof(Frm_QuickInsDryOrder));
		}

		private void gdv_Programs_DoubleClick(object sender, EventArgs e)
		{
			if (gdv_Programs.SelectedRows.Count == 1 &&
					order.Tables[Order.WETEND_LIST].Rows.Count >= gdv_Programs.SelectedRows[0].Index)
			{
				Frm_OrdersInsSubGroup.indexOrderToShowWET = gdv_Programs.SelectedRows[0].Index;
			}
			askForPage(typeof(Frm_InsWetOrder));
		}

		private void gdv_Programs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			DataRow dataRow = ((DataRowView)gdv_Programs.Rows[e.RowIndex].DataBoundItem).Row;

			if (this.gdv_Programs.Columns[e.ColumnIndex].Name == Order.WEL_PAPER_WIDTH
						&& e.Value != null)
			{
				try
				{
					e.Value = SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_Programs_CellFormatting - " + Order.WEL_PAPER_WIDTH + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
				}
			}
		}

		private void gdv_Orders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			DataRow dataRow = ((DataRowView)gdv_Orders.Rows[e.RowIndex].DataBoundItem).Row;

			if (e.Value != null)
			{
				//bool valid = false;
				try
				{
					bool valid = Convert.ToBoolean(dataRow[Order.DEL_VALID]);
          if (!valid)
          {
            gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Violet;
          }
          //else
          //  gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.BackColor = (((e.RowIndex % 2) == 0) ? Color.White : Color.LightGray);

				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_Orders_CellFormatting - " + Order.DEL_VALID + " - Error converting \"" + e.Value.ToString() + "\" to bool", ex);
				}
        //agrassi_FI28-10-08
        //bool tandemOCWithoutEnabling = false;
        try
        {
          if (e.RowIndex < gdv_Orders.Rows.Count - 1)
          {
            bool tandemOCWithoutEnabling = Convert.ToBoolean(((DataRowView)gdv_Orders.Rows[e.RowIndex + 1].DataBoundItem).Row[Order.DEL_TANDEM]) && (!DBManager.getDBManager().lineConfig.AOCConfig.AOCTandemEnable);
            if (tandemOCWithoutEnabling)
            {
              gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
              gdv_Orders.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.DarkSalmon;
            }
          }
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_Orders_CellFormatting - " + Order.DEL_TANDEM + " - Error converting \"" + e.Value.ToString() + "\" to bool", ex);
        }
      }

			if (this.gdv_Orders.Columns[e.ColumnIndex].Name == Order.DEL_SHEET_LENGTH_0
						&& e.Value != null)
			{
				try
				{
					e.Value = ((Convert.ToInt32(e.Value) *
						((Convert.ToInt32(dataRow[Order.DEL_SEGMENT_SHEET_COUNT_0]) /
						Convert.ToInt32(dataRow[Order.DEL_OUTS_0])))) / 10000);

				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_Orders_CellFormatting - " + Order.DEL_SHEET_LENGTH_0 + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
				}
			}

			if (this.gdv_Orders.Columns[e.ColumnIndex].Name == Order.DEL_TRIM
						&& e.Value != null)
			{
				try
				{
					e.Value = SyncroConverter.ConvertString(e.Value.ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
				}
				catch (Exception ex)
				{
					Logger.LogError(this, "gdv_Orders_CellFormatting - " + Order.DEL_TRIM + " - Error converting \"" + e.Value.ToString() + "\" to int32", ex);
				}
			}
		}

    private void gdv_Programs_Enter(object sender, EventArgs e)
    {
      this.lbl_WetEndTitle.BackColor = Color.Black;
      this.lbl_DryEndTitle.BackColor = Color.Gray;
    }

    private void gdv_Programs_Leave(object sender, EventArgs e)
    {
      this.lbl_WetEndTitle.BackColor = Color.Gray;
    }

    private void gdv_Orders_Enter(object sender, EventArgs e)
    {
      this.lbl_DryEndTitle.BackColor = Color.Black;
      this.lbl_WetEndTitle.BackColor = Color.Gray;
    }

    private void gdv_Orders_Leave(object sender, EventArgs e)
    {
      this.lbl_DryEndTitle.BackColor = Color.Gray;
    }

		
  }
}

