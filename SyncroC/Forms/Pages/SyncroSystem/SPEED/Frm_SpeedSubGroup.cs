using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.SyncroSystem.SPEED
{
	public class Frm_SpeedSubGroup : SyncroC.Forms.Pages.SyncroSystem.Frm_SyncroSystemGroup
	{
		private System.ComponentModel.IContainer components = null;

		public Frm_SpeedSubGroup()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
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

		protected override void F2()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_SpeedSettings));
		}

		protected override void F11()
		{
			askForPage(typeof(Frm_SpeedStatus));
		}

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (Historian.CurrentPageSyncroSystem_Speed) for the SyncroSystem_Speed subgroup is set here.
		/// </summary>
		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageSyncroSystem_Speed = this.getRequestCommand();
			base._Show();
		}
		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SpeedSubGroup));
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_Tab2
      // 
      this.tab_Tab2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab2.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // tab_TabA
      // 
      this.tab_TabA.Text = "Settings";
      this.tab_TabA.Visible = true;
      // 
      // tab_TabB
      // 
      this.tab_TabB.Text = "Status";
      this.tab_TabB.Visible = true;
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
      // Frm_SpeedSubGroup
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_SpeedSubGroup";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion
	}
}

