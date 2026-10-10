using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase;
using SyncroC.Utility;

namespace SyncroC.Controls.GroupPanel
{
  /// <summary>
  /// Delegate to use for OnIncDecButtonKitDown.
  /// </summary>
  public delegate void LateralBarItemSelectedIndexChanged(object sender, EventArgs args);

  public partial class LateralBarItem : SyncroC.Controls.SyncroUserControl
  {
    //agrassi_FI08-09-2010_1
    private SyncroC.Controls.Tools.MachineTools.ChangeShaftFunction changeFunction;
    private SyncroC.Controls.Tools.MachineTools.UpdateValuesFunction updateFunction;
    private SyncroC.Forms.Pages.Twin400.Status.Frm_twin400_blades frm_blade;
    //agrassi_FI22-08-2011_1
    private SyncroC.Forms.Pages.SlitterScorer.Status.Frm_SlitterScorerBlades frm_bladeOldSS;
    private SyncroC.DataBase.LineConfig.Knife knife;

    private bool toolDetailConfigured = false;
    /// <summary>
    /// Event raised every interval time when one of the button is down.
    /// </summary>
    [Description("Event raised when Lateral Bar Item Selected Index Changed."), Category("Fosbersoft")]
    public event LateralBarItemSelectedIndexChanged OnLateralBarItemSelectedIndexChanged;

    private int TIME_OUT_PER_UPDATE = 5;

    public LateralBarItem()
    {
      InitializeComponent();

      Settings s = Settings.getSettings();
      if (!s.forms.CorrugatorDirectionRight)
        CorrugatorDirectionLeft();

      DBManager dbm = DBManager.getDBManager();
      knife = dbm.lineConfig.KnifeConfig;

      if (knife.LevelCount == 1)
      {//valorizzazione con ordine
        this.tlb_UpperFormat.Visible = false;
        this.lbl_UpperFormatDetail1.Visible = false;
        this.lbl_UpperFormatDetail2.Visible = false;
        this.tlb_MiddleFormat.Visible = false;
        this.lbl_MiddleFormatDetail1.Visible = false;
        this.lbl_MiddleFormatDetail2.Visible = false;

        tlp_CurrentOrderDetails.RowCount = tlp_CurrentOrderDetails.RowCount - 6;
        tlp_CurrentOrderDetails.SetRow(this.tlb_LowerFormat, 1);
        tlp_CurrentOrderDetails.SetRow(this.lbl_LowerFormatDetail1, 2);
        tlp_CurrentOrderDetails.SetRow(this.lbl_LowerFormatDetail2, 3);

      }

      if (knife.LevelCount == 2)
      {//valorizzazione con ordine
        this.tlb_MiddleFormat.Visible = false;
        this.lbl_MiddleFormatDetail1.Visible = false;
        this.lbl_MiddleFormatDetail2.Visible = false;

        tlp_CurrentOrderDetails.RowCount = tlp_CurrentOrderDetails.RowCount - 3;

        tlp_CurrentOrderDetails.SetRow(this.tlb_LowerFormat, 4);
        tlp_CurrentOrderDetails.SetRow(this.lbl_LowerFormatDetail1, 5);
        tlp_CurrentOrderDetails.SetRow(this.lbl_LowerFormatDetail2, 6);

      }
    }

    public string getSelectedIndex()
    {
      return this.virtualForm.SelectedTab.Text;
    }

