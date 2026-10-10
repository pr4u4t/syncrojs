using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.DataBase;
using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.Forms.PopUp;
using SyncroC.Controls;
using SyncroC.DataBase.LineConfig;
using DGVPrinterHelper;

namespace SyncroC.Forms.Pages.Production
{
  public class Frm_Quality : SyncroC.Forms.Pages.Production.Frm_ProductionGroup
  {
    private SyncroC.Controls.Title ttl_QualityList;
    private SyncroC.Controls.TitledTextBox ttb_Text;
    private SyncroC.Controls.TitledTextBox ttb_QualityID;
    private SyncroC.Controls.Title ttl_PaperType;
    private SyncroC.Controls.TitledNumericUpDown tnud_Thickness;
    private SyncroC.Controls.TitledNumericUpDown tnud_AOCSpeed;
    private SyncroC.Controls.TitledNumericUpDown tnud_ProductionSpeed;
    private SyncroC.Controls.TitledNumericUpDown tnud_WeightFactor;
    private SyncroC.Controls.TitledList tlst_FluteID;
    private SyncroC.Controls.TitledNumericUpDown tnud_LubricationTime;
    private SyncroC.Controls.TitledNumericUpDown tnud_GrindingDuration;
    private SyncroC.Controls.TitledNumericUpDown tnud_TrimGrindingMultiplier;
    private SyncroC.Controls.TitledNumericUpDown tnud_LubricationParameter;
    private SyncroC.Controls.TitledNumericUpDown tnud_GrindingParameter;
    private SyncroC.Controls.TitledNumericUpDown tnud_ScoreGap;
    private SyncroC.Controls.Title ttl_SPEED;
    private SyncroC.Controls.Title ttl_Multistrater;
    private SyncroC.Controls.Title ttl_SlitterScore;
    private SyncroC.Controls.CheckBox chk_prePrint;
    private SyncroC.Controls.TitledList cmb_Paper6;
    private SyncroC.Controls.TitledList cmb_Paper5;
    private SyncroC.Controls.TitledList cmb_Paper4;
    private SyncroC.Controls.TitledList cmb_Paper2;
    private SyncroC.Controls.TitledList cmb_Paper1;
    private SyncroC.Controls.TitledList cmb_Paper0;
    private SyncroC.Controls.TitledList cmb_Paper3;
    private SyncroC.Controls.TitledNumericUpDown tnud_BundleLayersXStack;
    private SyncroC.Controls.CheckBox chk_BundleReverseMode;
    private System.Windows.Forms.Panel pnl_DRY;
    private System.Windows.Forms.Panel pnl_WET;
    private System.ComponentModel.IContainer components = null;
    private enum State { Insert, Modify, Read };
    private State state = State.Read;
    private SyncroC.Controls.Bracket brk_details;
    private SyncroC.Controls.GridView gdv_Quality;
    private bool ProductionSpeed_Warning;
    private bool AOCSpeed_Warning;
    private DataGridViewColumn clm_QualityCode;
    private PictureBox pct4;
    private PictureBox pct5;
    private PictureBox pct1;
    private PictureBox pct3;
    private PictureBox pct2;
    private PictureBox pct0;
    private PictureBox pct6;
    private SyncroC.Controls.CheckBox chk_EnableInclinateLastBelt;
    //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
    private SyncroC.Controls.CheckBox chk_InternalFlapDisabled;
    private SyncroC.Graph.GraphLinesEditor graphLinesEditor;
    private Panel pnl_PRC;
    private TreeView treeViewPRC;

    private string WEIGTH_FACTOR_SEPARATOR = " - ";

    private System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>>> listaModifiche = null;
    private PictureBox pct_PRCoverride;
    private DataGridView grd_data;
    private System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>> ultimoGrafico = null;
    private myLabel lbl_typeOfClassification;
    private myLabel lbl_filter;
    private DataTable myDataTable = null;
    private Button printGrid;
    private myLabel lbl_typeOfQuery;
    private PictureBox pct_Lock;
    private TitledNumericUpDown tnud_CruisingSpeed;

    private string filter = "";

    private void updateControlsEnable() //PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
    {
      DBManager dbm;
      SyncroC.DataBase.SyncroConfig.STKTerminal400CommsSettings stk1;
      SyncroC.DataBase.SyncroConfig.STKTerminal400CommsSettings stk2;
      SyncroC.DataBase.SyncroConfig.STKTerminal400CommsSettings stk3;


      dbm = DBManager.getDBManager();
      stk1 = dbm.syncroConfig.StkTerminal400Level1Settings;
      stk2 = dbm.syncroConfig.StkTerminal400Level2Settings;
      stk3 = dbm.syncroConfig.StkTerminal400Level3Settings;

      if (stk1.Enable || stk2.Enable || stk3.Enable)
      {
        if (chk_EnableInclinateLastBelt.Visible == false)
          chk_EnableInclinateLastBelt.Visible = true;

        //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
        if (SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastLow())
        {
          //if (chk_EnableInclinateLastBelt.BackColor != System.Drawing.Color.WhiteSmoke)
          //  chk_EnableInclinateLastBelt.BackColor = System.Drawing.Color.WhiteSmoke;
          chk_EnableInclinateLastBelt.ImagePassword = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];

          if (chk_EnableInclinateLastBelt.Enabled == false)
            chk_EnableInclinateLastBelt.Enabled = true;
        }
        else
        {
          //if (chk_EnableInclinateLastBelt.BackColor != System.Drawing.Color.Gainsboro)
          //  chk_EnableInclinateLastBelt.BackColor = System.Drawing.Color.Gainsboro;
          chk_EnableInclinateLastBelt.ImagePassword = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];

          if (chk_EnableInclinateLastBelt.Enabled == true)
            chk_EnableInclinateLastBelt.Enabled = false;
        }
      }
      else
      {
        chk_EnableInclinateLastBelt.Enabled = false;
        chk_EnableInclinateLastBelt.Visible = false;
      }

      //RFAZZI_FI09/03/2011_1. Aggiunta password su campo "Target Speed". 
      if (SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastLow())
      {
        this.tnud_CruisingSpeed.ImagePassword = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
      }
      else
      {
        this.tnud_CruisingSpeed.ImagePassword = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
      }

      //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      SyncroC.DataBase.SyncroConfig.SSTwin22_60Settings twin400;
      twin400 = dbm.syncroConfig.Twin22_60Setting;
      if (twin400.Enable)
      {
        if (chk_InternalFlapDisabled.Visible == false)
          chk_InternalFlapDisabled.Visible = true;


        //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
        if (SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastLow())
        {
          chk_InternalFlapDisabled.ImagePassword = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
          //if (chk_InternalFlapDisabled.BackColor != System.Drawing.Color.WhiteSmoke)
          //  chk_InternalFlapDisabled.BackColor = System.Drawing.Color.WhiteSmoke;

          if (chk_InternalFlapDisabled.Enabled == false)
            chk_InternalFlapDisabled.Enabled = true;
        }
        else
        {
          chk_InternalFlapDisabled.ImagePassword = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
          //if (chk_InternalFlapDisabled.BackColor != System.Drawing.Color.Gainsboro)
          //  chk_InternalFlapDisabled.BackColor = System.Drawing.Color.Gainsboro;

          if (chk_InternalFlapDisabled.Enabled == true)
            chk_InternalFlapDisabled.Enabled = false;
        }
      }
      else
      {
        chk_InternalFlapDisabled.Visible = false;
        chk_InternalFlapDisabled.Enabled = false;
      }

