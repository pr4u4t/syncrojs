using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.Forms.Pages.Stacker.Alarms;
using SyncroC.Forms.Pages.Stacker.Settings;

namespace SyncroC.Forms.Pages.Stacker
{
	public class Frm_StackerGroup : SyncroC.Forms.Pages.Frm_Page
	{
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// The index of the Stacker represented on this page.
		/// </summary>
		protected int index;
		protected LevelName level;

		public Frm_StackerGroup()
		{
			InitializeComponent();
		}

		public void _setFrmMachineName(string machineName) 
		{
			string header = string.Empty;
			if (machineName != null && !machineName.Equals(string.Empty))
				header = machineName + ": " + this.index.ToString();

			this.lbl_Title_YN.Text = header;

            //ZETA 01
            DBManager dbm = DBManager.getDBManager();
            level = (LevelName)index;
            if ((dbm.lineConfig.KnifeConfig.LevelCount == 2) && (index == 2))
                level = LevelName.Upper;
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_StackerGroup));
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(153)))), ((int)(((byte)(199)))));
      // 
      // lbl_Time
      // 
      this.lbl_Time.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      // 
      // lbl_Date
      // 
      this.lbl_Date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      // 
      // lbl_Status
      // 
      this.lbl_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      // 
      // tab_Tab1
      // 
      this.tab_Tab1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(2)))), ((int)(((byte)(28)))));
      this.tab_Tab1.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      this.tab_Tab1.TabSelection = SyncroC.Controls.TabSelectionType.UnselectedAlarm;
      this.tab_Tab1.Text = "Alarms";
      this.tab_Tab1.Visible = true;
      // 
      // tab_Tab2
      // 
      this.tab_Tab2.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      this.tab_Tab2.Text = "Settings";
      this.tab_Tab2.Visible = true;
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      this.tab_Tab3.Text = "Status";
      this.tab_Tab3.Visible = true;
      // 
      // tab_Tab4
      // 
      this.tab_Tab4.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      this.tab_Tab4.Text = "Operator";
      this.tab_Tab4.Visible = true;
      // 
      // tab_Tab5
      // 
      this.tab_Tab5.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      // 
      // tab_Tab6
      // 
      this.tab_Tab6.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      // 
      // tab_Tab7
      // 
      this.tab_Tab7.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      // 
      // tab_Tab8
      // 
      this.tab_Tab8.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      // 
      // tab_TabA
      // 
      this.tab_TabA.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      // 
      // tab_TabD
      // 
      this.tab_TabD.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      // 
      // tab_TabC
      // 
      this.tab_TabC.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      // 
      // tab_TabB
      // 
      this.tab_TabB.SelectedBackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(139)))));
      // 
      // pct_Home_NY
      // 
      this.pct_Home_NY.Image = global::SyncroC.Properties.Resources.express_terminal_04;
      // 
      // pct_Back_NY
      // 
      this.pct_Back_NY.Image = global::SyncroC.Properties.Resources.express_terminal_02;
      // 
      // pct_Forward_NY
      // 
      this.pct_Forward_NY.Image = global::SyncroC.Properties.Resources.express_terminal_03;
      // 
      // lbl_CtrlF3
      // 
      this.lbl_CtrlF3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(153)))), ((int)(((byte)(199)))));
      // 
      // lbl_CtrlF5
      // 
      this.lbl_CtrlF5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(153)))), ((int)(((byte)(199)))));
      // 
      // lbl_CtrlF2
      // 
      this.lbl_CtrlF2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(153)))), ((int)(((byte)(199)))));
      // 
      // lbl_CtrlF1
      // 
      this.lbl_CtrlF1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(153)))), ((int)(((byte)(199)))));
      // 
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(153)))), ((int)(((byte)(199)))));
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(153)))), ((int)(((byte)(199)))));
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(153)))), ((int)(((byte)(199)))));
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(153)))), ((int)(((byte)(199)))));
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(153)))), ((int)(((byte)(199)))));
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(153)))), ((int)(((byte)(199)))));
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
      // Frm_StackerGroup
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Name = "Frm_StackerGroup";
      this.Text = "Frm_StackerGroup";
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// To be called by the server and invoked by the client throw the reflection to show a new page.
		/// Last visited page for the Stacker.
		/// </summary>
		public override void _Show()
		{
			PageManager pm = PageManager.getPageManager();
			switch (index)
			{
				case 1:
				default:
					pm.pagesHistorian.CurrentPageStacker1 = this.getRequestCommand(index);
					break;
				case 2:
					pm.pagesHistorian.CurrentPageStacker2 = this.getRequestCommand(index);
					break;
				case 3:
					pm.pagesHistorian.CurrentPageStacker3 = this.getRequestCommand(index);
					break;
			}
			pm.pagesHistorian.PageChanged(this, index);
			pm.UpdateCache(this, index);
			base._Show();
		}

		protected override void setAccessibilityIcons(AccessibilityLevel accessibilityLevel)
		{
			switch (accessibilityLevel)
			{
        //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
        //ABELLO_FI14/08/2010_1. Riviste le password
        case AccessibilityLevel.NoPassword:
        case AccessibilityLevel.Low:
        case AccessibilityLevel.Medium:
					tab_Tab2.Image = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
					this.FKeyEnable[(int)FKEYS.F2] = false;
					break;
				case AccessibilityLevel.High:
					tab_Tab2.Image = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
					this.FKeyEnable[(int)FKEYS.F2] = true;
					break;
			}
		}


		/// <summary>
		/// Stacker Group must do nothing if AltF6 is pressed when index is 1
		/// </summary>
		protected override void AltF6()
		{
			if (index != 1)
				base.AltF6();
		}

		/// <summary>
		/// Stacker Group must do nothing if AltF7 is pressed when index is 2
		/// </summary>
		protected override void AltF7()
		{
			if (index != 2)
				base.AltF7();
		}

		protected override void F1()
		{
			CommunicationManager cm = CommunicationManager.GetCommunicationManager();
			PageManager pm = PageManager.getPageManager();
			switch (index)
			{
				case 1:
				default:
					cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageStackerAlarm1);
					break;
				case 2:
					cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageStackerAlarm2);
					break;
				case 3:
					cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageStackerAlarm3);
					break;
			}
		}
		protected override void F2()
		{
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      ///if (Accessibility.getAccessibility().isLevelAtLeastMedium())
      if (Accessibility.getAccessibility().isLevelHigh())
      {
				CommunicationManager cm = CommunicationManager.GetCommunicationManager();
				PageManager pm = PageManager.getPageManager();
				switch (index)
				{
					case 1:
					default:
						cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageStackerSettings1);
						break;
					case 2:
						cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageStackerSettings2);
						break;
					case 3:
						cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageStackerSettings3);
						break;
				}
			}
		}
		protected override void F3()
		{
      CommunicationManager cm = CommunicationManager.GetCommunicationManager();
      PageManager pm = PageManager.getPageManager();
      switch (index)
      {
        case 1:
        default:
          cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageStackerStatus1);
          break;
        case 2:
          cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageStackerStatus2);
          break;
        case 3:
          cm.SendTcp(cm.SYNCROS_PAGE_MANAGER, pm.pagesHistorian.CurrentPageStackerStatus3);
          break;
      }
		}

		protected override void F4()
		{
			askForPage(typeof(Frm_StackerConsole), index);
		}
		
	}
}

