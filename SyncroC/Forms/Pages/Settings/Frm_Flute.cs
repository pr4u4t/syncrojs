using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.DataBase.EventArgs;
using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Forms.PopUp;
using DGVPrinterHelper;

namespace SyncroC.Forms.Pages.Settings
{
  public class Frm_Flute : SyncroC.Forms.Pages.Settings.Frm_SettingsGroup
  {
    private System.ComponentModel.IContainer components = null;
    private SyncroC.Controls.Title ttl_FlutesList;
    private SyncroC.Controls.TitledTextBox ttb_FluteID;
    private SyncroC.Controls.TitledTextBox ttb_Text;
    private SyncroC.Controls.TitledNumericUpDown tnud_ScoreGap;
    private SyncroC.Controls.TitledNumericUpDown tnud_GrindingParameter;
    private SyncroC.Controls.TitledNumericUpDown tnud_TrimGrindingMultiplier;
    private SyncroC.Controls.TitledNumericUpDown tnud_GrindingDistance;
    private SyncroC.Controls.TitledNumericUpDown tnud_ProductionSpeed;
    private SyncroC.Controls.TitledNumericUpDown tnud_AOCSpeed;
    private SyncroC.Controls.TitledNumericUpDown tnud_Thickness;
    private SyncroC.Controls.Title title2;
    private SyncroC.Controls.Title title3;
    private SyncroC.Controls.Title title4;
    private SyncroC.Controls.TitledNumericUpDown tnud_TerminalFluteCode;
    private SyncroC.Controls.TitledNumericUpDown tnud_LubricationTime;
    private SyncroC.Controls.TitledNumericUpDown tnud_LubricationParameter;
    private System.Windows.Forms.Panel pnl_DRYFlute;
    private SyncroC.Controls.Bracket brk_Details;
    private SyncroC.Controls.GridView gdv_Flute;

    private enum State { Insert, Modify, Read };
    private State state = State.Read;

    //private Flute flute;

    //grid flute columns
    private DataGridViewColumn clm_FluteID;

    //per onde
    private bool modScoreGap = false;
    private bool modTrimGrindMultiplier = false;
    private bool modGrindDistance = false;
    private bool modGrindFrequency = false;
    private bool modLubeTime = false;
    private bool modLubeFrequency = false;
    private bool modThickness = false;
    private bool modProductionSpeed = false;
    private bool modAocSpeed = false;
    //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
    private bool modInternalFlapDisabled = false;
    //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
    private bool modCruisingSpeed = false;

    private SyncroC.Controls.TitledTextBox txt_SF1Flute;
    private SyncroC.Controls.Title title1;
    private SyncroC.Controls.TitledTextBox txt_SF3Flute;
    private SyncroC.Controls.TitledTextBox txt_SF2Flute;
    private SyncroC.Controls.CheckBox chk_InternalFlapDisabled;
    private bool modSingleFacerPosition = false;
    private Panel pnl_PRC;
    private SyncroC.Controls.Title ttl_ElementList;
    private TreeView treeViewPRC;
    private SyncroC.Controls.Bracket brk_CurveDetails;
    private DataGridView grd_data;
    private Button printGrid;
    private ContextMenuStrip contextMenuStrip_PRCCopiaIncolla;
    private ToolStripMenuItem toolStripMenuItem_Copia;
    private ToolStripMenuItem toolStripMenuItem_Incolla;
    private TreeView treeView_RoleHelper;
    private SplitContainer splitContainer_PRC;
    private SyncroC.Graph.GraphLines graphLines_RoleHelper;
    private Panel pnl_Help;
    private PictureBox pct1;
    private PictureBox pct3;
    private PictureBox pct2;
    private PictureBox pct0;
    private PictureBox pct4;
    private Label lbl_Initial_Roll1;
    private Label lbl_Initial_Roll2;
    private Label lbl_Initial_Roll3;
    private Label lbl_Initial_Roll4;
    private Label lbl_Initial_Roll5;
    private GroupBox groupBox_TipoCarta;
    private Label lbl_TipoCarta;
    private GroupBox groupBox_ClassificazioneCarta;
    private Label lbl_ClassificazioneCarta;
    private PictureBox pct_Help;
    private Label lbl_SoglieTipoCarta;
    private SyncroC.Controls.TitledNumericUpDown tnud_CruisingSpeed;

    private DataTable myDataTable = null;

