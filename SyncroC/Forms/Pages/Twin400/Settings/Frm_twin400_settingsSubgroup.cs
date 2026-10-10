using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;

namespace SyncroC.Forms.Pages.Twin400.Settings
{
	/// <summary>
	/// Form where sub group of twin400 settings pages inherith from.
	/// </summary>
	public class Frm_twin400_settingsSubgroup : SyncroC.Forms.Pages.Twin400.Frm_Twin400Group
	{
		private System.ComponentModel.IContainer components = null;

		public Frm_twin400_settingsSubgroup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (Historian.CurrentPageTwin400_Settings) for the Twin400_Settings subgroup is set here.
		/// </summary>
		public override void _Show()
		{		
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageTwin400_Settings = this.getRequestCommand();
			base._Show();
		}

		/// <summary>
		/// Changes icons on the page for the specified accessibility level .
		/// </summary>
		/// <param name="accessibilityLevel">The accessibility level to be set on the page.</param>
		protected override void setAccessibilityIcons(AccessibilityLevel accessibilityLevel)
		{
			switch (accessibilityLevel)
			{
        //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
        //ABELLO_FI14/08/2010_1. Riviste le password
        case AccessibilityLevel.NoPassword:
        case AccessibilityLevel.Low:
        case AccessibilityLevel.Medium: 
          tab_TabB.Image = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
					tab_TabC.Image = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
          tab_TabD.Image = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
          this.FKeyEnable[(int)FKEYS.F11] = false;
          this.FKeyEnable[(int)FKEYS.F12] = false;
          this.FKeyEnable[(int)FKEYS.F13] = false;
          break;
        case AccessibilityLevel.High:
					tab_TabB.Image = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
          tab_TabC.Image = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
          tab_TabD.Image = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
          this.FKeyEnable[(int)FKEYS.F11] = true;
          this.FKeyEnable[(int)FKEYS.F12] = true;
          this.FKeyEnable[(int)FKEYS.F13] = true;
          break;
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

    //RFAZZI_FI26/01/2011_1. Aggiunta gestione Warning su grafica di Syncro. 
		//protected override void F2()
		protected override void F3()
		{
		}

		protected override void F11()
		{
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      ///if (Accessibility.getAccessibility().isLevelAtLeastMedium())
      if (Accessibility.getAccessibility().isLevelHigh())
      {
				askForPage(typeof(Frm_twin400_machineSettings));
			}
		}

		protected override void F13()
		{
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      ///if (Accessibility.getAccessibility().isLevelAtLeastMedium())
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        askForPage(typeof(Frm_twin400_tasks));
			}
		}

    protected override void F12()
    {
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      ///if (Accessibility.getAccessibility().isLevelAtLeastMedium())
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        askForPage(typeof(Frm_twin400_plunges));
			}
    }

    protected override void F10()
    {
			askForPage(typeof(Frm_twin400_misc));
    }

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_settingsSubgroup));
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "TWIN 400 SETTINGS";
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab3.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // tab_TabA
      // 
      this.tab_TabA.Text = "Languages";
      this.tab_TabA.Visible = true;
      // 
      // tab_TabD
      // 
      this.tab_TabD.Text = "Tasks";
      this.tab_TabD.Visible = true;
      // 
      // tab_TabC
      // 
      this.tab_TabC.Text = "Plunges";
      this.tab_TabC.Visible = true;
      // 
      // tab_TabB
      // 
      this.tab_TabB.Text = "Machine";
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
      // Frm_twin400_settingsSubgroup
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_twin400_settingsSubgroup";
      this.Text = "Frm_twin400_settingsSubgroup";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    //public override void ConfigPage(SyncroC.Utility.Settings.Configuration config)
    //{
    //  base.ConfigPage(config);
    //  this.FKeyEnable[(int)FKEYS.F13] = this.tab_TabD.Visible = config.IsPmac400();
    //}

	}
}

