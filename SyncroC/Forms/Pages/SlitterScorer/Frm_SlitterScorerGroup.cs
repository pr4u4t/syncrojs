using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SlitterScorer
{
	public class Frm_SlitterScorerGroup : SyncroC.Forms.Pages.Frm_Page
	{
		private System.ComponentModel.IContainer components = null;
		
		//PMASSARO_FI04/11/08 - Disable the visualization in case of OldSS without control from Syncro
		private SyncroC.DataBase.LineConfig.SlitterScorer ss;
		
		public Frm_SlitterScorerGroup()
		{
			InitializeComponent();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SlitterScorerGroup));
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
			this.SuspendLayout();
			// 
			// lbl_Title_YN
			// 
			this.lbl_Title_YN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
			// 
			// lbl_Time
			// 
			this.lbl_Time.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			// 
			// lbl_Date
			// 
			this.lbl_Date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			// 
			// lbl_Status
			// 
			this.lbl_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			// 
			// tab_Tab1
			// 
			this.tab_Tab1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(2)))), ((int)(((byte)(28)))));
			this.tab_Tab1.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			this.tab_Tab1.TabSelection = SyncroC.Controls.TabSelectionType.UnselectedAlarm;
			this.tab_Tab1.Text = "Alarms";
			this.tab_Tab1.Visible = true;
			// 
			// tab_Tab2
			// 
			this.tab_Tab2.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			this.tab_Tab2.Text = "Settings";
			this.tab_Tab2.Visible = true;
			// 
			// tab_Tab3
			// 
			this.tab_Tab3.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			this.tab_Tab3.Text = "Status";
			this.tab_Tab3.Visible = true;
			// 
			// tab_Tab4
			// 
			this.tab_Tab4.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			this.tab_Tab4.Text = "Operator";
			this.tab_Tab4.Visible = true;
			// 
			// tab_Tab5
			// 
			this.tab_Tab5.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			// 
			// tab_Tab6
			// 
			this.tab_Tab6.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			// 
			// tab_Tab7
			// 
			this.tab_Tab7.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			// 
			// tab_Tab8
			// 
			this.tab_Tab8.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			// 
			// tab_TabA
			// 
			this.tab_TabA.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			// 
			// tab_TabD
			// 
			this.tab_TabD.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			// 
			// tab_TabC
			// 
			this.tab_TabC.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			// 
			// tab_TabB
			// 
			this.tab_TabB.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(128)))), ((int)(((byte)(193)))));
			// 
			// pct_Home_NY
			// 
			this.pct_Home_NY.Image = ((System.Drawing.Image)(resources.GetObject("pct_Home_NY.Image")));
			// 
			// pct_Back_NY
			// 
			this.pct_Back_NY.Image = ((System.Drawing.Image)(resources.GetObject("pct_Back_NY.Image")));
			// 
			// pct_Forward_NY
			// 
			this.pct_Forward_NY.Image = ((System.Drawing.Image)(resources.GetObject("pct_Forward_NY.Image")));
			// 
			// lbl_CtrlF3
			// 
			this.lbl_CtrlF3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
			// 
			// lbl_CtrlF5
			// 
			this.lbl_CtrlF5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
			// 
			// lbl_CtrlF2
			// 
			this.lbl_CtrlF2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
			// 
			// lbl_CtrlF1
			// 
			this.lbl_CtrlF1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
			// 
			// lbl_CtrlF4
			// 
			this.lbl_CtrlF4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
			// 
			// lbl_CtrlF4_YY
			// 
			this.lbl_CtrlF4_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
			// 
			// lbl_CtrlF1_YY
			// 
			this.lbl_CtrlF1_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
			// 
			// lbl_CtrlF2_YY
			// 
			this.lbl_CtrlF2_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
			// 
			// lbl_CtrlF5_YY
			// 
			this.lbl_CtrlF5_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
			// 
			// lbl_CtrlF3_YY
			// 
			this.lbl_CtrlF3_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(192)))), ((int)(((byte)(224)))));
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
			// Frm_SlitterScorerGroup
			// 
			this.ClientSize = new System.Drawing.Size(1280, 738);
			this.Name = "Frm_SlitterScorerGroup";
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (pagesHistorian.CurrentPageSlitterScorer) for the SlitterScorer Group.
		/// </summary>
		public override void _Show()
		{
			//PMASSARO_FI04/11/08 - Disable the visualization in case of OldSS without control from Syncro
			ss = DBManager.getDBManager().lineConfig.SlitterScorerConfig;
			if ( (!ss.SlitterScorerHeader.machineControlIntegratedInSyncro) && ss.IsCompact2() )
			{
			  this.tab_Tab4.Visible = false;
			  this.FKeyEnable[ (int)FKEYS.F4] = false;			  
			}			
			
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageSlitterScorer = this.getRequestCommand();
			pm.pagesHistorian.PageChanged(this);
			pm.UpdateCache(this);

			base._Show();
		}

		protected override void setAccessibilityIcons(AccessibilityLevel accessibilityLevel)
		{
			switch (accessibilityLevel)
			{
        //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
        //ABELLO_FI14/08/2010_1. Riviste le password
        case AccessibilityLevel.NoPassword:
        case AccessibilityLevel.Low:
        case AccessibilityLevel.Medium:
					tab_Tab2.Image = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
					this.FKeyEnable[(int)FKEYS.F2] = false;
					break;
				case AccessibilityLevel.High:
					tab_Tab2.Image = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
					this.FKeyEnable[(int)FKEYS.F2] = true;
					break;
			}
		}

		protected override void AltF4()
		{
		}

		protected override void F1()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSlitterScorerAlarms);
		}
		protected override void F2()
		{
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      ///if (Accessibility.getAccessibility().isLevelAtLeastMedium())
      if (Accessibility.getAccessibility().isLevelHigh())
      {
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSlitterScorerSettings);
			}
		}
		protected override void F3()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSlitterScorerStatus);
		}
		protected override void F4()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSlitterScorerTools);
		}
		//protected override void F5()
		//{
		//  //CommunicationManager cm = CommunicationManager.GetCommunicationManager();
		//  //cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSlitterScorerBlades);
		//}
		
	}
}