    private void updateControlsEnable()
    {
      DBManager dbm;
      dbm = DBManager.getDBManager();

      //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      SyncroC.DataBase.SyncroConfig.SSTwin22_60Settings twin400;
      twin400 = dbm.syncroConfig.Twin22_60Setting;
      if (twin400.Enable)
      {
        if (chk_InternalFlapDisabled.Visible == false)
          chk_InternalFlapDisabled.Visible = true;

        if (chk_InternalFlapDisabled.Enabled == false)
          chk_InternalFlapDisabled.Enabled = true;
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
    }

    public override void _Show()
    {
      base._Show();

      this.PRC_Init();
    }

    public Frm_Flute()
    {
      InitializeComponent();

      //this.flute = DBManager.getDBManager().flute;
      this.gdv_Flute.DataSource = DBManager.getDBManager().flute.getTable();

      //this.gdv_Flute.DataSource = DBManager.getDBManager().flute.getTable();

      //gdv_Quality
      clm_FluteID = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_FluteID.Name = Flute.FLUTE_ID;
      clm_FluteID.HeaderText = "Flute ID";
      clm_FluteID.DataPropertyName = Flute.FLUTE_ID;
      clm_FluteID.Width = 250;

      gdv_Flute.Columns.Add(clm_FluteID);

      //flute.flute.Tables[Flute.FLUTE_LIST].RowChanged += new DataRowChangeEventHandler(flute_RowChanged);
      DBManager.getDBManager().flute.OnDBFluteChange += new Flute.DBChangedHandler(flute_RowChanged);
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

        DataRow row = null;
        int FirstDisplayedScrollingRowIndex = this.gdv_Flute.FirstDisplayedScrollingRowIndex;

        if (this.gdv_Flute != null)
          if (this.gdv_Flute.SelectedRows != null)
            if (this.gdv_Flute.SelectedRows.Count != 0)
              row = ((DataRowView)this.gdv_Flute.SelectedRows[0].DataBoundItem).Row;

        this.gdv_Flute.SuspendLayout();

        this.gdv_Flute.DataSource = DBManager.getDBManager().flute.getTable();

        for (int i = 0; i < this.gdv_Flute.Rows.Count; i++)
        {
          if (row != null)
          {
            string a = ((DataRowView)this.gdv_Flute.Rows[i].DataBoundItem).Row[Flute.FLUTE_ID].ToString().Trim();
            string b = row[Flute.FLUTE_ID].ToString().Trim();
            if (a == b)
            {
              this.gdv_Flute.Rows[i].Selected = true;
            }
            else
              this.gdv_Flute.Rows[i].Selected = false;
          }
        }

        if ((row == null) && (this.gdv_Flute.Rows.Count > 0))
          this.gdv_Flute.Rows[0].Selected = true;

        if ((FirstDisplayedScrollingRowIndex >= 0) && (FirstDisplayedScrollingRowIndex <= DBManager.getDBManager().flute.getCount()))
          this.gdv_Flute.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex;

        this.gdv_Flute.ResumeLayout();

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
      this.gdv_Flute.DataSource = null;
      //flute.flute.Tables[Flute.FLUTE_LIST].RowChanged -= new DataRowChangeEventHandler(flute_RowChanged);
      DBManager.getDBManager().flute.OnDBFluteChange -= new Flute.DBChangedHandler(flute_RowChanged);
      base.Dispose(disposing);
    }

    #region Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Flute));
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("DB");
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("PHT");
      System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("GU");
      System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("SPL1");
      System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("SF1");
      System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("SPL2");
      System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("SPL3");
      this.ttl_FlutesList = new SyncroC.Controls.Title();
      this.ttb_FluteID = new SyncroC.Controls.TitledTextBox();
      this.ttb_Text = new SyncroC.Controls.TitledTextBox();
      this.tnud_ScoreGap = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_GrindingParameter = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_LubricationParameter = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_TrimGrindingMultiplier = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_GrindingDistance = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_LubricationTime = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_ProductionSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_AOCSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_Thickness = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_TerminalFluteCode = new SyncroC.Controls.TitledNumericUpDown();
      this.title2 = new SyncroC.Controls.Title();
      this.title3 = new SyncroC.Controls.Title();
      this.title4 = new SyncroC.Controls.Title();
      this.pnl_DRYFlute = new System.Windows.Forms.Panel();
      this.tnud_CruisingSpeed = new SyncroC.Controls.TitledNumericUpDown();
      this.chk_InternalFlapDisabled = new SyncroC.Controls.CheckBox();
      this.txt_SF3Flute = new SyncroC.Controls.TitledTextBox();
      this.txt_SF2Flute = new SyncroC.Controls.TitledTextBox();
      this.txt_SF1Flute = new SyncroC.Controls.TitledTextBox();
      this.title1 = new SyncroC.Controls.Title();
      this.brk_Details = new SyncroC.Controls.Bracket();
      this.gdv_Flute = new SyncroC.Controls.GridView();
      this.pnl_PRC = new System.Windows.Forms.Panel();
      this.pnl_Help = new System.Windows.Forms.Panel();
      this.lbl_SoglieTipoCarta = new System.Windows.Forms.Label();
      this.pct_Help = new System.Windows.Forms.PictureBox();
      this.groupBox_ClassificazioneCarta = new System.Windows.Forms.GroupBox();
      this.lbl_ClassificazioneCarta = new System.Windows.Forms.Label();
      this.groupBox_TipoCarta = new System.Windows.Forms.GroupBox();
      this.lbl_TipoCarta = new System.Windows.Forms.Label();
      this.pct1 = new System.Windows.Forms.PictureBox();
      this.pct3 = new System.Windows.Forms.PictureBox();
      this.pct2 = new System.Windows.Forms.PictureBox();
      this.pct0 = new System.Windows.Forms.PictureBox();
      this.pct4 = new System.Windows.Forms.PictureBox();
      this.lbl_Initial_Roll1 = new System.Windows.Forms.Label();
      this.lbl_Initial_Roll2 = new System.Windows.Forms.Label();
      this.lbl_Initial_Roll3 = new System.Windows.Forms.Label();
      this.lbl_Initial_Roll4 = new System.Windows.Forms.Label();
      this.lbl_Initial_Roll5 = new System.Windows.Forms.Label();
      this.printGrid = new System.Windows.Forms.Button();
      this.ttl_ElementList = new SyncroC.Controls.Title();
      this.treeViewPRC = new System.Windows.Forms.TreeView();
      this.brk_CurveDetails = new SyncroC.Controls.Bracket();
      this.splitContainer_PRC = new System.Windows.Forms.SplitContainer();
      this.grd_data = new System.Windows.Forms.DataGridView();
      this.graphLines_RoleHelper = new SyncroC.Graph.GraphLines();
      this.treeView_RoleHelper = new System.Windows.Forms.TreeView();
      this.contextMenuStrip_PRCCopiaIncolla = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.toolStripMenuItem_Copia = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem_Incolla = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.pnl_DRYFlute.SuspendLayout();
      this.pnl_PRC.SuspendLayout();
      this.pnl_Help.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Help)).BeginInit();
      this.groupBox_ClassificazioneCarta.SuspendLayout();
      this.groupBox_TipoCarta.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pct1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct0)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct4)).BeginInit();
      this.splitContainer_PRC.Panel1.SuspendLayout();
      this.splitContainer_PRC.Panel2.SuspendLayout();
      this.splitContainer_PRC.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grd_data)).BeginInit();
      this.contextMenuStrip_PRCCopiaIncolla.SuspendLayout();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Tag = "Flute";
      this.lbl_Title_YN.Text = "Flute";
      // 
      // tab_Tab3
      // 
      this.tab_Tab3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab3.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      // 
      // tab_TabA
      // 
      this.tab_TabA.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      this.tab_TabA.Text = "DRY";
      this.tab_TabA.Visible = true;
      // 
      // tab_TabB
      // 
      this.tab_TabB.Text = "Prc";
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
      // ttl_FlutesList
      // 
      this.ttl_FlutesList.BackColor = System.Drawing.Color.Black;
      this.ttl_FlutesList.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_FlutesList.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_FlutesList.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_FlutesList.Location = new System.Drawing.Point(78, 193);
      this.ttl_FlutesList.Name = "ttl_FlutesList";
      this.ttl_FlutesList.Size = new System.Drawing.Size(251, 519);
      this.ttl_FlutesList.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_FlutesList.TabIndex = 107;
      this.ttl_FlutesList.TabStop = false;
      this.ttl_FlutesList.Text = "FLUTES";
      this.ttl_FlutesList.TranslateText = true;
      // 
      // ttb_FluteID
      // 
      this.ttb_FluteID.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_FluteID.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_FluteID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_FluteID.ImagePassword = null;
      this.ttb_FluteID.Location = new System.Drawing.Point(391, 219);
      this.ttb_FluteID.MaxChar = 30;
      this.ttb_FluteID.Multiline = false;
      this.ttb_FluteID.Name = "ttb_FluteID";
      this.ttb_FluteID.PasswordChar = '\0';
      this.ttb_FluteID.ReadOnly = true;
      this.ttb_FluteID.Size = new System.Drawing.Size(197, 65);
      this.ttb_FluteID.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_FluteID.TabIndex = 1;
      this.ttb_FluteID.TabStop = false;
      this.ttb_FluteID.Text = "Flute ID";
      this.ttb_FluteID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_FluteID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_FluteID.TranslateText = true;
      this.ttb_FluteID.Value = " ";
      this.ttb_FluteID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_FluteID.Validating += new System.ComponentModel.CancelEventHandler(this.ttb_FluteID_YN_Validating);
      // 
      // ttb_Text
      // 
      this.ttb_Text.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_Text.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Text.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Text.ImagePassword = null;
      this.ttb_Text.Location = new System.Drawing.Point(604, 219);
      this.ttb_Text.MaxChar = 100;
      this.ttb_Text.Multiline = false;
      this.ttb_Text.Name = "ttb_Text";
      this.ttb_Text.PasswordChar = '\0';
      this.ttb_Text.ReadOnly = true;
      this.ttb_Text.Size = new System.Drawing.Size(541, 65);
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
      this.tnud_ScoreGap.Location = new System.Drawing.Point(15, 43);
      this.tnud_ScoreGap.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.tnud_ScoreGap.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_ScoreGap.Name = "tnud_ScoreGap";
      this.tnud_ScoreGap.ReadOnly = true;
      this.tnud_ScoreGap.Size = new System.Drawing.Size(160, 70);
      this.tnud_ScoreGap.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_ScoreGap.TabIndex = 3;
      this.tnud_ScoreGap.TabStop = false;
      this.tnud_ScoreGap.Text = "Score Gap";
      this.tnud_ScoreGap.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ScoreGap.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ScoreGap.TranslateText = true;
      this.tnud_ScoreGap.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tnud_ScoreGap.Value = "0";
      this.tnud_ScoreGap.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ScoreGap.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_ScoreGap.ValueChanged += new System.EventHandler(this.tnud_ScoreGap_ValueChanged);
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
      this.tnud_GrindingParameter.Location = new System.Drawing.Point(217, 134);
      this.tnud_GrindingParameter.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
      this.tnud_GrindingParameter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_GrindingParameter.Name = "tnud_GrindingParameter";
      this.tnud_GrindingParameter.ReadOnly = true;
      this.tnud_GrindingParameter.Size = new System.Drawing.Size(160, 70);
      this.tnud_GrindingParameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_GrindingParameter.TabIndex = 6;
      this.tnud_GrindingParameter.TabStop = false;
      this.tnud_GrindingParameter.Text = "Grinding Frequency";
      this.tnud_GrindingParameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingParameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingParameter.TranslateText = true;
      this.tnud_GrindingParameter.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_GrindingParameter.Value = "0";
      this.tnud_GrindingParameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingParameter.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_GrindingParameter.ValueChanged += new System.EventHandler(this.tnud_GrindingParameter_ValueChanged);
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
      this.tnud_LubricationParameter.Location = new System.Drawing.Point(217, 217);
      this.tnud_LubricationParameter.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
      this.tnud_LubricationParameter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_LubricationParameter.Name = "tnud_LubricationParameter";
      this.tnud_LubricationParameter.ReadOnly = true;
      this.tnud_LubricationParameter.Size = new System.Drawing.Size(160, 70);
      this.tnud_LubricationParameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_LubricationParameter.TabIndex = 8;
      this.tnud_LubricationParameter.Text = "Lubrication Frequency";
      this.tnud_LubricationParameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationParameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LubricationParameter.TranslateText = true;
      this.tnud_LubricationParameter.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_LubricationParameter.Value = "0";
      this.tnud_LubricationParameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationParameter.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_LubricationParameter.ValueChanged += new System.EventHandler(this.tnud_LubricationParameter_ValueChanged);
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
      this.tnud_TrimGrindingMultiplier.Location = new System.Drawing.Point(217, 43);
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
      this.tnud_TrimGrindingMultiplier.Size = new System.Drawing.Size(160, 70);
      this.tnud_TrimGrindingMultiplier.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_TrimGrindingMultiplier.TabIndex = 4;
      this.tnud_TrimGrindingMultiplier.TabStop = false;
      this.tnud_TrimGrindingMultiplier.Text = "Trim Grindings Multiplier";
      this.tnud_TrimGrindingMultiplier.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_TrimGrindingMultiplier.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_TrimGrindingMultiplier.TranslateText = true;
      this.tnud_TrimGrindingMultiplier.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_TrimGrindingMultiplier.Value = "1";
      this.tnud_TrimGrindingMultiplier.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_TrimGrindingMultiplier.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_TrimGrindingMultiplier.ValueChanged += new System.EventHandler(this.tnud_TrimGrindingMultiplier_ValueChanged);
      // 
      // tnud_GrindingDistance
      // 
      this.tnud_GrindingDistance.DecimalPlaces = 0;
      this.tnud_GrindingDistance.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_GrindingDistance.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingDistance.ImagePassword = null;
      this.tnud_GrindingDistance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_GrindingDistance.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_GrindingDistance.Location = new System.Drawing.Point(15, 134);
      this.tnud_GrindingDistance.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
      this.tnud_GrindingDistance.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_GrindingDistance.Name = "tnud_GrindingDistance";
      this.tnud_GrindingDistance.ReadOnly = true;
      this.tnud_GrindingDistance.Size = new System.Drawing.Size(160, 70);
      this.tnud_GrindingDistance.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_GrindingDistance.TabIndex = 5;
      this.tnud_GrindingDistance.TabStop = false;
      this.tnud_GrindingDistance.Text = "Grinding Distance";
      this.tnud_GrindingDistance.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingDistance.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_GrindingDistance.TranslateText = true;
      this.tnud_GrindingDistance.UnitMeasure = SyncroC.Utility.UnitMeasure.Metre;
      this.tnud_GrindingDistance.Value = "0";
      this.tnud_GrindingDistance.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_GrindingDistance.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_GrindingDistance.ValueChanged += new System.EventHandler(this.tnud_GrindingDistance_ValueChanged);
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
      this.tnud_LubricationTime.Location = new System.Drawing.Point(15, 217);
      this.tnud_LubricationTime.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
      this.tnud_LubricationTime.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_LubricationTime.Name = "tnud_LubricationTime";
      this.tnud_LubricationTime.ReadOnly = true;
      this.tnud_LubricationTime.Size = new System.Drawing.Size(160, 70);
      this.tnud_LubricationTime.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_LubricationTime.TabIndex = 7;
      this.tnud_LubricationTime.Text = "Lubrication Time";
      this.tnud_LubricationTime.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationTime.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_LubricationTime.TranslateText = true;
      this.tnud_LubricationTime.UnitMeasure = SyncroC.Utility.UnitMeasure.Second;
      this.tnud_LubricationTime.Value = "0";
      this.tnud_LubricationTime.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_LubricationTime.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_LubricationTime.ValueChanged += new System.EventHandler(this.tnud_LubricationTime_ValueChanged);
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
      this.tnud_ProductionSpeed.Location = new System.Drawing.Point(602, 43);
      this.tnud_ProductionSpeed.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_ProductionSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_ProductionSpeed.Name = "tnud_ProductionSpeed";
      this.tnud_ProductionSpeed.ReadOnly = true;
      this.tnud_ProductionSpeed.Size = new System.Drawing.Size(160, 70);
      this.tnud_ProductionSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_ProductionSpeed.TabIndex = 20;
      this.tnud_ProductionSpeed.TabStop = false;
      this.tnud_ProductionSpeed.Text = "Production Speed";
      this.tnud_ProductionSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ProductionSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_ProductionSpeed.TranslateText = true;
      this.tnud_ProductionSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_ProductionSpeed.Value = "0";
      this.tnud_ProductionSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_ProductionSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_ProductionSpeed.ValueChanged += new System.EventHandler(this.tnud_ProductionSpeed_ValueChanged);
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
      this.tnud_AOCSpeed.Location = new System.Drawing.Point(602, 135);
      this.tnud_AOCSpeed.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_AOCSpeed.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_AOCSpeed.Name = "tnud_AOCSpeed";
      this.tnud_AOCSpeed.ReadOnly = true;
      this.tnud_AOCSpeed.Size = new System.Drawing.Size(160, 70);
      this.tnud_AOCSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_AOCSpeed.TabIndex = 21;
      this.tnud_AOCSpeed.TabStop = false;
      this.tnud_AOCSpeed.Text = "AOC Speed";
      this.tnud_AOCSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_AOCSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_AOCSpeed.TranslateText = true;
      this.tnud_AOCSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_AOCSpeed.Value = "0";
      this.tnud_AOCSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_AOCSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_AOCSpeed.ValueChanged += new System.EventHandler(this.tnud_AOCSpeed_ValueChanged);
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
      this.tnud_Thickness.Location = new System.Drawing.Point(407, 135);
      this.tnud_Thickness.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_Thickness.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Thickness.Name = "tnud_Thickness";
      this.tnud_Thickness.ReadOnly = true;
      this.tnud_Thickness.Size = new System.Drawing.Size(160, 70);
      this.tnud_Thickness.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Thickness.TabIndex = 13;
      this.tnud_Thickness.TabStop = false;
      this.tnud_Thickness.Text = "Thickness";
      this.tnud_Thickness.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Thickness.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Thickness.TranslateText = true;
      this.tnud_Thickness.UnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tnud_Thickness.Value = "0";
      this.tnud_Thickness.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Thickness.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_Thickness.ValueChanged += new System.EventHandler(this.tnud_Thickness_ValueChanged);
      // 
      // tnud_TerminalFluteCode
      // 
      this.tnud_TerminalFluteCode.DecimalPlaces = 0;
      this.tnud_TerminalFluteCode.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_TerminalFluteCode.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_TerminalFluteCode.ImagePassword = null;
      this.tnud_TerminalFluteCode.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_TerminalFluteCode.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_TerminalFluteCode.Location = new System.Drawing.Point(407, 43);
      this.tnud_TerminalFluteCode.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
      this.tnud_TerminalFluteCode.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_TerminalFluteCode.Name = "tnud_TerminalFluteCode";
      this.tnud_TerminalFluteCode.ReadOnly = true;
      this.tnud_TerminalFluteCode.Size = new System.Drawing.Size(160, 70);
      this.tnud_TerminalFluteCode.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_TerminalFluteCode.TabIndex = 12;
      this.tnud_TerminalFluteCode.TabStop = false;
      this.tnud_TerminalFluteCode.Text = "Terminal Flute Code";
      this.tnud_TerminalFluteCode.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_TerminalFluteCode.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_TerminalFluteCode.TranslateText = true;
      this.tnud_TerminalFluteCode.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
      this.tnud_TerminalFluteCode.Value = "0";
      this.tnud_TerminalFluteCode.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_TerminalFluteCode.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // title2
      // 
      this.title2.BackColor = System.Drawing.Color.Black;
      this.title2.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title2.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title2.Location = new System.Drawing.Point(4, 17);
      this.title2.Name = "title2";
      this.title2.Size = new System.Drawing.Size(373, 368);
      this.title2.Style = SyncroC.Controls.TitleStyles.Small;
      this.title2.TabIndex = 135;
      this.title2.TabStop = false;
      this.title2.Text = "SLITTER SCORER";
      this.title2.TranslateText = true;
      // 
      // title3
      // 
      this.title3.BackColor = System.Drawing.Color.Black;
      this.title3.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title3.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title3.Location = new System.Drawing.Point(396, 17);
      this.title3.Name = "title3";
      this.title3.Size = new System.Drawing.Size(176, 198);
      this.title3.Style = SyncroC.Controls.TitleStyles.Small;
      this.title3.TabIndex = 136;
      this.title3.TabStop = false;
      this.title3.Text = "STACKER";
      this.title3.TranslateText = true;
      // 
      // title4
      // 
      this.title4.BackColor = System.Drawing.Color.Black;
      this.title4.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title4.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title4.Location = new System.Drawing.Point(591, 17);
      this.title4.Name = "title4";
      this.title4.Size = new System.Drawing.Size(176, 288);
      this.title4.Style = SyncroC.Controls.TitleStyles.Small;
      this.title4.TabIndex = 137;
      this.title4.TabStop = false;
      this.title4.Text = "SPEED";
      this.title4.TranslateText = true;
      // 
      // pnl_DRYFlute
      // 
      this.pnl_DRYFlute.Controls.Add(this.tnud_CruisingSpeed);
      this.pnl_DRYFlute.Controls.Add(this.chk_InternalFlapDisabled);
      this.pnl_DRYFlute.Controls.Add(this.txt_SF3Flute);
      this.pnl_DRYFlute.Controls.Add(this.txt_SF2Flute);
      this.pnl_DRYFlute.Controls.Add(this.txt_SF1Flute);
      this.pnl_DRYFlute.Controls.Add(this.title1);
      this.pnl_DRYFlute.Controls.Add(this.title4);
      this.pnl_DRYFlute.Controls.Add(this.tnud_ProductionSpeed);
      this.pnl_DRYFlute.Controls.Add(this.tnud_AOCSpeed);
      this.pnl_DRYFlute.Controls.Add(this.tnud_TerminalFluteCode);
      this.pnl_DRYFlute.Controls.Add(this.title3);
      this.pnl_DRYFlute.Controls.Add(this.tnud_Thickness);
      this.pnl_DRYFlute.Controls.Add(this.tnud_LubricationTime);
      this.pnl_DRYFlute.Controls.Add(this.tnud_ScoreGap);
      this.pnl_DRYFlute.Controls.Add(this.tnud_GrindingParameter);
      this.pnl_DRYFlute.Controls.Add(this.tnud_LubricationParameter);
      this.pnl_DRYFlute.Controls.Add(this.title2);
      this.pnl_DRYFlute.Controls.Add(this.tnud_GrindingDistance);
      this.pnl_DRYFlute.Controls.Add(this.tnud_TrimGrindingMultiplier);
      this.pnl_DRYFlute.Location = new System.Drawing.Point(376, 288);
      this.pnl_DRYFlute.Name = "pnl_DRYFlute";
      this.pnl_DRYFlute.Size = new System.Drawing.Size(769, 399);
      this.pnl_DRYFlute.TabIndex = 139;
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
      this.tnud_CruisingSpeed.Location = new System.Drawing.Point(602, 221);
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
      this.tnud_CruisingSpeed.Size = new System.Drawing.Size(160, 70);
      this.tnud_CruisingSpeed.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_CruisingSpeed.TabIndex = 143;
      this.tnud_CruisingSpeed.TabStop = false;
      this.tnud_CruisingSpeed.Text = "Cruising Speed";
      this.tnud_CruisingSpeed.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_CruisingSpeed.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_CruisingSpeed.TranslateText = true;
      this.tnud_CruisingSpeed.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_CruisingSpeed.Value = "0";
      this.tnud_CruisingSpeed.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_CruisingSpeed.ValueForeColor = System.Drawing.SystemColors.WindowText;
      this.tnud_CruisingSpeed.ValueChanged += new System.EventHandler(this.tnud_CruisingSpeed_ValueChanged);
      // 
      // chk_InternalFlapDisabled
      // 
      this.chk_InternalFlapDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_InternalFlapDisabled.Checked = false;
      this.chk_InternalFlapDisabled.FalseStyle = SyncroC.Controls.CheckBoxFalseStyles.RedX;
      this.chk_InternalFlapDisabled.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.chk_InternalFlapDisabled.ImagePassword = null;
      this.chk_InternalFlapDisabled.Location = new System.Drawing.Point(15, 311);
      this.chk_InternalFlapDisabled.Name = "chk_InternalFlapDisabled";
      this.chk_InternalFlapDisabled.ReadOnly = true;
      this.chk_InternalFlapDisabled.Size = new System.Drawing.Size(160, 74);
      this.chk_InternalFlapDisabled.Style = SyncroC.Controls.CheckBoxStyles.Light;
      this.chk_InternalFlapDisabled.TabIndex = 9;
      this.chk_InternalFlapDisabled.Text = "Internal Flap Disabled";
      this.chk_InternalFlapDisabled.TitleBackColor = System.Drawing.Color.WhiteSmoke;
      this.chk_InternalFlapDisabled.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.chk_InternalFlapDisabled.TranslateText = true;
      this.chk_InternalFlapDisabled.OnCheckedChanged += new System.EventHandler(this.chk_InternalFlapDisabled_ValueChanged);
      // 
      // txt_SF3Flute
      // 
      this.txt_SF3Flute.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.txt_SF3Flute.Font = new System.Drawing.Font("Arial Unicode MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_SF3Flute.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.txt_SF3Flute.ImagePassword = null;
      this.txt_SF3Flute.Location = new System.Drawing.Point(405, 273);
      this.txt_SF3Flute.MaxChar = 1;
      this.txt_SF3Flute.Multiline = false;
      this.txt_SF3Flute.Name = "txt_SF3Flute";
      this.txt_SF3Flute.PasswordChar = '\0';
      this.txt_SF3Flute.ReadOnly = true;
      this.txt_SF3Flute.Size = new System.Drawing.Size(43, 73);
      this.txt_SF3Flute.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.txt_SF3Flute.TabIndex = 142;
      this.txt_SF3Flute.TabStop = false;
      this.txt_SF3Flute.Text = "3";
      this.txt_SF3Flute.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.txt_SF3Flute.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.txt_SF3Flute.TranslateText = false;
      this.txt_SF3Flute.Value = "-";
      this.txt_SF3Flute.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // txt_SF2Flute
      // 
      this.txt_SF2Flute.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.txt_SF2Flute.Font = new System.Drawing.Font("Arial Unicode MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_SF2Flute.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.txt_SF2Flute.ImagePassword = null;
      this.txt_SF2Flute.Location = new System.Drawing.Point(467, 273);
      this.txt_SF2Flute.MaxChar = 1;
      this.txt_SF2Flute.Multiline = false;
      this.txt_SF2Flute.Name = "txt_SF2Flute";
      this.txt_SF2Flute.PasswordChar = '\0';
      this.txt_SF2Flute.ReadOnly = true;
      this.txt_SF2Flute.Size = new System.Drawing.Size(43, 73);
      this.txt_SF2Flute.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.txt_SF2Flute.TabIndex = 141;
      this.txt_SF2Flute.TabStop = false;
      this.txt_SF2Flute.Text = "2";
      this.txt_SF2Flute.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.txt_SF2Flute.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.txt_SF2Flute.TranslateText = false;
      this.txt_SF2Flute.Value = "-";
      this.txt_SF2Flute.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // txt_SF1Flute
      // 
      this.txt_SF1Flute.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.txt_SF1Flute.Font = new System.Drawing.Font("Arial Unicode MS", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_SF1Flute.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.txt_SF1Flute.ImagePassword = null;
      this.txt_SF1Flute.Location = new System.Drawing.Point(527, 273);
      this.txt_SF1Flute.MaxChar = 1;
      this.txt_SF1Flute.Multiline = false;
      this.txt_SF1Flute.Name = "txt_SF1Flute";
      this.txt_SF1Flute.PasswordChar = '\0';
      this.txt_SF1Flute.ReadOnly = true;
      this.txt_SF1Flute.Size = new System.Drawing.Size(43, 73);
      this.txt_SF1Flute.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.txt_SF1Flute.TabIndex = 139;
      this.txt_SF1Flute.TabStop = false;
      this.txt_SF1Flute.Text = "1";
      this.txt_SF1Flute.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.txt_SF1Flute.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.txt_SF1Flute.TranslateText = false;
      this.txt_SF1Flute.Value = "-";
      this.txt_SF1Flute.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      // 
      // title1
      // 
      this.title1.BackColor = System.Drawing.Color.Black;
      this.title1.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.title1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.title1.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.title1.Location = new System.Drawing.Point(394, 227);
      this.title1.Name = "title1";
      this.title1.Size = new System.Drawing.Size(178, 130);
      this.title1.Style = SyncroC.Controls.TitleStyles.Small;
      this.title1.TabIndex = 140;
      this.title1.TabStop = false;
      this.title1.Text = "SINGLE FACER";
      this.title1.TranslateText = true;
      // 
      // brk_Details
      // 
      this.brk_Details.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.brk_Details.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.brk_Details.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_Details.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_Details.Location = new System.Drawing.Point(342, 193);
      this.brk_Details.Name = "brk_Details";
      this.brk_Details.Position = SyncroC.Controls.BracketPosition.Right;
      this.brk_Details.ShowArrow = true;
      this.brk_Details.Size = new System.Drawing.Size(803, 519);
      this.brk_Details.TabIndex = 141;
      this.brk_Details.Text = "DETAILS";
      this.brk_Details.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_Details.TranslateText = true;
      // 
      // gdv_Flute
      // 
      this.gdv_Flute.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Flute.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Flute.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Flute.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Flute.AutoGenerateColumns = false;
      this.gdv_Flute.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Flute.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.gdv_Flute.DataMember = "";
      this.gdv_Flute.DataSource = null;
      this.gdv_Flute.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_Flute.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Flute.ImagePassword = null;
      this.gdv_Flute.Location = new System.Drawing.Point(89, 222);
      this.gdv_Flute.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.gdv_Flute.MultiSelect = true;
      this.gdv_Flute.Name = "gdv_Flute";
      this.gdv_Flute.RowsHeight = 30;
      this.gdv_Flute.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Flute.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Flute.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Flute.Size = new System.Drawing.Size(240, 490);
      this.gdv_Flute.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Flute.TabIndex = 163;
      this.gdv_Flute.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Flute.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Flute.TranslateText = true;
      this.gdv_Flute.SelectionChanged += new System.EventHandler(this.gdv_Flute_SelectedIndexChanged);
      this.gdv_Flute.Leave += new System.EventHandler(this.gdv_Flute_Leave);
      // 
      // pnl_PRC
      // 
      this.pnl_PRC.Controls.Add(this.pnl_Help);
      this.pnl_PRC.Controls.Add(this.printGrid);
      this.pnl_PRC.Controls.Add(this.ttl_ElementList);
      this.pnl_PRC.Controls.Add(this.treeViewPRC);
      this.pnl_PRC.Controls.Add(this.brk_CurveDetails);
      this.pnl_PRC.Controls.Add(this.splitContainer_PRC);
      this.pnl_PRC.Location = new System.Drawing.Point(78, 171);
      this.pnl_PRC.Name = "pnl_PRC";
      this.pnl_PRC.Size = new System.Drawing.Size(1078, 553);
      this.pnl_PRC.TabIndex = 167;
      this.pnl_PRC.Visible = false;
      // 
      // pnl_Help
      // 
      this.pnl_Help.BackColor = System.Drawing.Color.Lavender;
      this.pnl_Help.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnl_Help.Controls.Add(this.lbl_SoglieTipoCarta);
      this.pnl_Help.Controls.Add(this.pct_Help);
      this.pnl_Help.Controls.Add(this.groupBox_ClassificazioneCarta);
      this.pnl_Help.Controls.Add(this.groupBox_TipoCarta);
      this.pnl_Help.Controls.Add(this.pct1);
      this.pnl_Help.Controls.Add(this.pct3);
      this.pnl_Help.Controls.Add(this.pct2);
      this.pnl_Help.Controls.Add(this.pct0);
      this.pnl_Help.Controls.Add(this.pct4);
      this.pnl_Help.Controls.Add(this.lbl_Initial_Roll1);
      this.pnl_Help.Controls.Add(this.lbl_Initial_Roll2);
      this.pnl_Help.Controls.Add(this.lbl_Initial_Roll3);
      this.pnl_Help.Controls.Add(this.lbl_Initial_Roll4);
      this.pnl_Help.Controls.Add(this.lbl_Initial_Roll5);
      this.pnl_Help.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.pnl_Help.Location = new System.Drawing.Point(313, 27);
      this.pnl_Help.Name = "pnl_Help";
      this.pnl_Help.Size = new System.Drawing.Size(758, 80);
      this.pnl_Help.TabIndex = 169;
      // 
      // lbl_SoglieTipoCarta
      // 
      this.lbl_SoglieTipoCarta.AutoEllipsis = true;
      this.lbl_SoglieTipoCarta.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_SoglieTipoCarta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_SoglieTipoCarta.Location = new System.Drawing.Point(86, 58);
      this.lbl_SoglieTipoCarta.Name = "lbl_SoglieTipoCarta";
      this.lbl_SoglieTipoCarta.Size = new System.Drawing.Size(576, 15);
      this.lbl_SoglieTipoCarta.TabIndex = 244;
      this.lbl_SoglieTipoCarta.Text = "| VL <= 90 | L <= 115 | M <= 140 | H <= 150 | VH <= 200 |";
      this.lbl_SoglieTipoCarta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pct_Help
      // 
      this.pct_Help.Image = global::SyncroC.Properties.Resources.info_icon;
      this.pct_Help.Location = new System.Drawing.Point(689, 13);
      this.pct_Help.Name = "pct_Help";
      this.pct_Help.Size = new System.Drawing.Size(57, 50);
      this.pct_Help.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pct_Help.TabIndex = 243;
      this.pct_Help.TabStop = false;
      // 
      // groupBox_ClassificazioneCarta
      // 
      this.groupBox_ClassificazioneCarta.Controls.Add(this.lbl_ClassificazioneCarta);
      this.groupBox_ClassificazioneCarta.Location = new System.Drawing.Point(385, 0);
      this.groupBox_ClassificazioneCarta.Name = "groupBox_ClassificazioneCarta";
      this.groupBox_ClassificazioneCarta.Size = new System.Drawing.Size(280, 55);
      this.groupBox_ClassificazioneCarta.TabIndex = 242;
      this.groupBox_ClassificazioneCarta.TabStop = false;
      this.groupBox_ClassificazioneCarta.Text = "Classificazione Carta";
      // 
      // lbl_ClassificazioneCarta
      // 
      this.lbl_ClassificazioneCarta.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbl_ClassificazioneCarta.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_ClassificazioneCarta.Location = new System.Drawing.Point(3, 19);
      this.lbl_ClassificazioneCarta.Name = "lbl_ClassificazioneCarta";
      this.lbl_ClassificazioneCarta.Size = new System.Drawing.Size(274, 33);
      this.lbl_ClassificazioneCarta.TabIndex = 0;
      this.lbl_ClassificazioneCarta.Text = " 2 Ond. - Cla. 1 = P4 Cla. 2 = P5 \r\n 1 Ond. - Cla. 1 = P2 Cla. 2 = P3";
      this.lbl_ClassificazioneCarta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // groupBox_TipoCarta
      // 
      this.groupBox_TipoCarta.Controls.Add(this.lbl_TipoCarta);
      this.groupBox_TipoCarta.Location = new System.Drawing.Point(83, 0);
      this.groupBox_TipoCarta.Name = "groupBox_TipoCarta";
      this.groupBox_TipoCarta.Size = new System.Drawing.Size(280, 55);
      this.groupBox_TipoCarta.TabIndex = 241;
      this.groupBox_TipoCarta.TabStop = false;
      this.groupBox_TipoCarta.Text = "Tipo Carta";
      // 
      // lbl_TipoCarta
      // 
      this.lbl_TipoCarta.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbl_TipoCarta.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_TipoCarta.Location = new System.Drawing.Point(3, 19);
      this.lbl_TipoCarta.Name = "lbl_TipoCarta";
      this.lbl_TipoCarta.Size = new System.Drawing.Size(274, 33);
      this.lbl_TipoCarta.TabIndex = 0;
      this.lbl_TipoCarta.Text = "2 Onda - (P1 + P2 + P3 + P4 + P5) / 5\r\n1 Onda - (P1 + P2 + P3) / 3";
      this.lbl_TipoCarta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pct1
      // 
      this.pct1.Image = global::SyncroC.Properties.Resources.medium;
      this.pct1.Location = new System.Drawing.Point(3, 43);
      this.pct1.Name = "pct1";
      this.pct1.Size = new System.Drawing.Size(25, 20);
      this.pct1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct1.TabIndex = 240;
      this.pct1.TabStop = false;
      // 
      // pct3
      // 
      this.pct3.Image = global::SyncroC.Properties.Resources.medium;
      this.pct3.Location = new System.Drawing.Point(3, 13);
      this.pct3.Name = "pct3";
      this.pct3.Size = new System.Drawing.Size(25, 20);
      this.pct3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct3.TabIndex = 239;
      this.pct3.TabStop = false;
      // 
      // pct2
      // 
      this.pct2.Image = global::SyncroC.Properties.Resources.liner;
      this.pct2.Location = new System.Drawing.Point(3, 33);
      this.pct2.Name = "pct2";
      this.pct2.Size = new System.Drawing.Size(25, 10);
      this.pct2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct2.TabIndex = 238;
      this.pct2.TabStop = false;
      // 
      // pct0
      // 
      this.pct0.Image = global::SyncroC.Properties.Resources.liner;
      this.pct0.Location = new System.Drawing.Point(3, 63);
      this.pct0.Name = "pct0";
      this.pct0.Size = new System.Drawing.Size(25, 10);
      this.pct0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct0.TabIndex = 237;
      this.pct0.TabStop = false;
      // 
      // pct4
      // 
      this.pct4.Image = global::SyncroC.Properties.Resources.liner;
      this.pct4.Location = new System.Drawing.Point(3, 3);
      this.pct4.Name = "pct4";
      this.pct4.Size = new System.Drawing.Size(25, 10);
      this.pct4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pct4.TabIndex = 236;
      this.pct4.TabStop = false;
      // 
      // lbl_Initial_Roll1
      // 
      this.lbl_Initial_Roll1.AutoEllipsis = true;
      this.lbl_Initial_Roll1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Initial_Roll1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.lbl_Initial_Roll1.Location = new System.Drawing.Point(34, 60);
      this.lbl_Initial_Roll1.Name = "lbl_Initial_Roll1";
      this.lbl_Initial_Roll1.Size = new System.Drawing.Size(25, 15);
      this.lbl_Initial_Roll1.TabIndex = 235;
      this.lbl_Initial_Roll1.Text = "P1";
      this.lbl_Initial_Roll1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_Initial_Roll2
      // 
      this.lbl_Initial_Roll2.AutoEllipsis = true;
      this.lbl_Initial_Roll2.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Initial_Roll2.Location = new System.Drawing.Point(34, 44);
      this.lbl_Initial_Roll2.Name = "lbl_Initial_Roll2";
      this.lbl_Initial_Roll2.Size = new System.Drawing.Size(25, 15);
      this.lbl_Initial_Roll2.TabIndex = 234;
      this.lbl_Initial_Roll2.Text = "P2";
      this.lbl_Initial_Roll2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_Initial_Roll3
      // 
      this.lbl_Initial_Roll3.AutoEllipsis = true;
      this.lbl_Initial_Roll3.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Initial_Roll3.Location = new System.Drawing.Point(34, 29);
      this.lbl_Initial_Roll3.Name = "lbl_Initial_Roll3";
      this.lbl_Initial_Roll3.Size = new System.Drawing.Size(25, 15);
      this.lbl_Initial_Roll3.TabIndex = 233;
      this.lbl_Initial_Roll3.Text = "P3";
      this.lbl_Initial_Roll3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_Initial_Roll4
      // 
      this.lbl_Initial_Roll4.AutoEllipsis = true;
      this.lbl_Initial_Roll4.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Initial_Roll4.Location = new System.Drawing.Point(34, 13);
      this.lbl_Initial_Roll4.Name = "lbl_Initial_Roll4";
      this.lbl_Initial_Roll4.Size = new System.Drawing.Size(25, 15);
      this.lbl_Initial_Roll4.TabIndex = 232;
      this.lbl_Initial_Roll4.Text = "P4";
      this.lbl_Initial_Roll4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_Initial_Roll5
      // 
      this.lbl_Initial_Roll5.AutoEllipsis = true;
      this.lbl_Initial_Roll5.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Initial_Roll5.Location = new System.Drawing.Point(34, -3);
      this.lbl_Initial_Roll5.Name = "lbl_Initial_Roll5";
      this.lbl_Initial_Roll5.Size = new System.Drawing.Size(25, 15);
      this.lbl_Initial_Roll5.TabIndex = 231;
      this.lbl_Initial_Roll5.Text = "P5";
      this.lbl_Initial_Roll5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // printGrid
      // 
      this.printGrid.BackgroundImage = global::SyncroC.Properties.Resources.PrinterIcon;
      this.printGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.printGrid.Cursor = System.Windows.Forms.Cursors.Hand;
      this.printGrid.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.printGrid.Location = new System.Drawing.Point(992, 3);
      this.printGrid.Name = "printGrid";
      this.printGrid.Size = new System.Drawing.Size(75, 23);
      this.printGrid.TabIndex = 166;
      this.printGrid.UseVisualStyleBackColor = true;
      this.printGrid.Click += new System.EventHandler(this.printGrid_Click);
      // 
      // ttl_ElementList
      // 
      this.ttl_ElementList.BackColor = System.Drawing.Color.Black;
      this.ttl_ElementList.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_ElementList.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_ElementList.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_ElementList.Location = new System.Drawing.Point(3, 3);
      this.ttl_ElementList.Name = "ttl_ElementList";
      this.ttl_ElementList.Size = new System.Drawing.Size(274, 547);
      this.ttl_ElementList.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_ElementList.TabIndex = 112;
      this.ttl_ElementList.TabStop = false;
      this.ttl_ElementList.Text = "Control List";
      this.ttl_ElementList.TranslateText = true;
      // 
      // treeViewPRC
      // 
      this.treeViewPRC.BackColor = System.Drawing.Color.WhiteSmoke;
      this.treeViewPRC.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.treeViewPRC.HideSelection = false;
      this.treeViewPRC.Location = new System.Drawing.Point(14, 33);
      this.treeViewPRC.Name = "treeViewPRC";
      treeNode1.Name = "Node2";
      treeNode1.Text = "DB";
      treeNode2.Name = "Node3";
      treeNode2.Text = "PHT";
      treeNode3.Name = "Node4";
      treeNode3.Text = "GU";
      treeNode4.Name = "Node7";
      treeNode4.Text = "SPL1";
      treeNode5.Name = "Node6";
      treeNode5.Text = "SF1";
      treeNode6.Name = "Node7";
      treeNode6.Text = "SPL2";
      treeNode7.Name = "Node9";
      treeNode7.Text = "SPL3";
      this.treeViewPRC.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
      this.treeViewPRC.Size = new System.Drawing.Size(263, 517);
      this.treeViewPRC.TabIndex = 165;
      this.treeViewPRC.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewPRC_AfterSelect);
      this.treeViewPRC.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewPRC_MouseDown);
      this.treeViewPRC.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewPRC_BeforeSelect);
      // 
      // brk_CurveDetails
      // 
      this.brk_CurveDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.brk_CurveDetails.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.brk_CurveDetails.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_CurveDetails.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_CurveDetails.Location = new System.Drawing.Point(283, 3);
      this.brk_CurveDetails.Name = "brk_CurveDetails";
      this.brk_CurveDetails.Position = SyncroC.Controls.BracketPosition.Right;
      this.brk_CurveDetails.ShowArrow = true;
      this.brk_CurveDetails.Size = new System.Drawing.Size(788, 547);
      this.brk_CurveDetails.TabIndex = 130;
      this.brk_CurveDetails.Text = "DETAILS";
      this.brk_CurveDetails.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_CurveDetails.TranslateText = true;
      // 
      // splitContainer_PRC
      // 
      this.splitContainer_PRC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.splitContainer_PRC.Location = new System.Drawing.Point(313, 107);
      this.splitContainer_PRC.Name = "splitContainer_PRC";
      this.splitContainer_PRC.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer_PRC.Panel1
      // 
      this.splitContainer_PRC.Panel1.Controls.Add(this.grd_data);
      // 
      // splitContainer_PRC.Panel2
      // 
      this.splitContainer_PRC.Panel2.Controls.Add(this.graphLines_RoleHelper);
      this.splitContainer_PRC.Panel2.Controls.Add(this.treeView_RoleHelper);
      this.splitContainer_PRC.Size = new System.Drawing.Size(757, 420);
      this.splitContainer_PRC.SplitterDistance = 240;
      this.splitContainer_PRC.SplitterWidth = 10;
      this.splitContainer_PRC.TabIndex = 168;
      // 
      // grd_data
      // 
      this.grd_data.AllowUserToAddRows = false;
      this.grd_data.AllowUserToDeleteRows = false;
      this.grd_data.AllowUserToResizeColumns = false;
      this.grd_data.AllowUserToResizeRows = false;
      this.grd_data.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.grd_data.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      this.grd_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.grd_data.DataBindings.Add(new System.Windows.Forms.Binding("SelectionMode", global::SyncroC.Properties.Settings.Default, "FullRowSelect", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.grd_data.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grd_data.Location = new System.Drawing.Point(0, 0);
      this.grd_data.Margin = new System.Windows.Forms.Padding(0);
      this.grd_data.MultiSelect = false;
      this.grd_data.Name = "grd_data";
      this.grd_data.ReadOnly = true;
      this.grd_data.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
      this.grd_data.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
      this.grd_data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.grd_data.SelectionMode = global::SyncroC.Properties.Settings.Default.FullRowSelect;
      this.grd_data.Size = new System.Drawing.Size(757, 240);
      this.grd_data.TabIndex = 114;
      this.grd_data.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.grd_data_CellParsing);
      this.grd_data.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grd_data_CellFormatting);
      this.grd_data.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grd_data_CellValidating);
      this.grd_data.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_data_CellEndEdit);
      this.grd_data.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.grd_data_DefaultValuesNeeded);
      this.grd_data.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grd_data_DataError);
      this.grd_data.SelectionChanged += new System.EventHandler(this.grd_data_SelectionChanged);
      // 
      // graphLines_RoleHelper
      // 
      this.graphLines_RoleHelper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.graphLines_RoleHelper.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.graphLines_RoleHelper.Location = new System.Drawing.Point(380, 0);
      this.graphLines_RoleHelper.Margin = new System.Windows.Forms.Padding(0);
      this.graphLines_RoleHelper.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.WrapArmGraphLinesEditor;
      this.graphLines_RoleHelper.Name = "graphLines_RoleHelper";
      this.graphLines_RoleHelper.Size = new System.Drawing.Size(377, 169);
      this.graphLines_RoleHelper.TabIndex = 251;
      this.graphLines_RoleHelper.TranslateText = true;
      this.graphLines_RoleHelper.Visible = false;
      // 
      // treeView_RoleHelper
      // 
      this.treeView_RoleHelper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.treeView_RoleHelper.BackColor = System.Drawing.Color.White;
      this.treeView_RoleHelper.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.treeView_RoleHelper.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.treeView_RoleHelper.ForeColor = System.Drawing.Color.Black;
      this.treeView_RoleHelper.FullRowSelect = true;
      this.treeView_RoleHelper.Location = new System.Drawing.Point(0, 0);
      this.treeView_RoleHelper.Margin = new System.Windows.Forms.Padding(0);
      this.treeView_RoleHelper.Name = "treeView_RoleHelper";
      this.treeView_RoleHelper.Size = new System.Drawing.Size(380, 170);
      this.treeView_RoleHelper.TabIndex = 167;
      this.treeView_RoleHelper.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_RoleHelper_AfterSelect);
      // 
      // contextMenuStrip_PRCCopiaIncolla
      // 
      this.contextMenuStrip_PRCCopiaIncolla.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.contextMenuStrip_PRCCopiaIncolla.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Copia,
            this.toolStripMenuItem_Incolla});
      this.contextMenuStrip_PRCCopiaIncolla.Name = "contextMenuStrip_PRCCopiaIncolla";
      this.contextMenuStrip_PRCCopiaIncolla.Size = new System.Drawing.Size(135, 64);
      this.contextMenuStrip_PRCCopiaIncolla.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_PRCCopiaIncolla_Opening);
      // 
      // toolStripMenuItem_Copia
      // 
      this.toolStripMenuItem_Copia.Image = global::SyncroC.Properties.Resources.copy;
      this.toolStripMenuItem_Copia.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.toolStripMenuItem_Copia.Name = "toolStripMenuItem_Copia";
      this.toolStripMenuItem_Copia.Size = new System.Drawing.Size(134, 30);
      this.toolStripMenuItem_Copia.Text = "Copy";
      this.toolStripMenuItem_Copia.Click += new System.EventHandler(this.toolStripMenuItem_Copia_Click);
      // 
      // toolStripMenuItem_Incolla
      // 
      this.toolStripMenuItem_Incolla.Image = global::SyncroC.Properties.Resources.paste;
      this.toolStripMenuItem_Incolla.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.toolStripMenuItem_Incolla.Name = "toolStripMenuItem_Incolla";
      this.toolStripMenuItem_Incolla.Size = new System.Drawing.Size(134, 30);
      this.toolStripMenuItem_Incolla.Text = "Paste";
      this.toolStripMenuItem_Incolla.Click += new System.EventHandler(this.toolStripMenuItem_Incolla_Click);
      // 
      // Frm_Flute
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.pnl_PRC);
      this.Controls.Add(this.gdv_Flute);
      this.Controls.Add(this.ttb_Text);
      this.Controls.Add(this.ttb_FluteID);
      this.Controls.Add(this.ttl_FlutesList);
      this.Controls.Add(this.pnl_DRYFlute);
      this.Controls.Add(this.brk_Details);
      this.Name = "Frm_Flute";
      this.Tag = "";
      this.Text = "Frm_Flute";
      this.Activated += new System.EventHandler(this.Frm_Flute_Activated);
      this.Controls.SetChildIndex(this.tab_Tab9, 0);
      this.Controls.SetChildIndex(this.brk_Details, 0);
      this.Controls.SetChildIndex(this.pnl_DRYFlute, 0);
      this.Controls.SetChildIndex(this.ttl_FlutesList, 0);
      this.Controls.SetChildIndex(this.ttb_FluteID, 0);
      this.Controls.SetChildIndex(this.ttb_Text, 0);
      this.Controls.SetChildIndex(this.gdv_Flute, 0);
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
      this.Controls.SetChildIndex(this.pnl_PRC, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.pnl_DRYFlute.ResumeLayout(false);
      this.pnl_PRC.ResumeLayout(false);
      this.pnl_Help.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Help)).EndInit();
      this.groupBox_ClassificazioneCarta.ResumeLayout(false);
      this.groupBox_TipoCarta.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pct1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct0)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct4)).EndInit();
      this.splitContainer_PRC.Panel1.ResumeLayout(false);
      this.splitContainer_PRC.Panel2.ResumeLayout(false);
      this.splitContainer_PRC.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.grd_data)).EndInit();
      this.contextMenuStrip_PRCCopiaIncolla.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    #endregion

    protected override void F3()
    {
    }

    /// <summary>
    /// CtrlF1 key behaviour: New Flute
    /// </summary>
    protected override void CtrlF1()
    {
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        this.PRC_SetReadOnly(false);
        SetPageStatus(State.Insert);
      }
      else
        PageManager.getPageManager()._SetMessage("§SYCC_11");
    }

    /// <summary>
    /// CtrlF2 key behaviour: Delete an existing record
    /// </summary>
    protected override void CtrlF2()
    {
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        if (gdv_Flute.Focused)
        {
          //DataRowCollection rows = DBManager.getDBManager().flute.flute.Tables[SyncroC.DataBase.Flute.FLUTE_LIST].Rows;
          if (DBManager.getDBManager().flute.getCount() > 0)
          {
            Frm_Confirm conf = new Frm_Confirm();
            string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_39");
            if (gdv_Flute.SelectedRows.Count != 0
              && conf.ShowDialog(this, text).Equals(DialogResult.OK))
            {
              CommunicationManager cm = CommunicationManager.GetCommunicationManager();
              foreach (DataGridViewRow row in gdv_Flute.SelectedRows)
                cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_FLUTE + "." + Command.DB_CMD_DEL + "(" + ((DataRowView)row.DataBoundItem).Row[Flute.FLUTE_ID] + ")");
            }
          }
          SetPageStatus(State.Read);
        }
      }
      else
      {
        PageManager.getPageManager()._SetMessage("§SYCC_11");
      }
    }

    /// <summary>
    /// CtrlF3 key behaviour: Delete all DB flute
    /// </summary>
    protected override void CtrlF3()
    {
      if (Accessibility.getAccessibility().isLevelHigh())
      {
        string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_40");
        Frm_Confirm conf = new Frm_Confirm();
        if (gdv_Flute.Rows.Count > 0 && state.Equals(State.Read)
          && conf.ShowDialog(this, text).Equals(DialogResult.OK))
        {
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_FLUTE + "." + Command.DB_CMD_CLR + "()");
        }
        SetPageStatus(State.Read);
      }
      else
      {
        PageManager.getPageManager()._SetMessage("§SYCC_11");
      }
    }

    /// <summary>
    /// CtrlF4 key behaviour: Save a flute
    /// </summary>
    protected override void CtrlF4()
    {
      if (Accessibility.getAccessibility().isLevelAtLeastLow())
      {
        if (this.pnl_PRC.Visible)
        {
          this.PRC_CtrlF4();
          this.Escape();
          return;
        }
      }

      if (Accessibility.getAccessibility().isLevelAtLeastLow())
      {
        if (ttb_FluteID.Value.Trim() == "")
        {
          PageManager.getPageManager()._SetMessage("§SYCC_10", true);
          return;
        }

        string toSend = Command.DB_FLUTE + ".";
        if (state == State.Modify)
          toSend += Command.DB_CMD_MOD;
        else
          toSend += Command.DB_CMD_ADD;
        toSend += "(";
        if ((state == State.Modify) || (state == State.Insert))
        {
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          cm.SendTcp(cm.SYNCROS_DB_MANAGER,
            toSend +
            ttb_FluteID.Value + Command.PARAMETER_SEPARATOR +
            txt_SF1Flute.Value + txt_SF2Flute.Value + txt_SF3Flute.Value + Command.PARAMETER_SEPARATOR +
            ttb_Text.Value + Command.PARAMETER_SEPARATOR +
            //DRY DATA
            tnud_ScoreGap.Value + Command.PARAMETER_SEPARATOR +
            tnud_GrindingParameter.Value + Command.PARAMETER_SEPARATOR +
            tnud_LubricationParameter.Value + Command.PARAMETER_SEPARATOR +
            tnud_TrimGrindingMultiplier.Value + Command.PARAMETER_SEPARATOR +
            tnud_GrindingDistance.Value + Command.PARAMETER_SEPARATOR +
            tnud_LubricationTime.Value + Command.PARAMETER_SEPARATOR +
            tnud_ProductionSpeed.Value + Command.PARAMETER_SEPARATOR +
            tnud_AOCSpeed.Value + Command.PARAMETER_SEPARATOR +
            tnud_Thickness.Value + Command.PARAMETER_SEPARATOR +
            tnud_TerminalFluteCode.Value + Command.PARAMETER_SEPARATOR +
            //SPARE DATA
            //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
            //"SPARE" + Command.PARAMETER_SEPARATOR +
            (chk_InternalFlapDisabled.Checked ? "1" : "0") + Command.PARAMETER_SEPARATOR +
            //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
            ((tnud_CruisingSpeed.Value=="") ? "0" : tnud_CruisingSpeed.Value) + Command.PARAMETER_SEPARATOR +
            //"SPARE" + Command.PARAMETER_SEPARATOR +
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
          //per onde
          if (this.state == State.Modify)
          {
            SetPageStatus(State.Read);
            this.ManageQualityChangePerFlutes();
            //agrassi_FI24-04-09_1
            this.modAocSpeed = this.modGrindDistance = this.modGrindFrequency = this.modLubeFrequency =
            this.modLubeTime = this.modProductionSpeed = this.modScoreGap = this.modSingleFacerPosition =
            this.modThickness = this.modTrimGrindMultiplier = false;
            //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
            this.modInternalFlapDisabled = false;
            //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
            this.modCruisingSpeed = false;
          }
        }
        this.Escape();
      }
      else
      {
        PageManager.getPageManager()._SetMessage("§SYCC_11");
      }
    }

    /// <summary>
    /// CtrlF5 key behaviour: Modify a flute
    /// </summary>
    protected override void CtrlF5()
    {
      if (Accessibility.getAccessibility().isLevelAtLeastLow())
      {
        this.PRC_SetReadOnly(false);

        if (gdv_Flute.Rows.Count <= 0)
          return;
        if (state == State.Insert)
          return;
        SetPageStatus(State.Modify);
      }
      else
      {
        PageManager.getPageManager()._SetMessage("§SYCC_11");
      }
    }

    private void readOnly(bool isReadOnly)
    {
      //TAB
      ttb_Text.TabStop = txt_SF1Flute.TabStop = txt_SF2Flute.TabStop = txt_SF3Flute.TabStop =
        //dry data
      tnud_ScoreGap.TabStop = tnud_TrimGrindingMultiplier.TabStop = tnud_GrindingDistance.TabStop =
      tnud_GrindingParameter.TabStop = tnud_LubricationTime.TabStop = tnud_LubricationParameter.TabStop =
      tnud_TerminalFluteCode.TabStop = tnud_Thickness.TabStop = tnud_ProductionSpeed.TabStop =
      tnud_AOCSpeed.TabStop = !isReadOnly;
      //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      chk_InternalFlapDisabled.TabStop = !isReadOnly;
      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
      this.tnud_CruisingSpeed.TabStop = !isReadOnly;

      //READONLY
      ttb_Text.ReadOnly = tnud_ScoreGap.ReadOnly = txt_SF1Flute.ReadOnly = txt_SF2Flute.ReadOnly = txt_SF3Flute.ReadOnly =
        //dry data
      tnud_TrimGrindingMultiplier.ReadOnly = tnud_GrindingDistance.ReadOnly =
      tnud_GrindingParameter.ReadOnly = tnud_LubricationTime.ReadOnly =
      tnud_LubricationParameter.ReadOnly = tnud_TerminalFluteCode.ReadOnly =
      tnud_Thickness.ReadOnly = tnud_ProductionSpeed.ReadOnly =
      tnud_AOCSpeed.ReadOnly = isReadOnly;
      //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      chk_InternalFlapDisabled.ReadOnly = isReadOnly;
      this.tnud_CruisingSpeed.ReadOnly = isReadOnly;

      //per onde
      if (isReadOnly)
      {
        tnud_ScoreGap.BackColor = txt_SF1Flute.BackColor = txt_SF2Flute.BackColor = txt_SF3Flute.BackColor =
        tnud_TrimGrindingMultiplier.BackColor = tnud_GrindingDistance.BackColor =
        tnud_GrindingParameter.BackColor = tnud_LubricationTime.BackColor =
        tnud_LubricationParameter.BackColor = tnud_Thickness.BackColor =
        tnud_ProductionSpeed.BackColor = tnud_AOCSpeed.BackColor = Color.WhiteSmoke;
        //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
        chk_InternalFlapDisabled.BackColor = Color.WhiteSmoke;
        //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
        this.tnud_CruisingSpeed.BackColor = Color.WhiteSmoke;
        //agrassi_FI24-04-09_1
        //this.modAocSpeed = this.modGrindDistance = this.modGrindFrequency = this.modLubeFrequency =
        //this.modLubeTime = this.modProductionSpeed = this.modScoreGap = this.modSingleFacerPosition =
        //this.modThickness = this.modTrimGrindMultiplier = false;
        //this.modInternalFlapDisabled = false;
      }
    }

    private void CleanErrorProvider()
    {
      ttb_FluteID.SetError(string.Empty);
    }

    /// <summary>
    /// Escape key behaviour: Abort the operation
    /// </summary>
    protected override void Escape()
    {
      SetPageStatus(State.Read);
      this.PRC_Escape();
    }

    private void CleanFluteDetails()
    {
      //clean the graphics
      ttb_FluteID.Value = "";
      ttb_Text.Value = "";
      txt_SF1Flute.Value = "-";
      txt_SF2Flute.Value = "-";
      txt_SF3Flute.Value = "-";
      //DRY DATA
      tnud_ScoreGap.Value = "0";
      tnud_TrimGrindingMultiplier.Value = "0";
      tnud_GrindingDistance.Value = "0";
      tnud_GrindingParameter.Value = "0";

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
        tnud_LubricationTime.Value = "0";
        tnud_LubricationParameter.Value = "0";
      //}
      tnud_TerminalFluteCode.Value = "0";
      tnud_Thickness.Value = "0";
      tnud_ProductionSpeed.Value = "0";
      tnud_AOCSpeed.Value = "0";
      //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      chk_InternalFlapDisabled.Checked = false;
      //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
      this.tnud_CruisingSpeed.Value = "0";
    }

    private void gdv_Flute_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      if (gdv_Flute.Rows.Count > 0 && gdv_Flute.SelectedRows.Count == 1)
      {
        DataRow row = ((DataRowView)gdv_Flute.SelectedRows[0].DataBoundItem).Row;
        if (row != null && row.RowState != DataRowState.Detached)
        {
          try
          {
            ttb_FluteID.Value = row[SyncroC.DataBase.Flute.FLUTE_ID].ToString();
            ttb_Text.Value = row[SyncroC.DataBase.Flute.TEXT].ToString();
            string sFpos = row[SyncroC.DataBase.Flute.SFS_POSITION].ToString();
            if (sFpos.Length != 3)
              sFpos = "---";
            txt_SF1Flute.Value = sFpos.ToCharArray()[0].ToString();
            txt_SF2Flute.Value = sFpos.ToCharArray()[1].ToString();
            txt_SF3Flute.Value = sFpos.ToCharArray()[2].ToString();
            //DRY DATA
            tnud_ScoreGap.Value = row[SyncroC.DataBase.Flute.SCOREGAP_UDdmm].ToString();
            tnud_TrimGrindingMultiplier.Value = row[SyncroC.DataBase.Flute.TRIM_GRINDINGS_MULTIPLIER].ToString();
            tnud_GrindingDistance.Value = row[SyncroC.DataBase.Flute.GRINDING_DISTANCE_UDm].ToString();
            tnud_GrindingParameter.Value = row[SyncroC.DataBase.Flute.GRINDING_PARAMETER_UDm].ToString();
            
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
              tnud_LubricationTime.Value = row[SyncroC.DataBase.Flute.LUBRICATION_DURATION_UTms].ToString();
              tnud_LubricationParameter.Value = row[SyncroC.DataBase.Flute.LUBRICATION_PARAMETER_UDm].ToString();
            //}

            tnud_TerminalFluteCode.Value = row[SyncroC.DataBase.Flute.TERMINAL_FLUTE_CODE].ToString();
            tnud_Thickness.Value = row[SyncroC.DataBase.Flute.THICKNESS_UDdmm].ToString();
            tnud_ProductionSpeed.Value = row[SyncroC.DataBase.Flute.PRODUCTION_SPEED_UVm_m].ToString();
            tnud_AOCSpeed.Value = row[SyncroC.DataBase.Flute.AOC_SPEED_UVm_m].ToString();
            //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
            chk_InternalFlapDisabled.Checked = row[SyncroC.DataBase.Flute.TWIN400_INTERNAL_FLAP_DISABLED].ToString() == "0" ? false : true;
            //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
            this.tnud_CruisingSpeed.Value = row[SyncroC.DataBase.Flute.CRUISING_SPEED_UVm_m].ToString();
          }
          catch (RowNotInTableException ex)
          {
            Logger.LogError(this, "gdv_Flute_SelectedIndexChanged - the rowState is not Detached (it is \"" + row.RowState.ToString() + "\" but the RowNotInTableException has been thrown anyway....", ex);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Flute_SelectedIndexChanged - ", ex);
          }
        }
        else
          CleanFluteDetails();
      }
      else
        CleanFluteDetails();
    }

    private void flute_RowChanged(object sender, DBChangedEventArgs args)
    {
      if (state.Equals(State.Read))
      {
        //if ((args.Changement == DBChangement.Insert) && gdv_Flute.Rows.Count > 0)
        //{
        //  foreach (DataGridViewRow row in gdv_Flute.Rows)
        //    row.Selected = false;
        //  gdv_Flute.Rows[gdv_Flute.Rows.Count - 1].Selected = true;
        //}
        //else
        gdv_Flute_SelectedIndexChanged(null, null);
      }
    }

    private void lst_Flutes_NN_Enter(object sender, System.EventArgs e)
    {
      state = State.Read;
    }

    private void ttb_FluteID_YN_Validating(object sender, CancelEventArgs e)
    {
      if (ttb_FluteID.Value.Trim() == "")
      {
        string text = PageManager.getPageManager()._SetMessage("§SYCC_02", true);
        ttb_FluteID.SetError(text);
      }
      else
        ttb_FluteID.SetError("");
    }

    private void Frm_Flute_Activated(object sender, EventArgs e)
    {
      //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
      updateControlsEnable();

      if (state.Equals(State.Read))
        gdv_Flute.Focus();
    }

    private void gdv_Flute_Leave(object sender, EventArgs e)
    {
      if (SyncroC.Utility.Settings.getSettings().configuration.PRC)
        return;

      if (state.Equals(State.Read))
        gdv_Flute.Focus();
    }

    private void SetPageStatus(State newState)
    {
      if (!state.Equals(newState))
      {
        state = newState;
        switch (newState)
        {
          case State.Insert:
            CleanFluteDetails();
            foreach (DataGridViewRow row in gdv_Flute.Rows)
              row.Selected = false;
            ttb_FluteID.TabStop = true;
            ttb_FluteID.ReadOnly = false;
            readOnly(false);
            lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor = lbl_Status.BackColor;
            gdv_Flute.Enabled = false;
            ttb_FluteID.Focus();
            break;

          case State.Modify:
            readOnly(false);
            lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
            gdv_Flute.Enabled = false;
            ttb_Text.Focus();
            break;

          case State.Read:
            CleanErrorProvider();
            ttb_FluteID.TabStop = false;
            ttb_FluteID.ReadOnly = true;
            readOnly(true);
            lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor = lbl_Title_YN.BackColor;
            gdv_Flute.Enabled = true;
            if (gdv_Flute.SelectedRows.Count == 0 && gdv_Flute.Rows.Count > 0)
              gdv_Flute.Rows[0].Selected = true;
            else
              gdv_Flute_SelectedIndexChanged(null, null);
            gdv_Flute.Focus();
            break;
        }
      }
    }

    //per onde
    private void tnud_ScoreGap_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modScoreGap = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_TrimGrindingMultiplier_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modTrimGrindMultiplier = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_GrindingDistance_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modGrindDistance = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_GrindingParameter_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modGrindFrequency = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_LubricationTime_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modLubeTime = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_LubricationParameter_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modLubeFrequency = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_Thickness_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modThickness = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_ProductionSpeed_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modProductionSpeed = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void tnud_AOCSpeed_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modAocSpeed = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
    private void chk_InternalFlapDisabled_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modInternalFlapDisabled = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    private void txt_SFPosition_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modSingleFacerPosition = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
    private void tnud_CruisingSpeed_ValueChanged(object sender, EventArgs e)
    {
      if (this.state == State.Modify)
      {
        this.modCruisingSpeed = true;
        ((Control)sender).BackColor = Color.Yellow;
      }
    }

    //per onde
    private void ManageQualityChangePerFlutes()
    {
      PopUp.Frm_Confirm conf = new Frm_Confirm();
      string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_140");
      DialogResult res = conf.ShowDialog(this, text);
      if ((res == DialogResult.Yes) || (res == DialogResult.OK))  //first alert
      {
        conf = new Frm_Confirm();
        text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_141");
        res = conf.ShowDialog(this, text);
        bool updateSetups = false;
        if ((res == DialogResult.Yes) || (res == DialogResult.OK)) //second alert
        {
          conf = new Frm_Confirm();
          text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_65");
          res = conf.ShowDialog(this, text);
          if ((res == DialogResult.Yes) || (res == DialogResult.OK))
            updateSetups = true;

          //DataTable quality = DBManager.getDBManager().quality.quality.Tables[Quality.QUALITY_LIST];
          DataTable quality = DBManager.getDBManager().quality.getTable();

          for (int i = 0; i < quality.Rows.Count; i++)
          {
            if (quality.Rows[i][Quality.FLUTE_ID].Equals(this.ttb_FluteID.Value))
            {
              string toSend = Command.DB_QUALITY + "." + Command.DB_CMD_MOD;
              if (updateSetups)
                toSend += "WithSetup";
              toSend += "(";
              toSend += this.NotEmptyString(quality.Rows[i][Quality.QUALITY_ID].ToString()) + Command.PARAMETER_SEPARATOR;    //0
              toSend += this.NotEmptyString(quality.Rows[i][Quality.QUALITY_TEXT].ToString()) + Command.PARAMETER_SEPARATOR;  //1
              toSend += this.NotEmptyString(quality.Rows[i][Quality.FLUTE_ID].ToString()) + Command.PARAMETER_SEPARATOR;      //2
              //DRY DATA
              if (this.modScoreGap)
                toSend += this.NotEmptyString(this.tnud_ScoreGap.Value) + Command.PARAMETER_SEPARATOR;                            //3
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.SCOREGAP_UDdmm].ToString()) + Command.PARAMETER_SEPARATOR;  //3
              if (this.modGrindFrequency)
                toSend += this.NotEmptyString(this.tnud_GrindingParameter.Value) + Command.PARAMETER_SEPARATOR;                           //4
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.GRINDING_PARAMETER_UDm].ToString()) + Command.PARAMETER_SEPARATOR;  //4
              if (this.modLubeFrequency)
                toSend += this.NotEmptyString(this.tnud_LubricationParameter.Value) + Command.PARAMETER_SEPARATOR;                          //5
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.LUBRICATION_PARAMETER_UDm].ToString()) + Command.PARAMETER_SEPARATOR; //5
              if (this.modTrimGrindMultiplier)
                toSend += this.NotEmptyString(this.tnud_TrimGrindingMultiplier.Value) + Command.PARAMETER_SEPARATOR;                        //6
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.TRIM_GRINDINGS_MULTIPLIER].ToString()) + Command.PARAMETER_SEPARATOR; //6
              if (this.modGrindDistance)
                toSend += this.NotEmptyString(this.tnud_GrindingDistance.Value) + Command.PARAMETER_SEPARATOR;                          //7
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.GRINDING_DISTANCE_UDm].ToString()) + Command.PARAMETER_SEPARATOR; //7
              if (this.modLubeTime)
                toSend += this.NotEmptyString(this.tnud_LubricationTime.Value) + Command.PARAMETER_SEPARATOR;                               //8
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.LUBRICATION_DURATION_UTms].ToString()) + Command.PARAMETER_SEPARATOR; //8
              if (this.modProductionSpeed)
                toSend += this.NotEmptyString(this.tnud_ProductionSpeed.Value) + Command.PARAMETER_SEPARATOR;                           //9
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.PRODUCTION_SPEED_UVm_m].ToString()) + Command.PARAMETER_SEPARATOR;//9
              if (this.modAocSpeed)
                toSend += this.NotEmptyString(this.tnud_AOCSpeed.Value) + Command.PARAMETER_SEPARATOR;                            //10
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.AOC_SPEED_UVm_m].ToString()) + Command.PARAMETER_SEPARATOR; //10
              if (this.modThickness)
                toSend += this.NotEmptyString(this.tnud_Thickness.Value) + Command.PARAMETER_SEPARATOR;                           //11
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.THICKNESS_UDdmm].ToString()) + Command.PARAMETER_SEPARATOR; //11
              //ABELLO_FI15/12/2010_1 Aggiunta la gestione della Velocità di crociera
              if (this.modCruisingSpeed)
                toSend += this.NotEmptyString(this.tnud_CruisingSpeed.Value) + Command.PARAMETER_SEPARATOR;                           //12
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.CRUISING_SPEED_UVm_m].ToString()) + Command.PARAMETER_SEPARATOR; //12
              
              toSend += this.NotEmptyString(quality.Rows[i][Quality.BUNDLE_LAYERS_PER_STACK].ToString()) + Command.PARAMETER_SEPARATOR; //13
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;                                                                          //14
              toSend += this.NotEmptyString(quality.Rows[i][Quality.BUNDLE_REVERSE_MODE].ToString()) + Command.PARAMETER_SEPARATOR;     //15
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_0].ToString()) + Command.PARAMETER_SEPARATOR;              //16
              toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_0].ToString()) + Command.PARAMETER_SEPARATOR;         //17
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_1].ToString()) + Command.PARAMETER_SEPARATOR;              //18
              toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_1].ToString()) + Command.PARAMETER_SEPARATOR;         //19
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_2].ToString()) + Command.PARAMETER_SEPARATOR;              //20
              toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_2].ToString()) + Command.PARAMETER_SEPARATOR;         //21
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_3].ToString()) + Command.PARAMETER_SEPARATOR;              //22
              toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_3].ToString()) + Command.PARAMETER_SEPARATOR;         //23
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_4].ToString()) + Command.PARAMETER_SEPARATOR;              //24
              toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_4].ToString()) + Command.PARAMETER_SEPARATOR;         //25
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_5].ToString()) + Command.PARAMETER_SEPARATOR;              //26
              toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_5].ToString()) + Command.PARAMETER_SEPARATOR;         //27
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_6].ToString()) + Command.PARAMETER_SEPARATOR;              //28
              toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_6].ToString()) + Command.PARAMETER_SEPARATOR;         //29
              toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR].ToString()) + Command.PARAMETER_SEPARATOR;           //30
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PREPRINT].ToString()) + Command.PARAMETER_SEPARATOR;                //31
              //PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
              //toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.INCLINATELASTBELT].ToString()) + Command.PARAMETER_SEPARATOR;       //32
              //RFAZZI_FI10/01/2009_1.Added Parameter for disable the Flap in the Twin400
              //toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              if (this.modInternalFlapDisabled)
                toSend += (chk_InternalFlapDisabled.Checked ? "1" : "0") + Command.PARAMETER_SEPARATOR;                                           //33
              else
                toSend += this.NotEmptyString(quality.Rows[i][Quality.TWIN400_INTERNAL_FLAP_DISABLED].ToString()) + Command.PARAMETER_SEPARATOR;  //33
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += "SPARE";
              toSend += ")";
              CommunicationManager cm = CommunicationManager.GetCommunicationManager();
              cm.SendTcp(cm.SYNCROS_DB_MANAGER, toSend);
              System.Threading.Thread.Sleep(400);
            }
          }//end for loop
          PopUp.Frm_Alert alert = new Frm_Alert();
          text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_142");
          res = alert.ShowDialog(text);
        }//end second alert
      }//end first alert
    }

    private string lbl_CtrlF1_YY_Text = "";
    private string lbl_CtrlF2_YY_Text = "";
    private string lbl_CtrlF3_YY_Text = "";
    /// <summary>
    /// Switch to Dry data
    /// </summary>
    protected override void F10()
    {
      pnl_PRC.Visible = false;
      tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
      tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
      this.tab_TabA.Cursor = Cursors.Arrow;
      this.tab_TabB.Cursor = Cursors.Hand;

      this.lbl_CtrlF1.Enabled = this.lbl_CtrlF1_YY.Enabled = true;
      this.lbl_CtrlF2.Enabled = this.lbl_CtrlF2_YY.Enabled = true;
      this.lbl_CtrlF3.Enabled = this.lbl_CtrlF3_YY.Enabled = true;

      this.lbl_CtrlF1_YY.Text = this.lbl_CtrlF1_YY_Text;
      this.lbl_CtrlF2_YY.Text = this.lbl_CtrlF2_YY_Text;
      this.lbl_CtrlF3_YY.Text = this.lbl_CtrlF3_YY_Text;

      this.SetPageStatus(State.Read);
    }

    // <summary>
    /// Switch to PRC data
    /// </summary>
    protected override void F11()
    {
      //PRC
      if (SyncroC.Utility.Settings.getSettings().configuration.PRC)
      {
        pnl_PRC.Visible = true;
        tab_TabA.TabSelection = SyncroC.Controls.TabSelectionType.Unselected;
        tab_TabB.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
        this.tab_TabA.Cursor = Cursors.Hand;
        this.tab_TabB.Cursor = Cursors.Arrow;
        this.treeViewPRC.Select();

        this.lbl_CtrlF1.Enabled = this.lbl_CtrlF1_YY.Enabled = false;
        this.lbl_CtrlF2.Enabled = this.lbl_CtrlF2_YY.Enabled = false;
        this.lbl_CtrlF3.Enabled = this.lbl_CtrlF3_YY.Enabled = false;

        this.lbl_CtrlF1_YY_Text = this.lbl_CtrlF1_YY.Text;
        this.lbl_CtrlF2_YY_Text = this.lbl_CtrlF2_YY.Text;
        this.lbl_CtrlF3_YY_Text = this.lbl_CtrlF3_YY.Text;

        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";

        this.SetPageStatus(State.Read);
      }
    }

    private void PRC_CtrlF4()
    {
      this.PRC_ApplyChanges();
      this.PRC_SetReadOnly(true);
    }

    private void PRC_Init()
    {
      //ABELLO_FI14/08/2010_1. Riviste le password
      //PRC
      if (SyncroC.Utility.Settings.getSettings().configuration.PRC)
      {
        this.tab_TabB.Visible = true;

        if (SyncroC.Utility.Accessibility.getAccessibility().isLevelAtLeastMedium())
        {
          this.FKeyEnable[(int)FKEYS.F11] = true;
          this.tab_TabB.Image = iml_AccessibilityImages.Images[IMG_UNLOCKED_INDEX];

          this.myDataTable = new DataTable();

          SyncroC.DataBase.PRCManager.getPRCManager().generateTreeNode(this.treeViewPRC, this.contextMenuStrip_PRCCopiaIncolla);

          if (this.treeViewPRC.Nodes.Count != 0)
          {
            this.treeViewPRC.SelectedNode = this.treeViewPRC.Nodes[0].Nodes[0];
          }

          this.grd_data.DefaultCellStyle.Font = new Font("Arial Unicode MS", 9.75F, FontStyle.Bold);
          this.grd_data.ColumnHeadersDefaultCellStyle.Font = new Font("Arial Unicode MS", 12.0F, FontStyle.Bold);

          if (SyncroC.DataBase.PRCManager.getPRCManager().canPrint())
            this.printGrid.Visible = true;
          else
            this.printGrid.Visible = false;
        }
        else
        {
          this.FKeyEnable[(int)FKEYS.F11] = false;
          this.tab_TabB.Image = iml_AccessibilityImages.Images[IMG_LOCKED_INDEX];
          if(this.pnl_PRC.Visible)
            this.F10();
        }
      }
      else
      {
        this.FKeyEnable[(int)FKEYS.F11] = this.tab_TabB.Visible = false;
      }
    }

    private void PRC_ApplyChanges()
    {
      //this.grd_data.EndEdit();

      this.treeViewPRC.Focus();

      string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_" + this.treeViewPRC.SelectedNode.Name;

      if (!this.treeViewPRC.SelectedNode.Name.StartsWith("warp"))
      {
        SyncroC.DataBase.SqlManager.getSqlManager().doCommand("DELETE FROM " + myTableName + ";", true);

        foreach (DataRow dt in this.myDataTable.Rows)
        {
          if (dt.RowState != DataRowState.Deleted)
          {
            SyncroC.DataBase.SqlManager.getSqlManager().doCommand(
              String.Format("INSERT INTO {0} (fluteid,papertype,paperclassification,speed_uvm_m,value) VALUES ('{1}','{2}','{3}',{4},{5});",
              myTableName, SyncroC.DataBase.SqlManager.removeEscapeString(dt["fluteid"].ToString()), SyncroC.DataBase.SqlManager.removeEscapeString(dt["papertype"].ToString()), SyncroC.DataBase.SqlManager.removeEscapeString(dt["paperclassification"].ToString()), dt["speed_uvm_m"].ToString(), dt["value"].ToString()), false);
          }
        }
      }
      else
      {
        SyncroC.DataBase.SqlManager.getSqlManager().doCommand("DELETE FROM " + myTableName + ";", true);

        foreach (DataRow dt in this.myDataTable.Rows)
        {
          if (dt.RowState != DataRowState.Deleted)
          {
            if (((int)dt["sequence"] != 0))
            {
              SyncroC.DataBase.SqlManager.getSqlManager().doCommand(
                String.Format("INSERT INTO {0} (fluteid,papertype,regulatedcode,sequence,maxvaluechange) VALUES ('{1}','{2}','{3}',{4},{5});",
                myTableName, SyncroC.DataBase.SqlManager.removeEscapeString(dt["fluteid"].ToString()), SyncroC.DataBase.SqlManager.removeEscapeString(dt["papertype"].ToString()), SyncroC.DataBase.SqlManager.removeEscapeString(dt["regulatedcode"].ToString()), dt["sequence"].ToString(), dt["maxvaluechange"].ToString()), false);
            }
          }
        }
      }

      this.PRC_RefreshGrid(myTableName);
    }

    private void PRC_SetReadOnly(bool _readOnly)
    {
      try
      {
        if (SyncroC.Utility.Settings.getSettings().configuration.PRC)
        {
          this.grd_data.AllowUserToAddRows = !_readOnly;
          this.grd_data.AllowUserToDeleteRows = !_readOnly;
          this.grd_data.ReadOnly = _readOnly;
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    /// <summary>
    /// Escape key behaviour: Abort the operation
    /// </summary>
    private void PRC_Escape()
    {
      try
      {
        if (SyncroC.Utility.Settings.getSettings().configuration.PRC)
        {
          this.PRC_RefreshGrid(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_" + this.treeViewPRC.SelectedNode.Name);
          this.PRC_SetReadOnly(true);
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    //ABELLO_31/09/10 Rivista la pagina delle Onde Process. Con l'aggiunta dell'albero delle regole e del grafico.
    private void grd_data_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.treeViewPRC.SelectedNode == null)
        {
          this.graphLines_RoleHelper.Visible = false;
          return;
        }

        if (this.treeViewPRC.SelectedNode.Name.Contains("warp"))
        {
          this.graphLines_RoleHelper.Visible = false;
          return;
        }

        if (this.grd_data.Rows.Count > 0 && this.grd_data.SelectedRows.Count == 1)
        {
          DataRow row = ((DataRowView)this.grd_data.SelectedRows[0].DataBoundItem).Row;
          if (row != null && row.RowState != DataRowState.Detached)
          {
            this.treeView_RoleHelper.SuspendLayout();
            this.treeView_RoleHelper.CollapseAll();
            //fluteid='{0}' AND papertype='{1}' AND paperclassification=
            this.treeView_RoleHelper.Nodes[row["fluteid"].ToString()].Expand();
            this.treeView_RoleHelper.Nodes[row["fluteid"].ToString()].Nodes[row["papertype"].ToString()].Expand();
            this.treeView_RoleHelper.Nodes[row["fluteid"].ToString()].Nodes[row["papertype"].ToString()].Nodes[row["paperclassification"].ToString()].Expand();
            this.treeView_RoleHelper.SelectedNode = this.treeView_RoleHelper.Nodes[row["fluteid"].ToString()].Nodes[row["papertype"].ToString()].Nodes[row["paperclassification"].ToString()];

            this.treeView_RoleHelper.ResumeLayout();
          }
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    //ABELLO_31/09/10 Rivista la pagina delle Onde Process. Con l'aggiunta dell'albero delle regole e del grafico.
    private void treeView_RoleHelper_AfterSelect(object sender, TreeViewEventArgs e)
    {
      try
      {
        if (this.treeViewPRC.SelectedNode == null)
        {
          this.graphLines_RoleHelper.Visible = false;
          return;
        }

        if (this.treeViewPRC.SelectedNode.Name.Contains("warp"))
        {
          this.graphLines_RoleHelper.Visible = false;
          return;
        }

        //EB\*\*-*\[SF1 PRC_09 ; 0]
        string[] appo = this.treeView_RoleHelper.SelectedNode.FullPath.Split(new Char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

        if ((appo.Length == 3) || (appo.Length == 4))
        {
          int Minimum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, this.treeViewPRC.SelectedNode.Name);
          int Maximum = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, this.treeViewPRC.SelectedNode.Name);

          if (this.treeViewPRC.SelectedNode.Name.StartsWith("wraparm"))
          {
            //ABELLO_FI09/05/2011_1 Aggiunta gestione del Process senza sensori di temperatura
            if (SyncroC.DataBase.PRCManager.getPRCManager().getTemperatureSensorPresentForWrapArm(this.treeViewPRC.SelectedNode.Name))
              this.graphLines_RoleHelper.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.WrapArmGraphLinesEditor;
            else
              this.graphLines_RoleHelper.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.WrapArmPositionGraphLinesEditor;
          }
          else if (this.treeViewPRC.SelectedNode.Name.StartsWith("steamshower"))
            this.graphLines_RoleHelper.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.SteamShower;
          else if (this.treeViewPRC.SelectedNode.Name.StartsWith("sfmainsteam"))
            this.graphLines_RoleHelper.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.SteamPressure;
          else if (this.treeViewPRC.SelectedNode.Name.StartsWith("gluegap"))
            this.graphLines_RoleHelper.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.GlueGapGraphLinesEditor;
          else if (this.treeViewPRC.SelectedNode.Name.StartsWith("pressure_hot_section"))
            this.graphLines_RoleHelper.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.PressureHotSection;
          else if (this.treeViewPRC.SelectedNode.Name.StartsWith("pressure_cold_section"))
            this.graphLines_RoleHelper.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.PressureColdSection;
          else if (this.treeViewPRC.SelectedNode.Name.StartsWith("steam_group"))
            this.graphLines_RoleHelper.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.SteamPressure;
          else if (this.treeViewPRC.SelectedNode.Name.StartsWith("papertension"))
            this.graphLines_RoleHelper.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.TensionGraphLinesEditor;

          this.graphLines_RoleHelper.Visible = true;

          string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_" + this.treeViewPRC.SelectedNode.Name;
          string orderBy = " ORDER BY fluteid,papertype,paperclassification,speed_uvm_m ASC;";
          string where = String.Format(" WHERE (fluteid='{0}' AND papertype='{1}' AND paperclassification='{2}')", appo[0], appo[1], appo[2]);

          DataTable myDataTableRollHelper = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + where + orderBy);
          if (myDataTableRollHelper != null)
          {
            Microsoft.Tools.Graphs.Lines.Line lineRes = new Microsoft.Tools.Graphs.Lines.Line(Color.Blue);
            lineRes.Width = 1;

            bool firstRow = true;
            foreach (DataRow dr in myDataTableRollHelper.Rows)
            {
              if ((SyncroC.Utility.SyncroConverter.convertToInt(dr["speed_uvm_m"].ToString(), -1) != 0) && firstRow)
              {
                lineRes.AddPoint
                (
                0,
                SyncroC.Utility.SyncroConverter.convertToInt(dr["value"].ToString(), 0)
                );
              }

              firstRow = false;

              lineRes.AddPoint
                (
                SyncroC.Utility.SyncroConverter.convertToInt(dr["speed_uvm_m"].ToString(), 0), 
                SyncroC.Utility.SyncroConverter.convertToInt(dr["value"].ToString(),0)
                );
            }

            lineRes.AddPoint
                (
                400,
                lineRes.Points[lineRes.Points.Count-1].YValue
                );

            int X = -1;
            int Y = -1;
            if(appo.Length == 4)
            {
              string[] appoPunto = appo[3].Split(new Char[] { '[', ']', ';' }, StringSplitOptions.RemoveEmptyEntries);
              if (appoPunto.Length == 2)
              {
                X = SyncroC.Utility.SyncroConverter.convertToInt(appoPunto[0],0);
                Y = SyncroC.Utility.SyncroConverter.convertToInt(appoPunto[1],0);
              }
            }

            this.graphLines_RoleHelper.drawLine(lineRes, Minimum, Maximum + 5, new Point(X, Y), "");
          }
        }
        else
          this.graphLines_RoleHelper.Visible = false;
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    //ABELLO_31/09/10 Rivista la pagina delle Onde Process. Con l'aggiunta dell'albero delle regole e del grafico.
    private void PRC_RefreshRollHelper(string myTableName)
    {
      try
      {
        string orderBy = "";
        if (myTableName.Contains("warp"))
        {
          orderBy = " ORDER BY sequence,regulatedcode ASC";
          DataTable myDataTableRollHelper = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + orderBy);

          if (myDataTableRollHelper != null)
          {
            this.treeView_RoleHelper.SuspendLayout();

            this.treeView_RoleHelper.Nodes.Clear();

            foreach (DataRow dr in myDataTableRollHelper.Rows)
            {
              string sequence = dr["sequence"].ToString();
              string fluteId = dr["fluteid"].ToString();
              string papertype = dr["papertype"].ToString();
              string punto = String.Format("[{0} ; {1}]", SyncroC.DataBase.PRCManager.getPRCManager().getSimpleNameForComponent(true, dr["regulatedcode"]), dr["maxvaluechange"]);

              if (!this.treeView_RoleHelper.Nodes.ContainsKey(sequence))
              {
                this.treeView_RoleHelper.Nodes.Add(sequence, sequence);
                this.treeView_RoleHelper.Nodes[sequence].Nodes.Add(fluteId, fluteId);
                this.treeView_RoleHelper.Nodes[sequence].Nodes[fluteId].Nodes.Add(papertype, papertype);
                this.treeView_RoleHelper.Nodes[sequence].Nodes[fluteId].Nodes[papertype].Nodes.Add(punto, punto);
              }
              else
              {
                if (!this.treeView_RoleHelper.Nodes[sequence].Nodes.ContainsKey(fluteId))
                {
                  this.treeView_RoleHelper.Nodes[sequence].Nodes.Add(fluteId, fluteId);
                  this.treeView_RoleHelper.Nodes[sequence].Nodes[fluteId].Nodes.Add(papertype, papertype);
                  this.treeView_RoleHelper.Nodes[sequence].Nodes[fluteId].Nodes[papertype].Nodes.Add(punto, punto);
                }
                else
                {
                  if (!this.treeView_RoleHelper.Nodes[sequence].Nodes[fluteId].Nodes.ContainsKey(papertype))
                  {
                    this.treeView_RoleHelper.Nodes[sequence].Nodes[fluteId].Nodes.Add(papertype, papertype);
                    this.treeView_RoleHelper.Nodes[sequence].Nodes[fluteId].Nodes[papertype].Nodes.Add(punto, punto);
                  }
                  else
                  {
                    if (!this.treeView_RoleHelper.Nodes[sequence].Nodes[fluteId].Nodes[papertype].Nodes.ContainsKey(punto))
                    {
                      this.treeView_RoleHelper.Nodes[sequence].Nodes[fluteId].Nodes[papertype].Nodes.Add(punto, punto);
                    }
                  }
                }
              }
            } //for each

            this.treeView_RoleHelper.ResumeLayout();
          }
        }
        else
        {
          if (this.treeViewPRC.SelectedNode == null)
            return;

          string listFlute = "";
          object[] appo = SyncroC.DataBase.PRCManager.getPRCManager().generateFluteArray(this.treeViewPRC.SelectedNode.Name);
          for (int a = 0; a < appo.Length; a++)
          {
            if (a == appo.Length - 1)
              listFlute += "'" + appo[a].ToString() + "'";
            else
              listFlute += "'" + appo[a].ToString() + "',";
          }

          string where = String.Format(" WHERE ({0}.fluteid IN ({1}))", myTableName, listFlute);
          orderBy = " ORDER BY fluteid,papertype,paperclassification,speed_uvm_m ASC;";
          DataTable myDataTableRollHelper = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + where + orderBy);

          if (myDataTableRollHelper != null)
          {
            this.treeView_RoleHelper.SuspendLayout();

            this.treeView_RoleHelper.Nodes.Clear();

            foreach (DataRow dr in myDataTableRollHelper.Rows)
            {
              string fluteId = dr["fluteid"].ToString();
              string papertype = dr["papertype"].ToString();
              string paperclassification = dr["paperclassification"].ToString();
              string punto = String.Format("[{0} ; {1}]", dr["speed_uvm_m"], dr["value"]);

              if (!this.treeView_RoleHelper.Nodes.ContainsKey(fluteId))
              {
                this.treeView_RoleHelper.Nodes.Add(fluteId, fluteId);
                this.treeView_RoleHelper.Nodes[fluteId].Nodes.Add(papertype, papertype);
                this.treeView_RoleHelper.Nodes[fluteId].Nodes[papertype].Nodes.Add(paperclassification, paperclassification);
                this.treeView_RoleHelper.Nodes[fluteId].Nodes[papertype].Nodes[paperclassification].Nodes.Add(punto, punto);
              }
              else
              {
                if (!this.treeView_RoleHelper.Nodes[fluteId].Nodes.ContainsKey(papertype))
                {
                  this.treeView_RoleHelper.Nodes[fluteId].Nodes.Add(papertype, papertype);
                  this.treeView_RoleHelper.Nodes[fluteId].Nodes[papertype].Nodes.Add(paperclassification, paperclassification);
                  this.treeView_RoleHelper.Nodes[fluteId].Nodes[papertype].Nodes[paperclassification].Nodes.Add(punto, punto);
                }
                else
                {
                  if (!this.treeView_RoleHelper.Nodes[fluteId].Nodes[papertype].Nodes.ContainsKey(paperclassification))
                  {
                    this.treeView_RoleHelper.Nodes[fluteId].Nodes[papertype].Nodes.Add(paperclassification, paperclassification);
                    this.treeView_RoleHelper.Nodes[fluteId].Nodes[papertype].Nodes[paperclassification].Nodes.Add(punto, punto);
                  }
                  else
                  {
                    if (!this.treeView_RoleHelper.Nodes[fluteId].Nodes[papertype].Nodes[paperclassification].Nodes.ContainsKey(punto))
                    {
                      this.treeView_RoleHelper.Nodes[fluteId].Nodes[papertype].Nodes[paperclassification].Nodes.Add(punto, punto);
                    }
                  }
                }
              }
            }

            this.treeView_RoleHelper.ResumeLayout();
          }
        }

        this.grd_data_SelectionChanged(null, null);
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    //ABELLO_FI11-10-2010_2 Aggiunto l'Help sul Client sulla PRC Curve Generiche
    private void PRC_doHelp(string theComponent)
    {
      this.lbl_SoglieTipoCarta.Text = String.Format(
        "| VL <= {0} | L <= {1} | M <= {2} | H <= {3} | VH <= {4} |",
        DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdVeryLight_UWg,
        DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdLight_UWg,
        DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdMedium_UWg,
        DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdHeavy_UWg,
        DBManager.getDBManager().lineConfig.PRCConfig.Header.PaperTresholdVeryHeavy_UWg);

      string onda = DBManager.getDBManager().language.getTranslation("Frm_SettingsGroup.tab_Tab3.Text").Substring(0, 3) + ".";
      string classificazioneCarta = DBManager.getDBManager().language.getTranslation("Frm_Paper.tlst_PaperClassification.Text").Substring(0,3) + ".";

      this.groupBox_TipoCarta.Text = DBManager.getDBManager().language.getTranslation("Frm_StatisticsRollsList.gdv_PastRolls.2");;
      this.groupBox_ClassificazioneCarta.Text = DBManager.getDBManager().language.getTranslation("Frm_Paper.tlst_PaperClassification.Text");

      switch (theComponent)
      {
        case "warp_updown":
        case "warp_endtoend":
          this.lbl_TipoCarta.Text = String.Format("2 {0} - (P1 + P2 + P3 + P4 + P5) / 5 \n\r 1 {0} - (P1 + P2 + P3) / 3", onda);
          this.lbl_ClassificazioneCarta.Text = "---";
          break;

        case "pressure_hot_section_1":
        case "pressure_hot_section_2":
        case "pressure_hot_section_3":
        case "pressure_hot_section_4":
        case "pressure_cold_section_1":
        case "steam_group_0":
        case "steam_group_1":
        case "steam_group_2":
        case "steam_group_3":
          this.lbl_TipoCarta.Text = String.Format("2 {0} - (P1 + P2 + P3 + P4 + P5) / 5 \n\r 1 {0} - (P1 + P2 + P3) / 3", onda);
          this.lbl_ClassificazioneCarta.Text = "P1";
          break;

        //ABELLO_FI25/06/11_1 - [PRC] corretto help sulla pagina delle onde generiche per tensione giuntatrice 1
        case "papertension_1":
          this.lbl_TipoCarta.Text = String.Format("2 {0} - (P1 + P2 + P3 + P4 + P5) / 5 \n\r 1 {0} - (P1 + P2 + P3) / 3", onda);
          this.lbl_ClassificazioneCarta.Text = String.Format("{0} 1 = P1 {0} 2 = P2", classificazioneCarta); 
          break;
          
        case "wraparm_31":
        case "wraparm_41":
        case "wraparm_11":
        case "steamshower_4":
          this.lbl_TipoCarta.Text = "P1";
          this.lbl_ClassificazioneCarta.Text = "P1";
          break;

        case "papertension_2":
          this.lbl_TipoCarta.Text = "P2";
          this.lbl_ClassificazioneCarta.Text = "P2";
          break;

        case "papertension_3":
          this.lbl_TipoCarta.Text = "P3";
          this.lbl_ClassificazioneCarta.Text = "P3";
          break;

        case "papertension_4":
          this.lbl_TipoCarta.Text = String.Format(" 2 {0} - P4 \n\r 1 {0} - P2", onda);
          this.lbl_ClassificazioneCarta.Text = String.Format(" 2 {0} - P4 \n\r 1 {0} - P2", onda);
          break;
          
        case "papertension_5":
          this.lbl_TipoCarta.Text = String.Format(" 2 {0} - P5 \n\r 1 {0} - P3", onda);
          this.lbl_ClassificazioneCarta.Text = String.Format(" 2 {0} - P5 \n\r 1 {0} - P3", onda);
          break;

        case "papertension_21":
          this.lbl_TipoCarta.Text = "(P2 + P3) / 2";
          this.lbl_ClassificazioneCarta.Text = String.Format("{0} 1 = P2 {0} 2 = P3", classificazioneCarta);
          break;

        case "papertension_22":
          this.lbl_TipoCarta.Text = String.Format(" 2 {0} - (P4 + P5) / 2 \n\r 1 {0} - (P2 + P3) / 2", onda);
          this.lbl_ClassificazioneCarta.Text = String.Format(" 2 {0} - {1} 1 = P4 {1} 2 = P5 \n\r 1 {0} - {1} 1 = P2 {1} 2 = P3", onda, classificazioneCarta);
          break;

        case "wraparm_1":
        case "wraparm_10":
          this.lbl_TipoCarta.Text = "(P2 + P3) / 2";
          this.lbl_ClassificazioneCarta.Text = String.Format("{0} 1 = P2 \n\r {0} 2 = P3", classificazioneCarta);
          break;

        case "wraparm_2":
        case "wraparm_20":
          this.lbl_TipoCarta.Text = String.Format("2 {0} - (P4 + P5) / 2 \n\r 1 {0} - (P2 + P3) / 2", onda);
          this.lbl_ClassificazioneCarta.Text = String.Format(" 2 {0} - {1} 1 = P4 {1} 2 = P5 \n\r 1 {0} - {1} 1 = P2 {1} 2 = P3", onda, classificazioneCarta);
          break;

        case "gluegap_1":
          this.lbl_TipoCarta.Text = "(P1 + P2) / 2";
          this.lbl_ClassificazioneCarta.Text = String.Format("{0} 1 = P1 {0} 2 = P2", classificazioneCarta); 
          break;

        case "gluegap_2":
          this.lbl_TipoCarta.Text = String.Format(" 2 {0} - (P3 + P4) / 2 \n\r 1 {0} - (P1 + P2) / 2", onda);
          this.lbl_ClassificazioneCarta.Text = String.Format(" 2 {0} - {1} 1 = P3 {1} 2 = P4 \n\r 1 {0} - {1} 1 = P1 {1} 2 = P2", onda, classificazioneCarta);
          break;

        case "gluegap_11":
          this.lbl_TipoCarta.Text = "(P2 + P3) / 2";
          this.lbl_ClassificazioneCarta.Text = String.Format("{0} 1 = P2 {0} 2 = P3", classificazioneCarta);
          break;

        case "gluegap_21":
          this.lbl_TipoCarta.Text = String.Format(" 2 {0} - (P4 + P5) / 2 \n\r 1 {0} - (P2 + P3) / 2", onda);
          this.lbl_ClassificazioneCarta.Text = String.Format(" 2 {0} - {1} 1 = P4 {1} 2 = P5 \n\r 1 {0} - {1} 1 = P2 {1} 2 = P3", onda, classificazioneCarta);
          break;

        case "wraparm_13":
        case "wraparm_12":
          this.lbl_TipoCarta.Text = "P2";
          this.lbl_ClassificazioneCarta.Text = "P2";
          break;

        case "wraparm_23":
        case "wraparm_22":
          this.lbl_TipoCarta.Text = "P3";
          this.lbl_ClassificazioneCarta.Text = "P3";
          break;

        case "sfmainsteam_1":
          this.lbl_TipoCarta.Text = "P2";
          this.lbl_ClassificazioneCarta.Text = "P2";
          break;

        case "steamshower_1":
          this.lbl_TipoCarta.Text = "P2";
          this.lbl_ClassificazioneCarta.Text = "P2";
          break;

        case "wraparm_15":
        case "wraparm_14":
          this.lbl_TipoCarta.Text = String.Format(" 2 {0} - P4 \n\r 1 {0} - P2", onda);
          this.lbl_ClassificazioneCarta.Text = String.Format(" 2 {0} - P4 \n\r 1 {0} - P2", onda);
          break;

        case "wraparm_25":
        case "wraparm_24":
          this.lbl_TipoCarta.Text = String.Format(" 2 {0} - P5 \n\r 1 {0} - P3", onda);
          this.lbl_ClassificazioneCarta.Text = String.Format(" 2 {0} - P5 \n\r 1 {0} - P3", onda);
          break;

        //ABELLO_FI25/06/11_1 - [PRC] Aggiunto help sulla pagina delle onde generiche per main steam del sf e steam shower
        case "sfmainsteam_2":
          this.lbl_TipoCarta.Text = String.Format(" 2 {0} - P4 \n\r 1 {0} - P2", onda);
          this.lbl_ClassificazioneCarta.Text = String.Format(" 2 {0} - P4 \n\r 1 {0} - P2", onda);
          break;

        case "steamshower_2":
          this.lbl_TipoCarta.Text = String.Format(" 2 {0} - P4 \n\r 1 {0} - P2", onda);
          this.lbl_ClassificazioneCarta.Text = String.Format(" 2 {0} - P4 \n\r 1 {0} - P2", onda);
          break;

        default:
          this.lbl_TipoCarta.Text = "Help Not Found";
          this.lbl_ClassificazioneCarta.Text = "Help Not Found";
          break;
      }
    }

    private void treeViewPRC_AfterSelect(object sender, TreeViewEventArgs e)
    {
      try
      {
        this.PRC_RefreshGrid(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_" + e.Node.Name);
        this.PRC_doHelp(e.Node.Name);
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    private void PRC_RefreshGrid(string myTableName)
    {
      this.inibitCellValidating = true;

      try
      {
        string orderBy = "";
        if (myTableName.Contains("warp"))
        {
          orderBy = " ORDER BY sequence,regulatedcode ASC";
          this.myDataTable.Clear();
          this.myDataTable = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + orderBy);
        }
        else
        {
          if (this.treeViewPRC.SelectedNode == null)
            return;

          string listFlute = "";
          object[] appo = SyncroC.DataBase.PRCManager.getPRCManager().generateFluteArray(this.treeViewPRC.SelectedNode.Name);
          for (int a = 0; a < appo.Length; a++)
          {
            if (a == appo.Length - 1)
              listFlute += "'" + appo[a].ToString() + "'";
            else
              listFlute += "'" + appo[a].ToString() + "',";
          }

          string where = String.Format(" WHERE ({0}.fluteid IN ({1}))", myTableName, listFlute);

          //ABELLO_FI04/07/11 [PRC] Messe le curve generiche in ordine di pesantezza e non alfabetico
          //orderBy = " ORDER BY fluteid,papertype,paperclassification,speed_uvm_m ASC;";
          orderBy = " ORDER BY fluteid,CASE when papertype = '*' then 0 when papertype = 'VL' then 1 when papertype = 'L' then 2 when papertype = 'M' then 3 when papertype = 'H' then 4 when papertype = 'VH' then 5 END,paperclassification,speed_uvm_m ASC;";
          
          this.myDataTable = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + where + orderBy);
        }

        if (this.myDataTable != null)
        {
          this.grd_data.CancelEdit();
          this.grd_data.DataSource = this.myDataTable;
          this.grd_data.Refresh();
        }

        this.PRC_RefreshRollHelper(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_" + this.treeViewPRC.SelectedNode.Name);
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }

      this.inibitCellValidating = false;
    }

    private void treeViewPRC_BeforeSelect(object sender, TreeViewCancelEventArgs e)
    {
      try
      {
        if (this.myDataTable.GetChanges() != null && this.myDataTable.GetChanges().Rows.Count != 0)
        {
          SyncroC.Forms.PopUp.Frm_Confirm conf = new SyncroC.Forms.PopUp.Frm_Confirm();
          if (conf.ShowDialog(this, DBManager.getDBManager().message.getTranslatedMessage("§PRC_17")).Equals(DialogResult.OK))
            this.PRC_ApplyChanges();
        }

        //ABELLO_FI30/11/2010_1 la griglia era più piccola delle colonne
        int VerticalScrollBarWidth = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
        if (VerticalScrollBarWidth < 15)
          VerticalScrollBarWidth = 15;

        if ((e.Node.Name.StartsWith("warp_updown")) || (e.Node.Name.StartsWith("warp_endtoend")))
        {
          this.grd_data.Columns.Clear();

          System.Windows.Forms.DataGridViewComboBoxColumn fluteid = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewComboBoxColumn papertype = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewComboBoxColumn regulatedcode = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewComboBoxColumn sequence = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewTextBoxColumn maxvaluechange = new System.Windows.Forms.DataGridViewTextBoxColumn();

          int totalWidth = this.grd_data.Width - (300 + 25) - VerticalScrollBarWidth;

          // 
          // fluteid
          // 
          fluteid.DataPropertyName = "fluteid";
          fluteid.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_Quality.tlst_FluteID.Text");
          fluteid.Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generateFluteArray(e.Node.Name));
          fluteid.Name = "fluteid";
          fluteid.Width = 100;
          // 
          // papertype
          // 
          papertype.DataPropertyName = "papertype";
          papertype.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_StatisticsRollsList.gdv_PastRolls.2");
          papertype.Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generatePaperTypeArray());
          papertype.Name = "papertype";
          papertype.Width = 100;
          // 
          // sequence
          // 
          sequence.DataPropertyName = "sequence";
          sequence.HeaderText = DBManager.getDBManager().message.getTranslatedMessage("§PRC_02"); //"Sequence";
          sequence.Name = "sequence";
          sequence.Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generateWarpLimitArray());
          sequence.Width = 100;
          // 
          // regulatedcode
          // 
          regulatedcode.DataPropertyName = "regulatedcode";
          regulatedcode.HeaderText = DBManager.getDBManager().message.getTranslatedMessage("§PRC_01"); //"Regulated Code";
          regulatedcode.Name = "regulatedcode";
          regulatedcode.Width = totalWidth / 2;
          regulatedcode.Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generateNameForAllComponentArray(e.Node.Name.StartsWith("warp_updown")));
          regulatedcode.DisplayMember = "Second";
          regulatedcode.ValueMember = "First";
          // 
          // maxvaluechange
          // 
          maxvaluechange.DataPropertyName = "maxvaluechange";
          maxvaluechange.HeaderText = DBManager.getDBManager().message.getTranslatedMessage("§PRC_03"); //"Max % Value Change";
          maxvaluechange.Name = "maxvaluechange";
          maxvaluechange.Width = (totalWidth / 2) - VerticalScrollBarWidth;

          this.grd_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            sequence,
            fluteid,
            papertype,
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
        }
        else if (SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(e.Node.Name))
        {
          this.grd_data.RowTemplate.Height = this.grd_data.DefaultCellStyle.Font.Height + 2;

          this.grd_data.Columns.Clear();

          System.Windows.Forms.DataGridViewComboBoxColumn fluteid = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewComboBoxColumn papertype = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewTextBoxColumn paperclassification = new System.Windows.Forms.DataGridViewTextBoxColumn();
          System.Windows.Forms.DataGridViewComboBoxColumn paperclassification1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewComboBoxColumn paperclassification2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewTextBoxColumn speed_uvm_m = new System.Windows.Forms.DataGridViewTextBoxColumn();
          System.Windows.Forms.DataGridViewTextBoxColumn value = new System.Windows.Forms.DataGridViewTextBoxColumn();

          int totalWidth = this.grd_data.Width - (100 + (100 * 2) + 25) - VerticalScrollBarWidth;

          // 
          // fluteid
          // 
          fluteid.DataPropertyName = "fluteid";
          fluteid.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_Quality.tlst_FluteID.Text");
          fluteid.Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generateFluteArray(e.Node.Name));
          fluteid.Name = "fluteid";
          fluteid.Width = totalWidth / 3;
          // 
          // papertype
          // 
          papertype.DataPropertyName = "papertype";
          papertype.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_StatisticsRollsList.gdv_PastRolls.2");
          papertype.Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generatePaperTypeArray());
          papertype.Name = "papertype";
          papertype.Width = 100;
          // 
          // paperclassification
          // 
          paperclassification.DataPropertyName = "paperclassification";
          paperclassification.Name = "paperclassification";
          paperclassification.Visible = false;
          // 
          // paperclassification1
          // 
          paperclassification1.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_Paper.tlst_PaperClassification.Text") + " 1"; //"Paper1 Clas.";
          paperclassification1.Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generatePaperClassificationArray(true));
          paperclassification1.Name = "paperclassification1";
          paperclassification1.Width = 100;
          // 
          // paperclassification2
          // 
          paperclassification2.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_Paper.tlst_PaperClassification.Text") + " 2";
          paperclassification2.Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generatePaperClassificationArray(true));
          paperclassification2.Name = "paperclassification2";
          paperclassification2.Width = 100;
          // 
          // speed_uvm_m
          // 
          speed_uvm_m.DataPropertyName = "speed_uvm_m";
          speed_uvm_m.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_SingleFacerStatus01.tnud_Speed_UVmm_m.Text") + " mt/min";
          speed_uvm_m.Name = "speed_uvm_m";
          speed_uvm_m.Width = totalWidth / 3;
          // 
          // value
          // 
          value.DataPropertyName = "value";
          value.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_AskValue.tnd_Value.Text");
          if (e.Node.Name.StartsWith("papertension"))
            value.HeaderText += " (" + DBManager.getDBManager().lineConfig.PRCConfig.Header.StandardRollWidthForCalculateTension_UDmm + ")";
          value.Name = "value";
          value.Width = (totalWidth / 3) - VerticalScrollBarWidth;

          this.grd_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            fluteid,
            papertype,
            paperclassification,
            paperclassification1,
            paperclassification2,
            speed_uvm_m,
            value});

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
        }
        else if (
            e.Node.Name.StartsWith("wraparm") ||
            e.Node.Name.StartsWith("steamshower") ||
            e.Node.Name.StartsWith("sfmainsteam") ||
            e.Node.Name.StartsWith("papertension") ||
            e.Node.Name.StartsWith("steam_group") ||
            e.Node.Name.StartsWith("pressure_hot_section") ||
            e.Node.Name.StartsWith("pressure_cold_section")
            )
        {
          this.grd_data.RowTemplate.Height = this.grd_data.DefaultCellStyle.Font.Height + 2;

          this.grd_data.Columns.Clear();

          System.Windows.Forms.DataGridViewComboBoxColumn fluteid = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewComboBoxColumn papertype = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewComboBoxColumn paperclassification = new System.Windows.Forms.DataGridViewComboBoxColumn();
          System.Windows.Forms.DataGridViewTextBoxColumn speed_uvm_m = new System.Windows.Forms.DataGridViewTextBoxColumn();
          System.Windows.Forms.DataGridViewTextBoxColumn value = new System.Windows.Forms.DataGridViewTextBoxColumn();

          int totalWidth = this.grd_data.Width - (200 + 100 + 25) - VerticalScrollBarWidth;

          // 
          // fluteid
          // 
          fluteid.DataPropertyName = "fluteid";
          fluteid.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_Quality.tlst_FluteID.Text");
          fluteid.Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generateFluteArray(e.Node.Name));
          fluteid.Name = "fluteid";
          fluteid.Width = totalWidth / 3;
          // 
          // papertype
          // 
          papertype.DataPropertyName = "papertype";
          papertype.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_StatisticsRollsList.gdv_PastRolls.2");
          papertype.Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generatePaperTypeArray());
          papertype.Name = "papertype";
          papertype.Width = 200;
          // 
          // paperclassification
          // 
          paperclassification.DataPropertyName = "paperclassification";
          paperclassification.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_Paper.tlst_PaperClassification.Text");
          paperclassification.Items.AddRange(SyncroC.DataBase.PRCManager.getPRCManager().generatePaperClassificationArray(true));
          paperclassification.Name = "paperclassification";
          paperclassification.Width = 100;
          // 
          // speed_uvm_m
          // 
          speed_uvm_m.DataPropertyName = "speed_uvm_m";
          speed_uvm_m.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_SingleFacerStatus01.tnud_Speed_UVmm_m.Text") + " mt/min";
          speed_uvm_m.Name = "speed_uvm_m";
          speed_uvm_m.Width = totalWidth / 3;
          // 
          // value
          // 
          value.DataPropertyName = "value";
          value.HeaderText = DBManager.getDBManager().language.getTranslation("Frm_AskValue.tnd_Value.Text");
          if (e.Node.Name.StartsWith("papertension"))
            value.HeaderText += " (" + DBManager.getDBManager().lineConfig.PRCConfig.Header.StandardRollWidthForCalculateTension_UDmm + ")";
          value.Name = "value";
          value.Width = (totalWidth / 3) - VerticalScrollBarWidth;

          this.grd_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            fluteid,
            papertype,
            paperclassification,
            speed_uvm_m,
            value});

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
        }
        else
        {
          e.Cancel = true;
        }

        foreach (DataGridViewColumn dgvCol in this.grd_data.Columns)
          dgvCol.SortMode = DataGridViewColumnSortMode.NotSortable;
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    private void grd_data_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      try
      {
        if (this.treeViewPRC.SelectedNode == null)
        {
          return;
        }

        if (SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(this.treeViewPRC.SelectedNode.Name))
        {
          if (e.ColumnIndex == 3)
          {
            if (this.grd_data.Rows[e.RowIndex].Cells["paperclassification"].Value != null)
            {
              if (this.grd_data.Rows[e.RowIndex].Cells["paperclassification"].Value != System.DBNull.Value)
              {
                string res = (string)this.grd_data.Rows[e.RowIndex].Cells["paperclassification"].Value;
                string[] appo = res.Split(new Char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (appo.Length == 2)
                {
                  grd_data.Rows[e.RowIndex].Cells["paperclassification1"].Value = appo[0];
                  grd_data.Rows[e.RowIndex].Cells["paperclassification2"].Value = appo[1];
                }
              }
            }
          }
        }

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

        if (e.RowIndex > 0)
        {
          if (grd_data.Rows[e.RowIndex - 1].DefaultCellStyle.BackColor.IsEmpty)
            grd_data.Rows[e.RowIndex - 1].DefaultCellStyle.BackColor = Color.White;

          if (grd_data.Rows[e.RowIndex].DefaultCellStyle.BackColor.IsEmpty)
            grd_data.Rows[e.RowIndex].DefaultCellStyle.BackColor = grd_data.Rows[e.RowIndex - 1].DefaultCellStyle.BackColor;

          string fluteIdPrev = Convert.ToString(grd_data.Rows[e.RowIndex - 1].Cells["fluteid"].Value);
          string fluteIdCurr = Convert.ToString(grd_data.Rows[e.RowIndex].Cells["fluteid"].Value);

          string papertypePrev = Convert.ToString(grd_data.Rows[e.RowIndex - 1].Cells["papertype"].Value);
          string papertypeCurr = Convert.ToString(grd_data.Rows[e.RowIndex].Cells["papertype"].Value);

          string paperclassificationPrev = ""; 
          string paperclassificationCurr = ""; 

          if (!this.treeViewPRC.SelectedNode.Name.StartsWith("warp"))
          {
            paperclassificationPrev = Convert.ToString(grd_data.Rows[e.RowIndex - 1].Cells["paperclassification"].Value);
            paperclassificationCurr = Convert.ToString(grd_data.Rows[e.RowIndex].Cells["paperclassification"].Value);
          }

          if
            (
            (fluteIdPrev != fluteIdCurr) ||
            (papertypePrev != papertypeCurr) ||
            (paperclassificationPrev != paperclassificationCurr)
            )
          {
            if (grd_data.Rows[e.RowIndex - 1].DefaultCellStyle.BackColor == Color.White)
            {
              grd_data.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
            }
            else
            {
              grd_data.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
          }
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    private void grd_data_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
    {
      try
      {
        if (SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(this.treeViewPRC.SelectedNode.Name))
        {
          if (e != null)
          {
            if (e.Value != null)
            {
              try
              {
                if (this.grd_data.Columns[e.ColumnIndex].Name == "paperclassification1")
                  this.grd_data.Rows[e.RowIndex].Cells["paperclassification"].Value = e.Value + "-" + this.grd_data.Rows[e.RowIndex].Cells["paperclassification2"].Value;

                if (this.grd_data.Columns[e.ColumnIndex].Name == "paperclassification2")
                  this.grd_data.Rows[e.RowIndex].Cells["paperclassification"].Value = this.grd_data.Rows[e.RowIndex].Cells["paperclassification1"].Value + "-" + e.Value;

                // Set the ParsingApplied property to 
                // Show the event is handled.
                e.ParsingApplied = true;
              }
              catch (FormatException)
              {
                // Set to false in case another CellParsing handler
                // wants to try to parse this DataGridViewCellParsingEventArgs instance.
                e.ParsingApplied = false;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    private void printGrid_Click(System.Object sender, System.EventArgs e)
    {
      //ABELLO_FI23/11/2010_3 [PRC] se la griglia è vuota dà eccezione
      try
      {
        if (SyncroC.DataBase.PRCManager.getPRCManager().canPrint())
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
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    string tableNameToCopy = "";
    private void toolStripMenuItem_Copia_Click(object sender, EventArgs e)
    {
      if (!this.treeViewPRC.SelectedNode.Name.StartsWith("warp"))
      {
        this.tableNameToCopy = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_" + this.treeViewPRC.SelectedNode.Name;
      }
    }

    private void toolStripMenuItem_Incolla_Click(object sender, EventArgs e)
    {
      if (this.treeViewPRC.SelectedNode.Name.StartsWith("warp"))
        return;

      if (this.tableNameToCopy.Trim() != "")
      {
        string tableNameToPaste = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_" + this.treeViewPRC.SelectedNode.Name;

        if (tableNameToPaste.Equals(this.tableNameToCopy))
        {
          string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
          Frm_Alert alert = new Frm_Alert();
          alert.ShowDialog(this, text);
          return;
        }

        if (tableNameToCopy.Contains("wraparm"))
        {
          if (!this.treeViewPRC.SelectedNode.Name.Contains("wraparm"))
          {
            string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
            Frm_Alert alert = new Frm_Alert();
            alert.ShowDialog(this, text);
            return;
          }
          if (SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToPaste.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")) && !SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToCopy.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")))
          {
            string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
            Frm_Alert alert = new Frm_Alert();
            alert.ShowDialog(this, text);
            return;
          }
          if (!SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToPaste.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")) && SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToCopy.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")))
          {
            string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
            Frm_Alert alert = new Frm_Alert();
            alert.ShowDialog(this, text);
            return;
          }
        }

        if (tableNameToCopy.Contains("gluegap") && !this.treeViewPRC.SelectedNode.Name.Contains("gluegap"))
        {
          string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
          Frm_Alert alert = new Frm_Alert();
          alert.ShowDialog(this, text);
          return;
        }

        if (tableNameToCopy.Contains("steamshower") && !this.treeViewPRC.SelectedNode.Name.Contains("steamshower"))
        {
          string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
          Frm_Alert alert = new Frm_Alert();
          alert.ShowDialog(this, text);
          return;
        }

        if (tableNameToCopy.Contains("sfmainsteam") && !this.treeViewPRC.SelectedNode.Name.Contains("sfmainsteam"))
        {
          string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
          Frm_Alert alert = new Frm_Alert();
          alert.ShowDialog(this, text);
          return;
        }

        //ABELLO_FI23/11/2010_2 [PRC] il copia e incolla sulle tensioni và differenziato se ha 2 carte o una
        //if (tableNameToCopy.Contains("papertension") && !this.treeViewPRC.SelectedNode.Name.Contains("papertension"))
        //{
        //  string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
        //  Frm_Alert alert = new Frm_Alert();
        //  alert.ShowDialog(this, text);
        //  return;
        //}

        if (tableNameToCopy.Contains("papertension"))
        {
          if (!this.treeViewPRC.SelectedNode.Name.Contains("papertension"))
          {
            string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
            Frm_Alert alert = new Frm_Alert();
            alert.ShowDialog(this, text);
            return;
          }
          if (SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToPaste.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")) && !SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToCopy.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")))
          {
            string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
            Frm_Alert alert = new Frm_Alert();
            alert.ShowDialog(this, text);
            return;
          }
          if (!SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToPaste.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")) && SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToCopy.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")))
          {
            string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
            Frm_Alert alert = new Frm_Alert();
            alert.ShowDialog(this, text);
            return;
          }
        }

        if (tableNameToCopy.Contains("steam_group") && !this.treeViewPRC.SelectedNode.Name.Contains("steam_group"))
        {
          string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
          Frm_Alert alert = new Frm_Alert();
          alert.ShowDialog(this, text);
          return;
        }

        if (tableNameToCopy.Contains("pressure_hot_section") && !this.treeViewPRC.SelectedNode.Name.Contains("pressure_hot_section"))
        {
          string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_10");
          Frm_Alert alert = new Frm_Alert();
          alert.ShowDialog(this, text);
          return;
        }

        SyncroC.DataBase.SqlManager.getSqlManager().copyTable(tableNameToCopy, tableNameToPaste);
        this.PRC_RefreshGrid(tableNameToPaste);

      }
    }

    private void treeViewPRC_MouseDown(object sender, MouseEventArgs e)
    {
      // Show menu only if the right mouse button is clicked.
      if (e.Button == MouseButtons.Right)
      {
        // Point where the mouse is clicked.
        Point p = new Point(e.X, e.Y);

        // Get the node that the user has clicked.
        TreeNode node = this.treeViewPRC.GetNodeAt(p);
        if (node != null)
        {
          this.treeViewPRC.SelectedNode = node;
        }
      }
    }

    private void contextMenuStrip_PRCCopiaIncolla_Opening(object sender, CancelEventArgs e)
    {
      this.toolStripMenuItem_Incolla.Enabled = true;

      if (this.tableNameToCopy == "")
      {
        this.toolStripMenuItem_Incolla.Enabled = false;
      }
      else
      {
        string tableNameToPaste = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_" + this.treeViewPRC.SelectedNode.Name;

        if (tableNameToPaste.Equals(this.tableNameToCopy))
          this.toolStripMenuItem_Incolla.Enabled = false;

        if (tableNameToCopy.Contains("wraparm"))
        {
          if (!tableNameToPaste.Contains("wraparm"))
            this.toolStripMenuItem_Incolla.Enabled = false;

          if (SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToPaste.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")) && !SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToCopy.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")))
            this.toolStripMenuItem_Incolla.Enabled = false;

          if (!SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToPaste.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")) && SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToCopy.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")))
            this.toolStripMenuItem_Incolla.Enabled = false;

          //ABELLO_FI09/05/2011_1 Aggiunta gestione del Process senza sensori di temperatura
          if(
            SyncroC.DataBase.PRCManager.getPRCManager().getTemperatureSensorPresentForWrapArm(tableNameToPaste.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")) !=
            SyncroC.DataBase.PRCManager.getPRCManager().getTemperatureSensorPresentForWrapArm(tableNameToCopy.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")) 
            )
            this.toolStripMenuItem_Incolla.Enabled = false;
        }

        if (tableNameToCopy.Contains("gluegap") && !tableNameToPaste.Contains("gluegap"))
          this.toolStripMenuItem_Incolla.Enabled = false;

        if (tableNameToCopy.Contains("steamshower") && !tableNameToPaste.Contains("steamshower"))
          this.toolStripMenuItem_Incolla.Enabled = false;

        if (tableNameToCopy.Contains("sfmainsteam") && !tableNameToPaste.Contains("sfmainsteam"))
          this.toolStripMenuItem_Incolla.Enabled = false;

        //ABELLO_FI23/11/2010_2 [PRC] il copia e incolla sulle tensioni và differenziato se ha 2 carte o una
        //if (tableNameToCopy.Contains("papertension") && !tableNameToPaste.Contains("papertension"))
        //  this.toolStripMenuItem_Incolla.Enabled = false;

        if (tableNameToCopy.Contains("papertension"))
        {
          if (!tableNameToPaste.Contains("papertension"))
            this.toolStripMenuItem_Incolla.Enabled = false;

          if (SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToPaste.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")) && !SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToCopy.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")))
            this.toolStripMenuItem_Incolla.Enabled = false;

          if (!SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToPaste.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")) && SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(tableNameToCopy.Replace(SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".prc_std_", "")))
            this.toolStripMenuItem_Incolla.Enabled = false;
        }

        if (tableNameToCopy.Contains("steam_group") && !tableNameToPaste.Contains("steam_group"))
          this.toolStripMenuItem_Incolla.Enabled = false;

        if (tableNameToCopy.Contains("pressure_hot_section") && !tableNameToPaste.Contains("pressure_hot_section"))
          this.toolStripMenuItem_Incolla.Enabled = false;
      }
    }

    private void grd_data_DataError(object sender, DataGridViewDataErrorEventArgs anError)
    {
      DataGridView view = (DataGridView)sender;
      view.Rows[anError.RowIndex].ErrorText = "Error : ";
      view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "Error -> !" + view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].Value.ToString() + "!";

      anError.ThrowException = false;
    }

    private void grd_data_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
    {
      if (this.treeViewPRC.SelectedNode.Name.StartsWith("warp"))
      {
        e.Row.Cells["fluteid"].Value = "*";
        e.Row.Cells["papertype"].Value = "*";
        e.Row.Cells["sequence"].Value = -1;
        e.Row.Cells["maxvaluechange"].Value = 0;
        return;
      }

      if (SyncroC.DataBase.PRCManager.getPRCManager().isComponentWith2Paper(this.treeViewPRC.SelectedNode.Name))
      {
        e.Row.Cells["fluteid"].Value = "*";
        e.Row.Cells["papertype"].Value = "*";
        e.Row.Cells["paperclassification"].Value = "*-*";
        e.Row.Cells["paperclassification1"].Value = "*";
        e.Row.Cells["paperclassification2"].Value = "*";
        e.Row.Cells["speed_uvm_m"].Value = 0;
        e.Row.Cells["value"].Value = 0;
      }
      else
      {
        e.Row.Cells["fluteid"].Value = "*";
        e.Row.Cells["papertype"].Value = "*";
        e.Row.Cells["paperclassification"].Value = "*";
        e.Row.Cells["speed_uvm_m"].Value = 0;
        e.Row.Cells["value"].Value = 0;
      }
    }

    private bool inibitCellValidating = false;
    private void grd_data_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
      if (this.inibitCellValidating)
        return;

      // Don't try to validate the 'new row' until finished 
      // editing since there
      // is not any point in validating its initial value
      if (this.grd_data.Rows[e.RowIndex].IsNewRow)
      { return; }

      // Validate the Value entry by disallowing empty strings.
      if (this.grd_data.Columns[e.ColumnIndex].Name == "value")
      {
        int min = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(true, this.treeViewPRC.SelectedNode.Name);
        int max = SyncroC.DataBase.PRCManager.getPRCManager().getMinOrMaxValueForComponent(false, this.treeViewPRC.SelectedNode.Name);
        int myvalue = SyncroC.Utility.SyncroConverter.convertToInt(e.FormattedValue.ToString(), Int32.MinValue);
        if ((myvalue < min) || (myvalue > max))
        {
          this.grd_data.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = String.Format("{0} Out of Range Use [{1},{2}]", myvalue, min, max);
          e.Cancel = true;

          //sarebbe bello, ma questo fa del casino
         // MessageBox.Show(this, String.Format("Value {0} is Out of Range Use [{1},{2}]", myvalue, min, max), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }

      // Validate the maxvaluechange entry by disallowing empty strings.
      if (this.grd_data.Columns[e.ColumnIndex].Name == "maxvaluechange")
      {
        int min = -100;
        int max = 100;
        int myvalue = SyncroC.Utility.SyncroConverter.convertToInt(e.FormattedValue.ToString(), Int32.MinValue);
        if ((myvalue < min) || (myvalue > max))
        {
          this.grd_data.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = String.Format("{0} Out of Range Use [{1},{2}]", myvalue, min, max);
          e.Cancel = true;

          //sarebbe bello, ma questo fa del casino
          // MessageBox.Show(this, String.Format("Value {0} is Out of Range Use [{1},{2}]", myvalue, min, max), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private void grd_data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      // Clear the row error in case the user presses ESC.   
      this.grd_data.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = String.Empty;
    }
  }
}