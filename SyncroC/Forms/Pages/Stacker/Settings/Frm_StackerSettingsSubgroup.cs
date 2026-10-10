using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Stacker.Settings
{
	public class Frm_StackerSettingsSubgroup : SyncroC.Forms.Pages.Stacker.Frm_StackerGroup
	{
		private System.ComponentModel.IContainer components = null;

		public Frm_StackerSettingsSubgroup()
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

		/// <summary>
		/// Excludes components of this form or of all the inheritors from the language database.
		/// To include them into the database let this method return true, or remove it.
		/// </summary>
		/// <returns></returns>
		protected override bool IncludeComponentsIntoLanguageDB()
		{
			return false;
		}

		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			switch (index)
			{
				case 1:
				default:
					pm.pagesHistorian.CurrentPageStackerSettings1 = this.getRequestCommand(index);
					break;
				case 2:
					pm.pagesHistorian.CurrentPageStackerSettings2 = this.getRequestCommand(index);
					break;
				case 3:
					pm.pagesHistorian.CurrentPageStackerSettings3 = this.getRequestCommand(index);
					break;
			}
			base._Show();
		}

		protected override void F2()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_StackerSettingsMachine), index);
		}


		protected override void F11()
		{
			askForPage(typeof(Frm_StackerSettingsTask), index);
		}

		protected override void F12()
		{
			askForPage(typeof(Frm_StackerSettingsStatus), index);
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StackerSettingsSubgroup));
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
      this.tab_TabA.Text = "Machine";
      this.tab_TabA.Visible = true;
      // 
      // tab_TabC
      // 
      this.tab_TabC.Text = "Status";
      this.tab_TabC.Visible = true;
      // 
      // tab_TabB
      // 
      this.tab_TabB.Text = "Task";
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
      // Frm_StackerSettingsSubgroup
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_StackerSettingsSubgroup";
      this.Text = "Frm_StackerSettingsSubgroup";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion
	}
}

