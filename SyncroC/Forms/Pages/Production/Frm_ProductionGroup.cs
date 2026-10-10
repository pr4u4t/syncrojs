using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Production
{
	public class Frm_ProductionGroup : SyncroC.Forms.Pages.Frm_Page
	{
		private System.ComponentModel.IContainer components = null;

		public Frm_ProductionGroup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Changes icons on the page for the specified accessibility level .
		/// </summary>
		/// <param name="accessibilityLevel">The accessibility level to be set on the page.</param>
		protected override void setAccessibilityIcons(AccessibilityLevel accessibilityLevel)
		{
      //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro

      //switch (accessibilityLevel)
      //{
      //  //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      //  //ABELLO_FI14/08/2010_1. Riviste le password
      //  case AccessibilityLevel.NoPassword:
      //  case AccessibilityLevel.Low:
      //  case AccessibilityLevel.Medium:
      //    tab_Tab2.Image = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
      //    this.FKeyEnable[(int)FKEYS.F2] = false;
      //    break;
      //  case AccessibilityLevel.High:
      //    tab_Tab2.Image = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
      //    this.FKeyEnable[(int)FKEYS.F2] = true;
      //    break;
      //}
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

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (PagesHistorin.CurrentPageProduction) for the State group is set here.
		/// </summary>
		public override void _Show()
		{		
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageProduction = this.getRequestCommand();
			pm.pagesHistorian.PageChanged(this);
			pm.UpdateCache(this);
			base._Show();
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ProductionGroup));
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
      this.lbl_Title_YN.CausesValidation = false;
      // 
      // lbl_Time
      // 
      this.lbl_Time.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      this.lbl_Time.CausesValidation = false;
      // 
      // lbl_Date
      // 
      this.lbl_Date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      this.lbl_Date.CausesValidation = false;
      // 
      // lbl_Status
      // 
      this.lbl_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      this.lbl_Status.CausesValidation = false;
      // 
      // tab_Tab1
      // 
      this.tab_Tab1.CausesValidation = false;
      this.tab_Tab1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(2)))), ((int)(((byte)(28)))));
      this.tab_Tab1.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      this.tab_Tab1.TabSelection = SyncroC.Controls.TabSelectionType.UnselectedAlarm;
      this.tab_Tab1.Text = "Alarms";
      this.tab_Tab1.Visible = true;
      // 
      // tab_Tab2
      // 
      this.tab_Tab2.CausesValidation = false;
      this.tab_Tab2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(216)))), ((int)(((byte)(0)))));
      this.tab_Tab2.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      this.tab_Tab2.TabSelection = SyncroC.Controls.TabSelectionType.UnselectedWarning;
      this.tab_Tab2.Text = "Warnings";
      this.tab_Tab2.Visible = true;
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.CausesValidation = false;
      this.tab_Tab3.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      this.tab_Tab3.Text = "Home";
      this.tab_Tab3.Visible = true;
      // 
      // tab_Tab4
      // 
      this.tab_Tab4.CausesValidation = false;
      this.tab_Tab4.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      this.tab_Tab4.Text = "Operator";
      this.tab_Tab4.Visible = true;
      // 
      // tab_Tab5
      // 
      this.tab_Tab5.CausesValidation = false;
      this.tab_Tab5.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      this.tab_Tab5.Text = "Quality";
      this.tab_Tab5.Visible = true;
      // 
      // tab_Tab6
      // 
      this.tab_Tab6.CausesValidation = false;
      this.tab_Tab6.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      this.tab_Tab6.Text = "Paper";
      this.tab_Tab6.Visible = true;
      // 
      // tab_Tab7
      // 
      this.tab_Tab7.CausesValidation = false;
      this.tab_Tab7.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      this.tab_Tab7.Text = "Order lists";
      this.tab_Tab7.Visible = true;
      // 
      // tab_Tab8
      // 
      this.tab_Tab8.CausesValidation = false;
      this.tab_Tab8.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      this.tab_Tab8.Text = "Order details";
      this.tab_Tab8.Visible = true;
      // 
      // tab_Tab9
      // 
      this.tab_Tab9.CausesValidation = false;
      // 
      // tab_TabA
      // 
      this.tab_TabA.CausesValidation = false;
      this.tab_TabA.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      // 
      // tab_TabD
      // 
      this.tab_TabD.CausesValidation = false;
      this.tab_TabD.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      // 
      // tab_TabC
      // 
      this.tab_TabC.CausesValidation = false;
      this.tab_TabC.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
      // 
      // tab_TabB
      // 
      this.tab_TabB.CausesValidation = false;
      this.tab_TabB.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(104)))), ((int)(((byte)(128)))));
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
      this.lbl_CtrlF3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
      this.lbl_CtrlF3.CausesValidation = false;
      // 
      // lbl_CtrlF5
      // 
      this.lbl_CtrlF5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
      this.lbl_CtrlF5.CausesValidation = false;
      // 
      // lbl_CtrlF2
      // 
      this.lbl_CtrlF2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
      this.lbl_CtrlF2.CausesValidation = false;
      // 
      // lbl_CtrlF1
      // 
      this.lbl_CtrlF1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
      this.lbl_CtrlF1.CausesValidation = false;
      // 
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
      this.lbl_CtrlF4.CausesValidation = false;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
      this.lbl_CtrlF4_YY.CausesValidation = false;
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
      this.lbl_CtrlF1_YY.CausesValidation = false;
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
      this.lbl_CtrlF2_YY.CausesValidation = false;
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
      this.lbl_CtrlF5_YY.CausesValidation = false;
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(178)))), ((int)(((byte)(190)))));
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
      this.btn_Group1.Active = true;
      this.btn_Group1.Auto = true;
      this.btn_Group1.CausesValidation = false;
      this.btn_Group1.InLocal = true;
      this.btn_Group1.OnLine = true;
      this.btn_Group1.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedActiveImage")));
      this.btn_Group1.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group1.UserDefinedImage")));
      // 
      // btn_Group2
      // 
      this.btn_Group2.CausesValidation = false;
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
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // Frm_ProductionGroup
      // 
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.CausesValidation = false;
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_ProductionGroup";
      this.Text = "Frm_ProductionGroup";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Pages of the State group must do nothing on ShiftF1.
		/// </summary>
		protected override void ShiftF1()
		{
		}

		/// <summary>
		/// F1 key behaviour
		/// </summary>
		protected override void F1()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageProduction_Alarms);
		}

		/// <summary>
		/// F2 key behaviour
		/// </summary>
    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro
    //protected override void F2()
    //{
    //  //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
    //  ///if (Accessibility.getAccessibility().isLevelAtLeastMedium())
    //  if (Accessibility.getAccessibility().isLevelHigh())
    //  {
    //    CommunicationManager cm = CommunicationManager.GetCommunicationManager();
    //    cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem);
    //  }
    //}
    protected override void F2()
    {
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageProduction_Warnings);
    }


		/// <summary>
		/// F3 key behaviour
		/// </summary>
		protected override void F3()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageProduction_Home);
		}

		/// <summary>
		/// F4 key behaviour
		/// </summary>
		protected override void F4()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageProduction_Operator);
		}

		/// <summary>
		/// F5 key behaviour
		/// </summary>
		protected override void F5()
		{
			askForPage(typeof(Frm_Quality));
		}

		/// <summary>
		/// F6 key behaviour
		/// </summary>
		protected override void F6()
		{
			askForPage(typeof(Frm_Paper));
		}

		protected override void F7()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageProduction_OrdersList);
		}

		protected override void F8()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageProduction_OrdersIns);
		}
	}
}

