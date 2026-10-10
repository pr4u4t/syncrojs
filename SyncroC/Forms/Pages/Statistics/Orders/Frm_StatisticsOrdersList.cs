using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.DataBase.EventArgs;
using SyncroC.Utility;
using SyncroC.Network;
using SyncroC.DataBase;
using SyncroC.Forms.PopUp;

namespace SyncroC.Forms.Pages.Statistics.Orders
{
  public partial class Frm_StatisticsOrdersList : Frm_StatisticsOrdersSubgroup
  {
    //colonne che avrebbe dovuto fare il designer.
    private DataGridViewColumn clm_Order_Begin;
    private DataGridViewColumn clm_Order_SetupCode;
    private DataGridViewColumn clm_Order_Quality;
    private DataGridViewColumn clm_Order_DBMeterProd;
    private DataGridViewColumn clm_Order_SSMeterProd;
    private DataGridViewColumn clm_Order_StopTime;
    private DataGridViewColumn clm_Order_AveSpeed;

    private string SS_METER_PROD_COL_NAME = "SSMeterProd";
    private string DB_METER_PROD_COL_NAME = "DBMeterProd";

    private string filter = "";

    public Frm_StatisticsOrdersList()
    {
      InitializeComponent();

      //ABELLO_FI25/08/2010_1 se uso Postgres il doppio click sulla lista degli eseguiti andava a casaccio
      if (!SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        this.gdv_Orders.DataSource = DBManager.getDBManager().pastOrder.getTable();

        //PMASSARO_FI28/10/08 - Personalizazione ZETACARTON: selezione multipla nella pagina statistiche -> ordini prodotti
        if (DBManager.getDBManager().syncroConfig.GeneralSetting.Job == "3703")
        {
          gdv_Orders.MultiSelect = true;
        }
      }

      clm_Order_Begin = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_Begin.HeaderText = "Begin";
      clm_Order_Begin.DataPropertyName = PastOrder.POL_BEGIN_TIME;
      clm_Order_Begin.Name = PastOrder.POL_BEGIN_TIME;
      clm_Order_Begin.Width = 200;

      clm_Order_SetupCode = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_SetupCode.Name = PastOrder.POL_SETUP_CODE;
      clm_Order_SetupCode.HeaderText = "Code";
      clm_Order_SetupCode.DataPropertyName = PastOrder.POL_SETUP_CODE;
      clm_Order_SetupCode.Width = 200;

      clm_Order_Quality = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_Quality.Name = PastOrder.POL_QUALITY_ID;
      clm_Order_Quality.HeaderText = "Quality";
      clm_Order_Quality.DataPropertyName = PastOrder.POL_QUALITY_ID;
      clm_Order_Quality.Width = 354;

      clm_Order_DBMeterProd = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_DBMeterProd.Name = DB_METER_PROD_COL_NAME;
      clm_Order_DBMeterProd.HeaderText = "DB Meter";
      clm_Order_DBMeterProd.DataPropertyName = PastOrder.POL_CUTS_PRODUCED_0;
      clm_Order_DBMeterProd.Width = 100;

      clm_Order_SSMeterProd = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_SSMeterProd.Name = SS_METER_PROD_COL_NAME;
      clm_Order_SSMeterProd.HeaderText = "DE Meter";
      clm_Order_SSMeterProd.DataPropertyName = PastOrder.POL_CUTS_PRODUCED_0;
      clm_Order_SSMeterProd.Width = 100;

      clm_Order_StopTime = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_StopTime.Name = PastOrder.POL_STOPTIME;
      clm_Order_StopTime.HeaderText = "Stop Time";
      clm_Order_StopTime.DataPropertyName = PastOrder.POL_STOPTIME;
      clm_Order_StopTime.Width = 95;

      clm_Order_AveSpeed = new DataGridViewColumn(new DataGridViewTextBoxCell());
      clm_Order_AveSpeed.HeaderText = "Avg Speed";
      clm_Order_AveSpeed.DataPropertyName = PastOrder.POL_AVERAGE_SPEED;
      clm_Order_AveSpeed.Name = PastOrder.POL_AVERAGE_SPEED;
      clm_Order_AveSpeed.Width = 95;

      gdv_Orders.Columns.Add(clm_Order_Begin);
      gdv_Orders.Columns.Add(clm_Order_SetupCode);
      gdv_Orders.Columns.Add(clm_Order_Quality);
      gdv_Orders.Columns.Add(clm_Order_DBMeterProd);
      gdv_Orders.Columns.Add(clm_Order_SSMeterProd);
      gdv_Orders.Columns.Add(clm_Order_StopTime);
      gdv_Orders.Columns.Add(clm_Order_AveSpeed);

      gdv_Orders.Sort(clm_Order_Begin, ListSortDirection.Descending);

      //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
      if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].DischargeGroup ||
          SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].DischargeGroup ||
          SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].DischargeGroup)
      {
        nud_ScrapDischargeGroup0.Visible = true;
        nud_ScrapDischargeGroup1.Visible = true;
        nud_ScrapDischargeGroup2.Visible = true;

        //Riposiziono i campi di tagli richiesti, fatti buoni e scarto

        tlb_desired0.Width = 80;
        tlb_desired0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        tlb_produced0.Location = new Point(tlb_desired0.Location.X + tlb_desired0.Width + 10, tlb_produced0.Location.Y);
        tlb_produced0.Width = tlb_desired0.Width;
        tlb_produced0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        tlb_bad0.Location = new Point(tlb_produced0.Location.X + tlb_produced0.Width + 10, tlb_bad0.Location.Y);
        tlb_bad0.Width = 65;
        tlb_bad0.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        tlb_desired1.Width = 80;
        tlb_desired1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        tlb_produced1.Location = new Point(tlb_desired1.Location.X + tlb_desired1.Width + 10, tlb_produced1.Location.Y);
        tlb_produced1.Width = tlb_desired1.Width;
        tlb_produced1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        tlb_bad1.Location = new Point(tlb_produced1.Location.X + tlb_produced1.Width + 10, tlb_bad1.Location.Y);
        tlb_bad1.Width = 65;
        tlb_bad1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        tlb_desired2.Width = 80;
        tlb_desired2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        tlb_produced2.Location = new Point(tlb_desired2.Location.X + tlb_desired2.Width + 10, tlb_produced2.Location.Y);
        tlb_produced2.Width = tlb_desired2.Width;
        tlb_produced2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

        tlb_bad2.Location = new Point(tlb_produced2.Location.X + tlb_produced2.Width + 10, tlb_bad2.Location.Y);
        tlb_bad2.Width = 65;
        tlb_bad2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));

      }


      this.lbl_filter.Testo = (this.filter == "") ? "" : "!";

      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        this.lbl_filter.Visible = true;
        this.btn_ShowGraph.Visible = true;
      }
    }

    protected override void F10()
    {
    }

    public override void _Show()
    {
      base._Show();
      this.gdv_Orders.Focus();
    }

    public override void refreshMySQLData()
    {
      try
      {
        this.lbl_filter.Testo = (this.filter == "") ? "" : "!";

        DataRow row = null;
        int FirstDisplayedScrollingRowIndex = this.gdv_Orders.FirstDisplayedScrollingRowIndex;

        if (this.gdv_Orders != null)
          if (this.gdv_Orders.SelectedRows != null)
            if (this.gdv_Orders.SelectedRows.Count != 0)
              row = ((DataRowView)this.gdv_Orders.SelectedRows[0].DataBoundItem).Row;

        this.gdv_Orders.SuspendLayout();

        this.gdv_Orders.DataSource = DBManager.getDBManager().pastOrder.getTable(this.filter);

        for (int i = 0; i < this.gdv_Orders.Rows.Count; i++)
        {
          if (row != null)
          {
            string a = ((DataRowView)this.gdv_Orders.Rows[i].DataBoundItem).Row[PastOrder.POL_SETUP_ID].ToString().Trim();
            string b = row[PastOrder.POL_SETUP_ID].ToString().Trim();
            if (a == b)
            {
              this.gdv_Orders.Rows[i].Selected = true;
            }
          }
        }

        if ((row == null) && (this.gdv_Orders.Rows.Count > 0))
          this.gdv_Orders.Rows[0].Selected = true;

        if ((FirstDisplayedScrollingRowIndex >= 0) && (FirstDisplayedScrollingRowIndex <= DBManager.getDBManager().pastOrder.getCount(this.filter)))
          this.gdv_Orders.FirstDisplayedScrollingRowIndex = FirstDisplayedScrollingRowIndex;

        this.gdv_Orders.ResumeLayout();
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    private string setupCodeFilter = "";
    private string customerNameFilter = "";
    private string orderNumberFilter = "";
    private void lbl_filter_Click(object sender, EventArgs e)
    {
      try
      {
        Frm_FilterPastOrder askFilterPastOrder = new Frm_FilterPastOrder();
        FosberDialogResult ret = askFilterPastOrder.ShowDialog(this, this.setupCodeFilter, this.orderNumberFilter, this.customerNameFilter);
        if (ret.Dialog == DialogResult.OK)
        {
          this.filter = "";

          this.setupCodeFilter = ((string)ret.Data[0]).Trim();
          this.orderNumberFilter = ((string)ret.Data[1]).Trim();
          this.customerNameFilter = ((string)ret.Data[2]).Trim();

          if (this.setupCodeFilter != "")
            this.filter = String.Format("(setupcode LIKE '{0}%')", this.setupCodeFilter);

          if (this.orderNumberFilter != "")
          {
            if (this.filter != "")
              this.filter += String.Format("AND (ordernumber0 LIKE '{0}%' OR ordernumber1 LIKE '{0}%' OR ordernumber2 LIKE '{0}%')", this.orderNumberFilter);
            else
              this.filter = String.Format("(ordernumber0 LIKE '{0}%' OR ordernumber1 LIKE '{0}%' OR ordernumber2 LIKE '{0}%')", this.orderNumberFilter);
          }

          if (this.customerNameFilter != "")
          {
            if (this.filter != "")
              this.filter += String.Format("AND (customername0 LIKE '{0}%' OR customername1 LIKE '{0}%' OR customername2 LIKE '{0}%')", this.customerNameFilter);
            else
              this.filter = String.Format("(customername0 LIKE '{0}%' OR customername1 LIKE '{0}%' OR customername2 LIKE '{0}%')", this.customerNameFilter);
          }
        }

        this.gdv_Orders.SuspendLayout();
        this.gdv_Orders.DataSource = DBManager.getDBManager().pastOrder.getTable(this.filter);
        if (this.gdv_Orders.Rows.Count > 0)
          this.gdv_Orders.Rows[0].Selected = true;
        this.gdv_Orders.ResumeLayout();

        this.lbl_filter.Testo = (this.filter == "") ? "" : "!";
      }
      catch (Exception ex)
      {
        Logger.LogError(this, ex.Message);
      }
    }

    private void gdv_Orders_SelectionChanged(object sender, EventArgs e)
    {
      if (gdv_Orders.SelectedRows.Count == 1)
      {
        DataRow dataRow = ((DataRowView)gdv_Orders.SelectedRows[0].DataBoundItem).Row;
        //order 0
        ttl_Order0.Text = DBManager.getDBManager().language.getTranslation(
          typeof(LevelName).Name + "." + dataRow[PastOrder.POL_LEVEL_NAME_0].ToString()) +
          " - " + dataRow[PastOrder.POL_ORDER_NUMBER_0].ToString();
        lbl_Customer0.Text = dataRow[PastOrder.POL_CUSTOMER_NAME_0].ToString().Trim();
        lbl_Misc0.Text = SyncroConverter.ConvertString(dataRow[PastOrder.POL_SHEET_WIDTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)
          + " X " + SyncroConverter.ConvertString(dataRow[PastOrder.POL_SHEET_LENGTH_0].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)
          + " X " + dataRow[PastOrder.POL_OUTS_0].ToString();
        int outs = 0;
        int desired = 0;
        int produced = 0;

        //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
        /*
        int badPlusDefault = 0;
        try
        {
          outs = Convert.ToInt32(dataRow[PastOrder.POL_OUTS_0].ToString());
          desired = Convert.ToInt32(dataRow[PastOrder.POL_SEGMENT_SHEET_COUNT_0].ToString());
          produced = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_0].ToString()) * outs;
          badPlusDefault += Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_BAD_0].ToString());
          badPlusDefault += Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_DEFAULT_0].ToString());
          badPlusDefault *= outs;
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting order 0 data.", ex);
        }
        tlb_desired0.Value = desired.ToString();
        tlb_produced0.Value = produced.ToString();
        tlb_bad0.Value = badPlusDefault.ToString();
        this.pct_OrderPreprint0.Visible = SyncroConverter.ConvertByteToBoolean(dataRow[PastOrder.POL_PREPRINT_0].ToString());
        */
        int badSheets = 0;
        int defaultSheets = 0;
        int defaultcuts = 0;
        int scrapMadeDischargeGroup = 0;
        try
        {
          outs = Convert.ToInt32(dataRow[PastOrder.POL_OUTS_0].ToString());
          desired = Convert.ToInt32(dataRow[PastOrder.POL_SEGMENT_SHEET_COUNT_0].ToString());
          produced = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_0].ToString()) * outs;
          badSheets += Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_BAD_0].ToString()) * outs;
          defaultcuts = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_DEFAULT_0].ToString());
          defaultSheets = defaultcuts * outs;
          //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
          scrapMadeDischargeGroup = Convert.ToInt32(dataRow[PastOrder.POL_SCRAP_MADE_DISCHARGE_GROUP_0].ToString());
        }
        catch (Exception ex)
        {
          Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting order 0 data.", ex);
        }
        tlb_desired0.Value = desired.ToString();
        tlb_produced0.Value = produced.ToString();
        //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
        if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].DischargeGroup ||
            SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].DischargeGroup ||
            SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].DischargeGroup)
        {
          tlb_bad0.Value = badSheets.ToString();
          nud_ScrapDischargeGroup0.Value = (scrapMadeDischargeGroup + (defaultcuts * Convert.ToInt32(dataRow[PastOrder.POL_CUT_LENGTH_DEFAULT_0].ToString()))).ToString();
          nud_ScrapDischargeGroup0.ValueForeColor = System.Drawing.SystemColors.WindowText;
        }
        else
          tlb_bad0.Value = badSheets.ToString() + " + " + defaultSheets.ToString() + " (" + defaultcuts + ")";

        this.pct_OrderPreprint0.Visible = SyncroConverter.ConvertByteToBoolean(dataRow[PastOrder.POL_PREPRINT_0].ToString());

        //order 1 
        if (Convert.ToInt64(dataRow[PastOrder.POL_SHEET_LENGTH_1].ToString()) != 0)
        {
          ttl_Order1.Text = DBManager.getDBManager().language.getTranslation(
          typeof(LevelName).Name + "." + dataRow[PastOrder.POL_LEVEL_NAME_1].ToString()) +
            " - " + dataRow[PastOrder.POL_ORDER_NUMBER_1].ToString();
          lbl_Customer1.Text = dataRow[PastOrder.POL_CUSTOMER_NAME_1].ToString().Trim();
          lbl_Misc1.Text = SyncroConverter.ConvertString(dataRow[PastOrder.POL_SHEET_WIDTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)
            + " X " + SyncroConverter.ConvertString(dataRow[PastOrder.POL_SHEET_LENGTH_1].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)
            + " X " + dataRow[PastOrder.POL_OUTS_1].ToString();
          outs = 0;
          desired = 0;
          produced = 0;
          //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
          /*
          badPlusDefault = 0;
          try
          {
            outs = Convert.ToInt32(dataRow[PastOrder.POL_OUTS_1].ToString());
            desired = Convert.ToInt32(dataRow[PastOrder.POL_SEGMENT_SHEET_COUNT_1].ToString());
            produced = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_1].ToString()) * outs;
            badPlusDefault += Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_BAD_1].ToString());
            badPlusDefault += Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_DEFAULT_1].ToString());
            badPlusDefault *= outs;
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting order 1 data", ex);
          }
          tlb_desired1.Value = desired.ToString();
          tlb_produced1.Value = produced.ToString();
          tlb_bad1.Value = badPlusDefault.ToString();
          this.pct_OrderPreprint1.Visible = SyncroConverter.ConvertByteToBoolean(dataRow[PastOrder.POL_PREPRINT_1].ToString());
          */
          badSheets = 0;
          defaultSheets = 0;
          defaultcuts = 0;
          scrapMadeDischargeGroup = 0;
          try
          {
            outs = Convert.ToInt32(dataRow[PastOrder.POL_OUTS_1].ToString());
            desired = Convert.ToInt32(dataRow[PastOrder.POL_SEGMENT_SHEET_COUNT_1].ToString());
            produced = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_1].ToString()) * outs;
            badSheets += Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_BAD_1].ToString()) * outs;
            defaultcuts = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_DEFAULT_1].ToString());
            defaultSheets = defaultcuts * outs;
            //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
            scrapMadeDischargeGroup = Convert.ToInt32(dataRow[PastOrder.POL_SCRAP_MADE_DISCHARGE_GROUP_1].ToString());
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting order 1 data", ex);
          }
          tlb_desired1.Value = desired.ToString();
          tlb_produced1.Value = produced.ToString();
          //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
          if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].DischargeGroup ||
              SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].DischargeGroup ||
              SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].DischargeGroup)
          {
            tlb_bad1.Value = badSheets.ToString();
            nud_ScrapDischargeGroup1.Value = (scrapMadeDischargeGroup + (defaultcuts * Convert.ToInt32(dataRow[PastOrder.POL_CUT_LENGTH_DEFAULT_1].ToString()))).ToString();
            nud_ScrapDischargeGroup1.ValueForeColor = System.Drawing.SystemColors.WindowText;
          }
          else
            tlb_bad1.Value = badSheets.ToString() + " + " + defaultSheets.ToString() + " (" + defaultcuts + ")";

          this.pct_OrderPreprint1.Visible = SyncroConverter.ConvertByteToBoolean(dataRow[PastOrder.POL_PREPRINT_1].ToString());
        }
        else
        {
          CleanOrderDetails(false, true, false);
        }

        //order 2
        if (Convert.ToInt64(dataRow[Order.DEL_SHEET_LENGTH_2].ToString()) != 0)
        {
          ttl_Order2.Text = DBManager.getDBManager().language.getTranslation(
            typeof(LevelName).Name + "." + dataRow[PastOrder.POL_LEVEL_NAME_2].ToString()) +
            " - " + dataRow[PastOrder.POL_ORDER_NUMBER_2].ToString();
          lbl_Customer2.Text = dataRow[PastOrder.POL_CUSTOMER_NAME_2].ToString().Trim();
          lbl_Misc2.Text = SyncroConverter.ConvertString(dataRow[PastOrder.POL_SHEET_WIDTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)
            + " X " + SyncroConverter.ConvertString(dataRow[PastOrder.POL_SHEET_LENGTH_2].ToString(), UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre)
            + " X " + dataRow[PastOrder.POL_OUTS_2].ToString();
          outs = 0;
          desired = 0;
          produced = 0;
          //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
          /*
          badPlusDefault = 0;
          try
          {
            outs = Convert.ToInt32(dataRow[PastOrder.POL_OUTS_2].ToString());
            desired = Convert.ToInt32(dataRow[PastOrder.POL_SEGMENT_SHEET_COUNT_2].ToString());
            produced = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_2].ToString()) * outs;
            badPlusDefault += Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_BAD_2].ToString());
            badPlusDefault += Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_DEFAULT_2].ToString());
            badPlusDefault *= outs;
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting order 2 data", ex);
          }
          tlb_desired2.Value = desired.ToString();
          tlb_produced2.Value = produced.ToString();
          tlb_bad2.Value = badPlusDefault.ToString();
          this.pct_OrderPreprint2.Visible = SyncroConverter.ConvertByteToBoolean(dataRow[PastOrder.POL_PREPRINT_2].ToString());
          */
          badSheets = 0;
          defaultSheets = 0;
          defaultcuts = 0;
          scrapMadeDischargeGroup = 0;
          try
          {
            outs = Convert.ToInt32(dataRow[PastOrder.POL_OUTS_2].ToString());
            desired = Convert.ToInt32(dataRow[PastOrder.POL_SEGMENT_SHEET_COUNT_2].ToString());
            produced = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_2].ToString()) * outs;
            badSheets += Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_BAD_2].ToString()) * outs;
            defaultcuts = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_DEFAULT_2].ToString());
            defaultSheets = defaultcuts * outs;
            //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
            scrapMadeDischargeGroup = Convert.ToInt32(dataRow[PastOrder.POL_SCRAP_MADE_DISCHARGE_GROUP_2].ToString());
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_SelectionChanged - error converting order 2 data", ex);
          }
          tlb_desired2.Value = desired.ToString();
          tlb_produced2.Value = produced.ToString();
          //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
          if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].DischargeGroup ||
              SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].DischargeGroup ||
              SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].DischargeGroup)
          {
            tlb_bad2.Value = badSheets.ToString();
            nud_ScrapDischargeGroup2.Value = (scrapMadeDischargeGroup + (defaultcuts * Convert.ToInt32(dataRow[PastOrder.POL_CUT_LENGTH_DEFAULT_2].ToString()))).ToString();
            nud_ScrapDischargeGroup2.ValueForeColor = System.Drawing.SystemColors.WindowText;
          }
          else
            tlb_bad2.Value = badSheets.ToString() + " + " + defaultSheets.ToString() + " (" + defaultcuts + ")";

          this.pct_OrderPreprint2.Visible = SyncroConverter.ConvertByteToBoolean(dataRow[PastOrder.POL_PREPRINT_2].ToString());
        }
        else
        {
          CleanOrderDetails(false, false, true);
        }
        //debug info
        lbl_SetupID.Text = "SetUpID: " + dataRow[PastOrder.POL_SETUP_ID].ToString();
        lbl_ModID.Text = "ModID: " + dataRow[PastOrder.POL_SETUP_MODID].ToString();
      }
      else
      {
        CleanOrderDetails(true, true, true);
      }
    }

    /// <summary>
    /// Clean all controls for order details
    /// </summary>
    private void CleanOrderDetails(bool level0, bool level1, bool level2)
    {
      //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
      if (level0)
      {
        ttl_Order0.Text = lbl_Customer0.Text = lbl_Misc0.Text =
        tlb_desired0.Value = tlb_produced0.Value = tlb_bad0.Value = nud_ScrapDischargeGroup0.Value = String.Empty;
        nud_ScrapDischargeGroup0.ValueForeColor = System.Drawing.Color.WhiteSmoke;
        this.pct_OrderPreprint0.Visible = false;
      }
      if (level1)
      {
        ttl_Order1.Text = lbl_Customer1.Text = lbl_Misc1.Text =
        tlb_desired1.Value = tlb_produced1.Value = tlb_bad1.Value = nud_ScrapDischargeGroup1.Value = String.Empty;
        nud_ScrapDischargeGroup1.ValueForeColor = System.Drawing.Color.WhiteSmoke;
        this.pct_OrderPreprint1.Visible = false;
      }
      if (level2)
      {
        ttl_Order2.Text = lbl_Customer2.Text = lbl_Misc2.Text =
        tlb_desired2.Value = tlb_produced2.Value = tlb_bad2.Value = nud_ScrapDischargeGroup2.Value = String.Empty;
        nud_ScrapDischargeGroup2.ValueForeColor = System.Drawing.Color.WhiteSmoke;
        this.pct_OrderPreprint2.Visible = false;
      }
      if (level0 && level1 && level2)
      {
        lbl_SetupID.Text = "SetupID: ";
        lbl_ModID.Text = "ModID: ";
      }
    }

    /// <summary>
    /// Show / Hide debug info.
    /// </summary>
    protected override void ControlD()
    {
      base.ControlD();
      lbl_SetupID.Visible = lbl_ModID.Visible = !lbl_SetupID.Visible;
    }

    private void Frm_OrdersList_Load(object sender, EventArgs e)
    {
      if (gdv_Orders.Rows.Count > 0)
        gdv_Orders.Rows[0].Selected = true;
    }

    private void gdv_Orders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      DataRow dataRow = ((DataRowView)gdv_Orders.Rows[e.RowIndex].DataBoundItem).Row;

      if (this.gdv_Orders.Columns[e.ColumnIndex].Name == PastOrder.POL_QUALITY_ID
            && e.Value != null)
      {
        e.Value = e.Value.ToString().Trim();
      }

      if (this.gdv_Orders.Columns[e.ColumnIndex].Name.Equals(PastOrder.POL_BEGIN_TIME)
            && e.Value != null)
      {
        try
        {
          e.Value = SyncroC.Utility.SyncroConverter.ConvertDateTimeToStrForDataGridView(e.Value);
        }
        catch (InvalidCastException ex)
        {
          Logger.LogError(this, "gdv_Orders_CellFormatting - " + PastOrder.POL_BEGIN_TIME, ex);
        }
      }

      if (this.gdv_Orders.Columns[e.ColumnIndex].Name.Equals(DB_METER_PROD_COL_NAME)
                        && e.Value != null)
      {
        try
        {
          //same calculation as SS meter prod column + Scrap at RSH
          int cutsProducedBad = 0;
          try
          {
            cutsProducedBad = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_BAD_0].ToString());
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + DB_METER_PROD_COL_NAME + " cutsProducedBad", ex);
          }
          int cutsProduced = 0;
          try
          {
            cutsProduced = Convert.ToInt32(e.Value);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + DB_METER_PROD_COL_NAME + " cutsProduced", ex);
          }
          int Cuts = cutsProducedBad + cutsProduced;
          int cutsProducedDefault = 0;
          try
          {
            cutsProducedDefault = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_DEFAULT_0].ToString());
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + DB_METER_PROD_COL_NAME + " cutsProducedDefault", ex);
          }
          //RFAZZI_FI07/06/2011_2. Adesso lato client utiliziamo la lunghezza di taglio a default memorizzata nei PastOrders anzichè la lunghezza prevista nel LineConfig. 
          //int mtDefault = cutsProducedDefault * (DBManager.getDBManager().lineConfig.KnifeConfig.ChopOutLengthAOC / 1000);
          int mtDefault = (cutsProducedDefault * Convert.ToInt32(dataRow[PastOrder.POL_CUT_LENGTH_DEFAULT_0])) / 1000;

          int sheetLength = 0;
          try
          {
            sheetLength = Convert.ToInt32(dataRow[PastOrder.POL_SHEET_LENGTH_0]);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + DB_METER_PROD_COL_NAME + " sheetLength", ex);
          }
          int scrapRSH = 0;
          try
          {
            scrapRSH = Convert.ToInt32(dataRow[PastOrder.SCRAP_RSH_UDm]);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + DB_METER_PROD_COL_NAME + " scrapRSH", ex);
          }

          e.Value = (Cuts * sheetLength / 10000) + mtDefault + scrapRSH;
        }
        catch (InvalidCastException exception)
        {
          Logger.LogError(this, "gdv_Orders_CellFormatting - " + DB_METER_PROD_COL_NAME, exception);
        }
      }

      if (this.gdv_Orders.Columns[e.ColumnIndex].Name.Equals(SS_METER_PROD_COL_NAME)
            && e.Value != null)
      {
        try
        {
          int cutsProducedBad = 0;
          try
          {
            cutsProducedBad = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_BAD_0].ToString());
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + SS_METER_PROD_COL_NAME + " cutsProducedBad", ex);
          }
          int cutsProduced = 0;
          try
          {
            cutsProduced = Convert.ToInt32(e.Value);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + SS_METER_PROD_COL_NAME + " cutsProduced", ex);
          }
          int Cuts = cutsProducedBad + cutsProduced;
          int cutsProducedDefault = 0;
          try
          {
            cutsProducedDefault = Convert.ToInt32(dataRow[PastOrder.POL_CUTS_PRODUCED_DEFAULT_0].ToString());
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + SS_METER_PROD_COL_NAME + " cutsProducedDefault", ex);
          }
          //RFAZZI_FI07/06/2011_2. Adesso lato client utiliziamo la lunghezza di taglio a default memorizzata nei PastOrders anzichè la lunghezza prevista nel LineConfig. 
          //int mtDefault = cutsProducedDefault * (DBManager.getDBManager().lineConfig.KnifeConfig.ChopOutLengthAOC / 1000);
          int mtDefault = (cutsProducedDefault * Convert.ToInt32(dataRow[PastOrder.POL_CUT_LENGTH_DEFAULT_0])) / 1000;

          int sheetLength = 0;
          try
          {
            sheetLength = Convert.ToInt32(dataRow[PastOrder.POL_SHEET_LENGTH_0]);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_CellFormatting - " + SS_METER_PROD_COL_NAME + " sheetLength", ex);
          }

          e.Value = (Cuts * sheetLength / 10000) + mtDefault;
        }
        catch (InvalidCastException ex)
        {
          Logger.LogError(this, "gdv_Orders_CellFormatting - " + SS_METER_PROD_COL_NAME, ex);
        }
      }

      if (this.gdv_Orders.Columns[e.ColumnIndex].Name.Equals(PastOrder.POL_STOPTIME)
            && e.Value != null)
      {
        try
        {
          string formattedTime = SyncroConverter.ConvertToFormattedTime(Convert.ToInt32(e.Value.ToString()));
          e.Value = formattedTime;
        }
        catch (InvalidCastException ex)
        {
          Logger.LogError(this, "gdv_Orders_CellFormatting - " + PastOrder.POL_STOPTIME, ex);
        }
      }
    }

    private void gdv_Orders_DoubleClick(object sender, EventArgs e)
    {
      if (gdv_Orders.SelectedRows.Count == 1)
      {
        DataRow dataRow = ((DataRowView)gdv_Orders.SelectedRows[0].DataBoundItem).Row;
        int position = 1;
        if (dataRow != null)
        {
          try
          {
            position = Convert.ToInt32(dataRow[PastOrder.POL_POSITION].ToString());
            //position = DBManager.getDBManager().pastOrder.getIndex(dataRow);
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_DoubleClick - error converting value.", ex);
          }
        }
        else
        {
          Logger.LogError(this, "gdv_Orders_DoubleClick - datarow was null");
        }
        Frm_StatisticsGroup.positionOrderToShowPastDRY = position;
        Frm_StatisticsGroup.filtro = this.filter;
      }
      askForPage(typeof(Frm_StatisticsOrdersDetail));
    }

    private void btn_ShowGraph_Click(object sender, EventArgs e)
    {
      if (gdv_Orders.SelectedRows.Count == 1)
      {
        DataRow dataRow = ((DataRowView)gdv_Orders.SelectedRows[0].DataBoundItem).Row;
        DateTime dateTime = DateTime.MinValue;
        if (dataRow != null)
        {
          try
          {
            dateTime = (DateTime)dataRow[PastOrder.POL_BEGIN_TIME];
          }
          catch (Exception ex)
          {
            Logger.LogError(this, "gdv_Orders_DoubleClick - error converting value.", ex);
          }
        }
        else
        {
          Logger.LogError(this, "gdv_Orders_DoubleClick - datarow was null");
        }

        SyncroC.Forms.Pages.Statistics.Frm_ProductionTrace.from_Universal = dateTime;
      }

      askForPage(typeof(SyncroC.Forms.Pages.Statistics.Frm_ProductionTrace));
    }

  }
}