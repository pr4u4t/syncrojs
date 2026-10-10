using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.DryEndSupervisor
{
	public class Frm_DryEndSupervisorGroup : SyncroC.Forms.Pages.Frm_Page
	{
		private System.ComponentModel.IContainer components = null;

		public Frm_DryEndSupervisorGroup()
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
			return true;
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DryEndSupervisorGroup));
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "Dry End Supervisor";
      // 
      // tab_Tab1
      // 
      this.tab_Tab1.SelectedBackGroundColor = System.Drawing.Color.Silver;
      this.tab_Tab1.Text = "Overview";
      this.tab_Tab1.Visible = true;
      // 
      // tab_Tab2
      // 
      this.tab_Tab2.SelectedBackGroundColor = System.Drawing.Color.Silver;
      this.tab_Tab2.Text = "W. E. C.";
      this.tab_Tab2.Visible = true;
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.SelectedBackGroundColor = System.Drawing.Color.Silver;
      this.tab_Tab3.Text = "M. T. H.";
      this.tab_Tab3.Visible = true;
      // 
      // tab_Tab4
      // 
      this.tab_Tab4.SelectedBackGroundColor = System.Drawing.Color.Silver;
      // 
      // tab_Tab5
      // 
      this.tab_Tab5.SelectedBackGroundColor = System.Drawing.Color.Silver;
      // 
      // tab_Tab6
      // 
      this.tab_Tab6.SelectedBackGroundColor = System.Drawing.Color.Silver;
      // 
      // tab_Tab7
      // 
      this.tab_Tab7.SelectedBackGroundColor = System.Drawing.Color.Silver;
      // 
      // tab_Tab8
      // 
      this.tab_Tab8.SelectedBackGroundColor = System.Drawing.Color.Silver;
      // 
      // tab_TabA
      // 
      this.tab_TabA.SelectedBackGroundColor = System.Drawing.Color.Silver;
      // 
      // tab_TabD
      // 
      this.tab_TabD.SelectedBackGroundColor = System.Drawing.Color.Silver;
      // 
      // tab_TabC
      // 
      this.tab_TabC.SelectedBackGroundColor = System.Drawing.Color.Silver;
      // 
      // tab_TabB
      // 
      this.tab_TabB.SelectedBackGroundColor = System.Drawing.Color.Silver;
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
      // Frm_DryEndSupervisorGroup
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_DryEndSupervisorGroup";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (PagesHistorin.CurrentPageDryEndSupervisor) for the Dry End Supervisor group is set here.
		/// </summary>
		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageDryEndSupervisor = this.getRequestCommand();
			pm.pagesHistorian.PageChanged(this);
			pm.UpdateCache(this);

      if (!this.DesignMode)
      {
        if (!(DBManager.getDBManager().syncroConfig.WecSyncroSetting.BaseComms.Enable || DBManager.getDBManager().syncroConfig.WecBMUSetting.BaseComms.Enable ))
          this.tab_Tab2.Visible = false;

				//RFazzi_FI17/01/2008_1.Added task configurations on graphics
        //if (!(DBManager.getDBManager().syncroConfig.MTHCommsSetting.Enable || DBManager.getDBManager().syncroConfig.MTHDuckerSetting.Enable))
        //agrassi_FI19-11-08
        //if (!(DBManager.getDBManager().syncroConfig.MTHCommsSetting.Enable || DBManager.getDBManager().syncroConfig.MTHDuckerSetting.BaseComms.Enable))
        if (!(DBManager.getDBManager().syncroConfig.MTHCommsSetting.Enable || DBManager.getDBManager().syncroConfig.MTHDuckerSetting.BaseComms.Enable ||
              DBManager.getDBManager().syncroConfig.MthFosberMindaCommsSetting.BaseComms.Enable ||
							//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
							DBManager.getDBManager().syncroConfig.MTHSeemiSetting.BaseComms.Enable 
              //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
              || DBManager.getDBManager().syncroConfig.MthFosberStandardCommsSettings.Mth_01.BaseComms.Enable
              || DBManager.getDBManager().syncroConfig.MthFosberStandardCommsSettings.Mth_02.BaseComms.Enable
              || DBManager.getDBManager().syncroConfig.MthFosberStandardCommsSettings.Mth_03.BaseComms.Enable)
          )
          this.tab_Tab3.Visible = false;
      }

			base._Show();
		}

		/// <summary>
		/// Dry End Supervisor group must not do anything on Shift+F12 push
		/// </summary>
		protected override void AltF1()
		{
		}

		protected override void F1()
		{
			askForPage(typeof(Frm_DryEndSupervisor01));
		}

		protected override void F2()
		{
      if ((DBManager.getDBManager().syncroConfig.WecSyncroSetting.BaseComms.Enable || DBManager.getDBManager().syncroConfig.WecBMUSetting.BaseComms.Enable ))
			  askForPage(typeof(Frm_DryEndSupervisor02));
		}

		protected override void F3()
		{
			//RFazzi_FI17/01/2008_1.Added task configurations on graphics
			//if (DBManager.getDBManager().syncroConfig.MTHCommsSetting.Enable || DBManager.getDBManager().syncroConfig.MTHDuckerSetting.Enable)
      //agrassi_FI22-10-08
      //if (DBManager.getDBManager().syncroConfig.MTHCommsSetting.Enable || DBManager.getDBManager().syncroConfig.MTHDuckerSetting.BaseComms.Enable)
			
			if (DBManager.getDBManager().syncroConfig.MTHCommsSetting.Enable || DBManager.getDBManager().syncroConfig.MTHDuckerSetting.BaseComms.Enable ||
          DBManager.getDBManager().syncroConfig.MthFosberMindaCommsSetting.BaseComms.Enable ||
					//RFAZZI_FI08/07/2009_1.Creato task di comunicazione con movimentazione SEEMI. 
					DBManager.getDBManager().syncroConfig.MTHSeemiSetting.BaseComms.Enable 
          //PMASSARO_FI29/01/09 - Added Fosber MTH Task Settings On Graphic
          || DBManager.getDBManager().syncroConfig.MthFosberStandardCommsSettings.Mth_01.BaseComms.Enable
          || DBManager.getDBManager().syncroConfig.MthFosberStandardCommsSettings.Mth_02.BaseComms.Enable
          || DBManager.getDBManager().syncroConfig.MthFosberStandardCommsSettings.Mth_03.BaseComms.Enable)

        askForPage(typeof(Frm_DryEndSupervisor03));
		}
	}
}

