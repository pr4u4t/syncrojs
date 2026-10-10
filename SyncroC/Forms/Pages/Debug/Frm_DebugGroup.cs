using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Network;

namespace SyncroC.Forms.Pages.Debug
{
	public class Frm_DebugGroup : SyncroC.Forms.Pages.Frm_Page
	{
		private System.ComponentModel.IContainer components = null;

		public Frm_DebugGroup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Excludes components of this form or of all the inheritors from the language database.
		/// To include them into the database let this method return true, or remove it.
		/// </summary>
		/// <returns></returns>
		protected override bool IncludeComponentsIntoLanguageDB()
		{
			return false;
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
		/// Last visited page (Historian.CurrentPageDebug) for the Debug group is set here.
		/// </summary>
		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageDebug = this.getRequestCommand();
			pm.pagesHistorian.PageChanged(this);
			pm.UpdateCache(this);
			base._Show();
		}

		protected override void F1()
		{
			askForPage(typeof(Frm_DebugDatabase));
		}
		protected override void F2()
		{
			askForPage(typeof(Frm_DebugINI));
		}
    protected override void F3()
    {
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, PageManager.getPageManager().pagesHistorian.CurrentPageDebugTwin400);
    }
    //agrassi_FI25-05-2011_1
    protected override void F4()
    {
      askForPage(typeof(Frm_DebugTwin2));
    }

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DebugGroup));
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      // 
      // lbl_Time
      // 
      this.lbl_Time.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      // 
      // lbl_Date
      // 
      this.lbl_Date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      // 
      // lbl_Status
      // 
      this.lbl_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      // 
      // tab_Tab1
      // 
      this.tab_Tab1.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      this.tab_Tab1.Text = "Database";
      this.tab_Tab1.Visible = true;
      // 
      // tab_Tab2
      // 
      this.tab_Tab2.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      this.tab_Tab2.Text = "INI settings";
      this.tab_Tab2.Visible = true;
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      this.tab_Tab3.Text = "Twin400";
      this.tab_Tab3.Visible = true;
      // 
      // tab_Tab4
      // 
      this.tab_Tab4.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      this.tab_Tab4.Text = "Twin2";
      this.tab_Tab4.Visible = true;
      // 
      // tab_Tab5
      // 
      this.tab_Tab5.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      // 
      // tab_Tab6
      // 
      this.tab_Tab6.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      // 
      // tab_Tab7
      // 
      this.tab_Tab7.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      // 
      // tab_Tab8
      // 
      this.tab_Tab8.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      // 
      // tab_TabA
      // 
      this.tab_TabA.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      // 
      // tab_TabD
      // 
      this.tab_TabD.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      // 
      // tab_TabC
      // 
      this.tab_TabC.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      // 
      // tab_TabB
      // 
      this.tab_TabB.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
      // 
      // pct_Home_NY
      // 
      this.pct_Home_NY.Image = global::SyncroC.Properties.Resources.debug_04;
      // 
      // pct_Back_NY
      // 
      this.pct_Back_NY.Image = global::SyncroC.Properties.Resources.debug_02;
      // 
      // pct_Forward_NY
      // 
      this.pct_Forward_NY.Image = global::SyncroC.Properties.Resources.debug_03;
      // 
      // lbl_CtrlF3
      // 
      this.lbl_CtrlF3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      // 
      // lbl_CtrlF5
      // 
      this.lbl_CtrlF5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      // 
      // lbl_CtrlF2
      // 
      this.lbl_CtrlF2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      // 
      // lbl_CtrlF1
      // 
      this.lbl_CtrlF1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      // 
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
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
      this.btn_Group5.Active = true;
      this.btn_Group5.Auto = true;
      this.btn_Group5.InLocal = true;
      this.btn_Group5.OnLine = true;
      this.btn_Group5.UserDefinedActiveImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedActiveImage")));
      this.btn_Group5.UserDefinedImage = ((System.Drawing.Image)(resources.GetObject("btn_Group5.UserDefinedImage")));
      this.btn_Group5.Visible = true;
      // 
      // iml_AccessibilityImages
      // 
      this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
      this.iml_AccessibilityImages.Images.SetKeyName(0, "Lock_Blue.jpg");
      this.iml_AccessibilityImages.Images.SetKeyName(1, "unLock_blue.jpg");
      // 
      // Frm_DebugGroup
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_DebugGroup";
      this.Text = "Frm_DebugGroup";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion
	}
}

