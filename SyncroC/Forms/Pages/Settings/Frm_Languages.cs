using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.Utility;
using SyncroC.Controls;
using SyncroC.DataBase;

namespace SyncroC.Forms.Pages.Settings
{
	public class Frm_Languages : SyncroC.Forms.Pages.Settings.Frm_SettingsGroup
  {
		private SyncroC.Controls.LanguageAndAccessibilitySettings settingControl;
    private WebBrowser webBrowserManual;
    private System.ComponentModel.IContainer components = null;

		public Frm_Languages()
		{
			this.settingControl = new SyncroC.Controls.LanguageAndAccessibilitySettings();

			InitializeComponent();

			// 
			// settingControl
			//
      //agrassi_FI28_01_09_1
      //this.settingControl.Visible = false;
      this.settingControl.Visible = true;
      this.settingControl.BackColor = System.Drawing.Color.WhiteSmoke;
			this.settingControl.Location = new System.Drawing.Point(25, 193);
			this.settingControl.Name = "settingControl";
			this.settingControl.Size = new System.Drawing.Size(1171, 519);
			this.settingControl.TabIndex = 111;
			this.settingControl.TranslateText = false;
			this.Controls.Add(this.settingControl);
			this.Controls.SetChildIndex(this.settingControl, 0);

			Accessibility accessibility = Accessibility.getAccessibility();
			Utility.Settings settings = Utility.Settings.getSettings();
			this.settingControl.Config(ref accessibility, ref settings, ref DBManager.getDBManager().language, new Translate(PageManager.getPageManager().translateForms));
      //agrassi_FI28_01_09_1
      //this.loadManuals();
      if (!SyncroC.Utility.Settings.getSettings().configuration.getOnFosberNetwork())
        this.tab_TabA.Visible = false;
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
          //agrassi_FI09-09-08
          webBrowserManual.Url = null;
          webBrowserManual.Stop();
          webBrowserManual.Dispose();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Languages));
      this.webBrowserManual = new System.Windows.Forms.WebBrowser();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "Langauge settings";
      // 
      // tab_Tab1
      // 
      this.tab_Tab1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab1.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      this.tab_Tab1.Tag = "";
      // 
      // tab_TabA
      // 
      this.tab_TabA.Text = "Manuals";
      this.tab_TabA.Visible = true;
      // 
      // tab_TabB
      // 
      this.tab_TabB.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      this.tab_TabB.Text = "Settings";
      this.tab_TabB.Visible = true;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Set accessibility";
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Text = "Save";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Text = "Reset accessibility";
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
      // webBrowserManual
      // 
      this.webBrowserManual.Location = new System.Drawing.Point(25, 189);
      this.webBrowserManual.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowserManual.Name = "webBrowserManual";
      this.webBrowserManual.ScriptErrorsSuppressed = true;
      this.webBrowserManual.Size = new System.Drawing.Size(1181, 537);
      this.webBrowserManual.TabIndex = 111;
      this.webBrowserManual.Visible = false;
      this.webBrowserManual.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowserManual_Navigating);
      this.webBrowserManual.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowserManual_NewWindow);
      // 
      // Frm_Languages
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.webBrowserManual);
      this.Name = "Frm_Languages";
      this.Tag = "";
      this.Text = "Frm_Languages";
      this.Controls.SetChildIndex(this.webBrowserManual, 0);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
      this.Controls.SetChildIndex(this.lbl_Time, 0);
      this.Controls.SetChildIndex(this.lbl_Status, 0);
      this.Controls.SetChildIndex(this.tab_Tab1, 0);
      this.Controls.SetChildIndex(this.tab_Tab2, 0);
      this.Controls.SetChildIndex(this.tab_Tab3, 0);
      this.Controls.SetChildIndex(this.tab_Tab4, 0);
      this.Controls.SetChildIndex(this.tab_Tab5, 0);
      this.Controls.SetChildIndex(this.tab_Tab6, 0);
      this.Controls.SetChildIndex(this.tab_Tab7, 0);
      this.Controls.SetChildIndex(this.tab_Tab8, 0);
      this.Controls.SetChildIndex(this.tab_TabA, 0);
      this.Controls.SetChildIndex(this.tab_TabD, 0);
      this.Controls.SetChildIndex(this.tab_TabC, 0);
      this.Controls.SetChildIndex(this.tab_TabB, 0);
      this.Controls.SetChildIndex(this.pct_Home_NY, 0);
      this.Controls.SetChildIndex(this.pct_Back_NY, 0);
      this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

		public override void refreshData()
		{
      this.settingControl.RefreshData();
      //agrassi_FI28_01_09_1
      //this.loadManuals();
    }

		protected override void F1()
		{
		}

		/// <summary>
		/// Save the data currently edited into the page
		/// </summary>
		protected override void CtrlF2()
		{
      this.settingControl.Save();
      PageManager.getPageManager()._SetMessage("§SYCC_07");
		}

		protected override void CtrlF4()
		{
      this.settingControl.SetAccessibility();
    }
		protected override void Return()
		{
			this.settingControl.SetAccessibility();
		}

    protected override void CtrlF5()
		{
      this.settingControl.ResetAccessibility();
    }

    protected override void F10()
    {
      //PAGINA MANUALI
      //agrassi_FI28_01_09_1
      this.loadManuals();
      this.settingControl.Visible = false;
      this.webBrowserManual.Visible = true;
      this.tab_TabA.TabSelection = TabSelectionType.Selected;
      this.tab_TabB.TabSelection = TabSelectionType.Unselected;
      this.tab_TabA.Cursor = Cursors.Arrow;
      this.tab_TabB.Cursor = Cursors.Hand;
    }

    protected override void F11()
    {
      //PAGINA SETTAGGI
      this.settingControl.Visible = true;
      this.webBrowserManual.Visible = false;
      this.tab_TabB.TabSelection = TabSelectionType.Selected;
      this.tab_TabA.TabSelection = TabSelectionType.Unselected;
      this.tab_TabB.Cursor = Cursors.Arrow;
      this.tab_TabA.Cursor = Cursors.Hand;
    }

    private void loadManuals()
    {
			//RFAZZI_FI28/10/2008_1Access to the Syncro Manuals document removing absoluted file name.

			//string manFileName = "Manuals\\MANSYC06C00ITA.pdf";
			//string manFileNameENG = "Manuals\\MANSYC06C00ITA.pdf";

			//switch (Utility.Settings.getSettings().languages.FirstLanguage)
			//{
			//  case "English":
			//    manFileName = "Manuals\\MANSYC06C00ING.pdf";
			//    break;
			//  case "Italian":
			//    manFileName = "Manuals\\MANSYC06C00ITA.pdf";
			//    break;
			//  case "Chinese":
			//    manFileName = "Manuals\\MANSYC06C00CIN.pdf";
			//    break;
			//  case "Polish":
			//    manFileName = "Manuals\\MANSYC06C00POL.pdf";
			//    break;
			//  case "Portuguese":
			//    manFileName = "Manuals\\MANSYC06C00POR.pdf";
			//    break;
			//  case "Spanish":
			//    manFileName = "Manuals\\MANSYC06C00SPA.pdf";
			//    break;
			//  case "Swedish":
			//    manFileName = "Manuals\\MANSYC06C00SVE.pdf";
			//    break;
			//}

			//string fullPathfile = "";
			//if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
			//  fullPathfile = SyncroC.Utility.Settings.getSettings().paths.ServerSharedOnServer;
			//else
			//  fullPathfile = SyncroC.Utility.Settings.getSettings().paths.ServerSharedFromNetwork;

			//if (System.IO.File.Exists(fullPathfile + manFileName))
			//{
			//  //agrassi_FI09-09-08
			//  webBrowserManual.Navigate("file:"+fullPathfile + manFileName);
			//}
			//else
			//{
			//  if (System.IO.File.Exists(fullPathfile + manFileNameENG))
			//    //agrassi_FI09-09-08
			//    webBrowserManual.Navigate("file:" + fullPathfile + manFileNameENG);
			//}

      //RFAZZI_FI06/10/2010_1. Aggiunta gestione per nuovo nome del manuale di Syncro a partire dalla versione 3.0.0
      //string manFileName = "MANSYC*ITA.pdf";
      //string manFileNameENG = "MANSYC*ING.pdf";

      //switch (Utility.Settings.getSettings().languages.FirstLanguage)
      //{
      //  case "English":
      //    manFileName = "MANSYC*ING.pdf";
      //    break;
      //  case "Italian":
      //    manFileName = "MANSYC*ITA.pdf";
      //    break;
      //  case "Chinese":
      //    manFileName = "MANSYC*CIN.pdf";
      //    break;
      //  case "Polish":
      //    manFileName = "MANSYC*POL.pdf";
      //    break;
      //  case "Portuguese":
      //    manFileName = "MANSYC*POR.pdf";
      //    break;
      //  case "Spanish":
      //    manFileName = "MANSYC*SPA.pdf";
      //    break;
      //  case "Swedish":
      //    manFileName = "MANSYC*SVE.pdf";
      //    break;
      //  //RFAZZI_FI12/01/2009_1.Added Missed Language Loading the Syncro Manual
      //  case "German":
      //    manFileName = "MANSYC*TED.pdf";
      //    break;

      //  // To be added when the manual is ready
      //  //case "Dutch":
      //  //  manFileName = "MANSYC*OLA.pdf";
      //  //  break;
      //  //case "Czech":
      //  //  manFileName = "MANSYC*CEC.pdf";
      //  //  break;

      //  //PMASSARO_FI10/12/09 - Aggiunta copia del manuale di Syncro in lingua francese
      //  case "French":
      //    manFileName = "MANSYC*FRA.pdf";
      //    break;

      //  //case "Finnish":
      //  //  manFileName = "MANSYC*FIN.pdf";
      //  //  break;
      //  //case "Turkish":
      //  //  manFileName = "MANSYC*TUR.pdf";
      //  //  break;
      //  //case "Russian":
      //  //  manFileName = "MANSYC*RUS.pdf";
      //  //  break;
      //}
      string preName = "";

      if (PageManager.getPageManager().getSyncroVersion().Contains("3."))
        preName = "MIA";
      else
        preName = "MAN";


			string manFileName = preName + "SYC*ITA.pdf";
			string manFileNameENG = preName + "SYC*ING.pdf";

			switch (Utility.Settings.getSettings().languages.FirstLanguage)
			{
				case "English":
					manFileName = preName + "SYC*ING.pdf";
					break;
				case "Italian":
					manFileName = preName + "SYC*ITA.pdf";
					break;
				case "Chinese":
					manFileName = preName + "SYC*CIN.pdf";
					break;
				case "Polish":
					manFileName = preName + "SYC*POL.pdf";
					break;
				case "Portuguese":
					manFileName = preName + "SYC*POR.pdf";
					break;
				case "Spanish":
					manFileName = preName + "SYC*SPA.pdf";
					break;
				case "Swedish":
          manFileName = preName + "SYC*SVE.pdf";
					break;
				//RFAZZI_FI12/01/2009_1.Added Missed Language Loading the Syncro Manual
				case "German":
          manFileName = preName + "SYC*TED.pdf";
					break;

				// To be added when the manual is ready
				//case "Dutch":
				//  manFileName = "MANSYC*OLA.pdf";
				//  break;
				//case "Czech":
				//  manFileName = "MANSYC*CEC.pdf";
				//  break;

        //PMASSARO_FI10/12/09 - Aggiunta copia del manuale di Syncro in lingua francese
        case "French":
          manFileName = preName + "SYC*FRA.pdf";
				  break;
				
        //case "Finnish":
				//  manFileName = "MANSYC*FIN.pdf";
				//  break;
				//case "Turkish":
				//  manFileName = "MANSYC*TUR.pdf";
				//  break;
				//case "Russian":
				//  manFileName = "MANSYC*RUS.pdf";
				//  break;
        //MLAMIONI_FI21/01/11 - Se non esiste MANSYC relativo alla Lingua selezionato si visualizza ver.ING
        default:
          manFileName = "MANSYC*ING.pdf";
          break;
      }
			string fullPathfile = "";
			if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
				fullPathfile = SyncroC.Utility.Settings.getSettings().paths.ServerSharedOnServer;
			else
				fullPathfile = SyncroC.Utility.Settings.getSettings().paths.ServerSharedFromNetwork;

			string[] dirs = System.IO.Directory.GetFiles(fullPathfile + "Manuals", manFileName);

			if (dirs.Length >0)
      {
        //agrassi_FI09-09-08
        //agrassi_FI15-04-2010_1
        //webBrowserManual.Navigate("file:" + dirs[0]);
        webBrowserManual.Navigate("file:" + dirs[dirs.Length-1]);
      }
      else
      {
				dirs = System.IO.Directory.GetFiles(fullPathfile + "Manuals", manFileNameENG);
				if (dirs.Length >0)
          //agrassi_FI09-09-08
          //agrassi_FI15-04-2010_1
          //webBrowserManual.Navigate("file:" + dirs[0]);
          webBrowserManual.Navigate("file:" + dirs[dirs.Length - 1]);
      }
      //agrassi_FI15-04-2010_1
      for (int i = 0; i < dirs.Length - 1; i++)
      {
        System.IO.File.Delete(dirs[i]);
        Logger.LogError(this, dirs[i]+" deleted because a newer version exists");
      }
    }

    private void webBrowserManual_NewWindow(object sender, CancelEventArgs e)
    {
      e.Cancel = true;
    }

    private void webBrowserManual_Navigating(object sender, WebBrowserNavigatingEventArgs e)
    {
      if (e.Url.AbsoluteUri.Contains("mailto"))
      {
        e.Cancel = true;
        return;
      }

      //ABELLO_FI09/10/2008_1 I manuali sui client remoti non si vedevano per questo controllo
      //if (e.Url.Host != "")
      //{
      //  e.Cancel = true;
      //}
    }
  }
}

