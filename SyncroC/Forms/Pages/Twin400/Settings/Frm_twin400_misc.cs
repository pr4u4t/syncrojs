using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.Utility;
using SyncroC.Controls;
using SyncroC.DataBase;
//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
using SyncroC.DataBase.LineConfig;

//agrassi_FI05-03-09_4
namespace SyncroC.Forms.Pages.Twin400.Settings
{
	public class Frm_twin400_misc : SyncroC.Forms.Pages.Twin400.Settings.Frm_twin400_settingsSubgroup
  {
    private LanguageAndAccessibilitySettings settingControl;
    private TabControl tab_twin400_settings;
    private TabPage tab_language;
    private TabPage tab_manuals;
    private WebBrowser webBrowserManual;
    private System.ComponentModel.IContainer components = null;
		private DBManager dbm;
		private SyncroC.DataBase.LineConfig.SlitterScorer ss;

		public Frm_twin400_misc()
		{
			this.settingControl = new SyncroC.Controls.LanguageAndAccessibilitySettings();

			InitializeComponent();

			//RFAZZI_FI16/09/2008_1.Added pages required for old Fosber SS
			dbm = DBManager.getDBManager();
			this.ss = dbm.lineConfig.SlitterScorerConfig;

			// 
			// settingControl
			// 
      this.settingControl.Visible = true;
      this.settingControl.BackColor = System.Drawing.Color.WhiteSmoke;
			this.settingControl.Location = new System.Drawing.Point(11, 11);
			this.settingControl.Name = "settingControl";
			this.settingControl.Size = new System.Drawing.Size(1171, 516);
			this.settingControl.TabIndex = 110;
			this.settingControl.TranslateText = false;
			this.tab_language.Controls.Add(this.settingControl);

      Accessibility accessibility=Accessibility.getAccessibility();
      Utility.Settings settings=Utility.Settings.getSettings();
      this.settingControl.Config(ref accessibility, ref settings, ref DBManager.getDBManager().language, new Translate(PageManager.getPageManager().translateForms));
      if (!SyncroC.Utility.Settings.getSettings().configuration.getOnFosberNetwork())
        this.tab_manuals.Visible = false;
      else
        this.loadManuals();
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
          webBrowserManual.Url = null;
          webBrowserManual.Stop();
          webBrowserManual.Dispose();
        }
			}
			base.Dispose( disposing );
		}

    protected override void F10()
    {
    }

		public override void refreshData()
		{
      this.settingControl.RefreshData();
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_twin400_misc));
      this.tab_twin400_settings = new System.Windows.Forms.TabControl();
      this.tab_language = new System.Windows.Forms.TabPage();
      this.tab_manuals = new System.Windows.Forms.TabPage();
      this.webBrowserManual = new System.Windows.Forms.WebBrowser();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.tab_twin400_settings.SuspendLayout();
      this.tab_manuals.SuspendLayout();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // lbl_Time
      // 
      this.lbl_Time.Margin = new System.Windows.Forms.Padding(1, 3, 3, 1);
      // 
      // lbl_Date
      // 
      this.lbl_Date.Margin = new System.Windows.Forms.Padding(1, 1, 3, 3);
      // 
      // lbl_Status
      // 
      this.lbl_Status.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // pct_Home_NY
      // 
      this.pct_Home_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // pct_Back_NY
      // 
      this.pct_Back_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // pct_Forward_NY
      // 
      this.pct_Forward_NY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF3
      // 
      this.lbl_CtrlF3.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF5
      // 
      this.lbl_CtrlF5.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
      // 
      // lbl_CtrlF2
      // 
      this.lbl_CtrlF2.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF1
      // 
      this.lbl_CtrlF1.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
      // 
      // lbl_CtrlF4
      // 
      this.lbl_CtrlF4.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF4_YY.Text = "Set accessibility";
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF2_YY.Text = "Save";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
      this.lbl_CtrlF5_YY.Text = "Reset accessibility";
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
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
      // tab_twin400_settings
      // 
      this.tab_twin400_settings.Controls.Add(this.tab_language);
      this.tab_twin400_settings.Controls.Add(this.tab_manuals);
      this.tab_twin400_settings.Location = new System.Drawing.Point(30, 168);
      this.tab_twin400_settings.Name = "tab_twin400_settings";
      this.tab_twin400_settings.SelectedIndex = 0;
      this.tab_twin400_settings.Size = new System.Drawing.Size(1171, 569);
      this.tab_twin400_settings.TabIndex = 112;
      // 
      // tab_language
      // 
      this.tab_language.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tab_language.Location = new System.Drawing.Point(4, 22);
      this.tab_language.Name = "tab_language";
      this.tab_language.Padding = new System.Windows.Forms.Padding(3);
      this.tab_language.Size = new System.Drawing.Size(1163, 543);
      this.tab_language.TabIndex = 0;
      this.tab_language.Text = "Language";
      // 
      // tab_manuals
      // 
      this.tab_manuals.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tab_manuals.Controls.Add(this.webBrowserManual);
      this.tab_manuals.Location = new System.Drawing.Point(4, 22);
      this.tab_manuals.Name = "tab_manuals";
      this.tab_manuals.Padding = new System.Windows.Forms.Padding(3);
      this.tab_manuals.Size = new System.Drawing.Size(1163, 543);
      this.tab_manuals.TabIndex = 1;
      this.tab_manuals.Text = "Manuals";
      // 
      // webBrowserManual
      // 
      this.webBrowserManual.Location = new System.Drawing.Point(15, 3);
      this.webBrowserManual.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowserManual.Name = "webBrowserManual";
      this.webBrowserManual.ScriptErrorsSuppressed = true;
      this.webBrowserManual.Size = new System.Drawing.Size(1078, 537);
      this.webBrowserManual.TabIndex = 115;
      // 
      // Frm_twin400_misc
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tab_twin400_settings);
      this.Name = "Frm_twin400_misc";
      this.Tag = "";
      this.Text = "Frm_twin400_misc";
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.tab_twin400_settings, 0);
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
      this.tab_twin400_settings.ResumeLayout(false);
      this.tab_manuals.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private void loadManuals()
    {

      //agrassi_FI04-06-2010_1
      //string manFileName = "MANSYC*ITA.pdf";
      //string manFileNameENG = "MANSYC*ING.pdf";
      //if (Utility.Settings.getSettings().configuration.IsPmac400())
      //{

			string preName = "";

			if ( ss.Twin400.PLCInterfaceVersion >= SyncroC.DataBase.LineConfig.Twin400.MachineVersion.Ver5 )
				preName = "MIA";
			else
				preName = "MAN";

      //PMASSARO_FI26/04/11 - Durante il caricamento dei manuali del Twin400, non veniva fatta distinzione tra Top e Bottom Slitting
      /*
			string manFileName = preName + "TW4*ITA.pdf";
			string manFileNameENG = preName + "TW4*ING.pdf";

			switch (Utility.Settings.getSettings().languages.FirstLanguage)
			{
				case "English":
					manFileName = preName + "TW4*ING.pdf";
					break;
				case "Italian":
					manFileName = preName + "TW4*ITA.pdf";
					break;
				case "Chinese":
					manFileName = preName + "TW4*CIN.pdf";
					break;
				case "Polish":
					manFileName = preName + "TW4*POL.pdf";
					break;
				//case "Portuguese":
				//  manFileName = preName + "TW4*POR.pdf";
				//  break;
				case "Spanish":
					manFileName = preName + "TW4*SPA.pdf";
					break;
				//case "Swedish":
				//  manFileName = preName + "TW4*SVE.pdf";
				//  break;
				//RFAZZI_FI12/01/2009_1.Added Missed Language Loading the Syncro Manual
				case "German":
					manFileName = preName + "TW4*TED.pdf";
					break;
				// To be added when the manual is ready
				//case "Dutch":
				//  manFileName = preName + "TW4*OLA.pdf";
				//  break;
				//case "Czech":
				//  manFileName = preName + "TW4*CEC.pdf";
				//  break;
				case "French":
					manFileName = preName + "TW4*FRA.pdf";
					break;
				//case "Finnish":
				//  manFileName = preName + "TW4*FIN.pdf";
				//  break;
				//case "Turkish":
				//  manFileName = preName + "TW4*TUR.pdf";
				//  break;
				case "Russian":
					manFileName = preName + "TW4*RUS.pdf";
					break;
				case "Norwegian":
					manFileName = preName + "TW4*NOR.pdf";
					break;
			}
      */
      string TW4_Top_Bot_Name = "TW4*"; //inizializzazione per mantenere la compatibilità con i primi manuali in cui non c'era distinzione TW4T e TW4B
      if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsTwin400v1())
      {
        if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.Twin400.CutFromBottom == true)
          TW4_Top_Bot_Name = "TW4B*";
        else
          TW4_Top_Bot_Name = "TW4T*";
      }

      string manFileName = preName + TW4_Top_Bot_Name + "ITA.pdf";
      string manFileNameENG = preName + TW4_Top_Bot_Name + "ING.pdf";

      switch (Utility.Settings.getSettings().languages.FirstLanguage)
      {
        case "English":
          manFileName = preName + TW4_Top_Bot_Name + "ING.pdf";
          break;
        case "Italian":
          manFileName = preName + TW4_Top_Bot_Name + "ITA.pdf";
          break;
        case "Chinese":
          manFileName = preName + TW4_Top_Bot_Name + "CIN.pdf";
          break;
        case "Polish":
          manFileName = preName + TW4_Top_Bot_Name + "POL.pdf";
          break;
        case "Spanish":
          manFileName = preName + TW4_Top_Bot_Name + "SPA.pdf";
          break;
        case "German":
          manFileName = preName + TW4_Top_Bot_Name + "TED.pdf";
          break;
        case "French":
          manFileName = preName + TW4_Top_Bot_Name + "FRA.pdf";
          break;
        case "Russian":
          manFileName = preName + TW4_Top_Bot_Name + "RUS.pdf";
          break;
        case "Norwegian":
          manFileName = preName + TW4_Top_Bot_Name + "NOR.pdf";
          break;
      }

      //}

			string fullPathfile = "";
			if (SyncroC.Utility.Settings.getSettings().configuration.getOnServer())
				fullPathfile = SyncroC.Utility.Settings.getSettings().paths.ServerSharedOnServer;
			else
				fullPathfile = SyncroC.Utility.Settings.getSettings().paths.ServerSharedFromNetwork;

			string[] dirs = System.IO.Directory.GetFiles(fullPathfile + "Manuals", manFileName);

			if (dirs.Length >0)
      {
        //agrassi_FI09-09-08
        //agrassi_FI04-06-2010_1
        //webBrowserManual.Navigate("file:" + dirs[0]);
        webBrowserManual.Navigate("file:" + dirs[dirs.Length - 1]);
      }
      else
      {
				dirs = System.IO.Directory.GetFiles(fullPathfile + "Manuals", manFileNameENG);
				if (dirs.Length >0)
          //agrassi_FI09-09-08
          //agrassi_FI04-06-2010_1
          //webBrowserManual.Navigate("file:" + dirs[0]);
          webBrowserManual.Navigate("file:" + dirs[dirs.Length - 1]);
      }
      //agrassi_FI04-06-2010_1
      for (int i = 0; i < dirs.Length - 1; i++)
      {
        System.IO.File.Delete(dirs[i]);
        Logger.LogError(this, dirs[i] + " deleted because a newer version exists");
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
    }

	}
}

