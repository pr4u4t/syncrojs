using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;


namespace SyncroC.Forms.Pages.SlitterScorer.Settings
{
	public class Frm_SlitterScorerSettingsMachine : SyncroC.Forms.Pages.SlitterScorer.Settings.Frm_SlitterScorerSettingsSubgroup
	{
    private PropertyGrid WebCfgGrid;
    private PropertyGrid TwinCfgGrid;
		private System.ComponentModel.IContainer components = null;
    private string FRM_WEBDIRECTOR_MACHINESETTINGS = "webDirector_machineSettings";
    private string FRM_SLITTERSCORER_MACHINESETTINGS = "slitterScorer_machineSettings";
    private bool modify = false;
    private bool modTwin = false;
    private bool modWeb = false;
    private PropertyGrid TwinHeaderCfgGrid;
    private string[] modScoreTypes = new string[SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORE_TYPES_MACHINE];
    //agrassi_FI25-05-2011_1
    private string slitterScorerName;

		public Frm_SlitterScorerSettingsMachine()
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

		protected override void F10()
		{
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SlitterScorerSettingsMachine));
      this.WebCfgGrid = new System.Windows.Forms.PropertyGrid();
      this.TwinCfgGrid = new System.Windows.Forms.PropertyGrid();
      this.TwinHeaderCfgGrid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Text = "Modify";
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
      // WebCfgGrid
      // 
      this.WebCfgGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.WebCfgGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.WebCfgGrid.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.WebCfgGrid.Location = new System.Drawing.Point(691, 173);
      this.WebCfgGrid.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
      this.WebCfgGrid.Name = "WebCfgGrid";
      this.WebCfgGrid.Size = new System.Drawing.Size(510, 322);
      this.WebCfgGrid.TabIndex = 112;
      this.WebCfgGrid.ToolbarVisible = false;
      this.WebCfgGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.WebCfgGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.WebCfgGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // TwinCfgGrid
      // 
      this.TwinCfgGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.TwinCfgGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.TwinCfgGrid.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TwinCfgGrid.Location = new System.Drawing.Point(26, 499);
      this.TwinCfgGrid.Name = "TwinCfgGrid";
      this.TwinCfgGrid.Size = new System.Drawing.Size(645, 235);
      this.TwinCfgGrid.TabIndex = 111;
      this.TwinCfgGrid.ToolbarVisible = false;
      this.TwinCfgGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.TwinCfgGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.TwinCfgGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // TwinHeaderCfgGrid
      // 
      this.TwinHeaderCfgGrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.TwinHeaderCfgGrid.CommandsBackColor = System.Drawing.SystemColors.ControlLightLight;
      this.TwinHeaderCfgGrid.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TwinHeaderCfgGrid.Location = new System.Drawing.Point(26, 173);
      this.TwinHeaderCfgGrid.Name = "TwinHeaderCfgGrid";
      this.TwinHeaderCfgGrid.Size = new System.Drawing.Size(645, 324);
      this.TwinHeaderCfgGrid.TabIndex = 113;
      this.TwinHeaderCfgGrid.ToolbarVisible = false;
      this.TwinHeaderCfgGrid.ViewBackColor = System.Drawing.Color.WhiteSmoke;
      this.TwinHeaderCfgGrid.ViewForeColor = System.Drawing.Color.DimGray;
      this.TwinHeaderCfgGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CfgGrid_PropertyValueChanged);
      // 
      // Frm_SlitterScorerSettingsMachine
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.TwinHeaderCfgGrid);
      this.Controls.Add(this.WebCfgGrid);
      this.Controls.Add(this.TwinCfgGrid);
      this.Name = "Frm_SlitterScorerSettingsMachine";
      this.Load += new System.EventHandler(this.Frm_SlitterScorerSettingsMachine_Load);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
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
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.TwinCfgGrid, 0);
      this.Controls.SetChildIndex(this.WebCfgGrid, 0);
      this.Controls.SetChildIndex(this.TwinHeaderCfgGrid, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

		}
		#endregion

    private void Frm_SlitterScorerSettingsMachine_Load(object sender, EventArgs e)
    {
      for (int scr = 0; scr < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORE_TYPES_MACHINE; scr++)
        this.modScoreTypes[scr] = DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.ScoreTypes[scr].Scorer.ScoreId;
      this.refreshData();
      this.WebCfgGrid.ExpandAllGridItems();
    }

    protected override void CtrlF4()
    {
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        if (this.modify)
        {
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          if (this.modTwin)
          {
            //agrassi_FI25-05-2011_1
            //cm.SendTcp(cm.SYNCROS_RTC_MANAGER, this.FRM_SLITTERSCORER_MACHINESETTINGS + "." + Command.CONF_CONFIG + "("
            //+ ((SyncroC.DataBase.LineConfig.SlitterScorer)this.TwinCfgGrid.SelectedObject).SerializeProperties() + ")");
            string ssCfg = this.FRM_SLITTERSCORER_MACHINESETTINGS + "." + Command.CONF_CONFIG + "("
                + ((SyncroC.DataBase.LineConfig.SlitterScorerHeader)this.TwinHeaderCfgGrid.SelectedObject).SerializeProperties();
            if (((SyncroC.DataBase.LineConfig.SlitterScorerHeader)this.TwinHeaderCfgGrid.SelectedObject).Name == "Twin2")
              ssCfg += ((SyncroC.DataBase.LineConfig.Twin2)this.TwinCfgGrid.SelectedObject).SerializeProperties();
            else if (((SyncroC.DataBase.LineConfig.SlitterScorerHeader)this.TwinHeaderCfgGrid.SelectedObject).Name == "Compact2")
              ssCfg += ((SyncroC.DataBase.LineConfig.Compact2)this.TwinCfgGrid.SelectedObject).SerializeProperties();
            else if ((((SyncroC.DataBase.LineConfig.SlitterScorerHeader)this.TwinHeaderCfgGrid.SelectedObject).Name == "TwinE") || 
                   (((SyncroC.DataBase.LineConfig.SlitterScorerHeader)this.TwinHeaderCfgGrid.SelectedObject).Name == "Twin400"))
              ssCfg += ((SyncroC.DataBase.LineConfig.Twin400)this.TwinCfgGrid.SelectedObject).SerializeProperties();
            ssCfg += ")";
            cm.SendTcp(cm.SYNCROS_RTC_MANAGER, ssCfg);
          }
          if (this.modWeb)
            cm.SendTcp(cm.SYNCROS_RTC_MANAGER, this.FRM_WEBDIRECTOR_MACHINESETTINGS + "." + Command.CONF_CONFIG + "(" +
              ((WebDirector)this.WebCfgGrid.SelectedObject).SerializeProperties() + ")");
          this.refreshData();
          this.TwinCfgGrid.ViewForeColor = Color.DimGray;
          //agrassi_FI25-05-2011_1
          this.TwinHeaderCfgGrid.ViewForeColor = Color.DimGray;
          this.WebCfgGrid.ViewForeColor = Color.DimGray;
          this.modify = false;
          this.modScoreTypes.Initialize();
          lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
            lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
        }
      }
      else
      {
        PageManager.getPageManager()._SetMessage("§SYCC_11");
      }
    }

    protected override void CtrlF5()
    {
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        if ((!DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsTwin400()) ||
            (DBManager.getDBManager().lineConfig.WebDirectorConfig.SyncroInterface))
        {
          if (!DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsTwin400())
          {
            this.TwinCfgGrid.ViewForeColor = Color.Black;
            //agrassi_FI25-05-2011_1
            this.TwinHeaderCfgGrid.ViewForeColor = Color.Black;
          }
          if (DBManager.getDBManager().lineConfig.WebDirector)
            this.WebCfgGrid.ViewForeColor = Color.Black;
          this.modify = true;
          this.modTwin = false;
          this.modWeb = false;
          lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
        }
        else
        {
          PageManager.getPageManager()._SetMessage("§SYCC_150");
        }
      }
      else
      {
        PageManager.getPageManager()._SetMessage("§SYCC_11");
      }
    }

    public override void refreshData()
    {
      LineConfig cfg = DBManager.getDBManager().lineConfig;
      //agrassi_FI25-05-2011_1
      this.slitterScorerName = cfg.SlitterScorerConfig.SlitterScorerHeader.Name;
      //agrassi_FI25-05-2011_1
      //this.TwinCfgGrid.SelectedObject = (SyncroC.DataBase.LineConfig.SlitterScorer)cfg.SlitterScorerConfig.CreateCopy();
      if (cfg.SlitterScorerConfig.IsTwin2())
        this.TwinCfgGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Twin2)cfg.SlitterScorerConfig.Twin2.CreateCopy();
      else if (cfg.SlitterScorerConfig.IsCompact2())
        this.TwinCfgGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Compact2)cfg.SlitterScorerConfig.Compact2.CreateCopy();
      else if (cfg.SlitterScorerConfig.IsTwinE() || cfg.SlitterScorerConfig.IsTwin400())
        this.TwinCfgGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Twin400)cfg.SlitterScorerConfig.Twin400.CreateCopy();
      this.TwinHeaderCfgGrid.SelectedObject = (SyncroC.DataBase.LineConfig.SlitterScorerHeader)cfg.SlitterScorerConfig.SlitterScorerHeader.CreateCopy();
      this.WebCfgGrid.SelectedObject = (WebDirector)cfg.WebDirectorConfig.CreateCopy();
    }

    private void CfgGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
    {
      if (!this.modify)
        this.refreshData();
      else
      {
        LineConfig cfg = DBManager.getDBManager().lineConfig;
        //agrassi_FI25-05-2011_1
        //if (((PropertyGrid)s).Equals(this.TwinCfgGrid))
        if (((PropertyGrid)s).Equals(this.TwinHeaderCfgGrid))
        {
          //if (!cfg.SlitterScorerConfig.IsTwin400())
          //{
          this.modTwin = true;
          for (int scr = 0; scr < SyncroC.DataBase.LineConfig.SlitterScorerHeader.MAX_SCORE_TYPES_MACHINE; scr++)
          {
            //agrassi_FI25-05-2011_1
            //string newScoreId = (((SyncroC.DataBase.LineConfig.SlitterScorer)((PropertyGrid)s).SelectedObject).SlitterScorerHeader.ScoreTypes[scr].Scorer.ScoreId);
            string newScoreId = (((SyncroC.DataBase.LineConfig.SlitterScorerHeader)((PropertyGrid)s).SelectedObject).ScoreTypes[scr].Scorer.ScoreId);
            if (newScoreId != this.modScoreTypes[scr])
            {
              //agrassi_FI25-05-2011_1
              //cfg.SlitterScorerConfig.CheckScoreProfiles(((SyncroC.DataBase.LineConfig.SlitterScorer)((PropertyGrid)s).SelectedObject).SlitterScorerHeader, scr);
              cfg.SlitterScorerConfig.CheckScoreProfiles(((SyncroC.DataBase.LineConfig.SlitterScorerHeader)((PropertyGrid)s).SelectedObject), scr);
              this.modScoreTypes[scr] = newScoreId;
              ((PropertyGrid)s).Refresh();
              break;
            }
          }
          //agrassi_FI25-05-2011_1
          if (this.slitterScorerName != (((SyncroC.DataBase.LineConfig.SlitterScorerHeader)((PropertyGrid)s).SelectedObject).Name))
          {
            this.slitterScorerName = (((SyncroC.DataBase.LineConfig.SlitterScorerHeader)((PropertyGrid)s).SelectedObject).Name);
            if (this.slitterScorerName=="Twin2")
              this.TwinCfgGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Twin2)cfg.SlitterScorerConfig.Twin2.CreateCopy();
            else if (this.slitterScorerName=="Compact2")
              this.TwinCfgGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Compact2)cfg.SlitterScorerConfig.Compact2.CreateCopy();
            else if (this.slitterScorerName=="TwinE" || this.slitterScorerName=="Twin400")
              this.TwinCfgGrid.SelectedObject = (SyncroC.DataBase.LineConfig.Twin400)cfg.SlitterScorerConfig.Twin400.CreateCopy();
          }
          //}
          //else
          //  this.TwinCfgGrid.SelectedObject = (DataBase.LineConfig.SlitterScorer)cfg.SlitterScorerConfig.CreateCopy();

        }
        //agrassi_FI25-05-2011_1
        else if (((PropertyGrid)s).Equals(this.TwinCfgGrid))
          this.modTwin = true;
        else if (((PropertyGrid)s).Equals(this.WebCfgGrid))
        {
          if (DBManager.getDBManager().lineConfig.WebDirector)
            this.modWeb = true;
          else
            this.WebCfgGrid.SelectedObject = (WebDirector)cfg.WebDirectorConfig.CreateCopy();
        }
      }
    }

    protected override void Escape()
    {
      if (this.modify)
      {
        this.modify = false;
        lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor =
            lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Title_YN.BackColor;
        this.TwinCfgGrid.ViewForeColor = Color.DimGray;
        //agrassi_FI25-05-2011_1
        this.TwinHeaderCfgGrid.ViewForeColor = Color.DimGray;
        this.WebCfgGrid.ViewForeColor = Color.DimGray;
        this.modScoreTypes.Initialize();
        this.refreshData();
      }
    }
  }
}

