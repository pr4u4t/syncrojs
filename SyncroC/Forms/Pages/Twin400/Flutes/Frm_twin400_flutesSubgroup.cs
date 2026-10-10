using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Twin400.Flutes
{
	/// <summary>
	/// Form where sub group of twin400 flutes pages inherith from.
	/// </summary>
	public class Frm_twin400_flutesSubgroup : SyncroC.Forms.Pages.Twin400.Frm_Twin400Group
	{
		private System.ComponentModel.IContainer components = null;

		public Frm_twin400_flutesSubgroup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (Historian.CurrentPageTwin400_Flutes) for the Twin400_Flutes subgroup is set here.
		/// </summary>
		public override void _Show()
		{		
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageTwin400_Flutes = this.getRequestCommand();
			base._Show();
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
    //RFAZZI_FI08/07/2008_1.Moved Twin400 pages 
		//protected override void F8()
		//protected override void F7()
		protected override void F8()
		{
		}

		protected override void F11()
		{
			//RFAZZI PROVA
			//if (!DBManager.getDBManager().lineConfig.SlitterScorerConfig.CommunicatesWithSyncro)
			//  askForPage(typeof(Frm_twin400_flutes));
			if (!DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.CommunicatesWithSyncro)
				askForPage(typeof(Frm_twin400_flutes));
		}

		protected override void F10()
		{
			//RFAZZI PROVA
			//if (!DBManager.getDBManager().lineConfig.SlitterScorerConfig.CommunicatesWithSyncro)
			//  askForPage(typeof(Frm_twin400_quality));
			if (!DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.CommunicatesWithSyncro)
				askForPage(typeof(Frm_twin400_quality));
		}

    public override void refreshData()
    {
			//RFAZZI PROVA
			//this.tab_TabA.Visible = this.tab_TabB.Visible =
			//  (!DBManager.getDBManager().lineConfig.SlitterScorerConfig.CommunicatesWithSyncro);
			this.tab_TabA.Visible = this.tab_TabB.Visible =
				(!DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.CommunicatesWithSyncro);
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_flutesSubgroup));
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "TWIN 400 QUALITIES";
      // 
      // tab_Tab8
      // 
      this.tab_Tab8.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab8.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // tab_TabA
      // 
      this.tab_TabA.Text = "Qualities";
      this.tab_TabA.Visible = true;
      // 
      // tab_TabB
      // 
      this.tab_TabB.Text = "Flutes";
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
      // Frm_twin400_flutesSubgroup
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_twin400_flutesSubgroup";
      this.Text = "Frm_twin400_flutesSubgroup";
      this.Activated += new System.EventHandler(this.Frm_twin400_flutesSubgroup_Activated);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    private void Frm_twin400_flutesSubgroup_Activated(object sender, EventArgs e)
    {
      this.refreshData();
    }
	}
}

