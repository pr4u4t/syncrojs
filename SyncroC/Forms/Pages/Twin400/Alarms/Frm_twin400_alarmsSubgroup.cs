using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SyncroC.Forms.Pages.Twin400.Alarms
{
	/// <summary>
	/// Form where sub group of twin400 alarms pages inherith from.
	/// </summary>
	public class Frm_twin400_alarmsSubgroup : SyncroC.Forms.Pages.Twin400.Frm_Twin400Group
	{
		private System.ComponentModel.IContainer components = null;

		public Frm_twin400_alarmsSubgroup()
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

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page (Historian.CurrentPageTwin400_Alarms) for the Twin400_Alarms subgroup is set here.
		/// </summary>
		public override void _Show()
		{		
			PageManager pm = PageManager.getPageManager();
			pm.pagesHistorian.CurrentPageTwin400_Alarms = this.getRequestCommand();
			base._Show();
		}

		protected override void F1()
		{
		}

		protected override void F10()
		{
			askForPage(typeof(Frm_twin400_alarm1));
		}
		protected override void F11()
		{
			askForPage(typeof(Frm_twin400_alarm4));
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_alarmsSubgroup));
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
			this.SuspendLayout();
			// 
			// lbl_Title_YN
			// 
			this.lbl_Title_YN.Text = "TWIN 400 ALARMS";
			// 
			// tab_Tab1
			// 
			this.tab_Tab1.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.tab_Tab1.TabSelection = SyncroC.Controls.TabSelectionType.SelectedAlarm;
			// 
			// tab_TabA
			// 
			this.tab_TabA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(2)))), ((int)(((byte)(28)))));
			this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.UnselectedAlarm;
			this.tab_TabA.Text = "Active";
			this.tab_TabA.Visible = true;
			// 
			// tab_TabD
			// 
			this.tab_TabD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(2)))), ((int)(((byte)(28)))));
			this.tab_TabD.TabSelection = SyncroC.Controls.TabSelectionType.UnselectedAlarm;
			// 
			// tab_TabC
			// 
			this.tab_TabC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(2)))), ((int)(((byte)(28)))));
			this.tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.UnselectedAlarm;
			// 
			// tab_TabB
			// 
			this.tab_TabB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(2)))), ((int)(((byte)(28)))));
			this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.UnselectedAlarm;
			this.tab_TabB.Text = "History";
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
			// Frm_twin400_alarmsSubgroup
			// 
			this.ClientSize = new System.Drawing.Size(1280, 738);
			this.Name = "Frm_twin400_alarmsSubgroup";
			this.Text = "Frm_twin400_alarmsSubgroup";
			((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
			this.ResumeLayout(false);

    }
		#endregion

	}
}

