using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.DataBase;
using SyncroC.Utility;
using SyncroC.DataBase.LineConfig;
//using SyncroC.Forms.Pages.Twin400.Orders;

namespace SyncroC.Forms.Pages.SlitterScorer.Tools
{
	/// <summary>
	/// Form where sub group of twin400 tools pages inherith from.
	/// </summary>
	public class Frm_SlitterScorerToolsSubgroup : SyncroC.Forms.Pages.SlitterScorer.Frm_SlitterScorerGroup 
	{
    public static int externIndex = 0;
    private SyncroC.Controls.TitledLabel ttl_Order3;
    private SyncroC.Controls.TitledLabel ttl_Order2;
    private SyncroC.Controls.TitledLabel ttl_Order1;
    private SyncroC.Controls.TitledLabel ttl_WebWidth;
    private SyncroC.Controls.TitledLabel ttl_Position;
    private SyncroC.Controls.TitledLabel ttl_SetupCode;
    //protected const int DETAIL_SETUPS = 3;
    protected int toolsPos = 0;
		protected int axUsed = 0;
    protected DataRow setup;
    protected int visibleSetupCount;
    protected Label ModId, SetupId;
    protected SyncroC.Controls.Tools.MachineTools toolDetail;
    private System.ComponentModel.IContainer components = null;

