using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Production.OrdersIns
{
	public class Frm_OrdersInsSubGroup : SyncroC.Forms.Pages.Production.Frm_ProductionGroup
	{
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Index of the order currently showed on the screen.
		/// (static because it is used also in other groups such as orderList, StatusBar
		/// </summary>
		public static int indexOrderToShowDRY = 0;

		public static int positionPastOrderToShowDRY = 1;
    //ABELLO_FI25/08/2010_1 se uso Postgres il doppio click sulla lista degli eseguiti andava a casaccio
    public static string PastOrderfiltro = "";

		public static int indexOrderToShowWET = 0;

		public Frm_OrdersInsSubGroup()
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

		public override void ConfigPage(SyncroC.Utility.Settings.Configuration config)
		{
			base.ConfigPage(config);
			this.FKeyEnable[(int)FKEYS.F10] = this.tab_TabA.Visible = config.DryEndControl;
      //agrassi_FI24-02-2010_3
			//this.FKeyEnable[(int)FKEYS.F11] = this.tab_TabB.Visible = config.DryEndControl;
			this.FKeyEnable[(int)FKEYS.F12] = this.tab_TabC.Visible = config.WetEndControl;
		}

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (Historian.CurrentPageProduction_OrdersIns) for the Production_OrdersIns subgroup is set here.
		/// </summary>
		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageProduction_OrdersIns = this.getRequestCommand();
			base._Show();
		}

		protected override void F8()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_QuickInsDryOrder));
		}
		protected override void F11()
		{
      //agrassi_FI24-02-2010_3
      askForPage(typeof(Frm_DryPastOrders));
		}
		protected override void F12()
		{
			askForPage(typeof(Frm_InsWetOrder));
		}

		protected override void F13()
		{
      //agrassi_FI24-02-2010_3
      //askForPage(typeof(Frm_DryPastOrders));
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_OrdersInsSubGroup));
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_Tab8
      // 
      this.tab_Tab8.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab8.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // tab_TabA
      // 
      this.tab_TabA.Text = "Quick Dry";
      this.tab_TabA.Visible = true;
      // 
      // tab_TabD
      // 
      this.tab_TabD.Text = "Full Dry";
      // 
      // tab_TabC
      // 
      this.tab_TabC.Text = "Wet";
      this.tab_TabC.Visible = true;
      // 
      // tab_TabB
      // 
      this.tab_TabB.Text = "Past";
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
      // Frm_OrdersInsSubGroup
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_OrdersInsSubGroup";
      this.Text = "Frm_OrdersInsSubGroup";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion
	}
}