    public void setUpLateralBarItem()
    {

      DBManager dbm = DBManager.getDBManager();
      
      try
      {
        this.lbl_LowLevelSF1.Image = global::SyncroC.Properties.Resources.smart_01_bis;
        this.lbl_LowLevelSF1.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

        this.lbl_LowLevelSF2.Image = global::SyncroC.Properties.Resources.smart_01_bis;
        this.lbl_LowLevelSF2.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

        this.lbl_LowLevelPHTGU.Image = global::SyncroC.Properties.Resources.crest_01_bis;
        this.lbl_LowLevelPHTGU.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

        this.chk_TraceSpeed_SF2.Image = global::SyncroC.Properties.Resources.smart_01_bis;
        this.chk_TraceSpeed_SF2.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

        this.chk_TraceSpeed_SF1.Image = global::SyncroC.Properties.Resources.smart_01_bis;
        this.chk_TraceSpeed_SF1.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

        this.chk_TraceSpeed_PHTGU.Image = global::SyncroC.Properties.Resources.crest_01_bis;
        this.chk_TraceSpeed_PHTGU.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

        this.chk_TraceTemp_SF2.Image = global::SyncroC.Properties.Resources.smart_01_bis;
        this.chk_TraceTemp_SF2.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

        this.chk_TraceTemp_SF1.Image = global::SyncroC.Properties.Resources.smart_01_bis;
        this.chk_TraceTemp_SF1.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

        this.chk_TraceTemp_PHTGU.Image = global::SyncroC.Properties.Resources.crest_01_bis;
        this.chk_TraceTemp_PHTGU.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

        this.chk_TraceGlue_SF2.Image = global::SyncroC.Properties.Resources.smart_01_bis;
        this.chk_TraceGlue_SF2.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

        this.chk_TraceGlue_SF1.Image = global::SyncroC.Properties.Resources.smart_01_bis;
        this.chk_TraceGlue_SF1.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

        this.chk_TraceGlue_PHTGU.Image = global::SyncroC.Properties.Resources.crest_01_bis;
        this.chk_TraceGlue_PHTGU.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        if (dbm.lineConfig.SingleFacer3)
        {
          this.lbl_LowLevelSF3.Image = global::SyncroC.Properties.Resources.smart_01_bis;
          this.lbl_LowLevelSF3.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

          this.chk_TraceSpeed_SF3.Image = global::SyncroC.Properties.Resources.smart_01_bis;
          this.chk_TraceSpeed_SF3.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

          this.chk_TraceTemp_SF3.Image = global::SyncroC.Properties.Resources.smart_01_bis;
          this.chk_TraceTemp_SF3.Image.RotateFlip(RotateFlipType.Rotate270FlipX);

          this.chk_TraceGlue_SF3.Image = global::SyncroC.Properties.Resources.smart_01_bis;
          this.chk_TraceGlue_SF3.Image.RotateFlip(RotateFlipType.Rotate270FlipX);
        }

       
        //DRY
        if (!Settings.getSettings().configuration.DryEndControl)
        {
          this.contextMenuStrip_SceltaTab.Items[0].Visible = false;
          this.virtualForm.TabPages.RemoveByKey("tab_DRYList");
          this.virtualForm.TabPages.RemoveByKey("tab_SHFRunStop");
          this.virtualForm.TabPages.RemoveByKey("tab_DRYBlades");
          this.virtualForm.TabPages.RemoveByKey("tab_CurrentOrderDetails");
        }
        else
        {
          //Orders List
          this.dRYToolStripMenuItem.DropDownItems[0].Text = DBManager.getDBManager().language.getTranslation("Frm_DryOrdersList.lbl_DryEndTitle.Text");
          //Shift Stop e Run Time
          this.dRYToolStripMenuItem.DropDownItems[1].Text = DBManager.getDBManager().language.getTranslation("Frm_ShiftReport.tlb_RunTime.Text");
          this.lbl_IdleShift.Text = DBManager.getDBManager().language.getTranslation("Frm_ShiftReport.lbl_CtrlF2_YY.Text");
          //Coltelli twin
          this.dRYToolStripMenuItem.DropDownItems[2].Text = DBManager.getDBManager().language.getTranslation("Frm_SlitterScorerStatusSubgroup.tab_TabC.Text");
          //agrassi_FI08-09-2010_1
          //agrassi_FI22-08-2011_1
          if (SyncroC.DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsTwin2() ||
              SyncroC.DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsCompact2())
          {
            this.frm_bladeOldSS = new SyncroC.Forms.Pages.SlitterScorer.Status.Frm_SlitterScorerBlades(this.toolDetail, this.GrindSlit, this.lab_Grinding);
            this.GrindSlit.Click += new System.EventHandler(this.frm_bladeOldSS.GrindSlit_Click);
            this.GrindSlit.Text = DBManager.getDBManager().language.getTranslation("Frm_SlitterScorerBlades.GrindSlit.Text");
            this.changeFunction = new SyncroC.Controls.Tools.MachineTools.ChangeShaftFunction(this.frm_bladeOldSS.ChangeShaft);
          }
          else /* if (SyncroC.DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsTwin400()) */
          {
            this.frm_blade = new SyncroC.Forms.Pages.Twin400.Status.Frm_twin400_blades(this.toolDetail, this.GrindSlit, this.lab_Grinding, this.EnableGrinding);
            this.GrindSlit.Click += new System.EventHandler(this.frm_blade.GrindSlit_Click);
            this.StopMultiple.Click += new System.EventHandler(this.frm_blade.StopMultiple_Click);
            this.EnableGrinding.Click += new System.EventHandler(this.frm_blade.OnButtonPush);
            this.GrindSlit.Text = DBManager.getDBManager().language.getTranslation("Frm_twin400_blades.GrindSlit.Text");
            this.StopMultiple.Text = DBManager.getDBManager().language.getTranslation("Frm_twin400_blades.StopMultiple.Text");
            this.EnableGrinding.Text = DBManager.getDBManager().language.getTranslation("Frm_twin400_blades.EnableGrinding.Text");
            this.changeFunction = new SyncroC.Controls.Tools.MachineTools.ChangeShaftFunction(this.frm_blade.ChangeShaft);
            this.updateFunction = new SyncroC.Controls.Tools.MachineTools.UpdateValuesFunction(this.frm_blade.UpdateTempBrokenSlits);
          }
          //CurrentOrderDetails
          this.dRYToolStripMenuItem.DropDownItems[3].Text = DBManager.getDBManager().message.getTranslatedMessage("§SYCC_190");
        }

        //WET
        if (!Settings.getSettings().configuration.WetEndControl)
        {
          this.contextMenuStrip_SceltaTab.Items[1].Visible = false;
          this.virtualForm.TabPages.RemoveByKey("tab_LowLevelSF2");
          this.virtualForm.TabPages.RemoveByKey("tab_LowLevelSF1");
          this.virtualForm.TabPages.RemoveByKey("tab_LowLevelPHTGU");
          this.virtualForm.TabPages.RemoveByKey("tab_HomeWetEnd");
          //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
          this.virtualForm.TabPages.RemoveByKey("tab_LowLevelSF3");
        }
        else
        {
          //RFAZZI_FI16/12/2010_2. Aggiunta inizializzazione dei valori Min/Max dei componenti sulla LateralBar. 
          this.PHTGU_glueGap1.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG1.GlueGapRegulation.MinVal;
          this.PHTGU_glueGap1.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG1.GlueGapRegulation.MaxVal;
          this.PHTGU_glueGap2.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG2.GlueGapRegulation.MinVal;
          this.PHTGU_glueGap2.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.GlueUnitConfig.GluegapG2.GlueGapRegulation.MaxVal;

          this.SF1_glueGap11.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.SingleFacer1Config.Gluegap1.GlueGapRegulation.MinVal;
          this.SF1_glueGap11.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.SingleFacer1Config.Gluegap1.GlueGapRegulation.MaxVal;

          this.SF2_glueGap21.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.SingleFacer2Config.Gluegap1.GlueGapRegulation.MinVal;
          this.SF2_glueGap21.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.SingleFacer2Config.Gluegap1.GlueGapRegulation.MaxVal;

          this.SF3_glueGap31.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.SingleFacer3Config.Gluegap1.GlueGapRegulation.MinVal;
          this.SF3_glueGap31.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.SingleFacer3Config.Gluegap1.GlueGapRegulation.MaxVal;

          this.SPL_PaperTension1.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer1Config.PaperTension.PaperTensionRegulation.MinVal;
          this.SPL_PaperTension1.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer1Config.PaperTension.PaperTensionRegulation.MaxVal;
          this.SPL_PaperTension2.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer2Config.PaperTension.PaperTensionRegulation.MinVal;
          this.SPL_PaperTension2.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer2Config.PaperTension.PaperTensionRegulation.MaxVal;
          this.SPL_PaperTension3.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer3Config.PaperTension.PaperTensionRegulation.MinVal;
          this.SPL_PaperTension3.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer3Config.PaperTension.PaperTensionRegulation.MaxVal;
          this.SPL_PaperTension4.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer4Config.PaperTension.PaperTensionRegulation.MinVal;
          this.SPL_PaperTension4.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer4Config.PaperTension.PaperTensionRegulation.MaxVal;
          this.SPL_PaperTension5.MinSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer5Config.PaperTension.PaperTensionRegulation.MinVal;
          this.SPL_PaperTension5.MaxSetPointValue = SyncroC.DBManager.getDBManager().lineConfig.Splicer5Config.PaperTension.PaperTensionRegulation.MaxVal;

          this.stb_SpliceConfirmedS1.Text = DBManager.getDBManager().language.getTranslation("Frm_HomeWet.stb_SpliceConfirmedS1.Text");
          this.stb_SpliceConfirmedS2.Text = DBManager.getDBManager().language.getTranslation("Frm_HomeWet.stb_SpliceConfirmedS2.Text");
          this.stb_SpliceConfirmedS3.Text = DBManager.getDBManager().language.getTranslation("Frm_HomeWet.stb_SpliceConfirmedS3.Text");
          this.stb_SpliceConfirmedS4.Text = DBManager.getDBManager().language.getTranslation("Frm_HomeWet.stb_SpliceConfirmedS4.Text");
          this.stb_SpliceConfirmedS5.Text = DBManager.getDBManager().language.getTranslation("Frm_HomeWet.stb_SpliceConfirmedS5.Text");

          this.SyncIdle.Text = DBManager.getDBManager().language.getTranslation("Frm_HomeWet.SyncIdle.Text");
          this.SyncOnMetres.Text = DBManager.getDBManager().language.getTranslation("Frm_HomeWet.SyncOnMetres.Text");
          this.SyncOnSplicer1.Text = DBManager.getDBManager().language.getTranslation("Frm_HomeWet.SyncOnSplicer1.Text");
          this.SyncOnSplicer2.Text = DBManager.getDBManager().language.getTranslation("Frm_HomeWet.SyncOnSplicer2.Text");
          this.SyncOnSplicer3.Text = DBManager.getDBManager().language.getTranslation("Frm_HomeWet.SyncOnSplicer3.Text");
          this.SyncOnSplicer4.Text = DBManager.getDBManager().language.getTranslation("Frm_HomeWet.SyncOnSplicer4.Text");
          this.SyncOnSplicer5.Text = DBManager.getDBManager().language.getTranslation("Frm_HomeWet.SyncOnSplicer5.Text");

          this.tlb_WetEndMetersLeft.Text = DBManager.getDBManager().language.getTranslation("Frm_StatusBarWetEnd.tlb_WetEndMetersLeft.Text");

          if (!dbm.lineConfig.SingleFacer2)
          {
            this.stb_SpliceConfirmedS4.Enabled = false;
            this.stb_SpliceConfirmedS5.Enabled = false;
          }

          //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
          if (!dbm.lineConfig.SingleFacer3)
          {
            this.virtualForm.TabPages.RemoveByKey("tab_LowLevelSF3");

            this.chk_TraceSpeed_SF3.Visible = false;
            this.tLP_TraceSpeed.RowCount = this.tLP_TraceSpeed.RowCount - 1;
            this.tLP_TraceSpeed.SetRow(this.chk_TraceSpeed_SF2, 0);
            this.tLP_TraceSpeed.SetRow(this.chk_TraceSpeed_SF1, 1);
            this.tLP_TraceSpeed.SetRow(this.chk_TraceSpeed_PHTGU, 2);
            this.tLP_TraceSpeed.SetRowSpan(this.graphLines_Speed, 3);

            this.chk_TraceTemp_SF3.Visible = false;
            this.tLP_TraceTemp.RowCount = this.tLP_TraceTemp.RowCount - 1;
            this.tLP_TraceTemp.SetRow(this.chk_TraceTemp_SF2, 0);
            this.tLP_TraceTemp.SetRow(this.chk_TraceTemp_SF1, 1);
            this.tLP_TraceTemp.SetRow(this.chk_TraceTemp_PHTGU, 2);
            this.tLP_TraceTemp.SetRowSpan(this.graphLines_Temp, 3);

            this.chk_TraceGlue_SF3.Visible = false;
            this.tLP_TraceGlue.RowCount = this.tLP_TraceGlue.RowCount - 1;
            this.tLP_TraceGlue.SetRow(this.chk_TraceGlue_SF2, 0);
            this.tLP_TraceGlue.SetRow(this.chk_TraceGlue_SF1, 1);
            this.tLP_TraceGlue.SetRow(this.chk_TraceGlue_PHTGU, 2);
            this.tLP_TraceGlue.SetRowSpan(this.graphLines_Glue, 3);

            this.wETToolStripMenuItem.DropDownItems.Remove(this.sF3ToolStripMenuItem); 
          }
        }


        //RECORD
        if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        {
          this.contextMenuStrip_SceltaTab.Items[2].Visible = false;
          this.virtualForm.TabPages.RemoveByKey("tab_TraceSpeed");
          this.virtualForm.TabPages.RemoveByKey("tab_TraceTemp");
          this.virtualForm.TabPages.RemoveByKey("tab_TraceGlue");
          this.virtualForm.TabPages.RemoveByKey("tab_TraceMoisture");
        }
        else
        {
          //Speed
          this.rECORDToolStripMenuItem.DropDownItems[0].Text = DBManager.getDBManager().language.getTranslation("Frm_StatisticsOrdersDetail.graphLines_Speed2.Text");
          //Temperature
          this.rECORDToolStripMenuItem.DropDownItems[1].Text = DBManager.getDBManager().language.getTranslation("Frm_StatisticsOrdersDetail.graphLines_Temp2.Text");
          //POSITION
          if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
            this.rECORDToolStripMenuItem.DropDownItems[1].Text = DBManager.getDBManager().language.getTranslation("Frm_ProductionTrace.graphLines_WrapPosition.Text");
          //Glue
          this.rECORDToolStripMenuItem.DropDownItems[2].Text = DBManager.getDBManager().language.getTranslation("Frm_StatisticsOrdersDetail.graphLines_GlueGap2.Text");
          //Moisture
          if (SyncroC.DBManager.getDBManager().lineConfig.PRCConfig.Header.EnableBottom_UpperMoistureSensor)
            this.rECORDToolStripMenuItem.DropDownItems[3].Text = "Moisture";//DBManager.getDBManager().language.getTranslation("Frm_StatisticsOrdersDetail.graphLines_GlueGap2.Text");
          else
          {
            this.rECORDToolStripMenuItem.DropDownItems[3].Visible = false;
            this.virtualForm.TabPages.RemoveByKey("tab_TraceMoisture");
          }
        }

        //PROCESS
        if (!Settings.getSettings().configuration.PRC)
        {
          this.contextMenuStrip_SceltaTab.Items[3].Visible = false;
        }
        else
        {
          //per ora è false perchè non c'è nè la web cam nè il warp detector
          this.contextMenuStrip_SceltaTab.Items[3].Visible = false;
        }
        
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "setUpLateralBarItem - Error ", ex);
      }
    }