      //RFAZZI_FI07/06/2010_1. Aggiunta gestione per togliere a run time i parametri sulla lubrificazione in caso di Twin400V1. 
      SyncroC.DataBase.LineConfig.SlitterScorer ss;
      ss = dbm.lineConfig.SlitterScorerConfig;
      //agrassi_FI23-06-2011_1
      //if (twin400.Enable)
      //{
        this.tnud_LubricationParameter.Visible = ss.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication;
        this.tnud_LubricationTime.Visible = ss.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication;
      //}   

      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      if (SyncroC.Utility.Settings.getSettings().configuration.PRC)
      {
        //ABELLO_FI14/08/2010_1. Riviste le password
        //pct_Lock.Visible = !Accessibility.getAccessibility().isLevelAtLeastLow();
        pct_Lock.Visible = !Accessibility.getAccessibility().isLevelAtLeastMedium();
      }
    }

    public Frm_Quality()
    {
      InitializeComponent();

      DataView dataView = new DataView(DBManager.getDBManager().quality.getTable());
      dataView.Sort = Quality.QUALITY_ID + " Asc";
      this.gdv_Quality.DataSource = dataView;

      clm_QualityCode = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_QualityCode.Name = Quality.QUALITY_ID;
      clm_QualityCode.HeaderText = "Quality Code";
      clm_QualityCode.DataPropertyName = Quality.QUALITY_ID;
      //clm_QualityCode.Width = 250;
      clm_QualityCode.Width = 354;

      gdv_Quality.Columns.Add(clm_QualityCode);

      DBManager.getDBManager().paper.OnDBPaperChange += new SyncroC.DataBase.Paper.DBChangedHandler(refreshPaper);
      DBManager.getDBManager().flute.OnDBFluteChange += new SyncroC.DataBase.Flute.DBChangedHandler(refreshFlute);
      DBManager.getDBManager().quality.OnDBQualityChange += new Quality.DBChangedHandler(quality_RowChanged);

      this.lbl_filter.Testo = (this.filter == "") ? "" : "!";

      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        this.lbl_filter.Visible = true;

      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
      {
        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
        this.lbl_CtrlF5_YY.Text = "";
      }

      this.PRC_Init();
    }

    //agrassi_FI10-12-2010_1
    /// <summary>
    /// The page has to be kept into cache when insert or modify
    /// </summary>
    //public override bool IsRemovableFromCache()
    //{
    //  return this.state == State.Read;
    //}

    public override void refreshMySQLData()
    {
      try
      {
        //ABELLO_FI30/11/2010_2 il rinfresco della pagina se sono in modifica dà noia.
        if (this.state != State.Read)
          return;

        this.lbl_filter.Testo = (this.filter == "") ? "" : "!";

        DataRow row = null;
        int FirstDisplayedScrollingRowIndex = this.gdv_Quality.FirstDisplayedScrollingRowIndex;

        if (this.gdv_Quality != null)
          if (this.gdv_Quality.SelectedRows != null)
            if (this.gdv_Quality.SelectedRows.Count != 0)
              row = ((DataRowView)this.gdv_Quality.SelectedRows[0].DataBoundItem).Row;

        this.gdv_Quality.SuspendLayout();

        this.gdv_Quality.DataSource = DBManager.getDBManager().quality.getTable(this.filter);

        if (row != null)
        {
          for (int i = 0; i < this.gdv_Quality.Rows.Count; i++)
          {
            string a = ((DataRowView)this.gdv_Quality.Rows[i].DataBoundItem).Row[Quality.QUALITY_ID].ToString().Trim();
            string b = row[Quality.QUALITY_ID].ToString().Trim();
            if (a == b)
            {
              this.gdv_Quality.Rows[i].Selected = true;
            }
            else
              this.gdv_Quality.Rows[i].Selected = false;
          }
        }

        if ((row == null) && (this.gdv_Quality.Rows.Count > 0))
          this.gdv_Quality.Rows[0].Selected = true;

        if ((FirstDisplayedScrollingRowIndex >= 0) && (FirstDisplayedScrollingRowIndex <= DBManager.getDBManager().quality.getCount()))
          this.gdv_Quality.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex;

        this.gdv_Quality.ResumeLayout();
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    private string qualityIdFilter = "";
    private string fluteFilter = "";
    private string paperIdFilter = "";
    private string paperWeightFactorFilter = "";
    private void lbl_filter_Click(object sender, EventArgs e)
    {
      try
      {
        //Flute
        DataTable fluteTable = DBManager.getDBManager().flute.getTable();
        string[] fluteStrings = new string[fluteTable.Rows.Count];

        for (int i = 0; i < fluteTable.Rows.Count; i++)
          fluteStrings[i] = fluteTable.Rows[i][Flute.FLUTE_ID].ToString();

        Frm_FilterQuality askFilterQuality = new Frm_FilterQuality();
        FosberDialogResult ret = askFilterQuality.ShowDialog(this, this.qualityIdFilter, this.fluteFilter, fluteStrings, this.paperIdFilter, this.paperWeightFactorFilter);
        if (ret.Dialog == DialogResult.OK)
        {
          this.filter = "";

          this.qualityIdFilter = ((string)ret.Data[0]).Trim();
          this.fluteFilter = ((string)ret.Data[1]).Trim();
          this.paperIdFilter= ((string)ret.Data[2]).Trim();
          this.paperWeightFactorFilter = ((string)ret.Data[3]).Trim();

          if (this.qualityIdFilter != "")
            this.filter = String.Format("(qualityid LIKE '{0}%')", this.qualityIdFilter);

          if (this.fluteFilter != "")
          {
            if (this.filter != "")
              this.filter += String.Format("AND (flute = '{0}')", this.fluteFilter);
            else
              this.filter = String.Format("(flute = '{0}')", this.fluteFilter);
          }

          if (this.paperIdFilter != "")
          {
            if (this.filter != "")
              this.filter += String.Format("AND (paper1id LIKE '{0}%' OR paper2id LIKE '{0}%' OR paper3id LIKE '{0}%' OR paper4id LIKE '{0}%' OR paper5id LIKE '{0}%' OR paper6id LIKE '{0}%' OR paper7id LIKE '{0}%')", this.paperIdFilter);
            else
              this.filter = String.Format("(paper1id LIKE '{0}%' OR paper2id LIKE '{0}%' OR paper3id LIKE '{0}%' OR paper4id LIKE '{0}%' OR paper5id LIKE '{0}%' OR paper6id LIKE '{0}%' OR paper7id LIKE '{0}%')", this.paperIdFilter);
          }

          if (this.paperWeightFactorFilter != "")
          {
            int minLimit =0;
            int maxLimit=0;
            SyncroC.DataBase.PRCManager.getPRCManager().getPaperTreshold(this.paperWeightFactorFilter, out minLimit, out maxLimit);

            string appoWeightFilter = "(";
            for (int i = 1; i <= 7; i++)
            {
              appoWeightFilter += String.Format("((paper{0}weightfactor_uwg_m>{1}) AND (paper{0}weightfactor_uwg_m<={2}))", i, minLimit, maxLimit);
              if (i < 7)
                appoWeightFilter += " OR ";
            }
            appoWeightFilter += ")";

            if (this.filter != "")
              this.filter += "AND " + appoWeightFilter;
            else
              this.filter = appoWeightFilter;
          }

          this.gdv_Quality.SuspendLayout();
          this.gdv_Quality.DataSource = DBManager.getDBManager().quality.getTable(this.filter);
          if (this.gdv_Quality.Rows.Count > 0)
            this.gdv_Quality.Rows[0].Selected = true;
          this.gdv_Quality.ResumeLayout();

          this.lbl_filter.Testo = (this.filter == "") ? "" : "!";
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
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
      this.gdv_Quality.DataSource = null;

      DBManager.getDBManager().quality.OnDBQualityChange -= new Quality.DBChangedHandler(quality_RowChanged);
      DBManager.getDBManager().paper.OnDBPaperChange -= new SyncroC.DataBase.Paper.DBChangedHandler(refreshPaper);
      DBManager.getDBManager().flute.OnDBFluteChange -= new SyncroC.DataBase.Flute.DBChangedHandler(refreshFlute);

      base.Dispose(disposing);
    }

    #region Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Quality));
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("DB");
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("TE1");
      System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("TE11");
      System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("TE21");
      System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("TE20");
      System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("PHT", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
      System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("GG1");
      System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("GG2");
      System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("GU", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
      System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("TS1");
      System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("SPL1", new System.Windows.Forms.TreeNode[] {
            treeNode10});
      System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("SF1");
      System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("SPL2");
      System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("SPL3");
      this.ttl_QualityList = new SyncroC.Controls.Title();
      this.ttb_Text = new SyncroC.Controls.TitledTextBox();
      this.ttb_QualityID = new SyncroC.Controls.TitledTextBox();
      this.ttl_PaperType = new SyncroC.Controls.Title();
      this.cmb_Paper6 = new SyncroC.Controls.TitledList();
      this.cmb_Paper5 = new SyncroC.Controls.TitledList();
      this.cmb_Paper4 = new SyncroC.Controls.TitledList();
      this.cmb_Paper2 = new SyncroC.Controls.TitledList();
      this.cmb_Paper1 = new SyncroC.Controls.TitledList();
      this.cmb_Paper0 = new SyncroC.Controls.TitledList();
      this.chk_prePrint = new SyncroC.Controls.CheckBox();
      this.tnud_WeightFactor = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_Thickness = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_AOCSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_ProductionSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.tlst_FluteID = new SyncroC.Controls.TitledList();
      this.tnud_BundleLayersXStack = new SyncroC.Controls.TitledNumericUpDown();
      this.pnl_DRY = new System.Windows.Forms.Panel();
      this.tnud_CruisingSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.chk_InternalFlapDisabled = new SyncroC.Controls.CheckBox();
      this.pct1 = new System.Windows.Forms.PictureBox();
      this.pct3 = new System.Windows.Forms.PictureBox();
      this.pct2 = new System.Windows.Forms.PictureBox();
      this.pct0 = new System.Windows.Forms.PictureBox();
      this.pct6 = new System.Windows.Forms.PictureBox();
      this.pct4 = new System.Windows.Forms.PictureBox();
      this.pct5 = new System.Windows.Forms.PictureBox();
      this.ttl_SPEED = new SyncroC.Controls.Title();
      this.cmb_Paper3 = new SyncroC.Controls.TitledList();
      this.tnud_LubricationTime = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_GrindingDuration = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_LubricationParameter = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_GrindingParameter = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_ScoreGap = new SyncroC.Controls.TitledNumericUpDown();
      this.ttl_SlitterScore = new SyncroC.Controls.Title();
      this.tnud_TrimGrindingMultiplier = new SyncroC.Controls.TitledNumericUpDown();
      this.chk_BundleReverseMode = new SyncroC.Controls.CheckBox();
      this.ttl_Multistrater = new SyncroC.Controls.Title();
      this.pnl_WET = new System.Windows.Forms.Panel();
      this.chk_EnableInclinateLastBelt = new SyncroC.Controls.CheckBox();
      this.brk_details = new SyncroC.Controls.Bracket();
      this.gdv_Quality = new SyncroC.Controls.GridView();
      this.graphLinesEditor = new SyncroC.Graph.GraphLinesEditor();
      this.pnl_PRC = new System.Windows.Forms.Panel();
      this.pct_Lock = new System.Windows.Forms.PictureBox();
      this.grd_data = new System.Windows.Forms.DataGridView();
      this.lbl_typeOfQuery = new SyncroC.Controls.myLabel();
      this.printGrid = new System.Windows.Forms.Button();
      this.lbl_typeOfClassification = new SyncroC.Controls.myLabel();
      this.pct_PRCoverride = new System.Windows.Forms.PictureBox();
      this.treeViewPRC = new System.Windows.Forms.TreeView();
      this.lbl_filter = new SyncroC.Controls.myLabel();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.pnl_DRY.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct0)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct6)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct4)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct5)).BeginInit();
      this.pnl_WET.SuspendLayout();
      this.pnl_PRC.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Lock)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.grd_data)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_PRCoverride)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "Quality";
      // 
      // tab_Tab5
      // 
      this.tab_Tab5.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab5.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      this.tab_Tab5.Tag = "";
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      this.tab_TabA.Text = "Main";
      this.tab_TabA.Visible = true;
      // 
      // tab_TabC
      // 
      this.tab_TabC.Text = "PRC";
      // 
      // tab_TabB
      // 
      this.tab_TabB.Text = "Other";
      this.tab_TabB.Visible = true;
      // 
      // lbl_CtrlF4_YY
      // 
      this.lbl_CtrlF4_YY.Text = "Save";
      // 
      // lbl_CtrlF1_YY
      // 
      this.lbl_CtrlF1_YY.Text = "New";
      // 
      // lbl_CtrlF2_YY
      // 
      this.lbl_CtrlF2_YY.Text = "Erase";
      // 
      // lbl_CtrlF5_YY
      // 
      this.lbl_CtrlF5_YY.Text = "Modify";
      // 
      // lbl_CtrlF3_YY
      // 
      this.lbl_CtrlF3_YY.Text = "Erase All";
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
      // ttl_QualityList
      // 
      this.ttl_QualityList.BackColor = System.Drawing.Color.Black;
      this.ttl_QualityList.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_QualityList.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_QualityList.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_QualityList.Location = new System.Drawing.Point(30, 193);
      this.ttl_QualityList.Name = "ttl_QualityList";
      this.ttl_QualityList.Size = new System.Drawing.Size(279, 519);
      this.ttl_QualityList.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_QualityList.TabIndex = 107;
      this.ttl_QualityList.TabStop = false;
      this.ttl_QualityList.Text = "QUALITY LIST";
      this.ttl_QualityList.TranslateText = true;
      // 
      // ttb_Text
      // 
      this.ttb_Text.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_Text.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Text.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Text.ImagePassword = null;
      this.ttb_Text.ImeMode = System.Windows.Forms.ImeMode.On;
      this.ttb_Text.Location = new System.Drawing.Point(621, 220);
      this.ttb_Text.MaxChar = 100;
      this.ttb_Text.Multiline = false;
      this.ttb_Text.Name = "ttb_Text";
      this.ttb_Text.PasswordChar = '\0';
      this.ttb_Text.ReadOnly = true;
      this.ttb_Text.Size = new System.Drawing.Size(481, 65);
      this.ttb_Text.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_Text.TabIndex = 2;
      this.ttb_Text.TabStop = false;
      this.ttb_Text.Text = "Text";
      this.ttb_Text.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_Text.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Text.TranslateText = true;
      this.ttb_Text.Value = " ";
      this.ttb_Text.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // ttb_QualityID
      // 
      this.ttb_QualityID.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_QualityID.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_QualityID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_QualityID.ImagePassword = null;
      this.ttb_QualityID.ImeMode = System.Windows.Forms.ImeMode.On;
      this.ttb_QualityID.Location = new System.Drawing.Point(354, 220);
      this.ttb_QualityID.MaxChar = 30;
      this.ttb_QualityID.Multiline = false;
      this.ttb_QualityID.Name = "ttb_QualityID";
      this.ttb_QualityID.PasswordChar = '\0';
      this.ttb_QualityID.ReadOnly = true;
      this.ttb_QualityID.Size = new System.Drawing.Size(261, 64);
      this.ttb_QualityID.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_QualityID.TabIndex = 1;
      this.ttb_QualityID.TabStop = false;
      this.ttb_QualityID.Text = "Quality Code";
      this.ttb_QualityID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_QualityID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_QualityID.TranslateText = true;
      this.ttb_QualityID.Value = " ";
      this.ttb_QualityID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_QualityID.Validating += new System.ComponentModel.CancelEventHandler(this.ttb_QualityID_YN_Validating);
      // 
      // ttl_PaperType
      // 
      this.ttl_PaperType.BackColor = System.Drawing.Color.Black;
      this.ttl_PaperType.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PaperType.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PaperType.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_PaperType.Location = new System.Drawing.Point(558, 17);
      this.ttl_PaperType.Name = "ttl_PaperType";
      this.ttl_PaperType.Size = new System.Drawing.Size(290, 368);
      this.ttl_PaperType.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_PaperType.TabIndex = 136;
      this.ttl_PaperType.TabStop = false;
      this.ttl_PaperType.Text = "PAPERS";
      this.ttl_PaperType.TranslateText = true;
      // 
      // cmb_Paper6
      // 
      this.cmb_Paper6.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_Paper6.CanCancel = false;
      this.cmb_Paper6.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Paper6.ForeColor = System.Drawing.Color.Black;
      this.cmb_Paper6.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper6.ImagePassword = null;
      this.cmb_Paper6.Location = new System.Drawing.Point(567, 41);
      this.cmb_Paper6.Name = "cmb_Paper6";
      this.cmb_Paper6.ReadOnly = true;
      this.cmb_Paper6.SelectedIndex = -1;
      this.cmb_Paper6.SelectedItem = "";
      this.cmb_Paper6.SelectedItemData = "";
      this.cmb_Paper6.Size = new System.Drawing.Size(220, 42);
      this.cmb_Paper6.Sorted = true;
      this.cmb_Paper6.Style = SyncroC.Controls.TitledListStyles.NoTitle;
      this.cmb_Paper6.TabIndex = 20;
      this.cmb_Paper6.TabStop = false;
      this.cmb_Paper6.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper6.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper6.TranslateText = true;
      this.cmb_Paper6.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper6.Visible = false;
      this.cmb_Paper6.Validating += new System.ComponentModel.CancelEventHandler(this.cmb_Paper_Validating);
      // 
      // cmb_Paper5
      // 
      this.cmb_Paper5.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_Paper5.CanCancel = false;
      this.cmb_Paper5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Paper5.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper5.ImagePassword = null;
      this.cmb_Paper5.Location = new System.Drawing.Point(567, 91);
      this.cmb_Paper5.Name = "cmb_Paper5";
      this.cmb_Paper5.ReadOnly = true;
      this.cmb_Paper5.SelectedIndex = -1;
      this.cmb_Paper5.SelectedItem = "";
      this.cmb_Paper5.SelectedItemData = "";
      this.cmb_Paper5.Size = new System.Drawing.Size(220, 42);
      this.cmb_Paper5.Sorted = true;
      this.cmb_Paper5.Style = SyncroC.Controls.TitledListStyles.NoTitle;
      this.cmb_Paper5.TabIndex = 19;
      this.cmb_Paper5.TabStop = false;
      this.cmb_Paper5.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper5.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper5.TranslateText = true;
      this.cmb_Paper5.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper5.Visible = false;
      this.cmb_Paper5.Validating += new System.ComponentModel.CancelEventHandler(this.cmb_Paper_Validating);
      // 
      // cmb_Paper4
      // 
      this.cmb_Paper4.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_Paper4.CanCancel = false;
      this.cmb_Paper4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Paper4.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper4.ImagePassword = null;
      this.cmb_Paper4.Location = new System.Drawing.Point(567, 141);
      this.cmb_Paper4.Name = "cmb_Paper4";
      this.cmb_Paper4.ReadOnly = true;
      this.cmb_Paper4.SelectedIndex = -1;
      this.cmb_Paper4.SelectedItem = "";
      this.cmb_Paper4.SelectedItemData = "";
      this.cmb_Paper4.Size = new System.Drawing.Size(220, 42);
      this.cmb_Paper4.Sorted = true;
      this.cmb_Paper4.Style = SyncroC.Controls.TitledListStyles.NoTitle;
      this.cmb_Paper4.TabIndex = 18;
      this.cmb_Paper4.TabStop = false;
      this.cmb_Paper4.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper4.TranslateText = true;
      this.cmb_Paper4.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper4.Visible = false;
      this.cmb_Paper4.Validating += new System.ComponentModel.CancelEventHandler(this.cmb_Paper_Validating);
      // 
      // cmb_Paper2
      // 
      this.cmb_Paper2.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_Paper2.CanCancel = false;
      this.cmb_Paper2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Paper2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper2.ImagePassword = null;
      this.cmb_Paper2.Location = new System.Drawing.Point(567, 241);
      this.cmb_Paper2.Name = "cmb_Paper2";
      this.cmb_Paper2.ReadOnly = true;
      this.cmb_Paper2.SelectedIndex = -1;
      this.cmb_Paper2.SelectedItem = "";
      this.cmb_Paper2.SelectedItemData = "";
      this.cmb_Paper2.Size = new System.Drawing.Size(220, 42);
      this.cmb_Paper2.Sorted = true;
      this.cmb_Paper2.Style = SyncroC.Controls.TitledListStyles.NoTitle;
      this.cmb_Paper2.TabIndex = 16;
      this.cmb_Paper2.TabStop = false;
      this.cmb_Paper2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper2.TranslateText = true;
      this.cmb_Paper2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper2.Validating += new System.ComponentModel.CancelEventHandler(this.cmb_Paper_Validating);
      // 
      // cmb_Paper1
      // 
      this.cmb_Paper1.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_Paper1.CanCancel = false;
      this.cmb_Paper1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Paper1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper1.ImagePassword = null;
      this.cmb_Paper1.Location = new System.Drawing.Point(567, 291);
      this.cmb_Paper1.Name = "cmb_Paper1";
      this.cmb_Paper1.ReadOnly = true;
      this.cmb_Paper1.SelectedIndex = -1;
      this.cmb_Paper1.SelectedItem = "";
      this.cmb_Paper1.SelectedItemData = "";
      this.cmb_Paper1.Size = new System.Drawing.Size(220, 42);
      this.cmb_Paper1.Sorted = true;
      this.cmb_Paper1.Style = SyncroC.Controls.TitledListStyles.NoTitle;
      this.cmb_Paper1.TabIndex = 15;
      this.cmb_Paper1.TabStop = false;
      this.cmb_Paper1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper1.TranslateText = true;
      this.cmb_Paper1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper1.Validating += new System.ComponentModel.CancelEventHandler(this.cmb_Paper_Validating);
      // 
      // cmb_Paper0
      // 
      this.cmb_Paper0.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_Paper0.CanCancel = false;
      this.cmb_Paper0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Paper0.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper0.ImagePassword = null;
      this.cmb_Paper0.Location = new System.Drawing.Point(567, 341);
      this.cmb_Paper0.Name = "cmb_Paper0";
      this.cmb_Paper0.ReadOnly = true;
      this.cmb_Paper0.SelectedIndex = -1;
      this.cmb_Paper0.SelectedItem = "";
      this.cmb_Paper0.SelectedItemData = "";
      this.cmb_Paper0.Size = new System.Drawing.Size(220, 42);
      this.cmb_Paper0.Sorted = true;
      this.cmb_Paper0.Style = SyncroC.Controls.TitledListStyles.NoTitle;
      this.cmb_Paper0.TabIndex = 14;
      this.cmb_Paper0.TabStop = false;
      this.cmb_Paper0.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper0.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper0.TranslateText = true;
      this.cmb_Paper0.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper0.Validating += new System.ComponentModel.CancelEventHandler(this.cmb_Paper_Validating);
      // 
      // chk_prePrint
      // 
      this.chk_prePrint.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_prePrint.Checked = true;
      this.chk_prePrint.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_prePrint.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_prePrint.ImagePassword = null;
      this.chk_prePrint.Location = new System.Drawing.Point(4, 219);
      this.chk_prePrint.Name = "chk_prePrint";
      this.chk_prePrint.ReadOnly = true;
      this.chk_prePrint.Size = new System.Drawing.Size(230, 67);
      this.chk_prePrint.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_prePrint.TabIndex = 33;
      this.chk_prePrint.TabStop = false;
      this.chk_prePrint.Text = "Pre Print";
      this.chk_prePrint.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_prePrint.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_prePrint.TranslateText = true;
      // 
      // tnud_WeightFactor
      // 
      this.tnud_WeightFactor.DecimalPlaces = 0;
      this.tnud_WeightFactor.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_WeightFactor.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor.ImagePassword = null;
      this.tnud_WeightFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_WeightFactor.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_WeightFactor.Location = new System.Drawing.Point(4, 17);
      this.tnud_WeightFactor.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.tnud_WeightFactor.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_WeightFactor.Name = "tnud_WeightFactor";
      this.tnud_WeightFactor.ReadOnly = true;
      this.tnud_WeightFactor.Size = new System.Drawing.Size(230, 70);
      this.tnud_WeightFactor.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.tnud_WeightFactor.TabIndex = 31;
      this.tnud_WeightFactor.TabStop = false;
      this.tnud_WeightFactor.Text = "Weight Factor";
      this.tnud_WeightFactor.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor.TranslateText = true;
      this.tnud_WeightFactor.UnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor.Value = "0";
      this.tnud_WeightFactor.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_Thickness
      // 
      this.tnud_Thickness.DecimalPlaces = 0;
      this.tnud_Thickness.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Thickness.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Thickness.ImagePassword = null;
      this.tnud_Thickness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Thickness.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_Thickness.Location = new System.Drawing.Point(4, 118);
      this.tnud_Thickness.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.tnud_Thickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Thickness.Name = "tnud_Thickness";
      this.tnud_Thickness.ReadOnly = true;
      this.tnud_Thickness.Size = new System.Drawing.Size(230, 70);
      this.tnud_Thickness.Style = SyncroC.Controls.TitledNumericUpDownStyles.Normal;
      this.tnud_Thickness.TabIndex = 32;
      this.tnud_Thickness.TabStop = false;
      this.tnud_Thickness.Text = "Thickness";
      this.tnud_Thickness.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Thickness.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Thickness.TranslateText = true;
      this.tnud_Thickness.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tnud_Thickness.Value = "10";
      this.tnud_Thickness.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Thickness.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_AOCSpeed
      // 
      this.tnud_AOCSpeed.DecimalPlaces = 0;
      this.tnud_AOCSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_AOCSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_AOCSpeed.ImagePassword = null;
      this.tnud_AOCSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_AOCSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_AOCSpeed.Location = new System.Drawing.Point(406, 318);
      this.tnud_AOCSpeed.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.tnud_AOCSpeed.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.tnud_AOCSpeed.Name = "tnud_AOCSpeed";
      this.tnud_AOCSpeed.ReadOnly = true;
      this.tnud_AOCSpeed.Size = new System.Drawing.Size(140, 70);
      this.tnud_AOCSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_AOCSpeed.TabIndex = 13;
      this.tnud_AOCSpeed.TabStop = false;
      this.tnud_AOCSpeed.Text = "AOC Speed";
      this.tnud_AOCSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_AOCSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_AOCSpeed.TranslateText = true;
      this.tnud_AOCSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_AOCSpeed.Value = "5";
      this.tnud_AOCSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_AOCSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_AOCSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.tnud_AOCSpeed_Validating);
      // 
      // tnud_ProductionSpeed
      // 
      this.tnud_ProductionSpeed.DecimalPlaces = 0;
      this.tnud_ProductionSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_ProductionSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ProductionSpeed.ImagePassword = null;
      this.tnud_ProductionSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_ProductionSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_ProductionSpeed.Location = new System.Drawing.Point(32, 318);
      this.tnud_ProductionSpeed.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.tnud_ProductionSpeed.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.tnud_ProductionSpeed.Name = "tnud_ProductionSpeed";
      this.tnud_ProductionSpeed.ReadOnly = true;
      this.tnud_ProductionSpeed.Size = new System.Drawing.Size(140, 70);
      this.tnud_ProductionSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_ProductionSpeed.TabIndex = 11;
      this.tnud_ProductionSpeed.TabStop = false;
      this.tnud_ProductionSpeed.Text = "Production Speed";
      this.tnud_ProductionSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ProductionSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ProductionSpeed.TranslateText = true;
      this.tnud_ProductionSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_ProductionSpeed.Value = "500";
      this.tnud_ProductionSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ProductionSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_ProductionSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.tnud_ProductionSpeed_Validating);
      // 
      // tlst_FluteID
      // 
      this.tlst_FluteID.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlst_FluteID.CanCancel = false;
      this.tlst_FluteID.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlst_FluteID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlst_FluteID.ImagePassword = null;
      this.tlst_FluteID.Location = new System.Drawing.Point(1108, 220);
      this.tlst_FluteID.Name = "tlst_FluteID";
      this.tlst_FluteID.ReadOnly = true;
      this.tlst_FluteID.SelectedIndex = -1;
      this.tlst_FluteID.SelectedItem = "";
      this.tlst_FluteID.SelectedItemData = "";
      this.tlst_FluteID.Size = new System.Drawing.Size(92, 64);
      this.tlst_FluteID.Sorted = true;
      this.tlst_FluteID.Style = SyncroC.Controls.TitledListStyles.Light;
      this.tlst_FluteID.TabIndex = 3;
      this.tlst_FluteID.TabStop = false;
      this.tlst_FluteID.Text = "Flute ID";
      this.tlst_FluteID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlst_FluteID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlst_FluteID.TranslateText = true;
      this.tlst_FluteID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlst_FluteID.SelectedItemChanged += new System.EventHandler(this.tlst_FluteID_SelectedItemChanged);
      // 
      // tnud_BundleLayersXStack
      // 
      this.tnud_BundleLayersXStack.DecimalPlaces = 0;
      this.tnud_BundleLayersXStack.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_BundleLayersXStack.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_BundleLayersXStack.ImagePassword = null;
      this.tnud_BundleLayersXStack.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_BundleLayersXStack.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_BundleLayersXStack.Location = new System.Drawing.Point(291, 42);
      this.tnud_BundleLayersXStack.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.tnud_BundleLayersXStack.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_BundleLayersXStack.Name = "tnud_BundleLayersXStack";
      this.tnud_BundleLayersXStack.ReadOnly = true;
      this.tnud_BundleLayersXStack.Size = new System.Drawing.Size(229, 70);
      this.tnud_BundleLayersXStack.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_BundleLayersXStack.TabIndex = 41;
      this.tnud_BundleLayersXStack.TabStop = false;
      this.tnud_BundleLayersXStack.Text = "Layers per Stack";
      this.tnud_BundleLayersXStack.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_BundleLayersXStack.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_BundleLayersXStack.TranslateText = true;
      this.tnud_BundleLayersXStack.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_BundleLayersXStack.Value = "1";
      this.tnud_BundleLayersXStack.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_BundleLayersXStack.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // pnl_DRY
      // 
      this.pnl_DRY.Controls.Add(this.tnud_CruisingSpeed);
      this.pnl_DRY.Controls.Add(this.chk_InternalFlapDisabled);
      this.pnl_DRY.Controls.Add(this.pct1);
      this.pnl_DRY.Controls.Add(this.pct3);
      this.pnl_DRY.Controls.Add(this.pct2);
      this.pnl_DRY.Controls.Add(this.pct0);
      this.pnl_DRY.Controls.Add(this.pct6);
      this.pnl_DRY.Controls.Add(this.pct4);
      this.pnl_DRY.Controls.Add(this.pct5);
      this.pnl_DRY.Controls.Add(this.ttl_SPEED);
      this.pnl_DRY.Controls.Add(this.cmb_Paper3);
      this.pnl_DRY.Controls.Add(this.tnud_LubricationTime);
      this.pnl_DRY.Controls.Add(this.tnud_GrindingDuration);
      this.pnl_DRY.Controls.Add(this.tnud_LubricationParameter);
      this.pnl_DRY.Controls.Add(this.tnud_GrindingParameter);
      this.pnl_DRY.Controls.Add(this.tnud_ScoreGap);
      this.pnl_DRY.Controls.Add(this.tnud_ProductionSpeed);
      this.pnl_DRY.Controls.Add(this.tnud_AOCSpeed);
      this.pnl_DRY.Controls.Add(this.ttl_SlitterScore);
      this.pnl_DRY.Controls.Add(this.tnud_TrimGrindingMultiplier);
      this.pnl_DRY.Controls.Add(this.ttl_PaperType);
      this.pnl_DRY.Controls.Add(this.cmb_Paper0);
      this.pnl_DRY.Controls.Add(this.cmb_Paper2);
      this.pnl_DRY.Controls.Add(this.cmb_Paper4);
      this.pnl_DRY.Controls.Add(this.cmb_Paper1);
      this.pnl_DRY.Controls.Add(this.cmb_Paper5);
      this.pnl_DRY.Controls.Add(this.cmb_Paper6);
      this.pnl_DRY.Location = new System.Drawing.Point(350, 288);
      this.pnl_DRY.Name = "pnl_DRY";
      this.pnl_DRY.Size = new System.Drawing.Size(851, 399);
      this.pnl_DRY.TabIndex = 159;
      // 
      // tnud_CruisingSpeed
      // 
      this.tnud_CruisingSpeed.DecimalPlaces = 0;
      this.tnud_CruisingSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_CruisingSpeed.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_CruisingSpeed.ImagePassword = null;
      this.tnud_CruisingSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_CruisingSpeed.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_CruisingSpeed.Location = new System.Drawing.Point(219, 318);
      this.tnud_CruisingSpeed.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_CruisingSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_CruisingSpeed.Name = "tnud_CruisingSpeed";
      this.tnud_CruisingSpeed.ReadOnly = true;
      this.tnud_CruisingSpeed.Size = new System.Drawing.Size(140, 70);
      this.tnud_CruisingSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_CruisingSpeed.TabIndex = 12;
      this.tnud_CruisingSpeed.TabStop = false;
      this.tnud_CruisingSpeed.Text = "Target Speed";
      this.tnud_CruisingSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_CruisingSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_CruisingSpeed.TranslateText = true;
      this.tnud_CruisingSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_CruisingSpeed.Value = "0";
      this.tnud_CruisingSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_CruisingSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // chk_InternalFlapDisabled
      // 
      this.chk_InternalFlapDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_InternalFlapDisabled.Checked = false;
      this.chk_InternalFlapDisabled.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_InternalFlapDisabled.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_InternalFlapDisabled.ImagePassword = null;
      this.chk_InternalFlapDisabled.Location = new System.Drawing.Point(32, 227);
      this.chk_InternalFlapDisabled.Name = "chk_InternalFlapDisabled";
      this.chk_InternalFlapDisabled.ReadOnly = true;
      this.chk_InternalFlapDisabled.Size = new System.Drawing.Size(220, 55);
      this.chk_InternalFlapDisabled.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_InternalFlapDisabled.TabIndex = 10;
      this.chk_InternalFlapDisabled.Text = "Internal Flap Disabled";
      this.chk_InternalFlapDisabled.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_InternalFlapDisabled.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_InternalFlapDisabled.TranslateText = true;
      // 
      // pct1
      // 
      this.pct1.Image = global::SyncroC.Properties.Resources.medium;
      this.pct1.Location = new System.Drawing.Point(798, 287);
      this.pct1.Name = "pct1";
      this.pct1.Size = new System.Drawing.Size(50, 40);
      this.pct1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct1.TabIndex = 173;
      this.pct1.TabStop = false;
      // 
      // pct3
      // 
      this.pct3.Image = global::SyncroC.Properties.Resources.medium;
      this.pct3.Location = new System.Drawing.Point(798, 191);
      this.pct3.Name = "pct3";
      this.pct3.Size = new System.Drawing.Size(50, 40);
      this.pct3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct3.TabIndex = 172;
      this.pct3.TabStop = false;
      // 
      // pct2
      // 
      this.pct2.Image = global::SyncroC.Properties.Resources.liner;
      this.pct2.Location = new System.Drawing.Point(798, 241);
      this.pct2.Name = "pct2";
      this.pct2.Size = new System.Drawing.Size(50, 40);
      this.pct2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct2.TabIndex = 171;
      this.pct2.TabStop = false;
      // 
      // pct0
      // 
      this.pct0.Image = global::SyncroC.Properties.Resources.liner;
      this.pct0.Location = new System.Drawing.Point(798, 341);
      this.pct0.Name = "pct0";
      this.pct0.Size = new System.Drawing.Size(50, 40);
      this.pct0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct0.TabIndex = 170;
      this.pct0.TabStop = false;
      // 
      // pct6
      // 
      this.pct6.Image = global::SyncroC.Properties.Resources.liner;
      this.pct6.Location = new System.Drawing.Point(798, 41);
      this.pct6.Name = "pct6";
      this.pct6.Size = new System.Drawing.Size(50, 40);
      this.pct6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct6.TabIndex = 169;
      this.pct6.TabStop = false;
      // 
      // pct4
      // 
      this.pct4.Image = global::SyncroC.Properties.Resources.liner;
      this.pct4.Location = new System.Drawing.Point(798, 141);
      this.pct4.Name = "pct4";
      this.pct4.Size = new System.Drawing.Size(50, 40);
      this.pct4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct4.TabIndex = 168;
      this.pct4.TabStop = false;
      // 
      // pct5
      // 
      this.pct5.Image = global::SyncroC.Properties.Resources.medium;
      this.pct5.Location = new System.Drawing.Point(798, 91);
      this.pct5.Name = "pct5";
      this.pct5.Size = new System.Drawing.Size(50, 40);
      this.pct5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct5.TabIndex = 167;
      this.pct5.TabStop = false;
      // 
      // ttl_SPEED
      // 
      this.ttl_SPEED.BackColor = System.Drawing.Color.Black;
      this.ttl_SPEED.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SPEED.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SPEED.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SPEED.Location = new System.Drawing.Point(4, 293);
      this.ttl_SPEED.Name = "ttl_SPEED";
      this.ttl_SPEED.Size = new System.Drawing.Size(548, 92);
      this.ttl_SPEED.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_SPEED.TabIndex = 166;
      this.ttl_SPEED.TabStop = false;
      this.ttl_SPEED.Text = "SPEED";
      this.ttl_SPEED.TranslateText = true;
      // 
      // cmb_Paper3
      // 
      this.cmb_Paper3.BackColor = System.Drawing.Color.WhiteSmoke;
      this.cmb_Paper3.CanCancel = false;
      this.cmb_Paper3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmb_Paper3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper3.ImagePassword = null;
      this.cmb_Paper3.Location = new System.Drawing.Point(567, 191);
      this.cmb_Paper3.Name = "cmb_Paper3";
      this.cmb_Paper3.ReadOnly = true;
      this.cmb_Paper3.SelectedIndex = -1;
      this.cmb_Paper3.SelectedItem = "";
      this.cmb_Paper3.SelectedItemData = "";
      this.cmb_Paper3.Size = new System.Drawing.Size(220, 42);
      this.cmb_Paper3.Sorted = false;
      this.cmb_Paper3.Style = SyncroC.Controls.TitledListStyles.NoTitle;
      this.cmb_Paper3.TabIndex = 17;
      this.cmb_Paper3.TabStop = false;
      this.cmb_Paper3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.cmb_Paper3.TranslateText = true;
      this.cmb_Paper3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.cmb_Paper3.Visible = false;
      this.cmb_Paper3.Validating += new System.ComponentModel.CancelEventHandler(this.cmb_Paper_Validating);
      // 
      // tnud_LubricationTime
      // 
      this.tnud_LubricationTime.DecimalPlaces = 0;
      this.tnud_LubricationTime.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_LubricationTime.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LubricationTime.ImagePassword = null;
      this.tnud_LubricationTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_LubricationTime.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_LubricationTime.Location = new System.Drawing.Point(313, 164);
      this.tnud_LubricationTime.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
      this.tnud_LubricationTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_LubricationTime.Name = "tnud_LubricationTime";
      this.tnud_LubricationTime.ReadOnly = true;
      this.tnud_LubricationTime.Size = new System.Drawing.Size(220, 55);
      this.tnud_LubricationTime.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_LubricationTime.TabIndex = 9;
      this.tnud_LubricationTime.TabStop = false;
      this.tnud_LubricationTime.Text = "Lubrication Time";
      this.tnud_LubricationTime.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationTime.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LubricationTime.TranslateText = true;
      this.tnud_LubricationTime.UnitMeasure = SyncroC.Utility.UnitMeasure.TenthOfSecond;
      this.tnud_LubricationTime.Value = "1";
      this.tnud_LubricationTime.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationTime.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_GrindingDuration
      // 
      this.tnud_GrindingDuration.DecimalPlaces = 0;
      this.tnud_GrindingDuration.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_GrindingDuration.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingDuration.ImagePassword = null;
      this.tnud_GrindingDuration.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GrindingDuration.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_GrindingDuration.Location = new System.Drawing.Point(313, 102);
      this.tnud_GrindingDuration.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
      this.tnud_GrindingDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GrindingDuration.Name = "tnud_GrindingDuration";
      this.tnud_GrindingDuration.ReadOnly = true;
      this.tnud_GrindingDuration.Size = new System.Drawing.Size(220, 55);
      this.tnud_GrindingDuration.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_GrindingDuration.TabIndex = 7;
      this.tnud_GrindingDuration.TabStop = false;
      this.tnud_GrindingDuration.Text = "Grinding Duration";
      this.tnud_GrindingDuration.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingDuration.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingDuration.TranslateText = true;
      this.tnud_GrindingDuration.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_GrindingDuration.Value = "1";
      this.tnud_GrindingDuration.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingDuration.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_LubricationParameter
      // 
      this.tnud_LubricationParameter.DecimalPlaces = 0;
      this.tnud_LubricationParameter.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_LubricationParameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LubricationParameter.ImagePassword = null;
      this.tnud_LubricationParameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_LubricationParameter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_LubricationParameter.Location = new System.Drawing.Point(32, 164);
      this.tnud_LubricationParameter.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
      this.tnud_LubricationParameter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_LubricationParameter.Name = "tnud_LubricationParameter";
      this.tnud_LubricationParameter.ReadOnly = true;
      this.tnud_LubricationParameter.Size = new System.Drawing.Size(220, 55);
      this.tnud_LubricationParameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_LubricationParameter.TabIndex = 8;
      this.tnud_LubricationParameter.TabStop = false;
      this.tnud_LubricationParameter.Text = "Lubrication Frequency";
      this.tnud_LubricationParameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationParameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LubricationParameter.TranslateText = true;
      this.tnud_LubricationParameter.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_LubricationParameter.Value = "1";
      this.tnud_LubricationParameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationParameter.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_GrindingParameter
      // 
      this.tnud_GrindingParameter.DecimalPlaces = 0;
      this.tnud_GrindingParameter.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_GrindingParameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingParameter.ImagePassword = null;
      this.tnud_GrindingParameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GrindingParameter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_GrindingParameter.Location = new System.Drawing.Point(32, 102);
      this.tnud_GrindingParameter.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
      this.tnud_GrindingParameter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GrindingParameter.Name = "tnud_GrindingParameter";
      this.tnud_GrindingParameter.ReadOnly = true;
      this.tnud_GrindingParameter.Size = new System.Drawing.Size(220, 55);
      this.tnud_GrindingParameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_GrindingParameter.TabIndex = 6;
      this.tnud_GrindingParameter.TabStop = false;
      this.tnud_GrindingParameter.Text = "Grinding Frequency";
      this.tnud_GrindingParameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingParameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingParameter.TranslateText = true;
      this.tnud_GrindingParameter.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_GrindingParameter.Value = "1";
      this.tnud_GrindingParameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingParameter.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_ScoreGap
      // 
      this.tnud_ScoreGap.DecimalPlaces = 0;
      this.tnud_ScoreGap.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_ScoreGap.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ScoreGap.ImagePassword = null;
      this.tnud_ScoreGap.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_ScoreGap.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_ScoreGap.Location = new System.Drawing.Point(32, 40);
      this.tnud_ScoreGap.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.tnud_ScoreGap.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_ScoreGap.Name = "tnud_ScoreGap";
      this.tnud_ScoreGap.ReadOnly = true;
      this.tnud_ScoreGap.Size = new System.Drawing.Size(220, 55);
      this.tnud_ScoreGap.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_ScoreGap.TabIndex = 4;
      this.tnud_ScoreGap.TabStop = false;
      this.tnud_ScoreGap.Text = "Score Gap";
      this.tnud_ScoreGap.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ScoreGap.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ScoreGap.TranslateText = true;
      this.tnud_ScoreGap.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tnud_ScoreGap.Value = "10";
      this.tnud_ScoreGap.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ScoreGap.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // ttl_SlitterScore
      // 
      this.ttl_SlitterScore.BackColor = System.Drawing.Color.Black;
      this.ttl_SlitterScore.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_SlitterScore.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_SlitterScore.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_SlitterScore.Location = new System.Drawing.Point(4, 17);
      this.ttl_SlitterScore.Name = "ttl_SlitterScore";
      this.ttl_SlitterScore.Size = new System.Drawing.Size(548, 270);
      this.ttl_SlitterScore.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_SlitterScore.TabIndex = 165;
      this.ttl_SlitterScore.TabStop = false;
      this.ttl_SlitterScore.Text = "SLITTER SCORER";
      this.ttl_SlitterScore.TranslateText = true;
      // 
      // tnud_TrimGrindingMultiplier
      // 
      this.tnud_TrimGrindingMultiplier.DecimalPlaces = 0;
      this.tnud_TrimGrindingMultiplier.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_TrimGrindingMultiplier.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_TrimGrindingMultiplier.ImagePassword = null;
      this.tnud_TrimGrindingMultiplier.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_TrimGrindingMultiplier.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_TrimGrindingMultiplier.Location = new System.Drawing.Point(313, 40);
      this.tnud_TrimGrindingMultiplier.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.tnud_TrimGrindingMultiplier.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_TrimGrindingMultiplier.Name = "tnud_TrimGrindingMultiplier";
      this.tnud_TrimGrindingMultiplier.ReadOnly = true;
      this.tnud_TrimGrindingMultiplier.Size = new System.Drawing.Size(220, 56);
      this.tnud_TrimGrindingMultiplier.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_TrimGrindingMultiplier.TabIndex = 5;
      this.tnud_TrimGrindingMultiplier.TabStop = false;
      this.tnud_TrimGrindingMultiplier.Text = "Trim Grindings Multiplier";
      this.tnud_TrimGrindingMultiplier.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_TrimGrindingMultiplier.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_TrimGrindingMultiplier.TranslateText = true;
      this.tnud_TrimGrindingMultiplier.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_TrimGrindingMultiplier.Value = "1";
      this.tnud_TrimGrindingMultiplier.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_TrimGrindingMultiplier.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // chk_BundleReverseMode
      // 
      this.chk_BundleReverseMode.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_BundleReverseMode.Checked = true;
      this.chk_BundleReverseMode.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_BundleReverseMode.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_BundleReverseMode.ImagePassword = null;
      this.chk_BundleReverseMode.Location = new System.Drawing.Point(291, 118);
      this.chk_BundleReverseMode.Name = "chk_BundleReverseMode";
      this.chk_BundleReverseMode.ReadOnly = true;
      this.chk_BundleReverseMode.Size = new System.Drawing.Size(229, 70);
      this.chk_BundleReverseMode.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_BundleReverseMode.TabIndex = 42;
      this.chk_BundleReverseMode.TabStop = false;
      this.chk_BundleReverseMode.Text = "Reverse Mode";
      this.chk_BundleReverseMode.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_BundleReverseMode.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_BundleReverseMode.TranslateText = true;
      // 
      // ttl_Multistrater
      // 
      this.ttl_Multistrater.BackColor = System.Drawing.Color.Black;
      this.ttl_Multistrater.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_Multistrater.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_Multistrater.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_Multistrater.Location = new System.Drawing.Point(282, 16);
      this.ttl_Multistrater.Name = "ttl_Multistrater";
      this.ttl_Multistrater.Size = new System.Drawing.Size(240, 172);
      this.ttl_Multistrater.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_Multistrater.TabIndex = 167;
      this.ttl_Multistrater.TabStop = false;
      this.ttl_Multistrater.Text = "MULTISTRATER";
      this.ttl_Multistrater.TranslateText = true;
      // 
      // pnl_WET
      // 
      this.pnl_WET.Controls.Add(this.chk_EnableInclinateLastBelt);
      this.pnl_WET.Controls.Add(this.chk_BundleReverseMode);
      this.pnl_WET.Controls.Add(this.ttl_Multistrater);
      this.pnl_WET.Controls.Add(this.tnud_WeightFactor);
      this.pnl_WET.Controls.Add(this.chk_prePrint);
      this.pnl_WET.Controls.Add(this.tnud_Thickness);
      this.pnl_WET.Controls.Add(this.tnud_BundleLayersXStack);
      this.pnl_WET.Location = new System.Drawing.Point(350, 289);
      this.pnl_WET.Name = "pnl_WET";
      this.pnl_WET.Size = new System.Drawing.Size(851, 399);
      this.pnl_WET.TabIndex = 160;
      this.pnl_WET.Visible = false;
      // 
      // chk_EnableInclinateLastBelt
      // 
      this.chk_EnableInclinateLastBelt.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_EnableInclinateLastBelt.Checked = false;
      this.chk_EnableInclinateLastBelt.Enabled = false;
      this.chk_EnableInclinateLastBelt.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.Empty;
      this.chk_EnableInclinateLastBelt.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_EnableInclinateLastBelt.ImagePassword = null;
      this.chk_EnableInclinateLastBelt.Location = new System.Drawing.Point(5, 317);
      this.chk_EnableInclinateLastBelt.Name = "chk_EnableInclinateLastBelt";
      this.chk_EnableInclinateLastBelt.ReadOnly = true;
      this.chk_EnableInclinateLastBelt.Size = new System.Drawing.Size(230, 67);
      this.chk_EnableInclinateLastBelt.Style = SyncroC.Controls.CheckBoxStyles.Normal;
      this.chk_EnableInclinateLastBelt.TabIndex = 34;
      this.chk_EnableInclinateLastBelt.TabStop = false;
      this.chk_EnableInclinateLastBelt.Text = "Enable Last Belt Inclination";
      this.chk_EnableInclinateLastBelt.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_EnableInclinateLastBelt.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.chk_EnableInclinateLastBelt.TranslateText = true;
      this.chk_EnableInclinateLastBelt.Visible = false;
      // 
      // brk_details
      // 
      this.brk_details.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.brk_details.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.brk_details.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_details.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_details.Location = new System.Drawing.Point(316, 193);
      this.brk_details.Name = "brk_details";
      this.brk_details.Position = SyncroC.Controls.BracketPosition.Right;
      this.brk_details.ShowArrow = true;
      this.brk_details.Size = new System.Drawing.Size(885, 519);
      this.brk_details.TabIndex = 161;
      this.brk_details.Text = "DETAILS";
      this.brk_details.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_details.TranslateText = true;
      // 
      // gdv_Quality
      // 
      this.gdv_Quality.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Quality.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Quality.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Quality.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Quality.AutoGenerateColumns = false;
      this.gdv_Quality.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Quality.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.gdv_Quality.DataMember = "";
      this.gdv_Quality.DataSource = null;
      this.gdv_Quality.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_Quality.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Quality.ImagePassword = null;
      this.gdv_Quality.Location = new System.Drawing.Point(42, 222);
      this.gdv_Quality.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.gdv_Quality.MultiSelect = true;
      this.gdv_Quality.Name = "gdv_Quality";
      this.gdv_Quality.RowsHeight = 30;
      this.gdv_Quality.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Quality.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Quality.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Quality.Size = new System.Drawing.Size(269, 490);
      this.gdv_Quality.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Quality.TabIndex = 162;
      this.gdv_Quality.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Quality.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Quality.TranslateText = true;
      this.gdv_Quality.SelectionChanged += new System.EventHandler(this.gdv_Quality_SelectionChanged);
      this.gdv_Quality.Leave += new System.EventHandler(this.gdv_Quality_Leave);
      this.gdv_Quality.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Quality_CellFormatting);
      // 
      // graphLinesEditor
      // 
      this.graphLinesEditor.BackColor = System.Drawing.Color.WhiteSmoke;
      this.graphLinesEditor.Location = new System.Drawing.Point(240, 40);
      this.graphLinesEditor.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
      this.graphLinesEditor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.graphLinesEditor.MySuspendLayout = false;
      this.graphLinesEditor.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.WrapArmGraphLinesEditor;
      this.graphLinesEditor.Name = "graphLinesEditor";
      this.graphLinesEditor.ReadOnly = true;
      this.graphLinesEditor.Size = new System.Drawing.Size(597, 346);
      this.graphLinesEditor.TabIndex = 163;
      this.graphLinesEditor.Title = "Wrap Arm 11";
      // 
      // pnl_PRC
      // 
      this.pnl_PRC.AutoScroll = true;
      this.pnl_PRC.Controls.Add(this.pct_Lock);
      this.pnl_PRC.Controls.Add(this.grd_data);
      this.pnl_PRC.Controls.Add(this.lbl_typeOfQuery);
      this.pnl_PRC.Controls.Add(this.printGrid);
      this.pnl_PRC.Controls.Add(this.lbl_typeOfClassification);
      this.pnl_PRC.Controls.Add(this.pct_PRCoverride);
      this.pnl_PRC.Controls.Add(this.treeViewPRC);
      this.pnl_PRC.Controls.Add(this.graphLinesEditor);
      this.pnl_PRC.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.pnl_PRC.Location = new System.Drawing.Point(350, 288);
      this.pnl_PRC.Name = "pnl_PRC";
      this.pnl_PRC.Size = new System.Drawing.Size(851, 399);
      this.pnl_PRC.TabIndex = 164;
      this.pnl_PRC.Visible = false;
      // 
      // pct_Lock
      // 
      this.pct_Lock.Image = global::SyncroC.Properties.Resources.Lock_Blue;
      this.pct_Lock.Location = new System.Drawing.Point(784, 1);
      this.pct_Lock.Name = "pct_Lock";
      this.pct_Lock.Size = new System.Drawing.Size(52, 40);
      this.pct_Lock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_Lock.TabIndex = 517;
      this.pct_Lock.TabStop = false;
      this.pct_Lock.Visible = false;
      // 
      // grd_data
      // 
      this.grd_data.AllowUserToAddRows = false;
      this.grd_data.AllowUserToDeleteRows = false;
      this.grd_data.AllowUserToOrderColumns = true;
      this.grd_data.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.grd_data.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      this.grd_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd_data.DataBindings.Add(new System.Windows.Forms.Binding("SelectionMode", global::SyncroC.Properties.Settings.Default, "FullRowSelect", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.grd_data.Location = new System.Drawing.Point(240, 40);
      this.grd_data.MultiSelect = false;
      this.grd_data.Name = "grd_data";
      this.grd_data.ReadOnly = true;
      this.grd_data.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      this.grd_data.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
      this.grd_data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.grd_data.SelectionMode = global::SyncroC.Properties.Settings.Default.FullRowSelect;
      this.grd_data.Size = new System.Drawing.Size(604, 343);
      this.grd_data.TabIndex = 167;
      this.grd_data.Visible = false;
      this.grd_data.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grd_data_CellFormatting);
      this.grd_data.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.grd_data_DefaultValuesNeeded);
      this.grd_data.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grd_data_DataError);
      // 
      // lbl_typeOfQuery
      // 
      this.lbl_typeOfQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.lbl_typeOfQuery.BackcolorTesto = System.Drawing.Color.Transparent;
      this.lbl_typeOfQuery.Colore1 = System.Drawing.Color.White;
      this.lbl_typeOfQuery.Colore2 = System.Drawing.Color.White;
      this.lbl_typeOfQuery.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_typeOfQuery.ForeColor = System.Drawing.Color.White;
      this.lbl_typeOfQuery.Location = new System.Drawing.Point(476, 12);
      this.lbl_typeOfQuery.Name = "lbl_typeOfQuery";
      this.lbl_typeOfQuery.OutLineText = true;
      this.lbl_typeOfQuery.OutLineTextcolore = System.Drawing.Color.Black;
      this.lbl_typeOfQuery.Size = new System.Drawing.Size(214, 25);
      this.lbl_typeOfQuery.TabIndex = 171;
      this.lbl_typeOfQuery.Testo = "AB VeryHeavy Kw-Kw";
      this.lbl_typeOfQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_typeOfQuery.TranslateText = false;
      this.lbl_typeOfQuery.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_typeOfQuery_Paint);
      // 
      // printGrid
      // 
      this.printGrid.BackgroundImage = global::SyncroC.Properties.Resources.PrinterIcon;
      this.printGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.printGrid.Cursor = System.Windows.Forms.Cursors.Hand;
      this.printGrid.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.printGrid.Location = new System.Drawing.Point(699, 11);
      this.printGrid.Name = "printGrid";
      this.printGrid.Size = new System.Drawing.Size(75, 23);
      this.printGrid.TabIndex = 170;
      this.printGrid.UseVisualStyleBackColor = true;
      this.printGrid.Click += new System.EventHandler(this.printGrid_Click);
      // 
      // lbl_typeOfClassification
      // 
      this.lbl_typeOfClassification.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.lbl_typeOfClassification.BackcolorTesto = System.Drawing.Color.Transparent;
      this.lbl_typeOfClassification.Colore1 = System.Drawing.Color.White;
      this.lbl_typeOfClassification.Colore2 = System.Drawing.Color.White;
      this.lbl_typeOfClassification.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_typeOfClassification.ForeColor = System.Drawing.Color.White;
      this.lbl_typeOfClassification.Location = new System.Drawing.Point(256, 12);
      this.lbl_typeOfClassification.Name = "lbl_typeOfClassification";
      this.lbl_typeOfClassification.OutLineText = true;
      this.lbl_typeOfClassification.OutLineTextcolore = System.Drawing.Color.Black;
      this.lbl_typeOfClassification.Size = new System.Drawing.Size(214, 25);
      this.lbl_typeOfClassification.TabIndex = 169;
      this.lbl_typeOfClassification.Testo = "AB VeryHeavy Kw-Kw";
      this.lbl_typeOfClassification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.lbl_typeOfClassification.TranslateText = false;
      this.lbl_typeOfClassification.Paint += new System.Windows.Forms.PaintEventHandler(this.lbl_typeOfClassification_Paint);
      // 
      // pct_PRCoverride
      // 
      this.pct_PRCoverride.Cursor = System.Windows.Forms.Cursors.Hand;
      this.pct_PRCoverride.Image = global::SyncroC.Properties.Resources.ReturnToDefault;
      this.pct_PRCoverride.Location = new System.Drawing.Point(784, 4);
      this.pct_PRCoverride.Name = "pct_PRCoverride";
      this.pct_PRCoverride.Size = new System.Drawing.Size(55, 35);
      this.pct_PRCoverride.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct_PRCoverride.TabIndex = 166;
      this.pct_PRCoverride.TabStop = false;
      this.pct_PRCoverride.Visible = false;
      this.pct_PRCoverride.Click += new System.EventHandler(this.pct_PRCoverride_Click);
      // 
      // treeViewPRC
      // 
      this.treeViewPRC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.treeViewPRC.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.treeViewPRC.HideSelection = false;
      this.treeViewPRC.Location = new System.Drawing.Point(5, 12);
      this.treeViewPRC.Name = "treeViewPRC";
      treeNode1.Name = "Node2";
      treeNode1.Text = "DB";
      treeNode2.Name = "Node0_1";
      treeNode2.Text = "TE1";
      treeNode3.Name = "Node0_2";
      treeNode3.Text = "TE11";
      treeNode4.Name = "Node0";
      treeNode4.Text = "TE21";
      treeNode5.Name = "Node1";
      treeNode5.Text = "TE20";
      treeNode6.Name = "Node3";
      treeNode6.Text = "PHT";
      treeNode7.Name = "Node5";
      treeNode7.Text = "GG1";
      treeNode8.Name = "Node6";
      treeNode8.Text = "GG2";
      treeNode9.Name = "Node4";
      treeNode9.Text = "GU";
      treeNode10.Name = "Node8";
      treeNode10.Text = "TS1";
      treeNode11.Name = "Node7";
      treeNode11.Text = "SPL1";
      treeNode12.Name = "Node6";
      treeNode12.Text = "SF1";
      treeNode13.Name = "Node7";
      treeNode13.Text = "SPL2";
      treeNode14.Name = "Node9";
      treeNode14.Text = "SPL3";
      this.treeViewPRC.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6,
            treeNode9,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14});
      this.treeViewPRC.Size = new System.Drawing.Size(229, 371);
      this.treeViewPRC.TabIndex = 164;
      this.treeViewPRC.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewPRC_AfterSelect);
      this.treeViewPRC.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewPRC_BeforeSelect);
      // 
      // lbl_filter
      // 
      this.lbl_filter.BackcolorTesto = System.Drawing.Color.Transparent;
      this.lbl_filter.BackgroundImage = global::SyncroC.Properties.Resources.filter;
      this.lbl_filter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.lbl_filter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lbl_filter.Colore1 = System.Drawing.Color.Red;
      this.lbl_filter.Colore2 = System.Drawing.Color.Red;
      this.lbl_filter.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lbl_filter.Font = new System.Drawing.Font("Arial Unicode MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_filter.Location = new System.Drawing.Point(234, 171);
      this.lbl_filter.Name = "lbl_filter";
      this.lbl_filter.OutLineText = true;
      this.lbl_filter.OutLineTextcolore = System.Drawing.Color.Black;
      this.lbl_filter.Size = new System.Drawing.Size(75, 45);
      this.lbl_filter.TabIndex = 165;
      this.lbl_filter.Testo = "!";
      this.lbl_filter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.lbl_filter.TranslateText = false;
      this.lbl_filter.Visible = false;
      this.lbl_filter.Click += new System.EventHandler(this.lbl_filter_Click);
      // 
      // Frm_Quality
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.lbl_filter);
      this.Controls.Add(this.gdv_Quality);
      this.Controls.Add(this.brk_details);
      this.Controls.Add(this.ttb_Text);
      this.Controls.Add(this.ttb_QualityID);
      this.Controls.Add(this.ttl_QualityList);
      this.Controls.Add(this.tlst_FluteID);
      this.Controls.Add(this.pnl_DRY);
      this.Controls.Add(this.pnl_WET);
      this.Controls.Add(this.pnl_PRC);
      this.Name = "Frm_Quality";
      this.Tag = "";
      this.Text = "Frm_Quality";
      this.Load += new System.EventHandler(this.Frm_Quality_Load);
      this.Activated += new System.EventHandler(this.Frm_Quality_Activated);
      this.Controls.SetChildIndex(this.pnl_PRC, 0);
      this.Controls.SetChildIndex(this.pnl_WET, 0);
      this.Controls.SetChildIndex(this.pnl_DRY, 0);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.btn_Group5, 0);
      this.Controls.SetChildIndex(this.lbl_Date, 0);
      this.Controls.SetChildIndex(this.pct_Home_NY, 0);
      this.Controls.SetChildIndex(this.pct_Back_NY, 0);
      this.Controls.SetChildIndex(this.pct_Forward_NY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4, 0);
      this.Controls.SetChildIndex(this.tlst_FluteID, 0);
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
      this.Controls.SetChildIndex(this.btn_Group1, 0);
      this.Controls.SetChildIndex(this.btn_Group2, 0);
      this.Controls.SetChildIndex(this.btn_Group3, 0);
      this.Controls.SetChildIndex(this.lbl_Title_YN, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF3_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF5_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF2_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF1_YY, 0);
      this.Controls.SetChildIndex(this.lbl_CtrlF4_YY, 0);
      this.Controls.SetChildIndex(this.ttl_QualityList, 0);
      this.Controls.SetChildIndex(this.ttb_QualityID, 0);
      this.Controls.SetChildIndex(this.ttb_Text, 0);
      this.Controls.SetChildIndex(this.brk_details, 0);
      this.Controls.SetChildIndex(this.gdv_Quality, 0);
      this.Controls.SetChildIndex(this.lbl_filter, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.pnl_DRY.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct0)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct6)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct4)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct5)).EndInit();
      this.pnl_WET.ResumeLayout(false);
      this.pnl_PRC.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Lock)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.grd_data)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_PRCoverride)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    /// <summary>
    /// refresh all the data stored in a local databases showed on this page.
    /// to be called at the beginning and at every database change.
    /// </summary>
    public override void refreshData()
    {

      ////PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
      //dbm = DBManager.getDBManager();
      //this.stk1 = dbm.syncroConfig.StkTerminal400Level1Settings;
      //this.stk2 = dbm.syncroConfig.StkTerminal400Level2Settings;
      //this.stk3 = dbm.syncroConfig.StkTerminal400Level3Settings;

      //if (stk1.Enable || stk2.Enable || stk3.Enable)
      //{
      //    chk_EnableInclinateLastBelt.Visible = true;

      //    if ( SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastMedium() )
      //    {
      //        chk_EnableInclinateLastBelt.BackColor = System.Drawing.Color.WhiteSmoke;
      //        chk_EnableInclinateLastBelt.Enabled = true;
      //    }
      //    else
      //    {
      //        chk_EnableInclinateLastBelt.BackColor = System.Drawing.Color.Gainsboro;
      //        chk_EnableInclinateLastBelt.Enabled = false;
      //    }		    
      //}
      //else
      //{
      //    chk_EnableInclinateLastBelt.Visible = false;
      //}

      //Flute
      //DataTable fluteTable = this.flute.flute.Tables[Flute.FLUTE_LIST];
      DataTable fluteTable = DBManager.getDBManager().flute.getTable();
      string[] fluteStrings = new string[fluteTable.Rows.Count];

      for (int i = 0; i < fluteTable.Rows.Count; i++)
        fluteStrings[i] = fluteTable.Rows[i][Flute.FLUTE_ID].ToString();

      tlst_FluteID.ClearItems();
      tlst_FluteID.AddRangeItem(fluteStrings);

      //Papers
      //DataTable paperTable = this.paper.paper.Tables[Paper.PAPER_LIST];
      DataTable paperTable = DBManager.getDBManager().paper.getTable();
      string[] paperStrings = new string[paperTable.Rows.Count];

      for (int i = 0; i < paperTable.Rows.Count; i++)
        paperStrings[i] = createPaperIdentifier(paperTable.Rows[i][Paper.PAPER_ID].ToString(), paperTable.Rows[i][Paper.WEIGHT_FACTOR].ToString());

      cmb_Paper0.ClearItems();
      cmb_Paper0.AddRangeItem(paperStrings);

      cmb_Paper1.ClearItems();
      cmb_Paper1.AddRangeItem(paperStrings);

      cmb_Paper2.ClearItems();
      cmb_Paper2.AddRangeItem(paperStrings);

      cmb_Paper3.ClearItems();
      cmb_Paper3.AddRangeItem(paperStrings);

      cmb_Paper4.ClearItems();
      cmb_Paper4.AddRangeItem(paperStrings);

      cmb_Paper5.ClearItems();
      cmb_Paper5.AddRangeItem(paperStrings);

      cmb_Paper6.ClearItems();
      cmb_Paper6.AddRangeItem(paperStrings);
    }

    public void quality_RowChanged(object sender, DBChangedEventArgs args)
    {
      if (state.Equals(State.Read))
      {
        gdv_Quality_SelectionChanged(null, null);
      }
    }

    public void refreshPaper(object sender, DBChangedEventArgs args)
    {
      int separatorIndex = args.Key.IndexOf(Command.PARAMETER_SEPARATOR);
      if (separatorIndex == -1)
      {
        Logger.LogError(this, "refreshPaper - Invalid paper id, weigth factor");
        return;
      }
      string paperStringIdentifier = createPaperIdentifier(
        args.Key.Substring(0, separatorIndex), args.Key.Substring(separatorIndex + 1));
      switch (args.Changement)
      {
        case DBChangement.Delete:
          cmb_Paper0.RemoveItem(paperStringIdentifier);
          cmb_Paper1.RemoveItem(paperStringIdentifier);
          cmb_Paper2.RemoveItem(paperStringIdentifier);
          cmb_Paper3.RemoveItem(paperStringIdentifier);
          cmb_Paper4.RemoveItem(paperStringIdentifier);
          cmb_Paper5.RemoveItem(paperStringIdentifier);
          cmb_Paper6.RemoveItem(paperStringIdentifier);
          break;

        case DBChangement.Insert:
          cmb_Paper0.AddItem(paperStringIdentifier);
          cmb_Paper1.AddItem(paperStringIdentifier);
          cmb_Paper2.AddItem(paperStringIdentifier);
          cmb_Paper3.AddItem(paperStringIdentifier);
          cmb_Paper4.AddItem(paperStringIdentifier);
          cmb_Paper5.AddItem(paperStringIdentifier);
          cmb_Paper6.AddItem(paperStringIdentifier);
          break;

        case DBChangement.Modify:
          break;
      }
    }

    public void refreshFlute(object sender, DBChangedEventArgs args)
    {
      switch (args.Changement)
      {
        case DBChangement.Delete:
          tlst_FluteID.RemoveItem(args.Key);
          break;

        case DBChangement.Insert:
          tlst_FluteID.AddItem(args.Key);
          break;

        case DBChangement.Modify:
          break;
      }
    }

    private void readOnly(bool isReadOnly)
    {
      //TAB
      tlst_FluteID.TabStop = ttb_Text.TabStop =
        //wet data
      cmb_Paper0.TabStop = cmb_Paper1.TabStop = cmb_Paper2.TabStop = cmb_Paper3.TabStop = cmb_Paper4.TabStop = cmb_Paper5.TabStop = cmb_Paper6.TabStop =
      tnud_WeightFactor.TabStop =
      chk_prePrint.TabStop =
        //PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
      chk_EnableInclinateLastBelt.TabStop =

      //dry data
      tnud_ScoreGap.TabStop =
      tnud_TrimGrindingMultiplier.TabStop =
      tnud_GrindingDuration.TabStop =
      tnud_LubricationParameter.TabStop = tnud_LubricationTime.TabStop =
      tnud_GrindingParameter.TabStop =
      //  //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      //chk_InternalFlapDisabled.TabStop =
      tnud_Thickness.TabStop =
      tnud_BundleLayersXStack.TabStop =
      chk_BundleReverseMode.TabStop =
      tnud_ProductionSpeed.TabStop =
      tnud_AOCSpeed.TabStop = !isReadOnly;
      //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      //RFAZZI_FI12/05/2010_1.Bug Fix - L'abilitazione alla modifica dell'Internal Flap TW4 viene data solo in caso di password inserita
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      if (SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastLow())
      {
        chk_InternalFlapDisabled.TabStop = !isReadOnly;
        chk_InternalFlapDisabled.ImagePassword = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
        //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
        this.tnud_CruisingSpeed.TabStop = !isReadOnly;
        //RFAZZI_FI09/03/2011_1. Aggiunta password su campo "Target Speed". 
        this.tnud_CruisingSpeed.ImagePassword = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
      }
      else
      {
        chk_InternalFlapDisabled.TabStop = false;
        chk_InternalFlapDisabled.ImagePassword = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
        //RFAZZI_FI09/03/2011_1. Aggiunta password su campo "Target Speed". 
        this.tnud_CruisingSpeed.ImagePassword = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
        this.tnud_CruisingSpeed.TabStop=false;
      }

      //READONLY
      tlst_FluteID.ReadOnly = ttb_Text.ReadOnly =
        //wet data
      cmb_Paper0.ReadOnly = cmb_Paper1.ReadOnly = cmb_Paper2.ReadOnly = cmb_Paper3.ReadOnly = cmb_Paper4.ReadOnly = cmb_Paper5.ReadOnly = cmb_Paper6.ReadOnly =
      tnud_WeightFactor.ReadOnly =
      chk_prePrint.ReadOnly =
        //PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
      chk_EnableInclinateLastBelt.ReadOnly =
        //dry data
      tnud_ScoreGap.ReadOnly =
      tnud_TrimGrindingMultiplier.ReadOnly =
      tnud_GrindingDuration.ReadOnly =
      tnud_LubricationParameter.ReadOnly = tnud_LubricationTime.ReadOnly =
      tnud_GrindingParameter.ReadOnly =
      // //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      //chk_InternalFlapDisabled.ReadOnly =
      tnud_Thickness.ReadOnly =
      tnud_BundleLayersXStack.ReadOnly =
      chk_BundleReverseMode.ReadOnly =
      tnud_ProductionSpeed.ReadOnly =
      tnud_AOCSpeed.ReadOnly = isReadOnly;

      //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      //RFAZZI_FI12/05/2010_1.Bug Fix - L'abilitazione alla modifica dell'Internal Flap TW4 viene data solo in caso di password inserita
      //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
      if (SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastLow())
      {
        chk_InternalFlapDisabled.ReadOnly = isReadOnly;
        chk_InternalFlapDisabled.ImagePassword = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
        //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
        this.tnud_CruisingSpeed.ReadOnly = isReadOnly;
        //RFAZZI_FI09/03/2011_1. Aggiunta password su campo "Target Speed". 
        this.tnud_CruisingSpeed.ImagePassword = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];
      }
      else
      {
        chk_InternalFlapDisabled.ReadOnly = true;
        chk_InternalFlapDisabled.ImagePassword = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
        //RFAZZI_FI09/03/2011_1. Aggiunta password su campo "Target Speed". 
        this.tnud_CruisingSpeed.ImagePassword = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
        this.tnud_CruisingSpeed.ReadOnly = true;
      }

      
      this.PRC_readOnly(isReadOnly);
    }

    private void CleanErrorProvider()
    {
      ttb_QualityID.SetError(string.Empty);
      tnud_AOCSpeed.SetError(string.Empty);
      tnud_ProductionSpeed.SetError(string.Empty);
      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
      this.tnud_CruisingSpeed.SetError(string.Empty);
      tlst_FluteID.SetError(string.Empty);
      cmb_Paper0.SetError(string.Empty);
      cmb_Paper1.SetError(string.Empty);
      cmb_Paper2.SetError(string.Empty);
      cmb_Paper3.SetError(string.Empty);
      cmb_Paper4.SetError(string.Empty);
      cmb_Paper5.SetError(string.Empty);
      cmb_Paper6.SetError(string.Empty);
    }

    protected override void F5()
    {
    }


    /// <summary>
    /// CtrlF1 key behaviour: New Quality
    /// </summary>
    protected override void CtrlF1()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      SetPageStatus(State.Insert);
    }

    /// <summary>
    /// CtrlF2 key behaviour: Delete an existing record
    /// </summary>
    protected override void CtrlF2()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      if (this.gdv_Quality.Focused)
      {
       if (DBManager.getDBManager().quality.getCount() > 0)
        {
          Frm_Confirm conf = new Frm_Confirm();
          string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_36");
          if (gdv_Quality.SelectedRows.Count != 0
            && conf.ShowDialog(this, text).Equals(DialogResult.OK))
          {
            CommunicationManager cm = CommunicationManager.GetCommunicationManager();
            foreach (DataGridViewRow row in gdv_Quality.SelectedRows)
            {
              cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_QUALITY + "." + Command.DB_CMD_DEL + "(" + ((DataRowView)row.DataBoundItem).Row[Quality.QUALITY_ID] + ")");
            }
          }
        }
        SetPageStatus(State.Read);
      }
    }

    /// <summary>
    /// CtrlF3 key behaviour: Delete all DB quality
    /// </summary>
    protected override void CtrlF3()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      Frm_Confirm conf = new Frm_Confirm();
      string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_37");
      if (gdv_Quality.Rows.Count > 0 && state.Equals(State.Read) &&
        conf.ShowDialog(this, text).Equals(DialogResult.OK))
      {
        CommunicationManager.GetCommunicationManager().SendTcp(CommunicationManager.GetCommunicationManager().SYNCROS_DB_MANAGER, Command.DB_QUALITY + "." + Command.DB_CMD_CLR + "()");
      }
      SetPageStatus(State.Read);
    }

    /// <summary>
    /// CtrlF4 key behaviour: Save
    /// </summary>
    protected override void CtrlF4()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      if (ttb_QualityID.Value.Trim() == ""
        || ((cmb_Paper0.Visible) && (cmb_Paper0.Value == null)))
      {
        PageManager.getPageManager()._SetMessage("§SYCC_10", true);
        return;
      }

      if (tlst_FluteID.SelectedItem == null || tlst_FluteID.SelectedItem == string.Empty)
      {//flute id has not been selected
        PageManager.getPageManager()._SetMessage("§SYCC_31", true);
        return;
      }
      else
      {
        DataRow row = null;
        try
        {
          row = DBManager.getDBManager().flute.getRow(tlst_FluteID.SelectedItem);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "CtrlF4 - Flute \"" + tlst_FluteID.SelectedItem + "\" not found.", ex);
        }
        if (row != null)
        {//check for papers been selected
          int numberOfZonesUsed = getNumberOfZonesUsedByFlute(row);
          bool paperNotSelected = false;

          if (cmb_Paper0.Value.Trim() == string.Empty)
          {
            string msg = PageManager.getPageManager()._SetMessage("§SYCC_31", true);
            cmb_Paper0.SetError(msg);
            paperNotSelected = true;
          }
          if (cmb_Paper1.Value.Trim() == string.Empty)
          {
            string msg = PageManager.getPageManager()._SetMessage("§SYCC_31", true);
            cmb_Paper1.SetError(msg);
            paperNotSelected = true;
          }
          if (cmb_Paper2.Value.Trim() == string.Empty)
          {
            string msg = PageManager.getPageManager()._SetMessage("§SYCC_31", true);
            cmb_Paper2.SetError(msg);
            paperNotSelected = true;
          }
          if (numberOfZonesUsed >= 2)
          {
            if (cmb_Paper3.Value.Trim() == string.Empty)
            {
              string msg = PageManager.getPageManager()._SetMessage("§SYCC_31", true);
              cmb_Paper3.SetError(msg);
              paperNotSelected = true;
            }
            if (cmb_Paper4.Value.Trim() == string.Empty)
            {
              string msg = PageManager.getPageManager()._SetMessage("§SYCC_31", true);
              cmb_Paper4.SetError(msg);
              paperNotSelected = true;
            }
            if (numberOfZonesUsed == 3)
            {
              if (cmb_Paper5.Value.Trim() == string.Empty)
              {
                string msg = PageManager.getPageManager()._SetMessage("§SYCC_31", true);
                cmb_Paper5.SetError(msg);
                paperNotSelected = true;
              }
              if (cmb_Paper6.Value.Trim() == string.Empty)
              {
                string msg = PageManager.getPageManager()._SetMessage("§SYCC_31", true);
                cmb_Paper6.SetError(msg);
                paperNotSelected = true;
              }
            }
          }
          if (paperNotSelected)
            return;
        }
      }

      if (ProductionSpeed_Warning ||
        AOCSpeed_Warning)
      {
        //warning present, save only after confirmation
        PopUp.Frm_Confirm conf = new PopUp.Frm_Confirm();
        string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_86");//Unusual values, save anyway?
        DialogResult res = conf.ShowDialog(this, text);
        if ((res == DialogResult.Yes) || (res == DialogResult.OK))
        {
          saveData();
          this.Escape();
        }
      }
      else
      {
        saveData();
        this.Escape();
      }
    }

    private void saveData()
    {
      //Questo va fatto sempre prima di mandare il comando al DBMS
      this.PRC_saveData();

      string toSend = Command.DB_QUALITY + ".";
      if (state == State.Modify)
      {
        toSend += Command.DB_CMD_MOD;
        PopUp.Frm_Confirm conf = new Frm_Confirm();
        string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_65");
        DialogResult res = conf.ShowDialog(this, text);
        if ((res == DialogResult.Yes) || (res == DialogResult.OK))
          toSend += "WithSetup";
      }
      else
        toSend += Command.DB_CMD_ADD;
      toSend += "(";
      if ((state == State.Modify) || (state == State.Insert))
      {
        string paper0 = "---";
        string paper1 = "---";
        string paper2 = "---";
        string paper3 = "---";
        string paper4 = "---";
        string paper5 = "---";
        string paper6 = "---";
        string weigthFactor0 = "0";
        string weigthFactor1 = "0";
        string weigthFactor2 = "0";
        string weigthFactor3 = "0";
        string weigthFactor4 = "0";
        string weigthFactor5 = "0";
        string weigthFactor6 = "0";

        if ((cmb_Paper0.Value != null) && (cmb_Paper0.Value.Trim() != string.Empty))
        {
          paper0 = getIDFromPaperIdentifier(cmb_Paper0.Value);
          weigthFactor0 = getWEIGTHFACTORFromPaperIdentifier(cmb_Paper0.Value);
        }
        if ((cmb_Paper1.Value != null) && (cmb_Paper1.Value.Trim() != string.Empty))
        {
          paper1 = getIDFromPaperIdentifier(cmb_Paper1.Value);
          weigthFactor1 = getWEIGTHFACTORFromPaperIdentifier(cmb_Paper1.Value);
        }
        if ((cmb_Paper2.Value != null) && (cmb_Paper2.Value.Trim() != string.Empty))
        {
          paper2 = getIDFromPaperIdentifier(cmb_Paper2.Value);
          weigthFactor2 = getWEIGTHFACTORFromPaperIdentifier(cmb_Paper2.Value);
        }
        if ((cmb_Paper3.Value != null) && (cmb_Paper3.Value.Trim() != string.Empty))
        {
          paper3 = getIDFromPaperIdentifier(cmb_Paper3.Value);
          weigthFactor3 = getWEIGTHFACTORFromPaperIdentifier(cmb_Paper3.Value);
        }
        if ((cmb_Paper4.Value != null) && (cmb_Paper4.Value.Trim() != string.Empty))
        {
          paper4 = getIDFromPaperIdentifier(cmb_Paper4.Value);
          weigthFactor4 = getWEIGTHFACTORFromPaperIdentifier(cmb_Paper4.Value);
        }
        if ((cmb_Paper5.Value != null) && (cmb_Paper5.Value.Trim() != string.Empty))
        {
          paper5 = getIDFromPaperIdentifier(cmb_Paper5.Value);
          weigthFactor5 = getWEIGTHFACTORFromPaperIdentifier(cmb_Paper5.Value);
        }
        if ((cmb_Paper6.Value != null) && (cmb_Paper6.Value.Trim() != string.Empty))
        {
          paper6 = getIDFromPaperIdentifier(cmb_Paper6.Value);
          weigthFactor6 = getWEIGTHFACTORFromPaperIdentifier(cmb_Paper6.Value);
        }

        CommunicationManager cm = CommunicationManager.GetCommunicationManager();
        cm.SendTcp(cm.SYNCROS_DB_MANAGER,
          toSend +
          ttb_QualityID.Value + Command.PARAMETER_SEPARATOR +
          ttb_Text.Value + Command.PARAMETER_SEPARATOR +
          tlst_FluteID.Value + Command.PARAMETER_SEPARATOR +
          //DRY DATA
          tnud_ScoreGap.Value + Command.PARAMETER_SEPARATOR +
          tnud_GrindingParameter.Value + Command.PARAMETER_SEPARATOR +
          tnud_LubricationParameter.Value + Command.PARAMETER_SEPARATOR +
          tnud_TrimGrindingMultiplier.Value + Command.PARAMETER_SEPARATOR +
          tnud_GrindingDuration.Value + Command.PARAMETER_SEPARATOR +
          tnud_LubricationTime.Value + Command.PARAMETER_SEPARATOR +
          tnud_ProductionSpeed.Value + Command.PARAMETER_SEPARATOR +
          tnud_AOCSpeed.Value + Command.PARAMETER_SEPARATOR +
          tnud_Thickness.Value + Command.PARAMETER_SEPARATOR +
          //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
          this.tnud_CruisingSpeed.Value + Command.PARAMETER_SEPARATOR + //spare (ex sheets X stack)
          tnud_BundleLayersXStack.Value + Command.PARAMETER_SEPARATOR +
          "0" + Command.PARAMETER_SEPARATOR + //spare (ex sheets X bundle)
          chk_BundleReverseMode.Checked + Command.PARAMETER_SEPARATOR +
          //WET DATA
          paper0 + Command.PARAMETER_SEPARATOR +
          weigthFactor0 + Command.PARAMETER_SEPARATOR +
          paper1 + Command.PARAMETER_SEPARATOR +
          weigthFactor1 + Command.PARAMETER_SEPARATOR +
          paper2 + Command.PARAMETER_SEPARATOR +
          weigthFactor2 + Command.PARAMETER_SEPARATOR +
          paper3 + Command.PARAMETER_SEPARATOR +
          weigthFactor3 + Command.PARAMETER_SEPARATOR +
          paper4 + Command.PARAMETER_SEPARATOR +
          weigthFactor4 + Command.PARAMETER_SEPARATOR +
          paper5 + Command.PARAMETER_SEPARATOR +
          weigthFactor5 + Command.PARAMETER_SEPARATOR +
          paper6 + Command.PARAMETER_SEPARATOR +
          weigthFactor6 + Command.PARAMETER_SEPARATOR +
          tnud_WeightFactor.Value + Command.PARAMETER_SEPARATOR +
          chk_prePrint.Checked + Command.PARAMETER_SEPARATOR +
          //PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
          //"SPARE" + Command.PARAMETER_SEPARATOR +
          chk_EnableInclinateLastBelt.Checked + Command.PARAMETER_SEPARATOR +
          //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
          //"SPARE" + Command.PARAMETER_SEPARATOR +
          (chk_InternalFlapDisabled.Checked ? "1" : "0") + Command.PARAMETER_SEPARATOR +
          "SPARE" + Command.PARAMETER_SEPARATOR +
          "SPARE" + Command.PARAMETER_SEPARATOR +
          "SPARE" + Command.PARAMETER_SEPARATOR +
          "SPARE" + Command.PARAMETER_SEPARATOR +
          "SPARE" + Command.PARAMETER_SEPARATOR +
          "SPARE" + Command.PARAMETER_SEPARATOR +
          "SPARE" + Command.PARAMETER_SEPARATOR +
          "SPARE" + Command.PARAMETER_SEPARATOR +
          "SPARE" + Command.PARAMETER_SEPARATOR +
          "SPARE" + Command.PARAMETER_SEPARATOR +
          "SPARE" +
          ")");
      }
    }

    /// <summary>
    /// CtrlF5 key behaviour: modify an existing Quality
    /// </summary>
    protected override void CtrlF5()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      if (gdv_Quality.Rows.Count <= 0)
        return;

      if (state == State.Insert)
        return;

      SetPageStatus(State.Modify);

      this.PRC_gdv_Quality_SelectionChanged();
    }

    /// <summary>
    /// Escape key behaviour: Abort the operation
    /// </summary>
    protected override void Escape()
    {
      this.PRC_Escape();

      SetPageStatus(State.Read);
    }

    /// <summary>
    /// Switch to Dry data
    /// </summary>
    protected override void F10()
    {
      pnl_DRY.Visible = true;
      pnl_WET.Visible = false;
      pnl_PRC.Visible = false;
      tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabA.Cursor = Cursors.Arrow;
      this.tab_TabB.Cursor = Cursors.Hand;
      this.tab_TabC.Cursor = Cursors.Hand;
    }

    /// <summary>
    /// Switch to Wet data
    /// </summary>
    protected override void F11()
    {
      pnl_DRY.Visible = false;
      pnl_WET.Visible = true;
      pnl_PRC.Visible = false;
      tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabA.Cursor = Cursors.Hand;
      this.tab_TabB.Cursor = Cursors.Arrow;
      this.tab_TabC.Cursor = Cursors.Hand;
    }

    /// <summary>
    /// Switch to PRC data
    /// </summary>
    protected override void F12()
    {
      pnl_DRY.Visible = false;
      pnl_WET.Visible = false;
      pnl_PRC.Visible = true;
      tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      tab_TabC.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      this.tab_TabA.Cursor = Cursors.Hand;
      this.tab_TabB.Cursor = Cursors.Hand;
      this.tab_TabC.Cursor = Cursors.Arrow;
      this.treeViewPRC.Select();
    }

    private void gdv_Quality_SelectionChanged(object sender, EventArgs e)
    {
      if (gdv_Quality.Rows.Count > 0 && gdv_Quality.SelectedRows.Count == 1)
      {
        DataRow row = ((DataRowView)gdv_Quality.SelectedRows[0].DataBoundItem).Row;
        if (row != null && row.RowState != DataRowState.Detached)
        {
          try
          {
            //header
            ttb_QualityID.Value = row[Quality.QUALITY_ID].ToString();
            ttb_Text.Value = row[Quality.QUALITY_TEXT].ToString();
            tlst_FluteID.SelectedItem = row[Quality.FLUTE_ID].ToString();
            //DRY DATA
            tnud_ScoreGap.Value = row[Quality.SCOREGAP_UDdmm].ToString();
            tnud_GrindingParameter.Value = row[Quality.GRINDING_PARAMETER_UDm].ToString();



            //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
            DBManager dbm;
            dbm = DBManager.getDBManager();
            SyncroC.DataBase.SyncroConfig.SSTwin22_60Settings twin400;
            SyncroC.DataBase.LineConfig.SlitterScorer ss;
            twin400 = dbm.syncroConfig.Twin22_60Setting;
            ss = dbm.lineConfig.SlitterScorerConfig;
            //agrassi_FI23-06-2011_1
            //if (twin400.Enable && ss.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication )
            //{
              tnud_LubricationParameter.Value = row[Quality.LUBRICATION_PARAMETER_UDm].ToString();
              tnud_LubricationTime.Value = row[Quality.LUBRICATION_DURATION_UTms].ToString();
            //}

            tnud_TrimGrindingMultiplier.Value = row[Quality.TRIM_GRINDINGS_MULTIPLIER].ToString();
            tnud_GrindingDuration.Value = row[Quality.GRINDING_DISTANCE_UDm].ToString();
            tnud_ProductionSpeed.Value = row[Quality.PRODUCTION_SPEED_UVm_m].ToString();
            //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
            this.tnud_CruisingSpeed.Value = row[Quality.CRUISING_SPEED_UVm_m].ToString();
            tnud_AOCSpeed.Value = row[Quality.AOC_SPEED_UVm_m].ToString();
            tnud_Thickness.Value = row[Quality.THICKNESS_UDdmm].ToString();
            tnud_BundleLayersXStack.Value = row[Quality.BUNDLE_LAYERS_PER_STACK].ToString();
            chk_BundleReverseMode.Checked = Convert.ToBoolean(row[Quality.BUNDLE_REVERSE_MODE].ToString());
            //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
            chk_InternalFlapDisabled.Checked = row[Quality.TWIN400_INTERNAL_FLAP_DISABLED].ToString() == "0" ? false : true;
            //WET DATA
            DataRow fluteRow = null;
            try
            {
              fluteRow = DBManager.getDBManager().flute.getRow(tlst_FluteID.SelectedItem);
            }
            catch (Exception ex)
            {
              Logger.LogError(this, "gdv_Quality_SelectionChanged - Flute \"" + tlst_FluteID.SelectedItem + "\" not found.", ex);
            }
            int numberOfZonesUsed = 0;
            if (fluteRow != null)
              numberOfZonesUsed = getNumberOfZonesUsedByFlute(fluteRow);
            this.cmb_Paper3.Visible = pct3.Visible = this.cmb_Paper4.Visible = pct4.Visible = (numberOfZonesUsed >= 2);
            this.cmb_Paper5.Visible = pct5.Visible = this.cmb_Paper6.Visible = pct6.Visible = (numberOfZonesUsed == 3);

            cmb_Paper0.SelectedItem = createPaperIdentifier(row[Quality.PAPER_ID_0].ToString(), row[Quality.WEIGHT_FACTOR_0].ToString());
            cmb_Paper1.SelectedItem = createPaperIdentifier(row[Quality.PAPER_ID_1].ToString(), row[Quality.WEIGHT_FACTOR_1].ToString());
            cmb_Paper2.SelectedItem = createPaperIdentifier(row[Quality.PAPER_ID_2].ToString(), row[Quality.WEIGHT_FACTOR_2].ToString());
            cmb_Paper3.SelectedItem = createPaperIdentifier(row[Quality.PAPER_ID_3].ToString(), row[Quality.WEIGHT_FACTOR_3].ToString());
            cmb_Paper4.SelectedItem = createPaperIdentifier(row[Quality.PAPER_ID_4].ToString(), row[Quality.WEIGHT_FACTOR_4].ToString());
            cmb_Paper5.SelectedItem = createPaperIdentifier(row[Quality.PAPER_ID_5].ToString(), row[Quality.WEIGHT_FACTOR_5].ToString());
            cmb_Paper6.SelectedItem = createPaperIdentifier(row[Quality.PAPER_ID_6].ToString(), row[Quality.WEIGHT_FACTOR_6].ToString());

            tnud_WeightFactor.Value = row[Quality.WEIGHT_FACTOR].ToString();
            chk_prePrint.Checked = Convert.ToBoolean(row[Quality.PREPRINT].ToString());
            //PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
            chk_EnableInclinateLastBelt.Checked = Convert.ToBoolean(row[Quality.INCLINATELASTBELT].ToString());

            this.PRC_gdv_Quality_SelectionChanged();
          }
          catch (RowNotInTableException ex)
          {
            Logger.LogError(this, "gdv_Quality_SelectionChanged - the rowState is not Detached (it is \"" + row.RowState.ToString() + "\" but the RowNotInTableException has been thrown anyway....", ex);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Quality_SelectionChanged - ", ex);
          }
        }
        else
          CleanQualityDetails();
      }
      else
        CleanQualityDetails();
    }

    private void CleanQualityDetails()
    {
      ttb_QualityID.Value = "";
      ttb_Text.Value = "";
      tlst_FluteID.UnSelectItem();
      //DRY DATA
      tnud_TrimGrindingMultiplier.Value = "0";
      tnud_GrindingDuration.Value = "0";

      //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      DBManager dbm;
      dbm = DBManager.getDBManager();
      SyncroC.DataBase.SyncroConfig.SSTwin22_60Settings twin400;
      SyncroC.DataBase.LineConfig.SlitterScorer ss;
      twin400 = dbm.syncroConfig.Twin22_60Setting;
      ss = dbm.lineConfig.SlitterScorerConfig;
      //agrassi_FI23-06-2011_1
      //if (twin400.Enable && ss.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication)
      //{
        tnud_LubricationParameter.Value = "0";
        tnud_LubricationTime.Value = "0";
      //}
      tnud_GrindingParameter.Value = "0";
      tnud_Thickness.Value = "0";
      tnud_BundleLayersXStack.Value = "0";
      chk_BundleReverseMode.Checked = false;
      tnud_ProductionSpeed.Value = "0";
      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
      this.tnud_CruisingSpeed.Value = "0";
      tnud_AOCSpeed.Value = "0";
      //agrassi_FI26-08-08
      //tlst_FluteID.SelectedIndex = 0;
      //WET DATA
      tnud_WeightFactor.Value = "0";
      chk_prePrint.Checked = false;
      //PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
      chk_EnableInclinateLastBelt.Checked = false;
      //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      chk_InternalFlapDisabled.Checked = false;
      cmb_Paper0.UnSelectItem();
      cmb_Paper1.UnSelectItem();
      cmb_Paper2.UnSelectItem();
      cmb_Paper3.UnSelectItem();
      cmb_Paper4.UnSelectItem();
      cmb_Paper5.UnSelectItem();
      cmb_Paper6.UnSelectItem();
      tnud_ScoreGap.Value = "0";
    }

    private void tlst_FluteID_SelectedItemChanged(object sender, EventArgs e)
    {
      if (this.state == State.Read)
        return;
      if (tlst_FluteID.SelectedItem != null && tlst_FluteID.SelectedItem != string.Empty)
      {
        DataRow row = null;
        try
        {
          row = DBManager.getDBManager().flute.getRow(tlst_FluteID.SelectedItem);
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "FillFluteDefault - Flute \"" + tlst_FluteID.SelectedItem + "\" not found.", ex);
        }
        int numberOfZonesUsed = 0;
        if (row != null)
        {
          tlst_FluteID.SetError(string.Empty);
          this.tnud_Thickness.Value = row[Flute.THICKNESS_UDdmm].ToString();
          this.tnud_GrindingParameter.Value = row[Flute.GRINDING_PARAMETER_UDm].ToString();
          this.tnud_GrindingDuration.Value = row[Flute.GRINDING_DISTANCE_UDm].ToString();
          //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
          DBManager dbm;
          dbm = DBManager.getDBManager();
          SyncroC.DataBase.SyncroConfig.SSTwin22_60Settings twin400;
          SyncroC.DataBase.LineConfig.SlitterScorer ss;
          twin400 = dbm.syncroConfig.Twin22_60Setting;
          ss = dbm.lineConfig.SlitterScorerConfig;
          //agrassi_FI23-06-2011_1
          //if (twin400.Enable && ss.SlitterScorerHeader.PositioningOptions.Slits.EnableLubrication)
          //{
            this.tnud_LubricationParameter.Value = row[Flute.LUBRICATION_PARAMETER_UDm].ToString();
            this.tnud_LubricationTime.Value = row[Flute.LUBRICATION_DURATION_UTms].ToString();
          //}
          this.tnud_ScoreGap.Value = row[Flute.SCOREGAP_UDdmm].ToString();
          this.tnud_TrimGrindingMultiplier.Value = row[Flute.TRIM_GRINDINGS_MULTIPLIER].ToString();
          this.tnud_ProductionSpeed.Value = row[Flute.PRODUCTION_SPEED_UVm_m].ToString();
          //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
          this.tnud_CruisingSpeed.Value = row[Flute.CRUISING_SPEED_UVm_m].ToString();
          this.tnud_AOCSpeed.Value = row[Flute.AOC_SPEED_UVm_m].ToString();
          //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
          //agrassi_FI15-01-09_2
          //this.chk_InternalFlapDisabled.Checked = Convert.ToBoolean(row[Flute.TWIN400_INTERNAL_FLAP_DISABLED].ToString());
          this.chk_InternalFlapDisabled.Checked = row[Flute.TWIN400_INTERNAL_FLAP_DISABLED].ToString() == "0" ? false : true;

          numberOfZonesUsed = getNumberOfZonesUsedByFlute(row);
        }
        this.cmb_Paper3.Visible = pct3.Visible = this.cmb_Paper4.Visible = pct4.Visible = (numberOfZonesUsed >= 2);
        this.cmb_Paper5.Visible = pct5.Visible = this.cmb_Paper6.Visible = pct6.Visible = (numberOfZonesUsed == 3);
      }
    }

    private int getNumberOfZonesUsedByFlute(DataRow row)
    {
      string sfsPositions = row[Flute.SFS_POSITION].ToString();
      int numberOfZonesUsed = 0;
      for (int i = 0; i < sfsPositions.Length; i++)
        if (!sfsPositions[i].Equals('-'))
          numberOfZonesUsed++;
      return numberOfZonesUsed;
    }

    private void ttb_QualityID_YN_Validating(object sender, CancelEventArgs e)
    {
      if (ttb_QualityID.Value.Trim() == "")
      {
        string text = PageManager.getPageManager()._SetMessage("§SYCC_02", true);
        ttb_QualityID.SetError(text);
      }
      else
        ttb_QualityID.SetError("");
    }

    private void cmb_Paper_Validating(object sender, CancelEventArgs e)
    {
      if (!state.Equals(State.Read))
      {
        TitledList cmb_Paper = (TitledList)sender;
        if ((cmb_Paper.Visible) && ((cmb_Paper.Value == null) || (cmb_Paper.Value.Trim() == string.Empty)))
        {
          string text = PageManager.getPageManager()._SetMessage("§SYCC_31", true);
          cmb_Paper.SetError(text);
        }
        else
          cmb_Paper.SetError("");
      }
    }

    private void gdv_Quality_Leave(object sender, EventArgs e)
    {
      if (state.Equals(State.Read))
      {
        if (!this.pnl_PRC.Visible)
          this.gdv_Quality.Focus();
      }
    }

    private void Frm_Quality_Activated(object sender, EventArgs e)
    {
      this.updateControlsEnable(); //PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400

      if (state.Equals(State.Read))
        this.gdv_Quality.Focus();
    }

    private void SetPageStatus(State newState)
    {
      if (!state.Equals(newState))
      {
        state = newState;
        switch (newState)
        {
          case State.Insert:
            CleanQualityDetails();
            foreach (DataGridViewRow row in gdv_Quality.Rows)
              row.Selected = false;
            ttb_QualityID.TabStop = true;
            ttb_QualityID.ReadOnly = false;
            readOnly(false);
            lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor = lbl_Status.BackColor;
            //PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
            chk_EnableInclinateLastBelt.Checked = true;

            gdv_Quality.Enabled = false;
            ttb_QualityID.Focus();
            break;

          case State.Modify:
            readOnly(false);
            lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
            gdv_Quality.Enabled = false;
            ttb_Text.Focus();
            break;

          case State.Read:
            ttb_QualityID.TabStop = false;
            ttb_QualityID.ReadOnly = true;
            readOnly(true);
            lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor = lbl_Title_YN.BackColor;
            gdv_Quality.Enabled = true;
            if (gdv_Quality.SelectedRows.Count == 0 && gdv_Quality.Rows.Count > 0)
              gdv_Quality.Rows[0].Selected = true;
            else
              gdv_Quality_SelectionChanged(null, null);
            gdv_Quality.Focus();
            CleanErrorProvider();
            break;
        }
      }
    }

    private void tnud_ProductionSpeed_Validating(object sender, CancelEventArgs e)
    {
      this.ProductionSpeed_Warning =
          (this.tnud_ProductionSpeed.getValueAsInt() < LocalValidationLimits.ProductionSpeed_WarningMinLimit_UVm_m);

      if (this.ProductionSpeed_Warning)
      {
        string text = PageManager.getPageManager()._SetMessage("§SYCC_85");//Unusual value
        this.tnud_ProductionSpeed.SetError(text);
      }
      else
        this.tnud_ProductionSpeed.SetError(string.Empty);
    }

    private void tnud_AOCSpeed_Validating(object sender, CancelEventArgs e)
    {
      AOCSpeed_Warning =
          (tnud_AOCSpeed.getValueAsInt() < LocalValidationLimits.AOCSpeed_WarningMinLimit_UVm_m);

      if (AOCSpeed_Warning)
      {
        string text = PageManager.getPageManager()._SetMessage("§SYCC_85");//Unusual value
        tnud_AOCSpeed.SetError(text);
      }
      else
        tnud_AOCSpeed.SetError(string.Empty);
    }

    private void Frm_Quality_Load(object sender, EventArgs e)
    {
      this.gdv_Quality_SelectionChanged(null, null);
    }

    private string createPaperIdentifier(string paperID, string paperWeigthFactor)
    {
      if (paperID == null || paperID.Trim().Equals(string.Empty)
        || paperWeigthFactor == null || paperWeigthFactor.Trim().Equals(string.Empty))
        return string.Empty;
      return paperID + WEIGTH_FACTOR_SEPARATOR + paperWeigthFactor;
    }

    private string getIDFromPaperIdentifier(string paperIdentifier)
    {
      int separatorIndex = paperIdentifier.IndexOf(WEIGTH_FACTOR_SEPARATOR);
      if (separatorIndex != -1)
        return paperIdentifier.Substring(0, separatorIndex);
      return string.Empty;
    }

    private string getWEIGTHFACTORFromPaperIdentifier(string paperIdentifier)
    {
      int separatorIndex = paperIdentifier.IndexOf(WEIGTH_FACTOR_SEPARATOR);
      if (separatorIndex != -1)
        return paperIdentifier.Substring(separatorIndex + WEIGTH_FACTOR_SEPARATOR.Length);
      return string.Empty;
    }

    private void gdv_Quality_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (SyncroC.Utility.Settings.getSettings().configuration.PRC)
      {
        //BACKCOLOR IF IS OVERRIDED
        if (e.ColumnIndex == 0)
        {
          if (SyncroC.DataBase.PRCManager.getPRCManager().getIsQualityOverride(e.Value.ToString().Trim()))
            e.CellStyle.BackColor = SyncroC.DataBase.PRCManager.COLOR_PRC_OVERRIDE;
          e.FormattingApplied = true;
        }
      }
    }

    #region PRC

    private void PRC_Escape()
    {
      if (this.listaModifiche != null)
      {
        if (this.listaModifiche.Count != 0)
        {
          this.listaModifiche.Clear();
        }
      }
    }

    private void PRC_readOnly(bool isReadOnly)
    {
      if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        return;

      //ABELLO_FI14/08/2010_1. Riviste le password
      //if (!Accessibility.getAccessibility().isLevelAtLeastLow())
      if (!Accessibility.getAccessibility().isLevelAtLeastMedium())
      {
        this.graphLinesEditor.ReadOnly = true;
        this.grd_data.AllowUserToAddRows = false;
        this.grd_data.AllowUserToDeleteRows = false;
        this.grd_data.ReadOnly = true;
      }
      else
      {
        this.graphLinesEditor.ReadOnly = isReadOnly;
        this.grd_data.AllowUserToAddRows = !isReadOnly;
        this.grd_data.AllowUserToDeleteRows = !isReadOnly;
        this.grd_data.ReadOnly = isReadOnly;
      }
    }

    private void setupOverrideNodeRecursive(TreeNode treeNode, System.Collections.Generic.List<string> listTableWhereIsQualityOverride)
    {
      if (listTableWhereIsQualityOverride.Contains(treeNode.Name))
        treeNode.BackColor = SyncroC.DataBase.PRCManager.COLOR_PRC_OVERRIDE;
      else
        treeNode.BackColor = Color.WhiteSmoke;

      foreach (TreeNode tn in treeNode.Nodes)
        this.setupOverrideNodeRecursive(tn, listTableWhereIsQualityOverride);
    }

    void PRC_gdv_Quality_SelectionChanged()
    {
      if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        return;

      System.Collections.Generic.List<string> listTableWhereIsQualityOverride = SyncroC.DataBase.PRCManager.getPRCManager().getComponentsWhereIsQualityOverride(this.ttb_QualityID.Value);

      foreach (TreeNode n in this.treeViewPRC.Nodes)
        this.setupOverrideNodeRecursive(n, listTableWhereIsQualityOverride);

      if (this.treeViewPRC.SelectedNode != null)
      {
        if (this.graphLinesEditor.Visible)
          this.PRC_fillData(this.treeViewPRC.SelectedNode.Name);
        if (this.grd_data.Visible)
          this.PRC_refreshGrid(this.ttb_QualityID.Value);
      }
    }

    private void PRC_refreshGrid(string qualityid)
    {
      if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        return;

      string myTableNameNoWarpLevel = this.treeViewPRC.SelectedNode.Name;
      string warpLevel = "";
      if (this.treeViewPRC.SelectedNode.Name.Contains("|"))
      {
        string[] a = this.treeViewPRC.SelectedNode.Name.Split('|');
        myTableNameNoWarpLevel = a[0];
        warpLevel = a[1];
      }

      if (this.gdv_Quality.SelectedRows.Count != 0)
        this.lbl_typeOfClassification.Testo = SyncroC.DataBase.PRCManager.getPRCManager().getFluteAndPaperWeightAndPaperClassification(myTableNameNoWarpLevel, this.gdv_Quality.SelectedRows[0].Cells["qualityid"].Value.ToString());

      bool isOverrideWarp = false;
      string resQueryWarp = "";

      this.myDataTable = SyncroC.DataBase.PRCManager.getPRCManager().getWarpQuality(
        myTableNameNoWarpLevel,
        this.gdv_Quality.SelectedRows[0].Cells["qualityid"].Value.ToString(),
        out isOverrideWarp,
        out resQueryWarp);

      this.grd_data.DataSource = this.myDataTable;
      if(this.grd_data.Columns.Contains("sequence"))
        this.grd_data.Sort(this.grd_data.Columns["sequence"], ListSortDirection.Ascending);

      if (isOverrideWarp)
      {
        //SI OVERRIDE
        //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password.

        //ABELLO_FI14/08/2010_1. Riviste le password
        //this.pct_Lock.Visible = !Accessibility.getAccessibility().isLevelAtLeastLow();
        this.pct_Lock.Visible = !Accessibility.getAccessibility().isLevelAtLeastMedium();

        this.pct_PRCoverride.Visible = (this.state != State.Read) && Accessibility.getAccessibility().isLevelAtLeastLow(); //é visibile solo in modifica
        this.pct_PRCoverride.BringToFront();
        this.lbl_typeOfQuery.BackColor = SyncroC.DataBase.PRCManager.COLOR_PRC_OVERRIDE;
      }
      else
      {
        //NO OVERRIDE
        //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 

        //ABELLO_FI14/08/2010_1. Riviste le password
        //pct_Lock.Visible = !Accessibility.getAccessibility().isLevelAtLeastLow();
        pct_Lock.Visible = !Accessibility.getAccessibility().isLevelAtLeastMedium();

        this.pct_PRCoverride.Visible = false;
        this.pct_PRCoverride.SendToBack();
        this.lbl_typeOfQuery.BackColor = this.lbl_typeOfClassification.BackColor;
      }

      this.lbl_typeOfQuery.Testo = resQueryWarp; 
    }

    private void PRC_fillData(string PRCControl)
    {
      this.PRC_fillData(false, PRCControl);
    }

    private void PRC_fillData(bool removeOverride, string PRCControl)
    {
      if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        return;

      if (this.gdv_Quality.SelectedRows.Count != 0)
        this.lbl_typeOfClassification.Testo = SyncroC.DataBase.PRCManager.getPRCManager().getFluteAndPaperWeightAndPaperClassification(PRCControl, this.gdv_Quality.SelectedRows[0].Cells["qualityid"].Value.ToString());

      System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>> appo = new System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>>();

      if ((this.listaModifiche != null) && (this.listaModifiche.ContainsKey(PRCControl)))
      {
        this.listaModifiche.TryGetValue(PRCControl, out appo);

        if (this.gdv_Quality.SelectedRows.Count != 0)
          this.lbl_typeOfQuery.Testo = this.gdv_Quality.SelectedRows[0].Cells["qualityid"].Value.ToString();
      }
      else
      {

        if (this.gdv_Quality.SelectedRows.Count == 0)
          return;

        bool isOverride = false;
        string resQuery = "";
        appo = SyncroC.DataBase.PRCManager.getPRCManager().getPRCQuality(PRCControl, this.gdv_Quality.SelectedRows[0].Cells["qualityid"].Value.ToString(), out isOverride, out resQuery);
        if(isOverride)
        {
          this.lbl_typeOfQuery.BackColor = SyncroC.DataBase.PRCManager.COLOR_PRC_OVERRIDE;
          this.pct_PRCoverride.Visible = (this.state != State.Read); //é visibile solo in modifica
          this.pct_PRCoverride.BringToFront();
        }
        else
        {
          this.lbl_typeOfQuery.BackColor = this.lbl_typeOfClassification.BackColor;
          this.pct_PRCoverride.Visible = false;
          this.pct_PRCoverride.SendToBack();
        }          

        this.lbl_typeOfQuery.Testo = resQuery;
      }

      //if (this.graphLinesEditor.ReadOnly)
      {
        this.graphLinesEditor.fillData(appo);
      }
      //else
      //{
      //  if (removeOverride)
      //    this.graphLinesEditor.fillData(appo);
      //}

      this.ultimoGrafico = appo;
    }

    private void treeViewPRC_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        return;

      this.graphLinesEditor.MySuspendLayout = true;

      if (e.Node.Name.StartsWith("wraparm"))
      {
        this.graphLinesEditor.Title = e.Node.Text;

        //ABELLO_FI09/05/2011_1 Aggiunta gestione del Process senza sensori di temperatura
        if(SyncroC.DataBase.PRCManager.getPRCManager().getTemperatureSensorPresentForWrapArm(e.Node.Name))
          this.graphLinesEditor.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.WrapArmGraphLinesEditor;
        else
          this.graphLinesEditor.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.WrapArmPositionGraphLinesEditor;

        this.graphLinesEditor.Minimum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, e.Node.Name);
        this.graphLinesEditor.Maximum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, e.Node.Name);

        this.PRC_fillData(e.Node.Name);
      }
      else if (e.Node.Name.StartsWith("steamshower"))
			{
				this.graphLinesEditor.Title = e.Node.Text;
				this.graphLinesEditor.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.SteamShower;

        this.graphLinesEditor.Minimum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, e.Node.Name);
        this.graphLinesEditor.Maximum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, e.Node.Name);

				this.PRC_fillData(e.Node.Name);
			}
      else if (e.Node.Name.StartsWith("sfmainsteam"))
      {
        this.graphLinesEditor.Title = e.Node.Text;
        this.graphLinesEditor.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.SteamPressure;

        this.graphLinesEditor.Minimum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, e.Node.Name);
        this.graphLinesEditor.Maximum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, e.Node.Name);

        this.PRC_fillData(e.Node.Name);
      }
      else if (e.Node.Name.StartsWith("gluegap"))
			{
				this.graphLinesEditor.Title = e.Node.Text;
				this.graphLinesEditor.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.GlueGapGraphLinesEditor;

        this.graphLinesEditor.Minimum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, e.Node.Name);
        this.graphLinesEditor.Maximum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, e.Node.Name);

				this.PRC_fillData(e.Node.Name);
			}
      else if (e.Node.Name.StartsWith("pressure_hot_section"))
			{
				this.graphLinesEditor.Title = e.Node.Text;
				this.graphLinesEditor.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.PressureHotSection;

        this.graphLinesEditor.Minimum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, e.Node.Name);
        this.graphLinesEditor.Maximum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, e.Node.Name);

				this.PRC_fillData(e.Node.Name);
			}
      else if (e.Node.Name.StartsWith("pressure_cold_section"))
			{
				this.graphLinesEditor.Title = e.Node.Text;
				this.graphLinesEditor.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.PressureColdSection;

        this.graphLinesEditor.Minimum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, e.Node.Name);
        this.graphLinesEditor.Maximum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, e.Node.Name);

				this.PRC_fillData(e.Node.Name);
			}
      else if (e.Node.Name.StartsWith("steam_group"))
			{
				this.graphLinesEditor.Title = e.Node.Text;
				this.graphLinesEditor.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.SteamPressure;

        this.graphLinesEditor.Minimum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, e.Node.Name);
        this.graphLinesEditor.Maximum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, e.Node.Name);

				this.PRC_fillData(e.Node.Name);
			}
      else if (e.Node.Name.StartsWith("papertension"))
			{
				this.graphLinesEditor.Title = e.Node.Text;
				this.graphLinesEditor.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.TensionGraphLinesEditor;

        this.graphLinesEditor.Minimum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, e.Node.Name);
        this.graphLinesEditor.Maximum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, e.Node.Name);

				this.PRC_fillData(e.Node.Name);
			}

      this.graphLinesEditor.MySuspendLayout = false;

      if (e.Node.Name.StartsWith("warp"))
      {
        this.grd_data.Visible = true;
        this.grd_data.BringToFront();

        if (gdv_Quality.Rows.Count > 0 && gdv_Quality.SelectedRows.Count == 1)
        {
          DataRow row = ((DataRowView)gdv_Quality.SelectedRows[0].DataBoundItem).Row;
          this.PRC_refreshGrid(row[Quality.QUALITY_ID].ToString().Trim());
        }
      }
    }

    private void PRC_checkModification(string tableName)
    {
      if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        return;

      if (this.listaModifiche == null)
        this.listaModifiche = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>>>();

      if (this.ultimoGrafico == null)
        this.ultimoGrafico = new System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>>();

      if (this.ultimoGrafico.Count == 0)
        return;

      System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>> res = new System.Collections.Generic.List<SyncroC.Graph.Pair<int, int>>(this.graphLinesEditor.getData());

      bool toSave = false;
      if (res.Count == this.ultimoGrafico.Count)
      {
        for (int j = 0; j < res.Count; j++)
        {
          if ((res[j].First != this.ultimoGrafico[j].First) || (res[j].Second != this.ultimoGrafico[j].Second))
          {
            toSave = true;
            break;
          }
        }
      }
      else
        toSave = true;

      if (toSave)
      {
        if (this.listaModifiche.ContainsKey(tableName))
        {
          this.listaModifiche.Remove(tableName);
        }

        this.listaModifiche.Add(tableName, res);
      }
    }

    private void treeViewPRC_BeforeSelect(object sender, TreeViewCancelEventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        return;

      if (this.state != State.Read)
      {
        if (this.treeViewPRC.SelectedNode != null)
        {
          if ((this.treeViewPRC.SelectedNode.Name.StartsWith("warp_updown")) || (this.treeViewPRC.SelectedNode.Name.StartsWith("warp_endtoend")))
          {
            if (this.myDataTable.GetChanges() != null && this.myDataTable.GetChanges().Rows.Count != 0)
            {
              SyncroC.Forms.PopUp.Frm_Confirm conf = new SyncroC.Forms.PopUp.Frm_Confirm();
              if (conf.ShowDialog(this, DBManager.getDBManager().message.getTranslatedMessage("§PRC_17")).Equals(DialogResult.OK))
                this.PRC_saveData();
            }
          }
        }
      }

      if (
        e.Node.Name.StartsWith("wraparm") ||
        e.Node.Name.StartsWith("steamshower") ||
        e.Node.Name.StartsWith("sfmainsteam") ||
        e.Node.Name.StartsWith("gluegap") ||
        e.Node.Name.StartsWith("papertension") ||
        e.Node.Name.StartsWith("steam_group") ||
        e.Node.Name.StartsWith("pressure_hot_section") ||
        e.Node.Name.StartsWith("pressure_cold_section")
        )
      {
        this.grd_data.Visible = false;
        this.grd_data.SendToBack();

        this.graphLinesEditor.Visible = true;
        this.graphLinesEditor.BringToFront();

        if (this.state == State.Read)
          return;

        if (this.treeViewPRC.SelectedNode == null)
          return;

        this.PRC_checkModification(this.treeViewPRC.SelectedNode.Name);
      }
      else if ((e.Node.Name.StartsWith("warp_updown")) || (e.Node.Name.StartsWith("warp_endtoend")))
      {
        this.graphLinesEditor.Visible = false;
        this.graphLinesEditor.SendToBack();

        ((System.Windows.Forms.DataGridViewComboBoxColumn)this.grd_data.Columns["regulatedcode"]).Items.Clear();
        ((System.Windows.Forms.DataGridViewComboBoxColumn)this.grd_data.Columns["regulatedcode"]).Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generateNameForAllComponentArray(e.Node.Name.StartsWith("warp_updown")));
      }
      else
      {
        e.Cancel = true;

        if (this.state == State.Read)
          return;
      }
    }

    private void PRC_Init()
    {
      try
      {
        //PRC
        if (SyncroC.Utility.Settings.getSettings().configuration.PRC)
        {
          this.FKeyEnable[(int)FKEYS.F12] = this.tab_TabC.Visible = true;
          //RFAZZI_FI20/05/2010_1. Rivista gestione accesso pagine con password. 
          //this.tab_TabC.Image = Accessibility.getAccessibility().isLevelAtLeastMedium() ? this.iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX] : this.iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];

          //ABELLO_FI14/08/2010_1. Riviste le password
          //pct_Lock.Visible = !Accessibility.getAccessibility().isLevelAtLeastLow();
          this.pct_Lock.Visible = !Accessibility.getAccessibility().isLevelAtLeastMedium();

          SyncroC.DataBase.PRCManager.getPRCManager().generateTreeNode(this.treeViewPRC, null);

          this.grd_data.DefaultCellStyle.Font = new Font("Arial Unicode MS", 9.75F, FontStyle.Bold);
          this.grd_data.ColumnHeadersDefaultCellStyle.Font = new Font("Arial Unicode MS", 12.0F, FontStyle.Bold);

          this.grd_data.Columns.Clear();

          System.Windows.Forms.DataGridViewComboBoxColumn regulatedcode = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewComboBoxColumn sequence = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewTextBoxColumn maxvaluechange = new System.Windows.Forms.DataGridViewTextBoxColumn();

          int totalWidth = this.grd_data.Width - 100 - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;

          // 
          // regulatedcode
          // 
          regulatedcode.DataPropertyName = "regulatedcode";
          regulatedcode.HeaderText = DBManager.getDBManager().message.getTranslatedMessage("§PRC_01"); //"Regulated Code";
          regulatedcode.Name = "regulatedcode";
          regulatedcode.Width = totalWidth / 2;
          regulatedcode.DisplayMember = "Second";
          regulatedcode.ValueMember = "First";
          // 
          // sequence
          // 
          sequence.DataPropertyName = "sequence";
          sequence.HeaderText = DBManager.getDBManager().message.getTranslatedMessage("§PRC_02"); //"Sequence";
          sequence.Name = "sequence";
          sequence.Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generateWarpLimitArray());
          sequence.Width = 100;
          // 
          // maxvaluechange
          // 
          maxvaluechange.DataPropertyName = "maxvaluechange";
          maxvaluechange.HeaderText = DBManager.getDBManager().message.getTranslatedMessage("§PRC_03"); //"Max % Value Change";
          maxvaluechange.Name = "maxvaluechange";
          maxvaluechange.Width = totalWidth / 2;

          this.grd_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            sequence,
            regulatedcode,
            maxvaluechange});

          if (this.state != State.Read)
          {
            this.grd_data.AllowUserToAddRows = true;
            this.grd_data.AllowUserToDeleteRows = true;
          }
          else
          {
            this.grd_data.AllowUserToAddRows = false;
            this.grd_data.AllowUserToDeleteRows = false;
          }

          this.myDataTable = new DataTable();

          if (this.treeViewPRC.Nodes.Count != 0)
          {
            this.treeViewPRC.SelectedNode = this.treeViewPRC.Nodes[0].Nodes[0];
          }

          if (SyncroC.DataBase.PRCManager.getPRCManager().canPrint())
            this.printGrid.Visible = true;
          else
            this.printGrid.Visible = false;
        }
        else
        {
          this.FKeyEnable[(int)FKEYS.F12] = this.tab_TabC.Visible = false;
          this.treeViewPRC.Nodes.Clear();
          this.pnl_PRC.Visible = false;
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "PRC_Init - ", ex);
      }
    }

    private void PRC_saveData()
    {
      if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        return;

      if (this.treeViewPRC.SelectedNode != null)
        this.PRC_checkModification(this.treeViewPRC.SelectedNode.Name);

      if (this.listaModifiche != null)
      {
        if (this.listaModifiche.Count != 0)
        {
          string qualityid = ttb_QualityID.Value;

          foreach (string key in this.listaModifiche.Keys)
          {
            SyncroC.DataBase.PRCManager.getPRCManager().deletePRCQualityOverride(key, qualityid);
            SyncroC.DataBase.PRCManager.getPRCManager().insertPRCQualityOverride(key, qualityid, this.listaModifiche[key]);
          }

          this.listaModifiche.Clear();
        }
      }

      if (this.myDataTable != null)
      {
        if (this.myDataTable.GetChanges() != null && this.myDataTable.GetChanges().Rows.Count != 0)
        {
          string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_ove_" + this.treeViewPRC.SelectedNode.Name;
          string qualityid = ttb_QualityID.Value;

          SyncroC.DataBase.SqlManager.getSqlManager().doCommand("DELETE FROM " + myTableName + " WHERE qualityid='" + qualityid + "';", true);

          foreach (DataRow dt in this.myDataTable.Rows)
          {
            if (dt.RowState != DataRowState.Deleted)
            {
              if (((int)dt["maxvaluechange"] != 0) && ((int)dt["sequence"] != 0))
              {
                SyncroC.DataBase.SqlManager.getSqlManager().doCommand(
                  String.Format("INSERT INTO {0} (qualityid,regulatedcode,sequence,maxvaluechange) VALUES ('{1}','{2}',{3},{4});",
                  myTableName, SyncroC.DataBase.SqlManager.removeEscapeString(qualityid), dt[0].ToString(), dt[1].ToString(), dt[2].ToString()), true);
              }
            }
          }
        }
      }
    }

    private void pct_PRCoverride_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        return;

      //warning present, save only after confirmation
      PopUp.Frm_Confirm conf = new PopUp.Frm_Confirm();
      DialogResult res = conf.ShowDialog(this, DBManager.getDBManager().message.getTranslatedMessage("§PRC_11"));
      if ((res == DialogResult.Yes) || (res == DialogResult.OK))
      {
        if (this.treeViewPRC.SelectedNode != null)
        {
          if (this.graphLinesEditor.Visible)
          {
            SyncroC.DataBase.PRCManager.getPRCManager().deletePRCQualityOverride(this.treeViewPRC.SelectedNode.Name, this.ttb_QualityID.Value);
            this.PRC_fillData(true, this.treeViewPRC.SelectedNode.Name);
          }

          if (this.grd_data.Visible)
          {
            string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_ove_warp_updown";
            SyncroC.DataBase.SqlManager.getSqlManager().doCommand("DELETE FROM " + myTableName + " WHERE qualityid='" + this.ttb_QualityID.Value + "';", true);

            myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_ove_warp_endtoend";
            SyncroC.DataBase.SqlManager.getSqlManager().doCommand("DELETE FROM " + myTableName + " WHERE qualityid='" + this.ttb_QualityID.Value + "';", true);

            this.PRC_refreshGrid(this.ttb_QualityID.Value);
          }

        }
      }
    }

    private void printGrid_Click(object sender, EventArgs e)
    {
      //ABELLO_FI23/11/2010_3 [PRC] se la griglia è vuota dà eccezione
      try
      {
        if (SyncroC.DataBase.PRCManager.getPRCManager().canPrint())
        {
          if (this.treeViewPRC.SelectedNode != null)
          {
            if (this.treeViewPRC.SelectedNode.Name.StartsWith("warp"))
            {
              DGVPrinter printer = new DGVPrinter();
              printer.Title = "PRC Curve";
              printer.SubTitle = this.treeViewPRC.SelectedNode.Name;
              printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
              printer.PageNumbers = true;
              printer.PageNumberInHeader = false;
              printer.PorportionalColumns = true;
              printer.HeaderCellAlignment = StringAlignment.Near;
              printer.Footer = "Fosber s.p.a.";
              printer.FooterSpacing = 15;

              printer.PrintNoDisplay(this.grd_data);
            }
            else
            {
              this.graphLinesEditor.PrintNoDisplay(this.ttb_QualityID.Value);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    private void grd_data_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      DataGridView view = (DataGridView)sender;
      view.Rows[e.RowIndex].ErrorText = "Error : ";
      view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Error -> !" + view.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() + "!";

      e.ThrowException = false;
    }

    private void grd_data_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
    {
      if (this.treeViewPRC.SelectedNode.Name.StartsWith("warp"))
      {
        e.Row.Cells["sequence"].Value = -1;
        e.Row.Cells["maxvaluechange"].Value = 0;
        return;
      }
    }

    private void grd_data_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (this.treeViewPRC.SelectedNode.Name.StartsWith("warp"))
      {
        if ((e.ColumnIndex == 1) && (this.grd_data.Rows[e.RowIndex].Cells["sequence"].Value != null))
        {
          switch ((int)this.grd_data.Rows[e.RowIndex].Cells["sequence"].Value)
          {
            case -3:
            case 3:
              e.CellStyle.BackColor = Color.Red;
              break;
            case -2:
            case 2:
              e.CellStyle.BackColor = Color.Orange;
              break;
            case -1:
            case 1:
              e.CellStyle.BackColor = Color.Yellow;
              break;
          }
        }
      }
    }

    private void lbl_typeOfClassification_Paint(object sender, PaintEventArgs e)
    {
      System.Drawing.Drawing2D.GraphicsPath path = SyncroC.Utility.SyncroImage.Create(0, 0, this.lbl_typeOfClassification.Width, this.lbl_typeOfClassification.Height, 5);
      Region a = new Region(path);
      this.lbl_typeOfClassification.Region = a;
    }

    private void lbl_typeOfQuery_Paint(object sender, PaintEventArgs e)
    {
      System.Drawing.Drawing2D.GraphicsPath path = SyncroC.Utility.SyncroImage.Create(0, 0, this.lbl_typeOfQuery.Width, this.lbl_typeOfQuery.Height, 5);
      Region a = new Region(path);
      this.lbl_typeOfQuery.Region = a;
    }

    #endregion    
  }
}

