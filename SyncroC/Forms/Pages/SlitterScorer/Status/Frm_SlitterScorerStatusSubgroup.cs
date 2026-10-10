using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;

namespace SyncroC.Forms.Pages.SlitterScorer.Status
{
	public class Frm_SlitterScorerStatusSubgroup : SyncroC.Forms.Pages.SlitterScorer.Frm_SlitterScorerGroup
	{
		private System.ComponentModel.IContainer components = null;

		//PMASSARO_FI04/11/08 - Disable the visualization in case of OldSS without control from Syncro
		private SyncroC.DataBase.LineConfig.SlitterScorer ss;
		public Frm_SlitterScorerStatusSubgroup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (Historian.CurrentPageTwin400_Status) for the Twin400_Status subgroup is set here.
		/// </summary>
		public override void _Show()
		{
			//PMASSARO_FI04/11/08 - Disable the visualization in case of OldSS without control from Syncro
			ss = DBManager.getDBManager().lineConfig.SlitterScorerConfig;
			if (!ss.SlitterScorerHeader.machineControlIntegratedInSyncro)
			{
			  this.tab_TabC.Visible = false;
			  this.tab_TabD.Visible = false;
			  this.FKeyEnable[(int) FKEYS.F12] = false;
			  this.FKeyEnable[(int) FKEYS.F13] = false;
			}
			
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageSlitterScorerStatus = this.getRequestCommand();
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

		protected override void F3()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_SlitterScorerStatus));
		}

		protected override void F11()
		{
		   askForPage(typeof(Frm_SlitterScorerConsole));
		}

		protected override void F12()
		{
		   askForPage(typeof(Frm_SlitterScorerBlades));
		}

		protected override void F13()
		{
		   askForPage(typeof(Frm_SlitterScorerRegulations));
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SlitterScorerStatusSubgroup));
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
			this.SuspendLayout();
			// 
			// tab_Tab3
			// 
			this.tab_Tab3.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.tab_Tab3.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
			// Frm_SlitterScorerStatusSubgroup
			// 
			this.ClientSize = new System.Drawing.Size(1280, 738);
			this.Name = "Frm_SlitterScorerStatusSubgroup";
			this.Text = "Frm_twin400_statusSubgroup";
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		protected override void setAccessibilityIcons(AccessibilityLevel accessibilityLevel)
		{
			switch (accessibilityLevel)
			{
        //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
        //ABELLO_FI14/08/2010_1. Riviste le password
        case AccessibilityLevel.NoPassword:
        case AccessibilityLevel.Low:
        case AccessibilityLevel.Medium:
					tab_TabD.Image = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
					this.FKeyEnable[(int)FKEYS.F13] = false;
					break;
				case AccessibilityLevel.High:
					tab_TabD.Image = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
					this.FKeyEnable[(int)FKEYS.F13] = true;
					break;
			}
		}
	}
}