    public void _Update()
    {
      try
      {
        switch (this.virtualForm.SelectedTab.Text)
        {
          case "3":
            this._UpdateSpeedTrace();
            break;
          case "4":
            this._UpdateTempTrace();
            break;
          case "5":
            this._UpdateGlueTrace();
            break;
          case "6":
            this._UpdateOrdersList();
            break;
          case "9":
            this._UpdateCurrentOrderDetails();
            break;
          default:
            break;
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "_Update - Error ", ex);
      }
    }

    private int timeOutPerUpdate_CurrentShift = -1;
    public void _UpdateCurrentShift(bool isIdle, int secondsRunTime, int secondsStopTime, int averageSpeed, int metresProduced)
    {
      try
      {
        //questo serve perchè altrimenti rallenta troppo
        this.timeOutPerUpdate_CurrentShift--;
        if (this.timeOutPerUpdate_CurrentShift < 0)
          this.timeOutPerUpdate_CurrentShift = this.TIME_OUT_PER_UPDATE;
        else
          return;

        if (isIdle)
        {
          this.lbl_IdleShift.Visible = true;
          //this.graphPies_ShiftTempo.Visible = false;
          this.tLP_Shift.Visible = false;
        }
        else
        {
          this.lbl_IdleShift.Visible = false;
          //this.graphPies_ShiftTempo.Visible = true;
          this.tLP_Shift.Visible = true;

          if ((secondsRunTime > 60) || (secondsStopTime > 60))
          {
            secondsRunTime = secondsRunTime / 60;
            secondsStopTime = secondsStopTime / 60;
          }

          if ((secondsRunTime == 0) && (secondsStopTime == 0))
            return;

          this.graphPies_ShiftTempo.drawGraphPies(new int[] { secondsRunTime, secondsStopTime }, new Color[] { Color.Green, Color.Red });
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "_UpdateCurrentShift - Error ", ex);
      }
    }

