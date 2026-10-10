using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SyncroC.Network;
using SyncroC.Utility;
using SyncroC.Controls;
using SyncroC.DataBase.EventArgs;
using SyncroC.DataBase;
using SyncroC.DataBase.LineConfig;
using SyncroC.Controls.Colors;
using Microsoft.Tools.Graphs.Legends;
using SyncroC.Controls.Interfaces;

namespace SyncroC.Forms.Pages.Statistics.Orders
{
	public partial class Frm_StatisticsOrdersDetail : Frm_StatisticsOrdersSubgroup
	{
		private string FORMAT_SEPARATOR = " X ";
		private string SHEETS_SEPARATOR = " / ";

		public Frm_StatisticsOrdersDetail()
		{
			InitializeComponent();

			//CUT TO MARK
			//RFAZZI_FI.Changed the above value for level
			//if (this.knife.CutToMark)
      if (DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].CutToMark || DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].CutToMark || DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].CutToMark)
			{
				//level 0
				this.ttb_Measures0.Size = new Size(this.ttb_Measures0.Size.Width, (this.ttb_Measures0.Size.Height - this.chk_CutToMark0.Size.Height));
				this.ttl_Score0.Size = new Size(this.ttl_Score0.Size.Width, (this.ttl_Score0.Size.Height - this.chk_CutToMark0.Size.Height));
				this.chk_CutToMark0.Visible = true;

				//level 1
				this.ttb_Measures1.Size = new Size(this.ttb_Measures1.Size.Width, (this.ttb_Measures1.Size.Height - this.chk_CutToMark1.Size.Height));
				this.ttl_Score1.Size = new Size(this.ttl_Score1.Size.Width, (this.ttl_Score1.Size.Height - this.chk_CutToMark1.Size.Height));
				this.chk_CutToMark1.Visible = true;

				//level 2
				this.ttb_Measures2.Size = new Size(this.ttb_Measures2.Size.Width, (this.ttb_Measures2.Size.Height - this.chk_CutToMark2.Size.Height));
				this.ttl_Score2.Size = new Size(this.ttl_Score2.Size.Width, (this.ttl_Score2.Size.Height - this.chk_CutToMark2.Size.Height));
				this.chk_CutToMark2.Visible = true;
			}

			this.quality_RowFill(null, null);
			this.setListFluteItems(-1, true);

			this.setTitledListItems(this.cmb_TrimType, typeof(SyncroC.Utility.TrimType), new bool[] { true, true }, -1, true);
			//Order 0
      this.setTitledListItems(this.cmb_Level0, typeof(SyncroC.Utility.LevelName), new bool[] { false, true, (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount == 3), (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount >= 2) }, -1, true);
			this.setTitledListItems(this.cmb_sending0, typeof(SyncroC.Utility.PalletSending), new bool[] { true, true, true }, -1, true);
			this.setListPositioningItems(this.cmb_Positioning0, -1, true);
			this.setListScorerItems(this.cmb_Type0, null, -1, true);
			//Order 1
      this.setTitledListItems(this.cmb_Level1, typeof(SyncroC.Utility.LevelName), new bool[] { false, true, (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount == 3), (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount >= 2) }, -1, true);
			this.setTitledListItems(this.cmb_sending1, typeof(SyncroC.Utility.PalletSending), new bool[] { true, true, true }, -1, true);
			this.setListPositioningItems(this.cmb_Positioning1, -1, true);
			this.setListScorerItems(this.cmb_Type1, null, -1, true);
			//Order 2
      this.setTitledListItems(this.cmb_Level2, typeof(SyncroC.Utility.LevelName), new bool[] { false, true, (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount == 3), (DBManager.getDBManager().lineConfig.KnifeConfig.LevelCount >= 2) }, -1, true);
			this.setTitledListItems(this.cmb_sending2, typeof(SyncroC.Utility.PalletSending), new bool[] { true, true, true }, -1, true);
			this.setListPositioningItems(this.cmb_Positioning2, -1, true);
			this.setListScorerItems(this.cmb_Type2, null, -1, true);

      DBManager.getDBManager().quality.OnDBQualityChange += new Quality.DBChangedHandler(quality_RowFill);

      if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
      {
        this.btn_ShowGraph.Visible = true;

        if (SyncroC.DataBase.PRCManager.getPRCManager().canPrint())
          this.btn_printPanel.Visible = true;
        else
          this.btn_printPanel.Visible = false;
      }
		}

    protected override void ControlD()
    {
      base.ControlD();
      this.SetupId.Visible = !this.SetupId.Visible;
      this.ModId.Visible = !this.ModId.Visible;
    }

    public override void _Show()
    {
      base._Show();
      this.showOrder(Frm_StatisticsGroup.positionOrderToShowPastDRY, Frm_StatisticsGroup.filtro);
    }

		protected override void F11()
		{
		}

    void quality_RowFill(object sender, DBChangedEventArgs e)
		{
      //agrassi_FI07-09-09_2
      try
      {
        DataTable quality = DBManager.getDBManager().quality.getTable();
			  //Create the quality list
			  this.cmb_Quality.ClearItems();
			  string[] stringRows = new string[quality.Rows.Count];
			  int i = 0;
			  foreach (System.Data.DataRow r in quality.Rows)
				  stringRows[i++] = r[Quality.QUALITY_ID].ToString();
			  this.cmb_Quality.AddRangeItem(stringRows);
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "Error in quality_RowFill:" + ex.Message);
      }
    }

		/// <summary>
		/// Show previous order.
		/// </summary>
		protected override void PageUp()
		{
			if (positionOrderToShowPastDRY == 0)
				return;
      this.showOrder(--positionOrderToShowPastDRY, Frm_StatisticsGroup.filtro);
		}

		/// <summary>
		/// Show next order.
		/// </summary>
		protected override void PageDown()
		{
      if (positionOrderToShowPastDRY == DBManager.getDBManager().pastOrder.getCount(Frm_StatisticsGroup.filtro) - 1)
				return;
      this.showOrder(++positionOrderToShowPastDRY, Frm_StatisticsGroup.filtro);
		}

    private void setGraphVisible()
    {
      //ABELLO_FI24/11/2010_2 [PRC] Aggiunta storicizzazione e visualizzazione dell'umidità sopra/sotto
      if (!this.graphLines_Speed0.Visible)
      {
        if (!SyncroC.DBManager.getDBManager().lineConfig.PRCConfig.Header.EnableBottom_UpperMoistureSensor)
        {
          int yOffset = this.ttl_RECORD.Size.Height;

          this.pct_DB.Location = new Point(this.pct_DB.Location.X, this.pct_DB.Location.Y - yOffset);
          this.pct_PHT.Location = new Point(this.pct_PHT.Location.X, this.pct_PHT.Location.Y - yOffset);
          this.pct_sf1.Location = new Point(this.pct_sf1.Location.X, this.pct_sf1.Location.Y - yOffset);
          this.pct_sf2.Location = new Point(this.pct_sf2.Location.X, this.pct_sf2.Location.Y - yOffset);
          this.graphLines_GlueGap0.Location = new Point(this.graphLines_GlueGap0.Location.X, this.graphLines_GlueGap0.Location.Y - yOffset);
          this.graphLines_GlueGap1.Location = new Point(this.graphLines_GlueGap1.Location.X, this.graphLines_GlueGap1.Location.Y - yOffset);
          this.graphLines_GlueGap2.Location = new Point(this.graphLines_GlueGap2.Location.X, this.graphLines_GlueGap2.Location.Y - yOffset);
          this.graphLines_Speed0.Location = new Point(this.graphLines_Speed0.Location.X, this.graphLines_Speed0.Location.Y - yOffset);
          this.graphLines_Speed1.Location = new Point(this.graphLines_Speed1.Location.X, this.graphLines_Speed1.Location.Y - yOffset);
          this.graphLines_Speed2.Location = new Point(this.graphLines_Speed2.Location.X, this.graphLines_Speed2.Location.Y - yOffset);
          this.graphLines_Temp0.Location = new Point(this.graphLines_Temp0.Location.X, this.graphLines_Temp0.Location.Y - yOffset);
          this.graphLines_Temp1.Location = new Point(this.graphLines_Temp1.Location.X, this.graphLines_Temp1.Location.Y - yOffset);
          this.graphLines_Temp2.Location = new Point(this.graphLines_Temp2.Location.X, this.graphLines_Temp2.Location.Y - yOffset);
          this.ttl_Zone0.Location = new Point(this.ttl_Zone0.Location.X, this.ttl_Zone0.Location.Y - yOffset);
          this.ttl_Zone1.Location = new Point(this.ttl_Zone1.Location.X, this.ttl_Zone1.Location.Y - yOffset);
          this.ttl_Zone2.Location = new Point(this.ttl_Zone2.Location.X, this.ttl_Zone2.Location.Y - yOffset);
          //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
          if (DBManager.getDBManager().lineConfig.SingleFacer3)
          {
            this.pct_sf3.Location = new Point(this.pct_sf3.Location.X, this.pct_sf3.Location.Y - yOffset);
            this.graphLines_GlueGap3.Location = new Point(this.graphLines_GlueGap3.Location.X, this.graphLines_GlueGap3.Location.Y - yOffset);
            this.graphLines_Speed3.Location = new Point(this.graphLines_Speed3.Location.X, this.graphLines_Speed3.Location.Y - yOffset);
            this.graphLines_Temp3.Location = new Point(this.graphLines_Temp3.Location.X, this.graphLines_Temp3.Location.Y - yOffset);
            this.ttl_Zone3.Location = new Point(this.ttl_Zone3.Location.X, this.ttl_Zone3.Location.Y - yOffset);
          }
        }
        else
        {
          this.ttl_RECORD.Visible = true;
          this.graphLines_Moisture.Visible = true;
        }

        this.pct_DB.Visible = true;
        this.pct_PHT.Visible = true;
        this.pct_sf1.Visible = true;
        this.pct_sf2.Visible = true;
        this.graphLines_GlueGap0.Visible = true;
        this.graphLines_GlueGap1.Visible = true;
        this.graphLines_GlueGap2.Visible = true;
        this.graphLines_Speed0.Visible = true;
        this.graphLines_Speed1.Visible = true;
        this.graphLines_Speed2.Visible = true;
        this.graphLines_Temp0.Visible = true;
        this.graphLines_Temp1.Visible = true;
        this.graphLines_Temp2.Visible = true;
        this.lbl_Initial_Roll1.Visible = true;
        this.lbl_Initial_Roll2.Visible = true;
        this.lbl_Initial_Roll3.Visible = true;
        this.lbl_Initial_Roll4.Visible = true;
        this.lbl_Initial_Roll5.Visible = true;
        this.ttl_rolls.Visible = true;
        this.ttl_Zone0.Visible = true;
        this.ttl_Zone1.Visible = true;
        this.ttl_Zone2.Visible = true;
        //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
        if (DBManager.getDBManager().lineConfig.SingleFacer3)
        {
          this.pct_sf3.Visible = true;
          this.graphLines_GlueGap3.Visible = true;
          this.graphLines_Speed3.Visible = true;
          this.graphLines_Temp3.Visible = true;
          this.ttl_Zone3.Visible = true;
          this.lbl_Initial_Roll6.Visible = true;
          this.lbl_Initial_Roll7.Visible = true;
        }
      }
    }

    private void showGraph(int orderId, DateTime from, DateTime to)
    {
      try
      {
        this.setGraphVisible();

        //questo perchè il log sulla traccia potrebbe essere fatto un pò dopo il vero AOC
        DateTime fromPerAOCTraceRolls = from.Subtract(TimeSpan.FromSeconds(10));
        DateTime toPerAOCTraceRolls = to.AddSeconds(10);

        //ROLLS
        string myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".trace_syc_rolls";
        string where = String.Format("((time BETWEEN '{0}' AND '{1}') AND setupid = {2})", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(fromPerAOCTraceRolls), SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(toPerAOCTraceRolls), orderId);
        DataTable dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + " WHERE " + where + " ORDER BY time ASC LIMIT 1;");

        if ((dt != null) && (dt.Rows.Count != 0))
        {
          if ((int)dt.Rows[0]["weightfactor1_uwg_m"] > 0)
          {
            this.pct1.Visible = true;
            this.lbl_Initial_Roll1.Text = dt.Rows[0]["papertype1"] + " (" + dt.Rows[0]["weightfactor1_uwg_m"] + ") - " + dt.Rows[0]["rollid1"];
          }
          else
          {
            this.pct1.Visible = false;
            this.lbl_Initial_Roll1.Text = "";
          }

          if ((int)dt.Rows[0]["weightfactor2_uwg_m"] > 0)
          {
            this.pct2.Visible = true;
            this.lbl_Initial_Roll2.Text = dt.Rows[0]["papertype2"] + " (" + dt.Rows[0]["weightfactor2_uwg_m"] + ") - " + dt.Rows[0]["rollid2"];
          }
          else
          {
            this.pct2.Visible = false;
            this.lbl_Initial_Roll2.Text = "";
          }

          if ((int)dt.Rows[0]["weightfactor3_uwg_m"] > 0)
          {
            this.pct3.Visible = true;
            this.lbl_Initial_Roll3.Text = dt.Rows[0]["papertype3"] + " (" + dt.Rows[0]["weightfactor3_uwg_m"] + ") - " + dt.Rows[0]["rollid3"];
          }
          else
          {
            this.pct3.Visible = false;
            this.lbl_Initial_Roll3.Text = "";
          }

          if ((int)dt.Rows[0]["weightfactor4_uwg_m"] > 0)
          {
            this.pct4.Visible = true;
            this.lbl_Initial_Roll4.Text = dt.Rows[0]["papertype4"] + " (" + dt.Rows[0]["weightfactor4_uwg_m"] + ") - " + dt.Rows[0]["rollid4"];
          }
          else
          {
            this.pct4.Visible = false;
            this.lbl_Initial_Roll4.Text = "";
          }

          if ((int)dt.Rows[0]["weightfactor5_uwg_m"] > 0)
          {
            this.pct5.Visible = true;
            this.lbl_Initial_Roll5.Text = dt.Rows[0]["papertype5"] + " (" + dt.Rows[0]["weightfactor5_uwg_m"] + ") - " + dt.Rows[0]["rollid5"];
          }
          else
          {
            this.pct5.Visible = false;
            this.lbl_Initial_Roll5.Text = "";
          }

          //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
          if (DBManager.getDBManager().lineConfig.SingleFacer3 && ((int)dt.Rows[0]["weightfactor6_uwg_m"] > 0) )
          {
            this.pct6.Visible = true;
            this.lbl_Initial_Roll6.Text = dt.Rows[0]["papertype6"] + " (" + dt.Rows[0]["weightfactor6_uwg_m"] + ") - " + dt.Rows[0]["rollid6"];
          }
          else
          {
            this.pct6.Visible = false;
            this.lbl_Initial_Roll6.Text = "";
          }

          if (DBManager.getDBManager().lineConfig.SingleFacer3 && ((int)dt.Rows[0]["weightfactor7_uwg_m"] > 0) )
          {
            this.pct7.Visible = true;
            this.lbl_Initial_Roll7.Text = dt.Rows[0]["papertype7"] + " (" + dt.Rows[0]["weightfactor7_uwg_m"] + ") - " + dt.Rows[0]["rollid7"];
          }
          else
          {
            this.pct7.Visible = false;
            this.lbl_Initial_Roll7.Text = "";
          }
          
          myTableName = SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.getSQL_DATABASE() + ".trace_syc_rolls";
          where = String.Format("((time BETWEEN '{0}' AND '{1}') AND type LIKE 'Splice _ at DB')", SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(from), SyncroC.DataBase.SqlManager.dateTimeToSQLDateTime(to));
          dt = SyncroC.DataBase.SqlManager.getSqlManager().doSelect("SELECT * FROM " + myTableName + " WHERE " + where + " ORDER BY time ASC;");

          if ((dt != null) && (dt.Rows.Count != 0))
          {
           foreach (DataRow a in dt.Rows)
           {
             string typeOfRecord = (string)a["type"];

             string splNumberStr = typeOfRecord.Replace("Splice", "");
             splNumberStr = splNumberStr.Replace("at DB", "");
             int splNumber = Convert.ToInt32(splNumberStr.Trim());

             Label labelToWrite = null;
             switch (splNumber)
             {
               case 1:
                 labelToWrite = this.lbl_Initial_Roll1;
                 break;
               case 2:
                 labelToWrite = this.lbl_Initial_Roll2;
                 break;
               case 3:
                 labelToWrite = this.lbl_Initial_Roll3;
                 break;
               case 4:
                 labelToWrite = this.lbl_Initial_Roll4;
                 break;
               case 5:
                 labelToWrite = this.lbl_Initial_Roll5;
                 break;
               //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
               case 6:
                 if (DBManager.getDBManager().lineConfig.SingleFacer3)
                  labelToWrite = this.lbl_Initial_Roll6;
                 break;
               case 7:
                 if (DBManager.getDBManager().lineConfig.SingleFacer3)
                   labelToWrite = this.lbl_Initial_Roll7;
                 break;
               default:
                 labelToWrite = null;
                 break;
              }

             if((labelToWrite != null) && (labelToWrite.Text != ""))
               labelToWrite.Text += String.Format(" -> {2} | {0} ({1}) - {3}", ((string)a["papertype" + splNumber]), ((int)a["weightfactor" + splNumber + "_uwg_m"]), ((int)a["metersproduceddb_udm"]), ((string)a["rollid" + splNumber]));
            }
          }
        }
        
        //SPEED
        SyncroC.DataBase.TRACEManager.getTRACEManager().drawSpeed(this.graphLines_Speed0, 0, from, to, 60, false);
        SyncroC.DataBase.TRACEManager.getTRACEManager().drawSpeed(this.graphLines_Speed1, 1, from, to, 60, false);
        SyncroC.DataBase.TRACEManager.getTRACEManager().drawSpeed(this.graphLines_Speed2, 2, from, to, 60, false);
        //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
        if (DBManager.getDBManager().lineConfig.SingleFacer3)
          SyncroC.DataBase.TRACEManager.getTRACEManager().drawSpeed(this.graphLines_Speed3, 3, from, to, 60, false);

        //GLUE
        SyncroC.DataBase.TRACEManager.getTRACEManager().drawGlue(this.graphLines_GlueGap0, 0, from, to, 60, false);
        SyncroC.DataBase.TRACEManager.getTRACEManager().drawGlue(this.graphLines_GlueGap1, 1, from, to, 60, false);
        SyncroC.DataBase.TRACEManager.getTRACEManager().drawGlue(this.graphLines_GlueGap2, 2, from, to, 60, false);
        //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
        if (DBManager.getDBManager().lineConfig.SingleFacer3)
          SyncroC.DataBase.TRACEManager.getTRACEManager().drawGlue(this.graphLines_GlueGap3, 3, from, to, 60, false);

        if (!SyncroC.Utility.Settings.getSettings().configuration.PRC)
        {
          //WRAP ARM POSITION
          this.graphLines_Temp0.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.TRACE_WrapArmPositionGraphLinesEditor;
          SyncroC.DataBase.TRACEManager.getTRACEManager().drawWrapArmPosition(this.graphLines_Temp0, 0, from, to, 60, false);
          this.graphLines_Temp1.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.TRACE_WrapArmPositionGraphLinesEditor;
          SyncroC.DataBase.TRACEManager.getTRACEManager().drawWrapArmPosition(this.graphLines_Temp1, 1, from, to, 60, false);
          this.graphLines_Temp2.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.TRACE_WrapArmPositionGraphLinesEditor;
          SyncroC.DataBase.TRACEManager.getTRACEManager().drawWrapArmPosition(this.graphLines_Temp2, 2, from, to, 60, false);
          //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
          if (DBManager.getDBManager().lineConfig.SingleFacer3)
          {
            this.graphLines_Temp3.MyTypeOfGraphLinesEditor = SyncroC.Graph.GraphLines.TypeOfGraphLinesEditor.TRACE_WrapArmPositionGraphLinesEditor;
            SyncroC.DataBase.TRACEManager.getTRACEManager().drawWrapArmPosition(this.graphLines_Temp3, 3, from, to, 60, false);
          }

          //ABELLO_FI04/11/2010_1 Traduzione sbagliata
          this.graphLines_Temp0.Text = DBManager.getDBManager().language.getTranslation("Frm_ProductionTrace.graphLines_WrapPosition.Text");
          this.graphLines_Temp1.Text = DBManager.getDBManager().language.getTranslation("Frm_ProductionTrace.graphLines_WrapPosition.Text");
          this.graphLines_Temp2.Text = DBManager.getDBManager().language.getTranslation("Frm_ProductionTrace.graphLines_WrapPosition.Text");
          //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
          if (DBManager.getDBManager().lineConfig.SingleFacer3)
            this.graphLines_Temp3.Text = DBManager.getDBManager().language.getTranslation("Frm_ProductionTrace.graphLines_WrapPosition.Text");
        }
        else
        {
          //WRAP ARM TEMPERATURE
          SyncroC.DataBase.TRACEManager.getTRACEManager().drawTemperature(this.graphLines_Temp0, 0, from, to, 60, false);
          SyncroC.DataBase.TRACEManager.getTRACEManager().drawTemperature(this.graphLines_Temp1, 1, from, to, 60, false);
          SyncroC.DataBase.TRACEManager.getTRACEManager().drawTemperature(this.graphLines_Temp2, 2, from, to, 60, false);
          //RFAZZI_FI19/08/2011_1. Aggiunta la terza zona in RECORD. 
          if (DBManager.getDBManager().lineConfig.SingleFacer3)
            SyncroC.DataBase.TRACEManager.getTRACEManager().drawTemperature(this.graphLines_Temp3, 3, from, to, 60, false);

          if (SyncroC.DBManager.getDBManager().lineConfig.PRCConfig.Header.EnableBottom_UpperMoistureSensor)
          {
            SyncroC.DataBase.TRACEManager.getTRACEManager().drawMoisture(this.graphLines_Moisture, from, to, 180, false);
          }
        }
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "showGraph - PastOrder error.",ex);
      }
    }

		/// <summary>
		/// Show the specified order into the screen.
		/// </summary>
		/// <param name="position">Position of the order to be showed.</param>
		private void showOrder(int position, string filtro)
		{
      this.Cursor = Cursors.WaitCursor;

      long count = DBManager.getDBManager().pastOrder.getCount(filtro);

			this.cleanAllFields(false);
      if (count == 0)
			{
				Logger.LogError(this, "showOrder - PastOrder queue is empty.");
				return;
			}
			if (position < 1)
			{
				position = 1;
			}
      if (position > count)
			{
        position = (int)count;
			}

      //ABELLO_FI25/08/2010_1 se uso Postgres il doppio click sulla lista degli eseguiti andava a casaccio
      DataRow row = DBManager.getDBManager().pastOrder.getRow(position, filtro);

      if (row != null)
			{
				this.ttb_index.Value = position.ToString();

				//HEADER
				this.SetupId.Text = row[PastOrder.POL_SETUP_ID].ToString();
				this.ModId.Text = row[PastOrder.POL_SETUP_MODID].ToString();
				string setUpCode = row[PastOrder.POL_SETUP_CODE].ToString();
				ttb_ProgramNumber.Value = setUpCode.Substring(0, setUpCode.Length - 2);
				ttb_SetUpSequence.Value = setUpCode.Substring(setUpCode.Length - 2);
				this.nud_WebWidth.Value = row[PastOrder.POL_WEB_WIDTH].ToString();
				this.cmb_TrimType.SelectedItemData = row[PastOrder.POL_TRIM_TYPE].ToString();
				this.nud_ScoreGap.Value = row[PastOrder.POL_SCORE_GAP].ToString();
        DataRow qualityRow = DBManager.getDBManager().quality.getRow(row[PastOrder.POL_QUALITY_ID].ToString());
				if (qualityRow != null)
					this.cmb_Flute.SelectedItem = qualityRow[Quality.FLUTE_ID].ToString();
				this.cmb_Quality.SelectedItem = row[PastOrder.POL_QUALITY_ID].ToString();
				this.nud_Trim.Value = row[PastOrder.POL_TRIM].ToString();
				int setupLength = this.intParse(row[PastOrder.POL_SHEET_LENGTH_0].ToString()) *
					 this.intParse(row[PastOrder.POL_SEGMENT_SHEET_COUNT_0].ToString()) / this.intParse(row[PastOrder.POL_OUTS_0].ToString());
				setupLength = (int)SyncroConverter.Convert(setupLength, UnitMeasure.DeciMilliMetre, UnitMeasure.Metre);
				this.nud_SetupLength.Value = setupLength.ToString();
        this.mkt_BeginTime.ValueAsUNIVERSALDateTime = (DateTime)row[PastOrder.POL_BEGIN_TIME];
        
        string formattedTime = string.Empty;
				try
				{
					formattedTime = SyncroConverter.ConvertToFormattedTime(Convert.ToInt32(row[PastOrder.POL_STOPTIME].ToString()));
				}
				catch (Exception e)
				{
					Logger.LogError(this, "showOrder - error formatting time", e);
				}
				this.ttl_StopTime.Value = formattedTime;
				this.tnud_AVGSpeed.Value = row[PastOrder.POL_AVERAGE_SPEED].ToString();

        //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
				/*
        int length, width, outs, desired, produced, badPlusDefault;
        */
        int length, width, outs, desired, produced, badSheets; // badPlusDefault;        
        int defaultSheets = 0,
            defaultCuts = 0,
            scrapMadeDischargeGroup = 0;

				//PastOrder 0
				if (int.Parse(row[PastOrder.POL_OUTS_0].ToString()) > 0)
				{
					ttl_ord0.ForeColor = Color.LightGreen;
					this.ttb_OrderNumber0.Value = row[PastOrder.POL_ORDER_NUMBER_0].ToString();
					this.mkt_Delivery0.ValueAsUNIVERSALDateTime = (DateTime)row[PastOrder.POL_DELIVERY_0];					
          this.ttb_CustName0.Value = row[PastOrder.POL_CUSTOMER_NAME_0].ToString();
					this.ttb_Measures0.Value = Measures_dbToComponent(row[PastOrder.POL_SCORE_MEASURES_0].ToString());
					this.cmb_Positioning0.SelectedItemData = row[PastOrder.POL_POSIT_TYPE_0].ToString();
					this.cmb_Type0.SelectedItem = row[PastOrder.POL_SCORE_TYPE_0].ToString();
					this.cmb_Level0.SelectedItemData = row[PastOrder.POL_LEVEL_NAME_0].ToString();
					this.nud_sheetsXstack0.Value = row[PastOrder.POL_SHEETS_PER_STACK_0].ToString();
					this.nud_stacksXpallet0.Value = row[PastOrder.POL_STACKS_PER_PALLET_0].ToString();
					this.cmb_sending0.SelectedItemData = row[PastOrder.POL_SENDING_0].ToString();
          //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
					/*
           length = width = outs = desired = produced = badPlusDefault = 0;  
          */
          length = width = outs = desired = produced = badSheets = 0;   
          this.chk_CutToMark0.Checked = Convert.ToBoolean(row[PastOrder.POL_CUT_TO_MARK_0].ToString());

					try
					{
						length = Convert.ToInt32(row[PastOrder.POL_SHEET_LENGTH_0]);
						width = Convert.ToInt32(row[PastOrder.POL_SHEET_WIDTH_0]);
						outs = Convert.ToInt32(row[PastOrder.POL_OUTS_0].ToString());
						desired = Convert.ToInt32(row[PastOrder.POL_SEGMENT_SHEET_COUNT_0].ToString());
						produced = Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_0].ToString()) * outs;
            //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
						/*
            badPlusDefault += Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_BAD_0].ToString());
						badPlusDefault += Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_DEFAULT_0].ToString());
						badPlusDefault *= outs;
            */
            badSheets = Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_BAD_0].ToString()) * outs;
            defaultCuts = Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_DEFAULT_0].ToString());
            defaultSheets = defaultCuts * outs;

            //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
            scrapMadeDischargeGroup = Convert.ToInt32(row[PastOrder.POL_SCRAP_MADE_DISCHARGE_GROUP_0].ToString());

					}
					catch (Exception ex)
					{
						Logger.LogError(this, "showOrder - error converting order 0 data.", ex);
					}
					this.ttb_Format0.Value = (width / 10).ToString() + FORMAT_SEPARATOR +
						(length / 10).ToString() + FORMAT_SEPARATOR +
						outs.ToString();
          //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
					/*
          this.ttb_Sheets0.Value = desired.ToString() + SHEETS_SEPARATOR + 
						produced.ToString() + SHEETS_SEPARATOR + 
						badPlusDefault.ToString();
          */

          //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
          if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].DischargeGroup ||
              SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].DischargeGroup ||
              SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].DischargeGroup)
          {

            this.ttb_Sheets0.Value = desired.ToString() + SHEETS_SEPARATOR +
              produced.ToString() + SHEETS_SEPARATOR +
              badSheets.ToString() + SHEETS_SEPARATOR +
              (scrapMadeDischargeGroup + (defaultCuts * Convert.ToInt32(row[PastOrder.POL_CUT_LENGTH_DEFAULT_0].ToString()))).ToString();
          }
          else
          {
            this.ttb_Sheets0.Value = desired.ToString() + SHEETS_SEPARATOR +
              produced.ToString() + SHEETS_SEPARATOR +
              badSheets.ToString()
              + " + " + defaultSheets.ToString() + " (" + defaultCuts.ToString() + ")";
          }
				}

				//ORDER 1
				if (int.Parse(row[PastOrder.POL_OUTS_1].ToString()) > 0)
				{
					ttl_ord1.ForeColor = Color.LightGreen;
					this.ttb_OrderNumber1.Value = row[PastOrder.POL_ORDER_NUMBER_1].ToString();
          this.mkt_Delivery1.ValueAsUNIVERSALDateTime = (DateTime)row[PastOrder.POL_DELIVERY_1];
          this.ttb_CustName1.Value = row[PastOrder.POL_CUSTOMER_NAME_1].ToString();
					this.ttb_Measures1.Value = Measures_dbToComponent(row[PastOrder.POL_SCORE_MEASURES_1].ToString());
					this.cmb_Positioning1.SelectedItemData = row[PastOrder.POL_POSIT_TYPE_1].ToString();
					this.cmb_Type1.SelectedItem = row[PastOrder.POL_SCORE_TYPE_1].ToString();
					this.cmb_Level1.SelectedItemData = row[PastOrder.POL_LEVEL_NAME_1].ToString();
					this.nud_sheetsXstack1.Value = row[PastOrder.POL_SHEETS_PER_STACK_1].ToString();
					this.nud_stacksXpallet1.Value = row[PastOrder.POL_STACKS_PER_PALLET_1].ToString();
					this.cmb_sending1.SelectedItemData = row[PastOrder.POL_SENDING_1].ToString();
          //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
          /*
					length = width = outs = desired = produced = badPlusDefault = 0;
          */
          length = width = outs = desired = produced = badSheets = 0;
          this.chk_CutToMark1.Checked = Convert.ToBoolean(row[PastOrder.POL_CUT_TO_MARK_1].ToString());

          //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
          defaultSheets = 0;
          defaultCuts = 0;
          scrapMadeDischargeGroup = 0;

          try
					{
						length = Convert.ToInt32(row[PastOrder.POL_SHEET_LENGTH_1]);
						width = Convert.ToInt32(row[PastOrder.POL_SHEET_WIDTH_1]);
						outs = Convert.ToInt32(row[PastOrder.POL_OUTS_1].ToString());
						desired = Convert.ToInt32(row[PastOrder.POL_SEGMENT_SHEET_COUNT_1].ToString());
						produced = Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_1].ToString()) * outs;
            //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
						/*
            badPlusDefault += Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_BAD_1].ToString());
						badPlusDefault += Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_DEFAULT_1].ToString());
						badPlusDefault *= outs;
            */
            badSheets = Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_BAD_1].ToString()) * outs;
            defaultCuts = Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_DEFAULT_1].ToString());
            defaultSheets = defaultCuts * outs;
          
            //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
            scrapMadeDischargeGroup = Convert.ToInt32(row[PastOrder.POL_SCRAP_MADE_DISCHARGE_GROUP_1].ToString());
          }
					catch (Exception ex)
					{
						Logger.LogError(this, "showOrder - error converting order 1 data.", ex);
					}
					this.ttb_Format1.Value = (width / 10).ToString() + FORMAT_SEPARATOR +
						(length / 10).ToString() + FORMAT_SEPARATOR +
						outs.ToString();
          //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
					/*
					this.ttb_Sheets1.Value = desired.ToString() + SHEETS_SEPARATOR +
						produced.ToString() + SHEETS_SEPARATOR +
						badPlusDefault.ToString();
          */
          //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
          if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].DischargeGroup ||
              SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].DischargeGroup ||
              SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].DischargeGroup)
          {

            this.ttb_Sheets1.Value = desired.ToString() + SHEETS_SEPARATOR +
              produced.ToString() + SHEETS_SEPARATOR +
              badSheets.ToString() + SHEETS_SEPARATOR +
              (scrapMadeDischargeGroup + (defaultCuts * Convert.ToInt32(row[PastOrder.POL_CUT_LENGTH_DEFAULT_1].ToString()))).ToString();
          }
          else
          {
            this.ttb_Sheets1.Value = desired.ToString() + SHEETS_SEPARATOR +
              produced.ToString() + SHEETS_SEPARATOR +
              badSheets.ToString()
              + " + " + defaultSheets.ToString() + " (" + defaultCuts.ToString() + ")";
          }
        }

				//ORDER 2
				if (int.Parse(row[PastOrder.POL_OUTS_2].ToString()) > 0)
				{
					ttl_ord2.ForeColor = Color.LightGreen;
					this.ttb_OrderNumber2.Value = row[PastOrder.POL_ORDER_NUMBER_2].ToString();
          this.mkt_Delivery2.ValueAsUNIVERSALDateTime = (DateTime)row[PastOrder.POL_DELIVERY_2];
					this.ttb_CustName2.Value = row[PastOrder.POL_CUSTOMER_NAME_2].ToString();
					this.ttb_Measures2.Value = Measures_dbToComponent(row[PastOrder.POL_SCORE_MEASURES_2].ToString());
					this.cmb_Positioning2.SelectedItemData = row[PastOrder.POL_POSIT_TYPE_2].ToString();
					this.cmb_Type2.SelectedItem = row[PastOrder.POL_SCORE_TYPE_2].ToString();
					this.cmb_Level2.SelectedItemData = row[PastOrder.POL_LEVEL_NAME_2].ToString();
					this.nud_sheetsXstack2.Value = row[PastOrder.POL_SHEETS_PER_STACK_2].ToString();
					this.nud_stacksXpallet2.Value = row[PastOrder.POL_STACKS_PER_PALLET_2].ToString();
					this.cmb_sending2.SelectedItemData = row[PastOrder.POL_SENDING_2].ToString();
          //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
          /*
					length = width = outs = desired = produced = badPlusDefault = 0;
          */
          length = width = outs = desired = produced = badSheets = 0;
          this.chk_CutToMark2.Checked = Convert.ToBoolean(row[PastOrder.POL_CUT_TO_MARK_2].ToString());

          //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
          defaultSheets = 0;
          defaultCuts = 0;
          scrapMadeDischargeGroup = 0;

          try
					{
						length = Convert.ToInt32(row[PastOrder.POL_SHEET_LENGTH_2]);
						width = Convert.ToInt32(row[PastOrder.POL_SHEET_WIDTH_2]);
						outs = Convert.ToInt32(row[PastOrder.POL_OUTS_2].ToString());
						desired = Convert.ToInt32(row[PastOrder.POL_SEGMENT_SHEET_COUNT_2].ToString());
						produced = Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_2].ToString()) * outs;
            //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
            /*
						badPlusDefault += Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_BAD_2].ToString());
						badPlusDefault += Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_DEFAULT_2].ToString());
						badPlusDefault *= outs;
            */
            badSheets = Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_BAD_2].ToString()) * outs;
            defaultCuts = Convert.ToInt32(row[PastOrder.POL_CUTS_PRODUCED_DEFAULT_2].ToString());
            defaultSheets = defaultCuts * outs;
          
            //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
            scrapMadeDischargeGroup = Convert.ToInt32(row[PastOrder.POL_SCRAP_MADE_DISCHARGE_GROUP_2].ToString());
          }
					catch (Exception ex)
					{
						Logger.LogError(this, "showOrder - error converting order 2 data.", ex);
					}
					this.ttb_Format2.Value = (width / 10).ToString() + FORMAT_SEPARATOR +
						(length / 10).ToString() + FORMAT_SEPARATOR +
						outs.ToString();
          //PMASSARO_FI01/04/11 - Rivista Visualizzazione Dei Fogli Errati+Default Nel Dettaglio Ordini Eeguiti
          /*
					this.ttb_Sheets2.Value = desired.ToString() + SHEETS_SEPARATOR +
						produced.ToString() + SHEETS_SEPARATOR +
						badPlusDefault.ToString();
          */
          //RFAZZI_FI07/06/2011_1. Aggiunti contatori scarto fatto alla Master con il gruppo di Scarto. 
          if (SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[0].DischargeGroup ||
              SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[1].DischargeGroup ||
              SyncroC.DBManager.getDBManager().lineConfig.KnifeConfig.KnifeLevelSettings[2].DischargeGroup)
          {

            this.ttb_Sheets2.Value = desired.ToString() + SHEETS_SEPARATOR +
              produced.ToString() + SHEETS_SEPARATOR +
              badSheets.ToString() + SHEETS_SEPARATOR +
              (scrapMadeDischargeGroup + (defaultCuts * Convert.ToInt32(row[PastOrder.POL_CUT_LENGTH_DEFAULT_2].ToString()))).ToString();
          }
          else
          {
            this.ttb_Sheets2.Value = desired.ToString() + SHEETS_SEPARATOR +
              produced.ToString() + SHEETS_SEPARATOR +
              badSheets.ToString()
              + " + " + defaultSheets.ToString() + " (" + defaultCuts.ToString() + ")";
          }
        }

        if (SyncroC.DBManager.getDBManager().syncroConfig.GeneralSetting.SQL_USED_FOR_DATABASE_MANAGEMENT)
        {
          Application.DoEvents();
          this.showGraph((int)row[PastOrder.POL_SETUP_ID], (DateTime)row[PastOrder.POL_BEGIN_TIME], (DateTime)row[PastOrder.POL_FINISH_TIME]);
        }
			}

      this.Cursor = Cursors.Arrow;
		}

		/// <summary>
		/// Clean the value of every component of the page.
		/// </summary>
		private void cleanAllFields(bool cleanLists)
		{
			SetupId.Text = string.Empty;
			ModId.Text = string.Empty;
			//HEADER
			ttb_ProgramNumber.Value = String.Empty;
			ttb_SetUpSequence.Value = String.Empty;
			if (cleanLists)
			{
				cmb_Flute.UnSelectItem();
				cmb_Quality.UnSelectItem();
				cmb_TrimType.UnSelectItem();
			}
			nud_WebWidth.Value = String.Empty;
			nud_Trim.Value = String.Empty;
			nud_ScoreGap.Value = String.Empty;
			nud_SetupLength.Value = string.Empty;

			//ORDER 0
			ttl_ord0.ForeColor = Color.WhiteSmoke;
			ttb_OrderNumber0.Value = String.Empty;
			ttb_CustName0.Value = String.Empty;
			cmb_sending0.UnSelectItem();
			mkt_Delivery0.Value = String.Empty;
			ttb_Format0.Value = String.Empty;
			ttb_Sheets0.Value = String.Empty;
			nud_sheetsXstack0.Value = String.Empty;
			nud_stacksXpallet0.Value = String.Empty;
			cmb_Level0.UnSelectItem();
			cmb_Positioning0.UnSelectItem();
			cmb_Type0.UnSelectItem();
			ttb_Measures0.Value = String.Empty;
			this.chk_CutToMark0.Checked = false;

			//ORDER 1
			ttl_ord1.ForeColor = Color.WhiteSmoke;
			ttb_OrderNumber1.Value = String.Empty;
			ttb_CustName1.Value = String.Empty;
			cmb_sending1.UnSelectItem();
			mkt_Delivery1.Value = String.Empty;
			ttb_Format1.Value = String.Empty;
			ttb_Sheets1.Value = String.Empty;
			nud_sheetsXstack1.Value = String.Empty;
			nud_stacksXpallet1.Value = String.Empty;
			cmb_Level1.UnSelectItem();
			cmb_Positioning1.UnSelectItem();
			cmb_Type1.UnSelectItem();
			ttb_Measures1.Value = String.Empty;
			this.chk_CutToMark1.Checked = false;

			//ORDER 2
			ttl_ord2.ForeColor = Color.WhiteSmoke;
			ttb_OrderNumber2.Value = String.Empty;
			ttb_CustName2.Value = String.Empty;
			cmb_sending2.UnSelectItem();
			mkt_Delivery2.Value = String.Empty;
			ttb_Format2.Value = String.Empty;
			ttb_Sheets2.Value = String.Empty;
			nud_sheetsXstack2.Value = String.Empty;
			nud_stacksXpallet2.Value = String.Empty;
			cmb_Level2.UnSelectItem();
			cmb_Positioning2.UnSelectItem();
			cmb_Type2.UnSelectItem();
			ttb_Measures2.Value = String.Empty;
			this.chk_CutToMark2.Checked = false;
		}

		/// <summary>
		/// Prepares a string measure as it is stored in the database to be passed to a titledTextBox to be showed
		/// </summary>
		private string Measures_dbToComponent(string measures)
		{
			string s = SyncroConverter.ConvertScoreMeasures(measures, UnitMeasure.DeciMilliMetre, UnitMeasure.MilliMetre);
			s = s.Replace("   ", " ");
			s = s.Replace("  ", " ");
			s = s.Replace(" ", "\r\n");
			return s;
		}

		/// <summary>
		/// Fill the items of the Flute list using the flute database.
		/// </summary>
		/// <param name="indexToSelect">Index of the item to select into the list.</param>
		/// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
		private void setListFluteItems(int indexToSelect, bool Clean)
		{
			if (Clean)
				this.cmb_Flute.ClearItems();
      DataTable flute = DBManager.getDBManager().flute.getTable();
			for (int i = 0; i < flute.Rows.Count; i++)
			{
				if (!cmb_Flute.Contains(flute.Rows[i][Flute.FLUTE_ID].ToString()))
					this.cmb_Flute.AddItem(flute.Rows[i][Flute.FLUTE_ID].ToString());
			}
		}

		/// <summary>
		/// Fill the specified TitledList with the constant names of the specified Enum.
		/// </summary>
		/// <param name="titledList">The TitledList to be filled with the items.</param>
		/// <param name="type">Enum to get the constant names from.</param>
		/// <param name="includeMask">Specifie which constants has to be included into the list.</param>
		/// <param name="indexToSelect">Index of the item to select into the list.</param>
		/// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
		private void setTitledListItems(SyncroC.Controls.TitledList titledList, Type type, bool[] includeMask, int indexToSelect, bool Clean)
		{
			if (type == null || !type.IsEnum)
			{
				Logger.LogError(this, "setTitledListItems - invalid parameter type: it must be an Enum");
				return;
			}
			string[] items = Enum.GetNames(type);
			if (items.Length != includeMask.Length)
			{
				Logger.LogError(this, "setTitledListItems - parameter \"type\" and parameter \"includeMask\" are incompatibles: they must have same length.");
				return;
			}
			Language lang = DBManager.getDBManager().language;
			string typeName = type.Name + ".";
			if (Clean)
				titledList.ClearItems();
			for (int i = 0; i < items.Length; i++)
			{
				if (includeMask[i] &&
					(!titledList.Contains(items[i])))
					titledList.AddItem(lang.getTranslation(typeName + items[i]), items[i]);
			}
			if (indexToSelect >= 0 && indexToSelect <= titledList.Count - 1 && Clean)
				titledList.SelectedIndex = indexToSelect;
		}

		/// <summary>
		/// Fill the specified TitledList with the scorer positioning type according to the current line configuration.
		/// </summary>
		/// <param name="titledList"></param>
		/// <param name="indexToSelect"></param>
		/// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
		private void setListPositioningItems(SyncroC.Controls.TitledList titledList, int indexToSelect, bool Clean)
		{
			Language lang = DBManager.getDBManager().language;
			string typeName = typeof(SyncroC.Utility.PositType).Name + ".";
			if (Clean)
				titledList.ClearItems();
      ScoreType[] scoreType = DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.ScoreTypes;
			string[] positTypes = Enum.GetNames(typeof(SyncroC.Utility.PositType));
			for (int i = 0; i < scoreType.Length; i++)
			{
				for (int j = 1; j < positTypes.Length; j++) //j starts from 1 because the 0 enum is "NoPositType"
				{
					if (ScorerProfileType.IsPositioningAvailable(scoreType[i].Scorer.SClass, (PositType)j) && (!titledList.Contains(positTypes[j])))
            if (DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.PositioningOptions.Scores.ShowNormalReverse || ((PositType)j != PositType.NormalReverse))
							titledList.AddItem(lang.getTranslation(typeName + positTypes[j]), positTypes[j]);
				}
			}
			if (indexToSelect >= 0 && indexToSelect <= titledList.Count - 1 && Clean)
				titledList.SelectedIndex = indexToSelect;
		}

		/// <summary>
		/// Fill the specified TitledList with the scorer database according to the selected Posit Type
		/// </summary>
		/// <param name="titledList">The titledList to fill.</param>
		/// <param name="indexToSelect">Index of the item to select into the list.</param>
		/// <param name="Clean">Determine if the items already present into the list should be cleaned or not before new valorization and if the selection has to be eventually changed.</param>
		private void setListScorerItems(SyncroC.Controls.TitledList titledList, string SelectedPositType, int indexToSelect, bool Clean)
		{
			bool all = false;
			if (Clean)
				titledList.ClearItems();
			PositType positType = PositType.NoPositType;
			if (SelectedPositType == null || SelectedPositType.Equals(String.Empty)/*?? || SelectedPositType == NoPositType???*/)
				all = true;
			else
				positType = (PositType)Enum.Parse(typeof(SyncroC.Utility.PositType), SelectedPositType);
      ScoreType[] scoreType = DBManager.getDBManager().lineConfig.SlitterScorerConfig.SlitterScorerHeader.ScoreTypes;
			for (int i = 0; i < scoreType.Length; i++)
			{
				if ((ScorerProfileType.IsPositioningAvailable(scoreType[i].Scorer.SClass, positType) || all) &&
					(scoreType[i].Letter != ' ') && (!titledList.Contains(scoreType[i].Letter.ToString())))
				{
					titledList.AddItem(scoreType[i].Letter.ToString());
				}
			}
			if (indexToSelect >= 0 && indexToSelect <= titledList.Count - 1 && Clean)
				titledList.SelectedIndex = indexToSelect;
			else if (titledList.SelectedIndex == -1)
				titledList.SelectedIndex = 0;
		}

    private void btn_ShowGraph_Click(object sender, EventArgs e)
    {
      DateTime dateTime = DateTime.MinValue;
      try
      {
        dateTime = this.mkt_BeginTime.ValueAsUNIVERSALDateTime;
      }
      catch (Exception ex)
      {
        Logger.LogError(this, "gdv_Orders_DoubleClick - error converting value.", ex);
      }

      SyncroC.Forms.Pages.Statistics.Frm_ProductionTrace.from_Universal = dateTime;

      askForPage(typeof(SyncroC.Forms.Pages.Statistics.Frm_ProductionTrace));
    }

    private int page = 0;
    private void printPanel_Click(object sender, EventArgs e)
    {
      if (SyncroC.DataBase.PRCManager.getPRCManager().canPrint())
      {
        System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();
        doc.PrintController = new System.Drawing.Printing.StandardPrintController();
        doc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(doc_PrintPage);
        doc.DefaultPageSettings.Landscape = true;
        this.page = 0;
        doc.Print();
      }
    }

    void doc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
      switch (page)
      {
        case 0:
          {
            Bitmap[] bitmaps = new Bitmap[2];
            this.pnl_Main.AutoScrollPosition = new Point(0, 0);
            Application.DoEvents();
            bitmaps[0] = SyncroC.Utility.SyncroImage.createSnapShot(this.pnl_Main);
            this.pnl_Main.ScrollControlIntoView(this.ttl_rolls);
            Application.DoEvents();
            bitmaps[1] = SyncroC.Utility.SyncroImage.createSnapShot(this.ttl_rolls);
            Bitmap res = SyncroC.Utility.SyncroImage.CombineBitmaps(bitmaps);
            e.Graphics.DrawImage(res, 0, 0, e.PageSettings.PrintableArea.Height, res.Height);
            e.HasMorePages = true;
            break;
          }
        case 1:
          {
            if (SyncroC.DBManager.getDBManager().lineConfig.PRCConfig.Header.EnableBottom_UpperMoistureSensor)
            {
              this.pnl_Main.ScrollControlIntoView(this.ttl_RECORD);
              Application.DoEvents();
              Bitmap res = SyncroC.Utility.SyncroImage.createSnapShot(this.ttl_RECORD);
              e.Graphics.DrawImage(res, 0, 0, e.PageSettings.PrintableArea.Height, res.Height);
              e.HasMorePages = true;
              break;
            }
            else
            {
              page++;
              goto case 2;
            }
          }
          
        case 2:
          {
            this.pnl_Main.ScrollControlIntoView(this.ttl_Zone0);
            Application.DoEvents();
            Bitmap res = SyncroC.Utility.SyncroImage.createSnapShot(this.ttl_Zone0);
            e.Graphics.DrawImage(res, 0, 0, e.PageSettings.PrintableArea.Height, res.Height);
            e.HasMorePages = true;
            break;
          }
        case 3:
          {
            this.pnl_Main.ScrollControlIntoView(this.ttl_Zone1);
            Application.DoEvents();
            Bitmap res = SyncroC.Utility.SyncroImage.createSnapShot(this.ttl_Zone1);
            e.Graphics.DrawImage(res, 0, 0, e.PageSettings.PrintableArea.Height, res.Height);
            e.HasMorePages = true;
            break;
          }
        case 4:
          {
            this.pnl_Main.ScrollControlIntoView(this.ttl_Zone2);
            Application.DoEvents();
            Bitmap res = SyncroC.Utility.SyncroImage.createSnapShot(this.ttl_Zone2);
            e.Graphics.DrawImage(res, 0, 0, e.PageSettings.PrintableArea.Height, res.Height);
            e.HasMorePages = false;
            break;
          }
      }

      page++;
    }
	}
}