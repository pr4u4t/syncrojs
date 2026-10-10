using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.DataBase.EventArgs;
using SyncroC.DataBase;
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.Production
{
  public class Frm_Paper : SyncroC.Forms.Pages.Production.Frm_ProductionGroup
  {
    private SyncroC.Controls.Title ttl_PaperData;
    private SyncroC.Controls.Title ttl_PapersList;
    private SyncroC.Controls.TitledTextBox ttb_PaperID;
    private SyncroC.Controls.Title ttl_spliceSpeed;
    private SyncroC.Controls.TitledNumericUpDown tnud_CoreDiameter;
    private SyncroC.Controls.TitledNumericUpDown tnud_WeightFactor;
    private SyncroC.Controls.TitledTextBox ttb_Text;
    private SyncroC.Controls.TitledNumericUpDown tnud_Thickness;
    private SyncroC.Controls.TitledNumericUpDown tnud_Splice1;
    private SyncroC.Controls.TitledNumericUpDown tnud_Splice2;
    private SyncroC.Controls.TitledNumericUpDown tnud_Splice3;
    private SyncroC.Controls.TitledNumericUpDown tnud_Splice4;
    private SyncroC.Controls.TitledNumericUpDown tnud_Splice5;
    private SyncroC.Controls.TitledNumericUpDown tnud_Splice6;
    private SyncroC.Controls.TitledNumericUpDown tnud_Splice7;
    private System.ComponentModel.IContainer components = null;
    private SyncroC.Controls.Bracket brk_PaperDetails;
    private SyncroC.Controls.GridView gdv_Papers;

    private enum State { Insert, Modify, Read };
    private State state = State.Read;

    //grid papers columns
    private DataGridViewColumn clm_PaperID;
    private SyncroC.Controls.TitledList tlst_PaperClassification;
    private DataGridViewColumn clm_WeigthFactor;

    public Frm_Paper()
    {
      InitializeComponent();

      DataView dataView = new DataView(DBManager.getDBManager().paper.getTable());
      dataView.Sort = Paper.PAPER_ID + " Asc";
      this.gdv_Papers.DataSource = dataView;

      //gdv_Papers
      clm_PaperID = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_PaperID.Name = Paper.PAPER_ID;
      clm_PaperID.HeaderText = "ID Paper";
      clm_PaperID.DataPropertyName = Paper.PAPER_ID;
      clm_PaperID.Width = 125;
      clm_PaperID.Frozen = true;

      clm_WeigthFactor = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_WeigthFactor.Name = Paper.WEIGHT_FACTOR;
      clm_WeigthFactor.HeaderText = "Weigth";
      clm_WeigthFactor.DataPropertyName = Paper.WEIGHT_FACTOR;
      clm_WeigthFactor.Width = 125;
      clm_WeigthFactor.Frozen = true;

      gdv_Papers.Columns.Add(clm_PaperID);
      gdv_Papers.Columns.Add(clm_WeigthFactor);

      DBManager.getDBManager().paper.OnDBPaperChange += new Paper.DBChangedHandler(paper_RowChanged);

      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
      {
        this.lbl_CtrlF1_YY.Text = "";
        this.lbl_CtrlF2_YY.Text = "";
        this.lbl_CtrlF3_YY.Text = "";
        this.lbl_CtrlF4_YY.Text = "";
        this.lbl_CtrlF5_YY.Text = "";
      }

      //RFAZZI_FI07/06/2010_2. Aggiunta gestione per togliere a run time i parametri di velocità di giunta per le giuntatrici non presenti. 
      DBManager dbm;
      dbm = DBManager.getDBManager();

      tnud_Splice4.Visible = dbm.syncroConfig.SingleFacerGeneric2Setting.Enable;
      tnud_Splice5.Visible = dbm.syncroConfig.SingleFacerGeneric2Setting.Enable;
      tnud_Splice6.Visible = dbm.syncroConfig.SingleFacerGeneric3Setting.Enable;
      tnud_Splice7.Visible = dbm.syncroConfig.SingleFacerGeneric3Setting.Enable;

      //PRC
      if (SyncroC.Utility.Settings.getSettings().configuration.PRC)
      {
        this.tlst_PaperClassification.Visible = true;
        this.tlst_PaperClassification.AddRangeItem(SyncroC.DataBase.PRCManager.getPRCManager().generatePaperClassificationArray(false));
      }
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
        DataRow row = null;
        int FirstDisplayedScrollingRowIndex = this.gdv_Papers.FirstDisplayedScrollingRowIndex;

        if (this.gdv_Papers != null)
          if (this.gdv_Papers.SelectedRows != null)
            if (this.gdv_Papers.SelectedRows.Count != 0)
              row = ((DataRowView)this.gdv_Papers.SelectedRows[0].DataBoundItem).Row;

        this.gdv_Papers.SuspendLayout();

        this.gdv_Papers.DataSource = DBManager.getDBManager().paper.getTable();

        for (int i = 0; i < this.gdv_Papers.Rows.Count; i++)
        {
          if (row != null)
          {
            string a = ((DataRowView)this.gdv_Papers.Rows[i].DataBoundItem).Row[Paper.PAPER_ID].ToString().Trim() + ((DataRowView)this.gdv_Papers.Rows[i].DataBoundItem).Row[Paper.WEIGHT_FACTOR].ToString().Trim();
            string b = row[Paper.PAPER_ID].ToString().Trim() + row[Paper.WEIGHT_FACTOR].ToString().Trim();
            if (a == b)
            {
              this.gdv_Papers.Rows[i].Selected = true;
            }
            else
              this.gdv_Papers.Rows[i].Selected = false;
          }
        }

        if ((row == null) && (this.gdv_Papers.Rows.Count > 0))
          this.gdv_Papers.Rows[0].Selected = true;

        if ((FirstDisplayedScrollingRowIndex >= 0) && (FirstDisplayedScrollingRowIndex <= DBManager.getDBManager().paper.getCount()))
          this.gdv_Papers.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex;

        this.gdv_Papers.ResumeLayout();
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    void paper_RowChanged(object sender, DBChangedEventArgs args)
    {
      if (state.Equals(State.Read))
      {
        gdv_Papers_SelectionChanged(null, null);
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
      this.gdv_Papers.DataSource = null;
      DBManager.getDBManager().paper.OnDBPaperChange -= new Paper.DBChangedHandler(paper_RowChanged);
      base.Dispose(disposing);
    }

    #region Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Paper));
      this.ttl_PaperData = new SyncroC.Controls.Title();
      this.ttl_PapersList = new SyncroC.Controls.Title();
      this.ttb_PaperID = new SyncroC.Controls.TitledTextBox();
      this.ttl_spliceSpeed = new SyncroC.Controls.Title();
      this.tnud_CoreDiameter = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_WeightFactor = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_Splice1 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_Splice2 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_Splice3 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_Splice4 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_Splice5 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_Splice6 = new SyncroC.Controls.TitledNumericUpDown();
      this.tnud_Splice7 = new SyncroC.Controls.TitledNumericUpDown();
      this.brk_PaperDetails = new SyncroC.Controls.Bracket();
      this.gdv_Papers = new SyncroC.Controls.GridView();
      this.ttb_Text = new SyncroC.Controls.TitledTextBox();
      this.tnud_Thickness = new SyncroC.Controls.TitledNumericUpDown();
      this.tlst_PaperClassification = new SyncroC.Controls.TitledList();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).BeginInit();
      this.SuspendLayout();
      // 
      // lbl_Title_YN
      // 
      this.lbl_Title_YN.Text = "PAPER";
      // 
      // tab_Tab6
      // 
      this.tab_Tab6.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.tab_Tab6.TabSelection = SyncroC.Controls.TabSelectionType.Selected;
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
      // ttl_PaperData
      // 
      this.ttl_PaperData.BackColor = System.Drawing.Color.Black;
      this.ttl_PaperData.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PaperData.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PaperData.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_PaperData.Location = new System.Drawing.Point(330, 223);
      this.ttl_PaperData.Name = "ttl_PaperData";
      this.ttl_PaperData.Size = new System.Drawing.Size(871, 258);
      this.ttl_PaperData.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_PaperData.TabIndex = 106;
      this.ttl_PaperData.TabStop = false;
      this.ttl_PaperData.Text = "PAPER DATA";
      this.ttl_PaperData.TranslateText = true;
      // 
      // ttl_PapersList
      // 
      this.ttl_PapersList.BackColor = System.Drawing.Color.Black;
      this.ttl_PapersList.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_PapersList.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_PapersList.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_PapersList.Location = new System.Drawing.Point(30, 193);
      this.ttl_PapersList.Name = "ttl_PapersList";
      this.ttl_PapersList.Size = new System.Drawing.Size(253, 519);
      this.ttl_PapersList.Style = SyncroC.Controls.TitleStyles.Big;
      this.ttl_PapersList.TabIndex = 105;
      this.ttl_PapersList.TabStop = false;
      this.ttl_PapersList.Text = "PAPERS";
      this.ttl_PapersList.TranslateText = true;
      // 
      // ttb_PaperID
      // 
      this.ttb_PaperID.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_PaperID.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_PaperID.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_PaperID.ImagePassword = null;
      this.ttb_PaperID.Location = new System.Drawing.Point(340, 247);
      this.ttb_PaperID.MaxChar = 12;
      this.ttb_PaperID.Multiline = false;
      this.ttb_PaperID.Name = "ttb_PaperID";
      this.ttb_PaperID.PasswordChar = '\0';
      this.ttb_PaperID.ReadOnly = true;
      this.ttb_PaperID.Size = new System.Drawing.Size(230, 70);
      this.ttb_PaperID.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_PaperID.TabIndex = 10;
      this.ttb_PaperID.TabStop = false;
      this.ttb_PaperID.Text = "Paper ID";
      this.ttb_PaperID.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_PaperID.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_PaperID.TranslateText = true;
      this.ttb_PaperID.Value = " ";
      this.ttb_PaperID.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_PaperID.Validating += new System.ComponentModel.CancelEventHandler(this.ttb_PaperID_YN_Validating);
      // 
      // ttl_spliceSpeed
      // 
      this.ttl_spliceSpeed.BackColor = System.Drawing.Color.Black;
      this.ttl_spliceSpeed.color = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttl_spliceSpeed.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttl_spliceSpeed.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.ttl_spliceSpeed.Location = new System.Drawing.Point(330, 487);
      this.ttl_spliceSpeed.Name = "ttl_spliceSpeed";
      this.ttl_spliceSpeed.Size = new System.Drawing.Size(871, 194);
      this.ttl_spliceSpeed.Style = SyncroC.Controls.TitleStyles.Small;
      this.ttl_spliceSpeed.TabIndex = 117;
      this.ttl_spliceSpeed.TabStop = false;
      this.ttl_spliceSpeed.Text = "MAXIMUM SPLICE SPEED";
      this.ttl_spliceSpeed.TranslateText = true;
      // 
      // tnud_CoreDiameter
      // 
      this.tnud_CoreDiameter.DecimalPlaces = 0;
      this.tnud_CoreDiameter.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_CoreDiameter.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_CoreDiameter.ImagePassword = null;
      this.tnud_CoreDiameter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_CoreDiameter.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMilliMetre;
      this.tnud_CoreDiameter.Location = new System.Drawing.Point(842, 326);
      this.tnud_CoreDiameter.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
      this.tnud_CoreDiameter.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_CoreDiameter.Name = "tnud_CoreDiameter";
      this.tnud_CoreDiameter.ReadOnly = true;
      this.tnud_CoreDiameter.Size = new System.Drawing.Size(240, 70);
      this.tnud_CoreDiameter.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_CoreDiameter.TabIndex = 30;
      this.tnud_CoreDiameter.TabStop = false;
      this.tnud_CoreDiameter.Text = "Core Diameter";
      this.tnud_CoreDiameter.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_CoreDiameter.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_CoreDiameter.TranslateText = true;
      this.tnud_CoreDiameter.UnitMeasure = SyncroC.Utility.UnitMeasure.MilliMetre;
      this.tnud_CoreDiameter.Value = "0";
      this.tnud_CoreDiameter.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_CoreDiameter.ValueForeColor = System.Drawing.SystemColors.WindowText;
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
      this.tnud_WeightFactor.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor.Location = new System.Drawing.Point(340, 326);
      this.tnud_WeightFactor.Maximum = new decimal(new int[] {
            999,
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
      this.tnud_WeightFactor.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_WeightFactor.TabIndex = 15;
      this.tnud_WeightFactor.TabStop = false;
      this.tnud_WeightFactor.Text = "Weight Factor";
      this.tnud_WeightFactor.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_WeightFactor.TranslateText = true;
      this.tnud_WeightFactor.UnitMeasure = SyncroC.Utility.UnitMeasure.GramsPerM2;
      this.tnud_WeightFactor.Value = "0";
      this.tnud_WeightFactor.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_WeightFactor.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_Splice1
      // 
      this.tnud_Splice1.DecimalPlaces = 0;
      this.tnud_Splice1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Splice1.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice1.ImagePassword = null;
      this.tnud_Splice1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Splice1.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice1.Location = new System.Drawing.Point(340, 515);
      this.tnud_Splice1.Margin = new System.Windows.Forms.Padding(6);
      this.tnud_Splice1.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.tnud_Splice1.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Splice1.Name = "tnud_Splice1";
      this.tnud_Splice1.ReadOnly = true;
      this.tnud_Splice1.Size = new System.Drawing.Size(160, 70);
      this.tnud_Splice1.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Splice1.TabIndex = 40;
      this.tnud_Splice1.TabStop = false;
      this.tnud_Splice1.Text = "Splicer 1";
      this.tnud_Splice1.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice1.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice1.TranslateText = true;
      this.tnud_Splice1.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice1.Value = "0";
      this.tnud_Splice1.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice1.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_Splice2
      // 
      this.tnud_Splice2.DecimalPlaces = 0;
      this.tnud_Splice2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Splice2.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice2.ImagePassword = null;
      this.tnud_Splice2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Splice2.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice2.Location = new System.Drawing.Point(513, 515);
      this.tnud_Splice2.Margin = new System.Windows.Forms.Padding(7);
      this.tnud_Splice2.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.tnud_Splice2.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Splice2.Name = "tnud_Splice2";
      this.tnud_Splice2.ReadOnly = true;
      this.tnud_Splice2.Size = new System.Drawing.Size(160, 70);
      this.tnud_Splice2.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Splice2.TabIndex = 50;
      this.tnud_Splice2.TabStop = false;
      this.tnud_Splice2.Text = "Splicer 2";
      this.tnud_Splice2.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice2.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice2.TranslateText = true;
      this.tnud_Splice2.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice2.Value = "0";
      this.tnud_Splice2.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice2.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_Splice3
      // 
      this.tnud_Splice3.DecimalPlaces = 0;
      this.tnud_Splice3.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Splice3.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice3.ImagePassword = null;
      this.tnud_Splice3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Splice3.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice3.Location = new System.Drawing.Point(687, 515);
      this.tnud_Splice3.Margin = new System.Windows.Forms.Padding(7);
      this.tnud_Splice3.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.tnud_Splice3.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Splice3.Name = "tnud_Splice3";
      this.tnud_Splice3.ReadOnly = true;
      this.tnud_Splice3.Size = new System.Drawing.Size(160, 70);
      this.tnud_Splice3.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Splice3.TabIndex = 55;
      this.tnud_Splice3.TabStop = false;
      this.tnud_Splice3.Text = "Splicer 3";
      this.tnud_Splice3.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice3.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice3.TranslateText = true;
      this.tnud_Splice3.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice3.Value = "0";
      this.tnud_Splice3.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice3.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_Splice4
      // 
      this.tnud_Splice4.DecimalPlaces = 0;
      this.tnud_Splice4.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Splice4.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice4.ImagePassword = null;
      this.tnud_Splice4.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Splice4.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice4.Location = new System.Drawing.Point(862, 515);
      this.tnud_Splice4.Margin = new System.Windows.Forms.Padding(7);
      this.tnud_Splice4.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.tnud_Splice4.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Splice4.Name = "tnud_Splice4";
      this.tnud_Splice4.ReadOnly = true;
      this.tnud_Splice4.Size = new System.Drawing.Size(160, 70);
      this.tnud_Splice4.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Splice4.TabIndex = 60;
      this.tnud_Splice4.TabStop = false;
      this.tnud_Splice4.Text = "Splicer 4";
      this.tnud_Splice4.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice4.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice4.TranslateText = true;
      this.tnud_Splice4.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice4.Value = "0";
      this.tnud_Splice4.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice4.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_Splice5
      // 
      this.tnud_Splice5.DecimalPlaces = 0;
      this.tnud_Splice5.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Splice5.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice5.ImagePassword = null;
      this.tnud_Splice5.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Splice5.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice5.Location = new System.Drawing.Point(1036, 515);
      this.tnud_Splice5.Margin = new System.Windows.Forms.Padding(7);
      this.tnud_Splice5.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.tnud_Splice5.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Splice5.Name = "tnud_Splice5";
      this.tnud_Splice5.ReadOnly = true;
      this.tnud_Splice5.Size = new System.Drawing.Size(160, 70);
      this.tnud_Splice5.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Splice5.TabIndex = 65;
      this.tnud_Splice5.TabStop = false;
      this.tnud_Splice5.Text = "Splicer 5";
      this.tnud_Splice5.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice5.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice5.TranslateText = true;
      this.tnud_Splice5.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice5.Value = "0";
      this.tnud_Splice5.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice5.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_Splice6
      // 
      this.tnud_Splice6.DecimalPlaces = 0;
      this.tnud_Splice6.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Splice6.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice6.ImagePassword = null;
      this.tnud_Splice6.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Splice6.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice6.Location = new System.Drawing.Point(340, 611);
      this.tnud_Splice6.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.tnud_Splice6.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Splice6.Name = "tnud_Splice6";
      this.tnud_Splice6.ReadOnly = true;
      this.tnud_Splice6.Size = new System.Drawing.Size(160, 70);
      this.tnud_Splice6.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Splice6.TabIndex = 70;
      this.tnud_Splice6.TabStop = false;
      this.tnud_Splice6.Text = "Splicer 6";
      this.tnud_Splice6.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice6.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice6.TranslateText = true;
      this.tnud_Splice6.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice6.Value = "0";
      this.tnud_Splice6.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice6.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tnud_Splice7
      // 
      this.tnud_Splice7.DecimalPlaces = 0;
      this.tnud_Splice7.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tnud_Splice7.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice7.ImagePassword = null;
      this.tnud_Splice7.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.tnud_Splice7.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice7.Location = new System.Drawing.Point(513, 611);
      this.tnud_Splice7.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.tnud_Splice7.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.tnud_Splice7.Name = "tnud_Splice7";
      this.tnud_Splice7.ReadOnly = true;
      this.tnud_Splice7.Size = new System.Drawing.Size(160, 70);
      this.tnud_Splice7.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Splice7.TabIndex = 75;
      this.tnud_Splice7.TabStop = false;
      this.tnud_Splice7.Text = "Splicer 7";
      this.tnud_Splice7.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice7.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Splice7.TranslateText = true;
      this.tnud_Splice7.UnitMeasure = SyncroC.Utility.UnitMeasure.MetresPerMinute;
      this.tnud_Splice7.Value = "0";
      this.tnud_Splice7.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Splice7.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // brk_PaperDetails
      // 
      this.brk_PaperDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
      this.brk_PaperDetails.color = SyncroC.Controls.Colors.SyncroColor.Green;
      this.brk_PaperDetails.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.brk_PaperDetails.ForeColor = System.Drawing.Color.WhiteSmoke;
      this.brk_PaperDetails.Location = new System.Drawing.Point(294, 193);
      this.brk_PaperDetails.Name = "brk_PaperDetails";
      this.brk_PaperDetails.Position = SyncroC.Controls.BracketPosition.Right;
      this.brk_PaperDetails.ShowArrow = true;
      this.brk_PaperDetails.Size = new System.Drawing.Size(907, 519);
      this.brk_PaperDetails.TabIndex = 39;
      this.brk_PaperDetails.Text = "DETAILS";
      this.brk_PaperDetails.titleForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.brk_PaperDetails.TranslateText = true;
      // 
      // gdv_Papers
      // 
      this.gdv_Papers.AlternateBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
      this.gdv_Papers.AlternateForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Papers.AlternateSelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Papers.AlternateSelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Papers.AutoGenerateColumns = false;
      this.gdv_Papers.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gdv_Papers.ColumnSpacing = SyncroC.Controls.GridColumnSpacing.Medium;
      this.gdv_Papers.DataMember = "";
      this.gdv_Papers.DataSource = null;
      this.gdv_Papers.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.gdv_Papers.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Papers.ImagePassword = null;
      this.gdv_Papers.Location = new System.Drawing.Point(42, 222);
      this.gdv_Papers.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
      this.gdv_Papers.MultiSelect = true;
      this.gdv_Papers.Name = "gdv_Papers";
      this.gdv_Papers.RowsHeight = 30;
      this.gdv_Papers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.gdv_Papers.SelectionBackColor = SyncroC.Controls.Colors.SyncroColor.Green;
      this.gdv_Papers.SelectionForeColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Papers.Size = new System.Drawing.Size(240, 490);
      this.gdv_Papers.Style = SyncroC.Controls.GridStyles.Light;
      this.gdv_Papers.TabIndex = 149;
      this.gdv_Papers.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.gdv_Papers.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.gdv_Papers.TranslateText = true;
      this.gdv_Papers.SelectionChanged += new System.EventHandler(this.gdv_Papers_SelectionChanged);
      this.gdv_Papers.Leave += new System.EventHandler(this.gdv_Papers_Leave);
      this.gdv_Papers.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gdv_Papers_CellFormatting);
      // 
      // ttb_Text
      // 
      this.ttb_Text.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.ttb_Text.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ttb_Text.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Text.ImagePassword = null;
      this.ttb_Text.Location = new System.Drawing.Point(587, 247);
      this.ttb_Text.MaxChar = 12;
      this.ttb_Text.Multiline = false;
      this.ttb_Text.Name = "ttb_Text";
      this.ttb_Text.PasswordChar = '\0';
      this.ttb_Text.ReadOnly = true;
      this.ttb_Text.Size = new System.Drawing.Size(613, 70);
      this.ttb_Text.Style = SyncroC.Controls.TitledTextBoxStyles.Light;
      this.ttb_Text.TabIndex = 20;
      this.ttb_Text.TabStop = false;
      this.ttb_Text.Text = "Text";
      this.ttb_Text.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.ttb_Text.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.ttb_Text.TranslateText = true;
      this.ttb_Text.Value = " ";
      this.ttb_Text.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
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
      this.tnud_Thickness.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.DeciMicron;
      this.tnud_Thickness.Location = new System.Drawing.Point(587, 326);
      this.tnud_Thickness.Maximum = new decimal(new int[] {
            100000,
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
      this.tnud_Thickness.Size = new System.Drawing.Size(240, 70);
      this.tnud_Thickness.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
      this.tnud_Thickness.TabIndex = 25;
      this.tnud_Thickness.TabStop = false;
      this.tnud_Thickness.Text = "Thickness";
      this.tnud_Thickness.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Thickness.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tnud_Thickness.TranslateText = true;
      this.tnud_Thickness.UnitMeasure = SyncroC.Utility.UnitMeasure.Micron;
      this.tnud_Thickness.Value = "0";
      this.tnud_Thickness.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tnud_Thickness.ValueForeColor = System.Drawing.SystemColors.WindowText;
      // 
      // tlst_PaperClassification
      // 
      this.tlst_PaperClassification.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tlst_PaperClassification.CanCancel = false;
      this.tlst_PaperClassification.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tlst_PaperClassification.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlst_PaperClassification.ImagePassword = null;
      this.tlst_PaperClassification.Location = new System.Drawing.Point(1094, 326);
      this.tlst_PaperClassification.Name = "tlst_PaperClassification";
      this.tlst_PaperClassification.ReadOnly = true;
      this.tlst_PaperClassification.SelectedIndex = -1;
      this.tlst_PaperClassification.SelectedItem = "";
      this.tlst_PaperClassification.SelectedItemData = "";
      this.tlst_PaperClassification.Size = new System.Drawing.Size(106, 64);
      this.tlst_PaperClassification.Sorted = true;
      this.tlst_PaperClassification.Style = SyncroC.Controls.TitledListStyles.Light;
      this.tlst_PaperClassification.TabIndex = 150;
      this.tlst_PaperClassification.TabStop = false;
      this.tlst_PaperClassification.Text = "Class.";
      this.tlst_PaperClassification.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlst_PaperClassification.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
      this.tlst_PaperClassification.TranslateText = true;
      this.tlst_PaperClassification.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.White;
      this.tlst_PaperClassification.Visible = false;
      // 
      // Frm_Paper
      // 
      this.ClientSize = new System.Drawing.Size(1280, 738);
      this.Controls.Add(this.tlst_PaperClassification);
      this.Controls.Add(this.tnud_Thickness);
      this.Controls.Add(this.ttb_Text);
      this.Controls.Add(this.gdv_Papers);
      this.Controls.Add(this.brk_PaperDetails);
      this.Controls.Add(this.tnud_Splice7);
      this.Controls.Add(this.tnud_Splice6);
      this.Controls.Add(this.tnud_Splice5);
      this.Controls.Add(this.tnud_Splice4);
      this.Controls.Add(this.tnud_Splice3);
      this.Controls.Add(this.tnud_Splice2);
      this.Controls.Add(this.tnud_Splice1);
      this.Controls.Add(this.tnud_WeightFactor);
      this.Controls.Add(this.tnud_CoreDiameter);
      this.Controls.Add(this.ttl_spliceSpeed);
      this.Controls.Add(this.ttb_PaperID);
      this.Controls.Add(this.ttl_PaperData);
      this.Controls.Add(this.ttl_PapersList);
      this.Name = "Frm_Paper";
      this.Tag = "";
      this.Text = "Frm_Paper";
      this.Activated += new System.EventHandler(this.Frm_Paper_Activated);
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
      this.Controls.SetChildIndex(this.ttl_PapersList, 0);
      this.Controls.SetChildIndex(this.ttl_PaperData, 0);
      this.Controls.SetChildIndex(this.ttb_PaperID, 0);
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
      this.Controls.SetChildIndex(this.ttl_spliceSpeed, 0);
      this.Controls.SetChildIndex(this.tnud_CoreDiameter, 0);
      this.Controls.SetChildIndex(this.tnud_WeightFactor, 0);
      this.Controls.SetChildIndex(this.tnud_Splice1, 0);
      this.Controls.SetChildIndex(this.tnud_Splice2, 0);
      this.Controls.SetChildIndex(this.tnud_Splice3, 0);
      this.Controls.SetChildIndex(this.tnud_Splice4, 0);
      this.Controls.SetChildIndex(this.tnud_Splice5, 0);
      this.Controls.SetChildIndex(this.tnud_Splice6, 0);
      this.Controls.SetChildIndex(this.tnud_Splice7, 0);
      this.Controls.SetChildIndex(this.brk_PaperDetails, 0);
      this.Controls.SetChildIndex(this.gdv_Papers, 0);
      this.Controls.SetChildIndex(this.ttb_Text, 0);
      this.Controls.SetChildIndex(this.tnud_Thickness, 0);
      this.Controls.SetChildIndex(this.tlst_PaperClassification, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pct_Home_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Back_NY)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pct_Forward_NY)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    /// <summary>
    /// This page of the group must do nothing on F6.
    /// </summary>
    protected override void F6()
    {
    }

    /// <summary>
    /// CtrlF1 key behaviour: New Paper
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

      if (gdv_Papers.Focused)
      {
        DataRowCollection rows = DBManager.getDBManager().paper.getTable().Rows;
        if (rows.Count > 0)
        {
          string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_38");
          Frm_Confirm conf = new Frm_Confirm();
          if (gdv_Papers.SelectedRows.Count != 0 &&
            conf.ShowDialog(this, text).Equals(DialogResult.OK))
          {
            CommunicationManager cm = CommunicationManager.GetCommunicationManager();
            foreach (DataGridViewRow row in gdv_Papers.SelectedRows)
            {
              cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_PAPER + "." + Command.DB_CMD_DEL
                + "(" + ((DataRowView)row.DataBoundItem).Row[Paper.PAPER_ID]
                + Command.PARAMETER_SEPARATOR
                + ((DataRowView)row.DataBoundItem).Row[Paper.WEIGHT_FACTOR]
                + ")");
            }
          }
        }
        SetPageStatus(State.Read);
      }
    }

    /// <summary>
    /// CtrlF3 key behaviour: Delete all DB papers
    /// </summary>
    protected override void CtrlF3()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      if (gdv_Papers.Rows.Count > 0 && state.Equals(State.Read))
      {
        Frm_Confirm confirm = new Frm_Confirm();
        string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_30");
        if (confirm.ShowDialog(this, text).Equals(DialogResult.OK))
        {
          CommunicationManager cm = CommunicationManager.GetCommunicationManager();
          cm.SendTcp(cm.SYNCROS_DB_MANAGER, Command.DB_PAPER + "." + Command.DB_CMD_CLR + "()");
        }
      }
      SetPageStatus(State.Read);
    }

    /// <summary>
    /// CtrlF4 key behaviour: Save a paper
    /// </summary>
    protected override void CtrlF4()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      if (ttb_PaperID.Value.Trim() == "")
      {
        PageManager.getPageManager()._SetMessage("§SYCC_10", true);
      }
      else
      {
        string toSend = Command.DB_PAPER + ".";
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
            ttb_PaperID.Value + Command.PARAMETER_SEPARATOR +
            tnud_CoreDiameter.Value + Command.PARAMETER_SEPARATOR +
            tnud_WeightFactor.Value + Command.PARAMETER_SEPARATOR +
            ttb_Text.Value + Command.PARAMETER_SEPARATOR +
            tnud_Thickness.Value + Command.PARAMETER_SEPARATOR +
            this.NotEmptyString(this.tlst_PaperClassification.Value) + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            tnud_Splice1.Value + Command.PARAMETER_SEPARATOR +
            tnud_Splice2.Value + Command.PARAMETER_SEPARATOR +
            tnud_Splice3.Value + Command.PARAMETER_SEPARATOR +
            tnud_Splice4.Value + Command.PARAMETER_SEPARATOR +
            tnud_Splice5.Value + Command.PARAMETER_SEPARATOR +
            tnud_Splice6.Value + Command.PARAMETER_SEPARATOR +
            tnud_Splice7.Value + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" + Command.PARAMETER_SEPARATOR +
            "SPARE" +
            ")");
        }
        if (this.state == State.Modify)
          this.ManageQualityChangePerPapers();
      }
      SetPageStatus(State.Read);
    }

    /// <summary>
    /// CtrlF5 key behaviour: Modify a paper
    /// </summary>
    protected override void CtrlF5()
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.EditSetupQueue)
        return;

      if (gdv_Papers.Rows.Count <= 0)
        return;

      if (state == State.Insert)
        return;

      SetPageStatus(State.Modify);
    }

    private void ManageQualityChangePerPapers()
    {
      PopUp.Frm_Confirm conf = new Frm_Confirm();
      string text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_140");
      

      //DialogResult res = conf.ShowDialog(this, text);
      DialogResult res = DialogResult.Yes;
      
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
          DataTable quality = DBManager.getDBManager().quality.getTable();
          for (int i = 0; i < quality.Rows.Count; i++)
          {
            bool updatePaper0 = (quality.Rows[i][Quality.PAPER_ID_0].Equals(this.ttb_PaperID.Value)
              && quality.Rows[i][Quality.WEIGHT_FACTOR_0].ToString().Equals(this.tnud_WeightFactor.Value));
            bool updatePaper1 = (quality.Rows[i][Quality.PAPER_ID_1].Equals(this.ttb_PaperID.Value)
              && quality.Rows[i][Quality.WEIGHT_FACTOR_1].ToString().Equals(this.tnud_WeightFactor.Value));
            bool updatePaper2 = (quality.Rows[i][Quality.PAPER_ID_2].Equals(this.ttb_PaperID.Value)
              && quality.Rows[i][Quality.WEIGHT_FACTOR_2].ToString().Equals(this.tnud_WeightFactor.Value));
            bool updatePaper3 = (quality.Rows[i][Quality.PAPER_ID_3].Equals(this.ttb_PaperID.Value)
              && quality.Rows[i][Quality.WEIGHT_FACTOR_3].ToString().Equals(this.tnud_WeightFactor.Value));
            bool updatePaper4 = (quality.Rows[i][Quality.PAPER_ID_4].Equals(this.ttb_PaperID.Value)
              && quality.Rows[i][Quality.WEIGHT_FACTOR_4].ToString().Equals(this.tnud_WeightFactor.Value));
            bool updatePaper5 = (quality.Rows[i][Quality.PAPER_ID_5].Equals(this.ttb_PaperID.Value)
              && quality.Rows[i][Quality.WEIGHT_FACTOR_5].ToString().Equals(this.tnud_WeightFactor.Value));
            bool updatePaper6 = (quality.Rows[i][Quality.PAPER_ID_6].Equals(this.ttb_PaperID.Value)
              && quality.Rows[i][Quality.WEIGHT_FACTOR_6].ToString().Equals(this.tnud_WeightFactor.Value));

            if (updatePaper0
              || updatePaper1
              || updatePaper2
              || updatePaper3
              || updatePaper4
              || updatePaper5
              || updatePaper6)
            {
              string toSend = Command.DB_QUALITY + "." + Command.DB_CMD_MOD;

              //PMASSARO_FI18/01/10 - Non modifichiamo più la pressione di cordonatura impostata nell'ordine se non espressamente richiesto
              /*
              if (updateSetups)
                toSend += "WithSetup";
              */
              if (updateSetups)
                toSend += "WithSetup_NoPressure";

              toSend += "(";
              toSend += this.NotEmptyString(quality.Rows[i][Quality.QUALITY_ID].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.QUALITY_TEXT].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.FLUTE_ID].ToString()) + Command.PARAMETER_SEPARATOR;
              //DRY DATA
              toSend += this.NotEmptyString(quality.Rows[i][Quality.SCOREGAP_UDdmm].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.GRINDING_PARAMETER_UDm].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.LUBRICATION_PARAMETER_UDm].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.TRIM_GRINDINGS_MULTIPLIER].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.GRINDING_DISTANCE_UDm].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.LUBRICATION_DURATION_UTms].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PRODUCTION_SPEED_UVm_m].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.AOC_SPEED_UVm_m].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.THICKNESS_UDdmm].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += "0" + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.BUNDLE_LAYERS_PER_STACK].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.BUNDLE_REVERSE_MODE].ToString()) + Command.PARAMETER_SEPARATOR;

              if (updatePaper0)
              {
                toSend += this.NotEmptyString(ttb_PaperID.Value) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(tnud_WeightFactor.Value) + Command.PARAMETER_SEPARATOR;
              }
              else
              {
                toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_0].ToString()) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_0].ToString()) + Command.PARAMETER_SEPARATOR;
              }
              if (updatePaper1)
              {
                toSend += this.NotEmptyString(ttb_PaperID.Value) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(tnud_WeightFactor.Value) + Command.PARAMETER_SEPARATOR;
              }
              else
              {
                toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_1].ToString()) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_1].ToString()) + Command.PARAMETER_SEPARATOR;
              }
              if (updatePaper2)
              {
                toSend += this.NotEmptyString(ttb_PaperID.Value) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(tnud_WeightFactor.Value) + Command.PARAMETER_SEPARATOR;
              }
              else
              {
                toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_2].ToString()) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_2].ToString()) + Command.PARAMETER_SEPARATOR;
              }
              if (updatePaper3)
              {
                toSend += this.NotEmptyString(ttb_PaperID.Value) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(tnud_WeightFactor.Value) + Command.PARAMETER_SEPARATOR;
              }
              else
              {
                toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_3].ToString()) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_3].ToString()) + Command.PARAMETER_SEPARATOR;
              }
              if (updatePaper4)
              {
                toSend += this.NotEmptyString(ttb_PaperID.Value) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(tnud_WeightFactor.Value) + Command.PARAMETER_SEPARATOR;
              }
              else
              {
                toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_4].ToString()) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_4].ToString()) + Command.PARAMETER_SEPARATOR;
              }
              if (updatePaper5)
              {
                toSend += this.NotEmptyString(ttb_PaperID.Value) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(tnud_WeightFactor.Value) + Command.PARAMETER_SEPARATOR;
              }
              else
              {
                toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_5].ToString()) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_5].ToString()) + Command.PARAMETER_SEPARATOR;
              }
              if (updatePaper6)
              {
                toSend += this.NotEmptyString(ttb_PaperID.Value) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(tnud_WeightFactor.Value) + Command.PARAMETER_SEPARATOR;
              }
              else
              {
                toSend += this.NotEmptyString(quality.Rows[i][Quality.PAPER_ID_6].ToString()) + Command.PARAMETER_SEPARATOR;
                toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR_6].ToString()) + Command.PARAMETER_SEPARATOR;
              }
              toSend += this.NotEmptyString(quality.Rows[i][Quality.WEIGHT_FACTOR].ToString()) + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.PREPRINT].ToString()) + Command.PARAMETER_SEPARATOR;
              //PMASSARO_FI15/11/08 - Used To Inclinate Last Belt On Terminal400
              //toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.INCLINATELASTBELT].ToString()) + Command.PARAMETER_SEPARATOR;
              //PMASSARO_FI09/05/09 - Bug Fix. The Internal Flap Check Was Resetted After Paper Parameters Modify
              //toSend += "SPARE" + Command.PARAMETER_SEPARATOR;
              toSend += this.NotEmptyString(quality.Rows[i][Quality.TWIN400_INTERNAL_FLAP_DISABLED].ToString()) + Command.PARAMETER_SEPARATOR;
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

    private void readOnly(bool isReadOnly)
    {
      //tab
      tnud_CoreDiameter.TabStop = this.tlst_PaperClassification.TabStop = ttb_Text.TabStop = tnud_Thickness.TabStop = tnud_Splice1.TabStop =
      tnud_Splice2.TabStop = tnud_Splice3.TabStop = tnud_Splice4.TabStop = tnud_Splice5.TabStop = tnud_Splice6.TabStop =
      tnud_Splice7.TabStop = !isReadOnly;

      //readonly
      tnud_CoreDiameter.ReadOnly = this.tlst_PaperClassification.ReadOnly = ttb_Text.ReadOnly = tnud_Thickness.ReadOnly = tnud_Splice1.ReadOnly =
      tnud_Splice2.ReadOnly = tnud_Splice3.ReadOnly = tnud_Splice4.ReadOnly = tnud_Splice5.ReadOnly = tnud_Splice6.ReadOnly =
      tnud_Splice7.ReadOnly = isReadOnly;
    }

    /// <summary>
    /// Escape key behaviour: Abort the operation
    /// </summary>
    protected override void Escape()
    {
      SetPageStatus(State.Read);
    }

    private void gdv_Papers_SelectionChanged(object sender, EventArgs e)
    {
      if (gdv_Papers.Rows.Count > 0 && gdv_Papers.SelectedRows.Count == 1)
      {
        DataRow row = ((DataRowView)gdv_Papers.SelectedRows[0].DataBoundItem).Row;
        if (row != null && row.RowState != DataRowState.Detached)
        {
          try
          {
            //RFAZZI_FI07/06/2010_2. Aggiunta gestione per togliere a run time i parametri di velocità di giunta per le giuntatrici non presenti. 
            DBManager dbm;
            dbm = DBManager.getDBManager();

            ttb_PaperID.Value = row[Paper.PAPER_ID].ToString();
            tnud_CoreDiameter.Value = row[Paper.CORE_DIAMETER].ToString();
            tnud_WeightFactor.Value = row[Paper.WEIGHT_FACTOR].ToString();
            ttb_Text.Value = row[Paper.TEXT].ToString();
            tnud_Thickness.Value = row[Paper.DEFAULT_THICKNESS].ToString();
            tnud_Splice1.Value = row[Paper.MAX_SPLICE_SPEED_0].ToString();
            tnud_Splice2.Value = row[Paper.MAX_SPLICE_SPEED_1].ToString();
            tnud_Splice3.Value = row[Paper.MAX_SPLICE_SPEED_2].ToString();

            //RFAZZI_FI07/06/2010_2. Aggiunta gestione per togliere a run time i parametri di velocità di giunta per le giuntatrici non presenti. 
            if (dbm.syncroConfig.SingleFacerGeneric2Setting.Enable)
            {
              tnud_Splice4.Value = row[Paper.MAX_SPLICE_SPEED_3].ToString();
              tnud_Splice5.Value = row[Paper.MAX_SPLICE_SPEED_4].ToString();
            }

            //RFAZZI_FI07/06/2010_2. Aggiunta gestione per togliere a run time i parametri di velocità di giunta per le giuntatrici non presenti. 
            if (dbm.syncroConfig.SingleFacerGeneric3Setting.Enable)
            {
              tnud_Splice6.Value = row[Paper.MAX_SPLICE_SPEED_5].ToString();
              tnud_Splice7.Value = row[Paper.MAX_SPLICE_SPEED_6].ToString();
            }
            this.tlst_PaperClassification.SelectedItem = row[Paper.PAPER_CLASSIFICATION].ToString();
          }
          catch (RowNotInTableException ex)
          {
            Logger.LogError(this, "gdv_Orders_SelectionChanged - the rowState is not Detached (it is \"" + row.RowState.ToString() + "\" but the RowNotInTableException has been thrown anyway....", ex);
          }
        }
      }
      else
      {
        CleanPaperDetails();
      }
    }

    private void CleanPaperDetails()
    {
      ttb_PaperID.Value = ttb_Text.Value = "";
      tnud_CoreDiameter.Value = tnud_WeightFactor.Value = tnud_Thickness.Value = "0";
      
      //RFAZZI_FI07/06/2010_2. Aggiunta gestione per togliere a run time i parametri di velocità di giunta per le giuntatrici non presenti. 
      //tnud_Splice1.Value = tnud_Splice2.Value = tnud_Splice3.Value = tnud_Splice4.Value = tnud_Splice5.Value =
      tnud_Splice1.Value = tnud_Splice2.Value = tnud_Splice3.Value = "0";

      DBManager dbm;
      dbm = DBManager.getDBManager();
      if (dbm.syncroConfig.SingleFacerGeneric2Setting.Enable)
      {
        tnud_Splice4.Value = tnud_Splice5.Value = "0";
      }
      if (dbm.syncroConfig.SingleFacerGeneric3Setting.Enable)
      {
        tnud_Splice6.Value = tnud_Splice7.Value = "0";
      }
    }

    private void CleanErrorProvider()
    {
      ttb_PaperID.SetError(String.Empty);
    }

    private void ttb_PaperID_YN_Validating(object sender, CancelEventArgs e)
    {
      if (ttb_PaperID.Value.Trim() == "")
      {
        string text = PageManager.getPageManager()._SetMessage("§SYCC_02", true);
        ttb_PaperID.SetError(text);
      }
      else
        ttb_PaperID.SetError("");
    }

    private void Frm_Paper_Activated(object sender, EventArgs e)
    {
      //RFAZZI_FI07/06/2010_2. Aggiunta gestione per togliere a run time i parametri di velocità di giunta per le giuntatrici non presenti. 
      DBManager dbm;
      dbm = DBManager.getDBManager();

      tnud_Splice4.Visible = dbm.syncroConfig.SingleFacerGeneric2Setting.Enable;
      tnud_Splice5.Visible = dbm.syncroConfig.SingleFacerGeneric2Setting.Enable;
      tnud_Splice6.Visible = dbm.syncroConfig.SingleFacerGeneric3Setting.Enable;
      tnud_Splice7.Visible = dbm.syncroConfig.SingleFacerGeneric3Setting.Enable;

      if (this.state == State.Read)
      {
        gdv_Papers.Focus();
      }
    }

    private void gdv_Papers_Leave(object sender, EventArgs e)
    {
      if (state.Equals(State.Read))
        gdv_Papers.Focus();
    }

    private void SetPageStatus(State newState)
    {
      if (!state.Equals(newState))
      {
        state = newState;
        switch (newState)
        {
          case State.Insert:
            CleanPaperDetails();
            foreach (DataGridViewRow row in gdv_Papers.Rows)
              row.Selected = false;
            ttb_PaperID.TabStop = true;
            ttb_PaperID.ReadOnly = false;
            tnud_WeightFactor.TabStop = true;
            tnud_WeightFactor.ReadOnly = false;
            readOnly(false);
            lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor = lbl_Status.BackColor;
            gdv_Papers.Enabled = false;
            ttb_PaperID.Focus();
            break;

          case State.Modify:
            readOnly(false);
            lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_Status.BackColor;
            gdv_Papers.Enabled = false;
            tnud_CoreDiameter.Focus();
            break;

          case State.Read:
            CleanErrorProvider();
            ttb_PaperID.TabStop = false;
            ttb_PaperID.ReadOnly = true;
            tnud_WeightFactor.TabStop = false;
            tnud_WeightFactor.ReadOnly = true;
            readOnly(true);
            lbl_CtrlF5.BackColor = lbl_CtrlF5_YY.BackColor = lbl_CtrlF1.BackColor = lbl_CtrlF1_YY.BackColor = lbl_Title_YN.BackColor;
            gdv_Papers.Enabled = true;
            if (gdv_Papers.SelectedRows.Count == 0 && gdv_Papers.Rows.Count > 0)
              gdv_Papers.Rows[0].Selected = true;
            else
              gdv_Papers_SelectionChanged(null, null);
            gdv_Papers.Focus();
            break;
        }
      }
    }

    private void gdv_Papers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (SyncroC.Utility.Settings.getSettings().configuration.PRC)
      {
        DataRow dataRow = ((DataRowView)gdv_Papers.Rows[e.RowIndex].DataBoundItem).Row;

        if (dataRow != null)
        {
          if (dataRow[Paper.PAPER_ID].ToString().Trim() != "NOT_USED" && ((dataRow[Paper.PAPER_CLASSIFICATION] == null) || dataRow[Paper.PAPER_CLASSIFICATION].ToString().Trim() == ""))
          {
            gdv_Papers.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            gdv_Papers.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Violet;
          }
        }
      }
    }
  }
}