		public Frm_SlitterScorerToolsSubgroup()
		{
			InitializeComponent();
      //agrassi_FI12-06-08_2
      this.toolDetail = new SyncroC.Controls.Tools.MachineTools();
      this.toolDetail.BackColor = System.Drawing.Color.White;
      this.toolDetail.Debug = false;
      this.toolDetail.Location = new System.Drawing.Point(110, 225);
      this.toolDetail.Name = "toolDetail";
      this.toolDetail.Size = new System.Drawing.Size(1114, 513);
      this.toolDetail.TabIndex = 294;
      this.toolDetail.TranslateText = false;
      this.Controls.Add(this.toolDetail);
      this.Controls.SetChildIndex(this.toolDetail, 0);
      //this.tab_TabB.Visible = (DBManager.getDBManager().lineConfig.Rotary && (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount > 1));
      this.tab_TabC.Visible = DBManager.getDBManager().lineConfig.WebDirector;
    }

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (Historian.CurrentPageSlitterScorerTools) for the SlitterScorer_Tools subgroup is set here.
		/// </summary>
		public override void _Show()
		{		
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageSlitterScorerTools = this.getRequestCommand();
			base._Show();
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

		//protected override void F5()
		protected override void F4()
		{
		}

		protected override void F10()
		{
			//askForPage(typeof(Frm_twin400_bladesAndScorers));
		}

		protected override void F11()
		{
			//askForPage(typeof(Frm_twin400_rotary));
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_SlitterScorerWebDirector));
		}


		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SlitterScorerToolsSubgroup));
			this.ttl_Order3 = new SyncroC.Controls.TitledLabel();
			this.ttl_Order2 = new SyncroC.Controls.TitledLabel();
			this.ttl_Order1 = new SyncroC.Controls.TitledLabel();
			this.ttl_WebWidth = new SyncroC.Controls.TitledLabel();
			this.ttl_Position = new SyncroC.Controls.TitledLabel();
			this.ttl_SetupCode = new SyncroC.Controls.TitledLabel();
			this.ModId = new System.Windows.Forms.Label();
			this.SetupId = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
			this.SuspendLayout();
			// 
			// lbl_Title_YN
			// 
			this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
			this.lbl_Title_YN.Text = "TWIN 400 TOOLS";
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
			// tab_Tab4
			// 
			this.tab_Tab4.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.tab_Tab4.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
			// 
			// tab_TabA
			// 
			this.tab_TabA.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
			this.tab_TabA.Text = "Blades / scorers";
			this.tab_TabA.Visible = false;
			// 
			// tab_TabC
			// 
			this.tab_TabC.Text = "Web director";
			this.tab_TabC.Visible = true;
			// 
			// tab_TabB
			// 
			this.tab_TabB.Text = "Rotary";
			this.tab_TabB.Visible = false;
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
			this.iml_navigationImages.ImageSize = new System.Drawing.Size(79, 28);
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
			// ttl_Order3
			// 
			this.ttl_Order3.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ttl_Order3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ttl_Order3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
			this.ttl_Order3.Location = new System.Drawing.Point(1033, 168);
			this.ttl_Order3.Margin = new System.Windows.Forms.Padding(0, 3, 1, 3);
			this.ttl_Order3.Name = "ttl_Order3";
			this.ttl_Order3.Size = new System.Drawing.Size(191, 52);
			this.ttl_Order3.Style = SyncroC.Controls.TitledLabelStyles.Light;
			this.ttl_Order3.TabIndex = 293;
			this.ttl_Order3.TabStop = false;
			this.ttl_Order3.Text = "Order3";
			this.ttl_Order3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.ttl_Order3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
			this.ttl_Order3.TranslateText = true;
			this.ttl_Order3.Value = "";
			this.ttl_Order3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.ttl_Order3.ValueMultiline = true;
			// 
			// ttl_Order2
			// 
			this.ttl_Order2.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ttl_Order2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ttl_Order2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
			this.ttl_Order2.Location = new System.Drawing.Point(690, 168);
			this.ttl_Order2.Margin = new System.Windows.Forms.Padding(0, 3, 1, 3);
			this.ttl_Order2.Name = "ttl_Order2";
			this.ttl_Order2.Size = new System.Drawing.Size(342, 52);
			this.ttl_Order2.Style = SyncroC.Controls.TitledLabelStyles.Light;
			this.ttl_Order2.TabIndex = 292;
			this.ttl_Order2.TabStop = false;
			this.ttl_Order2.Text = "Order2";
			this.ttl_Order2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.ttl_Order2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
			this.ttl_Order2.TranslateText = true;
			this.ttl_Order2.Value = "";
			this.ttl_Order2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.ttl_Order2.ValueMultiline = true;
			// 
			// ttl_Order1
			// 
			this.ttl_Order1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ttl_Order1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ttl_Order1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
			this.ttl_Order1.Location = new System.Drawing.Point(347, 168);
			this.ttl_Order1.Margin = new System.Windows.Forms.Padding(0, 3, 1, 3);
			this.ttl_Order1.Name = "ttl_Order1";
			this.ttl_Order1.Size = new System.Drawing.Size(342, 52);
			this.ttl_Order1.Style = SyncroC.Controls.TitledLabelStyles.Light;
			this.ttl_Order1.TabIndex = 291;
			this.ttl_Order1.TabStop = false;
			this.ttl_Order1.Text = "Order1";
			this.ttl_Order1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.ttl_Order1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
			this.ttl_Order1.TranslateText = true;
			this.ttl_Order1.Value = "";
			this.ttl_Order1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.ttl_Order1.ValueMultiline = true;
			// 
			// ttl_WebWidth
			// 
			this.ttl_WebWidth.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ttl_WebWidth.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ttl_WebWidth.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
			this.ttl_WebWidth.Location = new System.Drawing.Point(283, 168);
			this.ttl_WebWidth.Margin = new System.Windows.Forms.Padding(0, 1, 1, 3);
			this.ttl_WebWidth.Name = "ttl_WebWidth";
			this.ttl_WebWidth.Size = new System.Drawing.Size(63, 52);
			this.ttl_WebWidth.Style = SyncroC.Controls.TitledLabelStyles.Light;
			this.ttl_WebWidth.TabIndex = 290;
			this.ttl_WebWidth.TabStop = false;
			this.ttl_WebWidth.Text = "Width";
			this.ttl_WebWidth.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.ttl_WebWidth.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
			this.ttl_WebWidth.TranslateText = true;
			this.ttl_WebWidth.Value = "";
			this.ttl_WebWidth.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.ttl_WebWidth.ValueMultiline = true;
			// 
			// ttl_Position
			// 
			this.ttl_Position.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ttl_Position.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ttl_Position.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
			this.ttl_Position.Location = new System.Drawing.Point(110, 168);
			this.ttl_Position.Margin = new System.Windows.Forms.Padding(3, 3, 1, 1);
			this.ttl_Position.Name = "ttl_Position";
			this.ttl_Position.Size = new System.Drawing.Size(40, 52);
			this.ttl_Position.Style = SyncroC.Controls.TitledLabelStyles.Light;
			this.ttl_Position.TabIndex = 289;
			this.ttl_Position.TabStop = false;
			this.ttl_Position.Text = "Pos";
			this.ttl_Position.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.ttl_Position.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
			this.ttl_Position.TranslateText = true;
			this.ttl_Position.Value = "";
			this.ttl_Position.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.ttl_Position.ValueMultiline = true;
			// 
			// ttl_SetupCode
			// 
			this.ttl_SetupCode.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ttl_SetupCode.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ttl_SetupCode.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ttl_SetupCode.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
			this.ttl_SetupCode.Location = new System.Drawing.Point(151, 168);
			this.ttl_SetupCode.Margin = new System.Windows.Forms.Padding(1, 3, 1, 2);
			this.ttl_SetupCode.Name = "ttl_SetupCode";
			this.ttl_SetupCode.Size = new System.Drawing.Size(131, 52);
			this.ttl_SetupCode.Style = SyncroC.Controls.TitledLabelStyles.Light;
			this.ttl_SetupCode.TabIndex = 288;
			this.ttl_SetupCode.TabStop = false;
			this.ttl_SetupCode.Text = "Setup code";
			this.ttl_SetupCode.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.ttl_SetupCode.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
			this.ttl_SetupCode.TranslateText = true;
			this.ttl_SetupCode.Value = "";
			this.ttl_SetupCode.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
			this.ttl_SetupCode.ValueMultiline = true;
			// 
			// ModId
			// 
			this.ModId.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ModId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ModId.Location = new System.Drawing.Point(283, 61);
			this.ModId.Name = "ModId";
			this.ModId.Size = new System.Drawing.Size(112, 32);
			this.ModId.TabIndex = 371;
			this.ModId.Text = "ModId:";
			this.ModId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ModId.Visible = false;
			// 
			// SetupId
			// 
			this.SetupId.Font = new System.Drawing.Font("Arial Unicode MS", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SetupId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.SetupId.Location = new System.Drawing.Point(120, 61);
			this.SetupId.Name = "SetupId";
			this.SetupId.Size = new System.Drawing.Size(164, 32);
			this.SetupId.TabIndex = 370;
			this.SetupId.Text = "SetupId:";
			this.SetupId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.SetupId.Visible = false;
			// 
			// Frm_SlitterScorerToolsSubgroup
			// 
			this.ClientSize = new System.Drawing.Size(1280, 738);
			this.Controls.Add(this.ModId);
			this.Controls.Add(this.SetupId);
			this.Controls.Add(this.ttl_Order3);
			this.Controls.Add(this.ttl_Order2);
			this.Controls.Add(this.ttl_Order1);
			this.Controls.Add(this.ttl_WebWidth);
			this.Controls.Add(this.ttl_Position);
			this.Controls.Add(this.ttl_SetupCode);
			this.Name = "Frm_SlitterScorerToolsSubgroup";
			this.Text = "Frm_SlitterScorerToolsSubgroup";
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
			this.Controls.SetChildIndex(this.ttl_SetupCode, 0);
			this.Controls.SetChildIndex(this.ttl_Position, 0);
			this.Controls.SetChildIndex(this.ttl_WebWidth, 0);
			this.Controls.SetChildIndex(this.ttl_Order1, 0);
			this.Controls.SetChildIndex(this.ttl_Order2, 0);
			this.Controls.SetChildIndex(this.ttl_Order3, 0);
			this.Controls.SetChildIndex(this.SetupId, 0);
			this.Controls.SetChildIndex(this.ModId, 0);
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
			this.ResumeLayout(false);

    }
		#endregion

    protected override void ControlD()
    {
      base.ControlD();
      this.toolDetail.Debug = !this.toolDetail.Debug;
      SetupId.Visible = ModId.Visible = !SetupId.Visible;

    }

		protected void Frm_SlitterScorerToolsSubgroup_Load(object sender, EventArgs e)
    {
      this.toolDetail.Focus();
    }

		protected void Frm_SlitterScorerToolsSubgroup_SizeChanged(object sender, EventArgs e)
    {
      this.ShowTools(this.toolsPos);
    }

		protected void Frm_SlitterScorerToolsSubgroup_Activated(object sender, EventArgs e)
    {
			if (Frm_SlitterScorerToolsSubgroup.externIndex != this.toolsPos)
      {
				this.toolsPos = Frm_SlitterScorerToolsSubgroup.externIndex;
        if (this.toolsPos < 0)
          this.toolsPos = 0;
        else if (this.toolsPos >= this.visibleSetupCount)
          this.toolsPos = this.visibleSetupCount - 1;
        else
        {
          //Frm_twin400_ordersSubgroup.externIndex = this.toolsPos;
					Frm_SlitterScorerToolsSubgroup.externIndex = this.toolsPos;
        }
        this.ShowTools(this.toolsPos);
      }
    }

    protected void SetupListChange(object sender, DataBase.EventArgs.DBChangedEventArgs e)
    {
      if (this.toolsPos >= DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows.Count)
        this.toolsPos--;
      this.ShowTools(this.toolsPos);
    }

    protected override void PageUp()
    {
      this.CtrlF4();
    }

    protected override void PageDown()
    {
      this.CtrlF5();
    }

    protected override void CtrlF4()
    {
      if (this.toolsPos > 0)
      {
        this.ShowTools(--this.toolsPos);
        //Frm_twin400_ordersSubgroup.externIndex = this.toolsPos;
				Frm_SlitterScorerToolsSubgroup.externIndex = this.toolsPos;
      }
    }

    protected override void CtrlF5()
    {
      if (this.toolsPos < this.visibleSetupCount - 1)
      {
        this.ShowTools(++this.toolsPos);
        //Frm_twin400_ordersSubgroup.externIndex = this.toolsPos;
				Frm_SlitterScorerToolsSubgroup.externIndex = this.toolsPos;
      }
    }

    protected virtual bool ShowTools(int pos)
    {
			this.visibleSetupCount = SyncroC.DataBase.LineConfig.SlitterScorerHeader.OLD_SS_SETUPS;
      return this.ShowSetupData(pos );
    }

    protected bool ShowSetupData(int pos)
    {
      if ((pos >= DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows.Count) || (pos<0))
        return false;
      setup = DBManager.getDBManager().order.order.Tables[Order.DRYEND_LIST].Rows[pos];
      this.SetupId.Text = "SetupId: " + setup[Order.DEL_SETUP_ID].ToString();
      this.ModId.Text = "ModId: " + setup[Order.DEL_SETUP_MODID].ToString();
      this.ttl_Position.Value = (pos + 1).ToString();
      this.ttl_SetupCode.Value = setup[Order.DEL_SETUP_CODE].ToString();
      Language lang = DBManager.getDBManager().language;
      this.ttl_WebWidth.Value = SyncroConverter.ConvertString(setup[Order.DEL_WEB_WIDTH].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre).ToString();
      this.ttl_Order1.Value = 
                  lang.getTranslation(typeof(LevelName).Name+"."+ setup[Order.DEL_LEVEL_NAME_0].ToString()) + 
                  ": " + setup[Order.DEL_OUTS_0].ToString() + " x " +
                  SyncroConverter.ConvertScoreMeasures(setup[Order.DEL_SCORE_MEASURES_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)+
                  " (" + SyncroConverter.ConvertString(setup[Order.DEL_SHEET_WIDTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + ") " +
                  setup[Order.DEL_SCORE_TYPE_0].ToString() + " " + 
                  lang.getTranslation(typeof(PositType).Name+"."+ setup[Order.DEL_POSIT_TYPE_0].ToString());
      if (int.Parse(setup[Order.DEL_OUTS_1].ToString()) > 0)
        this.ttl_Order2.Value =
                    lang.getTranslation(typeof(LevelName).Name + "." + setup[Order.DEL_LEVEL_NAME_1].ToString()) +
                    ": " + setup[Order.DEL_OUTS_1].ToString() + " x " +
                    SyncroConverter.ConvertScoreMeasures(setup[Order.DEL_SCORE_MEASURES_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
                    " (" + SyncroConverter.ConvertString(setup[Order.DEL_SHEET_WIDTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + ") " +
                    setup[Order.DEL_SCORE_TYPE_1].ToString() + " " +
                    lang.getTranslation(typeof(PositType).Name + "." + setup[Order.DEL_POSIT_TYPE_1].ToString());

      else
        this.ttl_Order2.Value = "";
      if (int.Parse(setup[Order.DEL_OUTS_2].ToString()) > 0)
        this.ttl_Order3.Value =
                    lang.getTranslation(typeof(LevelName).Name + "." + setup[Order.DEL_LEVEL_NAME_2].ToString()) +
                    ": " + setup[Order.DEL_OUTS_2].ToString() + " x " +
                    SyncroConverter.ConvertScoreMeasures(setup[Order.DEL_SCORE_MEASURES_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) +
                    " (" + SyncroConverter.ConvertString(setup[Order.DEL_SHEET_WIDTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + ") " +
                    setup[Order.DEL_SCORE_TYPE_2].ToString() + " " + 
                    lang.getTranslation(typeof(PositType).Name + "." + setup[Order.DEL_POSIT_TYPE_2].ToString());
      else
        this.ttl_Order3.Value = "";
      return true;
    }

    protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
    {
      base.OnPaint(e);
      const int MARGIN = 5;
      this.toolDetail.Left = this.leftHigh.X + MARGIN;
      this.toolDetail.Top = this.ttl_Position.Top + this.ttl_Position.Height + MARGIN;
      this.toolDetail.Width = this.rightHigh.X - this.toolDetail.Left - MARGIN;
      this.toolDetail.Height = this.leftLow.Y - this.toolDetail.Top - MARGIN;
    }
  }
}

