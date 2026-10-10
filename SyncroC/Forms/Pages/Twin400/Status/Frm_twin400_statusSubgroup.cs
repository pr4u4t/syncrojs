using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Twin400.Status
{
	public class Frm_twin400_statusSubgroup : SyncroC.Forms.Pages.Twin400.Frm_Twin400Group
	{
		private System.ComponentModel.IContainer components = null;

		public Frm_twin400_statusSubgroup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (Historian.CurrentPageTwin400_Status) for the Twin400_Status subgroup is set here.
		/// </summary>
		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageTwin400_Status = this.getRequestCommand();
			base._Show();
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

		protected override void F10()
		{
			askForPage(typeof(Frm_twin400_overview));
		}

		//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
		//protected override void F10()
		//{
		//  askForPage(typeof(Frm_twin400_positioning));
		//}

		protected override void F11()
		{
			askForPage(typeof(Frm_twin400_console));
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_twin400_blades));
		}

		protected override void F13()
		{
			askForPage(typeof(Frm_twin400_regulations));
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_statusSubgroup));
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_Tab4
      // 
      this.tab_Tab4.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab4.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // tab_TabA
      // 
      this.tab_TabA.Text = "Overview";
      this.tab_TabA.Visible = true;
      // 
      // tab_TabD
      // 
      this.tab_TabD.Text = "Regulations";
      this.tab_TabD.Visible = true;
      // 
      // tab_TabC
      // 
      this.tab_TabC.Text = "Blades";
      this.tab_TabC.Visible = true;
      // 
      // tab_TabB
      // 
      this.tab_TabB.Text = "Console";
      this.tab_TabB.Visible = true;
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
      // Frm_twin400_statusSubgroup
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_twin400_statusSubgroup";
      this.Text = "Frm_twin400_statusSubgroup";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		//RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
		//protected override void setAccessibilityIcons(AccessibilityLevel accessibilityLevel)
		//{
		//  switch (accessibilityLevel)
		//  {
		//    case AccessibilityLevel.Low:
		//      tab_TabB.Image = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
		//      this.FKeyEnable[(int)FKEYS.F10] = false;
		//      break;
		//    case AccessibilityLevel.Medium:
		//    case AccessibilityLevel.High:
		//      tab_TabB.Image = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
		//      this.FKeyEnable[(int)FKEYS.F10] = true;
		//      break;
		//  }
		//}
		protected override void setAccessibilityIcons(AccessibilityLevel accessibilityLevel)
		{
			switch (accessibilityLevel)
			{
        //ABELLO_FI14/08/2010_1. Riviste le password
        case AccessibilityLevel.NoPassword:
					tab_TabD.Image = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
					this.FKeyEnable[(int)FKEYS.F13] = false;
					break;
        case AccessibilityLevel.Low:
				case AccessibilityLevel.Medium:
				case AccessibilityLevel.High:
					tab_TabD.Image = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
					this.FKeyEnable[(int)FKEYS.F13] = true;
					break;
			}
		}
	}
}

