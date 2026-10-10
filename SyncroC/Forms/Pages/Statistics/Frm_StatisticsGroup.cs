using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Network;

namespace SyncroC.Forms.Pages.Statistics
{
	public class Frm_StatisticsGroup : SyncroC.Forms.Pages.Frm_Page
	{
		/// <summary>
		/// Position of the order currently showed on the screen.
		/// (static because it is used also in other groups such as orderList, StatusBar
		/// </summary>
		public static int positionOrderToShowPastDRY = 1;
    //ABELLO_FI25/08/2010_1 se uso Postgres il doppio click sulla lista degli eseguiti andava a casaccio
    public static string filtro = "";

		private System.ComponentModel.IContainer components = null;

		public Frm_StatisticsGroup()
		{
			InitializeComponent();
		}

		public override void ConfigPage(SyncroC.Utility.Settings.Configuration config)
		{
			base.ConfigPage(config);
			this.FKeyEnable[(int)FKEYS.F4] = this.tab_Tab4.Visible = config.WetEndControl;
      this.FKeyEnable[(int)FKEYS.F5] = this.tab_Tab5.Visible = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StatisticsGroup));
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(177)))), ((int)(((byte)(194)))));
      this.lbl_Title_YN.CausesValidation = false;
      // 
      // lbl_Time
      // 
      this.lbl_Time.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      this.lbl_Time.CausesValidation = false;
      // 
      // lbl_Date
      // 
      this.lbl_Date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      this.lbl_Date.CausesValidation = false;
      // 
      // lbl_Status
      // 
      this.lbl_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      this.lbl_Status.CausesValidation = false;
      // 
      // tab_Tab1
      // 
      this.tab_Tab1.CausesValidation = false;
      this.tab_Tab1.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      this.tab_Tab1.Text = "Shift report";
      this.tab_Tab1.Visible = true;
      // 
      // tab_Tab2
      // 
      this.tab_Tab2.CausesValidation = false;
      this.tab_Tab2.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      this.tab_Tab2.Text = "Downtime";
      this.tab_Tab2.Visible = true;
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.CausesValidation = false;
      this.tab_Tab3.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      this.tab_Tab3.Text = "Orders";
      this.tab_Tab3.Visible = true;
      // 
      // tab_Tab4
      // 
      this.tab_Tab4.CausesValidation = false;
      this.tab_Tab4.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      this.tab_Tab4.Text = "Rolls";
      this.tab_Tab4.Visible = true;
      // 
      // tab_Tab5
      // 
      this.tab_Tab5.CausesValidation = false;
      this.tab_Tab5.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      this.tab_Tab5.Text = "Record";
      // 
      // tab_Tab6
      // 
      this.tab_Tab6.CausesValidation = false;
      this.tab_Tab6.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      // 
      // tab_Tab7
      // 
      this.tab_Tab7.CausesValidation = false;
      this.tab_Tab7.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      // 
      // tab_Tab8
      // 
      this.tab_Tab8.CausesValidation = false;
      this.tab_Tab8.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      // 
      // tab_TabA
      // 
      this.tab_TabA.CausesValidation = false;
      this.tab_TabA.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      // 
      // tab_TabD
      // 
      this.tab_TabD.CausesValidation = false;
      this.tab_TabD.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      // 
      // tab_TabC
      // 
      this.tab_TabC.CausesValidation = false;
      this.tab_TabC.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      // 
      // tab_TabB
      // 
      this.tab_TabB.CausesValidation = false;
      this.tab_TabB.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(100)))), ((int)(((byte)(129)))));
      // 
      // pct_Home_NY
      // 
      this.pct_Home_NY.CausesValidation = false;
      this.pct_Home_NY.Image = ((System.Drawing.Image)(resources.GetObject("pct_Home_NY.Image")));
      // 
      // pct_Back_NY
      // 
      this.pct_Back_NY.CausesValidation = false;
      this.pct_Back_NY.Image = ((System.Drawing.Image)(resources.GetObject("pct_Back_NY.Image")));
      // 
      // pct_Forward_NY
      // 
      this.pct_Forward_NY.CausesValidation = false;
      this.pct_Forward_NY.Image = ((System.Drawing.Image)(resources.GetObject("pct_Forward_NY.Image")));
      // 
      // lbl_CtrlF3
      // 
      this.lbl_CtrlF3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(177)))), ((int)(((byte)(194)))));
      this.lbl_CtrlF3.CausesValidation = false;
      // 
      // lbl_CtrlF5
      // 
      this.lbl_CtrlF5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(177)))), ((int)(((byte)(194)))));
      this.lbl_CtrlF5.CausesValidation = false;
      // 
      // lbl_CtrlF2
      // 
      this.lbl_CtrlF2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(177)))), ((int)(((byte)(194)))));
      this.lbl_CtrlF2.CausesValidation = false;
      // 
      // lbl_CtrlF1
      // 
      this.lbl_CtrlF1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(177)))), ((int)(((byte)(194)))));
      this.lbl_CtrlF1.CausesValidation = false;
      // 
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(177)))), ((int)(((byte)(194)))));
      this.lbl_CtrlF4.CausesValidation = false;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(177)))), ((int)(((byte)(194)))));
      this.lbl_CtrlF4_YY.CausesValidation = false;
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(177)))), ((int)(((byte)(194)))));
      this.lbl_CtrlF1_YY.CausesValidation = false;
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(177)))), ((int)(((byte)(194)))));
      this.lbl_CtrlF2_YY.CausesValidation = false;
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(177)))), ((int)(((byte)(194)))));
      this.lbl_CtrlF5_YY.CausesValidation = false;
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(177)))), ((int)(((byte)(194)))));
      this.lbl_CtrlF3_YY.CausesValidation = false;
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
      this.btn_Group1.CausesValidation = false;
      this.btn_Group1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedActiveImage")));
      this.btn_Group1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedImage")));
      // 
      // btn_Group2
      // 
      this.btn_Group2.Active = true;
      this.btn_Group2.Auto = true;
      this.btn_Group2.CausesValidation = false;
      this.btn_Group2.InLocal = true;
      this.btn_Group2.OnLine = true;
      this.btn_Group2.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedActiveImage")));
      this.btn_Group2.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group2.UserDefinedImage")));
      // 
      // btn_Group3
      // 
      this.btn_Group3.CausesValidation = false;
      this.btn_Group3.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedActiveImage")));
      this.btn_Group3.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group3.UserDefinedImage")));
      // 
      // btn_Group5
      // 
      this.btn_Group5.CausesValidation = false;
      this.btn_Group5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedActiveImage")));
      this.btn_Group5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedImage")));
      // 
      // tab_Tab9
      // 
      this.tab_Tab9.CausesValidation = false;
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // Frm_StatisticsGroup
      // 
      this.CausesValidation = false;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_StatisticsGroup";
      this.Text = "Frm_StatisticsGroup";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (PagesHistorian.CurrentPageStistics) for the Statistics group is set here.
		/// </summary>
		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageStatistics = this.getRequestCommand();
			pm.pagesHistorian.PageChanged(this);
			pm.UpdateCache(this);

			base._Show();
		}

		protected override void ShiftF2()
		{
		}

		/// <summary>
		/// F1 key behaviour: asks for Shift Report page
		/// </summary>
		protected override void F1()
		{
			askForPage(typeof(Frm_ShiftReport));
		}

		/// <summary>
		/// F2 key behaviour: asks for downtime Report page
		/// </summary>
		protected override void F2()
		{
			askForPage(typeof(Frm_DowntimeReport));
		}

		/// <summary>
		/// F3 key behaviour: asks for ORDERS Report page
		/// </summary>
		protected override void F3()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageStatisticsOrders);
		}

		/// <summary>
		/// F3 key behaviour: asks for ORDERS Report page
		/// </summary>
		protected override void F4()
		{
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageStatisticsRolls);
		}

    /// <summary>
    /// F5 key behaviour: asks for Shift Report page
    /// </summary>
    protected override void F5()
    {
      askForPage(typeof(Frm_ProductionTrace));
    }
	}
}