    private string lastId_M1 = "";
    private string lastId_0 = "";
    private string lastId_1 = "";
    private void _UpdateOrdersList()
    {
      try
      {
        DataRow row_M1 = DBManager.getDBManager().pastOrder.getRow(1);
        if(
          (row_M1 != null) &&
          (lastId_M1 != (row_M1[PastOrder.POL_SETUP_ID].ToString() + row_M1[PastOrder.POL_SETUP_MODID].ToString()))
          )
        {
          this.lastId_M1 = row_M1[PastOrder.POL_SETUP_ID].ToString() + row_M1[PastOrder.POL_SETUP_MODID].ToString();

          this.ttl_DRYList_M1.Text = "-1) " + row_M1[PastOrder.POL_SETUP_CODE].ToString() + " - " + row_M1[PastOrder.POL_QUALITY_ID].ToString();
          string level1 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + row_M1[PastOrder.POL_LEVEL_NAME_0].ToString());
          string level2 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + row_M1[PastOrder.POL_LEVEL_NAME_1].ToString());
          string level3 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + row_M1[PastOrder.POL_LEVEL_NAME_2].ToString());
          this.lbl_OrderNumber1_M1.Text = level1 + " - " + row_M1[PastOrder.POL_ORDER_NUMBER_0].ToString() + " " + row_M1[Order.DEL_CUSTOMER_NAME_0].ToString() + " " + SyncroConverter.ConvertString(row_M1[Order.DEL_SHEET_WIDTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + SyncroConverter.ConvertString(row_M1[Order.DEL_SHEET_LENGTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + row_M1[Order.DEL_OUTS_0].ToString();
          this.lbl_OrderNumber2_M1.Text = level2 + " - " + row_M1[PastOrder.POL_ORDER_NUMBER_1].ToString() + " " + row_M1[Order.DEL_CUSTOMER_NAME_1].ToString() + " " + SyncroConverter.ConvertString(row_M1[Order.DEL_SHEET_WIDTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + SyncroConverter.ConvertString(row_M1[Order.DEL_SHEET_LENGTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + row_M1[Order.DEL_OUTS_1].ToString();
          this.lbl_OrderNumber3_M1.Text = level3 + " - " + row_M1[PastOrder.POL_ORDER_NUMBER_2].ToString() + " " + row_M1[Order.DEL_CUSTOMER_NAME_2].ToString() + " " + SyncroConverter.ConvertString(row_M1[Order.DEL_SHEET_WIDTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + SyncroConverter.ConvertString(row_M1[Order.DEL_SHEET_LENGTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + row_M1[Order.DEL_OUTS_2].ToString();

          if (row_M1[PastOrder.POL_LEVEL_NAME_0].ToString() == "Nolevel")
            this.lbl_OrderNumber1_M1.Text = "";
          if (row_M1[PastOrder.POL_LEVEL_NAME_1].ToString() == "Nolevel")
            this.lbl_OrderNumber2_M1.Text = "";
          if (row_M1[PastOrder.POL_LEVEL_NAME_2].ToString() == "Nolevel")
            this.lbl_OrderNumber3_M1.Text = "";
        }

        DataRow row_0 = DBManager.getDBManager().order.getRow(0);
        if (
          (row_0 != null) &&
          (lastId_0 != (row_0[Order.DEL_SETUP_ID].ToString() + row_0[Order.DEL_SETUP_MODID].ToString()))
          )
        {
          this.lastId_0 = row_0[Order.DEL_SETUP_ID].ToString() + row_0[Order.DEL_SETUP_MODID].ToString();

          this.ttl_DRYList_0.Text = "0) " + row_0[Order.DEL_SETUP_CODE].ToString() + " - " + row_0[Order.DEL_QUALITY_ID].ToString();
          string level1 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + row_0[Order.DEL_LEVEL_NAME_0].ToString());
          string level2 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + row_0[Order.DEL_LEVEL_NAME_1].ToString());
          string level3 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + row_0[Order.DEL_LEVEL_NAME_2].ToString());
          this.lbl_OrderNumber1_0.Text = level1 + " - " + row_0[Order.DEL_ORDER_NUMBER_0].ToString() + " " + row_0[Order.DEL_CUSTOMER_NAME_0].ToString() + " " + SyncroConverter.ConvertString(row_0[Order.DEL_SHEET_WIDTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + SyncroConverter.ConvertString(row_0[Order.DEL_SHEET_LENGTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + row_0[Order.DEL_OUTS_0].ToString();
          this.lbl_OrderNumber2_0.Text = level2 + " - " + row_0[Order.DEL_ORDER_NUMBER_1].ToString() + " " + row_0[Order.DEL_CUSTOMER_NAME_1].ToString() + " " + SyncroConverter.ConvertString(row_0[Order.DEL_SHEET_WIDTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + SyncroConverter.ConvertString(row_0[Order.DEL_SHEET_LENGTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + row_0[Order.DEL_OUTS_1].ToString();
          this.lbl_OrderNumber3_0.Text = level3 + " - " + row_0[Order.DEL_ORDER_NUMBER_2].ToString() + " " + row_0[Order.DEL_CUSTOMER_NAME_2].ToString() + " " + SyncroConverter.ConvertString(row_0[Order.DEL_SHEET_WIDTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + SyncroConverter.ConvertString(row_0[Order.DEL_SHEET_LENGTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + row_0[Order.DEL_OUTS_2].ToString();

          if (row_0[Order.DEL_LEVEL_NAME_0].ToString() == "Nolevel")
            this.lbl_OrderNumber1_0.Text = "";
          if (row_0[Order.DEL_LEVEL_NAME_1].ToString() == "Nolevel")
            this.lbl_OrderNumber2_0.Text = "";
          if (row_0[Order.DEL_LEVEL_NAME_2].ToString() == "Nolevel")
            this.lbl_OrderNumber3_0.Text = "";
        }

        DataRow row_1 = DBManager.getDBManager().order.getRow(1);
        if (
          (row_1 != null) &&
          (lastId_1 != (row_1[Order.DEL_SETUP_ID].ToString() + row_1[Order.DEL_SETUP_MODID].ToString()))
          )
        {
          this.lastId_1 = row_1[Order.DEL_SETUP_ID].ToString() + row_1[Order.DEL_SETUP_MODID].ToString();

          this.ttl_DRYList_1.Text = "1) " + row_1[Order.DEL_SETUP_CODE].ToString() + " - " + row_1[Order.DEL_QUALITY_ID].ToString();

          string level1 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + row_1[Order.DEL_LEVEL_NAME_0].ToString());
          string level2 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + row_1[Order.DEL_LEVEL_NAME_1].ToString());
          string level3 = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + row_1[Order.DEL_LEVEL_NAME_2].ToString());
          this.lbl_OrderNumber1_1.Text = level1 + " - " + row_1[Order.DEL_ORDER_NUMBER_0].ToString() + " " + row_1[Order.DEL_CUSTOMER_NAME_0].ToString() + " " + SyncroConverter.ConvertString(row_1[Order.DEL_SHEET_WIDTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + SyncroConverter.ConvertString(row_1[Order.DEL_SHEET_LENGTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + row_1[Order.DEL_OUTS_0].ToString();
          this.lbl_OrderNumber2_1.Text = level2 + " - " + row_1[Order.DEL_ORDER_NUMBER_1].ToString() + " " + row_1[Order.DEL_CUSTOMER_NAME_1].ToString() + " " + SyncroConverter.ConvertString(row_1[Order.DEL_SHEET_WIDTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + SyncroConverter.ConvertString(row_1[Order.DEL_SHEET_LENGTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + row_1[Order.DEL_OUTS_1].ToString();
          this.lbl_OrderNumber3_1.Text = level3 + " - " + row_1[Order.DEL_ORDER_NUMBER_2].ToString() + " " + row_1[Order.DEL_CUSTOMER_NAME_2].ToString() + " " + SyncroConverter.ConvertString(row_1[Order.DEL_SHEET_WIDTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + SyncroConverter.ConvertString(row_1[Order.DEL_SHEET_LENGTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + row_1[Order.DEL_OUTS_2].ToString();

          if (row_1[Order.DEL_LEVEL_NAME_0].ToString() == "Nolevel")
            this.lbl_OrderNumber1_1.Text = "";
          if (row_1[Order.DEL_LEVEL_NAME_1].ToString() == "Nolevel")
            this.lbl_OrderNumber2_1.Text = "";
          if (row_1[Order.DEL_LEVEL_NAME_2].ToString() == "Nolevel")
            this.lbl_OrderNumber3_1.Text = "";
        }

      }
      catch (Exception ex)
      {
        Logger.LogError(this, "_UpdateOrdersList - Error ", ex);
      }
    }

    private string lastId = "";
    private void _UpdateCurrentOrderDetails()
    {
      try
      {
        DataRow row = DBManager.getDBManager().order.getRow(0);
        if (
          (row != null) &&
          (lastId != (row[Order.DEL_SETUP_ID].ToString() + row[Order.DEL_SETUP_MODID].ToString()))
          )
        {
          this.lastId = row[Order.DEL_SETUP_ID].ToString() + row[Order.DEL_SETUP_MODID].ToString();

          this.lbl_CurrentHorderHeader.Text = row[Order.DEL_SETUP_CODE].ToString() + " - " + row[Order.DEL_QUALITY_ID].ToString();


          //Ricerco formato inferiore
          int index = 0;
          string setupID = row[Order.DEL_SETUP_ID].ToString();
          string modID = row[Order.DEL_SETUP_MODID].ToString();
          row = DBManager.getDBManager().order.getRow(setupID, modID, LevelName.Lower, ref index);
          if (row != null)
          {//valorizzazione con ordine
            this.tlb_LowerFormat.Text = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + row[Order.DEL_LEVEL_NAME_n + index.ToString()].ToString()) + " - " + row[Order.DEL_ORDER_NUMBER_n + index.ToString()].ToString();
            this.lbl_LowerFormatDetail1.Text = row[Order.DEL_CUSTOMER_NAME_n + index.ToString()].ToString();
            this.lbl_LowerFormatDetail2.Text = SyncroConverter.ConvertString(row[Order.DEL_SHEET_WIDTH_n + index.ToString()].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + SyncroConverter.ConvertString(row[Order.DEL_SHEET_LENGTH_n + index.ToString()].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + row[Order.DEL_OUTS_n + index.ToString()].ToString() + "      " + row[Order.DEL_SEGMENT_SHEET_COUNT_n + index.ToString()];
          }
          else
          {//ordine non presente nella lista ordini controllo presenza in past order
            this.tlb_LowerFormat.Text = "";
            this.lbl_LowerFormatDetail1.Text = "";
            this.lbl_LowerFormatDetail2.Text = "";
          }

          //Ricerco formato superiore o medio a seconda della configurazione della linea
          row = null;
          if (knife.LevelCount == 2 || knife.LevelCount == 3)
            row = DBManager.getDBManager().order.getRow(setupID, modID, LevelName.Upper, ref index);

          if (row != null)
          {//valorizzazione con ordine
            this.tlb_UpperFormat.Text = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + row[Order.DEL_LEVEL_NAME_n + index.ToString()].ToString()) + " - " + row[Order.DEL_ORDER_NUMBER_n + index.ToString()].ToString();
            this.lbl_UpperFormatDetail1.Text = row[Order.DEL_CUSTOMER_NAME_n + index.ToString()].ToString();
            this.lbl_UpperFormatDetail2.Text = SyncroConverter.ConvertString(row[Order.DEL_SHEET_WIDTH_n + index.ToString()].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + SyncroConverter.ConvertString(row[Order.DEL_SHEET_LENGTH_n + index.ToString()].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + row[Order.DEL_OUTS_n + index.ToString()].ToString() + "      " + row[Order.DEL_SEGMENT_SHEET_COUNT_n + index.ToString()];
          }
          else
          {//ordine non presente nella lista ordini controllo presenza in past order
            this.tlb_UpperFormat.Text = "";
            this.lbl_UpperFormatDetail1.Text = "";
            this.lbl_UpperFormatDetail2.Text = "";
          }

          row = null;
          if (knife.LevelCount == 3)
            row = DBManager.getDBManager().order.getRow(setupID, modID, LevelName.Middle, ref index);

          if (row != null)
          {//valorizzazione con ordine
            this.tlb_MiddleFormat.Text = DBManager.getDBManager().language.getTranslation(typeof(LevelName).Name + "." + row[Order.DEL_LEVEL_NAME_n + index.ToString()].ToString()) + " - " + row[Order.DEL_ORDER_NUMBER_n + index.ToString()].ToString();
            this.lbl_MiddleFormatDetail1.Text = row[Order.DEL_CUSTOMER_NAME_n + index.ToString()].ToString();
            this.lbl_MiddleFormatDetail2.Text = SyncroConverter.ConvertString(row[Order.DEL_SHEET_WIDTH_n + index.ToString()].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + SyncroConverter.ConvertString(row[Order.DEL_SHEET_LENGTH_n + index.ToString()].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre) + " X " + row[Order.DEL_OUTS_n + index.ToString()].ToString() + "      " + row[Order.DEL_SEGMENT_SHEET_COUNT_n + index.ToString()];
          }
          else
          {//ordine non presente nella lista ordini controllo presenza in past order
            this.tlb_MiddleFormat.Text = "";
            this.lbl_MiddleFormatDetail1.Text = "";
            this.lbl_MiddleFormatDetail2.Text = "";
          }
        }

      }
      catch (Exception ex)
      {
        Logger.LogError(this, "_UpdateCurrentOrderDetails - Error ", ex);
      }
    }

    private int timeOutPerUpdate_GlueTrace = -1;
    private void _UpdateGlueTrace()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        return;

      try
      {
        //questo serve perchè altrimenti rallenta troppo
        this.timeOutPerUpdate_GlueTrace--;
        if (this.timeOutPerUpdate_GlueTrace < 0)
          this.timeOutPerUpdate_GlueTrace = this.TIME_OUT_PER_UPDATE;
        else
          return;

        //SPEED
        this.graphLines_Glue.Visible = true;

        DateTime to = SyncroC.DataBase.TRACEManager.getTRACEManager().getLastRilevation();
        DateTime from = to.Subtract(TimeSpan.FromMinutes(15));

        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        int zoneNumber = 0;
        if (this.chk_TraceGlue_PHTGU.Checked)
          zoneNumber = 0;
        else if (this.chk_TraceGlue_SF1.Checked)
          zoneNumber = 1;
        else if (this.chk_TraceGlue_SF2.Checked)
          zoneNumber = 2;
        else if (this.chk_TraceGlue_SF3.Checked)
          zoneNumber = 3;

        SyncroC.DataBase.TRACEManager.getTRACEManager().drawGlue(
          this.graphLines_Glue,
          zoneNumber,
          from,
          to,
          100, 
          false
          );
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "_UpdateGlueTrace - Error ", ex);
      }
    }

    private int timeOutPerUpdate_TempTrace = -1;
    private void _UpdateTempTrace()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        return;

      try
      {
        //questo serve perchè altrimenti rallenta troppo
        this.timeOutPerUpdate_TempTrace--;
        if (this.timeOutPerUpdate_TempTrace < 0)
          this.timeOutPerUpdate_TempTrace = this.TIME_OUT_PER_UPDATE;
        else
          return;

        //SPEED
        this.graphLines_Temp.Visible = true;

        DateTime to = SyncroC.DataBase.TRACEManager.getTRACEManager().getLastRilevation();
        DateTime from = to.Subtract(TimeSpan.FromMinutes(15));

        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        int zoneNumber = 0;
        if (this.chk_TraceTemp_PHTGU.Checked)
          zoneNumber = 0;
        else if (this.chk_TraceTemp_SF1.Checked)
          zoneNumber = 1;
        else if (this.chk_TraceTemp_SF2.Checked)
          zoneNumber = 2;
        else if (this.chk_TraceTemp_SF3.Checked)
          zoneNumber = 3;

        if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        {
          this.graphLines_Temp.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.TRACE_WrapArmPositionGraphLinesEditor;
          SyncroC.DataBase.TRACEManager.getTRACEManager().drawWrapArmPosition(
            this.graphLines_Temp,
            zoneNumber,
            from,
            to,
            100,
            false
            );
        }
        else
        {
          SyncroC.DataBase.TRACEManager.getTRACEManager().drawTemperature(
            this.graphLines_Temp,
            zoneNumber,
            from,
            to,
            100, 
            false);
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "_UpdateTempTrace - Error ", ex);
      }
    }

    private int timeOutPerUpdate_SpeedTrace = -1;
    private void _UpdateSpeedTrace()
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        return;

      try
      {
        //questo serve perchè altrimenti rallenta troppo
        this.timeOutPerUpdate_SpeedTrace--;
        if (this.timeOutPerUpdate_SpeedTrace < 0)
          this.timeOutPerUpdate_SpeedTrace = this.TIME_OUT_PER_UPDATE;
        else
          return;

        //SPEED
        this.graphLines_Speed.Visible = true;

        DateTime to = SyncroC.DataBase.TRACEManager.getTRACEManager().getLastRilevation();
        DateTime from = to.Subtract(TimeSpan.FromMinutes(15));

        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        SyncroC.DataBase.TRACEManager.getTRACEManager().drawSpeed(
          this.graphLines_Speed,
          this.chk_TraceSpeed_PHTGU.Checked,
          this.chk_TraceSpeed_SF1.Checked,
          this.chk_TraceSpeed_SF2.Checked,
          this.chk_TraceSpeed_SF3.Checked,
          from,
          to,
          100, 
          false);
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "_UpdateSpeedTrace - Error ", ex);
      }
    }

    private int timeOutPerUpdate_MoistureTrace = -1;
    private void _UpdateMoistureTrace(int umido_sotto, int umido_sopra)
    {
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        return;

      if (!SyncroC.DBManager.getDBManager().lineConfig.PRCConfig.Header.EnableBottom_UpperMoistureSensor)
        return;

      try
      {
        if ((umido_sotto != 0) || (umido_sopra != 0))
        {
          int umido_sopra_MENO_umido_sotto = umido_sopra - umido_sotto;
          //999 - 999 = 999  =>   [-3]
          this.lbl_MoistureSensor.Text = String.Format("\u21D1 [{0}] - \u21D3 [{1}] = {2}", umido_sopra, umido_sotto, umido_sopra_MENO_umido_sotto);
        }

        //questo serve perchè altrimenti rallenta troppo
        this.timeOutPerUpdate_MoistureTrace--;
        if (this.timeOutPerUpdate_MoistureTrace < 0)
          this.timeOutPerUpdate_MoistureTrace = this.TIME_OUT_PER_UPDATE;
        else
          return;

        //MOISTURE
        this.graphLines_Moisture.Visible = true;

        DateTime to = SyncroC.DataBase.TRACEManager.getTRACEManager().getLastRilevation();
        DateTime from = to.Subtract(TimeSpan.FromMinutes(15));

        SyncroC.DataBase.TRACEManager.getTRACEManager().drawMoisture(
          this.graphLines_Moisture,
          from,
          to,
          100, 
          false);
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "_UpdateMoistureTrace - Error ", ex);
      }
    }

    //agrassi_FI08-09-2010_1
    private int timeOutPerUpdate_DRYBlades = -1;
    private void _UpdateDRYBlades(bool val, int shaft, int slit, bool multiple, string grindCounters, bool maintenance, string slitPositions)
    {
      if (!this.toolDetailConfigured)
      {
        this.toolDetailConfigured = (DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.Name.Trim() != String.Empty);
        if (this.toolDetailConfigured)
        {
          this.GrindSlit.Visible = DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.PositioningOptions.Slits.EnableGrinding;
          this.toolDetail.Config(DBManager.getDBManager().lineConfig, SyncroC.Controls.Tools.MachineToolsType.OnlySlits,
                                    true, true, this.changeFunction, this.updateFunction, "", "");
          this.toolDetail.PositionOnlySlits();
        }
      }
      try
      {
        //questo serve perchè altrimenti rallenta troppo
        this.timeOutPerUpdate_DRYBlades--;
        if (this.timeOutPerUpdate_DRYBlades < 0)
          this.timeOutPerUpdate_DRYBlades = this.TIME_OUT_PER_UPDATE/3;
        else
          return;
        //agrassi_FI22-08-2011_1
        if (SyncroC.DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsTwin2() ||
            SyncroC.DBManager.getDBManager().lineConfig.SlitterScorerConfig.IsCompact2())
        {
          this.frm_bladeOldSS._SetGrinding(val, shaft, slit, multiple, grindCounters, maintenance);
          this.frm_bladeOldSS._UpdateSlitPositions(slitPositions);
        }
        else
        {
          this.frm_blade._SetGrinding(val, shaft, slit, multiple, grindCounters, maintenance);
          this.frm_blade._UpdateSlitPositions(slitPositions);
        }
        this.toolDetail.Invalidate();
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "_UpdateDRYBlades - Error ", ex);
      }
    }

    private void clearAllTimeOut()
    {
      this.timeOutPerUpdate_CurrentShift = -1;
      this.timeOutPerUpdate_GlueTrace = -1;
      this.timeOutPerUpdate_SpeedTrace = -1;
      this.timeOutPerUpdate_TempTrace = -1;
      this.timeOutPerUpdate_MoistureTrace = -1;
      //agrassi_FI08-09-2010_1
      this.timeOutPerUpdate_DRYBlades = -1;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.setSelectedIndex(this.virtualForm.SelectedIndex + 1);
    }

    public void setSelectedIndex(int SelectedIndex)
    {
      if (SelectedIndex <= 0)
        SelectedIndex = 0;

      if (SelectedIndex >= this.virtualForm.TabCount)
        SelectedIndex = 0;

      this.virtualForm.SelectedIndex = SelectedIndex;
    }

    //graffaelli_FI02/11/2011_1 - Aggiunta funzione per selezionare la Tab sulla LateralBar in base al nome
    public void setSelectedTab(string SelectedTab)
    {
      this.virtualForm.SelectTab(SelectedTab);
    }

    private void virtualForm1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.OnLateralBarItemSelectedIndexChanged != null)
        OnLateralBarItemSelectedIndexChanged(this, e);

      this.clearAllTimeOut();

      return;
    }

    private void wa_wrapArmPreHeater_OnWrapArmButtonPressed(object sender, WrapArmButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      SyncroC.Controls.WrapArm mySender = ((SyncroC.Controls.WrapArm)sender);

      string machineName = mySender.Name.Substring(0, mySender.Name.IndexOf('_'));
      string cmd = "wrapArmPreHeater" + mySender.WrapName;

      string cmdMachineName = "";
      switch (machineName)
      {
        case "SF1":
          cmdMachineName = PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatus01), 1);
          break;
        case "SF2":
          cmdMachineName = PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatus01), 2);
          break;
        //RFAZZI_FI16/12/2010_1. Corretto errore sulla LateralBar nella finestra dei WrapArm Terna perchè nn prende i comandi di cambio stato
        //case "PHT":
        case "PHTGU":
          cmdMachineName = PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.PreHeaterTern.Frm_PreHeaterTernStatus));
          break;
        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        case "SF3":
          cmdMachineName = PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatus01), 3);
          break;

        default:
          return;
      }

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          cmdMachineName + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          cmdMachineName + "." + cmd + "." + Command.GO_TO_SETPOINT_WRAP + "(" + args.SetPoint.ToString() + ")");
      }
    }

    private void lowlevel_OnLowLevelControlButtonPressed(object sender, LowLevelControlButtonEventArgs args)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.LowLevelEnabled)
        return;

      SyncroC.Controls.LowLevelControl mySender = ((SyncroC.Controls.LowLevelControl)sender);

      string machineName = mySender.Name.Substring(0, mySender.Name.IndexOf('_'));

      string cmd = "";
      switch (mySender.MyTypeOfLowLevelControl)
      {
        case LowLevelControl.TypeOfLowLevelControl.GlueGap:
          cmd = "GlueGap";
          break;
        case LowLevelControl.TypeOfLowLevelControl.PaperTension:
          cmd = "PaperTension";
          break;
      }

      string cmdMachineName = "";
      switch (machineName)
      {
        case "SF1":
          cmdMachineName = PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatus01), 1);
          cmd += mySender.LowLevelControlName;
          break;
        case "SF2":
          cmdMachineName = PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatus01), 2);
          cmd += mySender.LowLevelControlName;
          break;
        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        case "SF3":
          cmdMachineName = PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.SingleFacer.Status.Frm_SingleFacerStatus01), 3);
          cmd += mySender.LowLevelControlName;
          break;

        case "PHTGU":
          cmdMachineName = PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.GlueUnit.Frm_GlueUnitStatus));
          cmd += mySender.LowLevelControlName;
          break;
        case "SPL":
          cmdMachineName = PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.Splicer.Frm_SplicerStatus));
          //RFAZZI_FI16/12/2010_3. L'indice sul comando di Toggle PaperTension per le Spliecr nella LateralBar non deve essere presente. 
          //cmd += mySender.LowLevelControlName;
          break;
        case "B1":
          cmdMachineName = PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.DoubleBacker.Status.Frm_DoubleBackerStatus01));
          cmd = "PaperTensionBridge21";
          break;
        case "B2":
          cmdMachineName = PageManager.getRequestCommand(typeof(SyncroC.Forms.Pages.DoubleBacker.Status.Frm_DoubleBackerStatus01));
          cmd = "PaperTensionBridge22";
          break;
        default:
          return;
      }

      if ((args.AutoMan != false) || (args.LocRem != false))
      {
        cmd = ((args.AutoMan) ? "AutoManual" : "LocalRemote") + cmd;
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          cmdMachineName + "." + cmd + "." + Command.TOGGLE_WITH_TIMEOUT);
      }
      else
      {
        SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          cmdMachineName + "." + cmd + "." + Command.GO_TO_SETPOINT_GLUE_GAP + "(" + args.SetPoint.ToString() + ")");
      }

    }

    private bool internalCheckedChanged = false;
    private void chk_TraceTempAndGlue_N_CheckedChanged(object sender, EventArgs e)
    {
      //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
      if (((System.Windows.Forms.CheckBox)sender).Checked)
      {
        this.internalCheckedChanged = true;
        switch (((System.Windows.Forms.CheckBox)sender).Name)
        {
          case "chk_TraceTemp_PHTGU":
            this.chk_TraceTemp_SF1.Checked = false;
            this.chk_TraceTemp_SF2.Checked = false;
            this.chk_TraceTemp_SF3.Checked = false;
            break;
          case "chk_TraceTemp_SF1":
            this.chk_TraceTemp_PHTGU.Checked = false;
            this.chk_TraceTemp_SF2.Checked = false;
            this.chk_TraceTemp_SF3.Checked = false;
            break;
          case "chk_TraceTemp_SF2":
            this.chk_TraceTemp_PHTGU.Checked = false;
            this.chk_TraceTemp_SF1.Checked = false;
            this.chk_TraceTemp_SF3.Checked = false;
            break;
          case "chk_TraceTemp_SF3":
            this.chk_TraceTemp_PHTGU.Checked = false;
            this.chk_TraceTemp_SF1.Checked = false;
            this.chk_TraceTemp_SF2.Checked = false;
            break;

          case "chk_TraceGlue_PHTGU":
            this.chk_TraceGlue_SF1.Checked = false;
            this.chk_TraceGlue_SF2.Checked = false;
            this.chk_TraceGlue_SF3.Checked = false;
            break;
          case "chk_TraceGlue_SF1":
            this.chk_TraceGlue_PHTGU.Checked = false;
            this.chk_TraceGlue_SF2.Checked = false;
            this.chk_TraceGlue_SF3.Checked = false;
            break;
          case "chk_TraceGlue_SF2":
            this.chk_TraceGlue_PHTGU.Checked = false;
            this.chk_TraceGlue_SF1.Checked = false;
            this.chk_TraceGlue_SF3.Checked = false;
            break;
          case "chk_TraceGlue_SF3":
            this.chk_TraceGlue_PHTGU.Checked = false;
            this.chk_TraceGlue_SF1.Checked = false;
            this.chk_TraceGlue_SF2.Checked = false;
            break;
        }
      }
      else
      {
        if(!this.internalCheckedChanged)
          ((System.Windows.Forms.CheckBox)sender).Checked = true;
      }

      this.clearAllTimeOut();
      this._Update();
      this.internalCheckedChanged = false;
    }

    private void NToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //switch (((System.Windows.Forms.ToolStripMenuItem)sender).Name)
      //{
      //  case "sPEEDToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 3;
      //    break;

      //  case "tEMPERATUREToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 4;
      //    break;

      //  case "gLUEToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 5;
      //    break;

      //  case "pHTGUToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 2;
      //    break;

      //  case "sF1ToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 0;
      //    break;

      //  case "sF2ToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 1;
      //    break;

      //  //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
      //  case "sF3ToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 12;
      //    break;

      //  case "ordersListToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 6;
      //    break;

      //  case "shiftRunStopToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 7;
      //    break;

      //  case "bladesStatusToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 8;
      //    break;

      //  case "mOISTUREToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 11;
      //    break;

      //  //RFAZZI_FI26/05/2010_1. Aggiunto in lateral bar la visualizzazione dei dettagli dell'ordine DryEnd Corrente.
      //  case "currentOrderDetailsToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 9;
      //    break;

      //  //RFAZZI_FI27/05/2010_1.Aggiunto in lateral bar la visualizzazione della HomeWetEnd. 
      //  case "homeWetEndToolStripMenuItem":
      //    this.virtualForm.SelectedIndex = 10;
      //    break;

      //}

      switch (((System.Windows.Forms.ToolStripMenuItem)sender).Name)
      {
        case "sPEEDToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_TraceSpeed");
          break;

        case "tEMPERATUREToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_TraceTemp");
          break;

        case "gLUEToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_TraceGlue");
          break;

        case "pHTGUToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_LowLevelPHTGU");
          break;

        case "sF1ToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_LowLevelSF1");
          break;

        case "sF2ToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_LowLevelSF2");
          break;

        //RFAZZI_FI18/08/2011_1. Aggiunta la terza zona nella LateralBar. 
        case "sF3ToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_LowLevelSF3");
          break;

        case "ordersListToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_DRYList");
          break;

        case "shiftRunStopToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_SHFRunStop");
          break;

        case "bladesStatusToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_DRYBlades");
          break;

        case "mOISTUREToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_TraceMoisture");
          break;

        //RFAZZI_FI26/05/2010_1. Aggiunto in lateral bar la visualizzazione dei dettagli dell'ordine DryEnd Corrente.
        case "currentOrderDetailsToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_CurrentOrderDetails");
          break;

        //RFAZZI_FI27/05/2010_1.Aggiunto in lateral bar la visualizzazione della HomeWetEnd. 
        case "homeWetEndToolStripMenuItem":
          this.virtualForm.SelectedIndex = this.virtualForm.TabPages.IndexOfKey("tab_homeWetEnd");
          break;

      }
    }

    public void setUpButtonLateralBarItemChange(bool setVisible)
    {
      if (setVisible)
      {
        this.btn_lateralBarItemChange.Visible = true;
        this.btn_lateralBarItemChange.BringToFront();
      }
      else
      {
        this.btn_lateralBarItemChange.Visible = false;
        this.btn_lateralBarItemChange.SendToBack();
      }
    }

    private void chk_TraceSpeed_N_CheckedChanged(object sender, EventArgs e)
    {
      this.clearAllTimeOut();
      this._Update();
    }

    private void _setPaperTypeForSplicer(int splNumber, string paperTypeCurrent, int paperWeigthFactorCurrent, int paperWidthCurrent, int remainingProductionCurrent_UDmm, int productionUntilSplice_UDmm, string paperTypeNext, int paperWeigthFactorNext, int paperWidthNext)
    {
      System.Windows.Forms.Label CurrentS = null;
      System.Windows.Forms.Label NextS = null;
      System.Windows.Forms.Label remainingProductionS = null;
      System.Windows.Forms.Label productionUntilS = null;

      switch (splNumber)
      {
        case 1:
          CurrentS = this.ttl_CurrentS1;
          NextS = this.ttl_NextS1;
          remainingProductionS = this.tnud_RemainingProductionS1;
          productionUntilS = this.nud_S1Meters;
          break;
        case 2:
          CurrentS = this.ttl_CurrentS2;
          NextS = this.ttl_NextS2;
          remainingProductionS = this.tnud_RemainingProductionS2;
          productionUntilS = this.nud_S2Meters;
          break;
        case 3:
          CurrentS = this.ttl_CurrentS3;
          NextS = this.ttl_NextS3;
          remainingProductionS = this.tnud_RemainingProductionS3;
          productionUntilS = this.nud_S3Meters;
          break;
        case 4:
          CurrentS = this.ttl_CurrentS4;
          NextS = this.ttl_NextS4;
          remainingProductionS = this.tnud_RemainingProductionS4;
          productionUntilS = this.nud_S4Meters;
          break;
        case 5:
          CurrentS = this.ttl_CurrentS5;
          NextS = this.ttl_NextS5;
          remainingProductionS = this.tnud_RemainingProductionS5;
          productionUntilS = this.nud_S5Meters;
          break;
        default:
          break;
      }

      //CM69 aggiunta la quantità di carta in bobina sulla home wet
      string paperTypeStr = paperTypeCurrent;
      if (paperWeigthFactorCurrent != 0)
        paperTypeStr = paperTypeStr + " (" + paperWeigthFactorCurrent.ToString() + ")";
      if (CurrentS != null)
        CurrentS.Text = paperTypeStr;

      paperTypeStr = paperTypeNext;
      if (paperWeigthFactorNext != 0)
        paperTypeStr = paperTypeStr + " (" + paperWeigthFactorNext.ToString() + ")";
      if (NextS != null)
      {
        NextS.Text = paperTypeStr;

        if ((paperTypeCurrent != paperTypeNext) || (paperWeigthFactorCurrent != paperWeigthFactorNext) || (paperWidthCurrent != paperWidthNext))
          NextS.BackColor = System.Drawing.Color.LightBlue;
        else
          NextS.BackColor = System.Drawing.Color.WhiteSmoke;
      }

      if (remainingProductionCurrent_UDmm == productionUntilSplice_UDmm)
      {
        remainingProductionS.Visible = false;
        remainingProductionS.Text = "";
      }
      else
      {
        if (remainingProductionS != null)
        {
          if (CurrentS.Text != "")
            remainingProductionS.Text = remainingProductionCurrent_UDmm.ToString();
          else
            remainingProductionS.Text = "";
          remainingProductionS.Visible = true;
        }
      }

      productionUntilS.Text = productionUntilSplice_UDmm.ToString();


    }

    public void SyncButtonsActive(bool SyncIdleActive, bool SyncOnMetresActive, bool SyncNowActive, bool SyncOnSplicer1Active,
      bool SyncOnSplicer2Active, bool SyncOnSplicer3Active, bool SyncOnSplicer4Active, bool SyncOnSplicer5Active)
    {
      SyncIdle.Active = SyncIdleActive;
      SyncOnMetres.Active = SyncOnMetresActive;
      SyncOnSplicer1.Active = SyncOnSplicer1Active;
      SyncOnSplicer2.Active = SyncOnSplicer2Active;
      SyncOnSplicer3.Active = SyncOnSplicer3Active;
      SyncOnSplicer4.Active = SyncOnSplicer4Active;
      SyncOnSplicer5.Active = SyncOnSplicer5Active;
    }

    public void SyncButtonsEnabled(bool enabled)
    {
      /*
       * enabled = (! wec syncModeChange)
       * SyncOnSplicerX must be (! wec syncModeChange) && SplicerX in auto
       */
      SyncIdle.Enabled = true;
      SyncOnMetres.Enabled = enabled;
      SyncOnSplicer1.Enabled = enabled && ((!ttl_CurrentS1.Text.Trim().Equals(string.Empty) && ttl_NextS1.BackColor.Equals(SyncroC.Controls.Colors.SyncroColor.LightGreen)) ||
                                                                  (!ttl_CurrentS1.Text.Trim().Equals(string.Empty) && ttl_NextS1.Text.Trim().Equals(string.Empty)));
      SyncOnSplicer2.Enabled = enabled && ((!ttl_CurrentS2.Text.Trim().Equals(string.Empty) && ttl_NextS2.BackColor.Equals(SyncroC.Controls.Colors.SyncroColor.LightGreen)) ||
                                                                  (!ttl_CurrentS2.Text.Trim().Equals(string.Empty) && ttl_NextS2.Text.Trim().Equals(string.Empty)));
      SyncOnSplicer3.Enabled = enabled && ((!ttl_CurrentS3.Text.Trim().Equals(string.Empty) && ttl_NextS3.BackColor.Equals(SyncroC.Controls.Colors.SyncroColor.LightGreen)) ||
                                                                  (!ttl_CurrentS3.Text.Trim().Equals(string.Empty) && ttl_NextS3.Text.Trim().Equals(string.Empty)));
      SyncOnSplicer4.Enabled = enabled && ((!ttl_CurrentS4.Text.Trim().Equals(string.Empty) && ttl_NextS4.BackColor.Equals(SyncroC.Controls.Colors.SyncroColor.LightGreen)) ||
                                                                  (!ttl_CurrentS4.Text.Trim().Equals(string.Empty) && ttl_NextS4.Text.Trim().Equals(string.Empty)));
      SyncOnSplicer5.Enabled = enabled && ((!ttl_CurrentS5.Text.Trim().Equals(string.Empty) && ttl_NextS5.BackColor.Equals(SyncroC.Controls.Colors.SyncroColor.LightGreen)) ||
                                                                  (!ttl_CurrentS5.Text.Trim().Equals(string.Empty) && ttl_NextS5.Text.Trim().Equals(string.Empty)));
    }

    private void SyncIdle_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
        return;

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
        Command.WEC + ".CancelSync()");//instead of select the SYNCMODE_IDLE we send a CancelSync command
    }

    private void SyncOnMetres_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
        return;

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
        Command.WEC + ".SyncMode(8)");//8 = SYNCMODE_METRES 
    }

    private void SyncOnSplicer1_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
        return;

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
        Command.WEC + ".SyncMode(1)");//1 = SYNCMODE_SPLICER1 
    }

    private void SyncOnSplicer2_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
        return;

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
        Command.WEC + ".SyncMode(2)");//2 = SYNCMODE_SPLICER2
    }

    private void SyncOnSplicer3_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
        return;

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
        Command.WEC + ".SyncMode(3)");//3 = SYNCMODE_SPLICER3
    }

    private void SyncOnSplicer4_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
        return;

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
        Command.WEC + ".SyncMode(4)");//4 = SYNCMODE_SPLICER4
    }

    private void SyncOnSplicer5_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
        return;

      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
        Command.WEC + ".SyncMode(5)");//5 = SYNCMODE_SPLICER5
    }

    private void stb_SpliceConfirmedSX_Click(object sender, EventArgs e)
    {
      if (!SyncroC.Utility.Settings.getSettings().facility.ChangeWETAutoManMode)
        return;

      string senderName = ((Control)sender).Name;
      SyncroC.Network.CommunicationManager.GetCommunicationManager().SendTcp(SyncroC.Network.CommunicationManager.GetCommunicationManager().SYNCROS_RTC_MANAGER,
          Command.SPLICER + senderName[senderName.Length - 1] + "." + Command.TOGGLE_CONFIRM_SPLICE + "()");
    }

    private void CorrugatorDirectionLeft()
    {
      tlp_homeWetEnd.SetColumn(this.ttl_Splicer1, 0);
      tlp_homeWetEnd.SetColumn(this.ttl_Splicer2, 1);
      tlp_homeWetEnd.SetColumn(this.ttl_Splicer3, 2);
      tlp_homeWetEnd.SetColumn(this.ttl_Splicer4, 3);
      tlp_homeWetEnd.SetColumn(this.ttl_Splicer5, 4);

      tlp_homeWetEnd.SetColumn(this.stb_SpliceConfirmedS1, 0);
      tlp_homeWetEnd.SetColumn(this.stb_SpliceConfirmedS2, 1);
      tlp_homeWetEnd.SetColumn(this.stb_SpliceConfirmedS3, 2);
      tlp_homeWetEnd.SetColumn(this.stb_SpliceConfirmedS4, 3);
      tlp_homeWetEnd.SetColumn(this.stb_SpliceConfirmedS5, 4);

      tlp_homeWetEnd.SetColumn(this.ttl_CurrentS1, 0);
      tlp_homeWetEnd.SetColumn(this.ttl_CurrentS2, 1);
      tlp_homeWetEnd.SetColumn(this.ttl_CurrentS3, 2);
      tlp_homeWetEnd.SetColumn(this.ttl_CurrentS4, 3);
      tlp_homeWetEnd.SetColumn(this.ttl_CurrentS5, 4);

      tlp_homeWetEnd.SetColumn(this.ttl_NextS1, 0);
      tlp_homeWetEnd.SetColumn(this.ttl_NextS2, 1);
      tlp_homeWetEnd.SetColumn(this.ttl_NextS3, 2);
      tlp_homeWetEnd.SetColumn(this.ttl_NextS4, 3);
      tlp_homeWetEnd.SetColumn(this.ttl_NextS5, 4);

      tlp_homeWetEnd.SetColumn(this.tnud_RemainingProductionS1, 0);
      tlp_homeWetEnd.SetColumn(this.tnud_RemainingProductionS2, 1);
      tlp_homeWetEnd.SetColumn(this.tnud_RemainingProductionS3, 2);
      tlp_homeWetEnd.SetColumn(this.tnud_RemainingProductionS4, 3);
      tlp_homeWetEnd.SetColumn(this.tnud_RemainingProductionS5, 4);

      tlp_homeWetEnd.SetColumn(this.nud_S1Meters, 0);
      tlp_homeWetEnd.SetColumn(this.nud_S2Meters, 1);
      tlp_homeWetEnd.SetColumn(this.nud_S3Meters, 2);
      tlp_homeWetEnd.SetColumn(this.nud_S4Meters, 3);
      tlp_homeWetEnd.SetColumn(this.nud_S5Meters, 4);

      tlp_homeWetEnd.SetColumn(this.SyncOnSplicer1, 0);
      tlp_homeWetEnd.SetColumn(this.SyncOnSplicer2, 1);
      tlp_homeWetEnd.SetColumn(this.SyncOnSplicer3, 2);
      tlp_homeWetEnd.SetColumn(this.SyncOnSplicer4, 3);
      tlp_homeWetEnd.SetColumn(this.SyncOnSplicer5, 4);

    }

  }
}
