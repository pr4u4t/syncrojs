using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Network;

namespace SyncroC.Forms.Pages.SyncroSystem
{
	public class Frm_SyncroSystemGroup : SyncroC.Forms.Pages.Frm_Page
	{
		private System.ComponentModel.IContainer components = null;

		public Frm_SyncroSystemGroup()
		{
			InitializeComponent();
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

		protected override bool IncludeComponentsIntoLanguageDB()
		{
			return false;
		}

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (PagesHistorin.CurrentPageSyncroSystem) for the State group is set here.
		/// </summary>
		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageSyncroSystem = this.getRequestCommand();
			pm.pagesHistorian.PageChanged(this);
			pm.UpdateCache(this);
			base._Show();
		}

		protected override void F1()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_AOC_WEC);
		}

		protected override void F2()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_Speed);
		}

		protected override void F3()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_SpeedReference);
		}

		protected override void F4()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_Syncro);
		}

    protected override void F5()
    {
        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_Printers);
    }

		protected override void F6()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_Office);
		}

		protected override void F7()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_Log);
		}

    protected override void F8()
    {
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageSyncroSystem_Operations);
    }

    #region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SyncroSystemGroup));
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Time
      // 
      this.lbl_Time.BackColor = System.Drawing.Color.Black;
      // 
      // lbl_Date
      // 
      this.lbl_Date.BackColor = System.Drawing.Color.Black;
      // 
      // lbl_Status
      // 
      this.lbl_Status.BackColor = System.Drawing.Color.Black;
      // 
      // tab_Tab1
      // 
      this.tab_Tab1.SelectedBackGroundColor = System.Drawing.Color.Black;
      this.tab_Tab1.Text = "AOC - WEC";
      this.tab_Tab1.Visible = true;
      // 
      // tab_Tab2
      // 
      this.tab_Tab2.SelectedBackGroundColor = System.Drawing.Color.Black;
      this.tab_Tab2.Text = "SPEED";
      this.tab_Tab2.Visible = true;
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.SelectedBackGroundColor = System.Drawing.Color.Black;
      this.tab_Tab3.Text = "SPEED REF";
      this.tab_Tab3.Visible = true;
      // 
      // tab_Tab4
      // 
      this.tab_Tab4.SelectedBackGroundColor = System.Drawing.Color.Black;
      this.tab_Tab4.Text = "SYNCRO";
      this.tab_Tab4.Visible = true;
      // 
      // tab_Tab5
      // 
      this.tab_Tab5.SelectedBackGroundColor = System.Drawing.Color.Black;
      this.tab_Tab5.Text = "PRINTERS";
      this.tab_Tab5.Visible = true;
      // 
      // tab_Tab6
      // 
      this.tab_Tab6.SelectedBackGroundColor = System.Drawing.Color.Black;
      this.tab_Tab6.Text = "OFFICE/PRC";
      this.tab_Tab6.Visible = true;
      // 
      // tab_Tab7
      // 
      this.tab_Tab7.SelectedBackGroundColor = System.Drawing.Color.Black;
      this.tab_Tab7.Text = "LOG";
      this.tab_Tab7.Visible = true;
      // 
      // tab_Tab8
      // 
      this.tab_Tab8.SelectedBackGroundColor = System.Drawing.Color.Black;
      this.tab_Tab8.Text = "Operations";
      this.tab_Tab8.Visible = true;
      // 
      // tab_TabA
      // 
      this.tab_TabA.SelectedBackGroundColor = System.Drawing.Color.Black;
      // 
      // tab_TabD
      // 
      this.tab_TabD.SelectedBackGroundColor = System.Drawing.Color.Black;
      // 
      // tab_TabC
      // 
      this.tab_TabC.SelectedBackGroundColor = System.Drawing.Color.Black;
      // 
      // tab_TabB
      // 
      this.tab_TabB.SelectedBackGroundColor = System.Drawing.Color.Black;
      // 
      // pct_Home_NY
      // 
      this.pct_Home_NY.BackColor = System.Drawing.Color.Black;
      this.pct_Home_NY.Image = ((System.Drawing.Image)(resources.GetObject("pct_Home_NY.Image")));
      // 
      // pct_Back_NY
      // 
      this.pct_Back_NY.BackColor = System.Drawing.Color.Black;
      this.pct_Back_NY.Image = ((System.Drawing.Image)(resources.GetObject("pct_Back_NY.Image")));
      // 
      // pct_Forward_NY
      // 
      this.pct_Forward_NY.BackColor = System.Drawing.Color.Black;
      this.pct_Forward_NY.Image = ((System.Drawing.Image)(resources.GetObject("pct_Forward_NY.Image")));
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
      // Frm_SyncroSystemGroup
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_SyncroSystemGroup";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion
	}
}

